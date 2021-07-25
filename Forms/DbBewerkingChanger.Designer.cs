
namespace Forms
{
    partial class DbBewerkingChanger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DbBewerkingChanger));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xpleklist = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel4 = new System.Windows.Forms.Panel();
            this.xeditplek = new System.Windows.Forms.Button();
            this.xdelwerkplek = new System.Windows.Forms.Button();
            this.xaddwerkplek = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.xwerkpleknaam = new System.Windows.Forms.TextBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xbewlist = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.xeditbew = new System.Windows.Forms.Button();
            this.xaddbew = new System.Windows.Forms.Button();
            this.xdelbew = new System.Windows.Forms.Button();
            this.xisbemand = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.xbewerkingnaam = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.xannuleren = new System.Windows.Forms.Button();
            this.xOpslaan = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpleklist)).BeginInit();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xbewlist)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel5.SuspendLayout();
            this.panel6.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.xpleklist);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(380, 60);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(285, 281);
            this.panel1.TabIndex = 0;
            // 
            // xpleklist
            // 
            this.xpleklist.AllColumns.Add(this.olvColumn3);
            this.xpleklist.CellEditUseWholeCell = false;
            this.xpleklist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn3});
            this.xpleklist.Cursor = System.Windows.Forms.Cursors.Default;
            this.xpleklist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xpleklist.FullRowSelect = true;
            this.xpleklist.HideSelection = false;
            this.xpleklist.Location = new System.Drawing.Point(0, 78);
            this.xpleklist.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xpleklist.Name = "xpleklist";
            this.xpleklist.ShowGroups = false;
            this.xpleklist.ShowItemToolTips = true;
            this.xpleklist.Size = new System.Drawing.Size(285, 203);
            this.xpleklist.TabIndex = 2;
            this.xpleklist.UseCompatibleStateImageBehavior = false;
            this.xpleklist.UseExplorerTheme = true;
            this.xpleklist.UseHotItem = true;
            this.xpleklist.UseTranslucentHotItem = true;
            this.xpleklist.UseTranslucentSelection = true;
            this.xpleklist.View = System.Windows.Forms.View.Details;
            this.xpleklist.SelectedIndexChanged += new System.EventHandler(this.xpleklist_SelectedIndexChanged);
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Naam";
            this.olvColumn3.FillsFreeSpace = true;
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.IsTileViewColumn = true;
            this.olvColumn3.Text = "Naam";
            this.olvColumn3.ToolTipText = "Bewerking naam";
            this.olvColumn3.Width = 213;
            this.olvColumn3.WordWrap = true;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.xeditplek);
            this.panel4.Controls.Add(this.xdelwerkplek);
            this.panel4.Controls.Add(this.xaddwerkplek);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.xwerkpleknaam);
            this.panel4.Controls.Add(this.pictureBox2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(285, 78);
            this.panel4.TabIndex = 0;
            // 
            // xeditplek
            // 
            this.xeditplek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xeditplek.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xeditplek.Location = new System.Drawing.Point(198, 12);
            this.xeditplek.Name = "xeditplek";
            this.xeditplek.Size = new System.Drawing.Size(38, 38);
            this.xeditplek.TabIndex = 6;
            this.xeditplek.UseVisualStyleBackColor = true;
            this.xeditplek.Click += new System.EventHandler(this.xeditplek_Click);
            // 
            // xdelwerkplek
            // 
            this.xdelwerkplek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdelwerkplek.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdelwerkplek.Location = new System.Drawing.Point(242, 12);
            this.xdelwerkplek.Name = "xdelwerkplek";
            this.xdelwerkplek.Size = new System.Drawing.Size(38, 38);
            this.xdelwerkplek.TabIndex = 5;
            this.xdelwerkplek.UseVisualStyleBackColor = true;
            this.xdelwerkplek.Click += new System.EventHandler(this.xdelwerkplek_Click);
            // 
            // xaddwerkplek
            // 
            this.xaddwerkplek.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xaddwerkplek.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xaddwerkplek.Location = new System.Drawing.Point(154, 12);
            this.xaddwerkplek.Name = "xaddwerkplek";
            this.xaddwerkplek.Size = new System.Drawing.Size(38, 38);
            this.xaddwerkplek.TabIndex = 4;
            this.xaddwerkplek.UseVisualStyleBackColor = true;
            this.xaddwerkplek.Click += new System.EventHandler(this.xaddwerkplek_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(72, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Werkplek";
            // 
            // xwerkpleknaam
            // 
            this.xwerkpleknaam.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xwerkpleknaam.Location = new System.Drawing.Point(66, 53);
            this.xwerkpleknaam.Name = "xwerkpleknaam";
            this.xwerkpleknaam.Size = new System.Drawing.Size(219, 25);
            this.xwerkpleknaam.TabIndex = 2;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox2.Image = global::ProductieManager.Properties.Resources.iconfinder_technology;
            this.pictureBox2.Location = new System.Drawing.Point(0, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(66, 78);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox2.TabIndex = 1;
            this.pictureBox2.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.xbewlist);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(20, 60);
            this.panel2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(360, 281);
            this.panel2.TabIndex = 1;
            // 
            // xbewlist
            // 
            this.xbewlist.AllColumns.Add(this.olvColumn1);
            this.xbewlist.AllColumns.Add(this.olvColumn2);
            this.xbewlist.CellEditUseWholeCell = false;
            this.xbewlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.xbewlist.Cursor = System.Windows.Forms.Cursors.Default;
            this.xbewlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xbewlist.FullRowSelect = true;
            this.xbewlist.HideSelection = false;
            this.xbewlist.Location = new System.Drawing.Point(0, 78);
            this.xbewlist.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xbewlist.Name = "xbewlist";
            this.xbewlist.ShowGroups = false;
            this.xbewlist.ShowItemToolTips = true;
            this.xbewlist.Size = new System.Drawing.Size(360, 203);
            this.xbewlist.TabIndex = 1;
            this.xbewlist.UseCompatibleStateImageBehavior = false;
            this.xbewlist.UseExplorerTheme = true;
            this.xbewlist.UseHotItem = true;
            this.xbewlist.UseTranslucentHotItem = true;
            this.xbewlist.UseTranslucentSelection = true;
            this.xbewlist.View = System.Windows.Forms.View.Details;
            this.xbewlist.SelectedIndexChanged += new System.EventHandler(this.xbewlist_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Naam";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.Text = "Naam";
            this.olvColumn1.ToolTipText = "Bewerking naam";
            this.olvColumn1.Width = 200;
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn2
            // 
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "IsBemand";
            this.olvColumn2.ToolTipText = "Of de bewerking bemand is";
            this.olvColumn2.Width = 100;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xeditbew);
            this.panel3.Controls.Add(this.xaddbew);
            this.panel3.Controls.Add(this.xdelbew);
            this.panel3.Controls.Add(this.xisbemand);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.xbewerkingnaam);
            this.panel3.Controls.Add(this.pictureBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(360, 78);
            this.panel3.TabIndex = 0;
            // 
            // xeditbew
            // 
            this.xeditbew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xeditbew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xeditbew.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xeditbew.Location = new System.Drawing.Point(275, 12);
            this.xeditbew.Name = "xeditbew";
            this.xeditbew.Size = new System.Drawing.Size(38, 38);
            this.xeditbew.TabIndex = 7;
            this.xeditbew.UseVisualStyleBackColor = true;
            this.xeditbew.Click += new System.EventHandler(this.xeditbew_Click);
            // 
            // xaddbew
            // 
            this.xaddbew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xaddbew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xaddbew.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xaddbew.Location = new System.Drawing.Point(231, 12);
            this.xaddbew.Name = "xaddbew";
            this.xaddbew.Size = new System.Drawing.Size(38, 38);
            this.xaddbew.TabIndex = 8;
            this.xaddbew.UseVisualStyleBackColor = true;
            this.xaddbew.Click += new System.EventHandler(this.xaddbew_Click);
            // 
            // xdelbew
            // 
            this.xdelbew.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xdelbew.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdelbew.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdelbew.Location = new System.Drawing.Point(319, 12);
            this.xdelbew.Name = "xdelbew";
            this.xdelbew.Size = new System.Drawing.Size(38, 38);
            this.xdelbew.TabIndex = 7;
            this.xdelbew.UseVisualStyleBackColor = true;
            this.xdelbew.Click += new System.EventHandler(this.xdelbew_Click);
            // 
            // xisbemand
            // 
            this.xisbemand.AutoSize = true;
            this.xisbemand.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xisbemand.Location = new System.Drawing.Point(72, 9);
            this.xisbemand.Name = "xisbemand";
            this.xisbemand.Size = new System.Drawing.Size(87, 21);
            this.xisbemand.TabIndex = 6;
            this.xisbemand.Text = "IsBemand";
            this.xisbemand.UseVisualStyleBackColor = true;
            this.xisbemand.CheckedChanged += new System.EventHandler(this.xisbemand_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(72, 33);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 17);
            this.label2.TabIndex = 5;
            this.label2.Text = "Bewerking";
            // 
            // xbewerkingnaam
            // 
            this.xbewerkingnaam.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xbewerkingnaam.Location = new System.Drawing.Point(66, 53);
            this.xbewerkingnaam.Name = "xbewerkingnaam";
            this.xbewerkingnaam.Size = new System.Drawing.Size(294, 25);
            this.xbewerkingnaam.TabIndex = 4;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.operation;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(66, 78);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.panel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(20, 341);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(645, 48);
            this.panel5.TabIndex = 2;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.xannuleren);
            this.panel6.Controls.Add(this.xOpslaan);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel6.Location = new System.Drawing.Point(361, 0);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(284, 48);
            this.panel6.TabIndex = 0;
            // 
            // xannuleren
            // 
            this.xannuleren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xannuleren.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xannuleren.ForeColor = System.Drawing.Color.Black;
            this.xannuleren.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xannuleren.Location = new System.Drawing.Point(144, 6);
            this.xannuleren.Name = "xannuleren";
            this.xannuleren.Size = new System.Drawing.Size(135, 38);
            this.xannuleren.TabIndex = 4;
            this.xannuleren.Text = "Annuleren";
            this.xannuleren.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xannuleren.UseVisualStyleBackColor = true;
            this.xannuleren.Click += new System.EventHandler(this.xannuleren_Click);
            // 
            // xOpslaan
            // 
            this.xOpslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOpslaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOpslaan.ForeColor = System.Drawing.Color.Black;
            this.xOpslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xOpslaan.Location = new System.Drawing.Point(3, 6);
            this.xOpslaan.Name = "xOpslaan";
            this.xOpslaan.Size = new System.Drawing.Size(135, 38);
            this.xOpslaan.TabIndex = 5;
            this.xOpslaan.Text = "Opslaan";
            this.xOpslaan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xOpslaan.UseVisualStyleBackColor = true;
            this.xOpslaan.Click += new System.EventHandler(this.xOpslaan_Click);
            // 
            // DbBewerkingChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 409);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel5);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(685, 400);
            this.Name = "DbBewerkingChanger";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Bewerkingen En Werkplekken DB";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xpleklist)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xbewlist)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private BrightIdeasSoftware.ObjectListView xpleklist;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private BrightIdeasSoftware.ObjectListView xbewlist;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.Button xdelwerkplek;
        private System.Windows.Forms.Button xaddwerkplek;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xwerkpleknaam;
        private System.Windows.Forms.Button xaddbew;
        private System.Windows.Forms.Button xdelbew;
        private System.Windows.Forms.CheckBox xisbemand;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox xbewerkingnaam;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button xannuleren;
        private System.Windows.Forms.Button xOpslaan;
        private System.Windows.Forms.Button xeditplek;
        private System.Windows.Forms.Button xeditbew;
    }
}