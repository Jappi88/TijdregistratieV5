namespace Forms
{
    partial class UserAccounts
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserAccounts));
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.listView1 = new System.Windows.Forms.ListView();
            this.xDatum = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.panel2 = new System.Windows.Forms.Panel();
            this.xinfo = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xlevelpanel = new System.Windows.Forms.Panel();
            this.xlevelselector = new System.Windows.Forms.ComboBox();
            this.xeditb = new System.Windows.Forms.Button();
            this.xlevellabel = new System.Windows.Forms.Label();
            this.xchangepass = new System.Windows.Forms.Button();
            this.xadduser = new System.Windows.Forms.Button();
            this.xremoveuser = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.groupBox3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel1.SuspendLayout();
            this.xlevelpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.listView1);
            this.groupBox3.Controls.Add(this.panel2);
            this.groupBox3.Controls.Add(this.panel1);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.ForeColor = System.Drawing.Color.Black;
            this.groupBox3.Location = new System.Drawing.Point(20, 60);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(585, 320);
            this.groupBox3.TabIndex = 17;
            this.groupBox3.TabStop = false;
            // 
            // listView1
            // 
            this.listView1.BackColor = System.Drawing.Color.White;
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.xDatum,
            this.columnHeader1,
            this.columnHeader2});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listView1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView1.ForeColor = System.Drawing.Color.Black;
            this.listView1.FullRowSelect = true;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 67);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(579, 212);
            this.listView1.TabIndex = 1;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            this.listView1.SelectedIndexChanged += new System.EventHandler(this.listView1_SelectedIndexChanged);
            // 
            // xDatum
            // 
            this.xDatum.Text = "Gebruikersname";
            this.xDatum.Width = 173;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Level";
            this.columnHeader1.Width = 277;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Ingelogd";
            this.columnHeader2.Width = 77;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xinfo);
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(3, 279);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(579, 38);
            this.panel2.TabIndex = 2;
            // 
            // xinfo
            // 
            this.xinfo.AutoSize = true;
            this.xinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xinfo.Location = new System.Drawing.Point(0, 0);
            this.xinfo.Name = "xinfo";
            this.xinfo.Size = new System.Drawing.Size(146, 21);
            this.xinfo.TabIndex = 2;
            this.xinfo.Text = "Aantal Gebruikers";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.button1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(457, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(122, 38);
            this.panel3.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.button1.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 38);
            this.button1.TabIndex = 0;
            this.button1.Text = "Sluiten";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xlevelpanel);
            this.panel1.Controls.Add(this.xchangepass);
            this.panel1.Controls.Add(this.xadduser);
            this.panel1.Controls.Add(this.xremoveuser);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 25);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(579, 42);
            this.panel1.TabIndex = 0;
            // 
            // xlevelpanel
            // 
            this.xlevelpanel.Controls.Add(this.xlevelselector);
            this.xlevelpanel.Controls.Add(this.xeditb);
            this.xlevelpanel.Controls.Add(this.xlevellabel);
            this.xlevelpanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.xlevelpanel.Location = new System.Drawing.Point(0, 0);
            this.xlevelpanel.Name = "xlevelpanel";
            this.xlevelpanel.Size = new System.Drawing.Size(456, 42);
            this.xlevelpanel.TabIndex = 6;
            this.xlevelpanel.Visible = false;
            // 
            // xlevelselector
            // 
            this.xlevelselector.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlevelselector.FormattingEnabled = true;
            this.xlevelselector.Location = new System.Drawing.Point(151, 8);
            this.xlevelselector.Name = "xlevelselector";
            this.xlevelselector.Size = new System.Drawing.Size(251, 28);
            this.xlevelselector.TabIndex = 3;
            this.xlevelselector.SelectedIndexChanged += new System.EventHandler(this.xlevelselector_SelectedIndexChanged);
            // 
            // xeditb
            // 
            this.xeditb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xeditb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xeditb.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xeditb.Location = new System.Drawing.Point(408, 0);
            this.xeditb.Name = "xeditb";
            this.xeditb.Size = new System.Drawing.Size(41, 42);
            this.xeditb.TabIndex = 5;
            this.toolTip1.SetToolTip(this.xeditb, "Wijzig Wachtwoord");
            this.xeditb.UseVisualStyleBackColor = true;
            this.xeditb.Visible = false;
            this.xeditb.Click += new System.EventHandler(this.xeditb_Click);
            // 
            // xlevellabel
            // 
            this.xlevellabel.AutoSize = true;
            this.xlevellabel.Location = new System.Drawing.Point(9, 10);
            this.xlevellabel.Name = "xlevellabel";
            this.xlevellabel.Size = new System.Drawing.Size(136, 21);
            this.xlevellabel.TabIndex = 4;
            this.xlevellabel.Text = "Gebruikers Level";
            // 
            // xchangepass
            // 
            this.xchangepass.Dock = System.Windows.Forms.DockStyle.Right;
            this.xchangepass.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xchangepass.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xchangepass.Image = global::ProductieManager.Properties.Resources.internet_lock_locked_padlock_password_secure_security_icon_127100__1_;
            this.xchangepass.Location = new System.Drawing.Point(456, 0);
            this.xchangepass.Name = "xchangepass";
            this.xchangepass.Size = new System.Drawing.Size(41, 42);
            this.xchangepass.TabIndex = 2;
            this.toolTip1.SetToolTip(this.xchangepass, "Wijzig Wachtwoord");
            this.xchangepass.UseVisualStyleBackColor = true;
            this.xchangepass.Visible = false;
            this.xchangepass.Click += new System.EventHandler(this.xchangepass_Click);
            // 
            // xadduser
            // 
            this.xadduser.Dock = System.Windows.Forms.DockStyle.Right;
            this.xadduser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xadduser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xadduser.Image = global::ProductieManager.Properties.Resources.user_add_12818;
            this.xadduser.Location = new System.Drawing.Point(497, 0);
            this.xadduser.Name = "xadduser";
            this.xadduser.Size = new System.Drawing.Size(41, 42);
            this.xadduser.TabIndex = 1;
            this.toolTip1.SetToolTip(this.xadduser, "Voeg Niewe Gebruiker");
            this.xadduser.UseVisualStyleBackColor = true;
            this.xadduser.Click += new System.EventHandler(this.xadduser_Click);
            // 
            // xremoveuser
            // 
            this.xremoveuser.Dock = System.Windows.Forms.DockStyle.Right;
            this.xremoveuser.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xremoveuser.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xremoveuser.Image = global::ProductieManager.Properties.Resources.delete_delete_deleteusers_delete_male_user_maleclient_2348;
            this.xremoveuser.Location = new System.Drawing.Point(538, 0);
            this.xremoveuser.Name = "xremoveuser";
            this.xremoveuser.Size = new System.Drawing.Size(41, 42);
            this.xremoveuser.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xremoveuser, "Verwijder geselecteerde gebruikers");
            this.xremoveuser.UseVisualStyleBackColor = true;
            this.xremoveuser.Visible = false;
            this.xremoveuser.Click += new System.EventHandler(this.xremoveuser_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Gebruikers";
            // 
            // UserAccounts
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 21F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 400);
            this.Controls.Add(this.groupBox3);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(625, 400);
            this.Name = "UserAccounts";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gebruikers";
            this.groupBox3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.xlevelpanel.ResumeLayout(false);
            this.xlevelpanel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader xDatum;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xadduser;
        private System.Windows.Forms.Button xremoveuser;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label xinfo;
        private System.Windows.Forms.Button xchangepass;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label xlevellabel;
        private System.Windows.Forms.ComboBox xlevelselector;
        private System.Windows.Forms.Button xeditb;
        private System.Windows.Forms.Panel xlevelpanel;
    }
}