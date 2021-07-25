
namespace Controls
{
    partial class MainMenu
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xmenu = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xbuttoncontainer = new MetroFramework.Controls.MetroPanel();
            this.panel1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xbuttoncontainer);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(180, 418);
            this.panel1.TabIndex = 0;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xmenu);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(180, 45);
            this.panel3.TabIndex = 4;
            // 
            // xmenu
            // 
            this.xmenu.BackColor = System.Drawing.Color.Transparent;
            this.xmenu.Dock = System.Windows.Forms.DockStyle.Top;
            this.xmenu.FlatAppearance.BorderSize = 0;
            this.xmenu.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xmenu.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xmenu.Image = global::ProductieManager.Properties.Resources.Menu_icon_2_icon_icons_com_71856;
            this.xmenu.Location = new System.Drawing.Point(0, 0);
            this.xmenu.Name = "xmenu";
            this.xmenu.Size = new System.Drawing.Size(180, 39);
            this.xmenu.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xmenu, "Menu");
            this.xmenu.UseVisualStyleBackColor = false;
            this.xmenu.Click += new System.EventHandler(this.xmenu_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Menu";
            // 
            // xbuttoncontainer
            // 
            this.xbuttoncontainer.AutoScroll = true;
            this.xbuttoncontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xbuttoncontainer.HorizontalScrollbar = true;
            this.xbuttoncontainer.HorizontalScrollbarBarColor = true;
            this.xbuttoncontainer.HorizontalScrollbarHighlightOnWheel = false;
            this.xbuttoncontainer.HorizontalScrollbarSize = 10;
            this.xbuttoncontainer.Location = new System.Drawing.Point(0, 45);
            this.xbuttoncontainer.Name = "xbuttoncontainer";
            this.xbuttoncontainer.Size = new System.Drawing.Size(180, 373);
            this.xbuttoncontainer.TabIndex = 5;
            this.xbuttoncontainer.VerticalScrollbar = true;
            this.xbuttoncontainer.VerticalScrollbarBarColor = true;
            this.xbuttoncontainer.VerticalScrollbarHighlightOnWheel = false;
            this.xbuttoncontainer.VerticalScrollbarSize = 10;
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.Controls.Add(this.panel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "MainMenu";
            this.Size = new System.Drawing.Size(180, 418);
            this.panel1.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xmenu;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolTip toolTip1;
        private MetroFramework.Controls.MetroPanel xbuttoncontainer;
    }
}
