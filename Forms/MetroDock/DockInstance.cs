using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace ProductieManager.Forms.MetroDock
{
    public class DockInstance : MetroForm, IDockContent, IContextMenuStripHost
    {
        private static readonly object DockChangedEvent = new();
        private string m_tabText;

        public DockInstance()
        {
            DockHandler =
                new DockContentHandler(this, GetPersistString);
            InitializeComponent();
            DockHandler.DockStateChanged += DockHandler_DockStateChanged;
            var fontInheritanceFix = PatchController.EnableFontInheritanceFix;

            var flag = true;
            if (fontInheritanceFix.GetValueOrDefault() & fontInheritanceFix.HasValue)
                return;
            ParentChanged += DockContent_ParentChanged;
        }

        [DefaultValue(true)]
        public bool AllowEndUserDocking
        {
            get => DockHandler.AllowEndUserDocking;
            set => DockHandler.AllowEndUserDocking = value;
        }

        [DefaultValue(DockAreas.Float | DockAreas.DockLeft | DockAreas.DockRight | DockAreas.DockTop |
                      DockAreas.DockBottom | DockAreas.Document)]
        public DockAreas DockAreas
        {
            get => DockHandler.DockAreas;
            set => DockHandler.DockAreas = value;
        }

        [DefaultValue(0.25)]
        public double AutoHidePortion
        {
            get => DockHandler.AutoHidePortion;
            set => DockHandler.AutoHidePortion = value;
        }

        [DefaultValue(null)]
        public string TabText
        {
            get => m_tabText;
            set => DockHandler.TabText = m_tabText = value;
        }

        [DefaultValue(true)]
        public bool CloseButton
        {
            get => DockHandler.CloseButton;
            set => DockHandler.CloseButton = value;
        }

        [DefaultValue(true)]
        public bool CloseButtonVisible
        {
            get => DockHandler.CloseButtonVisible;
            set => DockHandler.CloseButtonVisible = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockPanel DockPanel
        {
            get => DockHandler.DockPanel;
            set => DockHandler.DockPanel = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockState DockState
        {
            get => DockHandler.DockState;
            set => DockHandler.DockState = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockPane Pane
        {
            get => DockHandler.Pane;
            set => DockHandler.Pane = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsHidden
        {
            get => DockHandler.IsHidden;
            set => DockHandler.IsHidden = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockState VisibleState
        {
            get => DockHandler.VisibleState;
            set => DockHandler.VisibleState = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsFloat
        {
            get => DockHandler.IsFloat;
            set => DockHandler.IsFloat = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockPane PanelPane
        {
            get => DockHandler.PanelPane;
            set => DockHandler.PanelPane = value;
        }

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DockPane FloatPane
        {
            get => DockHandler.FloatPane;
            set => DockHandler.FloatPane = value;
        }

        [DefaultValue(false)]
        public bool HideOnClose
        {
            get => DockHandler.HideOnClose;
            set => DockHandler.HideOnClose = value;
        }

        [DefaultValue(DockState.Unknown)]
        public DockState ShowHint
        {
            get => DockHandler.ShowHint;
            set => DockHandler.ShowHint = value;
        }

        [Browsable(false)] public bool IsActivated => DockHandler.IsActivated;

        [DefaultValue(null)]
        public ContextMenu TabPageContextMenu
        {
            get => DockHandler.TabPageContextMenu;
            set => DockHandler.TabPageContextMenu = value;
        }

        [DefaultValue(null)]
        public ContextMenuStrip TabPageContextMenuStrip
        {
            get => DockHandler.TabPageContextMenuStrip;
            set => DockHandler.TabPageContextMenuStrip = value;
        }

        [Localizable(true)]
        [Category("Appearance")]
        [DefaultValue(null)]
        public string ToolTipText
        {
            get => DockHandler.ToolTipText;
            set => DockHandler.ToolTipText = value;
        }

        [Browsable(false)] public DockContentHandler DockHandler { get; }

        void IContextMenuStripHost.ApplyTheme()
        {
            //this.DockHandler.ApplyTheme();
            if (DockPanel == null)
                return;
            if (MainMenuStrip != null)
                DockPanel.Theme.ApplyTo(MainMenuStrip);
            if (ContextMenuStrip == null)
                return;
            DockPanel.Theme.ApplyTo(ContextMenuStrip);
        }

        void IDockContent.OnActivated(EventArgs e)
        {
            OnActivated(e);
        }

        void IDockContent.OnDeactivate(EventArgs e)
        {
            OnDeactivate(e);
        }

        private void DockContent_ParentChanged(object Sender, EventArgs e)
        {
            if (Parent == null)
                return;
            Font = Parent.Font;
        }

        private bool ShouldSerializeTabText()
        {
            return m_tabText != null;
        }

        protected virtual string GetPersistString()
        {
            return GetType().ToString();
        }

        public bool IsDockStateValid(DockState dockState)
        {
            return DockHandler.IsDockStateValid(dockState);
        }

        public new void Activate()
        {
            DockHandler.Activate();
        }

        public new void Hide()
        {
            DockHandler.Hide();
        }

        public new void Show()
        {
            DockHandler.Show();
        }

        public void Show(DockPanel dockPanel)
        {
            DockHandler.Show(dockPanel);
        }

        public void Show(DockPanel dockPanel, DockState dockState)
        {
            DockHandler.Show(dockPanel, dockState);
        }

        public void Show(DockPanel dockPanel, Rectangle floatWindowBounds)
        {
            DockHandler.Show(dockPanel, floatWindowBounds);
        }

        public void Show(DockPane pane, IDockContent beforeContent)
        {
            DockHandler.Show(pane, beforeContent);
        }

        public void Show(DockPane previousPane, DockAlignment alignment, double proportion)
        {
            DockHandler.Show(previousPane, alignment, proportion);
        }

        public void FloatAt(Rectangle floatWindowBounds)
        {
            DockHandler.FloatAt(floatWindowBounds);
        }

        public void DockTo(DockPane paneTo, DockStyle dockStyle, int contentIndex)
        {
            DockHandler.DockTo(paneTo, dockStyle, contentIndex);
        }

        public void DockTo(DockPanel panel, DockStyle dockStyle)
        {
            DockHandler.DockTo(panel, dockStyle);
        }

        private void DockHandler_DockStateChanged(object sender, EventArgs e)
        {
            OnDockStateChanged(e);
        }

        public event EventHandler DockStateChanged
        {
            add => Events.AddHandler(DockChangedEvent, value);
            remove => Events.RemoveHandler(DockChangedEvent, value);
        }

        protected virtual void OnDockStateChanged(EventArgs e)
        {
            var eventHandler = (EventHandler) Events[DockChangedEvent];
            if (eventHandler == null)
                return;
            eventHandler(this, e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            if (DockPanel is {SupportDeeplyNestedContent: true} && IsHandleCreated)
                BeginInvoke(new Action(() => base.OnSizeChanged(e)));
            else
                base.OnSizeChanged(e);
        }

        private void InitializeComponent()
        {
            SuspendLayout();
            // 
            // DockInstance
            // 
            ClientSize = new Size(583, 431);
            DisplayHeader = false;
            Movable = false;
            Name = "DockInstance";
            Padding = new Padding(10, 30, 10, 10);
            ShadowType = MetroFormShadowType.AeroShadow;
            ShowIcon = false;
            ShowInTaskbar = false;
            TransparencyKey = Color.Empty;
            ResumeLayout(false);
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}