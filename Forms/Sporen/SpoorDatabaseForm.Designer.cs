namespace Forms.Sporen
{
    partial class SpoorDatabaseForm
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
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xbuisdiametersGroup = new System.Windows.Forms.GroupBox();
            this.xdiameters = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.panel1.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.xbuisdiametersGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xdiameters)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xok);
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 540);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(883, 41);
            this.panel1.TabIndex = 0;
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Image = global::ProductieManager.Properties.Resources.diskette_save_saveas_1514;
            this.xok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xok.Location = new System.Drawing.Point(659, 3);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(110, 37);
            this.xok.TabIndex = 12;
            this.xok.TabStop = false;
            this.xok.Text = "OK";
            this.xok.UseVisualStyleBackColor = true;
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(775, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(105, 37);
            this.xsluiten.TabIndex = 13;
            this.xsluiten.TabStop = false;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(20, 60);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(883, 480);
            this.metroTabControl1.TabIndex = 1;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.panel2);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Padding = new System.Windows.Forms.Padding(5);
            this.metroTabPage1.Size = new System.Drawing.Size(875, 438);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Opdrukkers";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // panel2
            // 
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.xbuisdiametersGroup);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(5, 5);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(865, 428);
            this.panel2.TabIndex = 3;
            // 
            // xbuisdiametersGroup
            // 
            this.xbuisdiametersGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.xbuisdiametersGroup.BackColor = System.Drawing.Color.Transparent;
            this.xbuisdiametersGroup.Controls.Add(this.xdiameters);
            this.xbuisdiametersGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbuisdiametersGroup.ForeColor = System.Drawing.Color.Navy;
            this.xbuisdiametersGroup.Location = new System.Drawing.Point(3, 3);
            this.xbuisdiametersGroup.Name = "xbuisdiametersGroup";
            this.xbuisdiametersGroup.Size = new System.Drawing.Size(326, 422);
            this.xbuisdiametersGroup.TabIndex = 2;
            this.xbuisdiametersGroup.TabStop = false;
            this.xbuisdiametersGroup.Text = "Buis Diameters";
            // 
            // xdiameters
            // 
            this.xdiameters.AllColumns.Add(this.olvColumn1);
            this.xdiameters.AllColumns.Add(this.olvColumn2);
            this.xdiameters.CellEditActivation = BrightIdeasSoftware.ObjectListView.CellEditActivateMode.DoubleClick;
            this.xdiameters.CellEditEnterChangesRows = true;
            this.xdiameters.CellEditUseWholeCell = false;
            this.xdiameters.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.xdiameters.Cursor = System.Windows.Forms.Cursors.Default;
            this.xdiameters.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xdiameters.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xdiameters.FullRowSelect = true;
            this.xdiameters.GridLines = true;
            this.xdiameters.HideSelection = false;
            this.xdiameters.Location = new System.Drawing.Point(3, 21);
            this.xdiameters.Name = "xdiameters";
            this.xdiameters.ShowGroups = false;
            this.xdiameters.ShowItemToolTips = true;
            this.xdiameters.Size = new System.Drawing.Size(320, 398);
            this.xdiameters.TabIndex = 0;
            this.xdiameters.UseCompatibleStateImageBehavior = false;
            this.xdiameters.UseExplorerTheme = true;
            this.xdiameters.UseHotItem = true;
            this.xdiameters.UseTranslucentHotItem = true;
            this.xdiameters.UseTranslucentSelection = true;
            this.xdiameters.View = System.Windows.Forms.View.Details;
            this.xdiameters.CellEditFinished += new BrightIdeasSoftware.CellEditEventHandler(this.xdiameters_CellEditFinished);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Key";
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Code";
            this.olvColumn1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Value";
            this.olvColumn2.FillsFreeSpace = true;
            this.olvColumn2.Text = "Diameters";
            this.olvColumn2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.olvColumn2.Width = 100;
            // 
            // SpoorDatabaseForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(923, 601);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "SpoorDatabaseForm";
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Database";
            this.panel1.ResumeLayout(false);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.xbuisdiametersGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xdiameters)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xok;
        private System.Windows.Forms.Button xsluiten;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox xbuisdiametersGroup;
        private BrightIdeasSoftware.ObjectListView xdiameters;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
    }
}