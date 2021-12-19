namespace Forms
{
    partial class VerpakkingenForm
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
            this.xVerpakkingen = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel2 = new System.Windows.Forms.Panel();
            this.xsearch = new MetroFramework.Controls.MetroTextBox();
            this.xdelete = new System.Windows.Forms.Button();
            this.verpakkingInstructieUI1 = new Controls.VerpakkingInstructieUI();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xVerpakkingen)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xVerpakkingen);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(274, 492);
            this.panel1.TabIndex = 0;
            // 
            // xVerpakkingen
            // 
            this.xVerpakkingen.AllColumns.Add(this.olvColumn1);
            this.xVerpakkingen.AllColumns.Add(this.olvColumn2);
            this.xVerpakkingen.AlternateRowBackColor = System.Drawing.Color.BlanchedAlmond;
            this.xVerpakkingen.CellEditUseWholeCell = false;
            this.xVerpakkingen.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.xVerpakkingen.Cursor = System.Windows.Forms.Cursors.Default;
            this.xVerpakkingen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xVerpakkingen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xVerpakkingen.FullRowSelect = true;
            this.xVerpakkingen.GridLines = true;
            this.xVerpakkingen.HideSelection = false;
            this.xVerpakkingen.LargeImageList = this.imageList1;
            this.xVerpakkingen.Location = new System.Drawing.Point(0, 32);
            this.xVerpakkingen.Name = "xVerpakkingen";
            this.xVerpakkingen.ShowGroups = false;
            this.xVerpakkingen.ShowItemToolTips = true;
            this.xVerpakkingen.Size = new System.Drawing.Size(274, 460);
            this.xVerpakkingen.SmallImageList = this.imageList1;
            this.xVerpakkingen.TabIndex = 1;
            this.xVerpakkingen.UseAlternatingBackColors = true;
            this.xVerpakkingen.UseCompatibleStateImageBehavior = false;
            this.xVerpakkingen.UseFilterIndicator = true;
            this.xVerpakkingen.UseFiltering = true;
            this.xVerpakkingen.UseHotItem = true;
            this.xVerpakkingen.UseTranslucentHotItem = true;
            this.xVerpakkingen.View = System.Windows.Forms.View.Details;
            this.xVerpakkingen.SelectedIndexChanged += new System.EventHandler(this.xVerpakkingen_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "ArtikelNr";
            this.olvColumn1.Text = "ArtikelNr";
            this.olvColumn1.ToolTipText = "ArtikelNr";
            this.olvColumn1.Width = 100;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "ProductOmschrijving";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Text = "Omschrijving";
            this.olvColumn2.ToolTipText = "Omschrijving";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xsearch);
            this.panel2.Controls.Add(this.xdelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(274, 32);
            this.panel2.TabIndex = 0;
            // 
            // xsearch
            // 
            // 
            // 
            // 
            this.xsearch.CustomButton.Image = null;
            this.xsearch.CustomButton.Location = new System.Drawing.Point(209, 2);
            this.xsearch.CustomButton.Name = "";
            this.xsearch.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.xsearch.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xsearch.CustomButton.TabIndex = 1;
            this.xsearch.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xsearch.CustomButton.UseSelectable = true;
            this.xsearch.CustomButton.Visible = false;
            this.xsearch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xsearch.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xsearch.Lines = new string[0];
            this.xsearch.Location = new System.Drawing.Point(0, 0);
            this.xsearch.MaxLength = 32767;
            this.xsearch.Name = "xsearch";
            this.xsearch.PasswordChar = '\0';
            this.xsearch.PromptText = "Zoeken...";
            this.xsearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xsearch.SelectedText = "";
            this.xsearch.SelectionLength = 0;
            this.xsearch.SelectionStart = 0;
            this.xsearch.ShortcutsEnabled = true;
            this.xsearch.ShowClearButton = true;
            this.xsearch.Size = new System.Drawing.Size(239, 32);
            this.xsearch.TabIndex = 1;
            this.xsearch.UseSelectable = true;
            this.xsearch.WaterMark = "Zoeken...";
            this.xsearch.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xsearch.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xsearch.TextChanged += new System.EventHandler(this.metroTextBox1_TextChanged);
            // 
            // xdelete
            // 
            this.xdelete.Dock = System.Windows.Forms.DockStyle.Right;
            this.xdelete.Enabled = false;
            this.xdelete.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdelete.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdelete.Location = new System.Drawing.Point(239, 0);
            this.xdelete.Name = "xdelete";
            this.xdelete.Size = new System.Drawing.Size(35, 32);
            this.xdelete.TabIndex = 0;
            this.xdelete.UseVisualStyleBackColor = true;
            this.xdelete.Click += new System.EventHandler(this.xdelete_Click);
            // 
            // verpakkingInstructieUI1
            // 
            this.verpakkingInstructieUI1.AllowEditMode = true;
            this.verpakkingInstructieUI1.AutoScroll = true;
            this.verpakkingInstructieUI1.BackColor = System.Drawing.Color.White;
            this.verpakkingInstructieUI1.BodyColor = System.Drawing.Color.Empty;
            this.verpakkingInstructieUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verpakkingInstructieUI1.IsEditmode = false;
            this.verpakkingInstructieUI1.Location = new System.Drawing.Point(294, 60);
            this.verpakkingInstructieUI1.Name = "verpakkingInstructieUI1";
            this.verpakkingInstructieUI1.Padding = new System.Windows.Forms.Padding(5);
            this.verpakkingInstructieUI1.Productie = null;
            this.verpakkingInstructieUI1.Size = new System.Drawing.Size(642, 492);
            this.verpakkingInstructieUI1.TabIndex = 1;
            this.verpakkingInstructieUI1.TextColor = System.Drawing.Color.Empty;
            this.verpakkingInstructieUI1.Title = null;
            // 
            // VerpakkingenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(956, 572);
            this.Controls.Add(this.verpakkingInstructieUI1);
            this.Controls.Add(this.panel1);
            this.Name = "VerpakkingenForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Brown;
            this.Text = "Aangepaste Verpakkingen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VerpakkingenForm_FormClosing);
            this.Shown += new System.EventHandler(this.VerpakkingenForm_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xVerpakkingen)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Controls.MetroTextBox xsearch;
        private System.Windows.Forms.Button xdelete;
        private BrightIdeasSoftware.ObjectListView xVerpakkingen;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.ImageList imageList1;
        private Controls.VerpakkingInstructieUI verpakkingInstructieUI1;
    }
}