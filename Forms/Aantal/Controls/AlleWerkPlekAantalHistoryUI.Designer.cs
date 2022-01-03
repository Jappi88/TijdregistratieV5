namespace Forms.Aantal.Controls
{
    partial class AlleWerkPlekAantalHistoryUI
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
            this.xWerkplekTabs = new MetroFramework.Controls.MetroTabControl();
            this.SuspendLayout();
            // 
            // xWerkplekTabs
            // 
            this.xWerkplekTabs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xWerkplekTabs.Location = new System.Drawing.Point(10, 10);
            this.xWerkplekTabs.Name = "xWerkplekTabs";
            this.xWerkplekTabs.Size = new System.Drawing.Size(909, 488);
            this.xWerkplekTabs.Style = MetroFramework.MetroColorStyle.Green;
            this.xWerkplekTabs.TabIndex = 0;
            this.xWerkplekTabs.UseSelectable = true;
            // 
            // AlleWerkPlekAantalHistoryUI
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.xWerkplekTabs);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "AlleWerkPlekAantalHistoryUI";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Size = new System.Drawing.Size(929, 508);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Controls.MetroTabControl xWerkplekTabs;
    }
}
