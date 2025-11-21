using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace StockManager_1111
{
    public partial class FormGuide : Form
    {
        public FormGuide()
        {
            InitializeComponent();
        }

        private void FormGuide_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }
        private void pbGuide_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
