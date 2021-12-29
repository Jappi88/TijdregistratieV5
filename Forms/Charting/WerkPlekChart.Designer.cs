namespace Forms.Charting
{
    partial class WerkPlekChart
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
            this.xChart = new LiveCharts.WinForms.CartesianChart();
            this.SuspendLayout();
            // 
            // xChart
            // 
            this.xChart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xChart.Location = new System.Drawing.Point(10, 10);
            this.xChart.Name = "xChart";
            this.xChart.Size = new System.Drawing.Size(798, 191);
            this.xChart.TabIndex = 0;
            this.xChart.Text = "cartesianChart1";
            // 
            // WerkPlekChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xChart);
            this.DoubleBuffered = true;
            this.Name = "WerkPlekChart";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(818, 211);
            this.ResumeLayout(false);

        }

        #endregion

        private LiveCharts.WinForms.CartesianChart xChart;
    }
}
