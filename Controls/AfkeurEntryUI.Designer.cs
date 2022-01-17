
namespace Controls
{
    partial class AfkeurEntryUI
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
            this.xartikelnr = new System.Windows.Forms.Label();
            this.xomschrijving = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.xvalue = new System.Windows.Forms.NumericUpDown();
            this.xeenheid = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xpercent = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.xvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xartikelnr
            // 
            this.xartikelnr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xartikelnr.AutoSize = true;
            this.xartikelnr.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xartikelnr.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.xartikelnr.Location = new System.Drawing.Point(74, 59);
            this.xartikelnr.Name = "xartikelnr";
            this.xartikelnr.Size = new System.Drawing.Size(71, 17);
            this.xartikelnr.TabIndex = 1;
            this.xartikelnr.Text = "123456789";
            // 
            // xomschrijving
            // 
            this.xomschrijving.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xomschrijving.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xomschrijving.Location = new System.Drawing.Point(73, 21);
            this.xomschrijving.Name = "xomschrijving";
            this.xomschrijving.Size = new System.Drawing.Size(420, 31);
            this.xomschrijving.TabIndex = 2;
            this.xomschrijving.Text = "Omschrijving";
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(181, 59);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Aantal Afkeur";
            // 
            // xvalue
            // 
            this.xvalue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xvalue.DecimalPlaces = 2;
            this.xvalue.Location = new System.Drawing.Point(280, 55);
            this.xvalue.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xvalue.Name = "xvalue";
            this.xvalue.Size = new System.Drawing.Size(101, 25);
            this.xvalue.TabIndex = 4;
            this.xvalue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xvalue.ValueChanged += new System.EventHandler(this.xvalue_ValueChanged);
            this.xvalue.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.xvalue_KeyPress);
            // 
            // xeenheid
            // 
            this.xeenheid.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xeenheid.AutoSize = true;
            this.xeenheid.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xeenheid.Location = new System.Drawing.Point(387, 59);
            this.xeenheid.Name = "xeenheid";
            this.xeenheid.Size = new System.Drawing.Size(44, 17);
            this.xeenheid.TabIndex = 5;
            this.xeenheid.Text = "Meter";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.bolts_construction_rivet_screw_screws_128x128;
            this.pictureBox1.Location = new System.Drawing.Point(3, 21);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // xpercent
            // 
            this.xpercent.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xpercent.AutoSize = true;
            this.xpercent.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xpercent.Location = new System.Drawing.Point(433, 59);
            this.xpercent.Name = "xpercent";
            this.xpercent.Size = new System.Drawing.Size(50, 17);
            this.xpercent.TabIndex = 6;
            this.xpercent.Text = "(100%)";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.xartikelnr);
            this.groupBox1.Controls.Add(this.xpercent);
            this.groupBox1.Controls.Add(this.xomschrijving);
            this.groupBox1.Controls.Add(this.xeenheid);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.xvalue);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(499, 88);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            // 
            // AfkeurEntryUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AfkeurEntryUI";
            this.Size = new System.Drawing.Size(499, 88);
            ((System.ComponentModel.ISupportInitialize)(this.xvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label xartikelnr;
        private System.Windows.Forms.Label xomschrijving;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown xvalue;
        private System.Windows.Forms.Label xeenheid;
        private System.Windows.Forms.Label xpercent;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
