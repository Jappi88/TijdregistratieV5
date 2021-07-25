namespace Forms
{
    partial class CreateAccount
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreateAccount));
            this.label1 = new System.Windows.Forms.Label();
            this.xgebruikersname = new System.Windows.Forms.TextBox();
            this.xwachtwoord1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.xwachtwoord2 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.xtoegangslevel = new MetroFramework.Controls.MetroComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(23, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(142, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gebruikers Name";
            // 
            // xgebruikersname
            // 
            this.xgebruikersname.BackColor = System.Drawing.Color.White;
            this.xgebruikersname.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xgebruikersname.ForeColor = System.Drawing.Color.Black;
            this.xgebruikersname.Location = new System.Drawing.Point(27, 84);
            this.xgebruikersname.Name = "xgebruikersname";
            this.xgebruikersname.Size = new System.Drawing.Size(283, 29);
            this.xgebruikersname.TabIndex = 1;
            this.xgebruikersname.Text = "Vul in een gebruikersnaam...";
            this.xgebruikersname.Enter += new System.EventHandler(this.xgebruikersname_MouseEnter);
            this.xgebruikersname.Leave += new System.EventHandler(this.xgebruikersname_MouseLeave);
            // 
            // xwachtwoord1
            // 
            this.xwachtwoord1.BackColor = System.Drawing.Color.White;
            this.xwachtwoord1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xwachtwoord1.ForeColor = System.Drawing.Color.Black;
            this.xwachtwoord1.Location = new System.Drawing.Point(27, 140);
            this.xwachtwoord1.Name = "xwachtwoord1";
            this.xwachtwoord1.PasswordChar = '*';
            this.xwachtwoord1.Size = new System.Drawing.Size(283, 29);
            this.xwachtwoord1.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(23, 116);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 21);
            this.label2.TabIndex = 2;
            this.label2.Text = "Wachtwoord";
            // 
            // xwachtwoord2
            // 
            this.xwachtwoord2.BackColor = System.Drawing.Color.White;
            this.xwachtwoord2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xwachtwoord2.ForeColor = System.Drawing.Color.Black;
            this.xwachtwoord2.Location = new System.Drawing.Point(27, 196);
            this.xwachtwoord2.Name = "xwachtwoord2";
            this.xwachtwoord2.PasswordChar = '*';
            this.xwachtwoord2.Size = new System.Drawing.Size(283, 29);
            this.xwachtwoord2.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(23, 172);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(171, 21);
            this.label3.TabIndex = 4;
            this.label3.Text = "Herhaal Wachtwoord";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(318, 60);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(120, 21);
            this.label4.TabIndex = 6;
            this.label4.Text = "Toegang Level";
            // 
            // xtoegangslevel
            // 
            this.xtoegangslevel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xtoegangslevel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xtoegangslevel.FormattingEnabled = true;
            this.xtoegangslevel.ItemHeight = 23;
            this.xtoegangslevel.Items.AddRange(new object[] {
            "AlleenKijken",
            "ProductieBasis",
            "ProductieAdvance",
            "Manager"});
            this.xtoegangslevel.Location = new System.Drawing.Point(322, 84);
            this.xtoegangslevel.Name = "xtoegangslevel";
            this.xtoegangslevel.Size = new System.Drawing.Size(172, 29);
            this.xtoegangslevel.TabIndex = 7;
            this.xtoegangslevel.UseSelectable = true;
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(27, 231);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(283, 36);
            this.button1.TabIndex = 8;
            this.button1.Text = "Maak Gebruiker Aan";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(373, 231);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 36);
            this.button2.TabIndex = 9;
            this.button2.Text = "Annuleer";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // CreateAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(510, 280);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.xtoegangslevel);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.xwachtwoord2);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.xwachtwoord1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.xgebruikersname);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(510, 280);
            this.Name = "CreateAccount";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Maak Account Aan";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox xgebruikersname;
        private System.Windows.Forms.TextBox xwachtwoord1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox xwachtwoord2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroComboBox xtoegangslevel;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}