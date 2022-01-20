namespace Forms
{
    partial class SporenForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.xVerpakkingen = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verwijderenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.xsearch = new MetroFramework.Controls.MetroTextBox();
            this.xadd = new System.Windows.Forms.Button();
            this.xdelete = new System.Windows.Forms.Button();
            this.xcontainer = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.productieVerbruikUI1 = new Controls.ProductieVerbruikUI();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xVerpakkingen)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.xcontainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xVerpakkingen);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(319, 401);
            this.panel1.TabIndex = 0;
            // 
            // xVerpakkingen
            // 
            this.xVerpakkingen.AllColumns.Add(this.olvColumn1);
            this.xVerpakkingen.AllColumns.Add(this.olvColumn2);
            this.xVerpakkingen.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.xVerpakkingen.CellEditUseWholeCell = false;
            this.xVerpakkingen.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.xVerpakkingen.ContextMenuStrip = this.contextMenuStrip1;
            this.xVerpakkingen.Cursor = System.Windows.Forms.Cursors.Default;
            this.xVerpakkingen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xVerpakkingen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xVerpakkingen.FullRowSelect = true;
            this.xVerpakkingen.GridLines = true;
            this.xVerpakkingen.HideSelection = false;
            this.xVerpakkingen.LargeImageList = this.imageList1;
            this.xVerpakkingen.Location = new System.Drawing.Point(0, 32);
            this.xVerpakkingen.Name = "xVerpakkingen";
            this.xVerpakkingen.ShowGroups = false;
            this.xVerpakkingen.ShowItemToolTips = true;
            this.xVerpakkingen.Size = new System.Drawing.Size(319, 369);
            this.xVerpakkingen.SmallImageList = this.imageList1;
            this.xVerpakkingen.TabIndex = 1;
            this.xVerpakkingen.UseAlternatingBackColors = true;
            this.xVerpakkingen.UseCompatibleStateImageBehavior = false;
            this.xVerpakkingen.UseFilterIndicator = true;
            this.xVerpakkingen.UseFiltering = true;
            this.xVerpakkingen.UseHotItem = true;
            this.xVerpakkingen.UseTranslucentHotItem = true;
            this.xVerpakkingen.View = System.Windows.Forms.View.Details;
            this.xVerpakkingen.SelectedIndexChanged += new System.EventHandler(this.xVerpakkingen_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "ArtikelNr";
            this.olvColumn1.Text = "ArtikelNr";
            this.olvColumn1.ToolTipText = "ArtikelNr";
            this.olvColumn1.Width = 100;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "ProductOmschrijving";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Text = "Omschrijving";
            this.olvColumn2.ToolTipText = "Omschrijving";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verwijderenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 26);
            // 
            // verwijderenToolStripMenuItem
            // 
            this.verwijderenToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.verwijderenToolStripMenuItem.Name = "verwijderenToolStripMenuItem";
            this.verwijderenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.verwijderenToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.verwijderenToolStripMenuItem.Text = "Verwijderen";
            this.verwijderenToolStripMenuItem.Click += new System.EventHandler(this.xdelete_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xsearch);
            this.panel2.Controls.Add(this.xadd);
            this.panel2.Controls.Add(this.xdelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(319, 32);
            this.panel2.TabIndex = 0;
            // 
            // xsearch
            // 
            // 
            // 
            // 
            this.xsearch.CustomButton.Image = null;
            this.xsearch.CustomButton.Location = new System.Drawing.Point(217, 2);
            this.xsearch.CustomButton.Name = "";
            this.xsearch.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.xsearch.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xsearch.CustomButton.TabIndex = 1;
            this.xsearch.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xsearch.CustomButton.UseSelectable = true;
            this.xsearch.CustomButton.Visible = false;
            this.xsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xsearch.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xsearch.Lines = new string[0];
            this.xsearch.Location = new System.Drawing.Point(0, 0);
            this.xsearch.MaxLength = 32767;
            this.xsearch.Name = "xsearch";
            this.xsearch.PasswordChar = '\0';
            this.xsearch.PromptText = "Zoeken...";
            this.xsearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xsearch.SelectedText = "";
            this.xsearch.SelectionLength = 0;
            this.xsearch.SelectionStart = 0;
            this.xsearch.ShortcutsEnabled = true;
            this.xsearch.ShowClearButton = true;
            this.xsearch.Size = new System.Drawing.Size(255, 32);
            this.xsearch.TabIndex = 1;
            this.xsearch.UseSelectable = true;
            this.xsearch.WaterMark = "Zoeken...";
            this.xsearch.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xsearch.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xsearch.TextChanged += new System.EventHandler(this.metroTextBox1_TextChanged);
            // 
            // xadd
            // 
            this.xadd.Dock = System.Windows.Forms.DockStyle.Right;
            this.xadd.FlatAppearance.BorderSize = 0;
            this.xadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xadd.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xadd.Location = new System.Drawing.Point(255, 0);
            this.xadd.Name = "xadd";
            this.xadd.Size = new System.Drawing.Size(32, 32);
            this.xadd.TabIndex = 2;
            this.xadd.UseVisualStyleBackColor = true;
            this.xadd.Click += new System.EventHandler(this.xadd_Click);
            // 
            // xdelete
            // 
            this.xdelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.xdelete.Enabled = false;
            this.xdelete.FlatAppearance.BorderSize = 0;
            this.xdelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdelete.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdelete.Location = new System.Drawing.Point(287, 0);
            this.xdelete.Name = "xdelete";
            this.xdelete.Size = new System.Drawing.Size(32, 32);
            this.xdelete.TabIndex = 0;
            this.xdelete.UseVisualStyleBackColor = true;
            this.xdelete.Click += new System.EventHandler(this.xdelete_Click);
            // 
            // xcontainer
            // 
            this.xcontainer.Controls.Add(this.productieVerbruikUI1);
            this.xcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xcontainer.Location = new System.Drawing.Point(0, 0);
            this.xcontainer.Name = "xcontainer";
            this.xcontainer.Size = new System.Drawing.Size(631, 401);
            this.xcontainer.TabIndex = 1;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.xcontainer);
            this.splitContainer1.Size = new System.Drawing.Size(960, 401);
            this.splitContainer1.SplitterDistance = 319;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 1;
            // 
            // productieVerbruikUI1
            // 
            this.productieVerbruikUI1.BackColor = System.Drawing.Color.White;
            this.productieVerbruikUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieVerbruikUI1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieVerbruikUI1.Location = new System.Drawing.Point(0, 0);
            this.productieVerbruikUI1.Name = "productieVerbruikUI1";
            this.productieVerbruikUI1.Padding = new System.Windows.Forms.Padding(5);
            this.productieVerbruikUI1.ShowMateriaalSelector = false;
            this.productieVerbruikUI1.ShowOpslaan = true;
            this.productieVerbruikUI1.ShowSluiten = true;
            this.productieVerbruikUI1.Size = new System.Drawing.Size(631, 401);
            this.productieVerbruikUI1.TabIndex = 0;
            this.productieVerbruikUI1.Title = "Verbruik Berekenen";
            this.productieVerbruikUI1.Visible = false;
            // 
            // SporenForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1000, 481);
            this.Controls.Add(this.splitContainer1);
            this.MinimumSize = new System.Drawing.Size(1000, 450);
            this.Name = "SporenForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Aangepaste Sporen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VerpakkingenForm_FormClosing);
            this.Shown += new System.EventHandler(this.VerpakkingenForm_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xVerpakkingen)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.xcontainer.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroTextBox xsearch;
        private System.Windows.Forms.Button xdelete;
        private BrightIdeasSoftware.ObjectListView xVerpakkingen;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem verwijderenToolStripMenuItem;
        private System.Windows.Forms.Panel xcontainer;
        private Controls.ProductieVerbruikUI productieVerbruikUI1;
        private System.Windows.Forms.Button xadd;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}