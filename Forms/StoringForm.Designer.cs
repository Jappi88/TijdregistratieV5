
namespace Forms
{
    partial class StoringForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(StoringForm));
            this.werkPlekStoringen1 = new Controls.WerkPlekStoringen();
            this.SuspendLayout();
            // 
            // werkPlekStoringen1
            // 
            this.werkPlekStoringen1.BackColor = System.Drawing.Color.White;
            this.werkPlekStoringen1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.werkPlekStoringen1.IsCloseAble = true;
            this.werkPlekStoringen1.IsEditAble = true;
            this.werkPlekStoringen1.Location = new System.Drawing.Point(20, 60);
            this.werkPlekStoringen1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.werkPlekStoringen1.Name = "werkPlekStoringen1";
            this.werkPlekStoringen1.Size = new System.Drawing.Size(685, 395);
            this.werkPlekStoringen1.TabIndex = 0;
            // 
            // StoringForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(725, 475);
            this.Controls.Add(this.werkPlekStoringen1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(725, 475);
            this.Name = "StoringForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Storing Form";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StoringenForm_FormClosing);
            this.Shown += new System.EventHandler(this.StoringForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.WerkPlekStoringen werkPlekStoringen1;
    }
}