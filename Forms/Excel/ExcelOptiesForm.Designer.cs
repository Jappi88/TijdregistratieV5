
namespace Forms
{
    partial class ExcelOptiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ExcelOptiesForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xstandaard = new MetroFramework.Controls.MetroCheckBox();
            this.xtoepassen = new System.Windows.Forms.Button();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xOpslaan = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.xOptiesView = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xoptiespanel = new System.Windows.Forms.ToolStrip();
            this.xAddOptieButton = new System.Windows.Forms.ToolStripButton();
            this.xEditOpties = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.xDeleteOptieButton = new System.Windows.Forms.ToolStripButton();
            this.xBeschikbareColumns = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xZichtbareColumnsView = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xItemUp = new System.Windows.Forms.ToolStripButton();
            this.xItemDown = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xItemRight = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.xItemDelete = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.xWijzigColumnBreedte = new System.Windows.Forms.Button();
            this.xColumnBreedte = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.xWijzigColumnFormat = new System.Windows.Forms.Button();
            this.xColumnFormatTextbox = new System.Windows.Forms.TextBox();
            this.xWijzigColumnText = new System.Windows.Forms.Button();
            this.xColumnTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.xKleurenGroup = new System.Windows.Forms.GroupBox();
            this.xKleurRegelPanel = new System.Windows.Forms.Panel();
            this.xColorRegelStatusLabel = new System.Windows.Forms.Label();
            this.xKiesKleurRegels = new System.Windows.Forms.Button();
            this.xStaticColorPanel = new System.Windows.Forms.Panel();
            this.xTextKleurB = new System.Windows.Forms.Button();
            this.xColumnKleurB = new System.Windows.Forms.Button();
            this.xVariableColorRadio = new System.Windows.Forms.RadioButton();
            this.xVasteKleurRadio = new System.Windows.Forms.RadioButton();
            this.xGeenKleurRadio = new System.Windows.Forms.RadioButton();
            this.xverborgencheckbox = new System.Windows.Forms.CheckBox();
            this.xBerekeningGroup = new System.Windows.Forms.GroupBox();
            this.xBerekenGemiddeldRadio = new System.Windows.Forms.RadioButton();
            this.xSomAllesRadio = new System.Windows.Forms.RadioButton();
            this.xGeenBerekeningRadio = new System.Windows.Forms.RadioButton();
            this.xautoculmncheckbox = new System.Windows.Forms.CheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xOptiesView)).BeginInit();
            this.xoptiespanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xBeschikbareColumns)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xZichtbareColumnsView)).BeginInit();
            this.toolStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xColumnBreedte)).BeginInit();
            this.xKleurenGroup.SuspendLayout();
            this.xKleurRegelPanel.SuspendLayout();
            this.xStaticColorPanel.SuspendLayout();
            this.xBerekeningGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xstandaard);
            this.panel1.Controls.Add(this.xtoepassen);
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Controls.Add(this.xOpslaan);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(10, 553);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(892, 45);
            this.panel1.TabIndex = 2;
            // 
            // xstandaard
            // 
            this.xstandaard.AutoSize = true;
            this.xstandaard.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xstandaard.FontWeight = MetroFramework.MetroCheckBoxWeight.Bold;
            this.xstandaard.Location = new System.Drawing.Point(3, 14);
            this.xstandaard.Name = "xstandaard";
            this.xstandaard.Size = new System.Drawing.Size(163, 19);
            this.xstandaard.Style = MetroFramework.MetroColorStyle.Green;
            this.xstandaard.TabIndex = 9;
            this.xstandaard.Text = "Stel In Als Standaard";
            this.xstandaard.UseSelectable = true;
            this.xstandaard.UseStyleColors = true;
            this.xstandaard.Visible = false;
            this.xstandaard.CheckedChanged += new System.EventHandler(this.xstandaard_CheckedChanged);
            // 
            // xtoepassen
            // 
            this.xtoepassen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xtoepassen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xtoepassen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtoepassen.ForeColor = System.Drawing.Color.Black;
            this.xtoepassen.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.xtoepassen.Location = new System.Drawing.Point(494, 3);
            this.xtoepassen.Name = "xtoepassen";
            this.xtoepassen.Size = new System.Drawing.Size(145, 38);
            this.xtoepassen.TabIndex = 8;
            this.xtoepassen.Text = "Toepassen";
            this.xtoepassen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xtoepassen, "Pas wijzigingen toe");
            this.xtoepassen.UseVisualStyleBackColor = true;
            this.xtoepassen.Click += new System.EventHandler(this.xtoepassen_Click);
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.ForeColor = System.Drawing.Color.Black;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(770, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(119, 38);
            this.xsluiten.TabIndex = 6;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xOpslaan
            // 
            this.xOpslaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xOpslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOpslaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOpslaan.ForeColor = System.Drawing.Color.Black;
            this.xOpslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xOpslaan.Location = new System.Drawing.Point(645, 3);
            this.xOpslaan.Name = "xOpslaan";
            this.xOpslaan.Size = new System.Drawing.Size(119, 38);
            this.xOpslaan.TabIndex = 7;
            this.xOpslaan.Text = "Opslaan";
            this.xOpslaan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xOpslaan.UseVisualStyleBackColor = true;
            this.xOpslaan.Click += new System.EventHandler(this.xOpslaan_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.splitContainer1);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(10, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(892, 493);
            this.panel2.TabIndex = 3;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.xZichtbareColumnsView);
            this.splitContainer1.Panel2.Controls.Add(this.toolStrip1);
            this.splitContainer1.Size = new System.Drawing.Size(657, 493);
            this.splitContainer1.SplitterDistance = 401;
            this.splitContainer1.TabIndex = 3;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.xOptiesView);
            this.splitContainer2.Panel1.Controls.Add(this.xoptiespanel);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.xBeschikbareColumns);
            this.splitContainer2.Size = new System.Drawing.Size(401, 493);
            this.splitContainer2.SplitterDistance = 176;
            this.splitContainer2.TabIndex = 6;
            // 
            // xOptiesView
            // 
            this.xOptiesView.AllColumns.Add(this.olvColumn2);
            this.xOptiesView.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.xOptiesView.CellEditUseWholeCell = false;
            this.xOptiesView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2});
            this.xOptiesView.Cursor = System.Windows.Forms.Cursors.Default;
            this.xOptiesView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xOptiesView.FullRowSelect = true;
            this.xOptiesView.HeaderWordWrap = true;
            this.xOptiesView.HideSelection = false;
            this.xOptiesView.LargeImageList = this.imageList1;
            this.xOptiesView.Location = new System.Drawing.Point(0, 39);
            this.xOptiesView.Name = "xOptiesView";
            this.xOptiesView.ShowFilterMenuOnRightClick = false;
            this.xOptiesView.ShowGroups = false;
            this.xOptiesView.ShowItemToolTips = true;
            this.xOptiesView.Size = new System.Drawing.Size(176, 454);
            this.xOptiesView.SmallImageList = this.imageList1;
            this.xOptiesView.TabIndex = 7;
            this.xOptiesView.UseCompatibleStateImageBehavior = false;
            this.xOptiesView.UseExplorerTheme = true;
            this.xOptiesView.UseHotControls = false;
            this.xOptiesView.UseHotItem = true;
            this.xOptiesView.UseTranslucentHotItem = true;
            this.xOptiesView.UseTranslucentSelection = true;
            this.xOptiesView.View = System.Windows.Forms.View.Details;
            this.xOptiesView.SelectedIndexChanged += new System.EventHandler(this.xInstellingenView_SelectedIndexChanged);
            this.xOptiesView.DoubleClick += new System.EventHandler(this.xOptiesView_DoubleClick);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Name";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvColumn2.Text = "Opties";
            this.olvColumn2.Width = 248;
            this.olvColumn2.WordWrap = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xoptiespanel
            // 
            this.xoptiespanel.BackColor = System.Drawing.Color.White;
            this.xoptiespanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xAddOptieButton,
            this.xEditOpties,
            this.toolStripSeparator3,
            this.xDeleteOptieButton});
            this.xoptiespanel.Location = new System.Drawing.Point(0, 0);
            this.xoptiespanel.Name = "xoptiespanel";
            this.xoptiespanel.Size = new System.Drawing.Size(176, 39);
            this.xoptiespanel.TabIndex = 8;
            this.xoptiespanel.Text = "toolStrip2";
            // 
            // xAddOptieButton
            // 
            this.xAddOptieButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xAddOptieButton.Image = global::ProductieManager.Properties.Resources.add_Blue_circle_32x32;
            this.xAddOptieButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xAddOptieButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xAddOptieButton.Name = "xAddOptieButton";
            this.xAddOptieButton.Size = new System.Drawing.Size(36, 36);
            this.xAddOptieButton.Text = "toolStripButton1";
            this.xAddOptieButton.ToolTipText = "Maak nieuwe instellingen";
            this.xAddOptieButton.Click += new System.EventHandler(this.xAddOptieButton_Click);
            // 
            // xEditOpties
            // 
            this.xEditOpties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xEditOpties.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xEditOpties.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xEditOpties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xEditOpties.Name = "xEditOpties";
            this.xEditOpties.Size = new System.Drawing.Size(36, 36);
            this.xEditOpties.Text = "toolStripButton2";
            this.xEditOpties.ToolTipText = "Wijzig geselecteerde optie";
            this.xEditOpties.Click += new System.EventHandler(this.xEditOpties_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // xDeleteOptieButton
            // 
            this.xDeleteOptieButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xDeleteOptieButton.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xDeleteOptieButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xDeleteOptieButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xDeleteOptieButton.Name = "xDeleteOptieButton";
            this.xDeleteOptieButton.Size = new System.Drawing.Size(36, 36);
            this.xDeleteOptieButton.Text = "toolStripButton2";
            this.xDeleteOptieButton.ToolTipText = "Verwijderer selectie";
            this.xDeleteOptieButton.Click += new System.EventHandler(this.xDeleteOptieButton_Click);
            // 
            // xBeschikbareColumns
            // 
            this.xBeschikbareColumns.AllColumns.Add(this.olvColumn3);
            this.xBeschikbareColumns.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.xBeschikbareColumns.CellEditUseWholeCell = false;
            this.xBeschikbareColumns.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn3});
            this.xBeschikbareColumns.Cursor = System.Windows.Forms.Cursors.Default;
            this.xBeschikbareColumns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xBeschikbareColumns.FullRowSelect = true;
            this.xBeschikbareColumns.HeaderWordWrap = true;
            this.xBeschikbareColumns.HideSelection = false;
            this.xBeschikbareColumns.Location = new System.Drawing.Point(0, 0);
            this.xBeschikbareColumns.Name = "xBeschikbareColumns";
            this.xBeschikbareColumns.ShowFilterMenuOnRightClick = false;
            this.xBeschikbareColumns.ShowGroups = false;
            this.xBeschikbareColumns.ShowItemToolTips = true;
            this.xBeschikbareColumns.Size = new System.Drawing.Size(221, 493);
            this.xBeschikbareColumns.TabIndex = 7;
            this.xBeschikbareColumns.UseAlternatingBackColors = true;
            this.xBeschikbareColumns.UseCompatibleStateImageBehavior = false;
            this.xBeschikbareColumns.UseExplorerTheme = true;
            this.xBeschikbareColumns.UseHotControls = false;
            this.xBeschikbareColumns.UseHotItem = true;
            this.xBeschikbareColumns.UseTranslucentHotItem = true;
            this.xBeschikbareColumns.UseTranslucentSelection = true;
            this.xBeschikbareColumns.View = System.Windows.Forms.View.Details;
            this.xBeschikbareColumns.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.xBeschikbareColumns_CellToolTipShowing);
            this.xBeschikbareColumns.SelectedIndexChanged += new System.EventHandler(this.xBeschikbareColumns_SelectedIndexChanged);
            this.xBeschikbareColumns.DoubleClick += new System.EventHandler(this.xBeschikbareColumns_DoubleClick);
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Name";
            this.olvColumn3.FillsFreeSpace = true;
            this.olvColumn3.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvColumn3.Text = "Beschikbare Columns";
            this.olvColumn3.Width = 248;
            this.olvColumn3.WordWrap = true;
            // 
            // xZichtbareColumnsView
            // 
            this.xZichtbareColumnsView.AllColumns.Add(this.olvColumn1);
            this.xZichtbareColumnsView.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.xZichtbareColumnsView.CellEditUseWholeCell = false;
            this.xZichtbareColumnsView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.xZichtbareColumnsView.Cursor = System.Windows.Forms.Cursors.Default;
            this.xZichtbareColumnsView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xZichtbareColumnsView.FullRowSelect = true;
            this.xZichtbareColumnsView.HeaderWordWrap = true;
            this.xZichtbareColumnsView.HideSelection = false;
            this.xZichtbareColumnsView.Location = new System.Drawing.Point(37, 0);
            this.xZichtbareColumnsView.Name = "xZichtbareColumnsView";
            this.xZichtbareColumnsView.ShowFilterMenuOnRightClick = false;
            this.xZichtbareColumnsView.ShowGroups = false;
            this.xZichtbareColumnsView.ShowItemToolTips = true;
            this.xZichtbareColumnsView.Size = new System.Drawing.Size(215, 493);
            this.xZichtbareColumnsView.TabIndex = 6;
            this.xZichtbareColumnsView.UseAlternatingBackColors = true;
            this.xZichtbareColumnsView.UseCompatibleStateImageBehavior = false;
            this.xZichtbareColumnsView.UseExplorerTheme = true;
            this.xZichtbareColumnsView.UseHotControls = false;
            this.xZichtbareColumnsView.UseHotItem = true;
            this.xZichtbareColumnsView.UseTranslucentHotItem = true;
            this.xZichtbareColumnsView.UseTranslucentSelection = true;
            this.xZichtbareColumnsView.View = System.Windows.Forms.View.Details;
            this.xZichtbareColumnsView.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.xZichtbareColumnsView_CellToolTipShowing);
            this.xZichtbareColumnsView.SelectedIndexChanged += new System.EventHandler(this.xZichtbareColumnsView_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Naam";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvColumn1.Text = "Zichtbare Columns";
            this.olvColumn1.Width = 248;
            this.olvColumn1.WordWrap = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Left;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xItemUp,
            this.xItemDown,
            this.toolStripSeparator1,
            this.xItemRight,
            this.toolStripSeparator2,
            this.xItemDelete});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(37, 493);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // xItemUp
            // 
            this.xItemUp.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xItemUp.Image = global::ProductieManager.Properties.Resources.arrow_up_16741_32x32;
            this.xItemUp.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xItemUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xItemUp.Name = "xItemUp";
            this.xItemUp.Size = new System.Drawing.Size(34, 36);
            this.xItemUp.ToolTipText = "Plaats omhoog";
            this.xItemUp.Click += new System.EventHandler(this.xItemUp_Click);
            // 
            // xItemDown
            // 
            this.xItemDown.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xItemDown.Image = global::ProductieManager.Properties.Resources.arrow_down_16740_32x32;
            this.xItemDown.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xItemDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xItemDown.Name = "xItemDown";
            this.xItemDown.Size = new System.Drawing.Size(34, 36);
            this.xItemDown.ToolTipText = "Plaats onderen";
            this.xItemDown.Click += new System.EventHandler(this.xItemDown_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(34, 6);
            // 
            // xItemRight
            // 
            this.xItemRight.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xItemRight.Image = global::ProductieManager.Properties.Resources.arrow_right_16742_32x32;
            this.xItemRight.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xItemRight.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xItemRight.Name = "xItemRight";
            this.xItemRight.Size = new System.Drawing.Size(34, 36);
            this.xItemRight.ToolTipText = "Voeg culumn toe";
            this.xItemRight.Click += new System.EventHandler(this.xItemRight_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(34, 6);
            // 
            // xItemDelete
            // 
            this.xItemDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xItemDelete.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xItemDelete.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xItemDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xItemDelete.Name = "xItemDelete";
            this.xItemDelete.Size = new System.Drawing.Size(34, 36);
            this.xItemDelete.ToolTipText = "Verwijder column(s)";
            this.xItemDelete.Click += new System.EventHandler(this.xItemDelete_Click);
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.xWijzigColumnBreedte);
            this.panel3.Controls.Add(this.xColumnBreedte);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.xWijzigColumnFormat);
            this.panel3.Controls.Add(this.xColumnFormatTextbox);
            this.panel3.Controls.Add(this.xWijzigColumnText);
            this.panel3.Controls.Add(this.xColumnTextBox);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.xKleurenGroup);
            this.panel3.Controls.Add(this.xverborgencheckbox);
            this.panel3.Controls.Add(this.xBerekeningGroup);
            this.panel3.Controls.Add(this.xautoculmncheckbox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(657, 0);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(10);
            this.panel3.Size = new System.Drawing.Size(235, 493);
            this.panel3.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(116, 107);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 17);
            this.label3.TabIndex = 14;
            this.label3.Text = "Breedte";
            // 
            // xWijzigColumnBreedte
            // 
            this.xWijzigColumnBreedte.FlatAppearance.BorderSize = 0;
            this.xWijzigColumnBreedte.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xWijzigColumnBreedte.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.xWijzigColumnBreedte.Location = new System.Drawing.Point(200, 121);
            this.xWijzigColumnBreedte.Name = "xWijzigColumnBreedte";
            this.xWijzigColumnBreedte.Size = new System.Drawing.Size(32, 32);
            this.xWijzigColumnBreedte.TabIndex = 13;
            this.toolTip1.SetToolTip(this.xWijzigColumnBreedte, "Wijzig column breedte");
            this.xWijzigColumnBreedte.UseVisualStyleBackColor = true;
            this.xWijzigColumnBreedte.Visible = false;
            this.xWijzigColumnBreedte.Click += new System.EventHandler(this.xWijzigColumnBreedte_Click);
            // 
            // xColumnBreedte
            // 
            this.xColumnBreedte.Location = new System.Drawing.Point(119, 127);
            this.xColumnBreedte.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.xColumnBreedte.Name = "xColumnBreedte";
            this.xColumnBreedte.Size = new System.Drawing.Size(75, 25);
            this.xColumnBreedte.TabIndex = 12;
            this.xColumnBreedte.Value = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.xColumnBreedte.ValueChanged += new System.EventHandler(this.xColumnBreedte_ValueChanged);
            this.xColumnBreedte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xColumnBreedte_KeyDown);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(7, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 17);
            this.label2.TabIndex = 11;
            this.label2.Text = "Column Formaat";
            // 
            // xWijzigColumnFormat
            // 
            this.xWijzigColumnFormat.FlatAppearance.BorderSize = 0;
            this.xWijzigColumnFormat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xWijzigColumnFormat.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.xWijzigColumnFormat.Location = new System.Drawing.Point(200, 74);
            this.xWijzigColumnFormat.Name = "xWijzigColumnFormat";
            this.xWijzigColumnFormat.Size = new System.Drawing.Size(32, 32);
            this.xWijzigColumnFormat.TabIndex = 10;
            this.toolTip1.SetToolTip(this.xWijzigColumnFormat, "Wijzig column formaat");
            this.xWijzigColumnFormat.UseVisualStyleBackColor = true;
            this.xWijzigColumnFormat.Visible = false;
            this.xWijzigColumnFormat.Click += new System.EventHandler(this.xWijzigColumnFormat_Click);
            // 
            // xColumnFormatTextbox
            // 
            this.xColumnFormatTextbox.Location = new System.Drawing.Point(6, 79);
            this.xColumnFormatTextbox.Name = "xColumnFormatTextbox";
            this.xColumnFormatTextbox.Size = new System.Drawing.Size(190, 25);
            this.xColumnFormatTextbox.TabIndex = 9;
            this.xColumnFormatTextbox.TextChanged += new System.EventHandler(this.xColumnFormatTextbox_TextChanged);
            // 
            // xWijzigColumnText
            // 
            this.xWijzigColumnText.FlatAppearance.BorderSize = 0;
            this.xWijzigColumnText.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xWijzigColumnText.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.xWijzigColumnText.Location = new System.Drawing.Point(200, 26);
            this.xWijzigColumnText.Name = "xWijzigColumnText";
            this.xWijzigColumnText.Size = new System.Drawing.Size(32, 32);
            this.xWijzigColumnText.TabIndex = 8;
            this.toolTip1.SetToolTip(this.xWijzigColumnText, "Wijzig column text");
            this.xWijzigColumnText.UseVisualStyleBackColor = true;
            this.xWijzigColumnText.Visible = false;
            this.xWijzigColumnText.Click += new System.EventHandler(this.xWijzigColumnText_Click);
            // 
            // xColumnTextBox
            // 
            this.xColumnTextBox.Location = new System.Drawing.Point(7, 31);
            this.xColumnTextBox.Name = "xColumnTextBox";
            this.xColumnTextBox.Size = new System.Drawing.Size(190, 25);
            this.xColumnTextBox.TabIndex = 7;
            this.xColumnTextBox.TextChanged += new System.EventHandler(this.xColumnTextBox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(7, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Column Text";
            // 
            // xKleurenGroup
            // 
            this.xKleurenGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xKleurenGroup.Controls.Add(this.xKleurRegelPanel);
            this.xKleurenGroup.Controls.Add(this.xStaticColorPanel);
            this.xKleurenGroup.Controls.Add(this.xVariableColorRadio);
            this.xKleurenGroup.Controls.Add(this.xVasteKleurRadio);
            this.xKleurenGroup.Controls.Add(this.xGeenKleurRadio);
            this.xKleurenGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xKleurenGroup.Location = new System.Drawing.Point(8, 303);
            this.xKleurenGroup.Name = "xKleurenGroup";
            this.xKleurenGroup.Size = new System.Drawing.Size(219, 165);
            this.xKleurenGroup.TabIndex = 5;
            this.xKleurenGroup.TabStop = false;
            this.xKleurenGroup.Text = "Columns Kleuren";
            // 
            // xKleurRegelPanel
            // 
            this.xKleurRegelPanel.Controls.Add(this.xColorRegelStatusLabel);
            this.xKleurRegelPanel.Controls.Add(this.xKiesKleurRegels);
            this.xKleurRegelPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xKleurRegelPanel.Location = new System.Drawing.Point(3, 162);
            this.xKleurRegelPanel.Name = "xKleurRegelPanel";
            this.xKleurRegelPanel.Padding = new System.Windows.Forms.Padding(5);
            this.xKleurRegelPanel.Size = new System.Drawing.Size(213, 78);
            this.xKleurRegelPanel.TabIndex = 6;
            this.xKleurRegelPanel.Visible = false;
            // 
            // xColorRegelStatusLabel
            // 
            this.xColorRegelStatusLabel.AutoSize = true;
            this.xColorRegelStatusLabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xColorRegelStatusLabel.Location = new System.Drawing.Point(5, 5);
            this.xColorRegelStatusLabel.Name = "xColorRegelStatusLabel";
            this.xColorRegelStatusLabel.Size = new System.Drawing.Size(69, 17);
            this.xColorRegelStatusLabel.TabIndex = 2;
            this.xColorRegelStatusLabel.Text = "{0} Regels";
            // 
            // xKiesKleurRegels
            // 
            this.xKiesKleurRegels.BackColor = System.Drawing.Color.White;
            this.xKiesKleurRegels.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xKiesKleurRegels.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xKiesKleurRegels.ForeColor = System.Drawing.Color.Black;
            this.xKiesKleurRegels.Location = new System.Drawing.Point(5, 35);
            this.xKiesKleurRegels.Name = "xKiesKleurRegels";
            this.xKiesKleurRegels.Size = new System.Drawing.Size(203, 38);
            this.xKiesKleurRegels.TabIndex = 1;
            this.xKiesKleurRegels.Text = "Kies kleur regels";
            this.toolTip1.SetToolTip(this.xKiesKleurRegels, "Kies Kleur");
            this.xKiesKleurRegels.UseVisualStyleBackColor = false;
            this.xKiesKleurRegels.Click += new System.EventHandler(this.xKiesKleurRegels_Click);
            // 
            // xStaticColorPanel
            // 
            this.xStaticColorPanel.Controls.Add(this.xTextKleurB);
            this.xStaticColorPanel.Controls.Add(this.xColumnKleurB);
            this.xStaticColorPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xStaticColorPanel.Location = new System.Drawing.Point(3, 84);
            this.xStaticColorPanel.Name = "xStaticColorPanel";
            this.xStaticColorPanel.Size = new System.Drawing.Size(213, 78);
            this.xStaticColorPanel.TabIndex = 2;
            this.xStaticColorPanel.Visible = false;
            // 
            // xTextKleurB
            // 
            this.xTextKleurB.BackColor = System.Drawing.Color.White;
            this.xTextKleurB.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xTextKleurB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xTextKleurB.ForeColor = System.Drawing.Color.Black;
            this.xTextKleurB.Location = new System.Drawing.Point(0, 40);
            this.xTextKleurB.Name = "xTextKleurB";
            this.xTextKleurB.Size = new System.Drawing.Size(213, 38);
            this.xTextKleurB.TabIndex = 1;
            this.xTextKleurB.Text = "Text Kleur";
            this.toolTip1.SetToolTip(this.xTextKleurB, "Kies Kleur");
            this.xTextKleurB.UseVisualStyleBackColor = false;
            this.xTextKleurB.Click += new System.EventHandler(this.xTextKleurB_Click);
            // 
            // xColumnKleurB
            // 
            this.xColumnKleurB.BackColor = System.Drawing.Color.White;
            this.xColumnKleurB.Dock = System.Windows.Forms.DockStyle.Top;
            this.xColumnKleurB.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xColumnKleurB.Location = new System.Drawing.Point(0, 0);
            this.xColumnKleurB.Name = "xColumnKleurB";
            this.xColumnKleurB.Size = new System.Drawing.Size(213, 38);
            this.xColumnKleurB.TabIndex = 0;
            this.xColumnKleurB.Text = "Column Kleur";
            this.toolTip1.SetToolTip(this.xColumnKleurB, "Kies Kleur");
            this.xColumnKleurB.UseVisualStyleBackColor = false;
            this.xColumnKleurB.Click += new System.EventHandler(this.xColumnKleurB_Click);
            // 
            // xVariableColorRadio
            // 
            this.xVariableColorRadio.AutoSize = true;
            this.xVariableColorRadio.Dock = System.Windows.Forms.DockStyle.Top;
            this.xVariableColorRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xVariableColorRadio.Location = new System.Drawing.Point(3, 63);
            this.xVariableColorRadio.Name = "xVariableColorRadio";
            this.xVariableColorRadio.Size = new System.Drawing.Size(213, 21);
            this.xVariableColorRadio.TabIndex = 5;
            this.xVariableColorRadio.Text = "Variable Kleur";
            this.toolTip1.SetToolTip(this.xVariableColorRadio, "Kies dit als je de column wilt laten kleuren op basis van een variable");
            this.xVariableColorRadio.UseVisualStyleBackColor = true;
            this.xVariableColorRadio.CheckedChanged += new System.EventHandler(this.xVariableColorRadio_CheckedChanged);
            // 
            // xVasteKleurRadio
            // 
            this.xVasteKleurRadio.AutoSize = true;
            this.xVasteKleurRadio.Dock = System.Windows.Forms.DockStyle.Top;
            this.xVasteKleurRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xVasteKleurRadio.Location = new System.Drawing.Point(3, 42);
            this.xVasteKleurRadio.Name = "xVasteKleurRadio";
            this.xVasteKleurRadio.Size = new System.Drawing.Size(213, 21);
            this.xVasteKleurRadio.TabIndex = 4;
            this.xVasteKleurRadio.Text = "Vast Kleur";
            this.toolTip1.SetToolTip(this.xVasteKleurRadio, "Kies een vaste kleur");
            this.xVasteKleurRadio.UseVisualStyleBackColor = true;
            this.xVasteKleurRadio.CheckedChanged += new System.EventHandler(this.xVasteKleurRadio_CheckedChanged);
            // 
            // xGeenKleurRadio
            // 
            this.xGeenKleurRadio.AutoSize = true;
            this.xGeenKleurRadio.Checked = true;
            this.xGeenKleurRadio.Dock = System.Windows.Forms.DockStyle.Top;
            this.xGeenKleurRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xGeenKleurRadio.Location = new System.Drawing.Point(3, 21);
            this.xGeenKleurRadio.Name = "xGeenKleurRadio";
            this.xGeenKleurRadio.Size = new System.Drawing.Size(213, 21);
            this.xGeenKleurRadio.TabIndex = 3;
            this.xGeenKleurRadio.TabStop = true;
            this.xGeenKleurRadio.Text = "Geen Kleur";
            this.toolTip1.SetToolTip(this.xGeenKleurRadio, "Gebruik geen kleuren");
            this.xGeenKleurRadio.UseVisualStyleBackColor = true;
            this.xGeenKleurRadio.CheckedChanged += new System.EventHandler(this.xGeenKleurRadio_CheckedChanged);
            // 
            // xverborgencheckbox
            // 
            this.xverborgencheckbox.AutoSize = true;
            this.xverborgencheckbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xverborgencheckbox.Location = new System.Drawing.Point(10, 155);
            this.xverborgencheckbox.Name = "xverborgencheckbox";
            this.xverborgencheckbox.Size = new System.Drawing.Size(89, 21);
            this.xverborgencheckbox.TabIndex = 4;
            this.xverborgencheckbox.Text = "Verborgen";
            this.xverborgencheckbox.UseVisualStyleBackColor = true;
            this.xverborgencheckbox.CheckedChanged += new System.EventHandler(this.xverborgencheckbox_CheckedChanged);
            // 
            // xBerekeningGroup
            // 
            this.xBerekeningGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xBerekeningGroup.Controls.Add(this.xBerekenGemiddeldRadio);
            this.xBerekeningGroup.Controls.Add(this.xSomAllesRadio);
            this.xBerekeningGroup.Controls.Add(this.xGeenBerekeningRadio);
            this.xBerekeningGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xBerekeningGroup.Location = new System.Drawing.Point(7, 189);
            this.xBerekeningGroup.Name = "xBerekeningGroup";
            this.xBerekeningGroup.Size = new System.Drawing.Size(220, 108);
            this.xBerekeningGroup.TabIndex = 3;
            this.xBerekeningGroup.TabStop = false;
            this.xBerekeningGroup.Text = "Berekening";
            // 
            // xBerekenGemiddeldRadio
            // 
            this.xBerekenGemiddeldRadio.AutoSize = true;
            this.xBerekenGemiddeldRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xBerekenGemiddeldRadio.Location = new System.Drawing.Point(7, 78);
            this.xBerekenGemiddeldRadio.Name = "xBerekenGemiddeldRadio";
            this.xBerekenGemiddeldRadio.Size = new System.Drawing.Size(147, 21);
            this.xBerekenGemiddeldRadio.TabIndex = 2;
            this.xBerekenGemiddeldRadio.TabStop = true;
            this.xBerekenGemiddeldRadio.Text = "Bereken Gemiddelde";
            this.xBerekenGemiddeldRadio.UseVisualStyleBackColor = true;
            this.xBerekenGemiddeldRadio.CheckedChanged += new System.EventHandler(this.xBerekenGemiddeldRadio_CheckedChanged);
            // 
            // xSomAllesRadio
            // 
            this.xSomAllesRadio.AutoSize = true;
            this.xSomAllesRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xSomAllesRadio.Location = new System.Drawing.Point(7, 51);
            this.xSomAllesRadio.Name = "xSomAllesRadio";
            this.xSomAllesRadio.Size = new System.Drawing.Size(86, 21);
            this.xSomAllesRadio.TabIndex = 1;
            this.xSomAllesRadio.TabStop = true;
            this.xSomAllesRadio.Text = "SOM Alles";
            this.xSomAllesRadio.UseVisualStyleBackColor = true;
            this.xSomAllesRadio.CheckedChanged += new System.EventHandler(this.xSomAllesRadio_CheckedChanged);
            // 
            // xGeenBerekeningRadio
            // 
            this.xGeenBerekeningRadio.AutoSize = true;
            this.xGeenBerekeningRadio.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xGeenBerekeningRadio.Location = new System.Drawing.Point(7, 24);
            this.xGeenBerekeningRadio.Name = "xGeenBerekeningRadio";
            this.xGeenBerekeningRadio.Size = new System.Drawing.Size(124, 21);
            this.xGeenBerekeningRadio.TabIndex = 0;
            this.xGeenBerekeningRadio.TabStop = true;
            this.xGeenBerekeningRadio.Text = "Geen Berekening";
            this.xGeenBerekeningRadio.UseVisualStyleBackColor = true;
            this.xGeenBerekeningRadio.CheckedChanged += new System.EventHandler(this.xGeenBerekeningRadio_CheckedChanged);
            // 
            // xautoculmncheckbox
            // 
            this.xautoculmncheckbox.AutoSize = true;
            this.xautoculmncheckbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xautoculmncheckbox.Location = new System.Drawing.Point(10, 128);
            this.xautoculmncheckbox.Name = "xautoculmncheckbox";
            this.xautoculmncheckbox.Size = new System.Drawing.Size(103, 21);
            this.xautoculmncheckbox.TabIndex = 1;
            this.xautoculmncheckbox.Text = "Auto Breedte";
            this.xautoculmncheckbox.UseVisualStyleBackColor = true;
            this.xautoculmncheckbox.CheckedChanged += new System.EventHandler(this.xautoculmncheckbox_CheckedChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // ExcelOptiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(912, 608);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ExcelOptiesForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Column Opties";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xOptiesView)).EndInit();
            this.xoptiespanel.ResumeLayout(false);
            this.xoptiespanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xBeschikbareColumns)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xZichtbareColumnsView)).EndInit();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xColumnBreedte)).EndInit();
            this.xKleurenGroup.ResumeLayout(false);
            this.xKleurenGroup.PerformLayout();
            this.xKleurRegelPanel.ResumeLayout(false);
            this.xKleurRegelPanel.PerformLayout();
            this.xStaticColorPanel.ResumeLayout(false);
            this.xBerekeningGroup.ResumeLayout(false);
            this.xBerekeningGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xOpslaan;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.CheckBox xverborgencheckbox;
        private System.Windows.Forms.GroupBox xBerekeningGroup;
        private System.Windows.Forms.RadioButton xBerekenGemiddeldRadio;
        private System.Windows.Forms.RadioButton xSomAllesRadio;
        private System.Windows.Forms.RadioButton xGeenBerekeningRadio;
        private System.Windows.Forms.CheckBox xautoculmncheckbox;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton xItemUp;
        private System.Windows.Forms.ToolStripButton xItemDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton xItemRight;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton xItemDelete;
        private System.Windows.Forms.GroupBox xKleurenGroup;
        private System.Windows.Forms.Button xTextKleurB;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button xColumnKleurB;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private BrightIdeasSoftware.ObjectListView xOptiesView;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.ToolStrip xoptiespanel;
        private System.Windows.Forms.ToolStripButton xAddOptieButton;
        private System.Windows.Forms.ToolStripButton xDeleteOptieButton;
        private BrightIdeasSoftware.ObjectListView xBeschikbareColumns;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.ObjectListView xZichtbareColumnsView;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.TextBox xColumnTextBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel xKleurRegelPanel;
        private System.Windows.Forms.Label xColorRegelStatusLabel;
        private System.Windows.Forms.Button xKiesKleurRegels;
        private System.Windows.Forms.Panel xStaticColorPanel;
        private System.Windows.Forms.RadioButton xVariableColorRadio;
        private System.Windows.Forms.RadioButton xVasteKleurRadio;
        private System.Windows.Forms.RadioButton xGeenKleurRadio;
        private System.Windows.Forms.Button xWijzigColumnText;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripButton xEditOpties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.Button xWijzigColumnFormat;
        private System.Windows.Forms.TextBox xColumnFormatTextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button xWijzigColumnBreedte;
        private System.Windows.Forms.NumericUpDown xColumnBreedte;
        private System.Windows.Forms.Button xtoepassen;
        private MetroFramework.Controls.MetroCheckBox xstandaard;
    }
}