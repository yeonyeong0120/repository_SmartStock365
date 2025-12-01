namespace StockManager_1111
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.기본정보ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.카테고리관리ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.상품관리ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.매출매입현황ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.거래관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.거래처관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.거래처규칙관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.입출고관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.입고관리ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.출고관리ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.입출고이력조회ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.실시간재고세부현황ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.dgvDashboard = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.btnShowAll = new System.Windows.Forms.Button();
            this.btnShowImpending = new System.Windows.Forms.Button();
            this.btnShowExpired = new System.Windows.Forms.Button();
            this.btnOpenStockStatus = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.rbWeek1 = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.rbMonth6 = new System.Windows.Forms.RadioButton();
            this.rbMonth1 = new System.Windows.Forms.RadioButton();
            this.panel4rb = new System.Windows.Forms.Panel();
            this.chartTopProducts = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.panel3 = new System.Windows.Forms.Panel();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.dgvLowStock = new System.Windows.Forms.DataGridView();
            this.dgvRecentLogs = new System.Windows.Forms.DataGridView();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox6 = new System.Windows.Forms.PictureBox();
            this.pbGuide = new StockManager_1111.HoverPictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDashboard)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopProducts)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentLogs)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGuide)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.기본정보ToolStripMenuItem,
            this.거래관리ToolStripMenuItem,
            this.입출고관리ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(947, 29);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 기본정보ToolStripMenuItem
            // 
            this.기본정보ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.카테고리관리ToolStripMenuItem1,
            this.상품관리ToolStripMenuItem1,
            this.매출매입현황ToolStripMenuItem});
            this.기본정보ToolStripMenuItem.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.기본정보ToolStripMenuItem.ForeColor = System.Drawing.SystemColors.ControlText;
            this.기본정보ToolStripMenuItem.Name = "기본정보ToolStripMenuItem";
            this.기본정보ToolStripMenuItem.Size = new System.Drawing.Size(86, 25);
            this.기본정보ToolStripMenuItem.Text = "기본 정보";
            // 
            // 카테고리관리ToolStripMenuItem1
            // 
            this.카테고리관리ToolStripMenuItem1.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.카테고리관리ToolStripMenuItem1.Name = "카테고리관리ToolStripMenuItem1";
            this.카테고리관리ToolStripMenuItem1.Size = new System.Drawing.Size(179, 24);
            this.카테고리관리ToolStripMenuItem1.Text = "카테고리 관리";
            this.카테고리관리ToolStripMenuItem1.Click += new System.EventHandler(this.카테고리관리ToolStripMenuItem1_Click);
            // 
            // 상품관리ToolStripMenuItem1
            // 
            this.상품관리ToolStripMenuItem1.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.상품관리ToolStripMenuItem1.Name = "상품관리ToolStripMenuItem1";
            this.상품관리ToolStripMenuItem1.Size = new System.Drawing.Size(179, 24);
            this.상품관리ToolStripMenuItem1.Text = "상품 관리";
            this.상품관리ToolStripMenuItem1.Click += new System.EventHandler(this.상품관리ToolStripMenuItem1_Click);
            // 
            // 매출매입현황ToolStripMenuItem
            // 
            this.매출매입현황ToolStripMenuItem.Name = "매출매입현황ToolStripMenuItem";
            this.매출매입현황ToolStripMenuItem.Size = new System.Drawing.Size(179, 24);
            this.매출매입현황ToolStripMenuItem.Text = "매출/매입 현황";
            this.매출매입현황ToolStripMenuItem.Click += new System.EventHandler(this.매출매입현황ToolStripMenuItem_Click);
            // 
            // 거래관리ToolStripMenuItem
            // 
            this.거래관리ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.거래처관리ToolStripMenuItem,
            this.거래처규칙관리ToolStripMenuItem});
            this.거래관리ToolStripMenuItem.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.거래관리ToolStripMenuItem.Name = "거래관리ToolStripMenuItem";
            this.거래관리ToolStripMenuItem.Size = new System.Drawing.Size(92, 25);
            this.거래관리ToolStripMenuItem.Text = "거래 관리";
            // 
            // 거래처관리ToolStripMenuItem
            // 
            this.거래처관리ToolStripMenuItem.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.거래처관리ToolStripMenuItem.Name = "거래처관리ToolStripMenuItem";
            this.거래처관리ToolStripMenuItem.Size = new System.Drawing.Size(194, 24);
            this.거래처관리ToolStripMenuItem.Text = "거래처 관리";
            this.거래처관리ToolStripMenuItem.Click += new System.EventHandler(this.거래처관리ToolStripMenuItem_Click);
            // 
            // 거래처규칙관리ToolStripMenuItem
            // 
            this.거래처규칙관리ToolStripMenuItem.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.거래처규칙관리ToolStripMenuItem.Name = "거래처규칙관리ToolStripMenuItem";
            this.거래처규칙관리ToolStripMenuItem.Size = new System.Drawing.Size(194, 24);
            this.거래처규칙관리ToolStripMenuItem.Text = "거래처 규칙 관리";
            this.거래처규칙관리ToolStripMenuItem.Click += new System.EventHandler(this.거래처규칙관리ToolStripMenuItem_Click);
            // 
            // 입출고관리ToolStripMenuItem
            // 
            this.입출고관리ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.입고관리ToolStripMenuItem1,
            this.출고관리ToolStripMenuItem,
            this.입출고이력조회ToolStripMenuItem,
            this.실시간재고세부현황ToolStripMenuItem});
            this.입출고관리ToolStripMenuItem.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.입출고관리ToolStripMenuItem.Name = "입출고관리ToolStripMenuItem";
            this.입출고관리ToolStripMenuItem.Size = new System.Drawing.Size(101, 25);
            this.입출고관리ToolStripMenuItem.Text = "입출고 관리";
            // 
            // 입고관리ToolStripMenuItem1
            // 
            this.입고관리ToolStripMenuItem1.Name = "입고관리ToolStripMenuItem1";
            this.입고관리ToolStripMenuItem1.Size = new System.Drawing.Size(158, 24);
            this.입고관리ToolStripMenuItem1.Text = "입고 관리";
            this.입고관리ToolStripMenuItem1.Click += new System.EventHandler(this.입고관리ToolStripMenuItem1_Click);
            // 
            // 출고관리ToolStripMenuItem
            // 
            this.출고관리ToolStripMenuItem.Name = "출고관리ToolStripMenuItem";
            this.출고관리ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.출고관리ToolStripMenuItem.Text = "출고 관리";
            this.출고관리ToolStripMenuItem.Click += new System.EventHandler(this.출고관리ToolStripMenuItem_Click);
            // 
            // 입출고이력조회ToolStripMenuItem
            // 
            this.입출고이력조회ToolStripMenuItem.Name = "입출고이력조회ToolStripMenuItem";
            this.입출고이력조회ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.입출고이력조회ToolStripMenuItem.Text = "입출고 이력";
            this.입출고이력조회ToolStripMenuItem.Click += new System.EventHandler(this.입출고이력조회ToolStripMenuItem_Click);
            // 
            // 실시간재고세부현황ToolStripMenuItem
            // 
            this.실시간재고세부현황ToolStripMenuItem.Name = "실시간재고세부현황ToolStripMenuItem";
            this.실시간재고세부현황ToolStripMenuItem.Size = new System.Drawing.Size(158, 24);
            this.실시간재고세부현황ToolStripMenuItem.Text = "재고 현황";
            this.실시간재고세부현황ToolStripMenuItem.Click += new System.EventHandler(this.실시간재고세부현황ToolStripMenuItem_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.dgvDashboard);
            this.panel1.Location = new System.Drawing.Point(153, 189);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(516, 158);
            this.panel1.TabIndex = 1;
            // 
            // dgvDashboard
            // 
            this.dgvDashboard.AllowUserToAddRows = false;
            this.dgvDashboard.AllowUserToDeleteRows = false;
            this.dgvDashboard.AllowUserToResizeColumns = false;
            this.dgvDashboard.AllowUserToResizeRows = false;
            this.dgvDashboard.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvDashboard.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDashboard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDashboard.Location = new System.Drawing.Point(0, 0);
            this.dgvDashboard.Name = "dgvDashboard";
            this.dgvDashboard.ReadOnly = true;
            this.dgvDashboard.RowTemplate.Height = 23;
            this.dgvDashboard.Size = new System.Drawing.Size(516, 158);
            this.dgvDashboard.TabIndex = 0;
            this.dgvDashboard.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dgvDashboard_CellFormatting);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Pretendard Variable", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.Color.SteelBlue;
            this.label1.Location = new System.Drawing.Point(19, 42);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(300, 44);
            this.label1.TabIndex = 2;
            this.label1.Text = "Smart Stock 365\r\n";
            // 
            // btnShowAll
            // 
            this.btnShowAll.BackColor = System.Drawing.Color.GhostWhite;
            this.btnShowAll.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnShowAll.Location = new System.Drawing.Point(711, 187);
            this.btnShowAll.Name = "btnShowAll";
            this.btnShowAll.Size = new System.Drawing.Size(117, 35);
            this.btnShowAll.TabIndex = 3;
            this.btnShowAll.Text = "관리 대상";
            this.btnShowAll.UseVisualStyleBackColor = false;
            this.btnShowAll.Click += new System.EventHandler(this.btnShowAll_Click);
            // 
            // btnShowImpending
            // 
            this.btnShowImpending.BackColor = System.Drawing.Color.Linen;
            this.btnShowImpending.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnShowImpending.Location = new System.Drawing.Point(711, 228);
            this.btnShowImpending.Name = "btnShowImpending";
            this.btnShowImpending.Size = new System.Drawing.Size(117, 35);
            this.btnShowImpending.TabIndex = 4;
            this.btnShowImpending.Text = "임박 재고";
            this.btnShowImpending.UseVisualStyleBackColor = false;
            this.btnShowImpending.Click += new System.EventHandler(this.btnShowImpending_Click);
            // 
            // btnShowExpired
            // 
            this.btnShowExpired.BackColor = System.Drawing.Color.LavenderBlush;
            this.btnShowExpired.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnShowExpired.Location = new System.Drawing.Point(711, 269);
            this.btnShowExpired.Name = "btnShowExpired";
            this.btnShowExpired.Size = new System.Drawing.Size(117, 35);
            this.btnShowExpired.TabIndex = 5;
            this.btnShowExpired.Text = "만료 재고";
            this.btnShowExpired.UseVisualStyleBackColor = false;
            this.btnShowExpired.Click += new System.EventHandler(this.btnShowExpired_Click);
            // 
            // btnOpenStockStatus
            // 
            this.btnOpenStockStatus.BackColor = System.Drawing.Color.AliceBlue;
            this.btnOpenStockStatus.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOpenStockStatus.Location = new System.Drawing.Point(711, 310);
            this.btnOpenStockStatus.Name = "btnOpenStockStatus";
            this.btnOpenStockStatus.Size = new System.Drawing.Size(117, 35);
            this.btnOpenStockStatus.TabIndex = 6;
            this.btnOpenStockStatus.Text = "전체 재고 현황";
            this.btnOpenStockStatus.UseVisualStyleBackColor = false;
            this.btnOpenStockStatus.Click += new System.EventHandler(this.btnOpenStockStatus_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.LightSteelBlue;
            this.label2.Location = new System.Drawing.Point(22, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(900, 2);
            this.label2.TabIndex = 7;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Pretendard Variable", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.ForeColor = System.Drawing.Color.SteelBlue;
            this.label3.Location = new System.Drawing.Point(148, 154);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 25);
            this.label3.TabIndex = 8;
            this.label3.Text = "확인해주세요!";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Pretendard Variable SemiBold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.ForeColor = System.Drawing.Color.SteelBlue;
            this.label4.Location = new System.Drawing.Point(30, 94);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(189, 25);
            this.label4.TabIndex = 9;
            this.label4.Text = "한눈에 보는 우리 가게\r\n";
            // 
            // rbWeek1
            // 
            this.rbWeek1.AutoSize = true;
            this.rbWeek1.Location = new System.Drawing.Point(81, 258);
            this.rbWeek1.Name = "rbWeek1";
            this.rbWeek1.Size = new System.Drawing.Size(46, 21);
            this.rbWeek1.TabIndex = 10;
            this.rbWeek1.TabStop = true;
            this.rbWeek1.Text = "1주";
            this.rbWeek1.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.rbMonth6);
            this.panel2.Controls.Add(this.rbMonth1);
            this.panel2.Controls.Add(this.rbWeek1);
            this.panel2.Controls.Add(this.panel4rb);
            this.panel2.Controls.Add(this.chartTopProducts);
            this.panel2.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel2.Location = new System.Drawing.Point(22, 416);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(376, 308);
            this.panel2.TabIndex = 11;
            // 
            // rbMonth6
            // 
            this.rbMonth6.AutoSize = true;
            this.rbMonth6.Location = new System.Drawing.Point(244, 258);
            this.rbMonth6.Name = "rbMonth6";
            this.rbMonth6.Size = new System.Drawing.Size(59, 21);
            this.rbMonth6.TabIndex = 12;
            this.rbMonth6.TabStop = true;
            this.rbMonth6.Text = "6개월";
            this.rbMonth6.UseVisualStyleBackColor = true;
            // 
            // rbMonth1
            // 
            this.rbMonth1.AutoSize = true;
            this.rbMonth1.Location = new System.Drawing.Point(157, 258);
            this.rbMonth1.Name = "rbMonth1";
            this.rbMonth1.Size = new System.Drawing.Size(57, 21);
            this.rbMonth1.TabIndex = 11;
            this.rbMonth1.TabStop = true;
            this.rbMonth1.Text = "한 달";
            this.rbMonth1.UseVisualStyleBackColor = true;
            // 
            // panel4rb
            // 
            this.panel4rb.Location = new System.Drawing.Point(66, 248);
            this.panel4rb.Name = "panel4rb";
            this.panel4rb.Size = new System.Drawing.Size(245, 42);
            this.panel4rb.TabIndex = 14;
            this.panel4rb.Paint += new System.Windows.Forms.PaintEventHandler(this.panel4rb_Paint);
            // 
            // chartTopProducts
            // 
            chartArea1.Name = "ChartArea1";
            this.chartTopProducts.ChartAreas.Add(chartArea1);
            this.chartTopProducts.Dock = System.Windows.Forms.DockStyle.Top;
            legend1.Name = "Legend1";
            this.chartTopProducts.Legends.Add(legend1);
            this.chartTopProducts.Location = new System.Drawing.Point(0, 0);
            this.chartTopProducts.Name = "chartTopProducts";
            series1.ChartArea = "ChartArea1";
            series1.Legend = "Legend1";
            series1.Name = "Series1";
            this.chartTopProducts.Series.Add(series1);
            this.chartTopProducts.Size = new System.Drawing.Size(376, 238);
            this.chartTopProducts.TabIndex = 13;
            this.chartTopProducts.Text = "chart1";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.pictureBox4);
            this.panel3.Controls.Add(this.label7);
            this.panel3.Controls.Add(this.dgvLowStock);
            this.panel3.Controls.Add(this.dgvRecentLogs);
            this.panel3.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.panel3.Location = new System.Drawing.Point(431, 422);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(466, 302);
            this.panel3.TabIndex = 14;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.Location = new System.Drawing.Point(0, 135);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(31, 31);
            this.pictureBox4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox4.TabIndex = 20;
            this.pictureBox4.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Pretendard Variable", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.Color.SteelBlue;
            this.label7.Location = new System.Drawing.Point(28, 138);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 25);
            this.label7.TabIndex = 17;
            this.label7.Text = "입출고 기록";
            // 
            // dgvLowStock
            // 
            this.dgvLowStock.AllowUserToAddRows = false;
            this.dgvLowStock.AllowUserToDeleteRows = false;
            this.dgvLowStock.AllowUserToResizeColumns = false;
            this.dgvLowStock.AllowUserToResizeRows = false;
            this.dgvLowStock.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLowStock.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvLowStock.Location = new System.Drawing.Point(0, 0);
            this.dgvLowStock.Name = "dgvLowStock";
            this.dgvLowStock.ReadOnly = true;
            this.dgvLowStock.RowTemplate.Height = 23;
            this.dgvLowStock.Size = new System.Drawing.Size(466, 126);
            this.dgvLowStock.TabIndex = 1;
            // 
            // dgvRecentLogs
            // 
            this.dgvRecentLogs.AllowUserToAddRows = false;
            this.dgvRecentLogs.AllowUserToDeleteRows = false;
            this.dgvRecentLogs.AllowUserToResizeColumns = false;
            this.dgvRecentLogs.AllowUserToResizeRows = false;
            this.dgvRecentLogs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvRecentLogs.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvRecentLogs.Location = new System.Drawing.Point(0, 169);
            this.dgvRecentLogs.Name = "dgvRecentLogs";
            this.dgvRecentLogs.ReadOnly = true;
            this.dgvRecentLogs.RowTemplate.Height = 23;
            this.dgvRecentLogs.Size = new System.Drawing.Size(466, 133);
            this.dgvRecentLogs.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Pretendard Variable", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.Color.SteelBlue;
            this.label5.Location = new System.Drawing.Point(89, 389);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(184, 25);
            this.label5.TabIndex = 15;
            this.label5.Text = "우리 가게 베스트셀러";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Pretendard Variable", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.Color.SteelBlue;
            this.label6.Location = new System.Drawing.Point(463, 390);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 25);
            this.label6.TabIndex = 16;
            this.label6.Text = "발주가 필요해요";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(119, 151);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(31, 31);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 17;
            this.pictureBox1.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(56, 382);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(31, 31);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 18;
            this.pictureBox2.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(432, 385);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(31, 31);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 19;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox6
            // 
            this.pictureBox6.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox6.Image")));
            this.pictureBox6.Location = new System.Drawing.Point(222, 92);
            this.pictureBox6.Name = "pictureBox6";
            this.pictureBox6.Size = new System.Drawing.Size(31, 31);
            this.pictureBox6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox6.TabIndex = 21;
            this.pictureBox6.TabStop = false;
            // 
            // pbGuide
            // 
            this.pbGuide.Image = ((System.Drawing.Image)(resources.GetObject("pbGuide.Image")));
            this.pbGuide.ImageHover = ((System.Drawing.Image)(resources.GetObject("pbGuide.ImageHover")));
            this.pbGuide.ImageNormal = ((System.Drawing.Image)(resources.GetObject("pbGuide.ImageNormal")));
            this.pbGuide.Location = new System.Drawing.Point(851, 54);
            this.pbGuide.Name = "pbGuide";
            this.pbGuide.Size = new System.Drawing.Size(60, 60);
            this.pbGuide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGuide.TabIndex = 22;
            this.pbGuide.TabStop = false;
            this.pbGuide.Click += new System.EventHandler(this.pbGuide_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(61, 4);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(947, 767);
            this.Controls.Add(this.pbGuide);
            this.Controls.Add(this.pictureBox6);
            this.Controls.Add(this.pictureBox3);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnOpenStockStatus);
            this.Controls.Add(this.btnShowExpired);
            this.Controls.Add(this.btnShowImpending);
            this.Controls.Add(this.btnShowAll);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Smart Stock 365";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDashboard)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartTopProducts)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLowStock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentLogs)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pbGuide)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 기본정보ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 카테고리관리ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 상품관리ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 거래관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 거래처관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 거래처규칙관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 입출고관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 입고관리ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 출고관리ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 입출고이력조회ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 실시간재고세부현황ToolStripMenuItem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dgvDashboard;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnShowAll;
        private System.Windows.Forms.Button btnShowImpending;
        private System.Windows.Forms.Button btnShowExpired;
        private System.Windows.Forms.Button btnOpenStockStatus;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton rbWeek1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton rbMonth6;
        private System.Windows.Forms.RadioButton rbMonth1;
        private System.Windows.Forms.DataVisualization.Charting.Chart chartTopProducts;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.DataGridView dgvRecentLogs;
        private System.Windows.Forms.DataGridView dgvLowStock;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Panel panel4rb;
        private System.Windows.Forms.PictureBox pictureBox6;
        private HoverPictureBox pbGuide;
        private System.Windows.Forms.ToolStripMenuItem 매출매입현황ToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
    }
}

