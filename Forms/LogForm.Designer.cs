
namespace Forms
{
    partial class LogForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LogForm));
            this.realtimeLog1 = new Controls.RealtimeLog();
            this.SuspendLayout();
            // 
            // realtimeLog1
            // 
            this.realtimeLog1.BackColor = System.Drawing.Color.White;
            this.realtimeLog1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.realtimeLog1.IsCloseAble = true;
            this.realtimeLog1.Location = new System.Drawing.Point(20, 60);
            this.realtimeLog1.Name = "realtimeLog1";
            this.realtimeLog1.ReadInterval = 1000;
            this.realtimeLog1.Size = new System.Drawing.Size(560, 370);
            this.realtimeLog1.TabIndex = 0;
            this.realtimeLog1.OnCloseButtonPressed += new System.EventHandler(this.realtimeLog1_OnCloseButtonPressed);
            // 
            // LogForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 450);
            this.Controls.Add(this.realtimeLog1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(600, 450);
            this.Name = "LogForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Productie Logs ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LogForm_FormClosing);
            this.Shown += new System.EventHandler(this.LogForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.RealtimeLog realtimeLog1;
    }
}