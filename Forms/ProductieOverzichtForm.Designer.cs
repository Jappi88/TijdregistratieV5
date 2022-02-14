namespace Forms
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
            this.productieOverzichtUI1 = new Controls.ProductieOverzichtUI();
            this.SuspendLayout();
            // 
            // productieOverzichtUI1
            // 
            this.productieOverzichtUI1.BackColor = System.Drawing.Color.White;
            this.productieOverzichtUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieOverzichtUI1.Location = new System.Drawing.Point(20, 60);
            this.productieOverzichtUI1.Name = "productieOverzichtUI1";
            this.productieOverzichtUI1.Size = new System.Drawing.Size(765, 385);
            this.productieOverzichtUI1.TabIndex = 0;
            // 
            // ProductieOverzichtForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackLocation = MetroFramework.Forms.BackLocation.TopLeft;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.None;
            this.ClientSize = new System.Drawing.Size(805, 465);
            this.Controls.Add(this.productieOverzichtUI1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ProductieOverzichtForm";
            this.Text = "Productie Volgorde";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ProductieOverzichtForm_FormClosing);
            this.Shown += new System.EventHandler(this.ProductieOverzichtForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ProductieOverzichtUI productieOverzichtUI1;
    }
}