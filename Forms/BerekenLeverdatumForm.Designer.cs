
namespace Forms
{
    partial class BerekenLeverdatumForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BerekenLeverdatumForm));
            this.berekenLeverdatumUI1 = new Controls.BerekenLeverdatumUI();
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // berekenLeverdatumUI1
            // 
            this.berekenLeverdatumUI1.ArtikelNr = "";
            this.berekenLeverdatumUI1.BackColor = System.Drawing.Color.White;
            this.berekenLeverdatumUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.berekenLeverdatumUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.berekenLeverdatumUI1.Location = new System.Drawing.Point(20, 60);
            this.berekenLeverdatumUI1.Name = "berekenLeverdatumUI1";
            this.berekenLeverdatumUI1.Padding = new System.Windows.Forms.Padding(5);
            this.berekenLeverdatumUI1.Size = new System.Drawing.Size(701, 392);
            this.berekenLeverdatumUI1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 452);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(701, 35);
            this.panel1.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(610, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(91, 35);
            this.button1.TabIndex = 0;
            this.button1.Text = "Sluiten";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // BerekenLeverdatumForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(741, 507);
            this.Controls.Add(this.berekenLeverdatumUI1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "BerekenLeverdatumForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Bereken Leverdatum";
            this.Title = "Bereken Leverdatum";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.BerekenLeverdatumUI berekenLeverdatumUI1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
    }
}