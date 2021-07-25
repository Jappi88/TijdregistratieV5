
namespace Forms
{
    partial class EditCriteriaForm
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
            this.filterEntryEditorUI1 = new Controls.FilterEntryEditorUI();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xok = new MetroFramework.Controls.MetroButton();
            this.xannuleren = new MetroFramework.Controls.MetroButton();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // filterEntryEditorUI1
            // 
            this.filterEntryEditorUI1.BackColor = System.Drawing.Color.White;
            this.filterEntryEditorUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterEntryEditorUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterEntryEditorUI1.Location = new System.Drawing.Point(20, 60);
            this.filterEntryEditorUI1.Margin = new System.Windows.Forms.Padding(5);
            this.filterEntryEditorUI1.Name = "filterEntryEditorUI1";
            this.filterEntryEditorUI1.Padding = new System.Windows.Forms.Padding(5);
            this.filterEntryEditorUI1.Size = new System.Drawing.Size(560, 309);
            this.filterEntryEditorUI1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xok);
            this.panel1.Controls.Add(this.xannuleren);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 369);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(560, 41);
            this.panel1.TabIndex = 1;
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xok.Location = new System.Drawing.Point(351, 4);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(100, 34);
            this.xok.TabIndex = 3;
            this.xok.Text = "OK";
            this.xok.UseSelectable = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // xannuleren
            // 
            this.xannuleren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xannuleren.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xannuleren.Location = new System.Drawing.Point(457, 4);
            this.xannuleren.Name = "xannuleren";
            this.xannuleren.Size = new System.Drawing.Size(100, 34);
            this.xannuleren.TabIndex = 2;
            this.xannuleren.Text = "Annuleren";
            this.xannuleren.UseSelectable = true;
            this.xannuleren.Click += new System.EventHandler(this.xannuleren_Click);
            // 
            // EditCriteriaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 430);
            this.Controls.Add(this.filterEntryEditorUI1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(600, 430);
            this.Name = "EditCriteriaForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Brown;
            this.Text = "Wijzig Criteria";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.FilterEntryEditorUI filterEntryEditorUI1;
        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroButton xok;
        private MetroFramework.Controls.MetroButton xannuleren;
    }
}