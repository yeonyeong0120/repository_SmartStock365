namespace StockManager_1111
{
    partial class FormOutbound
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
            this.dgvStockLots = new System.Windows.Forms.DataGridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.numQuantity = new System.Windows.Forms.NumericUpDown();
            this.cbxProduct = new System.Windows.Forms.ComboBox();
            this.cbxCustomer = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblRedwInfo = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tbSelectedLotId = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lblSelected____ = new System.Windows.Forms.Label();
            this.btnOutbound = new System.Windows.Forms.Button();
            this.btnShowHistory = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockLots)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStockLots
            // 
            this.dgvStockLots.AllowUserToAddRows = false;
            this.dgvStockLots.AllowUserToDeleteRows = false;
            this.dgvStockLots.AllowUserToOrderColumns = true;
            this.dgvStockLots.AllowUserToResizeColumns = false;
            this.dgvStockLots.AllowUserToResizeRows = false;
            this.dgvStockLots.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStockLots.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.dgvStockLots.Location = new System.Drawing.Point(0, 368);
            this.dgvStockLots.Name = "dgvStockLots";
            this.dgvStockLots.RowTemplate.Height = 23;
            this.dgvStockLots.Size = new System.Drawing.Size(629, 215);
            this.dgvStockLots.TabIndex = 0;
            this.dgvStockLots.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvStockLots_CellClick);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.numQuantity);
            this.groupBox1.Controls.Add(this.cbxProduct);
            this.groupBox1.Controls.Add(this.cbxCustomer);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lblRedwInfo);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Malgun Gothic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.groupBox1.Location = new System.Drawing.Point(84, 18);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(455, 264);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "출고 등록";
            // 
            // numQuantity
            // 
            this.numQuantity.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.numQuantity.Location = new System.Drawing.Point(196, 195);
            this.numQuantity.Maximum = new decimal(new int[] {
            10000000,
            0,
            0,
            0});
            this.numQuantity.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numQuantity.Name = "numQuantity";
            this.numQuantity.Size = new System.Drawing.Size(170, 25);
            this.numQuantity.TabIndex = 27;
            this.numQuantity.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbxProduct
            // 
            this.cbxProduct.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxProduct.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbxProduct.FormattingEnabled = true;
            this.cbxProduct.Location = new System.Drawing.Point(196, 94);
            this.cbxProduct.Name = "cbxProduct";
            this.cbxProduct.Size = new System.Drawing.Size(170, 25);
            this.cbxProduct.TabIndex = 26;
            this.cbxProduct.SelectionChangeCommitted += new System.EventHandler(this.cbxProduct_SelectionChangeCommitted);
            // 
            // cbxCustomer
            // 
            this.cbxCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxCustomer.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.cbxCustomer.FormattingEnabled = true;
            this.cbxCustomer.Location = new System.Drawing.Point(196, 48);
            this.cbxCustomer.Name = "cbxCustomer";
            this.cbxCustomer.Size = new System.Drawing.Size(170, 25);
            this.cbxCustomer.TabIndex = 25;
            this.cbxCustomer.SelectionChangeCommitted += new System.EventHandler(this.cbxCustomer_SelectionChangeCommitted);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(80, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 20);
            this.label3.TabIndex = 24;
            this.label3.Text = "📦 출고 수량";
            // 
            // lblRedwInfo
            // 
            this.lblRedwInfo.AutoSize = true;
            this.lblRedwInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblRedwInfo.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblRedwInfo.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblRedwInfo.Location = new System.Drawing.Point(82, 148);
            this.lblRedwInfo.Name = "lblRedwInfo";
            this.lblRedwInfo.Size = new System.Drawing.Size(211, 20);
            this.lblRedwInfo.TabIndex = 23;
            this.lblRedwInfo.Text = "규칙은 이곳에 표시됩니다 📋";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(80, 98);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 20);
            this.label2.TabIndex = 22;
            this.label2.Text = "🏷️ 상품명";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(80, 52);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(80, 20);
            this.label1.TabIndex = 21;
            this.label1.Text = "🚚 출고처";
            // 
            // tbSelectedLotId
            // 
            this.tbSelectedLotId.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.tbSelectedLotId.Location = new System.Drawing.Point(178, 311);
            this.tbSelectedLotId.Name = "tbSelectedLotId";
            this.tbSelectedLotId.ReadOnly = true;
            this.tbSelectedLotId.Size = new System.Drawing.Size(87, 25);
            this.tbSelectedLotId.TabIndex = 20;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(78, 314);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(26, 17);
            this.label6.TabIndex = 19;
            this.label6.Text = "✔️";
            // 
            // lblSelected____
            // 
            this.lblSelected____.AutoSize = true;
            this.lblSelected____.Font = new System.Drawing.Font("Malgun Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.lblSelected____.Location = new System.Drawing.Point(103, 317);
            this.lblSelected____.Name = "lblSelected____";
            this.lblSelected____.Size = new System.Drawing.Size(65, 17);
            this.lblSelected____.TabIndex = 18;
            this.lblSelected____.Text = "선택 확인";
            // 
            // btnOutbound
            // 
            this.btnOutbound.BackColor = System.Drawing.Color.AliceBlue;
            this.btnOutbound.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnOutbound.Location = new System.Drawing.Point(293, 299);
            this.btnOutbound.Name = "btnOutbound";
            this.btnOutbound.Size = new System.Drawing.Size(115, 45);
            this.btnOutbound.TabIndex = 21;
            this.btnOutbound.Text = "출고 처리";
            this.btnOutbound.UseVisualStyleBackColor = false;
            this.btnOutbound.Click += new System.EventHandler(this.btnOutbound_Click);
            // 
            // btnShowHistory
            // 
            this.btnShowHistory.BackColor = System.Drawing.Color.GhostWhite;
            this.btnShowHistory.Font = new System.Drawing.Font("Malgun Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.btnShowHistory.Location = new System.Drawing.Point(416, 299);
            this.btnShowHistory.Name = "btnShowHistory";
            this.btnShowHistory.Size = new System.Drawing.Size(127, 45);
            this.btnShowHistory.TabIndex = 22;
            this.btnShowHistory.Text = "입출고이력 조회";
            this.btnShowHistory.UseVisualStyleBackColor = false;
            this.btnShowHistory.Click += new System.EventHandler(this.btnShowHistory_Click);
            // 
            // FormOutbound
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(629, 583);
            this.Controls.Add(this.btnShowHistory);
            this.Controls.Add(this.btnOutbound);
            this.Controls.Add(this.tbSelectedLotId);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lblSelected____);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.dgvStockLots);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "FormOutbound";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "출고 관리";
            this.Load += new System.EventHandler(this.FormOutbound_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvStockLots)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numQuantity)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStockLots;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox tbSelectedLotId;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblSelected____;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRedwInfo;
        private System.Windows.Forms.ComboBox cbxCustomer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown numQuantity;
        private System.Windows.Forms.ComboBox cbxProduct;
        private System.Windows.Forms.Button btnOutbound;
        private System.Windows.Forms.Button btnShowHistory;
    }
}