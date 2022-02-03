
namespace Forms
{
    partial class EmailClientsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EmailClientsForm));
            this.xontvangers = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.xdeleteuser = new System.Windows.Forms.Button();
            this.xedituser = new System.Windows.Forms.Button();
            this.xadduser = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xOpslaan = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.xontvangers)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // xontvangers
            // 
            this.xontvangers.AllColumns.Add(this.olvColumn1);
            this.xontvangers.AllColumns.Add(this.olvColumn2);
            this.xontvangers.CellEditUseWholeCell = false;
            this.xontvangers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.xontvangers.Cursor = System.Windows.Forms.Cursors.Default;
            this.xontvangers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xontvangers.FullRowSelect = true;
            this.xontvangers.HideSelection = false;
            this.xontvangers.LargeImageList = this.imageList1;
            this.xontvangers.Location = new System.Drawing.Point(20, 101);
            this.xontvangers.Name = "xontvangers";
            this.xontvangers.ShowGroups = false;
            this.xontvangers.ShowItemToolTips = true;
            this.xontvangers.Size = new System.Drawing.Size(460, 200);
            this.xontvangers.SmallImageList = this.imageList1;
            this.xontvangers.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.xontvangers.TabIndex = 0;
            this.xontvangers.UseCompatibleStateImageBehavior = false;
            this.xontvangers.UseExplorerTheme = true;
            this.xontvangers.UseHotItem = true;
            this.xontvangers.UseTranslucentHotItem = true;
            this.xontvangers.View = System.Windows.Forms.View.Details;
            this.xontvangers.SelectedIndexChanged += new System.EventHandler(this.xontvangers_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Name";
            this.olvColumn1.Text = "Naam";
            this.olvColumn1.ToolTipText = "Ontvanger naam";
            this.olvColumn1.Width = 165;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Email";
            this.olvColumn2.Text = "Email";
            this.olvColumn2.ToolTipText = "Ontvanger email";
            this.olvColumn2.Width = 200;
            this.olvColumn2.WordWrap = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "user_customer_person_32x32.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xdeleteuser);
            this.panel1.Controls.Add(this.xedituser);
            this.panel1.Controls.Add(this.xadduser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(3, 0, 3, 0);
            this.panel1.Size = new System.Drawing.Size(460, 41);
            this.panel1.TabIndex = 1;
            // 
            // xdeleteuser
            // 
            this.xdeleteuser.Dock = System.Windows.Forms.DockStyle.Left;
            this.xdeleteuser.Enabled = false;
            this.xdeleteuser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdeleteuser.Image = global::ProductieManager.Properties.Resources.delete_delete_deleteusers_delete_male_user_maleclient_2348;
            this.xdeleteuser.Location = new System.Drawing.Point(79, 0);
            this.xdeleteuser.Name = "xdeleteuser";
            this.xdeleteuser.Size = new System.Drawing.Size(38, 41);
            this.xdeleteuser.TabIndex = 1;
            this.toolTip1.SetToolTip(this.xdeleteuser, "Verwijder geselecteerde ontvangers");
            this.xdeleteuser.UseVisualStyleBackColor = true;
            this.xdeleteuser.Click += new System.EventHandler(this.xdeleteuser_Click);
            // 
            // xedituser
            // 
            this.xedituser.Dock = System.Windows.Forms.DockStyle.Left;
            this.xedituser.Enabled = false;
            this.xedituser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xedituser.Image = global::ProductieManager.Properties.Resources.businessapplication_edit_male_user;
            this.xedituser.Location = new System.Drawing.Point(41, 0);
            this.xedituser.Name = "xedituser";
            this.xedituser.Size = new System.Drawing.Size(38, 41);
            this.xedituser.TabIndex = 2;
            this.toolTip1.SetToolTip(this.xedituser, "Wijzig geselecteerde ontvanger");
            this.xedituser.UseVisualStyleBackColor = true;
            this.xedituser.Click += new System.EventHandler(this.xedituser_Click);
            // 
            // xadduser
            // 
            this.xadduser.Dock = System.Windows.Forms.DockStyle.Left;
            this.xadduser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xadduser.Image = global::ProductieManager.Properties.Resources.user_add_12818;
            this.xadduser.Location = new System.Drawing.Point(3, 0);
            this.xadduser.Name = "xadduser";
            this.xadduser.Size = new System.Drawing.Size(38, 41);
            this.xadduser.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xadduser, "Voeg nieuwe ontvanger toe");
            this.xadduser.UseVisualStyleBackColor = true;
            this.xadduser.Click += new System.EventHandler(this.xadduser_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Email Ontvangers";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 301);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(460, 44);
            this.panel2.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xsluiten);
            this.panel3.Controls.Add(this.xOpslaan);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(198, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(262, 44);
            this.panel3.TabIndex = 1;
            // 
            // xsluiten
            // 
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.ForeColor = System.Drawing.Color.Black;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(134, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(125, 38);
            this.xsluiten.TabIndex = 4;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xOpslaan
            // 
            this.xOpslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOpslaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOpslaan.ForeColor = System.Drawing.Color.Black;
            this.xOpslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xOpslaan.Location = new System.Drawing.Point(3, 3);
            this.xOpslaan.Name = "xOpslaan";
            this.xOpslaan.Size = new System.Drawing.Size(125, 38);
            this.xOpslaan.TabIndex = 5;
            this.xOpslaan.Text = "Opslaan";
            this.xOpslaan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xOpslaan.UseVisualStyleBackColor = true;
            this.xOpslaan.Click += new System.EventHandler(this.xOpslaan_Click);
            // 
            // EmailClientsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 365);
            this.Controls.Add(this.xontvangers);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(500, 365);
            this.Name = "EmailClientsForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Email Ontvangers";
            ((System.ComponentModel.ISupportInitialize)(this.xontvangers)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView xontvangers;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xdeleteuser;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button xadduser;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xOpslaan;
        private System.Windows.Forms.Button xedituser;
        private System.Windows.Forms.ImageList imageList1;
    }
}