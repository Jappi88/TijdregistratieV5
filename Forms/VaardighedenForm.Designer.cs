
namespace Forms
{
    partial class VaardighedenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VaardighedenForm));
            this.persoonVaardigheden1 = new Controls.PersoonVaardigheden();
            this.SuspendLayout();
            // 
            // persoonVaardigheden1
            // 
            this.persoonVaardigheden1.BackColor = System.Drawing.Color.White;
            this.persoonVaardigheden1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.persoonVaardigheden1.IsCloseAble = true;
            this.persoonVaardigheden1.Location = new System.Drawing.Point(20, 60);
            this.persoonVaardigheden1.Name = "persoonVaardigheden1";
            this.persoonVaardigheden1.Size = new System.Drawing.Size(895, 405);
            this.persoonVaardigheden1.TabIndex = 0;
            // 
            // VaardighedenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 485);
            this.Controls.Add(this.persoonVaardigheden1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "VaardighedenForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "VaardighedenForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VaardighedenForm_FormClosing);
            this.Load += new System.EventHandler(this.Form_Load);
            this.Shown += new System.EventHandler(this.VaardighedenForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.PersoonVaardigheden persoonVaardigheden1;
    }
}