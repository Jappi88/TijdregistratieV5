
namespace Forms
{
    partial class BackupRestoreForm
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
            this.xTreeView = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.herstellenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.vouwAllesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ontvouwAllesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xZoekPanel = new System.Windows.Forms.Panel();
            this.xSearchTextBox = new MetroFramework.Controls.MetroTextBox();
            this.xloadzip = new System.Windows.Forms.Button();
            this.xSearchTimer = new System.Windows.Forms.Timer(this.components);
            this.xStatusStrip = new System.Windows.Forms.ToolStrip();
            this.xProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.xCancelButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.xStatusLabel = new System.Windows.Forms.ToolStripLabel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xStatusResetTimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.xZoekPanel.SuspendLayout();
            this.xStatusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // xTreeView
            // 
            this.xTreeView.ContextMenuStrip = this.contextMenuStrip1;
            this.xTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xTreeView.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xTreeView.FullRowSelect = true;
            this.xTreeView.ImageIndex = 0;
            this.xTreeView.ImageList = this.imageList1;
            this.xTreeView.Location = new System.Drawing.Point(20, 92);
            this.xTreeView.Name = "xTreeView";
            this.xTreeView.SelectedImageIndex = 0;
            this.xTreeView.ShowNodeToolTips = true;
            this.xTreeView.Size = new System.Drawing.Size(440, 244);
            this.xTreeView.TabIndex = 0;
            this.xTreeView.AfterCollapse += new System.Windows.Forms.TreeViewEventHandler(this.xTreeView_AfterCollapse);
            this.xTreeView.AfterExpand += new System.Windows.Forms.TreeViewEventHandler(this.xTreeView_AfterExpand);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.herstellenToolStripMenuItem,
            this.toolStripSeparator1,
            this.vouwAllesToolStripMenuItem,
            this.ontvouwAllesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(152, 76);
            // 
            // herstellenToolStripMenuItem
            // 
            this.herstellenToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.herstellenToolStripMenuItem.Name = "herstellenToolStripMenuItem";
            this.herstellenToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            this.herstellenToolStripMenuItem.Text = "Herstellen";
            this.herstellenToolStripMenuItem.Click += new System.EventHandler(this.herstellenToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(148, 6);
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
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xZoekPanel
            // 
            this.xZoekPanel.Controls.Add(this.xSearchTextBox);
            this.xZoekPanel.Controls.Add(this.xloadzip);
            this.xZoekPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xZoekPanel.Location = new System.Drawing.Point(20, 60);
            this.xZoekPanel.Name = "xZoekPanel";
            this.xZoekPanel.Size = new System.Drawing.Size(440, 32);
            this.xZoekPanel.TabIndex = 1;
            // 
            // xSearchTextBox
            // 
            // 
            // 
            // 
            this.xSearchTextBox.CustomButton.Image = null;
            this.xSearchTextBox.CustomButton.Location = new System.Drawing.Point(780, 2);
            this.xSearchTextBox.CustomButton.Name = "";
            this.xSearchTextBox.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.xSearchTextBox.CustomButton.Style = MetroFramework.MetroColorStyle.Orange;
            this.xSearchTextBox.CustomButton.TabIndex = 1;
            this.xSearchTextBox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xSearchTextBox.CustomButton.UseSelectable = true;
            this.xSearchTextBox.CustomButton.Visible = false;
            this.xSearchTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xSearchTextBox.Lines = new string[0];
            this.xSearchTextBox.Location = new System.Drawing.Point(0, 0);
            this.xSearchTextBox.MaxLength = 32767;
            this.xSearchTextBox.Name = "xSearchTextBox";
            this.xSearchTextBox.PasswordChar = '\0';
            this.xSearchTextBox.PromptText = "Zoeken...";
            this.xSearchTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xSearchTextBox.SelectedText = "";
            this.xSearchTextBox.SelectionLength = 0;
            this.xSearchTextBox.SelectionStart = 0;
            this.xSearchTextBox.ShortcutsEnabled = true;
            this.xSearchTextBox.ShowClearButton = true;
            this.xSearchTextBox.Size = new System.Drawing.Size(407, 32);
            this.xSearchTextBox.Style = MetroFramework.MetroColorStyle.Orange;
            this.xSearchTextBox.TabIndex = 0;
            this.xSearchTextBox.UseSelectable = true;
            this.xSearchTextBox.WaterMark = "Zoeken...";
            this.xSearchTextBox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xSearchTextBox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xSearchTextBox.TextChanged += new System.EventHandler(this.xSearchTextBox_TextChanged);
            // 
            // xloadzip
            // 
            this.xloadzip.Dock = System.Windows.Forms.DockStyle.Right;
            this.xloadzip.FlatAppearance.BorderSize = 0;
            this.xloadzip.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xloadzip.Image = global::ProductieManager.Properties.Resources.files_folder_icon_32x32;
            this.xloadzip.Location = new System.Drawing.Point(407, 0);
            this.xloadzip.Name = "xloadzip";
            this.xloadzip.Size = new System.Drawing.Size(33, 32);
            this.xloadzip.TabIndex = 1;
            this.toolTip1.SetToolTip(this.xloadzip, ".zip Backup laden...");
            this.xloadzip.UseVisualStyleBackColor = true;
            this.xloadzip.Click += new System.EventHandler(this.xloadzip_Click);
            // 
            // xSearchTimer
            // 
            this.xSearchTimer.Interval = 500;
            this.xSearchTimer.Tick += new System.EventHandler(this.xSearchTimer_Tick);
            // 
            // xStatusStrip
            // 
            this.xStatusStrip.BackColor = System.Drawing.Color.Transparent;
            this.xStatusStrip.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xStatusStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.xStatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xProgressBar,
            this.xCancelButton,
            this.toolStripSeparator2,
            this.xStatusLabel});
            this.xStatusStrip.Location = new System.Drawing.Point(20, 500);
            this.xStatusStrip.Name = "xStatusStrip";
            this.xStatusStrip.Size = new System.Drawing.Size(843, 25);
            this.xStatusStrip.TabIndex = 2;
            this.xStatusStrip.Text = "toolStrip1";
            this.xStatusStrip.Visible = false;
            // 
            // xProgressBar
            // 
            this.xProgressBar.Name = "xProgressBar";
            this.xProgressBar.Size = new System.Drawing.Size(100, 22);
            // 
            // xCancelButton
            // 
            this.xCancelButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xCancelButton.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xCancelButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xCancelButton.Name = "xCancelButton";
            this.xCancelButton.Size = new System.Drawing.Size(23, 22);
            this.xCancelButton.ToolTipText = "Herstellen annuleren";
            this.xCancelButton.Visible = false;
            this.xCancelButton.Click += new System.EventHandler(this.xCancelButton_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // xStatusLabel
            // 
            this.xStatusLabel.Name = "xStatusLabel";
            this.xStatusLabel.Size = new System.Drawing.Size(26, 22);
            this.xStatusLabel.Text = "Idle";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // xStatusResetTimer
            // 
            this.xStatusResetTimer.Interval = 3500;
            this.xStatusResetTimer.Tick += new System.EventHandler(this.xStatusResetTimer_Tick);
            // 
            // BackupRestoreForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(480, 356);
            this.Controls.Add(this.xTreeView);
            this.Controls.Add(this.xStatusStrip);
            this.Controls.Add(this.xZoekPanel);
            this.Name = "BackupRestoreForm";
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Backup Herstellen";
            this.Title = "Backup Herstellen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BackupRestoreForm_FormClosing);
            this.contextMenuStrip1.ResumeLayout(false);
            this.xZoekPanel.ResumeLayout(false);
            this.xStatusStrip.ResumeLayout(false);
            this.xStatusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TreeView xTreeView;
        private System.Windows.Forms.Panel xZoekPanel;
        private MetroFramework.Controls.MetroTextBox xSearchTextBox;
        private System.Windows.Forms.Timer xSearchTimer;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem vouwAllesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ontvouwAllesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem herstellenToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStrip xStatusStrip;
        private System.Windows.Forms.ToolStripProgressBar xProgressBar;
        private System.Windows.Forms.ToolStripButton xCancelButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripLabel xStatusLabel;
        private System.Windows.Forms.Button xloadzip;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer xStatusResetTimer;
    }
}