using PSX4Any.PSX4AnyEmu.Util;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace PSX4Any.PSX4AnyEmu.UI
{

    public partial class AboutAppWindow : Form
    {

        private Point mouseCursor = Point.Empty;

        public AboutAppWindow()
        {
            InitializeComponent();
            UIHelper.EnableDoubleBuffered(this);

            MouseDown += OnMouseDown;
            MouseMove += OnMouseMove;
            MouseUp += OnMouseUp;
            foreach (Control c in Controls)
            {
                if (c == CloseButton)
                    continue;
                c.MouseDown += OnMouseDown;
                c.MouseMove += OnMouseMove;
                c.MouseUp += OnMouseUp;
            }

            CloseButton.Click += OnClose;
        }

        private void OnMouseDown(object sender, MouseEventArgs e)
        {
            mouseCursor = e.Location;
        }

        private void OnMouseMove(object sender, MouseEventArgs e)
        {
            if (mouseCursor == Point.Empty)
                return;
            Location = new Point(
                Location.X + (e.X - mouseCursor.X),
                Location.Y + (e.Y - mouseCursor.Y));
        }

        private void OnMouseUp(object sender, MouseEventArgs e)
        {
            mouseCursor = Point.Empty;
        }

        private void OnClose(object sender, EventArgs e)
        {
            Close();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            UIHelper.SetLocation(AppIcon, new Point(
                Width / 2 - AppIcon.Width / 2, 16));
            UIHelper.SetLocation(Title, new Point(
                Width / 2 - Title.Width / 2, 64));
            UIHelper.SetLocation(Desc, new Point(
                Width / 2 - Desc.Width / 2, 96));
            UIHelper.SetLocation(License, new Point(
                Width / 2 - License.Width / 2,
                Height - CloseButton.Height - CreditURL.Height - Credit.Height - License.Height - 32));
            UIHelper.SetLocation(Credit, new Point(
                Width / 2 - Credit.Width / 2, 
                Height - CloseButton.Height - CreditURL.Height - Credit.Height - 32));
            UIHelper.SetLocation(CreditURL, new Point(
                Width / 2 - CreditURL.Width / 2,
                Height - CloseButton.Height - CreditURL.Height - 32));
            UIHelper.SetLocation(CloseButton, new Point(
                Width / 2 - CloseButton.Width / 2, Height - CloseButton.Height - 16));
        }

    }

}
