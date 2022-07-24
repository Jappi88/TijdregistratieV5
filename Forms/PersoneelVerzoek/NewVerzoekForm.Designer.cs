
namespace Forms.PersoneelVerzoek
{
    partial class NewVerzoekForm
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.xTotDate = new System.Windows.Forms.DateTimePicker();
            this.xVanafDate = new System.Windows.Forms.DateTimePicker();
            this.xMelderNaam = new MetroFramework.Controls.MetroTextBox();
            this.xKiesPersoneel = new System.Windows.Forms.Button();
            this.xPersoneelNaam = new MetroFramework.Controls.MetroTextBox();
            this.xVerzoekType = new MetroFramework.Controls.MetroComboBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xOK = new System.Windows.Forms.Button();
            this.xAnnuleren = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.xMelderNaam);
            this.panel1.Controls.Add(this.xKiesPersoneel);
            this.panel1.Controls.Add(this.xPersoneelNaam);
            this.panel1.Controls.Add(this.xVerzoekType);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(511, 197);
            this.panel1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.tableLayoutPanel1);
            this.groupBox1.Location = new System.Drawing.Point(3, 112);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(503, 47);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Verzoek Periode (0 uur)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 51.417F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 48.583F));
            this.tableLayoutPanel1.Controls.Add(this.xTotDate, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.xVanafDate, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 16);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(497, 28);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // xTotDate
            // 
            this.xTotDate.CustomFormat = "\'Tot\' dddd dd MMMM yyyy HH:mm ";
            this.xTotDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xTotDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xTotDate.Location = new System.Drawing.Point(258, 3);
            this.xTotDate.Name = "xTotDate";
            this.xTotDate.Size = new System.Drawing.Size(236, 20);
            this.xTotDate.TabIndex = 4;
            this.xTotDate.ValueChanged += new System.EventHandler(this.xVanafDate_ValueChanged);
            // 
            // xVanafDate
            // 
            this.xVanafDate.CustomFormat = "\'Vanaf\' dddd dd MMMM yyyy HH:mm ";
            this.xVanafDate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xVanafDate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xVanafDate.Location = new System.Drawing.Point(3, 3);
            this.xVanafDate.Name = "xVanafDate";
            this.xVanafDate.Size = new System.Drawing.Size(249, 20);
            this.xVanafDate.TabIndex = 3;
            this.xVanafDate.ValueChanged += new System.EventHandler(this.xVanafDate_ValueChanged);
            // 
            // xMelderNaam
            // 
            this.xMelderNaam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.xMelderNaam.CustomButton.Image = null;
            this.xMelderNaam.CustomButton.Location = new System.Drawing.Point(481, 1);
            this.xMelderNaam.CustomButton.Name = "";
            this.xMelderNaam.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xMelderNaam.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xMelderNaam.CustomButton.TabIndex = 1;
            this.xMelderNaam.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xMelderNaam.CustomButton.UseSelectable = true;
            this.xMelderNaam.CustomButton.Visible = false;
            this.xMelderNaam.Lines = new string[0];
            this.xMelderNaam.Location = new System.Drawing.Point(3, 48);
            this.xMelderNaam.MaxLength = 32767;
            this.xMelderNaam.Name = "xMelderNaam";
            this.xMelderNaam.PasswordChar = '\0';
            this.xMelderNaam.PromptText = "Vul in jou naam...";
            this.xMelderNaam.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xMelderNaam.SelectedText = "";
            this.xMelderNaam.SelectionLength = 0;
            this.xMelderNaam.SelectionStart = 0;
            this.xMelderNaam.ShortcutsEnabled = true;
            this.xMelderNaam.ShowClearButton = true;
            this.xMelderNaam.Size = new System.Drawing.Size(503, 23);
            this.xMelderNaam.TabIndex = 1;
            this.xMelderNaam.UseSelectable = true;
            this.xMelderNaam.UseStyleColors = true;
            this.xMelderNaam.WaterMark = "Vul in jou naam...";
            this.xMelderNaam.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xMelderNaam.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xMelderNaam.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // xKiesPersoneel
            // 
            this.xKiesPersoneel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xKiesPersoneel.FlatAppearance.BorderSize = 0;
            this.xKiesPersoneel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xKiesPersoneel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xKiesPersoneel.Image = global::ProductieManager.Properties.Resources.users_12820;
            this.xKiesPersoneel.Location = new System.Drawing.Point(474, 9);
            this.xKiesPersoneel.Name = "xKiesPersoneel";
            this.xKiesPersoneel.Size = new System.Drawing.Size(32, 32);
            this.xKiesPersoneel.TabIndex = 2;
            this.xKiesPersoneel.TabStop = false;
            this.xKiesPersoneel.UseVisualStyleBackColor = true;
            this.xKiesPersoneel.Click += new System.EventHandler(this.xKiesPersoneel_Click);
            // 
            // xPersoneelNaam
            // 
            this.xPersoneelNaam.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xPersoneelNaam.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.xPersoneelNaam.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            // 
            // 
            // 
            this.xPersoneelNaam.CustomButton.Image = null;
            this.xPersoneelNaam.CustomButton.Location = new System.Drawing.Point(445, 1);
            this.xPersoneelNaam.CustomButton.Name = "";
            this.xPersoneelNaam.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xPersoneelNaam.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xPersoneelNaam.CustomButton.TabIndex = 1;
            this.xPersoneelNaam.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xPersoneelNaam.CustomButton.UseSelectable = true;
            this.xPersoneelNaam.CustomButton.Visible = false;
            this.xPersoneelNaam.Lines = new string[0];
            this.xPersoneelNaam.Location = new System.Drawing.Point(3, 19);
            this.xPersoneelNaam.MaxLength = 32767;
            this.xPersoneelNaam.Name = "xPersoneelNaam";
            this.xPersoneelNaam.PasswordChar = '\0';
            this.xPersoneelNaam.PromptText = "Vul in of kies een Personeel...";
            this.xPersoneelNaam.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xPersoneelNaam.SelectedText = "";
            this.xPersoneelNaam.SelectionLength = 0;
            this.xPersoneelNaam.SelectionStart = 0;
            this.xPersoneelNaam.ShortcutsEnabled = true;
            this.xPersoneelNaam.ShowClearButton = true;
            this.xPersoneelNaam.Size = new System.Drawing.Size(467, 23);
            this.xPersoneelNaam.TabIndex = 0;
            this.xPersoneelNaam.UseSelectable = true;
            this.xPersoneelNaam.UseStyleColors = true;
            this.xPersoneelNaam.WaterMark = "Vul in of kies een Personeel...";
            this.xPersoneelNaam.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xPersoneelNaam.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xPersoneelNaam.TextChanged += new System.EventHandler(this.TextBox_TextChanged);
            // 
            // xVerzoekType
            // 
            this.xVerzoekType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xVerzoekType.FormattingEnabled = true;
            this.xVerzoekType.ItemHeight = 23;
            this.xVerzoekType.Items.AddRange(new object[] {
            "Vrij",
            "Overwerk",
            "Verwijderen"});
            this.xVerzoekType.Location = new System.Drawing.Point(3, 77);
            this.xVerzoekType.Name = "xVerzoekType";
            this.xVerzoekType.PromptText = "Kies Verzoeksoort...";
            this.xVerzoekType.Size = new System.Drawing.Size(503, 29);
            this.xVerzoekType.TabIndex = 2;
            this.xVerzoekType.UseSelectable = true;
            this.xVerzoekType.UseStyleColors = true;
            this.xVerzoekType.SelectedIndexChanged += new System.EventHandler(this.xVerzoekType_SelectedIndexChanged);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xOK);
            this.panel2.Controls.Add(this.xAnnuleren);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 160);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(511, 37);
            this.panel2.TabIndex = 1;
            // 
            // xOK
            // 
            this.xOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xOK.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOK.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOK.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xOK.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xOK.Location = new System.Drawing.Point(296, 3);
            this.xOK.Name = "xOK";
            this.xOK.Size = new System.Drawing.Size(103, 32);
            this.xOK.TabIndex = 1;
            this.xOK.TabStop = false;
            this.xOK.Text = "OK";
            this.xOK.UseVisualStyleBackColor = true;
            this.xOK.Click += new System.EventHandler(this.xOK_Click);
            // 
            // xAnnuleren
            // 
            this.xAnnuleren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xAnnuleren.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xAnnuleren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xAnnuleren.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xAnnuleren.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xAnnuleren.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xAnnuleren.Location = new System.Drawing.Point(405, 3);
            this.xAnnuleren.Name = "xAnnuleren";
            this.xAnnuleren.Size = new System.Drawing.Size(103, 32);
            this.xAnnuleren.TabIndex = 0;
            this.xAnnuleren.TabStop = false;
            this.xAnnuleren.Text = "Annuleren";
            this.xAnnuleren.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xAnnuleren.UseVisualStyleBackColor = true;
            // 
            // NewVerzoekForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(551, 277);
            this.Controls.Add(this.panel1);
            this.Name = "NewVerzoekForm";
            this.SaveLastSize = false;
            this.ShowIcon = false;
            this.Text = "Verzoek Indienen";
            this.Title = "Verzoek Indienen";
            this.panel1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroComboBox xVerzoekType;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xKiesPersoneel;
        private MetroFramework.Controls.MetroTextBox xPersoneelNaam;
        private System.Windows.Forms.Button xOK;
        private System.Windows.Forms.Button xAnnuleren;
        private MetroFramework.Controls.MetroTextBox xMelderNaam;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DateTimePicker xTotDate;
        private System.Windows.Forms.DateTimePicker xVanafDate;
    }
}