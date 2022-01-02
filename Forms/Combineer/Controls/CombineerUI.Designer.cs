namespace Controls
{
    partial class CombineerUI
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.xomschrijving = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xaddproductie = new System.Windows.Forms.Button();
            this.xcontainer = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xomschrijving);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 10, 10, 0);
            this.panel1.Size = new System.Drawing.Size(797, 115);
            this.panel1.TabIndex = 0;
            // 
            // xomschrijving
            // 
            this.xomschrijving.AutoSize = false;
            this.xomschrijving.BackColor = System.Drawing.SystemColors.Window;
            this.xomschrijving.BaseStylesheet = null;
            this.xomschrijving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xomschrijving.Location = new System.Drawing.Point(84, 10);
            this.xomschrijving.Name = "xomschrijving";
            this.xomschrijving.Size = new System.Drawing.Size(703, 105);
            this.xomschrijving.TabIndex = 1;
            this.xomschrijving.Text = null;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.Merge_arrows_128x128;
            this.pictureBox1.Location = new System.Drawing.Point(0, 10);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Padding = new System.Windows.Forms.Padding(5);
            this.pictureBox1.Size = new System.Drawing.Size(84, 105);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // xaddproductie
            // 
            this.xaddproductie.Dock = System.Windows.Forms.DockStyle.Right;
            this.xaddproductie.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xaddproductie.Image = global::ProductieManager.Properties.Resources.new_file_40454;
            this.xaddproductie.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xaddproductie.Location = new System.Drawing.Point(620, 0);
            this.xaddproductie.Name = "xaddproductie";
            this.xaddproductie.Size = new System.Drawing.Size(177, 36);
            this.xaddproductie.TabIndex = 2;
            this.xaddproductie.Text = "Voeg Productie Toe";
            this.xaddproductie.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xaddproductie.UseVisualStyleBackColor = true;
            this.xaddproductie.Click += new System.EventHandler(this.xaddproductie_Click);
            // 
            // xcontainer
            // 
            this.xcontainer.AutoScroll = true;
            this.xcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xcontainer.Location = new System.Drawing.Point(5, 120);
            this.xcontainer.Name = "xcontainer";
            this.xcontainer.Padding = new System.Windows.Forms.Padding(10);
            this.xcontainer.Size = new System.Drawing.Size(797, 281);
            this.xcontainer.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xaddproductie);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(5, 401);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(797, 36);
            this.panel2.TabIndex = 2;
            // 
            // CombineerUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xcontainer);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "CombineerUI";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(807, 442);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button xaddproductie;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel xomschrijving;
        private System.Windows.Forms.Panel xcontainer;
        private System.Windows.Forms.Panel panel2;
    }
}
