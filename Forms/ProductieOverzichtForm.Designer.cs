
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductieOverzichtForm));
            this.xcontrolpanel = new System.Windows.Forms.Panel();
            this.xloadinglabel = new System.Windows.Forms.Label();
            this.xcontrolpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // xcontrolpanel
            // 
            this.xcontrolpanel.AutoScroll = true;
            this.xcontrolpanel.Controls.Add(this.xloadinglabel);
            this.xcontrolpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xcontrolpanel.Location = new System.Drawing.Point(10, 60);
            this.xcontrolpanel.Name = "xcontrolpanel";
            this.xcontrolpanel.Size = new System.Drawing.Size(764, 394);
            this.xcontrolpanel.TabIndex = 0;
            // 
            // xloadinglabel
            // 
            this.xloadinglabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xloadinglabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xloadinglabel.Location = new System.Drawing.Point(3, 0);
            this.xloadinglabel.Name = "xloadinglabel";
            this.xloadinglabel.Size = new System.Drawing.Size(758, 394);
            this.xloadinglabel.TabIndex = 31;
            this.xloadinglabel.Text = "Overzicht aanmaken...";
            this.xloadinglabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xloadinglabel.Visible = false;
            // 
            // ProductieOverzichtForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 464);
            this.Controls.Add(this.xcontrolpanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductieOverzichtForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Lime;
            this.Text = "Productie Overzicht";
            this.Shown += new System.EventHandler(this.ProductieOverzichtForm_Shown);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(ProductieOverzichtForm_FormClosing);
            this.xcontrolpanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel xcontrolpanel;
        private System.Windows.Forms.Label xloadinglabel;
    }
}