
namespace Forms
{
    partial class WerkplaatsSettingsForm
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
            this.xopslaan = new System.Windows.Forms.Button();
            this.xclose = new System.Windows.Forms.Button();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.roosterUI1 = new Controls.RoosterUI();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.filterEntryEditorUI1 = new Controls.FilterEntryEditorUI();
            this.panel1.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xopslaan);
            this.panel1.Controls.Add(this.xclose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 501);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(691, 39);
            this.panel1.TabIndex = 1;
            // 
            // xopslaan
            // 
            this.xopslaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xopslaan.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.xopslaan.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xopslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xopslaan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xopslaan.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.xopslaan.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xopslaan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xopslaan.Location = new System.Drawing.Point(487, 3);
            this.xopslaan.Name = "xopslaan";
            this.xopslaan.Size = new System.Drawing.Size(100, 32);
            this.xopslaan.TabIndex = 1;
            this.xopslaan.Text = "Opslaan";
            this.xopslaan.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xopslaan.UseVisualStyleBackColor = true;
            this.xopslaan.Click += new System.EventHandler(this.xopslaan_Click);
            // 
            // xclose
            // 
            this.xclose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xclose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xclose.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xclose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xclose.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xclose.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.xclose.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xclose.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xclose.Location = new System.Drawing.Point(593, 3);
            this.xclose.Name = "xclose";
            this.xclose.Size = new System.Drawing.Size(95, 32);
            this.xclose.TabIndex = 0;
            this.xclose.Text = "Sluiten";
            this.xclose.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xclose.UseVisualStyleBackColor = true;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(20, 60);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(691, 441);
            this.metroTabControl1.TabIndex = 2;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.roosterUI1);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Padding = new System.Windows.Forms.Padding(5);
            this.metroTabPage1.Size = new System.Drawing.Size(683, 399);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Rooster";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // roosterUI1
            // 
            this.roosterUI1.AutoUpdateBewerkingen = false;
            this.roosterUI1.BackColor = System.Drawing.Color.White;
            this.roosterUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.roosterUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.roosterUI1.Location = new System.Drawing.Point(5, 5);
            this.roosterUI1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.roosterUI1.Name = "roosterUI1";
            this.roosterUI1.Padding = new System.Windows.Forms.Padding(5);
            this.roosterUI1.ShowNationaleFeestDagen = true;
            this.roosterUI1.ShowSpecialeRoosterButton = true;
            this.roosterUI1.Size = new System.Drawing.Size(673, 389);
            this.roosterUI1.SpecialeRoosters = null;
            this.roosterUI1.TabIndex = 2;
            this.roosterUI1.WerkRooster = null;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.groupBox1);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Padding = new System.Windows.Forms.Padding(5);
            this.metroTabPage2.Size = new System.Drawing.Size(683, 399);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Indeling Voorwaardes";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // groupBox1
            // 
            this.groupBox1.BackColor = System.Drawing.Color.Transparent;
            this.groupBox1.Controls.Add(this.filterEntryEditorUI1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(5, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(673, 389);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Maak voorwaardes voor het indelen";
            // 
            // filterEntryEditorUI1
            // 
            this.filterEntryEditorUI1.BackColor = System.Drawing.Color.White;
            this.filterEntryEditorUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.filterEntryEditorUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.filterEntryEditorUI1.Location = new System.Drawing.Point(3, 25);
            this.filterEntryEditorUI1.Margin = new System.Windows.Forms.Padding(5);
            this.filterEntryEditorUI1.Name = "filterEntryEditorUI1";
            this.filterEntryEditorUI1.Padding = new System.Windows.Forms.Padding(5);
            this.filterEntryEditorUI1.SelectedFilter = null;
            this.filterEntryEditorUI1.Size = new System.Drawing.Size(667, 361);
            this.filterEntryEditorUI1.TabIndex = 2;
            // 
            // WerkplaatsSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(731, 560);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "WerkplaatsSettingsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Werkplaats Instellingen";
            this.Title = "Werkplaats Instellingen";
            this.Shown += new System.EventHandler(this.WerkplaatsSettingsForm_Shown);
            this.panel1.ResumeLayout(false);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xopslaan;
        private System.Windows.Forms.Button xclose;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private System.Windows.Forms.GroupBox groupBox1;
        private Controls.FilterEntryEditorUI filterEntryEditorUI1;
        public Controls.RoosterUI roosterUI1;
    }
}