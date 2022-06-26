namespace Forms
{
    partial class MateriaalForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MateriaalForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xOpslaan = new System.Windows.Forms.Button();
            this.materiaalUI1 = new Forms.MateriaalUI();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 551);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(994, 44);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xsluiten);
            this.panel2.Controls.Add(this.xOpslaan);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(743, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(251, 44);
            this.panel2.TabIndex = 1;
            // 
            // xsluiten
            // 
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.ForeColor = System.Drawing.Color.Black;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(128, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(119, 38);
            this.xsluiten.TabIndex = 4;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xannuleren_Click);
            // 
            // xOpslaan
            // 
            this.xOpslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOpslaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOpslaan.ForeColor = System.Drawing.Color.Black;
            this.xOpslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xOpslaan.Location = new System.Drawing.Point(3, 3);
            this.xOpslaan.Name = "xOpslaan";
            this.xOpslaan.Size = new System.Drawing.Size(119, 38);
            this.xOpslaan.TabIndex = 5;
            this.xOpslaan.Text = "Opslaan";
            this.xOpslaan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xOpslaan.UseVisualStyleBackColor = true;
            this.xOpslaan.Click += new System.EventHandler(this.xok_Click);
            // 
            // materiaalUI1
            // 
            this.materiaalUI1.BackColor = System.Drawing.Color.Transparent;
            this.materiaalUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.materiaalUI1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.materiaalUI1.Formulier = null;
            this.materiaalUI1.Location = new System.Drawing.Point(20, 60);
            this.materiaalUI1.Name = "materiaalUI1";
            this.materiaalUI1.Padding = new System.Windows.Forms.Padding(5);
            this.materiaalUI1.Size = new System.Drawing.Size(994, 491);
            this.materiaalUI1.TabIndex = 1;
            // 
            // MateriaalForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1034, 615);
            this.Controls.Add(this.materiaalUI1);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(0, 0);
            this.Name = "MateriaalForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MateriaalForm";
            this.Title = "MateriaalForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MateriaalForm_FormClosing);
            this.Shown += new System.EventHandler(this.MateriaalForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xOpslaan;
        private MateriaalUI materiaalUI1;
    }
}