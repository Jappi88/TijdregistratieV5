
namespace Forms
{
    partial class AddPersoneel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddPersoneel));
            this.xisuitzendcheck = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.xnaam = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.xbuttonpanel = new System.Windows.Forms.Panel();
            this.xklusjesb = new System.Windows.Forms.Button();
            this.xvaardigeheden = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.xafdeling = new System.Windows.Forms.TextBox();
            this.xrooster = new System.Windows.Forms.Button();
            this.panel4 = new System.Windows.Forms.Panel();
            this.xprofielfoto = new System.Windows.Forms.PictureBox();
            this.xkiesvrijetijd = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel3 = new System.Windows.Forms.Panel();
            this.xannueer = new System.Windows.Forms.Button();
            this.xok = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.xbuttonpanel.SuspendLayout();
            this.panel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xprofielfoto)).BeginInit();
            this.panel2.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // xisuitzendcheck
            // 
            this.xisuitzendcheck.AutoSize = true;
            this.xisuitzendcheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xisuitzendcheck.Location = new System.Drawing.Point(144, 194);
            this.xisuitzendcheck.Name = "xisuitzendcheck";
            this.xisuitzendcheck.Size = new System.Drawing.Size(125, 20);
            this.xisuitzendcheck.TabIndex = 4;
            this.xisuitzendcheck.Text = "Externe kracht";
            this.xisuitzendcheck.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(134, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(49, 16);
            this.label1.TabIndex = 13;
            this.label1.Text = "Naam";
            // 
            // xnaam
            // 
            this.xnaam.Location = new System.Drawing.Point(133, 29);
            this.xnaam.Name = "xnaam";
            this.xnaam.Size = new System.Drawing.Size(276, 22);
            this.xnaam.TabIndex = 0;
            this.xnaam.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.xnaam_KeyPress);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.xbuttonpanel);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.xafdeling);
            this.panel1.Controls.Add(this.xrooster);
            this.panel1.Controls.Add(this.panel4);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.xisuitzendcheck);
            this.panel1.Controls.Add(this.xnaam);
            this.panel1.Controls.Add(this.xkiesvrijetijd);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(20, 60);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(415, 273);
            this.panel1.TabIndex = 21;
            // 
            // xbuttonpanel
            // 
            this.xbuttonpanel.Controls.Add(this.xklusjesb);
            this.xbuttonpanel.Controls.Add(this.xvaardigeheden);
            this.xbuttonpanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.xbuttonpanel.Location = new System.Drawing.Point(128, 221);
            this.xbuttonpanel.Name = "xbuttonpanel";
            this.xbuttonpanel.Size = new System.Drawing.Size(287, 52);
            this.xbuttonpanel.TabIndex = 26;
            this.xbuttonpanel.Visible = false;
            // 
            // xklusjesb
            // 
            this.xklusjesb.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xklusjesb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xklusjesb.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xklusjesb.Image = global::ProductieManager.Properties.Resources.iconfinder_technologymachineelectronic32_32;
            this.xklusjesb.Location = new System.Drawing.Point(5, 3);
            this.xklusjesb.Name = "xklusjesb";
            this.xklusjesb.Size = new System.Drawing.Size(135, 40);
            this.xklusjesb.TabIndex = 20;
            this.xklusjesb.Text = "Klusjes";
            this.xklusjesb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xklusjesb.UseVisualStyleBackColor = true;
            this.xklusjesb.Click += new System.EventHandler(this.xklusjesb_Click);
            // 
            // xvaardigeheden
            // 
            this.xvaardigeheden.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xvaardigeheden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xvaardigeheden.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xvaardigeheden.Image = global::ProductieManager.Properties.Resources.key_skills;
            this.xvaardigeheden.Location = new System.Drawing.Point(146, 3);
            this.xvaardigeheden.Name = "xvaardigeheden";
            this.xvaardigeheden.Size = new System.Drawing.Size(135, 40);
            this.xvaardigeheden.TabIndex = 19;
            this.xvaardigeheden.Text = "Vaardigheden";
            this.xvaardigeheden.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xvaardigeheden.UseVisualStyleBackColor = true;
            this.xvaardigeheden.Click += new System.EventHandler(this.xvaardigeheden_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(134, 55);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 16);
            this.label2.TabIndex = 25;
            this.label2.Text = "Afdeling";
            // 
            // xafdeling
            // 
            this.xafdeling.Location = new System.Drawing.Point(133, 74);
            this.xafdeling.Name = "xafdeling";
            this.xafdeling.Size = new System.Drawing.Size(276, 22);
            this.xafdeling.TabIndex = 24;
            // 
            // xrooster
            // 
            this.xrooster.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xrooster.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xrooster.Image = global::ProductieManager.Properties.Resources.schedule_32_32;
            this.xrooster.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xrooster.Location = new System.Drawing.Point(133, 148);
            this.xrooster.Name = "xrooster";
            this.xrooster.Size = new System.Drawing.Size(276, 40);
            this.xrooster.TabIndex = 23;
            this.xrooster.Text = "Werk Rooster";
            this.xrooster.UseVisualStyleBackColor = true;
            this.xrooster.Click += new System.EventHandler(this.xrooster_Click);
            // 
            // panel4
            // 
            this.panel4.Controls.Add(this.xprofielfoto);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(128, 273);
            this.panel4.TabIndex = 22;
            // 
            // xprofielfoto
            // 
            this.xprofielfoto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xprofielfoto.Image = global::ProductieManager.Properties.Resources.user_customer_person_13976;
            this.xprofielfoto.Location = new System.Drawing.Point(0, 0);
            this.xprofielfoto.Name = "xprofielfoto";
            this.xprofielfoto.Size = new System.Drawing.Size(128, 273);
            this.xprofielfoto.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.xprofielfoto.TabIndex = 21;
            this.xprofielfoto.TabStop = false;
            // 
            // xkiesvrijetijd
            // 
            this.xkiesvrijetijd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xkiesvrijetijd.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xkiesvrijetijd.Image = global::ProductieManager.Properties.Resources.iconfinder_beach_sea_summer_chill_holiday_vacation_icon_4602018_122097;
            this.xkiesvrijetijd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xkiesvrijetijd.Location = new System.Drawing.Point(133, 102);
            this.xkiesvrijetijd.Name = "xkiesvrijetijd";
            this.xkiesvrijetijd.Size = new System.Drawing.Size(276, 40);
            this.xkiesvrijetijd.TabIndex = 1;
            this.xkiesvrijetijd.Text = "Beheer Verlof Tijden";
            this.xkiesvrijetijd.UseVisualStyleBackColor = true;
            this.xkiesvrijetijd.Click += new System.EventHandler(this.xkiesvrijetijd_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.panel3);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel2.Location = new System.Drawing.Point(20, 333);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(415, 37);
            this.panel2.TabIndex = 22;
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.xannueer);
            this.panel3.Controls.Add(this.xok);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel3.Location = new System.Drawing.Point(200, 0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(215, 37);
            this.panel3.TabIndex = 11;
            // 
            // xannueer
            // 
            this.xannueer.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xannueer.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xannueer.Location = new System.Drawing.Point(109, 3);
            this.xannueer.Name = "xannueer";
            this.xannueer.Size = new System.Drawing.Size(100, 30);
            this.xannueer.TabIndex = 8;
            this.xannueer.Text = "Sluiten";
            this.xannueer.UseVisualStyleBackColor = true;
            this.xannueer.Click += new System.EventHandler(this.xannueer_Click);
            // 
            // xok
            // 
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Location = new System.Drawing.Point(3, 3);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(100, 30);
            this.xok.TabIndex = 5;
            this.xok.Text = "&OK";
            this.xok.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // AddPersoneel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(455, 390);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(455, 390);
            this.Name = "AddPersoneel";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Voeg Personeel Toe";
            this.Title = "Voeg Personeel Toe";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.xbuttonpanel.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xprofielfoto)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox xisuitzendcheck;
        private System.Windows.Forms.Button xkiesvrijetijd;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xnaam;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xok;
        private System.Windows.Forms.Button xannueer;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.PictureBox xprofielfoto;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Button xrooster;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox xafdeling;
        private System.Windows.Forms.Panel xbuttonpanel;
        private System.Windows.Forms.Button xklusjesb;
        private System.Windows.Forms.Button xvaardigeheden;
    }
}