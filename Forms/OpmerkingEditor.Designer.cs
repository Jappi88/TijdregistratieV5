
namespace Forms
{
    partial class OpmerkingEditor
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
            this.components = new System.ComponentModel.Container();
            this.panel6 = new System.Windows.Forms.Panel();
            this.xgeplaatstOpLabel = new System.Windows.Forms.Label();
            this.xannuleren = new System.Windows.Forms.Button();
            this.xOpslaan = new System.Windows.Forms.Button();
            this.xtitle = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xopmerking = new System.Windows.Forms.TextBox();
            this.xontvangers = new System.Windows.Forms.ComboBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel6.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.xgeplaatstOpLabel);
            this.panel6.Controls.Add(this.xannuleren);
            this.panel6.Controls.Add(this.xOpslaan);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel6.Location = new System.Drawing.Point(10, 230);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(474, 40);
            this.panel6.TabIndex = 2;
            // 
            // xgeplaatstOpLabel
            // 
            this.xgeplaatstOpLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xgeplaatstOpLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xgeplaatstOpLabel.Location = new System.Drawing.Point(3, 3);
            this.xgeplaatstOpLabel.Name = "xgeplaatstOpLabel";
            this.xgeplaatstOpLabel.Size = new System.Drawing.Size(186, 35);
            this.xgeplaatstOpLabel.TabIndex = 6;
            this.xgeplaatstOpLabel.Text = "Geplaatst Op";
            this.xgeplaatstOpLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.toolTip1.SetToolTip(this.xgeplaatstOpLabel, "De datum + tijd waarop de opmerking is gemaakt");
            // 
            // xannuleren
            // 
            this.xannuleren.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xannuleren.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xannuleren.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xannuleren.ForeColor = System.Drawing.Color.Black;
            this.xannuleren.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xannuleren.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xannuleren.Location = new System.Drawing.Point(336, 3);
            this.xannuleren.Name = "xannuleren";
            this.xannuleren.Size = new System.Drawing.Size(135, 35);
            this.xannuleren.TabIndex = 4;
            this.xannuleren.Text = "Annuleren";
            this.xannuleren.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xannuleren.UseVisualStyleBackColor = true;
            this.xannuleren.Click += new System.EventHandler(this.xannuleren_Click);
            // 
            // xOpslaan
            // 
            this.xOpslaan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xOpslaan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xOpslaan.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xOpslaan.ForeColor = System.Drawing.Color.Black;
            this.xOpslaan.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xOpslaan.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xOpslaan.Location = new System.Drawing.Point(195, 3);
            this.xOpslaan.Name = "xOpslaan";
            this.xOpslaan.Size = new System.Drawing.Size(135, 35);
            this.xOpslaan.TabIndex = 5;
            this.xOpslaan.Text = "OK";
            this.xOpslaan.UseVisualStyleBackColor = true;
            this.xOpslaan.Click += new System.EventHandler(this.xOpslaan_Click);
            // 
            // xtitle
            // 
            this.xtitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.xtitle.Location = new System.Drawing.Point(0, 0);
            this.xtitle.Name = "xtitle";
            this.xtitle.Size = new System.Drawing.Size(474, 25);
            this.xtitle.TabIndex = 3;
            this.xtitle.Text = "Vul in een onderwerp...";
            this.toolTip1.SetToolTip(this.xtitle, "Vul in een title voor deze opmerking");
            this.xtitle.Enter += new System.EventHandler(this.xtitle_Enter);
            this.xtitle.Leave += new System.EventHandler(this.xtitle_Leave);
            // 
            // panel1
            // 
            this.panel1.AutoScroll = true;
            this.panel1.Controls.Add(this.xopmerking);
            this.panel1.Controls.Add(this.xontvangers);
            this.panel1.Controls.Add(this.xtitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(10, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(474, 170);
            this.panel1.TabIndex = 4;
            // 
            // xopmerking
            // 
            this.xopmerking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xopmerking.Location = new System.Drawing.Point(0, 50);
            this.xopmerking.Multiline = true;
            this.xopmerking.Name = "xopmerking";
            this.xopmerking.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.xopmerking.Size = new System.Drawing.Size(474, 120);
            this.xopmerking.TabIndex = 4;
            this.xopmerking.Text = "Vul in een vraag, opmerking of een verzoek...";
            this.toolTip1.SetToolTip(this.xopmerking, "Vul in een opmerking, vraag of verzoek.\r\n\r\nTyp in  A.U.B. duidelijk en met zo min" +
        " mogelijk spelfouten.\r\nHet is daardoor een stuk makkelijker om te begrijpen, en " +
        "er wat mee te doen.");
            this.xopmerking.Enter += new System.EventHandler(this.xopmerking_Enter);
            this.xopmerking.Leave += new System.EventHandler(this.xopmerking_Leave);
            // 
            // xontvangers
            // 
            this.xontvangers.Dock = System.Windows.Forms.DockStyle.Top;
            this.xontvangers.FormattingEnabled = true;
            this.xontvangers.Items.AddRange(new object[] {
            "Iedereen",
            "Type in de ontvanger(s)..."});
            this.xontvangers.Location = new System.Drawing.Point(0, 25);
            this.xontvangers.Name = "xontvangers";
            this.xontvangers.Size = new System.Drawing.Size(474, 25);
            this.xontvangers.TabIndex = 6;
            this.toolTip1.SetToolTip(this.xontvangers, "Vul in de ontvangers: \'Iedereen\' als je wilt dat iedereen deze opmerking ziet. \r\n" +
        "Vul in anders de ontvanger(s) naam. \r\nDe namen kan je onderscheiden door een \';\'" +
        " tussen te plaatsen.");
            this.xontvangers.Enter += new System.EventHandler(this.xontvangers_Enter);
            this.xontvangers.Leave += new System.EventHandler(this.xontvangers_Leave);
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            // 
            // OpmerkingEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 280);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel6);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "OpmerkingEditor";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Green;
            this.Text = "Nieuwe Opmerking";
            this.panel6.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button xannuleren;
        private System.Windows.Forms.Button xOpslaan;
        private System.Windows.Forms.TextBox xtitle;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TextBox xopmerking;
        private System.Windows.Forms.ComboBox xontvangers;
        private System.Windows.Forms.Label xgeplaatstOpLabel;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}