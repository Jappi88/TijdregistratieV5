namespace Forms
{
    partial class BijlageForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BijlageForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xclose = new System.Windows.Forms.Button();
            this.fileBrowserUI1 = new Forms.FileBrowser.FileBrowserUI();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xclose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 530);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(725, 39);
            this.panel1.TabIndex = 0;
            // 
            // xclose
            // 
            this.xclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xclose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xclose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xclose.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.xclose.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xclose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xclose.Location = new System.Drawing.Point(627, 3);
            this.xclose.Name = "xclose";
            this.xclose.Size = new System.Drawing.Size(95, 32);
            this.xclose.TabIndex = 0;
            this.xclose.Text = "Sluiten";
            this.xclose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xclose.UseVisualStyleBackColor = true;
            this.xclose.Click += new System.EventHandler(this.xclose_Click);
            // 
            // fileBrowserUI1
            // 
            this.fileBrowserUI1.AllowEditRoot = true;
            this.fileBrowserUI1.BackColor = System.Drawing.Color.White;
            this.fileBrowserUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fileBrowserUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileBrowserUI1.Location = new System.Drawing.Point(20, 60);
            this.fileBrowserUI1.Name = "fileBrowserUI1";
            this.fileBrowserUI1.RootOnlyFilledDirectories = false;
            this.fileBrowserUI1.RootPath = null;
            this.fileBrowserUI1.Size = new System.Drawing.Size(725, 470);
            this.fileBrowserUI1.TabIndex = 1;
            // 
            // BijlageForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(765, 589);
            this.Controls.Add(this.fileBrowserUI1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BijlageForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Bijlages";
            this.Title = "Bijlages";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.BijlageForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xclose;
        private FileBrowser.FileBrowserUI fileBrowserUI1;
    }
}