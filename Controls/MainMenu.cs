using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;
using Various;

namespace Controls
{
    public partial class MainMenu : UserControl
    {
        private readonly int minsize = 40;
        private MenuButton[] _menButtons;

        private int maxsize = 180;

        public MainMenu()
        {
            InitializeComponent();
        }

        public MenuButton[] MenuButtons
        {
            get => _menButtons;
            set
            {
                _menButtons = value;
                UpdateButtons();
            }
        }

        public bool IsExpanded
        {
            get => Width > minsize;
            set => Expand(value);
        }

        public void UpdateButtons()
        {
            xbuttoncontainer.Controls.Clear();
            if (_menButtons?.Length > 0)
            {
                var cursize = 180;
                for (var i = 0; i < _menButtons.Length; i++)
                {
                    var bsize = _menButtons[i].Text.MeasureString(Font);
                    bsize.Width += 50;
                    if (bsize.Width > cursize)
                        cursize = bsize.Width;
                }

                for (var i = 0; i < _menButtons.Length; i++)
                {
                    var xbutton = new Button
                    {
                        Text = IsExpanded ? _menButtons[i].Text : "",
                        Tag = _menButtons[i],
                        Enabled = _menButtons[i].Enabled,
                        Visible = _menButtons[i].Enabled,
                        Name = _menButtons[i].Name,
                        Image = _menButtons[i].Image,
                        ImageAlign = ContentAlignment.MiddleLeft,
                        FlatStyle = FlatStyle.Flat,
                        TextImageRelation = TextImageRelation.ImageBeforeText,
                        Dock = DockStyle.Top,
                        Size = new Size(cursize, 40),
                        Font = Font,
                        ContextMenuStrip = _menButtons[i].ContextMenu
                    };
                    if (!string.IsNullOrEmpty(_menButtons[i].Tooltip))
                        toolTip1.SetToolTip(xbutton, _menButtons[i].Tooltip);
                    xbutton.FlatAppearance.BorderSize = 0;
                    xbutton.FlatAppearance.MouseOverBackColor = Color.AliceBlue;
                    xbutton.Click += MenuClick;
                    _menButtons[i].Index = i;
                    _menButtons[i].Base = xbutton;
                    xbuttoncontainer.Controls.Add(xbutton);
                    xbutton.BringToFront();
                }

                maxsize = cursize;
            }
            else
            {
                maxsize = 180;
            }
        }

        public MenuButton GetMenuButton(string name)
        {
            if (MenuButtons?.Length == 0) return null;
            return MenuButtons?.FirstOrDefault(x =>
                string.Equals(name, x.Name, StringComparison.CurrentCultureIgnoreCase));
        }

        public Button GetButton(string name)
        {
            if (MenuButtons?.Length == 0) return null;
            if (xbuttoncontainer.Controls.ContainsKey(name))
                return (Button) xbuttoncontainer.Controls[name];
            return null;
        }

        public MenuButton GetMenuButton(int index)
        {
            if (MenuButtons == null || index >= MenuButtons.Length) return null;
            return MenuButtons[MenuButtons.Length - index];
        }

        public void OnSettingChanged(object instance, UserSettings settings, bool init)
        {
            var user = Manager.LogedInGebruiker;
            try
            {
                if (xbuttoncontainer.IsDisposed || xbuttoncontainer.Disposing) return;
                xbuttoncontainer.BeginInvoke(new MethodInvoker(() =>
                {
                    for (var i = 0; i < xbuttoncontainer.Controls.Count; i++)
                        if (xbuttoncontainer.Controls[i].Tag is MenuButton xmb)
                        {
                            xbuttoncontainer.Controls[i].Enabled = xmb.AccesLevel <= AccesType.AlleenKijken ||
                                                                   user != null && xmb.AccesLevel <= user.AccesLevel;
                            xbuttoncontainer.Controls[i].Visible = xbuttoncontainer.Controls[i].Enabled;
                        }
                }));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xmenu_Click(object sender, EventArgs e)
        {
            Expand(!IsExpanded);
        }

        private void Expand(bool open)
        {
            for (var i = 0; i < xbuttoncontainer.Controls.Count; i++)
                if (xbuttoncontainer.Controls[i].Tag is MenuButton xmb)
                    xbuttoncontainer.Controls[i].Text = open ? xmb.Text : "";
            Width = open ? maxsize : minsize;
        }

        public bool Enable(int index, bool enable)
        {
            if (_menButtons == null) return false;
            if (index >= xbuttoncontainer.Controls.Count)
                return false;
            _menButtons[index].Enabled = enable;
            var xindex = xbuttoncontainer.Controls.Count - index - 1;
            xbuttoncontainer.Controls[xindex].Enabled = enable;
            xbuttoncontainer.Controls[xindex].Visible = enable;
            xbuttoncontainer.Invalidate();
            return true;
        }

        public bool Enable(string name, bool enable)
        {
            var bttn = _menButtons?.FirstOrDefault(x => string.Equals(x.Name, name));
            if (bttn == null) return false;
            bttn.Enabled = enable;
            var xindex = xbuttoncontainer.Controls.Count - bttn.Index - 1;
            xbuttoncontainer.Controls[xindex].Enabled = enable;
            xbuttoncontainer.Controls[xindex].Visible = enable;
            xbuttoncontainer.Invalidate();
            return true;
        }

        public void PressButton(string name)
        {
            var bttn = _menButtons?.FirstOrDefault(x => string.Equals(x.Name, name));
            if (bttn == null) return;
            MenuClick(bttn.Base, EventArgs.Empty);
        }

        #region Events

        public event EventHandler OnMenuClick;

        protected virtual void MenuClick(object sender, EventArgs e)
        {
            OnMenuClick?.Invoke(sender, EventArgs.Empty);
        }

        #endregion Events
    }
}