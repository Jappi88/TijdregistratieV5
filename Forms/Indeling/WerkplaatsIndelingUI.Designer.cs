using TheArtOfDev.HtmlRenderer.WinForms;

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
            this.components = new System.ComponentModel.Container();
            this.xWerkplaatsIndelingPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.xCollapseButton = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xAddPersoneel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xautoindeling = new System.Windows.Forms.ToolStripButton();
            this.xDeletePersoneel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.xreset = new System.Windows.Forms.ToolStripButton();
            this.xGeselecteerdeGebruikerLabel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xPreviousIndeling = new System.Windows.Forms.Button();
            this.xNextIndeling = new System.Windows.Forms.Button();
            this.xIndelingPanel = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.productieListControl1 = new Controls.ProductieListControl();
            this.toolStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.xIndelingPanel.SuspendLayout();
            this.SuspendLayout();
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
            this.xWerkplaatsIndelingPanel.Padding = new System.Windows.Forms.Padding(2);
            this.xWerkplaatsIndelingPanel.Size = new System.Drawing.Size(486, 690);
            this.xWerkplaatsIndelingPanel.TabIndex = 2;
            this.xWerkplaatsIndelingPanel.WrapContents = false;
            this.xWerkplaatsIndelingPanel.Click += new System.EventHandler(this.xWerkplaatsIndelingPanel_Click);
            this.xWerkplaatsIndelingPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragDrop);
            this.xWerkplaatsIndelingPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.flowLayoutPanel1_DragEnter);
            // 
            // xCollapseButton
            // 
            this.xCollapseButton.BackColor = System.Drawing.Color.White;
            this.xCollapseButton.Dock = System.Windows.Forms.DockStyle.Right;
            this.xCollapseButton.FlatAppearance.BorderSize = 0;
            this.xCollapseButton.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.xCollapseButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xCollapseButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xCollapseButton.Image = global::ProductieManager.Properties.Resources.Navigate_left_36746;
            this.xCollapseButton.Location = new System.Drawing.Point(486, 39);
            this.xCollapseButton.Name = "xCollapseButton";
            this.xCollapseButton.Size = new System.Drawing.Size(24, 690);
            this.xCollapseButton.TabIndex = 0;
            this.xCollapseButton.UseVisualStyleBackColor = false;
            this.xCollapseButton.Click += new System.EventHandler(this.xCollapseButton_Click);
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
            this.toolStrip1.Size = new System.Drawing.Size(510, 39);
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
            this.xautoindeling.ToolTipText = "Deel alle producties automatisch in";
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
            // xGeselecteerdeGebruikerLabel
            // 
            this.xGeselecteerdeGebruikerLabel.AutoSize = false;
            this.xGeselecteerdeGebruikerLabel.BackColor = System.Drawing.Color.White;
            this.xGeselecteerdeGebruikerLabel.BaseStylesheet = null;
            this.xGeselecteerdeGebruikerLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xGeselecteerdeGebruikerLabel.IsContextMenuEnabled = false;
            this.xGeselecteerdeGebruikerLabel.IsSelectionEnabled = false;
            this.xGeselecteerdeGebruikerLabel.Location = new System.Drawing.Point(75, 2);
            this.xGeselecteerdeGebruikerLabel.Name = "xGeselecteerdeGebruikerLabel";
            this.xGeselecteerdeGebruikerLabel.Size = new System.Drawing.Size(687, 48);
            this.xGeselecteerdeGebruikerLabel.TabIndex = 2;
            this.xGeselecteerdeGebruikerLabel.Text = "htmlLabel1";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.productieListControl1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(510, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(764, 729);
            this.panel2.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xGeselecteerdeGebruikerLabel);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(2);
            this.panel1.Size = new System.Drawing.Size(764, 52);
            this.panel1.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xPreviousIndeling);
            this.panel3.Controls.Add(this.xNextIndeling);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(2, 2);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(2);
            this.panel3.Size = new System.Drawing.Size(73, 48);
            this.panel3.TabIndex = 0;
            // 
            // xPreviousIndeling
            // 
            this.xPreviousIndeling.BackColor = System.Drawing.Color.White;
            this.xPreviousIndeling.Dock = System.Windows.Forms.DockStyle.Left;
            this.xPreviousIndeling.FlatAppearance.BorderSize = 0;
            this.xPreviousIndeling.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.xPreviousIndeling.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xPreviousIndeling.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xPreviousIndeling.Image = global::ProductieManager.Properties.Resources.Navigate_left_36746;
            this.xPreviousIndeling.Location = new System.Drawing.Point(2, 2);
            this.xPreviousIndeling.Name = "xPreviousIndeling";
            this.xPreviousIndeling.Size = new System.Drawing.Size(32, 44);
            this.xPreviousIndeling.TabIndex = 2;
            this.toolTip1.SetToolTip(this.xPreviousIndeling, "Vorige indeling");
            this.xPreviousIndeling.UseVisualStyleBackColor = false;
            this.xPreviousIndeling.Click += new System.EventHandler(this.xPreviousIndeling_Click);
            // 
            // xNextIndeling
            // 
            this.xNextIndeling.BackColor = System.Drawing.Color.White;
            this.xNextIndeling.Dock = System.Windows.Forms.DockStyle.Right;
            this.xNextIndeling.FlatAppearance.BorderSize = 0;
            this.xNextIndeling.FlatAppearance.MouseDownBackColor = System.Drawing.Color.LightSkyBlue;
            this.xNextIndeling.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xNextIndeling.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xNextIndeling.Image = global::ProductieManager.Properties.Resources.Navigate_right_36745;
            this.xNextIndeling.Location = new System.Drawing.Point(39, 2);
            this.xNextIndeling.Name = "xNextIndeling";
            this.xNextIndeling.Size = new System.Drawing.Size(32, 44);
            this.xNextIndeling.TabIndex = 1;
            this.toolTip1.SetToolTip(this.xNextIndeling, "Volgende indeling");
            this.xNextIndeling.UseVisualStyleBackColor = false;
            this.xNextIndeling.Click += new System.EventHandler(this.xNextIndeling_Click);
            // 
            // xIndelingPanel
            // 
            this.xIndelingPanel.AutoScroll = true;
            this.xIndelingPanel.Controls.Add(this.xWerkplaatsIndelingPanel);
            this.xIndelingPanel.Controls.Add(this.xCollapseButton);
            this.xIndelingPanel.Controls.Add(this.toolStrip1);
            this.xIndelingPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.xIndelingPanel.Location = new System.Drawing.Point(0, 0);
            this.xIndelingPanel.Name = "xIndelingPanel";
            this.xIndelingPanel.Size = new System.Drawing.Size(510, 729);
            this.xIndelingPanel.TabIndex = 3;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
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
            this.productieListControl1.ListName = "WerkplaatsIndeling";
            this.productieListControl1.Location = new System.Drawing.Point(0, 52);
            this.productieListControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productieListControl1.Name = "productieListControl1";
            this.productieListControl1.SelectedItem = null;
            this.productieListControl1.ShowWaitUI = false;
            this.productieListControl1.Size = new System.Drawing.Size(764, 677);
            this.productieListControl1.TabIndex = 4;
            this.productieListControl1.ValidHandler = null;
            this.productieListControl1.SelectedItemChanged += new System.EventHandler(this.ProductieListControl1_SelectedItemChanged);
            this.productieListControl1.ItemCountChanged += new System.EventHandler(this.ProductieListControl1_ItemCountChanged);
            this.productieListControl1.SearchItems += new System.EventHandler(this.ProductieListControl1_SearchItems);
            // 
            // WerkplaatsIndelingUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.xIndelingPanel);
            this.DoubleBuffered = true;
            this.Name = "WerkplaatsIndelingUI";
            this.Size = new System.Drawing.Size(1274, 729);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.xIndelingPanel.ResumeLayout(false);
            this.xIndelingPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton xAddPersoneel;
        private System.Windows.Forms.ToolStripButton xDeletePersoneel;
        private System.Windows.Forms.FlowLayoutPanel xWerkplaatsIndelingPanel;
        private HtmlLabel xGeselecteerdeGebruikerLabel;
        private System.Windows.Forms.ToolStripButton xautoindeling;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton xreset;
        private System.Windows.Forms.Button xCollapseButton;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel xIndelingPanel;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button xPreviousIndeling;
        private System.Windows.Forms.Button xNextIndeling;
        private Controls.ProductieListControl productieListControl1;
    }
}