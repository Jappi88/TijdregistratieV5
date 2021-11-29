
namespace Forms
{
    partial class LijstWeergaveForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.xadd = new System.Windows.Forms.Button();
            this.xremove = new System.Windows.Forms.Button();
            this.xviewtext = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xOpslaan = new System.Windows.Forms.Button();
            this.xlistview = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xAddPanel = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xlistview)).BeginInit();
            this.xAddPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xAddPanel);
            this.panel1.Controls.Add(this.xremove);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(849, 43);
            this.panel1.TabIndex = 0;
            // 
            // xadd
            // 
            this.xadd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xadd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xadd.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xadd.Location = new System.Drawing.Point(742, 3);
            this.xadd.Name = "xadd";
            this.xadd.Size = new System.Drawing.Size(42, 35);
            this.xadd.TabIndex = 3;
            this.xadd.UseVisualStyleBackColor = true;
            this.xadd.Click += new System.EventHandler(this.xadd_Click);
            // 
            // xremove
            // 
            this.xremove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xremove.Enabled = false;
            this.xremove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xremove.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xremove.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xremove.Location = new System.Drawing.Point(796, 6);
            this.xremove.Name = "xremove";
            this.xremove.Size = new System.Drawing.Size(41, 35);
            this.xremove.TabIndex = 2;
            this.xremove.UseVisualStyleBackColor = true;
            this.xremove.Click += new System.EventHandler(this.xremove_Click);
            // 
            // xviewtext
            // 
            this.xviewtext.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xviewtext.Location = new System.Drawing.Point(3, 6);
            this.xviewtext.Name = "xviewtext";
            this.xviewtext.Size = new System.Drawing.Size(733, 29);
            this.xviewtext.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xsluiten);
            this.panel2.Controls.Add(this.xOpslaan);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 492);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(849, 43);
            this.panel2.TabIndex = 1;
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.ForeColor = System.Drawing.Color.Black;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(718, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(119, 38);
            this.xsluiten.TabIndex = 6;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xOpslaan
            // 
            this.xOpslaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xOpslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOpslaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOpslaan.ForeColor = System.Drawing.Color.Black;
            this.xOpslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xOpslaan.Location = new System.Drawing.Point(593, 2);
            this.xOpslaan.Name = "xOpslaan";
            this.xOpslaan.Size = new System.Drawing.Size(119, 38);
            this.xOpslaan.TabIndex = 7;
            this.xOpslaan.Text = "Opslaan";
            this.xOpslaan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xOpslaan.UseVisualStyleBackColor = true;
            this.xOpslaan.Click += new System.EventHandler(this.xOpslaan_Click);
            // 
            // xlistview
            // 
            this.xlistview.AllColumns.Add(this.olvColumn1);
            this.xlistview.CellEditUseWholeCell = false;
            this.xlistview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.xlistview.Cursor = System.Windows.Forms.Cursors.Default;
            this.xlistview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xlistview.FullRowSelect = true;
            this.xlistview.HideSelection = false;
            this.xlistview.Location = new System.Drawing.Point(20, 103);
            this.xlistview.Name = "xlistview";
            this.xlistview.ShowGroups = false;
            this.xlistview.ShowItemToolTips = true;
            this.xlistview.Size = new System.Drawing.Size(849, 389);
            this.xlistview.TabIndex = 2;
            this.xlistview.UseCompatibleStateImageBehavior = false;
            this.xlistview.UseExplorerTheme = true;
            this.xlistview.UseHotItem = true;
            this.xlistview.UseTranslucentHotItem = true;
            this.xlistview.UseTranslucentSelection = true;
            this.xlistview.View = System.Windows.Forms.View.Details;
            this.xlistview.SelectedIndexChanged += new System.EventHandler(this.xlistview_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.HeaderFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Omschrijving";
            this.olvColumn1.WordWrap = true;
            // 
            // xAddPanel
            // 
            this.xAddPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xAddPanel.Controls.Add(this.xviewtext);
            this.xAddPanel.Controls.Add(this.xadd);
            this.xAddPanel.Location = new System.Drawing.Point(3, 3);
            this.xAddPanel.Name = "xAddPanel";
            this.xAddPanel.Size = new System.Drawing.Size(787, 40);
            this.xAddPanel.TabIndex = 3;
            // 
            // LijstWeergaveForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(889, 555);
            this.Controls.Add(this.xlistview);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(600, 345);
            this.Name = "LijstWeergaveForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lijst Weergave";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xlistview)).EndInit();
            this.xAddPanel.ResumeLayout(false);
            this.xAddPanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox xviewtext;
        private System.Windows.Forms.Button xadd;
        private System.Windows.Forms.Button xremove;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xOpslaan;
        private BrightIdeasSoftware.ObjectListView xlistview;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.Panel xAddPanel;
    }
}