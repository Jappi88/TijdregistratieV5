using TheArtOfDev.HtmlRenderer.WinForms;

namespace Controls
{
    partial class ProductieInfoUI
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
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.metroTabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.xHeaderHtmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.metroTabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.xInforHtmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.metroTabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.xNotitieHtmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.metroTabPage4 = new MetroFramework.Controls.MetroTabPage();
            this.xDatumsHtmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.metroTabPage6 = new MetroFramework.Controls.MetroTabPage();
            this.xVerpakkingHtmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.metroTabPage5 = new MetroFramework.Controls.MetroTabPage();
            this.xMaterialenHtmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.metroTabPage7 = new MetroFramework.Controls.MetroTabPage();
            this.xWerkPlaatsenHtmlPanel = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            this.metroTabControl1.SuspendLayout();
            this.metroTabPage1.SuspendLayout();
            this.metroTabPage2.SuspendLayout();
            this.metroTabPage3.SuspendLayout();
            this.metroTabPage4.SuspendLayout();
            this.metroTabPage6.SuspendLayout();
            this.metroTabPage5.SuspendLayout();
            this.metroTabPage7.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Controls.Add(this.metroTabPage1);
            this.metroTabControl1.Controls.Add(this.metroTabPage2);
            this.metroTabControl1.Controls.Add(this.metroTabPage3);
            this.metroTabControl1.Controls.Add(this.metroTabPage4);
            this.metroTabControl1.Controls.Add(this.metroTabPage6);
            this.metroTabControl1.Controls.Add(this.metroTabPage5);
            this.metroTabControl1.Controls.Add(this.metroTabPage7);
            this.metroTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl1.Location = new System.Drawing.Point(0, 0);
            this.metroTabControl1.Multiline = true;
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(857, 594);
            this.metroTabControl1.Style = MetroFramework.MetroColorStyle.Red;
            this.metroTabControl1.TabIndex = 12;
            this.metroTabControl1.UseSelectable = true;
            this.metroTabControl1.SelectedIndexChanged += new System.EventHandler(this.metroTabControl1_SelectedIndexChanged);
            // 
            // metroTabPage1
            // 
            this.metroTabPage1.Controls.Add(this.xHeaderHtmlPanel);
            this.metroTabPage1.HorizontalScrollbarBarColor = true;
            this.metroTabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.HorizontalScrollbarSize = 6;
            this.metroTabPage1.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage1.Name = "metroTabPage1";
            this.metroTabPage1.Size = new System.Drawing.Size(849, 552);
            this.metroTabPage1.TabIndex = 6;
            this.metroTabPage1.Text = "Productie Status";
            this.metroTabPage1.VerticalScrollbarBarColor = true;
            this.metroTabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage1.VerticalScrollbarSize = 8;
            // 
            // xHeaderHtmlPanel
            // 
            this.xHeaderHtmlPanel.AutoScroll = true;
            this.xHeaderHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(849, 20);
            this.xHeaderHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xHeaderHtmlPanel.BaseStylesheet = null;
            this.xHeaderHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xHeaderHtmlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xHeaderHtmlPanel.Location = new System.Drawing.Point(0, 0);
            this.xHeaderHtmlPanel.Name = "xHeaderHtmlPanel";
            this.xHeaderHtmlPanel.Size = new System.Drawing.Size(849, 552);
            this.xHeaderHtmlPanel.TabIndex = 0;
            this.xHeaderHtmlPanel.Text = "Header Html Text";
            this.xHeaderHtmlPanel.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            // 
            // metroTabPage2
            // 
            this.metroTabPage2.Controls.Add(this.xInforHtmlPanel);
            this.metroTabPage2.HorizontalScrollbarBarColor = true;
            this.metroTabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.HorizontalScrollbarSize = 3;
            this.metroTabPage2.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage2.Name = "metroTabPage2";
            this.metroTabPage2.Size = new System.Drawing.Size(849, 552);
            this.metroTabPage2.TabIndex = 1;
            this.metroTabPage2.Text = "Productie Info";
            this.metroTabPage2.VerticalScrollbarBarColor = true;
            this.metroTabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage2.VerticalScrollbarSize = 5;
            // 
            // xInforHtmlPanel
            // 
            this.xInforHtmlPanel.AutoScroll = true;
            this.xInforHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(849, 20);
            this.xInforHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xInforHtmlPanel.BaseStylesheet = null;
            this.xInforHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xInforHtmlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xInforHtmlPanel.Location = new System.Drawing.Point(0, 0);
            this.xInforHtmlPanel.Name = "xInforHtmlPanel";
            this.xInforHtmlPanel.Size = new System.Drawing.Size(849, 552);
            this.xInforHtmlPanel.TabIndex = 1;
            this.xInforHtmlPanel.Text = "Info Html Text";
            this.xInforHtmlPanel.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            // 
            // metroTabPage3
            // 
            this.metroTabPage3.Controls.Add(this.xNotitieHtmlPanel);
            this.metroTabPage3.HorizontalScrollbarBarColor = true;
            this.metroTabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.HorizontalScrollbarSize = 3;
            this.metroTabPage3.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage3.Name = "metroTabPage3";
            this.metroTabPage3.Size = new System.Drawing.Size(849, 552);
            this.metroTabPage3.TabIndex = 2;
            this.metroTabPage3.Text = "Notities";
            this.metroTabPage3.VerticalScrollbarBarColor = true;
            this.metroTabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage3.VerticalScrollbarSize = 5;
            // 
            // xNotitieHtmlPanel
            // 
            this.xNotitieHtmlPanel.AutoScroll = true;
            this.xNotitieHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(849, 20);
            this.xNotitieHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xNotitieHtmlPanel.BaseStylesheet = null;
            this.xNotitieHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xNotitieHtmlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xNotitieHtmlPanel.Location = new System.Drawing.Point(0, 0);
            this.xNotitieHtmlPanel.Name = "xNotitieHtmlPanel";
            this.xNotitieHtmlPanel.Size = new System.Drawing.Size(849, 552);
            this.xNotitieHtmlPanel.TabIndex = 11;
            this.xNotitieHtmlPanel.Text = "Notitie Html Text";
            this.xNotitieHtmlPanel.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            // 
            // metroTabPage4
            // 
            this.metroTabPage4.Controls.Add(this.xDatumsHtmlPanel);
            this.metroTabPage4.HorizontalScrollbarBarColor = true;
            this.metroTabPage4.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage4.HorizontalScrollbarSize = 6;
            this.metroTabPage4.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage4.Name = "metroTabPage4";
            this.metroTabPage4.Size = new System.Drawing.Size(849, 552);
            this.metroTabPage4.TabIndex = 7;
            this.metroTabPage4.Text = "Datums";
            this.metroTabPage4.VerticalScrollbarBarColor = true;
            this.metroTabPage4.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage4.VerticalScrollbarSize = 8;
            // 
            // xDatumsHtmlPanel
            // 
            this.xDatumsHtmlPanel.AutoScroll = true;
            this.xDatumsHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(849, 20);
            this.xDatumsHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xDatumsHtmlPanel.BaseStylesheet = null;
            this.xDatumsHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xDatumsHtmlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xDatumsHtmlPanel.Location = new System.Drawing.Point(0, 0);
            this.xDatumsHtmlPanel.Name = "xDatumsHtmlPanel";
            this.xDatumsHtmlPanel.Padding = new System.Windows.Forms.Padding(5);
            this.xDatumsHtmlPanel.Size = new System.Drawing.Size(849, 552);
            this.xDatumsHtmlPanel.TabIndex = 6;
            this.xDatumsHtmlPanel.Text = "Datums Html Text";
            this.xDatumsHtmlPanel.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            // 
            // metroTabPage6
            // 
            this.metroTabPage6.Controls.Add(this.xVerpakkingHtmlPanel);
            this.metroTabPage6.HorizontalScrollbarBarColor = true;
            this.metroTabPage6.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage6.HorizontalScrollbarSize = 3;
            this.metroTabPage6.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage6.Name = "metroTabPage6";
            this.metroTabPage6.Size = new System.Drawing.Size(849, 552);
            this.metroTabPage6.TabIndex = 5;
            this.metroTabPage6.Text = "Verpakking";
            this.metroTabPage6.VerticalScrollbarBarColor = true;
            this.metroTabPage6.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage6.VerticalScrollbarSize = 5;
            // 
            // xVerpakkingHtmlPanel
            // 
            this.xVerpakkingHtmlPanel.AutoScroll = true;
            this.xVerpakkingHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(849, 20);
            this.xVerpakkingHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xVerpakkingHtmlPanel.BaseStylesheet = null;
            this.xVerpakkingHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xVerpakkingHtmlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xVerpakkingHtmlPanel.Location = new System.Drawing.Point(0, 0);
            this.xVerpakkingHtmlPanel.Name = "xVerpakkingHtmlPanel";
            this.xVerpakkingHtmlPanel.Size = new System.Drawing.Size(849, 552);
            this.xVerpakkingHtmlPanel.TabIndex = 4;
            this.xVerpakkingHtmlPanel.Text = "Verpakking Html Text";
            this.xVerpakkingHtmlPanel.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            // 
            // metroTabPage5
            // 
            this.metroTabPage5.Controls.Add(this.xMaterialenHtmlPanel);
            this.metroTabPage5.HorizontalScrollbarBarColor = true;
            this.metroTabPage5.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage5.HorizontalScrollbarSize = 6;
            this.metroTabPage5.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage5.Name = "metroTabPage5";
            this.metroTabPage5.Size = new System.Drawing.Size(849, 552);
            this.metroTabPage5.TabIndex = 8;
            this.metroTabPage5.Text = "Materialen";
            this.metroTabPage5.VerticalScrollbarBarColor = true;
            this.metroTabPage5.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage5.VerticalScrollbarSize = 8;
            // 
            // xMaterialenHtmlPanel
            // 
            this.xMaterialenHtmlPanel.AutoScroll = true;
            this.xMaterialenHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(849, 20);
            this.xMaterialenHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xMaterialenHtmlPanel.BaseStylesheet = null;
            this.xMaterialenHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xMaterialenHtmlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xMaterialenHtmlPanel.Location = new System.Drawing.Point(0, 0);
            this.xMaterialenHtmlPanel.Name = "xMaterialenHtmlPanel";
            this.xMaterialenHtmlPanel.Size = new System.Drawing.Size(849, 552);
            this.xMaterialenHtmlPanel.TabIndex = 7;
            this.xMaterialenHtmlPanel.Text = "Materialen Html Text";
            this.xMaterialenHtmlPanel.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            // 
            // metroTabPage7
            // 
            this.metroTabPage7.Controls.Add(this.xWerkPlaatsenHtmlPanel);
            this.metroTabPage7.HorizontalScrollbarBarColor = true;
            this.metroTabPage7.HorizontalScrollbarHighlightOnWheel = false;
            this.metroTabPage7.HorizontalScrollbarSize = 6;
            this.metroTabPage7.Location = new System.Drawing.Point(4, 38);
            this.metroTabPage7.Name = "metroTabPage7";
            this.metroTabPage7.Size = new System.Drawing.Size(849, 552);
            this.metroTabPage7.TabIndex = 9;
            this.metroTabPage7.Text = "WerkPlaatsen";
            this.metroTabPage7.VerticalScrollbarBarColor = true;
            this.metroTabPage7.VerticalScrollbarHighlightOnWheel = false;
            this.metroTabPage7.VerticalScrollbarSize = 8;
            // 
            // xWerkPlaatsenHtmlPanel
            // 
            this.xWerkPlaatsenHtmlPanel.AutoScroll = true;
            this.xWerkPlaatsenHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(849, 20);
            this.xWerkPlaatsenHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xWerkPlaatsenHtmlPanel.BaseStylesheet = null;
            this.xWerkPlaatsenHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xWerkPlaatsenHtmlPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xWerkPlaatsenHtmlPanel.Location = new System.Drawing.Point(0, 0);
            this.xWerkPlaatsenHtmlPanel.Name = "xWerkPlaatsenHtmlPanel";
            this.xWerkPlaatsenHtmlPanel.Size = new System.Drawing.Size(849, 552);
            this.xWerkPlaatsenHtmlPanel.TabIndex = 9;
            this.xWerkPlaatsenHtmlPanel.Text = "WerkPlaatsen Html Text";
            this.xWerkPlaatsenHtmlPanel.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            // 
            // ProductieInfoUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.metroTabControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProductieInfoUI";
            this.Size = new System.Drawing.Size(857, 594);
            this.metroTabControl1.ResumeLayout(false);
            this.metroTabPage1.ResumeLayout(false);
            this.metroTabPage2.ResumeLayout(false);
            this.metroTabPage3.ResumeLayout(false);
            this.metroTabPage4.ResumeLayout(false);
            this.metroTabPage6.ResumeLayout(false);
            this.metroTabPage5.ResumeLayout(false);
            this.metroTabPage7.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private HtmlPanel xWerkPlaatsenHtmlPanel;
        private HtmlPanel xMaterialenHtmlPanel;
        private HtmlPanel xNotitieHtmlPanel;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private MetroFramework.Controls.MetroTabPage metroTabPage2;
        private MetroFramework.Controls.MetroTabPage metroTabPage3;
        private MetroFramework.Controls.MetroTabPage metroTabPage6;
        private HtmlPanel xVerpakkingHtmlPanel;
        private HtmlPanel xDatumsHtmlPanel;
        private HtmlPanel xInforHtmlPanel;
        private HtmlPanel xHeaderHtmlPanel;
        private MetroFramework.Controls.MetroTabPage metroTabPage1;
        private MetroFramework.Controls.MetroTabPage metroTabPage4;
        private MetroFramework.Controls.MetroTabPage metroTabPage5;
        private MetroFramework.Controls.MetroTabPage metroTabPage7;
    }
}
