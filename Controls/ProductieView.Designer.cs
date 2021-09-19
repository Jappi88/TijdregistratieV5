using System.Windows.Forms;
using MetroFramework.Controls;
using Various;

namespace Controls
{
    partial class ProductieView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductieView));
            Various.MenuButton menuButton34 = new Various.MenuButton();
            Various.MenuButton menuButton35 = new Various.MenuButton();
            Various.MenuButton menuButton36 = new Various.MenuButton();
            Various.MenuButton menuButton37 = new Various.MenuButton();
            Various.MenuButton menuButton38 = new Various.MenuButton();
            Various.MenuButton menuButton39 = new Various.MenuButton();
            Various.MenuButton menuButton40 = new Various.MenuButton();
            Various.MenuButton menuButton41 = new Various.MenuButton();
            Various.MenuButton menuButton42 = new Various.MenuButton();
            Various.MenuButton menuButton43 = new Various.MenuButton();
            Various.MenuButton menuButton44 = new Various.MenuButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.xspeciaalroosterbutton = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.metroTabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.xproductieListControl1 = new Controls.ProductieListControl();
            this.tabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.xbewerkingListControl = new Controls.ProductieListControl();
            this.tabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.werkPlekkenUI1 = new Controls.WerkPlekkenUI();
            this.tabPage4 = new MetroFramework.Controls.MetroTabPage();
            this.recentGereedMeldingenUI1 = new Controls.RecentGereedMeldingenUI();
            this.xspeciaalroosterlabel = new System.Windows.Forms.Panel();
            this.xtabimages = new System.Windows.Forms.ImageList(this.components);
            this.takenManager1 = new Controls.TakenManager();
            this.mainMenu1 = new Controls.MainMenu();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.xbehpersoneel = new System.Windows.Forms.ToolStripButton();
            this.xbehvaardigheden = new System.Windows.Forms.ToolStripButton();
            this.xonderbrekeningen = new System.Windows.Forms.ToolStripButton();
            this.xdbbewerkingen = new System.Windows.Forms.ToolStripButton();
            this.xlogbook = new System.Windows.Forms.ToolStripButton();
            this.xoverzicht = new System.Windows.Forms.ToolStripButton();
            this.xmateriaalverbruikb = new System.Windows.Forms.ToolStripButton();
            this.xsendemail = new System.Windows.Forms.ToolStripButton();
            this.xallenotities = new System.Windows.Forms.ToolStripButton();
            this.xchatformbutton = new System.Windows.Forms.ToolStripButton();
            this.xproductieoverzichtb = new System.Windows.Forms.ToolStripButton();
            this.xsearchprodnr = new System.Windows.Forms.ToolStripButton();
            this.xopennewlijst = new System.Windows.Forms.ToolStripButton();
            this.xtoonartikels = new System.Windows.Forms.ToolStripButton();
            this.xaboutb = new System.Windows.Forms.ToolStripButton();
            this.xloginb = new System.Windows.Forms.ToolStripButton();
            this.xsettingsb = new System.Windows.Forms.ToolStripButton();
            this.xupdateb = new System.Windows.Forms.ToolStripButton();
            this.xupdateallform = new System.Windows.Forms.ToolStripButton();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.metroTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.xspeciaalroosterlabel.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Nieuwe Productie";
            this.toolTip1.SetToolTip(this.button1, "Maak nieuwe productie aan");
            this.button1.UseVisualStyleBackColor = true;
            // 
            // xspeciaalroosterbutton
            // 
            this.xspeciaalroosterbutton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xspeciaalroosterbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xspeciaalroosterbutton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xspeciaalroosterbutton.ForeColor = System.Drawing.Color.DarkRed;
            this.xspeciaalroosterbutton.Image = global::ProductieManager.Properties.Resources.schedule_32_32;
            this.xspeciaalroosterbutton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xspeciaalroosterbutton.Location = new System.Drawing.Point(3, 7);
            this.xspeciaalroosterbutton.Name = "xspeciaalroosterbutton";
            this.xspeciaalroosterbutton.Size = new System.Drawing.Size(1141, 40);
            this.xspeciaalroosterbutton.TabIndex = 0;
            this.xspeciaalroosterbutton.Text = "Rooster is momenteel inactief en zal geen tijd worden gemeten";
            this.xspeciaalroosterbutton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xspeciaalroosterbutton, "Wijzig rooster");
            this.xspeciaalroosterbutton.UseVisualStyleBackColor = true;
            this.xspeciaalroosterbutton.Click += new System.EventHandler(this.xspeciaalroosterbutton_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.toolStrip2);
            this.panel6.Controls.Add(this.pictureBox2);
            this.panel6.Controls.Add(this.pictureBox1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.panel6.Size = new System.Drawing.Size(1147, 43);
            this.panel6.TabIndex = 25;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1011, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(68, 43);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1079, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(68, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // metroTabControl
            // 
            this.metroTabControl.AllowDrop = true;
            this.metroTabControl.Controls.Add(this.tabPage1);
            this.metroTabControl.Controls.Add(this.tabPage2);
            this.metroTabControl.Controls.Add(this.tabPage3);
            this.metroTabControl.Controls.Add(this.tabPage4);
            this.metroTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl.Location = new System.Drawing.Point(40, 97);
            this.metroTabControl.Name = "metroTabControl";
            this.metroTabControl.SelectedIndex = 0;
            this.metroTabControl.ShowToolTips = true;
            this.metroTabControl.Size = new System.Drawing.Size(1069, 462);
            this.metroTabControl.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTabControl.TabIndex = 28;
            this.metroTabControl.UseSelectable = true;
            this.metroTabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.xproductieListControl1);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.HorizontalScrollbar = true;
            this.tabPage1.HorizontalScrollbarBarColor = true;
            this.tabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPage1.HorizontalScrollbarSize = 10;
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1061, 420);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Producties";
            this.tabPage1.VerticalScrollbar = true;
            this.tabPage1.VerticalScrollbarBarColor = true;
            this.tabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.tabPage1.VerticalScrollbarSize = 10;
            // 
            // xproductieListControl1
            // 
            this.xproductieListControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xproductieListControl1.BackColor = System.Drawing.Color.White;
            this.xproductieListControl1.CanLoad = true;
            this.xproductieListControl1.EnableEntryFiltering = false;
            this.xproductieListControl1.EnableFiltering = true;
            this.xproductieListControl1.EnableSync = true;
            this.xproductieListControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xproductieListControl1.IsBewerkingView = false;
            this.xproductieListControl1.ListName = "Producties";
            this.xproductieListControl1.Location = new System.Drawing.Point(3, 4);
            this.xproductieListControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xproductieListControl1.Name = "xproductieListControl1";
            this.xproductieListControl1.RemoveCustomItemIfNotValid = false;
            this.xproductieListControl1.SelectedItem = null;
            this.xproductieListControl1.Size = new System.Drawing.Size(1055, 412);
            this.xproductieListControl1.TabIndex = 2;
            this.xproductieListControl1.ValidHandler = null;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.xbewerkingListControl);
            this.tabPage2.HorizontalScrollbarBarColor = true;
            this.tabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPage2.HorizontalScrollbarSize = 10;
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1061, 420);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Bewerkingen";
            this.tabPage2.VerticalScrollbarBarColor = true;
            this.tabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.tabPage2.VerticalScrollbarSize = 10;
            // 
            // xbewerkingListControl
            // 
            this.xbewerkingListControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xbewerkingListControl.BackColor = System.Drawing.Color.White;
            this.xbewerkingListControl.CanLoad = true;
            this.xbewerkingListControl.EnableEntryFiltering = false;
            this.xbewerkingListControl.EnableFiltering = true;
            this.xbewerkingListControl.EnableSync = true;
            this.xbewerkingListControl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbewerkingListControl.IsBewerkingView = false;
            this.xbewerkingListControl.ListName = "Bewerkingen";
            this.xbewerkingListControl.Location = new System.Drawing.Point(3, 4);
            this.xbewerkingListControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xbewerkingListControl.Name = "xbewerkingListControl";
            this.xbewerkingListControl.RemoveCustomItemIfNotValid = false;
            this.xbewerkingListControl.SelectedItem = null;
            this.xbewerkingListControl.Size = new System.Drawing.Size(1055, 412);
            this.xbewerkingListControl.TabIndex = 2;
            this.xbewerkingListControl.ValidHandler = null;
            // 
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage3.Controls.Add(this.werkPlekkenUI1);
            this.tabPage3.HorizontalScrollbarBarColor = true;
            this.tabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPage3.HorizontalScrollbarSize = 10;
            this.tabPage3.Location = new System.Drawing.Point(4, 38);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1061, 420);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Werk Plaatsen";
            this.tabPage3.VerticalScrollbarBarColor = true;
            this.tabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.tabPage3.VerticalScrollbarSize = 10;
            // 
            // werkPlekkenUI1
            // 
            this.werkPlekkenUI1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.werkPlekkenUI1.BackColor = System.Drawing.Color.White;
            this.werkPlekkenUI1.EnableSync = true;
            this.werkPlekkenUI1.Location = new System.Drawing.Point(3, 3);
            this.werkPlekkenUI1.Name = "werkPlekkenUI1";
            this.werkPlekkenUI1.SelectedWerkplek = null;
            this.werkPlekkenUI1.Size = new System.Drawing.Size(1051, 410);
            this.werkPlekkenUI1.TabIndex = 0;
            this.werkPlekkenUI1.WerkPlekClicked += new System.EventHandler(this.werkPlekkenUI1_WerkPlekClicked);
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.recentGereedMeldingenUI1);
            this.tabPage4.HorizontalScrollbarBarColor = true;
            this.tabPage4.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPage4.HorizontalScrollbarSize = 10;
            this.tabPage4.Location = new System.Drawing.Point(4, 38);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1061, 420);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Recente Gereedmeldingen";
            this.tabPage4.VerticalScrollbarBarColor = true;
            this.tabPage4.VerticalScrollbarHighlightOnWheel = false;
            this.tabPage4.VerticalScrollbarSize = 10;
            // 
            // recentGereedMeldingenUI1
            // 
            this.recentGereedMeldingenUI1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recentGereedMeldingenUI1.BackColor = System.Drawing.Color.White;
            this.recentGereedMeldingenUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recentGereedMeldingenUI1.Location = new System.Drawing.Point(2, 4);
            this.recentGereedMeldingenUI1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.recentGereedMeldingenUI1.Name = "recentGereedMeldingenUI1";
            this.recentGereedMeldingenUI1.Size = new System.Drawing.Size(1056, 420);
            this.recentGereedMeldingenUI1.TabIndex = 2;
            // 
            // xspeciaalroosterlabel
            // 
            this.xspeciaalroosterlabel.Controls.Add(this.xspeciaalroosterbutton);
            this.xspeciaalroosterlabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xspeciaalroosterlabel.Location = new System.Drawing.Point(0, 43);
            this.xspeciaalroosterlabel.Name = "xspeciaalroosterlabel";
            this.xspeciaalroosterlabel.Size = new System.Drawing.Size(1147, 54);
            this.xspeciaalroosterlabel.TabIndex = 29;
            this.xspeciaalroosterlabel.Visible = false;
            // 
            // xtabimages
            // 
            this.xtabimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("xtabimages.ImageStream")));
            this.xtabimages.TransparentColor = System.Drawing.Color.Transparent;
            this.xtabimages.Images.SetKeyName(0, "page_document_16748_128_128.png");
            this.xtabimages.Images.SetKeyName(1, "operation.png");
            this.xtabimages.Images.SetKeyName(2, "iconfinder-technologymachineelectronic32_32.png");
            this.xtabimages.Images.SetKeyName(3, "ic_done_all_128_28243.png");
            // 
            // takenManager1
            // 
            this.takenManager1.BackColor = System.Drawing.Color.White;
            this.takenManager1.Dock = System.Windows.Forms.DockStyle.Right;
            this.takenManager1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.takenManager1.Location = new System.Drawing.Point(1109, 97);
            this.takenManager1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.takenManager1.Name = "takenManager1";
            this.takenManager1.SelectedItem = null;
            this.takenManager1.Size = new System.Drawing.Size(38, 462);
            this.takenManager1.TabIndex = 26;
            this.takenManager1.Visible = false;
            this.takenManager1.OnTaakClicked += new Rpm.Various.TaakHandler(this.takenManager1_OnTaakClicked);
            this.takenManager1.OnTaakUitvoeren += new Rpm.Various.TaakHandler(this.takenManager1_OnTaakUitvoeren);
            // 
            // mainMenu1
            // 
            this.mainMenu1.Dock = System.Windows.Forms.DockStyle.Left;
            this.mainMenu1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainMenu1.IsExpanded = false;
            this.mainMenu1.Location = new System.Drawing.Point(0, 97);
            this.mainMenu1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            menuButton34.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton34.CombineImage = null;
            menuButton34.CombineScale = 0D;
            menuButton34.ContextMenu = null;
            menuButton34.Enabled = true;
            menuButton34.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton34.Image")));
            menuButton34.ImageSize = new System.Drawing.Size(32, 32);
            menuButton34.Index = 0;
            menuButton34.Name = "xniewproductie";
            menuButton34.Text = "Nieuwe Productie";
            menuButton34.Tooltip = "Maak een nieuwe productie aan";
            menuButton35.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton35.CombineImage = null;
            menuButton35.CombineScale = 1.5D;
            menuButton35.ContextMenu = null;
            menuButton35.Enabled = true;
            menuButton35.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton35.Image")));
            menuButton35.ImageSize = new System.Drawing.Size(32, 32);
            menuButton35.Index = 1;
            menuButton35.Name = "xopenproductie";
            menuButton35.Text = "Open Productie";
            menuButton35.Tooltip = "Open productie vanuit een pdf";
            menuButton36.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton36.CombineImage = global::ProductieManager.Properties.Resources.lightning_weather_storm_2781;
            menuButton36.CombineScale = 1.25D;
            menuButton36.ContextMenu = null;
            menuButton36.Enabled = true;
            menuButton36.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton36.Image")));
            menuButton36.ImageSize = new System.Drawing.Size(32, 32);
            menuButton36.Index = 2;
            menuButton36.Name = "xquickproductie";
            menuButton36.Text = "Simpel Productie";
            menuButton36.Tooltip = "Maak een nieuwe simpele productie";
            menuButton37.AccesLevel = Rpm.Various.AccesType.AlleenKijken;
            menuButton37.CombineImage = null;
            menuButton37.CombineScale = 1.5D;
            menuButton37.ContextMenu = null;
            menuButton37.Enabled = true;
            menuButton37.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton37.Image")));
            menuButton37.ImageSize = new System.Drawing.Size(32, 32);
            menuButton37.Index = 3;
            menuButton37.Name = "xcreateexcel";
            menuButton37.Text = "Excel Overzicht";
            menuButton37.Tooltip = "Maak excel overzicht";
            menuButton38.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton38.CombineImage = null;
            menuButton38.CombineScale = 1.5D;
            menuButton38.ContextMenu = null;
            menuButton38.Enabled = true;
            menuButton38.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton38.Image")));
            menuButton38.ImageSize = new System.Drawing.Size(32, 32);
            menuButton38.Index = 4;
            menuButton38.Name = "xupdatedb";
            menuButton38.Text = "Update Database";
            menuButton38.Tooltip = "Update database vanuit adere locaties";
            menuButton39.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton39.CombineImage = null;
            menuButton39.CombineScale = 1.5D;
            menuButton39.ContextMenu = null;
            menuButton39.Enabled = true;
            menuButton39.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton39.Image")));
            menuButton39.ImageSize = new System.Drawing.Size(32, 32);
            menuButton39.Index = 5;
            menuButton39.Name = "xlaaddb";
            menuButton39.Text = "Laad Database";
            menuButton39.Tooltip = "Laad een andere database";
            menuButton40.AccesLevel = Rpm.Various.AccesType.AlleenKijken;
            menuButton40.CombineImage = null;
            menuButton40.CombineScale = 1.5D;
            menuButton40.ContextMenu = null;
            menuButton40.Enabled = true;
            menuButton40.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton40.Image")));
            menuButton40.ImageSize = new System.Drawing.Size(32, 32);
            menuButton40.Index = 6;
            menuButton40.Name = "xstats";
            menuButton40.Text = "Toon Statistieken";
            menuButton40.Tooltip = "Toon statistieken van de afgelopen periode";
            menuButton41.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton41.CombineImage = global::ProductieManager.Properties.Resources.lightning_weather_storm_2781;
            menuButton41.CombineScale = 1.25D;
            menuButton41.ContextMenu = null;
            menuButton41.Enabled = true;
            menuButton41.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton41.Image")));
            menuButton41.ImageSize = new System.Drawing.Size(32, 32);
            menuButton41.Index = 7;
            menuButton41.Name = "xstoringmenubutton";
            menuButton41.Text = "Onderbreking";
            menuButton41.Tooltip = "Maak/Wijzig onderbreking";
            menuButton42.AccesLevel = Rpm.Various.AccesType.AlleenKijken;
            menuButton42.CombineImage = null;
            menuButton42.CombineScale = 1.25D;
            menuButton42.ContextMenu = null;
            menuButton42.Enabled = false;
            menuButton42.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton42.Image")));
            menuButton42.ImageSize = new System.Drawing.Size(32, 32);
            menuButton42.Index = 8;
            menuButton42.Name = "xbekijkproductiepdf";
            menuButton42.Text = "Bekijk Productieformulier";
            menuButton42.Tooltip = "Open productieformulier pdf";
            menuButton43.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton43.CombineImage = null;
            menuButton43.CombineScale = 1.5D;
            menuButton43.ContextMenu = null;
            menuButton43.Enabled = true;
            menuButton43.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton43.Image")));
            menuButton43.ImageSize = new System.Drawing.Size(32, 32);
            menuButton43.Index = 9;
            menuButton43.Name = "xroostermenubutton";
            menuButton43.Text = "Eigen Rooster";
            menuButton43.Tooltip = "Kies hier je eigen rooster voor elke periode";
            menuButton44.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton44.CombineImage = global::ProductieManager.Properties.Resources.play_button_icon_icons_com_60615;
            menuButton44.CombineScale = 1.5D;
            menuButton44.ContextMenu = null;
            menuButton44.Enabled = true;
            menuButton44.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton44.Image")));
            menuButton44.ImageSize = new System.Drawing.Size(32, 32);
            menuButton44.Index = 10;
            menuButton44.Name = "xopenproducties";
            menuButton44.Text = "Gestart Producties";
            menuButton44.Tooltip = "Open alle gestarte producties";
            this.mainMenu1.MenuButtons = new Various.MenuButton[] {
        menuButton34,
        menuButton35,
        menuButton36,
        menuButton37,
        menuButton38,
        menuButton39,
        menuButton40,
        menuButton41,
        menuButton42,
        menuButton43,
        menuButton44};
            this.mainMenu1.Name = "mainMenu1";
            this.mainMenu1.Size = new System.Drawing.Size(40, 462);
            this.mainMenu1.TabIndex = 27;
            this.mainMenu1.OnMenuClick += new System.EventHandler(this.mainMenu1_OnMenuClick);
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.White;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xbehpersoneel,
            this.xbehvaardigheden,
            this.xonderbrekeningen,
            this.xdbbewerkingen,
            this.xlogbook,
            this.xoverzicht,
            this.xmateriaalverbruikb,
            this.xupdateallform,
            this.xsendemail,
            this.xallenotities,
            this.xchatformbutton,
            this.xproductieoverzichtb,
            this.xsearchprodnr,
            this.xopennewlijst,
            this.xtoonartikels,
            this.xaboutb,
            this.xloginb,
            this.xsettingsb,
            this.xupdateb});
            this.toolStrip2.Location = new System.Drawing.Point(5, 0);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(1006, 39);
            this.toolStrip2.TabIndex = 46;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // xbehpersoneel
            // 
            this.xbehpersoneel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xbehpersoneel.Image = global::ProductieManager.Properties.Resources.users_12820;
            this.xbehpersoneel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xbehpersoneel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xbehpersoneel.Name = "xbehpersoneel";
            this.xbehpersoneel.Size = new System.Drawing.Size(36, 36);
            this.xbehpersoneel.ToolTipText = "Toon alle medewerkers";
            this.xbehpersoneel.Click += new System.EventHandler(this.xpersoneelb_Click);
            // 
            // xbehvaardigheden
            // 
            this.xbehvaardigheden.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xbehvaardigheden.Image = global::ProductieManager.Properties.Resources.key_skills;
            this.xbehvaardigheden.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xbehvaardigheden.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xbehvaardigheden.Name = "xbehvaardigheden";
            this.xbehvaardigheden.Size = new System.Drawing.Size(36, 36);
            this.xbehvaardigheden.ToolTipText = "Toon alle vaardigheden";
            this.xbehvaardigheden.Click += new System.EventHandler(this.xallevaardighedenb_Click);
            // 
            // xonderbrekeningen
            // 
            this.xonderbrekeningen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xonderbrekeningen.Image = global::ProductieManager.Properties.Resources.onderhoud32_321;
            this.xonderbrekeningen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xonderbrekeningen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xonderbrekeningen.Name = "xonderbrekeningen";
            this.xonderbrekeningen.Size = new System.Drawing.Size(36, 36);
            this.xonderbrekeningen.ToolTipText = "Toon alle onderbrekeningen";
            this.xonderbrekeningen.Click += new System.EventHandler(this.xallstoringenb_Click);
            // 
            // xdbbewerkingen
            // 
            this.xdbbewerkingen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xdbbewerkingen.Image = global::ProductieManager.Properties.Resources.list_992_32_32;
            this.xdbbewerkingen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xdbbewerkingen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xdbbewerkingen.Name = "xdbbewerkingen";
            this.xdbbewerkingen.Size = new System.Drawing.Size(36, 36);
            this.xdbbewerkingen.ToolTipText = "Beheer Bewerkingen en de werkplaatsen";
            this.xdbbewerkingen.Click += new System.EventHandler(this.xdbbewerkingen_Click);
            // 
            // xlogbook
            // 
            this.xlogbook.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xlogbook.Image = global::ProductieManager.Properties.Resources.activitylogmanager_104624;
            this.xlogbook.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xlogbook.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xlogbook.Name = "xlogbook";
            this.xlogbook.Size = new System.Drawing.Size(36, 36);
            this.xlogbook.ToolTipText = "Toon alle logs";
            this.xlogbook.Click += new System.EventHandler(this.xtoonlogsb_Click);
            // 
            // xoverzicht
            // 
            this.xoverzicht.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xoverzicht.Image = global::ProductieManager.Properties.Resources.FocusEye_Img_32_32;
            this.xoverzicht.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xoverzicht.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xoverzicht.Name = "xoverzicht";
            this.xoverzicht.Size = new System.Drawing.Size(36, 36);
            this.xoverzicht.ToolTipText = "Zoek producties op basis van criteria\'s";
            this.xoverzicht.Click += new System.EventHandler(this.xprodinfob_Click);
            // 
            // xmateriaalverbruikb
            // 
            this.xmateriaalverbruikb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xmateriaalverbruikb.Image = global::ProductieManager.Properties.Resources.graph_9_icon_icons_com_58019_32x32;
            this.xmateriaalverbruikb.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xmateriaalverbruikb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xmateriaalverbruikb.Name = "xmateriaalverbruikb";
            this.xmateriaalverbruikb.Size = new System.Drawing.Size(36, 36);
            this.xmateriaalverbruikb.ToolTipText = "Toon alle verbruikte materialen";
            this.xmateriaalverbruikb.Click += new System.EventHandler(this.xmateriaalverbruikb_Click);
            // 
            // xsendemail
            // 
            this.xsendemail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xsendemail.Image = global::ProductieManager.Properties.Resources.email_18961;
            this.xsendemail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xsendemail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xsendemail.Name = "xsendemail";
            this.xsendemail.Size = new System.Drawing.Size(36, 36);
            this.xsendemail.ToolTipText = "Verzend een email";
            this.xsendemail.Click += new System.EventHandler(this.xsendemail_Click);
            // 
            // xallenotities
            // 
            this.xallenotities.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xallenotities.Image = global::ProductieManager.Properties.Resources.education_school_memo_pad_notes_reminder_task_icon_133450;
            this.xallenotities.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xallenotities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xallenotities.Name = "xallenotities";
            this.xallenotities.Size = new System.Drawing.Size(36, 36);
            this.xallenotities.ToolTipText = "Beheer alle notities";
            this.xallenotities.Click += new System.EventHandler(this.xallenotities_Click);
            // 
            // xchatformbutton
            // 
            this.xchatformbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xchatformbutton.Image = global::ProductieManager.Properties.Resources.conversation_chat_32x321;
            this.xchatformbutton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xchatformbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xchatformbutton.Name = "xchatformbutton";
            this.xchatformbutton.Size = new System.Drawing.Size(36, 36);
            this.xchatformbutton.ToolTipText = "Open ProductieChat";
            this.xchatformbutton.Click += new System.EventHandler(this.xchatformbutton_Click);
            // 
            // xproductieoverzichtb
            // 
            this.xproductieoverzichtb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xproductieoverzichtb.Image = global::ProductieManager.Properties.Resources.list_icon_icons_com_60651;
            this.xproductieoverzichtb.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xproductieoverzichtb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xproductieoverzichtb.Name = "xproductieoverzichtb";
            this.xproductieoverzichtb.Size = new System.Drawing.Size(36, 36);
            this.xproductieoverzichtb.ToolTipText = "Bekijk een ProductieOverzicht en de volgorde van het produceren";
            this.xproductieoverzichtb.Click += new System.EventHandler(this.xproductieoverzichtb_Click);
            // 
            // xsearchprodnr
            // 
            this.xsearchprodnr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xsearchprodnr.Image = global::ProductieManager.Properties.Resources.search_page_document_32x32;
            this.xsearchprodnr.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xsearchprodnr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xsearchprodnr.Name = "xsearchprodnr";
            this.xsearchprodnr.Size = new System.Drawing.Size(36, 36);
            this.xsearchprodnr.ToolTipText = "Zoek ProductieNr";
            this.xsearchprodnr.Click += new System.EventHandler(this.xsearchprodnr_Click);
            // 
            // xopennewlijst
            // 
            this.xopennewlijst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xopennewlijst.Image = global::ProductieManager.Properties.Resources.view_screen_32x32;
            this.xopennewlijst.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xopennewlijst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xopennewlijst.Name = "xopennewlijst";
            this.xopennewlijst.Size = new System.Drawing.Size(36, 36);
            this.xopennewlijst.ToolTipText = "Open een nieuw ProductieLijst venster";
            this.xopennewlijst.Click += new System.EventHandler(this.xopennewlijst_Click);
            // 
            // xtoonartikels
            // 
            this.xtoonartikels.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xtoonartikels.Image = global::ProductieManager.Properties.Resources.product_document_file_32x32;
            this.xtoonartikels.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xtoonartikels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xtoonartikels.Name = "xtoonartikels";
            this.xtoonartikels.Size = new System.Drawing.Size(36, 36);
            this.xtoonartikels.Text = "toolStripButton18";
            this.xtoonartikels.ToolTipText = "Toon alle artikelen";
            this.xtoonartikels.Click += new System.EventHandler(this.xtoonartikels_Click);
            // 
            // xaboutb
            // 
            this.xaboutb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xaboutb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xaboutb.Image = global::ProductieManager.Properties.Resources.info_15260;
            this.xaboutb.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xaboutb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xaboutb.Name = "xaboutb";
            this.xaboutb.Size = new System.Drawing.Size(36, 36);
            this.xaboutb.ToolTipText = "ProductieManager Info";
            this.xaboutb.Click += new System.EventHandler(this.xaboutb_Click);
            // 
            // xloginb
            // 
            this.xloginb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xloginb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xloginb.Image = global::ProductieManager.Properties.Resources.Login_37128__1_;
            this.xloginb.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xloginb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xloginb.Name = "xloginb";
            this.xloginb.Size = new System.Drawing.Size(36, 36);
            this.xloginb.ToolTipText = "Log In/Uit";
            this.xloginb.Click += new System.EventHandler(this.xloginb_Click);
            // 
            // xsettingsb
            // 
            this.xsettingsb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xsettingsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xsettingsb.Image = global::ProductieManager.Properties.Resources.ccsm_103993;
            this.xsettingsb.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xsettingsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xsettingsb.Name = "xsettingsb";
            this.xsettingsb.Size = new System.Drawing.Size(36, 36);
            this.xsettingsb.ToolTipText = "Beheer Instellingen";
            this.xsettingsb.Click += new System.EventHandler(this.xsettingsb_Click);
            // 
            // xupdateb
            // 
            this.xupdateb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xupdateb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xupdateb.Image = global::ProductieManager.Properties.Resources.clouddown_icon_icons_com_54405;
            this.xupdateb.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xupdateb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xupdateb.Name = "xupdateb";
            this.xupdateb.Size = new System.Drawing.Size(36, 36);
            this.xupdateb.ToolTipText = "Controleer voor een update";
            this.xupdateb.Click += new System.EventHandler(this.xUpdate_Click);
            // 
            // xupdateallform
            // 
            this.xupdateallform.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xupdateallform.Image = global::ProductieManager.Properties.Resources.task_update_folder_progress_icon_142270;
            this.xupdateallform.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xupdateallform.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xupdateallform.Name = "xupdateallform";
            this.xupdateallform.Size = new System.Drawing.Size(36, 36);
            this.xupdateallform.ToolTipText = "Update alle producties";
            this.xupdateallform.Click += new System.EventHandler(this.xupdateallform_Click);
            // 
            // ProductieView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.metroTabControl);
            this.Controls.Add(this.takenManager1);
            this.Controls.Add(this.mainMenu1);
            this.Controls.Add(this.xspeciaalroosterlabel);
            this.Controls.Add(this.panel6);
            this.DoubleBuffered = true;
            this.Name = "ProductieView";
            this.Size = new System.Drawing.Size(1147, 559);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.metroTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.xspeciaalroosterlabel.ResumeLayout(false);
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel6;
        private WerkPlekkenUI werkPlekkenUI1;
        private TakenManager takenManager1;
        private MainMenu mainMenu1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private MetroFramework.Controls.MetroTabControl metroTabControl;
        private MetroFramework.Controls.MetroTabPage tabPage1;
        private MetroFramework.Controls.MetroTabPage tabPage2;
        private MetroFramework.Controls.MetroTabPage tabPage3;
        private System.Windows.Forms.Panel xspeciaalroosterlabel;
        private System.Windows.Forms.Button xspeciaalroosterbutton;
        private ProductieListControl xproductieListControl1;
        private ProductieListControl xbewerkingListControl;
        private MetroTabPage tabPage4;
        private RecentGereedMeldingenUI recentGereedMeldingenUI1;
        private ImageList xtabimages;
        private ToolStrip toolStrip2;
        private ToolStripButton xbehpersoneel;
        private ToolStripButton xbehvaardigheden;
        private ToolStripButton xonderbrekeningen;
        private ToolStripButton xdbbewerkingen;
        private ToolStripButton xlogbook;
        private ToolStripButton xoverzicht;
        private ToolStripButton xmateriaalverbruikb;
        private ToolStripButton xupdateallform;
        private ToolStripButton xsendemail;
        private ToolStripButton xallenotities;
        private ToolStripButton xchatformbutton;
        private ToolStripButton xproductieoverzichtb;
        private ToolStripButton xsearchprodnr;
        private ToolStripButton xopennewlijst;
        private ToolStripButton xtoonartikels;
        private ToolStripButton xaboutb;
        private ToolStripButton xloginb;
        private ToolStripButton xsettingsb;
        private ToolStripButton xupdateb;
    }
}
