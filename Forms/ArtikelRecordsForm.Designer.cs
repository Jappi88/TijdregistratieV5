namespace Forms
{
    partial class ArtikelRecordsForm
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
            this.artikelRecordsUI1 = new Forms.ArtikelRecords.ArtikelRecordsUI();
            this.SuspendLayout();
            // 
            // artikelRecordsUI1
            // 
            this.artikelRecordsUI1.BackColor = System.Drawing.Color.White;
            this.artikelRecordsUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.artikelRecordsUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.artikelRecordsUI1.Location = new System.Drawing.Point(20, 60);
            this.artikelRecordsUI1.Margin = new System.Windows.Forms.Padding(4);
            this.artikelRecordsUI1.Name = "artikelRecordsUI1";
            this.artikelRecordsUI1.Size = new System.Drawing.Size(923, 511);
            this.artikelRecordsUI1.TabIndex = 0;
            this.artikelRecordsUI1.StatusTextChanged += new System.EventHandler(this.artikelRecordsUI1_StatusTextChanged);
            this.artikelRecordsUI1.CloseClicked += new System.EventHandler(this.artikelRecordsUI1_CloseClicked);
            // 
            // ArtikelRecordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(963, 576);
            this.Controls.Add(this.artikelRecordsUI1);
            this.Name = "ArtikelRecordsForm";
            this.Padding = new System.Windows.Forms.Padding(20, 60, 20, 5);
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Artikel Records";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ArtikelRecordsForm_FormClosing);
            this.Shown += new System.EventHandler(this.ArtikelRecordsForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private ArtikelRecords.ArtikelRecordsUI artikelRecordsUI1;
    }
}