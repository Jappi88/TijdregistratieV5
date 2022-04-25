
namespace Controls
{
    partial class BerekenLeverdatumUI
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
            this.xartikelnrTextbox = new MetroFramework.Controls.MetroTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xinfo = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xtekeningbutton = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.xgeproduceerd = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.xaantal = new System.Windows.Forms.NumericUpDown();
            this.xroosterbutton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.xperuur = new System.Windows.Forms.NumericUpDown();
            this.xaantalpersonen = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xgewenstleverdatumradio = new MetroFramework.Controls.MetroRadioButton();
            this.xbeginopradio = new MetroFramework.Controls.MetroRadioButton();
            this.xbeginop = new System.Windows.Forms.DateTimePicker();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgeproduceerd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xperuur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalpersonen)).BeginInit();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // xartikelnrTextbox
            // 
            this.xartikelnrTextbox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.xartikelnrTextbox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            // 
            // 
            // 
            this.xartikelnrTextbox.CustomButton.Image = null;
            this.xartikelnrTextbox.CustomButton.Location = new System.Drawing.Point(490, 1);
            this.xartikelnrTextbox.CustomButton.Name = "";
            this.xartikelnrTextbox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xartikelnrTextbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xartikelnrTextbox.CustomButton.TabIndex = 1;
            this.xartikelnrTextbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xartikelnrTextbox.CustomButton.UseSelectable = true;
            this.xartikelnrTextbox.CustomButton.Visible = false;
            this.xartikelnrTextbox.Dock = System.Windows.Forms.DockStyle.Top;
            this.xartikelnrTextbox.Lines = new string[0];
            this.xartikelnrTextbox.Location = new System.Drawing.Point(5, 5);
            this.xartikelnrTextbox.MaxLength = 32767;
            this.xartikelnrTextbox.Name = "xartikelnrTextbox";
            this.xartikelnrTextbox.PasswordChar = '\0';
            this.xartikelnrTextbox.PromptText = "Vul in een Artikelnr...";
            this.xartikelnrTextbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xartikelnrTextbox.SelectedText = "";
            this.xartikelnrTextbox.SelectionLength = 0;
            this.xartikelnrTextbox.SelectionStart = 0;
            this.xartikelnrTextbox.ShortcutsEnabled = true;
            this.xartikelnrTextbox.ShowClearButton = true;
            this.xartikelnrTextbox.Size = new System.Drawing.Size(512, 23);
            this.xartikelnrTextbox.TabIndex = 0;
            this.xartikelnrTextbox.UseSelectable = true;
            this.xartikelnrTextbox.WaterMark = "Vul in een Artikelnr...";
            this.xartikelnrTextbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xartikelnrTextbox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xartikelnrTextbox.TextChanged += new System.EventHandler(this.xartikelnrTextbox_TextChanged);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.xinfo);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.xartikelnrTextbox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(522, 373);
            this.panel1.TabIndex = 2;
            // 
            // xinfo
            // 
            this.xinfo.AutoScroll = true;
            this.xinfo.BackColor = System.Drawing.SystemColors.Window;
            this.xinfo.BaseStylesheet = null;
            this.xinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xinfo.IsContextMenuEnabled = false;
            this.xinfo.Location = new System.Drawing.Point(174, 80);
            this.xinfo.Name = "xinfo";
            this.xinfo.Size = new System.Drawing.Size(343, 288);
            this.xinfo.TabIndex = 17;
            this.xinfo.Text = null;
            this.xinfo.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xinfo_ImageLoad);
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.Controls.Add(this.xtekeningbutton);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.xgeproduceerd);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.xaantal);
            this.panel2.Controls.Add(this.xroosterbutton);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.xperuur);
            this.panel2.Controls.Add(this.xaantalpersonen);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(5, 80);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(169, 288);
            this.panel2.TabIndex = 18;
            // 
            // xtekeningbutton
            // 
            this.xtekeningbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xtekeningbutton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtekeningbutton.Image = global::ProductieManager.Properties.Resources.libreoffice_draw_icon_181050;
            this.xtekeningbutton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xtekeningbutton.Location = new System.Drawing.Point(6, 235);
            this.xtekeningbutton.Name = "xtekeningbutton";
            this.xtekeningbutton.Size = new System.Drawing.Size(141, 34);
            this.xtekeningbutton.TabIndex = 11;
            this.xtekeningbutton.Text = "Werk Tekening";
            this.xtekeningbutton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xtekeningbutton.UseVisualStyleBackColor = true;
            this.xtekeningbutton.Visible = false;
            this.xtekeningbutton.Click += new System.EventHandler(this.xtekeningbutton_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(3, 48);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 17);
            this.label5.TabIndex = 10;
            this.label5.Text = "Aantal Gemaakt";
            // 
            // xgeproduceerd
            // 
            this.xgeproduceerd.Location = new System.Drawing.Point(6, 68);
            this.xgeproduceerd.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xgeproduceerd.Name = "xgeproduceerd";
            this.xgeproduceerd.Size = new System.Drawing.Size(141, 25);
            this.xgeproduceerd.TabIndex = 9;
            this.xgeproduceerd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xgeproduceerd.ThousandsSeparator = true;
            this.xgeproduceerd.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(144, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Aantal Te Produceren:";
            // 
            // xaantal
            // 
            this.xaantal.Location = new System.Drawing.Point(6, 20);
            this.xaantal.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantal.Name = "xaantal";
            this.xaantal.Size = new System.Drawing.Size(141, 25);
            this.xaantal.TabIndex = 2;
            this.xaantal.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xaantal.ThousandsSeparator = true;
            this.xaantal.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // xroosterbutton
            // 
            this.xroosterbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xroosterbutton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xroosterbutton.Image = global::ProductieManager.Properties.Resources.schedule_32_32;
            this.xroosterbutton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xroosterbutton.Location = new System.Drawing.Point(6, 195);
            this.xroosterbutton.Name = "xroosterbutton";
            this.xroosterbutton.Size = new System.Drawing.Size(141, 34);
            this.xroosterbutton.TabIndex = 8;
            this.xroosterbutton.Text = "Werk Rooster";
            this.xroosterbutton.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xroosterbutton.UseVisualStyleBackColor = true;
            this.xroosterbutton.Click += new System.EventHandler(this.xroosterbutton_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(3, 96);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(113, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Aantal Personen:";
            // 
            // xperuur
            // 
            this.xperuur.Location = new System.Drawing.Point(6, 164);
            this.xperuur.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xperuur.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xperuur.Name = "xperuur";
            this.xperuur.Size = new System.Drawing.Size(141, 25);
            this.xperuur.TabIndex = 7;
            this.xperuur.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xperuur.ThousandsSeparator = true;
            this.xperuur.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xperuur.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // xaantalpersonen
            // 
            this.xaantalpersonen.Location = new System.Drawing.Point(6, 116);
            this.xaantalpersonen.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xaantalpersonen.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xaantalpersonen.Name = "xaantalpersonen";
            this.xaantalpersonen.Size = new System.Drawing.Size(141, 25);
            this.xaantalpersonen.TabIndex = 5;
            this.xaantalpersonen.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xaantalpersonen.ThousandsSeparator = true;
            this.xaantalpersonen.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xaantalpersonen.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(3, 144);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 6;
            this.label4.Text = "Aantal P/u:";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xgewenstleverdatumradio);
            this.panel3.Controls.Add(this.xbeginopradio);
            this.panel3.Controls.Add(this.xbeginop);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(5, 28);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(512, 52);
            this.panel3.TabIndex = 0;
            // 
            // xgewenstleverdatumradio
            // 
            this.xgewenstleverdatumradio.AutoSize = true;
            this.xgewenstleverdatumradio.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xgewenstleverdatumradio.FontWeight = MetroFramework.MetroCheckBoxWeight.Bold;
            this.xgewenstleverdatumradio.Location = new System.Drawing.Point(127, 6);
            this.xgewenstleverdatumradio.Name = "xgewenstleverdatumradio";
            this.xgewenstleverdatumradio.Size = new System.Drawing.Size(177, 19);
            this.xgewenstleverdatumradio.TabIndex = 3;
            this.xgewenstleverdatumradio.Text = "Gewenste Leverdatum:";
            this.xgewenstleverdatumradio.UseSelectable = true;
            this.xgewenstleverdatumradio.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // xbeginopradio
            // 
            this.xbeginopradio.AutoSize = true;
            this.xbeginopradio.Checked = true;
            this.xbeginopradio.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xbeginopradio.FontWeight = MetroFramework.MetroCheckBoxWeight.Bold;
            this.xbeginopradio.Location = new System.Drawing.Point(6, 6);
            this.xbeginopradio.Name = "xbeginopradio";
            this.xbeginopradio.Size = new System.Drawing.Size(115, 19);
            this.xbeginopradio.TabIndex = 2;
            this.xbeginopradio.TabStop = true;
            this.xbeginopradio.Text = "Beginnen Op:";
            this.xbeginopradio.UseSelectable = true;
            this.xbeginopradio.CheckedChanged += new System.EventHandler(this.ValueChanged);
            // 
            // xbeginop
            // 
            this.xbeginop.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xbeginop.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xbeginop.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xbeginop.Location = new System.Drawing.Point(0, 27);
            this.xbeginop.Name = "xbeginop";
            this.xbeginop.Size = new System.Drawing.Size(512, 25);
            this.xbeginop.TabIndex = 0;
            this.xbeginop.ValueChanged += new System.EventHandler(this.ValueChanged);
            // 
            // BerekenLeverdatumUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BerekenLeverdatumUI";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Size = new System.Drawing.Size(532, 383);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xgeproduceerd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xperuur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xaantalpersonen)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTextBox xartikelnrTextbox;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xroosterbutton;
        private System.Windows.Forms.NumericUpDown xperuur;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown xaantalpersonen;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown xaantal;
        private System.Windows.Forms.DateTimePicker xbeginop;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel xinfo;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown xgeproduceerd;
        private System.Windows.Forms.Button xtekeningbutton;
        private System.Windows.Forms.Panel panel3;
        private MetroFramework.Controls.MetroRadioButton xgewenstleverdatumradio;
        private MetroFramework.Controls.MetroRadioButton xbeginopradio;
    }
}
