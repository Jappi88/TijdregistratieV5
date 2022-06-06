
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.xok = new System.Windows.Forms.Button();
            this.xannuleren = new System.Windows.Forms.Button();
            this.productieListControl1 = new Controls.ProductieListControl();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xok);
            this.panel1.Controls.Add(this.xannuleren);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(10, 473);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(860, 41);
            this.panel1.TabIndex = 1;
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xok.Location = new System.Drawing.Point(663, 3);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(94, 34);
            this.xok.TabIndex = 1;
            this.xok.Text = "OK";
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // xannuleren
            // 
            this.xannuleren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xannuleren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xannuleren.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xannuleren.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xannuleren.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xannuleren.Location = new System.Drawing.Point(763, 3);
            this.xannuleren.Name = "xannuleren";
            this.xannuleren.Size = new System.Drawing.Size(94, 34);
            this.xannuleren.TabIndex = 0;
            this.xannuleren.Text = "Sluiten";
            this.xannuleren.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xannuleren.UseVisualStyleBackColor = true;
            this.xannuleren.Click += new System.EventHandler(this.xannuleren_Click);
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
            this.productieListControl1.RemoveCustomItemIfNotValid = true;
            this.productieListControl1.SelectedItem = null;
            this.productieListControl1.ShowWaitUI = true;
            this.productieListControl1.Size = new System.Drawing.Size(860, 413);
            this.productieListControl1.TabIndex = 2;
            this.productieListControl1.ValidHandler = null;
            // 
            // BewerkingSelectorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(880, 524);
            this.Controls.Add(this.productieListControl1);
            this.Controls.Add(this.panel1);
            this.Name = "BewerkingSelectorForm";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Selecteer Bewerkingen";
            this.Title = "Selecteer Bewerkingen";
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xannuleren;
        private System.Windows.Forms.Button xok;
        private Controls.ProductieListControl productieListControl1;
    }
}