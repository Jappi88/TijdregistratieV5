using Controls;

namespace Forms
{
    partial class StartProductie
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StartProductie));
            this.productieForm1 = new Controls.ProductieForm();
            this.SuspendLayout();
            // 
            // productieForm1
            // 
            this.productieForm1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.productieForm1.BackColor = System.Drawing.Color.White;
            this.productieForm1.Formulier = null;
            this.productieForm1.Location = new System.Drawing.Point(10, 10);
            this.productieForm1.Name = "productieForm1";
            this.productieForm1.SelectedBewerking = null;
            this.productieForm1.Size = new System.Drawing.Size(924, 565);
            this.productieForm1.TabIndex = 0;
            // 
            // StartProductie
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(944, 585);
            this.CloseButton = false;
            this.CloseButtonVisible = false;
            this.ControlBox = false;
            this.Controls.Add(this.productieForm1);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "StartProductie";
            this.Style = MetroFramework.MetroColorStyle.Custom;
            this.Text = "StartProductie";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StartProductie_FormClosing);
            this.Shown += new System.EventHandler(this.StartProductie_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private ProductieForm productieForm1;
    }
}