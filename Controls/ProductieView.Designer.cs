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
            Various.MenuButton menuButton46 = new Various.MenuButton();
            Various.MenuButton menuButton47 = new Various.MenuButton();
            Various.MenuButton menuButton48 = new Various.MenuButton();
            Various.MenuButton menuButton49 = new Various.MenuButton();
            Various.MenuButton menuButton50 = new Various.MenuButton();
            Various.MenuButton menuButton51 = new Various.MenuButton();
            Various.MenuButton menuButton52 = new Various.MenuButton();
            Various.MenuButton menuButton53 = new Various.MenuButton();
            Various.MenuButton menuButton54 = new Various.MenuButton();
            Various.MenuButton menuButton55 = new Various.MenuButton();
            Various.MenuButton menuButton56 = new Various.MenuButton();
            Various.MenuButton menuButton57 = new Various.MenuButton();
            Various.MenuButton menuButton58 = new Various.MenuButton();
            Various.MenuButton menuButton59 = new Various.MenuButton();
            Various.MenuButton menuButton60 = new Various.MenuButton();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.xspeciaalroosterbutton = new System.Windows.Forms.Button();
            this.panel6 = new System.Windows.Forms.Panel();
            this.xToolButtons = new System.Windows.Forms.ToolStrip();
            this.xbehpersoneel = new System.Windows.Forms.ToolStripButton();
            this.xbehvaardigheden = new System.Windows.Forms.ToolStripButton();
            this.xonderbrekeningen = new System.Windows.Forms.ToolStripButton();
            this.xdbbewerkingen = new System.Windows.Forms.ToolStripButton();
            this.xoverzicht = new System.Windows.Forms.ToolStripButton();
            this.xmateriaalverbruikb = new System.Windows.Forms.ToolStripButton();
            this.xupdateallform = new System.Windows.Forms.ToolStripButton();
            this.xsendemail = new System.Windows.Forms.ToolStripButton();
            this.xallenotities = new System.Windows.Forms.ToolStripButton();
            this.xchatformbutton = new System.Windows.Forms.ToolStripButton();
            this.xproductieoverzichtb = new System.Windows.Forms.ToolStripButton();
            this.xsearchprodnr = new System.Windows.Forms.ToolStripButton();
            this.xopennewlijst = new System.Windows.Forms.ToolStripButton();
            this.xtoonartikels = new System.Windows.Forms.ToolStripButton();
            this.xloginb = new System.Windows.Forms.ToolStripButton();
            this.xaboutb = new System.Windows.Forms.ToolStripButton();
            this.xHelpButton = new System.Windows.Forms.ToolStripButton();
            this.xShowPreview = new System.Windows.Forms.ToolStripButton();
            this.xShowDaily = new System.Windows.Forms.ToolStripButton();
            this.xsettingsb = new System.Windows.Forms.ToolStripButton();
            this.xupdateb = new System.Windows.Forms.ToolStripButton();
            this.xopmerkingentoolstripbutton = new System.Windows.Forms.ToolStripButton();
            this.xSporenButton = new System.Windows.Forms.ToolStripButton();
            this.xverpakkingen = new System.Windows.Forms.ToolStripButton();
            this.xklachten = new System.Windows.Forms.ToolStripButton();
            this.xcorruptedfilesbutton = new System.Windows.Forms.ToolStripButton();
            this.xMissingTekening = new System.Windows.Forms.ToolStripButton();
            this.xMaakWeekOverzichtToolstrip = new System.Windows.Forms.ToolStripButton();
            this.xArtikelRecordsToolstripButton = new System.Windows.Forms.ToolStripButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.metroTabControl = new MetroFramework.Controls.MetroTabControl();
            this.tabPage2 = new MetroFramework.Controls.MetroTabPage();
            this.tabPage3 = new MetroFramework.Controls.MetroTabPage();
            this.tabPage4 = new MetroFramework.Controls.MetroTabPage();
            this.xspeciaalroosterlabel = new System.Windows.Forms.Panel();
            this.xtabimages = new System.Windows.Forms.ImageList(this.components);
            this.xbewerkingListControl = new Controls.ProductieListControl();
            this.werkPlekkenUI1 = new Controls.WerkPlekkenUI();
            this.recentGereedMeldingenUI1 = new Controls.RecentGereedMeldingenUI();
            this.takenManager1 = new Controls.TakenManager();
            this.mainMenu1 = new Controls.MainMenu();
            this.panel6.SuspendLayout();
            this.xToolButtons.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.metroTabControl.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage4.SuspendLayout();
            this.xspeciaalroosterlabel.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipIcon = System.Windows.Forms.ToolTipIcon.Info;
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
            this.xspeciaalroosterbutton.Location = new System.Drawing.Point(3, 2);
            this.xspeciaalroosterbutton.Name = "xspeciaalroosterbutton";
            this.xspeciaalroosterbutton.Size = new System.Drawing.Size(1115, 40);
            this.xspeciaalroosterbutton.TabIndex = 0;
            this.xspeciaalroosterbutton.Text = "Rooster is momenteel inactief en zal geen tijd worden gemeten";
            this.xspeciaalroosterbutton.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageBeforeText;
            this.toolTip1.SetToolTip(this.xspeciaalroosterbutton, "Wijzig rooster");
            this.xspeciaalroosterbutton.UseVisualStyleBackColor = true;
            this.xspeciaalroosterbutton.Click += new System.EventHandler(this.xspeciaalroosterbutton_Click);
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.xToolButtons);
            this.panel6.Controls.Add(this.pictureBox2);
            this.panel6.Controls.Add(this.pictureBox1);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel6.Location = new System.Drawing.Point(0, 0);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.panel6.Size = new System.Drawing.Size(1121, 43);
            this.panel6.TabIndex = 25;
            // 
            // xToolButtons
            // 
            this.xToolButtons.BackColor = System.Drawing.Color.White;
            this.xToolButtons.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.xToolButtons.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xbehpersoneel,
            this.xbehvaardigheden,
            this.xonderbrekeningen,
            this.xdbbewerkingen,
            this.xoverzicht,
            this.xmateriaalverbruikb,
            this.xupdateallform,
            this.xsendemail,
            this.xallenotities,
            this.xchatformbutton,
            this.xproductieoverzichtb,
            this.xsearchprodnr,
            this.xopennewlijst,
            this.xtoonartikels,
            this.xloginb,
            this.xaboutb,
            this.xHelpButton,
            this.xShowPreview,
            this.xShowDaily,
            this.xsettingsb,
            this.xupdateb,
            this.xopmerkingentoolstripbutton,
            this.xSporenButton,
            this.xverpakkingen,
            this.xklachten,
            this.xcorruptedfilesbutton,
            this.xMissingTekening,
            this.xMaakWeekOverzichtToolstrip,
            this.xArtikelRecordsToolstripButton});
            this.xToolButtons.Location = new System.Drawing.Point(5, 0);
            this.xToolButtons.Name = "xToolButtons";
            this.xToolButtons.Size = new System.Drawing.Size(980, 39);
            this.xToolButtons.TabIndex = 46;
            this.xToolButtons.Text = "toolStrip2";
            // 
            // xbehpersoneel
            // 
            this.xbehpersoneel.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xbehpersoneel.Image = global::ProductieManager.Properties.Resources.users_12820;
            this.xbehpersoneel.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xbehpersoneel.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xbehpersoneel.Name = "xbehpersoneel";
            this.xbehpersoneel.Size = new System.Drawing.Size(36, 36);
            this.xbehpersoneel.ToolTipText = "Toon alle medewerkers";
            this.xbehpersoneel.Click += new System.EventHandler(this.xpersoneelb_Click);
            // 
            // xbehvaardigheden
            // 
            this.xbehvaardigheden.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xbehvaardigheden.Image = global::ProductieManager.Properties.Resources.key_skills;
            this.xbehvaardigheden.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xbehvaardigheden.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xbehvaardigheden.Name = "xbehvaardigheden";
            this.xbehvaardigheden.Size = new System.Drawing.Size(36, 36);
            this.xbehvaardigheden.ToolTipText = "Toon alle vaardigheden";
            this.xbehvaardigheden.Click += new System.EventHandler(this.xallevaardighedenb_Click);
            // 
            // xonderbrekeningen
            // 
            this.xonderbrekeningen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xonderbrekeningen.Image = global::ProductieManager.Properties.Resources.onderhoud32_321;
            this.xonderbrekeningen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xonderbrekeningen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xonderbrekeningen.Name = "xonderbrekeningen";
            this.xonderbrekeningen.Size = new System.Drawing.Size(36, 36);
            this.xonderbrekeningen.ToolTipText = "Toon alle onderbrekeningen";
            this.xonderbrekeningen.Click += new System.EventHandler(this.xallstoringenb_Click);
            // 
            // xdbbewerkingen
            // 
            this.xdbbewerkingen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xdbbewerkingen.Image = global::ProductieManager.Properties.Resources.list_992_32_32;
            this.xdbbewerkingen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xdbbewerkingen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xdbbewerkingen.Name = "xdbbewerkingen";
            this.xdbbewerkingen.Size = new System.Drawing.Size(36, 36);
            this.xdbbewerkingen.ToolTipText = "Beheer Bewerkingen en de werkplaatsen";
            this.xdbbewerkingen.Click += new System.EventHandler(this.xdbbewerkingen_Click);
            // 
            // xoverzicht
            // 
            this.xoverzicht.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xoverzicht.Image = global::ProductieManager.Properties.Resources.FocusEye_Img_32_32;
            this.xoverzicht.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xoverzicht.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xoverzicht.Name = "xoverzicht";
            this.xoverzicht.Size = new System.Drawing.Size(36, 36);
            this.xoverzicht.ToolTipText = "Zoek producties op basis van criteria\'s";
            this.xoverzicht.Click += new System.EventHandler(this.xprodinfob_Click);
            // 
            // xmateriaalverbruikb
            // 
            this.xmateriaalverbruikb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xmateriaalverbruikb.Image = global::ProductieManager.Properties.Resources.graph_9_icon_icons_com_58019_32x32;
            this.xmateriaalverbruikb.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xmateriaalverbruikb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xmateriaalverbruikb.Name = "xmateriaalverbruikb";
            this.xmateriaalverbruikb.Size = new System.Drawing.Size(36, 36);
            this.xmateriaalverbruikb.ToolTipText = "Toon alle verbruikte materialen";
            this.xmateriaalverbruikb.Click += new System.EventHandler(this.xmateriaalverbruikb_Click);
            // 
            // xupdateallform
            // 
            this.xupdateallform.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xupdateallform.Image = global::ProductieManager.Properties.Resources.task_update_folder_progress_icon_142270;
            this.xupdateallform.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xupdateallform.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xupdateallform.Name = "xupdateallform";
            this.xupdateallform.Size = new System.Drawing.Size(36, 36);
            this.xupdateallform.ToolTipText = "Update alle producties";
            this.xupdateallform.Click += new System.EventHandler(this.xupdateallform_Click);
            // 
            // xsendemail
            // 
            this.xsendemail.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xsendemail.Image = global::ProductieManager.Properties.Resources.email_18961;
            this.xsendemail.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xsendemail.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xsendemail.Name = "xsendemail";
            this.xsendemail.Size = new System.Drawing.Size(36, 36);
            this.xsendemail.ToolTipText = "Verzend een email";
            this.xsendemail.Click += new System.EventHandler(this.xsendemail_Click);
            // 
            // xallenotities
            // 
            this.xallenotities.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xallenotities.Image = global::ProductieManager.Properties.Resources.education_school_memo_pad_notes_reminder_task_icon_133450;
            this.xallenotities.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xallenotities.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xallenotities.Name = "xallenotities";
            this.xallenotities.Size = new System.Drawing.Size(36, 36);
            this.xallenotities.ToolTipText = "Beheer alle notities";
            this.xallenotities.Click += new System.EventHandler(this.xallenotities_Click);
            // 
            // xchatformbutton
            // 
            this.xchatformbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xchatformbutton.Image = global::ProductieManager.Properties.Resources.conversation_chat_32x321;
            this.xchatformbutton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xchatformbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xchatformbutton.Name = "xchatformbutton";
            this.xchatformbutton.Size = new System.Drawing.Size(36, 36);
            this.xchatformbutton.ToolTipText = "Open ProductieChat";
            this.xchatformbutton.Click += new System.EventHandler(this.xchatformbutton_Click);
            // 
            // xproductieoverzichtb
            // 
            this.xproductieoverzichtb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xproductieoverzichtb.Image = global::ProductieManager.Properties.Resources.list_icon_icons_com_60651;
            this.xproductieoverzichtb.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xproductieoverzichtb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xproductieoverzichtb.Name = "xproductieoverzichtb";
            this.xproductieoverzichtb.Size = new System.Drawing.Size(36, 36);
            this.xproductieoverzichtb.ToolTipText = "Bekijk een ProductieOverzicht en de volgorde van het produceren";
            this.xproductieoverzichtb.Click += new System.EventHandler(this.xproductieoverzichtb_Click);
            // 
            // xsearchprodnr
            // 
            this.xsearchprodnr.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xsearchprodnr.Image = global::ProductieManager.Properties.Resources.search_page_document_32x32;
            this.xsearchprodnr.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xsearchprodnr.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xsearchprodnr.Name = "xsearchprodnr";
            this.xsearchprodnr.Size = new System.Drawing.Size(36, 36);
            this.xsearchprodnr.ToolTipText = "Zoek ProductieNr";
            this.xsearchprodnr.Click += new System.EventHandler(this.xsearchprodnr_Click);
            // 
            // xopennewlijst
            // 
            this.xopennewlijst.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xopennewlijst.Image = global::ProductieManager.Properties.Resources.view_screen_32x32;
            this.xopennewlijst.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xopennewlijst.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xopennewlijst.Name = "xopennewlijst";
            this.xopennewlijst.Size = new System.Drawing.Size(36, 36);
            this.xopennewlijst.ToolTipText = "Open een nieuw ProductieLijst venster";
            this.xopennewlijst.Click += new System.EventHandler(this.xopennewlijst_Click);
            // 
            // xtoonartikels
            // 
            this.xtoonartikels.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xtoonartikels.Image = global::ProductieManager.Properties.Resources.product_document_file_32x32;
            this.xtoonartikels.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xtoonartikels.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xtoonartikels.Name = "xtoonartikels";
            this.xtoonartikels.Size = new System.Drawing.Size(36, 36);
            this.xtoonartikels.Text = "toolStripButton18";
            this.xtoonartikels.ToolTipText = "Toon alle artikelen";
            this.xtoonartikels.Click += new System.EventHandler(this.xtoonartikels_Click);
            // 
            // xloginb
            // 
            this.xloginb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xloginb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xloginb.Image = global::ProductieManager.Properties.Resources.Login_37128__1_;
            this.xloginb.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xloginb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xloginb.Name = "xloginb";
            this.xloginb.Size = new System.Drawing.Size(36, 36);
            this.xloginb.ToolTipText = "Log In/Uit";
            this.xloginb.Click += new System.EventHandler(this.xloginb_Click);
            // 
            // xaboutb
            // 
            this.xaboutb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xaboutb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xaboutb.Image = global::ProductieManager.Properties.Resources.info_15260;
            this.xaboutb.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xaboutb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xaboutb.Name = "xaboutb";
            this.xaboutb.Size = new System.Drawing.Size(36, 36);
            this.xaboutb.ToolTipText = "ProductieManager Info";
            this.xaboutb.Click += new System.EventHandler(this.xaboutb_Click);
            // 
            // xHelpButton
            // 
            this.xHelpButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xHelpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xHelpButton.Image = global::ProductieManager.Properties.Resources.ios_8_Help_icon_43821;
            this.xHelpButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xHelpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xHelpButton.Name = "xHelpButton";
            this.xHelpButton.Size = new System.Drawing.Size(36, 36);
            this.xHelpButton.ToolTipText = "HelpDesk";
            this.xHelpButton.Click += new System.EventHandler(this.xHelpButton_Click);
            // 
            // xShowPreview
            // 
            this.xShowPreview.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xShowPreview.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xShowPreview.Image = global::ProductieManager.Properties.Resources.new_25355;
            this.xShowPreview.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xShowPreview.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xShowPreview.Name = "xShowPreview";
            this.xShowPreview.Size = new System.Drawing.Size(36, 36);
            this.xShowPreview.ToolTipText = "Toon laatste aanpassingen";
            this.xShowPreview.Click += new System.EventHandler(this.xShowPreview_Click);
            // 
            // xShowDaily
            // 
            this.xShowDaily.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xShowDaily.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xShowDaily.Image = global::ProductieManager.Properties.Resources.Make_Lead_32x32;
            this.xShowDaily.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xShowDaily.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xShowDaily.Name = "xShowDaily";
            this.xShowDaily.Size = new System.Drawing.Size(36, 36);
            this.xShowDaily.ToolTipText = "Controleer op bijzonderheden";
            this.xShowDaily.Click += new System.EventHandler(this.xShowDaily_Click);
            // 
            // xsettingsb
            // 
            this.xsettingsb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xsettingsb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xsettingsb.Image = global::ProductieManager.Properties.Resources.ccsm_103993;
            this.xsettingsb.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xsettingsb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xsettingsb.Name = "xsettingsb";
            this.xsettingsb.Size = new System.Drawing.Size(36, 36);
            this.xsettingsb.ToolTipText = "Beheer Instellingen";
            this.xsettingsb.Click += new System.EventHandler(this.xsettingsb_Click);
            // 
            // xupdateb
            // 
            this.xupdateb.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.xupdateb.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xupdateb.Image = global::ProductieManager.Properties.Resources.clouddown_icon_icons_com_54405;
            this.xupdateb.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xupdateb.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xupdateb.Name = "xupdateb";
            this.xupdateb.Size = new System.Drawing.Size(36, 36);
            this.xupdateb.ToolTipText = "Controleer op een update";
            this.xupdateb.Click += new System.EventHandler(this.xUpdate_Click);
            // 
            // xopmerkingentoolstripbutton
            // 
            this.xopmerkingentoolstripbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xopmerkingentoolstripbutton.Image = global::ProductieManager.Properties.Resources.notes_office_page_papers_32x32;
            this.xopmerkingentoolstripbutton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xopmerkingentoolstripbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xopmerkingentoolstripbutton.Name = "xopmerkingentoolstripbutton";
            this.xopmerkingentoolstripbutton.Size = new System.Drawing.Size(36, 36);
            this.xopmerkingentoolstripbutton.ToolTipText = "Bekijk, wijzig of voeg toe een opmerking, vraag of een verzoek";
            this.xopmerkingentoolstripbutton.Click += new System.EventHandler(this.xopmerkingentoolstripbutton_Click);
            // 
            // xSporenButton
            // 
            this.xSporenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xSporenButton.Image = global::ProductieManager.Properties.Resources.geometry_measure_32x32;
            this.xSporenButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xSporenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xSporenButton.Name = "xSporenButton";
            this.xSporenButton.Size = new System.Drawing.Size(36, 36);
            this.xSporenButton.ToolTipText = "Alle aangepaste sporen";
            this.xSporenButton.Click += new System.EventHandler(this.xSporenButton_Click);
            // 
            // xverpakkingen
            // 
            this.xverpakkingen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xverpakkingen.Image = global::ProductieManager.Properties.Resources.Box_1_35524;
            this.xverpakkingen.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xverpakkingen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xverpakkingen.Name = "xverpakkingen";
            this.xverpakkingen.Size = new System.Drawing.Size(36, 36);
            this.xverpakkingen.ToolTipText = "Alle aangepaste verpakkingen";
            this.xverpakkingen.Click += new System.EventHandler(this.xverpakkingen_Click);
            // 
            // xklachten
            // 
            this.xklachten.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xklachten.Image = global::ProductieManager.Properties.Resources.Leave_80_icon_icons_com_57305;
            this.xklachten.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xklachten.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xklachten.Name = "xklachten";
            this.xklachten.Size = new System.Drawing.Size(36, 36);
            this.xklachten.ToolTipText = "Order Klachten";
            this.xklachten.Click += new System.EventHandler(this.xklachten_Click);
            // 
            // xcorruptedfilesbutton
            // 
            this.xcorruptedfilesbutton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xcorruptedfilesbutton.Image = global::ProductieManager.Properties.Resources.error_notification_32x32;
            this.xcorruptedfilesbutton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xcorruptedfilesbutton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xcorruptedfilesbutton.Name = "xcorruptedfilesbutton";
            this.xcorruptedfilesbutton.Size = new System.Drawing.Size(36, 36);
            this.xcorruptedfilesbutton.ToolTipText = "Toon alle corrupted bestanden";
            this.xcorruptedfilesbutton.Visible = false;
            this.xcorruptedfilesbutton.Click += new System.EventHandler(this.xcorruptedfilesbutton_Click);
            // 
            // xMissingTekening
            // 
            this.xMissingTekening.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xMissingTekening.Image = global::ProductieManager.Properties.Resources.ooo_draw_10002;
            this.xMissingTekening.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xMissingTekening.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xMissingTekening.Name = "xMissingTekening";
            this.xMissingTekening.Size = new System.Drawing.Size(36, 36);
            this.xMissingTekening.ToolTipText = "Creëer Ontbrekende Tekening Overzicht";
            this.xMissingTekening.Visible = false;
            this.xMissingTekening.Click += new System.EventHandler(this.xMissingTekening_Click);
            // 
            // xMaakWeekOverzichtToolstrip
            // 
            this.xMaakWeekOverzichtToolstrip.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xMaakWeekOverzichtToolstrip.Image = global::ProductieManager.Properties.Resources.microsoft_excel_22733;
            this.xMaakWeekOverzichtToolstrip.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xMaakWeekOverzichtToolstrip.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xMaakWeekOverzichtToolstrip.Name = "xMaakWeekOverzichtToolstrip";
            this.xMaakWeekOverzichtToolstrip.Size = new System.Drawing.Size(36, 36);
            this.xMaakWeekOverzichtToolstrip.Text = "Creëer Week Overzicht";
            this.xMaakWeekOverzichtToolstrip.Click += new System.EventHandler(this.xMaakWeekOverzichtToolstrip_Click);
            // 
            // xArtikelRecordsToolstripButton
            // 
            this.xArtikelRecordsToolstripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.xArtikelRecordsToolstripButton.Image = global::ProductieManager.Properties.Resources.time_management_tasks_32x32;
            this.xArtikelRecordsToolstripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.xArtikelRecordsToolstripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.xArtikelRecordsToolstripButton.Name = "xArtikelRecordsToolstripButton";
            this.xArtikelRecordsToolstripButton.Size = new System.Drawing.Size(36, 36);
            this.xArtikelRecordsToolstripButton.Text = "Toon Artikel Records";
            this.xArtikelRecordsToolstripButton.Click += new System.EventHandler(this.xArtikelRecordsToolstripButton_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Dock = System.Windows.Forms.DockStyle.Right;
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(985, 0);
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
            this.pictureBox1.Location = new System.Drawing.Point(1053, 0);
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
            this.metroTabControl.Controls.Add(this.tabPage2);
            this.metroTabControl.Controls.Add(this.tabPage3);
            this.metroTabControl.Controls.Add(this.tabPage4);
            this.metroTabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.metroTabControl.Location = new System.Drawing.Point(40, 88);
            this.metroTabControl.Name = "metroTabControl";
            this.metroTabControl.SelectedIndex = 0;
            this.metroTabControl.ShowToolTips = true;
            this.metroTabControl.Size = new System.Drawing.Size(1043, 538);
            this.metroTabControl.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroTabControl.TabIndex = 28;
            this.metroTabControl.UseSelectable = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.xbewerkingListControl);
            this.tabPage2.HorizontalScrollbarBarColor = true;
            this.tabPage2.HorizontalScrollbarHighlightOnWheel = false;
            this.tabPage2.HorizontalScrollbarSize = 10;
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.tabPage2.Size = new System.Drawing.Size(1035, 496);
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
            this.tabPage3.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.tabPage3.Size = new System.Drawing.Size(1035, 496);
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
            this.tabPage4.Padding = new System.Windows.Forms.Padding(5, 5, 5, 0);
            this.tabPage4.Size = new System.Drawing.Size(1035, 496);
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
            this.xspeciaalroosterlabel.Size = new System.Drawing.Size(1121, 45);
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
            // xbewerkingListControl
            // 
            this.xbewerkingListControl.AutoScroll = true;
            this.xbewerkingListControl.BackColor = System.Drawing.Color.White;
            this.xbewerkingListControl.CanLoad = true;
            this.xbewerkingListControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xbewerkingListControl.EnableEntryFiltering = true;
            this.xbewerkingListControl.EnableFiltering = true;
            this.xbewerkingListControl.EnableSync = false;
            this.xbewerkingListControl.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xbewerkingListControl.IsBewerkingView = true;
            this.xbewerkingListControl.ListName = "Bewerkingen";
            this.xbewerkingListControl.Location = new System.Drawing.Point(5, 5);
            this.xbewerkingListControl.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.xbewerkingListControl.Name = "xbewerkingListControl";
            this.xbewerkingListControl.RemoveCustomItemIfNotValid = false;
            this.xbewerkingListControl.SelectedItem = null;
            this.xbewerkingListControl.Size = new System.Drawing.Size(1025, 491);
            this.xbewerkingListControl.TabIndex = 2;
            this.xbewerkingListControl.ValidHandler = null;
            // 
            // werkPlekkenUI1
            // 
            this.werkPlekkenUI1.BackColor = System.Drawing.Color.White;
            this.werkPlekkenUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.werkPlekkenUI1.EnableSync = true;
            this.werkPlekkenUI1.Location = new System.Drawing.Point(5, 5);
            this.werkPlekkenUI1.Name = "werkPlekkenUI1";
            this.werkPlekkenUI1.SelectedWerkplek = null;
            this.werkPlekkenUI1.Size = new System.Drawing.Size(1021, 487);
            this.werkPlekkenUI1.TabIndex = 0;
            // 
            // recentGereedMeldingenUI1
            // 
            this.recentGereedMeldingenUI1.BackColor = System.Drawing.Color.White;
            this.recentGereedMeldingenUI1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.recentGereedMeldingenUI1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.recentGereedMeldingenUI1.Location = new System.Drawing.Point(5, 5);
            this.recentGereedMeldingenUI1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.recentGereedMeldingenUI1.Name = "recentGereedMeldingenUI1";
            this.recentGereedMeldingenUI1.Size = new System.Drawing.Size(1025, 491);
            this.recentGereedMeldingenUI1.TabIndex = 2;
            // 
            // takenManager1
            // 
            this.takenManager1.BackColor = System.Drawing.Color.White;
            this.takenManager1.Dock = System.Windows.Forms.DockStyle.Right;
            this.takenManager1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.takenManager1.Location = new System.Drawing.Point(1083, 88);
            this.takenManager1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.takenManager1.Name = "takenManager1";
            this.takenManager1.SelectedItem = null;
            this.takenManager1.Size = new System.Drawing.Size(38, 538);
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
            this.mainMenu1.Location = new System.Drawing.Point(0, 88);
            this.mainMenu1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            menuButton46.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton46.CombineImage = null;
            menuButton46.CombineScale = 0D;
            menuButton46.ContextMenu = null;
            menuButton46.Enabled = true;
            menuButton46.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton46.Image")));
            menuButton46.ImageSize = new System.Drawing.Size(32, 32);
            menuButton46.Index = 0;
            menuButton46.Name = "xniewproductie";
            menuButton46.Text = "Nieuwe Productie";
            menuButton46.Tooltip = "Maak een nieuwe productie aan";
            menuButton47.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton47.CombineImage = null;
            menuButton47.CombineScale = 1.5D;
            menuButton47.ContextMenu = null;
            menuButton47.Enabled = true;
            menuButton47.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton47.Image")));
            menuButton47.ImageSize = new System.Drawing.Size(32, 32);
            menuButton47.Index = 1;
            menuButton47.Name = "xopenproductie";
            menuButton47.Text = "Open Productie";
            menuButton47.Tooltip = "Open productie vanuit een pdf";
            menuButton48.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton48.CombineImage = global::ProductieManager.Properties.Resources.lightning_weather_storm_2781;
            menuButton48.CombineScale = 1.25D;
            menuButton48.ContextMenu = null;
            menuButton48.Enabled = true;
            menuButton48.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton48.Image")));
            menuButton48.ImageSize = new System.Drawing.Size(32, 32);
            menuButton48.Index = 2;
            menuButton48.Name = "xquickproductie";
            menuButton48.Text = "Simpel Productie";
            menuButton48.Tooltip = "Maak een nieuwe simpele productie";
            menuButton49.AccesLevel = Rpm.Various.AccesType.AlleenKijken;
            menuButton49.CombineImage = global::ProductieManager.Properties.Resources.search_locate_find_6278;
            menuButton49.CombineScale = 1.5D;
            menuButton49.ContextMenu = null;
            menuButton49.Enabled = true;
            menuButton49.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton49.Image")));
            menuButton49.ImageSize = new System.Drawing.Size(32, 32);
            menuButton49.Index = 3;
            menuButton49.Name = "xSearchTekening";
            menuButton49.Text = "Zoek WerkTekening";
            menuButton49.Tooltip = "Zoek een werktekening o.b.v een artikelnr (Ctrl+T)";
            menuButton50.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton50.CombineImage = null;
            menuButton50.CombineScale = 1.5D;
            menuButton50.ContextMenu = null;
            menuButton50.Enabled = true;
            menuButton50.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton50.Image")));
            menuButton50.ImageSize = new System.Drawing.Size(32, 32);
            menuButton50.Index = 4;
            menuButton50.Name = "xchangeaantal";
            menuButton50.Text = "Aantal Gemaakt";
            menuButton50.Tooltip = "Wijzig aantal gemaakt van alle actieve werkplaatsen";
            menuButton51.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton51.CombineImage = null;
            menuButton51.CombineScale = 1.5D;
            menuButton51.ContextMenu = null;
            menuButton51.Enabled = true;
            menuButton51.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton51.Image")));
            menuButton51.ImageSize = new System.Drawing.Size(32, 32);
            menuButton51.Index = 5;
            menuButton51.Name = "xwerkplaatsindeling";
            menuButton51.Text = "Personeel Indeling";
            menuButton51.Tooltip = "Beheer personeel leden en hun producties";
            menuButton52.AccesLevel = Rpm.Various.AccesType.AlleenKijken;
            menuButton52.CombineImage = null;
            menuButton52.CombineScale = 1.5D;
            menuButton52.ContextMenu = null;
            menuButton52.Enabled = true;
            menuButton52.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton52.Image")));
            menuButton52.ImageSize = new System.Drawing.Size(32, 32);
            menuButton52.Index = 6;
            menuButton52.Name = "xverbruik";
            menuButton52.Text = "Bereken Verbruik";
            menuButton52.Tooltip = "Bereken verbruik";
            menuButton53.AccesLevel = Rpm.Various.AccesType.ProductieAdvance;
            menuButton53.CombineImage = null;
            menuButton53.CombineScale = 1.5D;
            menuButton53.ContextMenu = null;
            menuButton53.Enabled = true;
            menuButton53.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton53.Image")));
            menuButton53.ImageSize = new System.Drawing.Size(32, 32);
            menuButton53.Index = 7;
            menuButton53.Name = "xupdateforms";
            menuButton53.Text = "Update Formulieren";
            menuButton53.Tooltip = "Update producties vanuit een folder met ProductieFormulieren pdfs";
            menuButton54.AccesLevel = Rpm.Various.AccesType.AlleenKijken;
            menuButton54.CombineImage = null;
            menuButton54.CombineScale = 1.5D;
            menuButton54.ContextMenu = null;
            menuButton54.Enabled = true;
            menuButton54.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton54.Image")));
            menuButton54.ImageSize = new System.Drawing.Size(32, 32);
            menuButton54.Index = 8;
            menuButton54.Name = "xcreateexcel";
            menuButton54.Text = "Excel Overzicht";
            menuButton54.Tooltip = "Maak excel overzicht";
            menuButton55.AccesLevel = Rpm.Various.AccesType.ProductieAdvance;
            menuButton55.CombineImage = null;
            menuButton55.CombineScale = 1.5D;
            menuButton55.ContextMenu = null;
            menuButton55.Enabled = true;
            menuButton55.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton55.Image")));
            menuButton55.ImageSize = new System.Drawing.Size(32, 32);
            menuButton55.Index = 9;
            menuButton55.Name = "xupdatedb";
            menuButton55.Text = "Update Database";
            menuButton55.Tooltip = "Update database vanuit adere locaties";
            menuButton56.AccesLevel = Rpm.Various.AccesType.ProductieAdvance;
            menuButton56.CombineImage = null;
            menuButton56.CombineScale = 1.5D;
            menuButton56.ContextMenu = null;
            menuButton56.Enabled = true;
            menuButton56.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton56.Image")));
            menuButton56.ImageSize = new System.Drawing.Size(32, 32);
            menuButton56.Index = 10;
            menuButton56.Name = "xlaaddb";
            menuButton56.Text = "Laad Database";
            menuButton56.Tooltip = "Laad een andere database";
            menuButton57.AccesLevel = Rpm.Various.AccesType.AlleenKijken;
            menuButton57.CombineImage = null;
            menuButton57.CombineScale = 1.5D;
            menuButton57.ContextMenu = null;
            menuButton57.Enabled = true;
            menuButton57.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton57.Image")));
            menuButton57.ImageSize = new System.Drawing.Size(32, 32);
            menuButton57.Index = 11;
            menuButton57.Name = "xstats";
            menuButton57.Text = "Toon Statistieken";
            menuButton57.Tooltip = "Toon statistieken van de afgelopen periode";
            menuButton58.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton58.CombineImage = null;
            menuButton58.CombineScale = 1.5D;
            menuButton58.ContextMenu = null;
            menuButton58.Enabled = true;
            menuButton58.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton58.Image")));
            menuButton58.ImageSize = new System.Drawing.Size(32, 32);
            menuButton58.Index = 12;
            menuButton58.Name = "xroostermenubutton";
            menuButton58.Text = "WerkRooster";
            menuButton58.Tooltip = "Kies Werkrooster";
            menuButton59.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton59.CombineImage = null;
            menuButton59.CombineScale = 1.5D;
            menuButton59.ContextMenu = null;
            menuButton59.Enabled = false;
            menuButton59.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton59.Image")));
            menuButton59.ImageSize = new System.Drawing.Size(32, 32);
            menuButton59.Index = 13;
            menuButton59.Name = "xspecialeroosterbutton";
            menuButton59.Text = "Speciale Roosters";
            menuButton59.Tooltip = "Beheer speciale roosters";
            menuButton60.AccesLevel = Rpm.Various.AccesType.ProductieBasis;
            menuButton60.CombineImage = global::ProductieManager.Properties.Resources.play_button_icon_icons_com_60615;
            menuButton60.CombineScale = 1.5D;
            menuButton60.ContextMenu = null;
            menuButton60.Enabled = true;
            menuButton60.Image = ((System.Drawing.Bitmap)(resources.GetObject("menuButton60.Image")));
            menuButton60.ImageSize = new System.Drawing.Size(32, 32);
            menuButton60.Index = 14;
            menuButton60.Name = "xopenproducties";
            menuButton60.Text = "Gestart Producties";
            menuButton60.Tooltip = "Open alle gestarte producties";
            this.mainMenu1.MenuButtons = new Various.MenuButton[] {
        menuButton46,
        menuButton47,
        menuButton48,
        menuButton49,
        menuButton50,
        menuButton51,
        menuButton52,
        menuButton53,
        menuButton54,
        menuButton55,
        menuButton56,
        menuButton57,
        menuButton58,
        menuButton59,
        menuButton60};
            this.mainMenu1.Name = "mainMenu1";
            this.mainMenu1.Size = new System.Drawing.Size(40, 538);
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
            this.Size = new System.Drawing.Size(1121, 626);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.xToolButtons.ResumeLayout(false);
            this.xToolButtons.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.metroTabControl.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage4.ResumeLayout(false);
            this.xspeciaalroosterlabel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panel6;
        private WerkPlekkenUI werkPlekkenUI1;
        private TakenManager takenManager1;
        private MainMenu mainMenu1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button4;
        private MetroFramework.Controls.MetroTabControl metroTabControl;
        private MetroFramework.Controls.MetroTabPage tabPage2;
        private MetroFramework.Controls.MetroTabPage tabPage3;
        private System.Windows.Forms.Panel xspeciaalroosterlabel;
        private System.Windows.Forms.Button xspeciaalroosterbutton;
        private ProductieListControl xbewerkingListControl;
        private MetroTabPage tabPage4;
        private RecentGereedMeldingenUI recentGereedMeldingenUI1;
        private ImageList xtabimages;
        private ToolStrip xToolButtons;
        private ToolStripButton xbehpersoneel;
        private ToolStripButton xbehvaardigheden;
        private ToolStripButton xonderbrekeningen;
        private ToolStripButton xdbbewerkingen;
        private ToolStripButton xoverzicht;
        private ToolStripButton xmateriaalverbruikb;
        private ToolStripButton xupdateallform;
        private ToolStripButton xsendemail;
        private ToolStripButton xallenotities;
        private ToolStripButton xchatformbutton;
        private ToolStripButton xproductieoverzichtb;
        private ToolStripButton xsearchprodnr;
        private ToolStripButton xopennewlijst;
        private ToolStripButton xtoonartikels;
        private ToolStripButton xaboutb;
        private ToolStripButton xloginb;
        private ToolStripButton xsettingsb;
        private ToolStripButton xupdateb;
        private ToolStripButton xopmerkingentoolstripbutton;
        private ToolStripButton xShowPreview;
        private ToolStripButton xHelpButton;
        private ToolStripButton xcorruptedfilesbutton;
        private ToolStripButton xMissingTekening;
        private ToolStripButton xklachten;
        private ToolStripButton xverpakkingen;
        private ToolStripButton xMaakWeekOverzichtToolstrip;
        private ToolStripButton xArtikelRecordsToolstripButton;
        private ToolStripButton xShowDaily;
        private ToolStripButton xSporenButton;
    }
}
