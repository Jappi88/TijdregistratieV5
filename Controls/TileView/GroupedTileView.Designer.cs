
namespace Controls.TileView
{
    partial class GroupedTileView
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
            this.xMainGroup = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.beheerTileLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.sorterenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.naamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.typeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kleurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.resetLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xGroupSluiten = new System.Windows.Forms.ToolStripButton();
            this.xBeheerweergavetoolstrip = new System.Windows.Forms.ToolStripSplitButton();
            this.wijzigGroepNaamToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.kiesAchtergrondKleurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.beheerTileLayoutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xBeheerLijstenToolstripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.vanLinksNaarRechtsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vanBovenNaarBenedenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vanRechtsNaarLinksToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vanOnderNaarBovenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.reserLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.tileViewer1 = new Controls.TileViewer();
            this.xMainGroup.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xMainGroup
            // 
            this.xMainGroup.BackColor = System.Drawing.Color.Transparent;
            this.xMainGroup.Controls.Add(this.tileViewer1);
            this.xMainGroup.Controls.Add(this.toolStrip1);
            this.xMainGroup.Cursor = System.Windows.Forms.Cursors.Default;
            this.xMainGroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xMainGroup.Font = new System.Drawing.Font("Tahoma", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xMainGroup.Location = new System.Drawing.Point(0, 0);
            this.xMainGroup.Name = "xMainGroup";
            this.xMainGroup.Padding = new System.Windows.Forms.Padding(10);
            this.xMainGroup.Size = new System.Drawing.Size(847, 550);
            this.xMainGroup.TabIndex = 1;
            this.xMainGroup.TabStop = false;
            this.xMainGroup.Text = "Tiles Groep";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem5,
            this.toolStripMenuItem4,
            this.toolStripSeparator6,
            this.toolStripMenuItem3,
            this.beheerTileLayoutToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripMenuItem2,
            this.toolStripSeparator5,
            this.sorterenToolStripMenuItem,
            this.resetLayoutToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(225, 288);
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Enabled = false;
            this.toolStripMenuItem4.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.toolStripMenuItem4.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(224, 38);
            this.toolStripMenuItem4.Text = "Wijzig Groep Naam";
            this.toolStripMenuItem4.Click += new System.EventHandler(this.wijzigGroepNaamToolStripMenuItem_Click);
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Image = global::ProductieManager.Properties.Resources.Edit_color_32x32;
            this.toolStripMenuItem3.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(224, 38);
            this.toolStripMenuItem3.Text = "Kies Achtergrond Kleur";
            this.toolStripMenuItem3.Click += new System.EventHandler(this.kiesAchtergrondKleurToolStripMenuItem_Click);
            // 
            // beheerTileLayoutToolStripMenuItem
            // 
            this.beheerTileLayoutToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.Tile_colors_icon_32x32;
            this.beheerTileLayoutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.beheerTileLayoutToolStripMenuItem.Name = "beheerTileLayoutToolStripMenuItem";
            this.beheerTileLayoutToolStripMenuItem.Size = new System.Drawing.Size(224, 38);
            this.beheerTileLayoutToolStripMenuItem.Text = "Wijzig TileLayout";
            this.beheerTileLayoutToolStripMenuItem.Click += new System.EventHandler(this.beheerTileLayoutToolStripMenuItem1_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(221, 6);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Image = global::ProductieManager.Properties.Resources.layout_widget_icon_32x32;
            this.toolStripMenuItem2.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.toolStripMenuItem2.Size = new System.Drawing.Size(224, 38);
            this.toolStripMenuItem2.Text = "Beheer Tiles";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.xBeheerLijstenToolstripItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(221, 6);
            // 
            // sorterenToolStripMenuItem
            // 
            this.sorterenToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.naamToolStripMenuItem,
            this.typeToolStripMenuItem,
            this.kleurToolStripMenuItem});
            this.sorterenToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.sort_icon_149866_32x32;
            this.sorterenToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.sorterenToolStripMenuItem.Name = "sorterenToolStripMenuItem";
            this.sorterenToolStripMenuItem.Size = new System.Drawing.Size(224, 38);
            this.sorterenToolStripMenuItem.Text = "Sorteer Op";
            // 
            // naamToolStripMenuItem
            // 
            this.naamToolStripMenuItem.Name = "naamToolStripMenuItem";
            this.naamToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.naamToolStripMenuItem.Text = "Naam";
            this.naamToolStripMenuItem.Click += new System.EventHandler(this.naamToolStripMenuItem_Click);
            // 
            // typeToolStripMenuItem
            // 
            this.typeToolStripMenuItem.Name = "typeToolStripMenuItem";
            this.typeToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.typeToolStripMenuItem.Text = "Type";
            this.typeToolStripMenuItem.Click += new System.EventHandler(this.typeToolStripMenuItem_Click);
            // 
            // kleurToolStripMenuItem
            // 
            this.kleurToolStripMenuItem.Name = "kleurToolStripMenuItem";
            this.kleurToolStripMenuItem.Size = new System.Drawing.Size(106, 22);
            this.kleurToolStripMenuItem.Text = "Kleur";
            this.kleurToolStripMenuItem.Click += new System.EventHandler(this.kleurToolStripMenuItem_Click);
            // 
            // resetLayoutToolStripMenuItem
            // 
            this.resetLayoutToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.resetLayoutToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.resetLayoutToolStripMenuItem.Name = "resetLayoutToolStripMenuItem";
            this.resetLayoutToolStripMenuItem.Size = new System.Drawing.Size(224, 38);
            this.resetLayoutToolStripMenuItem.Text = "Reset Layout";
            this.resetLayoutToolStripMenuItem.Click += new System.EventHandler(this.reserLayoutToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xGroupSluiten,
            this.xBeheerweergavetoolstrip});
            this.toolStrip1.Location = new System.Drawing.Point(10, 515);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(827, 25);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // xGroupSluiten
            // 
            this.xGroupSluiten.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xGroupSluiten.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xGroupSluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xGroupSluiten.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xGroupSluiten.Name = "xGroupSluiten";
            this.xGroupSluiten.Size = new System.Drawing.Size(23, 22);
            this.xGroupSluiten.ToolTipText = "Group Sluiten";
            this.xGroupSluiten.Visible = false;
            // 
            // xBeheerweergavetoolstrip
            // 
            this.xBeheerweergavetoolstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wijzigGroepNaamToolStripMenuItem,
            this.toolStripSeparator3,
            this.kiesAchtergrondKleurToolStripMenuItem,
            this.beheerTileLayoutToolStripMenuItem1,
            this.toolStripSeparator1,
            this.xBeheerLijstenToolstripItem,
            this.toolStripMenuItem1,
            this.toolStripSeparator4,
            this.reserLayoutToolStripMenuItem});
            this.xBeheerweergavetoolstrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xBeheerweergavetoolstrip.Image = global::ProductieManager.Properties.Resources.layout_widget_icon_32x32;
            this.xBeheerweergavetoolstrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xBeheerweergavetoolstrip.Name = "xBeheerweergavetoolstrip";
            this.xBeheerweergavetoolstrip.Size = new System.Drawing.Size(32, 22);
            this.xBeheerweergavetoolstrip.Click += new System.EventHandler(this.xBeheerweergavetoolstrip_Click);
            // 
            // wijzigGroepNaamToolStripMenuItem
            // 
            this.wijzigGroepNaamToolStripMenuItem.Enabled = false;
            this.wijzigGroepNaamToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.wijzigGroepNaamToolStripMenuItem.Name = "wijzigGroepNaamToolStripMenuItem";
            this.wijzigGroepNaamToolStripMenuItem.Size = new System.Drawing.Size(295, 26);
            this.wijzigGroepNaamToolStripMenuItem.Text = "Wijzig Groep Naam";
            this.wijzigGroepNaamToolStripMenuItem.Click += new System.EventHandler(this.wijzigGroepNaamToolStripMenuItem_Click);
            // 
            // kiesAchtergrondKleurToolStripMenuItem
            // 
            this.kiesAchtergrondKleurToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.Edit_color_32x32;
            this.kiesAchtergrondKleurToolStripMenuItem.Name = "kiesAchtergrondKleurToolStripMenuItem";
            this.kiesAchtergrondKleurToolStripMenuItem.Size = new System.Drawing.Size(295, 26);
            this.kiesAchtergrondKleurToolStripMenuItem.Text = "Kies Achtergrond Kleur";
            this.kiesAchtergrondKleurToolStripMenuItem.Click += new System.EventHandler(this.kiesAchtergrondKleurToolStripMenuItem_Click);
            // 
            // beheerTileLayoutToolStripMenuItem1
            // 
            this.beheerTileLayoutToolStripMenuItem1.Image = global::ProductieManager.Properties.Resources.Tile_colors_icon_32x32;
            this.beheerTileLayoutToolStripMenuItem1.Name = "beheerTileLayoutToolStripMenuItem1";
            this.beheerTileLayoutToolStripMenuItem1.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.T)));
            this.beheerTileLayoutToolStripMenuItem1.Size = new System.Drawing.Size(295, 26);
            this.beheerTileLayoutToolStripMenuItem1.Text = "Wijzig TileLayout";
            this.beheerTileLayoutToolStripMenuItem1.Click += new System.EventHandler(this.beheerTileLayoutToolStripMenuItem1_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(292, 6);
            // 
            // xBeheerLijstenToolstripItem
            // 
            this.xBeheerLijstenToolstripItem.Image = global::ProductieManager.Properties.Resources.layout_widget_icon_32x32;
            this.xBeheerLijstenToolstripItem.Name = "xBeheerLijstenToolstripItem";
            this.xBeheerLijstenToolstripItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.L)));
            this.xBeheerLijstenToolstripItem.Size = new System.Drawing.Size(295, 26);
            this.xBeheerLijstenToolstripItem.Text = "Beheer Tiles";
            this.xBeheerLijstenToolstripItem.Click += new System.EventHandler(this.xBeheerLijstenToolstripItem_Click);
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
            this.toolStripMenuItem1.Size = new System.Drawing.Size(295, 26);
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
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(292, 6);
            // 
            // reserLayoutToolStripMenuItem
            // 
            this.reserLayoutToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.reserLayoutToolStripMenuItem.Name = "reserLayoutToolStripMenuItem";
            this.reserLayoutToolStripMenuItem.Size = new System.Drawing.Size(295, 26);
            this.reserLayoutToolStripMenuItem.Text = "Reset Layout";
            this.reserLayoutToolStripMenuItem.Click += new System.EventHandler(this.reserLayoutToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(292, 6);
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Enabled = false;
            this.toolStripMenuItem5.Image = global::ProductieManager.Properties.Resources.icons8_expand_32;
            this.toolStripMenuItem5.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(224, 38);
            this.toolStripMenuItem5.Text = "Wijzig Groep Grootte";
            this.toolStripMenuItem5.Click += new System.EventHandler(this.wijzigGroepGrootteToolStripMenuItem_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(221, 6);
            // 
            // tileViewer1
            // 
            this.tileViewer1.AllowDrop = true;
            this.tileViewer1.AutoScroll = true;
            this.tileViewer1.BackColor = System.Drawing.Color.White;
            this.tileViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileViewer1.EnableSaveTiles = true;
            this.tileViewer1.EnableTileSelection = false;
            this.tileViewer1.EnableTimer = false;
            this.tileViewer1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tileViewer1.GroupName = null;
            this.tileViewer1.IsCompactMode = false;
            this.tileViewer1.Location = new System.Drawing.Point(10, 39);
            this.tileViewer1.MultipleSelections = false;
            this.tileViewer1.Name = "tileViewer1";
            this.tileViewer1.Padding = new System.Windows.Forms.Padding(50);
            this.tileViewer1.Size = new System.Drawing.Size(827, 476);
            this.tileViewer1.TabIndex = 2;
            this.tileViewer1.TileInfoRefresInterval = 10000;
            this.tileViewer1.TilesLoaded += new System.EventHandler(this.OnTilesLoaded);
            this.tileViewer1.TileClicked += new System.EventHandler(this.OnTileClicked);
            // 
            // GroupedTileView
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xMainGroup);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MinimumSize = new System.Drawing.Size(250, 250);
            this.Name = "GroupedTileView";
            this.Size = new System.Drawing.Size(847, 550);
            this.xMainGroup.ResumeLayout(false);
            this.xMainGroup.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox xMainGroup;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton xGroupSluiten;
        private System.Windows.Forms.ToolStripSplitButton xBeheerweergavetoolstrip;
        private System.Windows.Forms.ToolStripMenuItem wijzigGroepNaamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kiesAchtergrondKleurToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem beheerTileLayoutToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem xBeheerLijstenToolstripItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem vanLinksNaarRechtsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vanBovenNaarBenedenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vanRechtsNaarLinksToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vanOnderNaarBovenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem reserLayoutToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem beheerTileLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem sorterenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem naamToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem typeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem kleurToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetLayoutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem5;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private TileViewer tileViewer1;
    }
}
