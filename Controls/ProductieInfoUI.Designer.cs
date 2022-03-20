using TheArtOfDev.HtmlRenderer.WinForms;

namespace Controls
{
    partial class ProductieInfoUI
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
            this.xTabControl = new MetroFramework.Controls.MetroTabControl();
            this.xtab0 = new MetroFramework.Controls.MetroTabPage();
            this.xtab1 = new MetroFramework.Controls.MetroTabPage();
            this.xtab2 = new MetroFramework.Controls.MetroTabPage();
            this.xtab3 = new MetroFramework.Controls.MetroTabPage();
            this.xtab4 = new MetroFramework.Controls.MetroTabPage();
            this.xtab5 = new MetroFramework.Controls.MetroTabPage();
            this.xtab6 = new MetroFramework.Controls.MetroTabPage();
            this.xtab7 = new MetroFramework.Controls.MetroTabPage();
            this.xtab8 = new MetroFramework.Controls.MetroTabPage();
            this.xtab9 = new MetroFramework.Controls.MetroTabPage();
            this.xtab10 = new MetroFramework.Controls.MetroTabPage();
            this.xLogDataList = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xtab11 = new MetroFramework.Controls.MetroTabPage();
            this.productieVerbruikUI1 = new Controls.ProductieVerbruikUI();
            this.combineerUI1 = new Controls.CombineerUI();
            this.verpakkingInstructieUI1 = new Controls.VerpakkingInstructieUI();
            this.alleWerkPlekAantalHistoryUI1 = new Forms.Aantal.Controls.AlleWerkPlekAantalHistoryUI();
            this.aantalChangerUI1 = new ProductieManager.Forms.Aantal.Controls.AantalChangerUI();
            this.bijlageUI1 = new Controls.BijlageUI();
            this.xTabControl.SuspendLayout();
            this.xtab1.SuspendLayout();
            this.xtab2.SuspendLayout();
            this.xtab6.SuspendLayout();
            this.xtab9.SuspendLayout();
            this.xtab10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xLogDataList)).BeginInit();
            this.xtab11.SuspendLayout();
            this.SuspendLayout();
            // 
            // xTabControl
            // 
            this.xTabControl.Controls.Add(this.xtab0);
            this.xTabControl.Controls.Add(this.xtab1);
            this.xTabControl.Controls.Add(this.xtab2);
            this.xTabControl.Controls.Add(this.xtab3);
            this.xTabControl.Controls.Add(this.xtab4);
            this.xTabControl.Controls.Add(this.xtab5);
            this.xTabControl.Controls.Add(this.xtab6);
            this.xTabControl.Controls.Add(this.xtab7);
            this.xTabControl.Controls.Add(this.xtab8);
            this.xTabControl.Controls.Add(this.xtab9);
            this.xTabControl.Controls.Add(this.xtab10);
            this.xTabControl.Controls.Add(this.xtab11);
            this.xTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xTabControl.FontWeight = MetroFramework.MetroTabControlWeight.Regular;
            this.xTabControl.HotTrack = true;
            this.xTabControl.Location = new System.Drawing.Point(5, 5);
            this.xTabControl.Multiline = true;
            this.xTabControl.Name = "xTabControl";
            this.xTabControl.SelectedIndex = 11;
            this.xTabControl.ShowToolTips = true;
            this.xTabControl.Size = new System.Drawing.Size(921, 493);
            this.xTabControl.Style = MetroFramework.MetroColorStyle.Blue;
            this.xTabControl.TabIndex = 12;
            this.xTabControl.TextAlign = System.Drawing.ContentAlignment.TopLeft;
            this.xTabControl.UseSelectable = true;
            this.xTabControl.SelectedIndexChanged += new System.EventHandler(this.metroTabControl1_SelectedIndexChanged);
            // 
            // xtab0
            // 
            this.xtab0.HorizontalScrollbarBarColor = true;
            this.xtab0.HorizontalScrollbarHighlightOnWheel = false;
            this.xtab0.HorizontalScrollbarSize = 2;
            this.xtab0.Location = new System.Drawing.Point(4, 38);
            this.xtab0.Name = "xtab0";
            this.xtab0.Padding = new System.Windows.Forms.Padding(5);
            this.xtab0.Size = new System.Drawing.Size(913, 451);
            this.xtab0.Style = MetroFramework.MetroColorStyle.Blue;
            this.xtab0.TabIndex = 6;
            this.xtab0.Text = "Status";
            this.xtab0.ToolTipText = "Productie Status";
            this.xtab0.VerticalScrollbarBarColor = true;
            this.xtab0.VerticalScrollbarHighlightOnWheel = false;
            this.xtab0.VerticalScrollbarSize = 3;
            // 
            // xtab1
            // 
            this.xtab1.Controls.Add(this.productieVerbruikUI1);
            this.xtab1.HorizontalScrollbarBarColor = true;
            this.xtab1.HorizontalScrollbarHighlightOnWheel = false;
            this.xtab1.HorizontalScrollbarSize = 2;
            this.xtab1.Location = new System.Drawing.Point(4, 38);
            this.xtab1.Name = "xtab1";
            this.xtab1.Padding = new System.Windows.Forms.Padding(5);
            this.xtab1.Size = new System.Drawing.Size(913, 451);
            this.xtab1.TabIndex = 12;
            this.xtab1.Text = "Verbruik";
            this.xtab1.ToolTipText = "Verbruik berekenen";
            this.xtab1.VerticalScrollbarBarColor = true;
            this.xtab1.VerticalScrollbarHighlightOnWheel = false;
            this.xtab1.VerticalScrollbarSize = 3;
            // 
            // xtab2
            // 
            this.xtab2.Controls.Add(this.combineerUI1);
            this.xtab2.HorizontalScrollbarBarColor = true;
            this.xtab2.HorizontalScrollbarHighlightOnWheel = false;
            this.xtab2.HorizontalScrollbarSize = 2;
            this.xtab2.Location = new System.Drawing.Point(4, 38);
            this.xtab2.Name = "xtab2";
            this.xtab2.Padding = new System.Windows.Forms.Padding(5);
            this.xtab2.Size = new System.Drawing.Size(913, 451);
            this.xtab2.TabIndex = 10;
            this.xtab2.Text = "Combineer";
            this.xtab2.ToolTipText = "Combineer producties";
            this.xtab2.VerticalScrollbarBarColor = true;
            this.xtab2.VerticalScrollbarHighlightOnWheel = false;
            this.xtab2.VerticalScrollbarSize = 3;
            // 
            // xtab3
            // 
            this.xtab3.HorizontalScrollbarBarColor = true;
            this.xtab3.HorizontalScrollbarHighlightOnWheel = false;
            this.xtab3.HorizontalScrollbarSize = 2;
            this.xtab3.Location = new System.Drawing.Point(4, 38);
            this.xtab3.Name = "xtab3";
            this.xtab3.Padding = new System.Windows.Forms.Padding(5);
            this.xtab3.Size = new System.Drawing.Size(913, 451);
            this.xtab3.TabIndex = 1;
            this.xtab3.Text = "   Info";
            this.xtab3.ToolTipText = "Productie info";
            this.xtab3.VerticalScrollbarBarColor = true;
            this.xtab3.VerticalScrollbarHighlightOnWheel = false;
            this.xtab3.VerticalScrollbarSize = 3;
            // 
            // xtab4
            // 
            this.xtab4.HorizontalScrollbarBarColor = true;
            this.xtab4.HorizontalScrollbarHighlightOnWheel = false;
            this.xtab4.HorizontalScrollbarSize = 2;
            this.xtab4.Location = new System.Drawing.Point(4, 38);
            this.xtab4.Name = "xtab4";
            this.xtab4.Padding = new System.Windows.Forms.Padding(5);
            this.xtab4.Size = new System.Drawing.Size(913, 451);
            this.xtab4.TabIndex = 2;
            this.xtab4.Text = "Notities";
            this.xtab4.ToolTipText = "Alle genoteerde notities";
            this.xtab4.VerticalScrollbarBarColor = true;
            this.xtab4.VerticalScrollbarHighlightOnWheel = false;
            this.xtab4.VerticalScrollbarSize = 3;
            // 
            // xtab5
            // 
            this.xtab5.HorizontalScrollbarBarColor = true;
            this.xtab5.HorizontalScrollbarHighlightOnWheel = false;
            this.xtab5.HorizontalScrollbarSize = 2;
            this.xtab5.Location = new System.Drawing.Point(4, 38);
            this.xtab5.Name = "xtab5";
            this.xtab5.Padding = new System.Windows.Forms.Padding(5);
            this.xtab5.Size = new System.Drawing.Size(913, 451);
            this.xtab5.TabIndex = 7;
            this.xtab5.Text = "Datums";
            this.xtab5.ToolTipText = "Alle relevante datums";
            this.xtab5.VerticalScrollbarBarColor = true;
            this.xtab5.VerticalScrollbarHighlightOnWheel = false;
            this.xtab5.VerticalScrollbarSize = 3;
            // 
            // xtab6
            // 
            this.xtab6.AutoScroll = true;
            this.xtab6.Controls.Add(this.verpakkingInstructieUI1);
            this.xtab6.HorizontalScrollbar = true;
            this.xtab6.HorizontalScrollbarBarColor = true;
            this.xtab6.HorizontalScrollbarHighlightOnWheel = false;
            this.xtab6.HorizontalScrollbarSize = 2;
            this.xtab6.Location = new System.Drawing.Point(4, 38);
            this.xtab6.Name = "xtab6";
            this.xtab6.Padding = new System.Windows.Forms.Padding(5);
            this.xtab6.Size = new System.Drawing.Size(913, 451);
            this.xtab6.TabIndex = 5;
            this.xtab6.Text = "Verpakking";
            this.xtab6.ToolTipText = "VerpakkingsInstructies";
            this.xtab6.VerticalScrollbar = true;
            this.xtab6.VerticalScrollbarBarColor = true;
            this.xtab6.VerticalScrollbarHighlightOnWheel = false;
            this.xtab6.VerticalScrollbarSize = 3;
            // 
            // xtab7
            // 
            this.xtab7.HorizontalScrollbarBarColor = true;
            this.xtab7.HorizontalScrollbarHighlightOnWheel = false;
            this.xtab7.HorizontalScrollbarSize = 2;
            this.xtab7.Location = new System.Drawing.Point(4, 38);
            this.xtab7.Name = "xtab7";
            this.xtab7.Padding = new System.Windows.Forms.Padding(5);
            this.xtab7.Size = new System.Drawing.Size(913, 451);
            this.xtab7.TabIndex = 8;
            this.xtab7.Text = "Materialen";
            this.xtab7.ToolTipText = "Benodigde Materialen";
            this.xtab7.VerticalScrollbarBarColor = true;
            this.xtab7.VerticalScrollbarHighlightOnWheel = false;
            this.xtab7.VerticalScrollbarSize = 3;
            // 
            // xtab8
            // 
            this.xtab8.HorizontalScrollbarBarColor = true;
            this.xtab8.HorizontalScrollbarHighlightOnWheel = false;
            this.xtab8.HorizontalScrollbarSize = 2;
            this.xtab8.Location = new System.Drawing.Point(4, 38);
            this.xtab8.Name = "xtab8";
            this.xtab8.Padding = new System.Windows.Forms.Padding(5);
            this.xtab8.Size = new System.Drawing.Size(913, 451);
            this.xtab8.TabIndex = 9;
            this.xtab8.Text = "WerkPlaatsen";
            this.xtab8.ToolTipText = "Alle werkplaatsen";
            this.xtab8.VerticalScrollbarBarColor = true;
            this.xtab8.VerticalScrollbarHighlightOnWheel = false;
            this.xtab8.VerticalScrollbarSize = 3;
            // 
            // xtab9
            // 
            this.xtab9.Controls.Add(this.alleWerkPlekAantalHistoryUI1);
            this.xtab9.HorizontalScrollbarBarColor = true;
            this.xtab9.HorizontalScrollbarHighlightOnWheel = false;
            this.xtab9.HorizontalScrollbarSize = 2;
            this.xtab9.Location = new System.Drawing.Point(4, 38);
            this.xtab9.Name = "xtab9";
            this.xtab9.Padding = new System.Windows.Forms.Padding(5);
            this.xtab9.Size = new System.Drawing.Size(913, 451);
            this.xtab9.TabIndex = 11;
            this.xtab9.Text = "Aantallen";
            this.xtab9.ToolTipText = "Aantallen geschiedenis";
            this.xtab9.VerticalScrollbarBarColor = true;
            this.xtab9.VerticalScrollbarHighlightOnWheel = false;
            this.xtab9.VerticalScrollbarSize = 3;
            // 
            // xtab10
            // 
            this.xtab10.Controls.Add(this.xLogDataList);
            this.xtab10.HorizontalScrollbarBarColor = true;
            this.xtab10.HorizontalScrollbarHighlightOnWheel = false;
            this.xtab10.HorizontalScrollbarSize = 2;
            this.xtab10.Location = new System.Drawing.Point(4, 38);
            this.xtab10.Name = "xtab10";
            this.xtab10.Padding = new System.Windows.Forms.Padding(5, 5, 0, 0);
            this.xtab10.Size = new System.Drawing.Size(913, 451);
            this.xtab10.TabIndex = 13;
            this.xtab10.Text = "Productie Log";
            this.xtab10.VerticalScrollbarBarColor = true;
            this.xtab10.VerticalScrollbarHighlightOnWheel = false;
            this.xtab10.VerticalScrollbarSize = 3;
            // 
            // xLogDataList
            // 
            this.xLogDataList.AllColumns.Add(this.olvColumn1);
            this.xLogDataList.AllColumns.Add(this.olvColumn3);
            this.xLogDataList.AllColumns.Add(this.olvColumn2);
            this.xLogDataList.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.xLogDataList.CellEditUseWholeCell = false;
            this.xLogDataList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn3,
            this.olvColumn2});
            this.xLogDataList.Cursor = System.Windows.Forms.Cursors.Default;
            this.xLogDataList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xLogDataList.FullRowSelect = true;
            this.xLogDataList.GridLines = true;
            this.xLogDataList.HideSelection = false;
            this.xLogDataList.LargeImageList = this.imageList1;
            this.xLogDataList.Location = new System.Drawing.Point(5, 5);
            this.xLogDataList.Name = "xLogDataList";
            this.xLogDataList.ShowFilterMenuOnRightClick = false;
            this.xLogDataList.ShowGroups = false;
            this.xLogDataList.ShowItemToolTips = true;
            this.xLogDataList.Size = new System.Drawing.Size(908, 446);
            this.xLogDataList.SmallImageList = this.imageList1;
            this.xLogDataList.TabIndex = 2;
            this.xLogDataList.TintSortColumn = true;
            this.xLogDataList.UseAlternatingBackColors = true;
            this.xLogDataList.UseCompatibleStateImageBehavior = false;
            this.xLogDataList.UseExplorerTheme = true;
            this.xLogDataList.UseHotControls = false;
            this.xLogDataList.UseHotItem = true;
            this.xLogDataList.UseTranslucentHotItem = true;
            this.xLogDataList.UseTranslucentSelection = true;
            this.xLogDataList.View = System.Windows.Forms.View.Details;
            this.xLogDataList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xLogDataList_KeyDown);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Added";
            this.olvColumn1.Groupable = false;
            this.olvColumn1.Text = "Log Datum";
            this.olvColumn1.Width = 175;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Username";
            this.olvColumn3.Text = "Gebruiker";
            this.olvColumn3.Width = 120;
            this.olvColumn3.WordWrap = true;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Message";
            this.olvColumn2.Groupable = false;
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Omschrijving";
            this.olvColumn2.Width = 500;
            this.olvColumn2.WordWrap = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xtab11
            // 
            this.xtab11.Controls.Add(this.bijlageUI1);
            this.xtab11.HorizontalScrollbarBarColor = true;
            this.xtab11.HorizontalScrollbarHighlightOnWheel = false;
            this.xtab11.HorizontalScrollbarSize = 3;
            this.xtab11.Location = new System.Drawing.Point(4, 38);
            this.xtab11.Name = "xtab11";
            this.xtab11.Padding = new System.Windows.Forms.Padding(5);
            this.xtab11.Size = new System.Drawing.Size(913, 451);
            this.xtab11.TabIndex = 14;
            this.xtab11.Text = "Bijlages";
            this.xtab11.VerticalScrollbarBarColor = true;
            this.xtab11.VerticalScrollbarHighlightOnWheel = false;
            this.xtab11.VerticalScrollbarSize = 5;
            // 
            // productieVerbruikUI1
            // 
            this.productieVerbruikUI1.BackColor = System.Drawing.Color.White;
            this.productieVerbruikUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieVerbruikUI1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieVerbruikUI1.Location = new System.Drawing.Point(5, 5);
            this.productieVerbruikUI1.MaxUitgangsLengte = new decimal(new int[] {
            7500,
            0,
            0,
            0});
            this.productieVerbruikUI1.Name = "productieVerbruikUI1";
            this.productieVerbruikUI1.OpdrukkerArtikel = null;
            this.productieVerbruikUI1.Padding = new System.Windows.Forms.Padding(5);
            this.productieVerbruikUI1.RestStuk = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.productieVerbruikUI1.ShowMateriaalSelector = false;
            this.productieVerbruikUI1.ShowOpdrukkerArtikelNr = false;
            this.productieVerbruikUI1.ShowOpslaan = false;
            this.productieVerbruikUI1.ShowPerUur = false;
            this.productieVerbruikUI1.ShowSluiten = false;
            this.productieVerbruikUI1.Size = new System.Drawing.Size(903, 441);
            this.productieVerbruikUI1.TabIndex = 2;
            this.productieVerbruikUI1.Title = "Verbruik Berekenen";
            // 
            // combineerUI1
            // 
            this.combineerUI1.BackColor = System.Drawing.Color.White;
            this.combineerUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.combineerUI1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.combineerUI1.Location = new System.Drawing.Point(5, 5);
            this.combineerUI1.Name = "combineerUI1";
            this.combineerUI1.Padding = new System.Windows.Forms.Padding(5);
            this.combineerUI1.Size = new System.Drawing.Size(903, 441);
            this.combineerUI1.TabIndex = 2;
            // 
            // verpakkingInstructieUI1
            // 
            this.verpakkingInstructieUI1.AllowEditMode = false;
            this.verpakkingInstructieUI1.AutoScroll = true;
            this.verpakkingInstructieUI1.BackColor = System.Drawing.Color.White;
            this.verpakkingInstructieUI1.BodyColor = System.Drawing.Color.Empty;
            this.verpakkingInstructieUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verpakkingInstructieUI1.IsEditmode = false;
            this.verpakkingInstructieUI1.Location = new System.Drawing.Point(5, 5);
            this.verpakkingInstructieUI1.Name = "verpakkingInstructieUI1";
            this.verpakkingInstructieUI1.Padding = new System.Windows.Forms.Padding(5);
            this.verpakkingInstructieUI1.Productie = null;
            this.verpakkingInstructieUI1.Size = new System.Drawing.Size(903, 441);
            this.verpakkingInstructieUI1.TabIndex = 2;
            this.verpakkingInstructieUI1.TextColor = System.Drawing.Color.Empty;
            this.verpakkingInstructieUI1.Title = null;
            // 
            // alleWerkPlekAantalHistoryUI1
            // 
            this.alleWerkPlekAantalHistoryUI1.BackColor = System.Drawing.Color.White;
            this.alleWerkPlekAantalHistoryUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alleWerkPlekAantalHistoryUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alleWerkPlekAantalHistoryUI1.Location = new System.Drawing.Point(5, 5);
            this.alleWerkPlekAantalHistoryUI1.Margin = new System.Windows.Forms.Padding(4);
            this.alleWerkPlekAantalHistoryUI1.Name = "alleWerkPlekAantalHistoryUI1";
            this.alleWerkPlekAantalHistoryUI1.Padding = new System.Windows.Forms.Padding(10);
            this.alleWerkPlekAantalHistoryUI1.Size = new System.Drawing.Size(903, 441);
            this.alleWerkPlekAantalHistoryUI1.TabIndex = 2;
            // 
            // aantalChangerUI1
            // 
            this.aantalChangerUI1.BackColor = System.Drawing.Color.Transparent;
            this.aantalChangerUI1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.aantalChangerUI1.Location = new System.Drawing.Point(5, 498);
            this.aantalChangerUI1.Margin = new System.Windows.Forms.Padding(4);
            this.aantalChangerUI1.MinimumSize = new System.Drawing.Size(582, 100);
            this.aantalChangerUI1.Name = "aantalChangerUI1";
            this.aantalChangerUI1.Size = new System.Drawing.Size(921, 100);
            this.aantalChangerUI1.TabIndex = 0;
            // 
            // bijlageUI1
            // 
            this.bijlageUI1.BackColor = System.Drawing.Color.White;
            this.bijlageUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.bijlageUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bijlageUI1.Location = new System.Drawing.Point(5, 5);
            this.bijlageUI1.Name = "bijlageUI1";
            this.bijlageUI1.Size = new System.Drawing.Size(903, 441);
            this.bijlageUI1.TabIndex = 2;
            // 
            // ProductieInfoUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xTabControl);
            this.Controls.Add(this.aantalChangerUI1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProductieInfoUI";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(931, 603);
            this.xTabControl.ResumeLayout(false);
            this.xtab1.ResumeLayout(false);
            this.xtab2.ResumeLayout(false);
            this.xtab6.ResumeLayout(false);
            this.xtab9.ResumeLayout(false);
            this.xtab10.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xLogDataList)).EndInit();
            this.xtab11.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTabControl xTabControl;
        private MetroFramework.Controls.MetroTabPage xtab3;
        private MetroFramework.Controls.MetroTabPage xtab4;
        private MetroFramework.Controls.MetroTabPage xtab6;
        private MetroFramework.Controls.MetroTabPage xtab0;
        private MetroFramework.Controls.MetroTabPage xtab5;
        private MetroFramework.Controls.MetroTabPage xtab7;
        private MetroFramework.Controls.MetroTabPage xtab8;
        private VerpakkingInstructieUI verpakkingInstructieUI1;
        private ProductieManager.Forms.Aantal.Controls.AantalChangerUI aantalChangerUI1;
        private MetroFramework.Controls.MetroTabPage xtab2;
        private CombineerUI combineerUI1;
        private MetroFramework.Controls.MetroTabPage xtab9;
        private Forms.Aantal.Controls.AlleWerkPlekAantalHistoryUI alleWerkPlekAantalHistoryUI1;
        private MetroFramework.Controls.MetroTabPage xtab1;
        private ProductieVerbruikUI productieVerbruikUI1;
        private MetroFramework.Controls.MetroTabPage xtab10;
        private BrightIdeasSoftware.ObjectListView xLogDataList;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.ImageList imageList1;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private MetroFramework.Controls.MetroTabPage xtab11;
        private BijlageUI bijlageUI1;
    }
}
