namespace Forms
{
    partial class DatumChanger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DatumChanger));
            this.button2 = new MetroFramework.Controls.MetroButton();
            this.button1 = new MetroFramework.Controls.MetroButton();
            this.xmessage = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xdatepicker = new System.Windows.Forms.DateTimePicker();
            this.xaddtimepanel = new System.Windows.Forms.Panel();
            this.xfieldpanel = new System.Windows.Forms.Panel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.xextratijdcheckbox = new MetroFramework.Controls.MetroRadioButton();
            this.xwijzigdatumcheckbox = new MetroFramework.Controls.MetroRadioButton();
            this.label1 = new System.Windows.Forms.Label();
            this.xdagen = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.xuren = new System.Windows.Forms.NumericUpDown();
            this.xmin = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.xaddtimepanel.SuspendLayout();
            this.xfieldpanel.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xdagen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xuren)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xmin)).BeginInit();
            this.SuspendLayout();
            // 
            // button2
            // 
            this.button2.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.button2.Location = new System.Drawing.Point(3, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(100, 30);
            this.button2.TabIndex = 2;
            this.button2.Text = "OK";
            this.button2.UseSelectable = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.button1.Location = new System.Drawing.Point(109, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 30);
            this.button1.TabIndex = 1;
            this.button1.Text = "Sluiten";
            this.button1.UseSelectable = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // xmessage
            // 
            this.xmessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmessage.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmessage.Location = new System.Drawing.Point(151, 60);
            this.xmessage.Name = "xmessage";
            this.xmessage.Size = new System.Drawing.Size(556, 51);
            this.xmessage.TabIndex = 4;
            this.xmessage.Text = "Wijzig Datum";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 213);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(687, 36);
            this.panel1.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.button2);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(474, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(213, 36);
            this.panel2.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.systemtime_778;
            this.pictureBox1.Location = new System.Drawing.Point(20, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(131, 153);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // xdatepicker
            // 
            this.xdatepicker.CalendarFont = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdatepicker.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xdatepicker.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xdatepicker.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdatepicker.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xdatepicker.Location = new System.Drawing.Point(0, 39);
            this.xdatepicker.MinimumSize = new System.Drawing.Size(4, 35);
            this.xdatepicker.Name = "xdatepicker";
            this.xdatepicker.Size = new System.Drawing.Size(556, 35);
            this.xdatepicker.TabIndex = 6;
            this.xdatepicker.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dateTimePicker1_KeyDown);
            // 
            // xaddtimepanel
            // 
            this.xaddtimepanel.Controls.Add(this.xmin);
            this.xaddtimepanel.Controls.Add(this.label3);
            this.xaddtimepanel.Controls.Add(this.xuren);
            this.xaddtimepanel.Controls.Add(this.label2);
            this.xaddtimepanel.Controls.Add(this.xdagen);
            this.xaddtimepanel.Controls.Add(this.label1);
            this.xaddtimepanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xaddtimepanel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaddtimepanel.Location = new System.Drawing.Point(0, 0);
            this.xaddtimepanel.Name = "xaddtimepanel";
            this.xaddtimepanel.Size = new System.Drawing.Size(556, 35);
            this.xaddtimepanel.TabIndex = 7;
            this.xaddtimepanel.Visible = false;
            // 
            // xfieldpanel
            // 
            this.xfieldpanel.Controls.Add(this.xdatepicker);
            this.xfieldpanel.Controls.Add(this.xaddtimepanel);
            this.xfieldpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xfieldpanel.Location = new System.Drawing.Point(151, 139);
            this.xfieldpanel.Name = "xfieldpanel";
            this.xfieldpanel.Size = new System.Drawing.Size(556, 74);
            this.xfieldpanel.TabIndex = 8;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.xextratijdcheckbox);
            this.panel4.Controls.Add(this.xwijzigdatumcheckbox);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(151, 111);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(556, 28);
            this.panel4.TabIndex = 9;
            // 
            // xextratijdcheckbox
            // 
            this.xextratijdcheckbox.AutoSize = true;
            this.xextratijdcheckbox.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xextratijdcheckbox.Location = new System.Drawing.Point(132, 4);
            this.xextratijdcheckbox.Name = "xextratijdcheckbox";
            this.xextratijdcheckbox.Size = new System.Drawing.Size(137, 19);
            this.xextratijdcheckbox.TabIndex = 1;
            this.xextratijdcheckbox.Text = "Voeg toe extra tijd";
            this.xextratijdcheckbox.UseSelectable = true;
            this.xextratijdcheckbox.CheckedChanged += new System.EventHandler(this.xwijzigdatumcheckbox_CheckedChanged);
            // 
            // xwijzigdatumcheckbox
            // 
            this.xwijzigdatumcheckbox.AutoSize = true;
            this.xwijzigdatumcheckbox.Checked = true;
            this.xwijzigdatumcheckbox.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xwijzigdatumcheckbox.Location = new System.Drawing.Point(19, 4);
            this.xwijzigdatumcheckbox.Name = "xwijzigdatumcheckbox";
            this.xwijzigdatumcheckbox.Size = new System.Drawing.Size(107, 19);
            this.xwijzigdatumcheckbox.TabIndex = 0;
            this.xwijzigdatumcheckbox.TabStop = true;
            this.xwijzigdatumcheckbox.Text = "Wijzig Datum";
            this.xwijzigdatumcheckbox.UseSelectable = true;
            this.xwijzigdatumcheckbox.CheckedChanged += new System.EventHandler(this.xwijzigdatumcheckbox_CheckedChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Dagen: ";
            // 
            // xdagen
            // 
            this.xdagen.Location = new System.Drawing.Point(69, 5);
            this.xdagen.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xdagen.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.xdagen.Name = "xdagen";
            this.xdagen.Size = new System.Drawing.Size(104, 27);
            this.xdagen.TabIndex = 1;
            this.xdagen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(179, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Uur:";
            // 
            // xuren
            // 
            this.xuren.Location = new System.Drawing.Point(232, 5);
            this.xuren.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xuren.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.xuren.Name = "xuren";
            this.xuren.Size = new System.Drawing.Size(98, 27);
            this.xuren.TabIndex = 3;
            this.xuren.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // xmin
            // 
            this.xmin.Location = new System.Drawing.Point(383, 5);
            this.xmin.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xmin.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.xmin.Name = "xmin";
            this.xmin.Size = new System.Drawing.Size(98, 27);
            this.xmin.TabIndex = 5;
            this.xmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(336, 8);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Min: ";
            // 
            // DatumChanger
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(727, 269);
            this.Controls.Add(this.xmessage);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.xfieldpanel);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 230);
            this.Name = "DatumChanger";
            this.SaveLastSize = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wijzig Datum";
            this.Title = "Wijzig Datum";
            this.Shown += new System.EventHandler(this.DatumChanger_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.xaddtimepanel.ResumeLayout(false);
            this.xaddtimepanel.PerformLayout();
            this.xfieldpanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xdagen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xuren)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xmin)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton button2;
        private MetroFramework.Controls.MetroButton button1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label xmessage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.DateTimePicker xdatepicker;
        private System.Windows.Forms.Panel xaddtimepanel;
        private System.Windows.Forms.Panel xfieldpanel;
        private System.Windows.Forms.Panel panel4;
        private MetroFramework.Controls.MetroRadioButton xextratijdcheckbox;
        private MetroFramework.Controls.MetroRadioButton xwijzigdatumcheckbox;
        private System.Windows.Forms.NumericUpDown xmin;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown xuren;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown xdagen;
        private System.Windows.Forms.Label label1;
    }
}