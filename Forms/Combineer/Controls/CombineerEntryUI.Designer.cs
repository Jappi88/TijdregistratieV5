namespace Controls
{
    partial class CombineerEntryUI
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xCombiPeriode = new System.Windows.Forms.Button();
            this.xOpenProductie = new System.Windows.Forms.Button();
            this.xOnkoppel = new System.Windows.Forms.Button();
            this.xupdate = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xactiviteit = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xgroup = new System.Windows.Forms.GroupBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xactiviteit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.xgroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xOpenProductie);
            this.panel1.Controls.Add(this.xOnkoppel);
            this.panel1.Controls.Add(this.xupdate);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.xactiviteit);
            this.panel1.Controls.Add(this.xCombiPeriode);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(3, 62);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(574, 43);
            this.panel1.TabIndex = 0;
            // 
            // xCombiPeriode
            // 
            this.xCombiPeriode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xCombiPeriode.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xCombiPeriode.Image = global::ProductieManager.Properties.Resources.systemtime_778_32_32;
            this.xCombiPeriode.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xCombiPeriode.Location = new System.Drawing.Point(220, 3);
            this.xCombiPeriode.Name = "xCombiPeriode";
            this.xCombiPeriode.Size = new System.Drawing.Size(41, 37);
            this.xCombiPeriode.TabIndex = 6;
            this.xCombiPeriode.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xCombiPeriode, "Kies Actieve Periode");
            this.xCombiPeriode.UseVisualStyleBackColor = true;
            this.xCombiPeriode.Click += new System.EventHandler(this.xCombiPeriode_Click);
            // 
            // xOpenProductie
            // 
            this.xOpenProductie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xOpenProductie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOpenProductie.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOpenProductie.Image = global::ProductieManager.Properties.Resources.window_16756_32x32;
            this.xOpenProductie.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xOpenProductie.Location = new System.Drawing.Point(316, 3);
            this.xOpenProductie.Name = "xOpenProductie";
            this.xOpenProductie.Size = new System.Drawing.Size(115, 37);
            this.xOpenProductie.TabIndex = 5;
            this.xOpenProductie.Text = "Productie";
            this.xOpenProductie.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xOpenProductie, "Toon Productie");
            this.xOpenProductie.UseVisualStyleBackColor = true;
            this.xOpenProductie.Click += new System.EventHandler(this.xOpenProductie_Click);
            // 
            // xOnkoppel
            // 
            this.xOnkoppel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xOnkoppel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOnkoppel.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOnkoppel.Image = global::ProductieManager.Properties.Resources.UnMerge_arrows_32x32;
            this.xOnkoppel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xOnkoppel.Location = new System.Drawing.Point(434, 3);
            this.xOnkoppel.Name = "xOnkoppel";
            this.xOnkoppel.Size = new System.Drawing.Size(140, 37);
            this.xOnkoppel.TabIndex = 0;
            this.xOnkoppel.Text = "Ontkoppelen";
            this.xOnkoppel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xOnkoppel, "Onkoppel Productie");
            this.xOnkoppel.UseVisualStyleBackColor = true;
            this.xOnkoppel.Click += new System.EventHandler(this.xOnkoppel_Click);
            // 
            // xupdate
            // 
            this.xupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xupdate.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xupdate.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.xupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xupdate.Location = new System.Drawing.Point(176, 3);
            this.xupdate.Name = "xupdate";
            this.xupdate.Size = new System.Drawing.Size(41, 37);
            this.xupdate.TabIndex = 4;
            this.xupdate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xupdate, "Voer wijzigingen door");
            this.xupdate.UseVisualStyleBackColor = true;
            this.xupdate.Click += new System.EventHandler(this.xupdate_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(154, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(24, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "%";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Activiteit";
            // 
            // xactiviteit
            // 
            this.xactiviteit.DecimalPlaces = 2;
            this.xactiviteit.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xactiviteit.Location = new System.Drawing.Point(82, 9);
            this.xactiviteit.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            131072});
            this.xactiviteit.Name = "xactiviteit";
            this.xactiviteit.Size = new System.Drawing.Size(70, 27);
            this.xactiviteit.TabIndex = 1;
            this.toolTip1.SetToolTip(this.xactiviteit, "Het aantal procent dat de productie actief is");
            this.xactiviteit.Value = new decimal(new int[] {
            10,
            0,
            0,
            131072});
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.operation;
            this.pictureBox1.Location = new System.Drawing.Point(10, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(76, 108);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // xgroup
            // 
            this.xgroup.Controls.Add(this.panel1);
            this.xgroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xgroup.ForeColor = System.Drawing.Color.SteelBlue;
            this.xgroup.Location = new System.Drawing.Point(86, 10);
            this.xgroup.Name = "xgroup";
            this.xgroup.Size = new System.Drawing.Size(580, 108);
            this.xgroup.TabIndex = 3;
            this.xgroup.TabStop = false;
            // 
            // CombineerEntryUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xgroup);
            this.Controls.Add(this.pictureBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "CombineerEntryUI";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(676, 128);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xactiviteit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.xgroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button xOnkoppel;
        private System.Windows.Forms.Button xOpenProductie;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button xupdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown xactiviteit;
        private System.Windows.Forms.GroupBox xgroup;
        private System.Windows.Forms.Button xCombiPeriode;
    }
}
