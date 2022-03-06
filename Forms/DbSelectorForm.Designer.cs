namespace ProductieManager.Forms
{
    partial class DbSelectorForm
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
            this.tileViewer1 = new Controls.TileViewer();
            this.SuspendLayout();
            // 
            // tileViewer1
            // 
            this.tileViewer1.AllowDrop = true;
            this.tileViewer1.BackColor = System.Drawing.Color.White;
            this.tileViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tileViewer1.EnableSaveTiles = true;
            this.tileViewer1.EnableTileSelection = false;
            this.tileViewer1.EnableTimer = false;
            this.tileViewer1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tileViewer1.Location = new System.Drawing.Point(20, 60);
            this.tileViewer1.MultipleSelections = false;
            this.tileViewer1.Name = "tileViewer1";
            this.tileViewer1.Padding = new System.Windows.Forms.Padding(20);
            this.tileViewer1.Size = new System.Drawing.Size(570, 245);
            this.tileViewer1.TabIndex = 0;
            this.tileViewer1.TileInfoRefresInterval = 10000;
            // 
            // DbSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 325);
            this.Controls.Add(this.tileViewer1);
            this.MinimumSize = new System.Drawing.Size(610, 325);
            this.Name = "DbSelectorForm";
            this.SaveLastSize = false;
            this.Text = "Selecteer Database";
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.TileViewer tileViewer1;
    }
}