
namespace Forms
{
    partial class RangeCalculatorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RangeCalculatorForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xtotcheck = new System.Windows.Forms.CheckBox();
            this.xtotdate = new System.Windows.Forms.DateTimePicker();
            this.xvanafcheck = new System.Windows.Forms.CheckBox();
            this.xvanafdate = new System.Windows.Forms.DateTimePicker();
            this.xbewerkingcheck = new System.Windows.Forms.CheckBox();
            this.xwerkplekcheck = new System.Windows.Forms.CheckBox();
            this.xcriteriacheckbox = new System.Windows.Forms.CheckBox();
            this.xbewerkingen = new System.Windows.Forms.ComboBox();
            this.xwerkplekken = new System.Windows.Forms.ComboBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xverwerkb = new System.Windows.Forms.Button();
            this.xsluiten = new System.Windows.Forms.Button();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xoutput = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xprogresslabel = new System.Windows.Forms.Label();
            this.productieListControl1 = new Controls.ProductieListControl();
            this.xcriteria = new MetroFramework.Controls.MetroTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
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
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(920, 105);
            this.panel1.TabIndex = 0;
            // 
            // xtotcheck
            // 
            this.xtotcheck.AutoSize = true;
            this.xtotcheck.Checked = true;
            this.xtotcheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xtotcheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtotcheck.Location = new System.Drawing.Point(499, 82);
            this.xtotcheck.Name = "xtotcheck";
            this.xtotcheck.Size = new System.Drawing.Size(47, 21);
            this.xtotcheck.TabIndex = 13;
            this.xtotcheck.Text = "Tot";
            this.xtotcheck.UseVisualStyleBackColor = true;
            this.xtotcheck.CheckedChanged += new System.EventHandler(this.xtotcheck_CheckedChanged);
            // 
            // xtotdate
            // 
            this.xtotdate.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xtotdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtotdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xtotdate.Location = new System.Drawing.Point(614, 77);
            this.xtotdate.Name = "xtotdate";
            this.xtotdate.Size = new System.Drawing.Size(268, 25);
            this.xtotdate.TabIndex = 12;
            // 
            // xvanafcheck
            // 
            this.xvanafcheck.AutoSize = true;
            this.xvanafcheck.Checked = true;
            this.xvanafcheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xvanafcheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvanafcheck.Location = new System.Drawing.Point(130, 82);
            this.xvanafcheck.Name = "xvanafcheck";
            this.xvanafcheck.Size = new System.Drawing.Size(62, 21);
            this.xvanafcheck.TabIndex = 11;
            this.xvanafcheck.Text = "Vanaf";
            this.xvanafcheck.UseVisualStyleBackColor = true;
            this.xvanafcheck.CheckedChanged += new System.EventHandler(this.xvanafcheck_CheckedChanged);
            // 
            // xvanafdate
            // 
            this.xvanafdate.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xvanafdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvanafdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xvanafdate.Location = new System.Drawing.Point(225, 77);
            this.xvanafdate.Name = "xvanafdate";
            this.xvanafdate.Size = new System.Drawing.Size(268, 25);
            this.xvanafdate.TabIndex = 10;
            // 
            // xbewerkingcheck
            // 
            this.xbewerkingcheck.AutoSize = true;
            this.xbewerkingcheck.Checked = true;
            this.xbewerkingcheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xbewerkingcheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbewerkingcheck.Location = new System.Drawing.Point(499, 48);
            this.xbewerkingcheck.Name = "xbewerkingcheck";
            this.xbewerkingcheck.Size = new System.Drawing.Size(91, 21);
            this.xbewerkingcheck.TabIndex = 9;
            this.xbewerkingcheck.Text = "Bewerking";
            this.xbewerkingcheck.UseVisualStyleBackColor = true;
            this.xbewerkingcheck.CheckedChanged += new System.EventHandler(this.xbewerkingcheck_CheckedChanged);
            // 
            // xwerkplekcheck
            // 
            this.xwerkplekcheck.AutoSize = true;
            this.xwerkplekcheck.Checked = true;
            this.xwerkplekcheck.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xwerkplekcheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xwerkplekcheck.Location = new System.Drawing.Point(130, 48);
            this.xwerkplekcheck.Name = "xwerkplekcheck";
            this.xwerkplekcheck.Size = new System.Drawing.Size(89, 21);
            this.xwerkplekcheck.TabIndex = 8;
            this.xwerkplekcheck.Text = "Werk plek";
            this.xwerkplekcheck.UseVisualStyleBackColor = true;
            this.xwerkplekcheck.CheckedChanged += new System.EventHandler(this.xwerkplekcheck_CheckedChanged);
            // 
            // xcriteriacheckbox
            // 
            this.xcriteriacheckbox.AutoSize = true;
            this.xcriteriacheckbox.Checked = true;
            this.xcriteriacheckbox.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xcriteriacheckbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xcriteriacheckbox.Location = new System.Drawing.Point(130, 17);
            this.xcriteriacheckbox.Name = "xcriteriacheckbox";
            this.xcriteriacheckbox.Size = new System.Drawing.Size(287, 21);
            this.xcriteriacheckbox.TabIndex = 7;
            this.xcriteriacheckbox.Text = "Artikelnr, productienr of een omschrijving";
            this.xcriteriacheckbox.UseVisualStyleBackColor = true;
            this.xcriteriacheckbox.CheckedChanged += new System.EventHandler(this.xartikelnrcheck_CheckedChanged);
            // 
            // xbewerkingen
            // 
            this.xbewerkingen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.xbewerkingen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.xbewerkingen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbewerkingen.FormattingEnabled = true;
            this.xbewerkingen.Location = new System.Drawing.Point(614, 46);
            this.xbewerkingen.Name = "xbewerkingen";
            this.xbewerkingen.Size = new System.Drawing.Size(268, 25);
            this.xbewerkingen.TabIndex = 6;
            // 
            // xwerkplekken
            // 
            this.xwerkplekken.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.xwerkplekken.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.xwerkplekken.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xwerkplekken.FormattingEnabled = true;
            this.xwerkplekken.Location = new System.Drawing.Point(225, 44);
            this.xwerkplekken.Name = "xwerkplekken";
            this.xwerkplekken.Size = new System.Drawing.Size(268, 25);
            this.xwerkplekken.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.FocusEye_img_128_128;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 105);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // xverwerkb
            // 
            this.xverwerkb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xverwerkb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xverwerkb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xverwerkb.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.xverwerkb.Location = new System.Drawing.Point(638, 10);
            this.xverwerkb.Name = "xverwerkb";
            this.xverwerkb.Size = new System.Drawing.Size(153, 38);
            this.xverwerkb.TabIndex = 14;
            this.xverwerkb.Text = "Zoek Producties!";
            this.xverwerkb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xverwerkb.UseVisualStyleBackColor = true;
            this.xverwerkb.Click += new System.EventHandler(this.xverwerkb_Click);
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(797, 10);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(120, 38);
            this.xsluiten.TabIndex = 2;
            this.xsluiten.Text = "&Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(48, 48);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xoutput
            // 
            this.xoutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xoutput.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xoutput.Location = new System.Drawing.Point(5, 3);
            this.xoutput.Name = "xoutput";
            this.xoutput.Size = new System.Drawing.Size(627, 51);
            this.xoutput.TabIndex = 21;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xoutput);
            this.panel2.Controls.Add(this.xsluiten);
            this.panel2.Controls.Add(this.xverwerkb);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 485);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(920, 60);
            this.panel2.TabIndex = 22;
            // 
            // xprogresslabel
            // 
            this.xprogresslabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xprogresslabel.BackColor = System.Drawing.Color.White;
            this.xprogresslabel.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprogresslabel.Location = new System.Drawing.Point(20, 168);
            this.xprogresslabel.Name = "xprogresslabel";
            this.xprogresslabel.Size = new System.Drawing.Size(920, 320);
            this.xprogresslabel.TabIndex = 23;
            this.xprogresslabel.Text = "Producties laden...";
            this.xprogresslabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xprogresslabel.Visible = false;
            // 
            // productieListControl1
            // 
            this.productieListControl1.BackColor = System.Drawing.Color.White;
            this.productieListControl1.CanLoad = true;
            this.productieListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieListControl1.EnableEntryFiltering = false;
            this.productieListControl1.EnableFiltering = false;
            this.productieListControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieListControl1.IsBewerkingView = false;
            this.productieListControl1.Location = new System.Drawing.Point(20, 165);
            this.productieListControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productieListControl1.Name = "productieListControl1";
            this.productieListControl1.RemoveCustomItemIfNotValid = false;
            this.productieListControl1.SelectedItem = null;
            this.productieListControl1.Size = new System.Drawing.Size(920, 320);
            this.productieListControl1.TabIndex = 23;
            this.productieListControl1.ValidHandler = null;
            // 
            // xcriteria
            // 
            // 
            // 
            // 
            this.xcriteria.CustomButton.Image = null;
            this.xcriteria.CustomButton.Location = new System.Drawing.Point(437, 1);
            this.xcriteria.CustomButton.Name = "";
            this.xcriteria.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xcriteria.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xcriteria.CustomButton.TabIndex = 1;
            this.xcriteria.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xcriteria.CustomButton.UseSelectable = true;
            this.xcriteria.CustomButton.Visible = false;
            this.xcriteria.Lines = new string[0];
            this.xcriteria.Location = new System.Drawing.Point(423, 15);
            this.xcriteria.MaxLength = 32767;
            this.xcriteria.Name = "xcriteria";
            this.xcriteria.PasswordChar = '\0';
            this.xcriteria.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xcriteria.SelectedText = "";
            this.xcriteria.SelectionLength = 0;
            this.xcriteria.SelectionStart = 0;
            this.xcriteria.ShortcutsEnabled = true;
            this.xcriteria.Size = new System.Drawing.Size(459, 23);
            this.xcriteria.TabIndex = 0;
            this.xcriteria.UseSelectable = true;
            this.xcriteria.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xcriteria.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // RangeCalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(960, 565);
            this.Controls.Add(this.xprogresslabel);
            this.Controls.Add(this.productieListControl1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(725, 425);
            this.Name = "RangeCalculatorForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Bekijk producten aantal, tijden en gemiddelde.";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RangeCalculatorForm_FormClosing);
            this.Shown += new System.EventHandler(this.RangeCalculatorForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button xverwerkb;
        private System.Windows.Forms.CheckBox xtotcheck;
        private System.Windows.Forms.DateTimePicker xtotdate;
        private System.Windows.Forms.CheckBox xvanafcheck;
        private System.Windows.Forms.DateTimePicker xvanafdate;
        private System.Windows.Forms.CheckBox xbewerkingcheck;
        private System.Windows.Forms.CheckBox xwerkplekcheck;
        private System.Windows.Forms.CheckBox xcriteriacheckbox;
        private System.Windows.Forms.ComboBox xbewerkingen;
        private System.Windows.Forms.ComboBox xwerkplekken;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Label xoutput;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label xprogresslabel;
        private Controls.ProductieListControl productieListControl1;
        private MetroFramework.Controls.MetroTextBox xcriteria;
    }
}