
namespace Controls
{
    partial class ArtikelsUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArtikelsUI));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xartikelsList = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xsearchbox = new MetroFramework.Controls.MetroTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.productieListControl1 = new Controls.ProductieListControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xartikelsList)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xartikelsList);
            this.panel1.Controls.Add(this.xsearchbox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(200, 561);
            this.panel1.TabIndex = 0;
            // 
            // xartikelsList
            // 
            this.xartikelsList.AllColumns.Add(this.olvColumn1);
            this.xartikelsList.CellEditUseWholeCell = false;
            this.xartikelsList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1});
            this.xartikelsList.Cursor = System.Windows.Forms.Cursors.Default;
            this.xartikelsList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xartikelsList.FullRowSelect = true;
            this.xartikelsList.HideSelection = false;
            this.xartikelsList.LargeImageList = this.imageList1;
            this.xartikelsList.Location = new System.Drawing.Point(0, 30);
            this.xartikelsList.MultiSelect = false;
            this.xartikelsList.Name = "xartikelsList";
            this.xartikelsList.ShowGroups = false;
            this.xartikelsList.ShowItemToolTips = true;
            this.xartikelsList.Size = new System.Drawing.Size(200, 531);
            this.xartikelsList.SmallImageList = this.imageList1;
            this.xartikelsList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.xartikelsList.TabIndex = 1;
            this.xartikelsList.UseCompatibleStateImageBehavior = false;
            this.xartikelsList.UseFilterIndicator = true;
            this.xartikelsList.UseHotItem = true;
            this.xartikelsList.UseTranslucentHotItem = true;
            this.xartikelsList.UseTranslucentSelection = true;
            this.xartikelsList.View = System.Windows.Forms.View.Details;
            this.xartikelsList.SelectedIndexChanged += new System.EventHandler(this.xartikelsList_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Key";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.Groupable = false;
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "ArtikelNrs";
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "product_document_file_32x32.png");
            // 
            // xsearchbox
            // 
            // 
            // 
            // 
            this.xsearchbox.CustomButton.Image = null;
            this.xsearchbox.CustomButton.Location = new System.Drawing.Point(172, 2);
            this.xsearchbox.CustomButton.Name = "";
            this.xsearchbox.CustomButton.Size = new System.Drawing.Size(25, 25);
            this.xsearchbox.CustomButton.Style = MetroFramework.MetroColorStyle.Silver;
            this.xsearchbox.CustomButton.TabIndex = 1;
            this.xsearchbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xsearchbox.CustomButton.UseSelectable = true;
            this.xsearchbox.CustomButton.Visible = false;
            this.xsearchbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.xsearchbox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xsearchbox.Lines = new string[] {
        "Zoeken..."};
            this.xsearchbox.Location = new System.Drawing.Point(0, 0);
            this.xsearchbox.MaxLength = 32767;
            this.xsearchbox.Name = "xsearchbox";
            this.xsearchbox.PasswordChar = '\0';
            this.xsearchbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xsearchbox.SelectedText = "";
            this.xsearchbox.SelectionLength = 0;
            this.xsearchbox.SelectionStart = 0;
            this.xsearchbox.ShortcutsEnabled = true;
            this.xsearchbox.ShowClearButton = true;
            this.xsearchbox.Size = new System.Drawing.Size(200, 30);
            this.xsearchbox.Style = MetroFramework.MetroColorStyle.Silver;
            this.xsearchbox.TabIndex = 8;
            this.xsearchbox.Text = "Zoeken...";
            this.xsearchbox.UseSelectable = true;
            this.xsearchbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xsearchbox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xsearchbox.TextChanged += new System.EventHandler(this.xsearchArtikel_TextChanged);
            this.xsearchbox.Enter += new System.EventHandler(this.xsearchArtikel_Enter);
            this.xsearchbox.Leave += new System.EventHandler(this.xsearchArtikel_Leave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.productieListControl1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(200, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(722, 561);
            this.panel2.TabIndex = 1;
            // 
            // productieListControl1
            // 
            this.productieListControl1.AutoScroll = true;
            this.productieListControl1.BackColor = System.Drawing.Color.White;
            this.productieListControl1.CanLoad = true;
            this.productieListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieListControl1.EnableEntryFiltering = true;
            this.productieListControl1.EnableFiltering = false;
            this.productieListControl1.EnableSync = false;
            this.productieListControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieListControl1.IsBewerkingView = true;
            this.productieListControl1.ListName = "ArtikelsLijst";
            this.productieListControl1.Location = new System.Drawing.Point(0, 0);
            this.productieListControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productieListControl1.Name = "productieListControl1";
            this.productieListControl1.SelectedItem = null;
            this.productieListControl1.ShowWaitUI = true;
            this.productieListControl1.Size = new System.Drawing.Size(722, 561);
            this.productieListControl1.TabIndex = 0;
            this.productieListControl1.ValidHandler = null;
            // 
            // ArtikelsUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Name = "ArtikelsUI";
            this.Size = new System.Drawing.Size(922, 561);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xartikelsList)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ImageList imageList1;
        private Controls.ProductieListControl productieListControl1;
        private BrightIdeasSoftware.ObjectListView xartikelsList;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private MetroFramework.Controls.MetroTextBox xsearchbox;
    }
}