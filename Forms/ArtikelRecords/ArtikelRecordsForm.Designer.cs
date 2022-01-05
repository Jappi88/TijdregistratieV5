namespace Forms.ArtikelRecords
{
    partial class ArtikelRecordsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArtikelRecordsForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xArtikelList = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xsearchbox = new MetroFramework.Controls.MetroTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xArtikelList)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xArtikelList);
            this.panel1.Controls.Add(this.xsearchbox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(23, 78);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(824, 365);
            this.panel1.TabIndex = 0;
            // 
            // xArtikelList
            // 
            this.xArtikelList.AllColumns.Add(this.olvColumn1);
            this.xArtikelList.AllColumns.Add(this.olvColumn2);
            this.xArtikelList.AllColumns.Add(this.olvColumn4);
            this.xArtikelList.AllColumns.Add(this.olvColumn5);
            this.xArtikelList.AllColumns.Add(this.olvColumn8);
            this.xArtikelList.AllColumns.Add(this.olvColumn3);
            this.xArtikelList.AllColumns.Add(this.olvColumn6);
            this.xArtikelList.AllColumns.Add(this.olvColumn7);
            this.xArtikelList.CellEditUseWholeCell = false;
            this.xArtikelList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn4,
            this.olvColumn5,
            this.olvColumn8,
            this.olvColumn3,
            this.olvColumn6,
            this.olvColumn7});
            this.xArtikelList.Cursor = System.Windows.Forms.Cursors.Default;
            this.xArtikelList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xArtikelList.FullRowSelect = true;
            this.xArtikelList.GridLines = true;
            this.xArtikelList.HideSelection = false;
            this.xArtikelList.LargeImageList = this.imageList1;
            this.xArtikelList.Location = new System.Drawing.Point(0, 30);
            this.xArtikelList.Margin = new System.Windows.Forms.Padding(4);
            this.xArtikelList.Name = "xArtikelList";
            this.xArtikelList.ShowGroups = false;
            this.xArtikelList.ShowItemToolTips = true;
            this.xArtikelList.Size = new System.Drawing.Size(824, 335);
            this.xArtikelList.SmallImageList = this.imageList1;
            this.xArtikelList.TabIndex = 0;
            this.xArtikelList.TintSortColumn = true;
            this.xArtikelList.UseCompatibleStateImageBehavior = false;
            this.xArtikelList.UseExplorerTheme = true;
            this.xArtikelList.UseFilterIndicator = true;
            this.xArtikelList.UseFiltering = true;
            this.xArtikelList.UseHotItem = true;
            this.xArtikelList.UseTranslucentHotItem = true;
            this.xArtikelList.View = System.Windows.Forms.View.Details;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "ArtikelNr";
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "ArtikelNr";
            this.olvColumn1.Width = 120;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Omschrijving";
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Omschrijving";
            this.olvColumn2.Width = 250;
            this.olvColumn2.WordWrap = true;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "AantalGemaakt";
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.Text = "Gemaakt";
            this.olvColumn4.ToolTipText = "Totaal aantal gemaakt";
            this.olvColumn4.Width = 100;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "TijdGewerkt";
            this.olvColumn5.AspectToStringFormat = "{0} uur";
            this.olvColumn5.IsEditable = false;
            this.olvColumn5.Text = "Tijd Gewerkt";
            this.olvColumn5.ToolTipText = "Totaal tijd Gewerkt";
            this.olvColumn5.Width = 120;
            this.olvColumn5.WordWrap = true;
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "PerUur";
            this.olvColumn8.AspectToStringFormat = "{0} p/u";
            this.olvColumn8.IsEditable = false;
            this.olvColumn8.Text = "Aantal P/u";
            this.olvColumn8.ToolTipText = "Gemiddeld aantal per uur";
            this.olvColumn8.Width = 120;
            this.olvColumn8.WordWrap = true;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Vanaf";
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.Text = "Gemeten Vanaf";
            this.olvColumn3.ToolTipText = "De datum waarop begonnen is met meten";
            this.olvColumn3.Width = 120;
            this.olvColumn3.WordWrap = true;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "LaatstGeupdate";
            this.olvColumn6.IsEditable = false;
            this.olvColumn6.Text = "Laatst Geüpdatet";
            this.olvColumn6.Width = 120;
            this.olvColumn6.WordWrap = true;
            // 
            // olvColumn7
            // 
            this.olvColumn7.IsEditable = false;
            this.olvColumn7.Text = "Aantal Producties";
            this.olvColumn7.ToolTipText = "Aantal producties gemeten";
            this.olvColumn7.Width = 100;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xsearchbox
            // 
            // 
            // 
            // 
            this.xsearchbox.CustomButton.Image = null;
            this.xsearchbox.CustomButton.Location = new System.Drawing.Point(796, 2);
            this.xsearchbox.CustomButton.Name = "";
            this.xsearchbox.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.xsearchbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xsearchbox.CustomButton.TabIndex = 1;
            this.xsearchbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xsearchbox.CustomButton.UseSelectable = true;
            this.xsearchbox.CustomButton.Visible = false;
            this.xsearchbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.xsearchbox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xsearchbox.Lines = new string[] {
        "Zoeken..."};
            this.xsearchbox.Location = new System.Drawing.Point(0, 0);
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
            this.xsearchbox.Size = new System.Drawing.Size(824, 30);
            this.xsearchbox.TabIndex = 9;
            this.xsearchbox.Text = "Zoeken...";
            this.xsearchbox.UseSelectable = true;
            this.xsearchbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xsearchbox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xsearchbox.TextChanged += new System.EventHandler(this.xsearchArtikel_TextChanged);
            this.xsearchbox.Enter += new System.EventHandler(this.xsearchArtikel_Enter);
            this.xsearchbox.Leave += new System.EventHandler(this.xsearchArtikel_Leave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(23, 443);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(824, 36);
            this.panel2.TabIndex = 5;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(692, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(132, 36);
            this.panel3.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Margin = new System.Windows.Forms.Padding(4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(132, 36);
            this.button1.TabIndex = 0;
            this.button1.Text = "Sluiten";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // ArtikelRecordsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(870, 505);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ArtikelRecordsForm";
            this.Padding = new System.Windows.Forms.Padding(23, 78, 23, 26);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Artikel Records";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArtikelRecordsForm_FormClosing);
            this.Shown += new System.EventHandler(this.ArtikelRecordsForm_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xArtikelList)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private BrightIdeasSoftware.ObjectListView xArtikelList;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private MetroFramework.Controls.MetroTextBox xsearchbox;
        private System.Windows.Forms.ImageList imageList1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
    }
}