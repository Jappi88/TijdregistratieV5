﻿namespace Controls
{
    partial class ProductieForm
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
            Various.MenuButton menuButton1 = new Various.MenuButton();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductieForm));
            Various.MenuButton menuButton2 = new Various.MenuButton();
            Various.MenuButton menuButton3 = new Various.MenuButton();
            Various.MenuButton menuButton4 = new Various.MenuButton();
            Various.MenuButton menuButton5 = new Various.MenuButton();
            Various.MenuButton menuButton6 = new Various.MenuButton();
            Various.MenuButton menuButton7 = new Various.MenuButton();
            Various.MenuButton menuButton8 = new Various.MenuButton();
            Various.MenuButton menuButton9 = new Various.MenuButton();
            Various.MenuButton menuButton10 = new Various.MenuButton();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aantalTeMakenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.leverdatumToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xprodafkeurtoolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.notitieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.xbewerking = new MetroFramework.Controls.MetroComboBox();
            this.xprogressbar = new CircularProgressBar.CircularProgressBar();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xstatuslabel = new System.Windows.Forms.Label();
            this.xstopb = new System.Windows.Forms.Button();
            this.xstartb = new System.Windows.Forms.Button();
            this.xindelingb = new System.Windows.Forms.Button();
            this.xpanelcontainer = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.xstatusimage = new System.Windows.Forms.PictureBox();
            this.mainMenu1 = new Controls.MainMenu();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xstatusimage)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aantalTeMakenToolStripMenuItem,
            this.leverdatumToolStripMenuItem,
            this.xprodafkeurtoolstrip,
            this.notitieToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(178, 156);
            // 
            // aantalTeMakenToolStripMenuItem
            // 
            this.aantalTeMakenToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.hashtag_icon_152828_32_32;
            this.aantalTeMakenToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.aantalTeMakenToolStripMenuItem.Name = "aantalTeMakenToolStripMenuItem";
            this.aantalTeMakenToolStripMenuItem.Size = new System.Drawing.Size(177, 38);
            this.aantalTeMakenToolStripMenuItem.Text = "&Aantal Te Maken";
            this.aantalTeMakenToolStripMenuItem.Click += new System.EventHandler(this.aantalTeMakenToolStripMenuItem_Click);
            // 
            // leverdatumToolStripMenuItem
            // 
            this.leverdatumToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.systemtime_778_32_32;
            this.leverdatumToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.leverdatumToolStripMenuItem.Name = "leverdatumToolStripMenuItem";
            this.leverdatumToolStripMenuItem.Size = new System.Drawing.Size(177, 38);
            this.leverdatumToolStripMenuItem.Text = "&Leverdatum";
            this.leverdatumToolStripMenuItem.Click += new System.EventHandler(this.leverdatumToolStripMenuItem_Click);
            // 
            // xprodafkeurtoolstrip
            // 
            this.xprodafkeurtoolstrip.Image = global::ProductieManager.Properties.Resources.bin_icon_icons_com_32x32;
            this.xprodafkeurtoolstrip.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xprodafkeurtoolstrip.Name = "xprodafkeurtoolstrip";
            this.xprodafkeurtoolstrip.Size = new System.Drawing.Size(177, 38);
            this.xprodafkeurtoolstrip.Text = "Afkeur";
            this.xprodafkeurtoolstrip.ToolTipText = "Vul in product afkeur";
            this.xprodafkeurtoolstrip.Click += new System.EventHandler(this.xprodafkeurtoolstrip_Click);
            // 
            // notitieToolStripMenuItem
            // 
            this.notitieToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.Note_34576_32x32;
            this.notitieToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.notitieToolStripMenuItem.Name = "notitieToolStripMenuItem";
            this.notitieToolStripMenuItem.Size = new System.Drawing.Size(177, 38);
            this.notitieToolStripMenuItem.Text = "&Notitie";
            this.notitieToolStripMenuItem.Click += new System.EventHandler(this.notitieToolStripMenuItem_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.xbewerking);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(0, 0);
            this.groupBox3.Margin = new System.Windows.Forms.Padding(4);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Padding = new System.Windows.Forms.Padding(4);
            this.groupBox3.Size = new System.Drawing.Size(185, 53);
            this.groupBox3.TabIndex = 1;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Kies Bewerking";
            // 
            // xbewerking
            // 
            this.xbewerking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xbewerking.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbewerking.ItemHeight = 23;
            this.xbewerking.Location = new System.Drawing.Point(4, 23);
            this.xbewerking.Margin = new System.Windows.Forms.Padding(4);
            this.xbewerking.Name = "xbewerking";
            this.xbewerking.Size = new System.Drawing.Size(177, 29);
            this.xbewerking.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xbewerking, "Kies Bewerking");
            this.xbewerking.UseSelectable = true;
            // 
            // xprogressbar
            // 
            this.xprogressbar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.xprogressbar.AnimationSpeed = 500;
            this.xprogressbar.BackColor = System.Drawing.Color.Transparent;
            this.xprogressbar.ContextMenuStrip = this.contextMenuStrip1;
            this.xprogressbar.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprogressbar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xprogressbar.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.xprogressbar.InnerMargin = 5;
            this.xprogressbar.InnerWidth = 5;
            this.xprogressbar.Location = new System.Drawing.Point(14, 59);
            this.xprogressbar.Margin = new System.Windows.Forms.Padding(4);
            this.xprogressbar.MarqueeAnimationSpeed = 2000;
            this.xprogressbar.Name = "xprogressbar";
            this.xprogressbar.OuterColor = System.Drawing.Color.Gray;
            this.xprogressbar.OuterMargin = -28;
            this.xprogressbar.OuterWidth = 30;
            this.xprogressbar.ProgressColor = System.Drawing.Color.ForestGreen;
            this.xprogressbar.ProgressWidth = 20;
            this.xprogressbar.SecondaryFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprogressbar.Size = new System.Drawing.Size(159, 147);
            this.xprogressbar.StartAngle = 270;
            this.xprogressbar.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.xprogressbar.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.xprogressbar.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.xprogressbar.SubscriptText = "";
            this.xprogressbar.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.xprogressbar.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.xprogressbar.SuperscriptText = "";
            this.xprogressbar.TabIndex = 1;
            this.xprogressbar.Text = "68%";
            this.xprogressbar.TextMargin = new System.Windows.Forms.Padding(0);
            this.toolTip1.SetToolTip(this.xprogressbar, "Productie Voortgang");
            this.xprogressbar.Value = 68;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Productie";
            // 
            // xstatuslabel
            // 
            this.xstatuslabel.ContextMenuStrip = this.contextMenuStrip1;
            this.xstatuslabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xstatuslabel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xstatuslabel.Font = new System.Drawing.Font("Book Antiqua", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatuslabel.ForeColor = System.Drawing.Color.DarkGreen;
            this.xstatuslabel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xstatuslabel.Location = new System.Drawing.Point(40, 0);
            this.xstatuslabel.Name = "xstatuslabel";
            this.xstatuslabel.Padding = new System.Windows.Forms.Padding(5);
            this.xstatuslabel.Size = new System.Drawing.Size(644, 45);
            this.xstatuslabel.TabIndex = 0;
            this.xstatuslabel.Text = "Status";
            this.xstatuslabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.toolTip1.SetToolTip(this.xstatuslabel, "Productie Status");
            this.xstatuslabel.SizeChanged += new System.EventHandler(this.xstatuslabel_SizeChanged);
            // 
            // xstopb
            // 
            this.xstopb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xstopb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xstopb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstopb.ForeColor = System.Drawing.Color.Black;
            this.xstopb.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xstopb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xstopb.Location = new System.Drawing.Point(4, 364);
            this.xstopb.Margin = new System.Windows.Forms.Padding(4);
            this.xstopb.Name = "xstopb";
            this.xstopb.Size = new System.Drawing.Size(177, 40);
            this.xstopb.TabIndex = 1;
            this.xstopb.Text = "Sluiten";
            this.toolTip1.SetToolTip(this.xstopb, "Sluit productie pagina");
            this.xstopb.UseVisualStyleBackColor = true;
            this.xstopb.Click += new System.EventHandler(this.xstopb_Click);
            // 
            // xstartb
            // 
            this.xstartb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xstartb.ContextMenuStrip = this.contextMenuStrip1;
            this.xstartb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xstartb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstartb.ForeColor = System.Drawing.Color.Black;
            this.xstartb.Image = global::ProductieManager.Properties.Resources.play_button_icon_icons_com_60615;
            this.xstartb.Location = new System.Drawing.Point(4, 258);
            this.xstartb.Margin = new System.Windows.Forms.Padding(4);
            this.xstartb.Name = "xstartb";
            this.xstartb.Size = new System.Drawing.Size(177, 40);
            this.xstartb.TabIndex = 2;
            this.xstartb.Text = "Start";
            this.xstartb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xstartb, "Start of stop de bewerking");
            this.xstartb.UseVisualStyleBackColor = true;
            this.xstartb.Click += new System.EventHandler(this.xstartb_Click);
            // 
            // xindelingb
            // 
            this.xindelingb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xindelingb.ContextMenuStrip = this.contextMenuStrip1;
            this.xindelingb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xindelingb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xindelingb.ForeColor = System.Drawing.Color.Black;
            this.xindelingb.Image = global::ProductieManager.Properties.Resources.iconfinder_technologymachineelectronic32_32;
            this.xindelingb.Location = new System.Drawing.Point(5, 214);
            this.xindelingb.Margin = new System.Windows.Forms.Padding(4);
            this.xindelingb.Name = "xindelingb";
            this.xindelingb.Size = new System.Drawing.Size(177, 40);
            this.xindelingb.TabIndex = 3;
            this.xindelingb.Text = "Indeling";
            this.xindelingb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xindelingb, "Beheer de indeling");
            this.xindelingb.UseVisualStyleBackColor = true;
            this.xindelingb.Click += new System.EventHandler(this.xindelingb_Click);
            // 
            // xpanelcontainer
            // 
            this.xpanelcontainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.xpanelcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xpanelcontainer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xpanelcontainer.Location = new System.Drawing.Point(40, 45);
            this.xpanelcontainer.Name = "xpanelcontainer";
            this.xpanelcontainer.Size = new System.Drawing.Size(499, 408);
            this.xpanelcontainer.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.ContextMenuStrip = this.contextMenuStrip1;
            this.panel2.Controls.Add(this.xstopb);
            this.panel2.Controls.Add(this.xindelingb);
            this.panel2.Controls.Add(this.xstartb);
            this.panel2.Controls.Add(this.groupBox3);
            this.panel2.Controls.Add(this.xprogressbar);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(539, 45);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(185, 408);
            this.panel2.TabIndex = 12;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.xpanelcontainer);
            this.panel3.Controls.Add(this.panel2);
            this.panel3.Controls.Add(this.panel5);
            this.panel3.Controls.Add(this.mainMenu1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(724, 453);
            this.panel3.TabIndex = 6;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.xstatuslabel);
            this.panel5.Controls.Add(this.xstatusimage);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel5.Location = new System.Drawing.Point(40, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(684, 45);
            this.panel5.TabIndex = 5;
            // 
            // xstatusimage
            // 
            this.xstatusimage.ContextMenuStrip = this.contextMenuStrip1;
            this.xstatusimage.Dock = System.Windows.Forms.DockStyle.Left;
            this.xstatusimage.Location = new System.Drawing.Point(0, 0);
            this.xstatusimage.Name = "xstatusimage";
            this.xstatusimage.Size = new System.Drawing.Size(40, 45);
            this.xstatusimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.xstatusimage.TabIndex = 1;
            this.xstatusimage.TabStop = false;
            // 
            // mainMenu1
            // 
            this.mainMenu1.ContextMenuStrip = this.contextMenuStrip1;
            this.mainMenu1.Dock = System.Windows.Forms.DockStyle.Left;
            this.mainMenu1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainMenu1.IsExpanded = false;
            this.mainMenu1.Location = new System.Drawing.Point(0, 0);
            this.mainMenu1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            menuButton1.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton1.CombineImage = null;
            menuButton1.CombineScale = 1.5D;
            menuButton1.ContextMenu = this.contextMenuStrip1;
            menuButton1.Enabled = true;
            menuButton1.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton1.Image")));
            menuButton1.ImageSize = new System.Drawing.Size(32, 32);
            menuButton1.Index = 0;
            menuButton1.Name = "xwijzigproductie";
            menuButton1.Text = "Wijzig Productie";
            menuButton1.Tooltip = "Wijzig productie";
            menuButton2.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton2.CombineImage = null;
            menuButton2.CombineScale = 1.5D;
            menuButton2.ContextMenu = null;
            menuButton2.Enabled = true;
            menuButton2.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton2.Image")));
            menuButton2.ImageSize = new System.Drawing.Size(32, 32);
            menuButton2.Index = 1;
            menuButton2.Name = "xmeldgereed";
            menuButton2.Text = "Meld Gereed";
            menuButton2.Tooltip = "Meld bewerking gereed";
            menuButton3.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton3.CombineImage = null;
            menuButton3.CombineScale = 1.5D;
            menuButton3.ContextMenu = null;
            menuButton3.Enabled = true;
            menuButton3.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton3.Image")));
            menuButton3.ImageSize = new System.Drawing.Size(32, 32);
            menuButton3.Index = 2;
            menuButton3.Name = "xdeelmeldingen";
            menuButton3.Text = "Deels Gereedmeldingen";
            menuButton3.Tooltip = "Deels gereedmeldingen";
            menuButton4.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton4.CombineImage = null;
            menuButton4.CombineScale = 1.5D;
            menuButton4.ContextMenu = null;
            menuButton4.Enabled = true;
            menuButton4.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton4.Image")));
            menuButton4.ImageSize = new System.Drawing.Size(32, 32);
            menuButton4.Index = 3;
            menuButton4.Name = "xindeling";
            menuButton4.Text = "Beheer Indeling";
            menuButton4.Tooltip = "Pas je indeling aan met personeel en werkplekken";
            menuButton5.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton5.CombineImage = null;
            menuButton5.CombineScale = 1.5D;
            menuButton5.ContextMenu = null;
            menuButton5.Enabled = true;
            menuButton5.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton5.Image")));
            menuButton5.ImageSize = new System.Drawing.Size(32, 32);
            menuButton5.Index = 4;
            menuButton5.Name = "xaantalgemaakt";
            menuButton5.Text = "Aantal Gemaakt";
            menuButton5.Tooltip = "Wijzig aantal gemaakt";
            menuButton6.AccesLevel = Rpm.Various.AccesType.AlleenKijken;
            menuButton6.CombineImage = null;
            menuButton6.CombineScale = 1.5D;
            menuButton6.ContextMenu = null;
            menuButton6.Enabled = true;
            menuButton6.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton6.Image")));
            menuButton6.ImageSize = new System.Drawing.Size(32, 32);
            menuButton6.Index = 5;
            menuButton6.Name = "xaanbevolenpersonen";
            menuButton6.Text = "Aanbevolen Personen";
            menuButton6.Tooltip = "Toon aanbevolen personeel";
            menuButton7.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton7.CombineImage = null;
            menuButton7.CombineScale = 1.5D;
            menuButton7.ContextMenu = null;
            menuButton7.Enabled = true;
            menuButton7.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton7.Image")));
            menuButton7.ImageSize = new System.Drawing.Size(32, 32);
            menuButton7.Index = 6;
            menuButton7.Name = "xrooster";
            menuButton7.Text = "Werk Rooster";
            menuButton7.Tooltip = "Wijzig  werkrooster";
            menuButton8.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton8.CombineImage = null;
            menuButton8.CombineScale = 1.5D;
            menuButton8.ContextMenu = null;
            menuButton8.Enabled = true;
            menuButton8.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton8.Image")));
            menuButton8.ImageSize = new System.Drawing.Size(32, 32);
            menuButton8.Index = 7;
            menuButton8.Name = "xonderbreking";
            menuButton8.Text = "Onderbrekeningen";
            menuButton8.Tooltip = "Wijzig/voeg onderbrekeningen";
            menuButton9.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton9.CombineImage = null;
            menuButton9.CombineScale = 1.5D;
            menuButton9.ContextMenu = null;
            menuButton9.Enabled = false;
            menuButton9.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton9.Image")));
            menuButton9.ImageSize = new System.Drawing.Size(32, 32);
            menuButton9.Index = 8;
            menuButton9.Name = "xwerktijden";
            menuButton9.Text = "Gewerkte Tijden";
            menuButton9.Tooltip = "Toon of wijzig gewerkte tijden";
            menuButton10.AccesLevel = Rpm.Various.AccesType.AlleenKijken;
            menuButton10.CombineImage = null;
            menuButton10.CombineScale = 1.5D;
            menuButton10.ContextMenu = null;
            menuButton10.Enabled = true;
            menuButton10.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton10.Image")));
            menuButton10.ImageSize = new System.Drawing.Size(32, 32);
            menuButton10.Index = 9;
            menuButton10.Name = "xopenpdf";
            menuButton10.Text = "Productie Pdf";
            menuButton10.Tooltip = "Open productieformulier als pdf";
            this.mainMenu1.MenuButtons = new Various.MenuButton[] {
        menuButton1,
        menuButton2,
        menuButton3,
        menuButton4,
        menuButton5,
        menuButton6,
        menuButton7,
        menuButton8,
        menuButton9,
        menuButton10};
            this.mainMenu1.Name = "mainMenu1";
            this.mainMenu1.Size = new System.Drawing.Size(40, 453);
            this.mainMenu1.TabIndex = 4;
            this.mainMenu1.OnMenuClick += new System.EventHandler(this.mainMenu1_OnMenuClick);
            // 
            // ProductieForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel3);
            this.DoubleBuffered = true;
            this.Name = "ProductieForm";
            this.Size = new System.Drawing.Size(724, 453);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xstatusimage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox3;
        private MetroFramework.Controls.MetroComboBox xbewerking;
        private CircularProgressBar.CircularProgressBar xprogressbar;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel xpanelcontainer;
        private System.Windows.Forms.Label xstatuslabel;
        private MainMenu mainMenu1;
        private System.Windows.Forms.Button xstopb;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button xindelingb;
        private System.Windows.Forms.Button xstartb;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.PictureBox xstatusimage;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem aantalTeMakenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem leverdatumToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem notitieToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem xprodafkeurtoolstrip;
    }
}