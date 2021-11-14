namespace Forms
{
    partial class UpdatePreviewForm
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
            this.components = new System.ComponentModel.Container();
            this.htmlPanel1 = new HtmlRenderer.HtmlPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // htmlPanel1
            // 
            this.htmlPanel1.AutoScroll = true;
            this.htmlPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.htmlPanel1.BaseStylesheet = null;
            this.htmlPanel1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.htmlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlPanel1.Location = new System.Drawing.Point(20, 60);
            this.htmlPanel1.Name = "htmlPanel1";
            this.htmlPanel1.Size = new System.Drawing.Size(960, 770);
            this.htmlPanel1.TabIndex = 0;
            this.htmlPanel1.Text = null;
            this.htmlPanel1.StylesheetLoad += new System.EventHandler<HtmlRenderer.Entities.HtmlStylesheetLoadEventArgs>(this.htmlPanel1_StylesheetLoad);
            this.htmlPanel1.ImageLoad += new System.EventHandler<HtmlRenderer.Entities.HtmlImageLoadEventArgs>(this.htmlPanel1_ImageLoad);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UpdatePreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 850);
            this.Controls.Add(this.htmlPanel1);
            this.MinimumSize = new System.Drawing.Size(1000, 850);
            this.Name = "UpdatePreviewForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Nieuw in {0}!";
            this.Shown += new System.EventHandler(this.UpdatePreviewForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private HtmlRenderer.HtmlPanel htmlPanel1;
        private System.Windows.Forms.Timer timer1;
    }
}