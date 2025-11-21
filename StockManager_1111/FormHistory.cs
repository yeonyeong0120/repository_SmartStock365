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
    public partial class FormHistory : Form
    {
        public FormHistory()
        {
            InitializeComponent();
        }

        private void FormHistory_Load(object sender, EventArgs e)
        {
            // 툴팁 관련
            ToolTip tooltip = new ToolTip();
            tooltip.InitialDelay = 100;
            tooltip.SetToolTip(tbSearchProduct, "상품명의 일부만 검색해도 조회할 수 있어요!");

            this.AcceptButton = btnSearch; // 코드로 제어하는게 나을듯함

            cbxType.Items.Add("전체");    
            cbxType.Items.Add("입고(IN)");  
            cbxType.Items.Add("출고(OUT)");
            cbxType.SelectedIndex = 0; // 기본은 전체

            dtpStart.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEnd.Value = DateTime.Today;

            LoadHistoryGrid();
        }

        private void LoadHistoryGrid()
        {
            TransactionRepository txRepo = new TransactionRepository();
            List<Transaction> list = txRepo.GetAllTransactions();

            dgvHistory.DataSource = list;

            CustomizeGrid();
        }

        private void CustomizeGrid()
        {
            if (dgvHistory.Rows.Count == 0) return;

            string[] hiddenCols = { "UserId" }; // 유저아이디는 일단 숨김
            foreach (string col in hiddenCols)
            {
                if (dgvHistory.Columns.Contains(col))
                    dgvHistory.Columns[col].Visible = false;
            }

            if (dgvHistory.Columns.Contains("TransactionId")) dgvHistory.Columns["TransactionId"].HeaderText = "거래 번호";
            if (dgvHistory.Columns.Contains("TransactionDate")) dgvHistory.Columns["TransactionDate"].HeaderText = "거래 일시";
            if (dgvHistory.Columns.Contains("TransactionType")) dgvHistory.Columns["TransactionType"].HeaderText = "구분";
            if (dgvHistory.Columns.Contains("ProductName")) dgvHistory.Columns["ProductName"].HeaderText = "상품명";
            if (dgvHistory.Columns.Contains("Quantity")) dgvHistory.Columns["Quantity"].HeaderText = "수량";
            if (dgvHistory.Columns.Contains("LotId")) dgvHistory.Columns["LotId"].HeaderText = "재고번호";
            if (dgvHistory.Columns.Contains("Username")) dgvHistory.Columns["Username"].HeaderText = "담당자";

            dgvHistory.Columns["TransactionDate"].DisplayIndex = 0;
            dgvHistory.Columns["TransactionType"].DisplayIndex = 1;
            dgvHistory.Columns["ProductName"].DisplayIndex = 2;
            dgvHistory.Columns["Quantity"].DisplayIndex = 3;
            dgvHistory.Columns["Username"].DisplayIndex = 4;

            dgvHistory.Columns["TransactionDate"].Width = 120;
            dgvHistory.Columns["TransactionType"].Width = 65;
            dgvHistory.Columns["Quantity"].Width = 70;
            dgvHistory.Columns["LotId"].Width = 80;
            dgvHistory.Columns["TransactionId"].Width = 80;
            dgvHistory.Columns["Username"].Width = 80;
            if (dgvHistory.Columns.Contains("ProductName"))
            {
                dgvHistory.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
            dgvHistory.Columns["TransactionDate"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";

            Font boldFont = new Font(dgvHistory.DefaultCellStyle.Font, FontStyle.Bold); // 볼드체는 따로 스타일 만들어야함...
            foreach (DataGridViewRow row in dgvHistory.Rows)
            {
                string type = row.Cells["TransactionType"].Value.ToString();
                if (type == "IN")
                {
                    row.Cells["TransactionType"].Style.ForeColor = Color.SteelBlue;                    
                    row.Cells["TransactionType"].Style.Font = boldFont;
                }
                else if (type == "OUT")
                {
                    row.Cells["TransactionType"].Style.ForeColor = Color.LightCoral;                    
                    row.Cells["TransactionType"].Style.Font = boldFont;
                }
            }
        }
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dtpStart.Value = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpEnd.Value = DateTime.Today;
            cbxType.SelectedIndex = 0; // 전체
            tbSearchProduct.Text = ""; 

            btnSearch_Click(null, null);
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime startDate = dtpStart.Value.Date;
            DateTime endDate = dtpEnd.Value.Date.AddDays(1);  // 수정필

            string type = cbxType.SelectedItem.ToString();
            string keyword = tbSearchProduct.Text.Trim();

            // 여기가 검색
            TransactionRepository txRepo = new TransactionRepository();
            List<Transaction> list = txRepo.SearchTransactions(startDate, endDate, type, keyword);

            dgvHistory.DataSource = list;
            CustomizeGrid(); // 꾸미기

            if (list.Count == 0)
            {
                MessageBox.Show("조건에 맞는 이력이 없습니다...");
            }
        }

        
    } // 여기까지 클래스
}
