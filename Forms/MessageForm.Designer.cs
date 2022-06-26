
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
            this.xmessageb2 = new System.Windows.Forms.Button();
            this.xmessageb3 = new System.Windows.Forms.Button();
            this.xmessageb1 = new System.Windows.Forms.Button();
            this.xmessageb4 = new System.Windows.Forms.Button();
            this.xmessageicon = new System.Windows.Forms.PictureBox();
            this.xMessagePanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.xmainpanel.SuspendLayout();
            this.xchooserpanel.SuspendLayout();
            this.xbuttonpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xmessageicon)).BeginInit();
            this.xMessagePanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xmainpanel
            // 
            this.xmainpanel.Controls.Add(this.xmessage);
            this.xmainpanel.Controls.Add(this.xchooserpanel);
            this.xmainpanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmainpanel.Location = new System.Drawing.Point(98, 5);
            this.xmainpanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xmainpanel.Name = "xmainpanel";
            this.xmainpanel.Size = new System.Drawing.Size(572, 109);
            this.xmainpanel.TabIndex = 0;
            // 
            // xmessage
            // 
            this.xmessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmessage.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmessage.Location = new System.Drawing.Point(0, 0);
            this.xmessage.Name = "xmessage";
            this.xmessage.Size = new System.Drawing.Size(572, 59);
            this.xmessage.TabIndex = 0;
            this.xmessage.Text = "Message Text";
            this.xmessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xchooserpanel
            // 
            this.xchooserpanel.Controls.Add(this.xchooser);
            this.xchooserpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xchooserpanel.Location = new System.Drawing.Point(0, 59);
            this.xchooserpanel.Name = "xchooserpanel";
            this.xchooserpanel.Size = new System.Drawing.Size(572, 50);
            this.xchooserpanel.TabIndex = 1;
            // 
            // xchooser
            // 
            this.xchooser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xchooser.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xchooser.FontSize = MetroFramework.MetroComboBoxSize.Tall;
            this.xchooser.FormattingEnabled = true;
            this.xchooser.ItemHeight = 29;
            this.xchooser.Location = new System.Drawing.Point(0, 0);
            this.xchooser.Name = "xchooser";
            this.xchooser.Size = new System.Drawing.Size(572, 35);
            this.xchooser.TabIndex = 0;
            this.xchooser.UseSelectable = true;
            this.xchooser.UseStyleColors = true;
            // 
            // xbuttonpanel
            // 
            this.xbuttonpanel.Controls.Add(this.xmessageb2);
            this.xbuttonpanel.Controls.Add(this.xmessageb3);
            this.xbuttonpanel.Controls.Add(this.xmessageb1);
            this.xbuttonpanel.Controls.Add(this.xmessageb4);
            this.xbuttonpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xbuttonpanel.Location = new System.Drawing.Point(0, 119);
            this.xbuttonpanel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xbuttonpanel.Name = "xbuttonpanel";
            this.xbuttonpanel.Size = new System.Drawing.Size(674, 41);
            this.xbuttonpanel.TabIndex = 2;
            // 
            // xmessageb2
            // 
            this.xmessageb2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xmessageb2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xmessageb2.Location = new System.Drawing.Point(394, 3);
            this.xmessageb2.Name = "xmessageb2";
            this.xmessageb2.Size = new System.Drawing.Size(135, 34);
            this.xmessageb2.TabIndex = 1;
            this.xmessageb2.Text = "Button2";
            this.xmessageb2.UseVisualStyleBackColor = true;
            this.xmessageb2.Visible = false;
            this.xmessageb2.Click += new System.EventHandler(this.xmessageb2_Click);
            // 
            // xmessageb3
            // 
            this.xmessageb3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xmessageb3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xmessageb3.Location = new System.Drawing.Point(535, 3);
            this.xmessageb3.Name = "xmessageb3";
            this.xmessageb3.Size = new System.Drawing.Size(135, 34);
            this.xmessageb3.TabIndex = 0;
            this.xmessageb3.Text = "Button3";
            this.xmessageb3.UseVisualStyleBackColor = true;
            this.xmessageb3.Visible = false;
            this.xmessageb3.Click += new System.EventHandler(this.xmessageb3_Click);
            // 
            // xmessageb1
            // 
            this.xmessageb1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xmessageb1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xmessageb1.Location = new System.Drawing.Point(253, 3);
            this.xmessageb1.Name = "xmessageb1";
            this.xmessageb1.Size = new System.Drawing.Size(135, 34);
            this.xmessageb1.TabIndex = 2;
            this.xmessageb1.Text = "Button1";
            this.xmessageb1.UseVisualStyleBackColor = true;
            this.xmessageb1.Visible = false;
            this.xmessageb1.Click += new System.EventHandler(this.xmessageb1_Click);
            // 
            // xmessageb4
            // 
            this.xmessageb4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xmessageb4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xmessageb4.Location = new System.Drawing.Point(112, 3);
            this.xmessageb4.Name = "xmessageb4";
            this.xmessageb4.Size = new System.Drawing.Size(135, 34);
            this.xmessageb4.TabIndex = 3;
            this.xmessageb4.Text = "Button4";
            this.xmessageb4.UseVisualStyleBackColor = true;
            this.xmessageb4.Visible = false;
            this.xmessageb4.Click += new System.EventHandler(this.xmessageb4_Click);
            // 
            // xmessageicon
            // 
            this.xmessageicon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmessageicon.Image = global::ProductieManager.Properties.Resources.exit_close_error_15565;
            this.xmessageicon.Location = new System.Drawing.Point(4, 5);
            this.xmessageicon.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.xmessageicon.Name = "xmessageicon";
            this.xmessageicon.Size = new System.Drawing.Size(86, 109);
            this.xmessageicon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.xmessageicon.TabIndex = 1;
            this.xmessageicon.TabStop = false;
            this.xmessageicon.DoubleClick += new System.EventHandler(this.xmessageicon_DoubleClick);
            // 
            // xMessagePanel
            // 
            this.xMessagePanel.Controls.Add(this.tableLayoutPanel1);
            this.xMessagePanel.Controls.Add(this.xbuttonpanel);
            this.xMessagePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xMessagePanel.Location = new System.Drawing.Point(20, 60);
            this.xMessagePanel.Name = "xMessagePanel";
            this.xMessagePanel.Size = new System.Drawing.Size(674, 160);
            this.xMessagePanel.TabIndex = 3;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 13.94659F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 86.05341F));
            this.tableLayoutPanel1.Controls.Add(this.xmainpanel, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.xmessageicon, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(674, 119);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // XMessageBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(714, 240);
            this.Controls.Add(this.xMessagePanel);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(700, 240);
            this.Name = "XMessageBox";
            this.Text = "MessageForm";
            this.Title = "MessageForm";
            this.xmainpanel.ResumeLayout(false);
            this.xchooserpanel.ResumeLayout(false);
            this.xbuttonpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xmessageicon)).EndInit();
            this.xMessagePanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel xmainpanel;
        private System.Windows.Forms.PictureBox xmessageicon;
        private System.Windows.Forms.Panel xbuttonpanel;
        private System.Windows.Forms.Label xmessage;
        private System.Windows.Forms.Panel xchooserpanel;
        private MetroFramework.Controls.MetroComboBox xchooser;
        private System.Windows.Forms.Panel xMessagePanel;
        private System.Windows.Forms.Button xmessageb4;
        private System.Windows.Forms.Button xmessageb2;
        private System.Windows.Forms.Button xmessageb1;
        private System.Windows.Forms.Button xmessageb3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}