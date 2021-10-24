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
            this.xpanelcontainer = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xWerkPlaatsenHtmlPanel = new HtmlRenderer.HtmlPanel();
            this.xWerkPlaatsenButton = new System.Windows.Forms.Button();
            this.xMaterialenHtmlPanel = new HtmlRenderer.HtmlPanel();
            this.xMaterialenButton = new System.Windows.Forms.Button();
            this.xVerpakkingHtmlPanel = new HtmlRenderer.HtmlPanel();
            this.xVerpakkingsButton = new System.Windows.Forms.Button();
            this.xDatumsHtmlPanel = new HtmlRenderer.HtmlPanel();
            this.xProductieDatumsButton = new System.Windows.Forms.Button();
            this.xNotitieHtmlPanel = new HtmlRenderer.HtmlPanel();
            this.xNotitieButton = new System.Windows.Forms.Button();
            this.xInforHtmlPanel = new HtmlRenderer.HtmlPanel();
            this.xProductieInfoButton = new System.Windows.Forms.Button();
            this.xHeaderHtmlPanel = new HtmlRenderer.HtmlPanel();
            this.xProductieStatusButton = new System.Windows.Forms.Button();
            this.xpanelcontainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xpanelcontainer
            // 
            this.xpanelcontainer.AutoScroll = true;
            this.xpanelcontainer.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.xpanelcontainer.Controls.Add(this.panel1);
            this.xpanelcontainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xpanelcontainer.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xpanelcontainer.Location = new System.Drawing.Point(0, 0);
            this.xpanelcontainer.Name = "xpanelcontainer";
            this.xpanelcontainer.Padding = new System.Windows.Forms.Padding(5);
            this.xpanelcontainer.Size = new System.Drawing.Size(776, 441);
            this.xpanelcontainer.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.xWerkPlaatsenHtmlPanel);
            this.panel1.Controls.Add(this.xWerkPlaatsenButton);
            this.panel1.Controls.Add(this.xMaterialenHtmlPanel);
            this.panel1.Controls.Add(this.xMaterialenButton);
            this.panel1.Controls.Add(this.xVerpakkingHtmlPanel);
            this.panel1.Controls.Add(this.xVerpakkingsButton);
            this.panel1.Controls.Add(this.xDatumsHtmlPanel);
            this.panel1.Controls.Add(this.xProductieDatumsButton);
            this.panel1.Controls.Add(this.xNotitieHtmlPanel);
            this.panel1.Controls.Add(this.xNotitieButton);
            this.panel1.Controls.Add(this.xInforHtmlPanel);
            this.panel1.Controls.Add(this.xProductieInfoButton);
            this.panel1.Controls.Add(this.xHeaderHtmlPanel);
            this.panel1.Controls.Add(this.xProductieStatusButton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(5, 5);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(766, 431);
            this.panel1.TabIndex = 13;
            this.panel1.VisibleChanged += new System.EventHandler(this.panel1_VisibleChanged);
            // 
            // xWerkPlaatsenHtmlPanel
            // 
            this.xWerkPlaatsenHtmlPanel.AutoScroll = true;
            this.xWerkPlaatsenHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(756, 17);
            this.xWerkPlaatsenHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xWerkPlaatsenHtmlPanel.BaseStylesheet = null;
            this.xWerkPlaatsenHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xWerkPlaatsenHtmlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xWerkPlaatsenHtmlPanel.Location = new System.Drawing.Point(5, 334);
            this.xWerkPlaatsenHtmlPanel.Name = "xWerkPlaatsenHtmlPanel";
            this.xWerkPlaatsenHtmlPanel.Size = new System.Drawing.Size(756, 25);
            this.xWerkPlaatsenHtmlPanel.TabIndex = 9;
            this.xWerkPlaatsenHtmlPanel.Text = "WerkPlaatsen Html Text";
            this.xWerkPlaatsenHtmlPanel.Visible = false;
            this.xWerkPlaatsenHtmlPanel.ImageLoad += new System.EventHandler<HtmlRenderer.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            this.xWerkPlaatsenHtmlPanel.DoubleClick += new System.EventHandler(this.xWerkPlaatsenButton_Click);
            // 
            // xWerkPlaatsenButton
            // 
            this.xWerkPlaatsenButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.xWerkPlaatsenButton.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.xWerkPlaatsenButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xWerkPlaatsenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xWerkPlaatsenButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xWerkPlaatsenButton.Image = global::ProductieManager.Properties.Resources.Navigate_down_36747;
            this.xWerkPlaatsenButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xWerkPlaatsenButton.Location = new System.Drawing.Point(5, 309);
            this.xWerkPlaatsenButton.Name = "xWerkPlaatsenButton";
            this.xWerkPlaatsenButton.Size = new System.Drawing.Size(756, 25);
            this.xWerkPlaatsenButton.TabIndex = 10;
            this.xWerkPlaatsenButton.Text = "Werk Plaatsen";
            this.xWerkPlaatsenButton.UseVisualStyleBackColor = true;
            this.xWerkPlaatsenButton.Click += new System.EventHandler(this.xWerkPlaatsenButton_Click);
            // 
            // xMaterialenHtmlPanel
            // 
            this.xMaterialenHtmlPanel.AutoScroll = true;
            this.xMaterialenHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(756, 17);
            this.xMaterialenHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xMaterialenHtmlPanel.BaseStylesheet = null;
            this.xMaterialenHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xMaterialenHtmlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xMaterialenHtmlPanel.Location = new System.Drawing.Point(5, 284);
            this.xMaterialenHtmlPanel.Name = "xMaterialenHtmlPanel";
            this.xMaterialenHtmlPanel.Size = new System.Drawing.Size(756, 25);
            this.xMaterialenHtmlPanel.TabIndex = 7;
            this.xMaterialenHtmlPanel.Text = "Materialen Html Text";
            this.xMaterialenHtmlPanel.Visible = false;
            this.xMaterialenHtmlPanel.ImageLoad += new System.EventHandler<HtmlRenderer.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            this.xMaterialenHtmlPanel.DoubleClick += new System.EventHandler(this.xMaterialenButton_Click);
            // 
            // xMaterialenButton
            // 
            this.xMaterialenButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.xMaterialenButton.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.xMaterialenButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xMaterialenButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xMaterialenButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xMaterialenButton.Image = global::ProductieManager.Properties.Resources.Navigate_down_36747;
            this.xMaterialenButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xMaterialenButton.Location = new System.Drawing.Point(5, 259);
            this.xMaterialenButton.Name = "xMaterialenButton";
            this.xMaterialenButton.Size = new System.Drawing.Size(756, 25);
            this.xMaterialenButton.TabIndex = 8;
            this.xMaterialenButton.Text = "Materialen";
            this.xMaterialenButton.UseVisualStyleBackColor = true;
            this.xMaterialenButton.Click += new System.EventHandler(this.xMaterialenButton_Click);
            // 
            // xVerpakkingHtmlPanel
            // 
            this.xVerpakkingHtmlPanel.AutoScroll = true;
            this.xVerpakkingHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(756, 17);
            this.xVerpakkingHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xVerpakkingHtmlPanel.BaseStylesheet = null;
            this.xVerpakkingHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xVerpakkingHtmlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xVerpakkingHtmlPanel.Location = new System.Drawing.Point(5, 234);
            this.xVerpakkingHtmlPanel.Name = "xVerpakkingHtmlPanel";
            this.xVerpakkingHtmlPanel.Size = new System.Drawing.Size(756, 25);
            this.xVerpakkingHtmlPanel.TabIndex = 4;
            this.xVerpakkingHtmlPanel.Text = "Verpakking Html Text";
            this.xVerpakkingHtmlPanel.Visible = false;
            this.xVerpakkingHtmlPanel.ImageLoad += new System.EventHandler<HtmlRenderer.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            this.xVerpakkingHtmlPanel.DoubleClick += new System.EventHandler(this.xVerpakkingsButton_Click);
            // 
            // xVerpakkingsButton
            // 
            this.xVerpakkingsButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.xVerpakkingsButton.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.xVerpakkingsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xVerpakkingsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xVerpakkingsButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xVerpakkingsButton.Image = global::ProductieManager.Properties.Resources.Navigate_down_36747;
            this.xVerpakkingsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xVerpakkingsButton.Location = new System.Drawing.Point(5, 209);
            this.xVerpakkingsButton.Name = "xVerpakkingsButton";
            this.xVerpakkingsButton.Size = new System.Drawing.Size(756, 25);
            this.xVerpakkingsButton.TabIndex = 3;
            this.xVerpakkingsButton.Text = "VerpakkingsInstructies";
            this.xVerpakkingsButton.UseVisualStyleBackColor = true;
            this.xVerpakkingsButton.Click += new System.EventHandler(this.xVerpakkingsButton_Click);
            // 
            // xDatumsHtmlPanel
            // 
            this.xDatumsHtmlPanel.AutoScroll = true;
            this.xDatumsHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(756, 17);
            this.xDatumsHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xDatumsHtmlPanel.BaseStylesheet = null;
            this.xDatumsHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xDatumsHtmlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xDatumsHtmlPanel.Location = new System.Drawing.Point(5, 184);
            this.xDatumsHtmlPanel.Name = "xDatumsHtmlPanel";
            this.xDatumsHtmlPanel.Size = new System.Drawing.Size(756, 25);
            this.xDatumsHtmlPanel.TabIndex = 6;
            this.xDatumsHtmlPanel.Text = "Datums Html Text";
            this.xDatumsHtmlPanel.Visible = false;
            this.xDatumsHtmlPanel.ImageLoad += new System.EventHandler<HtmlRenderer.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            this.xDatumsHtmlPanel.DoubleClick += new System.EventHandler(this.xProductieDatumsButton_Click);
            // 
            // xProductieDatumsButton
            // 
            this.xProductieDatumsButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.xProductieDatumsButton.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.xProductieDatumsButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xProductieDatumsButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xProductieDatumsButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xProductieDatumsButton.Image = global::ProductieManager.Properties.Resources.Navigate_down_36747;
            this.xProductieDatumsButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xProductieDatumsButton.Location = new System.Drawing.Point(5, 159);
            this.xProductieDatumsButton.Name = "xProductieDatumsButton";
            this.xProductieDatumsButton.Size = new System.Drawing.Size(756, 25);
            this.xProductieDatumsButton.TabIndex = 5;
            this.xProductieDatumsButton.Text = "Productie Datums";
            this.xProductieDatumsButton.UseVisualStyleBackColor = true;
            this.xProductieDatumsButton.Click += new System.EventHandler(this.xProductieDatumsButton_Click);
            // 
            // xNotitieHtmlPanel
            // 
            this.xNotitieHtmlPanel.AutoScroll = true;
            this.xNotitieHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(756, 17);
            this.xNotitieHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xNotitieHtmlPanel.BaseStylesheet = null;
            this.xNotitieHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xNotitieHtmlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xNotitieHtmlPanel.Location = new System.Drawing.Point(5, 134);
            this.xNotitieHtmlPanel.Name = "xNotitieHtmlPanel";
            this.xNotitieHtmlPanel.Size = new System.Drawing.Size(756, 25);
            this.xNotitieHtmlPanel.TabIndex = 11;
            this.xNotitieHtmlPanel.Text = "Notitie Html Text";
            this.xNotitieHtmlPanel.Visible = false;
            this.xNotitieHtmlPanel.ImageLoad += new System.EventHandler<HtmlRenderer.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            this.xNotitieHtmlPanel.DoubleClick += new System.EventHandler(this.xNotitieButton_Click);
            // 
            // xNotitieButton
            // 
            this.xNotitieButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.xNotitieButton.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.xNotitieButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xNotitieButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xNotitieButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xNotitieButton.Image = global::ProductieManager.Properties.Resources.Navigate_down_36747;
            this.xNotitieButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xNotitieButton.Location = new System.Drawing.Point(5, 109);
            this.xNotitieButton.Name = "xNotitieButton";
            this.xNotitieButton.Size = new System.Drawing.Size(756, 25);
            this.xNotitieButton.TabIndex = 12;
            this.xNotitieButton.Text = "Notities";
            this.xNotitieButton.UseVisualStyleBackColor = true;
            this.xNotitieButton.Click += new System.EventHandler(this.xNotitieButton_Click);
            // 
            // xInforHtmlPanel
            // 
            this.xInforHtmlPanel.AutoScroll = true;
            this.xInforHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(756, 17);
            this.xInforHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xInforHtmlPanel.BaseStylesheet = null;
            this.xInforHtmlPanel.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.xInforHtmlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xInforHtmlPanel.Location = new System.Drawing.Point(5, 83);
            this.xInforHtmlPanel.Name = "xInforHtmlPanel";
            this.xInforHtmlPanel.Padding = new System.Windows.Forms.Padding(5);
            this.xInforHtmlPanel.Size = new System.Drawing.Size(756, 26);
            this.xInforHtmlPanel.TabIndex = 2;
            this.xInforHtmlPanel.Text = "Info Html Text";
            this.xInforHtmlPanel.Visible = false;
            this.xInforHtmlPanel.ImageLoad += new System.EventHandler<HtmlRenderer.Entities.HtmlImageLoadEventArgs>(this.xVerpakkingHtmlPanel_ImageLoad);
            this.xInforHtmlPanel.DoubleClick += new System.EventHandler(this.xProductieInfoButton_Click);
            // 
            // xProductieInfoButton
            // 
            this.xProductieInfoButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.xProductieInfoButton.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.xProductieInfoButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xProductieInfoButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xProductieInfoButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xProductieInfoButton.Image = global::ProductieManager.Properties.Resources.Navigate_down_36747;
            this.xProductieInfoButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xProductieInfoButton.Location = new System.Drawing.Point(5, 58);
            this.xProductieInfoButton.Name = "xProductieInfoButton";
            this.xProductieInfoButton.Size = new System.Drawing.Size(756, 25);
            this.xProductieInfoButton.TabIndex = 1;
            this.xProductieInfoButton.Text = "Productie Info";
            this.xProductieInfoButton.UseVisualStyleBackColor = true;
            this.xProductieInfoButton.Click += new System.EventHandler(this.xProductieInfoButton_Click);
            // 
            // xHeaderHtmlPanel
            // 
            this.xHeaderHtmlPanel.AutoScroll = true;
            this.xHeaderHtmlPanel.AutoScrollMinSize = new System.Drawing.Size(756, 17);
            this.xHeaderHtmlPanel.BackColor = System.Drawing.SystemColors.Window;
            this.xHeaderHtmlPanel.BaseStylesheet = null;
            this.xHeaderHtmlPanel.Cursor = System.Windows.Forms.Cursors.Default;
            this.xHeaderHtmlPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xHeaderHtmlPanel.Location = new System.Drawing.Point(5, 30);
            this.xHeaderHtmlPanel.Name = "xHeaderHtmlPanel";
            this.xHeaderHtmlPanel.Size = new System.Drawing.Size(756, 28);
            this.xHeaderHtmlPanel.TabIndex = 0;
            this.xHeaderHtmlPanel.Text = "Header Html Text";
            this.xHeaderHtmlPanel.Visible = false;
            this.xHeaderHtmlPanel.DoubleClick += new System.EventHandler(this.xProductieStatusButton_Click);
            // 
            // xProductieStatusButton
            // 
            this.xProductieStatusButton.Dock = System.Windows.Forms.DockStyle.Top;
            this.xProductieStatusButton.FlatAppearance.BorderColor = System.Drawing.Color.CornflowerBlue;
            this.xProductieStatusButton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xProductieStatusButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xProductieStatusButton.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xProductieStatusButton.Image = global::ProductieManager.Properties.Resources.Navigate_down_36747;
            this.xProductieStatusButton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xProductieStatusButton.Location = new System.Drawing.Point(5, 5);
            this.xProductieStatusButton.Name = "xProductieStatusButton";
            this.xProductieStatusButton.Size = new System.Drawing.Size(756, 25);
            this.xProductieStatusButton.TabIndex = 13;
            this.xProductieStatusButton.Text = "Productie Status";
            this.xProductieStatusButton.UseVisualStyleBackColor = true;
            this.xProductieStatusButton.Click += new System.EventHandler(this.xProductieStatusButton_Click);
            // 
            // ProductieInfoUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xpanelcontainer);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ProductieInfoUI";
            this.Size = new System.Drawing.Size(776, 441);
            this.xpanelcontainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel xpanelcontainer;
        private HtmlRenderer.HtmlPanel xWerkPlaatsenHtmlPanel;
        private System.Windows.Forms.Button xWerkPlaatsenButton;
        private HtmlRenderer.HtmlPanel xMaterialenHtmlPanel;
        private System.Windows.Forms.Button xMaterialenButton;
        private HtmlRenderer.HtmlPanel xVerpakkingHtmlPanel;
        private System.Windows.Forms.Button xProductieDatumsButton;
        private HtmlRenderer.HtmlPanel xDatumsHtmlPanel;
        private System.Windows.Forms.Button xVerpakkingsButton;
        private HtmlRenderer.HtmlPanel xInforHtmlPanel;
        private System.Windows.Forms.Button xProductieInfoButton;
        private HtmlRenderer.HtmlPanel xHeaderHtmlPanel;
        private HtmlRenderer.HtmlPanel xNotitieHtmlPanel;
        private System.Windows.Forms.Button xNotitieButton;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xProductieStatusButton;
    }
}
