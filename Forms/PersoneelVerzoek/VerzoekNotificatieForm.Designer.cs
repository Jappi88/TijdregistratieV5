
namespace Forms.Verzoeken
{
    partial class VerzoekNotificatieForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerzoekNotificatieForm));
            this.xMainImage = new System.Windows.Forms.PictureBox();
            this.xclose = new System.Windows.Forms.Button();
            this.xMessagePanel = new System.Windows.Forms.Panel();
            this.xmessage = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.xMenuPanel = new System.Windows.Forms.Panel();
            this.xGoedkeuren = new System.Windows.Forms.Button();
            this.xAfkeuren = new System.Windows.Forms.Button();
            this.xtitle = new System.Windows.Forms.Label();
            this.xMainPanel = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xMainImage)).BeginInit();
            this.xMessagePanel.SuspendLayout();
            this.xMenuPanel.SuspendLayout();
            this.xMainPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // xMainImage
            // 
            this.xMainImage.BackColor = System.Drawing.Color.Transparent;
            this.xMainImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.xMainImage.Image = global::ProductieManager.Properties.Resources.transfer_man_64x64;
            this.xMainImage.Location = new System.Drawing.Point(0, 0);
            this.xMainImage.Name = "xMainImage";
            this.xMainImage.Size = new System.Drawing.Size(70, 117);
            this.xMainImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.xMainImage.TabIndex = 0;
            this.xMainImage.TabStop = false;
            this.xMainImage.Click += new System.EventHandler(this.xMain_Click);
            this.xMainImage.MouseEnter += new System.EventHandler(this.xMain_MouseEnter);
            this.xMainImage.MouseLeave += new System.EventHandler(this.xMain_MouseLeave);
            // 
            // xclose
            // 
            this.xclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xclose.BackColor = System.Drawing.Color.Transparent;
            this.xclose.FlatAppearance.BorderSize = 0;
            this.xclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xclose.Image = global::ProductieManager.Properties.Resources.cancel_close_cross_delete_32x32;
            this.xclose.Location = new System.Drawing.Point(424, -1);
            this.xclose.Name = "xclose";
            this.xclose.Size = new System.Drawing.Size(28, 28);
            this.xclose.TabIndex = 1;
            this.xclose.UseVisualStyleBackColor = false;
            this.xclose.Click += new System.EventHandler(this.xclose_Click);
            // 
            // xMessagePanel
            // 
            this.xMessagePanel.AutoScroll = true;
            this.xMessagePanel.BackColor = System.Drawing.Color.Transparent;
            this.xMessagePanel.Controls.Add(this.xmessage);
            this.xMessagePanel.Controls.Add(this.xMenuPanel);
            this.xMessagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xMessagePanel.Location = new System.Drawing.Point(70, 0);
            this.xMessagePanel.Name = "xMessagePanel";
            this.xMessagePanel.Padding = new System.Windows.Forms.Padding(5);
            this.xMessagePanel.Size = new System.Drawing.Size(377, 117);
            this.xMessagePanel.TabIndex = 2;
            // 
            // xmessage
            // 
            this.xmessage.AutoScroll = true;
            this.xmessage.AutoScrollMinSize = new System.Drawing.Size(367, 20);
            this.xmessage.BackColor = System.Drawing.Color.Transparent;
            this.xmessage.BaseStylesheet = null;
            this.xmessage.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xmessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmessage.Location = new System.Drawing.Point(5, 5);
            this.xmessage.Name = "xmessage";
            this.xmessage.Size = new System.Drawing.Size(367, 73);
            this.xmessage.TabIndex = 5;
            this.xmessage.Text = "Bericht:";
            this.xmessage.LinkClicked += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlLinkClickedEventArgs>(this.xmessage_LinkClicked);
            this.xmessage.Click += new System.EventHandler(this.xMain_Click);
            this.xmessage.MouseEnter += new System.EventHandler(this.xMain_MouseEnter);
            this.xmessage.MouseLeave += new System.EventHandler(this.xMain_MouseLeave);
            // 
            // xMenuPanel
            // 
            this.xMenuPanel.BackColor = System.Drawing.Color.Transparent;
            this.xMenuPanel.Controls.Add(this.xGoedkeuren);
            this.xMenuPanel.Controls.Add(this.xAfkeuren);
            this.xMenuPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xMenuPanel.Location = new System.Drawing.Point(5, 78);
            this.xMenuPanel.Name = "xMenuPanel";
            this.xMenuPanel.Size = new System.Drawing.Size(367, 34);
            this.xMenuPanel.TabIndex = 6;
            this.xMenuPanel.Visible = false;
            this.xMenuPanel.Click += new System.EventHandler(this.xMain_Click);
            this.xMenuPanel.MouseEnter += new System.EventHandler(this.xMain_MouseEnter);
            this.xMenuPanel.MouseLeave += new System.EventHandler(this.xMain_MouseLeave);
            // 
            // xGoedkeuren
            // 
            this.xGoedkeuren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xGoedkeuren.FlatAppearance.BorderSize = 0;
            this.xGoedkeuren.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.xGoedkeuren.FlatAppearance.MouseOverBackColor = System.Drawing.Color.PaleGreen;
            this.xGoedkeuren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xGoedkeuren.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xGoedkeuren.Location = new System.Drawing.Point(293, 0);
            this.xGoedkeuren.Name = "xGoedkeuren";
            this.xGoedkeuren.Size = new System.Drawing.Size(34, 34);
            this.xGoedkeuren.TabIndex = 1;
            this.toolTip1.SetToolTip(this.xGoedkeuren, "Goedkeuren");
            this.xGoedkeuren.UseVisualStyleBackColor = true;
            this.xGoedkeuren.Click += new System.EventHandler(this.xGoedkeuren_Click);
            // 
            // xAfkeuren
            // 
            this.xAfkeuren.Dock = System.Windows.Forms.DockStyle.Right;
            this.xAfkeuren.FlatAppearance.BorderSize = 0;
            this.xAfkeuren.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Red;
            this.xAfkeuren.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Tomato;
            this.xAfkeuren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xAfkeuren.Image = global::ProductieManager.Properties.Resources.cancel_stop_exit_1583;
            this.xAfkeuren.Location = new System.Drawing.Point(333, 0);
            this.xAfkeuren.Name = "xAfkeuren";
            this.xAfkeuren.Size = new System.Drawing.Size(34, 34);
            this.xAfkeuren.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xAfkeuren, "Afwijzen");
            this.xAfkeuren.UseVisualStyleBackColor = true;
            this.xAfkeuren.Click += new System.EventHandler(this.xAfkeuren_Click);
            // 
            // xtitle
            // 
            this.xtitle.BackColor = System.Drawing.Color.Transparent;
            this.xtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtitle.ForeColor = System.Drawing.Color.Navy;
            this.xtitle.Location = new System.Drawing.Point(5, 5);
            this.xtitle.Name = "xtitle";
            this.xtitle.Size = new System.Drawing.Size(447, 28);
            this.xtitle.TabIndex = 3;
            this.xtitle.Text = "Title";
            this.xtitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xMainPanel
            // 
            this.xMainPanel.BackColor = System.Drawing.Color.Transparent;
            this.xMainPanel.Controls.Add(this.xMessagePanel);
            this.xMainPanel.Controls.Add(this.xMainImage);
            this.xMainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xMainPanel.Location = new System.Drawing.Point(5, 33);
            this.xMainPanel.Name = "xMainPanel";
            this.xMainPanel.Size = new System.Drawing.Size(447, 117);
            this.xMainPanel.TabIndex = 4;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // VerzoekNotificatieForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Lavender;
            this.ClientSize = new System.Drawing.Size(457, 155);
            this.Controls.Add(this.xclose);
            this.Controls.Add(this.xMainPanel);
            this.Controls.Add(this.xtitle);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VerzoekNotificatieForm";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Nieuw Verzoek!";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.xMainImage)).EndInit();
            this.xMessagePanel.ResumeLayout(false);
            this.xMenuPanel.ResumeLayout(false);
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
        private System.Windows.Forms.Panel xMenuPanel;
        private System.Windows.Forms.Button xGoedkeuren;
        private System.Windows.Forms.Button xAfkeuren;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
