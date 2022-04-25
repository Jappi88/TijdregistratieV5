
namespace Controls
{
    partial class RoosterUI
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
            this.label9 = new System.Windows.Forms.Label();
            this.xeindwerkdag = new System.Windows.Forms.DateTimePicker();
            this.xstartwerkdag = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.xstartpauze1 = new System.Windows.Forms.DateTimePicker();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.xduurpauze3 = new System.Windows.Forms.DateTimePicker();
            this.xduurpauze1 = new System.Windows.Forms.DateTimePicker();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.xstartpauze3 = new System.Windows.Forms.DateTimePicker();
            this.xstartpauze2 = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.xduurpauze2 = new System.Windows.Forms.DateTimePicker();
            this.xpauzetijdengroup = new System.Windows.Forms.GroupBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xgebruiktpauze = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xspeciaalroosterb = new System.Windows.Forms.Button();
            this.xstandaard = new System.Windows.Forms.Button();
            this.xnationaleFeestdageGroup = new System.Windows.Forms.GroupBox();
            this.xfeestdagen = new System.Windows.Forms.ListView();
            this.xDatum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel4 = new System.Windows.Forms.Panel();
            this.xfeestdagdate = new System.Windows.Forms.DateTimePicker();
            this.xaddfesstdag = new System.Windows.Forms.Button();
            this.xremovefeestdag = new System.Windows.Forms.Button();
            this.xpauzetijdengroup.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.xnationaleFeestdageGroup.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(6, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(96, 17);
            this.label9.TabIndex = 34;
            this.label9.Text = "Start Werkdag";
            // 
            // xeindwerkdag
            // 
            this.xeindwerkdag.CalendarFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xeindwerkdag.CustomFormat = "HH:mm";
            this.xeindwerkdag.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xeindwerkdag.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xeindwerkdag.Location = new System.Drawing.Point(118, 31);
            this.xeindwerkdag.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xeindwerkdag.Name = "xeindwerkdag";
            this.xeindwerkdag.ShowUpDown = true;
            this.xeindwerkdag.Size = new System.Drawing.Size(101, 25);
            this.xeindwerkdag.TabIndex = 19;
            this.xeindwerkdag.Tag = "1";
            this.xeindwerkdag.Value = new System.DateTime(2020, 10, 3, 16, 30, 0, 0);
            this.xeindwerkdag.ValueChanged += new System.EventHandler(this.xstartwerkdag_ValueChanged);
            // 
            // xstartwerkdag
            // 
            this.xstartwerkdag.CalendarFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstartwerkdag.CustomFormat = "HH:mm";
            this.xstartwerkdag.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstartwerkdag.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstartwerkdag.Location = new System.Drawing.Point(9, 31);
            this.xstartwerkdag.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xstartwerkdag.Name = "xstartwerkdag";
            this.xstartwerkdag.ShowUpDown = true;
            this.xstartwerkdag.Size = new System.Drawing.Size(103, 25);
            this.xstartwerkdag.TabIndex = 33;
            this.xstartwerkdag.Tag = "0";
            this.xstartwerkdag.Value = new System.DateTime(2020, 10, 3, 7, 30, 0, 0);
            this.xstartwerkdag.ValueChanged += new System.EventHandler(this.xstartwerkdag_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(115, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 17);
            this.label2.TabIndex = 20;
            this.label2.Text = "Eind Werkdag";
            // 
            // xstartpauze1
            // 
            this.xstartpauze1.CalendarFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstartpauze1.CustomFormat = "HH:mm";
            this.xstartpauze1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstartpauze1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstartpauze1.Location = new System.Drawing.Point(6, 25);
            this.xstartpauze1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xstartpauze1.Name = "xstartpauze1";
            this.xstartpauze1.ShowUpDown = true;
            this.xstartpauze1.Size = new System.Drawing.Size(101, 25);
            this.xstartpauze1.TabIndex = 21;
            this.xstartpauze1.Tag = "2";
            this.xstartpauze1.Value = new System.DateTime(2020, 10, 3, 9, 45, 0, 0);
            this.xstartpauze1.ValueChanged += new System.EventHandler(this.xstartwerkdag_ValueChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(112, 104);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 17);
            this.label7.TabIndex = 32;
            this.label7.Text = "Duur 3de Pauze";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 17);
            this.label4.TabIndex = 22;
            this.label4.Text = "Start 1ste Pauze ";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xduurpauze3
            // 
            this.xduurpauze3.CalendarFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xduurpauze3.CustomFormat = "HH:mm";
            this.xduurpauze3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xduurpauze3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xduurpauze3.Location = new System.Drawing.Point(115, 125);
            this.xduurpauze3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xduurpauze3.Name = "xduurpauze3";
            this.xduurpauze3.ShowUpDown = true;
            this.xduurpauze3.Size = new System.Drawing.Size(101, 25);
            this.xduurpauze3.TabIndex = 31;
            this.xduurpauze3.Tag = "7";
            this.xduurpauze3.Value = new System.DateTime(2020, 10, 3, 0, 15, 0, 0);
            this.xduurpauze3.ValueChanged += new System.EventHandler(this.xstartwerkdag_ValueChanged);
            // 
            // xduurpauze1
            // 
            this.xduurpauze1.CalendarFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xduurpauze1.CustomFormat = "HH:mm";
            this.xduurpauze1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xduurpauze1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xduurpauze1.Location = new System.Drawing.Point(115, 24);
            this.xduurpauze1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xduurpauze1.Name = "xduurpauze1";
            this.xduurpauze1.ShowUpDown = true;
            this.xduurpauze1.Size = new System.Drawing.Size(101, 25);
            this.xduurpauze1.TabIndex = 23;
            this.xduurpauze1.Tag = "3";
            this.xduurpauze1.Value = new System.DateTime(2020, 10, 3, 0, 15, 0, 0);
            this.xduurpauze1.ValueChanged += new System.EventHandler(this.xstartwerkdag_ValueChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.Location = new System.Drawing.Point(3, 104);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(103, 17);
            this.label8.TabIndex = 30;
            this.label8.Text = "Start 3de Pauze ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(112, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 17);
            this.label3.TabIndex = 24;
            this.label3.Text = "Duur 1ste Pauze";
            // 
            // xstartpauze3
            // 
            this.xstartpauze3.CalendarFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstartpauze3.CustomFormat = "HH:mm";
            this.xstartpauze3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstartpauze3.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstartpauze3.Location = new System.Drawing.Point(6, 125);
            this.xstartpauze3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xstartpauze3.Name = "xstartpauze3";
            this.xstartpauze3.ShowUpDown = true;
            this.xstartpauze3.Size = new System.Drawing.Size(103, 25);
            this.xstartpauze3.TabIndex = 29;
            this.xstartpauze3.Tag = "6";
            this.xstartpauze3.Value = new System.DateTime(2020, 10, 3, 14, 45, 0, 0);
            this.xstartpauze3.ValueChanged += new System.EventHandler(this.xstartwerkdag_ValueChanged);
            // 
            // xstartpauze2
            // 
            this.xstartpauze2.CalendarFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstartpauze2.CustomFormat = "HH:mm";
            this.xstartpauze2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstartpauze2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstartpauze2.Location = new System.Drawing.Point(6, 75);
            this.xstartpauze2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xstartpauze2.Name = "xstartpauze2";
            this.xstartpauze2.ShowUpDown = true;
            this.xstartpauze2.Size = new System.Drawing.Size(103, 25);
            this.xstartpauze2.TabIndex = 25;
            this.xstartpauze2.Tag = "4";
            this.xstartpauze2.Value = new System.DateTime(2020, 10, 3, 12, 0, 0, 0);
            this.xstartpauze2.ValueChanged += new System.EventHandler(this.xstartwerkdag_ValueChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(112, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 17);
            this.label5.TabIndex = 28;
            this.label5.Text = "Duur 2de Pauze";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(3, 54);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(103, 17);
            this.label6.TabIndex = 26;
            this.label6.Text = "Start 2de Pauze ";
            // 
            // xduurpauze2
            // 
            this.xduurpauze2.CalendarFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xduurpauze2.CustomFormat = "HH:mm";
            this.xduurpauze2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xduurpauze2.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xduurpauze2.Location = new System.Drawing.Point(115, 75);
            this.xduurpauze2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xduurpauze2.Name = "xduurpauze2";
            this.xduurpauze2.ShowUpDown = true;
            this.xduurpauze2.Size = new System.Drawing.Size(101, 25);
            this.xduurpauze2.TabIndex = 27;
            this.xduurpauze2.Tag = "5";
            this.xduurpauze2.Value = new System.DateTime(2020, 10, 3, 0, 30, 0, 0);
            this.xduurpauze2.ValueChanged += new System.EventHandler(this.xstartwerkdag_ValueChanged);
            // 
            // xpauzetijdengroup
            // 
            this.xpauzetijdengroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xpauzetijdengroup.Controls.Add(this.panel2);
            this.xpauzetijdengroup.Enabled = false;
            this.xpauzetijdengroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xpauzetijdengroup.Location = new System.Drawing.Point(3, 99);
            this.xpauzetijdengroup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xpauzetijdengroup.Name = "xpauzetijdengroup";
            this.xpauzetijdengroup.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xpauzetijdengroup.Size = new System.Drawing.Size(264, 181);
            this.xpauzetijdengroup.TabIndex = 35;
            this.xpauzetijdengroup.TabStop = false;
            this.xpauzetijdengroup.Text = "Pauze Tijden";
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.xduurpauze1);
            this.panel2.Controls.Add(this.xduurpauze2);
            this.panel2.Controls.Add(this.xduurpauze3);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.xstartpauze2);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.xstartpauze1);
            this.panel2.Controls.Add(this.xstartpauze3);
            this.panel2.Location = new System.Drawing.Point(3, 22);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(255, 155);
            this.panel2.TabIndex = 33;
            // 
            // xgebruiktpauze
            // 
            this.xgebruiktpauze.AutoSize = true;
            this.xgebruiktpauze.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xgebruiktpauze.Location = new System.Drawing.Point(6, 75);
            this.xgebruiktpauze.Name = "xgebruiktpauze";
            this.xgebruiktpauze.Size = new System.Drawing.Size(163, 21);
            this.xgebruiktpauze.TabIndex = 36;
            this.xgebruiktpauze.Text = "Gebruikt Pauze Tijden";
            this.xgebruiktpauze.UseVisualStyleBackColor = true;
            this.xgebruiktpauze.CheckedChanged += new System.EventHandler(this.xgebruiktpauze_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xstartwerkdag);
            this.panel1.Controls.Add(this.label9);
            this.panel1.Controls.Add(this.xeindwerkdag);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(271, 66);
            this.panel1.TabIndex = 37;
            // 
            // panel3
            // 
            this.panel3.AutoScroll = true;
            this.panel3.Controls.Add(this.xspeciaalroosterb);
            this.panel3.Controls.Add(this.xstandaard);
            this.panel3.Controls.Add(this.panel1);
            this.panel3.Controls.Add(this.xgebruiktpauze);
            this.panel3.Controls.Add(this.xpauzetijdengroup);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel3.Location = new System.Drawing.Point(5, 5);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(271, 380);
            this.panel3.TabIndex = 0;
            // 
            // xspeciaalroosterb
            // 
            this.xspeciaalroosterb.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xspeciaalroosterb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xspeciaalroosterb.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xspeciaalroosterb.Image = global::ProductieManager.Properties.Resources.augmented_reality_calendar_schedule_mountain_32x32;
            this.xspeciaalroosterb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xspeciaalroosterb.Location = new System.Drawing.Point(5, 331);
            this.xspeciaalroosterb.Name = "xspeciaalroosterb";
            this.xspeciaalroosterb.Size = new System.Drawing.Size(262, 42);
            this.xspeciaalroosterb.TabIndex = 39;
            this.xspeciaalroosterb.Text = "Speciale Roosters";
            this.xspeciaalroosterb.UseVisualStyleBackColor = true;
            this.xspeciaalroosterb.Click += new System.EventHandler(this.xspeciaalroosterb_Click);
            // 
            // xstandaard
            // 
            this.xstandaard.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xstandaard.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xstandaard.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstandaard.Image = global::ProductieManager.Properties.Resources.schedule_32_32;
            this.xstandaard.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xstandaard.Location = new System.Drawing.Point(6, 282);
            this.xstandaard.Name = "xstandaard";
            this.xstandaard.Size = new System.Drawing.Size(262, 43);
            this.xstandaard.TabIndex = 38;
            this.xstandaard.Text = "Standaard Rooster";
            this.xstandaard.UseVisualStyleBackColor = true;
            this.xstandaard.Click += new System.EventHandler(this.xstandaard_Click);
            // 
            // xnationaleFeestdageGroup
            // 
            this.xnationaleFeestdageGroup.Controls.Add(this.xfeestdagen);
            this.xnationaleFeestdageGroup.Controls.Add(this.panel4);
            this.xnationaleFeestdageGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xnationaleFeestdageGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xnationaleFeestdageGroup.ForeColor = System.Drawing.Color.Black;
            this.xnationaleFeestdageGroup.Location = new System.Drawing.Point(276, 5);
            this.xnationaleFeestdageGroup.Name = "xnationaleFeestdageGroup";
            this.xnationaleFeestdageGroup.Size = new System.Drawing.Size(353, 380);
            this.xnationaleFeestdageGroup.TabIndex = 17;
            this.xnationaleFeestdageGroup.TabStop = false;
            this.xnationaleFeestdageGroup.Text = "Nationale Feestdagen";
            // 
            // xfeestdagen
            // 
            this.xfeestdagen.BackColor = System.Drawing.Color.White;
            this.xfeestdagen.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.xDatum});
            this.xfeestdagen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xfeestdagen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xfeestdagen.ForeColor = System.Drawing.Color.Black;
            this.xfeestdagen.FullRowSelect = true;
            this.xfeestdagen.HideSelection = false;
            this.xfeestdagen.Location = new System.Drawing.Point(3, 56);
            this.xfeestdagen.Name = "xfeestdagen";
            this.xfeestdagen.Size = new System.Drawing.Size(347, 321);
            this.xfeestdagen.TabIndex = 1;
            this.xfeestdagen.UseCompatibleStateImageBehavior = false;
            this.xfeestdagen.View = System.Windows.Forms.View.Details;
            this.xfeestdagen.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // xDatum
            // 
            this.xDatum.Text = "Datum";
            this.xDatum.Width = 280;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.xfeestdagdate);
            this.panel4.Controls.Add(this.xaddfesstdag);
            this.panel4.Controls.Add(this.xremovefeestdag);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(3, 21);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(347, 35);
            this.panel4.TabIndex = 0;
            // 
            // xfeestdagdate
            // 
            this.xfeestdagdate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xfeestdagdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xfeestdagdate.Location = new System.Drawing.Point(0, 0);
            this.xfeestdagdate.Name = "xfeestdagdate";
            this.xfeestdagdate.Size = new System.Drawing.Size(265, 29);
            this.xfeestdagdate.TabIndex = 2;
            // 
            // xaddfesstdag
            // 
            this.xaddfesstdag.Dock = System.Windows.Forms.DockStyle.Right;
            this.xaddfesstdag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xaddfesstdag.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaddfesstdag.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xaddfesstdag.Location = new System.Drawing.Point(265, 0);
            this.xaddfesstdag.Name = "xaddfesstdag";
            this.xaddfesstdag.Size = new System.Drawing.Size(41, 35);
            this.xaddfesstdag.TabIndex = 1;
            this.xaddfesstdag.UseVisualStyleBackColor = true;
            this.xaddfesstdag.Click += new System.EventHandler(this.xaddfesstdag_Click);
            // 
            // xremovefeestdag
            // 
            this.xremovefeestdag.Dock = System.Windows.Forms.DockStyle.Right;
            this.xremovefeestdag.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xremovefeestdag.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xremovefeestdag.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xremovefeestdag.Location = new System.Drawing.Point(306, 0);
            this.xremovefeestdag.Name = "xremovefeestdag";
            this.xremovefeestdag.Size = new System.Drawing.Size(41, 35);
            this.xremovefeestdag.TabIndex = 0;
            this.xremovefeestdag.UseVisualStyleBackColor = true;
            this.xremovefeestdag.Visible = false;
            this.xremovefeestdag.Click += new System.EventHandler(this.xremovefeestdag_Click);
            // 
            // RoosterUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xnationaleFeestdageGroup);
            this.Controls.Add(this.panel3);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RoosterUI";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(634, 390);
            this.xpauzetijdengroup.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.xnationaleFeestdageGroup.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.DateTimePicker xeindwerkdag;
        private System.Windows.Forms.DateTimePicker xstartwerkdag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker xstartpauze1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker xduurpauze3;
        private System.Windows.Forms.DateTimePicker xduurpauze1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker xstartpauze3;
        private System.Windows.Forms.DateTimePicker xstartpauze2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.DateTimePicker xduurpauze2;
        private System.Windows.Forms.GroupBox xpauzetijdengroup;
        private System.Windows.Forms.CheckBox xgebruiktpauze;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button xstandaard;
        private System.Windows.Forms.Button xspeciaalroosterb;
        private System.Windows.Forms.GroupBox xnationaleFeestdageGroup;
        private System.Windows.Forms.ListView xfeestdagen;
        private System.Windows.Forms.ColumnHeader xDatum;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.DateTimePicker xfeestdagdate;
        private System.Windows.Forms.Button xaddfesstdag;
        private System.Windows.Forms.Button xremovefeestdag;
    }
}
