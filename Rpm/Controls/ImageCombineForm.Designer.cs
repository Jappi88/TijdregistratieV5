
namespace Rpm.Controls
{
    partial class ImageCombineForm
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.xalingments = new MetroFramework.Controls.MetroComboBox();
            this.xverhouding = new System.Windows.Forms.NumericUpDown();
            this.label4 = new System.Windows.Forms.Label();
            this.xkiesimage2 = new MetroFramework.Controls.MetroButton();
            this.xsecondimage = new System.Windows.Forms.PictureBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xkiesimage1 = new MetroFramework.Controls.MetroButton();
            this.xfirstimage = new System.Windows.Forms.PictureBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xresult = new System.Windows.Forms.PictureBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.xsluitenb = new MetroFramework.Controls.MetroButton();
            this.xopslaanalsb = new MetroFramework.Controls.MetroButton();
            this.panel1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox5.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xverhouding)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xsecondimage)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xfirstimage)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xresult)).BeginInit();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.groupBox3);
            this.panel1.Controls.Add(this.groupBox1);
            this.panel1.Controls.Add(this.groupBox2);
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(570, 330);
            this.panel1.TabIndex = 0;
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox3.Controls.Add(this.groupBox5);
            this.groupBox3.Controls.Add(this.xkiesimage2);
            this.groupBox3.Controls.Add(this.xsecondimage);
            this.groupBox3.Location = new System.Drawing.Point(144, 3);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(135, 290);
            this.groupBox3.TabIndex = 4;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Afbeelding 2";
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.xalingments);
            this.groupBox5.Controls.Add(this.xverhouding);
            this.groupBox5.Controls.Add(this.label4);
            this.groupBox5.Location = new System.Drawing.Point(6, 162);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(128, 122);
            this.groupBox5.TabIndex = 4;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Afmeting";
            // 
            // xalingments
            // 
            this.xalingments.FormattingEnabled = true;
            this.xalingments.ItemHeight = 23;
            this.xalingments.Location = new System.Drawing.Point(6, 20);
            this.xalingments.Name = "xalingments";
            this.xalingments.PromptText = "Kies Locatie";
            this.xalingments.Size = new System.Drawing.Size(116, 29);
            this.xalingments.TabIndex = 3;
            this.xalingments.UseSelectable = true;
            this.xalingments.SelectedIndexChanged += new System.EventHandler(this.xalingments_SelectedIndexChanged);
            // 
            // xverhouding
            // 
            this.xverhouding.DecimalPlaces = 2;
            this.xverhouding.Increment = new decimal(new int[] {
            1,
            0,
            0,
            131072});
            this.xverhouding.Location = new System.Drawing.Point(9, 72);
            this.xverhouding.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xverhouding.Name = "xverhouding";
            this.xverhouding.Size = new System.Drawing.Size(73, 25);
            this.xverhouding.TabIndex = 2;
            this.xverhouding.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.xverhouding.ValueChanged += new System.EventHandler(this.xverhouding_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(78, 17);
            this.label4.TabIndex = 0;
            this.label4.Text = "Verhouding:";
            // 
            // xkiesimage2
            // 
            this.xkiesimage2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xkiesimage2.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xkiesimage2.Location = new System.Drawing.Point(4, 126);
            this.xkiesimage2.Name = "xkiesimage2";
            this.xkiesimage2.Size = new System.Drawing.Size(125, 30);
            this.xkiesimage2.TabIndex = 3;
            this.xkiesimage2.Text = "Kies Afbeelding";
            this.xkiesimage2.UseSelectable = true;
            this.xkiesimage2.Click += new System.EventHandler(this.xkiesimage2_Click);
            // 
            // xsecondimage
            // 
            this.xsecondimage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xsecondimage.Location = new System.Drawing.Point(17, 24);
            this.xsecondimage.Name = "xsecondimage";
            this.xsecondimage.Size = new System.Drawing.Size(96, 96);
            this.xsecondimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.xsecondimage.TabIndex = 0;
            this.xsecondimage.TabStop = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.groupBox1.Controls.Add(this.xkiesimage1);
            this.groupBox1.Controls.Add(this.xfirstimage);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(135, 290);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Afbeelding 1";
            // 
            // xkiesimage1
            // 
            this.xkiesimage1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xkiesimage1.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xkiesimage1.Location = new System.Drawing.Point(4, 126);
            this.xkiesimage1.Name = "xkiesimage1";
            this.xkiesimage1.Size = new System.Drawing.Size(125, 30);
            this.xkiesimage1.TabIndex = 3;
            this.xkiesimage1.Text = "Kies Afbeelding";
            this.xkiesimage1.UseSelectable = true;
            this.xkiesimage1.Click += new System.EventHandler(this.xkiesimage1_Click);
            // 
            // xfirstimage
            // 
            this.xfirstimage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xfirstimage.Location = new System.Drawing.Point(17, 24);
            this.xfirstimage.Name = "xfirstimage";
            this.xfirstimage.Size = new System.Drawing.Size(96, 96);
            this.xfirstimage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.xfirstimage.TabIndex = 0;
            this.xfirstimage.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.xopslaanalsb);
            this.groupBox2.Controls.Add(this.xresult);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox2.Location = new System.Drawing.Point(281, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(289, 293);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Resultaat";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.numericUpDown2);
            this.groupBox4.Controls.Add(this.numericUpDown1);
            this.groupBox4.Controls.Add(this.label2);
            this.groupBox4.Controls.Add(this.label1);
            this.groupBox4.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox4.Location = new System.Drawing.Point(3, 159);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(283, 57);
            this.groupBox4.TabIndex = 1;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Afmeting";
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(202, 23);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(73, 25);
            this.numericUpDown2.TabIndex = 3;
            this.numericUpDown2.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(62, 23);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            256,
            0,
            0,
            0});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(73, 25);
            this.numericUpDown1.TabIndex = 2;
            this.numericUpDown1.Value = new decimal(new int[] {
            128,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(141, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Hoogte:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(2, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Breedte:";
            // 
            // xresult
            // 
            this.xresult.Dock = System.Windows.Forms.DockStyle.Top;
            this.xresult.Location = new System.Drawing.Point(3, 21);
            this.xresult.Name = "xresult";
            this.xresult.Size = new System.Drawing.Size(283, 138);
            this.xresult.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.xresult.TabIndex = 0;
            this.xresult.TabStop = false;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xsluitenb);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 293);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(570, 37);
            this.panel2.TabIndex = 5;
            // 
            // xsluitenb
            // 
            this.xsluitenb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.xsluitenb.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xsluitenb.Location = new System.Drawing.Point(439, 4);
            this.xsluitenb.Name = "xsluitenb";
            this.xsluitenb.Size = new System.Drawing.Size(125, 30);
            this.xsluitenb.TabIndex = 1;
            this.xsluitenb.Text = "Sluiten";
            this.xsluitenb.UseSelectable = true;
            this.xsluitenb.Click += new System.EventHandler(this.xsluitenb_Click);
            // 
            // xopslaanalsb
            // 
            this.xopslaanalsb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xopslaanalsb.FontSize = MetroFramework.MetroButtonSize.Medium;
            this.xopslaanalsb.Location = new System.Drawing.Point(158, 222);
            this.xopslaanalsb.Name = "xopslaanalsb";
            this.xopslaanalsb.Size = new System.Drawing.Size(125, 30);
            this.xopslaanalsb.TabIndex = 2;
            this.xopslaanalsb.Text = "Opslaan Als...";
            this.xopslaanalsb.UseSelectable = true;
            this.xopslaanalsb.Click += new System.EventHandler(this.xopslaanalsb_Click);
            // 
            // ImageCombineForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 410);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(610, 410);
            this.Name = "ImageCombineForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "Combineer Afbeelding";
            this.panel1.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xverhouding)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xsecondimage)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xfirstimage)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xresult)).EndInit();
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private MetroFramework.Controls.MetroButton xsluitenb;
        private System.Windows.Forms.PictureBox xresult;
        private MetroFramework.Controls.MetroButton xopslaanalsb;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox xfirstimage;
        private System.Windows.Forms.GroupBox groupBox3;
        private MetroFramework.Controls.MetroButton xkiesimage2;
        private System.Windows.Forms.PictureBox xsecondimage;
        private MetroFramework.Controls.MetroButton xkiesimage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox5;
        private MetroFramework.Controls.MetroComboBox xalingments;
        private System.Windows.Forms.NumericUpDown xverhouding;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
    }
}