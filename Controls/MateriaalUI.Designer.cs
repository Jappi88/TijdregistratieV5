namespace Controls
{
    partial class MateriaalUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MateriaalUI));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xmateriaallijst = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.xmateriaalpanel = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.xaantal = new System.Windows.Forms.NumericUpDown();
            this.xafkeurpercent = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.xafkeurvalue = new System.Windows.Forms.NumericUpDown();
            this.xniewmatb = new System.Windows.Forms.Button();
            this.xwijzigmatb = new System.Windows.Forms.Button();
            this.xverbruiktlabel = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.xaantalperstuk = new System.Windows.Forms.NumericUpDown();
            this.xeenheid = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.xlocatie = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.xartikelnr = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.xomschrijving = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.xaantalklaarzetlabel = new System.Windows.Forms.Label();
            this.xklaarzetpanel = new System.Windows.Forms.Panel();
            this.xklaarzetimage = new System.Windows.Forms.PictureBox();
            this.xklaarzetlabel = new System.Windows.Forms.Label();
            this.xlocatielabel = new System.Windows.Forms.Label();
            this.xverwijderb = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.xstatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xmateriaallijst)).BeginInit();
            this.panel1.SuspendLayout();
            this.xmateriaalpanel.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xaantal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xafkeurvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalperstuk)).BeginInit();
            this.xklaarzetpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xklaarzetimage)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.xmateriaallijst);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.xverwijderb);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(782, 483);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Materialen";
            // 
            // xmateriaallijst
            // 
            this.xmateriaallijst.AllColumns.Add(this.olvColumn1);
            this.xmateriaallijst.AllColumns.Add(this.olvColumn2);
            this.xmateriaallijst.AllColumns.Add(this.olvColumn3);
            this.xmateriaallijst.AllColumns.Add(this.olvColumn4);
            this.xmateriaallijst.AllColumns.Add(this.olvColumn5);
            this.xmateriaallijst.AllColumns.Add(this.olvColumn6);
            this.xmateriaallijst.AllColumns.Add(this.olvColumn7);
            this.xmateriaallijst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xmateriaallijst.CellEditUseWholeCell = false;
            this.xmateriaallijst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn6,
            this.olvColumn7});
            this.xmateriaallijst.Cursor = System.Windows.Forms.Cursors.Default;
            this.xmateriaallijst.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmateriaallijst.FullRowSelect = true;
            this.xmateriaallijst.HideSelection = false;
            this.xmateriaallijst.LargeImageList = this.imageList1;
            this.xmateriaallijst.Location = new System.Drawing.Point(361, 77);
            this.xmateriaallijst.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xmateriaallijst.Name = "xmateriaallijst";
            this.xmateriaallijst.ShowItemCountOnGroups = true;
            this.xmateriaallijst.ShowItemToolTips = true;
            this.xmateriaallijst.Size = new System.Drawing.Size(415, 401);
            this.xmateriaallijst.SmallImageList = this.imageList1;
            this.xmateriaallijst.SpaceBetweenGroups = 10;
            this.xmateriaallijst.TabIndex = 1;
            this.xmateriaallijst.TileSize = new System.Drawing.Size(350, 200);
            this.xmateriaallijst.UseCompatibleStateImageBehavior = false;
            this.xmateriaallijst.UseExplorerTheme = true;
            this.xmateriaallijst.UseFilterIndicator = true;
            this.xmateriaallijst.UseFiltering = true;
            this.xmateriaallijst.UseHotItem = true;
            this.xmateriaallijst.UseTranslucentHotItem = true;
            this.xmateriaallijst.UseTranslucentSelection = true;
            this.xmateriaallijst.View = System.Windows.Forms.View.Details;
            this.xmateriaallijst.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.xmateriaallijst_CellToolTipShowing);
            this.xmateriaallijst.SelectedIndexChanged += new System.EventHandler(this.xmateriaallijst_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Omschrijving";
            this.olvColumn1.Groupable = false;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.Text = "Omschrijving";
            this.olvColumn1.ToolTipText = "Product Omschrijving";
            this.olvColumn1.Width = 212;
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "ArtikelNr";
            this.olvColumn2.AspectToStringFormat = "";
            this.olvColumn2.Groupable = false;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.IsTileViewColumn = true;
            this.olvColumn2.Text = "ArtikelNr";
            this.olvColumn2.ToolTipText = "Product ArtikelNr";
            this.olvColumn2.Width = 100;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Locatie";
            this.olvColumn3.AspectToStringFormat = "";
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.IsTileViewColumn = true;
            this.olvColumn3.Text = "Locatie";
            this.olvColumn3.ToolTipText = "Product Locatie";
            this.olvColumn3.Width = 75;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Eenheid";
            this.olvColumn4.AspectToStringFormat = "";
            this.olvColumn4.Groupable = false;
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.IsTileViewColumn = true;
            this.olvColumn4.Text = "Eenheid";
            this.olvColumn4.ToolTipText = "Product Eenheid";
            this.olvColumn4.Width = 75;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "AantalPerStuk";
            this.olvColumn5.AspectToStringFormat = "";
            this.olvColumn5.Groupable = false;
            this.olvColumn5.IsEditable = false;
            this.olvColumn5.IsTileViewColumn = true;
            this.olvColumn5.Text = "Per Stuk";
            this.olvColumn5.ToolTipText = "Aantal Per Stuk";
            this.olvColumn5.Width = 75;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "Aantal";
            this.olvColumn6.AspectToStringFormat = "";
            this.olvColumn6.Groupable = false;
            this.olvColumn6.IsEditable = false;
            this.olvColumn6.IsTileViewColumn = true;
            this.olvColumn6.Text = "Aantal";
            this.olvColumn6.ToolTipText = "Product Aantal";
            this.olvColumn6.Width = 100;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "AantalAfkeur";
            this.olvColumn7.AspectToStringFormat = "";
            this.olvColumn7.Groupable = false;
            this.olvColumn7.IsEditable = false;
            this.olvColumn7.IsTileViewColumn = true;
            this.olvColumn7.Text = "Afkeur";
            this.olvColumn7.ToolTipText = "Product Aantal Afkeur";
            this.olvColumn7.Width = 75;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "business_package_box_products_2343.png");
            this.imageList1.Images.SetKeyName(1, "business_package_box_accept_productorpackagetoaccept_negocio_paquet_2334.png");
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.xmateriaalpanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(3, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(352, 455);
            this.panel1.TabIndex = 3;
            // 
            // xmateriaalpanel
            // 
            this.xmateriaalpanel.Controls.Add(this.panel2);
            this.xmateriaalpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmateriaalpanel.Location = new System.Drawing.Point(0, 0);
            this.xmateriaalpanel.Name = "xmateriaalpanel";
            this.xmateriaalpanel.Size = new System.Drawing.Size(352, 455);
            this.xmateriaalpanel.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.groupBox2);
            this.panel2.Controls.Add(this.xaantalklaarzetlabel);
            this.panel2.Controls.Add(this.xklaarzetpanel);
            this.panel2.Controls.Add(this.xlocatielabel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(352, 455);
            this.panel2.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.xaantal);
            this.groupBox2.Controls.Add(this.xafkeurpercent);
            this.groupBox2.Controls.Add(this.label9);
            this.groupBox2.Controls.Add(this.xafkeurvalue);
            this.groupBox2.Controls.Add(this.xniewmatb);
            this.groupBox2.Controls.Add(this.xwijzigmatb);
            this.groupBox2.Controls.Add(this.xverbruiktlabel);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.xaantalperstuk);
            this.groupBox2.Controls.Add(this.xeenheid);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.xlocatie);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.xartikelnr);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.xomschrijving);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(352, 321);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Wijzig Selectie";
            // 
            // xaantal
            // 
            this.xaantal.DecimalPlaces = 4;
            this.xaantal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantal.Location = new System.Drawing.Point(204, 233);
            this.xaantal.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantal.Name = "xaantal";
            this.xaantal.Size = new System.Drawing.Size(120, 25);
            this.xaantal.TabIndex = 17;
            this.xaantal.ValueChanged += new System.EventHandler(this.xaantal_ValueChanged);
            // 
            // xafkeurpercent
            // 
            this.xafkeurpercent.AutoSize = true;
            this.xafkeurpercent.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xafkeurpercent.Location = new System.Drawing.Point(129, 289);
            this.xafkeurpercent.Name = "xafkeurpercent";
            this.xafkeurpercent.Size = new System.Drawing.Size(73, 21);
            this.xafkeurpercent.TabIndex = 16;
            this.xafkeurpercent.Text = "Afkeur %";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 261);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(56, 21);
            this.label9.TabIndex = 15;
            this.label9.Text = "Afkeur";
            // 
            // xafkeurvalue
            // 
            this.xafkeurvalue.DecimalPlaces = 4;
            this.xafkeurvalue.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xafkeurvalue.Location = new System.Drawing.Point(3, 285);
            this.xafkeurvalue.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xafkeurvalue.Name = "xafkeurvalue";
            this.xafkeurvalue.Size = new System.Drawing.Size(120, 25);
            this.xafkeurvalue.TabIndex = 14;
            this.xafkeurvalue.ValueChanged += new System.EventHandler(this.xafkeurvalue_ValueChanged);
            // 
            // xniewmatb
            // 
            this.xniewmatb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xniewmatb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xniewmatb.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xniewmatb.Location = new System.Drawing.Point(244, 270);
            this.xniewmatb.Name = "xniewmatb";
            this.xniewmatb.Size = new System.Drawing.Size(40, 40);
            this.xniewmatb.TabIndex = 13;
            this.xniewmatb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xniewmatb, "Voeg toe als nieuw materiaal");
            this.xniewmatb.UseVisualStyleBackColor = true;
            this.xniewmatb.Click += new System.EventHandler(this.xniewmatb_Click);
            // 
            // xwijzigmatb
            // 
            this.xwijzigmatb.Enabled = false;
            this.xwijzigmatb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xwijzigmatb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xwijzigmatb.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xwijzigmatb.Location = new System.Drawing.Point(290, 270);
            this.xwijzigmatb.Name = "xwijzigmatb";
            this.xwijzigmatb.Size = new System.Drawing.Size(40, 40);
            this.xwijzigmatb.TabIndex = 12;
            this.xwijzigmatb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xwijzigmatb, "Wijzig geselecteerde materiaal");
            this.xwijzigmatb.UseVisualStyleBackColor = true;
            this.xwijzigmatb.Click += new System.EventHandler(this.xwijzigmatb_Click);
            // 
            // xverbruiktlabel
            // 
            this.xverbruiktlabel.AutoSize = true;
            this.xverbruiktlabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xverbruiktlabel.Location = new System.Drawing.Point(200, 209);
            this.xverbruiktlabel.Name = "xverbruiktlabel";
            this.xverbruiktlabel.Size = new System.Drawing.Size(74, 21);
            this.xverbruiktlabel.TabIndex = 11;
            this.xverbruiktlabel.Text = "Verbruikt";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 209);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(114, 21);
            this.label7.TabIndex = 9;
            this.label7.Text = "Aantal Per Stuk";
            // 
            // xaantalperstuk
            // 
            this.xaantalperstuk.DecimalPlaces = 4;
            this.xaantalperstuk.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantalperstuk.Location = new System.Drawing.Point(3, 233);
            this.xaantalperstuk.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantalperstuk.Name = "xaantalperstuk";
            this.xaantalperstuk.Size = new System.Drawing.Size(120, 25);
            this.xaantalperstuk.TabIndex = 8;
            this.xaantalperstuk.ValueChanged += new System.EventHandler(this.xaantalperstuk_ValueChanged);
            // 
            // xeenheid
            // 
            this.xeenheid.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.xeenheid.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.xeenheid.DisplayMember = "1";
            this.xeenheid.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xeenheid.FormattingEnabled = true;
            this.xeenheid.Items.AddRange(new object[] {
            "m (Meter)",
            "Stuks"});
            this.xeenheid.Location = new System.Drawing.Point(203, 181);
            this.xeenheid.Name = "xeenheid";
            this.xeenheid.Size = new System.Drawing.Size(121, 25);
            this.xeenheid.TabIndex = 7;
            this.xeenheid.SelectedIndexChanged += new System.EventHandler(this.xeenheid_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(200, 157);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 21);
            this.label6.TabIndex = 6;
            this.label6.Text = "Eenheid";
            // 
            // xlocatie
            // 
            this.xlocatie.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlocatie.Location = new System.Drawing.Point(3, 181);
            this.xlocatie.Name = "xlocatie";
            this.xlocatie.Size = new System.Drawing.Size(120, 25);
            this.xlocatie.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(59, 21);
            this.label5.TabIndex = 4;
            this.label5.Text = "Locatie";
            // 
            // xartikelnr
            // 
            this.xartikelnr.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xartikelnr.Location = new System.Drawing.Point(3, 129);
            this.xartikelnr.Name = "xartikelnr";
            this.xartikelnr.Size = new System.Drawing.Size(327, 25);
            this.xartikelnr.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 105);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(122, 21);
            this.label4.TabIndex = 2;
            this.label4.Text = "Artikel Nummer";
            // 
            // xomschrijving
            // 
            this.xomschrijving.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xomschrijving.Location = new System.Drawing.Point(3, 46);
            this.xomschrijving.Multiline = true;
            this.xomschrijving.Name = "xomschrijving";
            this.xomschrijving.Size = new System.Drawing.Size(324, 59);
            this.xomschrijving.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Top;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(103, 21);
            this.label3.TabIndex = 0;
            this.label3.Text = "Omschrijving";
            // 
            // xaantalklaarzetlabel
            // 
            this.xaantalklaarzetlabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantalklaarzetlabel.Location = new System.Drawing.Point(0, 375);
            this.xaantalklaarzetlabel.Name = "xaantalklaarzetlabel";
            this.xaantalklaarzetlabel.Size = new System.Drawing.Size(330, 35);
            this.xaantalklaarzetlabel.TabIndex = 3;
            this.xaantalklaarzetlabel.Text = "Aantal Klaarzetten: ";
            this.xaantalklaarzetlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xklaarzetpanel
            // 
            this.xklaarzetpanel.Controls.Add(this.xklaarzetimage);
            this.xklaarzetpanel.Controls.Add(this.xklaarzetlabel);
            this.xklaarzetpanel.Location = new System.Drawing.Point(3, 327);
            this.xklaarzetpanel.Name = "xklaarzetpanel";
            this.xklaarzetpanel.Size = new System.Drawing.Size(324, 45);
            this.xklaarzetpanel.TabIndex = 1;
            this.xklaarzetpanel.MouseEnter += new System.EventHandler(this.xklaarzetimage_MouseEnter);
            this.xklaarzetpanel.MouseLeave += new System.EventHandler(this.xklaarzetimage_MouseLeave);
            // 
            // xklaarzetimage
            // 
            this.xklaarzetimage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.xklaarzetimage.Location = new System.Drawing.Point(267, 6);
            this.xklaarzetimage.Name = "xklaarzetimage";
            this.xklaarzetimage.Size = new System.Drawing.Size(36, 36);
            this.xklaarzetimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.xklaarzetimage.TabIndex = 0;
            this.xklaarzetimage.TabStop = false;
            this.xklaarzetimage.Click += new System.EventHandler(this.xklaarzetimage_Click);
            this.xklaarzetimage.MouseEnter += new System.EventHandler(this.xklaarzetimage_MouseEnter);
            this.xklaarzetimage.MouseLeave += new System.EventHandler(this.xklaarzetimage_MouseLeave);
            // 
            // xklaarzetlabel
            // 
            this.xklaarzetlabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.xklaarzetlabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xklaarzetlabel.Location = new System.Drawing.Point(0, 0);
            this.xklaarzetlabel.Name = "xklaarzetlabel";
            this.xklaarzetlabel.Size = new System.Drawing.Size(262, 45);
            this.xklaarzetlabel.TabIndex = 1;
            this.xklaarzetlabel.Text = "Klaar Gezet";
            this.xklaarzetlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xklaarzetlabel.Click += new System.EventHandler(this.xklaarzetimage_Click);
            this.xklaarzetlabel.MouseEnter += new System.EventHandler(this.xklaarzetimage_MouseEnter);
            this.xklaarzetlabel.MouseLeave += new System.EventHandler(this.xklaarzetimage_MouseLeave);
            // 
            // xlocatielabel
            // 
            this.xlocatielabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlocatielabel.Location = new System.Drawing.Point(0, 410);
            this.xlocatielabel.Name = "xlocatielabel";
            this.xlocatielabel.Size = new System.Drawing.Size(330, 35);
            this.xlocatielabel.TabIndex = 2;
            this.xlocatielabel.Text = "Op Locatie: ";
            this.xlocatielabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xverwijderb
            // 
            this.xverwijderb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xverwijderb.Enabled = false;
            this.xverwijderb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xverwijderb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xverwijderb.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xverwijderb.Location = new System.Drawing.Point(736, 25);
            this.xverwijderb.Name = "xverwijderb";
            this.xverwijderb.Size = new System.Drawing.Size(40, 40);
            this.xverwijderb.TabIndex = 0;
            this.xverwijderb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xverwijderb, "Verwijder geselcteerde materialen");
            this.xverwijderb.UseVisualStyleBackColor = true;
            this.xverwijderb.Click += new System.EventHandler(this.xverwijderb_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.xstatus);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 483);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(782, 20);
            this.panel3.TabIndex = 8;
            // 
            // xstatus
            // 
            this.xstatus.BackColor = System.Drawing.Color.White;
            this.xstatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xstatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatus.ForeColor = System.Drawing.Color.Black;
            this.xstatus.Location = new System.Drawing.Point(0, 0);
            this.xstatus.Name = "xstatus";
            this.xstatus.Size = new System.Drawing.Size(782, 20);
            this.xstatus.TabIndex = 0;
            this.xstatus.Text = "Status";
            this.xstatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MateriaalUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel3);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "MateriaalUI";
            this.Size = new System.Drawing.Size(782, 503);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xmateriaallijst)).EndInit();
            this.panel1.ResumeLayout(false);
            this.xmateriaalpanel.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xaantal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xafkeurvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalperstuk)).EndInit();
            this.xklaarzetpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xklaarzetimage)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private BrightIdeasSoftware.ObjectListView xmateriaallijst;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel xmateriaalpanel;
        private System.Windows.Forms.PictureBox xklaarzetimage;
        private System.Windows.Forms.Panel xklaarzetpanel;
        private System.Windows.Forms.Label xklaarzetlabel;
        private System.Windows.Forms.Label xaantalklaarzetlabel;
        private System.Windows.Forms.Label xlocatielabel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox xomschrijving;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox xeenheid;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox xlocatie;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox xartikelnr;
        private System.Windows.Forms.Button xverwijderb;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button xniewmatb;
        private System.Windows.Forms.Button xwijzigmatb;
        private System.Windows.Forms.Label xverbruiktlabel;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown xaantalperstuk;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label xstatus;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown xafkeurvalue;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label xafkeurpercent;
        private System.Windows.Forms.NumericUpDown xaantal;
    }
}
