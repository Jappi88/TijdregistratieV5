
namespace Controls
{
    partial class PersoonVaardigheden
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersoonVaardigheden));
            this.xomschrijving = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xskillview = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.xstatuslabel = new System.Windows.Forms.Label();
            this.xclosepanel = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xsearchbox = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xskillview)).BeginInit();
            this.panel2.SuspendLayout();
            this.xclosepanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // xomschrijving
            // 
            this.xomschrijving.Dock = System.Windows.Forms.DockStyle.Top;
            this.xomschrijving.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xomschrijving.Location = new System.Drawing.Point(0, 0);
            this.xomschrijving.Name = "xomschrijving";
            this.xomschrijving.Size = new System.Drawing.Size(939, 66);
            this.xomschrijving.TabIndex = 0;
            this.xomschrijving.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xskillview);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.xsearchbox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 513);
            this.panel1.TabIndex = 1;
            // 
            // xskillview
            // 
            this.xskillview.AllColumns.Add(this.olvColumn1);
            this.xskillview.AllColumns.Add(this.olvColumn2);
            this.xskillview.AllColumns.Add(this.olvColumn6);
            this.xskillview.AllColumns.Add(this.olvColumn5);
            this.xskillview.AllColumns.Add(this.olvColumn7);
            this.xskillview.AllColumns.Add(this.olvColumn3);
            this.xskillview.AllColumns.Add(this.olvColumn4);
            this.xskillview.AllowColumnReorder = true;
            this.xskillview.CellEditUseWholeCell = false;
            this.xskillview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn6,
            this.olvColumn5,
            this.olvColumn7,
            this.olvColumn3,
            this.olvColumn4});
            this.xskillview.Cursor = System.Windows.Forms.Cursors.Default;
            this.xskillview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xskillview.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xskillview.FullRowSelect = true;
            this.xskillview.HideSelection = false;
            this.xskillview.LargeImageList = this.imageList1;
            this.xskillview.Location = new System.Drawing.Point(0, 29);
            this.xskillview.Name = "xskillview";
            this.xskillview.Size = new System.Drawing.Size(939, 440);
            this.xskillview.SmallImageList = this.imageList1;
            this.xskillview.SpaceBetweenGroups = 5;
            this.xskillview.TabIndex = 1;
            this.xskillview.UseCompatibleStateImageBehavior = false;
            this.xskillview.UseExplorerTheme = true;
            this.xskillview.UseFilterIndicator = true;
            this.xskillview.UseFiltering = true;
            this.xskillview.UseHotItem = true;
            this.xskillview.UseTranslucentHotItem = true;
            this.xskillview.View = System.Windows.Forms.View.Details;
            this.xskillview.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.xskillview_CellToolTipShowing);
            this.xskillview.SelectedIndexChanged += new System.EventHandler(this.xskillview_SelectionChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Omschrijving";
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.Text = "Omschrijving";
            this.olvColumn1.ToolTipText = "Omschrijving";
            this.olvColumn1.Width = 350;
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "ArtikelNr";
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.IsTileViewColumn = true;
            this.olvColumn2.Text = "ArtikelNr";
            this.olvColumn2.ToolTipText = "Product ArtikelNr";
            this.olvColumn2.Width = 120;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "Path";
            this.olvColumn6.IsEditable = false;
            this.olvColumn6.IsTileViewColumn = true;
            this.olvColumn6.Text = "Productie";
            this.olvColumn6.ToolTipText = "Productie";
            this.olvColumn6.Width = 200;
            this.olvColumn6.WordWrap = true;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "TijdGewerkt";
            this.olvColumn5.AspectToStringFormat = "{0} uur";
            this.olvColumn5.IsEditable = false;
            this.olvColumn5.IsTileViewColumn = true;
            this.olvColumn5.Text = "Tijd Gewerkt";
            this.olvColumn5.ToolTipText = "Totaal Tijd Gewerkt";
            this.olvColumn5.Width = 120;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "WerkPlek";
            this.olvColumn7.IsEditable = false;
            this.olvColumn7.IsTileViewColumn = true;
            this.olvColumn7.Text = "WerkPlek";
            this.olvColumn7.ToolTipText = "Werkplek";
            this.olvColumn7.Width = 175;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Gestart";
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.IsTileViewColumn = true;
            this.olvColumn3.Text = "Gestart";
            this.olvColumn3.ToolTipText = "Tijd Gestart";
            this.olvColumn3.Width = 150;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Gestopt";
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.IsTileViewColumn = true;
            this.olvColumn4.Text = "Gestopt";
            this.olvColumn4.ToolTipText = "Tijd Gestopt";
            this.olvColumn4.Width = 150;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "key-skills_64_64.png");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xstatuslabel);
            this.panel2.Controls.Add(this.xclosepanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 469);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(939, 44);
            this.panel2.TabIndex = 2;
            // 
            // xstatuslabel
            // 
            this.xstatuslabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xstatuslabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatuslabel.Location = new System.Drawing.Point(0, 0);
            this.xstatuslabel.Name = "xstatuslabel";
            this.xstatuslabel.Padding = new System.Windows.Forms.Padding(5);
            this.xstatuslabel.Size = new System.Drawing.Size(807, 44);
            this.xstatuslabel.TabIndex = 0;
            this.xstatuslabel.Text = "Geen Vaardigheden";
            this.xstatuslabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xclosepanel
            // 
            this.xclosepanel.Controls.Add(this.xsluiten);
            this.xclosepanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.xclosepanel.Location = new System.Drawing.Point(807, 0);
            this.xclosepanel.Name = "xclosepanel";
            this.xclosepanel.Size = new System.Drawing.Size(132, 44);
            this.xclosepanel.TabIndex = 1;
            // 
            // xsluiten
            // 
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(3, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(125, 38);
            this.xsluiten.TabIndex = 1;
            this.xsluiten.Text = "&Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xsluiten, "Sluit vaardigheden window");
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xsearchbox
            // 
            this.xsearchbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.xsearchbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsearchbox.Location = new System.Drawing.Point(0, 0);
            this.xsearchbox.Name = "xsearchbox";
            this.xsearchbox.Size = new System.Drawing.Size(939, 29);
            this.xsearchbox.TabIndex = 0;
            this.xsearchbox.Text = "Zoeken...";
            this.toolTip1.SetToolTip(this.xsearchbox, "Zoek in de omschrijving, artikelnr, productie of werkplek.");
            this.xsearchbox.TextChanged += new System.EventHandler(this.xsearchbox_TextChanged);
            this.xsearchbox.Enter += new System.EventHandler(this.xsearch_Enter);
            this.xsearchbox.Leave += new System.EventHandler(this.xsearch_Leave);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Vaardigheden";
            // 
            // PersoonVaardigheden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.xomschrijving);
            this.DoubleBuffered = true;
            this.Name = "PersoonVaardigheden";
            this.Size = new System.Drawing.Size(939, 579);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xskillview)).EndInit();
            this.panel2.ResumeLayout(false);
            this.xclosepanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label xomschrijving;
        private System.Windows.Forms.Panel panel1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox xsearchbox;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label xstatuslabel;
        private System.Windows.Forms.Panel xclosepanel;
        private System.Windows.Forms.Button xsluiten;
        public BrightIdeasSoftware.ObjectListView xskillview;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
