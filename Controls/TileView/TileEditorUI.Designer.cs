namespace Controls
{
    partial class TileEditorUI
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tileViewer1 = new Controls.TileViewer();
            this.tile1 = new Controls.Tile();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xformaatwijzigen = new System.Windows.Forms.Button();
            this.xtellingtextfont = new System.Windows.Forms.Button();
            this.xtextfont = new System.Windows.Forms.Button();
            this.xtextkleur = new System.Windows.Forms.Button();
            this.xtextkleurimage = new System.Windows.Forms.PictureBox();
            this.xtilekleurbutton = new System.Windows.Forms.Button();
            this.xtilekleur = new System.Windows.Forms.PictureBox();
            this.xtoontelling = new MetroFramework.Controls.MetroCheckBox();
            this.xomschrijving = new MetroFramework.Controls.MetroTextBox();
            this.xafbeeldingbutton = new System.Windows.Forms.Button();
            this.xafbeelding = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.tileViewer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xtextkleurimage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtilekleur)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xafbeelding)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tileViewer1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.Navy;
            this.groupBox1.Location = new System.Drawing.Point(356, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(379, 346);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Voorbeeld";
            // 
            // tileViewer1
            // 
            this.tileViewer1.AllowDrop = true;
            this.tileViewer1.AutoScroll = true;
            this.tileViewer1.BackColor = System.Drawing.Color.White;
            this.tileViewer1.Controls.Add(this.tile1);
            this.tileViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileViewer1.EnableTimer = false;
            this.tileViewer1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tileViewer1.Location = new System.Drawing.Point(3, 25);
            this.tileViewer1.Name = "tileViewer1";
            this.tileViewer1.Size = new System.Drawing.Size(373, 318);
            this.tileViewer1.TabIndex = 0;
            this.tileViewer1.TileInfoRefresInterval = 10000;
            // 
            // tile1
            // 
            this.tile1.ActiveControl = null;
            this.tile1.AllowTileEdit = false;
            this.tile1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tile1.BackColor = System.Drawing.Color.DeepSkyBlue;
            this.tile1.Location = new System.Drawing.Point(3, 3);
            this.tile1.Name = "tile1";
            this.tile1.Size = new System.Drawing.Size(256, 128);
            this.tile1.TabIndex = 0;
            this.tile1.Text = "tile1";
            this.tile1.TileCountFont = null;
            this.tile1.UseCustomBackColor = true;
            this.tile1.UseCustomForeColor = true;
            this.tile1.UseSelectable = true;
            this.tile1.UseStyleColors = true;
            this.tile1.UseTileImage = true;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.xformaatwijzigen);
            this.panel1.Controls.Add(this.xtellingtextfont);
            this.panel1.Controls.Add(this.xtextfont);
            this.panel1.Controls.Add(this.xtextkleur);
            this.panel1.Controls.Add(this.xtextkleurimage);
            this.panel1.Controls.Add(this.xtilekleurbutton);
            this.panel1.Controls.Add(this.xtilekleur);
            this.panel1.Controls.Add(this.xtoontelling);
            this.panel1.Controls.Add(this.xomschrijving);
            this.panel1.Controls.Add(this.xafbeeldingbutton);
            this.panel1.Controls.Add(this.xafbeelding);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(356, 346);
            this.panel1.TabIndex = 1;
            // 
            // xformaatwijzigen
            // 
            this.xformaatwijzigen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xformaatwijzigen.Location = new System.Drawing.Point(6, 113);
            this.xformaatwijzigen.Name = "xformaatwijzigen";
            this.xformaatwijzigen.Size = new System.Drawing.Size(242, 28);
            this.xformaatwijzigen.TabIndex = 10;
            this.xformaatwijzigen.Text = "Formaat Wijzigen";
            this.xformaatwijzigen.UseVisualStyleBackColor = true;
            this.xformaatwijzigen.Click += new System.EventHandler(this.xformaatwijzigen_Click);
            // 
            // xtellingtextfont
            // 
            this.xtellingtextfont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xtellingtextfont.Location = new System.Drawing.Point(123, 79);
            this.xtellingtextfont.Name = "xtellingtextfont";
            this.xtellingtextfont.Size = new System.Drawing.Size(125, 28);
            this.xtellingtextfont.TabIndex = 9;
            this.xtellingtextfont.Text = "Telling Text Font";
            this.xtellingtextfont.UseVisualStyleBackColor = true;
            this.xtellingtextfont.Click += new System.EventHandler(this.xtellingtextfont_Click);
            // 
            // xtextfont
            // 
            this.xtextfont.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xtextfont.Location = new System.Drawing.Point(6, 79);
            this.xtextfont.Name = "xtextfont";
            this.xtextfont.Size = new System.Drawing.Size(111, 28);
            this.xtextfont.TabIndex = 8;
            this.xtextfont.Text = "Text Font";
            this.xtextfont.UseVisualStyleBackColor = true;
            this.xtextfont.Click += new System.EventHandler(this.xtextfont_Click);
            // 
            // xtextkleur
            // 
            this.xtextkleur.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xtextkleur.Location = new System.Drawing.Point(6, 45);
            this.xtextkleur.Name = "xtextkleur";
            this.xtextkleur.Size = new System.Drawing.Size(111, 28);
            this.xtextkleur.TabIndex = 7;
            this.xtextkleur.Text = "Text Kleur";
            this.xtextkleur.UseVisualStyleBackColor = true;
            this.xtextkleur.Click += new System.EventHandler(this.xtextkleur_Click);
            // 
            // xtextkleurimage
            // 
            this.xtextkleurimage.Location = new System.Drawing.Point(123, 45);
            this.xtextkleurimage.Name = "xtextkleurimage";
            this.xtextkleurimage.Size = new System.Drawing.Size(28, 28);
            this.xtextkleurimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xtextkleurimage.TabIndex = 6;
            this.xtextkleurimage.TabStop = false;
            // 
            // xtilekleurbutton
            // 
            this.xtilekleurbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xtilekleurbutton.Location = new System.Drawing.Point(6, 7);
            this.xtilekleurbutton.Name = "xtilekleurbutton";
            this.xtilekleurbutton.Size = new System.Drawing.Size(111, 28);
            this.xtilekleurbutton.TabIndex = 5;
            this.xtilekleurbutton.Text = "Tile Kleur";
            this.xtilekleurbutton.UseVisualStyleBackColor = true;
            this.xtilekleurbutton.Click += new System.EventHandler(this.xtilekleurbutton_Click);
            // 
            // xtilekleur
            // 
            this.xtilekleur.Location = new System.Drawing.Point(123, 7);
            this.xtilekleur.Name = "xtilekleur";
            this.xtilekleur.Size = new System.Drawing.Size(28, 28);
            this.xtilekleur.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xtilekleur.TabIndex = 4;
            this.xtilekleur.TabStop = false;
            // 
            // xtoontelling
            // 
            this.xtoontelling.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xtoontelling.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xtoontelling.FontWeight = MetroFramework.MetroCheckBoxWeight.Bold;
            this.xtoontelling.Location = new System.Drawing.Point(6, 176);
            this.xtoontelling.Name = "xtoontelling";
            this.xtoontelling.Size = new System.Drawing.Size(344, 23);
            this.xtoontelling.TabIndex = 3;
            this.xtoontelling.Text = "Toon TileTelling indien mogelijk";
            this.xtoontelling.UseSelectable = true;
            this.xtoontelling.UseStyleColors = true;
            this.xtoontelling.CheckedChanged += new System.EventHandler(this.xtoontelling_CheckedChanged);
            // 
            // xomschrijving
            // 
            this.xomschrijving.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.xomschrijving.CustomButton.Image = null;
            this.xomschrijving.CustomButton.Location = new System.Drawing.Point(322, 1);
            this.xomschrijving.CustomButton.Name = "";
            this.xomschrijving.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xomschrijving.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xomschrijving.CustomButton.TabIndex = 1;
            this.xomschrijving.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xomschrijving.CustomButton.UseSelectable = true;
            this.xomschrijving.CustomButton.Visible = false;
            this.xomschrijving.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xomschrijving.Lines = new string[0];
            this.xomschrijving.Location = new System.Drawing.Point(6, 147);
            this.xomschrijving.MaxLength = 32767;
            this.xomschrijving.Name = "xomschrijving";
            this.xomschrijving.PasswordChar = '\0';
            this.xomschrijving.PromptText = "Tile Omschrijving";
            this.xomschrijving.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xomschrijving.SelectedText = "";
            this.xomschrijving.SelectionLength = 0;
            this.xomschrijving.SelectionStart = 0;
            this.xomschrijving.ShortcutsEnabled = true;
            this.xomschrijving.Size = new System.Drawing.Size(344, 23);
            this.xomschrijving.TabIndex = 2;
            this.xomschrijving.UseSelectable = true;
            this.xomschrijving.WaterMark = "Tile Omschrijving";
            this.xomschrijving.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xomschrijving.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xomschrijving.TextChanged += new System.EventHandler(this.xomschrijving_TextChanged);
            // 
            // xafbeeldingbutton
            // 
            this.xafbeeldingbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xafbeeldingbutton.Location = new System.Drawing.Point(254, 113);
            this.xafbeeldingbutton.Name = "xafbeeldingbutton";
            this.xafbeeldingbutton.Size = new System.Drawing.Size(96, 28);
            this.xafbeeldingbutton.TabIndex = 1;
            this.xafbeeldingbutton.Text = "Afbeelding";
            this.xafbeeldingbutton.UseVisualStyleBackColor = true;
            this.xafbeeldingbutton.Click += new System.EventHandler(this.xafbeeldingbutton_Click);
            // 
            // xafbeelding
            // 
            this.xafbeelding.Location = new System.Drawing.Point(254, 7);
            this.xafbeelding.Name = "xafbeelding";
            this.xafbeelding.Size = new System.Drawing.Size(96, 96);
            this.xafbeelding.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.xafbeelding.TabIndex = 0;
            this.xafbeelding.TabStop = false;
            // 
            // TileEditorUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "TileEditorUI";
            this.Size = new System.Drawing.Size(735, 346);
            this.groupBox1.ResumeLayout(false);
            this.tileViewer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xtextkleurimage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtilekleur)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xafbeelding)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.TileViewer tileViewer1;
        private Controls.Tile tile1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xformaatwijzigen;
        private System.Windows.Forms.Button xtellingtextfont;
        private System.Windows.Forms.Button xtextfont;
        private System.Windows.Forms.Button xtextkleur;
        private System.Windows.Forms.PictureBox xtextkleurimage;
        private System.Windows.Forms.Button xtilekleurbutton;
        private System.Windows.Forms.PictureBox xtilekleur;
        private MetroFramework.Controls.MetroCheckBox xtoontelling;
        private MetroFramework.Controls.MetroTextBox xomschrijving;
        private System.Windows.Forms.Button xafbeeldingbutton;
        private System.Windows.Forms.PictureBox xafbeelding;
    }
}
