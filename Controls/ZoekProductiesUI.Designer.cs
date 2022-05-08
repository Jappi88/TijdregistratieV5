
namespace Controls
{
    partial class ZoekProductiesUI
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
            this.xcriteria = new MetroFramework.Controls.MetroTextBox();
            this.xtotcheck = new MetroFramework.Controls.MetroCheckBox();
            this.xtotdate = new System.Windows.Forms.DateTimePicker();
            this.xvanafcheck = new MetroFramework.Controls.MetroCheckBox();
            this.xvanafdate = new System.Windows.Forms.DateTimePicker();
            this.xbewerkingcheck = new MetroFramework.Controls.MetroCheckBox();
            this.xwerkplekcheck = new MetroFramework.Controls.MetroCheckBox();
            this.xcriteriacheckbox = new MetroFramework.Controls.MetroCheckBox();
            this.xbewerkingen = new MetroFramework.Controls.MetroComboBox();
            this.xwerkplekken = new MetroFramework.Controls.MetroComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xsluitenpanel = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xverwerkb = new System.Windows.Forms.Button();
            this.xprogresslabel = new System.Windows.Forms.Label();
            this.productieListControl1 = new Controls.ProductieListControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.xsluitenpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.xcriteria);
            this.panel1.Controls.Add(this.xtotcheck);
            this.panel1.Controls.Add(this.xtotdate);
            this.panel1.Controls.Add(this.xvanafcheck);
            this.panel1.Controls.Add(this.xvanafdate);
            this.panel1.Controls.Add(this.xbewerkingcheck);
            this.panel1.Controls.Add(this.xwerkplekcheck);
            this.panel1.Controls.Add(this.xcriteriacheckbox);
            this.panel1.Controls.Add(this.xbewerkingen);
            this.panel1.Controls.Add(this.xwerkplekken);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1034, 124);
            this.panel1.TabIndex = 0;
            // 
            // xcriteria
            // 
            this.xcriteria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.xcriteria.CustomButton.Image = null;
            this.xcriteria.CustomButton.Location = new System.Drawing.Point(538, 1);
            this.xcriteria.CustomButton.Name = "";
            this.xcriteria.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xcriteria.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xcriteria.CustomButton.TabIndex = 1;
            this.xcriteria.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xcriteria.CustomButton.UseSelectable = true;
            this.xcriteria.CustomButton.Visible = false;
            this.xcriteria.Lines = new string[0];
            this.xcriteria.Location = new System.Drawing.Point(380, 15);
            this.xcriteria.MaxLength = 32767;
            this.xcriteria.Name = "xcriteria";
            this.xcriteria.PasswordChar = '\0';
            this.xcriteria.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xcriteria.SelectedText = "";
            this.xcriteria.SelectionLength = 0;
            this.xcriteria.SelectionStart = 0;
            this.xcriteria.ShortcutsEnabled = true;
            this.xcriteria.ShowClearButton = true;
            this.xcriteria.Size = new System.Drawing.Size(651, 23);
            this.xcriteria.TabIndex = 0;
            this.xcriteria.UseSelectable = true;
            this.xcriteria.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xcriteria.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xcriteria.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xcriteria_KeyDown);
            // 
            // xtotcheck
            // 
            this.xtotcheck.AutoSize = true;
            this.xtotcheck.Location = new System.Drawing.Point(499, 82);
            this.xtotcheck.Name = "xtotcheck";
            this.xtotcheck.Size = new System.Drawing.Size(39, 15);
            this.xtotcheck.TabIndex = 13;
            this.xtotcheck.Text = "Tot";
            this.xtotcheck.UseSelectable = true;
            this.xtotcheck.CheckedChanged += new System.EventHandler(this.xtotcheck_CheckedChanged);
            // 
            // xtotdate
            // 
            this.xtotdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtotdate.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xtotdate.Enabled = false;
            this.xtotdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtotdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xtotdate.Location = new System.Drawing.Point(583, 77);
            this.xtotdate.Name = "xtotdate";
            this.xtotdate.Size = new System.Drawing.Size(448, 25);
            this.xtotdate.TabIndex = 12;
            this.xtotdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xcriteria_KeyDown);
            // 
            // xvanafcheck
            // 
            this.xvanafcheck.AutoSize = true;
            this.xvanafcheck.Location = new System.Drawing.Point(130, 82);
            this.xvanafcheck.Name = "xvanafcheck";
            this.xvanafcheck.Size = new System.Drawing.Size(52, 15);
            this.xvanafcheck.TabIndex = 11;
            this.xvanafcheck.Text = "Vanaf";
            this.xvanafcheck.UseSelectable = true;
            this.xvanafcheck.CheckedChanged += new System.EventHandler(this.xvanafcheck_CheckedChanged);
            // 
            // xvanafdate
            // 
            this.xvanafdate.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xvanafdate.Enabled = false;
            this.xvanafdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvanafdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xvanafdate.Location = new System.Drawing.Point(211, 77);
            this.xvanafdate.Name = "xvanafdate";
            this.xvanafdate.Size = new System.Drawing.Size(282, 25);
            this.xvanafdate.TabIndex = 10;
            this.xvanafdate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xcriteria_KeyDown);
            // 
            // xbewerkingcheck
            // 
            this.xbewerkingcheck.AutoSize = true;
            this.xbewerkingcheck.Location = new System.Drawing.Point(499, 51);
            this.xbewerkingcheck.Name = "xbewerkingcheck";
            this.xbewerkingcheck.Size = new System.Drawing.Size(78, 15);
            this.xbewerkingcheck.TabIndex = 9;
            this.xbewerkingcheck.Text = "Bewerking";
            this.xbewerkingcheck.UseSelectable = true;
            this.xbewerkingcheck.CheckedChanged += new System.EventHandler(this.xbewerkingcheck_CheckedChanged);
            // 
            // xwerkplekcheck
            // 
            this.xwerkplekcheck.AutoSize = true;
            this.xwerkplekcheck.Location = new System.Drawing.Point(130, 51);
            this.xwerkplekcheck.Name = "xwerkplekcheck";
            this.xwerkplekcheck.Size = new System.Drawing.Size(75, 15);
            this.xwerkplekcheck.TabIndex = 8;
            this.xwerkplekcheck.Text = "Werk plek";
            this.xwerkplekcheck.UseSelectable = true;
            this.xwerkplekcheck.CheckedChanged += new System.EventHandler(this.xwerkplekcheck_CheckedChanged);
            // 
            // xcriteriacheckbox
            // 
            this.xcriteriacheckbox.AutoSize = true;
            this.xcriteriacheckbox.Checked = true;
            this.xcriteriacheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xcriteriacheckbox.Location = new System.Drawing.Point(130, 17);
            this.xcriteriacheckbox.Name = "xcriteriacheckbox";
            this.xcriteriacheckbox.Size = new System.Drawing.Size(244, 15);
            this.xcriteriacheckbox.TabIndex = 7;
            this.xcriteriacheckbox.Text = "Artikelnr, productienr of een omschrijving";
            this.xcriteriacheckbox.UseSelectable = true;
            this.xcriteriacheckbox.CheckedChanged += new System.EventHandler(this.xartikelnrcheck_CheckedChanged);
            // 
            // xbewerkingen
            // 
            this.xbewerkingen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xbewerkingen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.xbewerkingen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.xbewerkingen.Enabled = false;
            this.xbewerkingen.FormattingEnabled = true;
            this.xbewerkingen.ItemHeight = 23;
            this.xbewerkingen.Location = new System.Drawing.Point(583, 46);
            this.xbewerkingen.Name = "xbewerkingen";
            this.xbewerkingen.Size = new System.Drawing.Size(448, 29);
            this.xbewerkingen.TabIndex = 6;
            this.xbewerkingen.UseSelectable = true;
            this.xbewerkingen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xcriteria_KeyDown);
            // 
            // xwerkplekken
            // 
            this.xwerkplekken.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.xwerkplekken.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.xwerkplekken.Enabled = false;
            this.xwerkplekken.FormattingEnabled = true;
            this.xwerkplekken.ItemHeight = 23;
            this.xwerkplekken.Location = new System.Drawing.Point(211, 44);
            this.xwerkplekken.Name = "xwerkplekken";
            this.xwerkplekken.Size = new System.Drawing.Size(282, 29);
            this.xwerkplekken.TabIndex = 3;
            this.xwerkplekken.UseSelectable = true;
            this.xwerkplekken.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xcriteria_KeyDown);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.FocusEye_img_128_128;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 124);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(48, 48);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xsluitenpanel
            // 
            this.xsluitenpanel.BackColor = System.Drawing.Color.White;
            this.xsluitenpanel.Controls.Add(this.xsluiten);
            this.xsluitenpanel.Controls.Add(this.xverwerkb);
            this.xsluitenpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xsluitenpanel.Location = new System.Drawing.Point(0, 513);
            this.xsluitenpanel.Name = "xsluitenpanel";
            this.xsluitenpanel.Size = new System.Drawing.Size(1034, 39);
            this.xsluitenpanel.TabIndex = 22;
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(931, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(100, 32);
            this.xsluiten.TabIndex = 2;
            this.xsluiten.Text = "&Sluiten";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xverwerkb
            // 
            this.xverwerkb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xverwerkb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xverwerkb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xverwerkb.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.xverwerkb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xverwerkb.Location = new System.Drawing.Point(772, 3);
            this.xverwerkb.Name = "xverwerkb";
            this.xverwerkb.Size = new System.Drawing.Size(153, 32);
            this.xverwerkb.TabIndex = 14;
            this.xverwerkb.Text = "Zoek Producties!";
            this.xverwerkb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xverwerkb.UseVisualStyleBackColor = true;
            this.xverwerkb.Click += new System.EventHandler(this.xverwerkb_Click);
            // 
            // xprogresslabel
            // 
            this.xprogresslabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xprogresslabel.BackColor = System.Drawing.Color.White;
            this.xprogresslabel.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprogresslabel.Location = new System.Drawing.Point(0, 124);
            this.xprogresslabel.Name = "xprogresslabel";
            this.xprogresslabel.Size = new System.Drawing.Size(1034, 386);
            this.xprogresslabel.TabIndex = 23;
            this.xprogresslabel.Text = "Producties laden...";
            this.xprogresslabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xprogresslabel.Visible = false;
            // 
            // productieListControl1
            // 
            this.productieListControl1.AutoScroll = true;
            this.productieListControl1.BackColor = System.Drawing.Color.White;
            this.productieListControl1.CanLoad = true;
            this.productieListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieListControl1.EnableEntryFiltering = true;
            this.productieListControl1.EnableFiltering = false;
            this.productieListControl1.EnableSync = false;
            this.productieListControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieListControl1.IsBewerkingView = true;
            this.productieListControl1.ListName = "RangeBewerkingLijst";
            this.productieListControl1.Location = new System.Drawing.Point(0, 124);
            this.productieListControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productieListControl1.Name = "productieListControl1";
            this.productieListControl1.RemoveCustomItemIfNotValid = false;
            this.productieListControl1.SelectedItem = null;
            this.productieListControl1.ShowWaitUI = true;
            this.productieListControl1.Size = new System.Drawing.Size(1034, 389);
            this.productieListControl1.TabIndex = 23;
            this.productieListControl1.ValidHandler = null;
            // 
            // ZoekProductiesUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.xprogresslabel);
            this.Controls.Add(this.productieListControl1);
            this.Controls.Add(this.xsluitenpanel);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(725, 425);
            this.Name = "ZoekProductiesUI";
            this.Size = new System.Drawing.Size(1034, 552);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.xsluitenpanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button xverwerkb;
        private MetroFramework.Controls.MetroCheckBox xtotcheck;
        private System.Windows.Forms.DateTimePicker xtotdate;
        private MetroFramework.Controls.MetroCheckBox xvanafcheck;
        private System.Windows.Forms.DateTimePicker xvanafdate;
        private MetroFramework.Controls.MetroCheckBox xbewerkingcheck;
        private MetroFramework.Controls.MetroCheckBox xwerkplekcheck;
        private MetroFramework.Controls.MetroCheckBox xcriteriacheckbox;
        private MetroFramework.Controls.MetroComboBox xbewerkingen;
        private MetroFramework.Controls.MetroComboBox xwerkplekken;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel xsluitenpanel;
        private System.Windows.Forms.Label xprogresslabel;
        private Controls.ProductieListControl productieListControl1;
        private MetroFramework.Controls.MetroTextBox xcriteria;
    }
}