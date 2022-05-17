
namespace Forms
{
    partial class AlleKlusjes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlleKlusjes));
            this.xstatuslabel = new System.Windows.Forms.Label();
            this.xklusjes = new Controls.CustomObjectListview();
            this.olvColumn0 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openProductieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.wijzigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.verwijderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xklusimages = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.xsearchbox = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.xverwijderklusjes = new System.Windows.Forms.Button();
            this.xwijzigklus = new System.Windows.Forms.Button();
            this.xopenproductie = new System.Windows.Forms.Button();
            this.xnewklus = new System.Windows.Forms.Button();
            this.xsluiten = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.xklusjes)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // xstatuslabel
            // 
            this.xstatuslabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xstatuslabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatuslabel.Location = new System.Drawing.Point(18, 475);
            this.xstatuslabel.Name = "xstatuslabel";
            this.xstatuslabel.Size = new System.Drawing.Size(700, 45);
            this.xstatuslabel.TabIndex = 1;
            this.xstatuslabel.Text = "Totaal tijd gewerkt";
            this.xstatuslabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xklusjes
            // 
            this.xklusjes.AllColumns.Add(this.olvColumn0);
            this.xklusjes.AllColumns.Add(this.olvColumn1);
            this.xklusjes.AllColumns.Add(this.olvColumn2);
            this.xklusjes.AllColumns.Add(this.olvColumn3);
            this.xklusjes.AllColumns.Add(this.olvColumn4);
            this.xklusjes.AllColumns.Add(this.olvColumn8);
            this.xklusjes.AllowCellEdit = false;
            this.xklusjes.CellEditUseWholeCell = false;
            this.xklusjes.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn0,
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn8});
            this.xklusjes.ContextMenuStrip = this.contextMenuStrip1;
            this.xklusjes.Cursor = System.Windows.Forms.Cursors.Default;
            this.xklusjes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xklusjes.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xklusjes.FullRowSelect = true;
            this.xklusjes.HeaderWordWrap = true;
            this.xklusjes.HideSelection = false;
            this.xklusjes.LargeImageList = this.xklusimages;
            this.xklusjes.Location = new System.Drawing.Point(0, 68);
            this.xklusjes.MenuLabelColumns = "kolommen";
            this.xklusjes.MenuLabelGroupBy = "Groeperen op \'{0}\'";
            this.xklusjes.MenuLabelLockGroupingOn = "Groepering vergrendelen op \'{0}\'";
            this.xklusjes.MenuLabelSelectColumns = "Selecteer kolommen...";
            this.xklusjes.MenuLabelSortAscending = "Sorteer oplopend op \'{0}\'";
            this.xklusjes.MenuLabelSortDescending = "Aflopend sorteren op \'{0}\'";
            this.xklusjes.MenuLabelTurnOffGroups = "Groepen uitschakelen";
            this.xklusjes.MenuLabelUnlockGroupingOn = "Ontgrendel groeperen van \'{0}\'";
            this.xklusjes.MenuLabelUnsort = "Uitsorteren";
            this.xklusjes.Name = "xklusjes";
            this.xklusjes.OwnerDraw = false;
            this.xklusjes.ShowCommandMenuOnRightClick = true;
            this.xklusjes.ShowGroups = false;
            this.xklusjes.ShowItemCountOnGroups = true;
            this.xklusjes.ShowItemToolTips = true;
            this.xklusjes.Size = new System.Drawing.Size(821, 341);
            this.xklusjes.SmallImageList = this.xklusimages;
            this.xklusjes.SpaceBetweenGroups = 10;
            this.xklusjes.TabIndex = 2;
            this.xklusjes.TileSize = new System.Drawing.Size(300, 120);
            this.xklusjes.UseCompatibleStateImageBehavior = false;
            this.xklusjes.UseExplorerTheme = true;
            this.xklusjes.UseFilterIndicator = true;
            this.xklusjes.UseFiltering = true;
            this.xklusjes.UseHotControls = false;
            this.xklusjes.UseHotItem = true;
            this.xklusjes.UseOverlays = false;
            this.xklusjes.UseTranslucentHotItem = true;
            this.xklusjes.UseTranslucentSelection = true;
            this.xklusjes.View = System.Windows.Forms.View.Details;
            this.xklusjes.SelectedIndexChanged += new System.EventHandler(this.xklusjes_SelectedIndexChanged);
            this.xklusjes.DoubleClick += new System.EventHandler(this.xwijzigklus_Click);
            // 
            // olvColumn0
            // 
            this.olvColumn0.AspectName = "Omschrijving";
            this.olvColumn0.IsEditable = false;
            this.olvColumn0.IsTileViewColumn = true;
            this.olvColumn0.Text = "Omschrijving";
            this.olvColumn0.Width = 200;
            this.olvColumn0.WordWrap = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Naam";
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.Text = "Naam";
            this.olvColumn1.Width = 120;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "WerkPlek";
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.IsTileViewColumn = true;
            this.olvColumn2.Text = "Werkplek";
            this.olvColumn2.Width = 120;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "ProductieNr";
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.IsTileViewColumn = true;
            this.olvColumn3.Text = "ProductieNr";
            this.olvColumn3.Width = 100;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "ArtikelNr";
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.IsTileViewColumn = true;
            this.olvColumn4.Text = "ArtikelNr";
            this.olvColumn4.Width = 100;
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "";
            this.olvColumn8.IsEditable = false;
            this.olvColumn8.IsTileViewColumn = true;
            this.olvColumn8.Text = "Tijd Gewerkt";
            this.olvColumn8.Width = 100;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openProductieToolStripMenuItem,
            this.toolStripSeparator1,
            this.wijzigToolStripMenuItem,
            this.toolStripSeparator2,
            this.verwijderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(174, 130);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // openProductieToolStripMenuItem
            // 
            this.openProductieToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.window_16756_32x32;
            this.openProductieToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openProductieToolStripMenuItem.Name = "openProductieToolStripMenuItem";
            this.openProductieToolStripMenuItem.Size = new System.Drawing.Size(173, 38);
            this.openProductieToolStripMenuItem.Text = "&Open Productie";
            this.openProductieToolStripMenuItem.Click += new System.EventHandler(this.xopenproductie_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // wijzigToolStripMenuItem
            // 
            this.wijzigToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.wijzigToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.wijzigToolStripMenuItem.Name = "wijzigToolStripMenuItem";
            this.wijzigToolStripMenuItem.Size = new System.Drawing.Size(173, 38);
            this.wijzigToolStripMenuItem.Text = "&Wijzig";
            this.wijzigToolStripMenuItem.Click += new System.EventHandler(this.xwijzigklus_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(170, 6);
            // 
            // verwijderToolStripMenuItem
            // 
            this.verwijderToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.verwijderToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.verwijderToolStripMenuItem.Name = "verwijderToolStripMenuItem";
            this.verwijderToolStripMenuItem.Size = new System.Drawing.Size(173, 38);
            this.verwijderToolStripMenuItem.Text = "&Verwijder";
            this.verwijderToolStripMenuItem.Click += new System.EventHandler(this.xverwijderklusjes_Click);
            // 
            // xklusimages
            // 
            this.xklusimages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.xklusimages.ImageSize = new System.Drawing.Size(48, 48);
            this.xklusimages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.xklusjes);
            this.panel1.Controls.Add(this.xsearchbox);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Location = new System.Drawing.Point(23, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(821, 409);
            this.panel1.TabIndex = 3;
            // 
            // xsearchbox
            // 
            this.xsearchbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.xsearchbox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsearchbox.Location = new System.Drawing.Point(0, 41);
            this.xsearchbox.Name = "xsearchbox";
            this.xsearchbox.Size = new System.Drawing.Size(821, 27);
            this.xsearchbox.TabIndex = 3;
            this.xsearchbox.Text = "Zoeken...";
            this.xsearchbox.TextChanged += new System.EventHandler(this.xsearchbox_TextChanged);
            this.xsearchbox.Enter += new System.EventHandler(this.xsearch_Enter);
            this.xsearchbox.Leave += new System.EventHandler(this.xsearch_Leave);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.xverwijderklusjes);
            this.panel4.Controls.Add(this.xwijzigklus);
            this.panel4.Controls.Add(this.xopenproductie);
            this.panel4.Controls.Add(this.xnewklus);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(821, 41);
            this.panel4.TabIndex = 4;
            // 
            // xverwijderklusjes
            // 
            this.xverwijderklusjes.Dock = System.Windows.Forms.DockStyle.Left;
            this.xverwijderklusjes.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xverwijderklusjes.ForeColor = System.Drawing.Color.White;
            this.xverwijderklusjes.Image = ((System.Drawing.Image)(resources.GetObject("xverwijderklusjes.Image")));
            this.xverwijderklusjes.Location = new System.Drawing.Point(132, 0);
            this.xverwijderklusjes.Name = "xverwijderklusjes";
            this.xverwijderklusjes.Size = new System.Drawing.Size(44, 41);
            this.xverwijderklusjes.TabIndex = 26;
            this.xverwijderklusjes.UseVisualStyleBackColor = true;
            this.xverwijderklusjes.Visible = false;
            this.xverwijderklusjes.Click += new System.EventHandler(this.xverwijderklusjes_Click);
            // 
            // xwijzigklus
            // 
            this.xwijzigklus.Dock = System.Windows.Forms.DockStyle.Left;
            this.xwijzigklus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xwijzigklus.ForeColor = System.Drawing.Color.White;
            this.xwijzigklus.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xwijzigklus.Location = new System.Drawing.Point(88, 0);
            this.xwijzigklus.Name = "xwijzigklus";
            this.xwijzigklus.Size = new System.Drawing.Size(44, 41);
            this.xwijzigklus.TabIndex = 27;
            this.xwijzigklus.UseVisualStyleBackColor = true;
            this.xwijzigklus.Visible = false;
            this.xwijzigklus.Click += new System.EventHandler(this.xwijzigklus_Click);
            // 
            // xopenproductie
            // 
            this.xopenproductie.Dock = System.Windows.Forms.DockStyle.Left;
            this.xopenproductie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xopenproductie.ForeColor = System.Drawing.Color.White;
            this.xopenproductie.Image = global::ProductieManager.Properties.Resources.window_16756_32x32;
            this.xopenproductie.Location = new System.Drawing.Point(44, 0);
            this.xopenproductie.Name = "xopenproductie";
            this.xopenproductie.Size = new System.Drawing.Size(44, 41);
            this.xopenproductie.TabIndex = 28;
            this.xopenproductie.UseVisualStyleBackColor = true;
            this.xopenproductie.Visible = false;
            this.xopenproductie.Click += new System.EventHandler(this.xopenproductie_Click);
            // 
            // xnewklus
            // 
            this.xnewklus.Dock = System.Windows.Forms.DockStyle.Left;
            this.xnewklus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xnewklus.ForeColor = System.Drawing.Color.White;
            this.xnewklus.Image = global::ProductieManager.Properties.Resources.add_icon_icons_com_52393;
            this.xnewklus.Location = new System.Drawing.Point(0, 0);
            this.xnewklus.Name = "xnewklus";
            this.xnewklus.Size = new System.Drawing.Size(44, 41);
            this.xnewklus.TabIndex = 25;
            this.xnewklus.UseVisualStyleBackColor = true;
            this.xnewklus.Visible = false;
            this.xnewklus.Click += new System.EventHandler(this.xnewklus_Click);
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(724, 478);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(120, 38);
            this.xsluiten.TabIndex = 11;
            this.xsluiten.Text = "&Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // AlleKlusjes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 520);
            this.Controls.Add(this.xsluiten);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.xstatuslabel);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AlleKlusjes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alle Klusjes";
            this.Title = "Alle Klusjes";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlleKlusjes_FormClosing);
            this.Load += new System.EventHandler(this.AlleKlusjes_Load);
            this.Shown += new System.EventHandler(this.AlleKlusjes_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.xklusjes)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Label xstatuslabel;
        private Controls.CustomObjectListview xklusjes;
        private BrightIdeasSoftware.OLVColumn olvColumn0;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private System.Windows.Forms.ImageList xklusimages;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox xsearchbox;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button xverwijderklusjes;
        private System.Windows.Forms.Button xwijzigklus;
        private System.Windows.Forms.Button xnewklus;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem openProductieToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem wijzigToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem verwijderToolStripMenuItem;
        private System.Windows.Forms.Button xopenproductie;
    }
}