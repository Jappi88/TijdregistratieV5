
namespace ProductieManager.Forms
{
    partial class AlleNotitiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlleNotitiesForm));
            this.xsearchbox = new System.Windows.Forms.TextBox();
            this.xnotitielist = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.wijzigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openProductieToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.verwijderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xnewnotitie = new System.Windows.Forms.ToolStripButton();
            ((System.ComponentModel.ISupportInitialize)(this.xnotitielist)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xsearchbox
            // 
            this.xsearchbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.xsearchbox.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsearchbox.Location = new System.Drawing.Point(20, 99);
            this.xsearchbox.Name = "xsearchbox";
            this.xsearchbox.Size = new System.Drawing.Size(870, 27);
            this.xsearchbox.TabIndex = 7;
            this.xsearchbox.Text = "Zoeken...";
            this.xsearchbox.TextChanged += new System.EventHandler(this.xsearchbox_TextChanged);
            this.xsearchbox.Enter += new System.EventHandler(this.xsearch_Enter);
            this.xsearchbox.Leave += new System.EventHandler(this.xsearch_Leave);
            // 
            // xnotitielist
            // 
            this.xnotitielist.AllColumns.Add(this.olvColumn1);
            this.xnotitielist.AllColumns.Add(this.olvColumn2);
            this.xnotitielist.AllColumns.Add(this.olvColumn3);
            this.xnotitielist.AllColumns.Add(this.olvColumn4);
            this.xnotitielist.AllColumns.Add(this.olvColumn5);
            this.xnotitielist.AllowColumnReorder = true;
            this.xnotitielist.CellEditUseWholeCell = false;
            this.xnotitielist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn5});
            this.xnotitielist.ContextMenuStrip = this.contextMenuStrip1;
            this.xnotitielist.Cursor = System.Windows.Forms.Cursors.Default;
            this.xnotitielist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xnotitielist.FullRowSelect = true;
            this.xnotitielist.HideSelection = false;
            this.xnotitielist.LargeImageList = this.imageList1;
            this.xnotitielist.Location = new System.Drawing.Point(20, 126);
            this.xnotitielist.Name = "xnotitielist";
            this.xnotitielist.ShowItemToolTips = true;
            this.xnotitielist.Size = new System.Drawing.Size(870, 327);
            this.xnotitielist.SmallImageList = this.imageList1;
            this.xnotitielist.TabIndex = 8;
            this.xnotitielist.UseCompatibleStateImageBehavior = false;
            this.xnotitielist.UseExplorerTheme = true;
            this.xnotitielist.UseFilterIndicator = true;
            this.xnotitielist.UseFiltering = true;
            this.xnotitielist.UseHotItem = true;
            this.xnotitielist.UseTranslucentHotItem = true;
            this.xnotitielist.UseTranslucentSelection = true;
            this.xnotitielist.View = System.Windows.Forms.View.Details;
            this.xnotitielist.DoubleClick += new System.EventHandler(this.xnotitielist_DoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Notitie";
            this.olvColumn1.Text = "Notitie";
            this.olvColumn1.ToolTipText = "Notitie";
            this.olvColumn1.Width = 280;
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Naam";
            this.olvColumn2.Text = "Naam";
            this.olvColumn2.ToolTipText = "Naam van de gene die deze notitie heeft gemaakt";
            this.olvColumn2.Width = 120;
            this.olvColumn2.WordWrap = true;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Type";
            this.olvColumn3.Text = "Type";
            this.olvColumn3.ToolTipText = "Notitie Type";
            this.olvColumn3.Width = 120;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "DatumToegevoegd";
            this.olvColumn4.Text = "Datum Toegevoegd";
            this.olvColumn4.ToolTipText = "De datum van de notitie";
            this.olvColumn4.Width = 150;
            this.olvColumn4.WordWrap = true;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "Path";
            this.olvColumn5.Text = "Klus";
            this.olvColumn5.ToolTipText = "Klus waarvan deze notitie hoort";
            this.olvColumn5.Width = 226;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wijzigToolStripMenuItem,
            this.openProductieToolStripMenuItem,
            this.toolStripSeparator1,
            this.verwijderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(174, 124);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // wijzigToolStripMenuItem
            // 
            this.wijzigToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.wijzigToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.wijzigToolStripMenuItem.Name = "wijzigToolStripMenuItem";
            this.wijzigToolStripMenuItem.Size = new System.Drawing.Size(173, 38);
            this.wijzigToolStripMenuItem.Text = "&Wijzig";
            this.wijzigToolStripMenuItem.Click += new System.EventHandler(this.wijzigToolStripMenuItem_Click);
            // 
            // openProductieToolStripMenuItem
            // 
            this.openProductieToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.New_Window_36860;
            this.openProductieToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openProductieToolStripMenuItem.Name = "openProductieToolStripMenuItem";
            this.openProductieToolStripMenuItem.Size = new System.Drawing.Size(173, 38);
            this.openProductieToolStripMenuItem.Text = "&Open Productie";
            this.openProductieToolStripMenuItem.Click += new System.EventHandler(this.openProductieToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(170, 6);
            // 
            // verwijderToolStripMenuItem
            // 
            this.verwijderToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.verwijderToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.verwijderToolStripMenuItem.Name = "verwijderToolStripMenuItem";
            this.verwijderToolStripMenuItem.Size = new System.Drawing.Size(173, 38);
            this.verwijderToolStripMenuItem.Text = "&Verwijder";
            this.verwijderToolStripMenuItem.Click += new System.EventHandler(this.verwijderToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "note_notes_10052_64x64.png");
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xnewnotitie});
            this.toolStrip1.Location = new System.Drawing.Point(20, 60);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(870, 39);
            this.toolStrip1.TabIndex = 9;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // xnewnotitie
            // 
            this.xnewnotitie.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xnewnotitie.Image = global::ProductieManager.Properties.Resources.texteditor_note_notes_pencil_detext_9967_32x32;
            this.xnewnotitie.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xnewnotitie.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xnewnotitie.Name = "xnewnotitie";
            this.xnewnotitie.Size = new System.Drawing.Size(36, 36);
            this.xnewnotitie.Text = "Nieuwe Notitie";
            this.xnewnotitie.ToolTipText = "Maak een notitie";
            this.xnewnotitie.Click += new System.EventHandler(this.xnewnotitie_Click);
            // 
            // AlleNotitiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 473);
            this.Controls.Add(this.xnotitielist);
            this.Controls.Add(this.xsearchbox);
            this.Controls.Add(this.toolStrip1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AlleNotitiesForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Alle Notities";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlleNotitiesForm_FormClosing);
            this.Shown += new System.EventHandler(this.AlleNotitiesForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.xnotitielist)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox xsearchbox;
        private BrightIdeasSoftware.ObjectListView xnotitielist;
        private System.Windows.Forms.ImageList imageList1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wijzigToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openProductieToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem verwijderToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton xnewnotitie;
    }
}