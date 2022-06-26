using Controls;

namespace Forms
{
    partial class MateriaalUI
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
            this.xmateriaallijst = new Controls.CustomObjectListview();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verwijderenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.xverwijderb = new System.Windows.Forms.Button();
            this.xklaarzetpanel = new System.Windows.Forms.Panel();
            this.xklaarzetimage = new System.Windows.Forms.PictureBox();
            this.xklaarzetlabel = new System.Windows.Forms.Label();
            this.xniewmatb = new System.Windows.Forms.Button();
            this.xwijzigmatb = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.xstatus = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xmateriaallijst)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.xklaarzetpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xklaarzetimage)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.White;
            this.groupBox1.Controls.Add(this.xmateriaallijst);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(961, 490);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Materialen";
            // 
            // xmateriaallijst
            // 
            this.xmateriaallijst.AllColumns.Add(this.olvColumn1);
            this.xmateriaallijst.AllColumns.Add(this.olvColumn2);
            this.xmateriaallijst.AllColumns.Add(this.olvColumn3);
            this.xmateriaallijst.AllColumns.Add(this.olvColumn5);
            this.xmateriaallijst.AllColumns.Add(this.olvColumn8);
            this.xmateriaallijst.AllColumns.Add(this.olvColumn6);
            this.xmateriaallijst.AllColumns.Add(this.olvColumn7);
            this.xmateriaallijst.AllColumns.Add(this.olvColumn4);
            this.xmateriaallijst.AllowCellEdit = false;
            this.xmateriaallijst.CellEditUseWholeCell = false;
            this.xmateriaallijst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn5,
            this.olvColumn8,
            this.olvColumn6,
            this.olvColumn7,
            this.olvColumn4});
            this.xmateriaallijst.ContextMenuStrip = this.contextMenuStrip1;
            this.xmateriaallijst.Cursor = System.Windows.Forms.Cursors.Default;
            this.xmateriaallijst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmateriaallijst.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmateriaallijst.FullRowSelect = true;
            this.xmateriaallijst.GridLines = true;
            this.xmateriaallijst.HeaderWordWrap = true;
            this.xmateriaallijst.HideSelection = false;
            this.xmateriaallijst.LargeImageList = this.imageList1;
            this.xmateriaallijst.Location = new System.Drawing.Point(3, 72);
            this.xmateriaallijst.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.xmateriaallijst.MenuLabelColumns = "kolommen";
            this.xmateriaallijst.MenuLabelGroupBy = "Groeperen op \'{0}\'";
            this.xmateriaallijst.MenuLabelLockGroupingOn = "Groepering vergrendelen op \'{0}\'";
            this.xmateriaallijst.MenuLabelSelectColumns = "Selecteer kolommen...";
            this.xmateriaallijst.MenuLabelSortAscending = "Sorteer oplopend op \'{0}\'";
            this.xmateriaallijst.MenuLabelSortDescending = "Aflopend sorteren op \'{0}\'";
            this.xmateriaallijst.MenuLabelTurnOffGroups = "Groepen uitschakelen";
            this.xmateriaallijst.MenuLabelUnlockGroupingOn = "Ontgrendel groeperen van \'{0}\'";
            this.xmateriaallijst.MenuLabelUnsort = "Uitsorteren";
            this.xmateriaallijst.Name = "xmateriaallijst";
            this.xmateriaallijst.OwnerDraw = false;
            this.xmateriaallijst.ShowCommandMenuOnRightClick = true;
            this.xmateriaallijst.ShowGroups = false;
            this.xmateriaallijst.ShowItemCountOnGroups = true;
            this.xmateriaallijst.ShowItemToolTips = true;
            this.xmateriaallijst.Size = new System.Drawing.Size(955, 415);
            this.xmateriaallijst.SmallImageList = this.imageList1;
            this.xmateriaallijst.SpaceBetweenGroups = 10;
            this.xmateriaallijst.TabIndex = 1;
            this.xmateriaallijst.TileSize = new System.Drawing.Size(350, 200);
            this.xmateriaallijst.TintSortColumn = true;
            this.xmateriaallijst.UseCompatibleStateImageBehavior = false;
            this.xmateriaallijst.UseExplorerTheme = true;
            this.xmateriaallijst.UseFilterIndicator = true;
            this.xmateriaallijst.UseFiltering = true;
            this.xmateriaallijst.UseHotControls = false;
            this.xmateriaallijst.UseHotItem = true;
            this.xmateriaallijst.UseOverlays = false;
            this.xmateriaallijst.UseTranslucentHotItem = true;
            this.xmateriaallijst.UseTranslucentSelection = true;
            this.xmateriaallijst.View = System.Windows.Forms.View.Details;
            this.xmateriaallijst.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.xmateriaallijst_CellToolTipShowing);
            this.xmateriaallijst.SelectedIndexChanged += new System.EventHandler(this.xmateriaallijst_SelectedIndexChanged);
            this.xmateriaallijst.DoubleClick += new System.EventHandler(this.xwijzigmatb_Click);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Omschrijving";
            this.olvColumn1.Groupable = false;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.Text = "Omschrijving";
            this.olvColumn1.ToolTipText = "Product Omschrijving";
            this.olvColumn1.Width = 258;
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "ArtikelNr";
            this.olvColumn2.AspectToStringFormat = "";
            this.olvColumn2.Groupable = false;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.IsTileViewColumn = true;
            this.olvColumn2.Text = "ArtikelNr";
            this.olvColumn2.ToolTipText = "Product ArtikelNr";
            this.olvColumn2.Width = 100;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Locatie";
            this.olvColumn3.AspectToStringFormat = "";
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.IsTileViewColumn = true;
            this.olvColumn3.Text = "Locatie";
            this.olvColumn3.ToolTipText = "Product Locatie";
            this.olvColumn3.Width = 75;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "AantalPerStuk";
            this.olvColumn5.AspectToStringFormat = "";
            this.olvColumn5.Groupable = false;
            this.olvColumn5.IsEditable = false;
            this.olvColumn5.IsTileViewColumn = true;
            this.olvColumn5.Text = "Per Stuk";
            this.olvColumn5.ToolTipText = "Aantal per stuk";
            this.olvColumn5.Width = 75;
            this.olvColumn5.WordWrap = true;
            // 
            // olvColumn8
            // 
            this.olvColumn8.AspectName = "AantalNodig";
            this.olvColumn8.Text = "Aantal Nodig";
            this.olvColumn8.ToolTipText = "Aantal nodig";
            this.olvColumn8.Width = 140;
            this.olvColumn8.WordWrap = true;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "Aantal";
            this.olvColumn6.AspectToStringFormat = "";
            this.olvColumn6.Groupable = false;
            this.olvColumn6.IsEditable = false;
            this.olvColumn6.IsTileViewColumn = true;
            this.olvColumn6.Text = "Aantal Gebruikt";
            this.olvColumn6.ToolTipText = "Product Aantal";
            this.olvColumn6.Width = 140;
            this.olvColumn6.WordWrap = true;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "AantalAfkeur";
            this.olvColumn7.AspectToStringFormat = "";
            this.olvColumn7.Groupable = false;
            this.olvColumn7.IsEditable = false;
            this.olvColumn7.IsTileViewColumn = true;
            this.olvColumn7.Text = "Afkeur";
            this.olvColumn7.ToolTipText = "Product Aantal Afkeur";
            this.olvColumn7.Width = 75;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "Eenheid";
            this.olvColumn4.AspectToStringFormat = "";
            this.olvColumn4.Groupable = false;
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.IsTileViewColumn = true;
            this.olvColumn4.Text = "Eenheid";
            this.olvColumn4.ToolTipText = "Product Eenheid";
            this.olvColumn4.Width = 75;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verwijderenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 26);
            // 
            // verwijderenToolStripMenuItem
            // 
            this.verwijderenToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.verwijderenToolStripMenuItem.Name = "verwijderenToolStripMenuItem";
            this.verwijderenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.verwijderenToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.verwijderenToolStripMenuItem.Text = "Verwijderen";
            this.verwijderenToolStripMenuItem.Click += new System.EventHandler(this.xverwijderb_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xverwijderb);
            this.panel1.Controls.Add(this.xklaarzetpanel);
            this.panel1.Controls.Add(this.xniewmatb);
            this.panel1.Controls.Add(this.xwijzigmatb);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(955, 47);
            this.panel1.TabIndex = 15;
            // 
            // xverwijderb
            // 
            this.xverwijderb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xverwijderb.Enabled = false;
            this.xverwijderb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xverwijderb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xverwijderb.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xverwijderb.Location = new System.Drawing.Point(912, 3);
            this.xverwijderb.Name = "xverwijderb";
            this.xverwijderb.Size = new System.Drawing.Size(40, 40);
            this.xverwijderb.TabIndex = 0;
            this.xverwijderb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xverwijderb, "Verwijder geselcteerde materialen");
            this.xverwijderb.UseVisualStyleBackColor = true;
            this.xverwijderb.Click += new System.EventHandler(this.xverwijderb_Click);
            // 
            // xklaarzetpanel
            // 
            this.xklaarzetpanel.Controls.Add(this.xklaarzetimage);
            this.xklaarzetpanel.Controls.Add(this.xklaarzetlabel);
            this.xklaarzetpanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.xklaarzetpanel.Location = new System.Drawing.Point(0, 0);
            this.xklaarzetpanel.Name = "xklaarzetpanel";
            this.xklaarzetpanel.Size = new System.Drawing.Size(324, 47);
            this.xklaarzetpanel.TabIndex = 14;
            this.xklaarzetpanel.Click += new System.EventHandler(this.xklaarzetimage_Click);
            this.xklaarzetpanel.MouseEnter += new System.EventHandler(this.xklaarzetimage_MouseEnter);
            this.xklaarzetpanel.MouseLeave += new System.EventHandler(this.xklaarzetimage_MouseLeave);
            // 
            // xklaarzetimage
            // 
            this.xklaarzetimage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xklaarzetimage.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.xklaarzetimage.Location = new System.Drawing.Point(285, 5);
            this.xklaarzetimage.Name = "xklaarzetimage";
            this.xklaarzetimage.Size = new System.Drawing.Size(36, 36);
            this.xklaarzetimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.xklaarzetimage.TabIndex = 0;
            this.xklaarzetimage.TabStop = false;
            this.xklaarzetimage.Click += new System.EventHandler(this.xklaarzetimage_Click);
            this.xklaarzetimage.MouseEnter += new System.EventHandler(this.xklaarzetimage_MouseEnter);
            this.xklaarzetimage.MouseLeave += new System.EventHandler(this.xklaarzetimage_MouseLeave);
            // 
            // xklaarzetlabel
            // 
            this.xklaarzetlabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xklaarzetlabel.Location = new System.Drawing.Point(0, 0);
            this.xklaarzetlabel.Name = "xklaarzetlabel";
            this.xklaarzetlabel.Size = new System.Drawing.Size(279, 47);
            this.xklaarzetlabel.TabIndex = 1;
            this.xklaarzetlabel.Text = "Klaar Gezet";
            this.xklaarzetlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xklaarzetlabel.Click += new System.EventHandler(this.xklaarzetimage_Click);
            this.xklaarzetlabel.MouseEnter += new System.EventHandler(this.xklaarzetimage_MouseEnter);
            this.xklaarzetlabel.MouseLeave += new System.EventHandler(this.xklaarzetimage_MouseLeave);
            // 
            // xniewmatb
            // 
            this.xniewmatb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xniewmatb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xniewmatb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xniewmatb.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xniewmatb.Location = new System.Drawing.Point(820, 3);
            this.xniewmatb.Name = "xniewmatb";
            this.xniewmatb.Size = new System.Drawing.Size(40, 40);
            this.xniewmatb.TabIndex = 13;
            this.xniewmatb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xniewmatb, "Voeg toe als nieuw materiaal");
            this.xniewmatb.UseVisualStyleBackColor = true;
            this.xniewmatb.Click += new System.EventHandler(this.xniewmatb_Click);
            // 
            // xwijzigmatb
            // 
            this.xwijzigmatb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xwijzigmatb.Enabled = false;
            this.xwijzigmatb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xwijzigmatb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xwijzigmatb.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xwijzigmatb.Location = new System.Drawing.Point(866, 3);
            this.xwijzigmatb.Name = "xwijzigmatb";
            this.xwijzigmatb.Size = new System.Drawing.Size(40, 40);
            this.xwijzigmatb.TabIndex = 12;
            this.xwijzigmatb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xwijzigmatb, "Wijzig geselecteerde materiaal");
            this.xwijzigmatb.UseVisualStyleBackColor = true;
            this.xwijzigmatb.Click += new System.EventHandler(this.xwijzigmatb_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.Transparent;
            this.panel3.Controls.Add(this.xstatus);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(5, 495);
            this.panel3.Margin = new System.Windows.Forms.Padding(2);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(961, 20);
            this.panel3.TabIndex = 8;
            // 
            // xstatus
            // 
            this.xstatus.BackColor = System.Drawing.Color.White;
            this.xstatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xstatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatus.ForeColor = System.Drawing.Color.Black;
            this.xstatus.Location = new System.Drawing.Point(0, 0);
            this.xstatus.Name = "xstatus";
            this.xstatus.Size = new System.Drawing.Size(961, 20);
            this.xstatus.TabIndex = 0;
            this.xstatus.Text = "Status";
            this.xstatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // MateriaalUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel3);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.Name = "MateriaalUI";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(971, 520);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xmateriaallijst)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.xklaarzetpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xklaarzetimage)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private CustomObjectListview xmateriaallijst;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button xverwijderb;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button xniewmatb;
        private System.Windows.Forms.Button xwijzigmatb;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel xklaarzetpanel;
        private System.Windows.Forms.PictureBox xklaarzetimage;
        private System.Windows.Forms.Label xklaarzetlabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label xstatus;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem verwijderenToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn olvColumn8;
    }
}
