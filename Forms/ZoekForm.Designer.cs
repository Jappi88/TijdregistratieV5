
namespace Forms
{
    partial class ZoekForm
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
            this.xzoeken = new System.Windows.Forms.Button();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xgeavanceerdpannel = new System.Windows.Forms.Panel();
            this.xstatuscheckbox = new MetroFramework.Controls.MetroCheckBox();
            this.xstatuscombo = new MetroFramework.Controls.MetroComboBox();
            this.xmateriaalcriteria = new MetroFramework.Controls.MetroTextBox();
            this.xmateriaalcheckbox = new MetroFramework.Controls.MetroCheckBox();
            this.xtotcheck = new MetroFramework.Controls.MetroCheckBox();
            this.xtotdate = new System.Windows.Forms.DateTimePicker();
            this.xvanafcheck = new MetroFramework.Controls.MetroCheckBox();
            this.xvanafdate = new System.Windows.Forms.DateTimePicker();
            this.xbewerkingcheck = new MetroFramework.Controls.MetroCheckBox();
            this.xwerkplekcheck = new MetroFramework.Controls.MetroCheckBox();
            this.xbewerkingen = new MetroFramework.Controls.MetroComboBox();
            this.xwerkplekken = new MetroFramework.Controls.MetroComboBox();
            this.xcriteria = new MetroFramework.Controls.MetroTextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xgeavanceerd = new MetroFramework.Controls.MetroCheckBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.xgeavanceerdpannel.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xzoeken);
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(23, 370);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(644, 44);
            this.panel1.TabIndex = 0;
            // 
            // xzoeken
            // 
            this.xzoeken.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xzoeken.Image = global::ProductieManager.Properties.Resources.search_icon_32x32;
            this.xzoeken.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xzoeken.Location = new System.Drawing.Point(453, 3);
            this.xzoeken.Name = "xzoeken";
            this.xzoeken.Size = new System.Drawing.Size(91, 34);
            this.xzoeken.TabIndex = 1;
            this.xzoeken.Text = "Zoeken";
            this.xzoeken.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xzoeken, "Zoeken annuleren");
            this.xzoeken.UseVisualStyleBackColor = true;
            this.xzoeken.Click += new System.EventHandler(this.xzoeken_Click);
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(550, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(91, 34);
            this.xsluiten.TabIndex = 0;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xsluiten, "Zoeken annuleren");
            this.xsluiten.UseVisualStyleBackColor = true;
            // 
            // xgeavanceerdpannel
            // 
            this.xgeavanceerdpannel.Controls.Add(this.xstatuscheckbox);
            this.xgeavanceerdpannel.Controls.Add(this.xstatuscombo);
            this.xgeavanceerdpannel.Controls.Add(this.xmateriaalcriteria);
            this.xgeavanceerdpannel.Controls.Add(this.xmateriaalcheckbox);
            this.xgeavanceerdpannel.Controls.Add(this.xtotcheck);
            this.xgeavanceerdpannel.Controls.Add(this.xtotdate);
            this.xgeavanceerdpannel.Controls.Add(this.xvanafcheck);
            this.xgeavanceerdpannel.Controls.Add(this.xvanafdate);
            this.xgeavanceerdpannel.Controls.Add(this.xbewerkingcheck);
            this.xgeavanceerdpannel.Controls.Add(this.xwerkplekcheck);
            this.xgeavanceerdpannel.Controls.Add(this.xbewerkingen);
            this.xgeavanceerdpannel.Controls.Add(this.xwerkplekken);
            this.xgeavanceerdpannel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgeavanceerdpannel.Location = new System.Drawing.Point(23, 154);
            this.xgeavanceerdpannel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xgeavanceerdpannel.Name = "xgeavanceerdpannel";
            this.xgeavanceerdpannel.Size = new System.Drawing.Size(644, 216);
            this.xgeavanceerdpannel.TabIndex = 1;
            this.xgeavanceerdpannel.Visible = false;
            // 
            // xstatuscheckbox
            // 
            this.xstatuscheckbox.AutoSize = true;
            this.xstatuscheckbox.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xstatuscheckbox.Location = new System.Drawing.Point(9, 125);
            this.xstatuscheckbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xstatuscheckbox.Name = "xstatuscheckbox";
            this.xstatuscheckbox.Size = new System.Drawing.Size(125, 19);
            this.xstatuscheckbox.TabIndex = 27;
            this.xstatuscheckbox.Text = "Productie Status";
            this.xstatuscheckbox.UseSelectable = true;
            this.xstatuscheckbox.CheckedChanged += new System.EventHandler(this.xstatuscheckbox_CheckedChanged);
            // 
            // xstatuscombo
            // 
            this.xstatuscombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xstatuscombo.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.xstatuscombo.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.xstatuscombo.Enabled = false;
            this.xstatuscombo.FormattingEnabled = true;
            this.xstatuscombo.ItemHeight = 23;
            this.xstatuscombo.Location = new System.Drawing.Point(135, 119);
            this.xstatuscombo.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xstatuscombo.Name = "xstatuscombo";
            this.xstatuscombo.Size = new System.Drawing.Size(506, 29);
            this.xstatuscombo.TabIndex = 26;
            this.toolTip1.SetToolTip(this.xstatuscombo, "Kies een bewerking om te zoeken");
            this.xstatuscombo.UseSelectable = true;
            // 
            // xmateriaalcriteria
            // 
            this.xmateriaalcriteria.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.xmateriaalcriteria.CustomButton.Image = null;
            this.xmateriaalcriteria.CustomButton.Location = new System.Drawing.Point(478, 2);
            this.xmateriaalcriteria.CustomButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xmateriaalcriteria.CustomButton.Name = "";
            this.xmateriaalcriteria.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.xmateriaalcriteria.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xmateriaalcriteria.CustomButton.TabIndex = 1;
            this.xmateriaalcriteria.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xmateriaalcriteria.CustomButton.UseSelectable = true;
            this.xmateriaalcriteria.CustomButton.Visible = false;
            this.xmateriaalcriteria.Enabled = false;
            this.xmateriaalcriteria.Lines = new string[0];
            this.xmateriaalcriteria.Location = new System.Drawing.Point(135, 82);
            this.xmateriaalcriteria.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xmateriaalcriteria.MaxLength = 32767;
            this.xmateriaalcriteria.Name = "xmateriaalcriteria";
            this.xmateriaalcriteria.PasswordChar = '\0';
            this.xmateriaalcriteria.PromptText = "Vul in een criteria(s)...";
            this.xmateriaalcriteria.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xmateriaalcriteria.SelectedText = "";
            this.xmateriaalcriteria.SelectionLength = 0;
            this.xmateriaalcriteria.SelectionStart = 0;
            this.xmateriaalcriteria.ShortcutsEnabled = true;
            this.xmateriaalcriteria.ShowClearButton = true;
            this.xmateriaalcriteria.Size = new System.Drawing.Size(506, 30);
            this.xmateriaalcriteria.TabIndex = 25;
            this.toolTip1.SetToolTip(this.xmateriaalcriteria, "Vul in een criteria en onderscheid meerdere criteria\'s met een \';\'");
            this.xmateriaalcriteria.UseSelectable = true;
            this.xmateriaalcriteria.WaterMark = "Vul in een criteria(s)...";
            this.xmateriaalcriteria.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xmateriaalcriteria.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xmateriaalcriteria.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xcriteria_KeyDown);
            // 
            // xmateriaalcheckbox
            // 
            this.xmateriaalcheckbox.AutoSize = true;
            this.xmateriaalcheckbox.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xmateriaalcheckbox.Location = new System.Drawing.Point(9, 89);
            this.xmateriaalcheckbox.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xmateriaalcheckbox.Name = "xmateriaalcheckbox";
            this.xmateriaalcheckbox.Size = new System.Drawing.Size(120, 19);
            this.xmateriaalcheckbox.TabIndex = 24;
            this.xmateriaalcheckbox.Text = "Bevat Materiaal";
            this.xmateriaalcheckbox.UseSelectable = true;
            this.xmateriaalcheckbox.CheckedChanged += new System.EventHandler(this.xartikelnrcheck_CheckedChanged);
            // 
            // xtotcheck
            // 
            this.xtotcheck.AutoSize = true;
            this.xtotcheck.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xtotcheck.Location = new System.Drawing.Point(9, 190);
            this.xtotcheck.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xtotcheck.Name = "xtotcheck";
            this.xtotcheck.Size = new System.Drawing.Size(106, 19);
            this.xtotcheck.TabIndex = 23;
            this.xtotcheck.Text = "Gewijzigd Tot";
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
            this.xtotdate.Location = new System.Drawing.Point(135, 188);
            this.xtotdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xtotdate.Name = "xtotdate";
            this.xtotdate.Size = new System.Drawing.Size(506, 25);
            this.xtotdate.TabIndex = 22;
            this.toolTip1.SetToolTip(this.xtotdate, "Vul in de einddatum waarop een productie laats op is gewijzigd");
            // 
            // xvanafcheck
            // 
            this.xvanafcheck.AutoSize = true;
            this.xvanafcheck.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xvanafcheck.Location = new System.Drawing.Point(9, 158);
            this.xvanafcheck.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xvanafcheck.Name = "xvanafcheck";
            this.xvanafcheck.Size = new System.Drawing.Size(121, 19);
            this.xvanafcheck.TabIndex = 21;
            this.xvanafcheck.Text = "Gewijzigd Vanaf";
            this.xvanafcheck.UseSelectable = true;
            this.xvanafcheck.CheckedChanged += new System.EventHandler(this.xvanafcheck_CheckedChanged);
            // 
            // xvanafdate
            // 
            this.xvanafdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xvanafdate.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xvanafdate.Enabled = false;
            this.xvanafdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvanafdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xvanafdate.Location = new System.Drawing.Point(135, 156);
            this.xvanafdate.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xvanafdate.Name = "xvanafdate";
            this.xvanafdate.Size = new System.Drawing.Size(506, 25);
            this.xvanafdate.TabIndex = 20;
            this.toolTip1.SetToolTip(this.xvanafdate, "Vul in de startdatum waarop een productie laats op is gewijzigd.");
            // 
            // xbewerkingcheck
            // 
            this.xbewerkingcheck.AutoSize = true;
            this.xbewerkingcheck.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xbewerkingcheck.Location = new System.Drawing.Point(9, 51);
            this.xbewerkingcheck.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xbewerkingcheck.Name = "xbewerkingcheck";
            this.xbewerkingcheck.Size = new System.Drawing.Size(88, 19);
            this.xbewerkingcheck.TabIndex = 19;
            this.xbewerkingcheck.Text = "Bewerking";
            this.xbewerkingcheck.UseSelectable = true;
            this.xbewerkingcheck.CheckedChanged += new System.EventHandler(this.xbewerkingcheck_CheckedChanged);
            // 
            // xwerkplekcheck
            // 
            this.xwerkplekcheck.AutoSize = true;
            this.xwerkplekcheck.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xwerkplekcheck.Location = new System.Drawing.Point(9, 13);
            this.xwerkplekcheck.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xwerkplekcheck.Name = "xwerkplekcheck";
            this.xwerkplekcheck.Size = new System.Drawing.Size(85, 19);
            this.xwerkplekcheck.TabIndex = 18;
            this.xwerkplekcheck.Text = "Werk plek";
            this.xwerkplekcheck.UseSelectable = true;
            this.xwerkplekcheck.CheckedChanged += new System.EventHandler(this.xwerkplekcheck_CheckedChanged);
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
            this.xbewerkingen.Location = new System.Drawing.Point(100, 45);
            this.xbewerkingen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xbewerkingen.Name = "xbewerkingen";
            this.xbewerkingen.Size = new System.Drawing.Size(541, 29);
            this.xbewerkingen.TabIndex = 16;
            this.toolTip1.SetToolTip(this.xbewerkingen, "Kies een bewerking om te zoeken");
            this.xbewerkingen.UseSelectable = true;
            // 
            // xwerkplekken
            // 
            this.xwerkplekken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xwerkplekken.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.xwerkplekken.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.xwerkplekken.Enabled = false;
            this.xwerkplekken.FormattingEnabled = true;
            this.xwerkplekken.ItemHeight = 23;
            this.xwerkplekken.Location = new System.Drawing.Point(100, 8);
            this.xwerkplekken.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xwerkplekken.Name = "xwerkplekken";
            this.xwerkplekken.Size = new System.Drawing.Size(541, 29);
            this.xwerkplekken.TabIndex = 15;
            this.toolTip1.SetToolTip(this.xwerkplekken, "Kies een werkplek om op te zoeken");
            this.xwerkplekken.UseSelectable = true;
            // 
            // xcriteria
            // 
            // 
            // 
            // 
            this.xcriteria.CustomButton.Image = null;
            this.xcriteria.CustomButton.Location = new System.Drawing.Point(610, 2);
            this.xcriteria.CustomButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xcriteria.CustomButton.Name = "";
            this.xcriteria.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.xcriteria.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xcriteria.CustomButton.TabIndex = 1;
            this.xcriteria.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xcriteria.CustomButton.UseSelectable = true;
            this.xcriteria.CustomButton.Visible = false;
            this.xcriteria.Dock = System.Windows.Forms.DockStyle.Top;
            this.xcriteria.Lines = new string[0];
            this.xcriteria.Location = new System.Drawing.Point(3, 22);
            this.xcriteria.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xcriteria.MaxLength = 32767;
            this.xcriteria.Name = "xcriteria";
            this.xcriteria.PasswordChar = '\0';
            this.xcriteria.PromptText = "Vul in een criteria...";
            this.xcriteria.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xcriteria.SelectedText = "";
            this.xcriteria.SelectionLength = 0;
            this.xcriteria.SelectionStart = 0;
            this.xcriteria.ShortcutsEnabled = true;
            this.xcriteria.ShowClearButton = true;
            this.xcriteria.Size = new System.Drawing.Size(638, 30);
            this.xcriteria.TabIndex = 14;
            this.toolTip1.SetToolTip(this.xcriteria, "Vul in een criteria om te zoeken");
            this.xcriteria.UseSelectable = true;
            this.xcriteria.WaterMark = "Vul in een criteria...";
            this.xcriteria.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xcriteria.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xcriteria.TextChanged += new System.EventHandler(this.xcriteria_TextChanged);
            this.xcriteria.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xcriteria_KeyDown);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xcriteria);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(23, 78);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(644, 57);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Zoek een criteria van een  Artikelnr, Productienr of een omschrijving";
            // 
            // xgeavanceerd
            // 
            this.xgeavanceerd.AutoSize = true;
            this.xgeavanceerd.Dock = System.Windows.Forms.DockStyle.Top;
            this.xgeavanceerd.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xgeavanceerd.FontWeight = MetroFramework.MetroCheckBoxWeight.Bold;
            this.xgeavanceerd.Location = new System.Drawing.Point(23, 135);
            this.xgeavanceerd.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xgeavanceerd.Name = "xgeavanceerd";
            this.xgeavanceerd.Size = new System.Drawing.Size(644, 19);
            this.xgeavanceerd.TabIndex = 19;
            this.xgeavanceerd.Text = "Geavanceerd";
            this.toolTip1.SetToolTip(this.xgeavanceerd, "Gebruik geavanceerde voorkeuren");
            this.xgeavanceerd.UseSelectable = true;
            this.xgeavanceerd.CheckedChanged += new System.EventHandler(this.xgeavanceerd_CheckedChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // ZoekForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(690, 440);
            this.Controls.Add(this.xgeavanceerdpannel);
            this.Controls.Add(this.xgeavanceerd);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(500, 234);
            this.Name = "ZoekForm";
            this.Padding = new System.Windows.Forms.Padding(23, 78, 23, 26);
            this.SaveLastSize = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Zoeken...";
            this.Title = "Zoeken...";
            this.panel1.ResumeLayout(false);
            this.xgeavanceerdpannel.ResumeLayout(false);
            this.xgeavanceerdpannel.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel xgeavanceerdpannel;
        private MetroFramework.Controls.MetroCheckBox xtotcheck;
        private System.Windows.Forms.DateTimePicker xtotdate;
        private MetroFramework.Controls.MetroCheckBox xvanafcheck;
        private System.Windows.Forms.DateTimePicker xvanafdate;
        private MetroFramework.Controls.MetroCheckBox xbewerkingcheck;
        private MetroFramework.Controls.MetroCheckBox xwerkplekcheck;
        private MetroFramework.Controls.MetroComboBox xbewerkingen;
        private MetroFramework.Controls.MetroComboBox xwerkplekken;
        private MetroFramework.Controls.MetroTextBox xcriteria;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button xzoeken;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button xsluiten;
        private MetroFramework.Controls.MetroTextBox xmateriaalcriteria;
        private MetroFramework.Controls.MetroCheckBox xmateriaalcheckbox;
        private MetroFramework.Controls.MetroCheckBox xgeavanceerd;
        private MetroFramework.Controls.MetroCheckBox xstatuscheckbox;
        private MetroFramework.Controls.MetroComboBox xstatuscombo;
    }
}