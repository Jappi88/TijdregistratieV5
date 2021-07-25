
namespace Controls
{
    partial class ProgressWait
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
            this.xprogress = new CircularProgressBar.CircularProgressBar();
            this.SuspendLayout();
            // 
            // xprogress
            // 
            this.xprogress.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xprogress.AnimationFunction = WinFormAnimation.KnownAnimationFunctions.Liner;
            this.xprogress.AnimationSpeed = 500;
            this.xprogress.BackColor = System.Drawing.Color.Transparent;
            this.xprogress.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprogress.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.xprogress.InnerColor = System.Drawing.Color.Transparent;
            this.xprogress.InnerMargin = 2;
            this.xprogress.InnerWidth = -1;
            this.xprogress.Location = new System.Drawing.Point(0, 3);
            this.xprogress.MarqueeAnimationSpeed = 1500;
            this.xprogress.Name = "xprogress";
            this.xprogress.OuterColor = System.Drawing.Color.Gray;
            this.xprogress.OuterMargin = -25;
            this.xprogress.OuterWidth = 25;
            this.xprogress.ProgressColor = System.Drawing.Color.CornflowerBlue;
            this.xprogress.ProgressWidth = 25;
            this.xprogress.SecondaryFont = new System.Drawing.Font("Segoe UI", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xprogress.Size = new System.Drawing.Size(272, 272);
            this.xprogress.StartAngle = 270;
            this.xprogress.SubscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.xprogress.SubscriptMargin = new System.Windows.Forms.Padding(20, -35, 0, 0);
            this.xprogress.SubscriptText = "";
            this.xprogress.SuperscriptColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(166)))), ((int)(((byte)(166)))));
            this.xprogress.SuperscriptMargin = new System.Windows.Forms.Padding(0);
            this.xprogress.SuperscriptText = "";
            this.xprogress.TabIndex = 0;
            this.xprogress.Text = "Producties Laden...";
            this.xprogress.TextMargin = new System.Windows.Forms.Padding(8, 8, 0, 0);
            this.xprogress.Value = 68;
            // 
            // ProgressWait
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.xprogress);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "ProgressWait";
            this.Size = new System.Drawing.Size(275, 279);
            this.ResumeLayout(false);

        }

        #endregion

        private CircularProgressBar.CircularProgressBar xprogress;
    }
}
