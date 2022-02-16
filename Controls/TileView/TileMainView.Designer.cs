namespace Controls.TileView
{
    partial class TileMainView
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
            this.xBottomToolMenu = new System.Windows.Forms.ToolStrip();
            this.xBeheerweergavetoolstrip = new System.Windows.Forms.ToolStripSplitButton();
            this.beheerTileLayoutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.vanLinksNaarRechtsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vanBovenNaarBenedenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vanRechtsNaarLinksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vanOnderNaarBovenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xBeheerLijstenToolstripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.reserLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.beheerTileLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.resetLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tileViewer1 = new Controls.TileViewer();
            this.xBottomToolMenu.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xBottomToolMenu
            // 
            this.xBottomToolMenu.BackColor = System.Drawing.SystemColors.Window;
            this.xBottomToolMenu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xBottomToolMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.xBottomToolMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xBeheerweergavetoolstrip});
            this.xBottomToolMenu.Location = new System.Drawing.Point(0, 558);
            this.xBottomToolMenu.Name = "xBottomToolMenu";
            this.xBottomToolMenu.Size = new System.Drawing.Size(816, 28);
            this.xBottomToolMenu.TabIndex = 34;
            this.xBottomToolMenu.Text = "toolStrip1";
            // 
            // xBeheerweergavetoolstrip
            // 
            this.xBeheerweergavetoolstrip.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xBeheerweergavetoolstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.beheerTileLayoutToolStripMenuItem1,
            this.toolStripMenuItem1,
            this.xBeheerLijstenToolstripItem,
            this.toolStripSeparator1,
            this.reserLayoutToolStripMenuItem});
            this.xBeheerweergavetoolstrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xBeheerweergavetoolstrip.Image = global::ProductieManager.Properties.Resources.layout_widget_icon_32x32;
            this.xBeheerweergavetoolstrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xBeheerweergavetoolstrip.Name = "xBeheerweergavetoolstrip";
            this.xBeheerweergavetoolstrip.Size = new System.Drawing.Size(73, 25);
            this.xBeheerweergavetoolstrip.Text = "Tiles";
            this.xBeheerweergavetoolstrip.Click += new System.EventHandler(this.xBeheerweergavetoolstrip_ButtonClick);
            // 
            // beheerTileLayoutToolStripMenuItem1
            // 
            this.beheerTileLayoutToolStripMenuItem1.Image = global::ProductieManager.Properties.Resources.Tile_colors_icon_32x32;
            this.beheerTileLayoutToolStripMenuItem1.Name = "beheerTileLayoutToolStripMenuItem1";
            this.beheerTileLayoutToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.beheerTileLayoutToolStripMenuItem1.Size = new System.Drawing.Size(306, 26);
            this.beheerTileLayoutToolStripMenuItem1.Text = "Beheer TileLayout";
            this.beheerTileLayoutToolStripMenuItem1.Click += new System.EventHandler(this.beheerTileLayoutToolStripMenuItem1_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vanLinksNaarRechtsToolStripMenuItem,
            this.vanBovenNaarBenedenToolStripMenuItem,
            this.vanRechtsNaarLinksToolStripMenuItem,
            this.vanOnderNaarBovenToolStripMenuItem});
            this.toolStripMenuItem1.Image = global::ProductieManager.Properties.Resources.layout_widget_icon_32x32;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(306, 26);
            this.toolStripMenuItem1.Text = "Tile Layout Richting";
            this.toolStripMenuItem1.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.toolStripMenuItem1_DropDownItemClicked);
            // 
            // vanLinksNaarRechtsToolStripMenuItem
            // 
            this.vanLinksNaarRechtsToolStripMenuItem.Name = "vanLinksNaarRechtsToolStripMenuItem";
            this.vanLinksNaarRechtsToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.vanLinksNaarRechtsToolStripMenuItem.Text = "VanLinksNaarRechts";
            // 
            // vanBovenNaarBenedenToolStripMenuItem
            // 
            this.vanBovenNaarBenedenToolStripMenuItem.Name = "vanBovenNaarBenedenToolStripMenuItem";
            this.vanBovenNaarBenedenToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.vanBovenNaarBenedenToolStripMenuItem.Text = "VanBovenNaarOnder";
            // 
            // vanRechtsNaarLinksToolStripMenuItem
            // 
            this.vanRechtsNaarLinksToolStripMenuItem.Name = "vanRechtsNaarLinksToolStripMenuItem";
            this.vanRechtsNaarLinksToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.vanRechtsNaarLinksToolStripMenuItem.Text = "VanRechtsNaarLinks";
            // 
            // vanOnderNaarBovenToolStripMenuItem
            // 
            this.vanOnderNaarBovenToolStripMenuItem.Name = "vanOnderNaarBovenToolStripMenuItem";
            this.vanOnderNaarBovenToolStripMenuItem.Size = new System.Drawing.Size(227, 26);
            this.vanOnderNaarBovenToolStripMenuItem.Text = "VanOnderNaarBoven";
            // 
            // xBeheerLijstenToolstripItem
            // 
            this.xBeheerLijstenToolstripItem.Image = global::ProductieManager.Properties.Resources.extension_file_documents_32x32;
            this.xBeheerLijstenToolstripItem.Name = "xBeheerLijstenToolstripItem";
            this.xBeheerLijstenToolstripItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.xBeheerLijstenToolstripItem.Size = new System.Drawing.Size(306, 26);
            this.xBeheerLijstenToolstripItem.Text = "Beheer Tiles";
            this.xBeheerLijstenToolstripItem.Click += new System.EventHandler(this.xBeheerLijstenToolstripItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(303, 6);
            // 
            // reserLayoutToolStripMenuItem
            // 
            this.reserLayoutToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.reserLayoutToolStripMenuItem.Name = "reserLayoutToolStripMenuItem";
            this.reserLayoutToolStripMenuItem.Size = new System.Drawing.Size(306, 26);
            this.reserLayoutToolStripMenuItem.Text = "Reset Layout";
            this.reserLayoutToolStripMenuItem.Click += new System.EventHandler(this.reserLayoutToolStripMenuItem_Click);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.beheerTileLayoutToolStripMenuItem,
            this.toolStripSeparator2,
            this.resetLayoutToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 86);
            // 
            // beheerTileLayoutToolStripMenuItem
            // 
            this.beheerTileLayoutToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.Tile_colors_icon_32x32;
            this.beheerTileLayoutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.beheerTileLayoutToolStripMenuItem.Name = "beheerTileLayoutToolStripMenuItem";
            this.beheerTileLayoutToolStripMenuItem.Size = new System.Drawing.Size(264, 38);
            this.beheerTileLayoutToolStripMenuItem.Text = "Beheer TileLayout";
            this.beheerTileLayoutToolStripMenuItem.Click += new System.EventHandler(this.beheerTileLayoutToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(261, 6);
            // 
            // resetLayoutToolStripMenuItem
            // 
            this.resetLayoutToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.resetLayoutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetLayoutToolStripMenuItem.Name = "resetLayoutToolStripMenuItem";
            this.resetLayoutToolStripMenuItem.Size = new System.Drawing.Size(264, 38);
            this.resetLayoutToolStripMenuItem.Text = "Reset Layout";
            this.resetLayoutToolStripMenuItem.Click += new System.EventHandler(this.reserLayoutToolStripMenuItem_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.ContextMenuStrip = this.contextMenuStrip1;
            this.tableLayoutPanel1.Controls.Add(this.tileViewer1, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(816, 558);
            this.tableLayoutPanel1.TabIndex = 35;
            // 
            // tileViewer1
            // 
            this.tileViewer1.AllowDrop = true;
            this.tileViewer1.AutoScroll = true;
            this.tileViewer1.BackColor = System.Drawing.Color.Transparent;
            this.tileViewer1.ContextMenuStrip = this.contextMenuStrip1;
            this.tileViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileViewer1.EnableSaveTiles = true;
            this.tileViewer1.EnableTileSelection = false;
            this.tileViewer1.EnableTimer = false;
            this.tileViewer1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tileViewer1.Location = new System.Drawing.Point(84, 58);
            this.tileViewer1.MultipleSelections = false;
            this.tileViewer1.Name = "tileViewer1";
            this.tileViewer1.Size = new System.Drawing.Size(646, 440);
            this.tileViewer1.TabIndex = 0;
            this.tileViewer1.TileInfoRefresInterval = 10000;
            this.tileViewer1.TilesLoaded += new System.EventHandler(this.tileViewer1_TilesLoaded);
            this.tileViewer1.TileClicked += new System.EventHandler(this.tileViewer1_TileClicked);
            this.tileViewer1.TileRequestInfo += new Controls.TileChangeEventhandler(this.tileViewer1_TileRequestInfo);
            // 
            // TileMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.xBottomToolMenu);
            this.Name = "TileMainView";
            this.Size = new System.Drawing.Size(816, 586);
            this.xBottomToolMenu.ResumeLayout(false);
            this.xBottomToolMenu.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private TileViewer tileViewer1;
        private System.Windows.Forms.ToolStrip xBottomToolMenu;
        private System.Windows.Forms.ToolStripSplitButton xBeheerweergavetoolstrip;
        private System.Windows.Forms.ToolStripMenuItem xBeheerLijstenToolstripItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ToolStripMenuItem reserLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem vanLinksNaarRechtsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vanBovenNaarBenedenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vanRechtsNaarLinksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vanOnderNaarBovenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem beheerTileLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem resetLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beheerTileLayoutToolStripMenuItem1;
    }
}