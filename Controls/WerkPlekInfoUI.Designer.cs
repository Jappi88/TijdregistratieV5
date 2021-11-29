
using TheArtOfDev.HtmlRenderer.WinForms;

namespace Controls
{
    partial class WerkPlekInfoUI
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
            this.xmeebezig = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.xvolgende = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xmeebezig
            // 
            this.xmeebezig.AutoSize = false;
            this.xmeebezig.BackColor = System.Drawing.SystemColors.Window;
            this.xmeebezig.BaseStylesheet = null;
            this.xmeebezig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmeebezig.Location = new System.Drawing.Point(0, 0);
            this.xmeebezig.Name = "xmeebezig";
            this.xmeebezig.Size = new System.Drawing.Size(343, 180);
            this.xmeebezig.TabIndex = 3;
            this.xmeebezig.Text = "xhuidig";
            this.xmeebezig.LinkClicked += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlLinkClickedEventArgs>(this.xmeebezig_LinkClicked);
            this.xmeebezig.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xmeebezig_ImageLoad);
            this.xmeebezig.DoubleClick += new System.EventHandler(this.xmeebezig_DoubleClick);
            this.xmeebezig.MouseEnter += new System.EventHandler(this.xmeebezig_MouseEnter);
            this.xmeebezig.MouseLeave += new System.EventHandler(this.xmeebezig_MouseLeave);
            // 
            // xvolgende
            // 
            this.xvolgende.AutoSize = false;
            this.xvolgende.BackColor = System.Drawing.SystemColors.Window;
            this.xvolgende.BaseStylesheet = null;
            this.xvolgende.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xvolgende.Location = new System.Drawing.Point(0, 0);
            this.xvolgende.Name = "xvolgende";
            this.xvolgende.Size = new System.Drawing.Size(339, 180);
            this.xvolgende.TabIndex = 4;
            this.xvolgende.Text = "xvolgende";
            this.xvolgende.LinkClicked += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlLinkClickedEventArgs>(this.xmeebezig_LinkClicked);
            this.xvolgende.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xmeebezig_ImageLoad);
            this.xvolgende.DoubleClick += new System.EventHandler(this.xvolgende_DoubleClick);
            this.xvolgende.MouseEnter += new System.EventHandler(this.xmeebezig_MouseEnter);
            this.xvolgende.MouseLeave += new System.EventHandler(this.xmeebezig_MouseLeave);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(10, 10);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.xmeebezig);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.xvolgende);
            this.splitContainer1.Size = new System.Drawing.Size(686, 180);
            this.splitContainer1.SplitterDistance = 343;
            this.splitContainer1.TabIndex = 5;
            // 
            // WerkPlekInfoUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Name = "WerkPlekInfoUI";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(706, 200);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private HtmlLabel xmeebezig;
        private HtmlLabel xvolgende;
        private System.Windows.Forms.SplitContainer splitContainer1;
    }
}
