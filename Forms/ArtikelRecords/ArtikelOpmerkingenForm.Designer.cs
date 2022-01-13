namespace Forms.ArtikelRecords
{
    partial class ArtikelOpmerkingenForm
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xOpmerkingenList = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn10 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn11 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn12 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.xdeleteOpmerking = new System.Windows.Forms.Button();
            this.xWijzigOpmerking = new System.Windows.Forms.Button();
            this.xAddOpmerking = new System.Windows.Forms.Button();
            this.xsearchbox = new MetroFramework.Controls.MetroTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xOK = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xOpmerkingenList)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xOpmerkingenList);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(776, 339);
            this.panel1.TabIndex = 6;
            // 
            // xOpmerkingenList
            // 
            this.xOpmerkingenList.AllColumns.Add(this.olvColumn9);
            this.xOpmerkingenList.AllColumns.Add(this.olvColumn10);
            this.xOpmerkingenList.AllColumns.Add(this.olvColumn11);
            this.xOpmerkingenList.AllColumns.Add(this.olvColumn12);
            this.xOpmerkingenList.AllColumns.Add(this.olvColumn1);
            this.xOpmerkingenList.AllColumns.Add(this.olvColumn2);
            this.xOpmerkingenList.CellEditUseWholeCell = false;
            this.xOpmerkingenList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn9,
            this.olvColumn10,
            this.olvColumn11,
            this.olvColumn12,
            this.olvColumn1,
            this.olvColumn2});
            this.xOpmerkingenList.Cursor = System.Windows.Forms.Cursors.Default;
            this.xOpmerkingenList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xOpmerkingenList.FullRowSelect = true;
            this.xOpmerkingenList.GridLines = true;
            this.xOpmerkingenList.HideSelection = false;
            this.xOpmerkingenList.LargeImageList = this.imageList1;
            this.xOpmerkingenList.Location = new System.Drawing.Point(0, 38);
            this.xOpmerkingenList.Margin = new System.Windows.Forms.Padding(4);
            this.xOpmerkingenList.Name = "xOpmerkingenList";
            this.xOpmerkingenList.ShowGroups = false;
            this.xOpmerkingenList.ShowItemToolTips = true;
            this.xOpmerkingenList.Size = new System.Drawing.Size(776, 301);
            this.xOpmerkingenList.SmallImageList = this.imageList1;
            this.xOpmerkingenList.TabIndex = 0;
            this.xOpmerkingenList.TintSortColumn = true;
            this.xOpmerkingenList.UseCompatibleStateImageBehavior = false;
            this.xOpmerkingenList.UseExplorerTheme = true;
            this.xOpmerkingenList.UseFilterIndicator = true;
            this.xOpmerkingenList.UseFiltering = true;
            this.xOpmerkingenList.UseHotItem = true;
            this.xOpmerkingenList.UseTranslucentHotItem = true;
            this.xOpmerkingenList.View = System.Windows.Forms.View.Details;
            this.xOpmerkingenList.SelectedIndexChanged += new System.EventHandler(this.xOpmerkingenList_SelectedIndexChanged);
            this.xOpmerkingenList.DoubleClick += new System.EventHandler(this.xWijzigOpmerking_Click);
            // 
            // olvColumn9
            // 
            this.olvColumn9.AspectName = "Opmerking";
            this.olvColumn9.AspectToStringFormat = "";
            this.olvColumn9.Text = "Opmerking";
            this.olvColumn9.Width = 250;
            this.olvColumn9.WordWrap = true;
            // 
            // olvColumn10
            // 
            this.olvColumn10.AspectName = "Filter";
            this.olvColumn10.Text = "Filter";
            this.olvColumn10.Width = 150;
            this.olvColumn10.WordWrap = true;
            // 
            // olvColumn11
            // 
            this.olvColumn11.AspectName = "FilterSoort";
            this.olvColumn11.Text = "FilterSoort";
            this.olvColumn11.Width = 100;
            // 
            // olvColumn12
            // 
            this.olvColumn12.AspectName = "FilterWaarde";
            this.olvColumn12.Text = "Filter Waarde";
            this.olvColumn12.Width = 120;
            this.olvColumn12.WordWrap = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "GeplaatstOp";
            this.olvColumn1.Text = "Geplaatst Op";
            this.olvColumn1.Width = 150;
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Ontvangers";
            this.olvColumn2.Text = "Ontvangers";
            this.olvColumn2.ToolTipText = "Iedereen die de opmerking krijgt te zien";
            this.olvColumn2.Width = 200;
            this.olvColumn2.WordWrap = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xdeleteOpmerking);
            this.panel3.Controls.Add(this.xWijzigOpmerking);
            this.panel3.Controls.Add(this.xAddOpmerking);
            this.panel3.Controls.Add(this.xsearchbox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(776, 38);
            this.panel3.TabIndex = 10;
            // 
            // xdeleteOpmerking
            // 
            this.xdeleteOpmerking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xdeleteOpmerking.Enabled = false;
            this.xdeleteOpmerking.FlatAppearance.BorderSize = 0;
            this.xdeleteOpmerking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdeleteOpmerking.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdeleteOpmerking.Location = new System.Drawing.Point(737, 1);
            this.xdeleteOpmerking.Name = "xdeleteOpmerking";
            this.xdeleteOpmerking.Size = new System.Drawing.Size(34, 34);
            this.xdeleteOpmerking.TabIndex = 12;
            this.xdeleteOpmerking.UseVisualStyleBackColor = true;
            this.xdeleteOpmerking.Click += new System.EventHandler(this.xdeleteOpmerking_Click);
            // 
            // xWijzigOpmerking
            // 
            this.xWijzigOpmerking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xWijzigOpmerking.Enabled = false;
            this.xWijzigOpmerking.FlatAppearance.BorderSize = 0;
            this.xWijzigOpmerking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xWijzigOpmerking.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xWijzigOpmerking.Location = new System.Drawing.Point(697, 1);
            this.xWijzigOpmerking.Name = "xWijzigOpmerking";
            this.xWijzigOpmerking.Size = new System.Drawing.Size(34, 34);
            this.xWijzigOpmerking.TabIndex = 11;
            this.xWijzigOpmerking.UseVisualStyleBackColor = true;
            this.xWijzigOpmerking.Click += new System.EventHandler(this.xWijzigOpmerking_Click);
            // 
            // xAddOpmerking
            // 
            this.xAddOpmerking.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xAddOpmerking.FlatAppearance.BorderSize = 0;
            this.xAddOpmerking.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xAddOpmerking.Image = global::ProductieManager.Properties.Resources.add_Blue_circle_32x32;
            this.xAddOpmerking.Location = new System.Drawing.Point(657, 1);
            this.xAddOpmerking.Name = "xAddOpmerking";
            this.xAddOpmerking.Size = new System.Drawing.Size(34, 34);
            this.xAddOpmerking.TabIndex = 10;
            this.xAddOpmerking.UseVisualStyleBackColor = true;
            this.xAddOpmerking.Click += new System.EventHandler(this.xAddOpmerking_Click);
            // 
            // xsearchbox
            // 
            this.xsearchbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.xsearchbox.CustomButton.Image = null;
            this.xsearchbox.CustomButton.Location = new System.Drawing.Point(622, 2);
            this.xsearchbox.CustomButton.Name = "";
            this.xsearchbox.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.xsearchbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xsearchbox.CustomButton.TabIndex = 1;
            this.xsearchbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xsearchbox.CustomButton.UseSelectable = true;
            this.xsearchbox.CustomButton.Visible = false;
            this.xsearchbox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xsearchbox.Lines = new string[] {
        "Zoeken..."};
            this.xsearchbox.Location = new System.Drawing.Point(0, 4);
            this.xsearchbox.Margin = new System.Windows.Forms.Padding(4);
            this.xsearchbox.MaxLength = 32767;
            this.xsearchbox.Name = "xsearchbox";
            this.xsearchbox.PasswordChar = '\0';
            this.xsearchbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xsearchbox.SelectedText = "";
            this.xsearchbox.SelectionLength = 0;
            this.xsearchbox.SelectionStart = 0;
            this.xsearchbox.ShortcutsEnabled = true;
            this.xsearchbox.ShowClearButton = true;
            this.xsearchbox.Size = new System.Drawing.Size(650, 30);
            this.xsearchbox.Style = MetroFramework.MetroColorStyle.Purple;
            this.xsearchbox.TabIndex = 9;
            this.xsearchbox.Text = "Zoeken...";
            this.xsearchbox.UseSelectable = true;
            this.xsearchbox.UseStyleColors = true;
            this.xsearchbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xsearchbox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xsearchbox.TextChanged += new System.EventHandler(this.xsearchArtikel_TextChanged);
            this.xsearchbox.Enter += new System.EventHandler(this.xsearchArtikel_Enter);
            this.xsearchbox.Leave += new System.EventHandler(this.xsearchArtikel_Leave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xOK);
            this.panel2.Controls.Add(this.button1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 399);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(776, 42);
            this.panel2.TabIndex = 7;
            // 
            // xOK
            // 
            this.xOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOK.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOK.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xOK.Location = new System.Drawing.Point(516, 5);
            this.xOK.Margin = new System.Windows.Forms.Padding(4);
            this.xOK.Name = "xOK";
            this.xOK.Size = new System.Drawing.Size(117, 32);
            this.xOK.TabIndex = 1;
            this.xOK.Text = "Opslaan";
            this.xOK.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xOK.UseVisualStyleBackColor = true;
            this.xOK.Click += new System.EventHandler(this.xOK_Click);
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(641, 5);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(130, 32);
            this.button1.TabIndex = 0;
            this.button1.Text = "Annuleren";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ArtikelOpmerkingenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(816, 461);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "ArtikelOpmerkingenForm";
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Artikel/ Werkplek Meldingen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArtikelOpmerkingenForm_FormClosing);
            this.Shown += new System.EventHandler(this.ArtikelOpmerkingenForm_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xOpmerkingenList)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private BrightIdeasSoftware.ObjectListView xOpmerkingenList;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button xdeleteOpmerking;
        private System.Windows.Forms.Button xWijzigOpmerking;
        private System.Windows.Forms.Button xAddOpmerking;
        private MetroFramework.Controls.MetroTextBox xsearchbox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xOK;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private BrightIdeasSoftware.OLVColumn olvColumn9;
        private BrightIdeasSoftware.OLVColumn olvColumn10;
        private BrightIdeasSoftware.OLVColumn olvColumn11;
        private BrightIdeasSoftware.OLVColumn olvColumn12;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
    }
}