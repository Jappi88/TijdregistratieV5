using MetroFramework.Controls;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using MetroFramework;
using MetroFramework.Drawing;

namespace Controls
{
    public class MetroCustomTabControl : MetroTabControl
    {
        public Image FirstPageImage { get; set; } = Resources.home_icon_32x32.ResizeImage(16, 16);
        private MetroTabPage predraggedTab;

        private const int WM_NCHITTEST = 0x84;
        private const int HTTRANSPARENT = -1;
        private const int HTCLIENT = 1;
        public MetroCustomTabControl()
        {
            this.AllowDrop = true;
            this.DrawMode = TabDrawMode.OwnerDrawFixed;
            this.DrawItem += MetroCustomTabControl_DrawItem;
            this.MouseDown += MetroCustomTabPage_MouseClick;
            this.SizeMode = TabSizeMode.Normal;
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg == WM_NCHITTEST)
            {
                if (m.Result.ToInt32() == HTTRANSPARENT)
                    m.Result = new IntPtr(HTCLIENT);
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {

            predraggedTab = getPointedTab();
            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            predraggedTab = null;
            base.OnMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && predraggedTab != null)
                this.DoDragDrop(predraggedTab, DragDropEffects.Move);
            base.OnMouseMove(e);
        }

        protected override void OnDragDrop(DragEventArgs drgevent)
        {
            try
            {
                MetroTabPage draggedTab = (MetroTabPage)drgevent.Data.GetData(typeof(MetroTabPage));

            if (draggedTab != null && draggedTab.Parent != this)
            {
                draggedTab.Parent = this;
                this.SelectedTab = draggedTab;
            }

            predraggedTab = null;

            base.OnDragDrop(drgevent);
            }
            catch { }
        }

        protected override void OnDragOver(DragEventArgs drgevent)
        {
            try
            {

                MetroTabPage draggedTab = (MetroTabPage)drgevent.Data.GetData(typeof(MetroTabPage));
                MetroTabPage pointedTab = getPointedTab();

                if (draggedTab == predraggedTab && pointedTab != null)
                {
                    drgevent.Effect = DragDropEffects.Move;

                    if (pointedTab != draggedTab)
                        swapTabPages(draggedTab, pointedTab);
                }
                else if (draggedTab != null && draggedTab.Parent != this)
                {
                    drgevent.Effect = DragDropEffects.Move;
                }

                base.OnDragOver(drgevent);
            }
            catch { }
        }

        private MetroTabPage getPointedTab()
        {
            for (int i = 0; i < this.TabPages.Count; i++)
            {
                var rect = this.GetTabRect(i);
                rect.Width -=24;
                if (rect.Contains(this.PointToClient(Cursor.Position)))
                    return (MetroTabPage)this.TabPages[i];
            }
            return null;
        }

        private void swapTabPages(MetroTabPage src, MetroTabPage dst)
        {
            if (src == null || dst == null) return;
            int srci = this.TabPages.IndexOf(src);
            int dsti = this.TabPages.IndexOf(dst);
            if (srci < 0 || dsti < 0) return;
            this.TabPages[dsti] = src;
            this.TabPages[srci] = dst;

            if (this.SelectedIndex == srci)
                this.SelectedIndex = dsti;
            else if (this.SelectedIndex == dsti)
                this.SelectedIndex = srci;
            var curloc = Cursor.Position;
            var rect = this.GetTabRect(srci);
            var loc = this.PointToScreen(rect.Location);
            rect.Location = loc;
            if (rect.Contains(curloc))
            {
                rect = this.GetTabRect(dsti);
                loc = this.PointToScreen(rect.Location);
                loc.Y = curloc.Y;
                if (dsti < srci)
                    loc.X += rect.Width;
                Cursor.Position = loc;
            }
            this.Refresh();
        }

        private void MetroCustomTabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            try
            {
                try
                {
                    Image img = new Bitmap(Resources.cancel_close_cross_delete_32x32.ResizeImage(16, 16));
                    var r = e.Bounds;
                    var xtab = this.TabPages[e.Index];
                    string title = xtab.Text.Trim().Replace("    ", "");
                    bool xflag = xtab.Text.ToLower().Trim().Contains("start pagina");
                    if (xflag && FirstPageImage != null)
                    {
                        var xloc = new Point((r.X),
                            r.Y + 10);
                        e.Graphics.DrawImage(FirstPageImage, xloc);
                        r.X += 16;
                    }
                    TextRenderer.DrawText(e.Graphics, title, MetroFonts.TabControl(this.FontSize, this.FontWeight),
                        r, e.ForeColor, e.BackColor, MetroPaint.GetTextFormatFlags(TextAlign));
                    if (!xflag && ShowCloseButton)
                    {
                        r = GetTabRect(e.Index);
                        var xloc = new Point((r.X + (r.Width - 22)),
                            r.Y + 10);
                        e.Graphics.DrawImage(img, xloc);
                        if (CloseButtons.ContainsKey(e.Index))
                            CloseButtons[e.Index] = xloc;
                        else CloseButtons.Add(e.Index, xloc);

                    }

                }
                catch (Exception) { }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private readonly Dictionary<int, Point> CloseButtons = new Dictionary<int, Point>();
        public bool ShowCloseButton { get; set; }

        private void MetroCustomTabPage_MouseClick(object sender, MouseEventArgs e)
        {
            if (!ShowCloseButton) return;
            MetroTabControl tc = (MetroTabControl)sender;
            Point p = e.Location;
            int xremove = -1;
            foreach (var xk in CloseButtons)
            {
                Rectangle r = new Rectangle(xk.Value, new Size(16, 16));
                if (r.Contains(p))
                {
                    xremove = xk.Key;
                    MetroTabPage TabP = (MetroTabPage)tc.TabPages[tc.SelectedIndex];
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
                    this.TabPages.Remove(page);
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
