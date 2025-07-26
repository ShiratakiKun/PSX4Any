
namespace PSX4Any.PSX4AnyEmu.UI.Components
{
    partial class CategoryListItem
    {
        /// <summary> 
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region コンポーネント デザイナーで生成されたコード

        /// <summary> 
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を 
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.Icon = new PSX4Any.PSX4AnyEmu.UI.Components.HighQualityPictureBox();
            this.Title = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // Icon
            // 
            this.Icon.Location = new System.Drawing.Point(0, 0);
            this.Icon.Margin = new System.Windows.Forms.Padding(0);
            this.Icon.Name = "Icon";
            this.Icon.Size = new System.Drawing.Size(48, 48);
            this.Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Icon.TabIndex = 0;
            this.Icon.TabStop = false;
            // 
            // Title
            // 
            this.Title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Title.Location = new System.Drawing.Point(0, 0);
            this.Title.Margin = new System.Windows.Forms.Padding(0);
            this.Title.Name = "Title";
            this.Title.Padding = new System.Windows.Forms.Padding(56, 0, 0, 0);
            this.Title.Size = new System.Drawing.Size(256, 48);
            this.Title.TabIndex = 1;
            this.Title.Text = "Title";
            this.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CategoryListItem
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.Icon);
            this.Controls.Add(this.Title);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "CategoryListItem";
            this.Size = new System.Drawing.Size(256, 48);
            ((System.ComponentModel.ISupportInitialize)(this.Icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private HighQualityPictureBox Icon;
        private System.Windows.Forms.Label Title;
    }
}
