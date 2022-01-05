namespace Forms.Aantal
{
    partial class NewAantalRecordForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.xok = new System.Windows.Forms.Button();
            this.xsluiten = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xgemaaktlabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.xstopdatum = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.xstartdatum = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.xsecondvalue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.xfirstvalue = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xsecondvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xfirstvalue)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xok);
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 246);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(342, 44);
            this.panel1.TabIndex = 0;
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xok.Location = new System.Drawing.Point(57, 3);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(138, 38);
            this.xok.TabIndex = 1;
            this.xok.Text = "Toevoegen";
            this.xok.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.button1_Click);
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(201, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(138, 38);
            this.xsluiten.TabIndex = 0;
            this.xsluiten.Text = "Annuleren";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xgemaaktlabel);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.xstopdatum);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.xstartdatum);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.xsecondvalue);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.xfirstvalue);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(20, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(342, 186);
            this.panel2.TabIndex = 1;
            // 
            // xgemaaktlabel
            // 
            this.xgemaaktlabel.AutoSize = true;
            this.xgemaaktlabel.Location = new System.Drawing.Point(264, 40);
            this.xgemaaktlabel.Name = "xgemaaktlabel";
            this.xgemaaktlabel.Size = new System.Drawing.Size(0, 21);
            this.xgemaaktlabel.TabIndex = 8;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 126);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 21);
            this.label4.TabIndex = 7;
            this.label4.Text = "Einde Datum";
            // 
            // xstopdatum
            // 
            this.xstopdatum.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xstopdatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstopdatum.Location = new System.Drawing.Point(7, 150);
            this.xstopdatum.Name = "xstopdatum";
            this.xstopdatum.Size = new System.Drawing.Size(318, 29);
            this.xstopdatum.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 70);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Start Datum";
            // 
            // xstartdatum
            // 
            this.xstartdatum.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xstartdatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstartdatum.Location = new System.Drawing.Point(7, 94);
            this.xstartdatum.Name = "xstartdatum";
            this.xstartdatum.Size = new System.Drawing.Size(318, 29);
            this.xstartdatum.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(129, 14);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "Laatste Aantal";
            // 
            // xsecondvalue
            // 
            this.xsecondvalue.Location = new System.Drawing.Point(133, 38);
            this.xsecondvalue.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xsecondvalue.Name = "xsecondvalue";
            this.xsecondvalue.Size = new System.Drawing.Size(120, 29);
            this.xsecondvalue.TabIndex = 2;
            this.xsecondvalue.ValueChanged += new System.EventHandler(this.xfirstvalue_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 14);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Eerste Aantal";
            // 
            // xfirstvalue
            // 
            this.xfirstvalue.Location = new System.Drawing.Point(7, 38);
            this.xfirstvalue.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xfirstvalue.Name = "xfirstvalue";
            this.xfirstvalue.Size = new System.Drawing.Size(120, 29);
            this.xfirstvalue.TabIndex = 0;
            this.xfirstvalue.ValueChanged += new System.EventHandler(this.xfirstvalue_ValueChanged);
            // 
            // NewAantalRecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(382, 310);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "NewAantalRecordForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "Aantal Record";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xsecondvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xfirstvalue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xok;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown xfirstvalue;
        private System.Windows.Forms.Label xgemaaktlabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.DateTimePicker xstopdatum;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker xstartdatum;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown xsecondvalue;
    }
}