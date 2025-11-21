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
using System.Windows.Forms.DataVisualization.Charting;

namespace StockManager_1111
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        // 디비에서 가져온 원본데이터 임시 저장 리스트
        private List<StockLot> _allStocks = new List<StockLot>();

        private void 카테고리관리ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            // FormCategory 창을 만들어서 열기
            FormCategory formCategory = new FormCategory();
            formCategory.ShowDialog(); // 이 창 열린 동안은 딴거 클릭 못함
        }

        private void 상품관리ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormProduct formProduct = new FormProduct();
            formProduct.ShowDialog();
        }

        private void 거래처관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCustomer formCustomer = new FormCustomer();
            formCustomer.ShowDialog();
        }

        private void 거래처규칙관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormCustomerRules form = new FormCustomerRules();
            form.ShowDialog();
        }

        private void 입고관리ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormInbound formInbound = new FormInbound();
            formInbound.ShowDialog();
        }

        private void 출고관리ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOutbound formOutbound = new FormOutbound();
            formOutbound.ShowDialog();
        }
        private void 입출고이력조회ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // 여러번 열리기 방지
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is FormHistory)
                {
                    openForm.Activate();
                    return;
                }
            }
            FormHistory formHistory = new FormHistory();
            formHistory.Show();  // 다른작업하면서 볼수있도록 변경
        }

        private void 실시간재고세부현황ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form openForm in Application.OpenForms)
            {
                if (openForm is FormStockStatus)
                {
                    openForm.Activate();
                    return;
                }
            }
            FormStockStatus formStockStatus = new FormStockStatus();
            formStockStatus.Show();
        }

        // 폼로드~
        private void MainForm_Load(object sender, EventArgs e)
        {
            LoadData(); // 데이터 불러오기
            InitChart();  // 차트
            UpdateBottomPanel();  // dgv하나로 합치기
            FilterData("ALL");
        }
        private void MainForm_Activated(object sender, EventArgs e)
        {
            LoadData();
        }

        // 재고 가져오기
        private void LoadData()
        {
            StockLotRepository repo = new StockLotRepository();
            _allStocks = repo.GetAllStockLots();
        }

        // 필터링 로직 <- 여기부터  // 필터설정에서 관리하는방법으로 할지 아니면 카테고리를 따로만들지
        private void FilterData(string type)
        {

            List<StockLot> filteredList = new List<StockLot>();
            DateTime today = DateTime.Today;

            if (type == "ALL") // 임박만료
            {
                filteredList = _allStocks
                    .Where(s => (s.ExpirationDate.Date - today).Days <= 3)
                    .OrderBy(s => s.ExpirationDate) // 정렬 여기서
                    .ToList();
            }
            else if (type == "IMPENDING") // 임박
            {
                filteredList = _allStocks
                    .Where(s => (s.ExpirationDate.Date - today).Days >= 0 &&
                                (s.ExpirationDate.Date - today).Days <= 3)
                    .OrderBy(s => s.ExpirationDate)
                    .ToList();
            }
            else if (type == "EXPIRED") // 만료
            {
                filteredList = _allStocks
                    .Where(s => (s.ExpirationDate.Date - today).Days < 0)
                    .OrderBy(s => s.ExpirationDate)
                    .ToList();
            }

            // 그리드 연결
            dgvDashboard.DataSource = null; // 초기화
            dgvDashboard.DataSource = filteredList;

            CustomizeDashboardGrid(); // 그리드 구성은 따로 빼야할듯...
        }
        private void btnShowAll_Click(object sender, EventArgs e)
        {
            FilterData("ALL");
        }

        private void btnShowImpending_Click(object sender, EventArgs e)
        {
            FilterData("IMPENDING");
        }

        private void btnShowExpired_Click(object sender, EventArgs e)
        {
            FilterData("EXPIRED");
        }

        private void btnOpenStockStatus_Click(object sender, EventArgs e)
        {
            FormStockStatus form = new FormStockStatus();
            form.Show();
        }

        //////// 여기부터 그리드 보여주기 + 꾸미는 부분
        private void CustomizeDashboardGrid()
        {
            if (dgvDashboard.Rows.Count == 0) return;

            string[] hiddenCols = { "ProductId", "SupplierId", "PurchasePrice", "SupplierName" };
            foreach (string col in hiddenCols)
            {
                if (dgvDashboard.Columns.Contains(col))
                    dgvDashboard.Columns[col].Visible = false;
            }

            dgvDashboard.Columns["LotId"].HeaderText = "재고 번호";
            dgvDashboard.Columns["LotId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvDashboard.Columns["ProductName"].HeaderText = "상품명";
            dgvDashboard.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dgvDashboard.Columns["Quantity"].HeaderText = "현재고";
            dgvDashboard.Columns["ExpirationDate"].HeaderText = "유통기한";
            dgvDashboard.Columns["ExpirationDate"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dgvDashboard.Columns["CategoryName"].HeaderText = "구분";
            dgvDashboard.Columns["CategoryName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;

        }

        private void dgvDashboard_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            var dateValue = dgvDashboard.Rows[e.RowIndex].Cells["ExpirationDate"].Value;
            if (dateValue == null || dateValue == DBNull.Value) return;

            DateTime expDate = Convert.ToDateTime(dateValue);
            int daysLeft = (expDate.Date - DateTime.Today).Days;

            if (daysLeft < 0) // 만료
            {
                e.CellStyle.BackColor = Color.MistyRose;
                e.CellStyle.ForeColor = Color.Red;
                e.CellStyle.SelectionBackColor = Color.Firebrick;
                e.CellStyle.SelectionForeColor = Color.White;
            }
            else if (daysLeft <= 3) // 임박
            {
                e.CellStyle.BackColor = Color.LemonChiffon;
                e.CellStyle.ForeColor = Color.Peru;
                e.CellStyle.SelectionBackColor = Color.Gold;
                e.CellStyle.SelectionForeColor = Color.Black;
            }
        }
        //////// 요기까지 대시보드

        //// 여기부터 차트랑 하단dgv 관련
        private void InitChart()
        {
            // 버튼 공유로 변경...
            rbWeek1.CheckedChanged += rbPeriod_CheckedChanged;
            rbMonth1.CheckedChanged += rbPeriod_CheckedChanged;
            rbMonth6.CheckedChanged += rbPeriod_CheckedChanged;
            rbWeek1.Checked = true;

            // 차트퐅느변경
            var axisX = chartTopProducts.ChartAreas[0].AxisX;
            axisX.LabelStyle.Font = new Font("맑은 고딕", 10, FontStyle.Bold);
            axisX.LabelStyle.ForeColor = Color.Black;
            axisX.Interval = 1;

            // 차트그리드관련
            var chartArea = chartTopProducts.ChartAreas[0];
            //chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisX.MajorGrid.LineColor = Color.Gainsboro;
            chartArea.AxisY.MajorGrid.LineColor = Color.Gainsboro;
            chartArea.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;

            chartArea.AxisX.LineColor = Color.DarkGray;
            chartArea.AxisY.LineColor = Color.DarkGray;
        }
        private void rbPeriod_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rb = sender as RadioButton;  // 체크확인
            if (rb != null && rb.Checked)
            {
                UpdateChart();
            }
        }
        private void UpdateBottomPanel()
        {
            UpdateChart();
            UpdateRecentLogs();
            UpdateLowStock();    // 안전재고 미달
        }
        private void UpdateChart()
        {
            DateTime end = DateTime.Now;
            DateTime start = DateTime.Now.AddDays(-7);

            if (rbWeek1.Checked) start = DateTime.Now.AddDays(-7);
            else if (rbMonth1.Checked) start = DateTime.Now.AddMonths(-1);
            else if (rbMonth6.Checked) start = DateTime.Now.AddMonths(-6);

            TransactionRepository txRepo = new TransactionRepository();
            var data = txRepo.GetTopProducts(start, end);

            // 차트 팔레트 + 막대색깔 다르게학
            //chartTopProducts.Palette = ChartColorPalette.SeaGreen;            

            chartTopProducts.Series.Clear();
            chartTopProducts.Legends.Clear(); // 범례 삭제

            Series series = new Series("인기상품");
            series.ChartType = SeriesChartType.Bar; // 나중에 변경하기
            series.IsValueShownAsLabel = true;
            series.Palette = ChartColorPalette.BrightPastel; // 차트 팔레트

            foreach (var item in data)
            {
                series.Points.AddXY(item.Key, item.Value);
            }
            chartTopProducts.Series.Add(series);

            
        }
        private void UpdateRecentLogs()
        {
            TransactionRepository txRepo = new TransactionRepository();
            dgvRecentLogs.DataSource = txRepo.GetRecentTransactions();

            if (dgvRecentLogs.Rows.Count > 0)
            {
                string[] hiddenCols = { "TransactionId", "LotId", "UserId", "SupplierId", "PurchasePrice", "SupplierName" };
                foreach (string col in hiddenCols)
                {
                    if (dgvRecentLogs.Columns.Contains(col))
                        dgvRecentLogs.Columns[col].Visible = false;
                }

                // 뀨미기 부분
                SetGridColumn(dgvRecentLogs, "TransactionType", "구분", 0);
                dgvRecentLogs.Columns["TransactionType"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                SetGridColumn(dgvRecentLogs, "ProductName", "상품명", 1); // -1은 자동 너비
                SetGridColumn(dgvRecentLogs, "Quantity", "수량", 2);
                dgvRecentLogs.Columns["Quantity"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                SetGridColumn(dgvRecentLogs, "Username", "담당자", 3);
                dgvRecentLogs.Columns["Username"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                SetGridColumn(dgvRecentLogs, "TransactionDate", "일시", 4);

                dgvRecentLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;

                if (dgvRecentLogs.Columns.Contains("TransactionDate"))
                    dgvRecentLogs.Columns["TransactionDate"].DefaultCellStyle.Format = "MM-dd HH:mm";

                foreach (DataGridViewRow row in dgvRecentLogs.Rows)
                {
                    if (row.Cells["TransactionType"].Value?.ToString() == "IN")
                        row.Cells["TransactionType"].Style.ForeColor = Color.SteelBlue;
                    else
                        row.Cells["TransactionType"].Style.ForeColor = Color.Firebrick;
                }

                if (dgvRecentLogs.Columns.Contains("ProductName"))
                    dgvRecentLogs.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                // 그리드파트
                dgvRecentLogs.GridColor = Color.Gainsboro;
                dgvRecentLogs.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal; // 세로선삭제
                dgvRecentLogs.BackgroundColor = Color.White;
            }
        }
        private void UpdateLowStock()
        {
            StockLotRepository stockRepo = new StockLotRepository();
            dgvLowStock.DataSource = stockRepo.GetLowStockProducts();

            if (dgvLowStock.Columns.Contains("ProductName")) dgvLowStock.Columns["ProductName"].HeaderText = "상품명";
            if (dgvLowStock.Columns.Contains("CurrentStock")) dgvLowStock.Columns["CurrentStock"].HeaderText = "현재고";
            if (dgvLowStock.Columns.Contains("SafetyStock")) dgvLowStock.Columns["SafetyStock"].HeaderText = "필요";

            dgvLowStock.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // 그리드
            dgvLowStock.GridColor = Color.Gainsboro;
        }
        private void SetGridColumn(DataGridView dgv, string colName, string headerText, int index)
        {
            if (dgv.Columns.Contains(colName))
            {
                dgv.Columns[colName].HeaderText = headerText;
                dgv.Columns[colName].DisplayIndex = index;
            }
        }
        private void panel4rb_Paint(object sender, PaintEventArgs e)   // 범례판넬테두리
        {
            Color borderColor = Color.SteelBlue;
            int borderSize = 1;

            ControlPaint.DrawBorder(e.Graphics,
                                    panel4rb.ClientRectangle,
                                    borderColor, borderSize, ButtonBorderStyle.Dashed, // 왼쪽
                                    borderColor, borderSize, ButtonBorderStyle.Dashed, // 위쪽
                                    borderColor, borderSize, ButtonBorderStyle.Dashed, // 오른쪽
                                    borderColor, borderSize, ButtonBorderStyle.Dashed  // 아래쪽
                                    );
        }

        private void pbGuide_Click(object sender, EventArgs e)
        {
            FormGuide formGuide = new FormGuide();
            formGuide.ShowDialog();
        }
        //// 여기까지 차트,..


    } // 클래스
}
