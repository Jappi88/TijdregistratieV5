using Controls;

namespace Forms.FileBrowser
{
    partial class FileBrowserUI
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xvorige = new System.Windows.Forms.ToolStripButton();
            this.xvolgende = new System.Windows.Forms.ToolStripButton();
            this.xrefreshdirectory = new System.Windows.Forms.ToolStripButton();
            this.xhomebutton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xViewStyle = new System.Windows.Forms.ToolStripSplitButton();
            this.xclearsearchbox = new System.Windows.Forms.ToolStripButton();
            this.xsearchbox = new System.Windows.Forms.ToolStripTextBox();
            this.xstatus = new System.Windows.Forms.ToolStripLabel();
            this.xbrowser = new CustomObjectListview();
            this.xnaamcol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xgewijzigdcol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xtypecol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xsizecol = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.xlargeimagelist = new System.Windows.Forms.ImageList(this.components);
            this.xsmallimageList = new System.Windows.Forms.ImageList(this.components);
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.xtotalitems = new System.Windows.Forms.ToolStripLabel();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.xselected = new System.Windows.Forms.ToolStripLabel();
            this.xloadinglabel = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xbrowser)).BeginInit();
            this.toolStrip2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.AutoSize = false;
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xvorige,
            this.xvolgende,
            this.xrefreshdirectory,
            this.xhomebutton,
            this.toolStripSeparator1,
            this.xViewStyle,
            this.xclearsearchbox,
            this.xsearchbox,
            this.xstatus});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(892, 38);
            this.toolStrip1.TabIndex = 5;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // xvorige
            // 
            this.xvorige.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xvorige.Enabled = false;
            this.xvorige.Image = global::ProductieManager.Properties.Resources.arrow_left_15601;
            this.xvorige.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xvorige.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xvorige.Name = "xvorige";
            this.xvorige.Size = new System.Drawing.Size(36, 35);
            this.xvorige.ToolTipText = "Vorige";
            this.xvorige.Click += new System.EventHandler(this.xvorige_Click);
            // 
            // xvolgende
            // 
            this.xvolgende.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xvolgende.Enabled = false;
            this.xvolgende.Image = global::ProductieManager.Properties.Resources.arrow_right_15600;
            this.xvolgende.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xvolgende.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xvolgende.Name = "xvolgende";
            this.xvolgende.Size = new System.Drawing.Size(36, 35);
            this.xvolgende.ToolTipText = "Volgende";
            this.xvolgende.Click += new System.EventHandler(this.xvolgende_Click);
            // 
            // xrefreshdirectory
            // 
            this.xrefreshdirectory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xrefreshdirectory.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.xrefreshdirectory.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xrefreshdirectory.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xrefreshdirectory.Name = "xrefreshdirectory";
            this.xrefreshdirectory.Size = new System.Drawing.Size(36, 35);
            this.xrefreshdirectory.ToolTipText = "Refresh pagina";
            this.xrefreshdirectory.Click += new System.EventHandler(this.Refresh_Click);
            // 
            // xhomebutton
            // 
            this.xhomebutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xhomebutton.Enabled = false;
            this.xhomebutton.Image = global::ProductieManager.Properties.Resources.home_icon_color_32x32;
            this.xhomebutton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xhomebutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xhomebutton.Name = "xhomebutton";
            this.xhomebutton.Size = new System.Drawing.Size(36, 35);
            this.xhomebutton.ToolTipText = "Startpagina";
            this.xhomebutton.Click += new System.EventHandler(this.xhomebutton_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 38);
            // 
            // xViewStyle
            // 
            this.xViewStyle.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xViewStyle.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xViewStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xViewStyle.Image = global::ProductieManager.Properties.Resources.viewmorecolumns_6276;
            this.xViewStyle.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xViewStyle.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xViewStyle.Name = "xViewStyle";
            this.xViewStyle.Size = new System.Drawing.Size(48, 35);
            this.xViewStyle.Text = "toolStripButton2";
            this.xViewStyle.ToolTipText = "Indeling en weergaveopties";
            this.xViewStyle.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.xViewStyle_DropDownItemClicked);
            this.xViewStyle.Click += new System.EventHandler(this.xViewStyle_Click);
            // 
            // xclearsearchbox
            // 
            this.xclearsearchbox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xclearsearchbox.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xclearsearchbox.Image = global::ProductieManager.Properties.Resources.cancel_close_cross_delete_32x32;
            this.xclearsearchbox.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xclearsearchbox.Name = "xclearsearchbox";
            this.xclearsearchbox.Size = new System.Drawing.Size(23, 35);
            this.xclearsearchbox.ToolTipText = "Zoekbalk leegmaken...";
            this.xclearsearchbox.Click += new System.EventHandler(this.xclearsearchbox_Click);
            // 
            // xsearchbox
            // 
            this.xsearchbox.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xsearchbox.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xsearchbox.Name = "xsearchbox";
            this.xsearchbox.Size = new System.Drawing.Size(200, 38);
            this.xsearchbox.Text = "Zoeken...";
            this.xsearchbox.Enter += new System.EventHandler(this.xsearchbox_Enter);
            this.xsearchbox.Leave += new System.EventHandler(this.xsearchbox_Leave);
            this.xsearchbox.TextChanged += new System.EventHandler(this.xsearchbox_TextChanged);
            // 
            // xstatus
            // 
            this.xstatus.Name = "xstatus";
            this.xstatus.Size = new System.Drawing.Size(90, 35);
            this.xstatus.Text = "Header Text";
            // 
            // xbrowser
            // 
            this.xbrowser.AllColumns.Add(this.xnaamcol);
            this.xbrowser.AllColumns.Add(this.xgewijzigdcol);
            this.xbrowser.AllColumns.Add(this.xtypecol);
            this.xbrowser.AllColumns.Add(this.xsizecol);
            this.xbrowser.AllowDrop = true;
            this.xbrowser.BackColor = System.Drawing.Color.White;
            this.xbrowser.CellEditUseWholeCell = false;
            this.xbrowser.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.xnaamcol,
            this.xgewijzigdcol,
            this.xtypecol,
            this.xsizecol});
            this.xbrowser.ContextMenuStrip = this.xContextMenu;
            this.xbrowser.Cursor = System.Windows.Forms.Cursors.Default;
            this.xbrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xbrowser.EmptyListMsg = "Geen bijlages...";
            this.xbrowser.EmptyListMsgFont = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbrowser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbrowser.FullRowSelect = true;
            this.xbrowser.GridLines = true;
            this.xbrowser.HideSelection = false;
            this.xbrowser.IsSimpleDragSource = true;
            this.xbrowser.IsSimpleDropSink = true;
            this.xbrowser.LargeImageList = this.xlargeimagelist;
            this.xbrowser.Location = new System.Drawing.Point(0, 38);
            this.xbrowser.MenuLabelColumns = "kolommen";
            this.xbrowser.MenuLabelGroupBy = "Groeperen op \'{0}\'";
            this.xbrowser.MenuLabelLockGroupingOn = "Vergrending groupering op \'{0}\'";
            this.xbrowser.MenuLabelSelectColumns = "Selecteer kolommen...";
            this.xbrowser.MenuLabelSortAscending = "Sorteer oplopend op \'{0}\'";
            this.xbrowser.MenuLabelSortDescending = "Aflopend sorteren op \'{0}\'";
            this.xbrowser.MenuLabelTurnOffGroups = "Groepen uitschakelen";
            this.xbrowser.MenuLabelUnlockGroupingOn = "Ontgrendel groeperen van \'{0}\'";
            this.xbrowser.MenuLabelUnsort = "Uitsorteren";
            this.xbrowser.Name = "xbrowser";
            this.xbrowser.ShowCommandMenuOnRightClick = true;
            this.xbrowser.ShowItemCountOnGroups = true;
            this.xbrowser.ShowItemToolTips = true;
            this.xbrowser.Size = new System.Drawing.Size(892, 522);
            this.xbrowser.SmallImageList = this.xsmallimageList;
            this.xbrowser.TabIndex = 6;
            this.xbrowser.TileSize = new System.Drawing.Size(300, 96);
            this.xbrowser.UseCellFormatEvents = true;
            this.xbrowser.UseCompatibleStateImageBehavior = false;
            this.xbrowser.UseExplorerTheme = true;
            this.xbrowser.UseFilterIndicator = true;
            this.xbrowser.UseFiltering = true;
            this.xbrowser.UseHotControls = false;
            this.xbrowser.UseHotItem = true;
            this.xbrowser.UseTranslucentHotItem = true;
            this.xbrowser.UseTranslucentSelection = true;
            this.xbrowser.View = System.Windows.Forms.View.Tile;
            this.xbrowser.CanDrop += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.xbrowser_CanDrop);
            this.xbrowser.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.xbrowser_CellEditFinishing);
            this.xbrowser.CellEditStarting += new BrightIdeasSoftware.CellEditEventHandler(this.xbrowser_CellEditStarting);
            this.xbrowser.Dropped += new System.EventHandler<BrightIdeasSoftware.OlvDropEventArgs>(this.xbrowser_Dropped);
            this.xbrowser.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.xbrowser_FormatCell);
            this.xbrowser.FormatRow += new System.EventHandler<BrightIdeasSoftware.FormatRowEventArgs>(this.xbrowser_FormatRow);
            this.xbrowser.ModelCanDrop += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.xbrowser_ModelCanDrop);
            this.xbrowser.ModelDropped += new System.EventHandler<BrightIdeasSoftware.ModelDropEventArgs>(this.xbrowser_ModelDropped);
            this.xbrowser.SelectedIndexChanged += new System.EventHandler(this.xbrowser_SelectedIndexChanged);
            this.xbrowser.DoubleClick += new System.EventHandler(this.xbrowser_DoubleClick);
            // 
            // xnaamcol
            // 
            this.xnaamcol.AspectName = "Name";
            this.xnaamcol.Groupable = false;
            this.xnaamcol.HeaderForeColor = System.Drawing.Color.Black;
            this.xnaamcol.IsTileViewColumn = true;
            this.xnaamcol.Text = "Naam";
            this.xnaamcol.Width = 294;
            // 
            // xgewijzigdcol
            // 
            this.xgewijzigdcol.AspectName = "LastChanged";
            this.xgewijzigdcol.HeaderForeColor = System.Drawing.Color.Black;
            this.xgewijzigdcol.IsEditable = false;
            this.xgewijzigdcol.IsTileViewColumn = true;
            this.xgewijzigdcol.Text = "Gewijzigd Op";
            this.xgewijzigdcol.Width = 142;
            // 
            // xtypecol
            // 
            this.xtypecol.AspectName = "Type";
            this.xtypecol.HeaderForeColor = System.Drawing.Color.Black;
            this.xtypecol.IsEditable = false;
            this.xtypecol.IsTileViewColumn = true;
            this.xtypecol.Text = "Type";
            this.xtypecol.Width = 142;
            // 
            // xsizecol
            // 
            this.xsizecol.AspectName = "FriendlySize";
            this.xsizecol.HeaderForeColor = System.Drawing.Color.Black;
            this.xsizecol.IsEditable = false;
            this.xsizecol.IsTileViewColumn = true;
            this.xsizecol.Text = "Grootte";
            this.xsizecol.Width = 112;
            // 
            // xContextMenu
            // 
            this.xContextMenu.Name = "xContextMenu";
            this.xContextMenu.Size = new System.Drawing.Size(61, 4);
            this.xContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.xContextMenu_Opening);
            // 
            // xlargeimagelist
            // 
            this.xlargeimagelist.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.xlargeimagelist.ImageSize = new System.Drawing.Size(96, 96);
            this.xlargeimagelist.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xsmallimageList
            // 
            this.xsmallimageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.xsmallimageList.ImageSize = new System.Drawing.Size(48, 48);
            this.xsmallimageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // toolStrip2
            // 
            this.toolStrip2.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip2.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xtotalitems,
            this.toolStripSeparator2,
            this.xselected});
            this.toolStrip2.Location = new System.Drawing.Point(0, 560);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(892, 25);
            this.toolStrip2.TabIndex = 7;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // xtotalitems
            // 
            this.xtotalitems.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.xtotalitems.Name = "xtotalitems";
            this.xtotalitems.Size = new System.Drawing.Size(45, 22);
            this.xtotalitems.Text = "0 Items";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // xselected
            // 
            this.xselected.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.xselected.Name = "xselected";
            this.xselected.Size = new System.Drawing.Size(133, 22);
            this.xselected.Text = "0 Items geselecteerd 0 B";
            // 
            // xloadinglabel
            // 
            this.xloadinglabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xloadinglabel.BackColor = System.Drawing.Color.Transparent;
            this.xloadinglabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xloadinglabel.Location = new System.Drawing.Point(0, 38);
            this.xloadinglabel.Name = "xloadinglabel";
            this.xloadinglabel.Size = new System.Drawing.Size(889, 526);
            this.xloadinglabel.TabIndex = 30;
            this.xloadinglabel.Text = "Bijlages Laden...";
            this.xloadinglabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xloadinglabel.Visible = false;
            // 
            // FileBrowserUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xbrowser);
            this.Controls.Add(this.toolStrip2);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.xloadinglabel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FileBrowserUI";
            this.Size = new System.Drawing.Size(892, 585);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xbrowser)).EndInit();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton xvorige;
        private System.Windows.Forms.ToolStripButton xvolgende;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton xhomebutton;
        private System.Windows.Forms.ToolStripSplitButton xViewStyle;
        private CustomObjectListview xbrowser;
        private BrightIdeasSoftware.OLVColumn xnaamcol;
        private BrightIdeasSoftware.OLVColumn xgewijzigdcol;
        private BrightIdeasSoftware.OLVColumn xtypecol;
        private BrightIdeasSoftware.OLVColumn xsizecol;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripLabel xtotalitems;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel xselected;
        private System.Windows.Forms.ImageList xlargeimagelist;
        private System.Windows.Forms.ToolStripButton xclearsearchbox;
        private System.Windows.Forms.ToolStripTextBox xsearchbox;
        private System.Windows.Forms.ToolStripLabel xstatus;
        private System.Windows.Forms.ContextMenuStrip xContextMenu;
        private System.Windows.Forms.ToolStripButton xrefreshdirectory;
        private System.Windows.Forms.Label xloadinglabel;
        private System.Windows.Forms.ImageList xsmallimageList;
    }
}
