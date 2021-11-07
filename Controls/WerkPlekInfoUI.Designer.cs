
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
            this.xmeebezig = new HtmlRenderer.HtmlPanel();
            this.SuspendLayout();
            // 
            // xmeebezig
            // 
            this.xmeebezig.AutoScroll = true;
            this.xmeebezig.AutoScrollMinSize = new System.Drawing.Size(625, 17);
            this.xmeebezig.BackColor = System.Drawing.SystemColors.Window;
            this.xmeebezig.BaseStylesheet = null;
            this.xmeebezig.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xmeebezig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmeebezig.Location = new System.Drawing.Point(10, 10);
            this.xmeebezig.Name = "xmeebezig";
            this.xmeebezig.Size = new System.Drawing.Size(625, 205);
            this.xmeebezig.TabIndex = 2;
            this.xmeebezig.Text = "Mee Bezig";
            this.xmeebezig.LinkClicked += new System.EventHandler<HtmlRenderer.Entities.HtmlLinkClickedEventArgs>(this.xmeebezig_LinkClicked);
            this.xmeebezig.ImageLoad += new System.EventHandler<HtmlRenderer.Entities.HtmlImageLoadEventArgs>(this.xmeebezig_ImageLoad);
            this.xmeebezig.DoubleClick += new System.EventHandler(this.xmeebezig_DoubleClick);
            this.xmeebezig.MouseEnter += new System.EventHandler(this.xmeebezig_MouseEnter);
            this.xmeebezig.MouseLeave += new System.EventHandler(this.xmeebezig_MouseLeave);
            // 
            // WerkPlekInfoUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xmeebezig);
            this.DoubleBuffered = true;
            this.Name = "WerkPlekInfoUI";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(645, 225);
            this.ResumeLayout(false);

        }

        #endregion
        private HtmlRenderer.HtmlPanel xmeebezig;
    }
}
