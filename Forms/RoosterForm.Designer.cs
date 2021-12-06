
using Rpm.Productie;

namespace Forms
{
    partial class RoosterForm
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
            Rpm.Productie.Rooster rooster1 = new Rpm.Productie.Rooster();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RoosterForm));
            this.xcancelb = new System.Windows.Forms.Button();
            this.xokb = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xperiodegroup = new System.Windows.Forms.GroupBox();
            this.xtotdate = new System.Windows.Forms.DateTimePicker();
            this.xvanafdate = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.roosterUI1 = new Controls.RoosterUI();
            this.xgebruikperiode = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.xperiodegroup.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xcancelb
            // 
            this.xcancelb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xcancelb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xcancelb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xcancelb.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xcancelb.Location = new System.Drawing.Point(360, 7);
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
            this.xokb.Location = new System.Drawing.Point(234, 7);
            this.xokb.Name = "xokb";
            this.xokb.Size = new System.Drawing.Size(120, 38);
            this.xokb.TabIndex = 10;
            this.xokb.Text = "&OK";
            this.xokb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xokb.UseVisualStyleBackColor = true;
            this.xokb.Click += new System.EventHandler(this.xokb_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.rooster_128_128;
            this.pictureBox1.Location = new System.Drawing.Point(20, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(153, 441);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 9;
            this.pictureBox1.TabStop = false;
            // 
            // xperiodegroup
            // 
            this.xperiodegroup.Controls.Add(this.xtotdate);
            this.xperiodegroup.Controls.Add(this.xvanafdate);
            this.xperiodegroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xperiodegroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xperiodegroup.Location = new System.Drawing.Point(173, 416);
            this.xperiodegroup.Name = "xperiodegroup";
            this.xperiodegroup.Size = new System.Drawing.Size(339, 85);
            this.xperiodegroup.TabIndex = 13;
            this.xperiodegroup.TabStop = false;
            this.xperiodegroup.Text = "Periode";
            // 
            // xtotdate
            // 
            this.xtotdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtotdate.Checked = false;
            this.xtotdate.CustomFormat = "\'Tot:\' dddd dd MMMM yyyy HH:mm";
            this.xtotdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtotdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xtotdate.Location = new System.Drawing.Point(6, 55);
            this.xtotdate.Name = "xtotdate";
            this.xtotdate.ShowCheckBox = true;
            this.xtotdate.Size = new System.Drawing.Size(327, 25);
            this.xtotdate.TabIndex = 2;
            // 
            // xvanafdate
            // 
            this.xvanafdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xvanafdate.Checked = false;
            this.xvanafdate.CustomFormat = "\'Vanaf:\' dddd dd MMMM yyyy HH:mm";
            this.xvanafdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvanafdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xvanafdate.Location = new System.Drawing.Point(6, 24);
            this.xvanafdate.Name = "xvanafdate";
            this.xvanafdate.ShowCheckBox = true;
            this.xvanafdate.Size = new System.Drawing.Size(327, 25);
            this.xvanafdate.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xcancelb);
            this.panel1.Controls.Add(this.xokb);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 501);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(492, 57);
            this.panel1.TabIndex = 14;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xgebruikperiode);
            this.panel2.Controls.Add(this.roosterUI1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(173, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(339, 356);
            this.panel2.TabIndex = 15;
            // 
            // roosterUI1
            // 
            this.roosterUI1.BackColor = System.Drawing.Color.White;
            this.roosterUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roosterUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roosterUI1.Location = new System.Drawing.Point(0, 0);
            this.roosterUI1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.roosterUI1.Name = "roosterUI1";
            this.roosterUI1.Size = new System.Drawing.Size(339, 356);
            this.roosterUI1.TabIndex = 12;
            rooster1.DuurPauze1 = System.TimeSpan.Parse("00:15:00");
            rooster1.DuurPauze2 = System.TimeSpan.Parse("00:30:00");
            rooster1.DuurPauze3 = System.TimeSpan.Parse("00:15:00");
            rooster1.EindWerkdag = System.TimeSpan.Parse("16:30:00");
            rooster1.GebruiktBereik = false;
            rooster1.GebruiktPauze = false;
            rooster1.GebruiktTot = false;
            rooster1.GebruiktVanaf = false;
            rooster1.StartPauze1 = System.TimeSpan.Parse("09:45:00");
            rooster1.StartPauze2 = System.TimeSpan.Parse("12:00:00");
            rooster1.StartPauze3 = System.TimeSpan.Parse("14:45:00");
            rooster1.StartWerkdag = System.TimeSpan.Parse("07:30:00");
            rooster1.Tot = new System.DateTime(((long)(0)));
            rooster1.Vanaf = new System.DateTime(((long)(0)));
            this.roosterUI1.WerkRooster = rooster1;
            // 
            // xgebruikperiode
            // 
            this.xgebruikperiode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xgebruikperiode.AutoSize = true;
            this.xgebruikperiode.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xgebruikperiode.Location = new System.Drawing.Point(6, 332);
            this.xgebruikperiode.Name = "xgebruikperiode";
            this.xgebruikperiode.Size = new System.Drawing.Size(126, 21);
            this.xgebruikperiode.TabIndex = 16;
            this.xgebruikperiode.Text = "Gebruik Periode";
            this.xgebruikperiode.UseVisualStyleBackColor = true;
            this.xgebruikperiode.CheckedChanged += new System.EventHandler(this.xgebruikperiode_CheckedChanged);
            // 
            // RoosterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 578);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.xperiodegroup);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "RoosterForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Rooster";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.xperiodegroup.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button xcancelb;
        private System.Windows.Forms.Button xokb;
        private Controls.RoosterUI roosterUI1;
        private System.Windows.Forms.GroupBox xperiodegroup;
        private System.Windows.Forms.DateTimePicker xtotdate;
        private System.Windows.Forms.DateTimePicker xvanafdate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.CheckBox xgebruikperiode;
    }
}