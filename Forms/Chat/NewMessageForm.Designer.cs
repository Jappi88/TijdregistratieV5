
namespace Forms.Chat
{
    partial class NewMessageForm
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewMessageForm));
            this.xMainImage = new System.Windows.Forms.PictureBox();
            this.xclose = new System.Windows.Forms.Button();
            this.xMessagePanel = new System.Windows.Forms.Panel();
            this.xmessage = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.xtitle = new System.Windows.Forms.Label();
            this.xMainPanel = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.xMainImage)).BeginInit();
            this.xMessagePanel.SuspendLayout();
            this.xMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // xMainImage
            // 
            this.xMainImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.xMainImage.Image = global::ProductieManager.Properties.Resources.chat_26_icon_96x96;
            this.xMainImage.Location = new System.Drawing.Point(0, 0);
            this.xMainImage.Name = "xMainImage";
            this.xMainImage.Size = new System.Drawing.Size(70, 105);
            this.xMainImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.xMainImage.TabIndex = 0;
            this.xMainImage.TabStop = false;
            this.xMainImage.Click += new System.EventHandler(this.xMain_Click);
            this.xMainImage.MouseEnter += new System.EventHandler(this.xMain_MouseEnter);
            this.xMainImage.MouseLeave += new System.EventHandler(this.xMain_MouseLeave);
            // 
            // xclose
            // 
            this.xclose.BackColor = System.Drawing.Color.Transparent;
            this.xclose.FlatAppearance.BorderSize = 0;
            this.xclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xclose.Image = global::ProductieManager.Properties.Resources.cancel_close_cross_delete_32x32;
            this.xclose.Location = new System.Drawing.Point(376, 3);
            this.xclose.Name = "xclose";
            this.xclose.Size = new System.Drawing.Size(28, 28);
            this.xclose.TabIndex = 1;
            this.xclose.UseVisualStyleBackColor = false;
            this.xclose.Click += new System.EventHandler(this.xclose_Click);
            // 
            // xMessagePanel
            // 
            this.xMessagePanel.AutoScroll = true;
            this.xMessagePanel.Controls.Add(this.xmessage);
            this.xMessagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xMessagePanel.Location = new System.Drawing.Point(70, 0);
            this.xMessagePanel.Name = "xMessagePanel";
            this.xMessagePanel.Padding = new System.Windows.Forms.Padding(5);
            this.xMessagePanel.Size = new System.Drawing.Size(329, 105);
            this.xMessagePanel.TabIndex = 2;
            // 
            // xmessage
            // 
            this.xmessage.AutoScroll = true;
            this.xmessage.AutoScrollMinSize = new System.Drawing.Size(319, 20);
            this.xmessage.BackColor = System.Drawing.Color.Transparent;
            this.xmessage.BaseStylesheet = null;
            this.xmessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmessage.Location = new System.Drawing.Point(5, 5);
            this.xmessage.Name = "xmessage";
            this.xmessage.Size = new System.Drawing.Size(319, 95);
            this.xmessage.TabIndex = 5;
            this.xmessage.Text = "Bericht:";
            this.xmessage.LinkClicked += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlLinkClickedEventArgs>(this.xmessage_LinkClicked);
            this.xmessage.Click += new System.EventHandler(this.xMain_Click);
            this.xmessage.MouseEnter += new System.EventHandler(this.xMain_MouseEnter);
            this.xmessage.MouseLeave += new System.EventHandler(this.xMain_MouseLeave);
            // 
            // xtitle
            // 
            this.xtitle.BackColor = System.Drawing.Color.Transparent;
            this.xtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtitle.ForeColor = System.Drawing.Color.SteelBlue;
            this.xtitle.Location = new System.Drawing.Point(5, 5);
            this.xtitle.Name = "xtitle";
            this.xtitle.Size = new System.Drawing.Size(399, 28);
            this.xtitle.TabIndex = 3;
            this.xtitle.Text = "Title";
            this.xtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xMainPanel
            // 
            this.xMainPanel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.xMainPanel.Controls.Add(this.xMessagePanel);
            this.xMainPanel.Controls.Add(this.xMainImage);
            this.xMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xMainPanel.Location = new System.Drawing.Point(5, 33);
            this.xMainPanel.Name = "xMainPanel";
            this.xMainPanel.Size = new System.Drawing.Size(399, 105);
            this.xMainPanel.TabIndex = 4;
            // 
            // NewMessageForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.LightSteelBlue;
            this.ClientSize = new System.Drawing.Size(409, 143);
            this.Controls.Add(this.xMainPanel);
            this.Controls.Add(this.xclose);
            this.Controls.Add(this.xtitle);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NewMessageForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.xMainImage)).EndInit();
            this.xMessagePanel.ResumeLayout(false);
            this.xMainPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox xMainImage;
        private System.Windows.Forms.Button xclose;
        private System.Windows.Forms.Panel xMessagePanel;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel xmessage;
        private System.Windows.Forms.Label xtitle;
        private System.Windows.Forms.Panel xMainPanel;
    }
}
