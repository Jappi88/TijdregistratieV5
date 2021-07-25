
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
            this.xdagenvalue = new System.Windows.Forms.NumericUpDown();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xupdatetijdb = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.productieListControl1 = new Controls.ProductieListControl();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xdagenvalue)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xstatus
            // 
            this.xstatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xstatus.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatus.Location = new System.Drawing.Point(0, 0);
            this.xstatus.Name = "xstatus";
            this.xstatus.Size = new System.Drawing.Size(936, 64);
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
            this.panel1.Size = new System.Drawing.Size(936, 64);
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
            // xdagenvalue
            // 
            this.xdagenvalue.DecimalPlaces = 2;
            this.xdagenvalue.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdagenvalue.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.xdagenvalue.Location = new System.Drawing.Point(145, 8);
            this.xdagenvalue.Maximum = new decimal(new int[] {
            999999,
            0,
            0,
            0});
            this.xdagenvalue.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.xdagenvalue.Name = "xdagenvalue";
            this.xdagenvalue.Size = new System.Drawing.Size(85, 25);
            this.xdagenvalue.TabIndex = 2;
            this.toolTip1.SetToolTip(this.xdagenvalue, "Aantal dagen waarvanaf je wilt kijken");
            this.xdagenvalue.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xupdatetijdb);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.xdagenvalue);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Location = new System.Drawing.Point(402, 115);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(330, 40);
            this.panel2.TabIndex = 3;
            this.toolTip1.SetToolTip(this.panel2, "Vul in de aantal dagen waarvanaf je wilt kijken");
            // 
            // xupdatetijdb
            // 
            this.xupdatetijdb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xupdatetijdb.Image = global::ProductieManager.Properties.Resources.Time_machine__40675;
            this.xupdatetijdb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xupdatetijdb.Location = new System.Drawing.Point(233, 3);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 17);
            this.label1.TabIndex = 3;
            this.label1.Text = "Aantal Dagen:";
            // 
            // productieListControl1
            // 
            this.productieListControl1.BackColor = System.Drawing.Color.White;
            this.productieListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieListControl1.EnableEntryFiltering = false;
            this.productieListControl1.EnableFiltering = false;
            this.productieListControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieListControl1.IsBewerkingView = true;
            this.productieListControl1.Location = new System.Drawing.Point(0, 64);
            this.productieListControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productieListControl1.Name = "productieListControl1";
            this.productieListControl1.RemoveCustomItemIfNotValid = false;
            this.productieListControl1.SelectedItem = null;
            this.productieListControl1.Size = new System.Drawing.Size(936, 513);
            this.productieListControl1.TabIndex = 1;
            this.productieListControl1.ValidHandler = null;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
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
            this.Size = new System.Drawing.Size(936, 577);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xdagenvalue)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label xstatus;
        private Controls.ProductieListControl productieListControl1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.NumericUpDown xdagenvalue;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xupdatetijdb;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
