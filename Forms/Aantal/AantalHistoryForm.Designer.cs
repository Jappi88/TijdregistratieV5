namespace Forms.Aantal
{
    partial class AantalHistoryForm
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
            this.aantalGemaaktHistoryUI1 = new Forms.Aantal.Controls.AantalGemaaktHistoryUI();
            this.SuspendLayout();
            // 
            // aantalGemaaktHistoryUI1
            // 
            this.aantalGemaaktHistoryUI1.BackColor = System.Drawing.Color.White;
            this.aantalGemaaktHistoryUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.aantalGemaaktHistoryUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.aantalGemaaktHistoryUI1.Location = new System.Drawing.Point(20, 60);
            this.aantalGemaaktHistoryUI1.Margin = new System.Windows.Forms.Padding(4);
            this.aantalGemaaktHistoryUI1.Name = "aantalGemaaktHistoryUI1";
            this.aantalGemaaktHistoryUI1.Selected = null;
            this.aantalGemaaktHistoryUI1.Size = new System.Drawing.Size(741, 364);
            this.aantalGemaaktHistoryUI1.TabIndex = 0;
            // 
            // AantalHistoryForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(781, 444);
            this.Controls.Add(this.aantalGemaaktHistoryUI1);
            this.Name = "AantalHistoryForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Aantal Geschiedenis";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AantalHistoryForm_FormClosing);
            this.Shown += new System.EventHandler(this.AantalHistoryForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AantalGemaaktHistoryUI aantalGemaaktHistoryUI1;
    }
}