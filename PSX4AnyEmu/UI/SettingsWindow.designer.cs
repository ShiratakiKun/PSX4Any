
namespace PSX4Any.PSX4AnyEmu.UI
{
    partial class SettingsWindow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.EnableSound = new System.Windows.Forms.CheckBox();
            this.SoundTitle = new System.Windows.Forms.Label();
            this.TitleBar = new System.Windows.Forms.Panel();
            this.Options = new System.Windows.Forms.Panel();
            this.CategoryList = new PSX4Any.PSX4AnyEmu.UI.Components.CategoryListView();
            this.Sound = new PSX4Any.PSX4AnyEmu.UI.Components.CategoryListItem();
            this.TitleBar.SuspendLayout();
            this.Options.SuspendLayout();
            this.CategoryList.SuspendLayout();
            this.SuspendLayout();
            // 
            // EnableSound
            // 
            this.EnableSound.AutoSize = true;
            this.EnableSound.Font = new System.Drawing.Font("Bahnschrift", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.EnableSound.Location = new System.Drawing.Point(16, 48);
            this.EnableSound.Margin = new System.Windows.Forms.Padding(0);
            this.EnableSound.Name = "EnableSound";
            this.EnableSound.Size = new System.Drawing.Size(134, 25);
            this.EnableSound.TabIndex = 1;
            this.EnableSound.Text = "Enable Sound";
            this.EnableSound.UseVisualStyleBackColor = true;
            // 
            // SoundTitle
            // 
            this.SoundTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SoundTitle.Font = new System.Drawing.Font("Arial", 10F);
            this.SoundTitle.ForeColor = System.Drawing.Color.White;
            this.SoundTitle.Location = new System.Drawing.Point(0, 0);
            this.SoundTitle.Margin = new System.Windows.Forms.Padding(0);
            this.SoundTitle.Name = "SoundTitle";
            this.SoundTitle.Size = new System.Drawing.Size(320, 32);
            this.SoundTitle.TabIndex = 0;
            this.SoundTitle.Text = "Sound";
            this.SoundTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // TitleBar
            // 
            this.TitleBar.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.TitleBar.Controls.Add(this.SoundTitle);
            this.TitleBar.Location = new System.Drawing.Point(0, 0);
            this.TitleBar.Margin = new System.Windows.Forms.Padding(0);
            this.TitleBar.Name = "TitleBar";
            this.TitleBar.Size = new System.Drawing.Size(320, 32);
            this.TitleBar.TabIndex = 3;
            // 
            // Options
            // 
            this.Options.BackColor = System.Drawing.Color.Transparent;
            this.Options.Controls.Add(this.TitleBar);
            this.Options.Controls.Add(this.EnableSound);
            this.Options.Location = new System.Drawing.Point(160, 0);
            this.Options.Margin = new System.Windows.Forms.Padding(0);
            this.Options.Name = "Options";
            this.Options.Size = new System.Drawing.Size(320, 320);
            this.Options.TabIndex = 4;
            // 
            // CategoryList
            // 
            this.CategoryList.BackColor = System.Drawing.SystemColors.ControlLight;
            this.CategoryList.Controls.Add(this.Sound);
            this.CategoryList.ITEMS.Add(this.Sound);
            this.CategoryList.Location = new System.Drawing.Point(0, 0);
            this.CategoryList.Margin = new System.Windows.Forms.Padding(0);
            this.CategoryList.Name = "CategoryList";
            this.CategoryList.Size = new System.Drawing.Size(160, 320);
            this.CategoryList.TabIndex = 2;
            // 
            // Sound
            // 
            this.Sound.Back_Color = System.Drawing.Color.Transparent;
            this.Sound.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.Sound.Font = new System.Drawing.Font("MS UI Gothic", 11F);
            this.Sound.ForeColor = System.Drawing.Color.White;
            this.Sound.Icon_Image = global::PSX4Any.Properties.Resources.Sound;
            this.Sound.Location = new System.Drawing.Point(0, 0);
            this.Sound.Margin = new System.Windows.Forms.Padding(0);
            this.Sound.Name = "Sound";
            this.Sound.Selected = true;
            this.Sound.Selected_Color = System.Drawing.SystemColors.ActiveCaption;
            this.Sound.Size = new System.Drawing.Size(160, 48);
            this.Sound.TabIndex = 1;
            this.Sound.Title_Text = "Sound";
            // 
            // SettingsWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 320);
            this.Controls.Add(this.CategoryList);
            this.Controls.Add(this.Options);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingsWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Settings";
            this.TopMost = true;
            this.TitleBar.ResumeLayout(false);
            this.Options.ResumeLayout(false);
            this.Options.PerformLayout();
            this.CategoryList.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.CheckBox EnableSound;
        private System.Windows.Forms.Label SoundTitle;
        private Components.CategoryListView CategoryList;
        private Components.CategoryListItem Sound;
        private System.Windows.Forms.Panel TitleBar;
        private System.Windows.Forms.Panel Options;
    }
}