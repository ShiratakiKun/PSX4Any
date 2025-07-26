using PSX4Any.PSX4AnyEmu.Util;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace PSX4Any.PSX4AnyEmu.UI.Components
{

    public class HighQualityPictureBox : PictureBox
    {

        public HighQualityPictureBox()
        {
            UIHelper.EnableDoubleBuffered(this);
        }

        protected override void OnPaint(PaintEventArgs pe)
        {
            Graphics g = pe.Graphics;

            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBilinear;

            base.OnPaint(pe);
        }

    }

}
