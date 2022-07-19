namespace Forms.GereedMelden
{
    partial class GereedMelder
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GereedMelder));
            this.xgroup = new System.Windows.Forms.GroupBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.productieInfoUI1 = new Controls.ProductieInfoUI();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xafkeur = new System.Windows.Forms.Button();
            this.xgereedlijst = new System.Windows.Forms.Button();
            this.xnotitie = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.xparaaf = new System.Windows.Forms.TextBox();
            this.xaantal = new System.Windows.Forms.NumericUpDown();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xdeelsgereed = new System.Windows.Forms.Button();
            this.xannuleerb = new System.Windows.Forms.Button();
            this.xgereedb = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xgroup.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xaantal)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xgroup
            // 
            this.xgroup.BackColor = System.Drawing.Color.White;
            this.xgroup.Controls.Add(this.panel4);
            this.xgroup.Controls.Add(this.panel1);
            this.xgroup.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xgroup.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xgroup.ForeColor = System.Drawing.Color.Green;
            this.xgroup.Location = new System.Drawing.Point(20, 60);
            this.xgroup.Margin = new System.Windows.Forms.Padding(4);
            this.xgroup.Name = "xgroup";
            this.xgroup.Padding = new System.Windows.Forms.Padding(4);
            this.xgroup.Size = new System.Drawing.Size(814, 470);
            this.xgroup.TabIndex = 1;
            this.xgroup.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.productieInfoUI1);
            this.panel4.Controls.Add(this.panel3);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(4, 26);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(806, 388);
            this.panel4.TabIndex = 13;
            // 
            // productieInfoUI1
            // 
            this.productieInfoUI1.AllowVerpakkingEdit = false;
            this.productieInfoUI1.AutoScroll = true;
            this.productieInfoUI1.BackColor = System.Drawing.Color.White;
            this.productieInfoUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieInfoUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieInfoUI1.Location = new System.Drawing.Point(0, 0);
            this.productieInfoUI1.Margin = new System.Windows.Forms.Padding(4);
            this.productieInfoUI1.Name = "productieInfoUI1";
            this.productieInfoUI1.Padding = new System.Windows.Forms.Padding(5);
            this.productieInfoUI1.ShowAantal = false;
            this.productieInfoUI1.Size = new System.Drawing.Size(806, 317);
            this.productieInfoUI1.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xafkeur);
            this.panel3.Controls.Add(this.xgereedlijst);
            this.panel3.Controls.Add(this.xnotitie);
            this.panel3.Controls.Add(this.label1);
            this.panel3.Controls.Add(this.label3);
            this.panel3.Controls.Add(this.xparaaf);
            this.panel3.Controls.Add(this.xaantal);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel3.Location = new System.Drawing.Point(0, 317);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(806, 71);
            this.panel3.TabIndex = 8;
            // 
            // xafkeur
            // 
            this.xafkeur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xafkeur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xafkeur.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xafkeur.ForeColor = System.Drawing.Color.Black;
            this.xafkeur.Image = global::ProductieManager.Properties.Resources.bin_icon_icons_com_32x32;
            this.xafkeur.Location = new System.Drawing.Point(469, 16);
            this.xafkeur.Name = "xafkeur";
            this.xafkeur.Size = new System.Drawing.Size(126, 42);
            this.xafkeur.TabIndex = 8;
            this.xafkeur.Text = "Afkeur";
            this.xafkeur.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xafkeur, "Meld aantal afkeur");
            this.xafkeur.UseVisualStyleBackColor = true;
            this.xafkeur.Click += new System.EventHandler(this.xafkeur_Click);
            // 
            // xgereedlijst
            // 
            this.xgereedlijst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xgereedlijst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xgereedlijst.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xgereedlijst.ForeColor = System.Drawing.Color.Black;
            this.xgereedlijst.Image = global::ProductieManager.Properties.Resources.ic_done_all_128_28243;
            this.xgereedlijst.Location = new System.Drawing.Point(601, 16);
            this.xgereedlijst.Name = "xgereedlijst";
            this.xgereedlijst.Size = new System.Drawing.Size(201, 42);
            this.xgereedlijst.TabIndex = 6;
            this.xgereedlijst.Text = "Gereed Meldingen";
            this.xgereedlijst.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xgereedlijst, "Bekijk alle deels gereed meldingen");
            this.xgereedlijst.UseVisualStyleBackColor = true;
            this.xgereedlijst.Click += new System.EventHandler(this.xgereedlijst_Click);
            // 
            // xnotitie
            // 
            this.xnotitie.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xnotitie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xnotitie.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xnotitie.ForeColor = System.Drawing.Color.Black;
            this.xnotitie.Image = global::ProductieManager.Properties.Resources.Note_34576_32x32;
            this.xnotitie.Location = new System.Drawing.Point(337, 16);
            this.xnotitie.Name = "xnotitie";
            this.xnotitie.Size = new System.Drawing.Size(126, 42);
            this.xnotitie.TabIndex = 4;
            this.xnotitie.Text = "Notitie";
            this.xnotitie.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xnotitie, "Voeg een gereed notitie toe");
            this.xnotitie.UseVisualStyleBackColor = true;
            this.xnotitie.Click += new System.EventHandler(this.xnotitie_Click);
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(207, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(124, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Aantal Gemaakt";
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(57, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(55, 21);
            this.label3.TabIndex = 5;
            this.label3.Text = "Paraaf";
            // 
            // xparaaf
            // 
            this.xparaaf.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xparaaf.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xparaaf.Location = new System.Drawing.Point(57, 29);
            this.xparaaf.Name = "xparaaf";
            this.xparaaf.Size = new System.Drawing.Size(147, 29);
            this.xparaaf.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xparaaf, "Vul je naam in");
            this.xparaaf.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GereedMelder_KeyDown);
            // 
            // xaantal
            // 
            this.xaantal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xaantal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantal.Location = new System.Drawing.Point(211, 29);
            this.xaantal.Margin = new System.Windows.Forms.Padding(4);
            this.xaantal.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantal.Name = "xaantal";
            this.xaantal.Size = new System.Drawing.Size(111, 29);
            this.xaantal.TabIndex = 1;
            this.toolTip1.SetToolTip(this.xaantal, "Vul de aantal gemaakt");
            this.xaantal.KeyDown += new System.Windows.Forms.KeyEventHandler(this.GereedMelder_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(4, 414);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(806, 52);
            this.panel1.TabIndex = 7;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xdeelsgereed);
            this.panel2.Controls.Add(this.xannuleerb);
            this.panel2.Controls.Add(this.xgereedb);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(223, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(583, 52);
            this.panel2.TabIndex = 7;
            // 
            // xdeelsgereed
            // 
            this.xdeelsgereed.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdeelsgereed.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdeelsgereed.ForeColor = System.Drawing.Color.Black;
            this.xdeelsgereed.Image = global::ProductieManager.Properties.Resources.ic_done_128_28244;
            this.xdeelsgereed.Location = new System.Drawing.Point(3, 6);
            this.xdeelsgereed.Name = "xdeelsgereed";
            this.xdeelsgereed.Size = new System.Drawing.Size(230, 42);
            this.xdeelsgereed.TabIndex = 4;
            this.xdeelsgereed.Text = "Deels Gereed Melden";
            this.xdeelsgereed.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xdeelsgereed, "Meld aantal deels gereed");
            this.xdeelsgereed.UseVisualStyleBackColor = true;
            this.xdeelsgereed.Click += new System.EventHandler(this.xdeelsgereed_Click);
            // 
            // xannuleerb
            // 
            this.xannuleerb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xannuleerb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xannuleerb.ForeColor = System.Drawing.Color.Black;
            this.xannuleerb.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xannuleerb.Location = new System.Drawing.Point(429, 6);
            this.xannuleerb.Name = "xannuleerb";
            this.xannuleerb.Size = new System.Drawing.Size(150, 42);
            this.xannuleerb.TabIndex = 3;
            this.xannuleerb.Text = "Annuleer";
            this.xannuleerb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xannuleerb, "Annuleer gereedmelding");
            this.xannuleerb.UseVisualStyleBackColor = true;
            this.xannuleerb.Click += new System.EventHandler(this.button2_Click);
            // 
            // xgereedb
            // 
            this.xgereedb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xgereedb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xgereedb.ForeColor = System.Drawing.Color.Black;
            this.xgereedb.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xgereedb.Location = new System.Drawing.Point(240, 6);
            this.xgereedb.Name = "xgereedb";
            this.xgereedb.Size = new System.Drawing.Size(183, 42);
            this.xgereedb.TabIndex = 2;
            this.xgereedb.Text = "Gereed Melden!";
            this.xgereedb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xgereedb, "Meld productie gereed");
            this.xgereedb.UseVisualStyleBackColor = true;
            this.xgereedb.Click += new System.EventHandler(this.button1_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Gereed Melden";
            // 
            // GereedMelder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(854, 550);
            this.Controls.Add(this.xgroup);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(800, 550);
            this.Name = "GereedMelder";
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Meld Gereed";
            this.Title = "Meld Gereed";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.GereedMelder_FormClosing);
            this.Shown += new System.EventHandler(this.GereedMelder_Shown);
            this.VisibleChanged += new System.EventHandler(this.GereedMelder_VisibleChanged);
            this.xgroup.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xaantal)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox xgroup;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox xparaaf;
        private System.Windows.Forms.NumericUpDown xaantal;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xannuleerb;
        private System.Windows.Forms.Button xgereedb;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button xnotitie;
        private System.Windows.Forms.Button xdeelsgereed;
        private System.Windows.Forms.Button xgereedlijst;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button xafkeur;
        private Controls.ProductieInfoUI productieInfoUI1;
    }
}