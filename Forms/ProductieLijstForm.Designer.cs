using Controls;

namespace Forms
{
    partial class ProductieLijstForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductieLijstForm));
            this.productieListControl1 = new Controls.ProductieListControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xlijstname = new System.Windows.Forms.Label();
            this.xsluiten = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // productieListControl1
            // 
            this.productieListControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.productieListControl1.BackColor = System.Drawing.Color.White;
            this.productieListControl1.CanLoad = true;
            this.productieListControl1.EnableEntryFiltering = true;
            this.productieListControl1.EnableFiltering = true;
            this.productieListControl1.EnableSync = false;
            this.productieListControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieListControl1.IsBewerkingView = true;
            this.productieListControl1.Location = new System.Drawing.Point(12, 39);
            this.productieListControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productieListControl1.Name = "productieListControl1";
            this.productieListControl1.RemoveCustomItemIfNotValid = false;
            this.productieListControl1.SelectedItem = null;
            this.productieListControl1.Size = new System.Drawing.Size(945, 542);
            this.productieListControl1.TabIndex = 0;
            this.productieListControl1.ValidHandler = null;
            this.productieListControl1.ItemCountChanged += new System.EventHandler(this.productieListControl1_ItemCountChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xlijstname);
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(969, 32);
            this.panel1.TabIndex = 1;
            // 
            // xlijstname
            // 
            this.xlijstname.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xlijstname.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlijstname.Location = new System.Drawing.Point(0, 0);
            this.xlijstname.Name = "xlijstname";
            this.xlijstname.Size = new System.Drawing.Size(844, 32);
            this.xlijstname.TabIndex = 1;
            this.xlijstname.Text = "Productie Lijst";
            this.xlijstname.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xsluiten
            // 
            this.xsluiten.Dock = System.Windows.Forms.DockStyle.Right;
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(844, 0);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(125, 32);
            this.xsluiten.TabIndex = 0;
            this.xsluiten.Text = "&Sluiten";
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // ProductieLijstForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(969, 594);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.productieListControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductieLijstForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "StartProductie";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StartProductie_FormClosing);
            this.Shown += new System.EventHandler(this.StartProductie_Shown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private ProductieListControl productieListControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label xlijstname;
        private System.Windows.Forms.Button xsluiten;
    }
}