namespace Forms
{
    partial class WerkplaatsIndelingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WerkplaatsIndelingForm));
            this.werkplaatsIndeling1 = new Forms.WerkplaatsIndelingUI();
            this.SuspendLayout();
            // 
            // werkplaatsIndeling1
            // 
            this.werkplaatsIndeling1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.werkplaatsIndeling1.Location = new System.Drawing.Point(20, 60);
            this.werkplaatsIndeling1.Name = "werkplaatsIndeling1";
            this.werkplaatsIndeling1.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.werkplaatsIndeling1.Size = new System.Drawing.Size(872, 461);
            this.werkplaatsIndeling1.TabIndex = 0;
            this.werkplaatsIndeling1.StatusTextChanged += new System.EventHandler(this.werkplaatsIndeling1_StatusTextChanged);
            // 
            // WerkplaatsIndelingForm
            // 
            this.ClientSize = new System.Drawing.Size(912, 541);
            this.Controls.Add(this.werkplaatsIndeling1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WerkplaatsIndelingForm";
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Werkplaats Indeling";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PersoneelIndelingForm_FormClosing);
            this.Shown += new System.EventHandler(this.PersoneelIndelingForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private WerkplaatsIndelingUI werkplaatsIndeling1;
    }
}