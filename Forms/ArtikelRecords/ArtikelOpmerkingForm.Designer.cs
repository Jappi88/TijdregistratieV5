namespace Forms.ArtikelRecords
{
    partial class ArtikelOpmerkingForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ArtikelOpmerkingForm));
            this.panel2 = new System.Windows.Forms.Panel();
            this.xVoorbeeld = new System.Windows.Forms.Button();
            this.xok = new System.Windows.Forms.Button();
            this.xsluiten = new System.Windows.Forms.Button();
            this.xGelezenDoorPanel = new System.Windows.Forms.Panel();
            this.xGelezenDoorList = new BrightIdeasSoftware.ObjectListView();
            this.olvColumn1 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.olvColumn2 = ((BrightIdeasSoftware.OLVColumn)(new BrightIdeasSoftware.OLVColumn()));
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.verwijderenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.xGelezenDoorImageList = new System.Windows.Forms.ImageList(this.components);
            this.xControlContainer = new System.Windows.Forms.Panel();
            this.xtitletextbox = new MetroFramework.Controls.MetroTextBox();
            this.xontvangerstrip = new System.Windows.Forms.MenuStrip();
            this.xontvangermenuitem = new System.Windows.Forms.ToolStripMenuItem();
            this.xnieuweontvanger = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.xFilterWaarde = new System.Windows.Forms.NumericUpDown();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.xImage = new System.Windows.Forms.PictureBox();
            this.xOpmerking = new MetroFramework.Controls.MetroTextBox();
            this.xFilterTypeCombo = new MetroFramework.Controls.MetroComboBox();
            this.xFilterCombo = new MetroFramework.Controls.MetroComboBox();
            this.xfilterOp = new MetroFramework.Controls.MetroComboBox();
            this.panel2.SuspendLayout();
            this.xGelezenDoorPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xGelezenDoorList)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.xControlContainer.SuspendLayout();
            this.xontvangerstrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xFilterWaarde)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xImage)).BeginInit();
            this.SuspendLayout();
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.xVoorbeeld);
            this.panel2.Controls.Add(this.xok);
            this.panel2.Controls.Add(this.xsluiten);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(20, 530);
            this.panel2.Margin = new System.Windows.Forms.Padding(4);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(5);
            this.panel2.Size = new System.Drawing.Size(664, 42);
            this.panel2.TabIndex = 6;
            // 
            // xVoorbeeld
            // 
            this.xVoorbeeld.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xVoorbeeld.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xVoorbeeld.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xVoorbeeld.Image = global::ProductieManager.Properties.Resources.default_opmerking_16757_32x32;
            this.xVoorbeeld.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xVoorbeeld.Location = new System.Drawing.Point(238, 5);
            this.xVoorbeeld.Margin = new System.Windows.Forms.Padding(4);
            this.xVoorbeeld.Name = "xVoorbeeld";
            this.xVoorbeeld.Size = new System.Drawing.Size(135, 32);
            this.xVoorbeeld.TabIndex = 6;
            this.xVoorbeeld.Text = "Voorbeeld";
            this.xVoorbeeld.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xVoorbeeld.UseVisualStyleBackColor = true;
            this.xVoorbeeld.Click += new System.EventHandler(this.xVoorbeeld_Click);
            // 
            // xok
            // 
            this.xok.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xok.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xok.Image = global::ProductieManager.Properties.Resources.check_1582;
            this.xok.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xok.Location = new System.Drawing.Point(381, 5);
            this.xok.Margin = new System.Windows.Forms.Padding(4);
            this.xok.Name = "xok";
            this.xok.Size = new System.Drawing.Size(135, 32);
            this.xok.TabIndex = 7;
            this.xok.Text = "Toevoegen";
            this.xok.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xok.UseVisualStyleBackColor = true;
            this.xok.Click += new System.EventHandler(this.xok_Click);
            // 
            // xsluiten
            // 
            this.xsluiten.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.xsluiten.Dock = System.Windows.Forms.DockStyle.Right;
            this.xsluiten.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.xsluiten.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xsluiten.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.xsluiten.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.xsluiten.Location = new System.Drawing.Point(524, 5);
            this.xsluiten.Margin = new System.Windows.Forms.Padding(4);
            this.xsluiten.Name = "xsluiten";
            this.xsluiten.Size = new System.Drawing.Size(135, 32);
            this.xsluiten.TabIndex = 8;
            this.xsluiten.Text = "Annuleren";
            this.xsluiten.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.xsluiten.UseVisualStyleBackColor = true;
            this.xsluiten.Click += new System.EventHandler(this.xsluiten_Click);
            // 
            // xGelezenDoorPanel
            // 
            this.xGelezenDoorPanel.Controls.Add(this.xGelezenDoorList);
            this.xGelezenDoorPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.xGelezenDoorPanel.Location = new System.Drawing.Point(513, 60);
            this.xGelezenDoorPanel.Name = "xGelezenDoorPanel";
            this.xGelezenDoorPanel.Padding = new System.Windows.Forms.Padding(5);
            this.xGelezenDoorPanel.Size = new System.Drawing.Size(171, 470);
            this.xGelezenDoorPanel.TabIndex = 7;
            // 
            // xGelezenDoorList
            // 
            this.xGelezenDoorList.AllColumns.Add(this.olvColumn1);
            this.xGelezenDoorList.AllColumns.Add(this.olvColumn2);
            this.xGelezenDoorList.AlternateRowBackColor = System.Drawing.Color.AliceBlue;
            this.xGelezenDoorList.CellEditUseWholeCell = false;
            this.xGelezenDoorList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.olvColumn1,
            this.olvColumn2});
            this.xGelezenDoorList.ContextMenuStrip = this.contextMenuStrip1;
            this.xGelezenDoorList.Cursor = System.Windows.Forms.Cursors.Default;
            this.xGelezenDoorList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xGelezenDoorList.FullRowSelect = true;
            this.xGelezenDoorList.GridLines = true;
            this.xGelezenDoorList.HideSelection = false;
            this.xGelezenDoorList.LargeImageList = this.xGelezenDoorImageList;
            this.xGelezenDoorList.Location = new System.Drawing.Point(5, 5);
            this.xGelezenDoorList.Name = "xGelezenDoorList";
            this.xGelezenDoorList.ShowGroups = false;
            this.xGelezenDoorList.ShowItemToolTips = true;
            this.xGelezenDoorList.Size = new System.Drawing.Size(161, 460);
            this.xGelezenDoorList.SmallImageList = this.xGelezenDoorImageList;
            this.xGelezenDoorList.TabIndex = 9;
            this.xGelezenDoorList.UseAlternatingBackColors = true;
            this.xGelezenDoorList.UseCompatibleStateImageBehavior = false;
            this.xGelezenDoorList.UseExplorerTheme = true;
            this.xGelezenDoorList.UseHotItem = true;
            this.xGelezenDoorList.UseTranslucentHotItem = true;
            this.xGelezenDoorList.UseTranslucentSelection = true;
            this.xGelezenDoorList.View = System.Windows.Forms.View.Tile;
            // 
            // olvColumn1
            // 
            this.olvColumn1.AspectName = "Key";
            this.olvColumn1.AspectToStringFormat = "";
            this.olvColumn1.FillsFreeSpace = true;
            this.olvColumn1.IsTileViewColumn = true;
            this.olvColumn1.Text = "Gelezen Door";
            this.olvColumn1.ToolTipText = "Iedereen die de opmerking heeft gelezen";
            this.olvColumn1.WordWrap = true;
            // 
            // olvColumn2
            // 
            this.olvColumn2.AspectName = "Value";
            this.olvColumn2.AspectToStringFormat = "";
            this.olvColumn2.IsTileViewColumn = true;
            this.olvColumn2.Text = "";
            this.olvColumn2.Width = 0;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.verwijderenToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(160, 26);
            // 
            // verwijderenToolStripMenuItem
            // 
            this.verwijderenToolStripMenuItem.Image = global::ProductieManager.Properties.Resources.delete_1577;
            this.verwijderenToolStripMenuItem.Name = "verwijderenToolStripMenuItem";
            this.verwijderenToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Delete;
            this.verwijderenToolStripMenuItem.Size = new System.Drawing.Size(159, 22);
            this.verwijderenToolStripMenuItem.Text = "Verwijderen";
            this.verwijderenToolStripMenuItem.ToolTipText = "Verwijder gebruikers die de opmerking hebben gelezen";
            this.verwijderenToolStripMenuItem.Click += new System.EventHandler(this.verwijderenToolStripMenuItem_Click);
            // 
            // xGelezenDoorImageList
            // 
            this.xGelezenDoorImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.xGelezenDoorImageList.ImageSize = new System.Drawing.Size(48, 48);
            this.xGelezenDoorImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // xControlContainer
            // 
            this.xControlContainer.AutoScroll = true;
            this.xControlContainer.Controls.Add(this.xfilterOp);
            this.xControlContainer.Controls.Add(this.xtitletextbox);
            this.xControlContainer.Controls.Add(this.xontvangerstrip);
            this.xControlContainer.Controls.Add(this.label2);
            this.xControlContainer.Controls.Add(this.label1);
            this.xControlContainer.Controls.Add(this.xFilterWaarde);
            this.xControlContainer.Controls.Add(this.groupBox1);
            this.xControlContainer.Controls.Add(this.xOpmerking);
            this.xControlContainer.Controls.Add(this.xFilterTypeCombo);
            this.xControlContainer.Controls.Add(this.xFilterCombo);
            this.xControlContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xControlContainer.Location = new System.Drawing.Point(20, 60);
            this.xControlContainer.Name = "xControlContainer";
            this.xControlContainer.Size = new System.Drawing.Size(493, 470);
            this.xControlContainer.TabIndex = 8;
            // 
            // xtitletextbox
            // 
            this.xtitletextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.xtitletextbox.CustomButton.Image = null;
            this.xtitletextbox.CustomButton.Location = new System.Drawing.Point(460, 1);
            this.xtitletextbox.CustomButton.Name = "";
            this.xtitletextbox.CustomButton.Size = new System.Drawing.Size(23, 23);
            this.xtitletextbox.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xtitletextbox.CustomButton.TabIndex = 1;
            this.xtitletextbox.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xtitletextbox.CustomButton.UseSelectable = true;
            this.xtitletextbox.CustomButton.Visible = false;
            this.xtitletextbox.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xtitletextbox.Lines = new string[0];
            this.xtitletextbox.Location = new System.Drawing.Point(3, 203);
            this.xtitletextbox.MaxLength = 32767;
            this.xtitletextbox.Name = "xtitletextbox";
            this.xtitletextbox.PasswordChar = '\0';
            this.xtitletextbox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.xtitletextbox.SelectedText = "";
            this.xtitletextbox.SelectionLength = 0;
            this.xtitletextbox.SelectionStart = 0;
            this.xtitletextbox.ShortcutsEnabled = true;
            this.xtitletextbox.ShowClearButton = true;
            this.xtitletextbox.Size = new System.Drawing.Size(484, 25);
            this.xtitletextbox.Style = MetroFramework.MetroColorStyle.Purple;
            this.xtitletextbox.TabIndex = 10;
            this.xtitletextbox.UseSelectable = true;
            this.xtitletextbox.UseStyleColors = true;
            this.xtitletextbox.WaterMark = "Vul in de melding title";
            this.xtitletextbox.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xtitletextbox.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // xontvangerstrip
            // 
            this.xontvangerstrip.AllowMerge = false;
            this.xontvangerstrip.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xontvangerstrip.AutoSize = false;
            this.xontvangerstrip.BackColor = System.Drawing.Color.Transparent;
            this.xontvangerstrip.Dock = System.Windows.Forms.DockStyle.None;
            this.xontvangerstrip.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xontvangerstrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xontvangermenuitem});
            this.xontvangerstrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.Flow;
            this.xontvangerstrip.Location = new System.Drawing.Point(3, 5);
            this.xontvangerstrip.Name = "xontvangerstrip";
            this.xontvangerstrip.ShowItemToolTips = true;
            this.xontvangerstrip.Size = new System.Drawing.Size(484, 37);
            this.xontvangerstrip.TabIndex = 9;
            this.xontvangerstrip.Text = "menuStrip1";
            // 
            // xontvangermenuitem
            // 
            this.xontvangermenuitem.AutoToolTip = true;
            this.xontvangermenuitem.DoubleClickEnabled = true;
            this.xontvangermenuitem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xnieuweontvanger});
            this.xontvangermenuitem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xontvangermenuitem.Image = global::ProductieManager.Properties.Resources.users_12820;
            this.xontvangermenuitem.Name = "xontvangermenuitem";
            this.xontvangermenuitem.Size = new System.Drawing.Size(134, 20);
            this.xontvangermenuitem.Text = "Kies Ontvanger(s)";
            // 
            // xnieuweontvanger
            // 
            this.xnieuweontvanger.Image = global::ProductieManager.Properties.Resources.users_12820;
            this.xnieuweontvanger.Name = "xnieuweontvanger";
            this.xnieuweontvanger.Size = new System.Drawing.Size(125, 22);
            this.xnieuweontvanger.Tag = "Iedereen";
            this.xnieuweontvanger.Text = "Iedereen";
            this.xnieuweontvanger.ToolTipText = "Voeg een nieuwe ontvanger toe";
            this.xnieuweontvanger.Click += new System.EventHandler(this.EmailClient_Click);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(3, 160);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(481, 40);
            this.label2.TabIndex = 7;
            this.label2.Text = "Info Velden: ArtikelNr/Werkplek: \'{0}\', Omschrijving: \'{1}\', Filterwaarde: \'{2}\'," +
    "  Aantal: \'{3}\', TijdGewerkt: \'{4}\', PerUur: \'{5}\', Aantal Producties: \'{6}\'";
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(3, 108);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(484, 21);
            this.label1.TabIndex = 6;
            this.label1.Text = "Vul in een waarde om mee te vergelijken";
            // 
            // xFilterWaarde
            // 
            this.xFilterWaarde.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xFilterWaarde.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.xFilterWaarde.Location = new System.Drawing.Point(3, 132);
            this.xFilterWaarde.Maximum = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.xFilterWaarde.Minimum = new decimal(new int[] {
            999999999,
            0,
            0,
            -2147483648});
            this.xFilterWaarde.Name = "xFilterWaarde";
            this.xFilterWaarde.Size = new System.Drawing.Size(231, 25);
            this.xFilterWaarde.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.xImage);
            this.groupBox1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.ForeColor = System.Drawing.Color.DarkRed;
            this.groupBox1.Location = new System.Drawing.Point(3, 321);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(484, 142);
            this.groupBox1.TabIndex = 5;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "DoubleClick Voor Een Eigen Afbeelding";
            // 
            // xImage
            // 
            this.xImage.BackColor = System.Drawing.Color.Transparent;
            this.xImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xImage.Location = new System.Drawing.Point(3, 21);
            this.xImage.Name = "xImage";
            this.xImage.Size = new System.Drawing.Size(478, 118);
            this.xImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.xImage.TabIndex = 0;
            this.xImage.TabStop = false;
            this.xImage.DoubleClick += new System.EventHandler(this.pictureBox1_DoubleClick);
            this.xImage.MouseEnter += new System.EventHandler(this.pictureBox1_MouseEnter);
            this.xImage.MouseLeave += new System.EventHandler(this.pictureBox1_MouseLeave);
            // 
            // xOpmerking
            // 
            this.xOpmerking.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.xOpmerking.CustomButton.Image = null;
            this.xOpmerking.CustomButton.Location = new System.Drawing.Point(404, 1);
            this.xOpmerking.CustomButton.Name = "";
            this.xOpmerking.CustomButton.Size = new System.Drawing.Size(79, 79);
            this.xOpmerking.CustomButton.Style = MetroFramework.MetroColorStyle.Blue;
            this.xOpmerking.CustomButton.TabIndex = 1;
            this.xOpmerking.CustomButton.Theme = MetroFramework.MetroThemeStyle.Light;
            this.xOpmerking.CustomButton.UseSelectable = true;
            this.xOpmerking.CustomButton.Visible = false;
            this.xOpmerking.FontSize = MetroFramework.MetroTextBoxSize.Medium;
            this.xOpmerking.Lines = new string[0];
            this.xOpmerking.Location = new System.Drawing.Point(3, 234);
            this.xOpmerking.MaxLength = 32767;
            this.xOpmerking.Multiline = true;
            this.xOpmerking.Name = "xOpmerking";
            this.xOpmerking.PasswordChar = '\0';
            this.xOpmerking.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.xOpmerking.SelectedText = "";
            this.xOpmerking.SelectionLength = 0;
            this.xOpmerking.SelectionStart = 0;
            this.xOpmerking.ShortcutsEnabled = true;
            this.xOpmerking.ShowClearButton = true;
            this.xOpmerking.Size = new System.Drawing.Size(484, 81);
            this.xOpmerking.Style = MetroFramework.MetroColorStyle.Purple;
            this.xOpmerking.TabIndex = 4;
            this.xOpmerking.UseSelectable = true;
            this.xOpmerking.UseStyleColors = true;
            this.xOpmerking.WaterMark = "Vul in de melding die je wilt laten zien";
            this.xOpmerking.WaterMarkColor = System.Drawing.Color.FromArgb(((int)(((byte)(109)))), ((int)(((byte)(109)))), ((int)(((byte)(109)))));
            this.xOpmerking.WaterMarkFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Pixel);
            // 
            // xFilterTypeCombo
            // 
            this.xFilterTypeCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xFilterTypeCombo.FormattingEnabled = true;
            this.xFilterTypeCombo.ItemHeight = 23;
            this.xFilterTypeCombo.Location = new System.Drawing.Point(3, 77);
            this.xFilterTypeCombo.Name = "xFilterTypeCombo";
            this.xFilterTypeCombo.PromptText = "Welke waarde wil je vergelijken?";
            this.xFilterTypeCombo.Size = new System.Drawing.Size(484, 29);
            this.xFilterTypeCombo.Style = MetroFramework.MetroColorStyle.Purple;
            this.xFilterTypeCombo.TabIndex = 1;
            this.xFilterTypeCombo.UseSelectable = true;
            this.xFilterTypeCombo.UseStyleColors = true;
            this.xFilterTypeCombo.SelectedIndexChanged += new System.EventHandler(this.xFilterTypeCombo_SelectedIndexChanged);
            // 
            // xFilterCombo
            // 
            this.xFilterCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.xFilterCombo.FormattingEnabled = true;
            this.xFilterCombo.ItemHeight = 23;
            this.xFilterCombo.Location = new System.Drawing.Point(3, 45);
            this.xFilterCombo.Name = "xFilterCombo";
            this.xFilterCombo.PromptText = "Wanneer wil je de Opmerking laten zien?";
            this.xFilterCombo.Size = new System.Drawing.Size(484, 29);
            this.xFilterCombo.Style = MetroFramework.MetroColorStyle.Purple;
            this.xFilterCombo.TabIndex = 0;
            this.xFilterCombo.UseSelectable = true;
            this.xFilterCombo.UseStyleColors = true;
            // 
            // xfilterOp
            // 
            this.xfilterOp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.xfilterOp.FormattingEnabled = true;
            this.xfilterOp.ItemHeight = 23;
            this.xfilterOp.Location = new System.Drawing.Point(240, 128);
            this.xfilterOp.Name = "xfilterOp";
            this.xfilterOp.PromptText = "Waar wil je op vergelijken?";
            this.xfilterOp.Size = new System.Drawing.Size(247, 29);
            this.xfilterOp.Style = MetroFramework.MetroColorStyle.Purple;
            this.xfilterOp.TabIndex = 11;
            this.xfilterOp.UseSelectable = true;
            this.xfilterOp.UseStyleColors = true;
            // 
            // ArtikelOpmerkingForm
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(704, 592);
            this.Controls.Add(this.xControlContainer);
            this.Controls.Add(this.xGelezenDoorPanel);
            this.Controls.Add(this.panel2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(638, 592);
            this.Name = "ArtikelOpmerkingForm";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Style = MetroFramework.MetroColorStyle.Purple;
            this.Text = "Artikel Melding";
            this.Shown += new System.EventHandler(this.ArtikelOpmerkingForm_Shown);
            this.panel2.ResumeLayout(false);
            this.xGelezenDoorPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xGelezenDoorList)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.xControlContainer.ResumeLayout(false);
            this.xontvangerstrip.ResumeLayout(false);
            this.xontvangerstrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.xFilterWaarde)).EndInit();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.xImage)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button xok;
        private System.Windows.Forms.Button xsluiten;
        private System.Windows.Forms.Panel xGelezenDoorPanel;
        private BrightIdeasSoftware.ObjectListView xGelezenDoorList;
        private BrightIdeasSoftware.OLVColumn olvColumn1;
        private BrightIdeasSoftware.OLVColumn olvColumn2;
        private System.Windows.Forms.Panel xControlContainer;
        private System.Windows.Forms.ImageList xGelezenDoorImageList;
        private MetroFramework.Controls.MetroComboBox xFilterTypeCombo;
        private MetroFramework.Controls.MetroComboBox xFilterCombo;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox xImage;
        private MetroFramework.Controls.MetroTextBox xOpmerking;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown xFilterWaarde;
        private System.Windows.Forms.Button xVoorbeeld;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.MenuStrip xontvangerstrip;
        private System.Windows.Forms.ToolStripMenuItem xontvangermenuitem;
        private System.Windows.Forms.ToolStripMenuItem xnieuweontvanger;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem verwijderenToolStripMenuItem;
        private MetroFramework.Controls.MetroTextBox xtitletextbox;
        private MetroFramework.Controls.MetroComboBox xfilterOp;
    }
}