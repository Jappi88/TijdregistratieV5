
namespace Forms
{
    partial class AlleStoringen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlleStoringen));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xperscontainer = new System.Windows.Forms.Panel();
            this.xwerkplekken = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xstatuslabel = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xrangepanel = new System.Windows.Forms.Panel();
            this.xupdatetijdb = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xvanaf = new System.Windows.Forms.DateTimePicker();
            this.xtot = new System.Windows.Forms.DateTimePicker();
            this.werkPlekStoringen1 = new Controls.WerkPlekStoringen();
            this.panel1.SuspendLayout();
            this.xperscontainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xwerkplekken)).BeginInit();
            this.panel2.SuspendLayout();
            this.xrangepanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xperscontainer);
            this.panel1.Controls.Add(this.xstatuslabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 39);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 433);
            this.panel1.TabIndex = 0;
            // 
            // xperscontainer
            // 
            this.xperscontainer.AutoScroll = true;
            this.xperscontainer.Controls.Add(this.xwerkplekken);
            this.xperscontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xperscontainer.Location = new System.Drawing.Point(0, 80);
            this.xperscontainer.Name = "xperscontainer";
            this.xperscontainer.Padding = new System.Windows.Forms.Padding(5);
            this.xperscontainer.Size = new System.Drawing.Size(227, 353);
            this.xperscontainer.TabIndex = 0;
            // 
            // xwerkplekken
            // 
            this.xwerkplekken.AllColumns.Add(this.olvColumn1);
            this.xwerkplekken.CellEditUseWholeCell = false;
            this.xwerkplekken.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.xwerkplekken.Cursor = System.Windows.Forms.Cursors.Default;
            this.xwerkplekken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xwerkplekken.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xwerkplekken.FullRowSelect = true;
            this.xwerkplekken.HideSelection = false;
            this.xwerkplekken.LargeImageList = this.imageList1;
            this.xwerkplekken.Location = new System.Drawing.Point(5, 5);
            this.xwerkplekken.Name = "xwerkplekken";
            this.xwerkplekken.ShowGroups = false;
            this.xwerkplekken.ShowItemToolTips = true;
            this.xwerkplekken.Size = new System.Drawing.Size(217, 343);
            this.xwerkplekken.SmallImageList = this.imageList1;
            this.xwerkplekken.TabIndex = 0;
            this.xwerkplekken.TileSize = new System.Drawing.Size(150, 35);
            this.xwerkplekken.UseCompatibleStateImageBehavior = false;
            this.xwerkplekken.UseExplorerTheme = true;
            this.xwerkplekken.UseFilterIndicator = true;
            this.xwerkplekken.UseFiltering = true;
            this.xwerkplekken.UseHotItem = true;
            this.xwerkplekken.UseTranslucentHotItem = true;
            this.xwerkplekken.UseTranslucentSelection = true;
            this.xwerkplekken.View = System.Windows.Forms.View.Details;
            this.xwerkplekken.SelectedIndexChanged += new System.EventHandler(this.xuserlist_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Key";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.Groupable = false;
            this.olvColumn1.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.Text = "Werkplek";
            this.olvColumn1.ToolTipText = "Werkplek";
            this.olvColumn1.Width = 200;
            this.olvColumn1.WordWrap = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xstatuslabel
            // 
            this.xstatuslabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xstatuslabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatuslabel.Location = new System.Drawing.Point(0, 0);
            this.xstatuslabel.Name = "xstatuslabel";
            this.xstatuslabel.Size = new System.Drawing.Size(227, 80);
            this.xstatuslabel.TabIndex = 1;
            this.xstatuslabel.Text = "Alle Werkplekken";
            this.xstatuslabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.werkPlekStoringen1);
            this.panel2.Controls.Add(this.panel1);
            this.panel2.Controls.Add(this.xrangepanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(20, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(919, 472);
            this.panel2.TabIndex = 2;
            // 
            // xrangepanel
            // 
            this.xrangepanel.Controls.Add(this.xupdatetijdb);
            this.xrangepanel.Controls.Add(this.xtot);
            this.xrangepanel.Controls.Add(this.xvanaf);
            this.xrangepanel.Controls.Add(this.pictureBox1);
            this.xrangepanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xrangepanel.Location = new System.Drawing.Point(0, 0);
            this.xrangepanel.Name = "xrangepanel";
            this.xrangepanel.Size = new System.Drawing.Size(919, 39);
            this.xrangepanel.TabIndex = 0;
            // 
            // xupdatetijdb
            // 
            this.xupdatetijdb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xupdatetijdb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xupdatetijdb.Image = global::ProductieManager.Properties.Resources.Time_machine__40675;
            this.xupdatetijdb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xupdatetijdb.Location = new System.Drawing.Point(688, 4);
            this.xupdatetijdb.Margin = new System.Windows.Forms.Padding(0);
            this.xupdatetijdb.Name = "xupdatetijdb";
            this.xupdatetijdb.Size = new System.Drawing.Size(93, 31);
            this.xupdatetijdb.TabIndex = 5;
            this.xupdatetijdb.Text = "Update";
            this.xupdatetijdb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xupdatetijdb.UseVisualStyleBackColor = true;
            this.xupdatetijdb.Click += new System.EventHandler(this.xupdatetijdb_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.business_color_progress_icon_icons_com_53437;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 39);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // xvanaf
            // 
            this.xvanaf.CustomFormat = "\'Vanaf\' dddd dd MMMM yyyy HH:mm";
            this.xvanaf.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvanaf.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xvanaf.Location = new System.Drawing.Point(53, 7);
            this.xvanaf.Name = "xvanaf";
            this.xvanaf.Size = new System.Drawing.Size(313, 25);
            this.xvanaf.TabIndex = 6;
            // 
            // xtot
            // 
            this.xtot.CustomFormat = "\'t/m\' dddd dd MMMM yyyy HH:mm";
            this.xtot.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtot.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xtot.Location = new System.Drawing.Point(372, 7);
            this.xtot.Name = "xtot";
            this.xtot.Size = new System.Drawing.Size(313, 25);
            this.xtot.TabIndex = 7;
            // 
            // werkPlekStoringen1
            // 
            this.werkPlekStoringen1.BackColor = System.Drawing.Color.White;
            this.werkPlekStoringen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.werkPlekStoringen1.IsCloseAble = true;
            this.werkPlekStoringen1.IsEditAble = true;
            this.werkPlekStoringen1.Location = new System.Drawing.Point(227, 39);
            this.werkPlekStoringen1.Name = "werkPlekStoringen1";
            this.werkPlekStoringen1.Size = new System.Drawing.Size(692, 433);
            this.werkPlekStoringen1.TabIndex = 1;
            this.werkPlekStoringen1.OnCloseButtonPressed += new System.EventHandler(this.werkPlekStoringen1_OnCloseButtonPressed);
            // 
            // AlleStoringen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 552);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(825, 525);
            this.Name = "AlleStoringen";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Alle Onderbrekeningen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlleVaardigheden_FormClosing);
            this.Shown += new System.EventHandler(this.AlleVaardigheden_Shown);
            this.panel1.ResumeLayout(false);
            this.xperscontainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xwerkplekken)).EndInit();
            this.panel2.ResumeLayout(false);
            this.xrangepanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel xperscontainer;
        private System.Windows.Forms.Label xstatuslabel;
        private BrightIdeasSoftware.ObjectListView xwerkplekken;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.ImageList imageList1;
        private Controls.WerkPlekStoringen werkPlekStoringen1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel xrangepanel;
        private System.Windows.Forms.Button xupdatetijdb;
        private System.Windows.Forms.DateTimePicker xtot;
        private System.Windows.Forms.DateTimePicker xvanaf;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}