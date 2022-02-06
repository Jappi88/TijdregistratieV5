
namespace Forms
{
    partial class NieuwKlusForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NieuwKlusForm));
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.xstart = new System.Windows.Forms.DateTimePicker();
            this.xgestoptlabel = new System.Windows.Forms.Label();
            this.xstop = new System.Windows.Forms.DateTimePicker();
            this.xbewerkingen = new MetroFramework.Controls.MetroComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.xwerkplekken = new MetroFramework.Controls.MetroComboBox();
            this.xstatus = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.xgewerkt = new System.Windows.Forms.Label();
            this.xgewerktetijden = new System.Windows.Forms.Button();
            this.xrooster = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.iconfinder_technology;
            this.pictureBox1.Location = new System.Drawing.Point(20, 60);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(149, 395);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(176, 261);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Wanneer Begonnen?";
            // 
            // xstart
            // 
            this.xstart.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xstart.CustomFormat = "dddd dd MMMM yyyy \'om\' HH:mm \'uur\'";
            this.xstart.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstart.Location = new System.Drawing.Point(180, 286);
            this.xstart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xstart.Name = "xstart";
            this.xstart.Size = new System.Drawing.Size(468, 27);
            this.xstart.TabIndex = 2;
            this.xstart.ValueChanged += new System.EventHandler(this.xstart_ValueChanged);
            // 
            // xgestoptlabel
            // 
            this.xgestoptlabel.AutoSize = true;
            this.xgestoptlabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xgestoptlabel.Location = new System.Drawing.Point(176, 319);
            this.xgestoptlabel.Name = "xgestoptlabel";
            this.xgestoptlabel.Size = new System.Drawing.Size(119, 17);
            this.xgestoptlabel.TabIndex = 3;
            this.xgestoptlabel.Text = "Wanneer Gestopt?";
            // 
            // xstop
            // 
            this.xstop.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xstop.CustomFormat = "dddd dd MMMM yyyy \'om\' HH:mm \'uur\'";
            this.xstop.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstop.Location = new System.Drawing.Point(180, 343);
            this.xstop.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xstop.Name = "xstop";
            this.xstop.Size = new System.Drawing.Size(468, 27);
            this.xstop.TabIndex = 4;
            this.xstop.ValueChanged += new System.EventHandler(this.xstop_ValueChanged);
            // 
            // xbewerkingen
            // 
            this.xbewerkingen.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xbewerkingen.FormattingEnabled = true;
            this.xbewerkingen.ItemHeight = 23;
            this.xbewerkingen.Location = new System.Drawing.Point(180, 172);
            this.xbewerkingen.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xbewerkingen.Name = "xbewerkingen";
            this.xbewerkingen.Size = new System.Drawing.Size(468, 29);
            this.xbewerkingen.TabIndex = 5;
            this.xbewerkingen.UseSelectable = true;
            this.xbewerkingen.SelectedIndexChanged += new System.EventHandler(this.xbewerkingen_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(176, 149);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(101, 17);
            this.label3.TabIndex = 6;
            this.label3.Text = "Kies Bewerking";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(176, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 17);
            this.label4.TabIndex = 8;
            this.label4.Text = "Kies Werkplek";
            // 
            // xwerkplekken
            // 
            this.xwerkplekken.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xwerkplekken.FormattingEnabled = true;
            this.xwerkplekken.ItemHeight = 23;
            this.xwerkplekken.Location = new System.Drawing.Point(180, 229);
            this.xwerkplekken.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xwerkplekken.Name = "xwerkplekken";
            this.xwerkplekken.Size = new System.Drawing.Size(468, 29);
            this.xwerkplekken.TabIndex = 7;
            this.xwerkplekken.UseSelectable = true;
            this.xwerkplekken.SelectedIndexChanged += new System.EventHandler(this.xwerkplekken_SelectedIndexChanged);
            // 
            // xstatus
            // 
            this.xstatus.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xstatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatus.Location = new System.Drawing.Point(175, 60);
            this.xstatus.Name = "xstatus";
            this.xstatus.Size = new System.Drawing.Size(473, 89);
            this.xstatus.TabIndex = 9;
            this.xstatus.Text = "Kies Bewerking";
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(523, 425);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 38);
            this.button1.TabIndex = 10;
            this.button1.Text = "&Annuleren";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(419, 425);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(98, 38);
            this.button2.TabIndex = 11;
            this.button2.Text = "&OK";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // xgewerkt
            // 
            this.xgewerkt.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgewerkt.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xgewerkt.Location = new System.Drawing.Point(177, 374);
            this.xgewerkt.Name = "xgewerkt";
            this.xgewerkt.Size = new System.Drawing.Size(471, 48);
            this.xgewerkt.TabIndex = 12;
            this.xgewerkt.Text = "Aantal uren gewerkt";
            // 
            // xgewerktetijden
            // 
            this.xgewerktetijden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xgewerktetijden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xgewerktetijden.Image = global::ProductieManager.Properties.Resources.business_color_progress_icon_icons_com_53437;
            this.xgewerktetijden.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xgewerktetijden.Location = new System.Drawing.Point(174, 425);
            this.xgewerktetijden.Name = "xgewerktetijden";
            this.xgewerktetijden.Size = new System.Drawing.Size(131, 38);
            this.xgewerktetijden.TabIndex = 13;
            this.xgewerktetijden.Text = "Werk Tijden";
            this.xgewerktetijden.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xgewerktetijden.UseVisualStyleBackColor = true;
            this.xgewerktetijden.Click += new System.EventHandler(this.xgewerktetijden_Click);
            // 
            // xrooster
            // 
            this.xrooster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xrooster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xrooster.Image = global::ProductieManager.Properties.Resources.schedule_32_32;
            this.xrooster.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xrooster.Location = new System.Drawing.Point(311, 425);
            this.xrooster.Name = "xrooster";
            this.xrooster.Size = new System.Drawing.Size(102, 38);
            this.xrooster.TabIndex = 14;
            this.xrooster.Text = "Rooster";
            this.xrooster.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xrooster.UseVisualStyleBackColor = true;
            this.xrooster.Click += new System.EventHandler(this.xrooster_Click);
            // 
            // NieuwKlusForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(660, 475);
            this.Controls.Add(this.xrooster);
            this.Controls.Add(this.xgewerktetijden);
            this.Controls.Add(this.xgewerkt);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.xstatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.xwerkplekken);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.xbewerkingen);
            this.Controls.Add(this.xstop);
            this.Controls.Add(this.xgestoptlabel);
            this.Controls.Add(this.xstart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(660, 475);
            this.Name = "NieuwKlusForm";
            this.Text = "Klus Gegevens";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker xstart;
        private System.Windows.Forms.Label xgestoptlabel;
        private System.Windows.Forms.DateTimePicker xstop;
        private MetroFramework.Controls.MetroComboBox xbewerkingen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroComboBox xwerkplekken;
        private System.Windows.Forms.Label xstatus;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label xgewerkt;
        private System.Windows.Forms.Button xgewerktetijden;
        private System.Windows.Forms.Button xrooster;
    }
}