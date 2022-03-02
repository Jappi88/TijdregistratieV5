namespace Controls
{
    partial class PlannerUI
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
            Calendar.DrawTool drawTool1 = new Calendar.DrawTool();
            this.xsheduler = new Calendar.DayView();
            this.SuspendLayout();
            // 
            // xsheduler
            // 
            drawTool1.DayView = this.xsheduler;
            this.xsheduler.ActiveTool = drawTool1;
            this.xsheduler.AllowInplaceEditing = false;
            this.xsheduler.DaysToShow = 6;
            this.xsheduler.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xsheduler.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.xsheduler.Location = new System.Drawing.Point(0, 0);
            this.xsheduler.Name = "xsheduler";
            this.xsheduler.SelectionEnd = new System.DateTime(((long)(0)));
            this.xsheduler.SelectionStart = new System.DateTime(((long)(0)));
            this.xsheduler.Size = new System.Drawing.Size(972, 597);
            this.xsheduler.StartDate = new System.DateTime(((long)(0)));
            this.xsheduler.TabIndex = 0;
            this.xsheduler.Text = "dayView1";
            // 
            // PlannerUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.xsheduler);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "PlannerUI";
            this.Size = new System.Drawing.Size(972, 597);
            this.ResumeLayout(false);

        }

        #endregion

        private Calendar.DayView xsheduler;
    }
}
