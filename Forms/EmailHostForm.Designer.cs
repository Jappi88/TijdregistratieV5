
namespace Forms
{
    partial class EmailHostForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.xsave = new System.Windows.Forms.Button();
            this.xcancel = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.xusessl = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.xpassimage = new System.Windows.Forms.PictureBox();
            this.xpasstextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.xemailimage = new System.Windows.Forms.PictureBox();
            this.xemailtextbox = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpassimage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xemailimage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xsave);
            this.panel1.Controls.Add(this.xcancel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 207);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(460, 48);
            this.panel1.TabIndex = 0;
            // 
            // xsave
            // 
            this.xsave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xsave.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsave.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsave.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xsave.Location = new System.Drawing.Point(200, 6);
            this.xsave.Name = "xsave";
            this.xsave.Size = new System.Drawing.Size(125, 38);
            this.xsave.TabIndex = 1;
            this.xsave.Text = "Opslaan";
            this.xsave.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xsave, "Sla wijziging op");
            this.xsave.UseVisualStyleBackColor = true;
            this.xsave.Click += new System.EventHandler(this.xsave_Click);
            // 
            // xcancel
            // 
            this.xcancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xcancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xcancel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xcancel.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xcancel.Location = new System.Drawing.Point(331, 6);
            this.xcancel.Name = "xcancel";
            this.xcancel.Size = new System.Drawing.Size(125, 38);
            this.xcancel.TabIndex = 0;
            this.xcancel.Text = "Annuleren";
            this.xcancel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xcancel, "Annuleer wijzigingen");
            this.xcancel.UseVisualStyleBackColor = true;
            this.xcancel.Click += new System.EventHandler(this.xcancel_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.pictureBox2);
            this.panel2.Controls.Add(this.xusessl);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.xpassimage);
            this.panel2.Controls.Add(this.xpasstextbox);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.xemailimage);
            this.panel2.Controls.Add(this.xemailtextbox);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(20, 60);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(460, 147);
            this.panel2.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ProductieManager.Properties.Resources._3844443_disable_eye_inactive_see_show_view_watch_110296;
            this.pictureBox2.Location = new System.Drawing.Point(423, 80);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(32, 32);
            this.pictureBox2.TabIndex = 8;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Tag = "0";
            this.toolTip1.SetToolTip(this.pictureBox2, "Toon/verberg wachtwoord");
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox_Click);
            this.pictureBox2.MouseEnter += new System.EventHandler(this.pictureBox_MouseEnter);
            this.pictureBox2.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
            // 
            // xusessl
            // 
            this.xusessl.AutoSize = true;
            this.xusessl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xusessl.Location = new System.Drawing.Point(175, 118);
            this.xusessl.Name = "xusessl";
            this.xusessl.Size = new System.Drawing.Size(100, 21);
            this.xusessl.TabIndex = 7;
            this.xusessl.Text = "Gebruik SSL";
            this.toolTip1.SetToolTip(this.xusessl, "Gebruik SSL beveiliging ");
            this.xusessl.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(175, 68);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Wachtwoord:";
            // 
            // xpassimage
            // 
            this.xpassimage.Image = global::ProductieManager.Properties.Resources.internet_lock_locked_padlock_password_secure_security_icon_127100__1_;
            this.xpassimage.Location = new System.Drawing.Point(137, 80);
            this.xpassimage.Name = "xpassimage";
            this.xpassimage.Size = new System.Drawing.Size(32, 32);
            this.xpassimage.TabIndex = 5;
            this.xpassimage.TabStop = false;
            // 
            // xpasstextbox
            // 
            this.xpasstextbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xpasstextbox.Location = new System.Drawing.Point(175, 87);
            this.xpasstextbox.Name = "xpasstextbox";
            this.xpasstextbox.PasswordChar = '*';
            this.xpasstextbox.Size = new System.Drawing.Size(242, 25);
            this.xpasstextbox.TabIndex = 4;
            this.xpasstextbox.Tag = "1";
            this.toolTip1.SetToolTip(this.xpasstextbox, "Voor in een wachtwoord");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(175, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(85, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Email Adres:";
            // 
            // xemailimage
            // 
            this.xemailimage.Image = global::ProductieManager.Properties.Resources.Email_30017;
            this.xemailimage.Location = new System.Drawing.Point(137, 33);
            this.xemailimage.Name = "xemailimage";
            this.xemailimage.Size = new System.Drawing.Size(32, 32);
            this.xemailimage.TabIndex = 2;
            this.xemailimage.TabStop = false;
            // 
            // xemailtextbox
            // 
            this.xemailtextbox.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xemailtextbox.Location = new System.Drawing.Point(175, 40);
            this.xemailtextbox.Name = "xemailtextbox";
            this.xemailtextbox.Size = new System.Drawing.Size(242, 25);
            this.xemailtextbox.TabIndex = 1;
            this.xemailtextbox.Text = "Vul in een geldige email adres...";
            this.toolTip1.SetToolTip(this.xemailtextbox, "Vul in een geldige email adres");
            this.xemailtextbox.TextChanged += new System.EventHandler(this.xemailtextbox_TextChanged);
            this.xemailtextbox.Enter += new System.EventHandler(this.xemailtextbox_Enter);
            this.xemailtextbox.Leave += new System.EventHandler(this.xemailtextbox_Leave);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.email_send256_25228;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 144);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Email Host";
            // 
            // EmailHostForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(500, 275);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(500, 275);
            this.MinimumSize = new System.Drawing.Size(500, 275);
            this.Name = "EmailHostForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Email Host Gegevens";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xpassimage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xemailimage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xcancel;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xsave;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.CheckBox xusessl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox xpassimage;
        private System.Windows.Forms.TextBox xpasstextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox xemailimage;
        private System.Windows.Forms.TextBox xemailtextbox;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}