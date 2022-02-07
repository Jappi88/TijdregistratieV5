using TheArtOfDev.HtmlRenderer.WinForms;

namespace Forms
{
    partial class WerkplaatsIndeling
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WerkplaatsIndeling));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xWerkplaatsIndelingPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xAddPersoneel = new System.Windows.Forms.ToolStripButton();
            this.xDeletePersoneel = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.productieListControl1 = new Controls.ProductieListControl();
            this.xGeselecteerdeGebruikerLabel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.xloadinglabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xWerkplaatsIndelingPanel);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(490, 561);
            this.panel1.TabIndex = 0;
            // 
            // xWerkplaatsIndelingPanel
            // 
            this.xWerkplaatsIndelingPanel.AllowDrop = true;
            this.xWerkplaatsIndelingPanel.AutoScroll = true;
            this.xWerkplaatsIndelingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xWerkplaatsIndelingPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.xWerkplaatsIndelingPanel.Location = new System.Drawing.Point(0, 39);
            this.xWerkplaatsIndelingPanel.Name = "xWerkplaatsIndelingPanel";
            this.xWerkplaatsIndelingPanel.Size = new System.Drawing.Size(490, 522);
            this.xWerkplaatsIndelingPanel.TabIndex = 2;
            this.xWerkplaatsIndelingPanel.WrapContents = false;
            this.xWerkplaatsIndelingPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragDrop);
            this.xWerkplaatsIndelingPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragEnter);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xAddPersoneel,
            this.xDeletePersoneel});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(490, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // xAddPersoneel
            // 
            this.xAddPersoneel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xAddPersoneel.Image = global::ProductieManager.Properties.Resources.add_Blue_circle_32x32;
            this.xAddPersoneel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xAddPersoneel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xAddPersoneel.Name = "xAddPersoneel";
            this.xAddPersoneel.Size = new System.Drawing.Size(36, 36);
            this.xAddPersoneel.ToolTipText = "Voeg nieuwe werkplaats toe";
            this.xAddPersoneel.Click += new System.EventHandler(this.xAddPersoneel_Click);
            // 
            // xDeletePersoneel
            // 
            this.xDeletePersoneel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xDeletePersoneel.Enabled = false;
            this.xDeletePersoneel.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xDeletePersoneel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xDeletePersoneel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xDeletePersoneel.Name = "xDeletePersoneel";
            this.xDeletePersoneel.Size = new System.Drawing.Size(36, 36);
            this.xDeletePersoneel.ToolTipText = "Verwijder geselecteerde werkplaatsen";
            this.xDeletePersoneel.Click += new System.EventHandler(this.xDeletePersoneel_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(10, 60);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1MinSize = 480;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.productieListControl1);
            this.splitContainer1.Panel2.Controls.Add(this.xGeselecteerdeGebruikerLabel);
            this.splitContainer1.Panel2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer1.Size = new System.Drawing.Size(1130, 561);
            this.splitContainer1.SplitterDistance = 490;
            this.splitContainer1.TabIndex = 1;
            // 
            // productieListControl1
            // 
            this.productieListControl1.AutoScroll = true;
            this.productieListControl1.BackColor = System.Drawing.Color.White;
            this.productieListControl1.CanLoad = false;
            this.productieListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieListControl1.EnableEntryFiltering = true;
            this.productieListControl1.EnableFiltering = true;
            this.productieListControl1.EnableSync = false;
            this.productieListControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieListControl1.IsBewerkingView = true;
            this.productieListControl1.ListName = "PersoneelIndelingLijst";
            this.productieListControl1.Location = new System.Drawing.Point(0, 20);
            this.productieListControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productieListControl1.Name = "productieListControl1";
            this.productieListControl1.RemoveCustomItemIfNotValid = false;
            this.productieListControl1.SelectedItem = null;
            this.productieListControl1.Size = new System.Drawing.Size(636, 541);
            this.productieListControl1.TabIndex = 0;
            this.productieListControl1.ValidHandler = null;
            // 
            // xGeselecteerdeGebruikerLabel
            // 
            this.xGeselecteerdeGebruikerLabel.BackColor = System.Drawing.Color.Transparent;
            this.xGeselecteerdeGebruikerLabel.BaseStylesheet = null;
            this.xGeselecteerdeGebruikerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xGeselecteerdeGebruikerLabel.Location = new System.Drawing.Point(0, 0);
            this.xGeselecteerdeGebruikerLabel.Name = "xGeselecteerdeGebruikerLabel";
            this.xGeselecteerdeGebruikerLabel.Size = new System.Drawing.Size(636, 20);
            this.xGeselecteerdeGebruikerLabel.TabIndex = 2;
            this.xGeselecteerdeGebruikerLabel.Text = "htmlLabel1";
            // 
            // xloadinglabel
            // 
            this.xloadinglabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xloadinglabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xloadinglabel.Location = new System.Drawing.Point(13, 60);
            this.xloadinglabel.Name = "xloadinglabel";
            this.xloadinglabel.Size = new System.Drawing.Size(1136, 570);
            this.xloadinglabel.TabIndex = 31;
            this.xloadinglabel.Text = "Indeling laden...";
            this.xloadinglabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // WerkplaatsIndeling
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1150, 631);
            this.Controls.Add(this.xloadinglabel);
            this.Controls.Add(this.splitContainer1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WerkplaatsIndeling";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.Text = "Werkplaats Indeling";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WerkplaatsIndeling_FormClosing);
            this.Shown += new System.EventHandler(this.WerkplaatsIndeling_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private Controls.ProductieListControl productieListControl1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton xAddPersoneel;
        private System.Windows.Forms.ToolStripButton xDeletePersoneel;
        private System.Windows.Forms.Label xloadinglabel;
        private System.Windows.Forms.FlowLayoutPanel xWerkplaatsIndelingPanel;
        private HtmlLabel xGeselecteerdeGebruikerLabel;
    }
}