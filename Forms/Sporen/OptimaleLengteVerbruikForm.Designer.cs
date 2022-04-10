
namespace Forms.Sporen
{
    partial class OptimaleLengteVerbruikForm
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
            this.xok = new System.Windows.Forms.Button();
            this.xsluiten = new System.Windows.Forms.Button();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xmaakoverzicht = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xcleartext = new System.Windows.Forms.ToolStripButton();
            this.xzoekbalk = new System.Windows.Forms.ToolStripTextBox();
            this.xallesafronden = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.xopslaan = new System.Windows.Forms.ToolStripButton();
            this.xMaterialenLijst = new Controls.CustomObjectListview();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.wijzigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.verwijderenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xMaterialenLijst)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xok);
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 502);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(994, 43);
            this.panel1.TabIndex = 0;
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Image = global::ProductieManager.Properties.Resources.microsoft_excel_22733;
            this.xok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xok.Location = new System.Drawing.Point(732, 3);
            this.xok.Margin = new System.Windows.Forms.Padding(4);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(143, 36);
            this.xok.TabIndex = 7;
            this.xok.Text = "Export Excel";
            this.xok.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(883, 3);
            this.xsluiten.Margin = new System.Windows.Forms.Padding(4);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(107, 36);
            this.xsluiten.TabIndex = 8;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xmaakoverzicht,
            this.toolStripSeparator1,
            this.xcleartext,
            this.xzoekbalk,
            this.xallesafronden,
            this.toolStripSeparator3,
            this.xopslaan});
            this.toolStrip1.Location = new System.Drawing.Point(20, 60);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(994, 39);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // xmaakoverzicht
            // 
            this.xmaakoverzicht.Image = global::ProductieManager.Properties.Resources.maths_math_32x32;
            this.xmaakoverzicht.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xmaakoverzicht.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xmaakoverzicht.Name = "xmaakoverzicht";
            this.xmaakoverzicht.Size = new System.Drawing.Size(125, 36);
            this.xmaakoverzicht.Text = "Maak Overzicht";
            this.xmaakoverzicht.ToolTipText = "Maak een optimale Overzicht";
            this.xmaakoverzicht.Click += new System.EventHandler(this.xmaakoverzicht_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // xcleartext
            // 
            this.xcleartext.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xcleartext.AutoSize = false;
            this.xcleartext.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xcleartext.Image = global::ProductieManager.Properties.Resources.cancel_close_cross_delete_32x32;
            this.xcleartext.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xcleartext.Name = "xcleartext";
            this.xcleartext.Size = new System.Drawing.Size(23, 28);
            this.xcleartext.Text = "toolStripButton2";
            this.xcleartext.Click += new System.EventHandler(this.xcleartext_Click);
            // 
            // xzoekbalk
            // 
            this.xzoekbalk.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xzoekbalk.AutoSize = false;
            this.xzoekbalk.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.xzoekbalk.Name = "xzoekbalk";
            this.xzoekbalk.Size = new System.Drawing.Size(200, 28);
            this.xzoekbalk.Text = "Zoeken...";
            this.xzoekbalk.Enter += new System.EventHandler(this.xzoekbalk_Enter);
            this.xzoekbalk.Leave += new System.EventHandler(this.xzoekbalk_Leave);
            this.xzoekbalk.TextChanged += new System.EventHandler(this.xzoekbalk_TextChanged);
            // 
            // xallesafronden
            // 
            this.xallesafronden.Enabled = false;
            this.xallesafronden.Image = global::ProductieManager.Properties.Resources.code_html_link_share_icon_123633;
            this.xallesafronden.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xallesafronden.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xallesafronden.Name = "xallesafronden";
            this.xallesafronden.Size = new System.Drawing.Size(121, 36);
            this.xallesafronden.Text = "Alles Afronden";
            this.xallesafronden.Click += new System.EventHandler(this.xallesafronden_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 39);
            // 
            // xopslaan
            // 
            this.xopslaan.Enabled = false;
            this.xopslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xopslaan.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xopslaan.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xopslaan.Name = "xopslaan";
            this.xopslaan.Size = new System.Drawing.Size(86, 36);
            this.xopslaan.Text = "Opslaan";
            this.xopslaan.Click += new System.EventHandler(this.xopslaan_Click);
            // 
            // xMaterialenLijst
            // 
            this.xMaterialenLijst.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.xMaterialenLijst.CellEditUseWholeCell = false;
            this.xMaterialenLijst.ContextMenuStrip = this.contextMenuStrip1;
            this.xMaterialenLijst.Cursor = System.Windows.Forms.Cursors.Default;
            this.xMaterialenLijst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xMaterialenLijst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xMaterialenLijst.FullRowSelect = true;
            this.xMaterialenLijst.GridLines = true;
            this.xMaterialenLijst.HeaderWordWrap = true;
            this.xMaterialenLijst.HideSelection = false;
            this.xMaterialenLijst.LargeImageList = this.imageList1;
            this.xMaterialenLijst.Location = new System.Drawing.Point(20, 99);
            this.xMaterialenLijst.MenuLabelColumns = "kolommen";
            this.xMaterialenLijst.MenuLabelGroupBy = "Groeperen op \'{0}\'";
            this.xMaterialenLijst.MenuLabelLockGroupingOn = "Groepering vergrendelen op \'{0}\'";
            this.xMaterialenLijst.MenuLabelSelectColumns = "Selecteer kolommen...";
            this.xMaterialenLijst.MenuLabelSortAscending = "Sorteer oplopend op \'{0}\'";
            this.xMaterialenLijst.MenuLabelSortDescending = "Aflopend sorteren op \'{0}\'";
            this.xMaterialenLijst.MenuLabelTurnOffGroups = "Groepen uitschakelen";
            this.xMaterialenLijst.MenuLabelUnlockGroupingOn = "Ontgrendel groeperen van \'{0}\'";
            this.xMaterialenLijst.MenuLabelUnsort = "Uitsorteren";
            this.xMaterialenLijst.Name = "xMaterialenLijst";
            this.xMaterialenLijst.OwnerDraw = false;
            this.xMaterialenLijst.ShowCommandMenuOnRightClick = true;
            this.xMaterialenLijst.ShowGroups = false;
            this.xMaterialenLijst.ShowItemCountOnGroups = true;
            this.xMaterialenLijst.ShowItemToolTips = true;
            this.xMaterialenLijst.Size = new System.Drawing.Size(994, 403);
            this.xMaterialenLijst.SmallImageList = this.imageList1;
            this.xMaterialenLijst.SpaceBetweenGroups = 10;
            this.xMaterialenLijst.TabIndex = 26;
            this.xMaterialenLijst.TileSize = new System.Drawing.Size(300, 120);
            this.xMaterialenLijst.TintSortColumn = true;
            this.xMaterialenLijst.UseCompatibleStateImageBehavior = false;
            this.xMaterialenLijst.UseExplorerTheme = true;
            this.xMaterialenLijst.UseFiltering = true;
            this.xMaterialenLijst.UseHotControls = false;
            this.xMaterialenLijst.UseHotItem = true;
            this.xMaterialenLijst.UseOverlays = false;
            this.xMaterialenLijst.UseTranslucentHotItem = true;
            this.xMaterialenLijst.UseTranslucentSelection = true;
            this.xMaterialenLijst.View = System.Windows.Forms.View.Details;
            this.xMaterialenLijst.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.xMaterialenLijst_CellEditFinished);
            this.xMaterialenLijst.CellEditFinishing += new BrightIdeasSoftware.CellEditEventHandler(this.xMaterialenLijst_CellEditFinishing);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wijzigToolStripMenuItem,
            this.toolStripSeparator2,
            this.verwijderenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 54);
            // 
            // wijzigToolStripMenuItem
            // 
            this.wijzigToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.code_html_link_share_icon_123633;
            this.wijzigToolStripMenuItem.Name = "wijzigToolStripMenuItem";
            this.wijzigToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.wijzigToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.wijzigToolStripMenuItem.Text = "Afronden";
            this.wijzigToolStripMenuItem.Click += new System.EventHandler(this.wijzigToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(156, 6);
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
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // OptimaleLengteVerbruikForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 565);
            this.Controls.Add(this.xMaterialenLijst);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.panel1);
            this.Name = "OptimaleLengteVerbruikForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Optimale Lengte Verbruik";
            this.Title = "Optimale Lengte Verbruik";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OptimaleLengteVerbruikForm_FormClosing);
            this.Shown += new System.EventHandler(this.OptimaleLengteVerbruikForm_Shown);
            this.panel1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xMaterialenLijst)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private Controls.CustomObjectListview xMaterialenLijst;
        private System.Windows.Forms.Button xok;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.ToolStripButton xmaakoverzicht;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton xcleartext;
        private System.Windows.Forms.ToolStripTextBox xzoekbalk;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wijzigToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem verwijderenToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton xallesafronden;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton xopslaan;
    }
}