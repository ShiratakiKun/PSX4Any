using PSX4Any.PSX4AnyEmu.UI;
using System;
using System.Windows.Forms;

namespace PSX4Any
{

    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Window());
        }

    }

}
