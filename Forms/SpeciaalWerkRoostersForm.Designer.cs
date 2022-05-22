
namespace Forms
{
    partial class SpeciaalWerkRoostersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SpeciaalWerkRoostersForm));
            this.panel3 = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xOpslaan = new System.Windows.Forms.Button();
            this.xroosterlist = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xdeleterooster = new System.Windows.Forms.Button();
            this.xeditrooster = new System.Windows.Forms.Button();
            this.xaddrooster = new System.Windows.Forms.Button();
            this.xroosterdatelabel = new System.Windows.Forms.Label();
            this.roosterUI1 = new Controls.RoosterUI();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xroosterlist)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xsluiten);
            this.panel3.Controls.Add(this.xOpslaan);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(20, 476);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(771, 48);
            this.panel3.TabIndex = 1;
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.ForeColor = System.Drawing.Color.Black;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(649, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(119, 38);
            this.xsluiten.TabIndex = 4;
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
            this.xOpslaan.Location = new System.Drawing.Point(524, 3);
            this.xOpslaan.Name = "xOpslaan";
            this.xOpslaan.Size = new System.Drawing.Size(119, 38);
            this.xOpslaan.TabIndex = 5;
            this.xOpslaan.Text = "Opslaan";
            this.xOpslaan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xOpslaan.UseVisualStyleBackColor = true;
            this.xOpslaan.Click += new System.EventHandler(this.xOpslaan_Click);
            // 
            // xroosterlist
            // 
            this.xroosterlist.AllColumns.Add(this.olvColumn1);
            this.xroosterlist.CellEditUseWholeCell = false;
            this.xroosterlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.xroosterlist.Cursor = System.Windows.Forms.Cursors.Default;
            this.xroosterlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xroosterlist.FullRowSelect = true;
            this.xroosterlist.HideSelection = false;
            this.xroosterlist.LargeImageList = this.imageList1;
            this.xroosterlist.Location = new System.Drawing.Point(0, 35);
            this.xroosterlist.Name = "xroosterlist";
            this.xroosterlist.ShowGroups = false;
            this.xroosterlist.ShowItemToolTips = true;
            this.xroosterlist.Size = new System.Drawing.Size(258, 381);
            this.xroosterlist.SmallImageList = this.imageList1;
            this.xroosterlist.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.xroosterlist.TabIndex = 2;
            this.xroosterlist.UseCompatibleStateImageBehavior = false;
            this.xroosterlist.UseExplorerTheme = true;
            this.xroosterlist.UseHotItem = true;
            this.xroosterlist.UseTranslucentHotItem = true;
            this.xroosterlist.UseTranslucentSelection = true;
            this.xroosterlist.View = System.Windows.Forms.View.Details;
            this.xroosterlist.BeforeSorting += new System.EventHandler<BrightIdeasSoftware.BeforeSortingEventArgs>(this.xroosterlist_BeforeSorting);
            this.xroosterlist.SelectedIndexChanged += new System.EventHandler(this.xroosterlist_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Vanaf";
            this.olvColumn1.AspectToStringFormat = "";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Datum";
            this.olvColumn1.ToolTipText = "Rooster datum";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "schedule_32_32.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xroosterlist);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(258, 416);
            this.panel1.TabIndex = 3;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xdeleterooster);
            this.panel2.Controls.Add(this.xeditrooster);
            this.panel2.Controls.Add(this.xaddrooster);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(258, 35);
            this.panel2.TabIndex = 3;
            // 
            // xdeleterooster
            // 
            this.xdeleterooster.Dock = System.Windows.Forms.DockStyle.Left;
            this.xdeleterooster.Enabled = false;
            this.xdeleterooster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdeleterooster.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdeleterooster.Location = new System.Drawing.Point(76, 0);
            this.xdeleterooster.Name = "xdeleterooster";
            this.xdeleterooster.Size = new System.Drawing.Size(38, 35);
            this.xdeleterooster.TabIndex = 2;
            this.xdeleterooster.UseVisualStyleBackColor = true;
            this.xdeleterooster.Click += new System.EventHandler(this.xdeleterooster_Click);
            // 
            // xeditrooster
            // 
            this.xeditrooster.Dock = System.Windows.Forms.DockStyle.Left;
            this.xeditrooster.Enabled = false;
            this.xeditrooster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xeditrooster.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xeditrooster.Location = new System.Drawing.Point(38, 0);
            this.xeditrooster.Name = "xeditrooster";
            this.xeditrooster.Size = new System.Drawing.Size(38, 35);
            this.xeditrooster.TabIndex = 1;
            this.xeditrooster.UseVisualStyleBackColor = true;
            this.xeditrooster.Click += new System.EventHandler(this.xeditrooster_Click);
            // 
            // xaddrooster
            // 
            this.xaddrooster.Dock = System.Windows.Forms.DockStyle.Left;
            this.xaddrooster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xaddrooster.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xaddrooster.Location = new System.Drawing.Point(0, 0);
            this.xaddrooster.Name = "xaddrooster";
            this.xaddrooster.Size = new System.Drawing.Size(38, 35);
            this.xaddrooster.TabIndex = 0;
            this.xaddrooster.UseVisualStyleBackColor = true;
            this.xaddrooster.Click += new System.EventHandler(this.xaddrooster_Click);
            // 
            // xroosterdatelabel
            // 
            this.xroosterdatelabel.AutoSize = true;
            this.xroosterdatelabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xroosterdatelabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xroosterdatelabel.Location = new System.Drawing.Point(278, 60);
            this.xroosterdatelabel.Name = "xroosterdatelabel";
            this.xroosterdatelabel.Size = new System.Drawing.Size(146, 25);
            this.xroosterdatelabel.TabIndex = 5;
            this.xroosterdatelabel.Text = "Rooster Datum";
            // 
            // roosterUI1
            // 
            this.roosterUI1.AutoUpdateBewerkingen = false;
            this.roosterUI1.BackColor = System.Drawing.Color.White;
            this.roosterUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roosterUI1.Enabled = false;
            this.roosterUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roosterUI1.Location = new System.Drawing.Point(278, 85);
            this.roosterUI1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.roosterUI1.Name = "roosterUI1";
            this.roosterUI1.Padding = new System.Windows.Forms.Padding(5);
            this.roosterUI1.ShowNationaleFeestDagen = true;
            this.roosterUI1.ShowSpecialeRoosterButton = true;
            this.roosterUI1.Size = new System.Drawing.Size(513, 391);
            this.roosterUI1.SpecialeRoosters = null;
            this.roosterUI1.TabIndex = 4;
            this.roosterUI1.WerkRooster = null;
            // 
            // SpeciaalWerkRoostersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(811, 544);
            this.Controls.Add(this.roosterUI1);
            this.Controls.Add(this.xroosterdatelabel);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel3);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(565, 465);
            this.Name = "SpeciaalWerkRoostersForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Speciale Werk Roosters";
            this.Title = "Speciale Werk Roosters";
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xroosterlist)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xOpslaan;
        private BrightIdeasSoftware.ObjectListView xroosterlist;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imageList1;
        private Controls.RoosterUI roosterUI1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xdeleterooster;
        private System.Windows.Forms.Button xeditrooster;
        private System.Windows.Forms.Button xaddrooster;
        private System.Windows.Forms.Label xroosterdatelabel;
    }
}