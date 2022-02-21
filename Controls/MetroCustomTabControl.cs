using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Controls;
using MetroFramework.Drawing;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;

namespace Controls
{
    public class MetroCustomTabControl : MetroTabControl
    {
        private readonly Dictionary<int, Point> CloseButtons = new();

        public MetroCustomTabControl()
        {
            DrawMode = TabDrawMode.OwnerDrawFixed;
            DrawItem += MetroCustomTabControl_DrawItem;
            MouseDown += MetroCustomTabPage_MouseClick;
            SizeMode = TabSizeMode.Normal;
        }

        public Image FirstPageImage { get; set; } = Resources.home_icon_32x32.ResizeImage(16, 16);
        public bool ShowCloseButton { get; set; }

        private void MetroCustomTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                try
                {
                    Image img = new Bitmap(Resources.cancel_close_cross_delete_32x32.ResizeImage(16, 16));
                    var r = e.Bounds;
                    var xtab = TabPages[e.Index];
                    var title = xtab.Text.Trim().Replace("    ", "");
                    if (e.Index == 0 && FirstPageImage != null)
                    {
                        var xloc = new Point(r.X,
                            r.Y + 10);
                        e.Graphics.DrawImage(FirstPageImage, xloc);
                        r.X += 16;
                    }

                    TextRenderer.DrawText(e.Graphics, title, MetroFonts.TabControl(FontSize, FontWeight),
                        r, e.ForeColor, e.BackColor, MetroPaint.GetTextFormatFlags(TextAlign));
                    if (e.Index >= 1 && ShowCloseButton)
                    {
                        r = GetTabRect(e.Index);
                        var xloc = new Point(r.X + (r.Width - 22),
                            r.Y + 10);
                        e.Graphics.DrawImage(img, xloc);
                        if (CloseButtons.ContainsKey(e.Index))
                            CloseButtons[e.Index] = xloc;
                        else CloseButtons.Add(e.Index, xloc);
                    }
                }
                catch (Exception)
                {
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void MetroCustomTabPage_MouseClick(object sender, MouseEventArgs e)
        {
            if (!ShowCloseButton) return;
            var tc = (MetroTabControl) sender;
            var p = e.Location;
            var xremove = -1;
            foreach (var xk in CloseButtons)
            {
                var r = new Rectangle(xk.Value, new Size(16, 16));
                if (r.Contains(p))
                {
                    xremove = xk.Key;
                    var TabP = (MetroTabPage) tc.TabPages[tc.SelectedIndex];
                    tc.TabPages.Remove(TabP);
                    OnTabClosed(TabP);
                    break;
                }
            }

            if (xremove > -1)
                CloseButtons.Remove(xremove);
        }

        public void CloseTab(MetroTabPage page)
        {
            try
            {
                if (page != null)
                {
                    TabPages.Remove(page);
                    OnTabClosed(page);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public event EventHandler TabClosed;

        protected virtual void OnTabClosed(object sender)
        {
            TabClosed?.Invoke(sender, EventArgs.Empty);
        }
    }
}