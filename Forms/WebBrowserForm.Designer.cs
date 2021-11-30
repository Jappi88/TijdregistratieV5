namespace ProductieManager.Forms
{
    partial class WebBrowserForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WebBrowserForm));
            this.xBrowser = new System.Windows.Forms.WebBrowser();
            this.SuspendLayout();
            // 
            // xBrowser
            // 
            this.xBrowser.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xBrowser.IsWebBrowserContextMenuEnabled = false;
            this.xBrowser.Location = new System.Drawing.Point(0, 0);
            this.xBrowser.MinimumSize = new System.Drawing.Size(20, 20);
            this.xBrowser.Name = "xBrowser";
            this.xBrowser.ScriptErrorsSuppressed = true;
            this.xBrowser.Size = new System.Drawing.Size(808, 523);
            this.xBrowser.TabIndex = 0;
            this.xBrowser.Navigated += new System.Windows.Forms.WebBrowserNavigatedEventHandler(this.xBrowser_Navigated);
            // 
            // WebBrowserForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(808, 523);
            this.Controls.Add(this.xBrowser);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "WebBrowserForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fabrikage Tekening";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WebBrowserForm_FormClosing);
            this.Shown += new System.EventHandler(this.WebBrowserForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.WebBrowser xBrowser;
    }
}