
namespace ProductieManager.Forms
{
    partial class DeelsGereedMeldingenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeelsGereedMeldingenForm));
            this.xgereedlijst = new BrightIdeasSoftware.ObjectListView();
            this.xparaaf = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xwerkplek = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xaantal = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xdatum = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xnotitie = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.xdelete = new System.Windows.Forms.Button();
            this.xedit = new System.Windows.Forms.Button();
            this.xadd = new System.Windows.Forms.Button();
            this.xstatuslabel = new System.Windows.Forms.Label();
            this.xcancelb = new System.Windows.Forms.Button();
            this.xokb = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.xgereedlijst)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xgereedlijst
            // 
            this.xgereedlijst.AllColumns.Add(this.xparaaf);
            this.xgereedlijst.AllColumns.Add(this.xwerkplek);
            this.xgereedlijst.AllColumns.Add(this.xaantal);
            this.xgereedlijst.AllColumns.Add(this.xdatum);
            this.xgereedlijst.AllColumns.Add(this.xnotitie);
            this.xgereedlijst.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgereedlijst.CellEditUseWholeCell = false;
            this.xgereedlijst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.xparaaf,
            this.xwerkplek,
            this.xaantal,
            this.xdatum,
            this.xnotitie});
            this.xgereedlijst.Cursor = System.Windows.Forms.Cursors.Default;
            this.xgereedlijst.FullRowSelect = true;
            this.xgereedlijst.HideSelection = false;
            this.xgereedlijst.LargeImageList = this.imageList1;
            this.xgereedlijst.Location = new System.Drawing.Point(23, 107);
            this.xgereedlijst.Name = "xgereedlijst";
            this.xgereedlijst.ShowItemToolTips = true;
            this.xgereedlijst.Size = new System.Drawing.Size(604, 207);
            this.xgereedlijst.SmallImageList = this.imageList1;
            this.xgereedlijst.TabIndex = 10;
            this.xgereedlijst.UseCompatibleStateImageBehavior = false;
            this.xgereedlijst.UseExplorerTheme = true;
            this.xgereedlijst.UseHotItem = true;
            this.xgereedlijst.UseTranslucentHotItem = true;
            this.xgereedlijst.UseTranslucentSelection = true;
            this.xgereedlijst.View = System.Windows.Forms.View.Details;
            this.xgereedlijst.SelectedIndexChanged += new System.EventHandler(this.xgereedlijst_SelectedIndexChanged);
            // 
            // xparaaf
            // 
            this.xparaaf.AspectName = "Paraaf";
            this.xparaaf.Text = "Paraaf";
            this.xparaaf.ToolTipText = "Paraaf Gereed";
            this.xparaaf.Width = 120;
            this.xparaaf.WordWrap = true;
            // 
            // xwerkplek
            // 
            this.xwerkplek.AspectName = "WerkPlek";
            this.xwerkplek.Text = "WerkPlek";
            this.xwerkplek.ToolTipText = "Werkplek";
            this.xwerkplek.Width = 150;
            this.xwerkplek.WordWrap = true;
            // 
            // xaantal
            // 
            this.xaantal.AspectName = "Aantal";
            this.xaantal.Text = "Aantal";
            this.xaantal.ToolTipText = "Aantal gereed gemeld";
            this.xaantal.Width = 100;
            // 
            // xdatum
            // 
            this.xdatum.AspectName = "Datum";
            this.xdatum.Text = "Datum Gereed";
            this.xdatum.ToolTipText = "Datum gereed";
            this.xdatum.Width = 150;
            this.xdatum.WordWrap = true;
            // 
            // xnotitie
            // 
            this.xnotitie.AspectName = "Notitie";
            this.xnotitie.Text = "Notitie";
            this.xnotitie.ToolTipText = "Gereed Notitie";
            this.xnotitie.Width = 250;
            this.xnotitie.WordWrap = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "ic_done_all_128_28243.png");
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.Controls.Add(this.xdelete);
            this.panel1.Controls.Add(this.xedit);
            this.panel1.Controls.Add(this.xadd);
            this.panel1.Location = new System.Drawing.Point(23, 63);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(615, 38);
            this.panel1.TabIndex = 11;
            // 
            // xdelete
            // 
            this.xdelete.Dock = System.Windows.Forms.DockStyle.Left;
            this.xdelete.Enabled = false;
            this.xdelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdelete.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdelete.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdelete.Location = new System.Drawing.Point(82, 0);
            this.xdelete.Name = "xdelete";
            this.xdelete.Size = new System.Drawing.Size(41, 38);
            this.xdelete.TabIndex = 6;
            this.xdelete.UseVisualStyleBackColor = true;
            this.xdelete.Click += new System.EventHandler(this.xdelete_Click);
            // 
            // xedit
            // 
            this.xedit.Dock = System.Windows.Forms.DockStyle.Left;
            this.xedit.Enabled = false;
            this.xedit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xedit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xedit.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xedit.Location = new System.Drawing.Point(41, 0);
            this.xedit.Name = "xedit";
            this.xedit.Size = new System.Drawing.Size(41, 38);
            this.xedit.TabIndex = 5;
            this.xedit.UseVisualStyleBackColor = true;
            this.xedit.Click += new System.EventHandler(this.xedit_Click);
            // 
            // xadd
            // 
            this.xadd.Dock = System.Windows.Forms.DockStyle.Left;
            this.xadd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xadd.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xadd.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xadd.Location = new System.Drawing.Point(0, 0);
            this.xadd.Name = "xadd";
            this.xadd.Size = new System.Drawing.Size(41, 38);
            this.xadd.TabIndex = 4;
            this.xadd.UseVisualStyleBackColor = true;
            this.xadd.Click += new System.EventHandler(this.xadd_Click);
            // 
            // xstatuslabel
            // 
            this.xstatuslabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xstatuslabel.AutoSize = true;
            this.xstatuslabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatuslabel.Location = new System.Drawing.Point(23, 325);
            this.xstatuslabel.Name = "xstatuslabel";
            this.xstatuslabel.Size = new System.Drawing.Size(64, 25);
            this.xstatuslabel.TabIndex = 12;
            this.xstatuslabel.Text = "status";
            // 
            // xcancelb
            // 
            this.xcancelb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xcancelb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xcancelb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xcancelb.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xcancelb.Location = new System.Drawing.Point(507, 320);
            this.xcancelb.Name = "xcancelb";
            this.xcancelb.Size = new System.Drawing.Size(120, 38);
            this.xcancelb.TabIndex = 9;
            this.xcancelb.Text = "&Sluiten";
            this.xcancelb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xcancelb.UseVisualStyleBackColor = true;
            this.xcancelb.Click += new System.EventHandler(this.xcancelb_Click);
            // 
            // xokb
            // 
            this.xokb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xokb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xokb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xokb.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xokb.Location = new System.Drawing.Point(381, 320);
            this.xokb.Name = "xokb";
            this.xokb.Size = new System.Drawing.Size(120, 38);
            this.xokb.TabIndex = 8;
            this.xokb.Text = "&Opslaan";
            this.xokb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xokb.UseVisualStyleBackColor = true;
            this.xokb.Click += new System.EventHandler(this.xokb_Click);
            // 
            // DeelsGereedMeldingenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 370);
            this.Controls.Add(this.xstatuslabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.xgereedlijst);
            this.Controls.Add(this.xcancelb);
            this.Controls.Add(this.xokb);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(650, 300);
            this.Name = "DeelsGereedMeldingenForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Deels Gereed Meldingen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DeelsGereedMeldingenForm_FormClosing);
            this.Shown += new System.EventHandler(this.DeelsGereedMeldingenForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.xgereedlijst)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button xcancelb;
        private System.Windows.Forms.Button xokb;
        private BrightIdeasSoftware.ObjectListView xgereedlijst;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xdelete;
        private System.Windows.Forms.Button xedit;
        private System.Windows.Forms.Button xadd;
        private System.Windows.Forms.Label xstatuslabel;
        private System.Windows.Forms.ImageList imageList1;
        private BrightIdeasSoftware.OLVColumn xparaaf;
        private BrightIdeasSoftware.OLVColumn xwerkplek;
        private BrightIdeasSoftware.OLVColumn xaantal;
        private BrightIdeasSoftware.OLVColumn xdatum;
        private BrightIdeasSoftware.OLVColumn xnotitie;
    }
}