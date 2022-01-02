namespace Forms.Combineer
{
    partial class NewCombineerForm
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
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xanuleren = new System.Windows.Forms.Button();
            this.xok = new System.Windows.Forms.Button();
            this.xomschrijving = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.xhoofdvalue = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xhoofdvalue)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.Merge_arrows_128x128;
            this.pictureBox1.Location = new System.Drawing.Point(20, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(100, 270);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(120, 296);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(610, 34);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xanuleren);
            this.panel2.Controls.Add(this.xok);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(358, 0);
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
            this.xok.Text = "&Combineer";
            this.xok.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // xomschrijving
            // 
            this.xomschrijving.AutoSize = false;
            this.xomschrijving.BackColor = System.Drawing.SystemColors.Window;
            this.xomschrijving.BaseStylesheet = null;
            this.xomschrijving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xomschrijving.Location = new System.Drawing.Point(120, 60);
            this.xomschrijving.Name = "xomschrijving";
            this.xomschrijving.Size = new System.Drawing.Size(610, 197);
            this.xomschrijving.TabIndex = 8;
            this.xomschrijving.Text = "Omschrijving";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.xhoofdvalue);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(120, 257);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(610, 39);
            this.panel3.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(277, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(22, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "%";
            // 
            // xhoofdvalue
            // 
            this.xhoofdvalue.DecimalPlaces = 2;
            this.xhoofdvalue.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xhoofdvalue.Location = new System.Drawing.Point(207, 6);
            this.xhoofdvalue.Maximum = new decimal(new int[] {
            99,
            0,
            0,
            0});
            this.xhoofdvalue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xhoofdvalue.Name = "xhoofdvalue";
            this.xhoofdvalue.Size = new System.Drawing.Size(67, 27);
            this.xhoofdvalue.TabIndex = 1;
            this.xhoofdvalue.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.xhoofdvalue.ValueChanged += new System.EventHandler(this.xhoofdvalue_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(198, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Aantal Procent Verhouding";
            // 
            // NewCombineerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 350);
            this.Controls.Add(this.xomschrijving);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.MinimumSize = new System.Drawing.Size(750, 350);
            this.Name = "NewCombineerForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Magenta;
            this.Text = "Combineer Productie";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xhoofdvalue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xanuleren;
        private System.Windows.Forms.Button xok;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel xomschrijving;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.NumericUpDown xhoofdvalue;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}