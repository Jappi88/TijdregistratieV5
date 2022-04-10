
namespace Forms.Sporen
{
    partial class OptimaleVerbruikSettingForm
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
            this.xvoorkeur1group = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.xreststuk = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.xvoorkeur3Lengte = new System.Windows.Forms.NumericUpDown();
            this.xvoorkeur2Lengte = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.xvoorkeur1Lengte = new System.Windows.Forms.NumericUpDown();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.panel1.SuspendLayout();
            this.xvoorkeur1group.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xreststuk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xvoorkeur3Lengte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xvoorkeur2Lengte)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xvoorkeur1Lengte)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xok);
            this.panel1.Controls.Add(this.xsluiten);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(20, 321);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(5);
            this.panel1.Size = new System.Drawing.Size(365, 43);
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
            this.xok.Location = new System.Drawing.Point(119, 3);
            this.xok.Margin = new System.Windows.Forms.Padding(4);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(107, 36);
            this.xok.TabIndex = 7;
            this.xok.TabStop = false;
            this.xok.Text = "OK";
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // xsluiten
            // 
            this.xsluiten.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluiten.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(234, 3);
            this.xsluiten.Margin = new System.Windows.Forms.Padding(4);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(127, 36);
            this.xsluiten.TabIndex = 8;
            this.xsluiten.TabStop = false;
            this.xsluiten.Text = "Annuleren";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            // 
            // xvoorkeur1group
            // 
            this.xvoorkeur1group.Controls.Add(this.label6);
            this.xvoorkeur1group.Controls.Add(this.xreststuk);
            this.xvoorkeur1group.Controls.Add(this.label2);
            this.xvoorkeur1group.Controls.Add(this.label4);
            this.xvoorkeur1group.Controls.Add(this.xvoorkeur3Lengte);
            this.xvoorkeur1group.Controls.Add(this.xvoorkeur2Lengte);
            this.xvoorkeur1group.Controls.Add(this.label1);
            this.xvoorkeur1group.Controls.Add(this.xvoorkeur1Lengte);
            this.xvoorkeur1group.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xvoorkeur1group.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvoorkeur1group.Location = new System.Drawing.Point(20, 60);
            this.xvoorkeur1group.Name = "xvoorkeur1group";
            this.xvoorkeur1group.Size = new System.Drawing.Size(365, 261);
            this.xvoorkeur1group.TabIndex = 2;
            this.xvoorkeur1group.TabStop = false;
            this.xvoorkeur1group.Text = "Voorkeuren";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(7, 135);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(197, 20);
            this.label6.TabIndex = 1;
            this.label6.Text = "Voorkeur3 UitgangsLengte";
            // 
            // xreststuk
            // 
            this.xreststuk.DecimalPlaces = 2;
            this.xreststuk.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xreststuk.Location = new System.Drawing.Point(11, 211);
            this.xreststuk.Maximum = new decimal(new int[] {
            9999,
            0,
            0,
            0});
            this.xreststuk.Name = "xreststuk";
            this.xreststuk.Size = new System.Drawing.Size(122, 27);
            this.xreststuk.TabIndex = 3;
            this.xreststuk.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.metroToolTip1.SetToolTip(this.xreststuk, "Vul in de optimale reststuk");
            this.xreststuk.Enter += new System.EventHandler(this.xvoorkeur1Lengte_Enter);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(7, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Optimale Reststuk";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(7, 82);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(197, 20);
            this.label4.TabIndex = 1;
            this.label4.Text = "Voorkeur2 UitgangsLengte";
            // 
            // xvoorkeur3Lengte
            // 
            this.xvoorkeur3Lengte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xvoorkeur3Lengte.DecimalPlaces = 2;
            this.xvoorkeur3Lengte.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvoorkeur3Lengte.Location = new System.Drawing.Point(11, 158);
            this.xvoorkeur3Lengte.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xvoorkeur3Lengte.Name = "xvoorkeur3Lengte";
            this.xvoorkeur3Lengte.Size = new System.Drawing.Size(348, 27);
            this.xvoorkeur3Lengte.TabIndex = 2;
            this.xvoorkeur3Lengte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.metroToolTip1.SetToolTip(this.xvoorkeur3Lengte, "Vul in de uitgangslengte om vanaf te kijken");
            this.xvoorkeur3Lengte.Enter += new System.EventHandler(this.xvoorkeur1Lengte_Enter);
            // 
            // xvoorkeur2Lengte
            // 
            this.xvoorkeur2Lengte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xvoorkeur2Lengte.DecimalPlaces = 2;
            this.xvoorkeur2Lengte.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvoorkeur2Lengte.Location = new System.Drawing.Point(11, 105);
            this.xvoorkeur2Lengte.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xvoorkeur2Lengte.Name = "xvoorkeur2Lengte";
            this.xvoorkeur2Lengte.Size = new System.Drawing.Size(348, 27);
            this.xvoorkeur2Lengte.TabIndex = 1;
            this.xvoorkeur2Lengte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.metroToolTip1.SetToolTip(this.xvoorkeur2Lengte, "Vul in de uitgangslengte om vanaf te kijken");
            this.xvoorkeur2Lengte.Enter += new System.EventHandler(this.xvoorkeur1Lengte_Enter);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(7, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 20);
            this.label1.TabIndex = 1;
            this.label1.Text = "Voorkeur1 UitgangsLengte";
            // 
            // xvoorkeur1Lengte
            // 
            this.xvoorkeur1Lengte.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xvoorkeur1Lengte.DecimalPlaces = 2;
            this.xvoorkeur1Lengte.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvoorkeur1Lengte.Location = new System.Drawing.Point(11, 52);
            this.xvoorkeur1Lengte.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xvoorkeur1Lengte.Name = "xvoorkeur1Lengte";
            this.xvoorkeur1Lengte.Size = new System.Drawing.Size(348, 27);
            this.xvoorkeur1Lengte.TabIndex = 0;
            this.xvoorkeur1Lengte.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.metroToolTip1.SetToolTip(this.xvoorkeur1Lengte, "Vul in de uitgangslengte om vanaf te kijken");
            this.xvoorkeur1Lengte.Enter += new System.EventHandler(this.xvoorkeur1Lengte_Enter);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // OptimaleVerbruikSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(405, 384);
            this.Controls.Add(this.xvoorkeur1group);
            this.Controls.Add(this.panel1);
            this.MinimumSize = new System.Drawing.Size(250, 375);
            this.Name = "OptimaleVerbruikSettingForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Orange;
            this.Text = "Optimale Verbruik Opties";
            this.Title = "Optimale Verbruik Opties";
            this.Shown += new System.EventHandler(this.OptimaleVerbruikSettingForm_Shown);
            this.panel1.ResumeLayout(false);
            this.xvoorkeur1group.ResumeLayout(false);
            this.xvoorkeur1group.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xreststuk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xvoorkeur3Lengte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xvoorkeur2Lengte)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xvoorkeur1Lengte)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button xok;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.GroupBox xvoorkeur1group;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown xreststuk;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown xvoorkeur1Lengte;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown xvoorkeur2Lengte;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown xvoorkeur3Lengte;
    }
}