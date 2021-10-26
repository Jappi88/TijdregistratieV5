namespace Forms.Excel
{
    partial class KleurRegelsForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.xTextKleur = new System.Windows.Forms.CheckBox();
            this.xWijzigKleur = new System.Windows.Forms.Button();
            this.xcolorPanel = new System.Windows.Forms.Panel();
            this.xKiesKleur = new System.Windows.Forms.Button();
            this.xRegelTextPanel = new HtmlRenderer.HtmlPanel();
            this.xRegelView = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xanuleren = new System.Windows.Forms.Button();
            this.xok = new System.Windows.Forms.Button();
            this.xoptiespanel = new System.Windows.Forms.ToolStrip();
            this.xAddOptieButton = new System.Windows.Forms.ToolStripButton();
            this.xEditOpties = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.xDeleteOptieButton = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xRegelView)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.xoptiespanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xoptiespanel);
            this.panel1.Controls.Add(this.xWijzigKleur);
            this.panel1.Controls.Add(this.xTextKleur);
            this.panel1.Controls.Add(this.xcolorPanel);
            this.panel1.Controls.Add(this.xKiesKleur);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(10, 60);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(480, 39);
            this.panel1.TabIndex = 0;
            // 
            // xTextKleur
            // 
            this.xTextKleur.AutoSize = true;
            this.xTextKleur.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xTextKleur.Location = new System.Drawing.Point(136, 12);
            this.xTextKleur.Name = "xTextKleur";
            this.xTextKleur.Size = new System.Drawing.Size(85, 21);
            this.xTextKleur.TabIndex = 4;
            this.xTextKleur.Text = "TextKleur";
            this.xTextKleur.UseVisualStyleBackColor = true;
            this.xTextKleur.CheckedChanged += new System.EventHandler(this.xTextKleur_CheckedChanged);
            // 
            // xWijzigKleur
            // 
            this.xWijzigKleur.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xWijzigKleur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xWijzigKleur.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xWijzigKleur.Location = new System.Drawing.Point(380, 4);
            this.xWijzigKleur.Margin = new System.Windows.Forms.Padding(4);
            this.xWijzigKleur.Name = "xWijzigKleur";
            this.xWijzigKleur.Size = new System.Drawing.Size(96, 30);
            this.xWijzigKleur.TabIndex = 3;
            this.xWijzigKleur.Text = "Wijzig Kleur";
            this.xWijzigKleur.UseVisualStyleBackColor = true;
            this.xWijzigKleur.Click += new System.EventHandler(this.EditColor_Click);
            // 
            // xcolorPanel
            // 
            this.xcolorPanel.Location = new System.Drawing.Point(3, 5);
            this.xcolorPanel.Name = "xcolorPanel";
            this.xcolorPanel.Size = new System.Drawing.Size(31, 30);
            this.xcolorPanel.TabIndex = 2;
            // 
            // xKiesKleur
            // 
            this.xKiesKleur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xKiesKleur.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xKiesKleur.Location = new System.Drawing.Point(41, 5);
            this.xKiesKleur.Margin = new System.Windows.Forms.Padding(4);
            this.xKiesKleur.Name = "xKiesKleur";
            this.xKiesKleur.Size = new System.Drawing.Size(88, 30);
            this.xKiesKleur.TabIndex = 1;
            this.xKiesKleur.Text = "Kies Kleur";
            this.xKiesKleur.UseVisualStyleBackColor = true;
            this.xKiesKleur.Click += new System.EventHandler(this.xKiesKleur_Click);
            // 
            // xRegelTextPanel
            // 
            this.xRegelTextPanel.AutoScroll = true;
            this.xRegelTextPanel.AutoScrollMinSize = new System.Drawing.Size(480, 17);
            this.xRegelTextPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xRegelTextPanel.BaseStylesheet = null;
            this.xRegelTextPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.xRegelTextPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xRegelTextPanel.Location = new System.Drawing.Point(0, 0);
            this.xRegelTextPanel.Margin = new System.Windows.Forms.Padding(4);
            this.xRegelTextPanel.Name = "xRegelTextPanel";
            this.xRegelTextPanel.Size = new System.Drawing.Size(480, 58);
            this.xRegelTextPanel.TabIndex = 1;
            this.xRegelTextPanel.Text = "Regel Text";
            // 
            // xRegelView
            // 
            this.xRegelView.AllColumns.Add(this.olvColumn1);
            this.xRegelView.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.xRegelView.CellEditUseWholeCell = false;
            this.xRegelView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.xRegelView.Cursor = System.Windows.Forms.Cursors.Default;
            this.xRegelView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xRegelView.FullRowSelect = true;
            this.xRegelView.HideSelection = false;
            this.xRegelView.LargeImageList = this.imageList1;
            this.xRegelView.Location = new System.Drawing.Point(0, 0);
            this.xRegelView.Name = "xRegelView";
            this.xRegelView.ShowGroups = false;
            this.xRegelView.ShowItemToolTips = true;
            this.xRegelView.ShowSortIndicators = false;
            this.xRegelView.Size = new System.Drawing.Size(480, 139);
            this.xRegelView.SmallImageList = this.imageList1;
            this.xRegelView.TabIndex = 2;
            this.xRegelView.UseAlternatingBackColors = true;
            this.xRegelView.UseCompatibleStateImageBehavior = false;
            this.xRegelView.UseExplorerTheme = true;
            this.xRegelView.UseHotItem = true;
            this.xRegelView.UseTranslucentHotItem = true;
            this.xRegelView.UseTranslucentSelection = true;
            this.xRegelView.View = System.Windows.Forms.View.Details;
            this.xRegelView.SelectedIndexChanged += new System.EventHandler(this.xRegelView_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.Text = "Regels";
            this.olvColumn1.Width = 421;
            this.olvColumn1.WordWrap = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(10, 300);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(480, 40);
            this.panel2.TabIndex = 10;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xanuleren);
            this.panel3.Controls.Add(this.xok);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(228, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(252, 40);
            this.panel3.TabIndex = 3;
            // 
            // xanuleren
            // 
            this.xanuleren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xanuleren.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xanuleren.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xanuleren.Location = new System.Drawing.Point(129, 1);
            this.xanuleren.Name = "xanuleren";
            this.xanuleren.Size = new System.Drawing.Size(120, 38);
            this.xanuleren.TabIndex = 3;
            this.xanuleren.Text = "&Annuleren";
            this.xanuleren.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xanuleren.UseVisualStyleBackColor = true;
            this.xanuleren.Click += new System.EventHandler(this.xanuleren_Click);
            // 
            // xok
            // 
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xok.Location = new System.Drawing.Point(3, 1);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(120, 38);
            this.xok.TabIndex = 2;
            this.xok.Text = "&OK";
            this.xok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // xoptiespanel
            // 
            this.xoptiespanel.AutoSize = false;
            this.xoptiespanel.BackColor = System.Drawing.Color.White;
            this.xoptiespanel.Dock = System.Windows.Forms.DockStyle.None;
            this.xoptiespanel.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xAddOptieButton,
            this.xEditOpties,
            this.toolStripSeparator3,
            this.xDeleteOptieButton});
            this.xoptiespanel.Location = new System.Drawing.Point(227, 1);
            this.xoptiespanel.Name = "xoptiespanel";
            this.xoptiespanel.Size = new System.Drawing.Size(142, 38);
            this.xoptiespanel.TabIndex = 9;
            this.xoptiespanel.Text = "toolStrip2";
            // 
            // xAddOptieButton
            // 
            this.xAddOptieButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xAddOptieButton.Image = global::ProductieManager.Properties.Resources.add_Blue_circle_32x32;
            this.xAddOptieButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xAddOptieButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xAddOptieButton.Name = "xAddOptieButton";
            this.xAddOptieButton.Size = new System.Drawing.Size(36, 35);
            this.xAddOptieButton.Text = "toolStripButton1";
            this.xAddOptieButton.ToolTipText = "Maak nieuwe instellingen";
            this.xAddOptieButton.Click += new System.EventHandler(this.xAddOptieButton_Click);
            // 
            // xEditOpties
            // 
            this.xEditOpties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xEditOpties.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xEditOpties.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xEditOpties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xEditOpties.Name = "xEditOpties";
            this.xEditOpties.Size = new System.Drawing.Size(36, 35);
            this.xEditOpties.Text = "toolStripButton2";
            this.xEditOpties.ToolTipText = "Wijzig geselecteerde optie";
            this.xEditOpties.Click += new System.EventHandler(this.xEditOpties_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 38);
            // 
            // xDeleteOptieButton
            // 
            this.xDeleteOptieButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xDeleteOptieButton.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xDeleteOptieButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xDeleteOptieButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xDeleteOptieButton.Name = "xDeleteOptieButton";
            this.xDeleteOptieButton.Size = new System.Drawing.Size(36, 35);
            this.xDeleteOptieButton.Text = "toolStripButton2";
            this.xDeleteOptieButton.ToolTipText = "Verwijderer selectie";
            this.xDeleteOptieButton.Click += new System.EventHandler(this.xDeleteOptieButton_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(10, 99);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.xRegelView);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.xRegelTextPanel);
            this.splitContainer1.Size = new System.Drawing.Size(480, 201);
            this.splitContainer1.SplitterDistance = 139;
            this.splitContainer1.TabIndex = 11;
            // 
            // KleurRegelsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 350);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(500, 350);
            this.Name = "KleurRegelsForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Kleur Regels";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xRegelView)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.xoptiespanel.ResumeLayout(false);
            this.xoptiespanel.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xKiesKleur;
        private HtmlRenderer.HtmlPanel xRegelTextPanel;
        private BrightIdeasSoftware.ObjectListView xRegelView;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button xanuleren;
        private System.Windows.Forms.Button xok;
        private System.Windows.Forms.ToolStrip xoptiespanel;
        private System.Windows.Forms.ToolStripButton xAddOptieButton;
        private System.Windows.Forms.ToolStripButton xEditOpties;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton xDeleteOptieButton;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel xcolorPanel;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Button xWijzigKleur;
        private System.Windows.Forms.CheckBox xTextKleur;
    }
}