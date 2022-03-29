
using System.Windows.Forms;

namespace Forms.ImageViewer
{
    partial class ImageViewerForm
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
            this.xFlowImagePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.xMainImage = new Forms.ImageViewer.ImageBox();
            this.xnavigateleft = new System.Windows.Forms.Button();
            this.xleftpanel = new System.Windows.Forms.TableLayoutPanel();
            this.xrechtpanel = new System.Windows.Forms.TableLayoutPanel();
            this.xnavigateright = new System.Windows.Forms.Button();
            this.xleftpanel.SuspendLayout();
            this.xrechtpanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // xFlowImagePanel
            // 
            this.xFlowImagePanel.AutoScroll = true;
            this.xFlowImagePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xFlowImagePanel.Location = new System.Drawing.Point(10, 470);
            this.xFlowImagePanel.Name = "xFlowImagePanel";
            this.xFlowImagePanel.Padding = new System.Windows.Forms.Padding(5);
            this.xFlowImagePanel.Size = new System.Drawing.Size(823, 125);
            this.xFlowImagePanel.TabIndex = 0;
            this.xFlowImagePanel.WrapContents = false;
            // 
            // xMainImage
            // 
            this.xMainImage.AutoScroll = true;
            this.xMainImage.AutoSize = false;
            this.xMainImage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.xMainImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xMainImage.GridColor = System.Drawing.Color.Transparent;
            this.xMainImage.GridColorAlternate = System.Drawing.Color.Transparent;
            this.xMainImage.Location = new System.Drawing.Point(47, 60);
            this.xMainImage.Name = "xMainImage";
            this.xMainImage.Size = new System.Drawing.Size(749, 410);
            this.xMainImage.TabIndex = 0;
            this.xMainImage.MouseMove += new System.Windows.Forms.MouseEventHandler(this.xMainImage_MouseMove);
            // 
            // xnavigateleft
            // 
            this.xnavigateleft.FlatAppearance.BorderSize = 0;
            this.xnavigateleft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xnavigateleft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xnavigateleft.Image = global::ProductieManager.Properties.Resources.Navigate_left_36746;
            this.xnavigateleft.Location = new System.Drawing.Point(3, 160);
            this.xnavigateleft.Name = "xnavigateleft";
            this.xnavigateleft.Size = new System.Drawing.Size(31, 90);
            this.xnavigateleft.TabIndex = 0;
            this.xnavigateleft.UseVisualStyleBackColor = true;
            this.xnavigateleft.Click += new System.EventHandler(this.xnavigateleft_Click);
            // 
            // xleftpanel
            // 
            this.xleftpanel.BackColor = System.Drawing.Color.Transparent;
            this.xleftpanel.ColumnCount = 1;
            this.xleftpanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.xleftpanel.Controls.Add(this.xnavigateleft, 0, 1);
            this.xleftpanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.xleftpanel.Location = new System.Drawing.Point(10, 60);
            this.xleftpanel.Name = "xleftpanel";
            this.xleftpanel.RowCount = 3;
            this.xleftpanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.xleftpanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.xleftpanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.xleftpanel.Size = new System.Drawing.Size(37, 410);
            this.xleftpanel.TabIndex = 1;
            this.xleftpanel.Visible = false;
            // 
            // xrechtpanel
            // 
            this.xrechtpanel.BackColor = System.Drawing.Color.Transparent;
            this.xrechtpanel.ColumnCount = 1;
            this.xrechtpanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.xrechtpanel.Controls.Add(this.xnavigateright, 0, 1);
            this.xrechtpanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.xrechtpanel.Location = new System.Drawing.Point(796, 60);
            this.xrechtpanel.Name = "xrechtpanel";
            this.xrechtpanel.RowCount = 3;
            this.xrechtpanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.xrechtpanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.xrechtpanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.xrechtpanel.Size = new System.Drawing.Size(37, 410);
            this.xrechtpanel.TabIndex = 2;
            this.xrechtpanel.Visible = false;
            // 
            // xnavigateright
            // 
            this.xnavigateright.FlatAppearance.BorderSize = 0;
            this.xnavigateright.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xnavigateright.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xnavigateright.Image = global::ProductieManager.Properties.Resources.Navigate_right_36745;
            this.xnavigateright.Location = new System.Drawing.Point(3, 160);
            this.xnavigateright.Name = "xnavigateright";
            this.xnavigateright.Size = new System.Drawing.Size(31, 90);
            this.xnavigateright.TabIndex = 0;
            this.xnavigateright.UseVisualStyleBackColor = true;
            this.xnavigateright.Click += new System.EventHandler(this.xnavigateright_Click);
            // 
            // ImageViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(843, 605);
            this.Controls.Add(this.xMainImage);
            this.Controls.Add(this.xrechtpanel);
            this.Controls.Add(this.xleftpanel);
            this.Controls.Add(this.xFlowImagePanel);
            this.Name = "ImageViewerForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Yellow;
            this.Text = "Afbeeldingen";
            this.Title = "Afbeeldingen";
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ImageViewerForm_MouseMove);
            this.xleftpanel.ResumeLayout(false);
            this.xrechtpanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel xFlowImagePanel;
        private ImageBox xMainImage;
        private System.Windows.Forms.Button xnavigateleft;
        private System.Windows.Forms.TableLayoutPanel xleftpanel;
        private System.Windows.Forms.TableLayoutPanel xrechtpanel;
        private System.Windows.Forms.Button xnavigateright;
    }
}