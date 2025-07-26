using PSX4Any.PSX4AnyEmu.Util;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace PSX4Any.PSX4AnyEmu.UI.Components
{

    public partial class CategoryListView : UserControl
    {

        private Collection<CategoryListItem> listItems = new Collection<CategoryListItem>();
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Editor("System.ComponentModel.Design.CollectionEditor, System.Design", typeof(UITypeEditor))]
        [Browsable(true)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        public Collection<CategoryListItem> ITEMS
        {
            get
            {
                return this.listItems;
            }
            set
            {
                this.listItems = value;
                this.Reload();
            }
        }

        public delegate void ClickedEventHandler(object sender, ClickedEventArgs e);

        public class ClickedEventArgs : EventArgs
        {

            public string title;

            public ClickedEventArgs(string title)
            {
                this.title = title;
            }

        }

        public event ClickedEventHandler Clicked;

        public CategoryListView()
        {
            this.InitializeComponent();
            UIHelper.EnableDoubleBuffered(this);
        }

        private void UpdateItems()
        {
            for (int i = 0; i < this.listItems.Count; ++i)
            {
                CategoryListItem item = this.listItems[i];
                Point p = new Point(0, i * item.Height);
                UIHelper.SetLocation(item, p);
            }
        }

        private void SelectDefault()
        {
            foreach (Control control in this.Controls)
            {
                if (!(control is CategoryListItem))
                    continue;
                CategoryListItem i = control as CategoryListItem;
                if (!i.Selected)
                    continue;
                i.BackColor = i.Selected_Color;
            }
        }

        private void Select(CategoryListItem item, Color defaultColor)
        {
            foreach (Control control in this.Controls)
            {
                if (!(control is CategoryListItem))
                    continue;
                CategoryListItem i = control as CategoryListItem;
                if (i == item)
                {
                    i.Selected = true;
                    i.BackColor = i.Selected_Color;
                    continue;
                }
                i.Selected = false;
                i.BackColor = defaultColor;
            }
        }

        private void SetEvents()
        {
            CategoryListItem defaultItem = (CategoryListItem)this.Controls[0];
            Color defaultColor = defaultItem.Back_Color;

            for (int i = 0; i < this.Controls.Count; ++i)
            {
                Control control = this.Controls[i];
                if (!(control is CategoryListItem))
                    continue;
                CategoryListItem item = control as CategoryListItem;
                item.Clicked += (sender, e) =>
                {
                    if (this.Clicked != null)
                    {
                        this.Select(item, defaultColor);
                        this.Clicked(item, new ClickedEventArgs(item.Title_Text));
                    }
                };
            }
        }

        private bool load;

        private void Reload()
        {
            if (this.listItems.Count <= 0)
                return;
            if (this.Controls.Count == this.listItems.Count)
            {
                if (!this.load)
                {
                    this.SelectDefault();
                    this.SetEvents();
                    this.UpdateItems();
                    this.load = true;
                }
                return;
            }

            this.UpdateItems();

            foreach (CategoryListItem item in this.listItems)
                this.Controls.Add(item);

            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.Reload();
        }

    }

}
