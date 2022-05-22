
namespace Controls
{
    partial class ZoekProductiesUI
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
            this.xsluitenpanel = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xverwerkb = new System.Windows.Forms.Button();
            this.xprogresslabel = new System.Windows.Forms.Label();
            this.productieListControl1 = new Controls.ProductieListControl();
            this.xsluitenpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(48, 48);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xsluitenpanel
            // 
            this.xsluitenpanel.BackColor = System.Drawing.Color.White;
            this.xsluitenpanel.Controls.Add(this.xsluiten);
            this.xsluitenpanel.Controls.Add(this.xverwerkb);
            this.xsluitenpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xsluitenpanel.Location = new System.Drawing.Point(0, 513);
            this.xsluitenpanel.Name = "xsluitenpanel";
            this.xsluitenpanel.Size = new System.Drawing.Size(1034, 39);
            this.xsluitenpanel.TabIndex = 22;
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(931, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(100, 32);
            this.xsluiten.TabIndex = 2;
            this.xsluiten.Text = "&Sluiten";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xverwerkb
            // 
            this.xverwerkb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xverwerkb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xverwerkb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xverwerkb.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.xverwerkb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xverwerkb.Location = new System.Drawing.Point(772, 3);
            this.xverwerkb.Name = "xverwerkb";
            this.xverwerkb.Size = new System.Drawing.Size(153, 32);
            this.xverwerkb.TabIndex = 14;
            this.xverwerkb.Text = "Zoek Producties!";
            this.xverwerkb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xverwerkb.UseVisualStyleBackColor = true;
            this.xverwerkb.Click += new System.EventHandler(this.xverwerkb_Click);
            // 
            // xprogresslabel
            // 
            this.xprogresslabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xprogresslabel.BackColor = System.Drawing.Color.White;
            this.xprogresslabel.Font = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprogresslabel.Location = new System.Drawing.Point(0, 0);
            this.xprogresslabel.Name = "xprogresslabel";
            this.xprogresslabel.Size = new System.Drawing.Size(1034, 510);
            this.xprogresslabel.TabIndex = 23;
            this.xprogresslabel.Text = "Producties laden...";
            this.xprogresslabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xprogresslabel.Visible = false;
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
            this.productieListControl1.ListName = "RangeBewerkingLijst";
            this.productieListControl1.Location = new System.Drawing.Point(0, 0);
            this.productieListControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productieListControl1.Name = "productieListControl1";
            this.productieListControl1.RemoveCustomItemIfNotValid = false;
            this.productieListControl1.SelectedItem = null;
            this.productieListControl1.ShowWaitUI = true;
            this.productieListControl1.Size = new System.Drawing.Size(1034, 513);
            this.productieListControl1.TabIndex = 23;
            this.productieListControl1.ValidHandler = null;
            // 
            // ZoekProductiesUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.xprogresslabel);
            this.Controls.Add(this.productieListControl1);
            this.Controls.Add(this.xsluitenpanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(725, 425);
            this.Name = "ZoekProductiesUI";
            this.Size = new System.Drawing.Size(1034, 552);
            this.xsluitenpanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button xverwerkb;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.Panel xsluitenpanel;
        private System.Windows.Forms.Label xprogresslabel;
        private Controls.ProductieListControl productieListControl1;
    }
}