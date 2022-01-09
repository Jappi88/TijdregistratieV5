
namespace Controls
{
    partial class WerkPlekStoringen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WerkPlekStoringen));
            this.xomschrijving = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xskillview = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn10 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn11 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toonProductieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wijzigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.verwijderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.xsearchbox = new System.Windows.Forms.TextBox();
            this.xeditpanelcontainer = new System.Windows.Forms.Panel();
            this.xverwijderstoring = new System.Windows.Forms.Button();
            this.xwijzigstoring = new System.Windows.Forms.Button();
            this.xaddstoring = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xstatuslabel = new System.Windows.Forms.Label();
            this.xclosepanel = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xskillview)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.xeditpanelcontainer.SuspendLayout();
            this.panel2.SuspendLayout();
            this.xclosepanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // xomschrijving
            // 
            this.xomschrijving.Dock = System.Windows.Forms.DockStyle.Top;
            this.xomschrijving.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xomschrijving.Location = new System.Drawing.Point(0, 0);
            this.xomschrijving.Name = "xomschrijving";
            this.xomschrijving.Size = new System.Drawing.Size(939, 66);
            this.xomschrijving.TabIndex = 0;
            this.xomschrijving.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xskillview);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 513);
            this.panel1.TabIndex = 1;
            // 
            // xskillview
            // 
            this.xskillview.AllColumns.Add(this.olvColumn1);
            this.xskillview.AllColumns.Add(this.olvColumn6);
            this.xskillview.AllColumns.Add(this.olvColumn2);
            this.xskillview.AllColumns.Add(this.olvColumn3);
            this.xskillview.AllColumns.Add(this.olvColumn4);
            this.xskillview.AllColumns.Add(this.olvColumn5);
            this.xskillview.AllColumns.Add(this.olvColumn7);
            this.xskillview.AllColumns.Add(this.olvColumn8);
            this.xskillview.AllColumns.Add(this.olvColumn9);
            this.xskillview.AllColumns.Add(this.olvColumn10);
            this.xskillview.AllColumns.Add(this.olvColumn11);
            this.xskillview.AllowColumnReorder = true;
            this.xskillview.CellEditUseWholeCell = false;
            this.xskillview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn6,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn7,
            this.olvColumn8,
            this.olvColumn9,
            this.olvColumn10,
            this.olvColumn11});
            this.xskillview.ContextMenuStrip = this.contextMenuStrip1;
            this.xskillview.Cursor = System.Windows.Forms.Cursors.Default;
            this.xskillview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xskillview.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xskillview.FullRowSelect = true;
            this.xskillview.HideSelection = false;
            this.xskillview.LargeImageList = this.imageList1;
            this.xskillview.Location = new System.Drawing.Point(0, 38);
            this.xskillview.Name = "xskillview";
            this.xskillview.Size = new System.Drawing.Size(939, 431);
            this.xskillview.SmallImageList = this.imageList1;
            this.xskillview.SpaceBetweenGroups = 5;
            this.xskillview.TabIndex = 1;
            this.xskillview.UseCompatibleStateImageBehavior = false;
            this.xskillview.UseExplorerTheme = true;
            this.xskillview.UseFilterIndicator = true;
            this.xskillview.UseFiltering = true;
            this.xskillview.UseHotItem = true;
            this.xskillview.UseTranslucentHotItem = true;
            this.xskillview.View = System.Windows.Forms.View.Details;
            this.xskillview.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.xskillview_CellToolTipShowing);
            this.xskillview.SelectedIndexChanged += new System.EventHandler(this.xskillview_SelectionChanged);
            this.xskillview.DoubleClick += new System.EventHandler(this.xskillview_DoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "StoringType";
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.Text = "Storing Type";
            this.olvColumn1.ToolTipText = "De soort storing";
            this.olvColumn1.Width = 150;
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "Omschrijving";
            this.olvColumn6.DisplayIndex = 5;
            this.olvColumn6.IsEditable = false;
            this.olvColumn6.IsTileViewColumn = true;
            this.olvColumn6.Text = "Omschrijving";
            this.olvColumn6.ToolTipText = "Omschrijving";
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "GemeldDoor";
            this.olvColumn2.DisplayIndex = 1;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.IsTileViewColumn = true;
            this.olvColumn2.Text = "Gemeld Door";
            this.olvColumn2.ToolTipText = "Gemeld Door";
            this.olvColumn2.Width = 120;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Gestart";
            this.olvColumn3.DisplayIndex = 2;
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.IsTileViewColumn = true;
            this.olvColumn3.Text = "Gestart Op";
            this.olvColumn3.ToolTipText = "Gestart Op";
            this.olvColumn3.Width = 120;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Gestopt";
            this.olvColumn4.DisplayIndex = 3;
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.IsTileViewColumn = true;
            this.olvColumn4.Text = "Gestopt Op";
            this.olvColumn4.ToolTipText = "Gestopt Op";
            this.olvColumn4.Width = 120;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "IsVerholpen";
            this.olvColumn5.DisplayIndex = 4;
            this.olvColumn5.IsEditable = false;
            this.olvColumn5.IsTileViewColumn = true;
            this.olvColumn5.Text = "Is Verholpen";
            this.olvColumn5.ToolTipText = "Of het al verholpen is";
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "Oplossing";
            this.olvColumn7.IsEditable = false;
            this.olvColumn7.IsTileViewColumn = true;
            this.olvColumn7.Text = "Oplossing";
            this.olvColumn7.ToolTipText = "Oplossing";
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "Path";
            this.olvColumn8.IsEditable = false;
            this.olvColumn8.IsTileViewColumn = true;
            this.olvColumn8.Text = "Werk Locatie";
            this.olvColumn8.ToolTipText = "De locatie van de";
            this.olvColumn8.Width = 150;
            // 
            // olvColumn9
            // 
            this.olvColumn9.AspectName = "TotaalTijd";
            this.olvColumn9.AspectToStringFormat = "{0} uur";
            this.olvColumn9.IsEditable = false;
            this.olvColumn9.IsTileViewColumn = true;
            this.olvColumn9.Text = "Tijd";
            this.olvColumn9.ToolTipText = "De totale tijd";
            this.olvColumn9.Width = 120;
            // 
            // olvColumn10
            // 
            this.olvColumn10.AspectName = "VerholpenDoor";
            this.olvColumn10.IsEditable = false;
            this.olvColumn10.IsTileViewColumn = true;
            this.olvColumn10.Text = "Verholpen Door";
            this.olvColumn10.ToolTipText = "Verholpen Door";
            this.olvColumn10.Width = 120;
            // 
            // olvColumn11
            // 
            this.olvColumn11.AspectName = "WerkPlek";
            this.olvColumn11.IsEditable = false;
            this.olvColumn11.IsTileViewColumn = true;
            this.olvColumn11.Text = "Werkplek";
            this.olvColumn11.ToolTipText = "Werkplek";
            this.olvColumn11.Width = 120;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toonProductieToolStripMenuItem,
            this.wijzigToolStripMenuItem,
            this.toolStripSeparator1,
            this.verwijderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(183, 124);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // toonProductieToolStripMenuItem
            // 
            this.toonProductieToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.window_16756_32x32;
            this.toonProductieToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toonProductieToolStripMenuItem.Name = "toonProductieToolStripMenuItem";
            this.toonProductieToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.toonProductieToolStripMenuItem.Size = new System.Drawing.Size(182, 38);
            this.toonProductieToolStripMenuItem.Text = "&Productie";
            this.toonProductieToolStripMenuItem.ToolTipText = "Toon productie venster";
            this.toonProductieToolStripMenuItem.Click += new System.EventHandler(this.toonProductieToolStripMenuItem_Click);
            // 
            // wijzigToolStripMenuItem
            // 
            this.wijzigToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.wijzigToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.wijzigToolStripMenuItem.Name = "wijzigToolStripMenuItem";
            this.wijzigToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.wijzigToolStripMenuItem.Size = new System.Drawing.Size(182, 38);
            this.wijzigToolStripMenuItem.Text = "&Wijzig";
            this.wijzigToolStripMenuItem.ToolTipText = "Wijzig geselecteerde onderbreking";
            this.wijzigToolStripMenuItem.Click += new System.EventHandler(this.wijzigToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(179, 6);
            // 
            // verwijderToolStripMenuItem
            // 
            this.verwijderToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.verwijderToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.verwijderToolStripMenuItem.Name = "verwijderToolStripMenuItem";
            this.verwijderToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.verwijderToolStripMenuItem.Size = new System.Drawing.Size(182, 38);
            this.verwijderToolStripMenuItem.Text = "Verwijder";
            this.verwijderToolStripMenuItem.ToolTipText = "Verwijder geselecteerde onderbrekeningen";
            this.verwijderToolStripMenuItem.Click += new System.EventHandler(this.verwijderToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(48, 48);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xsearchbox);
            this.panel3.Controls.Add(this.xeditpanelcontainer);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(939, 38);
            this.panel3.TabIndex = 3;
            // 
            // xsearchbox
            // 
            this.xsearchbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xsearchbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsearchbox.Location = new System.Drawing.Point(134, 9);
            this.xsearchbox.Name = "xsearchbox";
            this.xsearchbox.Size = new System.Drawing.Size(805, 29);
            this.xsearchbox.TabIndex = 0;
            this.xsearchbox.Text = "Zoeken...";
            this.toolTip1.SetToolTip(this.xsearchbox, "Zoek naar een onderbreking");
            this.xsearchbox.TextChanged += new System.EventHandler(this.xsearchbox_TextChanged);
            this.xsearchbox.Enter += new System.EventHandler(this.xsearch_Enter);
            this.xsearchbox.Leave += new System.EventHandler(this.xsearch_Leave);
            // 
            // xeditpanelcontainer
            // 
            this.xeditpanelcontainer.Controls.Add(this.xverwijderstoring);
            this.xeditpanelcontainer.Controls.Add(this.xwijzigstoring);
            this.xeditpanelcontainer.Controls.Add(this.xaddstoring);
            this.xeditpanelcontainer.Dock = System.Windows.Forms.DockStyle.Left;
            this.xeditpanelcontainer.Location = new System.Drawing.Point(0, 0);
            this.xeditpanelcontainer.Name = "xeditpanelcontainer";
            this.xeditpanelcontainer.Size = new System.Drawing.Size(134, 38);
            this.xeditpanelcontainer.TabIndex = 1;
            // 
            // xverwijderstoring
            // 
            this.xverwijderstoring.Dock = System.Windows.Forms.DockStyle.Left;
            this.xverwijderstoring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xverwijderstoring.ForeColor = System.Drawing.Color.White;
            this.xverwijderstoring.Image = ((System.Drawing.Image)(resources.GetObject("xverwijderstoring.Image")));
            this.xverwijderstoring.Location = new System.Drawing.Point(88, 0);
            this.xverwijderstoring.Name = "xverwijderstoring";
            this.xverwijderstoring.Size = new System.Drawing.Size(44, 38);
            this.xverwijderstoring.TabIndex = 23;
            this.toolTip1.SetToolTip(this.xverwijderstoring, "Verwijder geselecteerde onderbrekeningen");
            this.xverwijderstoring.UseVisualStyleBackColor = true;
            this.xverwijderstoring.Click += new System.EventHandler(this.xverwijderstoring_Click);
            // 
            // xwijzigstoring
            // 
            this.xwijzigstoring.Dock = System.Windows.Forms.DockStyle.Left;
            this.xwijzigstoring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xwijzigstoring.ForeColor = System.Drawing.Color.White;
            this.xwijzigstoring.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xwijzigstoring.Location = new System.Drawing.Point(44, 0);
            this.xwijzigstoring.Name = "xwijzigstoring";
            this.xwijzigstoring.Size = new System.Drawing.Size(44, 38);
            this.xwijzigstoring.TabIndex = 24;
            this.toolTip1.SetToolTip(this.xwijzigstoring, "Wijzig geselecteerde onderbreking");
            this.xwijzigstoring.UseVisualStyleBackColor = true;
            this.xwijzigstoring.Click += new System.EventHandler(this.xwijzigstoring_Click);
            // 
            // xaddstoring
            // 
            this.xaddstoring.Dock = System.Windows.Forms.DockStyle.Left;
            this.xaddstoring.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xaddstoring.ForeColor = System.Drawing.Color.White;
            this.xaddstoring.Image = global::ProductieManager.Properties.Resources.add_icon_icons_com_52393;
            this.xaddstoring.Location = new System.Drawing.Point(0, 0);
            this.xaddstoring.Name = "xaddstoring";
            this.xaddstoring.Size = new System.Drawing.Size(44, 38);
            this.xaddstoring.TabIndex = 22;
            this.toolTip1.SetToolTip(this.xaddstoring, "Voeg nieuwe onderbreking toe");
            this.xaddstoring.UseVisualStyleBackColor = true;
            this.xaddstoring.Click += new System.EventHandler(this.xaddstoring_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xstatuslabel);
            this.panel2.Controls.Add(this.xclosepanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 469);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(939, 44);
            this.panel2.TabIndex = 2;
            // 
            // xstatuslabel
            // 
            this.xstatuslabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xstatuslabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatuslabel.Location = new System.Drawing.Point(0, 0);
            this.xstatuslabel.Name = "xstatuslabel";
            this.xstatuslabel.Padding = new System.Windows.Forms.Padding(5);
            this.xstatuslabel.Size = new System.Drawing.Size(807, 44);
            this.xstatuslabel.TabIndex = 0;
            this.xstatuslabel.Text = "Geen Onderbrekeningen";
            this.xstatuslabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xclosepanel
            // 
            this.xclosepanel.Controls.Add(this.xsluiten);
            this.xclosepanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.xclosepanel.Location = new System.Drawing.Point(807, 0);
            this.xclosepanel.Name = "xclosepanel";
            this.xclosepanel.Size = new System.Drawing.Size(132, 44);
            this.xclosepanel.TabIndex = 1;
            // 
            // xsluiten
            // 
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(3, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(125, 38);
            this.xsluiten.TabIndex = 1;
            this.xsluiten.Text = "&Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xsluiten, "Sluit storing window");
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Onderbrekeningen";
            // 
            // WerkPlekStoringen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.xomschrijving);
            this.DoubleBuffered = true;
            this.Name = "WerkPlekStoringen";
            this.Size = new System.Drawing.Size(939, 579);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xskillview)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.xeditpanelcontainer.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.xclosepanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label xomschrijving;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox xsearchbox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label xstatuslabel;
        private System.Windows.Forms.Panel xclosepanel;
        private System.Windows.Forms.Button xsluiten;
        public BrightIdeasSoftware.ObjectListView xskillview;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel xeditpanelcontainer;
        private System.Windows.Forms.Button xwijzigstoring;
        private System.Windows.Forms.Button xverwijderstoring;
        private System.Windows.Forms.Button xaddstoring;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private BrightIdeasSoftware.OLVColumn olvColumn9;
        private BrightIdeasSoftware.OLVColumn olvColumn10;
        private BrightIdeasSoftware.OLVColumn olvColumn11;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wijzigToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem verwijderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toonProductieToolStripMenuItem;
    }
}
