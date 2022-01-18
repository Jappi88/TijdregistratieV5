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
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xoptimalemaat = new System.Windows.Forms.Button();
            this.xinfo = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.xaantal = new System.Windows.Forms.Label();
            this.xprodlabel = new System.Windows.Forms.Label();
            this.xproduceren = new System.Windows.Forms.NumericUpDown();
            this.xprodlengte = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.xuitganglengte = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.xaantalsporen = new System.Windows.Forms.NumericUpDown();
            this.xlengtelabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xrekenmachine = new System.Windows.Forms.Button();
            this.xopslaan = new System.Windows.Forms.Button();
            this.xaangepastdoor = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.xmaterialen = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xproduceren)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xprodlengte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xuitganglengte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalsporen)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.xmaterialen);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(634, 446);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Verbruik Berekenen";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xoptimalemaat);
            this.panel1.Controls.Add(this.xinfo);
            this.panel1.Controls.Add(this.xaantal);
            this.panel1.Controls.Add(this.xprodlabel);
            this.panel1.Controls.Add(this.xproduceren);
            this.panel1.Controls.Add(this.xprodlengte);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.xuitganglengte);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.xaantalsporen);
            this.panel1.Controls.Add(this.xlengtelabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 54);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(628, 340);
            this.panel1.TabIndex = 15;
            // 
            // xoptimalemaat
            // 
            this.xoptimalemaat.BackColor = System.Drawing.Color.White;
            this.xoptimalemaat.FlatAppearance.BorderSize = 0;
            this.xoptimalemaat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xoptimalemaat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xoptimalemaat.Image = global::ProductieManager.Properties.Resources.geometry_measure_32x32;
            this.xoptimalemaat.Location = new System.Drawing.Point(293, 28);
            this.xoptimalemaat.Name = "xoptimalemaat";
            this.xoptimalemaat.Size = new System.Drawing.Size(35, 35);
            this.xoptimalemaat.TabIndex = 13;
            this.xoptimalemaat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xoptimalemaat, "Bereken Optimale UitgangsLengte");
            this.xoptimalemaat.UseVisualStyleBackColor = false;
            this.xoptimalemaat.Click += new System.EventHandler(this.xoptimalemaat_Click);
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
            this.xinfo.Location = new System.Drawing.Point(3, 69);
            this.xinfo.Name = "xinfo";
            this.xinfo.Size = new System.Drawing.Size(622, 268);
            this.xinfo.TabIndex = 8;
            this.xinfo.Text = null;
            // 
            // xaantal
            // 
            this.xaantal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xaantal.AutoSize = true;
            this.xaantal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantal.Location = new System.Drawing.Point(491, 4);
            this.xaantal.Name = "xaantal";
            this.xaantal.Size = new System.Drawing.Size(108, 21);
            this.xaantal.TabIndex = 14;
            this.xaantal.Text = "Te Produceren";
            // 
            // xprodlabel
            // 
            this.xprodlabel.AutoSize = true;
            this.xprodlabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprodlabel.Location = new System.Drawing.Point(3, 4);
            this.xprodlabel.Name = "xprodlabel";
            this.xprodlabel.Size = new System.Drawing.Size(115, 21);
            this.xprodlabel.TabIndex = 4;
            this.xprodlabel.Text = "Product Lengte";
            // 
            // xproduceren
            // 
            this.xproduceren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xproduceren.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xproduceren.ForeColor = System.Drawing.Color.Navy;
            this.xproduceren.Location = new System.Drawing.Point(495, 32);
            this.xproduceren.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xproduceren.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xproduceren.Name = "xproduceren";
            this.xproduceren.Size = new System.Drawing.Size(121, 29);
            this.xproduceren.TabIndex = 13;
            this.xproduceren.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xproduceren.ThousandsSeparator = true;
            this.xproduceren.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xproduceren.ValueChanged += new System.EventHandler(this.xproduceren_ValueChanged);
            this.xproduceren.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xprodlengte_KeyDown);
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
            this.xprodlengte.Location = new System.Drawing.Point(7, 32);
            this.xprodlengte.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xprodlengte.Name = "xprodlengte";
            this.xprodlengte.Size = new System.Drawing.Size(84, 29);
            this.xprodlengte.TabIndex = 0;
            this.xprodlengte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xprodlengte.ValueChanged += new System.EventHandler(this.xprodlengte_ValueChanged);
            this.xprodlengte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xprodlengte_KeyDown);
            this.xprodlengte.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.xprodlengte_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(97, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "mm";
            // 
            // xuitganglengte
            // 
            this.xuitganglengte.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xuitganglengte.ForeColor = System.Drawing.Color.Navy;
            this.xuitganglengte.Location = new System.Drawing.Point(202, 32);
            this.xuitganglengte.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xuitganglengte.Name = "xuitganglengte";
            this.xuitganglengte.Size = new System.Drawing.Size(88, 29);
            this.xuitganglengte.TabIndex = 2;
            this.xuitganglengte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xuitganglengte.ThousandsSeparator = true;
            this.xuitganglengte.Value = new decimal(new int[] {
            7000,
            0,
            0,
            0});
            this.xuitganglengte.ValueChanged += new System.EventHandler(this.xuitganglengte_ValueChanged);
            this.xuitganglengte.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xprodlengte_KeyDown);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(377, 4);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(108, 21);
            this.label3.TabIndex = 10;
            this.label3.Text = "Aantal Sporen";
            // 
            // xaantalsporen
            // 
            this.xaantalsporen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xaantalsporen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantalsporen.ForeColor = System.Drawing.Color.Navy;
            this.xaantalsporen.Location = new System.Drawing.Point(381, 32);
            this.xaantalsporen.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantalsporen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xaantalsporen.Name = "xaantalsporen";
            this.xaantalsporen.Size = new System.Drawing.Size(89, 29);
            this.xaantalsporen.TabIndex = 9;
            this.xaantalsporen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xaantalsporen.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xaantalsporen.ValueChanged += new System.EventHandler(this.xaantalsporen_ValueChanged);
            this.xaantalsporen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xprodlengte_KeyDown);
            // 
            // xlengtelabel
            // 
            this.xlengtelabel.AutoSize = true;
            this.xlengtelabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlengtelabel.Location = new System.Drawing.Point(198, 4);
            this.xlengtelabel.Name = "xlengtelabel";
            this.xlengtelabel.Size = new System.Drawing.Size(115, 21);
            this.xlengtelabel.TabIndex = 5;
            this.xlengtelabel.Text = "Uitgangslengte";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xrekenmachine);
            this.panel2.Controls.Add(this.xopslaan);
            this.panel2.Controls.Add(this.xaangepastdoor);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 394);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(628, 49);
            this.panel2.TabIndex = 16;
            // 
            // xrekenmachine
            // 
            this.xrekenmachine.BackColor = System.Drawing.Color.White;
            this.xrekenmachine.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xrekenmachine.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrekenmachine.Image = global::ProductieManager.Properties.Resources.calculator_icon_icons_com_72046;
            this.xrekenmachine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xrekenmachine.Location = new System.Drawing.Point(3, 6);
            this.xrekenmachine.Name = "xrekenmachine";
            this.xrekenmachine.Size = new System.Drawing.Size(153, 38);
            this.xrekenmachine.TabIndex = 7;
            this.xrekenmachine.Text = "Rekenmachine";
            this.xrekenmachine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xrekenmachine.UseVisualStyleBackColor = false;
            this.xrekenmachine.Click += new System.EventHandler(this.xrekenmachine_Click);
            // 
            // xopslaan
            // 
            this.xopslaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xopslaan.BackColor = System.Drawing.Color.White;
            this.xopslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xopslaan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xopslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xopslaan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xopslaan.Location = new System.Drawing.Point(504, 6);
            this.xopslaan.Name = "xopslaan";
            this.xopslaan.Size = new System.Drawing.Size(121, 38);
            this.xopslaan.TabIndex = 11;
            this.xopslaan.Text = "Opslaan";
            this.xopslaan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xopslaan.UseVisualStyleBackColor = false;
            this.xopslaan.Click += new System.EventHandler(this.xopslaan_Click);
            // 
            // xaangepastdoor
            // 
            this.xaangepastdoor.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xaangepastdoor.AutoSize = false;
            this.xaangepastdoor.BackColor = System.Drawing.SystemColors.Window;
            this.xaangepastdoor.BaseStylesheet = null;
            this.xaangepastdoor.IsContextMenuEnabled = false;
            this.xaangepastdoor.IsSelectionEnabled = false;
            this.xaangepastdoor.Location = new System.Drawing.Point(162, 6);
            this.xaangepastdoor.Name = "xaangepastdoor";
            this.xaangepastdoor.Size = new System.Drawing.Size(336, 38);
            this.xaangepastdoor.TabIndex = 12;
            this.xaangepastdoor.Text = null;
            // 
            // xmaterialen
            // 
            this.xmaterialen.Dock = System.Windows.Forms.DockStyle.Top;
            this.xmaterialen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmaterialen.FormattingEnabled = true;
            this.xmaterialen.Location = new System.Drawing.Point(3, 25);
            this.xmaterialen.Name = "xmaterialen";
            this.xmaterialen.Size = new System.Drawing.Size(628, 29);
            this.xmaterialen.TabIndex = 6;
            this.xmaterialen.SelectedIndexChanged += new System.EventHandler(this.xmaterialen_SelectedIndexChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
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
            this.Size = new System.Drawing.Size(644, 456);
            this.groupBox1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xproduceren)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xprodlengte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xuitganglengte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalsporen)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button xrekenmachine;
        private System.Windows.Forms.ComboBox xmaterialen;
        private System.Windows.Forms.Label xlengtelabel;
        private System.Windows.Forms.Label xprodlabel;
        private System.Windows.Forms.NumericUpDown xuitganglengte;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown xprodlengte;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel xinfo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.NumericUpDown xaantalsporen;
        private System.Windows.Forms.Button xopslaan;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel xaangepastdoor;
        private System.Windows.Forms.Label xaantal;
        private System.Windows.Forms.NumericUpDown xproduceren;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xoptimalemaat;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
