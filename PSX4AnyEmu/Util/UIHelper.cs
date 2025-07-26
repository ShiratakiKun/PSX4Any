using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace PSX4Any.PSX4AnyEmu.Util
{

    public class UIHelper
    {

        public static void SetLocation(Control control, Point location)
        {
            if (UIHelper.CheckControl(control))
                return;
            control.Location = location;
        }

        public static void SetSize(Control control, Size size)
        {
            if (UIHelper.CheckControl(control))
                return;
            control.Size = size;
        }

        public static void EnableDoubleBuffered(Control control)
        {
            if (UIHelper.CheckControl(control))
                return;
            PropertyInfo pInfo = typeof(Control).
                GetProperty("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance);
            if (control is Form)
            {
                Form form = control as Form;
                pInfo.SetValue(form, true, null);
                if (!form.HasChildren)
                    return;
                foreach (Control fChild in form.Controls)
                {
                    if (fChild.HasChildren)
                    {
                        UIHelper.EnableDoubleBuffered(fChild);
                        continue;
                    }
                    pInfo.SetValue(fChild, true, null);
                }
                return;
            }
            else if (control is UserControl)
            {
                UserControl userControl = control as UserControl;
                pInfo.SetValue(userControl, true, null);
                if (!userControl.HasChildren)
                    return;
                foreach (Control uChild in userControl.Controls)
                {
                    if (uChild.HasChildren)
                    {
                        UIHelper.EnableDoubleBuffered(uChild);
                        continue;
                    }
                    pInfo.SetValue(uChild, true, null);
                }
                return;
            }
            else if (control is Panel)
            {
                Panel panel = control as Panel;
                pInfo.SetValue(panel, true, null);
                if (!panel.HasChildren)
                    return;
                foreach (Control pChild in panel.Controls)
                {
                    if (pChild.HasChildren)
                    {
                        UIHelper.EnableDoubleBuffered(pChild);
                        continue;
                    }
                    pInfo.SetValue(pChild, true, null);
                }
                return;
            }
            pInfo.SetValue(control, true, null);
        }

        private static bool CheckControl(Control control)
        {
            return control == null || control.IsDisposed;
        }

    }

}
