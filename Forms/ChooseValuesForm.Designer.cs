namespace Forms
{
    partial class ChooseValuesForm
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
            this.xlayoutpanel = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xok = new System.Windows.Forms.Button();
            this.xcancel = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xlayoutpanel
            // 
            this.xlayoutpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xlayoutpanel.Location = new System.Drawing.Point(20, 60);
            this.xlayoutpanel.Name = "xlayoutpanel";
            this.xlayoutpanel.Padding = new System.Windows.Forms.Padding(50);
            this.xlayoutpanel.Size = new System.Drawing.Size(509, 205);
            this.xlayoutpanel.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xok);
            this.panel1.Controls.Add(this.xcancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 265);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(509, 40);
            this.panel1.TabIndex = 1;
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.xok.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xok.Location = new System.Drawing.Point(312, 3);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(94, 34);
            this.xok.TabIndex = 1;
            this.xok.Text = "OK";
            this.xok.UseVisualStyleBackColor = true;
            // 
            // xcancel
            // 
            this.xcancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xcancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xcancel.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xcancel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xcancel.Location = new System.Drawing.Point(412, 3);
            this.xcancel.Name = "xcancel";
            this.xcancel.Size = new System.Drawing.Size(94, 34);
            this.xcancel.TabIndex = 0;
            this.xcancel.Text = "Sluiten";
            this.xcancel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xcancel.UseVisualStyleBackColor = true;
            // 
            // ChooseValuesForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(549, 325);
            this.Controls.Add(this.xlayoutpanel);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ChooseValuesForm";
            this.SaveLastSize = false;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Kies Waardes";
            this.Title = "Kies Waardes";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel xlayoutpanel;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xcancel;
        private System.Windows.Forms.Button xok;
    }
}