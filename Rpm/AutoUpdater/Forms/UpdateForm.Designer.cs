using System.Threading;
using TheArtOfDev.HtmlRenderer.WinForms;

namespace AutoUpdaterDotNET
{
    partial class UpdateForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
            this.labelUpdate = new System.Windows.Forms.Label();
            this.buttonUpdate = new System.Windows.Forms.Button();
            this.buttonRemindLater = new System.Windows.Forms.Button();
            this.pictureBoxIcon = new System.Windows.Forms.PictureBox();
            this.buttonSkip = new System.Windows.Forms.Button();
            this.xdescription = new System.Windows.Forms.Label();
            this.xchangelog = new TheArtOfDev.HtmlRenderer.WinForms.HtmlPanel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).BeginInit();
            this.SuspendLayout();
            // 
            // labelUpdate
            // 
            resources.ApplyResources(this.labelUpdate, "labelUpdate");
            this.labelUpdate.Name = "labelUpdate";
            // 
            // buttonUpdate
            // 
            resources.ApplyResources(this.buttonUpdate, "buttonUpdate");
            this.buttonUpdate.Image = global::ProductieManager.Properties.Resources.clouddown_icon_icons_com_54405;
            this.buttonUpdate.Name = "buttonUpdate";
            this.buttonUpdate.UseVisualStyleBackColor = true;
            this.buttonUpdate.Click += new System.EventHandler(this.ButtonUpdateClick);
            // 
            // buttonRemindLater
            // 
            resources.ApplyResources(this.buttonRemindLater, "buttonRemindLater");
            this.buttonRemindLater.FlatAppearance.BorderSize = 2;
            this.buttonRemindLater.Image = global::ProductieManager.Properties.Resources.clock_go;
            this.buttonRemindLater.Name = "buttonRemindLater";
            this.buttonRemindLater.UseVisualStyleBackColor = true;
            this.buttonRemindLater.Click += new System.EventHandler(this.ButtonRemindLaterClick);
            // 
            // pictureBoxIcon
            // 
            this.pictureBoxIcon.Image = global::ProductieManager.Properties.Resources.cloudrefresh_icon_icons_com_54403_256x256;
            resources.ApplyResources(this.pictureBoxIcon, "pictureBoxIcon");
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.TabStop = false;
            // 
            // buttonSkip
            // 
            resources.ApplyResources(this.buttonSkip, "buttonSkip");
            this.buttonSkip.DialogResult = System.Windows.Forms.DialogResult.Abort;
            this.buttonSkip.Image = global::ProductieManager.Properties.Resources.hand_point;
            this.buttonSkip.Name = "buttonSkip";
            this.buttonSkip.UseVisualStyleBackColor = true;
            this.buttonSkip.Click += new System.EventHandler(this.ButtonSkipClick);
            // 
            // xdescription
            // 
            resources.ApplyResources(this.xdescription, "xdescription");
            this.xdescription.Name = "xdescription";
            // 
            // xchangelog
            // 
            resources.ApplyResources(this.xchangelog, "xchangelog");
            this.xchangelog.BackColor = System.Drawing.SystemColors.Window;
            this.xchangelog.BaseStylesheet = null;
            this.xchangelog.Cursor = System.Windows.Forms.Cursors.Default;
            this.xchangelog.Name = "xchangelog";
            this.xchangelog.StylesheetLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlStylesheetLoadEventArgs>(this.xchangelog_StylesheetLoad);
            this.xchangelog.ImageLoad += new System.EventHandler<TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs>(this.xchangelog_ImageLoad);
            // 
            // UpdateForm
            // 
            this.AcceptButton = this.buttonUpdate;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xchangelog);
            this.Controls.Add(this.xdescription);
            this.Controls.Add(this.pictureBoxIcon);
            this.Controls.Add(this.labelUpdate);
            this.Controls.Add(this.buttonUpdate);
            this.Controls.Add(this.buttonSkip);
            this.Controls.Add(this.buttonRemindLater);
            this.DoubleBuffered = true;
            this.MinimizeBox = false;
            this.Name = "UpdateForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.UpdateForm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.UpdateForm_FormClosed);
            this.Shown += new System.EventHandler(this.UpdateForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxIcon)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonRemindLater;
        private System.Windows.Forms.Button buttonUpdate;
        private System.Windows.Forms.Button buttonSkip;
        private System.Windows.Forms.Label labelUpdate;
        private System.Windows.Forms.PictureBox pictureBoxIcon;
        private System.Windows.Forms.Label xdescription;
        private HtmlPanel xchangelog;
    }
}