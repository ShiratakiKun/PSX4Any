using PSX4Any.PSX4AnyEmu.Util;
using System.ComponentModel;
using System.Windows.Forms;

namespace PSX4Any.PSX4AnyEmu.UI.Components
{

    [ToolboxItem(false)]
    public class DoubleBufferedPanel : Panel
    {

        public DoubleBufferedPanel()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            UIHelper.EnableDoubleBuffered(this);
        }

    }

}
