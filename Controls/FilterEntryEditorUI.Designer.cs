
namespace Controls
{
    partial class FilterEntryEditorUI
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.metroPanel3 = new MetroFramework.Controls.MetroPanel();
            this.xcriterialijst = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.metroPanel5 = new MetroFramework.Controls.MetroPanel();
            this.xregeeldown = new MetroFramework.Controls.MetroButton();
            this.xregelup = new MetroFramework.Controls.MetroButton();
            this.xdeletefilterregel = new MetroFramework.Controls.MetroButton();
            this.xwijzigfilterregel = new MetroFramework.Controls.MetroButton();
            this.xcriteriahtml = new HtmlRenderer.HtmlPanel();
            this.xvariablelijst = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.metroPanel4 = new MetroFramework.Controls.MetroPanel();
            this.xaddcriteria = new MetroFramework.Controls.MetroButton();
            this.panel1.SuspendLayout();
            this.metroPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xcriterialijst)).BeginInit();
            this.metroPanel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xvariablelijst)).BeginInit();
            this.metroPanel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.metroPanel3);
            this.panel1.Controls.Add(this.xvariablelijst);
            this.panel1.Controls.Add(this.metroPanel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(688, 384);
            this.panel1.TabIndex = 0;
            // 
            // metroPanel3
            // 
            this.metroPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.metroPanel3.Controls.Add(this.xcriterialijst);
            this.metroPanel3.Controls.Add(this.metroPanel5);
            this.metroPanel3.Controls.Add(this.xcriteriahtml);
            this.metroPanel3.HorizontalScrollbarBarColor = true;
            this.metroPanel3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel3.HorizontalScrollbarSize = 12;
            this.metroPanel3.Location = new System.Drawing.Point(292, 4);
            this.metroPanel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroPanel3.Name = "metroPanel3";
            this.metroPanel3.Size = new System.Drawing.Size(393, 376);
            this.metroPanel3.TabIndex = 5;
            this.metroPanel3.VerticalScrollbarBarColor = true;
            this.metroPanel3.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel3.VerticalScrollbarSize = 11;
            // 
            // xcriterialijst
            // 
            this.xcriterialijst.AllColumns.Add(this.olvColumn3);
            this.xcriterialijst.CellEditUseWholeCell = false;
            this.xcriterialijst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn3});
            this.xcriterialijst.Cursor = System.Windows.Forms.Cursors.Default;
            this.xcriterialijst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xcriterialijst.FullRowSelect = true;
            this.xcriterialijst.HideSelection = false;
            this.xcriterialijst.Location = new System.Drawing.Point(0, 0);
            this.xcriterialijst.MultiSelect = false;
            this.xcriterialijst.Name = "xcriterialijst";
            this.xcriterialijst.ShowGroups = false;
            this.xcriterialijst.ShowItemToolTips = true;
            this.xcriterialijst.Size = new System.Drawing.Size(350, 244);
            this.xcriterialijst.TabIndex = 6;
            this.xcriterialijst.UseCompatibleStateImageBehavior = false;
            this.xcriterialijst.UseExplorerTheme = true;
            this.xcriterialijst.UseHotItem = true;
            this.xcriterialijst.UseTranslucentHotItem = true;
            this.xcriterialijst.UseTranslucentSelection = true;
            this.xcriterialijst.View = System.Windows.Forms.View.Details;
            this.xcriterialijst.SelectedIndexChanged += new System.EventHandler(this.xcriterialijst_SelectedIndexChanged);
            this.xcriterialijst.DoubleClick += new System.EventHandler(this.xcriterialijst_DoubleClick);
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Criteria";
            this.olvColumn3.FillsFreeSpace = true;
            this.olvColumn3.Groupable = false;
            this.olvColumn3.HeaderFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.Text = "Criteria\'s";
            this.olvColumn3.ToolTipText = "Filter criteria\'s";
            this.olvColumn3.Width = 81;
            this.olvColumn3.WordWrap = true;
            // 
            // metroPanel5
            // 
            this.metroPanel5.Controls.Add(this.xregeeldown);
            this.metroPanel5.Controls.Add(this.xregelup);
            this.metroPanel5.Controls.Add(this.xdeletefilterregel);
            this.metroPanel5.Controls.Add(this.xwijzigfilterregel);
            this.metroPanel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.metroPanel5.HorizontalScrollbarBarColor = true;
            this.metroPanel5.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel5.HorizontalScrollbarSize = 12;
            this.metroPanel5.Location = new System.Drawing.Point(350, 0);
            this.metroPanel5.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroPanel5.Name = "metroPanel5";
            this.metroPanel5.Size = new System.Drawing.Size(43, 244);
            this.metroPanel5.TabIndex = 5;
            this.metroPanel5.VerticalScrollbarBarColor = true;
            this.metroPanel5.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel5.VerticalScrollbarSize = 11;
            // 
            // xregeeldown
            // 
            this.xregeeldown.BackgroundImage = global::ProductieManager.Properties.Resources.arrow_down_16740_32x32;
            this.xregeeldown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.xregeeldown.Enabled = false;
            this.xregeeldown.Location = new System.Drawing.Point(6, 84);
            this.xregeeldown.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xregeeldown.Name = "xregeeldown";
            this.xregeeldown.Size = new System.Drawing.Size(32, 32);
            this.xregeeldown.TabIndex = 8;
            this.xregeeldown.UseSelectable = true;
            this.xregeeldown.Click += new System.EventHandler(this.xregeeldown_Click);
            // 
            // xregelup
            // 
            this.xregelup.BackgroundImage = global::ProductieManager.Properties.Resources.arrow_up_16741_32x32;
            this.xregelup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.xregelup.Enabled = false;
            this.xregelup.Location = new System.Drawing.Point(6, 44);
            this.xregelup.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xregelup.Name = "xregelup";
            this.xregelup.Size = new System.Drawing.Size(32, 32);
            this.xregelup.TabIndex = 7;
            this.xregelup.UseSelectable = true;
            this.xregelup.Click += new System.EventHandler(this.xregelup_Click);
            // 
            // xdeletefilterregel
            // 
            this.xdeletefilterregel.BackgroundImage = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdeletefilterregel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.xdeletefilterregel.Enabled = false;
            this.xdeletefilterregel.Location = new System.Drawing.Point(6, 124);
            this.xdeletefilterregel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xdeletefilterregel.Name = "xdeletefilterregel";
            this.xdeletefilterregel.Size = new System.Drawing.Size(32, 32);
            this.xdeletefilterregel.TabIndex = 6;
            this.xdeletefilterregel.UseSelectable = true;
            this.xdeletefilterregel.Click += new System.EventHandler(this.xdeletefilterregel_Click);
            // 
            // xwijzigfilterregel
            // 
            this.xwijzigfilterregel.BackgroundImage = global::ProductieManager.Properties.Resources.edit__52382;
            this.xwijzigfilterregel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.xwijzigfilterregel.Enabled = false;
            this.xwijzigfilterregel.Location = new System.Drawing.Point(6, 4);
            this.xwijzigfilterregel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xwijzigfilterregel.Name = "xwijzigfilterregel";
            this.xwijzigfilterregel.Size = new System.Drawing.Size(32, 32);
            this.xwijzigfilterregel.TabIndex = 5;
            this.xwijzigfilterregel.UseSelectable = true;
            this.xwijzigfilterregel.Click += new System.EventHandler(this.xwijzigfilterregel_Click);
            // 
            // xcriteriahtml
            // 
            this.xcriteriahtml.AutoScroll = true;
            this.xcriteriahtml.BackColor = System.Drawing.SystemColors.Window;
            this.xcriteriahtml.BaseStylesheet = null;
            this.xcriteriahtml.Cursor = System.Windows.Forms.Cursors.Default;
            this.xcriteriahtml.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xcriteriahtml.Location = new System.Drawing.Point(0, 244);
            this.xcriteriahtml.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xcriteriahtml.Name = "xcriteriahtml";
            this.xcriteriahtml.Size = new System.Drawing.Size(393, 132);
            this.xcriteriahtml.TabIndex = 4;
            this.xcriteriahtml.Text = null;
            // 
            // xvariablelijst
            // 
            this.xvariablelijst.AllColumns.Add(this.olvColumn2);
            this.xvariablelijst.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.xvariablelijst.CellEditUseWholeCell = false;
            this.xvariablelijst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2});
            this.xvariablelijst.Cursor = System.Windows.Forms.Cursors.Default;
            this.xvariablelijst.FullRowSelect = true;
            this.xvariablelijst.HideSelection = false;
            this.xvariablelijst.Location = new System.Drawing.Point(3, 3);
            this.xvariablelijst.MultiSelect = false;
            this.xvariablelijst.Name = "xvariablelijst";
            this.xvariablelijst.ShowGroups = false;
            this.xvariablelijst.ShowItemToolTips = true;
            this.xvariablelijst.Size = new System.Drawing.Size(234, 377);
            this.xvariablelijst.TabIndex = 4;
            this.xvariablelijst.UseCompatibleStateImageBehavior = false;
            this.xvariablelijst.UseExplorerTheme = true;
            this.xvariablelijst.UseHotItem = true;
            this.xvariablelijst.UseTranslucentHotItem = true;
            this.xvariablelijst.UseTranslucentSelection = true;
            this.xvariablelijst.View = System.Windows.Forms.View.Details;
            this.xvariablelijst.SelectedIndexChanged += new System.EventHandler(this.xvariablelijst_SelectedIndexChanged);
            this.xvariablelijst.DoubleClick += new System.EventHandler(this.xvariablelijst_DoubleClick);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Name";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Groupable = false;
            this.olvColumn2.HeaderFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Variabelen";
            this.olvColumn2.ToolTipText = "Bewerking varable lijst";
            this.olvColumn2.Width = 102;
            this.olvColumn2.WordWrap = true;
            // 
            // metroPanel4
            // 
            this.metroPanel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.metroPanel4.Controls.Add(this.xaddcriteria);
            this.metroPanel4.HorizontalScrollbarBarColor = true;
            this.metroPanel4.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel4.HorizontalScrollbarSize = 12;
            this.metroPanel4.Location = new System.Drawing.Point(243, 4);
            this.metroPanel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.metroPanel4.Name = "metroPanel4";
            this.metroPanel4.Size = new System.Drawing.Size(43, 376);
            this.metroPanel4.TabIndex = 6;
            this.metroPanel4.VerticalScrollbarBarColor = true;
            this.metroPanel4.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel4.VerticalScrollbarSize = 11;
            // 
            // xaddcriteria
            // 
            this.xaddcriteria.BackgroundImage = global::ProductieManager.Properties.Resources.arrow_right_16742_32x32;
            this.xaddcriteria.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.xaddcriteria.Enabled = false;
            this.xaddcriteria.Location = new System.Drawing.Point(5, 4);
            this.xaddcriteria.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xaddcriteria.Name = "xaddcriteria";
            this.xaddcriteria.Size = new System.Drawing.Size(32, 32);
            this.xaddcriteria.TabIndex = 5;
            this.xaddcriteria.UseSelectable = true;
            this.xaddcriteria.Click += new System.EventHandler(this.xaddcriteria_Click);
            // 
            // FilterEntryEditorUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "FilterEntryEditorUI";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(698, 394);
            this.panel1.ResumeLayout(false);
            this.metroPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xcriterialijst)).EndInit();
            this.metroPanel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xvariablelijst)).EndInit();
            this.metroPanel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroPanel metroPanel3;
        private BrightIdeasSoftware.ObjectListView xcriterialijst;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private MetroFramework.Controls.MetroPanel metroPanel5;
        private MetroFramework.Controls.MetroButton xregeeldown;
        private MetroFramework.Controls.MetroButton xregelup;
        private MetroFramework.Controls.MetroButton xdeletefilterregel;
        private MetroFramework.Controls.MetroButton xwijzigfilterregel;
        private HtmlRenderer.HtmlPanel xcriteriahtml;
        private BrightIdeasSoftware.ObjectListView xvariablelijst;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private MetroFramework.Controls.MetroPanel metroPanel4;
        private MetroFramework.Controls.MetroButton xaddcriteria;
    }
}
