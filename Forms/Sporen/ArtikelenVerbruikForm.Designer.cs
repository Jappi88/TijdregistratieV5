namespace Forms
{
    partial class ArtikelenVerbruikForm
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
            this.artikelenVerbruikUI1 = new Controls.ArtikelenVerbruikUI();
            this.SuspendLayout();
            // 
            // artikelenVerbruikUI1
            // 
            this.artikelenVerbruikUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.artikelenVerbruikUI1.Location = new System.Drawing.Point(20, 60);
            this.artikelenVerbruikUI1.Name = "artikelenVerbruikUI1";
            this.artikelenVerbruikUI1.Size = new System.Drawing.Size(861, 441);
            this.artikelenVerbruikUI1.TabIndex = 0;
            this.artikelenVerbruikUI1.CloseClicked += new System.EventHandler(this.artikelenVerbruikUI1_CloseClicked);
            this.artikelenVerbruikUI1.StatusTextChanged += new System.EventHandler(this.artikelenVerbruikUI1_StatusTextChanged);
            // 
            // ArtikelenVerbruikForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(901, 521);
            this.Controls.Add(this.artikelenVerbruikUI1);
            this.Name = "ArtikelenVerbruikForm";
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Artikelen Verbruik";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArtikelenVerbruikForm_FormClosing);
            this.Shown += new System.EventHandler(this.ArtikelenVerbruikForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ArtikelenVerbruikUI artikelenVerbruikUI1;
    }
}