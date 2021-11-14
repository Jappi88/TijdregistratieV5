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
            this.xPacketGroup = new System.Windows.Forms.GroupBox();
            this.xaddPacket = new MetroFramework.Controls.MetroButton();
            this.xpacketlabel = new System.Windows.Forms.Label();
            this.xremovePacket = new MetroFramework.Controls.MetroButton();
            this.label4 = new System.Windows.Forms.Label();
            this.xtotalpacketlabel = new System.Windows.Forms.Label();
            this.xpacketvalue = new System.Windows.Forms.NumericUpDown();
            this.xaantalgemaaktlabel = new System.Windows.Forms.Label();
            this.xnextb = new MetroFramework.Controls.MetroButton();
            this.label2 = new System.Windows.Forms.Label();
            this.xaantal = new System.Windows.Forms.Label();
            this.xaantalgemaakt = new System.Windows.Forms.NumericUpDown();
            this.panel4 = new System.Windows.Forms.Panel();
            this.productieInfoUI1 = new Controls.ProductieInfoUI();
            this.xvaluepanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.xPacketGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpacketvalue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalgemaakt)).BeginInit();
            this.panel4.SuspendLayout();
            this.xvaluepanel.SuspendLayout();
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
            this.panel1.Location = new System.Drawing.Point(20, 472);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(810, 37);
            this.panel1.TabIndex = 4;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xannuleer);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel2.Location = new System.Drawing.Point(697, 0);
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
            // panel3
            // 
            this.panel3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel3.Controls.Add(this.xPacketGroup);
            this.panel3.Controls.Add(this.xaantalgemaaktlabel);
            this.panel3.Controls.Add(this.xnextb);
            this.panel3.Controls.Add(this.label2);
            this.panel3.Controls.Add(this.xaantal);
            this.panel3.Controls.Add(this.xaantalgemaakt);
            this.panel3.Location = new System.Drawing.Point(328, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(361, 97);
            this.panel3.TabIndex = 4;
            // 
            // xPacketGroup
            // 
            this.xPacketGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xPacketGroup.Controls.Add(this.xaddPacket);
            this.xPacketGroup.Controls.Add(this.xpacketlabel);
            this.xPacketGroup.Controls.Add(this.xremovePacket);
            this.xPacketGroup.Controls.Add(this.label4);
            this.xPacketGroup.Controls.Add(this.xtotalpacketlabel);
            this.xPacketGroup.Controls.Add(this.xpacketvalue);
            this.xPacketGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xPacketGroup.Location = new System.Drawing.Point(3, 38);
            this.xPacketGroup.Name = "xPacketGroup";
            this.xPacketGroup.Size = new System.Drawing.Size(355, 57);
            this.xPacketGroup.TabIndex = 9;
            this.xPacketGroup.TabStop = false;
            this.xPacketGroup.Text = "Aantal Kisten, Bakken, Dozen of pakketten";
            // 
            // xaddPacket
            // 
            this.xaddPacket.BackgroundImage = global::ProductieManager.Properties.Resources.add_icon_icons_com_52393;
            this.xaddPacket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.xaddPacket.Location = new System.Drawing.Point(138, 21);
            this.xaddPacket.Name = "xaddPacket";
            this.xaddPacket.Size = new System.Drawing.Size(34, 29);
            this.xaddPacket.TabIndex = 14;
            this.toolTip1.SetToolTip(this.xaddPacket, "Één Verpakking aantal meer");
            this.xaddPacket.UseSelectable = true;
            this.xaddPacket.Click += new System.EventHandler(this.xaddPacket_Click);
            // 
            // xpacketlabel
            // 
            this.xpacketlabel.AutoSize = true;
            this.xpacketlabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xpacketlabel.Location = new System.Drawing.Point(192, 23);
            this.xpacketlabel.Name = "xpacketlabel";
            this.xpacketlabel.Size = new System.Drawing.Size(56, 21);
            this.xpacketlabel.TabIndex = 13;
            this.xpacketlabel.Text = "Aantal";
            // 
            // xremovePacket
            // 
            this.xremovePacket.BackgroundImage = global::ProductieManager.Properties.Resources.minusflat_105990;
            this.xremovePacket.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.xremovePacket.Location = new System.Drawing.Point(102, 21);
            this.xremovePacket.Name = "xremovePacket";
            this.xremovePacket.Size = new System.Drawing.Size(34, 29);
            this.xremovePacket.TabIndex = 12;
            this.toolTip1.SetToolTip(this.xremovePacket, "Één Verpakking aantal minder");
            this.xremovePacket.UseSelectable = true;
            this.xremovePacket.Click += new System.EventHandler(this.xremovePacket_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(254, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(17, 21);
            this.label4.TabIndex = 11;
            this.label4.Text = "/";
            // 
            // xtotalpacketlabel
            // 
            this.xtotalpacketlabel.AutoSize = true;
            this.xtotalpacketlabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtotalpacketlabel.Location = new System.Drawing.Point(289, 23);
            this.xtotalpacketlabel.Name = "xtotalpacketlabel";
            this.xtotalpacketlabel.Size = new System.Drawing.Size(56, 21);
            this.xtotalpacketlabel.TabIndex = 10;
            this.xtotalpacketlabel.Text = "Aantal";
            // 
            // xpacketvalue
            // 
            this.xpacketvalue.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xpacketvalue.Location = new System.Drawing.Point(6, 21);
            this.xpacketvalue.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xpacketvalue.Name = "xpacketvalue";
            this.xpacketvalue.Size = new System.Drawing.Size(94, 29);
            this.xpacketvalue.TabIndex = 9;
            // 
            // xaantalgemaaktlabel
            // 
            this.xaantalgemaaktlabel.AutoSize = true;
            this.xaantalgemaaktlabel.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantalgemaaktlabel.Location = new System.Drawing.Point(195, 9);
            this.xaantalgemaaktlabel.Name = "xaantalgemaaktlabel";
            this.xaantalgemaaktlabel.Size = new System.Drawing.Size(56, 21);
            this.xaantalgemaaktlabel.TabIndex = 8;
            this.xaantalgemaaktlabel.Text = "Aantal";
            // 
            // xnextb
            // 
            this.xnextb.BackgroundImage = global::ProductieManager.Properties.Resources.arrow_right_15600;
            this.xnextb.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.xnextb.Location = new System.Drawing.Point(125, 6);
            this.xnextb.Name = "xnextb";
            this.xnextb.Size = new System.Drawing.Size(50, 29);
            this.xnextb.TabIndex = 7;
            this.toolTip1.SetToolTip(this.xnextb, "Wijzig Aantal");
            this.xnextb.UseSelectable = true;
            this.xnextb.Click += new System.EventHandler(this.xnextb_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(257, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(17, 21);
            this.label2.TabIndex = 6;
            this.label2.Text = "/";
            // 
            // xaantal
            // 
            this.xaantal.AutoSize = true;
            this.xaantal.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantal.Location = new System.Drawing.Point(292, 9);
            this.xaantal.Name = "xaantal";
            this.xaantal.Size = new System.Drawing.Size(56, 21);
            this.xaantal.TabIndex = 5;
            this.xaantal.Text = "Aantal";
            // 
            // xaantalgemaakt
            // 
            this.xaantalgemaakt.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantalgemaakt.Location = new System.Drawing.Point(3, 6);
            this.xaantalgemaakt.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantalgemaakt.Name = "xaantalgemaakt";
            this.xaantalgemaakt.Size = new System.Drawing.Size(120, 29);
            this.xaantalgemaakt.TabIndex = 4;
            this.xaantalgemaakt.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.Aantal_KeyPress);
            // 
            // panel4
            // 
            this.panel4.AutoScroll = true;
            this.panel4.Controls.Add(this.productieInfoUI1);
            this.panel4.Controls.Add(this.xvaluepanel);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(138, 60);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(692, 412);
            this.panel4.TabIndex = 5;
            // 
            // productieInfoUI1
            // 
            this.productieInfoUI1.BackColor = System.Drawing.Color.White;
            this.productieInfoUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieInfoUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieInfoUI1.Location = new System.Drawing.Point(0, 0);
            this.productieInfoUI1.Margin = new System.Windows.Forms.Padding(4);
            this.productieInfoUI1.Name = "productieInfoUI1";
            this.productieInfoUI1.Size = new System.Drawing.Size(692, 312);
            this.productieInfoUI1.TabIndex = 2;
            // 
            // xvaluepanel
            // 
            this.xvaluepanel.Controls.Add(this.panel3);
            this.xvaluepanel.Controls.Add(this.panel5);
            this.xvaluepanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xvaluepanel.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvaluepanel.Location = new System.Drawing.Point(0, 312);
            this.xvaluepanel.Name = "xvaluepanel";
            this.xvaluepanel.Size = new System.Drawing.Size(692, 100);
            this.xvaluepanel.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackColor = System.Drawing.Color.Transparent;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.Count_tool_34564__1_;
            this.pictureBox1.Location = new System.Drawing.Point(20, 60);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(118, 412);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
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
            this.ClientSize = new System.Drawing.Size(850, 529);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(850, 475);
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
            this.xPacketGroup.ResumeLayout(false);
            this.xPacketGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xpacketvalue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalgemaakt)).EndInit();
            this.panel4.ResumeLayout(false);
            this.xvaluepanel.ResumeLayout(false);
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
        private System.Windows.Forms.Panel xvaluepanel;
        private System.Windows.Forms.ToolTip toolTip1;
        private MetroFramework.Controls.MetroComboBox xwerkplekken;
        private MetroFramework.Controls.MetroButton xnextb;
        private Controls.ProductieInfoUI productieInfoUI1;
        private System.Windows.Forms.GroupBox xPacketGroup;
        private System.Windows.Forms.Label xpacketlabel;
        private MetroFramework.Controls.MetroButton xremovePacket;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label xtotalpacketlabel;
        private System.Windows.Forms.NumericUpDown xpacketvalue;
        private MetroFramework.Controls.MetroButton xaddPacket;
    }
}