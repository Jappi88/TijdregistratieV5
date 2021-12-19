
namespace Forms
{
    partial class VerpakkingInstructieForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VerpakkingInstructieForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xwijzig = new System.Windows.Forms.Button();
            this.xsluiten = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.verpakkingInstructieUI1 = new Controls.VerpakkingInstructieUI();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xwijzig);
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(10, 443);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(914, 47);
            this.panel1.TabIndex = 1;
            // 
            // xwijzig
            // 
            this.xwijzig.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xwijzig.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xwijzig.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xwijzig.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xwijzig.Location = new System.Drawing.Point(661, 6);
            this.xwijzig.Name = "xwijzig";
            this.xwijzig.Size = new System.Drawing.Size(122, 34);
            this.xwijzig.TabIndex = 1;
            this.xwijzig.Text = "Wijzig";
            this.xwijzig.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xwijzig, "Wijzig gegevens");
            this.xwijzig.UseVisualStyleBackColor = true;
            this.xwijzig.Click += new System.EventHandler(this.xwijzig_Click);
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(789, 6);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(122, 34);
            this.xsluiten.TabIndex = 0;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xsluiten, "Sluit venster");
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // verpakkingInstructieUI1
            // 
            this.verpakkingInstructieUI1.AllowEditMode = false;
            this.verpakkingInstructieUI1.AutoScroll = true;
            this.verpakkingInstructieUI1.BackColor = System.Drawing.Color.White;
            this.verpakkingInstructieUI1.BodyColor = System.Drawing.Color.Empty;
            this.verpakkingInstructieUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.verpakkingInstructieUI1.IsEditmode = false;
            this.verpakkingInstructieUI1.Location = new System.Drawing.Point(10, 60);
            this.verpakkingInstructieUI1.Name = "verpakkingInstructieUI1";
            this.verpakkingInstructieUI1.Padding = new System.Windows.Forms.Padding(5);
            this.verpakkingInstructieUI1.Productie = null;
            this.verpakkingInstructieUI1.Size = new System.Drawing.Size(914, 383);
            this.verpakkingInstructieUI1.TabIndex = 0;
            this.verpakkingInstructieUI1.TextColor = System.Drawing.Color.Empty;
            this.verpakkingInstructieUI1.Title = null;
            // 
            // VerpakkingInstructieForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 500);
            this.Controls.Add(this.verpakkingInstructieUI1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(700, 500);
            this.Name = "VerpakkingInstructieForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Brown;
            this.Text = "Verpakking Instructie";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VerpakkingInstructieForm_FormClosing);
            this.Shown += new System.EventHandler(this.VerpakkingInstructieForm_Shown);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.VerpakkingInstructieUI verpakkingInstructieUI1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xwijzig;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}