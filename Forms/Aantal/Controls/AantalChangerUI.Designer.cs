namespace ProductieManager.Forms.Aantal.Controls
{
    partial class AantalChangerUI
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
            this.xaantalpanel = new System.Windows.Forms.Panel();
            this.xPacketGroup = new System.Windows.Forms.GroupBox();
            this.xaddPacket = new MetroFramework.Controls.MetroButton();
            this.xpacketlabel = new System.Windows.Forms.Label();
            this.xremovePacket = new MetroFramework.Controls.MetroButton();
            this.xpacketvalue = new System.Windows.Forms.NumericUpDown();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.xwerkplekken = new MetroFramework.Controls.MetroComboBox();
            this.xnextb = new MetroFramework.Controls.MetroButton();
            this.xaantalgemaakt = new System.Windows.Forms.NumericUpDown();
            this.xaantalpanel.SuspendLayout();
            this.xPacketGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpacketvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalgemaakt)).BeginInit();
            this.SuspendLayout();
            // 
            // xaantalpanel
            // 
            this.xaantalpanel.Controls.Add(this.xPacketGroup);
            this.xaantalpanel.Controls.Add(this.pictureBox1);
            this.xaantalpanel.Controls.Add(this.panel5);
            this.xaantalpanel.Controls.Add(this.xnextb);
            this.xaantalpanel.Controls.Add(this.xaantalgemaakt);
            this.xaantalpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xaantalpanel.Location = new System.Drawing.Point(0, 0);
            this.xaantalpanel.Name = "xaantalpanel";
            this.xaantalpanel.Size = new System.Drawing.Size(582, 100);
            this.xaantalpanel.TabIndex = 14;
            // 
            // xPacketGroup
            // 
            this.xPacketGroup.Controls.Add(this.xaddPacket);
            this.xPacketGroup.Controls.Add(this.xpacketlabel);
            this.xPacketGroup.Controls.Add(this.xremovePacket);
            this.xPacketGroup.Controls.Add(this.xpacketvalue);
            this.xPacketGroup.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xPacketGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xPacketGroup.Location = new System.Drawing.Point(68, 45);
            this.xPacketGroup.Name = "xPacketGroup";
            this.xPacketGroup.Size = new System.Drawing.Size(514, 55);
            this.xPacketGroup.TabIndex = 9;
            this.xPacketGroup.TabStop = false;
            this.xPacketGroup.Text = "Aantal Kisten, Bakken, Dozen of pakketten";
            this.xPacketGroup.Visible = false;
            // 
            // xaddPacket
            // 
            this.xaddPacket.BackgroundImage = global::ProductieManager.Properties.Resources.add_icon_icons_com_52393;
            this.xaddPacket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.xaddPacket.Location = new System.Drawing.Point(142, 19);
            this.xaddPacket.Name = "xaddPacket";
            this.xaddPacket.Size = new System.Drawing.Size(34, 29);
            this.xaddPacket.TabIndex = 14;
            this.xaddPacket.UseSelectable = true;
            this.xaddPacket.Click += new System.EventHandler(this.xaddPacket_Click);
            // 
            // xpacketlabel
            // 
            this.xpacketlabel.AutoSize = true;
            this.xpacketlabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xpacketlabel.Location = new System.Drawing.Point(196, 21);
            this.xpacketlabel.Name = "xpacketlabel";
            this.xpacketlabel.Size = new System.Drawing.Size(56, 21);
            this.xpacketlabel.TabIndex = 13;
            this.xpacketlabel.Text = "Aantal";
            // 
            // xremovePacket
            // 
            this.xremovePacket.BackgroundImage = global::ProductieManager.Properties.Resources.minusflat_105990;
            this.xremovePacket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.xremovePacket.Location = new System.Drawing.Point(106, 19);
            this.xremovePacket.Name = "xremovePacket";
            this.xremovePacket.Size = new System.Drawing.Size(34, 29);
            this.xremovePacket.TabIndex = 12;
            this.xremovePacket.UseSelectable = true;
            this.xremovePacket.Click += new System.EventHandler(this.xremovePacket_Click);
            // 
            // xpacketvalue
            // 
            this.xpacketvalue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xpacketvalue.Location = new System.Drawing.Point(10, 19);
            this.xpacketvalue.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xpacketvalue.Name = "xpacketvalue";
            this.xpacketvalue.Size = new System.Drawing.Size(94, 29);
            this.xpacketvalue.TabIndex = 9;
            this.xpacketvalue.ValueChanged += new System.EventHandler(this.Xpacketvalue_ValueChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.Count_tool_34564__1_;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(68, 100);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.label3);
            this.panel5.Controls.Add(this.xwerkplekken);
            this.panel5.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(74, 2);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(316, 37);
            this.panel5.TabIndex = 10;
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
            this.xwerkplekken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xwerkplekken.FormattingEnabled = true;
            this.xwerkplekken.ItemHeight = 23;
            this.xwerkplekken.Location = new System.Drawing.Point(84, 4);
            this.xwerkplekken.Name = "xwerkplekken";
            this.xwerkplekken.Size = new System.Drawing.Size(229, 29);
            this.xwerkplekken.TabIndex = 5;
            this.xwerkplekken.UseSelectable = true;
            this.xwerkplekken.SelectedIndexChanged += new System.EventHandler(this.xwerkplekken_SelectedIndexChanged);
            // 
            // xnextb
            // 
            this.xnextb.BackgroundImage = global::ProductieManager.Properties.Resources.arrow_right_15600;
            this.xnextb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.xnextb.Location = new System.Drawing.Point(518, 6);
            this.xnextb.Name = "xnextb";
            this.xnextb.Size = new System.Drawing.Size(50, 29);
            this.xnextb.TabIndex = 7;
            this.xnextb.UseSelectable = true;
            this.xnextb.Click += new System.EventHandler(this.xnextb_Click);
            // 
            // xaantalgemaakt
            // 
            this.xaantalgemaakt.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantalgemaakt.Location = new System.Drawing.Point(396, 6);
            this.xaantalgemaakt.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantalgemaakt.Name = "xaantalgemaakt";
            this.xaantalgemaakt.Size = new System.Drawing.Size(120, 29);
            this.xaantalgemaakt.TabIndex = 4;
            this.xaantalgemaakt.KeyDown += new System.Windows.Forms.KeyEventHandler(this.xaantalgemaakt_KeyDown);
            // 
            // AantalChangerUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xaantalpanel);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(582, 100);
            this.Name = "AantalChangerUI";
            this.Size = new System.Drawing.Size(582, 100);
            this.xaantalpanel.ResumeLayout(false);
            this.xPacketGroup.ResumeLayout(false);
            this.xPacketGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpacketvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalgemaakt)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel xaantalpanel;
        private System.Windows.Forms.GroupBox xPacketGroup;
        private MetroFramework.Controls.MetroButton xaddPacket;
        private System.Windows.Forms.Label xpacketlabel;
        private MetroFramework.Controls.MetroButton xremovePacket;
        private System.Windows.Forms.NumericUpDown xpacketvalue;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private MetroFramework.Controls.MetroComboBox xwerkplekken;
        private MetroFramework.Controls.MetroButton xnextb;
        private System.Windows.Forms.NumericUpDown xaantalgemaakt;
    }
}
