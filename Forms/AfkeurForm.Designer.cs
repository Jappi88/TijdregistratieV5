
namespace Forms
{
    partial class AfkeurForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AfkeurForm));
            this.xok = new MetroFramework.Controls.MetroButton();
            this.xannuleren = new MetroFramework.Controls.MetroButton();
            this.xmaterialpanel = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xok.Location = new System.Drawing.Point(422, 405);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(100, 28);
            this.xok.TabIndex = 0;
            this.xok.Text = "&OK";
            this.xok.UseSelectable = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // xannuleren
            // 
            this.xannuleren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xannuleren.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xannuleren.Location = new System.Drawing.Point(528, 405);
            this.xannuleren.Name = "xannuleren";
            this.xannuleren.Size = new System.Drawing.Size(100, 28);
            this.xannuleren.TabIndex = 1;
            this.xannuleren.Text = "&Annuleren";
            this.xannuleren.UseSelectable = true;
            this.xannuleren.Click += new System.EventHandler(this.xannuleren_Click);
            // 
            // xmaterialpanel
            // 
            this.xmaterialpanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xmaterialpanel.AutoScroll = true;
            this.xmaterialpanel.Location = new System.Drawing.Point(14, 64);
            this.xmaterialpanel.Name = "xmaterialpanel";
            this.xmaterialpanel.Size = new System.Drawing.Size(623, 335);
            this.xmaterialpanel.TabIndex = 2;
            // 
            // AfkeurForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 446);
            this.Controls.Add(this.xmaterialpanel);
            this.Controls.Add(this.xannuleren);
            this.Controls.Add(this.xok);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(650, 300);
            this.Name = "AfkeurForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "AfkeurForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AfkeurForm_FormClosing);
            this.Shown += new System.EventHandler(this.AfkeurForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton xok;
        private MetroFramework.Controls.MetroButton xannuleren;
        private System.Windows.Forms.Panel xmaterialpanel;
    }
}