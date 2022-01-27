namespace Forms.Aantal.Controls
{
    partial class AantalGemaaktHistoryUI
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
            this.xHistoryList = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verwijderenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toevoegenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.wijzigenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xstatuslabel = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.xHistoryList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xHistoryList
            // 
            this.xHistoryList.AllColumns.Add(this.olvColumn2);
            this.xHistoryList.AllColumns.Add(this.olvColumn1);
            this.xHistoryList.AllColumns.Add(this.olvColumn3);
            this.xHistoryList.AllColumns.Add(this.olvColumn4);
            this.xHistoryList.AllColumns.Add(this.olvColumn7);
            this.xHistoryList.AllColumns.Add(this.olvColumn5);
            this.xHistoryList.AllColumns.Add(this.olvColumn6);
            this.xHistoryList.AlternateRowBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.xHistoryList.CellEditUseWholeCell = false;
            this.xHistoryList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2,
            this.olvColumn1,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn7,
            this.olvColumn5,
            this.olvColumn6});
            this.xHistoryList.ContextMenuStrip = this.contextMenuStrip1;
            this.xHistoryList.Cursor = System.Windows.Forms.Cursors.Default;
            this.xHistoryList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xHistoryList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xHistoryList.FullRowSelect = true;
            this.xHistoryList.GridLines = true;
            this.xHistoryList.HideSelection = false;
            this.xHistoryList.Location = new System.Drawing.Point(0, 0);
            this.xHistoryList.Name = "xHistoryList";
            this.xHistoryList.ShowGroups = false;
            this.xHistoryList.ShowItemToolTips = true;
            this.xHistoryList.Size = new System.Drawing.Size(815, 491);
            this.xHistoryList.TabIndex = 0;
            this.xHistoryList.TintSortColumn = true;
            this.xHistoryList.UseAlternatingBackColors = true;
            this.xHistoryList.UseCompatibleStateImageBehavior = false;
            this.xHistoryList.UseExplorerTheme = true;
            this.xHistoryList.UseFilterIndicator = true;
            this.xHistoryList.UseFiltering = true;
            this.xHistoryList.View = System.Windows.Forms.View.Details;
            this.xHistoryList.SelectedIndexChanged += new System.EventHandler(this.xHistoryList_SelectedIndexChanged);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Aantal";
            this.olvColumn2.Text = "Vorige Aantal";
            this.olvColumn2.ToolTipText = "Het vorige aantal dat door gegeven is";
            this.olvColumn2.Width = 100;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "LastAantal";
            this.olvColumn1.Text = "Aantal";
            this.olvColumn1.ToolTipText = "Het aantal dat is door gegeven";
            this.olvColumn1.Width = 100;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Gemaakt";
            this.olvColumn3.Text = "Gemaakt";
            this.olvColumn3.ToolTipText = "Het aantal dat gemaakt is op die periode";
            this.olvColumn3.Width = 100;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "";
            this.olvColumn4.AspectToStringFormat = "{0} uur";
            this.olvColumn4.Text = "Tijd Gewerkt";
            this.olvColumn4.Width = 100;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "";
            this.olvColumn7.AspectToStringFormat = "{0} p/u";
            this.olvColumn7.IsEditable = false;
            this.olvColumn7.Text = "Per Uur";
            this.olvColumn7.ToolTipText = "Aantal Per uur";
            this.olvColumn7.Width = 100;
            this.olvColumn7.WordWrap = true;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "DateChanged";
            this.olvColumn5.Text = "Gemaakt Vanaf";
            this.olvColumn5.ToolTipText = "datum en tijd van het doorgeven van het aantal";
            this.olvColumn5.Width = 150;
            this.olvColumn5.WordWrap = true;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "EndDate";
            this.olvColumn6.Text = "Gemaakt Tot";
            this.olvColumn6.Width = 150;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verwijderenToolStripMenuItem,
            this.toevoegenToolStripMenuItem,
            this.wijzigenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 70);
            // 
            // verwijderenToolStripMenuItem
            // 
            this.verwijderenToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.verwijderenToolStripMenuItem.Name = "verwijderenToolStripMenuItem";
            this.verwijderenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.verwijderenToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.verwijderenToolStripMenuItem.Text = "Verwijderen";
            this.verwijderenToolStripMenuItem.Click += new System.EventHandler(this.verwijderenToolStripMenuItem_Click);
            // 
            // toevoegenToolStripMenuItem
            // 
            this.toevoegenToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.add_icon_icons_com_52393;
            this.toevoegenToolStripMenuItem.Name = "toevoegenToolStripMenuItem";
            this.toevoegenToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.toevoegenToolStripMenuItem.Text = "Toevoegen";
            this.toevoegenToolStripMenuItem.Click += new System.EventHandler(this.toevoegenToolStripMenuItem_Click);
            // 
            // wijzigenToolStripMenuItem
            // 
            this.wijzigenToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.wijzigenToolStripMenuItem.Name = "wijzigenToolStripMenuItem";
            this.wijzigenToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.wijzigenToolStripMenuItem.Text = "Wijzigen";
            this.wijzigenToolStripMenuItem.Click += new System.EventHandler(this.wijzigenToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xstatuslabel
            // 
            this.xstatuslabel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xstatuslabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatuslabel.Location = new System.Drawing.Point(0, 491);
            this.xstatuslabel.Name = "xstatuslabel";
            this.xstatuslabel.Size = new System.Drawing.Size(815, 17);
            this.xstatuslabel.TabIndex = 1;
            this.xstatuslabel.Text = "Status";
            // 
            // AantalGemaaktHistoryUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xHistoryList);
            this.Controls.Add(this.xstatuslabel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AantalGemaaktHistoryUI";
            this.Size = new System.Drawing.Size(815, 508);
            ((System.ComponentModel.ISupportInitialize)(this.xHistoryList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView xHistoryList;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem verwijderenToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private System.Windows.Forms.ToolStripMenuItem toevoegenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem wijzigenToolStripMenuItem;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private System.Windows.Forms.Label xstatuslabel;
    }
}
