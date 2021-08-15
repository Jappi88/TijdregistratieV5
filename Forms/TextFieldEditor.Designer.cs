
namespace Forms
{
    partial class TextFieldEditor
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TextFieldEditor));
            this.xtextfield = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xanuleren = new System.Windows.Forms.Button();
            this.xok = new System.Windows.Forms.Button();
            this.ximage = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ximage)).BeginInit();
            this.SuspendLayout();
            // 
            // xtextfield
            // 
            this.xtextfield.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtextfield.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtextfield.Location = new System.Drawing.Point(5, 15);
            this.xtextfield.Name = "xtextfield";
            this.xtextfield.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.xtextfield.Size = new System.Drawing.Size(391, 29);
            this.xtextfield.TabIndex = 0;
            this.xtextfield.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xtextfield_KeyDown);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(129, 134);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(401, 46);
            this.panel1.TabIndex = 9;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xanuleren);
            this.panel2.Controls.Add(this.xok);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(149, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(252, 46);
            this.panel2.TabIndex = 3;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xtextfield);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(129, 60);
            this.panel3.Name = "panel3";
            this.panel3.Padding = new System.Windows.Forms.Padding(5, 15, 5, 15);
            this.panel3.Size = new System.Drawing.Size(401, 74);
            this.panel3.TabIndex = 10;
            // 
            // xanuleren
            // 
            this.xanuleren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xanuleren.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xanuleren.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xanuleren.Location = new System.Drawing.Point(129, 1);
            this.xanuleren.Name = "xanuleren";
            this.xanuleren.Size = new System.Drawing.Size(120, 38);
            this.xanuleren.TabIndex = 3;
            this.xanuleren.Text = "&Annuleren";
            this.xanuleren.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xanuleren.UseVisualStyleBackColor = true;
            this.xanuleren.Click += new System.EventHandler(this.xanuleren_Click);
            // 
            // xok
            // 
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xok.Location = new System.Drawing.Point(3, 1);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(120, 38);
            this.xok.TabIndex = 2;
            this.xok.Text = "&OK";
            this.xok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // ximage
            // 
            this.ximage.BackColor = System.Drawing.Color.Transparent;
            this.ximage.Dock = System.Windows.Forms.DockStyle.Left;
            this.ximage.Image = global::ProductieManager.Properties.Resources.text_edit_14943;
            this.ximage.Location = new System.Drawing.Point(20, 60);
            this.ximage.Name = "ximage";
            this.ximage.Size = new System.Drawing.Size(109, 120);
            this.ximage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ximage.TabIndex = 8;
            this.ximage.TabStop = false;
            // 
            // TextFieldEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 200);
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ximage);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(550, 200);
            this.Name = "TextFieldEditor";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vul In Text";
            this.Shown += new System.EventHandler(this.TextFieldEditor_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ximage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xanuleren;
        private System.Windows.Forms.Button xok;
        private System.Windows.Forms.PictureBox ximage;
        private System.Windows.Forms.TextBox xtextfield;
        private System.Windows.Forms.Panel panel3;
    }
}