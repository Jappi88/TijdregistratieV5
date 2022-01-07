
namespace Forms
{
    partial class XMessageBox
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XMessageBox));
            this.xmainpanel = new System.Windows.Forms.Panel();
            this.xmessage = new System.Windows.Forms.Label();
            this.xchooserpanel = new System.Windows.Forms.Panel();
            this.xchooser = new MetroFramework.Controls.MetroComboBox();
            this.xbuttonpanel = new System.Windows.Forms.Panel();
            this.xmessageb4 = new MetroFramework.Controls.MetroButton();
            this.xmessageb1 = new MetroFramework.Controls.MetroButton();
            this.xmessageb2 = new MetroFramework.Controls.MetroButton();
            this.xmessageb3 = new MetroFramework.Controls.MetroButton();
            this.xmessageicon = new System.Windows.Forms.PictureBox();
            this.xmainpanel.SuspendLayout();
            this.xchooserpanel.SuspendLayout();
            this.xbuttonpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xmessageicon)).BeginInit();
            this.SuspendLayout();
            // 
            // xmainpanel
            // 
            this.xmainpanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xmainpanel.Controls.Add(this.xmessage);
            this.xmainpanel.Controls.Add(this.xchooserpanel);
            this.xmainpanel.Location = new System.Drawing.Point(164, 65);
            this.xmainpanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xmainpanel.Name = "xmainpanel";
            this.xmainpanel.Size = new System.Drawing.Size(691, 167);
            this.xmainpanel.TabIndex = 0;
            // 
            // xmessage
            // 
            this.xmessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmessage.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmessage.Location = new System.Drawing.Point(0, 0);
            this.xmessage.Name = "xmessage";
            this.xmessage.Size = new System.Drawing.Size(691, 117);
            this.xmessage.TabIndex = 0;
            this.xmessage.Text = "Message Text";
            this.xmessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xchooserpanel
            // 
            this.xchooserpanel.Controls.Add(this.xchooser);
            this.xchooserpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xchooserpanel.Location = new System.Drawing.Point(0, 117);
            this.xchooserpanel.Name = "xchooserpanel";
            this.xchooserpanel.Size = new System.Drawing.Size(691, 50);
            this.xchooserpanel.TabIndex = 1;
            // 
            // xchooser
            // 
            this.xchooser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xchooser.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xchooser.FormattingEnabled = true;
            this.xchooser.ItemHeight = 23;
            this.xchooser.Location = new System.Drawing.Point(0, 0);
            this.xchooser.Name = "xchooser";
            this.xchooser.Size = new System.Drawing.Size(691, 29);
            this.xchooser.TabIndex = 0;
            this.xchooser.UseSelectable = true;
            // 
            // xbuttonpanel
            // 
            this.xbuttonpanel.Controls.Add(this.xmessageb4);
            this.xbuttonpanel.Controls.Add(this.xmessageb1);
            this.xbuttonpanel.Controls.Add(this.xmessageb2);
            this.xbuttonpanel.Controls.Add(this.xmessageb3);
            this.xbuttonpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xbuttonpanel.Location = new System.Drawing.Point(157, 251);
            this.xbuttonpanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xbuttonpanel.Name = "xbuttonpanel";
            this.xbuttonpanel.Size = new System.Drawing.Size(711, 41);
            this.xbuttonpanel.TabIndex = 2;
            // 
            // xmessageb4
            // 
            this.xmessageb4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xmessageb4.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xmessageb4.Location = new System.Drawing.Point(143, 6);
            this.xmessageb4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xmessageb4.Name = "xmessageb4";
            this.xmessageb4.Size = new System.Drawing.Size(135, 30);
            this.xmessageb4.TabIndex = 2;
            this.xmessageb4.Text = "Doe Maar";
            this.xmessageb4.UseSelectable = true;
            this.xmessageb4.Visible = false;
            this.xmessageb4.Click += new System.EventHandler(this.xmessageb4_Click);
            // 
            // xmessageb1
            // 
            this.xmessageb1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xmessageb1.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xmessageb1.Location = new System.Drawing.Point(286, 6);
            this.xmessageb1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xmessageb1.Name = "xmessageb1";
            this.xmessageb1.Size = new System.Drawing.Size(135, 30);
            this.xmessageb1.TabIndex = 1;
            this.xmessageb1.Text = "Doe Maar";
            this.xmessageb1.UseSelectable = true;
            this.xmessageb1.Visible = false;
            this.xmessageb1.Click += new System.EventHandler(this.xmessageb1_Click);
            // 
            // xmessageb2
            // 
            this.xmessageb2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xmessageb2.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xmessageb2.Location = new System.Drawing.Point(429, 6);
            this.xmessageb2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xmessageb2.Name = "xmessageb2";
            this.xmessageb2.Size = new System.Drawing.Size(135, 30);
            this.xmessageb2.TabIndex = 1;
            this.xmessageb2.Text = "No Thnx";
            this.xmessageb2.UseSelectable = true;
            this.xmessageb2.Visible = false;
            this.xmessageb2.Click += new System.EventHandler(this.xmessageb2_Click);
            // 
            // xmessageb3
            // 
            this.xmessageb3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xmessageb3.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xmessageb3.Location = new System.Drawing.Point(572, 6);
            this.xmessageb3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xmessageb3.Name = "xmessageb3";
            this.xmessageb3.Size = new System.Drawing.Size(135, 30);
            this.xmessageb3.TabIndex = 0;
            this.xmessageb3.Text = "Laat Maar";
            this.xmessageb3.UseSelectable = true;
            this.xmessageb3.Click += new System.EventHandler(this.xmessageb3_Click);
            // 
            // xmessageicon
            // 
            this.xmessageicon.Dock = System.Windows.Forms.DockStyle.Left;
            this.xmessageicon.Image = global::ProductieManager.Properties.Resources.exit_close_error_15565;
            this.xmessageicon.Location = new System.Drawing.Point(20, 60);
            this.xmessageicon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xmessageicon.Name = "xmessageicon";
            this.xmessageicon.Size = new System.Drawing.Size(137, 232);
            this.xmessageicon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.xmessageicon.TabIndex = 1;
            this.xmessageicon.TabStop = false;
            // 
            // XMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 312);
            this.Controls.Add(this.xbuttonpanel);
            this.Controls.Add(this.xmessageicon);
            this.Controls.Add(this.xmainpanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 240);
            this.Name = "XMessageBox";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "MessageForm";
            this.xmainpanel.ResumeLayout(false);
            this.xchooserpanel.ResumeLayout(false);
            this.xbuttonpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xmessageicon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel xmainpanel;
        private System.Windows.Forms.PictureBox xmessageicon;
        private System.Windows.Forms.Panel xbuttonpanel;
        private MetroFramework.Controls.MetroButton xmessageb1;
        private MetroFramework.Controls.MetroButton xmessageb2;
        private MetroFramework.Controls.MetroButton xmessageb3;
        private System.Windows.Forms.Label xmessage;
        private System.Windows.Forms.Panel xchooserpanel;
        private MetroFramework.Controls.MetroComboBox xchooser;
        private MetroFramework.Controls.MetroButton xmessageb4;
    }
}