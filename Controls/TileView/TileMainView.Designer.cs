namespace Controls.TileView
{
    partial class TileMainView
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
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xBeheerweergavetoolstrip = new System.Windows.Forms.ToolStripSplitButton();
            this.wijzigGroepGrootteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.kiesAchtergrondKleurToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.reserLayoutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xGroupContainer = new System.Windows.Forms.Panel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(193, 6);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xBeheerweergavetoolstrip});
            this.toolStrip1.Location = new System.Drawing.Point(0, 561);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(816, 25);
            this.toolStrip1.TabIndex = 2;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Visible = false;
            // 
            // xBeheerweergavetoolstrip
            // 
            this.xBeheerweergavetoolstrip.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xBeheerweergavetoolstrip.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.wijzigGroepGrootteToolStripMenuItem,
            this.toolStripSeparator1,
            this.kiesAchtergrondKleurToolStripMenuItem,
            this.toolStripSeparator2,
            this.toolStripSeparator4,
            this.reserLayoutToolStripMenuItem});
            this.xBeheerweergavetoolstrip.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xBeheerweergavetoolstrip.Image = global::ProductieManager.Properties.Resources.layout_widget_icon_32x32;
            this.xBeheerweergavetoolstrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xBeheerweergavetoolstrip.Name = "xBeheerweergavetoolstrip";
            this.xBeheerweergavetoolstrip.Size = new System.Drawing.Size(32, 22);
            this.xBeheerweergavetoolstrip.Visible = false;
            // 
            // wijzigGroepGrootteToolStripMenuItem
            // 
            this.wijzigGroepGrootteToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.add_Blue_circle_32x32;
            this.wijzigGroepGrootteToolStripMenuItem.Name = "wijzigGroepGrootteToolStripMenuItem";
            this.wijzigGroepGrootteToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.wijzigGroepGrootteToolStripMenuItem.Text = "Nieuwe Groep";
            this.wijzigGroepGrootteToolStripMenuItem.Click += new System.EventHandler(this.wijzigGroepGrootteToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(236, 6);
            // 
            // kiesAchtergrondKleurToolStripMenuItem
            // 
            this.kiesAchtergrondKleurToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.Edit_color_32x32;
            this.kiesAchtergrondKleurToolStripMenuItem.Name = "kiesAchtergrondKleurToolStripMenuItem";
            this.kiesAchtergrondKleurToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.kiesAchtergrondKleurToolStripMenuItem.Text = "Kies Achtergrond Kleur";
            this.kiesAchtergrondKleurToolStripMenuItem.Click += new System.EventHandler(this.kiesAchtergrondKleurToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(236, 6);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(236, 6);
            // 
            // reserLayoutToolStripMenuItem
            // 
            this.reserLayoutToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.reserLayoutToolStripMenuItem.Name = "reserLayoutToolStripMenuItem";
            this.reserLayoutToolStripMenuItem.Size = new System.Drawing.Size(239, 26);
            this.reserLayoutToolStripMenuItem.Text = "Reset Groepen";
            // 
            // xGroupContainer
            // 
            this.xGroupContainer.AutoScroll = true;
            this.xGroupContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xGroupContainer.Location = new System.Drawing.Point(0, 0);
            this.xGroupContainer.Name = "xGroupContainer";
            this.xGroupContainer.Size = new System.Drawing.Size(816, 586);
            this.xGroupContainer.TabIndex = 3;
            // 
            // TileMainView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xGroupContainer);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Name = "TileMainView";
            this.Size = new System.Drawing.Size(816, 586);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton xBeheerweergavetoolstrip;
        private System.Windows.Forms.ToolStripMenuItem wijzigGroepGrootteToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem kiesAchtergrondKleurToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem reserLayoutToolStripMenuItem;
        private System.Windows.Forms.Panel xGroupContainer;
    }
}