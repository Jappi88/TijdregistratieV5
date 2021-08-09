
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.xmeebezig = new HtmlRenderer.HtmlPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xvolgende = new HtmlRenderer.HtmlPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.xhuidigpic = new System.Windows.Forms.PictureBox();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xhuidigpic)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xmeebezig);
            this.panel1.Controls.Add(this.xhuidigpic);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(3, 22);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(733, 50);
            this.panel1.TabIndex = 0;
            // 
            // xmeebezig
            // 
            this.xmeebezig.AutoScroll = true;
            this.xmeebezig.AutoScrollMinSize = new System.Drawing.Size(664, 17);
            this.xmeebezig.BackColor = System.Drawing.SystemColors.Window;
            this.xmeebezig.BaseStylesheet = null;
            this.xmeebezig.Cursor = System.Windows.Forms.Cursors.Default;
            this.xmeebezig.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xmeebezig.Location = new System.Drawing.Point(69, 0);
            this.xmeebezig.Name = "xmeebezig";
            this.xmeebezig.Size = new System.Drawing.Size(664, 50);
            this.xmeebezig.TabIndex = 2;
            this.xmeebezig.Text = "Mee Bezig";
            this.xmeebezig.DoubleClick += new System.EventHandler(this.xmeebezig_DoubleClick);
            this.xmeebezig.MouseEnter += new System.EventHandler(this.xmeebezig_MouseEnter);
            this.xmeebezig.MouseLeave += new System.EventHandler(this.xmeebezig_MouseLeave);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xvolgende);
            this.panel2.Controls.Add(this.pictureBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(3, 72);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(733, 50);
            this.panel2.TabIndex = 1;
            // 
            // xvolgende
            // 
            this.xvolgende.AutoScroll = true;
            this.xvolgende.AutoScrollMinSize = new System.Drawing.Size(639, 17);
            this.xvolgende.BackColor = System.Drawing.SystemColors.Window;
            this.xvolgende.BaseStylesheet = "";
            this.xvolgende.Cursor = System.Windows.Forms.Cursors.Default;
            this.xvolgende.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xvolgende.Location = new System.Drawing.Point(94, 0);
            this.xvolgende.Name = "xvolgende";
            this.xvolgende.Size = new System.Drawing.Size(639, 50);
            this.xvolgende.TabIndex = 3;
            this.xvolgende.Text = "Volgende";
            this.xvolgende.DoubleClick += new System.EventHandler(this.xvolgende_DoubleClick);
            this.xvolgende.MouseEnter += new System.EventHandler(this.xmeebezig_MouseEnter);
            this.xvolgende.MouseLeave += new System.EventHandler(this.xmeebezig_MouseLeave);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.panel2);
            this.groupBox1.Controls.Add(this.panel1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(739, 125);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Werkplek";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.arrow_right_15600;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(94, 50);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // xhuidigpic
            // 
            this.xhuidigpic.Dock = System.Windows.Forms.DockStyle.Left;
            this.xhuidigpic.Image = global::ProductieManager.Properties.Resources.play_button_icon_icons_com_60615;
            this.xhuidigpic.Location = new System.Drawing.Point(0, 0);
            this.xhuidigpic.Name = "xhuidigpic";
            this.xhuidigpic.Size = new System.Drawing.Size(69, 50);
            this.xhuidigpic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.xhuidigpic.TabIndex = 3;
            this.xhuidigpic.TabStop = false;
            // 
            // WerkPlekInfoUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.groupBox1);
            this.DoubleBuffered = true;
            this.Name = "WerkPlekInfoUI";
            this.Size = new System.Drawing.Size(739, 125);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xhuidigpic)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.PictureBox xhuidigpic;
        private HtmlRenderer.HtmlPanel xmeebezig;
        private HtmlRenderer.HtmlPanel xvolgende;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}
