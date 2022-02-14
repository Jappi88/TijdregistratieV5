namespace Forms
{
    partial class KlachtenForm
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
            this.xklachtenContainer = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xaddklacht = new System.Windows.Forms.ToolStripButton();
            this.xeditklacht = new System.Windows.Forms.ToolStripButton();
            this.xdeleteklacht = new System.Windows.Forms.ToolStripButton();
            this.xshowkrachtinfo = new System.Windows.Forms.ToolStripButton();
            this.xsearchbox = new MetroFramework.Controls.MetroTextBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xklachtenContainer);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(906, 484);
            this.panel1.TabIndex = 0;
            // 
            // xklachtenContainer
            // 
            this.xklachtenContainer.AutoScroll = true;
            this.xklachtenContainer.BackColor = System.Drawing.Color.White;
            this.xklachtenContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xklachtenContainer.Location = new System.Drawing.Point(0, 37);
            this.xklachtenContainer.Name = "xklachtenContainer";
            this.xklachtenContainer.Size = new System.Drawing.Size(906, 447);
            this.xklachtenContainer.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Controls.Add(this.xsearchbox);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(906, 37);
            this.panel2.TabIndex = 0;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.White;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xaddklacht,
            this.xeditklacht,
            this.xdeleteklacht,
            this.xshowkrachtinfo});
            this.toolStrip1.Location = new System.Drawing.Point(376, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(530, 39);
            this.toolStrip1.TabIndex = 1;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // xaddklacht
            // 
            this.xaddklacht.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xaddklacht.Image = global::ProductieManager.Properties.Resources.add_Blue_circle_32x32;
            this.xaddklacht.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xaddklacht.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xaddklacht.Name = "xaddklacht";
            this.xaddklacht.Size = new System.Drawing.Size(36, 36);
            this.xaddklacht.ToolTipText = "Voeg een nieuwe klacht toe";
            this.xaddklacht.Click += new System.EventHandler(this.xaddklacht_Click);
            // 
            // xeditklacht
            // 
            this.xeditklacht.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xeditklacht.Image = global::ProductieManager.Properties.Resources.edit__52382;
            this.xeditklacht.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xeditklacht.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xeditklacht.Name = "xeditklacht";
            this.xeditklacht.Size = new System.Drawing.Size(36, 36);
            this.xeditklacht.ToolTipText = "Wijzig de geselecteerde klacht";
            this.xeditklacht.Click += new System.EventHandler(this.xeditklacht_Click);
            // 
            // xdeleteklacht
            // 
            this.xdeleteklacht.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xdeleteklacht.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xdeleteklacht.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xdeleteklacht.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xdeleteklacht.Name = "xdeleteklacht";
            this.xdeleteklacht.Size = new System.Drawing.Size(36, 36);
            this.xdeleteklacht.ToolTipText = "Verwijder de geselecteerde klacht";
            this.xdeleteklacht.Click += new System.EventHandler(this.xdeleteklacht_Click);
            // 
            // xshowkrachtinfo
            // 
            this.xshowkrachtinfo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xshowkrachtinfo.Image = global::ProductieManager.Properties.Resources.Leave_80_icon_icons_com_57305;
            this.xshowkrachtinfo.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xshowkrachtinfo.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xshowkrachtinfo.Name = "xshowkrachtinfo";
            this.xshowkrachtinfo.Size = new System.Drawing.Size(36, 36);
            this.xshowkrachtinfo.ToolTipText = "Toon klacht omschrijving";
            this.xshowkrachtinfo.Click += new System.EventHandler(this.xshowkrachtinfo_Click);
            // 
            // xsearchbox
            // 
            // 
            // 
            // 
            this.xsearchbox.CustomButton.Image = null;
            this.xsearchbox.CustomButton.Location = new System.Drawing.Point(340, 1);
            this.xsearchbox.CustomButton.Name = "";
            this.xsearchbox.CustomButton.Size = new System.Drawing.Size(35, 35);
            this.xsearchbox.CustomButton.Style = MetroFramework.MetroColorStyle.Red;
            this.xsearchbox.CustomButton.TabIndex = 1;
            this.xsearchbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xsearchbox.CustomButton.UseSelectable = true;
            this.xsearchbox.CustomButton.Visible = false;
            this.xsearchbox.DisplayIcon = true;
            this.xsearchbox.Dock = System.Windows.Forms.DockStyle.Left;
            this.xsearchbox.FontSize = MetroFramework.MetroTextBoxSize.Tall;
            this.xsearchbox.Icon = global::ProductieManager.Properties.Resources.search_icon_32x32;
            this.xsearchbox.Lines = new string[] {
        "Zoeken..."};
            this.xsearchbox.Location = new System.Drawing.Point(0, 0);
            this.xsearchbox.MaxLength = 32767;
            this.xsearchbox.Name = "xsearchbox";
            this.xsearchbox.PasswordChar = '\0';
            this.xsearchbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xsearchbox.SelectedText = "";
            this.xsearchbox.SelectionLength = 0;
            this.xsearchbox.SelectionStart = 0;
            this.xsearchbox.ShortcutsEnabled = true;
            this.xsearchbox.ShowClearButton = true;
            this.xsearchbox.Size = new System.Drawing.Size(376, 37);
            this.xsearchbox.Style = MetroFramework.MetroColorStyle.Red;
            this.xsearchbox.TabIndex = 1;
            this.xsearchbox.TabStop = false;
            this.xsearchbox.Text = "Zoeken...";
            this.xsearchbox.UseSelectable = true;
            this.xsearchbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xsearchbox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xsearchbox.TextChanged += new System.EventHandler(this.xsearchbox_TextChanged);
            this.xsearchbox.Enter += new System.EventHandler(this.xsearchbox_Enter);
            this.xsearchbox.Leave += new System.EventHandler(this.xsearchbox_Leave);
            // 
            // KlachtenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(946, 564);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(700, 480);
            this.Name = "KlachtenForm";
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Alle Klachten";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.KlachtenForm_FormClosing);
            this.Shown += new System.EventHandler(this.KlachtenForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton xaddklacht;
        private System.Windows.Forms.ToolStripButton xeditklacht;
        private System.Windows.Forms.ToolStripButton xdeleteklacht;
        private MetroFramework.Controls.MetroTextBox xsearchbox;
        private System.Windows.Forms.Panel xklachtenContainer;
        private System.Windows.Forms.ToolStripButton xshowkrachtinfo;
    }
}