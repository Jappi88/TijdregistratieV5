namespace Forms
{
    partial class ChatForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatForm));
            this.productieChatUI1 = new Forms.ProductieChatUI();
            this.SuspendLayout();
            // 
            // productieChatUI1
            // 
            this.productieChatUI1.BackColor = System.Drawing.Color.Transparent;
            this.productieChatUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.productieChatUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.productieChatUI1.Location = new System.Drawing.Point(20, 30);
            this.productieChatUI1.Margin = new System.Windows.Forms.Padding(4);
            this.productieChatUI1.Name = "productieChatUI1";
            this.productieChatUI1.Size = new System.Drawing.Size(804, 501);
            this.productieChatUI1.TabIndex = 0;
            // 
            // ChatForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(844, 551);
            this.Controls.Add(this.productieChatUI1);
            this.DisplayHeader = false;
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "ChatForm";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 20);
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Productie Chat";
            this.Title = "Productie Chat";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatForm_FormClosing);
            this.ResumeLayout(false);

        }

        #endregion

        private ProductieChatUI productieChatUI1;
    }
}