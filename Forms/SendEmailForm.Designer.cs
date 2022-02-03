
namespace Forms
{
    partial class SendEmailForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SendEmailForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.xafzender = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.xonderwerp = new System.Windows.Forms.TextBox();
            this.xontvangerstrip = new System.Windows.Forms.MenuStrip();
            this.xontvangermenuitem = new System.Windows.Forms.ToolStripMenuItem();
            this.xnieuweontvanger = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xbijlagestrip = new System.Windows.Forms.MenuStrip();
            this.xaddbijlage = new System.Windows.Forms.ToolStripMenuItem();
            this.xmessagebox = new System.Windows.Forms.RichTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xverzenden = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.xontvangerstrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.xbijlagestrip.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.xafzender);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.xonderwerp);
            this.panel1.Controls.Add(this.xontvangerstrip);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 15, 15, 0);
            this.panel1.Size = new System.Drawing.Size(600, 165);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(136, 114);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(60, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Afzender";
            // 
            // xafzender
            // 
            this.xafzender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xafzender.Location = new System.Drawing.Point(139, 134);
            this.xafzender.Name = "xafzender";
            this.xafzender.Size = new System.Drawing.Size(457, 25);
            this.xafzender.TabIndex = 1;
            this.toolTip1.SetToolTip(this.xafzender, "Vul je naam in");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(136, 66);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Onderwerp";
            // 
            // xonderwerp
            // 
            this.xonderwerp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xonderwerp.Location = new System.Drawing.Point(139, 86);
            this.xonderwerp.Name = "xonderwerp";
            this.xonderwerp.Size = new System.Drawing.Size(457, 25);
            this.xonderwerp.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xonderwerp, "Vul in een onderwerp");
            // 
            // xontvangerstrip
            // 
            this.xontvangerstrip.AllowMerge = false;
            this.xontvangerstrip.BackColor = System.Drawing.Color.Transparent;
            this.xontvangerstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xontvangermenuitem});
            this.xontvangerstrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.xontvangerstrip.Location = new System.Drawing.Point(130, 15);
            this.xontvangerstrip.Name = "xontvangerstrip";
            this.xontvangerstrip.ShowItemToolTips = true;
            this.xontvangerstrip.Size = new System.Drawing.Size(455, 24);
            this.xontvangerstrip.TabIndex = 8;
            this.xontvangerstrip.Text = "menuStrip1";
            // 
            // xontvangermenuitem
            // 
            this.xontvangermenuitem.AutoToolTip = true;
            this.xontvangermenuitem.DoubleClickEnabled = true;
            this.xontvangermenuitem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xnieuweontvanger});
            this.xontvangermenuitem.Image = global::ProductieManager.Properties.Resources.users_12820;
            this.xontvangermenuitem.Name = "xontvangermenuitem";
            this.xontvangermenuitem.Size = new System.Drawing.Size(104, 20);
            this.xontvangermenuitem.Text = "Ontvanger(s)";
            // 
            // xnieuweontvanger
            // 
            this.xnieuweontvanger.Image = global::ProductieManager.Properties.Resources.user_add_12818;
            this.xnieuweontvanger.Name = "xnieuweontvanger";
            this.xnieuweontvanger.Size = new System.Drawing.Size(173, 22);
            this.xnieuweontvanger.Text = "Nieuwe Ontvanger";
            this.xnieuweontvanger.ToolTipText = "Voeg een nieuwe ontvanger toe";
            this.xnieuweontvanger.Click += new System.EventHandler(this.xnieuweontvanger_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.email_18961_128x128;
            this.pictureBox1.Location = new System.Drawing.Point(0, 15);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(130, 150);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Verzend Email";
            // 
            // xbijlagestrip
            // 
            this.xbijlagestrip.AllowMerge = false;
            this.xbijlagestrip.BackColor = System.Drawing.Color.Transparent;
            this.xbijlagestrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xaddbijlage});
            this.xbijlagestrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.xbijlagestrip.Location = new System.Drawing.Point(20, 225);
            this.xbijlagestrip.Name = "xbijlagestrip";
            this.xbijlagestrip.ShowItemToolTips = true;
            this.xbijlagestrip.Size = new System.Drawing.Size(600, 24);
            this.xbijlagestrip.TabIndex = 7;
            this.xbijlagestrip.Text = "menuStrip1";
            // 
            // xaddbijlage
            // 
            this.xaddbijlage.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xaddbijlage.Name = "xaddbijlage";
            this.xaddbijlage.Size = new System.Drawing.Size(113, 20);
            this.xaddbijlage.Text = "Nieuwe Bijlage";
            this.xaddbijlage.Click += new System.EventHandler(this.xaddbijlage_Click);
            // 
            // xmessagebox
            // 
            this.xmessagebox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmessagebox.Location = new System.Drawing.Point(20, 249);
            this.xmessagebox.Name = "xmessagebox";
            this.xmessagebox.Size = new System.Drawing.Size(600, 137);
            this.xmessagebox.TabIndex = 2;
            this.xmessagebox.Text = "";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 386);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(600, 44);
            this.panel2.TabIndex = 9;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xsluiten);
            this.panel3.Controls.Add(this.xverzenden);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(307, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(293, 44);
            this.panel3.TabIndex = 1;
            // 
            // xsluiten
            // 
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.ForeColor = System.Drawing.Color.Black;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(149, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(140, 38);
            this.xsluiten.TabIndex = 4;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xverzenden
            // 
            this.xverzenden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xverzenden.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xverzenden.ForeColor = System.Drawing.Color.Black;
            this.xverzenden.Image = global::ProductieManager.Properties.Resources.email_send_32x32;
            this.xverzenden.Location = new System.Drawing.Point(3, 3);
            this.xverzenden.Name = "xverzenden";
            this.xverzenden.Size = new System.Drawing.Size(140, 38);
            this.xverzenden.TabIndex = 3;
            this.xverzenden.Text = "Verzenden";
            this.xverzenden.TextImageRelation = System.Windows.Forms.TextImageRelation.TextBeforeImage;
            this.xverzenden.UseVisualStyleBackColor = true;
            this.xverzenden.Click += new System.EventHandler(this.xverzenden_Click);
            // 
            // SendEmailForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 450);
            this.Controls.Add(this.xmessagebox);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.xbijlagestrip);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.xbijlagestrip;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(640, 450);
            this.Name = "SendEmailForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Nieuw Email";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.xontvangerstrip.ResumeLayout(false);
            this.xontvangerstrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.xbijlagestrip.ResumeLayout(false);
            this.xbijlagestrip.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox xonderwerp;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.MenuStrip xbijlagestrip;
        private System.Windows.Forms.ToolStripMenuItem xaddbijlage;
        private System.Windows.Forms.RichTextBox xmessagebox;
        private System.Windows.Forms.MenuStrip xontvangerstrip;
        private System.Windows.Forms.ToolStripMenuItem xontvangermenuitem;
        private System.Windows.Forms.ToolStripMenuItem xnieuweontvanger;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xverzenden;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xafzender;
    }
}