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
    public partial class FormInbound : Form
    {
        public FormInbound()
        {
            InitializeComponent();
        }

        private void FormInbound_Load(object sender, EventArgs e)
        {
            LoadProductComboBox();
            LoadStockLotsGrid();
            LoadSupplierComboBox();
        }

        private void LoadProductComboBox()
        {
            ProductRepository productRepo = new ProductRepository();
            cbxProduct.DataSource = productRepo.GetAllProducts();
            cbxProduct.DisplayMember = "ProductName";
            cbxProduct.ValueMember = "ProductId";
            cbxProduct.SelectedIndex = -1; // 기본 선택 ㄴㄴ
        }

        private void LoadStockLotsGrid()
        {
            StockLotRepository stockRepo = new StockLotRepository();
            dgvStockLots.DataSource = stockRepo.GetAllStockLots();

            dgvStockLots.Columns["ProductId"].Visible = false;
            dgvStockLots.Columns["SupplierId"].Visible = false;

            dgvStockLots.Columns["LotId"].HeaderText = "입고 번호";
            dgvStockLots.Columns["ProductName"].HeaderText = "상품명";
            dgvStockLots.Columns["Quantity"].HeaderText = "입고 수량";
            dgvStockLots.Columns["PurchasePrice"].HeaderText = "매입 단가";
            dgvStockLots.Columns["ExpirationDate"].HeaderText = "유통기한";
            dgvStockLots.Columns["SupplierName"].HeaderText = "매입처";

            dgvStockLots.Columns["LotId"].DisplayIndex = 0;
            dgvStockLots.Columns["ProductName"].DisplayIndex = 1;
            dgvStockLots.Columns["Quantity"].DisplayIndex = 2;
            dgvStockLots.Columns["PurchasePrice"].DisplayIndex = 3;
            dgvStockLots.Columns["ExpirationDate"].DisplayIndex = 4;
            dgvStockLots.Columns["SupplierName"].DisplayIndex = 5;

            dgvStockLots.Columns["PurchasePrice"].DefaultCellStyle.Format = "N0";
            dgvStockLots.Columns["ProductName"].Width = 198;
            dgvStockLots.Columns["ExpirationDate"].Width = 110;
        }

        private void btnInbound_Click(object sender, EventArgs e)
        {
            // 매입처, 상품선택 확인 둘다해야함
            if (cbxSupplier.SelectedValue == null)
            {
                MessageBox.Show("매입처를 선택하세요!");
                return;
            }
            if (cbxProduct.SelectedValue == null)
            {
                MessageBox.Show("상품을 선택하세요!");
                return;
            }
            if (numQuantity.Value <= 0)
            {
                MessageBox.Show("수량은 1 이상이어야 합니다!");
                return;
            }

            StockLot newLot = new StockLot();
            newLot.ProductId = (int)cbxProduct.SelectedValue;
            newLot.Quantity = (int)numQuantity.Value;
            newLot.PurchasePrice = numPrice.Value;
            newLot.ExpirationDate = dtpExpirationDate.Value;
            newLot.SupplierId = (int)cbxSupplier.SelectedValue;

            // 입고번호 받는걸루
            StockLotRepository stockRepo = new StockLotRepository();
            int newLotId = stockRepo.AddNewStockLot(newLot);
            if (newLotId > 0)
            {
                try
                {
                    TransactionRepository txRepo = new TransactionRepository();
                    Transaction newTx = new Transaction();

                    newTx.LotId = newLotId;
                    newTx.UserId = GlobalContext.CurrentUser.UserId;// 임시 관리자 ID <- 나중에 로그인 구현하기
                    newTx.TransactionType = "IN";        
                    newTx.Quantity = newLot.Quantity;    
                    newTx.TransactionDate = DateTime.Now;

                    // 이력 저장 실행!
                    txRepo.AddNewTransaction(newTx);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("입고는 되었으나 이력 기록 중 오류 발생: " + ex.Message);
                }

                MessageBox.Show("입고 처리가 완료되었습니다!");
                LoadStockLotsGrid(); // 새로고침

                // 입력 칸 초기화 코드 추가?
            }
            else
            {
                MessageBox.Show("입고 처리에 실패했습니다...");
            }
        }
        private void LoadSupplierComboBox()
        {
            CustomerRepository customerRepo = new CustomerRepository();
            cbxSupplier.DataSource = customerRepo.GetCustomersByType("매입처");

            cbxSupplier.DisplayMember = "CustomerName";
            cbxSupplier.ValueMember = "CustomerId";
            cbxSupplier.SelectedIndex = -1;
        }
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

        ///
    }
}
