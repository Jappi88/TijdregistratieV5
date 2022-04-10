namespace Forms.Sporen
{
    partial class NewSpoorForm
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
            this.xok = new System.Windows.Forms.Button();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xnaam = new MetroFramework.Controls.MetroTextBox();
            this.xomschrijving = new MetroFramework.Controls.MetroTextBox();
            this.productieVerbruikUI1 = new Controls.ProductieVerbruikUI();
            this.xartikelnr = new MetroFramework.Controls.MetroTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xok);
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(20, 590);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(955, 44);
            this.panel1.TabIndex = 0;
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xok.Location = new System.Drawing.Point(725, 3);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(110, 37);
            this.xok.TabIndex = 10;
            this.xok.TabStop = false;
            this.xok.Text = "OK";
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.button2_Click);
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(841, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(110, 37);
            this.xsluiten.TabIndex = 11;
            this.xsluiten.TabStop = false;
            this.xsluiten.Text = "Annuleren";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            // 
            // xnaam
            // 
            // 
            // 
            // 
            this.xnaam.CustomButton.Image = null;
            this.xnaam.CustomButton.Location = new System.Drawing.Point(933, 1);
            this.xnaam.CustomButton.Name = "";
            this.xnaam.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xnaam.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xnaam.CustomButton.TabIndex = 1;
            this.xnaam.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xnaam.CustomButton.UseSelectable = true;
            this.xnaam.CustomButton.Visible = false;
            this.xnaam.Dock = System.Windows.Forms.DockStyle.Top;
            this.xnaam.Lines = new string[0];
            this.xnaam.Location = new System.Drawing.Point(20, 60);
            this.xnaam.MaxLength = 32767;
            this.xnaam.Name = "xnaam";
            this.xnaam.PasswordChar = '\0';
            this.xnaam.PromptText = "Vul in jou naam";
            this.xnaam.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xnaam.SelectedText = "";
            this.xnaam.SelectionLength = 0;
            this.xnaam.SelectionStart = 0;
            this.xnaam.ShortcutsEnabled = true;
            this.xnaam.Size = new System.Drawing.Size(955, 23);
            this.xnaam.Style = MetroFramework.MetroColorStyle.Blue;
            this.xnaam.TabIndex = 0;
            this.xnaam.UseSelectable = true;
            this.xnaam.UseStyleColors = true;
            this.xnaam.WaterMark = "Vul in jou naam";
            this.xnaam.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xnaam.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // xomschrijving
            // 
            // 
            // 
            // 
            this.xomschrijving.CustomButton.Image = null;
            this.xomschrijving.CustomButton.Location = new System.Drawing.Point(871, 1);
            this.xomschrijving.CustomButton.Name = "";
            this.xomschrijving.CustomButton.Size = new System.Drawing.Size(83, 83);
            this.xomschrijving.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xomschrijving.CustomButton.TabIndex = 1;
            this.xomschrijving.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xomschrijving.CustomButton.UseSelectable = true;
            this.xomschrijving.CustomButton.Visible = false;
            this.xomschrijving.Dock = System.Windows.Forms.DockStyle.Top;
            this.xomschrijving.Lines = new string[0];
            this.xomschrijving.Location = new System.Drawing.Point(20, 106);
            this.xomschrijving.MaxLength = 32767;
            this.xomschrijving.Multiline = true;
            this.xomschrijving.Name = "xomschrijving";
            this.xomschrijving.PasswordChar = '\0';
            this.xomschrijving.PromptText = "Vul in een omschrijving";
            this.xomschrijving.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xomschrijving.SelectedText = "";
            this.xomschrijving.SelectionLength = 0;
            this.xomschrijving.SelectionStart = 0;
            this.xomschrijving.ShortcutsEnabled = true;
            this.xomschrijving.Size = new System.Drawing.Size(955, 85);
            this.xomschrijving.Style = MetroFramework.MetroColorStyle.Blue;
            this.xomschrijving.TabIndex = 2;
            this.xomschrijving.UseSelectable = true;
            this.xomschrijving.UseStyleColors = true;
            this.xomschrijving.WaterMark = "Vul in een omschrijving";
            this.xomschrijving.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xomschrijving.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // productieVerbruikUI1
            // 
            this.productieVerbruikUI1.BackColor = System.Drawing.Color.White;
            this.productieVerbruikUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieVerbruikUI1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieVerbruikUI1.Location = new System.Drawing.Point(20, 191);
            this.productieVerbruikUI1.MaxUitgangsLengte = new decimal(new int[] {
            7500,
            0,
            0,
            0});
            this.productieVerbruikUI1.Name = "productieVerbruikUI1";
            this.productieVerbruikUI1.OpdrukkerArtikel = null;
            this.productieVerbruikUI1.Padding = new System.Windows.Forms.Padding(5);
            this.productieVerbruikUI1.RestStuk = new decimal(new int[] {
            20,
            0,
            0,
            0});
            this.productieVerbruikUI1.ShowMateriaalSelector = false;
            this.productieVerbruikUI1.ShowOpdrukkerArtikelNr = false;
            this.productieVerbruikUI1.ShowOpslaan = false;
            this.productieVerbruikUI1.ShowPerUur = false;
            this.productieVerbruikUI1.ShowSluiten = false;
            this.productieVerbruikUI1.Size = new System.Drawing.Size(955, 399);
            this.productieVerbruikUI1.TabIndex = 9;
            this.productieVerbruikUI1.TabStop = false;
            this.productieVerbruikUI1.Title = "Verbruik Berekenen";
            // 
            // xartikelnr
            // 
            // 
            // 
            // 
            this.xartikelnr.CustomButton.Image = null;
            this.xartikelnr.CustomButton.Location = new System.Drawing.Point(933, 1);
            this.xartikelnr.CustomButton.Name = "";
            this.xartikelnr.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xartikelnr.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xartikelnr.CustomButton.TabIndex = 1;
            this.xartikelnr.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xartikelnr.CustomButton.UseSelectable = true;
            this.xartikelnr.CustomButton.Visible = false;
            this.xartikelnr.Dock = System.Windows.Forms.DockStyle.Top;
            this.xartikelnr.Lines = new string[0];
            this.xartikelnr.Location = new System.Drawing.Point(20, 83);
            this.xartikelnr.MaxLength = 32767;
            this.xartikelnr.Name = "xartikelnr";
            this.xartikelnr.PasswordChar = '\0';
            this.xartikelnr.PromptText = "Vul in een ArtikelNr";
            this.xartikelnr.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xartikelnr.SelectedText = "";
            this.xartikelnr.SelectionLength = 0;
            this.xartikelnr.SelectionStart = 0;
            this.xartikelnr.ShortcutsEnabled = true;
            this.xartikelnr.Size = new System.Drawing.Size(955, 23);
            this.xartikelnr.Style = MetroFramework.MetroColorStyle.Blue;
            this.xartikelnr.TabIndex = 1;
            this.xartikelnr.UseSelectable = true;
            this.xartikelnr.UseStyleColors = true;
            this.xartikelnr.WaterMark = "Vul in een ArtikelNr";
            this.xartikelnr.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xartikelnr.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // NewSpoorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(995, 654);
            this.Controls.Add(this.productieVerbruikUI1);
            this.Controls.Add(this.xomschrijving);
            this.Controls.Add(this.xartikelnr);
            this.Controls.Add(this.xnaam);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(627, 455);
            this.Name = "NewSpoorForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nieuwe Spoor Info";
            this.Title = "Nieuwe Spoor Info";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Button xok;
        private Controls.ProductieVerbruikUI productieVerbruikUI1;
        private MetroFramework.Controls.MetroTextBox xnaam;
        private MetroFramework.Controls.MetroTextBox xomschrijving;
        private MetroFramework.Controls.MetroTextBox xartikelnr;
    }
}