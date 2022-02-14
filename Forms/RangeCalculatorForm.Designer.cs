namespace Forms
{
    partial class RangeCalculatorForm
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
            this.zoekProductiesUI1 = new Controls.ZoekProductiesUI();
            this.SuspendLayout();
            // 
            // zoekProductiesUI1
            // 
            this.zoekProductiesUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.zoekProductiesUI1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.zoekProductiesUI1.Location = new System.Drawing.Point(20, 60);
            this.zoekProductiesUI1.Margin = new System.Windows.Forms.Padding(4);
            this.zoekProductiesUI1.MinimumSize = new System.Drawing.Size(725, 425);
            this.zoekProductiesUI1.Name = "zoekProductiesUI1";
            this.zoekProductiesUI1.Size = new System.Drawing.Size(919, 492);
            this.zoekProductiesUI1.TabIndex = 0;
            this.zoekProductiesUI1.ClosedClicked += new System.EventHandler(this.zoekProductiesUI1_ClosedClicked);
            this.zoekProductiesUI1.StatusTextChanged += new System.EventHandler(this.zoekProductiesUI1_StatusTextChanged);
            // 
            // RangeCalculatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(959, 572);
            this.Controls.Add(this.zoekProductiesUI1);
            this.Name = "RangeCalculatorForm";
            this.Text = "Zoek Producties";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RangeCalculatorForm_FormClosing);
            this.Shown += new System.EventHandler(this.RangeCalculatorForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ZoekProductiesUI zoekProductiesUI1;
    }
}