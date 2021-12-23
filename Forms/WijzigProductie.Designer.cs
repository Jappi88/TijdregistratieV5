using Controls;

namespace Forms
{
    partial class WijzigProductie
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WijzigProductie));
            this.xsluiten = new System.Windows.Forms.Button();
            this.xOpslaan = new System.Windows.Forms.Button();
            this.xbewerkingen = new System.Windows.Forms.ComboBox();
            this.xvoegbewtoe = new System.Windows.Forms.Button();
            this.xwerkplekken = new System.Windows.Forms.Button();
            this.xbeheeronderbrekeningen = new System.Windows.Forms.Button();
            this.xverwijderbewerking = new System.Windows.Forms.Button();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.xbewerkinglijst = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xnaambewerkingc = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn12 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn13 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn14 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn15 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn16 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn17 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn18 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn19 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn20 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn21 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn22 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn23 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn24 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn25 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn26 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.xedit = new System.Windows.Forms.Button();
            this.objectEditorUI1 = new Controls.ObjectEditorUI();
            this.materiaalUI1 = new Controls.MateriaalUI();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xbewerkinglijst)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            this.SuspendLayout();
            // 
            // xsluiten
            // 
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.ForeColor = System.Drawing.Color.Black;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(128, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(119, 38);
            this.xsluiten.TabIndex = 1;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.button1_Click);
            // 
            // xOpslaan
            // 
            this.xOpslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOpslaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOpslaan.ForeColor = System.Drawing.Color.Black;
            this.xOpslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xOpslaan.Location = new System.Drawing.Point(3, 3);
            this.xOpslaan.Name = "xOpslaan";
            this.xOpslaan.Size = new System.Drawing.Size(119, 38);
            this.xOpslaan.TabIndex = 3;
            this.xOpslaan.Text = "Opslaan";
            this.xOpslaan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xOpslaan.UseVisualStyleBackColor = true;
            this.xOpslaan.Click += new System.EventHandler(this.button2_Click);
            // 
            // xbewerkingen
            // 
            this.xbewerkingen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.xbewerkingen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.xbewerkingen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbewerkingen.FormattingEnabled = true;
            this.xbewerkingen.Location = new System.Drawing.Point(6, 27);
            this.xbewerkingen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xbewerkingen.Name = "xbewerkingen";
            this.xbewerkingen.Size = new System.Drawing.Size(293, 28);
            this.xbewerkingen.TabIndex = 25;
            // 
            // xvoegbewtoe
            // 
            this.xvoegbewtoe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xvoegbewtoe.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvoegbewtoe.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xvoegbewtoe.Location = new System.Drawing.Point(305, 21);
            this.xvoegbewtoe.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xvoegbewtoe.Name = "xvoegbewtoe";
            this.xvoegbewtoe.Size = new System.Drawing.Size(44, 40);
            this.xvoegbewtoe.TabIndex = 27;
            this.xvoegbewtoe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xvoegbewtoe.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xvoegbewtoe, "Voeg bewerking toe");
            this.xvoegbewtoe.UseVisualStyleBackColor = true;
            this.xvoegbewtoe.Click += new System.EventHandler(this.xvoegbewtoe_Click);
            // 
            // xwerkplekken
            // 
            this.xwerkplekken.Enabled = false;
            this.xwerkplekken.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xwerkplekken.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xwerkplekken.Image = global::ProductieManager.Properties.Resources.iconfinder_technologymachineelectronic32_32;
            this.xwerkplekken.Location = new System.Drawing.Point(405, 21);
            this.xwerkplekken.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xwerkplekken.Name = "xwerkplekken";
            this.xwerkplekken.Size = new System.Drawing.Size(40, 40);
            this.xwerkplekken.TabIndex = 27;
            this.xwerkplekken.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xwerkplekken.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xwerkplekken, "Beheer Werkplekken");
            this.xwerkplekken.UseVisualStyleBackColor = true;
            this.xwerkplekken.Click += new System.EventHandler(this.xpersoneel_Click);
            // 
            // xbeheeronderbrekeningen
            // 
            this.xbeheeronderbrekeningen.Enabled = false;
            this.xbeheeronderbrekeningen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xbeheeronderbrekeningen.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbeheeronderbrekeningen.Image = global::ProductieManager.Properties.Resources.onderhoud32_32;
            this.xbeheeronderbrekeningen.Location = new System.Drawing.Point(451, 21);
            this.xbeheeronderbrekeningen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xbeheeronderbrekeningen.Name = "xbeheeronderbrekeningen";
            this.xbeheeronderbrekeningen.Size = new System.Drawing.Size(40, 40);
            this.xbeheeronderbrekeningen.TabIndex = 43;
            this.xbeheeronderbrekeningen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xbeheeronderbrekeningen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xbeheeronderbrekeningen, "Beheer Onderbrekeningen");
            this.xbeheeronderbrekeningen.UseVisualStyleBackColor = true;
            this.xbeheeronderbrekeningen.Click += new System.EventHandler(this.xbeheeronderbrekeningen_Click);
            // 
            // xverwijderbewerking
            // 
            this.xverwijderbewerking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xverwijderbewerking.Enabled = false;
            this.xverwijderbewerking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xverwijderbewerking.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xverwijderbewerking.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xverwijderbewerking.Location = new System.Drawing.Point(800, 21);
            this.xverwijderbewerking.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xverwijderbewerking.Name = "xverwijderbewerking";
            this.xverwijderbewerking.Size = new System.Drawing.Size(40, 40);
            this.xverwijderbewerking.TabIndex = 28;
            this.xverwijderbewerking.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xverwijderbewerking, "Verwijder geselecteerde bewerking(en)");
            this.xverwijderbewerking.UseVisualStyleBackColor = true;
            this.xverwijderbewerking.Click += new System.EventHandler(this.xverwijderbewerking_Click);
            // 
            // groupBox4
            // 
            this.groupBox4.BackColor = System.Drawing.Color.Transparent;
            this.groupBox4.Controls.Add(this.xedit);
            this.groupBox4.Controls.Add(this.xbewerkinglijst);
            this.groupBox4.Controls.Add(this.xverwijderbewerking);
            this.groupBox4.Controls.Add(this.xwerkplekken);
            this.groupBox4.Controls.Add(this.xbeheeronderbrekeningen);
            this.groupBox4.Controls.Add(this.xbewerkingen);
            this.groupBox4.Controls.Add(this.xvoegbewtoe);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox4.ForeColor = System.Drawing.Color.Black;
            this.groupBox4.Location = new System.Drawing.Point(0, 0);
            this.groupBox4.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Padding = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.groupBox4.Size = new System.Drawing.Size(845, 382);
            this.groupBox4.TabIndex = 0;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Bewerkingen";
            // 
            // xbewerkinglijst
            // 
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn1);
            this.xbewerkinglijst.AllColumns.Add(this.xnaambewerkingc);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn12);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn13);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn14);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn15);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn16);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn17);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn18);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn19);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn20);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn21);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn22);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn23);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn24);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn25);
            this.xbewerkinglijst.AllColumns.Add(this.olvColumn26);
            this.xbewerkinglijst.AllowColumnReorder = true;
            this.xbewerkinglijst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xbewerkinglijst.CellEditUseWholeCell = false;
            this.xbewerkinglijst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.xnaambewerkingc,
            this.olvColumn12,
            this.olvColumn13,
            this.olvColumn14,
            this.olvColumn15,
            this.olvColumn16,
            this.olvColumn17,
            this.olvColumn18,
            this.olvColumn19,
            this.olvColumn20,
            this.olvColumn21,
            this.olvColumn22,
            this.olvColumn23,
            this.olvColumn24,
            this.olvColumn25,
            this.olvColumn26});
            this.xbewerkinglijst.Cursor = System.Windows.Forms.Cursors.Default;
            this.xbewerkinglijst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbewerkinglijst.FullRowSelect = true;
            this.xbewerkinglijst.HeaderWordWrap = true;
            this.xbewerkinglijst.HideSelection = false;
            this.xbewerkinglijst.LargeImageList = this.imageList1;
            this.xbewerkinglijst.Location = new System.Drawing.Point(6, 66);
            this.xbewerkinglijst.Name = "xbewerkinglijst";
            this.xbewerkinglijst.OwnerDraw = false;
            this.xbewerkinglijst.ShowItemCountOnGroups = true;
            this.xbewerkinglijst.ShowItemToolTips = true;
            this.xbewerkinglijst.Size = new System.Drawing.Size(833, 311);
            this.xbewerkinglijst.SmallImageList = this.imageList1;
            this.xbewerkinglijst.SpaceBetweenGroups = 10;
            this.xbewerkinglijst.TabIndex = 26;
            this.xbewerkinglijst.TileSize = new System.Drawing.Size(750, 120);
            this.xbewerkinglijst.UseCompatibleStateImageBehavior = false;
            this.xbewerkinglijst.UseExplorerTheme = true;
            this.xbewerkinglijst.UseFiltering = true;
            this.xbewerkinglijst.UseHotControls = false;
            this.xbewerkinglijst.UseHotItem = true;
            this.xbewerkinglijst.UseOverlays = false;
            this.xbewerkinglijst.UseTranslucentHotItem = true;
            this.xbewerkinglijst.UseTranslucentSelection = true;
            this.xbewerkinglijst.View = System.Windows.Forms.View.Details;
            this.xbewerkinglijst.SelectedIndexChanged += new System.EventHandler(this.xbewerkinglijst_SelectedIndexChanged);
            this.xbewerkinglijst.DoubleClick += new System.EventHandler(this.xbewerkinglijst_DoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Naam";
            this.olvColumn1.Text = "Naam";
            this.olvColumn1.ToolTipText = "Bewerking naam";
            this.olvColumn1.Width = 200;
            this.olvColumn1.WordWrap = true;
            // 
            // xnaambewerkingc
            // 
            this.xnaambewerkingc.AspectName = "Omschrijving";
            this.xnaambewerkingc.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            this.xnaambewerkingc.IsEditable = false;
            this.xnaambewerkingc.IsTileViewColumn = true;
            this.xnaambewerkingc.Text = "Omschrijving";
            this.xnaambewerkingc.ToolTipText = "Product Omschrijving";
            this.xnaambewerkingc.Width = 246;
            this.xnaambewerkingc.WordWrap = true;
            // 
            // olvColumn12
            // 
            this.olvColumn12.AspectName = "ArtikelNr";
            this.olvColumn12.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvColumn12.ImageAspectName = "";
            this.olvColumn12.IsEditable = false;
            this.olvColumn12.IsTileViewColumn = true;
            this.olvColumn12.Text = "ArtikelNr";
            this.olvColumn12.ToolTipText = "Product Artikel Nummer";
            this.olvColumn12.Width = 101;
            this.olvColumn12.WordWrap = true;
            // 
            // olvColumn13
            // 
            this.olvColumn13.AspectName = "ProductieNr";
            this.olvColumn13.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            this.olvColumn13.IsEditable = false;
            this.olvColumn13.IsTileViewColumn = true;
            this.olvColumn13.Text = "ProductieNr";
            this.olvColumn13.ToolTipText = "Productie Nummer";
            this.olvColumn13.Width = 85;
            this.olvColumn13.WordWrap = true;
            // 
            // olvColumn14
            // 
            this.olvColumn14.AspectName = "Aantal";
            this.olvColumn14.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            this.olvColumn14.IsEditable = false;
            this.olvColumn14.IsTileViewColumn = true;
            this.olvColumn14.Text = "Aantal";
            this.olvColumn14.ToolTipText = "Productie Aantal";
            this.olvColumn14.Width = 81;
            this.olvColumn14.WordWrap = true;
            // 
            // olvColumn15
            // 
            this.olvColumn15.AspectName = "AantalGemaakt";
            this.olvColumn15.IsEditable = false;
            this.olvColumn15.IsTileViewColumn = true;
            this.olvColumn15.Text = "#Gemaakt";
            this.olvColumn15.ToolTipText = "Aantal Producten Gemaakt";
            this.olvColumn15.Width = 120;
            // 
            // olvColumn16
            // 
            this.olvColumn16.AspectName = "Gereed";
            this.olvColumn16.AspectToStringFormat = "{0}%";
            this.olvColumn16.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            this.olvColumn16.IsEditable = false;
            this.olvColumn16.IsTileViewColumn = true;
            this.olvColumn16.Text = "Gereed(%)";
            this.olvColumn16.ToolTipText = "Gereed Percentage";
            this.olvColumn16.Width = 87;
            this.olvColumn16.WordWrap = true;
            // 
            // olvColumn17
            // 
            this.olvColumn17.AspectName = "DoorloopTijd";
            this.olvColumn17.AspectToStringFormat = "{0} uur";
            this.olvColumn17.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            this.olvColumn17.IsEditable = false;
            this.olvColumn17.IsTileViewColumn = true;
            this.olvColumn17.Text = "DoorloopTijd";
            this.olvColumn17.ToolTipText = "Productie Doorlooptijd In Uren";
            this.olvColumn17.Width = 95;
            this.olvColumn17.WordWrap = true;
            // 
            // olvColumn18
            // 
            this.olvColumn18.AspectName = "TijdGewerkt";
            this.olvColumn18.AspectToStringFormat = "{0} uur";
            this.olvColumn18.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            this.olvColumn18.IsEditable = false;
            this.olvColumn18.IsTileViewColumn = true;
            this.olvColumn18.Text = "Tijd Gewerkt";
            this.olvColumn18.ToolTipText = "Totaal Uren Aan Gewerkt";
            this.olvColumn18.Width = 89;
            this.olvColumn18.WordWrap = true;
            // 
            // olvColumn19
            // 
            this.olvColumn19.AspectName = "TotaalTijdGewerkt";
            this.olvColumn19.AspectToStringFormat = "{0} uur";
            this.olvColumn19.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            this.olvColumn19.IsEditable = false;
            this.olvColumn19.IsTileViewColumn = true;
            this.olvColumn19.Text = "Totaal Tijd Gewerkt";
            this.olvColumn19.ToolTipText = "Totaal gewerkte tijd";
            this.olvColumn19.Width = 104;
            this.olvColumn19.WordWrap = true;
            // 
            // olvColumn20
            // 
            this.olvColumn20.AspectName = "PerUur";
            this.olvColumn20.AspectToStringFormat = "{0} p/u";
            this.olvColumn20.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            this.olvColumn20.IsEditable = false;
            this.olvColumn20.IsTileViewColumn = true;
            this.olvColumn20.Text = "Aantal/Uur";
            this.olvColumn20.ToolTipText = "Aantal producten per uur";
            this.olvColumn20.Width = 106;
            this.olvColumn20.WordWrap = true;
            // 
            // olvColumn21
            // 
            this.olvColumn21.AspectName = "GemiddeldPerUur";
            this.olvColumn21.AspectToStringFormat = "{0} p/u";
            this.olvColumn21.Text = "Gemiddeld PerUur";
            this.olvColumn21.ToolTipText = "Totaal gemiddelde aantal per uur";
            this.olvColumn21.Width = 120;
            // 
            // olvColumn22
            // 
            this.olvColumn22.AspectName = "ActueelPerUur";
            this.olvColumn22.AspectToStringFormat = "{0} p/u";
            this.olvColumn22.Text = "Actueel/Uur";
            this.olvColumn22.ToolTipText = "Actuele aantal per uur";
            this.olvColumn22.Width = 120;
            // 
            // olvColumn23
            // 
            this.olvColumn23.AspectName = "VerwachtLeverDatum";
            this.olvColumn23.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            this.olvColumn23.IsEditable = false;
            this.olvColumn23.IsTileViewColumn = true;
            this.olvColumn23.Text = "Verwachte Leverdatum";
            this.olvColumn23.ToolTipText = "Verwachte Leverdatum";
            this.olvColumn23.Width = 151;
            this.olvColumn23.WordWrap = true;
            // 
            // olvColumn24
            // 
            this.olvColumn24.AspectName = "LeverDatum";
            this.olvColumn24.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            this.olvColumn24.IsEditable = false;
            this.olvColumn24.IsTileViewColumn = true;
            this.olvColumn24.Text = "Leverdatum";
            this.olvColumn24.ToolTipText = "Productie Leverdatum";
            this.olvColumn24.Width = 146;
            this.olvColumn24.WordWrap = true;
            // 
            // olvColumn25
            // 
            this.olvColumn25.AspectName = "AantalBewerkingen";
            this.olvColumn25.AspectToStringFormat = "";
            this.olvColumn25.IsEditable = false;
            this.olvColumn25.IsTileViewColumn = true;
            this.olvColumn25.Text = "#Bewerkingen";
            this.olvColumn25.ToolTipText = "Totaal aantal bewerkingen";
            this.olvColumn25.Width = 120;
            // 
            // olvColumn26
            // 
            this.olvColumn26.AspectName = "AantalPersonen";
            this.olvColumn26.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F);
            this.olvColumn26.IsEditable = false;
            this.olvColumn26.IsTileViewColumn = true;
            this.olvColumn26.Text = "#Personen";
            this.olvColumn26.ToolTipText = "Aantal Personen Mee Bezig";
            this.olvColumn26.Width = 86;
            this.olvColumn26.WordWrap = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "operation.png");
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 489);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(853, 46);
            this.panel2.TabIndex = 25;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.White;
            this.panel3.Controls.Add(this.xsluiten);
            this.panel3.Controls.Add(this.xOpslaan);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(600, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(253, 46);
            this.panel3.TabIndex = 25;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Bewerkingen";
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Location = new System.Drawing.Point(20, 60);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 1;
            this.metroTabControl1.Size = new System.Drawing.Size(853, 424);
            this.metroTabControl1.TabIndex = 26;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.objectEditorUI1);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(845, 382);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Productie Info";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.groupBox4);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(845, 382);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Bewerkingen";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            this.metroTabPage2.Visible = false;
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.materiaalUI1);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 10;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(845, 382);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Materialen";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 10;
            this.metroTabPage3.Visible = false;
            // 
            // xedit
            // 
            this.xedit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xedit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xedit.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xedit.Location = new System.Drawing.Point(355, 21);
            this.xedit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xedit.Name = "xedit";
            this.xedit.Size = new System.Drawing.Size(44, 40);
            this.xedit.TabIndex = 44;
            this.xedit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xedit.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xedit, "Wijzig bewerking");
            this.xedit.UseVisualStyleBackColor = true;
            this.xedit.Click += new System.EventHandler(this.xedit_Click);
            // 
            // objectEditorUI1
            // 
            this.objectEditorUI1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.objectEditorUI1.BackColor = System.Drawing.Color.White;
            this.objectEditorUI1.DisplayTypes = ((System.Collections.Generic.List<System.Type>)(resources.GetObject("objectEditorUI1.DisplayTypes")));
            this.objectEditorUI1.ExcludeItems = ((System.Collections.Generic.List<string>)(resources.GetObject("objectEditorUI1.ExcludeItems")));
            this.objectEditorUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.objectEditorUI1.Instance = null;
            this.objectEditorUI1.Location = new System.Drawing.Point(4, 4);
            this.objectEditorUI1.Margin = new System.Windows.Forms.Padding(4);
            this.objectEditorUI1.Name = "objectEditorUI1";
            this.objectEditorUI1.Size = new System.Drawing.Size(837, 374);
            this.objectEditorUI1.TabIndex = 2;
            // 
            // materiaalUI1
            // 
            this.materiaalUI1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.materiaalUI1.BackColor = System.Drawing.Color.Transparent;
            this.materiaalUI1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.materiaalUI1.Formulier = null;
            this.materiaalUI1.Location = new System.Drawing.Point(3, 3);
            this.materiaalUI1.Name = "materiaalUI1";
            this.materiaalUI1.Size = new System.Drawing.Size(980, 463);
            this.materiaalUI1.TabIndex = 0;
            // 
            // WijzigProductie
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.ClientSize = new System.Drawing.Size(893, 555);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(705, 480);
            this.Name = "WijzigProductie";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "Productie Formulier";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.AddProduction_FormClosed);
            this.Shown += new System.EventHandler(this.WijzigProductie_Shown);
            this.groupBox4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xbewerkinglijst)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xOpslaan;
        private System.Windows.Forms.ComboBox xbewerkingen;
        private System.Windows.Forms.Button xwerkplekken;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Button xvoegbewtoe;
        private System.Windows.Forms.Button xverwijderbewerking;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        public System.Windows.Forms.ImageList imageList1;
        private MateriaalUI materiaalUI1;
        private System.Windows.Forms.Button xbeheeronderbrekeningen;
        private System.Windows.Forms.ToolTip toolTip1;
        private BrightIdeasSoftware.ObjectListView xbewerkinglijst;
        private BrightIdeasSoftware.OLVColumn xnaambewerkingc;
        private BrightIdeasSoftware.OLVColumn olvColumn12;
        private BrightIdeasSoftware.OLVColumn olvColumn13;
        private BrightIdeasSoftware.OLVColumn olvColumn14;
        private BrightIdeasSoftware.OLVColumn olvColumn15;
        private BrightIdeasSoftware.OLVColumn olvColumn16;
        private BrightIdeasSoftware.OLVColumn olvColumn17;
        private BrightIdeasSoftware.OLVColumn olvColumn18;
        private BrightIdeasSoftware.OLVColumn olvColumn19;
        private BrightIdeasSoftware.OLVColumn olvColumn20;
        private BrightIdeasSoftware.OLVColumn olvColumn21;
        private BrightIdeasSoftware.OLVColumn olvColumn22;
        private BrightIdeasSoftware.OLVColumn olvColumn23;
        private BrightIdeasSoftware.OLVColumn olvColumn24;
        private BrightIdeasSoftware.OLVColumn olvColumn25;
        private BrightIdeasSoftware.OLVColumn olvColumn26;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private ObjectEditorUI objectEditorUI1;
        private System.Windows.Forms.Button xedit;
    }
}