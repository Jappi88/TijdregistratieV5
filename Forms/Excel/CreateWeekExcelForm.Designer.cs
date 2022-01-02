namespace Forms.Excel
{
    partial class CreateWeekExcelForm
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
            this.xopenexcel = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xOpslaan = new System.Windows.Forms.Button();
            this.xweeknr = new System.Windows.Forms.NumericUpDown();
            this.xjaar = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.xfiltersStrip = new System.Windows.Forms.MenuStrip();
            this.xfiltertoolstrip = new System.Windows.Forms.ToolStripMenuItem();
            this.xbeheerfilterstoolstripbutton = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.xbezig = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xweeknr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xjaar)).BeginInit();
            this.xfiltersStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // xopenexcel
            // 
            this.xopenexcel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xopenexcel.AutoSize = true;
            this.xopenexcel.Checked = true;
            this.xopenexcel.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xopenexcel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xopenexcel.Location = new System.Drawing.Point(382, 141);
            this.xopenexcel.Name = "xopenexcel";
            this.xopenexcel.Size = new System.Drawing.Size(95, 21);
            this.xopenexcel.TabIndex = 14;
            this.xopenexcel.Text = "Open Excel";
            this.xopenexcel.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(150, 139);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Week ";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.microsoft_excel_22733_128x128;
            this.pictureBox1.Location = new System.Drawing.Point(20, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(124, 195);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Controls.Add(this.xOpslaan);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(144, 210);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(346, 45);
            this.panel1.TabIndex = 12;
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.ForeColor = System.Drawing.Color.Black;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(224, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(119, 38);
            this.xsluiten.TabIndex = 6;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xOpslaan
            // 
            this.xOpslaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xOpslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOpslaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOpslaan.ForeColor = System.Drawing.Color.Black;
            this.xOpslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xOpslaan.Location = new System.Drawing.Point(99, 3);
            this.xOpslaan.Name = "xOpslaan";
            this.xOpslaan.Size = new System.Drawing.Size(119, 38);
            this.xOpslaan.TabIndex = 7;
            this.xOpslaan.Text = "Opslaan";
            this.xOpslaan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xOpslaan.UseVisualStyleBackColor = true;
            this.xOpslaan.Click += new System.EventHandler(this.xOpslaan_Click);
            // 
            // xweeknr
            // 
            this.xweeknr.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xweeknr.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xweeknr.Location = new System.Drawing.Point(213, 137);
            this.xweeknr.Maximum = new decimal(new int[] {
            52,
            0,
            0,
            0});
            this.xweeknr.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xweeknr.Name = "xweeknr";
            this.xweeknr.Size = new System.Drawing.Size(50, 29);
            this.xweeknr.TabIndex = 2;
            this.xweeknr.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // xjaar
            // 
            this.xjaar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xjaar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xjaar.Location = new System.Drawing.Point(316, 137);
            this.xjaar.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.xjaar.Minimum = new decimal(new int[] {
            2021,
            0,
            0,
            0});
            this.xjaar.Name = "xjaar";
            this.xjaar.Size = new System.Drawing.Size(60, 29);
            this.xjaar.TabIndex = 20;
            this.xjaar.Value = new decimal(new int[] {
            2021,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(269, 139);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 21);
            this.label2.TabIndex = 19;
            this.label2.Text = "Jaar";
            // 
            // xfiltersStrip
            // 
            this.xfiltersStrip.AllowMerge = false;
            this.xfiltersStrip.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xfiltersStrip.AutoSize = false;
            this.xfiltersStrip.BackColor = System.Drawing.Color.Transparent;
            this.xfiltersStrip.Dock = System.Windows.Forms.DockStyle.None;
            this.xfiltersStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xfiltertoolstrip});
            this.xfiltersStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.xfiltersStrip.Location = new System.Drawing.Point(148, 60);
            this.xfiltersStrip.Name = "xfiltersStrip";
            this.xfiltersStrip.ShowItemToolTips = true;
            this.xfiltersStrip.Size = new System.Drawing.Size(342, 74);
            this.xfiltersStrip.TabIndex = 21;
            this.xfiltersStrip.Text = "menuStrip1";
            // 
            // xfiltertoolstrip
            // 
            this.xfiltertoolstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xbeheerfilterstoolstripbutton,
            this.toolStripSeparator15});
            this.xfiltertoolstrip.Image = global::ProductieManager.Properties.Resources.filter_icon_icons_com_70971;
            this.xfiltertoolstrip.Name = "xfiltertoolstrip";
            this.xfiltertoolstrip.Size = new System.Drawing.Size(98, 20);
            this.xfiltertoolstrip.Text = "Filter Regels";
            this.xfiltertoolstrip.ToolTipText = "Kies productie filter";
            this.xfiltertoolstrip.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.xfiltersStripItem_DropDownItemClicked);
            // 
            // xbeheerfilterstoolstripbutton
            // 
            this.xbeheerfilterstoolstripbutton.Image = global::ProductieManager.Properties.Resources.filter_filters_navigation_32x32;
            this.xbeheerfilterstoolstripbutton.Name = "xbeheerfilterstoolstripbutton";
            this.xbeheerfilterstoolstripbutton.Size = new System.Drawing.Size(147, 22);
            this.xbeheerfilterstoolstripbutton.Text = "Beheer Regels";
            this.xbeheerfilterstoolstripbutton.ToolTipText = "Beheer filters";
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(144, 6);
            // 
            // xbezig
            // 
            this.xbezig.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xbezig.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbezig.ForeColor = System.Drawing.Color.DarkGreen;
            this.xbezig.Location = new System.Drawing.Point(144, 180);
            this.xbezig.Name = "xbezig";
            this.xbezig.Size = new System.Drawing.Size(346, 30);
            this.xbezig.TabIndex = 8;
            this.xbezig.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // CreateWeekExcelForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 275);
            this.Controls.Add(this.xbezig);
            this.Controls.Add(this.xfiltersStrip);
            this.Controls.Add(this.xjaar);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.xweeknr);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.xopenexcel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.MinimumSize = new System.Drawing.Size(510, 275);
            this.Name = "CreateWeekExcelForm";
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Creëer Week Overzicht";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xweeknr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xjaar)).EndInit();
            this.xfiltersStrip.ResumeLayout(false);
            this.xfiltersStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.CheckBox xopenexcel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xOpslaan;
        private System.Windows.Forms.NumericUpDown xweeknr;
        private System.Windows.Forms.NumericUpDown xjaar;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip xfiltersStrip;
        private System.Windows.Forms.ToolStripMenuItem xfiltertoolstrip;
        private System.Windows.Forms.ToolStripMenuItem xbeheerfilterstoolstripbutton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.Label xbezig;
    }
}