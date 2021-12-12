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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // chart1
            // 
            chartArea1.AlignmentOrientation = ((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations)((System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Vertical | System.Windows.Forms.DataVisualization.Charting.AreaAlignmentOrientations.Horizontal)));
            chartArea1.Area3DStyle.Enable3D = true;
            chartArea1.Area3DStyle.Inclination = 25;
            chartArea1.Area3DStyle.LightStyle = System.Windows.Forms.DataVisualization.Charting.LightStyle.Realistic;
            chartArea1.Area3DStyle.Rotation = 10;
            chartArea1.Area3DStyle.WallWidth = 3;
            chartArea1.AxisX.LineWidth = 0;
            chartArea1.AxisX2.LineWidth = 0;
            chartArea1.AxisY.LineWidth = 0;
            chartArea1.AxisY2.LineWidth = 0;
            chartArea1.BackColor = System.Drawing.SystemColors.ActiveCaption;
            chartArea1.BackGradientStyle = System.Windows.Forms.DataVisualization.Charting.GradientStyle.VerticalCenter;
            chartArea1.BackImageAlignment = System.Windows.Forms.DataVisualization.Charting.ChartImageAlignmentStyle.TopRight;
            chartArea1.BackImageTransparentColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            chartArea1.BorderWidth = 0;
            chartArea1.CursorX.LineWidth = 0;
            chartArea1.CursorY.LineWidth = 0;
            chartArea1.Name = "ChartArea1";
            chartArea1.ShadowColor = System.Drawing.Color.RosyBrown;
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Dock = System.Windows.Forms.DockStyle.Fill;
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.chart1.Legends.Add(legend1);
            this.chart1.Location = new System.Drawing.Point(10, 10);
            this.chart1.Name = "chart1";
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.SplineArea;
            series1.Legend = "Legend1";
            series1.Name = "Werkplek";
            this.chart1.Series.Add(series1);
            this.chart1.Size = new System.Drawing.Size(798, 191);
            this.chart1.TabIndex = 0;
            this.chart1.Text = "chart1";
            // 
            // WerkPlekChart
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.chart1);
            this.DoubleBuffered = true;
            this.Name = "WerkPlekChart";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(818, 211);
            this.Load += new System.EventHandler(this.WerkPlekChart_Load);
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
    }
}
