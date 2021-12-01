namespace Forms
{
    partial class LoadingForm
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
            this.xprogressbar = new CircularProgressBar.CircularProgressBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xstatustext = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // xprogressbar
            // 
            this.xprogressbar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xprogressbar.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.xprogressbar.AnimationSpeed = 500;
            this.xprogressbar.BackColor = System.Drawing.Color.Transparent;
            this.xprogressbar.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprogressbar.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xprogressbar.InnerColor = System.Drawing.Color.Transparent;
            this.xprogressbar.InnerMargin = 2;
            this.xprogressbar.InnerWidth = -1;
            this.xprogressbar.Location = new System.Drawing.Point(590, 3);
            this.xprogressbar.MarqueeAnimationSpeed = 1500;
            this.xprogressbar.Name = "xprogressbar";
            this.xprogressbar.OuterColor = System.Drawing.Color.White;
            this.xprogressbar.OuterMargin = -25;
            this.xprogressbar.OuterWidth = 26;
            this.xprogressbar.ProgressColor = System.Drawing.Color.CornflowerBlue;
            this.xprogressbar.ProgressWidth = 15;
            this.xprogressbar.SecondaryFont = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprogressbar.Size = new System.Drawing.Size(125, 120);
            this.xprogressbar.StartAngle = 270;
            this.xprogressbar.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.xprogressbar.SubscriptColor = System.Drawing.Color.Black;
            this.xprogressbar.SubscriptMargin = new System.Windows.Forms.Padding(10, -35, 0, 0);
            this.xprogressbar.SubscriptText = "";
            this.xprogressbar.SuperscriptColor = System.Drawing.Color.Black;
            this.xprogressbar.SuperscriptMargin = new System.Windows.Forms.Padding(10, 35, 0, 0);
            this.xprogressbar.SuperscriptText = "";
            this.xprogressbar.TabIndex = 0;
            this.xprogressbar.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.xprogressbar.Value = 68;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xstatustext);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.xprogressbar);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 30);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(718, 203);
            this.panel1.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xsluiten);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 159);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(718, 44);
            this.panel2.TabIndex = 2;
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(590, 3);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(125, 37);
            this.xsluiten.TabIndex = 0;
            this.xsluiten.Text = "Annuleren";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xstatustext
            // 
            this.xstatustext.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xstatustext.Font = new System.Drawing.Font("Microsoft YaHei", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xstatustext.Location = new System.Drawing.Point(3, 3);
            this.xstatustext.Name = "xstatustext";
            this.xstatustext.Size = new System.Drawing.Size(581, 153);
            this.xstatustext.TabIndex = 3;
            this.xstatustext.Text = "Status...";
            this.xstatustext.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // LoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(758, 253);
            this.Controls.Add(this.panel1);
            this.DisplayHeader = false;
            this.MinimumSize = new System.Drawing.Size(650, 220);
            this.Name = "LoadingForm";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LoadingForm_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CircularProgressBar.CircularProgressBar xprogressbar;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label xstatustext;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xsluiten;
    }
}