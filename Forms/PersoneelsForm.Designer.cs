namespace Forms
{
    partial class PersoneelsForm
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
            this.personeelsUI1 = new Controls.PersoneelsUI();
            this.SuspendLayout();
            // 
            // personeelsUI1
            // 
            this.personeelsUI1.BackColor = System.Drawing.Color.White;
            this.personeelsUI1.Bewerking = null;
            this.personeelsUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.personeelsUI1.Location = new System.Drawing.Point(20, 60);
            this.personeelsUI1.Name = "personeelsUI1";
            this.personeelsUI1.Size = new System.Drawing.Size(737, 439);
            this.personeelsUI1.TabIndex = 0;
            this.personeelsUI1.Title = null;
            this.personeelsUI1.StatusTextChanged += new System.EventHandler(this.personeelsUI1_StatusTextChanged);
            this.personeelsUI1.CloseClicked += new System.EventHandler(this.personeelsUI1_CloseClicked);
            this.personeelsUI1.OKClicked += new System.EventHandler(this.personeelsUI1_OKClicked);
            // 
            // PersoneelsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 519);
            this.Controls.Add(this.personeelsUI1);
            this.Name = "PersoneelsForm";
            this.Text = "Personeel";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PersoneelsForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.PersoneelsUI personeelsUI1;
    }
}