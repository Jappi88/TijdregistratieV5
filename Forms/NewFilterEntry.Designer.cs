
namespace ProductieManager.Forms
{
    partial class NewFilterEntry
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
            this.xannuleren = new MetroFramework.Controls.MetroButton();
            this.xok = new MetroFramework.Controls.MetroButton();
            this.xoperandtype = new MetroFramework.Controls.MetroComboBox();
            this.xvariablenamelabel = new MetroFramework.Controls.MetroLabel();
            this.metroPanel1 = new MetroFramework.Controls.MetroPanel();
            this.xvaluetypes = new MetroFramework.Controls.MetroComboBox();
            this.xtextvalue = new MetroFramework.Controls.MetroTextBox();
            this.xvaluepanel = new MetroFramework.Controls.MetroPanel();
            this.xcheckvalue = new MetroFramework.Controls.MetroCheckBox();
            this.xdecimalvalue = new System.Windows.Forms.NumericUpDown();
            this.xdatevalue = new MetroFramework.Controls.MetroDateTime();
            this.xcombovalue = new MetroFramework.Controls.MetroComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xcriteriahtml = new HtmlRenderer.HtmlPanel();
            this.xvoorwaardenb = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.metroPanel1.SuspendLayout();
            this.xvaluepanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xdecimalvalue)).BeginInit();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xannuleren
            // 
            this.xannuleren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xannuleren.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xannuleren.Location = new System.Drawing.Point(347, 6);
            this.xannuleren.Name = "xannuleren";
            this.xannuleren.Size = new System.Drawing.Size(100, 34);
            this.xannuleren.TabIndex = 0;
            this.xannuleren.Text = "Annuleren";
            this.xannuleren.UseSelectable = true;
            this.xannuleren.Click += new System.EventHandler(this.xannuleren_Click);
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xok.Location = new System.Drawing.Point(241, 6);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(100, 34);
            this.xok.TabIndex = 1;
            this.xok.Text = "OK";
            this.xok.UseSelectable = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // xoperandtype
            // 
            this.xoperandtype.Dock = System.Windows.Forms.DockStyle.Left;
            this.xoperandtype.FormattingEnabled = true;
            this.xoperandtype.ItemHeight = 23;
            this.xoperandtype.Location = new System.Drawing.Point(0, 0);
            this.xoperandtype.Name = "xoperandtype";
            this.xoperandtype.Size = new System.Drawing.Size(79, 29);
            this.xoperandtype.TabIndex = 2;
            this.xoperandtype.UseSelectable = true;
            this.xoperandtype.SelectedIndexChanged += new System.EventHandler(this.xoperandtype_SelectedIndexChanged);
            // 
            // xvariablenamelabel
            // 
            this.xvariablenamelabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.xvariablenamelabel.FontWeight = MetroFramework.MetroLabelWeight.Bold;
            this.xvariablenamelabel.Location = new System.Drawing.Point(79, 0);
            this.xvariablenamelabel.Name = "xvariablenamelabel";
            this.xvariablenamelabel.Size = new System.Drawing.Size(180, 31);
            this.xvariablenamelabel.TabIndex = 3;
            this.xvariablenamelabel.Text = "Naam Variable";
            this.xvariablenamelabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // metroPanel1
            // 
            this.metroPanel1.Controls.Add(this.xvaluetypes);
            this.metroPanel1.Controls.Add(this.xvariablenamelabel);
            this.metroPanel1.Controls.Add(this.xoperandtype);
            this.metroPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroPanel1.HorizontalScrollbarBarColor = true;
            this.metroPanel1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroPanel1.HorizontalScrollbarSize = 10;
            this.metroPanel1.Location = new System.Drawing.Point(20, 60);
            this.metroPanel1.Name = "metroPanel1";
            this.metroPanel1.Size = new System.Drawing.Size(450, 31);
            this.metroPanel1.TabIndex = 5;
            this.metroPanel1.VerticalScrollbarBarColor = true;
            this.metroPanel1.VerticalScrollbarHighlightOnWheel = false;
            this.metroPanel1.VerticalScrollbarSize = 10;
            // 
            // xvaluetypes
            // 
            this.xvaluetypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xvaluetypes.FormattingEnabled = true;
            this.xvaluetypes.ItemHeight = 23;
            this.xvaluetypes.Items.AddRange(new object[] {
            "ALS",
            "EN",
            "OF"});
            this.xvaluetypes.Location = new System.Drawing.Point(259, 0);
            this.xvaluetypes.Name = "xvaluetypes";
            this.xvaluetypes.Size = new System.Drawing.Size(191, 29);
            this.xvaluetypes.TabIndex = 5;
            this.xvaluetypes.UseSelectable = true;
            this.xvaluetypes.SelectedIndexChanged += new System.EventHandler(this.xvaluetypes_SelectedIndexChanged);
            // 
            // xtextvalue
            // 
            // 
            // 
            // 
            this.xtextvalue.CustomButton.Image = null;
            this.xtextvalue.CustomButton.Location = new System.Drawing.Point(422, 1);
            this.xtextvalue.CustomButton.Name = "";
            this.xtextvalue.CustomButton.Size = new System.Drawing.Size(27, 27);
            this.xtextvalue.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xtextvalue.CustomButton.TabIndex = 1;
            this.xtextvalue.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xtextvalue.CustomButton.UseSelectable = true;
            this.xtextvalue.CustomButton.Visible = false;
            this.xtextvalue.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtextvalue.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xtextvalue.ForeColor = System.Drawing.Color.Gray;
            this.xtextvalue.Lines = new string[] {
        "Vul in een criteria..."};
            this.xtextvalue.Location = new System.Drawing.Point(0, 29);
            this.xtextvalue.MaxLength = 32767;
            this.xtextvalue.Name = "xtextvalue";
            this.xtextvalue.PasswordChar = '\0';
            this.xtextvalue.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xtextvalue.SelectedText = "";
            this.xtextvalue.SelectionLength = 0;
            this.xtextvalue.SelectionStart = 0;
            this.xtextvalue.ShortcutsEnabled = true;
            this.xtextvalue.ShowClearButton = true;
            this.xtextvalue.Size = new System.Drawing.Size(450, 29);
            this.xtextvalue.TabIndex = 6;
            this.xtextvalue.Text = "Vul in een criteria...";
            this.xtextvalue.UseSelectable = true;
            this.xtextvalue.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xtextvalue.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xtextvalue.TextChanged += new System.EventHandler(this.xtextvalue_TextChanged);
            this.xtextvalue.Enter += new System.EventHandler(this.xtextvalue_Enter);
            this.xtextvalue.Leave += new System.EventHandler(this.xtextvalue_Leave);
            // 
            // xvaluepanel
            // 
            this.xvaluepanel.Controls.Add(this.xcheckvalue);
            this.xvaluepanel.Controls.Add(this.xdecimalvalue);
            this.xvaluepanel.Controls.Add(this.xdatevalue);
            this.xvaluepanel.Controls.Add(this.xtextvalue);
            this.xvaluepanel.Controls.Add(this.xcombovalue);
            this.xvaluepanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xvaluepanel.HorizontalScrollbarBarColor = true;
            this.xvaluepanel.HorizontalScrollbarHighlightOnWheel = false;
            this.xvaluepanel.HorizontalScrollbarSize = 10;
            this.xvaluepanel.Location = new System.Drawing.Point(20, 91);
            this.xvaluepanel.Name = "xvaluepanel";
            this.xvaluepanel.Size = new System.Drawing.Size(450, 151);
            this.xvaluepanel.TabIndex = 7;
            this.xvaluepanel.VerticalScrollbarBarColor = true;
            this.xvaluepanel.VerticalScrollbarHighlightOnWheel = false;
            this.xvaluepanel.VerticalScrollbarSize = 10;
            // 
            // xcheckvalue
            // 
            this.xcheckvalue.AutoSize = true;
            this.xcheckvalue.Dock = System.Windows.Forms.DockStyle.Top;
            this.xcheckvalue.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xcheckvalue.Location = new System.Drawing.Point(0, 114);
            this.xcheckvalue.Name = "xcheckvalue";
            this.xcheckvalue.Size = new System.Drawing.Size(450, 19);
            this.xcheckvalue.TabIndex = 10;
            this.xcheckvalue.Text = "NIET WAAR";
            this.xcheckvalue.UseSelectable = true;
            this.xcheckvalue.CheckedChanged += new System.EventHandler(this.xcheckvalue_CheckedChanged);
            // 
            // xdecimalvalue
            // 
            this.xdecimalvalue.Dock = System.Windows.Forms.DockStyle.Top;
            this.xdecimalvalue.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdecimalvalue.Location = new System.Drawing.Point(0, 87);
            this.xdecimalvalue.Maximum = new decimal(new int[] {
            1410065407,
            2,
            0,
            0});
            this.xdecimalvalue.Name = "xdecimalvalue";
            this.xdecimalvalue.Size = new System.Drawing.Size(450, 27);
            this.xdecimalvalue.TabIndex = 9;
            this.xdecimalvalue.ValueChanged += new System.EventHandler(this.xdecimalvalue_ValueChanged);
            // 
            // xdatevalue
            // 
            this.xdatevalue.Dock = System.Windows.Forms.DockStyle.Top;
            this.xdatevalue.Location = new System.Drawing.Point(0, 58);
            this.xdatevalue.MinimumSize = new System.Drawing.Size(4, 29);
            this.xdatevalue.Name = "xdatevalue";
            this.xdatevalue.Size = new System.Drawing.Size(450, 29);
            this.xdatevalue.TabIndex = 8;
            this.xdatevalue.ValueChanged += new System.EventHandler(this.xdatevalue_ValueChanged);
            // 
            // xcombovalue
            // 
            this.xcombovalue.Dock = System.Windows.Forms.DockStyle.Top;
            this.xcombovalue.FormattingEnabled = true;
            this.xcombovalue.ItemHeight = 23;
            this.xcombovalue.Location = new System.Drawing.Point(0, 0);
            this.xcombovalue.Name = "xcombovalue";
            this.xcombovalue.Size = new System.Drawing.Size(450, 29);
            this.xcombovalue.TabIndex = 7;
            this.xcombovalue.UseSelectable = true;
            this.xcombovalue.SelectedIndexChanged += new System.EventHandler(this.xcombovalue_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xcriteriahtml);
            this.panel1.Controls.Add(this.xvoorwaardenb);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 242);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(450, 95);
            this.panel1.TabIndex = 9;
            // 
            // xcriteriahtml
            // 
            this.xcriteriahtml.AutoScroll = true;
            this.xcriteriahtml.BackColor = System.Drawing.SystemColors.Window;
            this.xcriteriahtml.BaseStylesheet = null;
            this.xcriteriahtml.Cursor = System.Windows.Forms.Cursors.Default;
            this.xcriteriahtml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xcriteriahtml.Location = new System.Drawing.Point(0, 42);
            this.xcriteriahtml.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xcriteriahtml.Name = "xcriteriahtml";
            this.xcriteriahtml.Size = new System.Drawing.Size(450, 53);
            this.xcriteriahtml.TabIndex = 8;
            this.xcriteriahtml.Text = null;
            // 
            // xvoorwaardenb
            // 
            this.xvoorwaardenb.Dock = System.Windows.Forms.DockStyle.Top;
            this.xvoorwaardenb.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.xvoorwaardenb.FlatAppearance.BorderSize = 2;
            this.xvoorwaardenb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xvoorwaardenb.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvoorwaardenb.Image = global::ProductieManager.Properties.Resources.code_html_link_share_icon_123633;
            this.xvoorwaardenb.ImageAlign = System.Drawing.ContentAlignment.BottomRight;
            this.xvoorwaardenb.Location = new System.Drawing.Point(0, 0);
            this.xvoorwaardenb.Name = "xvoorwaardenb";
            this.xvoorwaardenb.Size = new System.Drawing.Size(450, 42);
            this.xvoorwaardenb.TabIndex = 9;
            this.xvoorwaardenb.Text = "Voorwaarden";
            this.xvoorwaardenb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xvoorwaardenb.UseVisualStyleBackColor = true;
            this.xvoorwaardenb.Click += new System.EventHandler(this.xvoorwaardenb_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xok);
            this.panel2.Controls.Add(this.xannuleren);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 337);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(450, 43);
            this.panel2.TabIndex = 10;
            // 
            // NewFilterEntry
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(490, 400);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.xvaluepanel);
            this.Controls.Add(this.metroPanel1);
            this.Controls.Add(this.panel2);
            this.MinimumSize = new System.Drawing.Size(490, 400);
            this.Name = "NewFilterEntry";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Nieuwe Filter Regel(s)";
            this.metroPanel1.ResumeLayout(false);
            this.xvaluepanel.ResumeLayout(false);
            this.xvaluepanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xdecimalvalue)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroButton xannuleren;
        private MetroFramework.Controls.MetroButton xok;
        private MetroFramework.Controls.MetroComboBox xoperandtype;
        private MetroFramework.Controls.MetroLabel xvariablenamelabel;
        private MetroFramework.Controls.MetroPanel metroPanel1;
        private MetroFramework.Controls.MetroComboBox xvaluetypes;
        private MetroFramework.Controls.MetroTextBox xtextvalue;
        private MetroFramework.Controls.MetroPanel xvaluepanel;
        private System.Windows.Forms.NumericUpDown xdecimalvalue;
        private MetroFramework.Controls.MetroDateTime xdatevalue;
        private MetroFramework.Controls.MetroComboBox xcombovalue;
        private MetroFramework.Controls.MetroCheckBox xcheckvalue;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private HtmlRenderer.HtmlPanel xcriteriahtml;
        private System.Windows.Forms.Button xvoorwaardenb;
    }
}