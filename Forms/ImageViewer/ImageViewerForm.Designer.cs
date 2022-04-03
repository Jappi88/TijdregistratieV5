
using System.Windows.Forms;
using MetroFramework;

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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageViewerForm));
            this.xFlowImagePanel = new System.Windows.Forms.FlowLayoutPanel();
            this.xMainImage = new Forms.ImageViewer.ImageBox();
            this.xnavigateleft = new System.Windows.Forms.Button();
            this.xleftpanel = new System.Windows.Forms.TableLayoutPanel();
            this.xrechtpanel = new System.Windows.Forms.TableLayoutPanel();
            this.xnavigateright = new System.Windows.Forms.Button();
            this.metroStyleManager1 = new MetroFramework.Components.MetroStyleManager(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xrechtbutton = new System.Windows.Forms.Button();
            this.xopeninexplorer = new System.Windows.Forms.Button();
            this.xrotateleft = new System.Windows.Forms.Button();
            this.xrotaterecht = new System.Windows.Forms.Button();
            this.xleftbutton = new System.Windows.Forms.Button();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.xleftpanel.SuspendLayout();
            this.xrechtpanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).BeginInit();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // xFlowImagePanel
            // 
            this.xFlowImagePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xFlowImagePanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xFlowImagePanel.Location = new System.Drawing.Point(47, 420);
            this.xFlowImagePanel.Name = "xFlowImagePanel";
            this.xFlowImagePanel.Padding = new System.Windows.Forms.Padding(5);
            this.xFlowImagePanel.Size = new System.Drawing.Size(875, 135);
            this.xFlowImagePanel.TabIndex = 0;
            this.xFlowImagePanel.WrapContents = false;
            // 
            // xMainImage
            // 
            this.xMainImage.AutoScroll = true;
            this.xMainImage.AutoSize = false;
            this.xMainImage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xMainImage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.xMainImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xMainImage.GridColor = System.Drawing.Color.Transparent;
            this.xMainImage.GridColorAlternate = System.Drawing.Color.Transparent;
            this.xMainImage.Location = new System.Drawing.Point(47, 60);
            this.xMainImage.Name = "xMainImage";
            this.xMainImage.Size = new System.Drawing.Size(875, 322);
            this.xMainImage.TabIndex = 0;
            // 
            // xnavigateleft
            // 
            this.xnavigateleft.FlatAppearance.BorderSize = 0;
            this.xnavigateleft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xnavigateleft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xnavigateleft.Image = global::ProductieManager.Properties.Resources.Navigate_left_36746;
            this.xnavigateleft.Location = new System.Drawing.Point(3, 202);
            this.xnavigateleft.Name = "xnavigateleft";
            this.xnavigateleft.Size = new System.Drawing.Size(31, 90);
            this.xnavigateleft.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xnavigateleft, "Navigeer naar links");
            this.xnavigateleft.UseVisualStyleBackColor = true;
            this.xnavigateleft.Visible = false;
            this.xnavigateleft.Click += new System.EventHandler(this.xnavigateleft_Click);
            // 
            // xleftpanel
            // 
            this.xleftpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
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
            this.xleftpanel.Size = new System.Drawing.Size(37, 495);
            this.xleftpanel.TabIndex = 1;
            // 
            // xrechtpanel
            // 
            this.xrechtpanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xrechtpanel.ColumnCount = 1;
            this.xrechtpanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.xrechtpanel.Controls.Add(this.xnavigateright, 0, 1);
            this.xrechtpanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.xrechtpanel.Location = new System.Drawing.Point(922, 60);
            this.xrechtpanel.Name = "xrechtpanel";
            this.xrechtpanel.RowCount = 3;
            this.xrechtpanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.xrechtpanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 96F));
            this.xrechtpanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.xrechtpanel.Size = new System.Drawing.Size(37, 495);
            this.xrechtpanel.TabIndex = 2;
            // 
            // xnavigateright
            // 
            this.xnavigateright.FlatAppearance.BorderSize = 0;
            this.xnavigateright.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xnavigateright.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xnavigateright.Image = global::ProductieManager.Properties.Resources.Navigate_right_36745;
            this.xnavigateright.Location = new System.Drawing.Point(3, 202);
            this.xnavigateright.Name = "xnavigateright";
            this.xnavigateright.Size = new System.Drawing.Size(31, 90);
            this.xnavigateright.TabIndex = 0;
            this.toolTip1.SetToolTip(this.xnavigateright, "Navigeer naar rechts");
            this.xnavigateright.UseVisualStyleBackColor = true;
            this.xnavigateright.Visible = false;
            this.xnavigateright.Click += new System.EventHandler(this.xnavigateright_Click);
            // 
            // metroStyleManager1
            // 
            this.metroStyleManager1.Owner = null;
            this.metroStyleManager1.Theme = MetroFramework.MetroThemeStyle.Dark;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 165F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(47, 382);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(875, 38);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xrechtbutton);
            this.panel1.Controls.Add(this.xopeninexplorer);
            this.panel1.Controls.Add(this.xrotateleft);
            this.panel1.Controls.Add(this.xrotaterecht);
            this.panel1.Controls.Add(this.xleftbutton);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(358, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(159, 32);
            this.panel1.TabIndex = 0;
            // 
            // xrechtbutton
            // 
            this.xrechtbutton.Dock = System.Windows.Forms.DockStyle.Left;
            this.xrechtbutton.FlatAppearance.BorderSize = 0;
            this.xrechtbutton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xrechtbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xrechtbutton.Image = global::ProductieManager.Properties.Resources.Navigate_right_36745;
            this.xrechtbutton.Location = new System.Drawing.Point(124, 0);
            this.xrechtbutton.Name = "xrechtbutton";
            this.xrechtbutton.Size = new System.Drawing.Size(31, 32);
            this.xrechtbutton.TabIndex = 2;
            this.toolTip1.SetToolTip(this.xrechtbutton, "Navigeer rechts");
            this.xrechtbutton.UseVisualStyleBackColor = true;
            this.xrechtbutton.Click += new System.EventHandler(this.xnavigateright_Click);
            // 
            // xopeninexplorer
            // 
            this.xopeninexplorer.Dock = System.Windows.Forms.DockStyle.Left;
            this.xopeninexplorer.FlatAppearance.BorderSize = 0;
            this.xopeninexplorer.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xopeninexplorer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xopeninexplorer.Image = global::ProductieManager.Properties.Resources.windows_folder_20788;
            this.xopeninexplorer.Location = new System.Drawing.Point(93, 0);
            this.xopeninexplorer.Name = "xopeninexplorer";
            this.xopeninexplorer.Size = new System.Drawing.Size(31, 32);
            this.xopeninexplorer.TabIndex = 5;
            this.toolTip1.SetToolTip(this.xopeninexplorer, "Open afbeelding in Windows Foto");
            this.xopeninexplorer.UseVisualStyleBackColor = true;
            this.xopeninexplorer.Click += new System.EventHandler(this.xopeninexplorer_Click);
            // 
            // xrotateleft
            // 
            this.xrotateleft.Dock = System.Windows.Forms.DockStyle.Left;
            this.xrotateleft.FlatAppearance.BorderSize = 0;
            this.xrotateleft.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xrotateleft.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xrotateleft.Image = ((System.Drawing.Image)(resources.GetObject("xrotateleft.Image")));
            this.xrotateleft.Location = new System.Drawing.Point(62, 0);
            this.xrotateleft.Name = "xrotateleft";
            this.xrotateleft.Size = new System.Drawing.Size(31, 32);
            this.xrotateleft.TabIndex = 4;
            this.toolTip1.SetToolTip(this.xrotateleft, "Rotate links");
            this.xrotateleft.UseVisualStyleBackColor = true;
            this.xrotateleft.Click += new System.EventHandler(this.xrotateleft_Click);
            // 
            // xrotaterecht
            // 
            this.xrotaterecht.Dock = System.Windows.Forms.DockStyle.Left;
            this.xrotaterecht.FlatAppearance.BorderSize = 0;
            this.xrotaterecht.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xrotaterecht.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xrotaterecht.Image = ((System.Drawing.Image)(resources.GetObject("xrotaterecht.Image")));
            this.xrotaterecht.Location = new System.Drawing.Point(31, 0);
            this.xrotaterecht.Name = "xrotaterecht";
            this.xrotaterecht.Size = new System.Drawing.Size(31, 32);
            this.xrotaterecht.TabIndex = 3;
            this.toolTip1.SetToolTip(this.xrotaterecht, "Rotate rechts");
            this.xrotaterecht.UseVisualStyleBackColor = true;
            this.xrotaterecht.Click += new System.EventHandler(this.xrotaterecht_Click);
            // 
            // xleftbutton
            // 
            this.xleftbutton.Dock = System.Windows.Forms.DockStyle.Left;
            this.xleftbutton.FlatAppearance.BorderSize = 0;
            this.xleftbutton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.AliceBlue;
            this.xleftbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xleftbutton.Image = global::ProductieManager.Properties.Resources.Navigate_left_36746;
            this.xleftbutton.Location = new System.Drawing.Point(0, 0);
            this.xleftbutton.Name = "xleftbutton";
            this.xleftbutton.Size = new System.Drawing.Size(31, 32);
            this.xleftbutton.TabIndex = 1;
            this.toolTip1.SetToolTip(this.xleftbutton, "Navigeer links");
            this.xleftbutton.UseVisualStyleBackColor = true;
            this.xleftbutton.Click += new System.EventHandler(this.xnavigateleft_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // ImageViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(969, 565);
            this.Controls.Add(this.xMainImage);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.xFlowImagePanel);
            this.Controls.Add(this.xrechtpanel);
            this.Controls.Add(this.xleftpanel);
            this.Name = "ImageViewerForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.StyleManager = this.metroStyleManager1;
            this.Text = "Afbeeldingen";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.Title = "Afbeeldingen";
            this.TransparencyKey = System.Drawing.Color.White;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageViewerForm_FormClosing);
            this.Shown += new System.EventHandler(this.ImageViewerForm_Shown);
            this.Resize += new System.EventHandler(this.frmNewForm_ResizeEnd);
            this.xleftpanel.ResumeLayout(false);
            this.xrechtpanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.metroStyleManager1)).EndInit();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel xFlowImagePanel;
        private ImageBox xMainImage;
        private System.Windows.Forms.Button xnavigateleft;
        private System.Windows.Forms.TableLayoutPanel xleftpanel;
        private System.Windows.Forms.TableLayoutPanel xrechtpanel;
        private System.Windows.Forms.Button xnavigateright;
        private MetroFramework.Components.MetroStyleManager metroStyleManager1;
        private TableLayoutPanel tableLayoutPanel1;
        private Panel panel1;
        private Button xrotaterecht;
        private Button xrechtbutton;
        private Button xleftbutton;
        private Button xrotateleft;
        private Button xopeninexplorer;
        private ToolTip toolTip1;
    }
}