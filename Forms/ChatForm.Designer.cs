
namespace ProductieManager.Forms
{
    partial class ChatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xuserlist = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.xuserimages = new System.Windows.Forms.ImageList(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.xprofilestatusimage = new System.Windows.Forms.PictureBox();
            this.xprofilestatus = new System.Windows.Forms.Label();
            this.xprofilenaam = new System.Windows.Forms.Label();
            this.xprofileimage = new System.Windows.Forms.PictureBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.wijzigProfielFotoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xchattextbox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xsendbutton = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.menuStrip2 = new System.Windows.Forms.MenuStrip();
            this.xselecteduserprofilebutton = new System.Windows.Forms.ToolStripMenuItem();
            this.xselecteduserdate = new System.Windows.Forms.Label();
            this.xselecteduserstatusimage = new System.Windows.Forms.PictureBox();
            this.xselecteduserstatus = new System.Windows.Forms.Label();
            this.xselectedusername = new System.Windows.Forms.Label();
            this.xselecteduserimage = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xchatpanel = new HtmlRenderer.HtmlPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xuserlist)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xprofilestatusimage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xprofileimage)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.menuStrip2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xselecteduserstatusimage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xselecteduserimage)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xuserlist);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 420);
            this.panel1.TabIndex = 3;
            // 
            // xuserlist
            // 
            this.xuserlist.AllColumns.Add(this.olvColumn1);
            this.xuserlist.AllColumns.Add(this.olvColumn2);
            this.xuserlist.AllColumns.Add(this.olvColumn3);
            this.xuserlist.CellEditUseWholeCell = false;
            this.xuserlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3});
            this.xuserlist.Cursor = System.Windows.Forms.Cursors.Default;
            this.xuserlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xuserlist.FullRowSelect = true;
            this.xuserlist.HideSelection = false;
            this.xuserlist.LargeImageList = this.xuserimages;
            this.xuserlist.Location = new System.Drawing.Point(0, 72);
            this.xuserlist.MultiSelect = false;
            this.xuserlist.Name = "xuserlist";
            this.xuserlist.ShowGroups = false;
            this.xuserlist.ShowItemToolTips = true;
            this.xuserlist.Size = new System.Drawing.Size(250, 348);
            this.xuserlist.SmallImageList = this.xuserimages;
            this.xuserlist.TabIndex = 9;
            this.xuserlist.TileSize = new System.Drawing.Size(240, 68);
            this.xuserlist.UseCompatibleStateImageBehavior = false;
            this.xuserlist.UseExplorerTheme = true;
            this.xuserlist.UseHotItem = true;
            this.xuserlist.UseTranslucentHotItem = true;
            this.xuserlist.UseTranslucentSelection = true;
            this.xuserlist.View = System.Windows.Forms.View.Tile;
            this.xuserlist.SelectedIndexChanged += new System.EventHandler(this.xuserlist_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.ShowTextInHeader = false;
            this.olvColumn1.Text = "";
            this.olvColumn1.Width = 50;
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn2
            // 
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.IsTileViewColumn = true;
            this.olvColumn2.ShowTextInHeader = false;
            this.olvColumn2.Text = "";
            this.olvColumn2.Width = 0;
            // 
            // olvColumn3
            // 
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.IsTileViewColumn = true;
            this.olvColumn3.ShowTextInHeader = false;
            this.olvColumn3.Text = "";
            this.olvColumn3.Width = 0;
            this.olvColumn3.WordWrap = true;
            // 
            // xuserimages
            // 
            this.xuserimages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.xuserimages.ImageSize = new System.Drawing.Size(64, 64);
            this.xuserimages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.AliceBlue;
            this.panel3.Controls.Add(this.xprofilestatusimage);
            this.panel3.Controls.Add(this.xprofilestatus);
            this.panel3.Controls.Add(this.xprofilenaam);
            this.panel3.Controls.Add(this.xprofileimage);
            this.panel3.Controls.Add(this.menuStrip1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(250, 72);
            this.panel3.TabIndex = 8;
            this.toolTip1.SetToolTip(this.panel3, "Mijn Profiel");
            // 
            // xprofilestatusimage
            // 
            this.xprofilestatusimage.BackColor = System.Drawing.Color.Transparent;
            this.xprofilestatusimage.Image = global::ProductieManager.Properties.Resources.offline_32x32;
            this.xprofilestatusimage.Location = new System.Drawing.Point(71, 28);
            this.xprofilestatusimage.Name = "xprofilestatusimage";
            this.xprofilestatusimage.Size = new System.Drawing.Size(16, 16);
            this.xprofilestatusimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xprofilestatusimage.TabIndex = 13;
            this.xprofilestatusimage.TabStop = false;
            // 
            // xprofilestatus
            // 
            this.xprofilestatus.AutoSize = true;
            this.xprofilestatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprofilestatus.ForeColor = System.Drawing.Color.DimGray;
            this.xprofilestatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xprofilestatus.Location = new System.Drawing.Point(93, 28);
            this.xprofilestatus.Name = "xprofilestatus";
            this.xprofilestatus.Size = new System.Drawing.Size(46, 17);
            this.xprofilestatus.TabIndex = 12;
            this.xprofilestatus.Text = "Offline";
            // 
            // xprofilenaam
            // 
            this.xprofilenaam.AutoSize = true;
            this.xprofilenaam.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprofilenaam.Location = new System.Drawing.Point(68, 9);
            this.xprofilenaam.Name = "xprofilenaam";
            this.xprofilenaam.Size = new System.Drawing.Size(68, 17);
            this.xprofilenaam.TabIndex = 11;
            this.xprofilenaam.Text = "Gebruiker";
            // 
            // xprofileimage
            // 
            this.xprofileimage.BackColor = System.Drawing.Color.Transparent;
            this.xprofileimage.Location = new System.Drawing.Point(3, 3);
            this.xprofileimage.Name = "xprofileimage";
            this.xprofileimage.Size = new System.Drawing.Size(64, 64);
            this.xprofileimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xprofileimage.TabIndex = 7;
            this.xprofileimage.TabStop = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.AutoSize = false;
            this.menuStrip1.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip1.Dock = System.Windows.Forms.DockStyle.Right;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.menuStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip1.Location = new System.Drawing.Point(197, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(53, 72);
            this.menuStrip1.TabIndex = 14;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.Transparent;
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wijzigProfielFotoToolStripMenuItem});
            this.toolStripMenuItem1.Image = global::ProductieManager.Properties.Resources.icons8_Menu_Vertical_32;
            this.toolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(44, 68);
            // 
            // wijzigProfielFotoToolStripMenuItem
            // 
            this.wijzigProfielFotoToolStripMenuItem.BackColor = System.Drawing.Color.AliceBlue;
            this.wijzigProfielFotoToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.profile_picture_32x32;
            this.wijzigProfielFotoToolStripMenuItem.Name = "wijzigProfielFotoToolStripMenuItem";
            this.wijzigProfielFotoToolStripMenuItem.Size = new System.Drawing.Size(170, 22);
            this.wijzigProfielFotoToolStripMenuItem.Text = "Wijzig Profiel Foto";
            this.wijzigProfielFotoToolStripMenuItem.Click += new System.EventHandler(this.wijzigProfielFotoToolStripMenuItem_Click);
            // 
            // xchattextbox
            // 
            this.xchattextbox.AllowDrop = true;
            this.xchattextbox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xchattextbox.BackColor = System.Drawing.Color.White;
            this.xchattextbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xchattextbox.ForeColor = System.Drawing.Color.Gray;
            this.xchattextbox.Location = new System.Drawing.Point(6, 3);
            this.xchattextbox.MinimumSize = new System.Drawing.Size(4, 30);
            this.xchattextbox.Multiline = true;
            this.xchattextbox.Name = "xchattextbox";
            this.xchattextbox.Size = new System.Drawing.Size(461, 51);
            this.xchattextbox.TabIndex = 4;
            this.xchattextbox.Text = "Typ bericht...";
            this.toolTip1.SetToolTip(this.xchattextbox, "Type in een bericht...");
            this.xchattextbox.DragDrop += new System.Windows.Forms.DragEventHandler(this.xchatpanel_DragDrop);
            this.xchattextbox.DragEnter += new System.Windows.Forms.DragEventHandler(this.xchatpanel_DragEnter);
            this.xchattextbox.Enter += new System.EventHandler(this.xchattextbox_Enter);
            this.xchattextbox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xchattextbox_KeyDown);
            this.xchattextbox.Leave += new System.EventHandler(this.xchattextbox_Leave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xchattextbox);
            this.panel2.Controls.Add(this.xsendbutton);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 288);
            this.panel2.MinimumSize = new System.Drawing.Size(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(546, 57);
            this.panel2.TabIndex = 5;
            // 
            // xsendbutton
            // 
            this.xsendbutton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsendbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsendbutton.Image = global::ProductieManager.Properties.Resources.ic_send_128_28719;
            this.xsendbutton.Location = new System.Drawing.Point(467, 28);
            this.xsendbutton.Name = "xsendbutton";
            this.xsendbutton.Size = new System.Drawing.Size(76, 26);
            this.xsendbutton.TabIndex = 5;
            this.toolTip1.SetToolTip(this.xsendbutton, "Verzend bericht...");
            this.xsendbutton.UseVisualStyleBackColor = true;
            this.xsendbutton.Click += new System.EventHandler(this.xsendbutton_Click);
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.AliceBlue;
            this.panel4.Controls.Add(this.menuStrip2);
            this.panel4.Controls.Add(this.xselecteduserdate);
            this.panel4.Controls.Add(this.xselecteduserstatusimage);
            this.panel4.Controls.Add(this.xselecteduserstatus);
            this.panel4.Controls.Add(this.xselectedusername);
            this.panel4.Controls.Add(this.xselecteduserimage);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(250, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(552, 72);
            this.panel4.TabIndex = 9;
            // 
            // menuStrip2
            // 
            this.menuStrip2.AutoSize = false;
            this.menuStrip2.BackColor = System.Drawing.Color.Transparent;
            this.menuStrip2.Dock = System.Windows.Forms.DockStyle.Right;
            this.menuStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xselecteduserprofilebutton});
            this.menuStrip2.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.menuStrip2.Location = new System.Drawing.Point(499, 0);
            this.menuStrip2.Name = "menuStrip2";
            this.menuStrip2.Size = new System.Drawing.Size(53, 72);
            this.menuStrip2.TabIndex = 15;
            this.menuStrip2.Text = "menuStrip2";
            // 
            // xselecteduserprofilebutton
            // 
            this.xselecteduserprofilebutton.BackColor = System.Drawing.Color.Transparent;
            this.xselecteduserprofilebutton.Image = global::ProductieManager.Properties.Resources.icons8_Menu_Vertical_32;
            this.xselecteduserprofilebutton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xselecteduserprofilebutton.Name = "xselecteduserprofilebutton";
            this.xselecteduserprofilebutton.Size = new System.Drawing.Size(44, 68);
            this.xselecteduserprofilebutton.Visible = false;
            // 
            // xselecteduserdate
            // 
            this.xselecteduserdate.AutoSize = true;
            this.xselecteduserdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xselecteduserdate.ForeColor = System.Drawing.Color.Black;
            this.xselecteduserdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xselecteduserdate.Location = new System.Drawing.Point(73, 47);
            this.xselecteduserdate.Name = "xselecteduserdate";
            this.xselecteduserdate.Size = new System.Drawing.Size(198, 17);
            this.xselecteduserdate.TabIndex = 11;
            this.xselecteduserdate.Text = "Laatsts online op 24-5-2021 8:30";
            // 
            // xselecteduserstatusimage
            // 
            this.xselecteduserstatusimage.BackColor = System.Drawing.Color.Transparent;
            this.xselecteduserstatusimage.Image = global::ProductieManager.Properties.Resources.Online_32;
            this.xselecteduserstatusimage.Location = new System.Drawing.Point(76, 28);
            this.xselecteduserstatusimage.Name = "xselecteduserstatusimage";
            this.xselecteduserstatusimage.Size = new System.Drawing.Size(16, 16);
            this.xselecteduserstatusimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xselecteduserstatusimage.TabIndex = 10;
            this.xselecteduserstatusimage.TabStop = false;
            // 
            // xselecteduserstatus
            // 
            this.xselecteduserstatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xselecteduserstatus.ForeColor = System.Drawing.Color.Black;
            this.xselecteduserstatus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xselecteduserstatus.Location = new System.Drawing.Point(98, 28);
            this.xselecteduserstatus.Name = "xselecteduserstatus";
            this.xselecteduserstatus.Size = new System.Drawing.Size(76, 16);
            this.xselecteduserstatus.TabIndex = 9;
            this.xselecteduserstatus.Text = "Online";
            // 
            // xselectedusername
            // 
            this.xselectedusername.AutoSize = true;
            this.xselectedusername.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xselectedusername.Location = new System.Drawing.Point(73, 9);
            this.xselectedusername.Name = "xselectedusername";
            this.xselectedusername.Size = new System.Drawing.Size(68, 17);
            this.xselectedusername.TabIndex = 8;
            this.xselectedusername.Text = "Gebruiker";
            // 
            // xselecteduserimage
            // 
            this.xselecteduserimage.BackColor = System.Drawing.Color.Transparent;
            this.xselecteduserimage.Location = new System.Drawing.Point(3, 3);
            this.xselecteduserimage.Name = "xselecteduserimage";
            this.xselecteduserimage.Size = new System.Drawing.Size(64, 64);
            this.xselecteduserimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xselecteduserimage.TabIndex = 7;
            this.xselecteduserimage.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Productie Chat";
            // 
            // xchatpanel
            // 
            this.xchatpanel.AutoScroll = true;
            this.xchatpanel.BackColor = System.Drawing.Color.Transparent;
            this.xchatpanel.BaseStylesheet = null;
            this.xchatpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xchatpanel.Location = new System.Drawing.Point(3, 3);
            this.xchatpanel.Name = "xchatpanel";
            this.xchatpanel.Size = new System.Drawing.Size(546, 279);
            this.xchatpanel.TabIndex = 0;
            this.xchatpanel.Text = null;
            this.xchatpanel.LinkClicked += new System.EventHandler<HtmlRenderer.Entities.HtmlLinkClickedEventArgs>(this.xchatpanel_LinkClicked);
            this.xchatpanel.ImageLoad += new System.EventHandler<HtmlRenderer.Entities.HtmlImageLoadEventArgs>(this.xchatview_ImageLoad);
            this.xchatpanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.xchatpanel_DragDrop);
            this.xchatpanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.xchatpanel_DragEnter);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.xchatpanel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(250, 72);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 63F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(552, 348);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // ChatForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.AliceBlue;
            this.ClientSize = new System.Drawing.Size(802, 420);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.MinimumSize = new System.Drawing.Size(135, 163);
            this.Name = "ChatForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Productie Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.Load += new System.EventHandler(this.ChatForm_Load);
            this.Shown += new System.EventHandler(this.ChatForm_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xuserlist)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xprofilestatusimage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xprofileimage)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.menuStrip2.ResumeLayout(false);
            this.menuStrip2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xselecteduserstatusimage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xselecteduserimage)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox xchattextbox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox xprofileimage;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label xselecteduserstatus;
        private System.Windows.Forms.Label xselectedusername;
        private System.Windows.Forms.PictureBox xselecteduserimage;
        private System.Windows.Forms.Label xselecteduserdate;
        private System.Windows.Forms.PictureBox xselecteduserstatusimage;
        private System.Windows.Forms.ImageList xuserimages;
        private System.Windows.Forms.PictureBox xprofilestatusimage;
        private System.Windows.Forms.Label xprofilestatus;
        private System.Windows.Forms.Label xprofilenaam;
        private BrightIdeasSoftware.ObjectListView xuserlist;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button xsendbutton;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem wijzigProfielFotoToolStripMenuItem;
        private System.Windows.Forms.MenuStrip menuStrip2;
        private System.Windows.Forms.ToolStripMenuItem xselecteduserprofilebutton;
        private HtmlRenderer.HtmlPanel xchatpanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}