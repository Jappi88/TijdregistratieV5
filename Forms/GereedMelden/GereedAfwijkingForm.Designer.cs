
namespace Forms.GereedMelden
{
    partial class GereedAfwijkingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GereedAfwijkingForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xredenen = new System.Windows.Forms.ListView();
            this.xcolumn1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xfieldmessage = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(10, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(757, 235);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xredenen);
            this.panel3.Controls.Add(this.xfieldmessage);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(132, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(625, 235);
            this.panel3.TabIndex = 4;
            // 
            // xredenen
            // 
            this.xredenen.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.xredenen.CheckBoxes = true;
            this.xredenen.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.xcolumn1});
            this.xredenen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xredenen.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xredenen.FullRowSelect = true;
            this.xredenen.HideSelection = false;
            this.xredenen.LargeImageList = this.imageList1;
            this.xredenen.Location = new System.Drawing.Point(0, 87);
            this.xredenen.Name = "xredenen";
            this.xredenen.ShowGroups = false;
            this.xredenen.ShowItemToolTips = true;
            this.xredenen.Size = new System.Drawing.Size(625, 148);
            this.xredenen.SmallImageList = this.imageList1;
            this.xredenen.TabIndex = 1;
            this.xredenen.UseCompatibleStateImageBehavior = false;
            this.xredenen.View = System.Windows.Forms.View.Details;
            // 
            // xcolumn1
            // 
            this.xcolumn1.Text = "Hoge Afwijking Redenen";
            this.xcolumn1.Width = 600;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "3605318-bolt-bolts-construction-rivet-screw-screws_107870.png");
            // 
            // xfieldmessage
            // 
            this.xfieldmessage.AutoSize = false;
            this.xfieldmessage.BackColor = System.Drawing.SystemColors.Window;
            this.xfieldmessage.BaseStylesheet = null;
            this.xfieldmessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.xfieldmessage.Location = new System.Drawing.Point(0, 0);
            this.xfieldmessage.Name = "xfieldmessage";
            this.xfieldmessage.Size = new System.Drawing.Size(625, 87);
            this.xfieldmessage.TabIndex = 2;
            this.xfieldmessage.Text = "htmlLabel1";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.Private_80_icon_icons_com_57286;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 235);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(10, 295);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(757, 45);
            this.panel4.TabIndex = 1;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.button2.Location = new System.Drawing.Point(495, 2);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 38);
            this.button2.TabIndex = 13;
            this.button2.Text = "&OK";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.button1.Location = new System.Drawing.Point(629, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 38);
            this.button1.TabIndex = 12;
            this.button1.Text = "&Annuleren";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // GereedAfwijkingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 350);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(777, 600);
            this.MinimumSize = new System.Drawing.Size(777, 350);
            this.Name = "GereedAfwijkingForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Productie Afsluiten";
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView xredenen;
        private System.Windows.Forms.ColumnHeader xcolumn1;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel xfieldmessage;
    }
}