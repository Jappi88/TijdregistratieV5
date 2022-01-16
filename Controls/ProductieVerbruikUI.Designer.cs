namespace Controls
{
    partial class ProductieVerbruikUI
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xrekenmachine = new System.Windows.Forms.Button();
            this.xmaterialen = new System.Windows.Forms.ComboBox();
            this.xlengtelabel = new System.Windows.Forms.Label();
            this.xprodlabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.xuitganglengte = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.xprodlengte = new System.Windows.Forms.NumericUpDown();
            this.xinfo = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xuitganglengte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xprodlengte)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.xinfo);
            this.groupBox1.Controls.Add(this.xrekenmachine);
            this.groupBox1.Controls.Add(this.xmaterialen);
            this.groupBox1.Controls.Add(this.xlengtelabel);
            this.groupBox1.Controls.Add(this.xprodlabel);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.xuitganglengte);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.xprodlengte);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(574, 418);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Verbruik Berekenen";
            // 
            // xrekenmachine
            // 
            this.xrekenmachine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xrekenmachine.BackColor = System.Drawing.Color.White;
            this.xrekenmachine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xrekenmachine.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrekenmachine.Image = global::ProductieManager.Properties.Resources.calculator_icon_icons_com_72046;
            this.xrekenmachine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xrekenmachine.Location = new System.Drawing.Point(10, 374);
            this.xrekenmachine.Name = "xrekenmachine";
            this.xrekenmachine.Size = new System.Drawing.Size(153, 38);
            this.xrekenmachine.TabIndex = 7;
            this.xrekenmachine.Text = "Rekenmachine";
            this.xrekenmachine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xrekenmachine.UseVisualStyleBackColor = false;
            this.xrekenmachine.Click += new System.EventHandler(this.xrekenmachine_Click);
            // 
            // xmaterialen
            // 
            this.xmaterialen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xmaterialen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmaterialen.FormattingEnabled = true;
            this.xmaterialen.Location = new System.Drawing.Point(6, 28);
            this.xmaterialen.Name = "xmaterialen";
            this.xmaterialen.Size = new System.Drawing.Size(562, 29);
            this.xmaterialen.TabIndex = 6;
            this.xmaterialen.SelectedIndexChanged += new System.EventHandler(this.xmaterialen_SelectedIndexChanged);
            // 
            // xlengtelabel
            // 
            this.xlengtelabel.AutoSize = true;
            this.xlengtelabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlengtelabel.Location = new System.Drawing.Point(206, 60);
            this.xlengtelabel.Name = "xlengtelabel";
            this.xlengtelabel.Size = new System.Drawing.Size(115, 21);
            this.xlengtelabel.TabIndex = 5;
            this.xlengtelabel.Text = "Uitgangslengte";
            // 
            // xprodlabel
            // 
            this.xprodlabel.AutoSize = true;
            this.xprodlabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprodlabel.Location = new System.Drawing.Point(6, 60);
            this.xprodlabel.Name = "xprodlabel";
            this.xprodlabel.Size = new System.Drawing.Size(115, 21);
            this.xprodlabel.TabIndex = 4;
            this.xprodlabel.Text = "Product Lengte";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(366, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(40, 21);
            this.label2.TabIndex = 3;
            this.label2.Text = "mm";
            // 
            // xuitganglengte
            // 
            this.xuitganglengte.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xuitganglengte.ForeColor = System.Drawing.Color.Navy;
            this.xuitganglengte.Location = new System.Drawing.Point(210, 84);
            this.xuitganglengte.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xuitganglengte.Name = "xuitganglengte";
            this.xuitganglengte.Size = new System.Drawing.Size(150, 29);
            this.xuitganglengte.TabIndex = 2;
            this.xuitganglengte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xuitganglengte.Value = new decimal(new int[] {
            7000,
            0,
            0,
            0});
            this.xuitganglengte.ValueChanged += new System.EventHandler(this.xprodlengte_ValueChanged_1);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(166, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "mm";
            // 
            // xprodlengte
            // 
            this.xprodlengte.DecimalPlaces = 2;
            this.xprodlengte.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprodlengte.ForeColor = System.Drawing.Color.Navy;
            this.xprodlengte.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.xprodlengte.Location = new System.Drawing.Point(10, 84);
            this.xprodlengte.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xprodlengte.Name = "xprodlengte";
            this.xprodlengte.Size = new System.Drawing.Size(150, 29);
            this.xprodlengte.TabIndex = 0;
            this.xprodlengte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xprodlengte.ValueChanged += new System.EventHandler(this.xprodlengte_ValueChanged_1);
            // 
            // xinfo
            // 
            this.xinfo.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xinfo.AutoSize = false;
            this.xinfo.BackColor = System.Drawing.SystemColors.Window;
            this.xinfo.BaseStylesheet = null;
            this.xinfo.IsContextMenuEnabled = false;
            this.xinfo.IsSelectionEnabled = false;
            this.xinfo.Location = new System.Drawing.Point(10, 120);
            this.xinfo.Name = "xinfo";
            this.xinfo.Size = new System.Drawing.Size(558, 248);
            this.xinfo.TabIndex = 8;
            // 
            // ProductieVerbruikUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ProductieVerbruikUI";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(584, 428);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xuitganglengte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xprodlengte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button xrekenmachine;
        private System.Windows.Forms.ComboBox xmaterialen;
        private System.Windows.Forms.Label xlengtelabel;
        private System.Windows.Forms.Label xprodlabel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown xuitganglengte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown xprodlengte;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel xinfo;
    }
}
