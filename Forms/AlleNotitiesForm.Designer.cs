namespace Forms
{
    partial class AlleNotitiesForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlleNotitiesForm));
            this.alleNotitiesUI1 = new Controls.AlleNotitiesUI();
            this.SuspendLayout();
            // 
            // alleNotitiesUI1
            // 
            this.alleNotitiesUI1.BackColor = System.Drawing.Color.White;
            this.alleNotitiesUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alleNotitiesUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alleNotitiesUI1.Location = new System.Drawing.Point(20, 60);
            this.alleNotitiesUI1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.alleNotitiesUI1.Name = "alleNotitiesUI1";
            this.alleNotitiesUI1.Size = new System.Drawing.Size(798, 402);
            this.alleNotitiesUI1.TabIndex = 0;
            // 
            // AlleNotitiesForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(838, 482);
            this.Controls.Add(this.alleNotitiesUI1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AlleNotitiesForm";
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Alle Notities";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlleNotitiesForm_FormClosing);
            this.Shown += new System.EventHandler(this.AlleNotitiesForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AlleNotitiesUI alleNotitiesUI1;
    }
}