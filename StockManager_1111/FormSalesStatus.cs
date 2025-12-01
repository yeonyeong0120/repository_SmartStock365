using StockManagerDAL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace StockManager_1111
{
    public partial class FormSalesStatus : Form
    {
        public FormSalesStatus()
        {
            InitializeComponent();
        }

        private void FormSalesStatus_Load(object sender, EventArgs e)
        {
            UpdateSummaryLabels(); // 라벨 업데이트
            UpdateTrendChart();  // 꺾은선

            dtpStart.Value = DateTime.Today;
            dtpEnd.Value = DateTime.Today;
            btnSearch_Click(sender, e); // 조회 버튼을 강제로 한 번 클릭
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            DateTime start = dtpStart.Value;
            DateTime end = dtpEnd.Value;
            string keyword = tbSearchBox.Text.Trim();

            TransactionRepository txRepo = new TransactionRepository();
            DataTable dtGrid = txRepo.SearchTransactions(start, end, keyword);

            //파이 차트 데이터
            var dictChart = txRepo.GetSalesShare(start, end, keyword);

            UpdateGrid(dtGrid);
            UpdatePieChart(dictChart);
        }
        // 그리드
        private void UpdateGrid(DataTable dt)
        {
            dgvSales.DataSource = dt;

            if (dgvSales.Columns.Contains("TotalAmount"))
            {
                dgvSales.Columns["TransactionDate"].HeaderText = "날짜";
                dgvSales.Columns["TransactionType"].HeaderText = "구분";
                dgvSales.Columns["ProductName"].HeaderText = "상품명";
                dgvSales.Columns["Quantity"].HeaderText = "수량";
                dgvSales.Columns["Price"].HeaderText = "단가";
                dgvSales.Columns["TotalAmount"].HeaderText = "금액";
                dgvSales.Columns["CustomerName"].HeaderText = "거래처";

                dgvSales.Columns["Price"].DefaultCellStyle.Format = "N0";
                dgvSales.Columns["TotalAmount"].DefaultCellStyle.Format = "N0";

                dgvSales.Columns["ProductName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            }
        }

        // 파이 차트
        private void UpdatePieChart(Dictionary<string, long> data)
        {
            chartSales.Series.Clear();

            Series series = new Series("SalesShare");
            series.ChartType = SeriesChartType.Pie; // 파이 차트 설정

            if (data.Count == 0) return;

            foreach (var item in data)
            {
                int index = series.Points.AddXY(item.Key, item.Value);

                // 툴팁
                series.Points[index].ToolTip = $"{item.Key}: {item.Value:N0}원";

                // 라벨  // 퍼센트만!!
                series.Points[index].Label = "#PERCENT";
                series.Points[index].LegendText = "#VALX";
            }

            chartSales.Series.Add(series);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            dtpStart.Value = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtpEnd.Value = DateTime.Now;

            tbSearchBox.Text = "";
            dgvSales.DataSource = null;
        }

        // 상단 요약 라벨
        private void UpdateSummaryLabels()
        {
            TransactionRepository txRepo = new TransactionRepository();

            long[] summary = txRepo.GetMonthlySummary();
            long sales = summary[0]; // 매출
            long cost = summary[1];  // 매입
            long profit = summary[2]; // 순수익

            lblTotalSales.Text = $"{sales:N0} 원";
            lblTotalCost.Text = $"{cost:N0} 원";
            lblNetProfit.Text = $"{profit:N0} 원";

            if (profit >= 0) lblNetProfit.ForeColor = Color.SeaGreen;
            else lblNetProfit.ForeColor = Color.Crimson;

            // 4. 마진율 계산 및 표시
            double marginRate = 0;
            if (sales > 0) marginRate = (double)profit / sales * 100;

            lblMargin.Text = $"이 달의 마진율은 : {marginRate:F1}% 입니다!";
        }

        // 매출 추이 차트
        private void UpdateTrendChart()
        {
            TransactionRepository txRepo = new TransactionRepository();
            var trendData = txRepo.GetMonthlyTrend();

            chartTrend.Series.Clear();
            Series series = new Series("월별 매출");
            series.ChartType = SeriesChartType.Line; // 꺾은선
            series.BorderWidth = 3;
            series.Color = Color.SteelBlue;
            series.MarkerStyle = MarkerStyle.Circle;
            series.MarkerSize = 8;
            series.IsValueShownAsLabel = true;

            foreach (var item in trendData)
            {
                series.Points.AddXY(item.Key, item.Value);
            }

            chartTrend.Series.Add(series);

            // 여기가 디자인
            if (chartTrend.ChartAreas.Count > 0)
            {
                var area = chartTrend.ChartAreas[0];
                area.AxisX.MajorGrid.Enabled = false; // 세로줄 삭제
                area.AxisY.MajorGrid.LineColor = Color.WhiteSmoke; // 가로줄 연하게
                area.AxisY.MajorGrid.LineDashStyle = ChartDashStyle.Dash;
            }
        }


        // 여기에 추가
    }
}
