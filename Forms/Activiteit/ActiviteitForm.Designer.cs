
namespace Forms.Activiteit
{
    partial class ActiviteitForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ActiviteitForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xvandaag = new MetroFramework.Controls.MetroCheckBox();
            this.button1 = new System.Windows.Forms.Button();
            this.xActiviteitList = new Controls.CustomObjectListview();
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.xsearch = new MetroFramework.Controls.MetroTextBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xActiviteitList)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xvandaag);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 344);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 40);
            this.panel1.TabIndex = 0;
            // 
            // xvandaag
            // 
            this.xvandaag.AutoSize = true;
            this.xvandaag.Checked = true;
            this.xvandaag.CheckState = System.Windows.Forms.CheckState.Checked;
            this.xvandaag.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xvandaag.Location = new System.Drawing.Point(3, 8);
            this.xvandaag.Name = "xvandaag";
            this.xvandaag.Size = new System.Drawing.Size(119, 19);
            this.xvandaag.Style = MetroFramework.MetroColorStyle.Blue;
            this.xvandaag.TabIndex = 1;
            this.xvandaag.Text = "Alleen Vandaag";
            this.xvandaag.UseSelectable = true;
            this.xvandaag.CheckedChanged += new System.EventHandler(this.xvandaag_CheckedChanged);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(507, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(90, 34);
            this.button1.TabIndex = 0;
            this.button1.Text = "Sluiten";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // xActiviteitList
            // 
            this.xActiviteitList.AllColumns.Add(this.olvColumn2);
            this.xActiviteitList.AllColumns.Add(this.olvColumn1);
            this.xActiviteitList.AllColumns.Add(this.olvColumn3);
            this.xActiviteitList.AllowCellEdit = false;
            this.xActiviteitList.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.xActiviteitList.CellEditUseWholeCell = false;
            this.xActiviteitList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn2,
            this.olvColumn1,
            this.olvColumn3});
            this.xActiviteitList.Cursor = System.Windows.Forms.Cursors.Default;
            this.xActiviteitList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xActiviteitList.EmptyListMsg = "Geen Activiteiten...";
            this.xActiviteitList.EmptyListMsgFont = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xActiviteitList.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xActiviteitList.FullRowSelect = true;
            this.xActiviteitList.HeaderWordWrap = true;
            this.xActiviteitList.HideSelection = false;
            this.xActiviteitList.LargeImageList = this.imageList1;
            this.xActiviteitList.Location = new System.Drawing.Point(20, 83);
            this.xActiviteitList.MenuLabelColumns = "kolommen";
            this.xActiviteitList.MenuLabelGroupBy = "Groeperen op \'{0}\'";
            this.xActiviteitList.MenuLabelLockGroupingOn = "Groepering vergrendelen op \'{0}\'";
            this.xActiviteitList.MenuLabelSelectColumns = "Selecteer kolommen...";
            this.xActiviteitList.MenuLabelSortAscending = "Sorteer oplopend op \'{0}\'";
            this.xActiviteitList.MenuLabelSortDescending = "Aflopend sorteren op \'{0}\'";
            this.xActiviteitList.MenuLabelTurnOffGroups = "Groepen uitschakelen";
            this.xActiviteitList.MenuLabelUnlockGroupingOn = "Ontgrendel groeperen van \'{0}\'";
            this.xActiviteitList.MenuLabelUnsort = "Uitsorteren";
            this.xActiviteitList.Name = "xActiviteitList";
            this.xActiviteitList.OverlayText.Text = "";
            this.xActiviteitList.OverlayText.TextColor = System.Drawing.Color.Navy;
            this.xActiviteitList.OwnerDraw = false;
            this.xActiviteitList.ShowCommandMenuOnRightClick = true;
            this.xActiviteitList.ShowGroups = false;
            this.xActiviteitList.ShowItemCountOnGroups = true;
            this.xActiviteitList.ShowItemToolTips = true;
            this.xActiviteitList.Size = new System.Drawing.Size(600, 261);
            this.xActiviteitList.SmallImageList = this.imageList1;
            this.xActiviteitList.SpaceBetweenGroups = 10;
            this.xActiviteitList.TabIndex = 26;
            this.xActiviteitList.TileSize = new System.Drawing.Size(300, 120);
            this.xActiviteitList.UseAlternatingBackColors = true;
            this.xActiviteitList.UseCompatibleStateImageBehavior = false;
            this.xActiviteitList.UseExplorerTheme = true;
            this.xActiviteitList.UseFiltering = true;
            this.xActiviteitList.UseHotControls = false;
            this.xActiviteitList.UseHotItem = true;
            this.xActiviteitList.UseOverlays = false;
            this.xActiviteitList.UseTranslucentHotItem = true;
            this.xActiviteitList.UseTranslucentSelection = true;
            this.xActiviteitList.View = System.Windows.Forms.View.Details;
            this.xActiviteitList.DoubleClick += new System.EventHandler(this.xActiviteitList_DoubleClick);
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "TimeChanged";
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Activiteit Op";
            this.olvColumn2.Width = 162;
            this.olvColumn2.WordWrap = true;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "User";
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Gebruiker";
            this.olvColumn1.Width = 120;
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "Change";
            this.olvColumn3.FillsFreeSpace = true;
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.Text = "Activiteit Log";
            this.olvColumn3.Width = 250;
            this.olvColumn3.WordWrap = true;
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(32, 32);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xsearch
            // 
            // 
            // 
            // 
            this.xsearch.CustomButton.Image = null;
            this.xsearch.CustomButton.Location = new System.Drawing.Point(578, 1);
            this.xsearch.CustomButton.Name = "";
            this.xsearch.CustomButton.Size = new System.Drawing.Size(21, 21);
            this.xsearch.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xsearch.CustomButton.TabIndex = 1;
            this.xsearch.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xsearch.CustomButton.UseSelectable = true;
            this.xsearch.CustomButton.Visible = false;
            this.xsearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.xsearch.Lines = new string[0];
            this.xsearch.Location = new System.Drawing.Point(20, 60);
            this.xsearch.MaxLength = 32767;
            this.xsearch.Name = "xsearch";
            this.xsearch.PasswordChar = '\0';
            this.xsearch.PromptText = "Zoeken...";
            this.xsearch.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.xsearch.SelectedText = "";
            this.xsearch.SelectionLength = 0;
            this.xsearch.SelectionStart = 0;
            this.xsearch.ShortcutsEnabled = true;
            this.xsearch.ShowClearButton = true;
            this.xsearch.Size = new System.Drawing.Size(600, 23);
            this.xsearch.Style = MetroFramework.MetroColorStyle.Blue;
            this.xsearch.TabIndex = 27;
            this.xsearch.UseSelectable = true;
            this.xsearch.WaterMark = "Zoeken...";
            this.xsearch.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xsearch.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            this.xsearch.TextChanged += new System.EventHandler(this.xsearch_TextChanged);
            // 
            // ActiviteitForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(640, 404);
            this.Controls.Add(this.xActiviteitList);
            this.Controls.Add(this.xsearch);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ActiviteitForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Activiteiten";
            this.Title = "Activiteiten";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ActiviteitForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xActiviteitList)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private Controls.CustomObjectListview xActiviteitList;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.Windows.Forms.ImageList imageList1;
        private MetroFramework.Controls.MetroTextBox xsearch;
        private MetroFramework.Controls.MetroCheckBox xvandaag;
    }
}