
namespace Forms
{
    partial class NiewProductieForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NiewProductieForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xprodnrchecked = new System.Windows.Forms.CheckBox();
            this.xprodnr = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xstarten = new System.Windows.Forms.Button();
            this.xopslaan = new System.Windows.Forms.Button();
            this.xannuleren = new System.Windows.Forms.Button();
            this.xomschrijving = new System.Windows.Forms.TextBox();
            this.xleverdatum = new System.Windows.Forms.DateTimePicker();
            this.label5 = new System.Windows.Forms.Label();
            this.xperuur = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.xaantal = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.xbewerking = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.xartikelnr = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xperuur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantal)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xprodnrchecked);
            this.groupBox1.Controls.Add(this.xprodnr);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.xstarten);
            this.groupBox1.Controls.Add(this.xopslaan);
            this.groupBox1.Controls.Add(this.xannuleren);
            this.groupBox1.Controls.Add(this.xomschrijving);
            this.groupBox1.Controls.Add(this.xleverdatum);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.xperuur);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.xaantal);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.xbewerking);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.xartikelnr);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(20, 60);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 395);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Maak Nieuwe Productie Aan";
            // 
            // xprodnrchecked
            // 
            this.xprodnrchecked.AutoSize = true;
            this.xprodnrchecked.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprodnrchecked.Location = new System.Drawing.Point(284, 25);
            this.xprodnrchecked.Name = "xprodnrchecked";
            this.xprodnrchecked.Size = new System.Drawing.Size(144, 21);
            this.xprodnrchecked.TabIndex = 16;
            this.xprodnrchecked.Text = "Productie Nummer";
            this.xprodnrchecked.UseVisualStyleBackColor = true;
            this.xprodnrchecked.CheckedChanged += new System.EventHandler(this.xprodnrchecked_CheckedChanged);
            // 
            // xprodnr
            // 
            this.xprodnr.Enabled = false;
            this.xprodnr.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprodnr.Location = new System.Drawing.Point(284, 52);
            this.xprodnr.Name = "xprodnr";
            this.xprodnr.Size = new System.Drawing.Size(138, 25);
            this.xprodnr.TabIndex = 15;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Location = new System.Drawing.Point(6, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(125, 193);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 14;
            this.pictureBox1.TabStop = false;
            // 
            // xstarten
            // 
            this.xstarten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xstarten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xstarten.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstarten.Image = global::ProductieManager.Properties.Resources.play_button_icon_icons_com_60615;
            this.xstarten.Location = new System.Drawing.Point(26, 343);
            this.xstarten.Name = "xstarten";
            this.xstarten.Size = new System.Drawing.Size(168, 40);
            this.xstarten.TabIndex = 13;
            this.xstarten.Text = "Opslaan En Starten";
            this.xstarten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xstarten.UseVisualStyleBackColor = true;
            this.xstarten.Click += new System.EventHandler(this.xstarten_Click);
            // 
            // xopslaan
            // 
            this.xopslaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xopslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xopslaan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xopslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xopslaan.Location = new System.Drawing.Point(202, 343);
            this.xopslaan.Name = "xopslaan";
            this.xopslaan.Size = new System.Drawing.Size(115, 40);
            this.xopslaan.TabIndex = 12;
            this.xopslaan.Text = "Opslaan";
            this.xopslaan.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xopslaan.UseVisualStyleBackColor = true;
            this.xopslaan.Click += new System.EventHandler(this.xopslaan_Click);
            // 
            // xannuleren
            // 
            this.xannuleren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xannuleren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xannuleren.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xannuleren.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xannuleren.Location = new System.Drawing.Point(323, 342);
            this.xannuleren.Name = "xannuleren";
            this.xannuleren.Size = new System.Drawing.Size(115, 40);
            this.xannuleren.TabIndex = 11;
            this.xannuleren.Text = "Annuleren";
            this.xannuleren.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xannuleren.UseVisualStyleBackColor = true;
            this.xannuleren.Click += new System.EventHandler(this.xannuleren_Click);
            // 
            // xomschrijving
            // 
            this.xomschrijving.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xomschrijving.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xomschrijving.Location = new System.Drawing.Point(9, 228);
            this.xomschrijving.Multiline = true;
            this.xomschrijving.Name = "xomschrijving";
            this.xomschrijving.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.xomschrijving.Size = new System.Drawing.Size(429, 109);
            this.xomschrijving.TabIndex = 10;
            // 
            // xleverdatum
            // 
            this.xleverdatum.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xleverdatum.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xleverdatum.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xleverdatum.Location = new System.Drawing.Point(140, 196);
            this.xleverdatum.Name = "xleverdatum";
            this.xleverdatum.Size = new System.Drawing.Size(282, 25);
            this.xleverdatum.TabIndex = 9;
            this.xleverdatum.Value = new System.DateTime(2022, 3, 13, 16, 30, 0, 0);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(137, 176);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "Leverdatum";
            // 
            // xperuur
            // 
            this.xperuur.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xperuur.Location = new System.Drawing.Point(292, 148);
            this.xperuur.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xperuur.Name = "xperuur";
            this.xperuur.Size = new System.Drawing.Size(130, 25);
            this.xperuur.TabIndex = 7;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(289, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Per Uur";
            // 
            // xaantal
            // 
            this.xaantal.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xaantal.Location = new System.Drawing.Point(140, 148);
            this.xaantal.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantal.Name = "xaantal";
            this.xaantal.Size = new System.Drawing.Size(130, 25);
            this.xaantal.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(137, 128);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Aantal";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(137, 80);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(101, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Kies Bewerking";
            // 
            // xbewerking
            // 
            this.xbewerking.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.xbewerking.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.xbewerking.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbewerking.FormattingEnabled = true;
            this.xbewerking.Location = new System.Drawing.Point(140, 100);
            this.xbewerking.Name = "xbewerking";
            this.xbewerking.Size = new System.Drawing.Size(282, 25);
            this.xbewerking.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(137, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Artikel Nummer";
            // 
            // xartikelnr
            // 
            this.xartikelnr.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xartikelnr.Location = new System.Drawing.Point(137, 52);
            this.xartikelnr.Name = "xartikelnr";
            this.xartikelnr.Size = new System.Drawing.Size(138, 25);
            this.xartikelnr.TabIndex = 0;
            // 
            // NiewProductieForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 475);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(490, 475);
            this.Name = "NiewProductieForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nieuwe Productie Aanmaken";
            this.Title = "Nieuwe Productie Aanmaken";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xperuur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantal)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox xomschrijving;
        private System.Windows.Forms.DateTimePicker xleverdatum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown xperuur;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown xaantal;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox xbewerking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xartikelnr;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button xstarten;
        private System.Windows.Forms.Button xopslaan;
        private System.Windows.Forms.Button xannuleren;
        private System.Windows.Forms.TextBox xprodnr;
        private System.Windows.Forms.CheckBox xprodnrchecked;
    }
}