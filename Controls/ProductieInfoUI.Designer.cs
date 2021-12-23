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
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.xHeaderHtmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.xInforHtmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.xNotitieHtmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.metroTabPage4 = new MetroFramework.Controls.MetroTabPage();
            this.xDatumsHtmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.metroTabPage6 = new MetroFramework.Controls.MetroTabPage();
            this.verpakkingInstructieUI1 = new Controls.VerpakkingInstructieUI();
            this.metroTabPage5 = new MetroFramework.Controls.MetroTabPage();
            this.xMaterialenHtmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.metroTabPage7 = new MetroFramework.Controls.MetroTabPage();
            this.xWerkPlaatsenHtmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.xaantalpanel = new System.Windows.Forms.Panel();
            this.xPacketGroup = new System.Windows.Forms.GroupBox();
            this.xaddPacket = new MetroFramework.Controls.MetroButton();
            this.xpacketlabel = new System.Windows.Forms.Label();
            this.xremovePacket = new MetroFramework.Controls.MetroButton();
            this.xpacketvalue = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.xwerkplekken = new MetroFramework.Controls.MetroComboBox();
            this.xnextb = new MetroFramework.Controls.MetroButton();
            this.xaantalgemaakt = new System.Windows.Forms.NumericUpDown();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            this.metroTabPage4.SuspendLayout();
            this.metroTabPage6.SuspendLayout();
            this.metroTabPage5.SuspendLayout();
            this.metroTabPage7.SuspendLayout();
            this.xaantalpanel.SuspendLayout();
            this.xPacketGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpacketvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalgemaakt)).BeginInit();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Controls.Add(this.metroTabPage4);
            this.metroTabControl1.Controls.Add(this.metroTabPage6);
            this.metroTabControl1.Controls.Add(this.metroTabPage5);
            this.metroTabControl1.Controls.Add(this.metroTabPage7);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(0, 0);
            this.metroTabControl1.Multiline = true;
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(685, 396);
            this.metroTabControl1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTabControl1.TabIndex = 12;
            this.metroTabControl1.UseSelectable = true;
            this.metroTabControl1.SelectedIndexChanged += new System.EventHandler(this.metroTabControl1_SelectedIndexChanged);
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.xHeaderHtmlPanel);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 2;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(677, 354);
            this.metroTabPage1.TabIndex = 6;
            this.metroTabPage1.Text = "Productie Status";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 3;
            // 
            // xHeaderHtmlPanel
            // 
            this.xHeaderHtmlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xHeaderHtmlPanel.AutoScroll = true;
            this.xHeaderHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(670, 20);
            this.xHeaderHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xHeaderHtmlPanel.BaseStylesheet = null;
            this.xHeaderHtmlPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.xHeaderHtmlPanel.IsContextMenuEnabled = false;
            this.xHeaderHtmlPanel.Location = new System.Drawing.Point(4, 4);
            this.xHeaderHtmlPanel.Name = "xHeaderHtmlPanel";
            this.xHeaderHtmlPanel.Size = new System.Drawing.Size(670, 347);
            this.xHeaderHtmlPanel.TabIndex = 0;
            this.xHeaderHtmlPanel.Text = "Header Html Text";
            this.xHeaderHtmlPanel.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.xInforHtmlPanel);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 2;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(677, 354);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Productie Info";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 3;
            // 
            // xInforHtmlPanel
            // 
            this.xInforHtmlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xInforHtmlPanel.AutoScroll = true;
            this.xInforHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(671, 20);
            this.xInforHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xInforHtmlPanel.BaseStylesheet = null;
            this.xInforHtmlPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.xInforHtmlPanel.IsContextMenuEnabled = false;
            this.xInforHtmlPanel.IsSelectionEnabled = false;
            this.xInforHtmlPanel.Location = new System.Drawing.Point(3, 3);
            this.xInforHtmlPanel.Name = "xInforHtmlPanel";
            this.xInforHtmlPanel.Size = new System.Drawing.Size(671, 348);
            this.xInforHtmlPanel.TabIndex = 1;
            this.xInforHtmlPanel.Text = "Info Html Text";
            this.xInforHtmlPanel.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.xNotitieHtmlPanel);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 2;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(677, 354);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Notities";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 3;
            // 
            // xNotitieHtmlPanel
            // 
            this.xNotitieHtmlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xNotitieHtmlPanel.AutoScroll = true;
            this.xNotitieHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(674, 20);
            this.xNotitieHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xNotitieHtmlPanel.BaseStylesheet = null;
            this.xNotitieHtmlPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.xNotitieHtmlPanel.IsContextMenuEnabled = false;
            this.xNotitieHtmlPanel.IsSelectionEnabled = false;
            this.xNotitieHtmlPanel.Location = new System.Drawing.Point(3, 3);
            this.xNotitieHtmlPanel.Name = "xNotitieHtmlPanel";
            this.xNotitieHtmlPanel.Size = new System.Drawing.Size(674, 348);
            this.xNotitieHtmlPanel.TabIndex = 11;
            this.xNotitieHtmlPanel.Text = "Notitie Html Text";
            this.xNotitieHtmlPanel.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            // 
            // metroTabPage4
            // 
            this.metroTabPage4.Controls.Add(this.xDatumsHtmlPanel);
            this.metroTabPage4.HorizontalScrollbarBarColor = true;
            this.metroTabPage4.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage4.HorizontalScrollbarSize = 2;
            this.metroTabPage4.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage4.Name = "metroTabPage4";
            this.metroTabPage4.Size = new System.Drawing.Size(677, 354);
            this.metroTabPage4.TabIndex = 7;
            this.metroTabPage4.Text = "Datums";
            this.metroTabPage4.VerticalScrollbarBarColor = true;
            this.metroTabPage4.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage4.VerticalScrollbarSize = 3;
            // 
            // xDatumsHtmlPanel
            // 
            this.xDatumsHtmlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xDatumsHtmlPanel.AutoScroll = true;
            this.xDatumsHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(671, 20);
            this.xDatumsHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xDatumsHtmlPanel.BaseStylesheet = null;
            this.xDatumsHtmlPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.xDatumsHtmlPanel.IsContextMenuEnabled = false;
            this.xDatumsHtmlPanel.IsSelectionEnabled = false;
            this.xDatumsHtmlPanel.Location = new System.Drawing.Point(3, 3);
            this.xDatumsHtmlPanel.Name = "xDatumsHtmlPanel";
            this.xDatumsHtmlPanel.Padding = new System.Windows.Forms.Padding(5);
            this.xDatumsHtmlPanel.Size = new System.Drawing.Size(671, 348);
            this.xDatumsHtmlPanel.TabIndex = 6;
            this.xDatumsHtmlPanel.Text = "Datums Html Text";
            this.xDatumsHtmlPanel.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            // 
            // metroTabPage6
            // 
            this.metroTabPage6.AutoScroll = true;
            this.metroTabPage6.Controls.Add(this.verpakkingInstructieUI1);
            this.metroTabPage6.HorizontalScrollbar = true;
            this.metroTabPage6.HorizontalScrollbarBarColor = true;
            this.metroTabPage6.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage6.HorizontalScrollbarSize = 2;
            this.metroTabPage6.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage6.Name = "metroTabPage6";
            this.metroTabPage6.Size = new System.Drawing.Size(677, 354);
            this.metroTabPage6.TabIndex = 5;
            this.metroTabPage6.Text = "Verpakking";
            this.metroTabPage6.VerticalScrollbar = true;
            this.metroTabPage6.VerticalScrollbarBarColor = true;
            this.metroTabPage6.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage6.VerticalScrollbarSize = 3;
            // 
            // verpakkingInstructieUI1
            // 
            this.verpakkingInstructieUI1.AllowEditMode = false;
            this.verpakkingInstructieUI1.AutoScroll = true;
            this.verpakkingInstructieUI1.BackColor = System.Drawing.Color.White;
            this.verpakkingInstructieUI1.BodyColor = System.Drawing.Color.Empty;
            this.verpakkingInstructieUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verpakkingInstructieUI1.IsEditmode = false;
            this.verpakkingInstructieUI1.Location = new System.Drawing.Point(0, 0);
            this.verpakkingInstructieUI1.Name = "verpakkingInstructieUI1";
            this.verpakkingInstructieUI1.Padding = new System.Windows.Forms.Padding(5);
            this.verpakkingInstructieUI1.Productie = null;
            this.verpakkingInstructieUI1.Size = new System.Drawing.Size(677, 354);
            this.verpakkingInstructieUI1.TabIndex = 2;
            this.verpakkingInstructieUI1.TextColor = System.Drawing.Color.Empty;
            this.verpakkingInstructieUI1.Title = null;
            // 
            // metroTabPage5
            // 
            this.metroTabPage5.Controls.Add(this.xMaterialenHtmlPanel);
            this.metroTabPage5.HorizontalScrollbarBarColor = true;
            this.metroTabPage5.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage5.HorizontalScrollbarSize = 2;
            this.metroTabPage5.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage5.Name = "metroTabPage5";
            this.metroTabPage5.Size = new System.Drawing.Size(677, 354);
            this.metroTabPage5.TabIndex = 8;
            this.metroTabPage5.Text = "Materialen";
            this.metroTabPage5.VerticalScrollbarBarColor = true;
            this.metroTabPage5.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage5.VerticalScrollbarSize = 3;
            // 
            // xMaterialenHtmlPanel
            // 
            this.xMaterialenHtmlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xMaterialenHtmlPanel.AutoScroll = true;
            this.xMaterialenHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(671, 20);
            this.xMaterialenHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xMaterialenHtmlPanel.BaseStylesheet = null;
            this.xMaterialenHtmlPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.xMaterialenHtmlPanel.IsContextMenuEnabled = false;
            this.xMaterialenHtmlPanel.IsSelectionEnabled = false;
            this.xMaterialenHtmlPanel.Location = new System.Drawing.Point(3, 3);
            this.xMaterialenHtmlPanel.Name = "xMaterialenHtmlPanel";
            this.xMaterialenHtmlPanel.Size = new System.Drawing.Size(671, 348);
            this.xMaterialenHtmlPanel.TabIndex = 7;
            this.xMaterialenHtmlPanel.Text = "Materialen Html Text";
            this.xMaterialenHtmlPanel.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            // 
            // metroTabPage7
            // 
            this.metroTabPage7.Controls.Add(this.xWerkPlaatsenHtmlPanel);
            this.metroTabPage7.HorizontalScrollbarBarColor = true;
            this.metroTabPage7.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage7.HorizontalScrollbarSize = 2;
            this.metroTabPage7.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage7.Name = "metroTabPage7";
            this.metroTabPage7.Size = new System.Drawing.Size(677, 354);
            this.metroTabPage7.TabIndex = 9;
            this.metroTabPage7.Text = "WerkPlaatsen";
            this.metroTabPage7.VerticalScrollbarBarColor = true;
            this.metroTabPage7.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage7.VerticalScrollbarSize = 3;
            // 
            // xWerkPlaatsenHtmlPanel
            // 
            this.xWerkPlaatsenHtmlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xWerkPlaatsenHtmlPanel.AutoScroll = true;
            this.xWerkPlaatsenHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(671, 20);
            this.xWerkPlaatsenHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xWerkPlaatsenHtmlPanel.BaseStylesheet = null;
            this.xWerkPlaatsenHtmlPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.xWerkPlaatsenHtmlPanel.IsContextMenuEnabled = false;
            this.xWerkPlaatsenHtmlPanel.IsSelectionEnabled = false;
            this.xWerkPlaatsenHtmlPanel.Location = new System.Drawing.Point(3, 3);
            this.xWerkPlaatsenHtmlPanel.Name = "xWerkPlaatsenHtmlPanel";
            this.xWerkPlaatsenHtmlPanel.Size = new System.Drawing.Size(671, 349);
            this.xWerkPlaatsenHtmlPanel.TabIndex = 9;
            this.xWerkPlaatsenHtmlPanel.Text = "WerkPlaatsen Html Text";
            this.xWerkPlaatsenHtmlPanel.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            // 
            // xaantalpanel
            // 
            this.xaantalpanel.Controls.Add(this.xPacketGroup);
            this.xaantalpanel.Controls.Add(this.pictureBox1);
            this.xaantalpanel.Controls.Add(this.panel5);
            this.xaantalpanel.Controls.Add(this.xnextb);
            this.xaantalpanel.Controls.Add(this.xaantalgemaakt);
            this.xaantalpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xaantalpanel.Location = new System.Drawing.Point(0, 396);
            this.xaantalpanel.Name = "xaantalpanel";
            this.xaantalpanel.Size = new System.Drawing.Size(685, 100);
            this.xaantalpanel.TabIndex = 13;
            this.xaantalpanel.Visible = false;
            // 
            // xPacketGroup
            // 
            this.xPacketGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xPacketGroup.Controls.Add(this.xaddPacket);
            this.xPacketGroup.Controls.Add(this.xpacketlabel);
            this.xPacketGroup.Controls.Add(this.xremovePacket);
            this.xPacketGroup.Controls.Add(this.xpacketvalue);
            this.xPacketGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xPacketGroup.Location = new System.Drawing.Point(74, 42);
            this.xPacketGroup.Name = "xPacketGroup";
            this.xPacketGroup.Size = new System.Drawing.Size(410, 55);
            this.xPacketGroup.TabIndex = 9;
            this.xPacketGroup.TabStop = false;
            this.xPacketGroup.Text = "Aantal Kisten, Bakken, Dozen of pakketten";
            this.xPacketGroup.Visible = false;
            // 
            // xaddPacket
            // 
            this.xaddPacket.BackgroundImage = global::ProductieManager.Properties.Resources.add_icon_icons_com_52393;
            this.xaddPacket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.xaddPacket.Location = new System.Drawing.Point(142, 19);
            this.xaddPacket.Name = "xaddPacket";
            this.xaddPacket.Size = new System.Drawing.Size(34, 29);
            this.xaddPacket.TabIndex = 14;
            this.xaddPacket.UseSelectable = true;
            this.xaddPacket.Click += new System.EventHandler(this.xaddPacket_Click);
            // 
            // xpacketlabel
            // 
            this.xpacketlabel.AutoSize = true;
            this.xpacketlabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xpacketlabel.Location = new System.Drawing.Point(196, 21);
            this.xpacketlabel.Name = "xpacketlabel";
            this.xpacketlabel.Size = new System.Drawing.Size(56, 21);
            this.xpacketlabel.TabIndex = 13;
            this.xpacketlabel.Text = "Aantal";
            // 
            // xremovePacket
            // 
            this.xremovePacket.BackgroundImage = global::ProductieManager.Properties.Resources.minusflat_105990;
            this.xremovePacket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.xremovePacket.Location = new System.Drawing.Point(106, 19);
            this.xremovePacket.Name = "xremovePacket";
            this.xremovePacket.Size = new System.Drawing.Size(34, 29);
            this.xremovePacket.TabIndex = 12;
            this.xremovePacket.UseSelectable = true;
            this.xremovePacket.Click += new System.EventHandler(this.xremovePacket_Click);
            // 
            // xpacketvalue
            // 
            this.xpacketvalue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xpacketvalue.Location = new System.Drawing.Point(10, 19);
            this.xpacketvalue.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xpacketvalue.Name = "xpacketvalue";
            this.xpacketvalue.Size = new System.Drawing.Size(94, 29);
            this.xpacketvalue.TabIndex = 9;
            this.xpacketvalue.ValueChanged += new System.EventHandler(this.Xpacketvalue_ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.Count_tool_34564__1_;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(68, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.xwerkplekken);
            this.panel5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(74, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(316, 37);
            this.panel5.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "WerkPlek";
            // 
            // xwerkplekken
            // 
            this.xwerkplekken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xwerkplekken.FormattingEnabled = true;
            this.xwerkplekken.ItemHeight = 23;
            this.xwerkplekken.Location = new System.Drawing.Point(84, 4);
            this.xwerkplekken.Name = "xwerkplekken";
            this.xwerkplekken.Size = new System.Drawing.Size(229, 29);
            this.xwerkplekken.TabIndex = 5;
            this.xwerkplekken.UseSelectable = true;
            this.xwerkplekken.SelectedIndexChanged += new System.EventHandler(this.xwerkplekken_SelectedIndexChanged);
            // 
            // xnextb
            // 
            this.xnextb.BackgroundImage = global::ProductieManager.Properties.Resources.arrow_right_15600;
            this.xnextb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.xnextb.Location = new System.Drawing.Point(518, 6);
            this.xnextb.Name = "xnextb";
            this.xnextb.Size = new System.Drawing.Size(50, 29);
            this.xnextb.TabIndex = 7;
            this.xnextb.UseSelectable = true;
            this.xnextb.Click += new System.EventHandler(this.xnextb_Click);
            // 
            // xaantalgemaakt
            // 
            this.xaantalgemaakt.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantalgemaakt.Location = new System.Drawing.Point(396, 6);
            this.xaantalgemaakt.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantalgemaakt.Name = "xaantalgemaakt";
            this.xaantalgemaakt.Size = new System.Drawing.Size(120, 29);
            this.xaantalgemaakt.TabIndex = 4;
            this.xaantalgemaakt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xaantalgemaakt_KeyDown);
            // 
            // ProductieInfoUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.xaantalpanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProductieInfoUI";
            this.Size = new System.Drawing.Size(685, 496);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage4.ResumeLayout(false);
            this.metroTabPage6.ResumeLayout(false);
            this.metroTabPage5.ResumeLayout(false);
            this.metroTabPage7.ResumeLayout(false);
            this.xaantalpanel.ResumeLayout(false);
            this.xPacketGroup.ResumeLayout(false);
            this.xPacketGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpacketvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalgemaakt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private HtmlPanel xWerkPlaatsenHtmlPanel;
        private HtmlPanel xMaterialenHtmlPanel;
        private HtmlPanel xNotitieHtmlPanel;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroTabPage metroTabPage6;
        private HtmlPanel xDatumsHtmlPanel;
        private HtmlPanel xInforHtmlPanel;
        private HtmlPanel xHeaderHtmlPanel;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage4;
        private MetroFramework.Controls.MetroTabPage metroTabPage5;
        private MetroFramework.Controls.MetroTabPage metroTabPage7;
        private System.Windows.Forms.Panel xaantalpanel;
        private System.Windows.Forms.GroupBox xPacketGroup;
        private MetroFramework.Controls.MetroButton xaddPacket;
        private System.Windows.Forms.Label xpacketlabel;
        private MetroFramework.Controls.MetroButton xremovePacket;
        private System.Windows.Forms.NumericUpDown xpacketvalue;
        private MetroFramework.Controls.MetroButton xnextb;
        private System.Windows.Forms.NumericUpDown xaantalgemaakt;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroComboBox xwerkplekken;
        private System.Windows.Forms.PictureBox pictureBox1;
        private VerpakkingInstructieUI verpakkingInstructieUI1;
    }
}
