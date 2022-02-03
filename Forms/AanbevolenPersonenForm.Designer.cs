
using TheArtOfDev.HtmlRenderer.WinForms;

namespace Forms
{
    partial class AanbevolenPersonenForm
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
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.xwerkplekkenHtmlPanel = new HtmlPanel();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.xpersHtmlPanel = new HtmlPanel();
            this.xloadinglabel = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.metroTabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(830, 530);
            this.panel1.TabIndex = 0;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(0, 0);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(830, 530);
            this.metroTabControl1.Style = MetroFramework.MetroColorStyle.Green;
            this.metroTabControl1.TabIndex = 0;
            this.metroTabControl1.UseSelectable = true;
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.xwerkplekkenHtmlPanel);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 10;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(822, 488);
            this.metroTabPage1.TabIndex = 0;
            this.metroTabPage1.Text = "Aanbevolen Werkplekken";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 10;
            // 
            // xwerkplekkenHtmlPanel
            // 
            this.xwerkplekkenHtmlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xwerkplekkenHtmlPanel.AutoScroll = true;
            this.xwerkplekkenHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xwerkplekkenHtmlPanel.BaseStylesheet = null;
            this.xwerkplekkenHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xwerkplekkenHtmlPanel.Location = new System.Drawing.Point(3, 3);
            this.xwerkplekkenHtmlPanel.Name = "xwerkplekkenHtmlPanel";
            this.xwerkplekkenHtmlPanel.Size = new System.Drawing.Size(816, 482);
            this.xwerkplekkenHtmlPanel.TabIndex = 0;
            this.xwerkplekkenHtmlPanel.Text = null;
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.xpersHtmlPanel);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 10;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(822, 488);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Aanbevolen Personeel";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 10;
            // 
            // xpersHtmlPanel
            // 
            this.xpersHtmlPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xpersHtmlPanel.AutoScroll = true;
            this.xpersHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xpersHtmlPanel.BaseStylesheet = null;
            this.xpersHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xpersHtmlPanel.Location = new System.Drawing.Point(3, 3);
            this.xpersHtmlPanel.Name = "xpersHtmlPanel";
            this.xpersHtmlPanel.Size = new System.Drawing.Size(816, 494);
            this.xpersHtmlPanel.TabIndex = 2;
            this.xpersHtmlPanel.Text = null;
            // 
            // xloadinglabel
            // 
            this.xloadinglabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xloadinglabel.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xloadinglabel.Location = new System.Drawing.Point(20, 60);
            this.xloadinglabel.Name = "xloadinglabel";
            this.xloadinglabel.Size = new System.Drawing.Size(830, 530);
            this.xloadinglabel.TabIndex = 30;
            this.xloadinglabel.Text = "Zoeken naar aanbevelingen...";
            this.xloadinglabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.xloadinglabel.Visible = false;
            // 
            // AanbevolenPersonenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 610);
            this.Controls.Add(this.xloadinglabel);
            this.Controls.Add(this.panel1);
            this.Name = "AanbevolenPersonenForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Geen Aanbevelingen";
            this.Shown += new System.EventHandler(this.AanbevolenPersonenForm_Shown);
            this.panel1.ResumeLayout(false);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private HtmlPanel xwerkplekkenHtmlPanel;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private HtmlPanel xpersHtmlPanel;
        private System.Windows.Forms.Label xloadinglabel;
    }
}