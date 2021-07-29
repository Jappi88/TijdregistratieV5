using System.Windows.Forms;
using MetroFramework.Controls;
using Various;

namespace Controls
{
    partial class ProductieView
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProductieView));
            Various.MenuButton menuButton12 = new Various.MenuButton();
            Various.MenuButton menuButton13 = new Various.MenuButton();
            Various.MenuButton menuButton14 = new Various.MenuButton();
            Various.MenuButton menuButton15 = new Various.MenuButton();
            Various.MenuButton menuButton16 = new Various.MenuButton();
            Various.MenuButton menuButton17 = new Various.MenuButton();
            Various.MenuButton menuButton18 = new Various.MenuButton();
            Various.MenuButton menuButton19 = new Various.MenuButton();
            Various.MenuButton menuButton20 = new Various.MenuButton();
            Various.MenuButton menuButton21 = new Various.MenuButton();
            Various.MenuButton menuButton22 = new Various.MenuButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.xspeciaalroosterbutton = new System.Windows.Forms.Button();
            this.xchatformbutton = new System.Windows.Forms.Button();
            this.xallenotities = new System.Windows.Forms.Button();
            this.xsendemail = new System.Windows.Forms.Button();
            this.xupdateallform = new System.Windows.Forms.Button();
            this.xmateriaalverbruikb = new System.Windows.Forms.Button();
            this.xupdateb = new System.Windows.Forms.Button();
            this.xoverzicht = new System.Windows.Forms.Button();
            this.xlogbook = new System.Windows.Forms.Button();
            this.xdbbewerkingen = new System.Windows.Forms.Button();
            this.xonderbrekeningen = new System.Windows.Forms.Button();
            this.xbehvaardigheden = new System.Windows.Forms.Button();
            this.xbehpersoneel = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.xsettingsb = new System.Windows.Forms.Button();
            this.xloginb = new System.Windows.Forms.Button();
            this.xaboutb = new System.Windows.Forms.Button();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.metroTabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabPage1 = new MetroFramework.Controls.MetroTabPage();
            this.tabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.tabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.tabPage4 = new MetroFramework.Controls.MetroTabPage();
            this.xspeciaalroosterlabel = new System.Windows.Forms.Panel();
            this.xtabimages = new System.Windows.Forms.ImageList(this.components);
            this.xproductieListControl1 = new Controls.ProductieListControl();
            this.xbewerkingListControl = new Controls.ProductieListControl();
            this.werkPlekkenUI1 = new Controls.WerkPlekkenUI();
            this.recentGereedMeldingenUI1 = new Controls.RecentGereedMeldingenUI();
            this.takenManager1 = new Controls.TakenManager();
            this.mainMenu1 = new Controls.MainMenu();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.metroTabControl.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.xspeciaalroosterlabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.toolTip1.ToolTipTitle = "Producties";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(0, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Nieuwe Productie";
            this.toolTip1.SetToolTip(this.button1, "Maak nieuwe productie aan");
            this.button1.UseVisualStyleBackColor = true;
            // 
            // xspeciaalroosterbutton
            // 
            this.xspeciaalroosterbutton.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xspeciaalroosterbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xspeciaalroosterbutton.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xspeciaalroosterbutton.ForeColor = System.Drawing.Color.DarkRed;
            this.xspeciaalroosterbutton.Image = global::ProductieManager.Properties.Resources.schedule_32_32;
            this.xspeciaalroosterbutton.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xspeciaalroosterbutton.Location = new System.Drawing.Point(3, 7);
            this.xspeciaalroosterbutton.Name = "xspeciaalroosterbutton";
            this.xspeciaalroosterbutton.Size = new System.Drawing.Size(1141, 40);
            this.xspeciaalroosterbutton.TabIndex = 0;
            this.xspeciaalroosterbutton.Text = "Rooster is momenteel inactief en zal geen tijd worden gemeten";
            this.xspeciaalroosterbutton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xspeciaalroosterbutton, "Wijzig rooster");
            this.xspeciaalroosterbutton.UseVisualStyleBackColor = true;
            this.xspeciaalroosterbutton.Click += new System.EventHandler(this.xspeciaalroosterbutton_Click);
            // 
            // xchatformbutton
            // 
            this.xchatformbutton.BackColor = System.Drawing.Color.Transparent;
            this.xchatformbutton.Dock = System.Windows.Forms.DockStyle.Left;
            this.xchatformbutton.FlatAppearance.BorderSize = 0;
            this.xchatformbutton.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xchatformbutton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xchatformbutton.Image = global::ProductieManager.Properties.Resources.conversation_chat_32x321;
            this.xchatformbutton.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xchatformbutton.Location = new System.Drawing.Point(480, 0);
            this.xchatformbutton.Name = "xchatformbutton";
            this.xchatformbutton.Size = new System.Drawing.Size(44, 43);
            this.xchatformbutton.TabIndex = 40;
            this.xchatformbutton.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xchatformbutton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xchatformbutton, "Cumuniceer met productie");
            this.xchatformbutton.UseVisualStyleBackColor = false;
            this.xchatformbutton.Click += new System.EventHandler(this.xchatformbutton_Click);
            // 
            // xallenotities
            // 
            this.xallenotities.BackColor = System.Drawing.Color.Transparent;
            this.xallenotities.Dock = System.Windows.Forms.DockStyle.Left;
            this.xallenotities.FlatAppearance.BorderSize = 0;
            this.xallenotities.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xallenotities.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xallenotities.Image = global::ProductieManager.Properties.Resources.education_school_memo_pad_notes_reminder_task_icon_133450;
            this.xallenotities.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xallenotities.Location = new System.Drawing.Point(436, 0);
            this.xallenotities.Name = "xallenotities";
            this.xallenotities.Size = new System.Drawing.Size(44, 43);
            this.xallenotities.TabIndex = 39;
            this.xallenotities.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xallenotities.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xallenotities, "Toon alle notities");
            this.xallenotities.UseVisualStyleBackColor = false;
            this.xallenotities.Click += new System.EventHandler(this.xallenotities_Click);
            // 
            // xsendemail
            // 
            this.xsendemail.BackColor = System.Drawing.Color.Transparent;
            this.xsendemail.Dock = System.Windows.Forms.DockStyle.Left;
            this.xsendemail.FlatAppearance.BorderSize = 0;
            this.xsendemail.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xsendemail.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsendemail.Image = global::ProductieManager.Properties.Resources.email_18961;
            this.xsendemail.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsendemail.Location = new System.Drawing.Point(392, 0);
            this.xsendemail.Name = "xsendemail";
            this.xsendemail.Size = new System.Drawing.Size(44, 43);
            this.xsendemail.TabIndex = 37;
            this.xsendemail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsendemail.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xsendemail, "Stuur Email");
            this.xsendemail.UseVisualStyleBackColor = false;
            this.xsendemail.Click += new System.EventHandler(this.xsendemail_Click);
            // 
            // xupdateallform
            // 
            this.xupdateallform.BackColor = System.Drawing.Color.Transparent;
            this.xupdateallform.Dock = System.Windows.Forms.DockStyle.Left;
            this.xupdateallform.FlatAppearance.BorderSize = 0;
            this.xupdateallform.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xupdateallform.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xupdateallform.Image = global::ProductieManager.Properties.Resources.task_update_folder_progress_icon_142270;
            this.xupdateallform.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xupdateallform.Location = new System.Drawing.Point(348, 0);
            this.xupdateallform.Name = "xupdateallform";
            this.xupdateallform.Size = new System.Drawing.Size(44, 43);
            this.xupdateallform.TabIndex = 36;
            this.xupdateallform.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xupdateallform.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xupdateallform, "Update alle productie tijden");
            this.xupdateallform.UseVisualStyleBackColor = false;
            this.xupdateallform.Click += new System.EventHandler(this.xupdateallform_Click);
            // 
            // xmateriaalverbruikb
            // 
            this.xmateriaalverbruikb.BackColor = System.Drawing.Color.Transparent;
            this.xmateriaalverbruikb.Dock = System.Windows.Forms.DockStyle.Left;
            this.xmateriaalverbruikb.FlatAppearance.BorderSize = 0;
            this.xmateriaalverbruikb.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xmateriaalverbruikb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xmateriaalverbruikb.Image = global::ProductieManager.Properties.Resources.graph_9_icon_icons_com_58019_32x32;
            this.xmateriaalverbruikb.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xmateriaalverbruikb.Location = new System.Drawing.Point(304, 0);
            this.xmateriaalverbruikb.Name = "xmateriaalverbruikb";
            this.xmateriaalverbruikb.Size = new System.Drawing.Size(44, 43);
            this.xmateriaalverbruikb.TabIndex = 41;
            this.xmateriaalverbruikb.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xmateriaalverbruikb.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xmateriaalverbruikb, "Materiaal verbruik");
            this.xmateriaalverbruikb.UseVisualStyleBackColor = false;
            this.xmateriaalverbruikb.Click += new System.EventHandler(this.xmateriaalverbruikb_Click);
            // 
            // xupdateb
            // 
            this.xupdateb.Dock = System.Windows.Forms.DockStyle.Right;
            this.xupdateb.FlatAppearance.BorderSize = 0;
            this.xupdateb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xupdateb.Image = global::ProductieManager.Properties.Resources.cloudrefresh_icon_icons_com_54403_32x32;
            this.xupdateb.Location = new System.Drawing.Point(835, 0);
            this.xupdateb.Name = "xupdateb";
            this.xupdateb.Size = new System.Drawing.Size(44, 43);
            this.xupdateb.TabIndex = 38;
            this.toolTip1.SetToolTip(this.xupdateb, "Controlleer op updates");
            this.xupdateb.UseVisualStyleBackColor = true;
            this.xupdateb.Click += new System.EventHandler(this.xUpdate_Click);
            // 
            // xoverzicht
            // 
            this.xoverzicht.BackColor = System.Drawing.Color.Transparent;
            this.xoverzicht.Dock = System.Windows.Forms.DockStyle.Left;
            this.xoverzicht.FlatAppearance.BorderSize = 0;
            this.xoverzicht.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xoverzicht.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xoverzicht.Image = global::ProductieManager.Properties.Resources.FocusEye_Img_32_32;
            this.xoverzicht.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xoverzicht.Location = new System.Drawing.Point(260, 0);
            this.xoverzicht.Name = "xoverzicht";
            this.xoverzicht.Size = new System.Drawing.Size(44, 43);
            this.xoverzicht.TabIndex = 34;
            this.xoverzicht.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xoverzicht.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xoverzicht, "Bekijk Overzicht");
            this.xoverzicht.UseVisualStyleBackColor = false;
            this.xoverzicht.Click += new System.EventHandler(this.xprodinfob_Click);
            // 
            // xlogbook
            // 
            this.xlogbook.BackColor = System.Drawing.Color.Transparent;
            this.xlogbook.Dock = System.Windows.Forms.DockStyle.Left;
            this.xlogbook.FlatAppearance.BorderSize = 0;
            this.xlogbook.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xlogbook.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xlogbook.Image = global::ProductieManager.Properties.Resources.activitylogmanager_104624;
            this.xlogbook.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xlogbook.Location = new System.Drawing.Point(216, 0);
            this.xlogbook.Name = "xlogbook";
            this.xlogbook.Size = new System.Drawing.Size(44, 43);
            this.xlogbook.TabIndex = 33;
            this.xlogbook.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xlogbook.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xlogbook, "Productie Logboek");
            this.xlogbook.UseVisualStyleBackColor = false;
            this.xlogbook.Click += new System.EventHandler(this.xtoonlogsb_Click);
            // 
            // xdbbewerkingen
            // 
            this.xdbbewerkingen.BackColor = System.Drawing.Color.Transparent;
            this.xdbbewerkingen.Dock = System.Windows.Forms.DockStyle.Left;
            this.xdbbewerkingen.FlatAppearance.BorderSize = 0;
            this.xdbbewerkingen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xdbbewerkingen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xdbbewerkingen.Image = global::ProductieManager.Properties.Resources.list_992_32_32;
            this.xdbbewerkingen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xdbbewerkingen.Location = new System.Drawing.Point(172, 0);
            this.xdbbewerkingen.Name = "xdbbewerkingen";
            this.xdbbewerkingen.Size = new System.Drawing.Size(44, 43);
            this.xdbbewerkingen.TabIndex = 35;
            this.xdbbewerkingen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xdbbewerkingen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xdbbewerkingen, "Bewerkinglijst Database");
            this.xdbbewerkingen.UseVisualStyleBackColor = false;
            this.xdbbewerkingen.Click += new System.EventHandler(this.xdbbewerkingen_Click);
            // 
            // xonderbrekeningen
            // 
            this.xonderbrekeningen.BackColor = System.Drawing.Color.Transparent;
            this.xonderbrekeningen.Dock = System.Windows.Forms.DockStyle.Left;
            this.xonderbrekeningen.FlatAppearance.BorderSize = 0;
            this.xonderbrekeningen.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xonderbrekeningen.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xonderbrekeningen.Image = global::ProductieManager.Properties.Resources.onderhoud32_32;
            this.xonderbrekeningen.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xonderbrekeningen.Location = new System.Drawing.Point(128, 0);
            this.xonderbrekeningen.Name = "xonderbrekeningen";
            this.xonderbrekeningen.Size = new System.Drawing.Size(44, 43);
            this.xonderbrekeningen.TabIndex = 32;
            this.xonderbrekeningen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xonderbrekeningen.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xonderbrekeningen, "Onderbrekeningen");
            this.xonderbrekeningen.UseVisualStyleBackColor = false;
            this.xonderbrekeningen.Click += new System.EventHandler(this.xallstoringenb_Click);
            // 
            // xbehvaardigheden
            // 
            this.xbehvaardigheden.BackColor = System.Drawing.Color.Transparent;
            this.xbehvaardigheden.Dock = System.Windows.Forms.DockStyle.Left;
            this.xbehvaardigheden.FlatAppearance.BorderSize = 0;
            this.xbehvaardigheden.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xbehvaardigheden.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xbehvaardigheden.Image = global::ProductieManager.Properties.Resources.key_skills;
            this.xbehvaardigheden.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xbehvaardigheden.Location = new System.Drawing.Point(84, 0);
            this.xbehvaardigheden.Name = "xbehvaardigheden";
            this.xbehvaardigheden.Size = new System.Drawing.Size(44, 43);
            this.xbehvaardigheden.TabIndex = 31;
            this.xbehvaardigheden.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xbehvaardigheden.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xbehvaardigheden, "Beheer Vaardigheden");
            this.xbehvaardigheden.UseVisualStyleBackColor = false;
            this.xbehvaardigheden.Click += new System.EventHandler(this.xallevaardighedenb_Click);
            // 
            // xbehpersoneel
            // 
            this.xbehpersoneel.BackColor = System.Drawing.Color.Transparent;
            this.xbehpersoneel.Dock = System.Windows.Forms.DockStyle.Left;
            this.xbehpersoneel.FlatAppearance.BorderSize = 0;
            this.xbehpersoneel.FlatAppearance.MouseOverBackColor = System.Drawing.Color.LightSkyBlue;
            this.xbehpersoneel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xbehpersoneel.Image = global::ProductieManager.Properties.Resources.users_12820;
            this.xbehpersoneel.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xbehpersoneel.Location = new System.Drawing.Point(40, 0);
            this.xbehpersoneel.Name = "xbehpersoneel";
            this.xbehpersoneel.Size = new System.Drawing.Size(44, 43);
            this.xbehpersoneel.TabIndex = 30;
            this.xbehpersoneel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xbehpersoneel.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xbehpersoneel, "Beheer Personeel");
            this.xbehpersoneel.UseVisualStyleBackColor = false;
            this.xbehpersoneel.Click += new System.EventHandler(this.xpersoneelb_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.xchatformbutton);
            this.panel6.Controls.Add(this.xallenotities);
            this.panel6.Controls.Add(this.xsendemail);
            this.panel6.Controls.Add(this.xupdateallform);
            this.panel6.Controls.Add(this.xmateriaalverbruikb);
            this.panel6.Controls.Add(this.xupdateb);
            this.panel6.Controls.Add(this.xoverzicht);
            this.panel6.Controls.Add(this.xlogbook);
            this.panel6.Controls.Add(this.xdbbewerkingen);
            this.panel6.Controls.Add(this.xonderbrekeningen);
            this.panel6.Controls.Add(this.xbehvaardigheden);
            this.panel6.Controls.Add(this.xbehpersoneel);
            this.panel6.Controls.Add(this.xsettingsb);
            this.panel6.Controls.Add(this.xloginb);
            this.panel6.Controls.Add(this.xaboutb);
            this.panel6.Controls.Add(this.pictureBox2);
            this.panel6.Controls.Add(this.pictureBox1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.panel6.Size = new System.Drawing.Size(1147, 43);
            this.panel6.TabIndex = 25;
            // 
            // xsettingsb
            // 
            this.xsettingsb.Dock = System.Windows.Forms.DockStyle.Right;
            this.xsettingsb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsettingsb.ForeColor = System.Drawing.Color.White;
            this.xsettingsb.Image = ((System.Drawing.Image)(resources.GetObject("xsettingsb.Image")));
            this.xsettingsb.Location = new System.Drawing.Point(879, 0);
            this.xsettingsb.Name = "xsettingsb";
            this.xsettingsb.Size = new System.Drawing.Size(44, 43);
            this.xsettingsb.TabIndex = 27;
            this.xsettingsb.UseVisualStyleBackColor = true;
            this.xsettingsb.Click += new System.EventHandler(this.xsettingsb_Click);
            // 
            // xloginb
            // 
            this.xloginb.Dock = System.Windows.Forms.DockStyle.Right;
            this.xloginb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xloginb.ForeColor = System.Drawing.Color.White;
            this.xloginb.Image = ((System.Drawing.Image)(resources.GetObject("xloginb.Image")));
            this.xloginb.Location = new System.Drawing.Point(923, 0);
            this.xloginb.Name = "xloginb";
            this.xloginb.Size = new System.Drawing.Size(44, 43);
            this.xloginb.TabIndex = 26;
            this.xloginb.UseVisualStyleBackColor = true;
            this.xloginb.Click += new System.EventHandler(this.xloginb_Click);
            // 
            // xaboutb
            // 
            this.xaboutb.Dock = System.Windows.Forms.DockStyle.Right;
            this.xaboutb.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xaboutb.ForeColor = System.Drawing.Color.White;
            this.xaboutb.Image = global::ProductieManager.Properties.Resources.info_15260;
            this.xaboutb.Location = new System.Drawing.Point(967, 0);
            this.xaboutb.Name = "xaboutb";
            this.xaboutb.Size = new System.Drawing.Size(44, 43);
            this.xaboutb.TabIndex = 29;
            this.xaboutb.UseVisualStyleBackColor = true;
            this.xaboutb.Click += new System.EventHandler(this.xaboutb_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(1011, 0);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(68, 43);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 28;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(1079, 0);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(68, 43);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 25;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(0, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(0, 0);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(0, 0);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 0;
            this.button4.Text = "button4";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // metroTabControl
            // 
            this.metroTabControl.AllowDrop = true;
            this.metroTabControl.Controls.Add(this.tabPage1);
            this.metroTabControl.Controls.Add(this.tabPage2);
            this.metroTabControl.Controls.Add(this.tabPage3);
            this.metroTabControl.Controls.Add(this.tabPage4);
            this.metroTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl.Location = new System.Drawing.Point(40, 97);
            this.metroTabControl.Name = "metroTabControl";
            this.metroTabControl.SelectedIndex = 3;
            this.metroTabControl.ShowToolTips = true;
            this.metroTabControl.Size = new System.Drawing.Size(1069, 462);
            this.metroTabControl.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTabControl.TabIndex = 28;
            this.metroTabControl.UseSelectable = true;
            this.metroTabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl1_SelectedIndexChanged);
            // 
            // tabPage1
            // 
            this.tabPage1.AutoScroll = true;
            this.tabPage1.Controls.Add(this.xproductieListControl1);
            this.tabPage1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.HorizontalScrollbar = true;
            this.tabPage1.HorizontalScrollbarBarColor = true;
            this.tabPage1.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPage1.HorizontalScrollbarSize = 10;
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1061, 420);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Producties";
            this.tabPage1.VerticalScrollbar = true;
            this.tabPage1.VerticalScrollbarBarColor = true;
            this.tabPage1.VerticalScrollbarHighlightOnWheel = false;
            this.tabPage1.VerticalScrollbarSize = 10;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.xbewerkingListControl);
            this.tabPage2.HorizontalScrollbarBarColor = true;
            this.tabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPage2.HorizontalScrollbarSize = 10;
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1061, 420);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Bewerkingen";
            this.tabPage2.VerticalScrollbarBarColor = true;
            this.tabPage2.VerticalScrollbarHighlightOnWheel = false;
            this.tabPage2.VerticalScrollbarSize = 10;
            // 
            // tabPage3
            // 
            this.tabPage3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.tabPage3.Controls.Add(this.werkPlekkenUI1);
            this.tabPage3.HorizontalScrollbarBarColor = true;
            this.tabPage3.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPage3.HorizontalScrollbarSize = 10;
            this.tabPage3.Location = new System.Drawing.Point(4, 38);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(1061, 420);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Werk Plaatsen";
            this.tabPage3.VerticalScrollbarBarColor = true;
            this.tabPage3.VerticalScrollbarHighlightOnWheel = false;
            this.tabPage3.VerticalScrollbarSize = 10;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.recentGereedMeldingenUI1);
            this.tabPage4.HorizontalScrollbarBarColor = true;
            this.tabPage4.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPage4.HorizontalScrollbarSize = 10;
            this.tabPage4.Location = new System.Drawing.Point(4, 38);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Size = new System.Drawing.Size(1061, 420);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Recente Gereedmeldingen";
            this.tabPage4.VerticalScrollbarBarColor = true;
            this.tabPage4.VerticalScrollbarHighlightOnWheel = false;
            this.tabPage4.VerticalScrollbarSize = 10;
            // 
            // xspeciaalroosterlabel
            // 
            this.xspeciaalroosterlabel.Controls.Add(this.xspeciaalroosterbutton);
            this.xspeciaalroosterlabel.Dock = System.Windows.Forms.DockStyle.Top;
            this.xspeciaalroosterlabel.Location = new System.Drawing.Point(0, 43);
            this.xspeciaalroosterlabel.Name = "xspeciaalroosterlabel";
            this.xspeciaalroosterlabel.Size = new System.Drawing.Size(1147, 54);
            this.xspeciaalroosterlabel.TabIndex = 29;
            this.xspeciaalroosterlabel.Visible = false;
            // 
            // xtabimages
            // 
            this.xtabimages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("xtabimages.ImageStream")));
            this.xtabimages.TransparentColor = System.Drawing.Color.Transparent;
            this.xtabimages.Images.SetKeyName(0, "page_document_16748_128_128.png");
            this.xtabimages.Images.SetKeyName(1, "operation.png");
            this.xtabimages.Images.SetKeyName(2, "iconfinder-technologymachineelectronic32_32.png");
            this.xtabimages.Images.SetKeyName(3, "ic_done_all_128_28243.png");
            // 
            // xproductieListControl1
            // 
            this.xproductieListControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xproductieListControl1.BackColor = System.Drawing.Color.White;
            this.xproductieListControl1.CanLoad = true;
            this.xproductieListControl1.EnableEntryFiltering = false;
            this.xproductieListControl1.EnableFiltering = true;
            this.xproductieListControl1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xproductieListControl1.IsBewerkingView = false;
            this.xproductieListControl1.Location = new System.Drawing.Point(3, 4);
            this.xproductieListControl1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xproductieListControl1.Name = "xproductieListControl1";
            this.xproductieListControl1.RemoveCustomItemIfNotValid = false;
            this.xproductieListControl1.SelectedItem = null;
            this.xproductieListControl1.Size = new System.Drawing.Size(1055, 412);
            this.xproductieListControl1.TabIndex = 2;
            this.xproductieListControl1.ValidHandler = null;
            // 
            // xbewerkingListControl
            // 
            this.xbewerkingListControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xbewerkingListControl.BackColor = System.Drawing.Color.White;
            this.xbewerkingListControl.CanLoad = true;
            this.xbewerkingListControl.EnableEntryFiltering = false;
            this.xbewerkingListControl.EnableFiltering = true;
            this.xbewerkingListControl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbewerkingListControl.IsBewerkingView = false;
            this.xbewerkingListControl.Location = new System.Drawing.Point(3, 4);
            this.xbewerkingListControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xbewerkingListControl.Name = "xbewerkingListControl";
            this.xbewerkingListControl.RemoveCustomItemIfNotValid = false;
            this.xbewerkingListControl.SelectedItem = null;
            this.xbewerkingListControl.Size = new System.Drawing.Size(1055, 412);
            this.xbewerkingListControl.TabIndex = 2;
            this.xbewerkingListControl.ValidHandler = null;
            // 
            // werkPlekkenUI1
            // 
            this.werkPlekkenUI1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.werkPlekkenUI1.BackColor = System.Drawing.Color.White;
            this.werkPlekkenUI1.Location = new System.Drawing.Point(3, 3);
            this.werkPlekkenUI1.Name = "werkPlekkenUI1";
            this.werkPlekkenUI1.SelectedWerkplek = null;
            this.werkPlekkenUI1.Size = new System.Drawing.Size(1051, 410);
            this.werkPlekkenUI1.TabIndex = 0;
            this.werkPlekkenUI1.WerkPlekClicked += new System.EventHandler(this.werkPlekkenUI1_WerkPlekClicked);
            // 
            // recentGereedMeldingenUI1
            // 
            this.recentGereedMeldingenUI1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.recentGereedMeldingenUI1.BackColor = System.Drawing.Color.White;
            this.recentGereedMeldingenUI1.EnableSync = true;
            this.recentGereedMeldingenUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recentGereedMeldingenUI1.Location = new System.Drawing.Point(2, 4);
            this.recentGereedMeldingenUI1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.recentGereedMeldingenUI1.Name = "recentGereedMeldingenUI1";
            this.recentGereedMeldingenUI1.Size = new System.Drawing.Size(1056, 420);
            this.recentGereedMeldingenUI1.SyncInterval = 30000;
            this.recentGereedMeldingenUI1.TabIndex = 2;
            // 
            // takenManager1
            // 
            this.takenManager1.BackColor = System.Drawing.Color.White;
            this.takenManager1.Dock = System.Windows.Forms.DockStyle.Right;
            this.takenManager1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.takenManager1.Location = new System.Drawing.Point(1109, 97);
            this.takenManager1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.takenManager1.Name = "takenManager1";
            this.takenManager1.SelectedItem = null;
            this.takenManager1.Size = new System.Drawing.Size(38, 462);
            this.takenManager1.TabIndex = 26;
            this.takenManager1.Visible = false;
            this.takenManager1.OnTaakClicked += new Rpm.Various.TaakHandler(this.takenManager1_OnTaakClicked);
            this.takenManager1.OnTaakUitvoeren += new Rpm.Various.TaakHandler(this.takenManager1_OnTaakUitvoeren);
            // 
            // mainMenu1
            // 
            this.mainMenu1.Dock = System.Windows.Forms.DockStyle.Left;
            this.mainMenu1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mainMenu1.IsExpanded = false;
            this.mainMenu1.Location = new System.Drawing.Point(0, 97);
            this.mainMenu1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            menuButton12.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton12.CombineImage = null;
            menuButton12.CombineScale = 0D;
            menuButton12.ContextMenu = null;
            menuButton12.Enabled = true;
            menuButton12.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton12.Image")));
            menuButton12.ImageSize = new System.Drawing.Size(32, 32);
            menuButton12.Index = 0;
            menuButton12.Name = "xniewproductie";
            menuButton12.Text = "Nieuwe Productie";
            menuButton12.Tooltip = "Maak een nieuwe productie aan";
            menuButton13.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton13.CombineImage = null;
            menuButton13.CombineScale = 1.5D;
            menuButton13.ContextMenu = null;
            menuButton13.Enabled = true;
            menuButton13.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton13.Image")));
            menuButton13.ImageSize = new System.Drawing.Size(32, 32);
            menuButton13.Index = 1;
            menuButton13.Name = "xopenproductie";
            menuButton13.Text = "Open Productie";
            menuButton13.Tooltip = "Open productie vanuit een pdf";
            menuButton14.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton14.CombineImage = global::ProductieManager.Properties.Resources.lightning_weather_storm_2781;
            menuButton14.CombineScale = 1.25D;
            menuButton14.ContextMenu = null;
            menuButton14.Enabled = true;
            menuButton14.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton14.Image")));
            menuButton14.ImageSize = new System.Drawing.Size(32, 32);
            menuButton14.Index = 2;
            menuButton14.Name = "xquickproductie";
            menuButton14.Text = "Simpel Productie";
            menuButton14.Tooltip = "Maak een nieuwe simpele productie";
            menuButton15.AccesLevel = Rpm.Various.AccesType.AlleenKijken;
            menuButton15.CombineImage = null;
            menuButton15.CombineScale = 1.5D;
            menuButton15.ContextMenu = null;
            menuButton15.Enabled = true;
            menuButton15.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton15.Image")));
            menuButton15.ImageSize = new System.Drawing.Size(32, 32);
            menuButton15.Index = 3;
            menuButton15.Name = "xcreateexcel";
            menuButton15.Text = "Excel Overzicht";
            menuButton15.Tooltip = "Maak excel overzicht";
            menuButton16.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton16.CombineImage = null;
            menuButton16.CombineScale = 1.5D;
            menuButton16.ContextMenu = null;
            menuButton16.Enabled = true;
            menuButton16.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton16.Image")));
            menuButton16.ImageSize = new System.Drawing.Size(32, 32);
            menuButton16.Index = 4;
            menuButton16.Name = "xupdatedb";
            menuButton16.Text = "Update Database";
            menuButton16.Tooltip = "Update database vanuit adere locaties";
            menuButton17.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton17.CombineImage = null;
            menuButton17.CombineScale = 1.5D;
            menuButton17.ContextMenu = null;
            menuButton17.Enabled = true;
            menuButton17.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton17.Image")));
            menuButton17.ImageSize = new System.Drawing.Size(32, 32);
            menuButton17.Index = 5;
            menuButton17.Name = "xlaaddb";
            menuButton17.Text = "Laad Database";
            menuButton17.Tooltip = "Laad een andere database";
            menuButton18.AccesLevel = Rpm.Various.AccesType.AlleenKijken;
            menuButton18.CombineImage = null;
            menuButton18.CombineScale = 1.5D;
            menuButton18.ContextMenu = null;
            menuButton18.Enabled = true;
            menuButton18.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton18.Image")));
            menuButton18.ImageSize = new System.Drawing.Size(32, 32);
            menuButton18.Index = 6;
            menuButton18.Name = "xstats";
            menuButton18.Text = "Toon Statistieken";
            menuButton18.Tooltip = "Toon statistieken van de afgelopen periode";
            menuButton19.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton19.CombineImage = global::ProductieManager.Properties.Resources.lightning_weather_storm_2781;
            menuButton19.CombineScale = 1.25D;
            menuButton19.ContextMenu = null;
            menuButton19.Enabled = true;
            menuButton19.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton19.Image")));
            menuButton19.ImageSize = new System.Drawing.Size(32, 32);
            menuButton19.Index = 7;
            menuButton19.Name = "xstoringmenubutton";
            menuButton19.Text = "Onderbreking";
            menuButton19.Tooltip = "Maak/Wijzig onderbreking";
            menuButton20.AccesLevel = Rpm.Various.AccesType.AlleenKijken;
            menuButton20.CombineImage = null;
            menuButton20.CombineScale = 1.25D;
            menuButton20.ContextMenu = null;
            menuButton20.Enabled = false;
            menuButton20.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton20.Image")));
            menuButton20.ImageSize = new System.Drawing.Size(32, 32);
            menuButton20.Index = 8;
            menuButton20.Name = "xbekijkproductiepdf";
            menuButton20.Text = "Bekijk Productieformulier";
            menuButton20.Tooltip = "Open productieformulier pdf";
            menuButton21.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton21.CombineImage = null;
            menuButton21.CombineScale = 1.5D;
            menuButton21.ContextMenu = null;
            menuButton21.Enabled = true;
            menuButton21.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton21.Image")));
            menuButton21.ImageSize = new System.Drawing.Size(32, 32);
            menuButton21.Index = 9;
            menuButton21.Name = "xroostermenubutton";
            menuButton21.Text = "Eigen Rooster";
            menuButton21.Tooltip = "Kies hier je eigen rooster voor elke periode";
            menuButton22.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton22.CombineImage = global::ProductieManager.Properties.Resources.play_button_icon_icons_com_60615;
            menuButton22.CombineScale = 1.5D;
            menuButton22.ContextMenu = null;
            menuButton22.Enabled = true;
            menuButton22.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton22.Image")));
            menuButton22.ImageSize = new System.Drawing.Size(32, 32);
            menuButton22.Index = 10;
            menuButton22.Name = "xopenproducties";
            menuButton22.Text = "Gestart Producties";
            menuButton22.Tooltip = "Open alle gestarte producties";
            this.mainMenu1.MenuButtons = new Various.MenuButton[] {
        menuButton12,
        menuButton13,
        menuButton14,
        menuButton15,
        menuButton16,
        menuButton17,
        menuButton18,
        menuButton19,
        menuButton20,
        menuButton21,
        menuButton22};
            this.mainMenu1.Name = "mainMenu1";
            this.mainMenu1.Size = new System.Drawing.Size(40, 462);
            this.mainMenu1.TabIndex = 27;
            this.mainMenu1.OnMenuClick += new System.EventHandler(this.mainMenu1_OnMenuClick);
            // 
            // ProductieView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.metroTabControl);
            this.Controls.Add(this.takenManager1);
            this.Controls.Add(this.mainMenu1);
            this.Controls.Add(this.xspeciaalroosterlabel);
            this.Controls.Add(this.panel6);
            this.DoubleBuffered = true;
            this.Name = "ProductieView";
            this.Size = new System.Drawing.Size(1147, 559);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.metroTabControl.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.xspeciaalroosterlabel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button xsettingsb;
        private System.Windows.Forms.Button xloginb;
        private System.Windows.Forms.Button xaboutb;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel6;
        private WerkPlekkenUI werkPlekkenUI1;
        private TakenManager takenManager1;
        private System.Windows.Forms.Button xoverzicht;
        private System.Windows.Forms.Button xlogbook;
        private System.Windows.Forms.Button xonderbrekeningen;
        private System.Windows.Forms.Button xbehvaardigheden;
        private System.Windows.Forms.Button xbehpersoneel;
        private MainMenu mainMenu1;
        private System.Windows.Forms.Button xdbbewerkingen;
        private System.Windows.Forms.Button xupdateallform;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private MetroFramework.Controls.MetroTabControl metroTabControl;
        private MetroFramework.Controls.MetroTabPage tabPage1;
        private MetroFramework.Controls.MetroTabPage tabPage2;
        private MetroFramework.Controls.MetroTabPage tabPage3;
        private System.Windows.Forms.Button xsendemail;
        private System.Windows.Forms.Panel xspeciaalroosterlabel;
        private System.Windows.Forms.Button xspeciaalroosterbutton;
        private System.Windows.Forms.Button xupdateb;
        private System.Windows.Forms.Button xallenotities;
        private System.Windows.Forms.Button xchatformbutton;
        private Button xmateriaalverbruikb;
        private ProductieListControl xproductieListControl1;
        private ProductieListControl xbewerkingListControl;
        private MetroTabPage tabPage4;
        private RecentGereedMeldingenUI recentGereedMeldingenUI1;
        private ImageList xtabimages;
    }
}
