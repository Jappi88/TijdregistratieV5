
namespace Forms
{
    partial class AlleVaardigheden
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlleVaardigheden));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xperscontainer = new System.Windows.Forms.Panel();
            this.xuserlist = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xstatuslabel = new System.Windows.Forms.Label();
            this.persoonVaardigheden1 = new Controls.PersoonVaardigheden();
            this.panel1.SuspendLayout();
            this.xperscontainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xuserlist)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xperscontainer);
            this.panel1.Controls.Add(this.xstatuslabel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(227, 506);
            this.panel1.TabIndex = 0;
            // 
            // xperscontainer
            // 
            this.xperscontainer.AutoScroll = true;
            this.xperscontainer.Controls.Add(this.xuserlist);
            this.xperscontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xperscontainer.Location = new System.Drawing.Point(0, 80);
            this.xperscontainer.Name = "xperscontainer";
            this.xperscontainer.Padding = new System.Windows.Forms.Padding(5);
            this.xperscontainer.Size = new System.Drawing.Size(227, 426);
            this.xperscontainer.TabIndex = 0;
            // 
            // xuserlist
            // 
            this.xuserlist.AllColumns.Add(this.olvColumn1);
            this.xuserlist.CellEditUseWholeCell = false;
            this.xuserlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.xuserlist.Cursor = System.Windows.Forms.Cursors.Default;
            this.xuserlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xuserlist.FullRowSelect = true;
            this.xuserlist.HideSelection = false;
            this.xuserlist.LargeImageList = this.imageList1;
            this.xuserlist.Location = new System.Drawing.Point(5, 5);
            this.xuserlist.Name = "xuserlist";
            this.xuserlist.ShowGroups = false;
            this.xuserlist.ShowItemToolTips = true;
            this.xuserlist.Size = new System.Drawing.Size(217, 416);
            this.xuserlist.SmallImageList = this.imageList1;
            this.xuserlist.TabIndex = 0;
            this.xuserlist.TileSize = new System.Drawing.Size(150, 35);
            this.xuserlist.UseCompatibleStateImageBehavior = false;
            this.xuserlist.UseExplorerTheme = true;
            this.xuserlist.UseFilterIndicator = true;
            this.xuserlist.UseFiltering = true;
            this.xuserlist.UseHotItem = true;
            this.xuserlist.UseTranslucentHotItem = true;
            this.xuserlist.UseTranslucentSelection = true;
            this.xuserlist.View = System.Windows.Forms.View.Tile;
            this.xuserlist.SelectedIndexChanged += new System.EventHandler(this.xuserlist_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "PersoneelNaam";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.Groupable = false;
            this.olvColumn1.HeaderFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.Text = "Naam";
            this.olvColumn1.ToolTipText = "Personeel naam";
            this.olvColumn1.Width = 200;
            this.olvColumn1.WordWrap = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "user_customer_person_13976.png");
            // 
            // xstatuslabel
            // 
            this.xstatuslabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xstatuslabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatuslabel.Location = new System.Drawing.Point(0, 0);
            this.xstatuslabel.Name = "xstatuslabel";
            this.xstatuslabel.Size = new System.Drawing.Size(227, 80);
            this.xstatuslabel.TabIndex = 1;
            this.xstatuslabel.Text = "Alle Medewerkers";
            this.xstatuslabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // persoonVaardigheden1
            // 
            this.persoonVaardigheden1.BackColor = System.Drawing.Color.White;
            this.persoonVaardigheden1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.persoonVaardigheden1.IsCloseAble = true;
            this.persoonVaardigheden1.Location = new System.Drawing.Point(247, 60);
            this.persoonVaardigheden1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.persoonVaardigheden1.Name = "persoonVaardigheden1";
            this.persoonVaardigheden1.Size = new System.Drawing.Size(764, 506);
            this.persoonVaardigheden1.TabIndex = 1;
            this.persoonVaardigheden1.OnCloseButtonPressed += new System.EventHandler(this.persoonVaardigheden1_OnCloseButtonPressed);
            // 
            // AlleVaardigheden
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 586);
            this.Controls.Add(this.persoonVaardigheden1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "AlleVaardigheden";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Alle Vaardigheden";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlleVaardigheden_FormClosing);
            this.Shown += new System.EventHandler(this.AlleVaardigheden_Shown);
            this.panel1.ResumeLayout(false);
            this.xperscontainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xuserlist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel xperscontainer;
        private System.Windows.Forms.Label xstatuslabel;
        private BrightIdeasSoftware.ObjectListView xuserlist;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private Controls.PersoonVaardigheden persoonVaardigheden1;
        private System.Windows.Forms.ImageList imageList1;
    }
}