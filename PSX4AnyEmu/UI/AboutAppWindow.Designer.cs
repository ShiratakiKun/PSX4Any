
namespace PSX4Any.PSX4AnyEmu.UI
{
    partial class AboutAppWindow
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
            this.Title = new System.Windows.Forms.Label();
            this.AppIcon = new PSX4Any.PSX4AnyEmu.UI.Components.HighQualityPictureBox();
            this.License = new System.Windows.Forms.Label();
            this.Credit = new System.Windows.Forms.Label();
            this.CreditURL = new System.Windows.Forms.Label();
            this.Desc = new System.Windows.Forms.Label();
            this.CloseButton = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.AppIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // Title
            // 
            this.Title.AutoSize = true;
            this.Title.Font = new System.Drawing.Font("Arial", 13.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Title.Location = new System.Drawing.Point(132, 64);
            this.Title.Margin = new System.Windows.Forms.Padding(0);
            this.Title.Name = "Title";
            this.Title.Size = new System.Drawing.Size(217, 29);
            this.Title.TabIndex = 0;
            this.Title.Text = "PSX4Any (ver 0.1)";
            // 
            // AppIcon
            // 
            this.AppIcon.Image = global::PSX4Any.Properties.Resources.Icon;
            this.AppIcon.Location = new System.Drawing.Point(207, 13);
            this.AppIcon.Margin = new System.Windows.Forms.Padding(0);
            this.AppIcon.Name = "AppIcon";
            this.AppIcon.Size = new System.Drawing.Size(48, 48);
            this.AppIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.AppIcon.TabIndex = 1;
            this.AppIcon.TabStop = false;
            // 
            // License
            // 
            this.License.AutoSize = true;
            this.License.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.License.Location = new System.Drawing.Point(161, 207);
            this.License.Margin = new System.Windows.Forms.Padding(0);
            this.License.Name = "License";
            this.License.Size = new System.Drawing.Size(159, 19);
            this.License.TabIndex = 2;
            this.License.Text = "License: MIT license";
            // 
            // Credit
            // 
            this.Credit.AutoSize = true;
            this.Credit.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Credit.Location = new System.Drawing.Point(100, 226);
            this.Credit.Margin = new System.Windows.Forms.Padding(0);
            this.Credit.Name = "Credit";
            this.Credit.Size = new System.Drawing.Size(280, 19);
            this.Credit.TabIndex = 3;
            this.Credit.Text = "Original Developed by BluestormDNA";
            // 
            // CreditURL
            // 
            this.CreditURL.AutoSize = true;
            this.CreditURL.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CreditURL.Location = new System.Drawing.Point(71, 245);
            this.CreditURL.Margin = new System.Windows.Forms.Padding(0);
            this.CreditURL.Name = "CreditURL";
            this.CreditURL.Size = new System.Drawing.Size(338, 19);
            this.CreditURL.TabIndex = 4;
            this.CreditURL.Text = "https://github.com/BluestormDNA/ProjectPSX";
            // 
            // Desc
            // 
            this.Desc.AutoSize = true;
            this.Desc.Font = new System.Drawing.Font("Arial", 10.2F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Desc.Location = new System.Drawing.Point(75, 93);
            this.Desc.Margin = new System.Windows.Forms.Padding(0);
            this.Desc.Name = "Desc";
            this.Desc.Size = new System.Drawing.Size(316, 19);
            this.Desc.TabIndex = 5;
            this.Desc.Text = "ProjectPSX port to .NET Framework 4.5";
            // 
            // CloseButton
            // 
            this.CloseButton.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CloseButton.Location = new System.Drawing.Point(192, 279);
            this.CloseButton.Margin = new System.Windows.Forms.Padding(0);
            this.CloseButton.Name = "CloseButton";
            this.CloseButton.Size = new System.Drawing.Size(96, 32);
            this.CloseButton.TabIndex = 6;
            this.CloseButton.Text = "Close";
            this.CloseButton.UseVisualStyleBackColor = true;
            // 
            // AboutAppWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 320);
            this.Controls.Add(this.AppIcon);
            this.Controls.Add(this.Title);
            this.Controls.Add(this.Desc);
            this.Controls.Add(this.License);
            this.Controls.Add(this.Credit);
            this.Controls.Add(this.CreditURL);
            this.Controls.Add(this.CloseButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "AboutAppWindow";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AboutApp";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.AppIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label Title;
        private PSX4Any.PSX4AnyEmu.UI.Components.HighQualityPictureBox AppIcon;
        private System.Windows.Forms.Label License;
        private System.Windows.Forms.Label Credit;
        private System.Windows.Forms.Label CreditURL;
        private System.Windows.Forms.Label Desc;
        private System.Windows.Forms.Button CloseButton;
    }
}