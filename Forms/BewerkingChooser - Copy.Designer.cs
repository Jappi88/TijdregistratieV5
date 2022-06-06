
namespace Forms
{
    partial class BewerkingChooser
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BewerkingChooser));
            this.xbewerkingen = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xanuleren = new System.Windows.Forms.Button();
            this.xok = new System.Windows.Forms.Button();
            this.ximage = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ximage)).BeginInit();
            this.SuspendLayout();
            // 
            // xbewerkingen
            // 
            this.xbewerkingen.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xbewerkingen.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.xbewerkingen.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.xbewerkingen.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbewerkingen.FormattingEnabled = true;
            this.xbewerkingen.Location = new System.Drawing.Point(116, 101);
            this.xbewerkingen.Name = "xbewerkingen";
            this.xbewerkingen.Size = new System.Drawing.Size(324, 29);
            this.xbewerkingen.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(110, 164);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(330, 36);
            this.panel1.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xanuleren);
            this.panel2.Controls.Add(this.xok);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(78, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(252, 36);
            this.panel2.TabIndex = 3;
            // 
            // xanuleren
            // 
            this.xanuleren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xanuleren.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xanuleren.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xanuleren.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xanuleren.Location = new System.Drawing.Point(129, 2);
            this.xanuleren.Name = "xanuleren";
            this.xanuleren.Size = new System.Drawing.Size(120, 34);
            this.xanuleren.TabIndex = 3;
            this.xanuleren.Text = "&Sluiten";
            this.xanuleren.UseVisualStyleBackColor = true;
            this.xanuleren.Click += new System.EventHandler(this.xanuleren_Click);
            // 
            // xok
            // 
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xok.Location = new System.Drawing.Point(3, 2);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(120, 34);
            this.xok.TabIndex = 2;
            this.xok.Text = "&OK";
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // ximage
            // 
            this.ximage.Dock = System.Windows.Forms.DockStyle.Left;
            this.ximage.Image = global::ProductieManager.Properties.Resources.operation;
            this.ximage.Location = new System.Drawing.Point(20, 60);
            this.ximage.Name = "ximage";
            this.ximage.Size = new System.Drawing.Size(90, 140);
            this.ximage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.ximage.TabIndex = 5;
            this.ximage.TabStop = false;
            // 
            // BewerkingChooser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(460, 220);
            this.Controls.Add(this.xbewerkingen);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.ximage);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(460, 220);
            this.Name = "BewerkingChooser";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kies Bewerking";
            this.Title = "Kies Bewerking";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ximage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox xbewerkingen;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xanuleren;
        private System.Windows.Forms.Button xok;
        private System.Windows.Forms.PictureBox ximage;
    }
}