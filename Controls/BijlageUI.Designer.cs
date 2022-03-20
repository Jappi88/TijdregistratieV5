namespace Controls
{
    partial class BijlageUI
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
            this.xbijlagebrowser = new System.Windows.Forms.WebBrowser();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.xvorige = new System.Windows.Forms.ToolStripButton();
            this.xvolgende = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.xstatus = new System.Windows.Forms.ToolStripLabel();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xbijlagebrowser
            // 
            this.xbijlagebrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xbijlagebrowser.Location = new System.Drawing.Point(0, 25);
            this.xbijlagebrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.xbijlagebrowser.Name = "xbijlagebrowser";
            this.xbijlagebrowser.ScriptErrorsSuppressed = true;
            this.xbijlagebrowser.Size = new System.Drawing.Size(746, 449);
            this.xbijlagebrowser.TabIndex = 3;
            this.xbijlagebrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.xbijlagebrowser_Navigated);
            this.xbijlagebrowser.Navigating += new System.Windows.Forms.WebBrowserNavigatingEventHandler(this.xbijlagebrowser_Navigating);
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.Transparent;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xvorige,
            this.xvolgende,
            this.toolStripSeparator1,
            this.xstatus});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(746, 25);
            this.toolStrip1.TabIndex = 4;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // xvorige
            // 
            this.xvorige.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xvorige.Enabled = false;
            this.xvorige.Image = global::ProductieManager.Properties.Resources.arrow_left_15601;
            this.xvorige.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xvorige.Name = "xvorige";
            this.xvorige.Size = new System.Drawing.Size(23, 22);
            this.xvorige.ToolTipText = "Vorige pagina";
            this.xvorige.Click += new System.EventHandler(this.xvorige_Click);
            // 
            // xvolgende
            // 
            this.xvolgende.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xvolgende.Enabled = false;
            this.xvolgende.Image = global::ProductieManager.Properties.Resources.arrow_right_15600;
            this.xvolgende.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xvolgende.Name = "xvolgende";
            this.xvolgende.Size = new System.Drawing.Size(23, 22);
            this.xvolgende.ToolTipText = "Volgende pagina";
            this.xvolgende.Click += new System.EventHandler(this.xvolgende_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // xstatus
            // 
            this.xstatus.Name = "xstatus";
            this.xstatus.Size = new System.Drawing.Size(47, 22);
            this.xstatus.Text = "Bijlages";
            // 
            // BijlageUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xbijlagebrowser);
            this.Controls.Add(this.toolStrip1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "BijlageUI";
            this.Size = new System.Drawing.Size(746, 474);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.WebBrowser xbijlagebrowser;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton xvorige;
        private System.Windows.Forms.ToolStripButton xvolgende;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel xstatus;
    }
}
