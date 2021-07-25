
namespace ProductieManager.Forms
{
    partial class FilterEditor
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
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.xfilterlijst = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.metroPanel2 = new MetroFramework.Controls.MetroPanel();
            this.xfilternaam = new MetroFramework.Controls.MetroTextBox();
            this.xwijzigfilter = new MetroFramework.Controls.MetroButton();
            this.xdeletefilter = new MetroFramework.Controls.MetroButton();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.metroPanel6 = new MetroFramework.Controls.MetroPanel();
            this.xopslaan = new MetroFramework.Controls.MetroButton();
            this.xannuleren = new MetroFramework.Controls.MetroButton();
            this.filterEntryEditorUI1 = new Controls.FilterEntryEditorUI();
            this.metroPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xfilterlijst)).BeginInit();
            this.metroPanel2.SuspendLayout();
            this.metroPanel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroPanel1
            // 
            this.metroPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.metroPanel1.Controls.Add(this.xfilterlijst);
            this.metroPanel1.Controls.Add(this.metroPanel2);
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 12;
            this.metroPanel1.Location = new System.Drawing.Point(7, 60);
            this.metroPanel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(237, 374);
            this.metroPanel1.TabIndex = 1;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 11;
            // 
            // xfilterlijst
            // 
            this.xfilterlijst.AllColumns.Add(this.olvColumn1);
            this.xfilterlijst.CellEditUseWholeCell = false;
            this.xfilterlijst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.xfilterlijst.Cursor = System.Windows.Forms.Cursors.Default;
            this.xfilterlijst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xfilterlijst.FullRowSelect = true;
            this.xfilterlijst.HideSelection = false;
            this.xfilterlijst.Location = new System.Drawing.Point(0, 28);
            this.xfilterlijst.MultiSelect = false;
            this.xfilterlijst.Name = "xfilterlijst";
            this.xfilterlijst.ShowGroups = false;
            this.xfilterlijst.ShowItemToolTips = true;
            this.xfilterlijst.Size = new System.Drawing.Size(237, 346);
            this.xfilterlijst.TabIndex = 0;
            this.xfilterlijst.UseCompatibleStateImageBehavior = false;
            this.xfilterlijst.UseExplorerTheme = true;
            this.xfilterlijst.UseHotItem = true;
            this.xfilterlijst.UseTranslucentHotItem = true;
            this.xfilterlijst.UseTranslucentSelection = true;
            this.xfilterlijst.View = System.Windows.Forms.View.Details;
            this.xfilterlijst.SelectedIndexChanged += new System.EventHandler(this.xfilterlijst_SelectedIndexChanged);
            this.xfilterlijst.DoubleClick += new System.EventHandler(this.xfilterlijst_DoubleClick);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.Groupable = false;
            this.olvColumn1.HeaderFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Filters";
            this.olvColumn1.ToolTipText = "Opgeslagen filter lijst";
            this.olvColumn1.WordWrap = true;
            // 
            // metroPanel2
            // 
            this.metroPanel2.Controls.Add(this.xfilternaam);
            this.metroPanel2.Controls.Add(this.xwijzigfilter);
            this.metroPanel2.Controls.Add(this.xdeletefilter);
            this.metroPanel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel2.HorizontalScrollbarBarColor = true;
            this.metroPanel2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel2.HorizontalScrollbarSize = 12;
            this.metroPanel2.Location = new System.Drawing.Point(0, 0);
            this.metroPanel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroPanel2.Name = "metroPanel2";
            this.metroPanel2.Size = new System.Drawing.Size(237, 28);
            this.metroPanel2.TabIndex = 3;
            this.metroPanel2.VerticalScrollbarBarColor = true;
            this.metroPanel2.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel2.VerticalScrollbarSize = 11;
            // 
            // xfilternaam
            // 
            // 
            // 
            // 
            this.xfilternaam.CustomButton.BackColor = System.Drawing.Color.Transparent;
            this.xfilternaam.CustomButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.xfilternaam.CustomButton.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xfilternaam.CustomButton.Location = new System.Drawing.Point(157, 2);
            this.xfilternaam.CustomButton.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xfilternaam.CustomButton.Name = "";
            this.xfilternaam.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.xfilternaam.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xfilternaam.CustomButton.TabIndex = 1;
            this.xfilternaam.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xfilternaam.CustomButton.UseSelectable = true;
            this.xfilternaam.CustomButton.UseVisualStyleBackColor = false;
            this.xfilternaam.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xfilternaam.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xfilternaam.Lines = new string[] {
        "Filter naam..."};
            this.xfilternaam.Location = new System.Drawing.Point(0, 0);
            this.xfilternaam.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xfilternaam.MaxLength = 32767;
            this.xfilternaam.Name = "xfilternaam";
            this.xfilternaam.PasswordChar = '\0';
            this.xfilternaam.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xfilternaam.SelectedText = "";
            this.xfilternaam.SelectionLength = 0;
            this.xfilternaam.SelectionStart = 0;
            this.xfilternaam.ShortcutsEnabled = true;
            this.xfilternaam.ShowButton = true;
            this.xfilternaam.Size = new System.Drawing.Size(183, 28);
            this.xfilternaam.TabIndex = 5;
            this.xfilternaam.Text = "Filter naam...";
            this.xfilternaam.UseSelectable = true;
            this.xfilternaam.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xfilternaam.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xfilternaam.Enter += new System.EventHandler(this.xfilternaam_Enter);
            this.xfilternaam.Leave += new System.EventHandler(this.xfilternaam_Leave);
            // 
            // xwijzigfilter
            // 
            this.xwijzigfilter.BackgroundImage = global::ProductieManager.Properties.Resources.edit__52382;
            this.xwijzigfilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.xwijzigfilter.Dock = System.Windows.Forms.DockStyle.Right;
            this.xwijzigfilter.Enabled = false;
            this.xwijzigfilter.Location = new System.Drawing.Point(183, 0);
            this.xwijzigfilter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xwijzigfilter.Name = "xwijzigfilter";
            this.xwijzigfilter.Size = new System.Drawing.Size(27, 28);
            this.xwijzigfilter.TabIndex = 4;
            this.metroToolTip1.SetToolTip(this.xwijzigfilter, "Wijzig filter naam");
            this.xwijzigfilter.UseSelectable = true;
            this.xwijzigfilter.Click += new System.EventHandler(this.xwijzigfilter_Click);
            // 
            // xdeletefilter
            // 
            this.xdeletefilter.BackgroundImage = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdeletefilter.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.xdeletefilter.Dock = System.Windows.Forms.DockStyle.Right;
            this.xdeletefilter.Enabled = false;
            this.xdeletefilter.Location = new System.Drawing.Point(210, 0);
            this.xdeletefilter.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xdeletefilter.Name = "xdeletefilter";
            this.xdeletefilter.Size = new System.Drawing.Size(27, 28);
            this.xdeletefilter.TabIndex = 3;
            this.metroToolTip1.SetToolTip(this.xdeletefilter, "Verwijder geselecteerde filters");
            this.xdeletefilter.UseSelectable = true;
            this.xdeletefilter.Click += new System.EventHandler(this.xdeletefilter_Click);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // metroPanel6
            // 
            this.metroPanel6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroPanel6.Controls.Add(this.xopslaan);
            this.metroPanel6.Controls.Add(this.xannuleren);
            this.metroPanel6.HorizontalScrollbarBarColor = true;
            this.metroPanel6.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel6.HorizontalScrollbarSize = 10;
            this.metroPanel6.Location = new System.Drawing.Point(7, 441);
            this.metroPanel6.Name = "metroPanel6";
            this.metroPanel6.Size = new System.Drawing.Size(880, 39);
            this.metroPanel6.TabIndex = 3;
            this.metroPanel6.VerticalScrollbarBarColor = true;
            this.metroPanel6.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel6.VerticalScrollbarSize = 10;
            // 
            // xopslaan
            // 
            this.xopslaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xopslaan.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.xopslaan.Location = new System.Drawing.Point(650, 4);
            this.xopslaan.Name = "xopslaan";
            this.xopslaan.Size = new System.Drawing.Size(110, 33);
            this.xopslaan.TabIndex = 3;
            this.xopslaan.Text = "Opslaan";
            this.xopslaan.UseSelectable = true;
            this.xopslaan.Click += new System.EventHandler(this.xopslaan_Click);
            // 
            // xannuleren
            // 
            this.xannuleren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xannuleren.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.xannuleren.Location = new System.Drawing.Point(766, 4);
            this.xannuleren.Name = "xannuleren";
            this.xannuleren.Size = new System.Drawing.Size(110, 33);
            this.xannuleren.TabIndex = 2;
            this.xannuleren.Text = "Annuleren";
            this.xannuleren.UseSelectable = true;
            this.xannuleren.Click += new System.EventHandler(this.xannuleren_Click);
            // 
            // filterEntryEditorUI1
            // 
            this.filterEntryEditorUI1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.filterEntryEditorUI1.BackColor = System.Drawing.Color.White;
            this.filterEntryEditorUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterEntryEditorUI1.Location = new System.Drawing.Point(252, 60);
            this.filterEntryEditorUI1.Margin = new System.Windows.Forms.Padding(5);
            this.filterEntryEditorUI1.Name = "filterEntryEditorUI1";
            this.filterEntryEditorUI1.Padding = new System.Windows.Forms.Padding(5);
            this.filterEntryEditorUI1.Size = new System.Drawing.Size(631, 373);
            this.filterEntryEditorUI1.TabIndex = 4;
            // 
            // FilterEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(895, 495);
            this.Controls.Add(this.filterEntryEditorUI1);
            this.Controls.Add(this.metroPanel6);
            this.Controls.Add(this.metroPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MinimumSize = new System.Drawing.Size(895, 495);
            this.Name = "FilterEditor";
            this.Padding = new System.Windows.Forms.Padding(26, 92, 26, 31);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Beheer Filters";
            this.metroPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xfilterlijst)).EndInit();
            this.metroPanel2.ResumeLayout(false);
            this.metroPanel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroPanel metroPanel2;
        private MetroFramework.Controls.MetroTextBox xfilternaam;
        private MetroFramework.Controls.MetroButton xwijzigfilter;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private MetroFramework.Controls.MetroButton xdeletefilter;
        private MetroFramework.Controls.MetroPanel metroPanel6;
        private MetroFramework.Controls.MetroButton xopslaan;
        private MetroFramework.Controls.MetroButton xannuleren;
        private BrightIdeasSoftware.ObjectListView xfilterlijst;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private Controls.FilterEntryEditorUI filterEntryEditorUI1;
    }
}