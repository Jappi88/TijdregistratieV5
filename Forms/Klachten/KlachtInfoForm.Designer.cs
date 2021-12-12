namespace Forms.Klachten
{
    partial class KlachtInfoForm
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
            this.xtextfield = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // xtextfield
            // 
            this.xtextfield.AutoScroll = true;
            this.xtextfield.AutoScrollMinSize = new System.Drawing.Size(774, 20);
            this.xtextfield.BackColor = System.Drawing.Color.Transparent;
            this.xtextfield.BaseStylesheet = null;
            this.xtextfield.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xtextfield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtextfield.Location = new System.Drawing.Point(20, 30);
            this.xtextfield.Name = "xtextfield";
            this.xtextfield.Size = new System.Drawing.Size(774, 432);
            this.xtextfield.TabIndex = 4;
            this.xtextfield.Text = "Text Veld";
            this.xtextfield.LinkClicked += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlLinkClickedEventArgs>(this.xtextfield_LinkClicked);
            this.xtextfield.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xtextfield_ImageLoad);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // KlachtInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(814, 482);
            this.Controls.Add(this.xtextfield);
            this.DisplayHeader = false;
            this.Name = "KlachtInfoForm";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Klacht Omschrijving";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KlachtInfoForm_FormClosing);
            this.Shown += new System.EventHandler(this.KlachtInfoForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel xtextfield;
        private System.Windows.Forms.Timer timer1;
    }
}