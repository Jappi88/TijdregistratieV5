namespace Forms.Klachten
{
    partial class NewKlachtForm
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xplaatsen = new System.Windows.Forms.Button();
            this.xbijlagestrip = new System.Windows.Forms.MenuStrip();
            this.xaddbijlage = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xDatePanel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.xdateklacht = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.xafzender = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.xonderwerp = new System.Windows.Forms.TextBox();
            this.xontvangerstrip = new System.Windows.Forms.MenuStrip();
            this.xontvangermenuitem = new System.Windows.Forms.ToolStripMenuItem();
            this.xnieuweontvanger = new System.Windows.Forms.ToolStripMenuItem();
            this.ximagebox = new System.Windows.Forms.PictureBox();
            this.xproductiesstrip = new System.Windows.Forms.MenuStrip();
            this.xaddproductie = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xmessagebox = new System.Windows.Forms.TextBox();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.xbijlagestrip.SuspendLayout();
            this.panel1.SuspendLayout();
            this.xDatePanel.SuspendLayout();
            this.xontvangerstrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ximagebox)).BeginInit();
            this.xproductiesstrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.White;
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 431);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(676, 44);
            this.panel2.TabIndex = 13;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xsluiten);
            this.panel3.Controls.Add(this.xplaatsen);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(312, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(364, 44);
            this.panel3.TabIndex = 1;
            // 
            // xsluiten
            // 
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.ForeColor = System.Drawing.Color.Black;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(219, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(140, 38);
            this.xsluiten.TabIndex = 4;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xplaatsen
            // 
            this.xplaatsen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xplaatsen.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xplaatsen.ForeColor = System.Drawing.Color.Black;
            this.xplaatsen.Image = global::ProductieManager.Properties.Resources.Leave_80_icon_icons_com_57305;
            this.xplaatsen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xplaatsen.Location = new System.Drawing.Point(3, 3);
            this.xplaatsen.Name = "xplaatsen";
            this.xplaatsen.Size = new System.Drawing.Size(210, 38);
            this.xplaatsen.TabIndex = 3;
            this.xplaatsen.Text = "Klacht Plaatsen";
            this.xplaatsen.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xplaatsen.UseVisualStyleBackColor = true;
            this.xplaatsen.Click += new System.EventHandler(this.xplaatsen_Click);
            // 
            // xbijlagestrip
            // 
            this.xbijlagestrip.AllowMerge = false;
            this.xbijlagestrip.BackColor = System.Drawing.Color.Transparent;
            this.xbijlagestrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xaddbijlage});
            this.xbijlagestrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.xbijlagestrip.Location = new System.Drawing.Point(20, 169);
            this.xbijlagestrip.Name = "xbijlagestrip";
            this.xbijlagestrip.ShowItemToolTips = true;
            this.xbijlagestrip.Size = new System.Drawing.Size(676, 24);
            this.xbijlagestrip.TabIndex = 12;
            this.xbijlagestrip.Text = "menuStrip1";
            // 
            // xaddbijlage
            // 
            this.xaddbijlage.Image = global::ProductieManager.Properties.Resources.attachment_32x32;
            this.xaddbijlage.Name = "xaddbijlage";
            this.xaddbijlage.Size = new System.Drawing.Size(70, 20);
            this.xaddbijlage.Text = "Bijlage";
            this.xaddbijlage.ToolTipText = "Voeg nieuwe bijlage toe";
            this.xaddbijlage.Click += new System.EventHandler(this.xaddbijlage_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xDatePanel);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.xafzender);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.xonderwerp);
            this.panel1.Controls.Add(this.xontvangerstrip);
            this.panel1.Controls.Add(this.ximagebox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(0, 15, 15, 0);
            this.panel1.Size = new System.Drawing.Size(676, 109);
            this.panel1.TabIndex = 10;
            // 
            // xDatePanel
            // 
            this.xDatePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xDatePanel.Controls.Add(this.label3);
            this.xDatePanel.Controls.Add(this.xdateklacht);
            this.xDatePanel.Location = new System.Drawing.Point(391, 80);
            this.xDatePanel.Name = "xDatePanel";
            this.xDatePanel.Size = new System.Drawing.Size(282, 25);
            this.xDatePanel.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(30, 17);
            this.label3.TabIndex = 11;
            this.label3.Text = "Op:";
            // 
            // xdateklacht
            // 
            this.xdateklacht.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xdateklacht.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdateklacht.Location = new System.Drawing.Point(39, 0);
            this.xdateklacht.Name = "xdateklacht";
            this.xdateklacht.Size = new System.Drawing.Size(239, 25);
            this.xdateklacht.TabIndex = 10;
            this.toolTip1.SetToolTip(this.xdateklacht, "Vul in de datum waarvan de klacht is ingediend");
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(102, 86);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 17);
            this.label1.TabIndex = 9;
            this.label1.Text = "Afzender:";
            // 
            // xafzender
            // 
            this.xafzender.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xafzender.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xafzender.Location = new System.Drawing.Point(176, 80);
            this.xafzender.Name = "xafzender";
            this.xafzender.Size = new System.Drawing.Size(209, 25);
            this.xafzender.TabIndex = 1;
            this.toolTip1.SetToolTip(this.xafzender, "Vul in van wie deze klacht afkomstig is");
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(90, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 17);
            this.label2.TabIndex = 4;
            this.label2.Text = "Onderwerp:";
            // 
            // xonderwerp
            // 
            this.xonderwerp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xonderwerp.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xonderwerp.Location = new System.Drawing.Point(176, 49);
            this.xonderwerp.Name = "xonderwerp";
            this.xonderwerp.Size = new System.Drawing.Size(496, 25);
            this.xonderwerp.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xonderwerp, "Vul in de onderwerp van deze klacht");
            // 
            // xontvangerstrip
            // 
            this.xontvangerstrip.AllowMerge = false;
            this.xontvangerstrip.BackColor = System.Drawing.Color.Transparent;
            this.xontvangerstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xontvangermenuitem});
            this.xontvangerstrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.xontvangerstrip.Location = new System.Drawing.Point(90, 15);
            this.xontvangerstrip.Name = "xontvangerstrip";
            this.xontvangerstrip.ShowItemToolTips = true;
            this.xontvangerstrip.Size = new System.Drawing.Size(571, 24);
            this.xontvangerstrip.TabIndex = 8;
            this.xontvangerstrip.Text = "menuStrip1";
            // 
            // xontvangermenuitem
            // 
            this.xontvangermenuitem.AutoToolTip = true;
            this.xontvangermenuitem.DoubleClickEnabled = true;
            this.xontvangermenuitem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xnieuweontvanger});
            this.xontvangermenuitem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xontvangermenuitem.Image = global::ProductieManager.Properties.Resources.users_12820;
            this.xontvangermenuitem.Name = "xontvangermenuitem";
            this.xontvangermenuitem.Size = new System.Drawing.Size(134, 20);
            this.xontvangermenuitem.Text = "Kies Ontvanger(s)";
            this.xontvangermenuitem.Click += new System.EventHandler(this.EmailClient_Click);
            // 
            // xnieuweontvanger
            // 
            this.xnieuweontvanger.Image = global::ProductieManager.Properties.Resources.users_12820;
            this.xnieuweontvanger.Name = "xnieuweontvanger";
            this.xnieuweontvanger.Size = new System.Drawing.Size(125, 22);
            this.xnieuweontvanger.Tag = "";
            this.xnieuweontvanger.Text = "Iedereen";
            this.xnieuweontvanger.ToolTipText = "Voeg een nieuwe ontvanger toe";
            this.xnieuweontvanger.Click += new System.EventHandler(this.xnieuweontvanger_Click);
            // 
            // ximagebox
            // 
            this.ximagebox.Dock = System.Windows.Forms.DockStyle.Left;
            this.ximagebox.Image = global::ProductieManager.Properties.Resources.file_warning_40447;
            this.ximagebox.Location = new System.Drawing.Point(0, 15);
            this.ximagebox.Name = "ximagebox";
            this.ximagebox.Size = new System.Drawing.Size(90, 94);
            this.ximagebox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ximagebox.TabIndex = 0;
            this.ximagebox.TabStop = false;
            // 
            // xproductiesstrip
            // 
            this.xproductiesstrip.AllowMerge = false;
            this.xproductiesstrip.BackColor = System.Drawing.Color.Transparent;
            this.xproductiesstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xaddproductie});
            this.xproductiesstrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.xproductiesstrip.Location = new System.Drawing.Point(20, 193);
            this.xproductiesstrip.Name = "xproductiesstrip";
            this.xproductiesstrip.ShowItemToolTips = true;
            this.xproductiesstrip.Size = new System.Drawing.Size(676, 24);
            this.xproductiesstrip.TabIndex = 14;
            this.xproductiesstrip.Text = "menuStrip1";
            // 
            // xaddproductie
            // 
            this.xaddproductie.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xaddproductie.Name = "xaddproductie";
            this.xaddproductie.Size = new System.Drawing.Size(86, 20);
            this.xaddproductie.Text = "Productie";
            this.xaddproductie.ToolTipText = "Voeg een gerelateerde productie  toe";
            this.xaddproductie.Click += new System.EventHandler(this.xaddproductie_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // xmessagebox
            // 
            this.xmessagebox.AllowDrop = true;
            this.xmessagebox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xmessagebox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmessagebox.Location = new System.Drawing.Point(23, 223);
            this.xmessagebox.Multiline = true;
            this.xmessagebox.Name = "xmessagebox";
            this.xmessagebox.Size = new System.Drawing.Size(673, 202);
            this.xmessagebox.TabIndex = 15;
            this.xmessagebox.DragDrop += new System.Windows.Forms.DragEventHandler(this.Xmessagebox_DragDrop);
            this.xmessagebox.DragEnter += new System.Windows.Forms.DragEventHandler(this.Xmessagebox_DragEnter);
            // 
            // NewKlachtForm
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(716, 495);
            this.Controls.Add(this.xmessagebox);
            this.Controls.Add(this.xproductiesstrip);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.xbijlagestrip);
            this.Controls.Add(this.panel1);
            this.Name = "NewKlachtForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Nieuwe Klacht";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Xmessagebox_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Xmessagebox_DragEnter);
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.xbijlagestrip.ResumeLayout(false);
            this.xbijlagestrip.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.xDatePanel.ResumeLayout(false);
            this.xDatePanel.PerformLayout();
            this.xontvangerstrip.ResumeLayout(false);
            this.xontvangerstrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ximagebox)).EndInit();
            this.xproductiesstrip.ResumeLayout(false);
            this.xproductiesstrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xplaatsen;
        private System.Windows.Forms.MenuStrip xbijlagestrip;
        private System.Windows.Forms.ToolStripMenuItem xaddbijlage;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xafzender;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox xonderwerp;
        private System.Windows.Forms.MenuStrip xontvangerstrip;
        private System.Windows.Forms.ToolStripMenuItem xontvangermenuitem;
        private System.Windows.Forms.ToolStripMenuItem xnieuweontvanger;
        private System.Windows.Forms.PictureBox ximagebox;
        private System.Windows.Forms.MenuStrip xproductiesstrip;
        private System.Windows.Forms.ToolStripMenuItem xaddproductie;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker xdateklacht;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.TextBox xmessagebox;
        private System.Windows.Forms.Panel xDatePanel;
    }
}