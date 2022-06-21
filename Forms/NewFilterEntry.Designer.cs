
using TheArtOfDev.HtmlRenderer.WinForms;

namespace ProductieManager.Forms
{
    partial class NewFilterEntry
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
            this.xannuleren = new MetroFramework.Controls.MetroButton();
            this.xok = new MetroFramework.Controls.MetroButton();
            this.xoperandtype = new MetroFramework.Controls.MetroComboBox();
            this.xvariablenamelabel = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.xvaluetypes = new MetroFramework.Controls.MetroComboBox();
            this.xtextvalue = new MetroFramework.Controls.MetroTextBox();
            this.xvaluepanel = new MetroFramework.Controls.MetroPanel();
            this.xdatepanel = new System.Windows.Forms.Panel();
            this.xdezeweekcheckbox = new System.Windows.Forms.CheckBox();
            this.xdatevalue = new System.Windows.Forms.DateTimePicker();
            this.xhuidigeDatum = new System.Windows.Forms.CheckBox();
            this.xhuidigeTijd = new System.Windows.Forms.CheckBox();
            this.xcheckvalue = new MetroFramework.Controls.MetroCheckBox();
            this.xdecimalvalue = new System.Windows.Forms.NumericUpDown();
            this.xComboPanel = new System.Windows.Forms.Panel();
            this.xcombovalue = new MetroFramework.Controls.MetroComboBox();
            this.xEditValueRangePanel = new System.Windows.Forms.Panel();
            this.xRangeDevider = new MetroFramework.Controls.MetroComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xVergelijkVariableCheck = new System.Windows.Forms.RadioButton();
            this.xVergelijkWaardeCheck = new System.Windows.Forms.RadioButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xcriteriahtml = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.xvoorwaardenb = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.metroPanel1.SuspendLayout();
            this.xvaluepanel.SuspendLayout();
            this.xdatepanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xdecimalvalue)).BeginInit();
            this.xComboPanel.SuspendLayout();
            this.xEditValueRangePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xannuleren
            // 
            this.xannuleren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xannuleren.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xannuleren.Location = new System.Drawing.Point(432, 6);
            this.xannuleren.Name = "xannuleren";
            this.xannuleren.Size = new System.Drawing.Size(100, 34);
            this.xannuleren.TabIndex = 0;
            this.xannuleren.Text = "Annuleren";
            this.toolTip1.SetToolTip(this.xannuleren, "Annuleren");
            this.xannuleren.UseSelectable = true;
            this.xannuleren.Click += new System.EventHandler(this.xannuleren_Click);
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xok.Location = new System.Drawing.Point(326, 6);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(100, 34);
            this.xok.TabIndex = 1;
            this.xok.Text = "OK";
            this.toolTip1.SetToolTip(this.xok, "Opslaan");
            this.xok.UseSelectable = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // xoperandtype
            // 
            this.xoperandtype.Dock = System.Windows.Forms.DockStyle.Left;
            this.xoperandtype.FormattingEnabled = true;
            this.xoperandtype.ItemHeight = 23;
            this.xoperandtype.Location = new System.Drawing.Point(0, 0);
            this.xoperandtype.Name = "xoperandtype";
            this.xoperandtype.Size = new System.Drawing.Size(79, 29);
            this.xoperandtype.TabIndex = 2;
            this.toolTip1.SetToolTip(this.xoperandtype, "Kies of de criteria \'EN\', \'OF\' of \'ALS\', als de andere waarde moet zijn. ");
            this.xoperandtype.UseSelectable = true;
            this.xoperandtype.SelectedIndexChanged += new System.EventHandler(this.xoperandtype_SelectedIndexChanged);
            // 
            // xvariablenamelabel
            // 
            this.xvariablenamelabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.xvariablenamelabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.xvariablenamelabel.Location = new System.Drawing.Point(79, 0);
            this.xvariablenamelabel.Name = "xvariablenamelabel";
            this.xvariablenamelabel.Size = new System.Drawing.Size(180, 31);
            this.xvariablenamelabel.TabIndex = 3;
            this.xvariablenamelabel.Text = "Naam Variable";
            this.xvariablenamelabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.xvaluetypes);
            this.metroPanel1.Controls.Add(this.xvariablenamelabel);
            this.metroPanel1.Controls.Add(this.xoperandtype);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 60);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(535, 31);
            this.metroPanel1.TabIndex = 5;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // xvaluetypes
            // 
            this.xvaluetypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xvaluetypes.FormattingEnabled = true;
            this.xvaluetypes.ItemHeight = 23;
            this.xvaluetypes.Location = new System.Drawing.Point(259, 0);
            this.xvaluetypes.Name = "xvaluetypes";
            this.xvaluetypes.Size = new System.Drawing.Size(276, 29);
            this.xvaluetypes.TabIndex = 5;
            this.toolTip1.SetToolTip(this.xvaluetypes, "Kies een type van HOE de criteria vergeleken moet worden");
            this.xvaluetypes.UseSelectable = true;
            this.xvaluetypes.SelectedIndexChanged += new System.EventHandler(this.xvaluetypes_SelectedIndexChanged);
            // 
            // xtextvalue
            // 
            // 
            // 
            // 
            this.xtextvalue.CustomButton.Image = null;
            this.xtextvalue.CustomButton.Location = new System.Drawing.Point(507, 1);
            this.xtextvalue.CustomButton.Name = "";
            this.xtextvalue.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.xtextvalue.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xtextvalue.CustomButton.TabIndex = 1;
            this.xtextvalue.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xtextvalue.CustomButton.UseSelectable = true;
            this.xtextvalue.CustomButton.Visible = false;
            this.xtextvalue.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtextvalue.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xtextvalue.ForeColor = System.Drawing.Color.Gray;
            this.xtextvalue.Lines = new string[] {
        "Vul in een criteria..."};
            this.xtextvalue.Location = new System.Drawing.Point(0, 67);
            this.xtextvalue.MaxLength = 32767;
            this.xtextvalue.Name = "xtextvalue";
            this.xtextvalue.PasswordChar = '\0';
            this.xtextvalue.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xtextvalue.SelectedText = "";
            this.xtextvalue.SelectionLength = 0;
            this.xtextvalue.SelectionStart = 0;
            this.xtextvalue.ShortcutsEnabled = true;
            this.xtextvalue.ShowClearButton = true;
            this.xtextvalue.Size = new System.Drawing.Size(535, 29);
            this.xtextvalue.TabIndex = 6;
            this.xtextvalue.Text = "Vul in een criteria...";
            this.toolTip1.SetToolTip(this.xtextvalue, "Vul in een text waaraan de criteria moet voldoen");
            this.xtextvalue.UseSelectable = true;
            this.xtextvalue.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xtextvalue.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xtextvalue.TextChanged += new System.EventHandler(this.xtextvalue_TextChanged);
            this.xtextvalue.Enter += new System.EventHandler(this.xtextvalue_Enter);
            this.xtextvalue.Leave += new System.EventHandler(this.xtextvalue_Leave);
            // 
            // xvaluepanel
            // 
            this.xvaluepanel.Controls.Add(this.xdatepanel);
            this.xvaluepanel.Controls.Add(this.xcheckvalue);
            this.xvaluepanel.Controls.Add(this.xdecimalvalue);
            this.xvaluepanel.Controls.Add(this.xtextvalue);
            this.xvaluepanel.Controls.Add(this.xComboPanel);
            this.xvaluepanel.Controls.Add(this.panel3);
            this.xvaluepanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xvaluepanel.HorizontalScrollbarBarColor = true;
            this.xvaluepanel.HorizontalScrollbarHighlightOnWheel = false;
            this.xvaluepanel.HorizontalScrollbarSize = 10;
            this.xvaluepanel.Location = new System.Drawing.Point(20, 91);
            this.xvaluepanel.Name = "xvaluepanel";
            this.xvaluepanel.Size = new System.Drawing.Size(535, 200);
            this.xvaluepanel.TabIndex = 7;
            this.xvaluepanel.VerticalScrollbarBarColor = true;
            this.xvaluepanel.VerticalScrollbarHighlightOnWheel = false;
            this.xvaluepanel.VerticalScrollbarSize = 10;
            // 
            // xdatepanel
            // 
            this.xdatepanel.Controls.Add(this.xdezeweekcheckbox);
            this.xdatepanel.Controls.Add(this.xdatevalue);
            this.xdatepanel.Controls.Add(this.xhuidigeDatum);
            this.xdatepanel.Controls.Add(this.xhuidigeTijd);
            this.xdatepanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xdatepanel.Location = new System.Drawing.Point(0, 142);
            this.xdatepanel.Name = "xdatepanel";
            this.xdatepanel.Size = new System.Drawing.Size(535, 52);
            this.xdatepanel.TabIndex = 11;
            this.xdatepanel.Visible = false;
            // 
            // xdezeweekcheckbox
            // 
            this.xdezeweekcheckbox.AutoSize = true;
            this.xdezeweekcheckbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdezeweekcheckbox.Location = new System.Drawing.Point(3, 13);
            this.xdezeweekcheckbox.Name = "xdezeweekcheckbox";
            this.xdezeweekcheckbox.Size = new System.Drawing.Size(95, 21);
            this.xdezeweekcheckbox.TabIndex = 11;
            this.xdezeweekcheckbox.Text = "Deze Week";
            this.toolTip1.SetToolTip(this.xdezeweekcheckbox, "KIes dit voor als je de criteria wilt vergelijken met de huidige datum");
            this.xdezeweekcheckbox.UseVisualStyleBackColor = true;
            this.xdezeweekcheckbox.CheckedChanged += new System.EventHandler(this.xdezeweekcheckbox_CheckedChanged);
            // 
            // xdatevalue
            // 
            this.xdatevalue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xdatevalue.CalendarFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdatevalue.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xdatevalue.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdatevalue.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xdatevalue.Location = new System.Drawing.Point(231, 13);
            this.xdatevalue.MinimumSize = new System.Drawing.Size(4, 29);
            this.xdatevalue.Name = "xdatevalue";
            this.xdatevalue.Size = new System.Drawing.Size(301, 29);
            this.xdatevalue.TabIndex = 8;
            this.toolTip1.SetToolTip(this.xdatevalue, "Vul in de datum en tijd waaraan de criteria moet voldoen");
            this.xdatevalue.ValueChanged += new System.EventHandler(this.xdatevalue_ValueChanged);
            // 
            // xhuidigeDatum
            // 
            this.xhuidigeDatum.AutoSize = true;
            this.xhuidigeDatum.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xhuidigeDatum.Location = new System.Drawing.Point(104, 3);
            this.xhuidigeDatum.Name = "xhuidigeDatum";
            this.xhuidigeDatum.Size = new System.Drawing.Size(122, 21);
            this.xhuidigeDatum.TabIndex = 10;
            this.xhuidigeDatum.Text = "Huidige Datum";
            this.toolTip1.SetToolTip(this.xhuidigeDatum, "KIes dit voor als je de criteria wilt vergelijken met de huidige datum");
            this.xhuidigeDatum.UseVisualStyleBackColor = true;
            this.xhuidigeDatum.CheckStateChanged += new System.EventHandler(this.xhuidigeDatum_CheckStateChanged);
            // 
            // xhuidigeTijd
            // 
            this.xhuidigeTijd.AutoSize = true;
            this.xhuidigeTijd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xhuidigeTijd.Location = new System.Drawing.Point(104, 28);
            this.xhuidigeTijd.Name = "xhuidigeTijd";
            this.xhuidigeTijd.Size = new System.Drawing.Size(104, 21);
            this.xhuidigeTijd.TabIndex = 9;
            this.xhuidigeTijd.Text = "Huidige Tijd";
            this.toolTip1.SetToolTip(this.xhuidigeTijd, "KIes dit voor als je de criteria wilt vergelijken met de huidige tijd ");
            this.xhuidigeTijd.UseVisualStyleBackColor = true;
            this.xhuidigeTijd.CheckedChanged += new System.EventHandler(this.xhuidigeTijd_CheckedChanged);
            // 
            // xcheckvalue
            // 
            this.xcheckvalue.AutoSize = true;
            this.xcheckvalue.Dock = System.Windows.Forms.DockStyle.Top;
            this.xcheckvalue.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xcheckvalue.Location = new System.Drawing.Point(0, 123);
            this.xcheckvalue.Name = "xcheckvalue";
            this.xcheckvalue.Size = new System.Drawing.Size(535, 19);
            this.xcheckvalue.TabIndex = 10;
            this.xcheckvalue.Text = "NIET WAAR";
            this.toolTip1.SetToolTip(this.xcheckvalue, "Vergelijk de criteria met waar of niet waar");
            this.xcheckvalue.UseSelectable = true;
            this.xcheckvalue.CheckedChanged += new System.EventHandler(this.xcheckvalue_CheckedChanged);
            // 
            // xdecimalvalue
            // 
            this.xdecimalvalue.Dock = System.Windows.Forms.DockStyle.Top;
            this.xdecimalvalue.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdecimalvalue.Location = new System.Drawing.Point(0, 96);
            this.xdecimalvalue.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.xdecimalvalue.Minimum = new decimal(new int[] {
            1410065407,
            2,
            0,
            -2147483648});
            this.xdecimalvalue.Name = "xdecimalvalue";
            this.xdecimalvalue.Size = new System.Drawing.Size(535, 27);
            this.xdecimalvalue.TabIndex = 9;
            this.xdecimalvalue.ThousandsSeparator = true;
            this.toolTip1.SetToolTip(this.xdecimalvalue, "Vul in de waarde waar de criteria mee vergeleken moet worden");
            this.xdecimalvalue.ValueChanged += new System.EventHandler(this.xdecimalvalue_ValueChanged);
            // 
            // xComboPanel
            // 
            this.xComboPanel.Controls.Add(this.xcombovalue);
            this.xComboPanel.Controls.Add(this.xEditValueRangePanel);
            this.xComboPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xComboPanel.Location = new System.Drawing.Point(0, 33);
            this.xComboPanel.Name = "xComboPanel";
            this.xComboPanel.Size = new System.Drawing.Size(535, 34);
            this.xComboPanel.TabIndex = 14;
            // 
            // xcombovalue
            // 
            this.xcombovalue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xcombovalue.FormattingEnabled = true;
            this.xcombovalue.ItemHeight = 23;
            this.xcombovalue.Location = new System.Drawing.Point(0, 0);
            this.xcombovalue.Name = "xcombovalue";
            this.xcombovalue.Size = new System.Drawing.Size(212, 29);
            this.xcombovalue.TabIndex = 7;
            this.toolTip1.SetToolTip(this.xcombovalue, "Kies een waarde waaraan de criteria moet voldoen.");
            this.xcombovalue.UseSelectable = true;
            this.xcombovalue.SelectedIndexChanged += new System.EventHandler(this.xcombovalue_SelectedIndexChanged);
            // 
            // xEditValueRangePanel
            // 
            this.xEditValueRangePanel.Controls.Add(this.xRangeDevider);
            this.xEditValueRangePanel.Controls.Add(this.label1);
            this.xEditValueRangePanel.Controls.Add(this.numericUpDown1);
            this.xEditValueRangePanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.xEditValueRangePanel.Location = new System.Drawing.Point(212, 0);
            this.xEditValueRangePanel.Name = "xEditValueRangePanel";
            this.xEditValueRangePanel.Size = new System.Drawing.Size(323, 34);
            this.xEditValueRangePanel.TabIndex = 8;
            this.xEditValueRangePanel.Visible = false;
            // 
            // xRangeDevider
            // 
            this.xRangeDevider.FormattingEnabled = true;
            this.xRangeDevider.ItemHeight = 23;
            this.xRangeDevider.Location = new System.Drawing.Point(78, 2);
            this.xRangeDevider.Name = "xRangeDevider";
            this.xRangeDevider.Size = new System.Drawing.Size(125, 29);
            this.xRangeDevider.TabIndex = 8;
            this.toolTip1.SetToolTip(this.xRangeDevider, "Kies een waarde waaraan de criteria moet voldoen.");
            this.xRangeDevider.UseSelectable = true;
            this.xRangeDevider.SelectedIndexChanged += new System.EventHandler(this.xRangeDevider_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(5, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(70, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Verreken :";
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.numericUpDown1.Location = new System.Drawing.Point(208, 2);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(113, 29);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.ThousandsSeparator = true;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDown1_ValueChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xVergelijkVariableCheck);
            this.panel3.Controls.Add(this.xVergelijkWaardeCheck);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(535, 33);
            this.panel3.TabIndex = 13;
            // 
            // xVergelijkVariableCheck
            // 
            this.xVergelijkVariableCheck.AutoSize = true;
            this.xVergelijkVariableCheck.Location = new System.Drawing.Point(193, 6);
            this.xVergelijkVariableCheck.Name = "xVergelijkVariableCheck";
            this.xVergelijkVariableCheck.Size = new System.Drawing.Size(188, 21);
            this.xVergelijkVariableCheck.TabIndex = 1;
            this.xVergelijkVariableCheck.Text = "Vergelijk met een Variable";
            this.toolTip1.SetToolTip(this.xVergelijkVariableCheck, "Vergelijk met een Variable waarde");
            this.xVergelijkVariableCheck.UseVisualStyleBackColor = true;
            this.xVergelijkVariableCheck.CheckedChanged += new System.EventHandler(this.xVergelijkVariableCheck_CheckedChanged);
            // 
            // xVergelijkWaardeCheck
            // 
            this.xVergelijkWaardeCheck.AutoSize = true;
            this.xVergelijkWaardeCheck.Checked = true;
            this.xVergelijkWaardeCheck.Location = new System.Drawing.Point(3, 6);
            this.xVergelijkWaardeCheck.Name = "xVergelijkWaardeCheck";
            this.xVergelijkWaardeCheck.Size = new System.Drawing.Size(184, 21);
            this.xVergelijkWaardeCheck.TabIndex = 0;
            this.xVergelijkWaardeCheck.TabStop = true;
            this.xVergelijkWaardeCheck.Text = "Vergelijk met een Waarde";
            this.toolTip1.SetToolTip(this.xVergelijkWaardeCheck, "Vergelijk met een ingevoerde waarde");
            this.xVergelijkWaardeCheck.UseVisualStyleBackColor = true;
            this.xVergelijkWaardeCheck.CheckedChanged += new System.EventHandler(this.xVergelijkWaardeCheck_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xcriteriahtml);
            this.panel1.Controls.Add(this.xvoorwaardenb);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 291);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(535, 49);
            this.panel1.TabIndex = 9;
            // 
            // xcriteriahtml
            // 
            this.xcriteriahtml.AutoScroll = true;
            this.xcriteriahtml.BackColor = System.Drawing.SystemColors.Window;
            this.xcriteriahtml.BaseStylesheet = null;
            this.xcriteriahtml.Cursor = System.Windows.Forms.Cursors.Default;
            this.xcriteriahtml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xcriteriahtml.Location = new System.Drawing.Point(0, 42);
            this.xcriteriahtml.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xcriteriahtml.Name = "xcriteriahtml";
            this.xcriteriahtml.Size = new System.Drawing.Size(535, 7);
            this.xcriteriahtml.TabIndex = 8;
            this.xcriteriahtml.Text = null;
            // 
            // xvoorwaardenb
            // 
            this.xvoorwaardenb.Dock = System.Windows.Forms.DockStyle.Top;
            this.xvoorwaardenb.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.xvoorwaardenb.FlatAppearance.BorderSize = 2;
            this.xvoorwaardenb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xvoorwaardenb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvoorwaardenb.Image = global::ProductieManager.Properties.Resources.code_html_link_share_icon_123633;
            this.xvoorwaardenb.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.xvoorwaardenb.Location = new System.Drawing.Point(0, 0);
            this.xvoorwaardenb.Name = "xvoorwaardenb";
            this.xvoorwaardenb.Size = new System.Drawing.Size(535, 42);
            this.xvoorwaardenb.TabIndex = 9;
            this.xvoorwaardenb.Text = "Voorwaarden";
            this.xvoorwaardenb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xvoorwaardenb.UseVisualStyleBackColor = true;
            this.xvoorwaardenb.Click += new System.EventHandler(this.xvoorwaardenb_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xok);
            this.panel2.Controls.Add(this.xannuleren);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 340);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(535, 43);
            this.panel2.TabIndex = 10;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // NewFilterEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(575, 403);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.xvaluepanel);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.panel2);
            this.MinimumSize = new System.Drawing.Size(575, 400);
            this.Name = "NewFilterEntry";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nieuwe Filter Regel(s)";
            this.Title = "Nieuwe Filter Regel(s)";
            this.metroPanel1.ResumeLayout(false);
            this.xvaluepanel.ResumeLayout(false);
            this.xvaluepanel.PerformLayout();
            this.xdatepanel.ResumeLayout(false);
            this.xdatepanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xdecimalvalue)).EndInit();
            this.xComboPanel.ResumeLayout(false);
            this.xEditValueRangePanel.ResumeLayout(false);
            this.xEditValueRangePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton xannuleren;
        private MetroFramework.Controls.MetroButton xok;
        private MetroFramework.Controls.MetroComboBox xoperandtype;
        private MetroFramework.Controls.MetroLabel xvariablenamelabel;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroComboBox xvaluetypes;
        private MetroFramework.Controls.MetroTextBox xtextvalue;
        private MetroFramework.Controls.MetroPanel xvaluepanel;
        private System.Windows.Forms.NumericUpDown xdecimalvalue;
        private System.Windows.Forms.DateTimePicker xdatevalue;
        private MetroFramework.Controls.MetroComboBox xcombovalue;
        private MetroFramework.Controls.MetroCheckBox xcheckvalue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private HtmlPanel xcriteriahtml;
        private System.Windows.Forms.Button xvoorwaardenb;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel xdatepanel;
        private System.Windows.Forms.CheckBox xhuidigeTijd;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.RadioButton xVergelijkVariableCheck;
        private System.Windows.Forms.RadioButton xVergelijkWaardeCheck;
        private System.Windows.Forms.Panel xComboPanel;
        private System.Windows.Forms.Panel xEditValueRangePanel;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox xhuidigeDatum;
        private MetroFramework.Controls.MetroComboBox xRangeDevider;
        private System.Windows.Forms.CheckBox xdezeweekcheckbox;
    }
}