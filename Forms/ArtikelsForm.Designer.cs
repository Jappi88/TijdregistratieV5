namespace Forms
{
    partial class ArtikelsForm
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
            this.artikelsUI1 = new Controls.ArtikelsUI();
            this.SuspendLayout();
            // 
            // artikelsUI1
            // 
            this.artikelsUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.artikelsUI1.Location = new System.Drawing.Point(20, 60);
            this.artikelsUI1.Name = "artikelsUI1";
            this.artikelsUI1.SelectedArtikelNr = null;
            this.artikelsUI1.Size = new System.Drawing.Size(1078, 527);
            this.artikelsUI1.TabIndex = 0;
            this.artikelsUI1.StatusTextChanged += new System.EventHandler(this.artikelsUI1_StatusTextChanged);
            // 
            // ArtikelsForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1118, 607);
            this.Controls.Add(this.artikelsUI1);
            this.Name = "ArtikelsForm";
            this.Style = MetroFramework.MetroColorStyle.Silver;
            this.Text = "Alle Artikelen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArtikelsForm_FormClosing);
            this.Shown += new System.EventHandler(this.ArtikelsForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ArtikelsUI artikelsUI1;
    }
}