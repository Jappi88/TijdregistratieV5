
namespace Forms
{
    partial class OpenTakenForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OpenTakenForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.xfieldmessage = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panel4 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.xindelinglabel = new System.Windows.Forms.Label();
            this.xwerktijdlabel = new System.Windows.Forms.Label();
            this.xondbrlabel = new System.Windows.Forms.Label();
            this.xindeling = new System.Windows.Forms.Button();
            this.xwerktijd = new System.Windows.Forms.Button();
            this.xonderbrekeningen = new System.Windows.Forms.Button();
            this.xrooster = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Controls.Add(this.xfieldmessage);
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(660, 300);
            this.panel1.TabIndex = 2;
            // 
            // xfieldmessage
            // 
            this.xfieldmessage.Dock = System.Windows.Forms.DockStyle.Top;
            this.xfieldmessage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xfieldmessage.Location = new System.Drawing.Point(132, 0);
            this.xfieldmessage.Name = "xfieldmessage";
            this.xfieldmessage.Size = new System.Drawing.Size(528, 158);
            this.xfieldmessage.TabIndex = 1;
            this.xfieldmessage.Text = resources.GetString("xfieldmessage.Text");
            this.xfieldmessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Left;
            this.pictureBox1.Image = global::ProductieManager.Properties.Resources.Private_80_icon_icons1;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(132, 300);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.xrooster);
            this.panel4.Controls.Add(this.button2);
            this.panel4.Controls.Add(this.button1);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel4.Location = new System.Drawing.Point(20, 360);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(660, 45);
            this.panel4.TabIndex = 3;
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.button2.Location = new System.Drawing.Point(398, 3);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(128, 38);
            this.button2.TabIndex = 13;
            this.button2.Text = "&OK";
            this.button2.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.button1.Location = new System.Drawing.Point(532, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(125, 38);
            this.button1.TabIndex = 12;
            this.button1.Text = "&Annuleren";
            this.button1.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xonderbrekeningen);
            this.panel2.Controls.Add(this.xwerktijd);
            this.panel2.Controls.Add(this.xindeling);
            this.panel2.Controls.Add(this.xondbrlabel);
            this.panel2.Controls.Add(this.xwerktijdlabel);
            this.panel2.Controls.Add(this.xindelinglabel);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(132, 158);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(528, 142);
            this.panel2.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(6, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Indeling: ";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(6, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 21);
            this.label2.TabIndex = 1;
            this.label2.Text = "Gewerkte Tijd:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(6, 106);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(148, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Onderbrekeningen: ";
            // 
            // xindelinglabel
            // 
            this.xindelinglabel.AutoSize = true;
            this.xindelinglabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xindelinglabel.Location = new System.Drawing.Point(160, 18);
            this.xindelinglabel.Name = "xindelinglabel";
            this.xindelinglabel.Size = new System.Drawing.Size(141, 21);
            this.xindelinglabel.TabIndex = 4;
            this.xindelinglabel.Text = "{0} Medewerkers.";
            // 
            // xwerktijdlabel
            // 
            this.xwerktijdlabel.AutoSize = true;
            this.xwerktijdlabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xwerktijdlabel.Location = new System.Drawing.Point(160, 62);
            this.xwerktijdlabel.Name = "xwerktijdlabel";
            this.xwerktijdlabel.Size = new System.Drawing.Size(63, 21);
            this.xwerktijdlabel.TabIndex = 5;
            this.xwerktijdlabel.Text = "{0} Uur";
            // 
            // xondbrlabel
            // 
            this.xondbrlabel.AutoSize = true;
            this.xondbrlabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xondbrlabel.Location = new System.Drawing.Point(160, 106);
            this.xondbrlabel.Name = "xondbrlabel";
            this.xondbrlabel.Size = new System.Drawing.Size(31, 21);
            this.xondbrlabel.TabIndex = 6;
            this.xondbrlabel.Text = "{0}";
            // 
            // xindeling
            // 
            this.xindeling.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xindeling.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xindeling.Image = global::ProductieManager.Properties.Resources.iconfinder_technologymachineelectronic32_32;
            this.xindeling.Location = new System.Drawing.Point(307, 10);
            this.xindeling.Name = "xindeling";
            this.xindeling.Size = new System.Drawing.Size(218, 38);
            this.xindeling.TabIndex = 7;
            this.xindeling.Text = "Beheer Indeling";
            this.xindeling.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xindeling.UseVisualStyleBackColor = true;
            this.xindeling.Click += new System.EventHandler(this.xindeling_Click);
            // 
            // xwerktijd
            // 
            this.xwerktijd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xwerktijd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xwerktijd.Image = global::ProductieManager.Properties.Resources.business_color_progress_icon_icons_com_53437;
            this.xwerktijd.Location = new System.Drawing.Point(307, 54);
            this.xwerktijd.Name = "xwerktijd";
            this.xwerktijd.Size = new System.Drawing.Size(218, 38);
            this.xwerktijd.TabIndex = 8;
            this.xwerktijd.Text = "Beheer Werktijd";
            this.xwerktijd.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xwerktijd.UseVisualStyleBackColor = true;
            this.xwerktijd.Click += new System.EventHandler(this.xwerktijd_Click);
            // 
            // xonderbrekeningen
            // 
            this.xonderbrekeningen.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xonderbrekeningen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xonderbrekeningen.Image = global::ProductieManager.Properties.Resources.onderhoud32_32;
            this.xonderbrekeningen.Location = new System.Drawing.Point(307, 98);
            this.xonderbrekeningen.Name = "xonderbrekeningen";
            this.xonderbrekeningen.Size = new System.Drawing.Size(218, 38);
            this.xonderbrekeningen.TabIndex = 9;
            this.xonderbrekeningen.Text = "Beheer Onderbrekeningen";
            this.xonderbrekeningen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xonderbrekeningen.UseVisualStyleBackColor = true;
            this.xonderbrekeningen.Click += new System.EventHandler(this.xonderbrekeningen_Click);
            // 
            // xrooster
            // 
            this.xrooster.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xrooster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xrooster.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrooster.Image = global::ProductieManager.Properties.Resources.schedule_32_32;
            this.xrooster.Location = new System.Drawing.Point(246, 3);
            this.xrooster.Name = "xrooster";
            this.xrooster.Size = new System.Drawing.Size(146, 38);
            this.xrooster.TabIndex = 14;
            this.xrooster.Text = "Beheer Rooster";
            this.xrooster.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xrooster.UseVisualStyleBackColor = true;
            this.xrooster.Click += new System.EventHandler(this.xrooster_Click);
            // 
            // OpenTakenForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 425);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel4);
            this.MinimumSize = new System.Drawing.Size(700, 425);
            this.Name = "OpenTakenForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Red;
            this.Text = "Aandacht vereist!";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OpenTakenForm_FormClosing);
            this.Shown += new System.EventHandler(this.OpenTakenForm_Shown);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label xfieldmessage;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xonderbrekeningen;
        private System.Windows.Forms.Button xwerktijd;
        private System.Windows.Forms.Button xindeling;
        private System.Windows.Forms.Label xondbrlabel;
        private System.Windows.Forms.Label xwerktijdlabel;
        private System.Windows.Forms.Label xindelinglabel;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button xrooster;
    }
}