
namespace ProductieManager.Forms
{
    partial class ProductieOverzichtForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductieOverzichtForm));
            this.xcontrolpanel = new System.Windows.Forms.Panel();
            this.xloadinglabel = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xsearchbox = new System.Windows.Forms.TextBox();
            this.xwerkpleklist = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selecteerAllesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deSelecteerAllesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xcontrolpanel
            // 
            this.xcontrolpanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xcontrolpanel.AutoScroll = true;
            this.xcontrolpanel.Location = new System.Drawing.Point(6, 0);
            this.xcontrolpanel.Name = "xcontrolpanel";
            this.xcontrolpanel.Size = new System.Drawing.Size(684, 518);
            this.xcontrolpanel.TabIndex = 0;
            // 
            // xloadinglabel
            // 
            this.xloadinglabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xloadinglabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xloadinglabel.Location = new System.Drawing.Point(0, 0);
            this.xloadinglabel.Name = "xloadinglabel";
            this.xloadinglabel.Size = new System.Drawing.Size(693, 518);
            this.xloadinglabel.TabIndex = 31;
            this.xloadinglabel.Text = "Overzicht aanmaken...";
            this.xloadinglabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xloadinglabel.Visible = false;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xsearchbox);
            this.panel1.Controls.Add(this.xwerkpleklist);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(10, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(332, 518);
            this.panel1.TabIndex = 1;
            // 
            // xsearchbox
            // 
            this.xsearchbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.xsearchbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsearchbox.Location = new System.Drawing.Point(0, 39);
            this.xsearchbox.Name = "xsearchbox";
            this.xsearchbox.Size = new System.Drawing.Size(332, 29);
            this.xsearchbox.TabIndex = 2;
            this.xsearchbox.Text = "Zoeken...";
            this.xsearchbox.TextChanged += new System.EventHandler(this.xsearchbox_TextChanged);
            this.xsearchbox.Enter += new System.EventHandler(this.xsearchbox_Enter);
            this.xsearchbox.Leave += new System.EventHandler(this.xsearchbox_Leave);
            // 
            // xwerkpleklist
            // 
            this.xwerkpleklist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xwerkpleklist.CheckBoxes = true;
            this.xwerkpleklist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.xwerkpleklist.ContextMenuStrip = this.contextMenuStrip1;
            this.xwerkpleklist.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xwerkpleklist.FullRowSelect = true;
            this.xwerkpleklist.GridLines = true;
            this.xwerkpleklist.HideSelection = false;
            this.xwerkpleklist.HoverSelection = true;
            this.xwerkpleklist.LargeImageList = this.imageList1;
            this.xwerkpleklist.Location = new System.Drawing.Point(0, 70);
            this.xwerkpleklist.Name = "xwerkpleklist";
            this.xwerkpleklist.ShowItemToolTips = true;
            this.xwerkpleklist.Size = new System.Drawing.Size(332, 445);
            this.xwerkpleklist.SmallImageList = this.imageList1;
            this.xwerkpleklist.TabIndex = 1;
            this.xwerkpleklist.UseCompatibleStateImageBehavior = false;
            this.xwerkpleklist.View = System.Windows.Forms.View.Details;
            this.xwerkpleklist.ItemChecked += new System.Windows.Forms.ItemCheckedEventHandler(this.xwerkpleklist_ItemChecked);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "WerkPlaatsen";
            this.columnHeader1.Width = 320;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selecteerAllesToolStripMenuItem,
            this.deSelecteerAllesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(169, 48);
            // 
            // selecteerAllesToolStripMenuItem
            // 
            this.selecteerAllesToolStripMenuItem.Name = "selecteerAllesToolStripMenuItem";
            this.selecteerAllesToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.selecteerAllesToolStripMenuItem.Text = "Selecteer Alles";
            this.selecteerAllesToolStripMenuItem.Click += new System.EventHandler(this.selecteerAllesToolStripMenuItem_Click);
            // 
            // deSelecteerAllesToolStripMenuItem
            // 
            this.deSelecteerAllesToolStripMenuItem.Name = "deSelecteerAllesToolStripMenuItem";
            this.deSelecteerAllesToolStripMenuItem.Size = new System.Drawing.Size(168, 22);
            this.deSelecteerAllesToolStripMenuItem.Text = "De-Selecteer Alles";
            this.deSelecteerAllesToolStripMenuItem.Click += new System.EventHandler(this.deSelecteerAllesToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(332, 39);
            this.button1.TabIndex = 0;
            this.button1.Text = "Creëer ProductieOverzicht";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xloadinglabel);
            this.panel2.Controls.Add(this.xcontrolpanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(342, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(693, 518);
            this.panel2.TabIndex = 32;
            // 
            // ProductieOverzichtForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1045, 588);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductieOverzichtForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow;
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "Productie Overzicht";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductieOverzichtForm_FormClosing);
            this.Shown += new System.EventHandler(this.ProductieOverzichtForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel xcontrolpanel;
        private System.Windows.Forms.Label xloadinglabel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListView xwerkpleklist;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox xsearchbox;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem selecteerAllesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deSelecteerAllesToolStripMenuItem;
        private System.Windows.Forms.Panel panel2;
    }
}