namespace Forms
{
    partial class AantalGemaaktUI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AantalGemaaktUI));
            this.xannuleer = new MetroFramework.Controls.MetroButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.xwerkplekken = new MetroFramework.Controls.MetroComboBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xaantalgemaaktlabel = new System.Windows.Forms.Label();
            this.xnextb = new MetroFramework.Controls.MetroButton();
            this.label2 = new System.Windows.Forms.Label();
            this.xaantal = new System.Windows.Forms.Label();
            this.xaantalgemaakt = new System.Windows.Forms.NumericUpDown();
            this.panel4 = new System.Windows.Forms.Panel();
            this.xwerkinfopanel = new HtmlRenderer.HtmlPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalgemaakt)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // xannuleer
            // 
            this.xannuleer.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xannuleer.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xannuleer.Location = new System.Drawing.Point(3, 0);
            this.xannuleer.Name = "xannuleer";
            this.xannuleer.Size = new System.Drawing.Size(107, 34);
            this.xannuleer.TabIndex = 1;
            this.xannuleer.Text = "&Sluiten";
            this.xannuleer.UseSelectable = true;
            this.xannuleer.Click += new System.EventHandler(this.xannuleer_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 393);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(827, 37);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xannuleer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(714, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(113, 37);
            this.panel2.TabIndex = 3;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.xwerkplekken);
            this.panel5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(6, 3);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(316, 37);
            this.panel5.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 7);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(78, 21);
            this.label3.TabIndex = 7;
            this.label3.Text = "WerkPlek";
            // 
            // xwerkplekken
            // 
            this.xwerkplekken.FormattingEnabled = true;
            this.xwerkplekken.ItemHeight = 23;
            this.xwerkplekken.Location = new System.Drawing.Point(87, 4);
            this.xwerkplekken.Name = "xwerkplekken";
            this.xwerkplekken.Size = new System.Drawing.Size(219, 29);
            this.xwerkplekken.TabIndex = 5;
            this.xwerkplekken.UseSelectable = true;
            this.xwerkplekken.SelectedIndexChanged += new System.EventHandler(this.xwerkplekken_SelectedIndexChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xaantalgemaaktlabel);
            this.panel3.Controls.Add(this.xnextb);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.xaantal);
            this.panel3.Controls.Add(this.xaantalgemaakt);
            this.panel3.Location = new System.Drawing.Point(320, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(389, 37);
            this.panel3.TabIndex = 4;
            // 
            // xaantalgemaaktlabel
            // 
            this.xaantalgemaaktlabel.AutoSize = true;
            this.xaantalgemaaktlabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantalgemaaktlabel.Location = new System.Drawing.Point(195, 7);
            this.xaantalgemaaktlabel.Name = "xaantalgemaaktlabel";
            this.xaantalgemaaktlabel.Size = new System.Drawing.Size(56, 21);
            this.xaantalgemaaktlabel.TabIndex = 8;
            this.xaantalgemaaktlabel.Text = "Aantal";
            // 
            // xnextb
            // 
            this.xnextb.BackgroundImage = global::ProductieManager.Properties.Resources.arrow_right_15600;
            this.xnextb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.xnextb.Location = new System.Drawing.Point(134, 4);
            this.xnextb.Name = "xnextb";
            this.xnextb.Size = new System.Drawing.Size(50, 29);
            this.xnextb.TabIndex = 7;
            this.toolTip1.SetToolTip(this.xnextb, "Volgende Werkplek");
            this.xnextb.UseSelectable = true;
            this.xnextb.Click += new System.EventHandler(this.xnextb_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(257, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "/";
            // 
            // xaantal
            // 
            this.xaantal.AutoSize = true;
            this.xaantal.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantal.Location = new System.Drawing.Point(292, 7);
            this.xaantal.Name = "xaantal";
            this.xaantal.Size = new System.Drawing.Size(56, 21);
            this.xaantal.TabIndex = 5;
            this.xaantal.Text = "Aantal";
            // 
            // xaantalgemaakt
            // 
            this.xaantalgemaakt.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantalgemaakt.Location = new System.Drawing.Point(3, 4);
            this.xaantalgemaakt.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantalgemaakt.Name = "xaantalgemaakt";
            this.xaantalgemaakt.Size = new System.Drawing.Size(130, 29);
            this.xaantalgemaakt.TabIndex = 4;
            this.xaantalgemaakt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Aantal_KeyPress);
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.xwerkinfopanel);
            this.panel4.Controls.Add(this.panel6);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(138, 60);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(709, 333);
            this.panel4.TabIndex = 5;
            // 
            // xwerkinfopanel
            // 
            this.xwerkinfopanel.AutoScroll = true;
            this.xwerkinfopanel.AutoScrollMinSize = new System.Drawing.Size(709, 17);
            this.xwerkinfopanel.BackColor = System.Drawing.SystemColors.Window;
            this.xwerkinfopanel.BaseStylesheet = null;
            this.xwerkinfopanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.xwerkinfopanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xwerkinfopanel.Location = new System.Drawing.Point(0, 0);
            this.xwerkinfopanel.Name = "xwerkinfopanel";
            this.xwerkinfopanel.Size = new System.Drawing.Size(709, 293);
            this.xwerkinfopanel.TabIndex = 3;
            this.xwerkinfopanel.Text = "werkinfo";
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.panel3);
            this.panel6.Controls.Add(this.panel5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(0, 293);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(709, 40);
            this.panel6.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.ic_info_outline_128_28513;
            this.pictureBox1.Location = new System.Drawing.Point(20, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 333);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Aantal Gemaakt";
            // 
            // AantalGemaaktUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(867, 450);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(850, 450);
            this.Name = "AantalGemaaktUI";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Update Aantal Gemaakt";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AantalGemaaktUI_FormClosing);
            this.Shown += new System.EventHandler(this.AantalUI_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalgemaakt)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private MetroFramework.Controls.MetroButton xannuleer;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label xaantal;
        private System.Windows.Forms.NumericUpDown xaantalgemaakt;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label xaantalgemaaktlabel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.ToolTip toolTip1;
        private MetroFramework.Controls.MetroComboBox xwerkplekken;
        private MetroFramework.Controls.MetroButton xnextb;
        private HtmlRenderer.HtmlPanel xwerkinfopanel;
    }
}