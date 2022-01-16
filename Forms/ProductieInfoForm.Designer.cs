
using TheArtOfDev.HtmlRenderer.WinForms;

namespace Forms
{
    partial class ProductieInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductieInfoForm));
            this.xcontainer = new System.Windows.Forms.Panel();
            this.xexport = new System.Windows.Forms.Button();
            this.xstatsb = new System.Windows.Forms.Button();
            this.xsluiten = new System.Windows.Forms.Button();
            this.productieInfoUI1 = new Controls.ProductieInfoUI();
            this.xcontainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // xcontainer
            // 
            this.xcontainer.AutoScroll = true;
            this.xcontainer.Controls.Add(this.productieInfoUI1);
            this.xcontainer.Controls.Add(this.xexport);
            this.xcontainer.Controls.Add(this.xstatsb);
            this.xcontainer.Controls.Add(this.xsluiten);
            this.xcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xcontainer.Location = new System.Drawing.Point(10, 60);
            this.xcontainer.Margin = new System.Windows.Forms.Padding(0);
            this.xcontainer.Name = "xcontainer";
            this.xcontainer.Padding = new System.Windows.Forms.Padding(10);
            this.xcontainer.Size = new System.Drawing.Size(867, 518);
            this.xcontainer.TabIndex = 11;
            // 
            // xexport
            // 
            this.xexport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xexport.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xexport.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xexport.ForeColor = System.Drawing.Color.Black;
            this.xexport.Image = global::ProductieManager.Properties.Resources.microsoft_excel_22733;
            this.xexport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xexport.Location = new System.Drawing.Point(423, 477);
            this.xexport.Name = "xexport";
            this.xexport.Size = new System.Drawing.Size(152, 35);
            this.xexport.TabIndex = 7;
            this.xexport.Text = "Exporteer";
            this.xexport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xexport.UseVisualStyleBackColor = true;
            this.xexport.Visible = false;
            this.xexport.Click += new System.EventHandler(this.xexport_Click);
            // 
            // xstatsb
            // 
            this.xstatsb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xstatsb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xstatsb.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatsb.ForeColor = System.Drawing.Color.Black;
            this.xstatsb.Image = global::ProductieManager.Properties.Resources.Statics_Icon_32_32;
            this.xstatsb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xstatsb.Location = new System.Drawing.Point(581, 477);
            this.xstatsb.Name = "xstatsb";
            this.xstatsb.Size = new System.Drawing.Size(152, 35);
            this.xstatsb.TabIndex = 6;
            this.xstatsb.Text = "Statistieken";
            this.xstatsb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xstatsb.UseVisualStyleBackColor = true;
            this.xstatsb.Visible = false;
            this.xstatsb.Click += new System.EventHandler(this.xstatsb_Click);
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.ForeColor = System.Drawing.Color.Black;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(739, 477);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(115, 35);
            this.xsluiten.TabIndex = 5;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // productieInfoUI1
            // 
            this.productieInfoUI1.AllowVerpakkingEdit = false;
            this.productieInfoUI1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.productieInfoUI1.AutoScroll = true;
            this.productieInfoUI1.BackColor = System.Drawing.Color.White;
            this.productieInfoUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieInfoUI1.Location = new System.Drawing.Point(10, 10);
            this.productieInfoUI1.Margin = new System.Windows.Forms.Padding(4);
            this.productieInfoUI1.Name = "productieInfoUI1";
            this.productieInfoUI1.ShowAantal = false;
            this.productieInfoUI1.Size = new System.Drawing.Size(847, 460);
            this.productieInfoUI1.TabIndex = 0;
            // 
            // ProductieInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this.xsluiten;
            this.ClientSize = new System.Drawing.Size(887, 588);
            this.Controls.Add(this.xcontainer);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(680, 340);
            this.Name = "ProductieInfoForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Productie Info";
            this.xcontainer.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel xcontainer;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xstatsb;
        private System.Windows.Forms.Button xexport;
        private Controls.ProductieInfoUI productieInfoUI1;
    }
}