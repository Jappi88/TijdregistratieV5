using TheArtOfDev.HtmlRenderer.WinForms;
using TheArtOfDev.HtmlRenderer.Core.Entities;

namespace Controls
{
    partial class WerkplekIndeling
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
            this.xVerwijderPersoneel = new System.Windows.Forms.Button();
            this.xpanel = new System.Windows.Forms.Panel();
            this.xcompactinfo = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.xmiscPanel = new System.Windows.Forms.Panel();
            this.xcompact = new System.Windows.Forms.Button();
            this.xsettings = new System.Windows.Forms.Button();
            this.xresetindeling = new System.Windows.Forms.Button();
            this.xknoppenpanel = new System.Windows.Forms.Panel();
            this.xStartKlus = new System.Windows.Forms.Button();
            this.xStopKlus = new System.Windows.Forms.Button();
            this.xVerwijderKlus = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xnietingedeeld = new MetroFramework.Controls.MetroCheckBox();
            this.xpersoonInfo = new TheArtOfDev.HtmlRenderer.WinForms.HtmlLabel();
            this.xpanel.SuspendLayout();
            this.xmiscPanel.SuspendLayout();
            this.xknoppenpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // xVerwijderPersoneel
            // 
            this.xVerwijderPersoneel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xVerwijderPersoneel.BackColor = System.Drawing.Color.Transparent;
            this.xVerwijderPersoneel.FlatAppearance.BorderSize = 0;
            this.xVerwijderPersoneel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xVerwijderPersoneel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xVerwijderPersoneel.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xVerwijderPersoneel.Location = new System.Drawing.Point(117, 5);
            this.xVerwijderPersoneel.Name = "xVerwijderPersoneel";
            this.xVerwijderPersoneel.Size = new System.Drawing.Size(34, 34);
            this.xVerwijderPersoneel.TabIndex = 3;
            this.toolTip1.SetToolTip(this.xVerwijderPersoneel, "Verwijder Werkplaats");
            this.xVerwijderPersoneel.UseVisualStyleBackColor = false;
            this.xVerwijderPersoneel.Click += new System.EventHandler(this.xVerwijderPersoneel_Click);
            // 
            // xpanel
            // 
            this.xpanel.BackColor = System.Drawing.Color.Transparent;
            this.xpanel.Controls.Add(this.xcompactinfo);
            this.xpanel.Controls.Add(this.xmiscPanel);
            this.xpanel.Controls.Add(this.xknoppenpanel);
            this.xpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xpanel.Location = new System.Drawing.Point(0, 120);
            this.xpanel.Name = "xpanel";
            this.xpanel.Size = new System.Drawing.Size(467, 44);
            this.xpanel.TabIndex = 2;
            this.xpanel.Click += new System.EventHandler(this.xPersoonImage_Click);
            this.xpanel.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.xpersoonInfo_GiveFeedback);
            this.xpanel.DoubleClick += new System.EventHandler(this.xPersoonImage_DoubleClick);
            this.xpanel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IndelingMouseDown);
            this.xpanel.MouseEnter += new System.EventHandler(this.xPersoonImage_MouseEnter);
            this.xpanel.MouseLeave += new System.EventHandler(this.xPersoonImage_MouseLeave);
            this.xpanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.IndelingMouseMove);
            // 
            // xcompactinfo
            // 
            this.xcompactinfo.AutoSize = false;
            this.xcompactinfo.BackColor = System.Drawing.Color.Transparent;
            this.xcompactinfo.BaseStylesheet = null;
            this.xcompactinfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xcompactinfo.IsContextMenuEnabled = false;
            this.xcompactinfo.IsSelectionEnabled = false;
            this.xcompactinfo.Location = new System.Drawing.Point(158, 0);
            this.xcompactinfo.Name = "xcompactinfo";
            this.xcompactinfo.Size = new System.Drawing.Size(154, 44);
            this.xcompactinfo.TabIndex = 7;
            this.xcompactinfo.Text = null;
            this.xcompactinfo.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xpersoonInfo_ImageLoad);
            this.xcompactinfo.Click += new System.EventHandler(this.xPersoonImage_Click);
            this.xcompactinfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.PersoonIndeling_DragDrop);
            this.xcompactinfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.PersoonIndeling_DragEnter);
            this.xcompactinfo.DragLeave += new System.EventHandler(this.PersoonIndeling_DragLeave);
            this.xcompactinfo.DoubleClick += new System.EventHandler(this.xPersoonImage_DoubleClick);
            this.xcompactinfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IndelingMouseDown);
            this.xcompactinfo.MouseEnter += new System.EventHandler(this.xPersoonImage_MouseEnter);
            this.xcompactinfo.MouseLeave += new System.EventHandler(this.xPersoonImage_MouseLeave);
            this.xcompactinfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.IndelingMouseMove);
            // 
            // xmiscPanel
            // 
            this.xmiscPanel.Controls.Add(this.xcompact);
            this.xmiscPanel.Controls.Add(this.xsettings);
            this.xmiscPanel.Controls.Add(this.xVerwijderPersoneel);
            this.xmiscPanel.Controls.Add(this.xresetindeling);
            this.xmiscPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.xmiscPanel.Location = new System.Drawing.Point(312, 0);
            this.xmiscPanel.Name = "xmiscPanel";
            this.xmiscPanel.Size = new System.Drawing.Size(155, 44);
            this.xmiscPanel.TabIndex = 7;
            this.xmiscPanel.Click += new System.EventHandler(this.xPersoonImage_Click);
            this.xmiscPanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.PersoonIndeling_DragDrop);
            this.xmiscPanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.PersoonIndeling_DragEnter);
            this.xmiscPanel.MouseEnter += new System.EventHandler(this.xPersoonImage_MouseEnter);
            this.xmiscPanel.MouseLeave += new System.EventHandler(this.xPersoonImage_MouseLeave);
            // 
            // xcompact
            // 
            this.xcompact.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xcompact.BackColor = System.Drawing.Color.Transparent;
            this.xcompact.FlatAppearance.BorderSize = 0;
            this.xcompact.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xcompact.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xcompact.Image = global::ProductieManager.Properties.Resources.icons8_collapse_32;
            this.xcompact.Location = new System.Drawing.Point(6, 6);
            this.xcompact.Name = "xcompact";
            this.xcompact.Size = new System.Drawing.Size(34, 34);
            this.xcompact.TabIndex = 8;
            this.xcompact.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xcompact, "Compact mode");
            this.xcompact.UseVisualStyleBackColor = false;
            this.xcompact.Click += new System.EventHandler(this.xcompact_Click);
            // 
            // xsettings
            // 
            this.xsettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsettings.BackColor = System.Drawing.Color.Transparent;
            this.xsettings.FlatAppearance.BorderSize = 0;
            this.xsettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xsettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsettings.Image = global::ProductieManager.Properties.Resources.ccsm_103993;
            this.xsettings.Location = new System.Drawing.Point(40, 5);
            this.xsettings.Name = "xsettings";
            this.xsettings.Size = new System.Drawing.Size(34, 34);
            this.xsettings.TabIndex = 9;
            this.xsettings.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xsettings, "Werkplaats instellingen");
            this.xsettings.UseVisualStyleBackColor = false;
            this.xsettings.Click += new System.EventHandler(this.xsettings_Click);
            // 
            // xresetindeling
            // 
            this.xresetindeling.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xresetindeling.BackColor = System.Drawing.Color.Transparent;
            this.xresetindeling.FlatAppearance.BorderSize = 0;
            this.xresetindeling.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xresetindeling.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xresetindeling.Image = global::ProductieManager.Properties.Resources.refresh_arrow_1546;
            this.xresetindeling.Location = new System.Drawing.Point(79, 5);
            this.xresetindeling.Name = "xresetindeling";
            this.xresetindeling.Size = new System.Drawing.Size(34, 34);
            this.xresetindeling.TabIndex = 7;
            this.xresetindeling.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xresetindeling, "Reset Indeling");
            this.xresetindeling.UseVisualStyleBackColor = false;
            this.xresetindeling.Click += new System.EventHandler(this.xresetindeling_Click);
            // 
            // xknoppenpanel
            // 
            this.xknoppenpanel.Controls.Add(this.xStartKlus);
            this.xknoppenpanel.Controls.Add(this.xStopKlus);
            this.xknoppenpanel.Controls.Add(this.xVerwijderKlus);
            this.xknoppenpanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.xknoppenpanel.Location = new System.Drawing.Point(0, 0);
            this.xknoppenpanel.Name = "xknoppenpanel";
            this.xknoppenpanel.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.xknoppenpanel.Size = new System.Drawing.Size(158, 44);
            this.xknoppenpanel.TabIndex = 7;
            this.xknoppenpanel.Visible = false;
            this.xknoppenpanel.Click += new System.EventHandler(this.xPersoonImage_Click);
            this.xknoppenpanel.DragDrop += new System.Windows.Forms.DragEventHandler(this.PersoonIndeling_DragDrop);
            this.xknoppenpanel.DragEnter += new System.Windows.Forms.DragEventHandler(this.PersoonIndeling_DragEnter);
            this.xknoppenpanel.DragLeave += new System.EventHandler(this.PersoonIndeling_DragLeave);
            this.xknoppenpanel.DoubleClick += new System.EventHandler(this.xPersoonImage_DoubleClick);
            // 
            // xStartKlus
            // 
            this.xStartKlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xStartKlus.BackColor = System.Drawing.Color.Transparent;
            this.xStartKlus.FlatAppearance.BorderSize = 0;
            this.xStartKlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xStartKlus.Image = global::ProductieManager.Properties.Resources.play_button_icon_icons_com_60615;
            this.xStartKlus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xStartKlus.Location = new System.Drawing.Point(108, 0);
            this.xStartKlus.Name = "xStartKlus";
            this.xStartKlus.Size = new System.Drawing.Size(44, 44);
            this.xStartKlus.TabIndex = 0;
            this.xStartKlus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xStartKlus, "Start aan de geselecteerde productie");
            this.xStartKlus.UseVisualStyleBackColor = false;
            this.xStartKlus.Click += new System.EventHandler(this.xStartKlus_Click);
            this.xStartKlus.MouseEnter += new System.EventHandler(this.xPersoonImage_MouseEnter);
            this.xStartKlus.MouseLeave += new System.EventHandler(this.xPersoonImage_MouseLeave);
            // 
            // xStopKlus
            // 
            this.xStopKlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xStopKlus.BackColor = System.Drawing.Color.Transparent;
            this.xStopKlus.FlatAppearance.BorderSize = 0;
            this.xStopKlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xStopKlus.Image = global::ProductieManager.Properties.Resources.stop_red256_24890;
            this.xStopKlus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xStopKlus.Location = new System.Drawing.Point(58, 0);
            this.xStopKlus.Name = "xStopKlus";
            this.xStopKlus.Size = new System.Drawing.Size(44, 44);
            this.xStopKlus.TabIndex = 1;
            this.xStopKlus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xStopKlus, "Stop de geselecteerde productie");
            this.xStopKlus.UseVisualStyleBackColor = false;
            this.xStopKlus.Click += new System.EventHandler(this.xStopKlus_Click);
            this.xStopKlus.MouseEnter += new System.EventHandler(this.xPersoonImage_MouseEnter);
            this.xStopKlus.MouseLeave += new System.EventHandler(this.xPersoonImage_MouseLeave);
            // 
            // xVerwijderKlus
            // 
            this.xVerwijderKlus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xVerwijderKlus.BackColor = System.Drawing.Color.Transparent;
            this.xVerwijderKlus.FlatAppearance.BorderSize = 0;
            this.xVerwijderKlus.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xVerwijderKlus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xVerwijderKlus.Image = global::ProductieManager.Properties.Resources.page_remove_12862;
            this.xVerwijderKlus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xVerwijderKlus.Location = new System.Drawing.Point(8, 0);
            this.xVerwijderKlus.Name = "xVerwijderKlus";
            this.xVerwijderKlus.Size = new System.Drawing.Size(44, 44);
            this.xVerwijderKlus.TabIndex = 2;
            this.xVerwijderKlus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.toolTip1.SetToolTip(this.xVerwijderKlus, "Haal de geselecteerde productie uit de indeling");
            this.xVerwijderKlus.UseVisualStyleBackColor = false;
            this.xVerwijderKlus.Click += new System.EventHandler(this.xVerwijderKlus_Click);
            this.xVerwijderKlus.MouseEnter += new System.EventHandler(this.xPersoonImage_MouseEnter);
            this.xVerwijderKlus.MouseLeave += new System.EventHandler(this.xPersoonImage_MouseLeave);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // xnietingedeeld
            // 
            this.xnietingedeeld.AutoSize = true;
            this.xnietingedeeld.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xnietingedeeld.FontSize = MetroFramework.MetroCheckBoxSize.Medium;
            this.xnietingedeeld.FontWeight = MetroFramework.MetroCheckBoxWeight.Bold;
            this.xnietingedeeld.Location = new System.Drawing.Point(0, 101);
            this.xnietingedeeld.Name = "xnietingedeeld";
            this.xnietingedeeld.Size = new System.Drawing.Size(467, 19);
            this.xnietingedeeld.Style = MetroFramework.MetroColorStyle.Blue;
            this.xnietingedeeld.TabIndex = 6;
            this.xnietingedeeld.Text = "Nog In te delen";
            this.toolTip1.SetToolTip(this.xnietingedeeld, "Toon alleen niet ingedeelde producties");
            this.xnietingedeeld.UseSelectable = true;
            this.xnietingedeeld.UseStyleColors = true;
            this.xnietingedeeld.Visible = false;
            this.xnietingedeeld.CheckedChanged += new System.EventHandler(this.xnietingedeeld_CheckedChanged);
            // 
            // xpersoonInfo
            // 
            this.xpersoonInfo.AutoSize = false;
            this.xpersoonInfo.BackColor = System.Drawing.Color.Transparent;
            this.xpersoonInfo.BaseStylesheet = null;
            this.xpersoonInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xpersoonInfo.IsContextMenuEnabled = false;
            this.xpersoonInfo.IsSelectionEnabled = false;
            this.xpersoonInfo.Location = new System.Drawing.Point(0, 0);
            this.xpersoonInfo.Name = "xpersoonInfo";
            this.xpersoonInfo.Size = new System.Drawing.Size(467, 101);
            this.xpersoonInfo.TabIndex = 5;
            this.xpersoonInfo.Text = null;
            this.xpersoonInfo.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xpersoonInfo_ImageLoad);
            this.xpersoonInfo.Click += new System.EventHandler(this.xPersoonImage_Click);
            this.xpersoonInfo.DragDrop += new System.Windows.Forms.DragEventHandler(this.PersoonIndeling_DragDrop);
            this.xpersoonInfo.DragEnter += new System.Windows.Forms.DragEventHandler(this.PersoonIndeling_DragEnter);
            this.xpersoonInfo.DragLeave += new System.EventHandler(this.PersoonIndeling_DragLeave);
            this.xpersoonInfo.DoubleClick += new System.EventHandler(this.xPersoonImage_DoubleClick);
            this.xpersoonInfo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.IndelingMouseDown);
            this.xpersoonInfo.MouseEnter += new System.EventHandler(this.xPersoonImage_MouseEnter);
            this.xpersoonInfo.MouseLeave += new System.EventHandler(this.xPersoonImage_MouseLeave);
            this.xpersoonInfo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.IndelingMouseMove);
            // 
            // WerkplekIndeling
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xpersoonInfo);
            this.Controls.Add(this.xnietingedeeld);
            this.Controls.Add(this.xpanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WerkplekIndeling";
            this.Size = new System.Drawing.Size(467, 164);
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.PersoonIndeling_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.PersoonIndeling_DragEnter);
            this.DragLeave += new System.EventHandler(this.PersoonIndeling_DragLeave);
            this.xpanel.ResumeLayout(false);
            this.xmiscPanel.ResumeLayout(false);
            this.xknoppenpanel.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Panel xpanel;
        private System.Windows.Forms.Button xVerwijderKlus;
        private System.Windows.Forms.Button xStopKlus;
        private System.Windows.Forms.Button xStartKlus;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Button xVerwijderPersoneel;
        private HtmlLabel xpersoonInfo;
        private MetroFramework.Controls.MetroCheckBox xnietingedeeld;
        private System.Windows.Forms.Button xresetindeling;
        private System.Windows.Forms.Button xcompact;
        private System.Windows.Forms.Panel xknoppenpanel;
        private HtmlLabel xcompactinfo;
        private System.Windows.Forms.Panel xmiscPanel;
        private System.Windows.Forms.Button xsettings;
    }
}
