namespace Forms
{
    partial class AantalMonitorForm
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
            this.xChart = new LiveCharts.WinForms.CartesianChart();
            this.SuspendLayout();
            // 
            // xChart
            // 
            this.xChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xChart.Location = new System.Drawing.Point(20, 60);
            this.xChart.Name = "xChart";
            this.xChart.Size = new System.Drawing.Size(991, 469);
            this.xChart.TabIndex = 0;
            this.xChart.Text = "cartesianChart1";
            // 
            // AantalMonitorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1031, 549);
            this.Controls.Add(this.xChart);
            this.Name = "AantalMonitorForm";
            this.Text = "Aantal Monitor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.AantalMonitorForm_FormClosing);
            this.Shown += new System.EventHandler(this.AantalMonitorForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart xChart;
    }
}