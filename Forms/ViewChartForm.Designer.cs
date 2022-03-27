
namespace Forms
{
    partial class ViewChartForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewChartForm));
            this.chartView1 = new Controls.ChartView();
            this.SuspendLayout();
            // 
            // chartView1
            // 
            this.chartView1.BackColor = System.Drawing.Color.White;
            resources.ApplyResources(this.chartView1, "chartView1");
            this.chartView1.Name = "chartView1";
            this.chartView1.PeriodeWeergave = true;
            // 
            // ViewChartForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.chartView1);
            this.Name = "ViewChartForm";
            this.Title = "Productie Statistieken ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ViewChartForm_FormClosing);
            this.Load += new System.EventHandler(this.ViewChartForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.ChartView chartView1;
    }
}