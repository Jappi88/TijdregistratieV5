
namespace Controls
{
    partial class RecentGereedMeldingenUI
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
            this.components = new System.ComponentModel.Container();
            this.xstatus = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xupdatetijdb = new System.Windows.Forms.Button();
            this.xtotgereed = new System.Windows.Forms.DateTimePicker();
            this.xvanafgereed = new System.Windows.Forms.DateTimePicker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.productieListControl1 = new Controls.ProductieListControl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xstatus
            // 
            this.xstatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xstatus.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatus.Location = new System.Drawing.Point(0, 0);
            this.xstatus.Name = "xstatus";
            this.xstatus.Size = new System.Drawing.Size(1098, 64);
            this.xstatus.TabIndex = 0;
            this.xstatus.Text = "Recente Gereedmeldingen";
            this.xstatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xstatus);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1098, 64);
            this.panel1.TabIndex = 2;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.business_color_progress_icon_icons_com_53437;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(47, 40);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xupdatetijdb);
            this.panel2.Controls.Add(this.xtotgereed);
            this.panel2.Controls.Add(this.xvanafgereed);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(402, 115);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(615, 40);
            this.panel2.TabIndex = 3;
            this.toolTip1.SetToolTip(this.panel2, "Vul in de aantal dagen waarvanaf je wilt kijken");
            // 
            // xupdatetijdb
            // 
            this.xupdatetijdb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xupdatetijdb.Image = global::ProductieManager.Properties.Resources.Time_machine__40675;
            this.xupdatetijdb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xupdatetijdb.Location = new System.Drawing.Point(512, 3);
            this.xupdatetijdb.Margin = new System.Windows.Forms.Padding(0);
            this.xupdatetijdb.Name = "xupdatetijdb";
            this.xupdatetijdb.Size = new System.Drawing.Size(92, 34);
            this.xupdatetijdb.TabIndex = 5;
            this.xupdatetijdb.Text = "Update";
            this.xupdatetijdb.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xupdatetijdb, "Update volgens de ingevulde dagen");
            this.xupdatetijdb.UseVisualStyleBackColor = true;
            this.xupdatetijdb.Click += new System.EventHandler(this.xupdatetijdb_Click);
            // 
            // xtotgereed
            // 
            this.xtotgereed.CustomFormat = "\'t/m\' ddd dd MMM yyyy HH:mm";
            this.xtotgereed.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xtotgereed.Location = new System.Drawing.Point(284, 9);
            this.xtotgereed.Name = "xtotgereed";
            this.xtotgereed.ShowCheckBox = true;
            this.xtotgereed.Size = new System.Drawing.Size(225, 25);
            this.xtotgereed.TabIndex = 7;
            // 
            // xvanafgereed
            // 
            this.xvanafgereed.CustomFormat = "\'Vanaf\' ddd dd MMM yyyy HH:mm";
            this.xvanafgereed.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xvanafgereed.Location = new System.Drawing.Point(53, 9);
            this.xvanafgereed.Name = "xvanafgereed";
            this.xvanafgereed.ShowCheckBox = true;
            this.xvanafgereed.Size = new System.Drawing.Size(225, 25);
            this.xvanafgereed.TabIndex = 6;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // productieListControl1
            // 
            this.productieListControl1.BackColor = System.Drawing.Color.White;
            this.productieListControl1.CanLoad = true;
            this.productieListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieListControl1.EnableEntryFiltering = false;
            this.productieListControl1.EnableFiltering = false;
            this.productieListControl1.EnableSync = false;
            this.productieListControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieListControl1.IsBewerkingView = true;
            this.productieListControl1.ListName = "GereedProducties";
            this.productieListControl1.Location = new System.Drawing.Point(0, 64);
            this.productieListControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productieListControl1.Name = "productieListControl1";
            this.productieListControl1.RemoveCustomItemIfNotValid = false;
            this.productieListControl1.SelectedItem = null;
            this.productieListControl1.Size = new System.Drawing.Size(1098, 564);
            this.productieListControl1.TabIndex = 1;
            this.productieListControl1.ValidHandler = null;
            // 
            // RecentGereedMeldingenUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.productieListControl1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "RecentGereedMeldingenUI";
            this.Size = new System.Drawing.Size(1098, 628);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label xstatus;
        private Controls.ProductieListControl productieListControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xupdatetijdb;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.DateTimePicker xtotgereed;
        private System.Windows.Forms.DateTimePicker xvanafgereed;
    }
}
