namespace Controls
{
    partial class ObjectEditorUI
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
            this.components = new System.ComponentModel.Container();
            this.xFlowPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // xFlowPanel
            // 
            this.xFlowPanel.AutoScroll = true;
            this.xFlowPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xFlowPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.xFlowPanel.Location = new System.Drawing.Point(0, 0);
            this.xFlowPanel.Name = "xFlowPanel";
            this.xFlowPanel.Size = new System.Drawing.Size(670, 367);
            this.xFlowPanel.TabIndex = 0;
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // ObjectEditorUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.xFlowPanel);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ObjectEditorUI";
            this.Size = new System.Drawing.Size(670, 367);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel xFlowPanel;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}
