using PSX4Any.PSX4AnyEmu.Util;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace PSX4Any.PSX4AnyEmu.UI.Components
{

    [ToolboxItem(false)]
    public partial class CategoryListItem : UserControl
    {

        private bool selected;
        public bool Selected
        {
            get
            {
                return this.selected;
            }
            set
            {
                this.selected = value;
            }
        }

        private Color back_color;
        public Color Back_Color
        {
            get
            {
                return this.back_color;
            }
            set
            {
                this.back_color = value;
                this.BackColor = value;
                this.Invalidate();
            }
        }

        private Color selected_color;
        public Color Selected_Color
        {
            get
            {
                return this.selected_color;
            }
            set
            {
                this.selected_color = value;
            }
        }

        private Image icon_image;
        public Image Icon_Image
        {
            get
            {
                return this.icon_image;
            }
            set
            {
                this.icon_image = value;
                this.Icon.Image = value;
                this.UpdateObject();
                this.Invalidate();
            }
        }

        private string title_text;
        public string Title_Text
        {
            get
            {
                return this.title_text;
            }
            set
            {
                this.title_text = value;
                this.Title.Text = value;
                this.UpdateObject();
                this.Invalidate();
            }
        }

        public delegate void ClickedEventHandler(object sender, EventArgs e);

        public event ClickedEventHandler Clicked;

        public CategoryListItem()
        {
            this.InitializeComponent();
            UIHelper.EnableDoubleBuffered(this);
            this.Icon.Click += (sender, e) =>
            {
                if (this.Clicked != null)
                    this.Clicked(this, new EventArgs());
            };
            this.Title.Click += (sender, e) =>
            {
                if (this.Clicked != null)
                    this.Clicked(this, new EventArgs());
            };
        }

        private void UpdateObject()
        {
            int s = 8;

            UIHelper.SetLocation(this.Icon, new Point(0, this.Height / 2 - this.Icon.Height / 2));
            UIHelper.SetSize(this.Icon, new Size(this.Height - s, this.Height - s));

            Padding p = this.Title.Padding;
            p.Left = this.Height + s;

            this.Title.Padding = p;
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.UpdateObject();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.UpdateObject();
        }

    }

}
