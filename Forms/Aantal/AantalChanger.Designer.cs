
namespace Forms
{
    partial class AantalChanger
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AantalChanger));
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.metroButton2 = new MetroFramework.Controls.MetroButton();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.xaantal = new System.Windows.Forms.NumericUpDown();
            this.xtotaal = new MetroFramework.Controls.MetroLabel();
            this.xinfolabel = new MetroFramework.Controls.MetroLabel();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xaantal)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(80, 155);
            this.panel1.TabIndex = 0;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.Count_tool_34564__1_;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(80, 155);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.metroButton2);
            this.panel2.Controls.Add(this.metroButton1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(100, 174);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(509, 41);
            this.panel2.TabIndex = 2;
            // 
            // metroButton2
            // 
            this.metroButton2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroButton2.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButton2.Location = new System.Drawing.Point(394, 3);
            this.metroButton2.Name = "metroButton2";
            this.metroButton2.Size = new System.Drawing.Size(112, 34);
            this.metroButton2.TabIndex = 3;
            this.metroButton2.Text = "&Annuleren";
            this.metroButton2.UseSelectable = true;
            this.metroButton2.Click += new System.EventHandler(this.button1_Click);
            // 
            // metroButton1
            // 
            this.metroButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.metroButton1.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.metroButton1.Location = new System.Drawing.Point(276, 3);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(112, 34);
            this.metroButton1.TabIndex = 2;
            this.metroButton1.Text = "&OK";
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.button2_Click);
            // 
            // xaantal
            // 
            this.xaantal.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantal.Location = new System.Drawing.Point(179, 99);
            this.xaantal.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantal.Name = "xaantal";
            this.xaantal.Size = new System.Drawing.Size(200, 29);
            this.xaantal.TabIndex = 3;
            this.xaantal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xaantal.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.xaantal_KeyPress);
            // 
            // xtotaal
            // 
            this.xtotaal.BackColor = System.Drawing.Color.Transparent;
            this.xtotaal.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.xtotaal.Location = new System.Drawing.Point(385, 99);
            this.xtotaal.Name = "xtotaal";
            this.xtotaal.Size = new System.Drawing.Size(98, 29);
            this.xtotaal.TabIndex = 5;
            this.xtotaal.Text = "/10000";
            this.xtotaal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xinfolabel
            // 
            this.xinfolabel.AutoSize = true;
            this.xinfolabel.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.xinfolabel.Location = new System.Drawing.Point(106, 60);
            this.xinfolabel.Name = "xinfolabel";
            this.xinfolabel.Size = new System.Drawing.Size(377, 25);
            this.xinfolabel.TabIndex = 6;
            this.xinfolabel.Text = "Geef het aantal door dat gemaakt moet worden.";
            // 
            // AantalChanger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(629, 235);
            this.Controls.Add(this.xinfolabel);
            this.Controls.Add(this.xtotaal);
            this.Controls.Add(this.xaantal);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(500, 225);
            this.Name = "AantalChanger";
            this.Resizable = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Wijzig Aantal";
            this.Title = "Wijzig Aantal";
            this.Shown += new System.EventHandler(this.AantalChanger_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xaantal)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.NumericUpDown xaantal;
        private MetroFramework.Controls.MetroButton metroButton2;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroLabel xtotaal;
        private MetroFramework.Controls.MetroLabel xinfolabel;
    }
}