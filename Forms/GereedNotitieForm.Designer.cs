
namespace Forms
{
    partial class GereedNotitieForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GereedNotitieForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xfieldmessage = new System.Windows.Forms.Label();
            this.xmateriaalopcheck = new System.Windows.Forms.RadioButton();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xvollepalletcheck = new System.Windows.Forms.RadioButton();
            this.xanderscheck = new System.Windows.Forms.RadioButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xredentextbox = new System.Windows.Forms.TextBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.xmaterialen = new System.Windows.Forms.ListView();
            this.xcolumn1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.xcolmn2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.xfieldmessage);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(10, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(757, 135);
            this.panel1.TabIndex = 0;
            // 
            // xfieldmessage
            // 
            this.xfieldmessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.xfieldmessage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xfieldmessage.Location = new System.Drawing.Point(132, 0);
            this.xfieldmessage.Name = "xfieldmessage";
            this.xfieldmessage.Size = new System.Drawing.Size(625, 103);
            this.xfieldmessage.TabIndex = 1;
            this.xfieldmessage.Text = "Je hebt {0} minder gemaakt dan de gevraagde aantal...\r\nWat is de reden dat je {1}" +
    " eerder wilt afsluiten?";
            this.xfieldmessage.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xmateriaalopcheck
            // 
            this.xmateriaalopcheck.AutoSize = true;
            this.xmateriaalopcheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xmateriaalopcheck.Location = new System.Drawing.Point(6, 2);
            this.xmateriaalopcheck.Name = "xmateriaalopcheck";
            this.xmateriaalopcheck.Size = new System.Drawing.Size(170, 21);
            this.xmateriaalopcheck.TabIndex = 2;
            this.xmateriaalopcheck.Text = "Materiaal op, namelijk:";
            this.xmateriaalopcheck.UseVisualStyleBackColor = true;
            this.xmateriaalopcheck.CheckedChanged += new System.EventHandler(this.xmateriaalopcheck_CheckedChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xanderscheck);
            this.panel2.Controls.Add(this.xvollepalletcheck);
            this.panel2.Controls.Add(this.xmateriaalopcheck);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(132, 103);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(625, 29);
            this.panel2.TabIndex = 3;
            // 
            // xvollepalletcheck
            // 
            this.xvollepalletcheck.AutoSize = true;
            this.xvollepalletcheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvollepalletcheck.Location = new System.Drawing.Point(196, 2);
            this.xvollepalletcheck.Name = "xvollepalletcheck";
            this.xvollepalletcheck.Size = new System.Drawing.Size(210, 21);
            this.xvollepalletcheck.TabIndex = 3;
            this.xvollepalletcheck.Text = "Geëindigd op volle pallet/bak";
            this.xvollepalletcheck.UseVisualStyleBackColor = true;
            this.xvollepalletcheck.CheckedChanged += new System.EventHandler(this.xvollepalletcheck_CheckedChanged);
            // 
            // xanderscheck
            // 
            this.xanderscheck.AutoSize = true;
            this.xanderscheck.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xanderscheck.Location = new System.Drawing.Point(433, 2);
            this.xanderscheck.Name = "xanderscheck";
            this.xanderscheck.Size = new System.Drawing.Size(175, 21);
            this.xanderscheck.TabIndex = 4;
            this.xanderscheck.Text = "Andere reden, namelijk:";
            this.xanderscheck.UseVisualStyleBackColor = true;
            this.xanderscheck.CheckedChanged += new System.EventHandler(this.xanderscheck_CheckedChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xredentextbox);
            this.panel3.Controls.Add(this.xmaterialen);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(132, 132);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(625, 3);
            this.panel3.TabIndex = 4;
            // 
            // xredentextbox
            // 
            this.xredentextbox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xredentextbox.Location = new System.Drawing.Point(0, 0);
            this.xredentextbox.Multiline = true;
            this.xredentextbox.Name = "xredentextbox";
            this.xredentextbox.Size = new System.Drawing.Size(625, 3);
            this.xredentextbox.TabIndex = 0;
            this.xredentextbox.Visible = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(10, 195);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(757, 45);
            this.panel4.TabIndex = 1;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "3605318-bolt-bolts-construction-rivet-screw-screws_107870.png");
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.Private_80_icon_icons1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 135);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
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
            // xmaterialen
            // 
            this.xmaterialen.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.xmaterialen.CheckBoxes = true;
            this.xmaterialen.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.xcolumn1,
            this.xcolmn2});
            this.xmaterialen.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmaterialen.FullRowSelect = true;
            this.xmaterialen.HideSelection = false;
            this.xmaterialen.LargeImageList = this.imageList1;
            this.xmaterialen.Location = new System.Drawing.Point(0, 0);
            this.xmaterialen.Name = "xmaterialen";
            this.xmaterialen.ShowGroups = false;
            this.xmaterialen.ShowItemToolTips = true;
            this.xmaterialen.Size = new System.Drawing.Size(625, 3);
            this.xmaterialen.SmallImageList = this.imageList1;
            this.xmaterialen.TabIndex = 1;
            this.xmaterialen.UseCompatibleStateImageBehavior = false;
            this.xmaterialen.View = System.Windows.Forms.View.Details;
            this.xmaterialen.Visible = false;
            // 
            // xcolumn1
            // 
            this.xcolumn1.Text = "ArtikelNr";
            this.xcolumn1.Width = 202;
            // 
            // xcolmn2
            // 
            this.xcolmn2.Text = "Omschrijving";
            this.xcolmn2.Width = 401;
            // 
            // GereedNotitieForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(777, 250);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(777, 250);
            this.MinimumSize = new System.Drawing.Size(777, 250);
            this.Name = "GereedNotitieForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Productie Afsluiten";
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.TextBox xredentextbox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.RadioButton xanderscheck;
        private System.Windows.Forms.RadioButton xvollepalletcheck;
        private System.Windows.Forms.RadioButton xmateriaalopcheck;
        private System.Windows.Forms.Label xfieldmessage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ListView xmaterialen;
        private System.Windows.Forms.ColumnHeader xcolumn1;
        private System.Windows.Forms.ColumnHeader xcolmn2;
    }
}