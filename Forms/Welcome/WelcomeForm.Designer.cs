namespace ProductieManager.Forms.Welcome
{
    partial class WelcomeForm
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
            this.xvorige = new System.Windows.Forms.Button();
            this.xvolgende = new System.Windows.Forms.Button();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.xwelcomepage = new MetroFramework.Controls.MetroTabPage();
            this.xaccounttab = new MetroFramework.Controls.MetroTabPage();
            this.xdatabasetab = new MetroFramework.Controls.MetroTabPage();
            this.xfiltertab = new MetroFramework.Controls.MetroTabPage();
            this.xbeginnentab = new MetroFramework.Controls.MetroTabPage();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xproductiestab = new MetroFramework.Controls.MetroTabPage();
            this.xwelcomepanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.panel1.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.xwelcomepage.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xvorige);
            this.panel1.Controls.Add(this.xvolgende);
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(20, 530);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1096, 32);
            this.panel1.TabIndex = 0;
            // 
            // xvorige
            // 
            this.xvorige.Dock = System.Windows.Forms.DockStyle.Right;
            this.xvorige.FlatAppearance.BorderSize = 0;
            this.xvorige.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xvorige.Image = global::ProductieManager.Properties.Resources.arrow_left_15601;
            this.xvorige.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xvorige.Location = new System.Drawing.Point(776, 0);
            this.xvorige.Name = "xvorige";
            this.xvorige.Size = new System.Drawing.Size(110, 32);
            this.xvorige.TabIndex = 2;
            this.xvorige.Text = "Vorige";
            this.xvorige.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xvorige.UseVisualStyleBackColor = true;
            this.xvorige.Click += new System.EventHandler(this.xvorige_Click);
            // 
            // xvolgende
            // 
            this.xvolgende.Dock = System.Windows.Forms.DockStyle.Right;
            this.xvolgende.FlatAppearance.BorderSize = 0;
            this.xvolgende.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xvolgende.Image = global::ProductieManager.Properties.Resources.arrow_right_15600;
            this.xvolgende.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xvolgende.Location = new System.Drawing.Point(886, 0);
            this.xvolgende.Name = "xvolgende";
            this.xvolgende.Size = new System.Drawing.Size(110, 32);
            this.xvolgende.TabIndex = 1;
            this.xvolgende.Text = "Volgende";
            this.xvolgende.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xvolgende.UseVisualStyleBackColor = true;
            this.xvolgende.Click += new System.EventHandler(this.xvolgende_Click);
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.xwelcomepage);
            this.metroTabControl1.Controls.Add(this.xdatabasetab);
            this.metroTabControl1.Controls.Add(this.xaccounttab);
            this.metroTabControl1.Controls.Add(this.xfiltertab);
            this.metroTabControl1.Controls.Add(this.xproductiestab);
            this.metroTabControl1.Controls.Add(this.xbeginnentab);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(20, 60);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.ShowToolTips = true;
            this.metroTabControl1.Size = new System.Drawing.Size(1096, 470);
            this.metroTabControl1.TabIndex = 1;
            this.metroTabControl1.UseSelectable = true;
            this.metroTabControl1.SelectedIndexChanged += new System.EventHandler(this.metroTabControl1_SelectedIndexChanged);
            // 
            // xwelcomepage
            // 
            this.xwelcomepage.Controls.Add(this.xwelcomepanel);
            this.xwelcomepage.HorizontalScrollbarBarColor = true;
            this.xwelcomepage.HorizontalScrollbarHighlightOnWheel = false;
            this.xwelcomepage.HorizontalScrollbarSize = 10;
            this.xwelcomepage.Location = new System.Drawing.Point(4, 38);
            this.xwelcomepage.Name = "xwelcomepage";
            this.xwelcomepage.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.xwelcomepage.Size = new System.Drawing.Size(1088, 428);
            this.xwelcomepage.TabIndex = 0;
            this.xwelcomepage.Text = "Welkom";
            this.xwelcomepage.ToolTipText = "Welkom";
            this.xwelcomepage.VerticalScrollbarBarColor = true;
            this.xwelcomepage.VerticalScrollbarHighlightOnWheel = false;
            this.xwelcomepage.VerticalScrollbarSize = 10;
            // 
            // xaccounttab
            // 
            this.xaccounttab.HorizontalScrollbarBarColor = true;
            this.xaccounttab.HorizontalScrollbarHighlightOnWheel = false;
            this.xaccounttab.HorizontalScrollbarSize = 10;
            this.xaccounttab.Location = new System.Drawing.Point(4, 38);
            this.xaccounttab.Name = "xaccounttab";
            this.xaccounttab.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.xaccounttab.Size = new System.Drawing.Size(1088, 428);
            this.xaccounttab.TabIndex = 1;
            this.xaccounttab.Text = "Account";
            this.xaccounttab.ToolTipText = "Account instellen";
            this.xaccounttab.VerticalScrollbarBarColor = true;
            this.xaccounttab.VerticalScrollbarHighlightOnWheel = false;
            this.xaccounttab.VerticalScrollbarSize = 10;
            // 
            // xdatabasetab
            // 
            this.xdatabasetab.HorizontalScrollbarBarColor = true;
            this.xdatabasetab.HorizontalScrollbarHighlightOnWheel = false;
            this.xdatabasetab.HorizontalScrollbarSize = 10;
            this.xdatabasetab.Location = new System.Drawing.Point(4, 38);
            this.xdatabasetab.Name = "xdatabasetab";
            this.xdatabasetab.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.xdatabasetab.Size = new System.Drawing.Size(1088, 428);
            this.xdatabasetab.TabIndex = 2;
            this.xdatabasetab.Text = "Database";
            this.xdatabasetab.ToolTipText = "Stel in de database";
            this.xdatabasetab.VerticalScrollbarBarColor = true;
            this.xdatabasetab.VerticalScrollbarHighlightOnWheel = false;
            this.xdatabasetab.VerticalScrollbarSize = 10;
            // 
            // xfiltertab
            // 
            this.xfiltertab.HorizontalScrollbarBarColor = true;
            this.xfiltertab.HorizontalScrollbarHighlightOnWheel = false;
            this.xfiltertab.HorizontalScrollbarSize = 10;
            this.xfiltertab.Location = new System.Drawing.Point(4, 38);
            this.xfiltertab.Name = "xfiltertab";
            this.xfiltertab.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.xfiltertab.Size = new System.Drawing.Size(1088, 428);
            this.xfiltertab.TabIndex = 3;
            this.xfiltertab.Text = "Filter";
            this.xfiltertab.ToolTipText = "Kies Filters";
            this.xfiltertab.VerticalScrollbarBarColor = true;
            this.xfiltertab.VerticalScrollbarHighlightOnWheel = false;
            this.xfiltertab.VerticalScrollbarSize = 10;
            // 
            // xbeginnentab
            // 
            this.xbeginnentab.HorizontalScrollbarBarColor = true;
            this.xbeginnentab.HorizontalScrollbarHighlightOnWheel = false;
            this.xbeginnentab.HorizontalScrollbarSize = 10;
            this.xbeginnentab.Location = new System.Drawing.Point(4, 38);
            this.xbeginnentab.Name = "xbeginnentab";
            this.xbeginnentab.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.xbeginnentab.Size = new System.Drawing.Size(1088, 428);
            this.xbeginnentab.TabIndex = 4;
            this.xbeginnentab.Text = "Beginnen";
            this.xbeginnentab.ToolTipText = "Beginnen";
            this.xbeginnentab.VerticalScrollbarBarColor = true;
            this.xbeginnentab.VerticalScrollbarHighlightOnWheel = false;
            this.xbeginnentab.VerticalScrollbarSize = 10;
            // 
            // xsluiten
            // 
            this.xsluiten.Dock = System.Windows.Forms.DockStyle.Right;
            this.xsluiten.FlatAppearance.BorderSize = 0;
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(996, 0);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(100, 32);
            this.xsluiten.TabIndex = 0;
            this.xsluiten.Text = "Sluiten";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xproductiestab
            // 
            this.xproductiestab.HorizontalScrollbarBarColor = true;
            this.xproductiestab.HorizontalScrollbarHighlightOnWheel = false;
            this.xproductiestab.HorizontalScrollbarSize = 10;
            this.xproductiestab.Location = new System.Drawing.Point(4, 38);
            this.xproductiestab.Name = "xproductiestab";
            this.xproductiestab.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.xproductiestab.Size = new System.Drawing.Size(1088, 428);
            this.xproductiestab.TabIndex = 5;
            this.xproductiestab.Text = "Producties";
            this.xproductiestab.ToolTipText = "Productie Info";
            this.xproductiestab.VerticalScrollbarBarColor = true;
            this.xproductiestab.VerticalScrollbarHighlightOnWheel = false;
            this.xproductiestab.VerticalScrollbarSize = 10;
            // 
            // xwelcomepanel
            // 
            this.xwelcomepanel.AutoScroll = true;
            this.xwelcomepanel.AutoScrollMinSize = new System.Drawing.Size(1078, 20);
            this.xwelcomepanel.BackColor = System.Drawing.SystemColors.Window;
            this.xwelcomepanel.BaseStylesheet = null;
            this.xwelcomepanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xwelcomepanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xwelcomepanel.IsContextMenuEnabled = false;
            this.xwelcomepanel.Location = new System.Drawing.Point(5, 5);
            this.xwelcomepanel.Name = "xwelcomepanel";
            this.xwelcomepanel.Size = new System.Drawing.Size(1078, 423);
            this.xwelcomepanel.TabIndex = 2;
            this.xwelcomepanel.Text = "Welkom!";
            // 
            // WelcomeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1136, 582);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.panel1);
            this.Name = "WelcomeForm";
            this.Text = "Welkom!";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.WelcomeForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.metroTabControl1.ResumeLayout(false);
            this.xwelcomepage.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xvorige;
        private System.Windows.Forms.Button xvolgende;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage xwelcomepage;
        private MetroFramework.Controls.MetroTabPage xaccounttab;
        private MetroFramework.Controls.MetroTabPage xdatabasetab;
        private MetroFramework.Controls.MetroTabPage xfiltertab;
        private MetroFramework.Controls.MetroTabPage xbeginnentab;
        private System.Windows.Forms.Button xsluiten;
        private TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel xwelcomepanel;
        private MetroFramework.Controls.MetroTabPage xproductiestab;
    }
}