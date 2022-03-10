namespace Controls
{
    partial class Tile
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
            this.SuspendLayout();
            // 
            // Tile
            // 
            this.Size = new System.Drawing.Size(692, 368);
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.GiveFeedback += new System.Windows.Forms.GiveFeedbackEventHandler(this.Tile_GiveFeedback);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.TileMouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.TileMouseMove);
            this.ResumeLayout(false);

        }

        #endregion
    }
}
