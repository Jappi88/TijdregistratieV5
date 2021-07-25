
namespace ProductieManager.Forms
{
    partial class DeelsGereedForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeelsGereedForm));
            this.xgereednotitie = new System.Windows.Forms.TextBox();
            this.xnaam = new System.Windows.Forms.TextBox();
            this.xaantal = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xgereeddate = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xcancelb = new System.Windows.Forms.Button();
            this.xokb = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.xaantal)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // xgereednotitie
            // 
            this.xgereednotitie.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgereednotitie.Location = new System.Drawing.Point(23, 196);
            this.xgereednotitie.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xgereednotitie.Multiline = true;
            this.xgereednotitie.Name = "xgereednotitie";
            this.xgereednotitie.Size = new System.Drawing.Size(504, 77);
            this.xgereednotitie.TabIndex = 12;
            // 
            // xnaam
            // 
            this.xnaam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xnaam.Location = new System.Drawing.Point(104, 6);
            this.xnaam.Name = "xnaam";
            this.xnaam.Size = new System.Drawing.Size(267, 25);
            this.xnaam.TabIndex = 14;
            // 
            // xaantal
            // 
            this.xaantal.Location = new System.Drawing.Point(104, 37);
            this.xaantal.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantal.Name = "xaantal";
            this.xaantal.Size = new System.Drawing.Size(103, 25);
            this.xaantal.TabIndex = 15;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 17;
            this.label2.Text = "Paraaf: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 39);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(56, 17);
            this.label3.TabIndex = 18;
            this.label3.Text = "Aantal: ";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DarkRed;
            this.label4.Location = new System.Drawing.Point(19, 171);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 21);
            this.label4.TabIndex = 19;
            this.label4.Text = "Notitie: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 76);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 17);
            this.label1.TabIndex = 21;
            this.label1.Text = "Gereed Op:";
            // 
            // xgereeddate
            // 
            this.xgereeddate.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgereeddate.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xgereeddate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xgereeddate.Location = new System.Drawing.Point(104, 70);
            this.xgereeddate.Name = "xgereeddate";
            this.xgereeddate.Size = new System.Drawing.Size(267, 25);
            this.xgereeddate.TabIndex = 22;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.xgereeddate);
            this.panel1.Controls.Add(this.xnaam);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.xaantal);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Location = new System.Drawing.Point(153, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(374, 105);
            this.panel1.TabIndex = 23;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.notification_done_114461;
            this.pictureBox1.Location = new System.Drawing.Point(23, 63);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 105);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 24;
            this.pictureBox1.TabStop = false;
            // 
            // xcancelb
            // 
            this.xcancelb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xcancelb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xcancelb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xcancelb.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xcancelb.Location = new System.Drawing.Point(402, 281);
            this.xcancelb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xcancelb.Name = "xcancelb";
            this.xcancelb.Size = new System.Drawing.Size(125, 40);
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
            this.xokb.Location = new System.Drawing.Point(271, 281);
            this.xokb.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xokb.Name = "xokb";
            this.xokb.Size = new System.Drawing.Size(125, 40);
            this.xokb.TabIndex = 10;
            this.xokb.Text = "&OK";
            this.xokb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xokb.UseVisualStyleBackColor = true;
            this.xokb.Click += new System.EventHandler(this.xokb_Click);
            // 
            // DeelsGereedForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 325);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.xgereednotitie);
            this.Controls.Add(this.xcancelb);
            this.Controls.Add(this.xokb);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(550, 325);
            this.Name = "DeelsGereedForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wijzig Deels Gereed";
            ((System.ComponentModel.ISupportInitialize)(this.xaantal)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button xcancelb;
        private System.Windows.Forms.Button xokb;
        private System.Windows.Forms.TextBox xgereednotitie;
        private System.Windows.Forms.TextBox xnaam;
        private System.Windows.Forms.NumericUpDown xaantal;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker xgereeddate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}