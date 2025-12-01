using StockManager.Models;
using StockManagerDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManager_1111
{
    public partial class FormProduct : Form
    {
        public FormProduct()
        {
            InitializeComponent();
        }

        private void FormProduct_Load(object sender, EventArgs e)
        {
            // 상품 목록, 카테고리 목록을 불러오기
            LoadProductsGrid();
            LoadCategoryComboBox();  // DB
            LoadDefaultComboBoxes(); // 고정값으로 채우기

            if (GlobalContext.CurrentUser.Role != "Admin") // 어드민아니라면...
            {
                //btnNew.Enabled = false;  
                //btnSave.Enabled = false;   
                //btnUpdate.Enabled = false; 
                btnDelete.Enabled = false; 

                this.Text += " (조회와 입력 전용)";
            }
        }
        private void LoadProductsGrid()
        {
            ProductRepository productRepo = new ProductRepository();

            // 모든 상품 목록을 가져오기 (JOIN 쿼리)
            List<Product> products = productRepo.GetAllProducts();
            dgvProducts.DataSource = products;

            // 헤더 한글로
            dgvProducts.Columns["ProductName"].HeaderText = "상품명";
            dgvProducts.Columns["CategoryName"].HeaderText = "구분";
            dgvProducts.Columns["StorageType"].HeaderText = "보관방법";
            dgvProducts.Columns["Unit"].HeaderText = "단위";
            dgvProducts.Columns["SafetyStock"].HeaderText = "안전재고";
            dgvProducts.Columns["ProductId"].HeaderText = "상품ID";

            // 카테고리ID가 쿼리를 바꿔도 순서가 바뀌지않는 문제 해결...ㅠ_ㅠ
            dgvProducts.Columns["CategoryId"].Visible = false; // 이건 일단 숨김

            dgvProducts.Columns["ProductId"].DisplayIndex = 0;
            dgvProducts.Columns["ProductName"].DisplayIndex = 1;
            dgvProducts.Columns["CategoryName"].DisplayIndex = 2;
            dgvProducts.Columns["StorageType"].DisplayIndex = 5;
            dgvProducts.Columns["Unit"].DisplayIndex = 3;
            dgvProducts.Columns["SafetyStock"].DisplayIndex = 4;

            dgvProducts.Columns["ProductId"].Width = 70;
            dgvProducts.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvProducts.Columns["Unit"].Width = 80;
            dgvProducts.Columns["SafetyStock"].Width = 80;
            dgvProducts.Columns["StorageType"].Width = 80;

            // 판매가만 모아서...
            dgvProducts.Columns["SellingPrice"].HeaderText = "판매가";
            dgvProducts.Columns["SellingPrice"].DefaultCellStyle.Format = "N0"; // 천단위
            dgvProducts.Columns["SellingPrice"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            dgvProducts.Columns["SellingPrice"].Width = 70;


        }
        private void LoadCategoryComboBox()
        {
            CategoryRepository categoryRepo = new CategoryRepository();
            List<Category> categories = categoryRepo.GetAllCategories();

            // 데이터를 연결
            cbxCategory.DataSource = categories;

            // 사용자에게 보여줄 값(Text)
            // 내부적으로 사용할 값(Value)을 설정
            cbxCategory.DisplayMember = "CategoryName"; // 눈에 보일 속성 (예: "유제품")
            cbxCategory.ValueMember = "CategoryId";   // value (예: 1)
        }
        private void LoadDefaultComboBoxes()
        {
            cbxStorageType.Items.Add("실온");
            cbxStorageType.Items.Add("냉장");
            cbxStorageType.Items.Add("냉동");
            cbxStorageType.SelectedIndex = 0; // 기본값은 실온, 인덱스 0

            cbxUnit.Items.Add("EA");
            cbxUnit.Items.Add("kg");
            cbxUnit.Items.Add("box");
            cbxUnit.Items.Add("g");
            cbxUnit.Items.Add("L");
            cbxUnit.Items.Add("ml");
            cbxUnit.SelectedIndex = 0; // "EA"를 기본값으로 선택
        }

        // 저장버튼 : 사용자가 DB에 요청하고 -> DAL에서 쿼리 실행
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbProductName.Text))
            {
                MessageBox.Show("상품명을 입력해주세요!", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return; // 함수 그냥 종료
            }
            if (cbxCategory.SelectedValue == null)
            {
                MessageBox.Show("카테고리를 선택해주세요!", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                // 사용자가 입력한 값 가져오기
                Product newProduct = new Product();
                newProduct.ProductName = tbProductName.Text;
                newProduct.CategoryId = (int)cbxCategory.SelectedValue; // ValueMember로 설정했던 CategoryId가 (int)로 변환되어 들어감
                newProduct.StorageType = cbxStorageType.SelectedItem.ToString();
                newProduct.Unit = cbxUnit.SelectedItem.ToString();
                newProduct.SafetyStock = (int)numSafety.Value; // NumericUpDown의 값은 decimal이라 int로 변환
                newProduct.SellingPrice = (int)numSellingPrice.Value;

                // Repository에 전달
                ProductRepository productRepo = new ProductRepository();

                // DAL에 요청하는 함수 호출
                bool success = productRepo.AddNewProduct(newProduct);

                // 결과 피드백
                if (success)
                {
                    MessageBox.Show("상품이 성공적으로 등록되었습니다!", "저장 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProductsGrid(); // 그리드 새로고침
                }
                else
                {
                    MessageBox.Show("상품 등록에 실패했습니다!", "저장 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // DB 저장 중 발생할 수 있는 모든 예외를 잡습니다.
                MessageBox.Show("저장 중 오류가 발생했습니다 : " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvProducts_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return; // 딴데 클릭 무시

            DataGridViewRow row = dgvProducts.Rows[e.RowIndex];

            tbSelectedProductId.Text = row.Cells["ProductId"].Value.ToString();

            tbProductName.Text = row.Cells["ProductName"].Value.ToString();
            cbxCategory.SelectedValue = row.Cells["CategoryId"].Value;
            cbxStorageType.SelectedItem = row.Cells["StorageType"].Value.ToString();
            cbxUnit.SelectedItem = row.Cells["Unit"].Value.ToString();
            numSafety.Value = Convert.ToDecimal(row.Cells["SafetyStock"].Value);
            numSellingPrice.Value = Convert.ToDecimal(row.Cells["SellingPrice"].Value);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            int productId;
            if (!int.TryParse(tbSelectedProductId.Text, out productId))
            {
                MessageBox.Show("수정할 상품을 목록에서 먼저 선택해주세요.", "선택 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Product productToUpdate = new Product();
            productToUpdate.ProductId = productId; // ★중요: 수정할 ID
            productToUpdate.ProductName = tbProductName.Text;
            productToUpdate.CategoryId = (int)cbxCategory.SelectedValue;
            productToUpdate.StorageType = cbxStorageType.SelectedItem.ToString();
            productToUpdate.Unit = cbxUnit.SelectedItem.ToString();
            productToUpdate.SafetyStock = (int)numSafety.Value;
            productToUpdate.SellingPrice = (int)numSellingPrice.Value;

            try
            {
                ProductRepository productRepo = new ProductRepository();
                bool success = productRepo.UpdateProduct(productToUpdate);

                if (success)
                {
                    MessageBox.Show("상품 정보가 수정되었습니다!", "수정 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadProductsGrid(); // 목록 새로고침
                }
                else
                {
                    MessageBox.Show("상품 수정에 실패했습니다!", "수정 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("오류 발생 : " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            int productId;
            if (!int.TryParse(tbSelectedProductId.Text, out productId))
            {
                MessageBox.Show("삭제할 상품을 목록에서 먼저 선택해주세요!", "선택 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = "정말로 이 상품 정보를 삭제하시겠습니까?\n\n삭제된 데이터는 복구할 수 없으며,\n이 상품과 관련된 입출고 내역이 있다면\n문제가 발생할 수 있습니다.";
            DialogResult result = MessageBox.Show(message, "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    ProductRepository productRepo = new ProductRepository();

                    // (★ 5단계에서 만들 함수)
                    bool success = productRepo.DeleteProduct(productId);

                    if (success)
                    {
                        MessageBox.Show("상품이 삭제되었습니다!", "삭제 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        LoadProductsGrid(); // 목록 새로고침

                        // 입력 칸 초기화, 편의상 상품명만 지움
                        tbProductName.Text = "";
                        tbSelectedProductId.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("상품 삭제에 실패했습니다!", "삭제 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException sqlEx)
                {
                    // 이미 재고나 거래내역이 있는 경우
                    if (sqlEx.Number == 547)
                    {
                        MessageBox.Show("이 상품은 이미 재고가 등록되어 있거나 거래 내역이 존재하여 삭제할 수 없습니다!", "삭제 불가", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("DB 오류: " + sqlEx.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            tbProductName.Text = "";
            tbSelectedProductId.Text = "";

            // 일단 인덱스 0으로
            if (cbxCategory.Items.Count > 0)
            {
                cbxCategory.SelectedIndex = 0;
            }
            if (cbxStorageType.Items.Count > 0)
            {
                cbxStorageType.SelectedIndex = 0;
            }
            if (cbxUnit.Items.Count > 0)
            {
                cbxUnit.SelectedIndex = 0;
            }

            numSafety.Value = 0;
            tbProductName.Focus();
        }
    }
}
