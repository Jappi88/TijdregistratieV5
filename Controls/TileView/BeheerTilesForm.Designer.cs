namespace Controls
{
    partial class BeheerTilesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BeheerTilesForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xopslaan = new System.Windows.Forms.Button();
            this.xsluiten = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.xmainlist = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.xaddtile = new System.Windows.Forms.Button();
            this.xlist = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.xedittile = new System.Windows.Forms.Button();
            this.xdeletetile = new System.Windows.Forms.Button();
            this.xmovedown = new System.Windows.Forms.Button();
            this.xmoveup = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xmainlist)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xlist)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xopslaan);
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(20, 547);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(875, 36);
            this.panel1.TabIndex = 1;
            // 
            // xopslaan
            // 
            this.xopslaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xopslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xopslaan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xopslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xopslaan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xopslaan.Location = new System.Drawing.Point(642, 3);
            this.xopslaan.Name = "xopslaan";
            this.xopslaan.Size = new System.Drawing.Size(112, 30);
            this.xopslaan.TabIndex = 1;
            this.xopslaan.Text = "Opslaan";
            this.xopslaan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xopslaan.UseVisualStyleBackColor = true;
            this.xopslaan.Click += new System.EventHandler(this.xopslaan_Click);
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(760, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(112, 30);
            this.xsluiten.TabIndex = 0;
            this.xsluiten.Text = "Annuleren";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(20, 60);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.xmainlist);
            this.splitContainer1.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.xlist);
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Size = new System.Drawing.Size(875, 487);
            this.splitContainer1.SplitterDistance = 403;
            this.splitContainer1.TabIndex = 2;
            // 
            // xmainlist
            // 
            this.xmainlist.AllColumns.Add(this.olvColumn1);
            this.xmainlist.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.xmainlist.CellEditUseWholeCell = false;
            this.xmainlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.xmainlist.Cursor = System.Windows.Forms.Cursors.Default;
            this.xmainlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmainlist.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmainlist.FullRowSelect = true;
            this.xmainlist.GridLines = true;
            this.xmainlist.HideSelection = false;
            this.xmainlist.LargeImageList = this.imageList1;
            this.xmainlist.Location = new System.Drawing.Point(0, 0);
            this.xmainlist.Name = "xmainlist";
            this.xmainlist.ShowFilterMenuOnRightClick = false;
            this.xmainlist.ShowGroups = false;
            this.xmainlist.ShowItemToolTips = true;
            this.xmainlist.Size = new System.Drawing.Size(371, 487);
            this.xmainlist.SmallImageList = this.imageList1;
            this.xmainlist.TabIndex = 0;
            this.xmainlist.UseAlternatingBackColors = true;
            this.xmainlist.UseCompatibleStateImageBehavior = false;
            this.xmainlist.UseExplorerTheme = true;
            this.xmainlist.UseHotItem = true;
            this.xmainlist.UseTranslucentHotItem = true;
            this.xmainlist.UseTranslucentSelection = true;
            this.xmainlist.View = System.Windows.Forms.View.Details;
            this.xmainlist.SelectedIndexChanged += new System.EventHandler(this.xmainlist_SelectedIndexChanged);
            this.xmainlist.DoubleClick += new System.EventHandler(this.xmainlist_DoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Text";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Beschikbare Tiles";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "layout_widget_icon_32x32.png");
            this.imageList1.Images.SetKeyName(1, "Tile_colors_icon_32x32.png");
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xaddtile);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(371, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(32, 487);
            this.panel2.TabIndex = 1;
            // 
            // xaddtile
            // 
            this.xaddtile.Enabled = false;
            this.xaddtile.FlatAppearance.BorderSize = 0;
            this.xaddtile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xaddtile.Image = global::ProductieManager.Properties.Resources.arrow_right_16742_32x32;
            this.xaddtile.Location = new System.Drawing.Point(0, 41);
            this.xaddtile.Name = "xaddtile";
            this.xaddtile.Size = new System.Drawing.Size(32, 32);
            this.xaddtile.TabIndex = 3;
            this.toolTip1.SetToolTip(this.xaddtile, "Voeg Tile Toe");
            this.xaddtile.UseVisualStyleBackColor = true;
            this.xaddtile.Click += new System.EventHandler(this.xaddtile_Click);
            // 
            // xlist
            // 
            this.xlist.AllColumns.Add(this.olvColumn2);
            this.xlist.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.xlist.CellEditUseWholeCell = false;
            this.xlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2});
            this.xlist.Cursor = System.Windows.Forms.Cursors.Default;
            this.xlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xlist.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlist.FullRowSelect = true;
            this.xlist.GridLines = true;
            this.xlist.HideSelection = false;
            this.xlist.LargeImageList = this.imageList1;
            this.xlist.Location = new System.Drawing.Point(0, 0);
            this.xlist.Name = "xlist";
            this.xlist.ShowFilterMenuOnRightClick = false;
            this.xlist.ShowGroups = false;
            this.xlist.ShowItemToolTips = true;
            this.xlist.Size = new System.Drawing.Size(436, 487);
            this.xlist.SmallImageList = this.imageList1;
            this.xlist.TabIndex = 3;
            this.xlist.UseAlternatingBackColors = true;
            this.xlist.UseCompatibleStateImageBehavior = false;
            this.xlist.UseExplorerTheme = true;
            this.xlist.UseHotItem = true;
            this.xlist.UseTranslucentHotItem = true;
            this.xlist.UseTranslucentSelection = true;
            this.xlist.View = System.Windows.Forms.View.Details;
            this.xlist.SelectedIndexChanged += new System.EventHandler(this.xmainlist_SelectedIndexChanged);
            this.xlist.DoubleClick += new System.EventHandler(this.xlist_DoubleClick);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Text";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Zichtbare Tiles";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xedittile);
            this.panel3.Controls.Add(this.xdeletetile);
            this.panel3.Controls.Add(this.xmovedown);
            this.panel3.Controls.Add(this.xmoveup);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(436, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(32, 487);
            this.panel3.TabIndex = 2;
            // 
            // xedittile
            // 
            this.xedittile.Enabled = false;
            this.xedittile.FlatAppearance.BorderSize = 0;
            this.xedittile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xedittile.Image = global::ProductieManager.Properties.Resources.Tile_colors_icon_32x32;
            this.xedittile.Location = new System.Drawing.Point(0, 79);
            this.xedittile.Name = "xedittile";
            this.xedittile.Size = new System.Drawing.Size(32, 32);
            this.xedittile.TabIndex = 3;
            this.toolTip1.SetToolTip(this.xedittile, "Wijzig Tile");
            this.xedittile.UseVisualStyleBackColor = true;
            this.xedittile.Click += new System.EventHandler(this.xedittile_Click);
            // 
            // xdeletetile
            // 
            this.xdeletetile.Enabled = false;
            this.xdeletetile.FlatAppearance.BorderSize = 0;
            this.xdeletetile.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdeletetile.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdeletetile.Location = new System.Drawing.Point(0, 117);
            this.xdeletetile.Name = "xdeletetile";
            this.xdeletetile.Size = new System.Drawing.Size(32, 32);
            this.xdeletetile.TabIndex = 2;
            this.toolTip1.SetToolTip(this.xdeletetile, "Verwijder Tile");
            this.xdeletetile.UseVisualStyleBackColor = true;
            this.xdeletetile.Click += new System.EventHandler(this.xdeletetile_Click);
            // 
            // xmovedown
            // 
            this.xmovedown.Enabled = false;
            this.xmovedown.FlatAppearance.BorderSize = 0;
            this.xmovedown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xmovedown.Image = global::ProductieManager.Properties.Resources.arrow_down_16740_32x32;
            this.xmovedown.Location = new System.Drawing.Point(0, 41);
            this.xmovedown.Name = "xmovedown";
            this.xmovedown.Size = new System.Drawing.Size(32, 32);
            this.xmovedown.TabIndex = 1;
            this.toolTip1.SetToolTip(this.xmovedown, "Plaats Omlaag");
            this.xmovedown.UseVisualStyleBackColor = true;
            this.xmovedown.Click += new System.EventHandler(this.xmovedown_Click);
            // 
            // xmoveup
            // 
            this.xmoveup.Enabled = false;
            this.xmoveup.FlatAppearance.BorderSize = 0;
            this.xmoveup.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xmoveup.Image = global::ProductieManager.Properties.Resources.arrow_up_16741_32x32;
            this.xmoveup.Location = new System.Drawing.Point(0, 3);
            this.xmoveup.Name = "xmoveup";
            this.xmoveup.Size = new System.Drawing.Size(32, 32);
            this.xmoveup.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xmoveup, "Plaats Omhoog");
            this.xmoveup.UseVisualStyleBackColor = true;
            this.xmoveup.Click += new System.EventHandler(this.xmoveup_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.BackColor = System.Drawing.Color.White;
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // BeheerTilesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 603);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel1);
            this.Name = "BeheerTilesForm";
            this.ShowIcon = false;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Beheer Tiles";
            this.panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xmainlist)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xlist)).EndInit();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xopslaan;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private BrightIdeasSoftware.ObjectListView xmainlist;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button xaddtile;
        private System.Windows.Forms.Button xdeletetile;
        private System.Windows.Forms.Button xmovedown;
        private System.Windows.Forms.Button xmoveup;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.ToolTip toolTip1;
        private BrightIdeasSoftware.ObjectListView xlist;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.Button xedittile;
        private System.Windows.Forms.ImageList imageList1;
    }
}