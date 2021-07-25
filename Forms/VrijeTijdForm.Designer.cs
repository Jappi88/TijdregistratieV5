
namespace Forms
{
    partial class VrijeTijdForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VrijeTijdForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xverwijdervrij = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xuurvrij = new System.Windows.Forms.Label();
            this.xvoegvrijtoe = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.xstartvrij = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.xeindvrij = new System.Windows.Forms.DateTimePicker();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xok = new System.Windows.Forms.Button();
            this.xannuleer = new System.Windows.Forms.Button();
            this.xstatus = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.xvrijetijdlist = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn3 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xvrijetijdlist)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(485, 66);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xverwijdervrij);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(436, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(49, 66);
            this.panel3.TabIndex = 5;
            // 
            // xverwijdervrij
            // 
            this.xverwijdervrij.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xverwijdervrij.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xverwijdervrij.Location = new System.Drawing.Point(3, 25);
            this.xverwijdervrij.Name = "xverwijdervrij";
            this.xverwijdervrij.Size = new System.Drawing.Size(34, 34);
            this.xverwijdervrij.TabIndex = 5;
            this.toolTip1.SetToolTip(this.xverwijdervrij, "Verwijder geselecteerde vrije dagen");
            this.xverwijdervrij.UseVisualStyleBackColor = true;
            this.xverwijdervrij.Click += new System.EventHandler(this.xverwijdervrij_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xuurvrij);
            this.panel2.Controls.Add(this.xvoegvrijtoe);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.xstartvrij);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.xeindvrij);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(434, 66);
            this.panel2.TabIndex = 4;
            // 
            // xuurvrij
            // 
            this.xuurvrij.AutoSize = true;
            this.xuurvrij.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xuurvrij.Location = new System.Drawing.Point(347, 4);
            this.xuurvrij.Name = "xuurvrij";
            this.xuurvrij.Size = new System.Drawing.Size(72, 17);
            this.xuurvrij.TabIndex = 5;
            this.xuurvrij.Text = "Totaal Uur";
            // 
            // xvoegvrijtoe
            // 
            this.xvoegvrijtoe.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xvoegvrijtoe.Image = global::ProductieManager.Properties.Resources.add_1588;
            this.xvoegvrijtoe.Location = new System.Drawing.Point(350, 25);
            this.xvoegvrijtoe.Name = "xvoegvrijtoe";
            this.xvoegvrijtoe.Size = new System.Drawing.Size(34, 34);
            this.xvoegvrijtoe.TabIndex = 4;
            this.toolTip1.SetToolTip(this.xvoegvrijtoe, "Voeg vrije tijd toe");
            this.xvoegvrijtoe.UseVisualStyleBackColor = true;
            this.xvoegvrijtoe.Click += new System.EventHandler(this.xvoegvrijtoe_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(56, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Eind Vrij";
            // 
            // xstartvrij
            // 
            this.xstartvrij.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xstartvrij.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xstartvrij.Location = new System.Drawing.Point(74, 3);
            this.xstartvrij.Name = "xstartvrij";
            this.xstartvrij.Size = new System.Drawing.Size(266, 25);
            this.xstartvrij.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xstartvrij, "Start vrije dag");
            this.xstartvrij.ValueChanged += new System.EventHandler(this.xstartvrij_ValueChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 2;
            this.label1.Text = "Start Vrij";
            // 
            // xeindvrij
            // 
            this.xeindvrij.CustomFormat = "dddd dd MMMM yyyy HH:mm";
            this.xeindvrij.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.xeindvrij.Location = new System.Drawing.Point(74, 34);
            this.xeindvrij.Name = "xeindvrij";
            this.xeindvrij.Size = new System.Drawing.Size(266, 25);
            this.xeindvrij.TabIndex = 1;
            this.toolTip1.SetToolTip(this.xeindvrij, "Eind vrije dag");
            this.xeindvrij.ValueChanged += new System.EventHandler(this.xeindvrij_ValueChanged);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Vrije Tijd";
            // 
            // xok
            // 
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Location = new System.Drawing.Point(3, 3);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(100, 30);
            this.xok.TabIndex = 0;
            this.xok.Text = "&OK";
            this.toolTip1.SetToolTip(this.xok, "Accepteer Wijzigingen");
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // xannuleer
            // 
            this.xannuleer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xannuleer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xannuleer.Location = new System.Drawing.Point(109, 3);
            this.xannuleer.Name = "xannuleer";
            this.xannuleer.Size = new System.Drawing.Size(100, 30);
            this.xannuleer.TabIndex = 1;
            this.xannuleer.Text = "Annuleren";
            this.toolTip1.SetToolTip(this.xannuleer, "Annuleer wijzigingen");
            this.xannuleer.UseVisualStyleBackColor = true;
            this.xannuleer.Click += new System.EventHandler(this.xannuleer_Click);
            // 
            // xstatus
            // 
            this.xstatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xstatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatus.Location = new System.Drawing.Point(0, 0);
            this.xstatus.Name = "xstatus";
            this.xstatus.Size = new System.Drawing.Size(274, 35);
            this.xstatus.TabIndex = 1;
            this.xstatus.Text = "Vrije Tijd";
            this.xstatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.xstatus, "Totaal vrije tijd");
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.xstatus);
            this.panel4.Controls.Add(this.panel5);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel4.Location = new System.Drawing.Point(20, 285);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(485, 35);
            this.panel4.TabIndex = 1;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.xannuleer);
            this.panel5.Controls.Add(this.xok);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel5.Location = new System.Drawing.Point(274, 0);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(211, 35);
            this.panel5.TabIndex = 0;
            // 
            // xvrijetijdlist
            // 
            this.xvrijetijdlist.AllColumns.Add(this.olvColumn1);
            this.xvrijetijdlist.AllColumns.Add(this.olvColumn2);
            this.xvrijetijdlist.AllColumns.Add(this.olvColumn3);
            this.xvrijetijdlist.CellEditUseWholeCell = false;
            this.xvrijetijdlist.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2,
            this.olvColumn3});
            this.xvrijetijdlist.Cursor = System.Windows.Forms.Cursors.Default;
            this.xvrijetijdlist.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xvrijetijdlist.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvrijetijdlist.FullRowSelect = true;
            this.xvrijetijdlist.HideSelection = false;
            this.xvrijetijdlist.LargeImageList = this.imageList1;
            this.xvrijetijdlist.Location = new System.Drawing.Point(20, 126);
            this.xvrijetijdlist.Name = "xvrijetijdlist";
            this.xvrijetijdlist.ShowGroups = false;
            this.xvrijetijdlist.ShowItemToolTips = true;
            this.xvrijetijdlist.Size = new System.Drawing.Size(485, 159);
            this.xvrijetijdlist.SmallImageList = this.imageList1;
            this.xvrijetijdlist.TabIndex = 2;
            this.xvrijetijdlist.UseCompatibleStateImageBehavior = false;
            this.xvrijetijdlist.UseExplorerTheme = true;
            this.xvrijetijdlist.UseHotItem = true;
            this.xvrijetijdlist.UseTranslucentHotItem = true;
            this.xvrijetijdlist.View = System.Windows.Forms.View.Details;
            this.xvrijetijdlist.SelectedIndexChanged += new System.EventHandler(this.xvrijetijdlist_SelectedIndexChanged);
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Start";
            this.olvColumn1.IsEditable = false;
            this.olvColumn1.Text = "Start Tijd";
            this.olvColumn1.ToolTipText = "Start Datum";
            this.olvColumn1.Width = 230;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Stop";
            this.olvColumn2.IsEditable = false;
            this.olvColumn2.Text = "Eind Tijd";
            this.olvColumn2.ToolTipText = "Eind Tijd";
            this.olvColumn2.Width = 230;
            // 
            // olvColumn3
            // 
            this.olvColumn3.AspectName = "TotaalTijd";
            this.olvColumn3.IsEditable = false;
            this.olvColumn3.Text = "Totaal Uur";
            this.olvColumn3.ToolTipText = "Totaal uur vrij";
            this.olvColumn3.Width = 110;
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "business-color_progress_icon-icons.com_53437.png");
            // 
            // VrijeTijdForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(525, 340);
            this.Controls.Add(this.xvrijetijdlist);
            this.Controls.Add(this.panel4);
            this.Controls.Add(this.panel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(525, 340);
            this.Name = "VrijeTijdForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vrije Tijd Beheren";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.VrijeTijdForm_FormClosing);
            this.Shown += new System.EventHandler(this.VrijeTijdForm_Shown);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xvrijetijdlist)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xvoegvrijtoe;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker xstartvrij;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker xeindvrij;
        private System.Windows.Forms.Label xuurvrij;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button xverwijdervrij;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label xstatus;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Button xannuleer;
        private System.Windows.Forms.Button xok;
        private BrightIdeasSoftware.ObjectListView xvrijetijdlist;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private BrightIdeasSoftware.OLVColumn olvColumn3;
        private System.Windows.Forms.ImageList imageList1;
    }
}