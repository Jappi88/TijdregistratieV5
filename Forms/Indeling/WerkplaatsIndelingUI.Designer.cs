﻿using TheArtOfDev.HtmlRenderer.WinForms;

namespace Forms
{
    partial class WerkplaatsIndelingUI
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.xWerkplaatsIndelingPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xAddPersoneel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xautoindeling = new System.Windows.Forms.ToolStripButton();
            this.xDeletePersoneel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.xreset = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.productieListControl1 = new Controls.ProductieListControl();
            this.xGeselecteerdeGebruikerLabel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
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
            this.panel1.Size = new System.Drawing.Size(490, 631);
            this.panel1.TabIndex = 0;
            // 
            // xWerkplaatsIndelingPanel
            // 
            this.xWerkplaatsIndelingPanel.AllowDrop = true;
            this.xWerkplaatsIndelingPanel.AutoScroll = true;
            this.xWerkplaatsIndelingPanel.BackColor = System.Drawing.Color.White;
            this.xWerkplaatsIndelingPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xWerkplaatsIndelingPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.xWerkplaatsIndelingPanel.Location = new System.Drawing.Point(0, 39);
            this.xWerkplaatsIndelingPanel.Name = "xWerkplaatsIndelingPanel";
            this.xWerkplaatsIndelingPanel.Size = new System.Drawing.Size(490, 592);
            this.xWerkplaatsIndelingPanel.TabIndex = 2;
            this.xWerkplaatsIndelingPanel.WrapContents = false;
            this.xWerkplaatsIndelingPanel.Click += new System.EventHandler(this.xWerkplaatsIndelingPanel_Click);
            this.xWerkplaatsIndelingPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragDrop);
            this.xWerkplaatsIndelingPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragEnter);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xAddPersoneel,
            this.toolStripSeparator1,
            this.xautoindeling,
            this.xDeletePersoneel,
            this.toolStripSeparator2,
            this.xreset});
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
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // xautoindeling
            // 
            this.xautoindeling.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xautoindeling.Image = global::ProductieManager.Properties.Resources.indelen_32x32;
            this.xautoindeling.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xautoindeling.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xautoindeling.Name = "xautoindeling";
            this.xautoindeling.Size = new System.Drawing.Size(36, 36);
            this.xautoindeling.ToolTipText = "Deel alle producties automatch in";
            this.xautoindeling.Click += new System.EventHandler(this.xautoindeling_Click);
            // 
            // xDeletePersoneel
            // 
            this.xDeletePersoneel.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
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
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 39);
            // 
            // xreset
            // 
            this.xreset.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xreset.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.xreset.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xreset.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xreset.Name = "xreset";
            this.xreset.Size = new System.Drawing.Size(36, 36);
            this.xreset.ToolTipText = "Reset alle ingedeelde producties";
            this.xreset.Click += new System.EventHandler(this.xreset_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1MinSize = 490;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.productieListControl1);
            this.splitContainer1.Panel2.Controls.Add(this.xGeselecteerdeGebruikerLabel);
            this.splitContainer1.Panel2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.splitContainer1.Size = new System.Drawing.Size(1150, 631);
            this.splitContainer1.SplitterDistance = 490;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitContainer1_SplitterMoved);
            // 
            // productieListControl1
            // 
            this.productieListControl1.AutoScroll = true;
            this.productieListControl1.BackColor = System.Drawing.Color.White;
            this.productieListControl1.CanLoad = false;
            this.productieListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieListControl1.EnableCheckBox = false;
            this.productieListControl1.EnableContextMenu = true;
            this.productieListControl1.EnableEntryFiltering = true;
            this.productieListControl1.EnableFiltering = true;
            this.productieListControl1.EnableSync = false;
            this.productieListControl1.EnableToolBar = true;
            this.productieListControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieListControl1.IsBewerkingView = true;
            this.productieListControl1.ListName = "WerkplaatsIndelingLijst";
            this.productieListControl1.Location = new System.Drawing.Point(0, 40);
            this.productieListControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productieListControl1.Name = "productieListControl1";
            this.productieListControl1.SelectedItem = null;
            this.productieListControl1.ShowWaitUI = true;
            this.productieListControl1.Size = new System.Drawing.Size(655, 591);
            this.productieListControl1.TabIndex = 0;
            this.productieListControl1.ValidHandler = null;
            // 
            // xGeselecteerdeGebruikerLabel
            // 
            this.xGeselecteerdeGebruikerLabel.AutoSize = false;
            this.xGeselecteerdeGebruikerLabel.BackColor = System.Drawing.Color.White;
            this.xGeselecteerdeGebruikerLabel.BaseStylesheet = null;
            this.xGeselecteerdeGebruikerLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xGeselecteerdeGebruikerLabel.IsContextMenuEnabled = false;
            this.xGeselecteerdeGebruikerLabel.IsSelectionEnabled = false;
            this.xGeselecteerdeGebruikerLabel.Location = new System.Drawing.Point(0, 0);
            this.xGeselecteerdeGebruikerLabel.Name = "xGeselecteerdeGebruikerLabel";
            this.xGeselecteerdeGebruikerLabel.Size = new System.Drawing.Size(655, 40);
            this.xGeselecteerdeGebruikerLabel.TabIndex = 2;
            this.xGeselecteerdeGebruikerLabel.Text = "htmlLabel1";
            // 
            // WerkplaatsIndelingUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Name = "WerkplaatsIndelingUI";
            this.Size = new System.Drawing.Size(1150, 631);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
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
        private System.Windows.Forms.FlowLayoutPanel xWerkplaatsIndelingPanel;
        private HtmlLabel xGeselecteerdeGebruikerLabel;
        private System.Windows.Forms.ToolStripButton xautoindeling;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton xreset;
    }
}