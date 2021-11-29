
using TheArtOfDev.HtmlRenderer.WinForms;

namespace Forms
{
    partial class MateriaalVerbruikForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MateriaalVerbruikForm));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xstop = new System.Windows.Forms.DateTimePicker();
            this.xloadmaterialen = new MetroFramework.Controls.MetroButton();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xstart = new System.Windows.Forms.DateTimePicker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xloadinglabel = new System.Windows.Forms.Label();
            this.xmateriaalList = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn5 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn4 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn6 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn7 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel3 = new System.Windows.Forms.Panel();
            this.xsearchbox = new MetroFramework.Controls.MetroTextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xstatuslabel = new HtmlPanel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.groupBox1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xmateriaalList)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.xstop);
            this.groupBox1.Controls.Add(this.xloadmaterialen);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.xstart);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(849, 50);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Verbruik Bereik";
            // 
            // xstop
            // 
            this.xstop.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstop.Location = new System.Drawing.Point(336, 19);
            this.xstop.Name = "xstop";
            this.xstop.Size = new System.Drawing.Size(232, 25);
            this.xstop.TabIndex = 2;
            this.metroToolTip1.SetToolTip(this.xstop, "Bereik eind datum");
            // 
            // xloadmaterialen
            // 
            this.xloadmaterialen.Location = new System.Drawing.Point(574, 19);
            this.xloadmaterialen.Name = "xloadmaterialen";
            this.xloadmaterialen.Size = new System.Drawing.Size(99, 25);
            this.xloadmaterialen.TabIndex = 4;
            this.xloadmaterialen.Text = "Laad Materialen";
            this.metroToolTip1.SetToolTip(this.xloadmaterialen, "Laad materialen volgens het aangegeven bereik");
            this.xloadmaterialen.UseSelectable = true;
            this.xloadmaterialen.Click += new System.EventHandler(this.xloadmaterialen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(297, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "t/m: ";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vanaf: ";
            // 
            // xstart
            // 
            this.xstart.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstart.Location = new System.Drawing.Point(59, 19);
            this.xstart.Name = "xstart";
            this.xstart.Size = new System.Drawing.Size(232, 25);
            this.xstart.TabIndex = 0;
            this.metroToolTip1.SetToolTip(this.xstart, "Bereik start datum");
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xloadinglabel);
            this.panel1.Controls.Add(this.xmateriaalList);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(10, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(849, 460);
            this.panel1.TabIndex = 2;
            // 
            // xloadinglabel
            // 
            this.xloadinglabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xloadinglabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xloadinglabel.Location = new System.Drawing.Point(4, 87);
            this.xloadinglabel.Name = "xloadinglabel";
            this.xloadinglabel.Size = new System.Drawing.Size(845, 370);
            this.xloadinglabel.TabIndex = 3;
            this.xloadinglabel.Text = "Materialen Laden...";
            this.xloadinglabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xloadinglabel.Visible = false;
            // 
            // xmateriaalList
            // 
            this.xmateriaalList.AllColumns.Add(this.olvColumn1);
            this.xmateriaalList.AllColumns.Add(this.olvColumn5);
            this.xmateriaalList.AllColumns.Add(this.olvColumn2);
            this.xmateriaalList.AllColumns.Add(this.olvColumn3);
            this.xmateriaalList.AllColumns.Add(this.olvColumn4);
            this.xmateriaalList.AllColumns.Add(this.olvColumn6);
            this.xmateriaalList.AllColumns.Add(this.olvColumn7);
            this.xmateriaalList.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.xmateriaalList.CellEditUseWholeCell = false;
            this.xmateriaalList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn5,
            this.olvColumn2,
            this.olvColumn3,
            this.olvColumn4,
            this.olvColumn6,
            this.olvColumn7});
            this.xmateriaalList.Cursor = System.Windows.Forms.Cursors.Default;
            this.xmateriaalList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmateriaalList.FullRowSelect = true;
            this.xmateriaalList.HideSelection = false;
            this.xmateriaalList.Location = new System.Drawing.Point(0, 84);
            this.xmateriaalList.Name = "xmateriaalList";
            this.xmateriaalList.ShowGroups = false;
            this.xmateriaalList.ShowItemToolTips = true;
            this.xmateriaalList.Size = new System.Drawing.Size(849, 376);
            this.xmateriaalList.SpaceBetweenGroups = 10;
            this.xmateriaalList.TabIndex = 3;
            this.xmateriaalList.TintSortColumn = true;
            this.xmateriaalList.UseAlternatingBackColors = true;
            this.xmateriaalList.UseCompatibleStateImageBehavior = false;
            this.xmateriaalList.UseExplorerTheme = true;
            this.xmateriaalList.UseFiltering = true;
            this.xmateriaalList.UseHotItem = true;
            this.xmateriaalList.UseTranslucentHotItem = true;
            this.xmateriaalList.UseTranslucentSelection = true;
            this.xmateriaalList.View = System.Windows.Forms.View.Details;
            this.xmateriaalList.FormatCell += new System.EventHandler<BrightIdeasSoftware.FormatCellEventArgs>(this.xmateriaalList_FormatCell);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "ArtikelNr";
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "ArtikelNr";
            this.olvColumn1.ToolTipText = "Materiaal artikel nummer";
            this.olvColumn1.Width = 150;
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn5
            // 
            this.olvColumn5.AspectName = "Omschrijving";
            this.olvColumn5.IsEditable = false;
            this.olvColumn5.Text = "Omschrijving";
            this.olvColumn5.ToolTipText = "Materiaal omschrijving";
            this.olvColumn5.Width = 250;
            this.olvColumn5.WordWrap = true;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Verbruik";
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Verbruik";
            this.olvColumn2.ToolTipText = "Totaal verbruik";
            this.olvColumn2.Width = 80;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Afkeur";
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.Text = "Afkeur";
            this.olvColumn3.ToolTipText = "Totaal aantal afkeur";
            this.olvColumn3.Width = 80;
            // 
            // olvColumn4
            // 
            this.olvColumn4.AspectName = "PerEenHeid";
            this.olvColumn4.IsEditable = false;
            this.olvColumn4.Text = "Gemiddeld P/Eenheid";
            this.olvColumn4.ToolTipText = "Gemiddeld verbruik per eenheid";
            this.olvColumn4.Width = 120;
            this.olvColumn4.WordWrap = true;
            // 
            // olvColumn6
            // 
            this.olvColumn6.AspectName = "AantalProducties";
            this.olvColumn6.IsEditable = false;
            this.olvColumn6.Text = "Aantal Gebruikt";
            this.olvColumn6.ToolTipText = "Aantal keer gebruikt in producties";
            this.olvColumn6.Width = 100;
            this.olvColumn6.WordWrap = true;
            // 
            // olvColumn7
            // 
            this.olvColumn7.AspectName = "Eenheid";
            this.olvColumn7.IsEditable = false;
            this.olvColumn7.Text = "Eenheid";
            this.olvColumn7.ToolTipText = "Materiaal eenheid";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xsearchbox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 50);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(849, 34);
            this.panel3.TabIndex = 2;
            // 
            // xsearchbox
            // 
            this.xsearchbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.xsearchbox.CustomButton.Image = null;
            this.xsearchbox.CustomButton.Location = new System.Drawing.Point(819, 2);
            this.xsearchbox.CustomButton.Name = "";
            this.xsearchbox.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xsearchbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xsearchbox.CustomButton.TabIndex = 1;
            this.xsearchbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xsearchbox.CustomButton.UseSelectable = true;
            this.xsearchbox.CustomButton.Visible = false;
            this.xsearchbox.DisplayIcon = true;
            this.xsearchbox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xsearchbox.Lines = new string[] {
        "Zoeken..."};
            this.xsearchbox.Location = new System.Drawing.Point(3, 4);
            this.xsearchbox.MaxLength = 32767;
            this.xsearchbox.Name = "xsearchbox";
            this.xsearchbox.PasswordChar = '\0';
            this.xsearchbox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xsearchbox.SelectedText = "";
            this.xsearchbox.SelectionLength = 0;
            this.xsearchbox.SelectionStart = 0;
            this.xsearchbox.ShortcutsEnabled = true;
            this.xsearchbox.ShowClearButton = true;
            this.xsearchbox.Size = new System.Drawing.Size(843, 26);
            this.xsearchbox.TabIndex = 0;
            this.xsearchbox.Text = "Zoeken...";
            this.metroToolTip1.SetToolTip(this.xsearchbox, "Vul een artikel nummer dat je wilt vinden.\r\nonderscheid meerdere zoektermen met e" +
        "en \';\'");
            this.xsearchbox.UseSelectable = true;
            this.xsearchbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xsearchbox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xsearchbox.TextChanged += new System.EventHandler(this.xsearchbox_TextChanged);
            this.xsearchbox.Enter += new System.EventHandler(this.xsearchbox_Enter);
            this.xsearchbox.Leave += new System.EventHandler(this.xsearchbox_Leave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xstatuslabel);
            this.panel2.Controls.Add(this.xsluiten);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(10, 520);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(849, 63);
            this.panel2.TabIndex = 3;
            // 
            // xstatuslabel
            // 
            this.xstatuslabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xstatuslabel.AutoScroll = true;
            this.xstatuslabel.BackColor = System.Drawing.SystemColors.Window;
            this.xstatuslabel.BaseStylesheet = null;
            this.xstatuslabel.Cursor = System.Windows.Forms.Cursors.Default;
            this.xstatuslabel.Location = new System.Drawing.Point(3, 3);
            this.xstatuslabel.Name = "xstatuslabel";
            this.xstatuslabel.Size = new System.Drawing.Size(702, 57);
            this.xstatuslabel.TabIndex = 13;
            this.xstatuslabel.Text = null;
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(711, 23);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(135, 37);
            this.xsluiten.TabIndex = 12;
            this.xsluiten.Text = "&Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.metroToolTip1.SetToolTip(this.xsluiten, "Sluit materiaal overzicht");
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Default;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Default;
            // 
            // MateriaalVerbruikForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(869, 593);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(725, 465);
            this.Name = "MateriaalVerbruikForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Materiaal Verbruik";
            this.Shown += new System.EventHandler(this.MateriaalVerbruikForm_Shown);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xmateriaalList)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.DateTimePicker xstop;
        private MetroFramework.Controls.MetroButton xloadmaterialen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker xstart;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label xloadinglabel;
        private System.Windows.Forms.Panel panel3;
        private MetroFramework.Controls.MetroTextBox xsearchbox;
        private System.Windows.Forms.Panel panel2;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.Button xsluiten;
        private BrightIdeasSoftware.ObjectListView xmateriaalList;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn4;
        private BrightIdeasSoftware.OLVColumn olvColumn5;
        private BrightIdeasSoftware.OLVColumn olvColumn6;
        private BrightIdeasSoftware.OLVColumn olvColumn7;
        private HtmlPanel xstatuslabel;
    }
}