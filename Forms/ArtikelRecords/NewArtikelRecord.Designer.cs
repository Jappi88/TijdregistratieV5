namespace Forms.ArtikelRecords
{
    partial class NewArtikelRecord
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
            this.panel2 = new System.Windows.Forms.Panel();
            this.xwerkplekcheck = new MetroFramework.Controls.MetroCheckBox();
            this.xok = new System.Windows.Forms.Button();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xartikelnr = new MetroFramework.Controls.MetroTextBox();
            this.xomschrijving = new MetroFramework.Controls.MetroTextBox();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xwerkplekcheck);
            this.panel2.Controls.Add(this.xok);
            this.panel2.Controls.Add(this.xsluiten);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 176);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(469, 42);
            this.panel2.TabIndex = 7;
            // 
            // xwerkplekcheck
            // 
            this.xwerkplekcheck.Dock = System.Windows.Forms.DockStyle.Left;
            this.xwerkplekcheck.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.xwerkplekcheck.ForeColor = System.Drawing.SystemColors.ControlText;
            this.xwerkplekcheck.Location = new System.Drawing.Point(5, 5);
            this.xwerkplekcheck.Name = "xwerkplekcheck";
            this.xwerkplekcheck.Size = new System.Drawing.Size(105, 32);
            this.xwerkplekcheck.Style = MetroFramework.MetroColorStyle.Purple;
            this.xwerkplekcheck.TabIndex = 9;
            this.xwerkplekcheck.Text = "Werkplek";
            this.xwerkplekcheck.UseSelectable = true;
            this.xwerkplekcheck.UseStyleColors = true;
            this.xwerkplekcheck.CheckedChanged += new System.EventHandler(this.xwerkplekcheck_CheckedChanged);
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xok.Location = new System.Drawing.Point(186, 5);
            this.xok.Margin = new System.Windows.Forms.Padding(4);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(135, 32);
            this.xok.TabIndex = 7;
            this.xok.Text = "Toevoegen";
            this.xok.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // xsluiten
            // 
            this.xsluiten.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xsluiten.Dock = System.Windows.Forms.DockStyle.Right;
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(329, 5);
            this.xsluiten.Margin = new System.Windows.Forms.Padding(4);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(135, 32);
            this.xsluiten.TabIndex = 8;
            this.xsluiten.Text = "Annuleren";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xartikelnr
            // 
            // 
            // 
            // 
            this.xartikelnr.CustomButton.Image = null;
            this.xartikelnr.CustomButton.Location = new System.Drawing.Point(439, 2);
            this.xartikelnr.CustomButton.Name = "";
            this.xartikelnr.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.xartikelnr.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xartikelnr.CustomButton.TabIndex = 1;
            this.xartikelnr.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xartikelnr.CustomButton.UseSelectable = true;
            this.xartikelnr.CustomButton.Visible = false;
            this.xartikelnr.Dock = System.Windows.Forms.DockStyle.Top;
            this.xartikelnr.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xartikelnr.Lines = new string[0];
            this.xartikelnr.Location = new System.Drawing.Point(20, 60);
            this.xartikelnr.MaxLength = 32767;
            this.xartikelnr.Name = "xartikelnr";
            this.xartikelnr.PasswordChar = '\0';
            this.xartikelnr.PromptText = "Vul in een ArtikelNr of een Werkplek naam";
            this.xartikelnr.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xartikelnr.SelectedText = "";
            this.xartikelnr.SelectionLength = 0;
            this.xartikelnr.SelectionStart = 0;
            this.xartikelnr.ShortcutsEnabled = true;
            this.xartikelnr.ShowClearButton = true;
            this.xartikelnr.Size = new System.Drawing.Size(469, 32);
            this.xartikelnr.Style = MetroFramework.MetroColorStyle.Purple;
            this.xartikelnr.TabIndex = 8;
            this.xartikelnr.UseSelectable = true;
            this.xartikelnr.WaterMark = "Vul in een ArtikelNr of een Werkplek naam";
            this.xartikelnr.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xartikelnr.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // xomschrijving
            // 
            // 
            // 
            // 
            this.xomschrijving.CustomButton.Image = null;
            this.xomschrijving.CustomButton.Location = new System.Drawing.Point(387, 2);
            this.xomschrijving.CustomButton.Name = "";
            this.xomschrijving.CustomButton.Size = new System.Drawing.Size(79, 79);
            this.xomschrijving.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xomschrijving.CustomButton.TabIndex = 1;
            this.xomschrijving.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xomschrijving.CustomButton.UseSelectable = true;
            this.xomschrijving.CustomButton.Visible = false;
            this.xomschrijving.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xomschrijving.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xomschrijving.Lines = new string[0];
            this.xomschrijving.Location = new System.Drawing.Point(20, 92);
            this.xomschrijving.MaxLength = 32767;
            this.xomschrijving.Multiline = true;
            this.xomschrijving.Name = "xomschrijving";
            this.xomschrijving.PasswordChar = '\0';
            this.xomschrijving.PromptText = "Vul in een Omschrijving";
            this.xomschrijving.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xomschrijving.SelectedText = "";
            this.xomschrijving.SelectionLength = 0;
            this.xomschrijving.SelectionStart = 0;
            this.xomschrijving.ShortcutsEnabled = true;
            this.xomschrijving.ShowClearButton = true;
            this.xomschrijving.Size = new System.Drawing.Size(469, 84);
            this.xomschrijving.Style = MetroFramework.MetroColorStyle.Purple;
            this.xomschrijving.TabIndex = 9;
            this.xomschrijving.UseSelectable = true;
            this.xomschrijving.WaterMark = "Vul in een Omschrijving";
            this.xomschrijving.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xomschrijving.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // NewArtikelRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(509, 238);
            this.Controls.Add(this.xomschrijving);
            this.Controls.Add(this.xartikelnr);
            this.Controls.Add(this.panel2);
            this.Name = "NewArtikelRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Nieuwe Artikel/ Werkplek";
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xok;
        private System.Windows.Forms.Button xsluiten;
        private MetroFramework.Controls.MetroTextBox xartikelnr;
        private MetroFramework.Controls.MetroTextBox xomschrijving;
        private MetroFramework.Controls.MetroCheckBox xwerkplekcheck;
    }
}