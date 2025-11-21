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
    public partial class FormStockStatus : Form
    {
        // 불러온거 저장용
        private List<StockLot> _originalData = new List<StockLot>();
        public FormStockStatus()
        {
            InitializeComponent();
        }
        private void FormStockStatus_Load(object sender, EventArgs e)
        {
            LoadStockStatus();
            LoadCategoryFilter();
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            tbSearchName.Text = "";
            cbxCategoryFilter.SelectedIndex = 0;
            chkExpired.Checked = false;
            chkImpending.Checked = false;
            LoadStockStatus();
        }
        private void LoadStockStatus()
        {
            StockLotRepository stockRepo = new StockLotRepository();
            dgvStockStatus.DataSource = stockRepo.GetAllStockLots();
            
            _originalData = stockRepo.GetAllStockLots();
            ApplyFilter();

            CustomizeGrid();  // 너뮤길어서 메서드로 빼는 방법써보기
        }

        private void ApplyFilter() // 나중에 수정필요...
        {
            var query = _originalData.AsEnumerable();

            if (cbxCategoryFilter.SelectedIndex > 0)
            {
                string selectedCat = cbxCategoryFilter.Text;
                query = query.Where(s => s.CategoryName == selectedCat);
            }

            string searchText = tbSearchName.Text.Trim();
            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(s => s.ProductName.IndexOf(searchText, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            // 체크밬스 전용
            DateTime today = DateTime.Today;
            bool showExpired = chkExpired.Checked;
            bool showImpending = chkImpending.Checked;

            if (showExpired || showImpending)
            {
                query = query.Where(s =>
                    (showExpired && (s.ExpirationDate.Date - today).Days < 0) ||
                    (showImpending && (s.ExpirationDate.Date - today).Days >= 0 && (s.ExpirationDate.Date - today).Days <= 3)
                );
            }

            List<StockLot> filteredList = query.ToList();
            dgvStockStatus.DataSource = null;
            dgvStockStatus.DataSource = filteredList;

            CustomizeGrid();
        }
        private void CustomizeGrid()
        {
            dgvStockStatus.Columns["ProductId"].Visible = false;
            dgvStockStatus.Columns["SupplierId"].Visible = false;

            dgvStockStatus.Columns["CategoryName"].HeaderText = "구분";
            dgvStockStatus.Columns["CategoryName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvStockStatus.Columns["LotId"].HeaderText = "재고번호";
            dgvStockStatus.Columns["LotId"].DisplayIndex = 0;
            dgvStockStatus.Columns["LotId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvStockStatus.Columns["ProductName"].HeaderText = "상품명";
            dgvStockStatus.Columns["ProductName"].DisplayIndex = 1;
            dgvStockStatus.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvStockStatus.Columns["Quantity"].HeaderText = "현재고";
            dgvStockStatus.Columns["Quantity"].DisplayIndex = 2;
            dgvStockStatus.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

            dgvStockStatus.Columns["ExpirationDate"].HeaderText = "유통기한";
            dgvStockStatus.Columns["ExpirationDate"].DisplayIndex = 3;

            dgvStockStatus.Columns["SupplierName"].HeaderText = "매입처";
            dgvStockStatus.Columns["SupplierName"].DisplayIndex = 4;

            dgvStockStatus.Columns["PurchasePrice"].HeaderText = "매입가";

            dgvStockStatus.Columns["Quantity"].DefaultCellStyle.Format = "N0";
            
        }

        private void dgvStockStatus_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvStockStatus.Rows[e.RowIndex].Cells["ExpirationDate"].Value == null) return;
            if (dgvStockStatus.Rows[e.RowIndex].Cells["ExpirationDate"].Value == DBNull.Value) return;

            DateTime expDate = Convert.ToDateTime(dgvStockStatus.Rows[e.RowIndex].Cells["ExpirationDate"].Value);
            int daysLeft = (expDate.Date - DateTime.Today).Days; // 남은일수

            if (daysLeft < 0) // 만료
            {
                e.CellStyle.BackColor = Color.MistyRose;
                e.CellStyle.ForeColor = Color.Red;
                e.CellStyle.SelectionBackColor = Color.Firebrick; // 선택했을 때
                e.CellStyle.SelectionForeColor = Color.White;
            }
            else if (daysLeft <= 3) // 임박
            {
                e.CellStyle.BackColor = Color.LemonChiffon;
                e.CellStyle.ForeColor = Color.Peru;
                e.CellStyle.SelectionBackColor = Color.Gold;
                e.CellStyle.SelectionForeColor = Color.Black;
            }
            // 나머지는 그대로

        }

        private void LoadCategoryFilter()
        {
            CategoryRepository catRepo = new CategoryRepository();
            List<Category> categories = catRepo.GetAllCategories();
            categories.Insert(0, new Category { CategoryName = "전체", CategoryId = 0 });

            cbxCategoryFilter.DataSource = categories;
            cbxCategoryFilter.DisplayMember = "CategoryName";
            cbxCategoryFilter.ValueMember = "CategoryId";
            cbxCategoryFilter.SelectedIndex = 0;
        }

        private void cbxCategoryFilter_SelectedIndexChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void tbSearchName_TextChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void chkImpending_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }
        private void chkExpired_CheckedChanged(object sender, EventArgs e)
        {
            ApplyFilter();
        }


    }  // class
}
