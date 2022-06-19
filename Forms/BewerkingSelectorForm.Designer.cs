
namespace Forms
{
    partial class BewerkingSelectorForm
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
            this.productieListControl1 = new Controls.ProductieListControl();
            this.SuspendLayout();
            // 
            // productieListControl1
            // 
            this.productieListControl1.AutoScroll = true;
            this.productieListControl1.BackColor = System.Drawing.Color.White;
            this.productieListControl1.CanLoad = false;
            this.productieListControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieListControl1.EnableCheckBox = true;
            this.productieListControl1.EnableContextMenu = false;
            this.productieListControl1.EnableEntryFiltering = true;
            this.productieListControl1.EnableFiltering = false;
            this.productieListControl1.EnableSync = false;
            this.productieListControl1.EnableToolBar = false;
            this.productieListControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieListControl1.IsBewerkingView = true;
            this.productieListControl1.ListName = null;
            this.productieListControl1.Location = new System.Drawing.Point(10, 60);
            this.productieListControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.productieListControl1.Name = "productieListControl1";
            this.productieListControl1.SelectedItem = null;
            this.productieListControl1.ShowWaitUI = true;
            this.productieListControl1.Size = new System.Drawing.Size(860, 454);
            this.productieListControl1.TabIndex = 2;
            this.productieListControl1.ValidHandler = null;
            this.productieListControl1.ItemsChosen += new System.EventHandler(this.xok_Click);
            this.productieListControl1.ItemsCancel += new System.EventHandler(this.xannuleren_Click);
            // 
            // BewerkingSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 524);
            this.Controls.Add(this.productieListControl1);
            this.Name = "BewerkingSelectorForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selecteer Bewerkingen";
            this.Title = "Selecteer Bewerkingen";
            this.ResumeLayout(false);

        }

        #endregion
        private Controls.ProductieListControl productieListControl1;
    }
}