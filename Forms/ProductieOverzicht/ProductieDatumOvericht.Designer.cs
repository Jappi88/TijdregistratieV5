
namespace Forms
{
    partial class ProductieDatumOvericht
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
            this.xsearchbox = new MetroFramework.Controls.MetroTextBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.vouwAllesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ontvouwAllesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.searchtimer = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xsearchbox
            // 
            // 
            // 
            // 
            this.xsearchbox.CustomButton.Image = null;
            this.xsearchbox.CustomButton.Location = new System.Drawing.Point(719, 1);
            this.xsearchbox.CustomButton.Name = "";
            this.xsearchbox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xsearchbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xsearchbox.CustomButton.TabIndex = 1;
            this.xsearchbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xsearchbox.CustomButton.UseSelectable = true;
            this.xsearchbox.CustomButton.Visible = false;
            this.xsearchbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.xsearchbox.Lines = new string[0];
            this.xsearchbox.Location = new System.Drawing.Point(20, 60);
            this.xsearchbox.MaxLength = 32767;
            this.xsearchbox.Name = "xsearchbox";
            this.xsearchbox.PasswordChar = '\0';
            this.xsearchbox.PromptText = "Zoeken...";
            this.xsearchbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xsearchbox.SelectedText = "";
            this.xsearchbox.SelectionLength = 0;
            this.xsearchbox.SelectionStart = 0;
            this.xsearchbox.ShortcutsEnabled = true;
            this.xsearchbox.ShowClearButton = true;
            this.xsearchbox.Size = new System.Drawing.Size(741, 23);
            this.xsearchbox.TabIndex = 1;
            this.xsearchbox.UseSelectable = true;
            this.xsearchbox.WaterMark = "Zoeken...";
            this.xsearchbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xsearchbox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xsearchbox.TextChanged += new System.EventHandler(this.xsearchbox_TextChanged);
            // 
            // treeView1
            // 
            this.treeView1.ContextMenuStrip = this.contextMenuStrip1;
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.treeView1.ImageIndex = 0;
            this.treeView1.ImageList = this.imageList1;
            this.treeView1.Location = new System.Drawing.Point(20, 83);
            this.treeView1.Name = "treeView1";
            this.treeView1.SelectedImageIndex = 0;
            this.treeView1.ShowNodeToolTips = true;
            this.treeView1.Size = new System.Drawing.Size(741, 320);
            this.treeView1.TabIndex = 2;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vouwAllesToolStripMenuItem,
            this.ontvouwAllesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(181, 70);
            // 
            // vouwAllesToolStripMenuItem
            // 
            this.vouwAllesToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.icons8_collapse_32;
            this.vouwAllesToolStripMenuItem.Name = "vouwAllesToolStripMenuItem";
            this.vouwAllesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.vouwAllesToolStripMenuItem.Text = "Vouw Alles";
            this.vouwAllesToolStripMenuItem.Click += new System.EventHandler(this.vouwAllesToolStripMenuItem_Click);
            // 
            // ontvouwAllesToolStripMenuItem
            // 
            this.ontvouwAllesToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.icons8_expand_32;
            this.ontvouwAllesToolStripMenuItem.Name = "ontvouwAllesToolStripMenuItem";
            this.ontvouwAllesToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.ontvouwAllesToolStripMenuItem.Text = "Ontvouw Alles";
            this.ontvouwAllesToolStripMenuItem.Click += new System.EventHandler(this.ontvouwAllesToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(24, 24);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // searchtimer
            // 
            this.searchtimer.Interval = 500;
            this.searchtimer.Tick += new System.EventHandler(this.searchtimer_Tick);
            // 
            // ProductieDatumOvericht
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(781, 423);
            this.Controls.Add(this.treeView1);
            this.Controls.Add(this.xsearchbox);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ProductieDatumOvericht";
            this.Text = "Productie Overzicht";
            this.Title = "Productie Overzicht";
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroTextBox xsearchbox;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem vouwAllesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ontvouwAllesToolStripMenuItem;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Timer searchtimer;
    }
}