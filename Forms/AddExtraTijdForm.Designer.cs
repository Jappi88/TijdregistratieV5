
namespace Forms
{
    partial class AddExtraTijdForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddExtraTijdForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xcancelb = new System.Windows.Forms.Button();
            this.xokb = new System.Windows.Forms.Button();
            this.xaantaltijd = new System.Windows.Forms.Label();
            this.xgebruikbereik = new System.Windows.Forms.CheckBox();
            this.xtotdate = new System.Windows.Forms.DateTimePicker();
            this.xaantalkeer = new System.Windows.Forms.NumericUpDown();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.xaantaltype = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.xvanafdate = new System.Windows.Forms.DateTimePicker();
            this.xbereikfield = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xstarttime = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.xstoptime = new System.Windows.Forms.DateTimePicker();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalkeer)).BeginInit();
            this.xbereikfield.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.metroPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.Time_machine__40675_128_128;
            this.pictureBox1.Location = new System.Drawing.Point(20, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(136, 440);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // xcancelb
            // 
            this.xcancelb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xcancelb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xcancelb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xcancelb.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xcancelb.Location = new System.Drawing.Point(478, 470);
            this.xcancelb.Name = "xcancelb";
            this.xcancelb.Size = new System.Drawing.Size(120, 38);
            this.xcancelb.TabIndex = 11;
            this.xcancelb.Text = "&Annuleren";
            this.xcancelb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xcancelb.UseVisualStyleBackColor = true;
            this.xcancelb.Click += new System.EventHandler(this.xcancelb_Click);
            // 
            // xokb
            // 
            this.xokb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xokb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xokb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xokb.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xokb.Location = new System.Drawing.Point(352, 470);
            this.xokb.Name = "xokb";
            this.xokb.Size = new System.Drawing.Size(120, 38);
            this.xokb.TabIndex = 10;
            this.xokb.Text = "&OK";
            this.xokb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xokb.UseVisualStyleBackColor = true;
            this.xokb.Click += new System.EventHandler(this.xokb_Click);
            // 
            // xaantaltijd
            // 
            this.xaantaltijd.AutoSize = true;
            this.xaantaltijd.Location = new System.Drawing.Point(297, 25);
            this.xaantaltijd.Name = "xaantaltijd";
            this.xaantaltijd.Size = new System.Drawing.Size(101, 21);
            this.xaantaltijd.TabIndex = 5;
            this.xaantaltijd.Text = "Aantal Uren";
            // 
            // xgebruikbereik
            // 
            this.xgebruikbereik.AutoSize = true;
            this.xgebruikbereik.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xgebruikbereik.Location = new System.Drawing.Point(25, 158);
            this.xgebruikbereik.Name = "xgebruikbereik";
            this.xgebruikbereik.Size = new System.Drawing.Size(173, 25);
            this.xgebruikbereik.TabIndex = 15;
            this.xgebruikbereik.Text = "Gebruik Een Bereik";
            this.xgebruikbereik.UseVisualStyleBackColor = true;
            this.xgebruikbereik.CheckedChanged += new System.EventHandler(this.xgebruikbereik_CheckedChanged);
            // 
            // xtotdate
            // 
            this.xtotdate.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xtotdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtotdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xtotdate.Location = new System.Drawing.Point(10, 105);
            this.xtotdate.Name = "xtotdate";
            this.xtotdate.Size = new System.Drawing.Size(388, 29);
            this.xtotdate.TabIndex = 7;
            // 
            // xaantalkeer
            // 
            this.xaantalkeer.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantalkeer.Location = new System.Drawing.Point(10, 161);
            this.xaantalkeer.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.xaantalkeer.Name = "xaantalkeer";
            this.xaantalkeer.Size = new System.Drawing.Size(102, 29);
            this.xaantalkeer.TabIndex = 13;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 137);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(114, 21);
            this.label6.TabIndex = 12;
            this.label6.Text = "Doe dat voor:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 81);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(34, 21);
            this.label5.TabIndex = 8;
            this.label5.Text = "Tot";
            // 
            // xaantaltype
            // 
            this.xaantaltype.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantaltype.FormattingEnabled = true;
            this.xaantaltype.Location = new System.Drawing.Point(118, 161);
            this.xaantaltype.Name = "xaantaltype";
            this.xaantaltype.Size = new System.Drawing.Size(280, 29);
            this.xaantaltype.TabIndex = 14;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 25);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Vanaf";
            // 
            // xvanafdate
            // 
            this.xvanafdate.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xvanafdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvanafdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xvanafdate.Location = new System.Drawing.Point(10, 49);
            this.xvanafdate.Name = "xvanafdate";
            this.xvanafdate.Size = new System.Drawing.Size(388, 29);
            this.xvanafdate.TabIndex = 1;
            // 
            // xbereikfield
            // 
            this.xbereikfield.Controls.Add(this.xvanafdate);
            this.xbereikfield.Controls.Add(this.label4);
            this.xbereikfield.Controls.Add(this.xaantaltype);
            this.xbereikfield.Controls.Add(this.label5);
            this.xbereikfield.Controls.Add(this.label6);
            this.xbereikfield.Controls.Add(this.xaantalkeer);
            this.xbereikfield.Controls.Add(this.xtotdate);
            this.xbereikfield.Enabled = false;
            this.xbereikfield.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbereikfield.Location = new System.Drawing.Point(15, 189);
            this.xbereikfield.Name = "xbereikfield";
            this.xbereikfield.Size = new System.Drawing.Size(416, 205);
            this.xbereikfield.TabIndex = 17;
            this.xbereikfield.TabStop = false;
            this.xbereikfield.Text = "Bereik";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xstarttime);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.xstoptime);
            this.groupBox1.Controls.Add(this.xaantaltijd);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(15, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(416, 149);
            this.groupBox1.TabIndex = 18;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Voeg extra tijd toe";
            // 
            // xstarttime
            // 
            this.xstarttime.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xstarttime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstarttime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstarttime.Location = new System.Drawing.Point(10, 49);
            this.xstarttime.Name = "xstarttime";
            this.xstarttime.Size = new System.Drawing.Size(388, 29);
            this.xstarttime.TabIndex = 1;
            this.xstarttime.ValueChanged += new System.EventHandler(this.xstarttime_ValueChanged_1);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(6, 25);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 21);
            this.label3.TabIndex = 6;
            this.label3.Text = "Vanaf";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(6, 81);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(34, 21);
            this.label7.TabIndex = 8;
            this.label7.Text = "Tot";
            // 
            // xstoptime
            // 
            this.xstoptime.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xstoptime.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstoptime.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstoptime.Location = new System.Drawing.Point(10, 105);
            this.xstoptime.Name = "xstoptime";
            this.xstoptime.Size = new System.Drawing.Size(388, 29);
            this.xstoptime.TabIndex = 7;
            this.xstoptime.ValueChanged += new System.EventHandler(this.xstarttime_ValueChanged_1);
            // 
            // metroPanel1
            // 
            this.metroPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroPanel1.AutoScroll = true;
            this.metroPanel1.Controls.Add(this.groupBox1);
            this.metroPanel1.Controls.Add(this.xbereikfield);
            this.metroPanel1.Controls.Add(this.xgebruikbereik);
            this.metroPanel1.HorizontalScrollbar = true;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(162, 60);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(436, 404);
            this.metroPanel1.TabIndex = 19;
            this.metroPanel1.VerticalScrollbar = true;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // AddExtraTijdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 520);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.xcancelb);
            this.Controls.Add(this.xokb);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(610, 520);
            this.Name = "AddExtraTijdForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Voeg Extra Tijd Toe";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalkeer)).EndInit();
            this.xbereikfield.ResumeLayout(false);
            this.xbereikfield.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.metroPanel1.ResumeLayout(false);
            this.metroPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button xcancelb;
        private System.Windows.Forms.Button xokb;
        private System.Windows.Forms.Label xaantaltijd;
        private System.Windows.Forms.CheckBox xgebruikbereik;
        private System.Windows.Forms.DateTimePicker xtotdate;
        private System.Windows.Forms.NumericUpDown xaantalkeer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox xaantaltype;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker xvanafdate;
        private System.Windows.Forms.GroupBox xbereikfield;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker xstarttime;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.DateTimePicker xstoptime;
        private MetroFramework.Controls.MetroPanel metroPanel1;
    }
}