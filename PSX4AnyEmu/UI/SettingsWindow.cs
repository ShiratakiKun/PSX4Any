using PSX4Any.PSX4AnyEmu.Util;
using PSX4AnyCommon;
using System;
using System.Windows.Forms;
using static AppSettings;

namespace PSX4Any.PSX4AnyEmu.UI
{

    public partial class SettingsWindow : Form
    {

        public SettingsWindow()
        {
            InitializeComponent();
            UIHelper.EnableDoubleBuffered(this);
            this.InitChecked();
            this.CategoryList.Clicked += (sender, e) =>
            {
            };
            this.EnableSound.CheckedChanged += this.CheckedChanged;
        }

        public void InitChecked()
        {
            this.EnableSound.Checked = AppSettings.GetValueBool(CommonSettings.Enable_Sound_SETTINGS);
            CommonSettings.EnableSound = this.EnableSound.Checked;
        }

        public void CheckedChanged(object sender, EventArgs e)
        {
            CheckBox checkBox = sender as CheckBox;
            bool value = checkBox.Checked;
            if (checkBox == this.EnableSound)
            {
                AppSettings.SetValue(CommonSettings.Enable_Sound_SETTINGS, value);
                CommonSettings.EnableSound = value;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

    }

}
