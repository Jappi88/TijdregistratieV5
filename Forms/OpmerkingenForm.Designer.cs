
namespace Forms
{
    partial class OpmerkingenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpmerkingenForm));
            this.xopmerkingimages = new System.Windows.Forms.ImageList(this.components);
            this.panel6 = new System.Windows.Forms.Panel();
            this.xannuleren = new System.Windows.Forms.Button();
            this.xOpslaan = new System.Windows.Forms.Button();
            this.xselectedopmerkingpanel = new System.Windows.Forms.Panel();
            this.xwijzigbutton = new System.Windows.Forms.Button();
            this.xaansluitencheckbox = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.xreactietextbox = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xopmerkingtextbox = new System.Windows.Forms.TextBox();
            this.xselectedopmerkinglabel = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.xOpmerkingenTree = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.wijzigToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.verwijderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.vouwAllesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ontvouwAllesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xaddtoolstripbutton = new System.Windows.Forms.ToolStripButton();
            this.xdeletetoolstripbutton = new System.Windows.Forms.ToolStripButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panel6.SuspendLayout();
            this.xselectedopmerkingpanel.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xopmerkingimages
            // 
            this.xopmerkingimages.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.xopmerkingimages.ImageSize = new System.Drawing.Size(32, 32);
            this.xopmerkingimages.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.xannuleren);
            this.panel6.Controls.Add(this.xOpslaan);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(10, 505);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(884, 44);
            this.panel6.TabIndex = 1;
            // 
            // xannuleren
            // 
            this.xannuleren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xannuleren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xannuleren.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xannuleren.ForeColor = System.Drawing.Color.Black;
            this.xannuleren.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xannuleren.Location = new System.Drawing.Point(746, 3);
            this.xannuleren.Name = "xannuleren";
            this.xannuleren.Size = new System.Drawing.Size(135, 38);
            this.xannuleren.TabIndex = 4;
            this.xannuleren.Text = "Sluiten";
            this.xannuleren.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xannuleren, "Sluit venster");
            this.xannuleren.UseVisualStyleBackColor = true;
            this.xannuleren.Click += new System.EventHandler(this.xannuleren_Click);
            // 
            // xOpslaan
            // 
            this.xOpslaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xOpslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOpslaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOpslaan.ForeColor = System.Drawing.Color.Black;
            this.xOpslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xOpslaan.Location = new System.Drawing.Point(605, 3);
            this.xOpslaan.Name = "xOpslaan";
            this.xOpslaan.Size = new System.Drawing.Size(135, 38);
            this.xOpslaan.TabIndex = 5;
            this.xOpslaan.Text = "Opslaan";
            this.xOpslaan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xOpslaan.UseVisualStyleBackColor = true;
            this.xOpslaan.Click += new System.EventHandler(this.xOpslaan_Click);
            // 
            // xselectedopmerkingpanel
            // 
            this.xselectedopmerkingpanel.AutoScroll = true;
            this.xselectedopmerkingpanel.Controls.Add(this.xwijzigbutton);
            this.xselectedopmerkingpanel.Controls.Add(this.xaansluitencheckbox);
            this.xselectedopmerkingpanel.Controls.Add(this.groupBox2);
            this.xselectedopmerkingpanel.Controls.Add(this.groupBox1);
            this.xselectedopmerkingpanel.Controls.Add(this.xselectedopmerkinglabel);
            this.xselectedopmerkingpanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.xselectedopmerkingpanel.Location = new System.Drawing.Point(655, 60);
            this.xselectedopmerkingpanel.Name = "xselectedopmerkingpanel";
            this.xselectedopmerkingpanel.Size = new System.Drawing.Size(239, 445);
            this.xselectedopmerkingpanel.TabIndex = 3;
            this.xselectedopmerkingpanel.Visible = false;
            // 
            // xwijzigbutton
            // 
            this.xwijzigbutton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xwijzigbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xwijzigbutton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xwijzigbutton.ForeColor = System.Drawing.Color.Black;
            this.xwijzigbutton.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.xwijzigbutton.Location = new System.Drawing.Point(3, 351);
            this.xwijzigbutton.Name = "xwijzigbutton";
            this.xwijzigbutton.Size = new System.Drawing.Size(233, 38);
            this.xwijzigbutton.TabIndex = 6;
            this.xwijzigbutton.Text = "Wijzigingen Toepassen";
            this.xwijzigbutton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xwijzigbutton, "Pas de wijzigingen toe");
            this.xwijzigbutton.UseVisualStyleBackColor = true;
            this.xwijzigbutton.Visible = false;
            this.xwijzigbutton.Click += new System.EventHandler(this.xwijzigbutton_Click);
            // 
            // xaansluitencheckbox
            // 
            this.xaansluitencheckbox.AutoSize = true;
            this.xaansluitencheckbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaansluitencheckbox.Location = new System.Drawing.Point(6, 324);
            this.xaansluitencheckbox.Name = "xaansluitencheckbox";
            this.xaansluitencheckbox.Size = new System.Drawing.Size(129, 21);
            this.xaansluitencheckbox.TabIndex = 2;
            this.xaansluitencheckbox.Text = "Sluit Hierbij Aan";
            this.toolTip1.SetToolTip(this.xaansluitencheckbox, "Selecteer als je wilt aansluiten op deze opmerking");
            this.xaansluitencheckbox.UseVisualStyleBackColor = true;
            this.xaansluitencheckbox.CheckedChanged += new System.EventHandler(this.xaansluitencheckbox_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.xreactietextbox);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(0, 196);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(239, 122);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Reactie";
            // 
            // xreactietextbox
            // 
            this.xreactietextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xreactietextbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xreactietextbox.Location = new System.Drawing.Point(3, 21);
            this.xreactietextbox.Multiline = true;
            this.xreactietextbox.Name = "xreactietextbox";
            this.xreactietextbox.Size = new System.Drawing.Size(233, 98);
            this.xreactietextbox.TabIndex = 0;
            this.xreactietextbox.TextChanged += new System.EventHandler(this.xreactietextbox_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xopmerkingtextbox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 39);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(239, 157);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Opmerking";
            // 
            // xopmerkingtextbox
            // 
            this.xopmerkingtextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xopmerkingtextbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xopmerkingtextbox.Location = new System.Drawing.Point(3, 21);
            this.xopmerkingtextbox.Multiline = true;
            this.xopmerkingtextbox.Name = "xopmerkingtextbox";
            this.xopmerkingtextbox.Size = new System.Drawing.Size(233, 133);
            this.xopmerkingtextbox.TabIndex = 0;
            this.xopmerkingtextbox.TextChanged += new System.EventHandler(this.xopmerkingtextbox_TextChanged);
            // 
            // xselectedopmerkinglabel
            // 
            this.xselectedopmerkinglabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xselectedopmerkinglabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xselectedopmerkinglabel.Location = new System.Drawing.Point(0, 0);
            this.xselectedopmerkinglabel.Name = "xselectedopmerkinglabel";
            this.xselectedopmerkinglabel.Size = new System.Drawing.Size(239, 39);
            this.xselectedopmerkinglabel.TabIndex = 7;
            this.xselectedopmerkinglabel.Text = "Opmerking Van: ";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xOpmerkingenTree);
            this.panel1.Controls.Add(this.toolStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(10, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(645, 445);
            this.panel1.TabIndex = 4;
            // 
            // xOpmerkingenTree
            // 
            this.xOpmerkingenTree.ContextMenuStrip = this.contextMenuStrip1;
            this.xOpmerkingenTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xOpmerkingenTree.FullRowSelect = true;
            this.xOpmerkingenTree.ImageIndex = 0;
            this.xOpmerkingenTree.ImageList = this.xopmerkingimages;
            this.xOpmerkingenTree.Location = new System.Drawing.Point(0, 39);
            this.xOpmerkingenTree.Name = "xOpmerkingenTree";
            this.xOpmerkingenTree.SelectedImageIndex = 0;
            this.xOpmerkingenTree.ShowLines = false;
            this.xOpmerkingenTree.ShowNodeToolTips = true;
            this.xOpmerkingenTree.Size = new System.Drawing.Size(645, 406);
            this.xOpmerkingenTree.TabIndex = 1;
            this.xOpmerkingenTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.xOpmerkingenTree_AfterSelect);
            this.xOpmerkingenTree.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.xOpmerkingenTree_NodeMouseClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wijzigToolStripMenuItem,
            this.toolStripSeparator1,
            this.verwijderToolStripMenuItem,
            this.toolStripSeparator2,
            this.vouwAllesToolStripMenuItem,
            this.ontvouwAllesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(152, 104);
            this.contextMenuStrip1.Opening += new System.ComponentModel.CancelEventHandler(this.contextMenuStrip1_Opening);
            // 
            // wijzigToolStripMenuItem
            // 
            this.wijzigToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.wijzigToolStripMenuItem.Name = "wijzigToolStripMenuItem";
            this.wijzigToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.wijzigToolStripMenuItem.Text = "Wijzig";
            this.wijzigToolStripMenuItem.Click += new System.EventHandler(this.wijzigToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
            // 
            // verwijderToolStripMenuItem
            // 
            this.verwijderToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.verwijderToolStripMenuItem.Name = "verwijderToolStripMenuItem";
            this.verwijderToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.verwijderToolStripMenuItem.Text = "Verwijder";
            this.verwijderToolStripMenuItem.Click += new System.EventHandler(this.xdeletetoolstripbutton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(148, 6);
            // 
            // vouwAllesToolStripMenuItem
            // 
            this.vouwAllesToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.icons8_collapse_32;
            this.vouwAllesToolStripMenuItem.Name = "vouwAllesToolStripMenuItem";
            this.vouwAllesToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.vouwAllesToolStripMenuItem.Text = "Vouw Alles";
            this.vouwAllesToolStripMenuItem.Click += new System.EventHandler(this.vouwAllesToolStripMenuItem_Click);
            // 
            // ontvouwAllesToolStripMenuItem
            // 
            this.ontvouwAllesToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.icons8_expand_32;
            this.ontvouwAllesToolStripMenuItem.Name = "ontvouwAllesToolStripMenuItem";
            this.ontvouwAllesToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.ontvouwAllesToolStripMenuItem.Text = "Ontvouw Alles";
            this.ontvouwAllesToolStripMenuItem.Click += new System.EventHandler(this.ontvouwAllesToolStripMenuItem_Click);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xaddtoolstripbutton,
            this.xdeletetoolstripbutton});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(645, 39);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // xaddtoolstripbutton
            // 
            this.xaddtoolstripbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xaddtoolstripbutton.Image = global::ProductieManager.Properties.Resources.add_Blue_circle_32x32;
            this.xaddtoolstripbutton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xaddtoolstripbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xaddtoolstripbutton.Name = "xaddtoolstripbutton";
            this.xaddtoolstripbutton.Size = new System.Drawing.Size(36, 36);
            this.xaddtoolstripbutton.ToolTipText = "Voeg een nieuwe opmerking toe";
            this.xaddtoolstripbutton.Click += new System.EventHandler(this.xaddtoolstripbutton_Click);
            // 
            // xdeletetoolstripbutton
            // 
            this.xdeletetoolstripbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xdeletetoolstripbutton.Enabled = false;
            this.xdeletetoolstripbutton.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdeletetoolstripbutton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xdeletetoolstripbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xdeletetoolstripbutton.Name = "xdeletetoolstripbutton";
            this.xdeletetoolstripbutton.Size = new System.Drawing.Size(36, 36);
            this.xdeletetoolstripbutton.Text = "toolStripButton2";
            this.xdeletetoolstripbutton.ToolTipText = "Verdwijder geselecteerde opmerking";
            this.xdeletetoolstripbutton.Click += new System.EventHandler(this.xdeletetoolstripbutton_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // OpmerkingenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 559);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.xselectedopmerkingpanel);
            this.Controls.Add(this.panel6);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "OpmerkingenForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Teal;
            this.Text = "Opmerkingen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OpmerkingenForm_FormClosing);
            this.Shown += new System.EventHandler(this.OpmerkingenForm_Shown);
            this.panel6.ResumeLayout(false);
            this.xselectedopmerkingpanel.ResumeLayout(false);
            this.xselectedopmerkingpanel.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button xannuleren;
        private System.Windows.Forms.Button xOpslaan;
        private System.Windows.Forms.Panel xselectedopmerkingpanel;
        private System.Windows.Forms.CheckBox xaansluitencheckbox;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox xreactietextbox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox xopmerkingtextbox;
        private System.Windows.Forms.ImageList xopmerkingimages;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton xaddtoolstripbutton;
        private System.Windows.Forms.ToolStripButton xdeletetoolstripbutton;
        private System.Windows.Forms.TreeView xOpmerkingenTree;
        private System.Windows.Forms.Button xwijzigbutton;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem wijzigToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem verwijderToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem vouwAllesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ontvouwAllesToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label xselectedopmerkinglabel;
    }
}