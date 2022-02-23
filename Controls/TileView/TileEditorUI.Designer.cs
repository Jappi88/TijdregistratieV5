namespace Controls
{
    partial class TileEditorUI
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tileViewer1 = new Controls.TileViewer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xtileview = new MetroFramework.Controls.MetroComboBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.metroLabel3 = new MetroFramework.Controls.MetroLabel();
            this.xtilehoogte = new System.Windows.Forms.NumericUpDown();
            this.metroLabel4 = new MetroFramework.Controls.MetroLabel();
            this.xtilebreedte = new System.Windows.Forms.NumericUpDown();
            this.xtoontelling = new MetroFramework.Controls.MetroCheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xformaatpanel = new System.Windows.Forms.Panel();
            this.metroLabel2 = new MetroFramework.Controls.MetroLabel();
            this.ximagehoogte = new System.Windows.Forms.NumericUpDown();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.ximagebreedte = new System.Windows.Forms.NumericUpDown();
            this.xautocheckbox = new MetroFramework.Controls.MetroRadioButton();
            this.xaangepastcheckbox = new MetroFramework.Controls.MetroRadioButton();
            this.xnonecheckbox = new MetroFramework.Controls.MetroRadioButton();
            this.xtellingtextfont = new System.Windows.Forms.Button();
            this.xtextfont = new System.Windows.Forms.Button();
            this.xtextkleur = new System.Windows.Forms.Button();
            this.xtextkleurimage = new System.Windows.Forms.PictureBox();
            this.xtilekleurbutton = new System.Windows.Forms.Button();
            this.xtilekleur = new System.Windows.Forms.PictureBox();
            this.xomschrijving = new MetroFramework.Controls.MetroTextBox();
            this.xafbeeldingbutton = new System.Windows.Forms.Button();
            this.xafbeelding = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtilehoogte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtilebreedte)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.xformaatpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ximagehoogte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ximagebreedte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtextkleurimage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtilekleur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xafbeelding)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tileViewer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(356, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 463);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Voorbeeld";
            // 
            // tileViewer1
            // 
            this.tileViewer1.AllowDrop = true;
            this.tileViewer1.AutoScroll = true;
            this.tileViewer1.BackColor = System.Drawing.Color.White;
            this.tileViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileViewer1.EnableSaveTiles = false;
            this.tileViewer1.EnableTileSelection = false;
            this.tileViewer1.EnableTimer = false;
            this.tileViewer1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tileViewer1.Location = new System.Drawing.Point(3, 25);
            this.tileViewer1.MultipleSelections = false;
            this.tileViewer1.Name = "tileViewer1";
            this.tileViewer1.Size = new System.Drawing.Size(478, 435);
            this.tileViewer1.TabIndex = 0;
            this.tileViewer1.TileInfoRefresInterval = 10000;
            this.tileViewer1.SelectionChanged += new System.EventHandler(this.tileViewer1_SelectionChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.xtileview);
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.xtoontelling);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.xtellingtextfont);
            this.panel1.Controls.Add(this.xtextfont);
            this.panel1.Controls.Add(this.xtextkleur);
            this.panel1.Controls.Add(this.xtextkleurimage);
            this.panel1.Controls.Add(this.xtilekleurbutton);
            this.panel1.Controls.Add(this.xtilekleur);
            this.panel1.Controls.Add(this.xomschrijving);
            this.panel1.Controls.Add(this.xafbeeldingbutton);
            this.panel1.Controls.Add(this.xafbeelding);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 463);
            this.panel1.TabIndex = 1;
            // 
            // xtileview
            // 
            this.xtileview.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtileview.FormattingEnabled = true;
            this.xtileview.ItemHeight = 23;
            this.xtileview.Items.AddRange(new object[] {
            "Van Links Naar Rechts",
            "Van Boven Naar Onder",
            "Van Rechts Naar Links",
            "Van Onder Naar Boven"});
            this.xtileview.Location = new System.Drawing.Point(6, 329);
            this.xtileview.Name = "xtileview";
            this.xtileview.Size = new System.Drawing.Size(341, 29);
            this.xtileview.TabIndex = 14;
            this.xtileview.UseSelectable = true;
            this.xtileview.Visible = false;
            this.xtileview.SelectedIndexChanged += new System.EventHandler(this.xtileview_SelectedIndexChanged);
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.panel4);
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(6, 109);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(341, 57);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Tile Formaat";
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.metroLabel3);
            this.panel4.Controls.Add(this.xtilehoogte);
            this.panel4.Controls.Add(this.metroLabel4);
            this.panel4.Controls.Add(this.xtilebreedte);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(3, 21);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(335, 33);
            this.panel4.TabIndex = 5;
            // 
            // metroLabel3
            // 
            this.metroLabel3.AutoSize = true;
            this.metroLabel3.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel3.Location = new System.Drawing.Point(148, 9);
            this.metroLabel3.Name = "metroLabel3";
            this.metroLabel3.Size = new System.Drawing.Size(55, 19);
            this.metroLabel3.TabIndex = 6;
            this.metroLabel3.Text = "Hoogte";
            this.metroLabel3.UseStyleColors = true;
            // 
            // xtilehoogte
            // 
            this.xtilehoogte.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtilehoogte.Location = new System.Drawing.Point(209, 5);
            this.xtilehoogte.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.xtilehoogte.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.xtilehoogte.Name = "xtilehoogte";
            this.xtilehoogte.Size = new System.Drawing.Size(79, 25);
            this.xtilehoogte.TabIndex = 5;
            this.xtilehoogte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xtilehoogte.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.xtilehoogte.ValueChanged += new System.EventHandler(this.xtilebreedte_ValueChanged);
            // 
            // metroLabel4
            // 
            this.metroLabel4.AutoSize = true;
            this.metroLabel4.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel4.Location = new System.Drawing.Point(2, 9);
            this.metroLabel4.Name = "metroLabel4";
            this.metroLabel4.Size = new System.Drawing.Size(56, 19);
            this.metroLabel4.TabIndex = 4;
            this.metroLabel4.Text = "Breedte";
            this.metroLabel4.UseStyleColors = true;
            // 
            // xtilebreedte
            // 
            this.xtilebreedte.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtilebreedte.Location = new System.Drawing.Point(63, 5);
            this.xtilebreedte.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.xtilebreedte.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.xtilebreedte.Name = "xtilebreedte";
            this.xtilebreedte.Size = new System.Drawing.Size(79, 25);
            this.xtilebreedte.TabIndex = 3;
            this.xtilebreedte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xtilebreedte.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.xtilebreedte.ValueChanged += new System.EventHandler(this.xtilebreedte_ValueChanged);
            // 
            // xtoontelling
            // 
            this.xtoontelling.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtoontelling.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xtoontelling.FontWeight = MetroFramework.MetroCheckBoxWeight.Bold;
            this.xtoontelling.Location = new System.Drawing.Point(6, 300);
            this.xtoontelling.Name = "xtoontelling";
            this.xtoontelling.Size = new System.Drawing.Size(341, 23);
            this.xtoontelling.TabIndex = 3;
            this.xtoontelling.Text = "Toon TileTelling indien mogelijk";
            this.xtoontelling.UseSelectable = true;
            this.xtoontelling.UseStyleColors = true;
            this.xtoontelling.CheckedChanged += new System.EventHandler(this.xtoontelling_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.panel2);
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(6, 169);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(341, 99);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Afbeelding Formaat";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xformaatpanel);
            this.panel2.Controls.Add(this.xautocheckbox);
            this.panel2.Controls.Add(this.xaangepastcheckbox);
            this.panel2.Controls.Add(this.xnonecheckbox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 21);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(335, 75);
            this.panel2.TabIndex = 11;
            // 
            // xformaatpanel
            // 
            this.xformaatpanel.Controls.Add(this.metroLabel2);
            this.xformaatpanel.Controls.Add(this.ximagehoogte);
            this.xformaatpanel.Controls.Add(this.metroLabel1);
            this.xformaatpanel.Controls.Add(this.ximagebreedte);
            this.xformaatpanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.xformaatpanel.Location = new System.Drawing.Point(181, 0);
            this.xformaatpanel.Name = "xformaatpanel";
            this.xformaatpanel.Size = new System.Drawing.Size(154, 75);
            this.xformaatpanel.TabIndex = 4;
            this.xformaatpanel.Visible = false;
            // 
            // metroLabel2
            // 
            this.metroLabel2.AutoSize = true;
            this.metroLabel2.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel2.Location = new System.Drawing.Point(4, 38);
            this.metroLabel2.Name = "metroLabel2";
            this.metroLabel2.Size = new System.Drawing.Size(55, 19);
            this.metroLabel2.TabIndex = 6;
            this.metroLabel2.Text = "Hoogte";
            this.metroLabel2.UseStyleColors = true;
            // 
            // ximagehoogte
            // 
            this.ximagehoogte.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ximagehoogte.Location = new System.Drawing.Point(64, 35);
            this.ximagehoogte.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.ximagehoogte.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.ximagehoogte.Name = "ximagehoogte";
            this.ximagehoogte.Size = new System.Drawing.Size(79, 25);
            this.ximagehoogte.TabIndex = 5;
            this.ximagehoogte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ximagehoogte.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.ximagehoogte.ValueChanged += new System.EventHandler(this.xbreedte_ValueChanged);
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontWeight = MetroFramework.MetroLabelWeight.Regular;
            this.metroLabel1.Location = new System.Drawing.Point(3, 8);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(56, 19);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Breedte";
            this.metroLabel1.UseStyleColors = true;
            // 
            // ximagebreedte
            // 
            this.ximagebreedte.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ximagebreedte.Location = new System.Drawing.Point(64, 4);
            this.ximagebreedte.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.ximagebreedte.Minimum = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.ximagebreedte.Name = "ximagebreedte";
            this.ximagebreedte.Size = new System.Drawing.Size(79, 25);
            this.ximagebreedte.TabIndex = 3;
            this.ximagebreedte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ximagebreedte.Value = new decimal(new int[] {
            16,
            0,
            0,
            0});
            this.ximagebreedte.ValueChanged += new System.EventHandler(this.xbreedte_ValueChanged);
            // 
            // xautocheckbox
            // 
            this.xautocheckbox.AutoSize = true;
            this.xautocheckbox.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xautocheckbox.Location = new System.Drawing.Point(3, 53);
            this.xautocheckbox.Name = "xautocheckbox";
            this.xautocheckbox.Size = new System.Drawing.Size(172, 19);
            this.xautocheckbox.TabIndex = 2;
            this.xautocheckbox.Text = "Automatisch Aanpassen";
            this.xautocheckbox.UseSelectable = true;
            this.xautocheckbox.UseStyleColors = true;
            this.xautocheckbox.CheckedChanged += new System.EventHandler(this.xnonecheckbox_CheckedChanged);
            // 
            // xaangepastcheckbox
            // 
            this.xaangepastcheckbox.AutoSize = true;
            this.xaangepastcheckbox.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xaangepastcheckbox.Location = new System.Drawing.Point(3, 28);
            this.xaangepastcheckbox.Name = "xaangepastcheckbox";
            this.xaangepastcheckbox.Size = new System.Drawing.Size(145, 19);
            this.xaangepastcheckbox.TabIndex = 1;
            this.xaangepastcheckbox.Text = "Aangepast Formaat";
            this.xaangepastcheckbox.UseSelectable = true;
            this.xaangepastcheckbox.UseStyleColors = true;
            this.xaangepastcheckbox.CheckedChanged += new System.EventHandler(this.xnonecheckbox_CheckedChanged);
            // 
            // xnonecheckbox
            // 
            this.xnonecheckbox.AutoSize = true;
            this.xnonecheckbox.Checked = true;
            this.xnonecheckbox.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xnonecheckbox.Location = new System.Drawing.Point(3, 3);
            this.xnonecheckbox.Name = "xnonecheckbox";
            this.xnonecheckbox.Size = new System.Drawing.Size(145, 19);
            this.xnonecheckbox.TabIndex = 0;
            this.xnonecheckbox.TabStop = true;
            this.xnonecheckbox.Text = "Afbeelding Formaat";
            this.xnonecheckbox.UseSelectable = true;
            this.xnonecheckbox.UseStyleColors = true;
            this.xnonecheckbox.CheckedChanged += new System.EventHandler(this.xnonecheckbox_CheckedChanged);
            // 
            // xtellingtextfont
            // 
            this.xtellingtextfont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xtellingtextfont.Location = new System.Drawing.Point(6, 75);
            this.xtellingtextfont.Name = "xtellingtextfont";
            this.xtellingtextfont.Size = new System.Drawing.Size(247, 28);
            this.xtellingtextfont.TabIndex = 9;
            this.xtellingtextfont.Text = "Telling Text Font";
            this.xtellingtextfont.UseVisualStyleBackColor = true;
            this.xtellingtextfont.Click += new System.EventHandler(this.xtellingtextfont_Click);
            // 
            // xtextfont
            // 
            this.xtextfont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xtextfont.Location = new System.Drawing.Point(157, 41);
            this.xtextfont.Name = "xtextfont";
            this.xtextfont.Size = new System.Drawing.Size(96, 28);
            this.xtextfont.TabIndex = 8;
            this.xtextfont.Text = "Text Font";
            this.xtextfont.UseVisualStyleBackColor = true;
            this.xtextfont.Click += new System.EventHandler(this.xtextfont_Click);
            // 
            // xtextkleur
            // 
            this.xtextkleur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xtextkleur.Location = new System.Drawing.Point(6, 41);
            this.xtextkleur.Name = "xtextkleur";
            this.xtextkleur.Size = new System.Drawing.Size(111, 28);
            this.xtextkleur.TabIndex = 7;
            this.xtextkleur.Text = "Text Kleur";
            this.xtextkleur.UseVisualStyleBackColor = true;
            this.xtextkleur.Click += new System.EventHandler(this.xtextkleur_Click);
            // 
            // xtextkleurimage
            // 
            this.xtextkleurimage.Location = new System.Drawing.Point(123, 41);
            this.xtextkleurimage.Name = "xtextkleurimage";
            this.xtextkleurimage.Size = new System.Drawing.Size(28, 28);
            this.xtextkleurimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xtextkleurimage.TabIndex = 6;
            this.xtextkleurimage.TabStop = false;
            // 
            // xtilekleurbutton
            // 
            this.xtilekleurbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xtilekleurbutton.Location = new System.Drawing.Point(6, 7);
            this.xtilekleurbutton.Name = "xtilekleurbutton";
            this.xtilekleurbutton.Size = new System.Drawing.Size(111, 28);
            this.xtilekleurbutton.TabIndex = 5;
            this.xtilekleurbutton.Text = "Tile Kleur";
            this.xtilekleurbutton.UseVisualStyleBackColor = true;
            this.xtilekleurbutton.Click += new System.EventHandler(this.xtilekleurbutton_Click);
            // 
            // xtilekleur
            // 
            this.xtilekleur.Location = new System.Drawing.Point(123, 7);
            this.xtilekleur.Name = "xtilekleur";
            this.xtilekleur.Size = new System.Drawing.Size(28, 28);
            this.xtilekleur.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xtilekleur.TabIndex = 4;
            this.xtilekleur.TabStop = false;
            // 
            // xomschrijving
            // 
            this.xomschrijving.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.xomschrijving.CustomButton.Image = null;
            this.xomschrijving.CustomButton.Location = new System.Drawing.Point(317, 2);
            this.xomschrijving.CustomButton.Name = "";
            this.xomschrijving.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xomschrijving.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xomschrijving.CustomButton.TabIndex = 1;
            this.xomschrijving.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xomschrijving.CustomButton.UseSelectable = true;
            this.xomschrijving.CustomButton.Visible = false;
            this.xomschrijving.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xomschrijving.Lines = new string[0];
            this.xomschrijving.Location = new System.Drawing.Point(6, 268);
            this.xomschrijving.MaxLength = 32767;
            this.xomschrijving.Name = "xomschrijving";
            this.xomschrijving.PasswordChar = '\0';
            this.xomschrijving.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xomschrijving.SelectedText = "";
            this.xomschrijving.SelectionLength = 0;
            this.xomschrijving.SelectionStart = 0;
            this.xomschrijving.ShortcutsEnabled = true;
            this.xomschrijving.Size = new System.Drawing.Size(341, 26);
            this.xomschrijving.TabIndex = 2;
            this.xomschrijving.UseSelectable = true;
            this.xomschrijving.WaterMark = "Tile Omschrijving";
            this.xomschrijving.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xomschrijving.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xomschrijving.TextChanged += new System.EventHandler(this.xomschrijving_TextChanged);
            // 
            // xafbeeldingbutton
            // 
            this.xafbeeldingbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xafbeeldingbutton.Location = new System.Drawing.Point(157, 7);
            this.xafbeeldingbutton.Name = "xafbeeldingbutton";
            this.xafbeeldingbutton.Size = new System.Drawing.Size(96, 28);
            this.xafbeeldingbutton.TabIndex = 1;
            this.xafbeeldingbutton.Text = "Afbeelding";
            this.xafbeeldingbutton.UseVisualStyleBackColor = true;
            this.xafbeeldingbutton.Click += new System.EventHandler(this.xafbeeldingbutton_Click);
            // 
            // xafbeelding
            // 
            this.xafbeelding.Location = new System.Drawing.Point(254, 7);
            this.xafbeelding.Name = "xafbeelding";
            this.xafbeelding.Size = new System.Drawing.Size(96, 96);
            this.xafbeelding.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xafbeelding.TabIndex = 0;
            this.xafbeelding.TabStop = false;
            // 
            // TileEditorUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TileEditorUI";
            this.Size = new System.Drawing.Size(840, 463);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtilehoogte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtilebreedte)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.xformaatpanel.ResumeLayout(false);
            this.xformaatpanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ximagehoogte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ximagebreedte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtextkleurimage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtilekleur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xafbeelding)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.TileViewer tileViewer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xtellingtextfont;
        private System.Windows.Forms.Button xtextfont;
        private System.Windows.Forms.Button xtextkleur;
        private System.Windows.Forms.PictureBox xtextkleurimage;
        private System.Windows.Forms.Button xtilekleurbutton;
        private System.Windows.Forms.PictureBox xtilekleur;
        private MetroFramework.Controls.MetroCheckBox xtoontelling;
        private MetroFramework.Controls.MetroTextBox xomschrijving;
        private System.Windows.Forms.Button xafbeeldingbutton;
        private System.Windows.Forms.PictureBox xafbeelding;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel xformaatpanel;
        private MetroFramework.Controls.MetroLabel metroLabel2;
        private System.Windows.Forms.NumericUpDown ximagehoogte;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.NumericUpDown ximagebreedte;
        private MetroFramework.Controls.MetroRadioButton xautocheckbox;
        private MetroFramework.Controls.MetroRadioButton xaangepastcheckbox;
        private MetroFramework.Controls.MetroRadioButton xnonecheckbox;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Panel panel4;
        private MetroFramework.Controls.MetroLabel metroLabel3;
        private System.Windows.Forms.NumericUpDown xtilehoogte;
        private MetroFramework.Controls.MetroLabel metroLabel4;
        private System.Windows.Forms.NumericUpDown xtilebreedte;
        private MetroFramework.Controls.MetroComboBox xtileview;
    }
}
