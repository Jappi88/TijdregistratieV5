
namespace Forms
{
    partial class ColorPickerForm
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
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xkleurenlist = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xOpslaan = new System.Windows.Forms.Button();
            this.xselectedcolor = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xselectedcolor)).BeginInit();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xkleurenlist
            // 
            this.xkleurenlist.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xkleurenlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.xkleurenlist.FullRowSelect = true;
            this.xkleurenlist.HideSelection = false;
            this.xkleurenlist.LargeImageList = this.imageList1;
            this.xkleurenlist.Location = new System.Drawing.Point(13, 63);
            this.xkleurenlist.MultiSelect = false;
            this.xkleurenlist.Name = "xkleurenlist";
            this.xkleurenlist.ShowGroups = false;
            this.xkleurenlist.Size = new System.Drawing.Size(360, 166);
            this.xkleurenlist.SmallImageList = this.imageList1;
            this.xkleurenlist.TabIndex = 2;
            this.xkleurenlist.TileSize = new System.Drawing.Size(38, 38);
            this.xkleurenlist.UseCompatibleStateImageBehavior = false;
            this.xkleurenlist.View = System.Windows.Forms.View.Tile;
            this.xkleurenlist.SelectedIndexChanged += new System.EventHandler(this.xkleurenlist_SelectedIndexChanged);
            this.xkleurenlist.DoubleClick += new System.EventHandler(this.xkleurenlist_DoubleClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Kleuren";
            this.columnHeader1.Width = 100;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xselectedcolor);
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Controls.Add(this.xOpslaan);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(10, 235);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(366, 45);
            this.panel1.TabIndex = 3;
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.ForeColor = System.Drawing.Color.Black;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(244, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(119, 38);
            this.xsluiten.TabIndex = 6;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xOpslaan
            // 
            this.xOpslaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xOpslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOpslaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOpslaan.ForeColor = System.Drawing.Color.Black;
            this.xOpslaan.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xOpslaan.Location = new System.Drawing.Point(119, 3);
            this.xOpslaan.Name = "xOpslaan";
            this.xOpslaan.Size = new System.Drawing.Size(119, 38);
            this.xOpslaan.TabIndex = 7;
            this.xOpslaan.Text = "OK";
            this.xOpslaan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xOpslaan.UseVisualStyleBackColor = true;
            this.xOpslaan.Visible = false;
            this.xOpslaan.Click += new System.EventHandler(this.xOpslaan_Click);
            // 
            // xselectedcolor
            // 
            this.xselectedcolor.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xselectedcolor.Location = new System.Drawing.Point(81, 3);
            this.xselectedcolor.Name = "xselectedcolor";
            this.xselectedcolor.Size = new System.Drawing.Size(34, 38);
            this.xselectedcolor.TabIndex = 8;
            this.xselectedcolor.TabStop = false;
            // 
            // ColorPickerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 290);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.xkleurenlist);
            this.MinimumSize = new System.Drawing.Size(309, 261);
            this.Name = "ColorPickerForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kies Een Kleur";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xselectedcolor)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView xkleurenlist;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox xselectedcolor;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xOpslaan;
    }
}