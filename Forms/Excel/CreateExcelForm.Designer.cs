
namespace Forms
{
    partial class CreateExcelForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateExcelForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xbezig = new System.Windows.Forms.Label();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xOpslaan = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xinfolabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.xtotdate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.xvanafdate = new System.Windows.Forms.DateTimePicker();
            this.xopenexcel = new System.Windows.Forms.CheckBox();
            this.xcreeroverzicht = new System.Windows.Forms.CheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.xcolumnsStatusLabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Controls.Add(this.xOpslaan);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 273);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(550, 45);
            this.panel1.TabIndex = 1;
            // 
            // xbezig
            // 
            this.xbezig.AutoSize = true;
            this.xbezig.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbezig.Location = new System.Drawing.Point(146, 247);
            this.xbezig.Name = "xbezig";
            this.xbezig.Size = new System.Drawing.Size(166, 20);
            this.xbezig.TabIndex = 8;
            this.xbezig.Text = "Overzicht Aanmaken...";
            this.xbezig.Visible = false;
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.ForeColor = System.Drawing.Color.Black;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(428, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(119, 38);
            this.xsluiten.TabIndex = 6;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xOpslaan
            // 
            this.xOpslaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xOpslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOpslaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOpslaan.ForeColor = System.Drawing.Color.Black;
            this.xOpslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xOpslaan.Location = new System.Drawing.Point(303, 3);
            this.xOpslaan.Name = "xOpslaan";
            this.xOpslaan.Size = new System.Drawing.Size(119, 38);
            this.xOpslaan.TabIndex = 7;
            this.xOpslaan.Text = "Opslaan";
            this.xOpslaan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xOpslaan.UseVisualStyleBackColor = true;
            this.xOpslaan.Click += new System.EventHandler(this.xOpslaan_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.microsoft_excel_22733_128x128;
            this.pictureBox1.Location = new System.Drawing.Point(20, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 213);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xinfolabel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.xtotdate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.xvanafdate);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(144, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(426, 92);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kies Bereik";
            // 
            // xinfolabel
            // 
            this.xinfolabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xinfolabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xinfolabel.Location = new System.Drawing.Point(2, 3);
            this.xinfolabel.Name = "xinfolabel";
            this.xinfolabel.Size = new System.Drawing.Size(424, 89);
            this.xinfolabel.TabIndex = 4;
            this.xinfolabel.Text = "Informatie";
            this.xinfolabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xinfolabel.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(17, 65);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(28, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tot";
            // 
            // xtotdate
            // 
            this.xtotdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtotdate.CalendarFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtotdate.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xtotdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtotdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xtotdate.Location = new System.Drawing.Point(55, 59);
            this.xtotdate.Name = "xtotdate";
            this.xtotdate.Size = new System.Drawing.Size(368, 25);
            this.xtotdate.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vanaf";
            // 
            // xvanafdate
            // 
            this.xvanafdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xvanafdate.CalendarFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvanafdate.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xvanafdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvanafdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xvanafdate.Location = new System.Drawing.Point(55, 28);
            this.xvanafdate.Name = "xvanafdate";
            this.xvanafdate.Size = new System.Drawing.Size(368, 25);
            this.xvanafdate.TabIndex = 0;
            // 
            // xopenexcel
            // 
            this.xopenexcel.AutoSize = true;
            this.xopenexcel.Checked = true;
            this.xopenexcel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xopenexcel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xopenexcel.Location = new System.Drawing.Point(480, 223);
            this.xopenexcel.Name = "xopenexcel";
            this.xopenexcel.Size = new System.Drawing.Size(95, 21);
            this.xopenexcel.TabIndex = 3;
            this.xopenexcel.Text = "Open Excel";
            this.xopenexcel.UseVisualStyleBackColor = true;
            // 
            // xcreeroverzicht
            // 
            this.xcreeroverzicht.AutoSize = true;
            this.xcreeroverzicht.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xcreeroverzicht.Location = new System.Drawing.Point(150, 223);
            this.xcreeroverzicht.Name = "xcreeroverzicht";
            this.xcreeroverzicht.Size = new System.Drawing.Size(211, 21);
            this.xcreeroverzicht.TabIndex = 4;
            this.xcreeroverzicht.Text = "Creeër Overzicht statistieken  ";
            this.xcreeroverzicht.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Image = global::ProductieManager.Properties.Resources.mimetypes_excel_32x32;
            this.button1.Location = new System.Drawing.Point(150, 179);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(231, 38);
            this.button1.TabIndex = 9;
            this.button1.Text = "Kies Columns Opties";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // xcolumnsStatusLabel
            // 
            this.xcolumnsStatusLabel.AutoSize = true;
            this.xcolumnsStatusLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xcolumnsStatusLabel.ForeColor = System.Drawing.Color.DarkRed;
            this.xcolumnsStatusLabel.Location = new System.Drawing.Point(150, 155);
            this.xcolumnsStatusLabel.Name = "xcolumnsStatusLabel";
            this.xcolumnsStatusLabel.Size = new System.Drawing.Size(279, 21);
            this.xcolumnsStatusLabel.TabIndex = 10;
            this.xcolumnsStatusLabel.Text = "Geen column  instellingen gekozen";
            this.xcolumnsStatusLabel.Click += new System.EventHandler(this.xcolumnsStatusLabel_Click);
            // 
            // CreateExcelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(590, 338);
            this.Controls.Add(this.xbezig);
            this.Controls.Add(this.xcolumnsStatusLabel);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.xcreeroverzicht);
            this.Controls.Add(this.xopenexcel);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(590, 338);
            this.Name = "CreateExcelForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Excel Overzicht Aanmaken";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.CreateExcelForm_FormClosing);
            this.Shown += new System.EventHandler(this.CreateExcelForm_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xOpslaan;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker xtotdate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker xvanafdate;
        private System.Windows.Forms.CheckBox xopenexcel;
        private System.Windows.Forms.Label xbezig;
        private System.Windows.Forms.Label xinfolabel;
        private System.Windows.Forms.CheckBox xcreeroverzicht;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label xcolumnsStatusLabel;
    }
}