using StockManager.Models; // 데이터 저장소 기억하기
using StockManagerDAL; // DB 전담 기억하기
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
    public partial class FormCategory : Form
    {
        public FormCategory()
        {
            InitializeComponent();
        }

        private void FormCategory_Load(object sender, EventArgs e)
        {
            // 카테고리 전담
            CategoryRepository categoryRepo = new CategoryRepository();

            // 모든 카테고리 목록을 가져오기
            List<Category> categories = categoryRepo.GetAllCategories();

            // 받아온 목록을 dgv에 뿌려주기
            dgvCategories.DataSource = categories;

            dgvCategories.Columns["CategoryId"].HeaderText = "카테고리ID";
            dgvCategories.Columns["CategoryName"].HeaderText = "카테고리명";

            dgvCategories.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill; // 알아서 채우기

            if (GlobalContext.CurrentUser.Role != "Admin")
            {
                btnSaveCategory.Enabled = false;
                btnUpdate.Enabled = false;
                btnDelete.Enabled = false;
                this.Text += " (조회 전용)";
            }
        }

        private void btnSaveCategory_Click(object sender, EventArgs e)
        {
            // 오입력 방지
            string categoryName = tbCategoryName.Text.Trim();
            if (string.IsNullOrEmpty(categoryName))
            {
                MessageBox.Show("카테고리명을 입력해주세요!", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // 실제 입력
            try
            {
                Category newCategory = new Category();
                newCategory.CategoryName = categoryName;

                CategoryRepository categoryRepo = new CategoryRepository();

                bool success = categoryRepo.AddNewCategory(newCategory);

                if (success)
                {
                    MessageBox.Show("카테고리가 성공적으로 등록되었습니다!", "저장 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);                   
                    FormCategory_Load(sender, e);
                    tbCategoryName.Text = "";
                }
                else
                {
                    MessageBox.Show("카테고리 등록에 실패했습니다...", "저장 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 2627 || sqlEx.Number == 2601)
                {
                    MessageBox.Show("이미 존재하는 카테고리명입니다!", "중복 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    MessageBox.Show("DB 저장 중 오류가 발생했습니다 : " + sqlEx.Message, "DB 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("알 수 없는 오류가 발생했습니다 : " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string categoryName = tbCategoryName.Text.Trim();
            int categoryId;

            if (!int.TryParse(tbSelectedCat.Text, out categoryId))
            {
                MessageBox.Show("수정할 카테고리를 먼저 선택해주세요!", "선택 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(categoryName))
            {
                MessageBox.Show("카테고리명을 입력해주세요!", "입력 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Category categoryToUpdate = new Category();
                categoryToUpdate.CategoryId = categoryId;
                categoryToUpdate.CategoryName = categoryName;

                CategoryRepository categoryRepo = new CategoryRepository();
                bool success = categoryRepo.UpdateCategory(categoryToUpdate);

                // 결과 피드백
                if (success)
                {
                    MessageBox.Show("카테고리가 성공적으로 수정되었습니다!", "수정 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    FormCategory_Load(sender, e); // 그리드 새로고침
                }
                else
                {
                    MessageBox.Show("카테고리 수정에 실패했습니다...", "수정 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("수정 중 오류가 발생했습니다 : " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // 삭제는 신중하게 @_@
        private void btnDelete_Click(object sender, EventArgs e)
        {
            int categoryId;
            if (!int.TryParse(tbSelectedCat.Text, out categoryId))
            {
                MessageBox.Show("삭제할 카테고리를 먼저 선택해주세요!", "선택 오류", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = "정말로 이 카테고리를 삭제하시겠습니까?\n\n이 카테고리에 연결된 상품 정보가 영향을 받을 수 있습니다.\n이 작업은 되돌릴 수 없습니다.";
            DialogResult result = MessageBox.Show(message, "삭제 확인", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

            // Yes를 누른 경우에만 삭제
            if (result == DialogResult.Yes)
            {
                try
                {
                    CategoryRepository categoryRepo = new CategoryRepository();
                    bool success = categoryRepo.DeleteCategory(categoryId);

                    if (success)
                    {
                        MessageBox.Show("카테고리가 삭제되었습니다!", "삭제 완료", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        FormCategory_Load(sender, e); 
                        tbCategoryName.Text = ""; 
                        tbSelectedCat.Text = ""; 
                    }
                    else
                    {
                        MessageBox.Show("카테고리 삭제에 실패했습니다!", "삭제 실패", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (SqlException sqlEx)
                {
                    // 이부분 추가 // 외래키 오류 관련...
                    if (sqlEx.Number == 547)
                    {
                        MessageBox.Show("이 카테고리를 참조하는 상품(Products)이 존재하여 삭제할 수 없습니다.\n\n먼저 연결된 상품을 삭제하거나 다른 카테고리로 변경해주세요.", "삭제 실패 (참조 오류)", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        MessageBox.Show("DB 삭제 중 오류가 발생했습니다 : " + sqlEx.Message, "DB 오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("삭제 중 오류가 발생했습니다 : " + ex.Message, "오류", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void dgvCategories_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // 다른데 클릭하면 무시
            if (e.RowIndex < 0) return;

            // 행을 선택하면 데이터 가져오기
            DataGridViewRow row = dgvCategories.Rows[e.RowIndex];
            tbCategoryName.Text = row.Cells["CategoryName"].Value.ToString();
            tbSelectedCat.Text = row.Cells["CategoryId"].Value.ToString();
        }
        private void btnRefresh_Click_1(object sender, EventArgs e)
        {
            tbCategoryName.Text = "";
            tbSelectedCat.Text = "";
            tbCategoryName.Focus(); // 커서 이동
        }
    }
}
