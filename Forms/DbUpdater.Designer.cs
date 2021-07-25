
namespace Forms
{
    partial class DbUpdater
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbUpdater));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.xdbentrylist = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xdbentrypanel = new System.Windows.Forms.Panel();
            this.xvanafdate = new System.Windows.Forms.DateTimePicker();
            this.label6 = new System.Windows.Forms.Label();
            this.xnaam = new System.Windows.Forms.TextBox();
            this.xaccounttype = new System.Windows.Forms.CheckBox();
            this.xinstellingtype = new System.Windows.Forms.CheckBox();
            this.xupdate = new System.Windows.Forms.Button();
            this.xprodtype = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.xperstype = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.xinterval = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.xaautoupdate = new System.Windows.Forms.CheckBox();
            this.xupdatewhenstartup = new System.Windows.Forms.CheckBox();
            this.xlocatie = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.xpath = new System.Windows.Forms.TextBox();
            this.xchoosepath = new System.Windows.Forms.Button();
            this.xaddb = new System.Windows.Forms.Button();
            this.xdeleteb = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.xstatus = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xannuleren = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xdbentrylist)).BeginInit();
            this.xdbentrypanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xinterval)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.download_database_21022_128_128;
            this.pictureBox1.Location = new System.Drawing.Point(20, 60);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(134, 497);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel5);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.xstatus);
            this.panel1.Controls.Add(this.progressBar1);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(154, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(847, 497);
            this.panel1.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel5.Controls.Add(this.xdbentrylist);
            this.panel5.Controls.Add(this.xdbentrypanel);
            this.panel5.Location = new System.Drawing.Point(6, 68);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(829, 336);
            this.panel5.TabIndex = 11;
            // 
            // xdbentrylist
            // 
            this.xdbentrylist.AllColumns.Add(this.olvColumn5);
            this.xdbentrylist.AllColumns.Add(this.olvColumn1);
            this.xdbentrylist.AllColumns.Add(this.olvColumn4);
            this.xdbentrylist.AllColumns.Add(this.olvColumn2);
            this.xdbentrylist.CellEditUseWholeCell = false;
            this.xdbentrylist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn5,
            this.olvColumn1,
            this.olvColumn4,
            this.olvColumn2});
            this.xdbentrylist.Cursor = System.Windows.Forms.Cursors.Default;
            this.xdbentrylist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xdbentrylist.FullRowSelect = true;
            this.xdbentrylist.HideSelection = false;
            this.xdbentrylist.LargeImageList = this.imageList1;
            this.xdbentrylist.Location = new System.Drawing.Point(0, 0);
            this.xdbentrylist.Name = "xdbentrylist";
            this.xdbentrylist.ShowGroups = false;
            this.xdbentrylist.ShowItemToolTips = true;
            this.xdbentrylist.Size = new System.Drawing.Size(585, 336);
            this.xdbentrylist.SmallImageList = this.imageList1;
            this.xdbentrylist.TabIndex = 9;
            this.xdbentrylist.UseCompatibleStateImageBehavior = false;
            this.xdbentrylist.UseExplorerTheme = true;
            this.xdbentrylist.UseHotItem = true;
            this.xdbentrylist.UseTranslucentHotItem = true;
            this.xdbentrylist.UseTranslucentSelection = true;
            this.xdbentrylist.View = System.Windows.Forms.View.Details;
            this.xdbentrylist.SelectedIndexChanged += new System.EventHandler(this.xdbentrylist_SelectedIndexChanged);
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "Naam";
            this.olvColumn5.Text = "Naam";
            this.olvColumn5.Width = 138;
            this.olvColumn5.WordWrap = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "UpdatePath";
            this.olvColumn1.Text = "Locatie";
            this.olvColumn1.Width = 292;
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn4
            // 
            this.olvColumn4.Text = "Update Met Opstarten";
            this.olvColumn4.Width = 148;
            this.olvColumn4.WordWrap = true;
            // 
            // olvColumn2
            // 
            this.olvColumn2.Text = "AutoUpdate";
            this.olvColumn2.Width = 84;
            this.olvColumn2.WordWrap = true;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "filesystems_network_server_database_630.png");
            // 
            // xdbentrypanel
            // 
            this.xdbentrypanel.AutoScroll = true;
            this.xdbentrypanel.Controls.Add(this.xvanafdate);
            this.xdbentrypanel.Controls.Add(this.label6);
            this.xdbentrypanel.Controls.Add(this.xnaam);
            this.xdbentrypanel.Controls.Add(this.xaccounttype);
            this.xdbentrypanel.Controls.Add(this.xinstellingtype);
            this.xdbentrypanel.Controls.Add(this.xupdate);
            this.xdbentrypanel.Controls.Add(this.xprodtype);
            this.xdbentrypanel.Controls.Add(this.label5);
            this.xdbentrypanel.Controls.Add(this.xperstype);
            this.xdbentrypanel.Controls.Add(this.label4);
            this.xdbentrypanel.Controls.Add(this.xinterval);
            this.xdbentrypanel.Controls.Add(this.label3);
            this.xdbentrypanel.Controls.Add(this.xaautoupdate);
            this.xdbentrypanel.Controls.Add(this.xupdatewhenstartup);
            this.xdbentrypanel.Controls.Add(this.xlocatie);
            this.xdbentrypanel.Controls.Add(this.label1);
            this.xdbentrypanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.xdbentrypanel.Location = new System.Drawing.Point(585, 0);
            this.xdbentrypanel.Name = "xdbentrypanel";
            this.xdbentrypanel.Size = new System.Drawing.Size(244, 336);
            this.xdbentrypanel.TabIndex = 10;
            this.xdbentrypanel.Visible = false;
            // 
            // xvanafdate
            // 
            this.xvanafdate.Location = new System.Drawing.Point(3, 347);
            this.xvanafdate.Name = "xvanafdate";
            this.xvanafdate.Size = new System.Drawing.Size(222, 25);
            this.xvanafdate.TabIndex = 14;
            this.xvanafdate.ValueChanged += new System.EventHandler(this.xvanafdate_ValueChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(1, 68);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(105, 17);
            this.label6.TabIndex = 13;
            this.label6.Text = "Database Naam";
            // 
            // xnaam
            // 
            this.xnaam.Location = new System.Drawing.Point(4, 88);
            this.xnaam.Name = "xnaam";
            this.xnaam.Size = new System.Drawing.Size(172, 25);
            this.xnaam.TabIndex = 12;
            this.xnaam.TextChanged += new System.EventHandler(this.xnaam_TextChanged);
            // 
            // xaccounttype
            // 
            this.xaccounttype.Location = new System.Drawing.Point(3, 300);
            this.xaccounttype.Name = "xaccounttype";
            this.xaccounttype.Size = new System.Drawing.Size(211, 24);
            this.xaccounttype.TabIndex = 11;
            this.xaccounttype.Text = "Accounts";
            this.xaccounttype.UseVisualStyleBackColor = true;
            this.xaccounttype.CheckedChanged += new System.EventHandler(this.xdbchecktype_CheckedChanged);
            // 
            // xinstellingtype
            // 
            this.xinstellingtype.Location = new System.Drawing.Point(3, 270);
            this.xinstellingtype.Name = "xinstellingtype";
            this.xinstellingtype.Size = new System.Drawing.Size(211, 24);
            this.xinstellingtype.TabIndex = 10;
            this.xinstellingtype.Text = "Instellingen";
            this.xinstellingtype.UseVisualStyleBackColor = true;
            this.xinstellingtype.CheckedChanged += new System.EventHandler(this.xdbchecktype_CheckedChanged);
            // 
            // xupdate
            // 
            this.xupdate.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xupdate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xupdate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xupdate.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.xupdate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xupdate.Location = new System.Drawing.Point(0, 372);
            this.xupdate.Name = "xupdate";
            this.xupdate.Size = new System.Drawing.Size(227, 38);
            this.xupdate.TabIndex = 7;
            this.xupdate.Text = "Update";
            this.xupdate.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xupdate.UseVisualStyleBackColor = true;
            this.xupdate.Click += new System.EventHandler(this.xupdate_Click);
            // 
            // xprodtype
            // 
            this.xprodtype.Location = new System.Drawing.Point(3, 240);
            this.xprodtype.Name = "xprodtype";
            this.xprodtype.Size = new System.Drawing.Size(211, 24);
            this.xprodtype.TabIndex = 9;
            this.xprodtype.Text = "Producties";
            this.xprodtype.UseVisualStyleBackColor = true;
            this.xprodtype.CheckedChanged += new System.EventHandler(this.xdbchecktype_CheckedChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(6, 190);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(157, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Kies wat u wilt updaten:";
            // 
            // xperstype
            // 
            this.xperstype.Location = new System.Drawing.Point(3, 210);
            this.xperstype.Name = "xperstype";
            this.xperstype.Size = new System.Drawing.Size(211, 24);
            this.xperstype.TabIndex = 7;
            this.xperstype.Text = "Personeel";
            this.xperstype.UseVisualStyleBackColor = true;
            this.xperstype.CheckedChanged += new System.EventHandler(this.xdbchecktype_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(189, 164);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Min";
            // 
            // xinterval
            // 
            this.xinterval.Location = new System.Drawing.Point(139, 162);
            this.xinterval.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xinterval.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xinterval.Name = "xinterval";
            this.xinterval.Size = new System.Drawing.Size(44, 25);
            this.xinterval.TabIndex = 5;
            this.xinterval.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.xinterval.ValueChanged += new System.EventHandler(this.xinterval_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 327);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(102, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Vanaf Periode: ";
            // 
            // xaautoupdate
            // 
            this.xaautoupdate.Location = new System.Drawing.Point(4, 163);
            this.xaautoupdate.Name = "xaautoupdate";
            this.xaautoupdate.Size = new System.Drawing.Size(172, 24);
            this.xaautoupdate.TabIndex = 3;
            this.xaautoupdate.Text = "Automatich Update";
            this.xaautoupdate.UseVisualStyleBackColor = true;
            this.xaautoupdate.CheckedChanged += new System.EventHandler(this.xaautoupdate_CheckedChanged);
            // 
            // xupdatewhenstartup
            // 
            this.xupdatewhenstartup.Location = new System.Drawing.Point(4, 119);
            this.xupdatewhenstartup.Name = "xupdatewhenstartup";
            this.xupdatewhenstartup.Size = new System.Drawing.Size(172, 43);
            this.xupdatewhenstartup.TabIndex = 2;
            this.xupdatewhenstartup.Text = "Update zodra programma is opgestart";
            this.xupdatewhenstartup.UseVisualStyleBackColor = true;
            this.xupdatewhenstartup.CheckedChanged += new System.EventHandler(this.xupdatewhenstartup_CheckedChanged);
            // 
            // xlocatie
            // 
            this.xlocatie.Dock = System.Windows.Forms.DockStyle.Top;
            this.xlocatie.Location = new System.Drawing.Point(0, 20);
            this.xlocatie.Name = "xlocatie";
            this.xlocatie.Size = new System.Drawing.Size(227, 48);
            this.xlocatie.TabIndex = 1;
            this.xlocatie.Text = "Locatie";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Top;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Wijzig Gegevens";
            // 
            // panel4
            // 
            this.panel4.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel4.Controls.Add(this.xpath);
            this.panel4.Controls.Add(this.xchoosepath);
            this.panel4.Controls.Add(this.xaddb);
            this.panel4.Controls.Add(this.xdeleteb);
            this.panel4.Location = new System.Drawing.Point(6, 36);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(829, 30);
            this.panel4.TabIndex = 10;
            // 
            // xpath
            // 
            this.xpath.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xpath.Location = new System.Drawing.Point(0, 0);
            this.xpath.Multiline = true;
            this.xpath.Name = "xpath";
            this.xpath.Size = new System.Drawing.Size(736, 30);
            this.xpath.TabIndex = 4;
            // 
            // xchoosepath
            // 
            this.xchoosepath.Dock = System.Windows.Forms.DockStyle.Right;
            this.xchoosepath.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xchoosepath.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xchoosepath.Location = new System.Drawing.Point(736, 0);
            this.xchoosepath.Name = "xchoosepath";
            this.xchoosepath.Size = new System.Drawing.Size(33, 30);
            this.xchoosepath.TabIndex = 5;
            this.xchoosepath.Text = "...";
            this.xchoosepath.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.xchoosepath.UseVisualStyleBackColor = true;
            this.xchoosepath.Click += new System.EventHandler(this.xchoosepath_Click);
            // 
            // xaddb
            // 
            this.xaddb.Dock = System.Windows.Forms.DockStyle.Right;
            this.xaddb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xaddb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaddb.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xaddb.Location = new System.Drawing.Point(769, 0);
            this.xaddb.Name = "xaddb";
            this.xaddb.Size = new System.Drawing.Size(30, 30);
            this.xaddb.TabIndex = 8;
            this.xaddb.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.xaddb.UseVisualStyleBackColor = true;
            this.xaddb.Click += new System.EventHandler(this.xaddb_Click);
            // 
            // xdeleteb
            // 
            this.xdeleteb.Dock = System.Windows.Forms.DockStyle.Right;
            this.xdeleteb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdeleteb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdeleteb.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdeleteb.Location = new System.Drawing.Point(799, 0);
            this.xdeleteb.Name = "xdeleteb";
            this.xdeleteb.Size = new System.Drawing.Size(30, 30);
            this.xdeleteb.TabIndex = 9;
            this.xdeleteb.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.xdeleteb.UseVisualStyleBackColor = true;
            this.xdeleteb.Click += new System.EventHandler(this.xdeleteb_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(2, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(141, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "Update Database";
            // 
            // xstatus
            // 
            this.xstatus.AutoSize = true;
            this.xstatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xstatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatus.Location = new System.Drawing.Point(0, 407);
            this.xstatus.Name = "xstatus";
            this.xstatus.Size = new System.Drawing.Size(74, 17);
            this.xstatus.TabIndex = 3;
            this.xstatus.Text = "Update Db";
            // 
            // progressBar1
            // 
            this.progressBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.progressBar1.Location = new System.Drawing.Point(0, 424);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(847, 30);
            this.progressBar1.TabIndex = 2;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 454);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(847, 43);
            this.panel2.TabIndex = 1;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xannuleren);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(726, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(121, 43);
            this.panel3.TabIndex = 0;
            // 
            // xannuleren
            // 
            this.xannuleren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xannuleren.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xannuleren.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xannuleren.Location = new System.Drawing.Point(4, 2);
            this.xannuleren.Name = "xannuleren";
            this.xannuleren.Size = new System.Drawing.Size(113, 38);
            this.xannuleren.TabIndex = 0;
            this.xannuleren.Text = "&Sluiten";
            this.xannuleren.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xannuleren.UseVisualStyleBackColor = true;
            this.xannuleren.Click += new System.EventHandler(this.xannuleren_Click);
            // 
            // DbUpdater
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1021, 577);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DbUpdater";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Update Database";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DbUpdater_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xdbentrylist)).EndInit();
            this.xdbentrypanel.ResumeLayout(false);
            this.xdbentrypanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xinterval)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xupdate;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button xchoosepath;
        private System.Windows.Forms.TextBox xpath;
        private System.Windows.Forms.Label xstatus;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button xannuleren;
        private BrightIdeasSoftware.ObjectListView xdbentrylist;
        private System.Windows.Forms.Button xaddb;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel xdbentrypanel;
        private System.Windows.Forms.CheckBox xaccounttype;
        private System.Windows.Forms.CheckBox xinstellingtype;
        private System.Windows.Forms.CheckBox xprodtype;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox xperstype;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown xinterval;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox xaautoupdate;
        private System.Windows.Forms.CheckBox xupdatewhenstartup;
        private System.Windows.Forms.Label xlocatie;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel4;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.Button xdeleteb;
        private System.Windows.Forms.ImageList imageList1;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox xnaam;
        private System.Windows.Forms.DateTimePicker xvanafdate;
    }
}