
namespace Forms
{
    partial class InkomendMailInfoForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xtextfield = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 516);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(891, 41);
            this.panel1.TabIndex = 0;
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(788, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(100, 34);
            this.xsluiten.TabIndex = 0;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xtextfield
            // 
            this.xtextfield.AutoScroll = true;
            this.xtextfield.AutoScrollMinSize = new System.Drawing.Size(891, 20);
            this.xtextfield.BackColor = System.Drawing.Color.Transparent;
            this.xtextfield.BaseStylesheet = null;
            this.xtextfield.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xtextfield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtextfield.Location = new System.Drawing.Point(20, 60);
            this.xtextfield.Name = "xtextfield";
            this.xtextfield.Size = new System.Drawing.Size(891, 456);
            this.xtextfield.TabIndex = 5;
            this.xtextfield.Text = "Text Veld";
            // 
            // InkomendMailInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(931, 577);
            this.Controls.Add(this.xtextfield);
            this.Controls.Add(this.panel1);
            this.Name = "InkomendMailInfoForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Mail Opstellen";
            this.Title = "Mail Opstellen";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xsluiten;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel xtextfield;
    }
}