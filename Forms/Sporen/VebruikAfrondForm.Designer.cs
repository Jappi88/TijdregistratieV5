namespace Forms.Sporen
{
    partial class VebruikAfrondForm
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.xsporendownlabel = new System.Windows.Forms.Label();
            this.xsporenuplabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Image = global::ProductieManager.Properties.Resources.Navigate_up_36744;
            this.button1.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.Location = new System.Drawing.Point(28, 104);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(264, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "SPOREN HOGER  AFRONDEN";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Image = global::ProductieManager.Properties.Resources.Navigate_down_36747;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(28, 151);
            this.button2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(264, 37);
            this.button2.TabIndex = 2;
            this.button2.Text = "SPOREN LAGER  AFRONDEN";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // xsporendownlabel
            // 
            this.xsporendownlabel.AutoSize = true;
            this.xsporendownlabel.Location = new System.Drawing.Point(24, 193);
            this.xsporendownlabel.Name = "xsporendownlabel";
            this.xsporendownlabel.Size = new System.Drawing.Size(131, 21);
            this.xsporendownlabel.TabIndex = 3;
            this.xsporendownlabel.Text = "Afgerond Benden";
            // 
            // xsporenuplabel
            // 
            this.xsporenuplabel.AutoSize = true;
            this.xsporenuplabel.Location = new System.Drawing.Point(24, 76);
            this.xsporenuplabel.Name = "xsporenuplabel";
            this.xsporenuplabel.Size = new System.Drawing.Size(122, 21);
            this.xsporenuplabel.TabIndex = 4;
            this.xsporenuplabel.Text = "Afgerond Boven";
            // 
            // VebruikAfrondForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(539, 245);
            this.Controls.Add(this.xsporenuplabel);
            this.Controls.Add(this.xsporendownlabel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "VebruikAfrondForm";
            this.Padding = new System.Windows.Forms.Padding(30, 97, 30, 32);
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Aantal Afronden";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label xsporendownlabel;
        private System.Windows.Forms.Label xsporenuplabel;
    }
}