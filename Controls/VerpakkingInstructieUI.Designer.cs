
using TheArtOfDev.HtmlRenderer.WinForms;

namespace Controls
{
    partial class VerpakkingInstructieUI
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
            this.xeditpanel = new System.Windows.Forms.Panel();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.xbulklocatie = new MetroFramework.Controls.MetroTextBox();
            this.xstandaardlocatie = new MetroFramework.Controls.MetroTextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.xaantelpercolli = new System.Windows.Forms.NumericUpDown();
            this.label5 = new System.Windows.Forms.Label();
            this.xdozenpercolli = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.xdozenperlaag = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.xlagenpercolli = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.xverpakkenper = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xpalletsoort = new MetroFramework.Controls.MetroTextBox();
            this.xverpakkingsoort = new MetroFramework.Controls.MetroTextBox();
            this.htmlPanel1 = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.xwijzigPanel = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xwijzig = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xeditpanel.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xaantelpercolli)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xdozenpercolli)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xdozenperlaag)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xlagenpercolli)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xverpakkenper)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.xwijzigPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // xeditpanel
            // 
            this.xeditpanel.AutoScroll = true;
            this.xeditpanel.Controls.Add(this.groupBox3);
            this.xeditpanel.Controls.Add(this.groupBox2);
            this.xeditpanel.Controls.Add(this.groupBox1);
            this.xeditpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xeditpanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xeditpanel.Location = new System.Drawing.Point(5, 5);
            this.xeditpanel.Name = "xeditpanel";
            this.xeditpanel.Size = new System.Drawing.Size(538, 303);
            this.xeditpanel.TabIndex = 0;
            this.xeditpanel.Visible = false;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.xbulklocatie);
            this.groupBox3.Controls.Add(this.xstandaardlocatie);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox3.Location = new System.Drawing.Point(0, 207);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(538, 86);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Locatie";
            // 
            // xbulklocatie
            // 
            this.xbulklocatie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.xbulklocatie.CustomButton.Image = null;
            this.xbulklocatie.CustomButton.Location = new System.Drawing.Point(504, 1);
            this.xbulklocatie.CustomButton.Name = "";
            this.xbulklocatie.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xbulklocatie.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xbulklocatie.CustomButton.TabIndex = 1;
            this.xbulklocatie.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xbulklocatie.CustomButton.UseSelectable = true;
            this.xbulklocatie.CustomButton.Visible = false;
            this.xbulklocatie.Lines = new string[0];
            this.xbulklocatie.Location = new System.Drawing.Point(6, 57);
            this.xbulklocatie.MaxLength = 32767;
            this.xbulklocatie.Name = "xbulklocatie";
            this.xbulklocatie.PasswordChar = '\0';
            this.xbulklocatie.PromptText = "Type in een bulk locatie...";
            this.xbulklocatie.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xbulklocatie.SelectedText = "";
            this.xbulklocatie.SelectionLength = 0;
            this.xbulklocatie.SelectionStart = 0;
            this.xbulklocatie.ShortcutsEnabled = true;
            this.xbulklocatie.ShowClearButton = true;
            this.xbulklocatie.Size = new System.Drawing.Size(526, 23);
            this.xbulklocatie.TabIndex = 8;
            this.toolTip1.SetToolTip(this.xbulklocatie, "Type in een bulk locatie");
            this.xbulklocatie.UseSelectable = true;
            this.xbulklocatie.WaterMark = "Type in een bulk locatie...";
            this.xbulklocatie.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xbulklocatie.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // xstandaardlocatie
            // 
            this.xstandaardlocatie.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.xstandaardlocatie.CustomButton.Image = null;
            this.xstandaardlocatie.CustomButton.Location = new System.Drawing.Point(504, 1);
            this.xstandaardlocatie.CustomButton.Name = "";
            this.xstandaardlocatie.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xstandaardlocatie.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xstandaardlocatie.CustomButton.TabIndex = 1;
            this.xstandaardlocatie.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xstandaardlocatie.CustomButton.UseSelectable = true;
            this.xstandaardlocatie.CustomButton.Visible = false;
            this.xstandaardlocatie.Lines = new string[0];
            this.xstandaardlocatie.Location = new System.Drawing.Point(6, 28);
            this.xstandaardlocatie.MaxLength = 32767;
            this.xstandaardlocatie.Name = "xstandaardlocatie";
            this.xstandaardlocatie.PasswordChar = '\0';
            this.xstandaardlocatie.PromptText = "Type in een standaard locatie...";
            this.xstandaardlocatie.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xstandaardlocatie.SelectedText = "";
            this.xstandaardlocatie.SelectionLength = 0;
            this.xstandaardlocatie.SelectionStart = 0;
            this.xstandaardlocatie.ShortcutsEnabled = true;
            this.xstandaardlocatie.ShowClearButton = true;
            this.xstandaardlocatie.Size = new System.Drawing.Size(526, 23);
            this.xstandaardlocatie.TabIndex = 7;
            this.toolTip1.SetToolTip(this.xstandaardlocatie, "Type in een standaard locatie");
            this.xstandaardlocatie.UseSelectable = true;
            this.xstandaardlocatie.WaterMark = "Type in een standaard locatie...";
            this.xstandaardlocatie.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xstandaardlocatie.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.xaantelpercolli);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.xdozenpercolli);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.xdozenperlaag);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.xlagenpercolli);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.xverpakkenper);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(0, 89);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(538, 118);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Verpakking Aantallen";
            // 
            // xaantelpercolli
            // 
            this.xaantelpercolli.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantelpercolli.Location = new System.Drawing.Point(326, 54);
            this.xaantelpercolli.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantelpercolli.Name = "xaantelpercolli";
            this.xaantelpercolli.Size = new System.Drawing.Size(92, 25);
            this.xaantelpercolli.TabIndex = 6;
            this.toolTip1.SetToolTip(this.xaantelpercolli, "Aantal producten per colli");
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(217, 56);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(103, 17);
            this.label5.TabIndex = 9;
            this.label5.Text = "Aantal Per Colli: ";
            // 
            // xdozenpercolli
            // 
            this.xdozenpercolli.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdozenpercolli.Location = new System.Drawing.Point(119, 85);
            this.xdozenpercolli.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.xdozenpercolli.Name = "xdozenpercolli";
            this.xdozenpercolli.Size = new System.Drawing.Size(92, 25);
            this.xdozenpercolli.TabIndex = 4;
            this.toolTip1.SetToolTip(this.xdozenpercolli, "De aantal dozen per colli");
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 87);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "Dozen Per Colli: ";
            // 
            // xdozenperlaag
            // 
            this.xdozenperlaag.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdozenperlaag.Location = new System.Drawing.Point(119, 54);
            this.xdozenperlaag.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.xdozenperlaag.Name = "xdozenperlaag";
            this.xdozenperlaag.Size = new System.Drawing.Size(92, 25);
            this.xdozenperlaag.TabIndex = 3;
            this.toolTip1.SetToolTip(this.xdozenperlaag, "De aantal dozen/bakken per colli");
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 56);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Dozen Per Laag: ";
            // 
            // xlagenpercolli
            // 
            this.xlagenpercolli.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlagenpercolli.Location = new System.Drawing.Point(119, 23);
            this.xlagenpercolli.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.xlagenpercolli.Name = "xlagenpercolli";
            this.xlagenpercolli.Size = new System.Drawing.Size(92, 25);
            this.xlagenpercolli.TabIndex = 2;
            this.toolTip1.SetToolTip(this.xlagenpercolli, "De aantal lagen op een colli");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Lagen Per Colli: ";
            // 
            // xverpakkenper
            // 
            this.xverpakkenper.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xverpakkenper.Location = new System.Drawing.Point(326, 23);
            this.xverpakkenper.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xverpakkenper.Name = "xverpakkenper";
            this.xverpakkenper.Size = new System.Drawing.Size(92, 25);
            this.xverpakkenper.TabIndex = 5;
            this.toolTip1.SetToolTip(this.xverpakkenper, "Aantal per verpakking (doos,bak ect)");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(217, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 0;
            this.label2.Text = "Verpakken Per: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xpalletsoort);
            this.groupBox1.Controls.Add(this.xverpakkingsoort);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(538, 89);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Verpakking Soort";
            // 
            // xpalletsoort
            // 
            this.xpalletsoort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.xpalletsoort.CustomButton.Image = null;
            this.xpalletsoort.CustomButton.Location = new System.Drawing.Point(504, 1);
            this.xpalletsoort.CustomButton.Name = "";
            this.xpalletsoort.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xpalletsoort.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xpalletsoort.CustomButton.TabIndex = 1;
            this.xpalletsoort.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xpalletsoort.CustomButton.UseSelectable = true;
            this.xpalletsoort.CustomButton.Visible = false;
            this.xpalletsoort.Lines = new string[0];
            this.xpalletsoort.Location = new System.Drawing.Point(6, 57);
            this.xpalletsoort.MaxLength = 32767;
            this.xpalletsoort.Name = "xpalletsoort";
            this.xpalletsoort.PasswordChar = '\0';
            this.xpalletsoort.PromptText = "Type in een pallet soort...";
            this.xpalletsoort.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xpalletsoort.SelectedText = "";
            this.xpalletsoort.SelectionLength = 0;
            this.xpalletsoort.SelectionStart = 0;
            this.xpalletsoort.ShortcutsEnabled = true;
            this.xpalletsoort.ShowClearButton = true;
            this.xpalletsoort.Size = new System.Drawing.Size(526, 23);
            this.xpalletsoort.TabIndex = 1;
            this.toolTip1.SetToolTip(this.xpalletsoort, "Type in een pallet soort");
            this.xpalletsoort.UseSelectable = true;
            this.xpalletsoort.WaterMark = "Type in een pallet soort...";
            this.xpalletsoort.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xpalletsoort.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // xverpakkingsoort
            // 
            this.xverpakkingsoort.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.xverpakkingsoort.CustomButton.Image = null;
            this.xverpakkingsoort.CustomButton.Location = new System.Drawing.Point(504, 1);
            this.xverpakkingsoort.CustomButton.Name = "";
            this.xverpakkingsoort.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xverpakkingsoort.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xverpakkingsoort.CustomButton.TabIndex = 1;
            this.xverpakkingsoort.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xverpakkingsoort.CustomButton.UseSelectable = true;
            this.xverpakkingsoort.CustomButton.Visible = false;
            this.xverpakkingsoort.Lines = new string[0];
            this.xverpakkingsoort.Location = new System.Drawing.Point(6, 28);
            this.xverpakkingsoort.MaxLength = 32767;
            this.xverpakkingsoort.Name = "xverpakkingsoort";
            this.xverpakkingsoort.PasswordChar = '\0';
            this.xverpakkingsoort.PromptText = "Type in een verpakking soort...";
            this.xverpakkingsoort.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xverpakkingsoort.SelectedText = "";
            this.xverpakkingsoort.SelectionLength = 0;
            this.xverpakkingsoort.SelectionStart = 0;
            this.xverpakkingsoort.ShortcutsEnabled = true;
            this.xverpakkingsoort.ShowClearButton = true;
            this.xverpakkingsoort.Size = new System.Drawing.Size(526, 23);
            this.xverpakkingsoort.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xverpakkingsoort, "Type in een verpakking soort");
            this.xverpakkingsoort.UseSelectable = true;
            this.xverpakkingsoort.WaterMark = "Type in een verpakking soort...";
            this.xverpakkingsoort.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xverpakkingsoort.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // htmlPanel1
            // 
            this.htmlPanel1.AutoScroll = true;
            this.htmlPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.htmlPanel1.BaseStylesheet = null;
            this.htmlPanel1.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.htmlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.htmlPanel1.Location = new System.Drawing.Point(5, 5);
            this.htmlPanel1.Name = "htmlPanel1";
            this.htmlPanel1.Size = new System.Drawing.Size(538, 303);
            this.htmlPanel1.TabIndex = 11;
            this.htmlPanel1.StylesheetLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlStylesheetLoadEventArgs>(this.htmlPanel1_StylesheetLoad);
            this.htmlPanel1.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.htmlPanel1_ImageLoad);
            // 
            // xwijzigPanel
            // 
            this.xwijzigPanel.Controls.Add(this.xsluiten);
            this.xwijzigPanel.Controls.Add(this.xwijzig);
            this.xwijzigPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xwijzigPanel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xwijzigPanel.Location = new System.Drawing.Point(5, 308);
            this.xwijzigPanel.Name = "xwijzigPanel";
            this.xwijzigPanel.Size = new System.Drawing.Size(538, 40);
            this.xwijzigPanel.TabIndex = 1;
            this.xwijzigPanel.Visible = false;
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(269, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(135, 34);
            this.xsluiten.TabIndex = 9;
            this.xsluiten.Text = "Annuleren";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xsluiten, "Sluit venster");
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xwijzig
            // 
            this.xwijzig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xwijzig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xwijzig.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xwijzig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xwijzig.Location = new System.Drawing.Point(410, 3);
            this.xwijzig.Name = "xwijzig";
            this.xwijzig.Size = new System.Drawing.Size(122, 35);
            this.xwijzig.TabIndex = 10;
            this.xwijzig.Text = "Wijzig";
            this.xwijzig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xwijzig, "Wijzig gegevens");
            this.xwijzig.UseVisualStyleBackColor = true;
            this.xwijzig.Click += new System.EventHandler(this.xwijzig_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // VerpakkingInstructieUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xeditpanel);
            this.Controls.Add(this.htmlPanel1);
            this.Controls.Add(this.xwijzigPanel);
            this.Name = "VerpakkingInstructieUI";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(548, 353);
            this.xeditpanel.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xaantelpercolli)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xdozenpercolli)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xdozenperlaag)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xlagenpercolli)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xverpakkenper)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.xwijzigPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel xeditpanel;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox1;
        private MetroFramework.Controls.MetroTextBox xverpakkingsoort;
        private System.Windows.Forms.NumericUpDown xaantelpercolli;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown xdozenpercolli;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown xdozenperlaag;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown xlagenpercolli;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown xverpakkenper;
        private MetroFramework.Controls.MetroTextBox xbulklocatie;
        private MetroFramework.Controls.MetroTextBox xstandaardlocatie;
        private MetroFramework.Controls.MetroTextBox xpalletsoort;
        private HtmlPanel htmlPanel1;
        private System.Windows.Forms.Panel xwijzigPanel;
        private System.Windows.Forms.Button xwijzig;
        private System.Windows.Forms.Button xsluiten;
    }
}
