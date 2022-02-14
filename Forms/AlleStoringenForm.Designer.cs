namespace Forms
{
    partial class AlleStoringenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AlleStoringenForm));
            this.alleStoringenUI1 = new Controls.AlleStoringenUI();
            this.SuspendLayout();
            // 
            // alleStoringenUI1
            // 
            this.alleStoringenUI1.BackColor = System.Drawing.Color.White;
            this.alleStoringenUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.alleStoringenUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.alleStoringenUI1.Location = new System.Drawing.Point(20, 60);
            this.alleStoringenUI1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.alleStoringenUI1.MinimumSize = new System.Drawing.Size(825, 525);
            this.alleStoringenUI1.Name = "alleStoringenUI1";
            this.alleStoringenUI1.Size = new System.Drawing.Size(1028, 525);
            this.alleStoringenUI1.TabIndex = 0;
            this.alleStoringenUI1.CloseClicked += new System.EventHandler(this.alleStoringenUI1_CloseClicked);
            this.alleStoringenUI1.StatusTextChanged += new System.EventHandler(this.alleStoringenUI1_StatusChanged);
            // 
            // AlleStoringenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1068, 597);
            this.Controls.Add(this.alleStoringenUI1);
            this.Name = "AlleStoringenForm";
            this.Text = "Alle Storingen";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AlleStoringenForm_FormClosing);
            this.Shown += new System.EventHandler(this.AlleStoringenForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.AlleStoringenUI alleStoringenUI1;
    }
}