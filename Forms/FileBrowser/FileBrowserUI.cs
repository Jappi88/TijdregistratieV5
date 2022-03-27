using BrightIdeasSoftware;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.NativeMethods;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Forms.FileBrowser
{
    public partial class FileBrowserUI : UserControl
    {
        private CustomFileWatcher _watcher;
        public string RootPath { get; private set; }
        public string CurrentPath { get; private set; }
        public List<string> History { get; private set; } = new List<string>();
        public ObjectListView FileView => xbrowser;

        public FileBrowserUI()
        {
            InitializeComponent();
            ((OLVColumn)xbrowser.Columns[0]).ImageGetter = GetIndexImage;
            InitViewStyles(xViewStyle);
        }

        public FileBrowserUI(string root) : this()
        {
            Navigate(root);
        }

        private object GetIndexImage(object item)
        {
            try
            {
                if (item is BrowseEntry ent)
                    return xsmallImageList.Images.IndexOfKey(ent.Name);
                return 0;
            }
            catch { return 0; }
        }

        public bool CanNavigateBack()
        {
            if (History.Count == 0) return false;
            var xindex = History.IndexOf(CurrentPath);
            if (xindex == -1) return false;
            return xindex > 0;
        }

        public bool CanNavigateHome()
        {
            return !string.Equals(RootPath, CurrentPath, StringComparison.CurrentCultureIgnoreCase);
        }

        public bool CanNavigateForward()
        {
            if (History.Count == 0) return false;
            var xindex = History.IndexOf(CurrentPath);
            if (xindex == -1) return false;
            return xindex < History.Count - 1;
        }

        private void ClearPathHistory(string path)
        {
            var xcurindex = History.IndexOf(CurrentPath);
            if (xcurindex > -1)
            {
                xcurindex++;
                History.RemoveRange(xcurindex, History.Count - xcurindex);
            }
        }

        public void Navigate(string path)
        {
            try
            {
                if (string.IsNullOrEmpty(RootPath))
                    RootPath = path;
                if (string.Equals(CurrentPath, path, StringComparison.CurrentCultureIgnoreCase)) return;
                var xindex = History.IndexOf(path);
                if (xindex == -1)
                {
                    ClearPathHistory(CurrentPath);
                    History.Add(path);
                }
                CurrentPath = path;
                xbrowser.EmptyListMsg = $"Geen bijlages voor " +
                    $"{(CurrentPath.Replace(Manager.DbPath + "\\Bijlages\\", ""))}";
                _watcher?.Dispose();
                _watcher = new CustomFileWatcher();
                _watcher.FileChanged += _watcher_FileChanged;
                _watcher.FileDeleted += _watcher_FileDeleted;
                _watcher.FolderChanged += _watcher_FolderChanged;
                _watcher.FolderDeleted += _watcher_FolderDeleted;
                _watcher.WatcherLoaded += _watcher_WatcherLoaded;
                _watcher.InitWatcher(path, "*.*", true);
                _watcher.Interval = 1000;
                UpdateNavigationBar();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Close()
        {
            _watcher?.Dispose();
            History.Clear();
            CurrentPath = null;
            RootPath = null;
        }

        private void UpdateNavigationBar()
        {
            if (InvokeRequired)
                this.Invoke(new MethodInvoker(UpdateNavigationBar));
            else
            {
                xvorige.Enabled = CanNavigateBack();
                xvolgende.Enabled = CanNavigateForward();
                xhomebutton.Enabled = CanNavigateHome();
                xstatus.Text = CurrentPath.Replace(Manager.DbPath + "\\", "");
            }
        }

        private void UpdateStatusPanel()
        {
            if (InvokeRequired)
                this.Invoke(new MethodInvoker(UpdateStatusPanel));
            else
            {
                if (xbrowser.SelectedObject is BrowseEntry ent)
                {
                    xselected.Text = ent.Name + $" {ent.FriendlySize}";
                }
                else if (xbrowser.SelectedObjects.Count > 0)
                {
                    var xitems = xbrowser.SelectedObjects.Cast<BrowseEntry>().ToList();
                    xselected.Text = $"{xitems.Count} items geselecteerd";
                    if (!xitems.Any(x => x.IsDirectory))
                    {
                        xselected.Text += $" {BrowseEntry.GetFriendlySize(xitems.Sum(x => x.Size))}";
                    }
                }
                else xselected.Text = "";
                var x1 = xbrowser.Items.Count == 1 ? "item" : "items";
                xtotalitems.Text = $"{xbrowser.Items.Count} {x1}";
            }
        }

        public void NavigateBack()
        {
            try
            {
                if (History.Count == 0) return;
                var xindex = History.IndexOf(CurrentPath);
                xindex--;
                if (xindex < 0) return;
                if (!string.IsNullOrEmpty(History[xindex]))
                    Navigate(History[xindex]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void NavigateForward()
        {
            try
            {
                if (History.Count == 0) return;
                var xindex = History.IndexOf(CurrentPath);
                if (xindex == -1) return;
                xindex++;
                if (xindex > History.Count - 1) return;
                if (!string.IsNullOrEmpty(History[xindex]))
                    Navigate(History[xindex]);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void LoadList(bool reload)
        {
            if (InvokeRequired)
                this.Invoke(new MethodInvoker(() => LoadList(reload)));
            else
            {
                string crit = xsearchbox.Text.ToLower().Replace("zoeken...", "").Trim();

                if (reload || _lastterm == null || !string.Equals(_lastterm, crit, StringComparison.CurrentCultureIgnoreCase))
                {
                    _lastterm = crit;
                    var xitems = GetAllItems(crit);
                    xbrowser.BeginUpdate();
                    var selected = xbrowser.SelectedObjects;
                    UpdateImageList(xitems);
                    xbrowser.SetObjects(xitems);
                    xbrowser.SelectedObjects = selected;
                    xbrowser.EndUpdate();
                    UpdateStatusPanel();
                    _lastselected = xbrowser.SelectedObject as BrowseEntry;
                }
            }
        }

        private List<BrowseEntry> GetAllItems(string filter)
        {
            var xret = new List<BrowseEntry>();
            try
            {
                List<string> xitems = new List<string>();
                lock (_watcher.Records)
                {
                    xitems = _watcher.Records.Select(x => x.Key).ToList();
                }
                for (int i = 0; i < xitems.Count; i++)
                {
                    var item = xitems[i];
                    var br = new BrowseEntry(item);
                    if (!string.IsNullOrEmpty(filter))
                        if (!br.Name.ToLower().Contains(filter.ToLower()))
                            continue;
                    xret.Add(br);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return xret;
        }

        private void UpdateImageList(List<BrowseEntry> entries)
        {
            try
            {
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(() => UpdateImageList(entries)));
                else
                {
                    xbrowser.BeginUpdate();
                    xsmallImageList.Images.Clear();
                    xlargeimagelist.Images.Clear();
                    xsmallImageList.Images.Add("default", SystemIcons.WinLogo);
                    xlargeimagelist.Images.Add("default", SystemIcons.WinLogo);
                    var xents = entries.Where(x => x.Exists()).ToList();
                    foreach (var br in xents)
                    {
                        xsmallImageList.Images.Add(br.Name, br.Icon);
                        xlargeimagelist.Images.Add(br.Name, br.Icon);
                    }
                    xbrowser.RefreshObjects(xents);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                try
                {
                    xbrowser.EndUpdate();
                }
                catch { }
            }
        }

        private void _watcher_WatcherLoaded(object sender, System.EventArgs e)
        {
            LoadList(true);
        }

        private void _watcher_FolderDeleted(object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                if (this.IsDisposed || Disposing) return;
                DeleteEntries(e.FullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool IsAllowed(BrowseEntry ent)
        {
            if (ent == null) return false;
            if (xsearchbox.Text.Trim().ToLower() != "zoeken..." && !string.IsNullOrEmpty(xsearchbox.Text.Trim()) &&
                !ent.Name.ToLower().Trim().Contains(xsearchbox.Text.ToLower().Trim()))
                return false;
            return string.Equals(CurrentPath, Directory.GetParent(ent.Path).FullName, StringComparison.CurrentCultureIgnoreCase);
        }

        private void UpdateBrowserEntry(BrowseEntry entry)
        {
            try
            {
                if (this.IsDisposed || Disposing) return;
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(() => UpdateBrowserEntry(entry)));
                else
                {
                    var xw = entry;
                    bool updataimg = false;
                    bool xflag = IsAllowed(xw) && entry.Exists();
                    var xold = xbrowser.Objects.Cast<BrowseEntry>().FirstOrDefault(x => x.Equals(xw));
                    if (xold == null && xflag)
                    {
                        xbrowser.AddObject(xw);
                        updataimg = true;
                    }
                    else if (xold != null)
                    {
                        if (xflag)
                            xbrowser.RefreshObject(xw);
                        else
                        {
                            xbrowser.RemoveObject(xold);
                            ClearPathHistory(xold.Path);
                            updataimg = true;
                        }
                    }
                    if (updataimg)
                    {
                        UpdateNavigationBar();
                        UpdateStatusPanel();
                        UpdateImageList(xbrowser.Objects.Cast<BrowseEntry>().ToList());
                    }

                    xbrowser.Invalidate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void _watcher_FolderChanged(object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                if (this.IsDisposed || Disposing) return;
                var xw = new BrowseEntry(e.FullPath);
                UpdateBrowserEntry(xw);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DeleteEntries(string path)
        {
            try
            {
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(() => DeleteEntries(path)));
                else
                {
                    var xrem = xbrowser.Objects.Cast<BrowseEntry>().Where(x => string.Equals(x.Path, path, StringComparison.CurrentCultureIgnoreCase)).ToList();
                    if (xrem.Count > 0)
                    {
                        xbrowser.RemoveObjects(xrem);
                        foreach (var v in xrem)
                            ClearPathHistory(v.Path);
                        UpdateNavigationBar();
                        UpdateStatusPanel();
                        UpdateImageList(xbrowser.Objects.Cast<BrowseEntry>().ToList());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void _watcher_FileDeleted(object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                if (this.IsDisposed || Disposing) return;
                DeleteEntries(e.FullPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void _watcher_FileChanged(object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                if (this.IsDisposed || Disposing) return;
                var xw = new BrowseEntry(e.FullPath);
                UpdateBrowserEntry(xw);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void xclearsearchbox_Click(object sender, EventArgs e)
        {
            xsearchbox.Text = "";
            xsearchbox.Select();
            xsearchbox.Focus();
        }

        private string _lastterm;

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            LoadList(false);
        }

        private void xsearchbox_Enter(object sender, EventArgs e)
        {
            if (xsearchbox.Text.ToLower().Trim().StartsWith("zoeken..."))
                xsearchbox.Text = "";
        }

        private void xsearchbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xsearchbox.Text.Trim()))
                xsearchbox.Text = "Zoeken...";
        }

        private void xvorige_Click(object sender, EventArgs e)
        {
            NavigateBack();
        }

        private void xvolgende_Click(object sender, EventArgs e)
        {
            NavigateForward();
        }

        private void xhomebutton_Click(object sender, EventArgs e)
        {
            Navigate(RootPath);
        }

        private void xbrowser_CellEditFinished(object sender, CellEditEventArgs e)
        {
            if (e.RowObject is BrowseEntry brw)
            {
                try
                {
                    var xnewname = e.NewValue.ToString();
                    var xoldname = e.Value.ToString();
                    if (string.Equals(xnewname, xoldname, StringComparison.CurrentCultureIgnoreCase))
                    {
                        e.Cancel = true;
                        return;
                    }
                    brw.Rename(xnewname);
                    var xindex = xsmallImageList.Images.IndexOfKey(xoldname);
                    xsmallImageList.Images.Add(xnewname, brw.GetIcon());
                    xlargeimagelist.Images.Add(xnewname, brw.GetIcon());
                    xsmallImageList.Images.RemoveAt(xindex);
                    xlargeimagelist.Images.RemoveAt(xindex);
                }
                catch (Exception ex)
                {
                    XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
                    e.Cancel = true;
                }
            }
            else e.Cancel = true;
        }

        BrowseEntry _lastselected = null;
        DateTime _lastclicked = DateTime.Now;
        private void xbrowser_CellClick(object sender, CellClickEventArgs e)
        {
            if (e.ClickCount > 1) return;
            var xdt = DateTime.Now;
            var xlast = (xdt - _lastclicked).TotalMilliseconds;
            if (e.Model is BrowseEntry ent)
            {
                if (_lastselected != null && _lastselected.Equals(ent) && xlast > 500 && xlast <= 1500)
                {
                    EditModel(ent);
                }
                _lastselected = ent;
            }
            else
                _lastselected = xbrowser.SelectedObject as BrowseEntry;
            _lastclicked = DateTime.Now;
        }

        private void EditModel(BrowseEntry ent)
        {
            try
            {
                xbrowser.SelectedObject = ent;
                xbrowser.EditModel(ent);
                if (xbrowser.CellEditor != null)
                {
                    xbrowser.CellEditor.TextChanged -= CellEditor_TextChanged;
                    xbrowser.CellEditor.TextChanged += CellEditor_TextChanged;
                    UpdateSelectedEditControl(xbrowser.CellEditor);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CellEditor_TextChanged(object sender, EventArgs e)
        {
            if (sender is Control tb)
            {
                UpdateSelectedEditControl(tb);
            }
        }

        private void UpdateSelectedEditControl(Control tb)
        {
            var xsel = xbrowser.SelectedItem;
            if (xsel == null) return;
            if (xbrowser.View == View.Details || xbrowser.View == View.List) return;
            var subrec = xbrowser.CalculateCellBounds(xsel, 0);
            var xsize = tb.Text.MeasureString(tb.Font, new Size(175, 100));
            var width = xsize.Width;
            var height = xsize.Height;
            if (width < 100)
                width = 100;
            width += 5;
            height += 5;
            var xloc = new Point((subrec.Location.X + subrec.Width / 2) - (width / 2), subrec.Y);
            tb.Bounds = new Rectangle(xloc, new Size(width, height));
        }

        private void xbrowser_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatusPanel();
        }

        private void xViewStyle_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag is View v)
            {
                bool check = !SelectedToolstripItem()?.Equals((ToolStripMenuItem)e.ClickedItem) ?? true;
                if (!check) return;
                CheckViewToolStripCheck((ToolStripMenuItem)e.ClickedItem, check);
                xbrowser.View = v;
                if (xbrowser.Items.Count > 0)
                    xbrowser.RedrawItems(0, xbrowser.Items.Count - 1, false);
            }
        }

        private void InitViewStyles(ToolStripItem item)
        {
            try
            {
                if (item is ToolStripMenuItem xitem)
                {
                    xitem.DropDownItems.Clear();
                    var xenums = Enum.GetValues(typeof(View));
                    foreach (var value in xenums)
                    {
                        var xxitem = new ToolStripMenuItem(Enum.GetName(typeof(View), value));
                        xxitem.Tag = value;
                        xxitem.Checked = xbrowser.View == (View)value;
                        xitem.DropDownItems.Add(xxitem);
                    }
                }
                else if(item is ToolStripSplitButton tb)
                {
                    tb.DropDownItems.Clear();
                    var xenums = Enum.GetValues(typeof(View));
                    foreach (var value in xenums)
                    {
                        var xxitem = new ToolStripMenuItem(Enum.GetName(typeof(View), value));
                        xxitem.Tag = value;
                        xxitem.Checked = xbrowser.View == (View)value;
                        tb.DropDownItems.Add(xxitem);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void CheckViewToolStripCheck(ToolStripMenuItem item, bool ischecked)
        {
            if (ischecked)
                foreach (var x in xViewStyle.DropDownItems.Cast<ToolStripMenuItem>())
                {
                    if (string.Equals(item.Text, x.Text, StringComparison.CurrentCultureIgnoreCase))
                        x.Checked = ischecked;
                    else
                        x.Checked = false;
                }
            item.Checked = ischecked;
        }

        private ToolStripMenuItem SelectedToolstripItem()
        {
            return xViewStyle.DropDownItems.Cast<ToolStripMenuItem>().FirstOrDefault(x => x.Checked);
        }

        private void xViewStyle_Click(object sender, EventArgs e)
        {
            xViewStyle.ShowDropDown();
        }

        private void xbrowser_DoubleClick(object sender, EventArgs e)
        {
            if (xbrowser.SelectedObject is BrowseEntry ent)
            {
                if (ent.IsDirectory)
                    Navigate(ent.Path);
                else
                {
                    try
                    {
                        var exe = new Executable().IsExecutable(ent.Path);
                        if (exe == Executable.ExecutableType.Unknown)
                            System.Diagnostics.Process.Start(ent.Path);
                        else
                            XMessageBox.Show(this, "Het is niet toegestaan om een Uitvoerbaar bestand te openen!", "Niet Toegestaan!", MessageBoxIcon.Exclamation);
                    }
                    catch
                    {

                    }

                }
            }
        }

        private void xbrowser_CellEditStarting(object sender, CellEditEventArgs e)
        {
            try
            {
                if (e.Control != null && e.Control is TextBox tb)
                {
                    tb.AutoCompleteMode = AutoCompleteMode.Suggest;
                    var size = tb.Text.MeasureString(this.Font);
                    if (size.Height < 20)
                        size.Height = 20;
                    if (size.Width < 100)
                        size.Width = 100;
                    size.Width += 5;
                    size.Height += 5;
                    tb.Bounds = new Rectangle(tb.Bounds.X, tb.Bounds.Y, size.Width, size.Height);
                    tb.Multiline = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool CanDrop(object dataobject, DropTargetLocation location, OLVListItem target)
        {
            if (dataobject is DataObject data && data.GetDataPresent(DataFormats.FileDrop))
            {
                var xobj = data.GetData(DataFormats.FileDrop);
                if (xobj is string[] values)
                {
                    values = values.Where(x => new Executable().IsExecutable(x) == Executable.ExecutableType.Unknown).ToArray();
                    return CanDrop(values, location, target);
                }
            }
            else if (dataobject is OLVDataObject olvdata && olvdata.ModelObjects != null)
            {
                var xitems = new List<string>();
                foreach (var model in olvdata.ModelObjects)
                {
                    if (model is string path)
                    {
                        if (new Executable().IsExecutable(path) == Executable.ExecutableType.Unknown)
                            xitems.Add(path);
                    }
                    if (model is BrowseEntry ent)
                    {
                        if (new Executable().IsExecutable(ent.Path) == Executable.ExecutableType.Unknown)
                            xitems.Add(ent.Path);
                    }
                }
                return CanDrop(xitems.ToArray(), location, target);
            }
            return false;
        }

        private bool CanDrop(string[] values, DropTargetLocation location, OLVListItem target)
        {
            if (values == null || values.Length == 0) return false;
            if (IsDisposed || Disposing) return false;
            if (target == null) return true;
            if (location == DropTargetLocation.None) return false;
            if (location == DropTargetLocation.Item)
            {
                if (target?.RowObject is BrowseEntry ent)
                {
                    if (!ent.IsDirectory) return false;
                }
                else return false;
            }

            return true;
        }

        private void DoDrop(object dataobject, DropTargetLocation location, OLVListItem target)
        {
            try
            {
                if (dataobject is DataObject data && data.GetDataPresent(DataFormats.FileDrop))
                {
                    var xobj = data.GetData(DataFormats.FileDrop);
                    if (xobj is string[] values)
                    {
                        values = values.Where(x => new Executable().IsExecutable(x) == Executable.ExecutableType.Unknown).ToArray();
                        if (CanDrop(values, location, target))
                        {
                            DoDrop(values, location, target);
                        }
                    }
                }
                else if (dataobject is OLVDataObject olvdata && olvdata.ModelObjects != null)
                {
                    var xitems = new List<string>();
                    foreach (var model in olvdata.ModelObjects)
                    {
                        if (model is string path)
                        {
                            if (new Executable().IsExecutable(path) == Executable.ExecutableType.Unknown)
                                xitems.Add(path);
                        }
                        if (model is BrowseEntry ent)
                        {
                            if (new Executable().IsExecutable(ent.Path) == Executable.ExecutableType.Unknown)
                                xitems.Add(ent.Path);
                        }
                    }
                    var values = xitems.ToArray();
                    if (CanDrop(values, location, target))
                    {
                        DoDrop(values, location, target);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DoDrop(string[] values, DropTargetLocation location, OLVListItem target)
        {
            if (values == null || values.Length == 0) return;
            if (IsDisposed || Disposing) return;
            if (location == DropTargetLocation.Item)
            {
                if (target?.RowObject is BrowseEntry ent)
                {
                    if (ent.IsDirectory)
                    {
                        DropItems(values, ent.Path);
                    }
                }
            }
            else
            {
                DropItems(values, CurrentPath);
            }
        }

        private void DropItems(string[] items, string path)
        {
            try
            {
                foreach (var item in items)
                {
                    var xpar = Directory.GetParent(item)?.FullName;
                    if (string.Equals(xpar, path, StringComparison.CurrentCultureIgnoreCase))
                        continue;
                    var bwr = new BrowseEntry(item);
                    if (xpar.ToLower().Contains(RootPath.ToLower()))
                        bwr.MoveTo(Path.Combine(path, Path.GetFileName(item)));
                    else bwr.CopyTo(Path.Combine(path, Path.GetFileName(item)),true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void xbrowser_ModelCanDrop(object sender, ModelDropEventArgs e)
        {
            if (CanDrop(e.DataObject, e.DropTargetLocation, e.DropTargetItem))
                e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
        }

        private void xbrowser_CanDrop(object sender, OlvDropEventArgs e)
        {
            if (CanDrop(e.DataObject, e.DropTargetLocation, e.DropTargetItem))
                e.Effect = DragDropEffects.Copy;
            else e.Effect = DragDropEffects.None;
        }

        private void xbrowser_ModelDropped(object sender, ModelDropEventArgs e)
        {
            try
            {
                DoDrop(e.DataObject, e.DropTargetLocation, e.DropTargetItem);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void xbrowser_Dropped(object sender, OlvDropEventArgs e)
        {
            try
            {
                DoDrop(e.DataObject, e.DropTargetLocation, e.DropTargetItem);
                e.Handled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private bool InitContextMenu()
        {
            try
            {
                if (xbrowser.SelectedObjects.Count > 0)
                {
                    return InitEntriesContextMenu();
                }
                else
                {
                    return InitRootContextMenu();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private bool InitEntriesContextMenu()
        {
            try
            {
                xContextMenu.Items.Clear();
                var xopen = new ToolStripMenuItem("Openen", Resources.open_file256_25211, xbrowser_DoubleClick);
                xopen.ShowShortcutKeys = true;
                xopen.ShortcutKeyDisplayString = "Enter";
                xContextMenu.Items.Add(xopen);
                xContextMenu.Items.Add(new ToolStripSeparator());
                bool flag = Manager.LogedInGebruiker != null && Manager.LogedInGebruiker.AccesLevel >= Rpm.Various.AccesType.ProductieAdvance;
                if (!flag) return true;
                var xcopy = new ToolStripMenuItem("Kopieren", Resources.documents_32x32, CopyDocuments);
                xcopy.ShowShortcutKeys = true;
                xcopy.ShortcutKeys = Keys.Control | Keys.C;
                xContextMenu.Items.Add(xcopy);
                var xdel = new ToolStripMenuItem("Verwijderen", Resources.delete_1577, DeleteDocuments);
                xdel.ShowShortcutKeys = true;
                xdel.ShortcutKeyDisplayString = "Delete";
                xContextMenu.Items.Add(xdel);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private DataObject GetDataObject()
        {
            try
            {
               return Clipboard.ContainsFileDropList() ? (DataObject)Clipboard.GetDataObject() : _clipboard;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private bool InitRootContextMenu()
        {
            try
            {
                xContextMenu.Items.Clear();
                var xbeeld = new ToolStripMenuItem("Beeld", Resources.viewmorecolumns_6276);
                InitViewStyles(xbeeld);
                xbeeld.DropDownItemClicked += xViewStyle_DropDownItemClicked;
                xContextMenu.Items.Add(xbeeld);
                xContextMenu.Items.Add(new ToolStripSeparator());
                if (CanNavigateBack() || CanNavigateForward() || CanNavigateHome())
                {
                    if (CanNavigateBack())
                    {
                        var vorige = new ToolStripMenuItem("Vorige", Resources.arrow_left_15601, xvorige_Click);
                        xContextMenu.Items.Add(vorige);
                    }
                    if (CanNavigateForward())
                    {
                        var volgende = new ToolStripMenuItem("Volgende", Resources.arrow_right_15600, xvolgende_Click);
                        xContextMenu.Items.Add(volgende);
                    }

                    if (CanNavigateHome())
                    {
                        var home = new ToolStripMenuItem("Startpagina", Resources.home_icon_color_32x32, xhomebutton_Click);
                        xContextMenu.Items.Add(home);
                    }

                    xContextMenu.Items.Add(new ToolStripSeparator());
                }
                bool flag = Manager.LogedInGebruiker != null && Manager.LogedInGebruiker.AccesLevel >= Rpm.Various.AccesType.ProductieAdvance;
                if (!flag) return true;
                var data = GetDataObject();
                if (data != null && data.ContainsFileDropList())
                {
                    var xdata = data.GetFileDropList();
                    if (xdata.Count > 0)
                    {
                        var files = xdata.Cast<string>().Where(x => new BrowseEntry(x).Exists() && new Executable().IsExecutable(x) == Executable.ExecutableType.Unknown).ToList();
                        if (files.Count > 0)
                        {
                            var paste = new ToolStripMenuItem("Plakken", Resources.copy_paste_document_file_1557, PasteDocuments);
                            paste.ShowShortcutKeys = true;
                            paste.ShortcutKeys = Keys.Control | Keys.V;
                            xContextMenu.Items.Add(paste);
                        }
                    }
                }

                var xnew = new ToolStripMenuItem("Nieuw", Resources.add_Blue_circle_32x32);
                xnew.DropDownItems.Add("Map", Resources.folder_636_32x32);
                xnew.DropDownItems.Add("Word-document", Resources.word_icon_130070_32x32);
                xnew.DropDownItems.Add("Tekstdocument", Resources.document_edit_icon_icons_com_52428);
                xnew.DropDownItems.Add("Excel-werkblad", Resources.microsoft_excel_22733);
                xnew.DropDownItems.Add("Bitmapafbeelding", Resources.photo_photography_image_32x32);
                xnew.DropDownItemClicked += Xnew_DropDownItemClicked;
                xContextMenu.Items.Add(xnew);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        private void PasteDocuments(object sender, EventArgs e)
        {
            try
            {
                var data = GetDataObject();
                if (data != null && data.ContainsFileDropList())
                {
                    var xdata = data.GetFileDropList();
                    if (xdata.Count > 0)
                    {
                        var files = xdata.Cast<string>().Where(x => new BrowseEntry(x).Exists() && new Executable().IsExecutable(x) == Executable.ExecutableType.Unknown).ToList();
                        if (files.Count > 0)
                        {
                            foreach (var f in files)
                            {
                                try
                                {
                                    var xbw = new BrowseEntry(f);
                                    var path = Functions.GetAvailibleFilepath(CurrentPath, Path.GetFileName(f));
                                    xbw.CopyTo(path,false);
                                    UpdateBrowserEntry(xbw);
                                    xbrowser.SelectedObject = xbw;
                                    xbrowser.SelectedItem?.EnsureVisible();
                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine(ex.Message);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private DataObject _clipboard;

        private void CopyDocuments(object sender, EventArgs e)
        {
            try
            {
                var files = xbrowser.SelectedObjects?.Cast<BrowseEntry>().ToList() ?? new List<BrowseEntry>();
                if (files.Count > 0)
                {
                    var xd = files.Select(x => x.Path).ToArray();
                    var xcol = new System.Collections.Specialized.StringCollection();
                    xcol.AddRange(xd);
                    _clipboard = new DataObject();
                    _clipboard.SetFileDropList(xcol);
                    _clipboard.SetData("Preferred DropEffect", DragDropEffects.Copy);
                    Clipboard.SetDataObject(_clipboard);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void DeleteDocuments(object sender, EventArgs e)
        {
            try
            {
                var files = xbrowser.SelectedObjects?.Cast<BrowseEntry>().ToList() ?? new List<BrowseEntry>();
                if (files.Count > 0)
                {
                    if (files.Count == 1)
                    {
                        var f = files.FirstOrDefault();
                        f.Delete();
                        xbrowser.RemoveObject(f);
                        ClearPathHistory(f.Path);
                    }
                    else
                    {
                        for (int i = 0; i < files.Count; i++)
                        {
                            try
                            {
                                var f = files[i];
                                if (f.IsDirectory)
                                    Directory.Delete(f.Path,true);
                                else File.Delete(f.Path);
                                xbrowser.RemoveObject(f);
                                ClearPathHistory(f.Path);
                            }
                            catch { }
                        }
                    }
                    UpdateNavigationBar();
                    UpdateStatusPanel();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void Xnew_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            try
            {
                string name = string.Empty;
                string filename = string.Empty;
                switch (e.ClickedItem.Text.ToLower())
                {
                    case "map":
                        name = "Nieuwe map";
                        filename = Functions.GetAvailibleFilepath(this.CurrentPath, name);
                        Microsoft.VisualBasic.FileIO.FileSystem.CreateDirectory(filename);
                        var bwr = new BrowseEntry(filename);
                        UpdateBrowserEntry(bwr);
                        EditModel(bwr);
                        return;
                    case "word-document":
                        name = "Nieuw - Word-document.docx";
                        break;
                    case "tekstdocument":
                        name = "Nieuw - Tekstdocument.txt";
                        break;
                    case "excel-werkblad":
                        name = "Nieuw - Excel-werkblad.xlsx";
                        filename = Functions.GetAvailibleFilepath(this.CurrentPath, name);
                        File.WriteAllBytes(filename, Resources.Nieuw___Microsoft_Excel_werkblad);
                        var xbwr = new BrowseEntry(filename);
                        UpdateBrowserEntry(xbwr);
                        EditModel(xbwr);
                        return;
                    case "bitmapafbeelding":
                        name = "Nieuw - Bitmapafbeelding.bmp";
                        break;
                }
                if (!string.IsNullOrEmpty(name))
                {
                    filename = Functions.GetAvailibleFilepath(this.CurrentPath, name);
                    using (var fs = File.Create(filename))
                        fs.Close();
                    var bwr = new BrowseEntry(filename);
                    UpdateBrowserEntry(bwr);
                    EditModel(bwr);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void xContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = !InitContextMenu();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            var e = new KeyEventArgs(keyData);

            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = e.Handled = true;
                xbrowser_DoubleClick(xbrowser, EventArgs.Empty);
            }
            else if (e.KeyCode == Keys.Delete)
            {
                e.SuppressKeyPress = e.Handled = true;
                DeleteDocuments(xbrowser, EventArgs.Empty);
            }
            else if (e.Control)
            {
                if (e.KeyCode == Keys.C)
                {
                    e.SuppressKeyPress = e.Handled = true;
                    CopyDocuments(this, e);
                }
                if (e.KeyCode == Keys.V)
                {
                    e.SuppressKeyPress = e.Handled = true;
                    PasteDocuments(this, e);
                    
                }
            }
            if (e.Handled) return true;
            return base.ProcessCmdKey(ref msg, keyData);
        }
    }
}
