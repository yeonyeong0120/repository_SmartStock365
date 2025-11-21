using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace StockManager_1111
{
    public class HoverPictureBox : PictureBox
    {
        public Image ImageNormal { get; set; } // 원래이거
        public Image ImageHover { get; set; }

        public HoverPictureBox()
        {
            this.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            if (ImageHover != null)
            {
                this.Image = ImageHover;
            }
            this.Cursor = Cursors.Hand;
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            if (ImageNormal != null)
            {
                this.Image = ImageNormal;
            }
            this.Cursor = Cursors.Default;
        }

    } // 클래스
}
