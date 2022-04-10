
namespace Forms.Sporen
{
    partial class AfrondForm
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
            this.xsluiten = new System.Windows.Forms.Button();
            this.xhogerRadio = new MetroFramework.Controls.MetroRadioButton();
            this.xlagerRadio = new MetroFramework.Controls.MetroRadioButton();
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.xfactor = new System.Windows.Forms.NumericUpDown();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xfactor)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xok);
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 142);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(346, 43);
            this.panel1.TabIndex = 1;
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xok.Location = new System.Drawing.Point(74, 3);
            this.xok.Margin = new System.Windows.Forms.Padding(4);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(130, 36);
            this.xok.TabIndex = 7;
            this.xok.Text = "&OK";
            this.xok.UseVisualStyleBackColor = true;
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(212, 3);
            this.xsluiten.Margin = new System.Windows.Forms.Padding(4);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(130, 36);
            this.xsluiten.TabIndex = 8;
            this.xsluiten.Text = "Annuleren";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            // 
            // xhogerRadio
            // 
            this.xhogerRadio.AutoSize = true;
            this.xhogerRadio.Checked = true;
            this.xhogerRadio.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.xhogerRadio.Location = new System.Drawing.Point(172, 65);
            this.xhogerRadio.Name = "xhogerRadio";
            this.xhogerRadio.Size = new System.Drawing.Size(167, 25);
            this.xhogerRadio.Style = MetroFramework.MetroColorStyle.Orange;
            this.xhogerRadio.TabIndex = 2;
            this.xhogerRadio.TabStop = true;
            this.xhogerRadio.Text = "HOGER Afronden";
            this.xhogerRadio.UseSelectable = true;
            // 
            // xlagerRadio
            // 
            this.xlagerRadio.AutoSize = true;
            this.xlagerRadio.FontSize = MetroFramework.MetroCheckBoxSize.Tall;
            this.xlagerRadio.Location = new System.Drawing.Point(172, 96);
            this.xlagerRadio.Name = "xlagerRadio";
            this.xlagerRadio.Size = new System.Drawing.Size(161, 25);
            this.xlagerRadio.Style = MetroFramework.MetroColorStyle.Orange;
            this.xlagerRadio.TabIndex = 3;
            this.xlagerRadio.Text = "LAGER Afronden";
            this.xlagerRadio.UseSelectable = true;
            // 
            // metroLabel1
            // 
            this.metroLabel1.AutoSize = true;
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(20, 60);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(138, 25);
            this.metroLabel1.TabIndex = 4;
            this.metroLabel1.Text = "Afronding Factor";
            // 
            // xfactor
            // 
            this.xfactor.DecimalPlaces = 2;
            this.xfactor.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xfactor.Location = new System.Drawing.Point(20, 88);
            this.xfactor.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xfactor.Name = "xfactor";
            this.xfactor.Size = new System.Drawing.Size(140, 33);
            this.xfactor.TabIndex = 5;
            this.xfactor.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.xfactor.ThousandsSeparator = true;
            this.xfactor.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // AfrondForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(386, 205);
            this.Controls.Add(this.xfactor);
            this.Controls.Add(this.metroLabel1);
            this.Controls.Add(this.xlagerRadio);
            this.Controls.Add(this.xhogerRadio);
            this.Controls.Add(this.panel1);
            this.Name = "AfrondForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Afronden";
            this.Title = "Afronden";
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xfactor)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xok;
        private System.Windows.Forms.Button xsluiten;
        private MetroFramework.Controls.MetroRadioButton xhogerRadio;
        private MetroFramework.Controls.MetroRadioButton xlagerRadio;
        private MetroFramework.Controls.MetroLabel metroLabel1;
        private System.Windows.Forms.NumericUpDown xfactor;
    }
}