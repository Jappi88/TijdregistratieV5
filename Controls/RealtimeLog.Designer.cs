
namespace Controls
{
    partial class RealtimeLog
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
            this.xomschrijving = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xlogview = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn0 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel3 = new System.Windows.Forms.Panel();
            this.xsearchbox = new System.Windows.Forms.TextBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xstatuslabel = new System.Windows.Forms.Label();
            this.xclosepanel = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xclearbutton = new System.Windows.Forms.Button();
            this.xenddate = new System.Windows.Forms.DateTimePicker();
            this.xstartdate = new System.Windows.Forms.DateTimePicker();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xlogview)).BeginInit();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.xclosepanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.SuspendLayout();
            // 
            // xomschrijving
            // 
            this.xomschrijving.Dock = System.Windows.Forms.DockStyle.Top;
            this.xomschrijving.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xomschrijving.Location = new System.Drawing.Point(0, 0);
            this.xomschrijving.Name = "xomschrijving";
            this.xomschrijving.Size = new System.Drawing.Size(939, 51);
            this.xomschrijving.TabIndex = 0;
            this.xomschrijving.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xlogview);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 87);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 492);
            this.panel1.TabIndex = 1;
            // 
            // xlogview
            // 
            this.xlogview.AllColumns.Add(this.olvColumn0);
            this.xlogview.AllColumns.Add(this.olvColumn1);
            this.xlogview.AllColumns.Add(this.olvColumn2);
            this.xlogview.AllColumns.Add(this.olvColumn3);
            this.xlogview.AllowColumnReorder = true;
            this.xlogview.CellEditUseWholeCell = false;
            this.xlogview.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn0,
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3});
            this.xlogview.Cursor = System.Windows.Forms.Cursors.Default;
            this.xlogview.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xlogview.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xlogview.FullRowSelect = true;
            this.xlogview.HideSelection = false;
            this.xlogview.LargeImageList = this.imageList1;
            this.xlogview.Location = new System.Drawing.Point(0, 32);
            this.xlogview.Name = "xlogview";
            this.xlogview.Size = new System.Drawing.Size(939, 416);
            this.xlogview.SmallImageList = this.imageList1;
            this.xlogview.SpaceBetweenGroups = 5;
            this.xlogview.TabIndex = 1;
            this.xlogview.UseCompatibleStateImageBehavior = false;
            this.xlogview.UseExplorerTheme = true;
            this.xlogview.UseFilterIndicator = true;
            this.xlogview.UseFiltering = true;
            this.xlogview.UseHotItem = true;
            this.xlogview.UseTranslucentHotItem = true;
            this.xlogview.View = System.Windows.Forms.View.Details;
            this.xlogview.CellToolTipShowing += new System.EventHandler<BrightIdeasSoftware.ToolTipShowingEventArgs>(this.xlogview_CellToolTipShowing);
            this.xlogview.SelectedIndexChanged += new System.EventHandler(this.xskillview_SelectionChanged);
            // 
            // olvColumn0
            // 
            this.olvColumn0.AspectName = "Added";
            this.olvColumn0.AspectToStringFormat = "[{0}]";
            this.olvColumn0.Groupable = false;
            this.olvColumn0.Text = "Toegevoegd";
            this.olvColumn0.ToolTipText = "Datum en tijd toegevoegd.";
            this.olvColumn0.Width = 200;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Type";
            this.olvColumn1.AspectToStringFormat = "[{0}]";
            this.olvColumn1.Text = "Type";
            this.olvColumn1.ToolTipText = "Type";
            this.olvColumn1.Width = 75;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Username";
            this.olvColumn2.Text = "Gebruiker";
            this.olvColumn2.ToolTipText = "Gebruiker";
            this.olvColumn2.Width = 150;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Message";
            this.olvColumn3.FillsFreeSpace = true;
            this.olvColumn3.Text = "Log";
            this.olvColumn3.ToolTipText = "Bericht log";
            this.olvColumn3.Width = 350;
            this.olvColumn3.WordWrap = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(36, 36);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xsearchbox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(939, 32);
            this.panel3.TabIndex = 3;
            // 
            // xsearchbox
            // 
            this.xsearchbox.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xsearchbox.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsearchbox.Location = new System.Drawing.Point(0, 3);
            this.xsearchbox.Name = "xsearchbox";
            this.xsearchbox.Size = new System.Drawing.Size(939, 29);
            this.xsearchbox.TabIndex = 0;
            this.xsearchbox.Text = "Zoeken...";
            this.toolTip1.SetToolTip(this.xsearchbox, "Zoek naar een log");
            this.xsearchbox.TextChanged += new System.EventHandler(this.xsearchbox_TextChanged);
            this.xsearchbox.Enter += new System.EventHandler(this.xsearch_Enter);
            this.xsearchbox.Leave += new System.EventHandler(this.xsearch_Leave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xstatuslabel);
            this.panel2.Controls.Add(this.xclosepanel);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 448);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(939, 44);
            this.panel2.TabIndex = 2;
            // 
            // xstatuslabel
            // 
            this.xstatuslabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xstatuslabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatuslabel.Location = new System.Drawing.Point(0, 0);
            this.xstatuslabel.Name = "xstatuslabel";
            this.xstatuslabel.Padding = new System.Windows.Forms.Padding(5);
            this.xstatuslabel.Size = new System.Drawing.Size(807, 44);
            this.xstatuslabel.TabIndex = 0;
            this.xstatuslabel.Text = "Laatste Log";
            this.xstatuslabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // xclosepanel
            // 
            this.xclosepanel.Controls.Add(this.xsluiten);
            this.xclosepanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.xclosepanel.Location = new System.Drawing.Point(807, 0);
            this.xclosepanel.Name = "xclosepanel";
            this.xclosepanel.Size = new System.Drawing.Size(132, 44);
            this.xclosepanel.TabIndex = 1;
            // 
            // xsluiten
            // 
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.Location = new System.Drawing.Point(3, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(125, 38);
            this.xsluiten.TabIndex = 1;
            this.xsluiten.Text = "&Sluiten";
            this.xsluiten.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xsluiten, "Sluit log window");
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Productie Logs";
            // 
            // xclearbutton
            // 
            this.xclearbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xclearbutton.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xclearbutton.Location = new System.Drawing.Point(477, 0);
            this.xclearbutton.Name = "xclearbutton";
            this.xclearbutton.Size = new System.Drawing.Size(35, 35);
            this.xclearbutton.TabIndex = 4;
            this.toolTip1.SetToolTip(this.xclearbutton, "Verwijder Logs");
            this.xclearbutton.UseVisualStyleBackColor = true;
            this.xclearbutton.Click += new System.EventHandler(this.xclearbutton_Click);
            // 
            // xenddate
            // 
            this.xenddate.CalendarFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xenddate.Checked = false;
            this.xenddate.CustomFormat = "dd-MM-yyyy HH:mm";
            this.xenddate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xenddate.Location = new System.Drawing.Point(294, 5);
            this.xenddate.Name = "xenddate";
            this.xenddate.ShowCheckBox = true;
            this.xenddate.Size = new System.Drawing.Size(177, 25);
            this.xenddate.TabIndex = 2;
            this.toolTip1.SetToolTip(this.xenddate, "Kies eind datum van de logs dat je wilt zien");
            this.xenddate.ValueChanged += new System.EventHandler(this.xenddate_ValueChanged);
            // 
            // xstartdate
            // 
            this.xstartdate.CalendarFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstartdate.Checked = false;
            this.xstartdate.CustomFormat = "dd-MM-yyyy HH:mm";
            this.xstartdate.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstartdate.Location = new System.Drawing.Point(62, 5);
            this.xstartdate.Name = "xstartdate";
            this.xstartdate.ShowCheckBox = true;
            this.xstartdate.Size = new System.Drawing.Size(177, 25);
            this.xstartdate.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xstartdate, "Kies start datum van de logs dat je wilt zien");
            this.xstartdate.ValueChanged += new System.EventHandler(this.xstartdate_ValueChanged);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.xclearbutton);
            this.panel4.Controls.Add(this.label2);
            this.panel4.Controls.Add(this.xenddate);
            this.panel4.Controls.Add(this.label1);
            this.panel4.Controls.Add(this.xstartdate);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(0, 51);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(939, 36);
            this.panel4.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(259, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Tot:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "Vanaf:";
            // 
            // RealtimeLog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.xomschrijving);
            this.DoubleBuffered = true;
            this.Name = "RealtimeLog";
            this.Size = new System.Drawing.Size(939, 579);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xlogview)).EndInit();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.xclosepanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label xomschrijving;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.TextBox xsearchbox;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label xstatuslabel;
        private System.Windows.Forms.Panel xclosepanel;
        private System.Windows.Forms.Button xsluiten;
        public BrightIdeasSoftware.ObjectListView xlogview;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel3;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private BrightIdeasSoftware.OLVColumn olvColumn0;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button xclearbutton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker xenddate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker xstartdate;
    }
}
