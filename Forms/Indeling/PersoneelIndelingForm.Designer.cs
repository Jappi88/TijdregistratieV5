namespace Forms
{
    partial class PersoneelIndelingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PersoneelIndelingForm));
            this.personeelIndelingUI1 = new Forms.PersoneelIndelingUI();
            this.SuspendLayout();
            // 
            // personeelIndelingUI1
            // 
            this.personeelIndelingUI1.BackColor = System.Drawing.Color.White;
            this.personeelIndelingUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.personeelIndelingUI1.Location = new System.Drawing.Point(20, 60);
            this.personeelIndelingUI1.Name = "personeelIndelingUI1";
            this.personeelIndelingUI1.Size = new System.Drawing.Size(1085, 535);
            this.personeelIndelingUI1.TabIndex = 0;
            this.personeelIndelingUI1.StatusTextChanged += new System.EventHandler(this.personeelIndelingUI1_StatusTextChanged);
            // 
            // PersoneelIndelingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1125, 615);
            this.Controls.Add(this.personeelIndelingUI1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "PersoneelIndelingForm";
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Personeel Indeling";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PersoneelIndelingForm_FormClosing);
            this.Shown += new System.EventHandler(this.PersoneelIndelingForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private PersoneelIndelingUI personeelIndelingUI1;
    }
}