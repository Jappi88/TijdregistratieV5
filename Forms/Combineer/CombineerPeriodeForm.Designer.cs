namespace Forms.Combineer
{
    partial class CombineerPeriodeForm
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
            this.xstartdate = new System.Windows.Forms.DateTimePicker();
            this.xstopdate = new System.Windows.Forms.DateTimePicker();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xanuleren = new System.Windows.Forms.Button();
            this.xok = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xstartdate
            // 
            this.xstartdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xstartdate.CustomFormat = "\'Start Combinatie: \'dddd dd MMMM yyyy HH:mm";
            this.xstartdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstartdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstartdate.Location = new System.Drawing.Point(23, 63);
            this.xstartdate.Name = "xstartdate";
            this.xstartdate.Size = new System.Drawing.Size(454, 29);
            this.xstartdate.TabIndex = 0;
            // 
            // xstopdate
            // 
            this.xstopdate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xstopdate.CustomFormat = "\'Einde Combinatie: \'dddd dd MMMM yyyy HH:mm";
            this.xstopdate.Enabled = false;
            this.xstopdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstopdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstopdate.Location = new System.Drawing.Point(23, 129);
            this.xstopdate.Name = "xstopdate";
            this.xstopdate.Size = new System.Drawing.Size(454, 29);
            this.xstopdate.TabIndex = 1;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(23, 98);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(201, 25);
            this.checkBox1.TabIndex = 2;
            this.checkBox1.Text = "Beëindig Combinatie Op:";
            this.checkBox1.UseVisualStyleBackColor = true;
            this.checkBox1.CheckedChanged += new System.EventHandler(this.checkBox1_CheckedChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 166);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 34);
            this.panel1.TabIndex = 8;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xanuleren);
            this.panel2.Controls.Add(this.xok);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(208, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(252, 34);
            this.panel2.TabIndex = 3;
            // 
            // xanuleren
            // 
            this.xanuleren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xanuleren.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xanuleren.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xanuleren.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xanuleren.Location = new System.Drawing.Point(129, 1);
            this.xanuleren.Name = "xanuleren";
            this.xanuleren.Size = new System.Drawing.Size(120, 30);
            this.xanuleren.TabIndex = 3;
            this.xanuleren.Text = "&Annuleren";
            this.xanuleren.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xanuleren.UseVisualStyleBackColor = true;
            this.xanuleren.Click += new System.EventHandler(this.xanuleren_Click);
            // 
            // xok
            // 
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xok.Location = new System.Drawing.Point(3, 1);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(120, 30);
            this.xok.TabIndex = 2;
            this.xok.Text = "&OK";
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // CombineerPeriodeForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(500, 220);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.xstopdate);
            this.Controls.Add(this.xstartdate);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(500, 220);
            this.Name = "CombineerPeriodeForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Magenta;
            this.Text = "Vul In De Combinatie Periode";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DateTimePicker xstartdate;
        private System.Windows.Forms.DateTimePicker xstopdate;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xanuleren;
        private System.Windows.Forms.Button xok;
    }
}