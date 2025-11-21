using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using StockManager.Models;
using StockManagerDAL;

namespace StockManager_1111
{
    public partial class FormOutbound : Form
    {
        public FormOutbound()
        {
            InitializeComponent();
        }

        private void FormOutbound_Load(object sender, EventArgs e)
        {
            LoadCustomerComboBox();
            LoadProductComboBox();
            dgvStockLots.DataSource = null;
        }

        private void LoadCustomerComboBox()
        {
            CustomerRepository customerRepo = new CustomerRepository();

            // 매출처만
            cbxCustomer.DataSource = customerRepo.GetCustomersByType("매출처");
            cbxCustomer.DisplayMember = "CustomerName";
            cbxCustomer.ValueMember = "CustomerId";
            cbxCustomer.SelectedIndex = -1;
        }
        private void LoadProductComboBox()
        {
            ProductRepository productRepo = new ProductRepository();
            cbxProduct.DataSource = productRepo.GetAllProducts();
            cbxProduct.DisplayMember = "ProductName";
            cbxProduct.ValueMember = "ProductId";
            cbxProduct.SelectedIndex = -1;
        }

        private void cbxCustomer_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SearchAvailableStock();
        }

        private void cbxProduct_SelectionChangeCommitted(object sender, EventArgs e)
        {
            SearchAvailableStock();
        }
        // 함수 넘 어려워서 일단 보류...
        private void SearchAvailableStock()
        {
            // 일단 둘다 선택
            if (cbxCustomer.SelectedValue == null || cbxProduct.SelectedValue == null) return;

            int customerId = (int)cbxCustomer.SelectedValue;
            int productId = (int)cbxProduct.SelectedValue;

            // 규칙 조회
            CustomerRuleRepository ruleRepo = new CustomerRuleRepository();
            int redwDays = ruleRepo.GetRedwDays(customerId, productId);

            if (redwDays == -1)
            {
                lblRedwInfo.Text = "설정된 규칙 없음 (모든 재고 조회)";
                lblRedwInfo.ForeColor = Color.MediumVioletRed;
                redwDays = 0; // 규칙이 없으면 0일 이상 남은것(즉, 유통기한 안 지난 모든 것) 조회
            }
            else
            {
                lblRedwInfo.Text = $"규칙: 유통기한 {redwDays}일 이상 남은 제품만 납품 가능";
                lblRedwInfo.ForeColor = Color.SteelBlue;
            }

            // 조건에 맞는 재고만 조회
            StockLotRepository stockRepo = new StockLotRepository();
            List<StockLot> validLots = stockRepo.GetValidStockLots(productId, redwDays);

            dgvStockLots.DataSource = validLots;

            // 결과가 없으면 알림
            if (validLots.Count == 0)
            {
                MessageBox.Show("조건을 만족하는 재고가 없습니다!\n(유통기한이 너무 짧거나 재고가 부족합니다...)");
            }

            // 꾸미는 부분!
            dgvStockLots.Columns["ProductId"].Visible = false;
            dgvStockLots.Columns["SupplierId"].Visible = false;

            dgvStockLots.Columns["LotId"].HeaderText = "재고 번호";
            dgvStockLots.Columns["ProductName"].HeaderText = "상품명";
            dgvStockLots.Columns["Quantity"].HeaderText = "현재 재고";
            dgvStockLots.Columns["ExpirationDate"].HeaderText = "유통기한";
            dgvStockLots.Columns["PurchasePrice"].HeaderText = "매입 단가";
            dgvStockLots.Columns["SupplierName"].HeaderText = "매입처";
            dgvStockLots.Columns["CategoryName"].HeaderText = "구분";

            dgvStockLots.Columns["LotId"].DisplayIndex = 0;
            dgvStockLots.Columns["ProductName"].DisplayIndex = 1;
            dgvStockLots.Columns["Quantity"].DisplayIndex = 2;
            dgvStockLots.Columns["ExpirationDate"].DisplayIndex = 3;
            dgvStockLots.Columns["PurchasePrice"].DisplayIndex = 4;
            dgvStockLots.Columns["SupplierName"].DisplayIndex = 5;

            dgvStockLots.Columns["PurchasePrice"].DefaultCellStyle.Format = "N0";
            dgvStockLots.Columns["LotId"].Width = 80;
            dgvStockLots.Columns["Quantity"].Width = 80;
            dgvStockLots.Columns["ExpirationDate"].Width = 80;
            dgvStockLots.Columns["PurchasePrice"].Width = 80;
            if (dgvStockLots.Columns.Contains("ProductName"))
            {
                dgvStockLots.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }

        }

        private void dgvStockLots_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;
            DataGridViewRow row = dgvStockLots.Rows[e.RowIndex];
            tbSelectedLotId.Text = row.Cells["LotId"].Value.ToString();
        }

        private void btnOutbound_Click(object sender, EventArgs e)
        {
            // 헷갈리니까 유효성검사는 선택 재고목록으로 변경
            int lotId;
            if (!int.TryParse(tbSelectedLotId.Text, out lotId))
            {
                MessageBox.Show("출고할 재고를 목록에서 선택해주세요!");
                return;
            }
            int outQuantity = (int)numQuantity.Value;
            if (outQuantity <= 0)
            {
                MessageBox.Show("출고 수량은 1개 이상이어야 합니다!");
                return;
            }

            // 재고 수량 확인 먼저
            int currentStock = Convert.ToInt32(dgvStockLots.CurrentRow.Cells["Quantity"].Value);
            if (outQuantity > currentStock)
            {
                MessageBox.Show($"재고가 부족합니다!\n(현재 재고: {currentStock} 개)");
                return;
            }

            // 실제 출고
            StockLotRepository stockRepo = new StockLotRepository();
            bool success = stockRepo.DecreaseStockQuantity(lotId, outQuantity);

            if (success)
            {
                try
                {
                    TransactionRepository txRepo = new TransactionRepository();
                    Transaction newTx = new Transaction();

                    newTx.LotId = lotId;
                    newTx.UserId = GlobalContext.CurrentUser.UserId;
                    newTx.TransactionType = "OUT"; // out으로 변경
                    newTx.Quantity = outQuantity;
                    newTx.TransactionDate = DateTime.Now;

                    txRepo.AddNewTransaction(newTx);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("재고는 차감되었으나 이력 기록 중 오류 발생: " + ex.Message);
                }

                MessageBox.Show("출고 처리가 완료되었습니다!");

                // 남은 재고 다시 불러오기
                SearchAvailableStock();

                // 입력 초기화
                numQuantity.Value = 1;
                tbSelectedLotId.Text = "";
            }
            else
            {
                MessageBox.Show("출고 처리에 실패했습니다...");
            }
        }  // 출고버튼

        private void btnShowHistory_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is FormHistory)
                {
                    openForm.Activate();
                    return;
                }
            }
            FormHistory formHistory = new FormHistory();
            formHistory.Show();
        }

        // 여기에 추가

    } // 아웃바운드폼 클래스
}
