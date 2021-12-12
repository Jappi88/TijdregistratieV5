namespace ProductieManager.Forms.Klachten
{
    partial class KlachtControl
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
            this.xKlachtImage = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toonklachtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.wijzigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.verwijderenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xklachtinfo = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xexpand = new System.Windows.Forms.Button();
            this.xedit = new System.Windows.Forms.Button();
            this.xdelete = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.xKlachtImage)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xKlachtImage
            // 
            this.xKlachtImage.BackColor = System.Drawing.Color.Transparent;
            this.xKlachtImage.ContextMenuStrip = this.contextMenuStrip1;
            this.xKlachtImage.Dock = System.Windows.Forms.DockStyle.Left;
            this.xKlachtImage.Location = new System.Drawing.Point(0, 0);
            this.xKlachtImage.Name = "xKlachtImage";
            this.xKlachtImage.Size = new System.Drawing.Size(100, 119);
            this.xKlachtImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.xKlachtImage.TabIndex = 0;
            this.xKlachtImage.TabStop = false;
            this.xKlachtImage.Click += new System.EventHandler(this.KlachtControl_Click);
            this.xKlachtImage.DoubleClick += new System.EventHandler(this.KlachtControl_DoubleClick);
            this.xKlachtImage.MouseEnter += new System.EventHandler(this.KlachtControl_MouseEnter);
            this.xKlachtImage.MouseLeave += new System.EventHandler(this.KlachtControl_MouseLeave);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toonklachtToolStripMenuItem,
            this.toolStripSeparator2,
            this.wijzigToolStripMenuItem,
            this.toolStripSeparator1,
            this.verwijderenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 130);
            // 
            // toonklachtToolStripMenuItem
            // 
            this.toonklachtToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.Leave_80_icon_icons_com_57305;
            this.toonklachtToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toonklachtToolStripMenuItem.Name = "toonklachtToolStripMenuItem";
            this.toonklachtToolStripMenuItem.Size = new System.Drawing.Size(152, 38);
            this.toonklachtToolStripMenuItem.Text = "Toon Klacht";
            this.toonklachtToolStripMenuItem.Click += new System.EventHandler(this.xexpand_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(149, 6);
            // 
            // wijzigToolStripMenuItem
            // 
            this.wijzigToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.wijzigToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.wijzigToolStripMenuItem.Name = "wijzigToolStripMenuItem";
            this.wijzigToolStripMenuItem.Size = new System.Drawing.Size(152, 38);
            this.wijzigToolStripMenuItem.Text = "Wijzigen";
            this.wijzigToolStripMenuItem.Click += new System.EventHandler(this.xedit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
            // 
            // verwijderenToolStripMenuItem
            // 
            this.verwijderenToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.verwijderenToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.verwijderenToolStripMenuItem.Name = "verwijderenToolStripMenuItem";
            this.verwijderenToolStripMenuItem.Size = new System.Drawing.Size(152, 38);
            this.verwijderenToolStripMenuItem.Text = "Verwijderen";
            this.verwijderenToolStripMenuItem.Click += new System.EventHandler(this.xdelete_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xklachtinfo);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.xKlachtImage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(455, 119);
            this.panel1.TabIndex = 1;
            this.panel1.MouseEnter += new System.EventHandler(this.KlachtControl_MouseEnter);
            this.panel1.MouseLeave += new System.EventHandler(this.KlachtControl_MouseLeave);
            // 
            // xklachtinfo
            // 
            this.xklachtinfo.AutoSize = false;
            this.xklachtinfo.BackColor = System.Drawing.Color.Transparent;
            this.xklachtinfo.BaseStylesheet = null;
            this.xklachtinfo.ContextMenuStrip = this.contextMenuStrip1;
            this.xklachtinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xklachtinfo.IsContextMenuEnabled = false;
            this.xklachtinfo.IsSelectionEnabled = false;
            this.xklachtinfo.Location = new System.Drawing.Point(100, 0);
            this.xklachtinfo.Name = "xklachtinfo";
            this.xklachtinfo.Size = new System.Drawing.Size(316, 119);
            this.xklachtinfo.TabIndex = 1;
            this.xklachtinfo.Text = "Klacht Info";
            this.xklachtinfo.LinkClicked += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlLinkClickedEventArgs>(this.xklachtinfo_LinkClicked);
            this.xklachtinfo.Click += new System.EventHandler(this.KlachtControl_Click);
            this.xklachtinfo.DoubleClick += new System.EventHandler(this.KlachtControl_DoubleClick);
            this.xklachtinfo.MouseEnter += new System.EventHandler(this.KlachtControl_MouseEnter);
            this.xklachtinfo.MouseLeave += new System.EventHandler(this.KlachtControl_MouseLeave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xexpand);
            this.panel2.Controls.Add(this.xedit);
            this.panel2.Controls.Add(this.xdelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(416, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(39, 119);
            this.panel2.TabIndex = 5;
            // 
            // xexpand
            // 
            this.xexpand.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xexpand.BackColor = System.Drawing.Color.Transparent;
            this.xexpand.FlatAppearance.BorderSize = 0;
            this.xexpand.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xexpand.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xexpand.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xexpand.Image = global::ProductieManager.Properties.Resources.Leave_80_icon_icons_com_57305;
            this.xexpand.Location = new System.Drawing.Point(3, 3);
            this.xexpand.Name = "xexpand";
            this.xexpand.Size = new System.Drawing.Size(33, 33);
            this.xexpand.TabIndex = 2;
            this.toolTip1.SetToolTip(this.xexpand, "Toon klacht omschrijving");
            this.xexpand.UseVisualStyleBackColor = false;
            this.xexpand.Click += new System.EventHandler(this.xexpand_Click);
            // 
            // xedit
            // 
            this.xedit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xedit.BackColor = System.Drawing.Color.Transparent;
            this.xedit.FlatAppearance.BorderSize = 0;
            this.xedit.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xedit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xedit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xedit.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xedit.Location = new System.Drawing.Point(3, 42);
            this.xedit.Name = "xedit";
            this.xedit.Size = new System.Drawing.Size(33, 33);
            this.xedit.TabIndex = 3;
            this.toolTip1.SetToolTip(this.xedit, "Wijzig klacht gegevens");
            this.xedit.UseVisualStyleBackColor = false;
            this.xedit.Visible = false;
            this.xedit.Click += new System.EventHandler(this.xedit_Click);
            // 
            // xdelete
            // 
            this.xdelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xdelete.BackColor = System.Drawing.Color.Transparent;
            this.xdelete.FlatAppearance.BorderSize = 0;
            this.xdelete.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xdelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdelete.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdelete.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdelete.Location = new System.Drawing.Point(3, 81);
            this.xdelete.Name = "xdelete";
            this.xdelete.Size = new System.Drawing.Size(33, 33);
            this.xdelete.TabIndex = 4;
            this.toolTip1.SetToolTip(this.xdelete, "Verwijder Klacht");
            this.xdelete.UseVisualStyleBackColor = false;
            this.xdelete.Visible = false;
            this.xdelete.Click += new System.EventHandler(this.xdelete_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // KlachtControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "KlachtControl";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(465, 129);
            this.Click += new System.EventHandler(this.KlachtControl_Click);
            this.MouseEnter += new System.EventHandler(this.KlachtControl_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.KlachtControl_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.xKlachtImage)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox xKlachtImage;
        private System.Windows.Forms.Panel panel1;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel xklachtinfo;
        private System.Windows.Forms.Button xexpand;
        private System.Windows.Forms.Button xdelete;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button xedit;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toonklachtToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem wijzigToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem verwijderenToolStripMenuItem;
    }
}
