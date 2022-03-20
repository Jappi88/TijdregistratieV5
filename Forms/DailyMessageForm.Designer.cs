namespace Forms
{
    partial class DailyMessageForm
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
            this.htmlPanel1 = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.SuspendLayout();
            // 
            // htmlPanel1
            // 
            this.htmlPanel1.AutoScroll = true;
            this.htmlPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.htmlPanel1.BaseStylesheet = "StyleSheet";
            this.htmlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlPanel1.Location = new System.Drawing.Point(20, 60);
            this.htmlPanel1.Name = "htmlPanel1";
            this.htmlPanel1.Size = new System.Drawing.Size(1082, 622);
            this.htmlPanel1.TabIndex = 0;
            this.htmlPanel1.Text = null;
            this.htmlPanel1.LinkClicked += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlLinkClickedEventArgs>(this.htmlPanel1_LinkClicked);
            this.htmlPanel1.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.htmlPanel1_ImageLoad);
            // 
            // DailyMessageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1122, 702);
            this.Controls.Add(this.htmlPanel1);
            this.Name = "DailyMessageForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "DailyMessageForm";
            this.Title = "DailyMessageForm";
            this.TopMost = true;
            this.ResumeLayout(false);

        }

        #endregion

        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel htmlPanel1;
    }
}