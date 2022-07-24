
namespace Forms.PersoneelVerzoek
{
    partial class VerzoekForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xZoekTextbox = new System.Windows.Forms.ToolStripTextBox();
            this.xClearZoek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.xpersoneelnamen = new System.Windows.Forms.ToolStripTextBox();
            this.xPersoneelSluiten = new System.Windows.Forms.ToolStripButton();
            this.xKiesPersoneel = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xNieuweVerzoek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.xEditVerzoek = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.xverwijderverzoek = new System.Windows.Forms.ToolStripButton();
            this.xVerzoeklijst = new Controls.CustomObjectListview();
            this.xColumn9 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xColmn0 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xColumn8 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xColmn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.wijzigenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.verwijderenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xPersoneelTimer = new System.Windows.Forms.Timer(this.components);
            this.xalleafdelingencheckbox = new MetroFramework.Controls.MetroCheckBox();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xVerzoeklijst)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xZoekTextbox,
            this.xClearZoek,
            this.toolStripSeparator3,
            this.xpersoneelnamen,
            this.xPersoneelSluiten,
            this.xKiesPersoneel,
            this.toolStripSeparator1,
            this.xNieuweVerzoek,
            this.toolStripSeparator4,
            this.xEditVerzoek,
            this.toolStripSeparator2,
            this.xverwijderverzoek});
            this.toolStrip1.Location = new System.Drawing.Point(20, 60);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(683, 25);
            this.toolStrip1.TabIndex = 27;
            // 
            // xZoekTextbox
            // 
            this.xZoekTextbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xZoekTextbox.ForeColor = System.Drawing.Color.Gray;
            this.xZoekTextbox.Name = "xZoekTextbox";
            this.xZoekTextbox.Size = new System.Drawing.Size(200, 25);
            this.xZoekTextbox.Text = "Zoeken...";
            this.xZoekTextbox.ToolTipText = "Zoeken...";
            this.xZoekTextbox.Enter += new System.EventHandler(this.xZoekTextbox_Enter);
            this.xZoekTextbox.Leave += new System.EventHandler(this.xZoekTextbox_Leave);
            this.xZoekTextbox.TextChanged += new System.EventHandler(this.xZoekTextbox_TextChanged);
            // 
            // xClearZoek
            // 
            this.xClearZoek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xClearZoek.Image = global::ProductieManager.Properties.Resources.cancel_close_cross_delete_32x32;
            this.xClearZoek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xClearZoek.Name = "xClearZoek";
            this.xClearZoek.Size = new System.Drawing.Size(23, 22);
            this.xClearZoek.Text = "toolStripButton1";
            this.xClearZoek.Click += new System.EventHandler(this.xClearZoek_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // xpersoneelnamen
            // 
            this.xpersoneelnamen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.xpersoneelnamen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.xpersoneelnamen.BackColor = System.Drawing.Color.White;
            this.xpersoneelnamen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xpersoneelnamen.ForeColor = System.Drawing.Color.Gray;
            this.xpersoneelnamen.Name = "xpersoneelnamen";
            this.xpersoneelnamen.Size = new System.Drawing.Size(150, 25);
            this.xpersoneelnamen.Text = "Personeelnaam...";
            this.xpersoneelnamen.Enter += new System.EventHandler(this.xpersoneelnamen_Enter);
            this.xpersoneelnamen.Leave += new System.EventHandler(this.xpersoneelnamen_Leave);
            this.xpersoneelnamen.TextChanged += new System.EventHandler(this.xpersoneelnamen_TextChanged);
            // 
            // xPersoneelSluiten
            // 
            this.xPersoneelSluiten.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xPersoneelSluiten.Image = global::ProductieManager.Properties.Resources.cancel_close_cross_delete_32x32;
            this.xPersoneelSluiten.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xPersoneelSluiten.Name = "xPersoneelSluiten";
            this.xPersoneelSluiten.Size = new System.Drawing.Size(23, 22);
            this.xPersoneelSluiten.ToolTipText = "Personeel sluiten";
            this.xPersoneelSluiten.Click += new System.EventHandler(this.xPersoneelSluiten_Click);
            // 
            // xKiesPersoneel
            // 
            this.xKiesPersoneel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xKiesPersoneel.Image = global::ProductieManager.Properties.Resources.users_12820;
            this.xKiesPersoneel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xKiesPersoneel.Name = "xKiesPersoneel";
            this.xKiesPersoneel.Size = new System.Drawing.Size(23, 22);
            this.xKiesPersoneel.Text = "toolStripButton1";
            this.xKiesPersoneel.ToolTipText = "Kies personeel";
            this.xKiesPersoneel.Click += new System.EventHandler(this.xKiesPersoneel_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // xNieuweVerzoek
            // 
            this.xNieuweVerzoek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xNieuweVerzoek.Enabled = false;
            this.xNieuweVerzoek.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xNieuweVerzoek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xNieuweVerzoek.Name = "xNieuweVerzoek";
            this.xNieuweVerzoek.Size = new System.Drawing.Size(23, 22);
            this.xNieuweVerzoek.ToolTipText = "Verzoek indienen";
            this.xNieuweVerzoek.Click += new System.EventHandler(this.xNieuweVerzoek_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // xEditVerzoek
            // 
            this.xEditVerzoek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xEditVerzoek.Enabled = false;
            this.xEditVerzoek.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xEditVerzoek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xEditVerzoek.Name = "xEditVerzoek";
            this.xEditVerzoek.Size = new System.Drawing.Size(23, 22);
            this.xEditVerzoek.Text = "Wijzig verzoek";
            this.xEditVerzoek.ToolTipText = "Wijzig verzoek";
            this.xEditVerzoek.Click += new System.EventHandler(this.xEditVerzoek_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // xverwijderverzoek
            // 
            this.xverwijderverzoek.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xverwijderverzoek.Enabled = false;
            this.xverwijderverzoek.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xverwijderverzoek.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xverwijderverzoek.Name = "xverwijderverzoek";
            this.xverwijderverzoek.Size = new System.Drawing.Size(23, 22);
            this.xverwijderverzoek.ToolTipText = "Verzoek verwijderen";
            this.xverwijderverzoek.Click += new System.EventHandler(this.xverwijderverzoek_Click);
            // 
            // xVerzoeklijst
            // 
            this.xVerzoeklijst.AllColumns.Add(this.xColumn9);
            this.xVerzoeklijst.AllColumns.Add(this.xColmn0);
            this.xVerzoeklijst.AllColumns.Add(this.xColumn8);
            this.xVerzoeklijst.AllColumns.Add(this.xColmn2);
            this.xVerzoeklijst.AllColumns.Add(this.xColumn3);
            this.xVerzoeklijst.AllColumns.Add(this.xColumn4);
            this.xVerzoeklijst.AllColumns.Add(this.olvColumn1);
            this.xVerzoeklijst.AllColumns.Add(this.xColumn5);
            this.xVerzoeklijst.AllColumns.Add(this.olvColumn2);
            this.xVerzoeklijst.AllColumns.Add(this.xColumn6);
            this.xVerzoeklijst.AllColumns.Add(this.xColumn7);
            this.xVerzoeklijst.AllowCellEdit = false;
            this.xVerzoeklijst.CellEditUseWholeCell = false;
            this.xVerzoeklijst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.xColumn9,
            this.xColmn0,
            this.xColumn8,
            this.xColmn2,
            this.xColumn3,
            this.xColumn4,
            this.olvColumn1,
            this.xColumn5,
            this.olvColumn2,
            this.xColumn6,
            this.xColumn7});
            this.xVerzoeklijst.ContextMenuStrip = this.contextMenuStrip1;
            this.xVerzoeklijst.Cursor = System.Windows.Forms.Cursors.Default;
            this.xVerzoeklijst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xVerzoeklijst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xVerzoeklijst.FullRowSelect = true;
            this.xVerzoeklijst.HeaderWordWrap = true;
            this.xVerzoeklijst.HideSelection = false;
            this.xVerzoeklijst.LargeImageList = this.imageList1;
            this.xVerzoeklijst.Location = new System.Drawing.Point(20, 85);
            this.xVerzoeklijst.MenuLabelColumns = "kolommen";
            this.xVerzoeklijst.MenuLabelGroupBy = "Groeperen op \'{0}\'";
            this.xVerzoeklijst.MenuLabelLockGroupingOn = "Groepering vergrendelen op \'{0}\'";
            this.xVerzoeklijst.MenuLabelSelectColumns = "Selecteer kolommen...";
            this.xVerzoeklijst.MenuLabelSortAscending = "Sorteer oplopend op \'{0}\'";
            this.xVerzoeklijst.MenuLabelSortDescending = "Aflopend sorteren op \'{0}\'";
            this.xVerzoeklijst.MenuLabelTurnOffGroups = "Groepen uitschakelen";
            this.xVerzoeklijst.MenuLabelUnlockGroupingOn = "Ontgrendel groeperen van \'{0}\'";
            this.xVerzoeklijst.MenuLabelUnsort = "Uitsorteren";
            this.xVerzoeklijst.Name = "xVerzoeklijst";
            this.xVerzoeklijst.OwnerDraw = false;
            this.xVerzoeklijst.ShowCommandMenuOnRightClick = true;
            this.xVerzoeklijst.ShowGroups = false;
            this.xVerzoeklijst.ShowItemCountOnGroups = true;
            this.xVerzoeklijst.ShowItemToolTips = true;
            this.xVerzoeklijst.Size = new System.Drawing.Size(683, 282);
            this.xVerzoeklijst.SmallImageList = this.imageList1;
            this.xVerzoeklijst.SpaceBetweenGroups = 10;
            this.xVerzoeklijst.TabIndex = 26;
            this.xVerzoeklijst.TileSize = new System.Drawing.Size(300, 120);
            this.xVerzoeklijst.UseCompatibleStateImageBehavior = false;
            this.xVerzoeklijst.UseExplorerTheme = true;
            this.xVerzoeklijst.UseFiltering = true;
            this.xVerzoeklijst.UseHotControls = false;
            this.xVerzoeklijst.UseHotItem = true;
            this.xVerzoeklijst.UseOverlays = false;
            this.xVerzoeklijst.UseTranslucentHotItem = true;
            this.xVerzoeklijst.UseTranslucentSelection = true;
            this.xVerzoeklijst.View = System.Windows.Forms.View.Details;
            this.xVerzoeklijst.SelectedIndexChanged += new System.EventHandler(this.xVerzoeklijst_SelectedIndexChanged);
            this.xVerzoeklijst.DoubleClick += new System.EventHandler(this.xVerzoeklijst_DoubleClick);
            // 
            // xColumn9
            // 
            this.xColumn9.AspectName = "Status";
            this.xColumn9.IsEditable = false;
            this.xColumn9.IsTileViewColumn = true;
            this.xColumn9.Text = "Status";
            this.xColumn9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xColumn9.ToolTipText = "Verzoek status";
            this.xColumn9.Width = 150;
            this.xColumn9.WordWrap = true;
            // 
            // xColmn0
            // 
            this.xColmn0.AspectName = "PersoneelNaam";
            this.xColmn0.IsEditable = false;
            this.xColmn0.IsTileViewColumn = true;
            this.xColmn0.Text = "Personeel";
            this.xColmn0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xColmn0.ToolTipText = "Personeel naam";
            this.xColmn0.Width = 120;
            this.xColmn0.WordWrap = true;
            // 
            // xColumn8
            // 
            this.xColumn8.AspectName = "VerzoekSoort";
            this.xColumn8.IsEditable = false;
            this.xColumn8.IsTileViewColumn = true;
            this.xColumn8.Text = "Type";
            this.xColumn8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xColumn8.ToolTipText = "Verzoek type";
            this.xColumn8.Width = 120;
            this.xColumn8.WordWrap = true;
            // 
            // xColmn2
            // 
            this.xColmn2.AspectName = "StartDatum";
            this.xColmn2.IsEditable = false;
            this.xColmn2.IsTileViewColumn = true;
            this.xColmn2.Text = "Start Datum";
            this.xColmn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xColmn2.ToolTipText = "Start datum";
            this.xColmn2.Width = 150;
            this.xColmn2.WordWrap = true;
            // 
            // xColumn3
            // 
            this.xColumn3.AspectName = "EindDatum";
            this.xColumn3.IsEditable = false;
            this.xColumn3.IsTileViewColumn = true;
            this.xColumn3.Text = "Eind Datum";
            this.xColumn3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xColumn3.ToolTipText = "Eind datum";
            this.xColumn3.Width = 150;
            this.xColumn3.WordWrap = true;
            // 
            // xColumn4
            // 
            this.xColumn4.AspectName = "TotaalTijd";
            this.xColumn4.AspectToStringFormat = "{0} uur";
            this.xColumn4.IsEditable = false;
            this.xColumn4.IsTileViewColumn = true;
            this.xColumn4.Text = "Totaal tijd";
            this.xColumn4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xColumn4.ToolTipText = "Totaal tijd";
            this.xColumn4.Width = 120;
            this.xColumn4.WordWrap = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "NaamMelder";
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Ingediend door";
            this.olvColumn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn1.ToolTipText = "Ingediend door";
            this.olvColumn1.Width = 120;
            this.olvColumn1.WordWrap = true;
            // 
            // xColumn5
            // 
            this.xColumn5.AspectName = "IngediendOp";
            this.xColumn5.IsEditable = false;
            this.xColumn5.IsTileViewColumn = true;
            this.xColumn5.Text = "Ingediend Op";
            this.xColumn5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xColumn5.ToolTipText = "Ingediend op";
            this.xColumn5.Width = 150;
            this.xColumn5.WordWrap = true;
            // 
            // xColumn6
            // 
            this.xColumn6.AspectName = "VerzoekMelding";
            this.xColumn6.IsEditable = false;
            this.xColumn6.IsTileViewColumn = true;
            this.xColumn6.Text = "Notitie";
            this.xColumn6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xColumn6.ToolTipText = "Verzoek notitie";
            this.xColumn6.Width = 200;
            this.xColumn6.WordWrap = true;
            // 
            // xColumn7
            // 
            this.xColumn7.AspectName = "VerzoekReactie";
            this.xColumn7.IsEditable = false;
            this.xColumn7.IsTileViewColumn = true;
            this.xColumn7.Text = "Reactie";
            this.xColumn7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xColumn7.ToolTipText = "Verzoek reactie";
            this.xColumn7.Width = 200;
            this.xColumn7.WordWrap = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wijzigenToolStripMenuItem,
            this.toolStripSeparator5,
            this.verwijderenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(136, 54);
            // 
            // wijzigenToolStripMenuItem
            // 
            this.wijzigenToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.wijzigenToolStripMenuItem.Name = "wijzigenToolStripMenuItem";
            this.wijzigenToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.wijzigenToolStripMenuItem.Text = "Wijzigen";
            this.wijzigenToolStripMenuItem.Click += new System.EventHandler(this.xEditVerzoek_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(132, 6);
            // 
            // verwijderenToolStripMenuItem
            // 
            this.verwijderenToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.verwijderenToolStripMenuItem.Name = "verwijderenToolStripMenuItem";
            this.verwijderenToolStripMenuItem.Size = new System.Drawing.Size(135, 22);
            this.verwijderenToolStripMenuItem.Text = "Verwijderen";
            this.verwijderenToolStripMenuItem.Click += new System.EventHandler(this.xverwijderverzoek_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xPersoneelTimer
            // 
            this.xPersoneelTimer.Interval = 1000;
            this.xPersoneelTimer.Tick += new System.EventHandler(this.xPersoneelTimer_Tick);
            // 
            // xalleafdelingencheckbox
            // 
            this.xalleafdelingencheckbox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xalleafdelingencheckbox.AutoSize = true;
            this.xalleafdelingencheckbox.FontWeight = MetroFramework.MetroCheckBoxWeight.Bold;
            this.xalleafdelingencheckbox.Location = new System.Drawing.Point(563, 64);
            this.xalleafdelingencheckbox.Name = "xalleafdelingencheckbox";
            this.xalleafdelingencheckbox.Size = new System.Drawing.Size(137, 15);
            this.xalleafdelingencheckbox.Style = MetroFramework.MetroColorStyle.Purple;
            this.xalleafdelingencheckbox.TabIndex = 28;
            this.xalleafdelingencheckbox.Text = "Toon Alle Verzoeken";
            this.xalleafdelingencheckbox.UseSelectable = true;
            this.xalleafdelingencheckbox.UseStyleColors = true;
            this.xalleafdelingencheckbox.Visible = false;
            this.xalleafdelingencheckbox.CheckedChanged += new System.EventHandler(this.xalleafdelingencheckbox_CheckedChanged);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Afdeling";
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.IsTileViewColumn = true;
            this.olvColumn2.Text = "Afdeling";
            this.olvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn2.ToolTipText = "Afdeling";
            this.olvColumn2.Width = 120;
            this.olvColumn2.WordWrap = true;
            // 
            // VerzoekForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(723, 387);
            this.Controls.Add(this.xalleafdelingencheckbox);
            this.Controls.Add(this.xVerzoeklijst);
            this.Controls.Add(this.toolStrip1);
            this.MinimumSize = new System.Drawing.Size(720, 385);
            this.Name = "VerzoekForm";
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Personeel Verzoeken";
            this.Title = "Personeel Verzoeken";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xVerzoeklijst)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripTextBox xpersoneelnamen;
        private System.Windows.Forms.ToolStripButton xKiesPersoneel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton xNieuweVerzoek;
        private Controls.CustomObjectListview xVerzoeklijst;
        private BrightIdeasSoftware.OLVColumn xColmn0;
        private BrightIdeasSoftware.OLVColumn xColumn5;
        private BrightIdeasSoftware.OLVColumn xColumn3;
        private BrightIdeasSoftware.OLVColumn xColmn2;
        private System.Windows.Forms.ToolStripButton xPersoneelSluiten;
        private BrightIdeasSoftware.OLVColumn xColumn4;
        private BrightIdeasSoftware.OLVColumn xColumn6;
        private BrightIdeasSoftware.OLVColumn xColumn7;
        private BrightIdeasSoftware.OLVColumn xColumn8;
        private BrightIdeasSoftware.OLVColumn xColumn9;
        private System.Windows.Forms.Timer xPersoneelTimer;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton xverwijderverzoek;
        private System.Windows.Forms.ToolStripTextBox xZoekTextbox;
        private System.Windows.Forms.ToolStripButton xClearZoek;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripButton xEditVerzoek;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wijzigenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem verwijderenToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private MetroFramework.Controls.MetroCheckBox xalleafdelingencheckbox;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
    }
}