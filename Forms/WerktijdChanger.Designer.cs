
namespace Forms
{
    partial class WerktijdChanger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WerktijdChanger));
            this.xwerktijden = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.roosterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.verwijderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xstatuslabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xentryrooster = new System.Windows.Forms.Button();
            this.xaddextratime = new System.Windows.Forms.Button();
            this.xpasaan = new System.Windows.Forms.Button();
            this.xuurgewerkt = new System.Windows.Forms.Label();
            this.xgestoptlabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xstopdate = new System.Windows.Forms.DateTimePicker();
            this.xstartdate = new System.Windows.Forms.DateTimePicker();
            this.xaddb = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xdeleteb = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xroosterb = new System.Windows.Forms.Button();
            this.xspeciaalroosterb = new System.Windows.Forms.Button();
            this.xcancelb = new System.Windows.Forms.Button();
            this.xokb = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.xwerktijden)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // xwerktijden
            // 
            this.xwerktijden.AllColumns.Add(this.olvColumn5);
            this.xwerktijden.AllColumns.Add(this.olvColumn6);
            this.xwerktijden.AllColumns.Add(this.olvColumn7);
            this.xwerktijden.AllColumns.Add(this.olvColumn2);
            this.xwerktijden.AllColumns.Add(this.olvColumn1);
            this.xwerktijden.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xwerktijden.CellEditUseWholeCell = false;
            this.xwerktijden.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn5,
            this.olvColumn6,
            this.olvColumn7,
            this.olvColumn2,
            this.olvColumn1});
            this.xwerktijden.ContextMenuStrip = this.contextMenuStrip1;
            this.xwerktijden.Cursor = System.Windows.Forms.Cursors.Default;
            this.xwerktijden.FullRowSelect = true;
            this.xwerktijden.HideSelection = false;
            this.xwerktijden.Location = new System.Drawing.Point(20, 187);
            this.xwerktijden.Name = "xwerktijden";
            this.xwerktijden.ShowGroups = false;
            this.xwerktijden.Size = new System.Drawing.Size(610, 188);
            this.xwerktijden.TabIndex = 3;
            this.xwerktijden.UseCompatibleStateImageBehavior = false;
            this.xwerktijden.UseExplorerTheme = true;
            this.xwerktijden.UseHotItem = true;
            this.xwerktijden.UseTranslucentHotItem = true;
            this.xwerktijden.UseTranslucentSelection = true;
            this.xwerktijden.View = System.Windows.Forms.View.Details;
            this.xwerktijden.ButtonClick += new System.EventHandler<BrightIdeasSoftware.CellClickEventArgs>(this.xwerktijden_ButtonClick);
            this.xwerktijden.SelectedIndexChanged += new System.EventHandler(this.xwerktijden_SelectedIndexChanged);
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "Gestart";
            this.olvColumn5.IsEditable = false;
            this.olvColumn5.IsTileViewColumn = true;
            this.olvColumn5.Text = "Gestart Op";
            this.olvColumn5.Width = 150;
            this.olvColumn5.WordWrap = true;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "Gestopt";
            this.olvColumn6.IsEditable = false;
            this.olvColumn6.IsTileViewColumn = true;
            this.olvColumn6.Text = "Gestopt Op";
            this.olvColumn6.Width = 150;
            this.olvColumn6.WordWrap = true;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "TotaalTijd";
            this.olvColumn7.IsEditable = false;
            this.olvColumn7.IsTileViewColumn = true;
            this.olvColumn7.Text = "Totaal Tijd";
            this.olvColumn7.Width = 77;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "";
            this.olvColumn2.ButtonPadding = new System.Drawing.Size(2, 2);
            this.olvColumn2.ButtonSizing = BrightIdeasSoftware.OLVColumn.ButtonSizingMode.CellBounds;
            this.olvColumn2.IsButton = true;
            this.olvColumn2.Text = "Aangepast Rooster";
            this.olvColumn2.ToolTipText = "Of tijdlijn een afwijken rooster heeft ";
            this.olvColumn2.Width = 125;
            this.olvColumn2.WordWrap = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "InUse";
            this.olvColumn1.Text = "Actief";
            this.olvColumn1.ToolTipText = "Of de werktijd momenteel word gebruikt.";
            this.olvColumn1.Width = 80;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.roosterToolStripMenuItem,
            this.toolStripSeparator1,
            this.verwijderToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(184, 86);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // roosterToolStripMenuItem
            // 
            this.roosterToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.schedule_32_32;
            this.roosterToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.roosterToolStripMenuItem.Name = "roosterToolStripMenuItem";
            this.roosterToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.roosterToolStripMenuItem.Size = new System.Drawing.Size(183, 38);
            this.roosterToolStripMenuItem.Text = "Rooster";
            this.roosterToolStripMenuItem.Click += new System.EventHandler(this.roosterToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(180, 6);
            // 
            // verwijderToolStripMenuItem
            // 
            this.verwijderToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.verwijderToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.verwijderToolStripMenuItem.Name = "verwijderToolStripMenuItem";
            this.verwijderToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.verwijderToolStripMenuItem.Size = new System.Drawing.Size(183, 38);
            this.verwijderToolStripMenuItem.Text = "Verwijder";
            this.verwijderToolStripMenuItem.Click += new System.EventHandler(this.verwijderToolStripMenuItem_Click);
            // 
            // xstatuslabel
            // 
            this.xstatuslabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xstatuslabel.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatuslabel.Location = new System.Drawing.Point(277, 4);
            this.xstatuslabel.Name = "xstatuslabel";
            this.xstatuslabel.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.xstatuslabel.Size = new System.Drawing.Size(330, 28);
            this.xstatuslabel.TabIndex = 4;
            this.xstatuslabel.Text = "Totaal tijd gewerkt";
            this.xstatuslabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xentryrooster);
            this.panel1.Controls.Add(this.xaddextratime);
            this.panel1.Controls.Add(this.xpasaan);
            this.panel1.Controls.Add(this.xuurgewerkt);
            this.panel1.Controls.Add(this.xgestoptlabel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.xstopdate);
            this.panel1.Controls.Add(this.xstartdate);
            this.panel1.Controls.Add(this.xstatuslabel);
            this.panel1.Controls.Add(this.xaddb);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.xdeleteb);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(610, 121);
            this.panel1.TabIndex = 5;
            // 
            // xentryrooster
            // 
            this.xentryrooster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xentryrooster.Enabled = false;
            this.xentryrooster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xentryrooster.Image = global::ProductieManager.Properties.Resources.schedule_32_32;
            this.xentryrooster.Location = new System.Drawing.Point(446, 79);
            this.xentryrooster.Name = "xentryrooster";
            this.xentryrooster.Size = new System.Drawing.Size(38, 38);
            this.xentryrooster.TabIndex = 10;
            this.toolTip1.SetToolTip(this.xentryrooster, "Ras rooster aan van de geselecteerde tijd regel");
            this.xentryrooster.UseVisualStyleBackColor = true;
            this.xentryrooster.Click += new System.EventHandler(this.xentryrooster_Click);
            // 
            // xaddextratime
            // 
            this.xaddextratime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xaddextratime.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xaddextratime.Image = global::ProductieManager.Properties.Resources.Time_machine__40675;
            this.xaddextratime.Location = new System.Drawing.Point(446, 35);
            this.xaddextratime.Name = "xaddextratime";
            this.xaddextratime.Size = new System.Drawing.Size(38, 38);
            this.xaddextratime.TabIndex = 9;
            this.toolTip1.SetToolTip(this.xaddextratime, "Voeg  extra tijd toe buiten om de werkrooster");
            this.xaddextratime.UseVisualStyleBackColor = true;
            this.xaddextratime.Click += new System.EventHandler(this.xaddextratime_Click);
            // 
            // xpasaan
            // 
            this.xpasaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xpasaan.Enabled = false;
            this.xpasaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xpasaan.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xpasaan.Location = new System.Drawing.Point(402, 79);
            this.xpasaan.Name = "xpasaan";
            this.xpasaan.Size = new System.Drawing.Size(38, 38);
            this.xpasaan.TabIndex = 8;
            this.toolTip1.SetToolTip(this.xpasaan, "Pas geselecteerde werktijd aan");
            this.xpasaan.UseVisualStyleBackColor = true;
            this.xpasaan.Click += new System.EventHandler(this.xpasaan_Click);
            // 
            // xuurgewerkt
            // 
            this.xuurgewerkt.AutoSize = true;
            this.xuurgewerkt.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xuurgewerkt.Location = new System.Drawing.Point(324, 72);
            this.xuurgewerkt.Name = "xuurgewerkt";
            this.xuurgewerkt.Size = new System.Drawing.Size(72, 17);
            this.xuurgewerkt.TabIndex = 7;
            this.xuurgewerkt.Text = "Totaal Uur";
            // 
            // xgestoptlabel
            // 
            this.xgestoptlabel.AutoSize = true;
            this.xgestoptlabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xgestoptlabel.Location = new System.Drawing.Point(135, 72);
            this.xgestoptlabel.Name = "xgestoptlabel";
            this.xgestoptlabel.Size = new System.Drawing.Size(78, 17);
            this.xgestoptlabel.TabIndex = 6;
            this.xgestoptlabel.Text = "Gestopt Op";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(138, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "Gestart Op";
            // 
            // xstopdate
            // 
            this.xstopdate.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xstopdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstopdate.Location = new System.Drawing.Point(138, 92);
            this.xstopdate.Name = "xstopdate";
            this.xstopdate.Size = new System.Drawing.Size(258, 25);
            this.xstopdate.TabIndex = 4;
            this.xstopdate.ValueChanged += new System.EventHandler(this.xstopdate_ValueChanged);
            // 
            // xstartdate
            // 
            this.xstartdate.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xstartdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstartdate.Location = new System.Drawing.Point(138, 44);
            this.xstartdate.Name = "xstartdate";
            this.xstartdate.Size = new System.Drawing.Size(258, 25);
            this.xstartdate.TabIndex = 3;
            this.xstartdate.ValueChanged += new System.EventHandler(this.xstartdate_ValueChanged);
            // 
            // xaddb
            // 
            this.xaddb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xaddb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xaddb.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xaddb.Location = new System.Drawing.Point(402, 35);
            this.xaddb.Name = "xaddb";
            this.xaddb.Size = new System.Drawing.Size(38, 38);
            this.xaddb.TabIndex = 2;
            this.toolTip1.SetToolTip(this.xaddb, "Voeg nieuwe werktijd toe");
            this.xaddb.UseVisualStyleBackColor = true;
            this.xaddb.Click += new System.EventHandler(this.xaddb_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.business_color_progress_128_128;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 121);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 1;
            this.pictureBox1.TabStop = false;
            // 
            // xdeleteb
            // 
            this.xdeleteb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.xdeleteb.Enabled = false;
            this.xdeleteb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdeleteb.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdeleteb.Location = new System.Drawing.Point(490, 79);
            this.xdeleteb.Name = "xdeleteb";
            this.xdeleteb.Size = new System.Drawing.Size(38, 38);
            this.xdeleteb.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xdeleteb, "Verwijder geselecteerde tijden");
            this.xdeleteb.UseVisualStyleBackColor = true;
            this.xdeleteb.Click += new System.EventHandler(this.xdeleteb_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Werktijd";
            // 
            // xroosterb
            // 
            this.xroosterb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xroosterb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xroosterb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xroosterb.Image = global::ProductieManager.Properties.Resources.schedule_32_32;
            this.xroosterb.Location = new System.Drawing.Point(50, 381);
            this.xroosterb.Name = "xroosterb";
            this.xroosterb.Size = new System.Drawing.Size(140, 38);
            this.xroosterb.TabIndex = 10;
            this.xroosterb.Text = "Werk Rooster";
            this.xroosterb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xroosterb, resources.GetString("xroosterb.ToolTip"));
            this.xroosterb.UseVisualStyleBackColor = true;
            this.xroosterb.Click += new System.EventHandler(this.xroosterb_Click);
            // 
            // xspeciaalroosterb
            // 
            this.xspeciaalroosterb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xspeciaalroosterb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xspeciaalroosterb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xspeciaalroosterb.Image = global::ProductieManager.Properties.Resources.augmented_reality_calendar_schedule_mountain_32x32;
            this.xspeciaalroosterb.Location = new System.Drawing.Point(196, 381);
            this.xspeciaalroosterb.Name = "xspeciaalroosterb";
            this.xspeciaalroosterb.Size = new System.Drawing.Size(158, 38);
            this.xspeciaalroosterb.TabIndex = 11;
            this.xspeciaalroosterb.Text = "Speciale Roosters";
            this.xspeciaalroosterb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xspeciaalroosterb, "Beheer speciale roosters.\r\nSpecial roosters zijn roosters die vallen buiten om de" +
        " normale werkdagen.");
            this.xspeciaalroosterb.UseVisualStyleBackColor = true;
            this.xspeciaalroosterb.Click += new System.EventHandler(this.xspeciaalroosterb_Click);
            // 
            // xcancelb
            // 
            this.xcancelb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xcancelb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xcancelb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xcancelb.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xcancelb.Location = new System.Drawing.Point(510, 381);
            this.xcancelb.Name = "xcancelb";
            this.xcancelb.Size = new System.Drawing.Size(120, 38);
            this.xcancelb.TabIndex = 7;
            this.xcancelb.Text = "&Sluiten";
            this.xcancelb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xcancelb, "Annuleer wijzigingen en sluiten");
            this.xcancelb.UseVisualStyleBackColor = true;
            this.xcancelb.Click += new System.EventHandler(this.xcancelb_Click);
            // 
            // xokb
            // 
            this.xokb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xokb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xokb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xokb.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xokb.Location = new System.Drawing.Point(384, 381);
            this.xokb.Name = "xokb";
            this.xokb.Size = new System.Drawing.Size(120, 38);
            this.xokb.TabIndex = 6;
            this.xokb.Text = "&Opslaan";
            this.xokb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xokb, "Sla wijzigingen op en sluit scherm");
            this.xokb.UseVisualStyleBackColor = true;
            this.xokb.Click += new System.EventHandler(this.xokb_Click);
            // 
            // WerktijdChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 425);
            this.Controls.Add(this.xroosterb);
            this.Controls.Add(this.xspeciaalroosterb);
            this.Controls.Add(this.xcancelb);
            this.Controls.Add(this.xokb);
            this.Controls.Add(this.xwerktijden);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(650, 425);
            this.Name = "WerktijdChanger";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Pas Werktijd Aan";
            ((System.ComponentModel.ISupportInitialize)(this.xwerktijden)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private BrightIdeasSoftware.ObjectListView xwerktijden;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private System.Windows.Forms.Label xstatuslabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button xdeleteb;
        private System.Windows.Forms.Label xgestoptlabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker xstopdate;
        private System.Windows.Forms.DateTimePicker xstartdate;
        private System.Windows.Forms.Button xaddb;
        private System.Windows.Forms.Button xokb;
        private System.Windows.Forms.Button xcancelb;
        private System.Windows.Forms.Label xuurgewerkt;
        private System.Windows.Forms.Button xpasaan;
        private System.Windows.Forms.ToolTip toolTip1;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.Button xaddextratime;
        private System.Windows.Forms.Button xroosterb;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem roosterToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem verwijderToolStripMenuItem;
        private System.Windows.Forms.Button xspeciaalroosterb;
        private System.Windows.Forms.Button xentryrooster;
    }
}