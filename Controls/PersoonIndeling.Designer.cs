namespace Controls
{
    partial class PersoonIndeling
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
            this.xpersoonInfo = new HtmlRenderer.HtmlPanel();
            this.xVerwijderPersoneel = new System.Windows.Forms.Button();
            this.xknoppenpanel = new System.Windows.Forms.Panel();
            this.xVerwijderKlus = new System.Windows.Forms.Button();
            this.xStopKlus = new System.Windows.Forms.Button();
            this.xStartKlus = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xpersoonInfo.SuspendLayout();
            this.xknoppenpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // xpersoonInfo
            // 
            this.xpersoonInfo.AutoScroll = true;
            this.xpersoonInfo.AutoScrollMinSize = new System.Drawing.Size(460, 17);
            this.xpersoonInfo.BackColor = System.Drawing.Color.Transparent;
            this.xpersoonInfo.BaseStylesheet = null;
            this.xpersoonInfo.Controls.Add(this.xVerwijderPersoneel);
            this.xpersoonInfo.Cursor = System.Windows.Forms.Cursors.Default;
            this.xpersoonInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xpersoonInfo.IsContextMenuEnabled = false;
            this.xpersoonInfo.IsSelectionEnabled = false;
            this.xpersoonInfo.Location = new System.Drawing.Point(0, 0);
            this.xpersoonInfo.Name = "xpersoonInfo";
            this.xpersoonInfo.Size = new System.Drawing.Size(460, 104);
            this.xpersoonInfo.TabIndex = 1;
            this.xpersoonInfo.Text = "HTMLPanel";
            this.xpersoonInfo.ImageLoad += new System.EventHandler<HtmlRenderer.Entities.HtmlImageLoadEventArgs>(this.xpersoonInfo_ImageLoad);
            this.xpersoonInfo.Click += new System.EventHandler(this.xPersoonImage_Click);
            this.xpersoonInfo.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.xpersoonInfo_GiveFeedback);
            this.xpersoonInfo.DoubleClick += new System.EventHandler(this.xPersoonImage_DoubleClick);
            this.xpersoonInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IndelingMouseDown);
            this.xpersoonInfo.MouseEnter += new System.EventHandler(this.xPersoonImage_MouseEnter);
            this.xpersoonInfo.MouseLeave += new System.EventHandler(this.xPersoonImage_MouseLeave);
            this.xpersoonInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.IndelingMouseMove);
            // 
            // xVerwijderPersoneel
            // 
            this.xVerwijderPersoneel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xVerwijderPersoneel.BackColor = System.Drawing.Color.Transparent;
            this.xVerwijderPersoneel.FlatAppearance.BorderSize = 0;
            this.xVerwijderPersoneel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xVerwijderPersoneel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xVerwijderPersoneel.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xVerwijderPersoneel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xVerwijderPersoneel.Location = new System.Drawing.Point(415, 3);
            this.xVerwijderPersoneel.Name = "xVerwijderPersoneel";
            this.xVerwijderPersoneel.Size = new System.Drawing.Size(42, 29);
            this.xVerwijderPersoneel.TabIndex = 3;
            this.xVerwijderPersoneel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xVerwijderPersoneel, "Verwijder personeel");
            this.xVerwijderPersoneel.UseVisualStyleBackColor = false;
            this.xVerwijderPersoneel.Click += new System.EventHandler(this.xVerwijderPersoneel_Click);
            // 
            // xknoppenpanel
            // 
            this.xknoppenpanel.BackColor = System.Drawing.Color.Transparent;
            this.xknoppenpanel.Controls.Add(this.xVerwijderKlus);
            this.xknoppenpanel.Controls.Add(this.xStopKlus);
            this.xknoppenpanel.Controls.Add(this.xStartKlus);
            this.xknoppenpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xknoppenpanel.Location = new System.Drawing.Point(0, 104);
            this.xknoppenpanel.Name = "xknoppenpanel";
            this.xknoppenpanel.Size = new System.Drawing.Size(460, 36);
            this.xknoppenpanel.TabIndex = 2;
            this.xknoppenpanel.Visible = false;
            this.xknoppenpanel.Click += new System.EventHandler(this.xPersoonImage_Click);
            this.xknoppenpanel.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.xpersoonInfo_GiveFeedback);
            this.xknoppenpanel.DoubleClick += new System.EventHandler(this.xPersoonImage_DoubleClick);
            this.xknoppenpanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IndelingMouseDown);
            this.xknoppenpanel.MouseEnter += new System.EventHandler(this.xPersoonImage_MouseEnter);
            this.xknoppenpanel.MouseLeave += new System.EventHandler(this.xPersoonImage_MouseLeave);
            this.xknoppenpanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.IndelingMouseMove);
            // 
            // xVerwijderKlus
            // 
            this.xVerwijderKlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xVerwijderKlus.BackColor = System.Drawing.Color.White;
            this.xVerwijderKlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xVerwijderKlus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xVerwijderKlus.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xVerwijderKlus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xVerwijderKlus.Location = new System.Drawing.Point(59, 4);
            this.xVerwijderKlus.Name = "xVerwijderKlus";
            this.xVerwijderKlus.Size = new System.Drawing.Size(136, 29);
            this.xVerwijderKlus.TabIndex = 2;
            this.xVerwijderKlus.Text = "Verwijder Klus";
            this.xVerwijderKlus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xVerwijderKlus, "Verwijder klus van de geselecteerde producties");
            this.xVerwijderKlus.UseVisualStyleBackColor = false;
            this.xVerwijderKlus.Click += new System.EventHandler(this.xVerwijderKlus_Click);
            this.xVerwijderKlus.MouseEnter += new System.EventHandler(this.xPersoonImage_MouseEnter);
            this.xVerwijderKlus.MouseLeave += new System.EventHandler(this.xPersoonImage_MouseLeave);
            // 
            // xStopKlus
            // 
            this.xStopKlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xStopKlus.BackColor = System.Drawing.Color.White;
            this.xStopKlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xStopKlus.Image = global::ProductieManager.Properties.Resources.stop_red256_24890;
            this.xStopKlus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xStopKlus.Location = new System.Drawing.Point(201, 4);
            this.xStopKlus.Name = "xStopKlus";
            this.xStopKlus.Size = new System.Drawing.Size(125, 29);
            this.xStopKlus.TabIndex = 1;
            this.xStopKlus.Text = "Stop";
            this.toolTip1.SetToolTip(this.xStopKlus, "Stop aan alle geselecteerde producties");
            this.xStopKlus.UseVisualStyleBackColor = false;
            this.xStopKlus.Click += new System.EventHandler(this.xStopKlus_Click);
            this.xStopKlus.MouseEnter += new System.EventHandler(this.xPersoonImage_MouseEnter);
            this.xStopKlus.MouseLeave += new System.EventHandler(this.xPersoonImage_MouseLeave);
            // 
            // xStartKlus
            // 
            this.xStartKlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xStartKlus.BackColor = System.Drawing.Color.White;
            this.xStartKlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xStartKlus.Image = global::ProductieManager.Properties.Resources.play_button_icon_icons_com_60615;
            this.xStartKlus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xStartKlus.Location = new System.Drawing.Point(332, 4);
            this.xStartKlus.Name = "xStartKlus";
            this.xStartKlus.Size = new System.Drawing.Size(125, 29);
            this.xStartKlus.TabIndex = 0;
            this.xStartKlus.Text = "Start";
            this.toolTip1.SetToolTip(this.xStartKlus, "Start aan de geselecteerde productie");
            this.xStartKlus.UseVisualStyleBackColor = false;
            this.xStartKlus.Click += new System.EventHandler(this.xStartKlus_Click);
            this.xStartKlus.MouseEnter += new System.EventHandler(this.xPersoonImage_MouseEnter);
            this.xStartKlus.MouseLeave += new System.EventHandler(this.xPersoonImage_MouseLeave);
            // 
            // PersoonIndeling
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xpersoonInfo);
            this.Controls.Add(this.xknoppenpanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "PersoonIndeling";
            this.Size = new System.Drawing.Size(460, 140);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.PersoonIndeling_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.PersoonIndeling_DragEnter);
            this.DragLeave += new System.EventHandler(this.PersoonIndeling_DragLeave);
            this.xpersoonInfo.ResumeLayout(false);
            this.xknoppenpanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private HtmlRenderer.HtmlPanel xpersoonInfo;
        private System.Windows.Forms.Panel xknoppenpanel;
        private System.Windows.Forms.Button xVerwijderKlus;
        private System.Windows.Forms.Button xStopKlus;
        private System.Windows.Forms.Button xStartKlus;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button xVerwijderPersoneel;
    }
}
