
namespace Forms
{
    partial class BewerkingSelectorForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BewerkingSelectorForm));
            this.xbewerkinglijst = new System.Windows.Forms.ListView();
            this.xnaamclmn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.xomschrijvingclmnb = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.xartikelclmn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.xprodnrcolmn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.selecteerAllesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deselecteerAllesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.xok = new System.Windows.Forms.Button();
            this.xannuleren = new System.Windows.Forms.Button();
            this.contextMenuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xbewerkinglijst
            // 
            this.xbewerkinglijst.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.xbewerkinglijst.CheckBoxes = true;
            this.xbewerkinglijst.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.xnaamclmn,
            this.xomschrijvingclmnb,
            this.xartikelclmn,
            this.xprodnrcolmn});
            this.xbewerkinglijst.ContextMenuStrip = this.contextMenuStrip1;
            this.xbewerkinglijst.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xbewerkinglijst.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbewerkinglijst.FullRowSelect = true;
            this.xbewerkinglijst.HideSelection = false;
            this.xbewerkinglijst.HoverSelection = true;
            this.xbewerkinglijst.LargeImageList = this.imageList1;
            this.xbewerkinglijst.Location = new System.Drawing.Point(10, 60);
            this.xbewerkinglijst.Name = "xbewerkinglijst";
            this.xbewerkinglijst.ShowItemToolTips = true;
            this.xbewerkinglijst.Size = new System.Drawing.Size(860, 454);
            this.xbewerkinglijst.SmallImageList = this.imageList1;
            this.xbewerkinglijst.TabIndex = 0;
            this.xbewerkinglijst.UseCompatibleStateImageBehavior = false;
            this.xbewerkinglijst.View = System.Windows.Forms.View.Details;
            // 
            // xnaamclmn
            // 
            this.xnaamclmn.Text = "Naam";
            this.xnaamclmn.Width = 138;
            // 
            // xomschrijvingclmnb
            // 
            this.xomschrijvingclmnb.Text = "Omschrijving";
            this.xomschrijvingclmnb.Width = 369;
            // 
            // xartikelclmn
            // 
            this.xartikelclmn.Text = "ArtikelNr";
            this.xartikelclmn.Width = 120;
            // 
            // xprodnrcolmn
            // 
            this.xprodnrcolmn.Text = "ProductieNr";
            this.xprodnrcolmn.Width = 120;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selecteerAllesToolStripMenuItem,
            this.deselecteerAllesToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(163, 48);
            // 
            // selecteerAllesToolStripMenuItem
            // 
            this.selecteerAllesToolStripMenuItem.Name = "selecteerAllesToolStripMenuItem";
            this.selecteerAllesToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.selecteerAllesToolStripMenuItem.Text = "&Selecteer Alles";
            this.selecteerAllesToolStripMenuItem.Click += new System.EventHandler(this.selecteerAllesToolStripMenuItem_Click);
            // 
            // deselecteerAllesToolStripMenuItem
            // 
            this.deselecteerAllesToolStripMenuItem.Name = "deselecteerAllesToolStripMenuItem";
            this.deselecteerAllesToolStripMenuItem.Size = new System.Drawing.Size(162, 22);
            this.deselecteerAllesToolStripMenuItem.Text = "&Deselecteer Alles";
            this.deselecteerAllesToolStripMenuItem.Click += new System.EventHandler(this.deselecteerAllesToolStripMenuItem_Click);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "operation_32x32.png");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xok);
            this.panel1.Controls.Add(this.xannuleren);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(10, 473);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 41);
            this.panel1.TabIndex = 1;
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xok.Location = new System.Drawing.Point(621, 3);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(115, 34);
            this.xok.TabIndex = 1;
            this.xok.Text = "OK";
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // xannuleren
            // 
            this.xannuleren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xannuleren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xannuleren.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xannuleren.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xannuleren.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xannuleren.Location = new System.Drawing.Point(742, 3);
            this.xannuleren.Name = "xannuleren";
            this.xannuleren.Size = new System.Drawing.Size(115, 34);
            this.xannuleren.TabIndex = 0;
            this.xannuleren.Text = "Annuleren";
            this.xannuleren.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xannuleren.UseVisualStyleBackColor = true;
            this.xannuleren.Click += new System.EventHandler(this.xannuleren_Click);
            // 
            // BewerkingSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 524);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.xbewerkinglijst);
            this.Name = "BewerkingSelectorForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selecteer Bewerkingen";
            this.contextMenuStrip1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListView xbewerkinglijst;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xannuleren;
        private System.Windows.Forms.Button xok;
        private System.Windows.Forms.ColumnHeader xnaamclmn;
        private System.Windows.Forms.ColumnHeader xomschrijvingclmnb;
        private System.Windows.Forms.ColumnHeader xartikelclmn;
        private System.Windows.Forms.ColumnHeader xprodnrcolmn;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem selecteerAllesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deselecteerAllesToolStripMenuItem;
    }
}