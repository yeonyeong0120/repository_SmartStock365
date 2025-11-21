namespace StockManager_1111
{
    partial class FormGuide
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
            this.pbGuide = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pbGuide)).BeginInit();
            this.SuspendLayout();
            // 
            // pbGuide
            // 
            this.pbGuide.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pbGuide.Location = new System.Drawing.Point(0, 0);
            this.pbGuide.Name = "pbGuide";
            this.pbGuide.Size = new System.Drawing.Size(800, 450);
            this.pbGuide.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pbGuide.TabIndex = 0;
            this.pbGuide.TabStop = false;
            this.pbGuide.Click += new System.EventHandler(this.pbGuide_Click);
            // 
            // FormGuide
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.pbGuide);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.KeyPreview = true;
            this.Name = "FormGuide";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "안내";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGuide_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pbGuide)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pbGuide;
    }
}