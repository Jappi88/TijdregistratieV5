using Controls;
using ICSharpCode.SharpZipLib.Zip;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Productie;
using Rpm.SqlLite;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Forms
{
    public partial class BackupRestoreForm : MetroBase.MetroBaseForm
    {
        Dictionary<string, List<ZipEntry>> Entries = new Dictionary<string, List<ZipEntry>>();
        private List<string> ExpandedItems = new List<string>();
        private readonly CustomFileWatcher _BackupWatcher;

        public BackupRestoreForm()
        {
            InitializeComponent();
            imageList1.Images.Add(Resources.folder_636_32x32);
            imageList1.Images.Add(Resources.page_document_empty_32x32);
            imageList1.Images.Add(Resources.zip_36268_32x32);
            xTreeView.NodeMouseClick += (sender, args) =>
            {
                if (args.Node != null)
                    args.Node.SelectedImageIndex = args.Node.ImageIndex;
                xTreeView.SelectedNode = args.Node;
            };
            LoadItems(); 
            _BackupWatcher = new CustomFileWatcher();
            _BackupWatcher.FileChanged += _BackupWatcher_FileChanged;
            _BackupWatcher.FileDeleted += _BackupWatcher_FileDeleted;
            _BackupWatcher.InitWatcher(Manager.BackupPath, "*.zip", false);
        }

        private void _BackupWatcher_FileDeleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (Entries.ContainsKey(e.FullPath))
                {
                    Entries.Remove(e.FullPath);
                    ListItems();
                }
            }
            catch { }
        }

        private void _BackupWatcher_FileChanged(object sender, FileSystemEventArgs e)
        {
            try
            {
                if (BackupInfo.IsValidBackup(e.FullPath, null))
                {
                    LoadFileItems(e.FullPath);
                    ListItems();
                }
            }
            catch { }
        }

        private void xSearchTextBox_TextChanged(object sender, EventArgs e)
        {
            xSearchTextBox.ShowClearButton = true;
            xSearchTextBox.Invalidate();
            xSearchTimer.Stop();
            xSearchTimer.Start();
        }

        private void xSearchTimer_Tick(object sender, EventArgs e)
        {
            xSearchTimer.Stop();
            ListItems();
        }

        public void ListItems()
        {
            try
            {
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(ListItems));
                else
                {
                    var sel = xTreeView.SelectedNode;
                    xTreeView.BeginUpdate();
                    xTreeView.Nodes.Clear();
                    foreach (var val in Entries)
                    {
                        var created = File.GetCreationTime(val.Key);
                        var root = new TreeNode($"[{created}] " + Path.GetFileName(val.Key));
                        root.Name = Path.GetFileName(val.Key);
                        root.Tag = val.Key;
                        root.ImageIndex = 2;
                        root.SelectedImageIndex = 2;
                        var rootitems = val.Value.OrderBy(x => x.Name);//.Where(x => !string.IsNullOrEmpty(x.Name) && !x.Name.Contains("/")).ToList();
                        foreach (var item in rootitems)
                        {
                            AddTreeItem(root, item);
                        }
                        xTreeView.Nodes.Add(root);
                        if (IsExpanded(root.Name))
                            root.Expand();
                    }
                    xTreeView.EndUpdate();
                    xTreeView.SelectedNode = sel;
                    xTreeView.SelectedNode?.EnsureVisible();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void LoadFileItems(string filepath)
        {
            try
            {
                var b = filepath;
                if (!Entries.ContainsKey(b))
                    Entries.Add(b, new List<ZipEntry>());
                else Entries[b].Clear();
                using ZipInputStream s = new ZipInputStream(File.OpenRead(b));
                var ents = Entries[b];
                try
                {
                    ZipEntry theEntry;
                    var time = DateTime.Now;
                    List<string> dirs = new List<string>();
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        if (ents.IndexOf(theEntry) == -1)
                            ents.Add(theEntry);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
                finally
                {
                    s.Close();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private bool IsExpanded(string path)
        {
            if (string.IsNullOrEmpty(path)) return false;
            return ExpandedItems.IndexOf(path) > -1;
        }

        private void AddTreeItem(TreeNode root, ZipEntry item)
        {
            try
            {
                if (item?.Name == null) return;
                string filter = xSearchTextBox.Text.Trim();
                if (!string.IsNullOrEmpty(filter) && !item.Name.ToLower().Contains(filter.ToLower()))
                    return;
                var dirs = item.Name.Split('/');
                string rootname = root.Text;
                for (int i = 0; i < dirs.Length; i++)
                {
                    var tnName = root.Name + "\\" + dirs[i];
                    var xr = root.Nodes.Find(tnName, false)?.LastOrDefault();
                    if (xr != null)
                    {
                        root = xr;
                        continue;
                    }
                    var xtn = new TreeNode(dirs[i]);
                    xtn.Name = tnName;
                    xtn.Tag = item;
                    xtn.ImageIndex = i == dirs.Length - 1 ? 1 : 0;
                    xtn.SelectedImageIndex = i == dirs.Length - 1 ? 1 : 0;

                    root.Nodes.Add(xtn);
                    if (IsExpanded(tnName))
                        xtn.Expand();
                    root = xtn;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        public void LoadItems()
        {
            try
            {
                var backuppath = Manager.BackupPath;
                var backups = Directory.GetFiles(backuppath, "*.zip", SearchOption.TopDirectoryOnly);
                if (backuppath == "")
                    backuppath = Directory.GetCurrentDirectory();
                if (!backuppath.EndsWith("\\"))
                    backuppath = backuppath + "\\";
                foreach (var b in backups)
                {
                    LoadFileItems(b);
                }
                ListItems();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private ProgressArg _arg;

        private bool isrestoring;
        public void RestoreBackup(string backupfile, List<string> entries = null)
        {
            if (isrestoring) return;
            string name = Path.GetFileName(backupfile);
            if (entries != null && entries.Count == 0)
                entries = null;
            var x1 = entries == null || entries.Count == 0 ? $"{ name}" : (entries.Count == 1 ? $"'{entries[0]}'" : $"{entries.Count} bestanden");
            var result = XMessageBox.Show(this, $"Weet je zeker dat je {x1} wilt herstellen?", "Backup Herstellen", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
            if (result != DialogResult.Yes) return;
            isrestoring = true;
            if (_arg == null)
            {
                _arg = new ProgressArg();
                _arg.Changed += Arg_Changed;
            }
            else
            {
                _arg.Token = new System.Threading.CancellationTokenSource();
            }
            if (BackupInfo.IsValidBackup(backupfile, null))
            {
                Manager.BackupInfo.UnZip(backupfile, Manager.DbPath, null, true, entries, _arg);
               // XMessageBox.Show(this, $"{x1} succesvol hersteld!", "Backup Herstellen");
            }
            else
            {
                isrestoring = false;
                XMessageBox.Show(this, $"'{Path.GetFileName(backupfile)}' is geen geldige backup", "Ongeldige Backup", MessageBoxIcon.Warning);
            }
        }

        private void Arg_Changed(object sender, ProgressArg arg)
        {
            if (this.IsDisposed || this.Disposing || !this.Visible) return;
            try
            {
                if (InvokeRequired)
                {
                    this.Invoke(new MethodInvoker(() => UpdateStatus(arg)));
                }
                else
                    UpdateStatus(arg);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

        }

        private void UpdateStatus(ProgressArg arg)
        {
            try
            {
                xStatusStrip.Visible = true;
                xProgressBar.Value = arg.Progress;
                xStatusLabel.Text = arg.Message;
                xCancelButton.Visible = arg.Type is ProgressType.ReadBussy or ProgressType.WriteBussy;
                isrestoring = xCancelButton.Visible;
                if (!xCancelButton.Visible)
                {
                    xStatusResetTimer.Start();
                }
                xProgressBar.Invalidate();
                xStatusLabel.Invalidate();
                Application.DoEvents();
            }
            catch { }
        }

        private void xTreeView_AfterCollapse(object sender, TreeViewEventArgs e)
        {
            ExpandedItems.RemoveAll(x => string.Equals(x, e.Node.Name, StringComparison.CurrentCultureIgnoreCase));
        }

        private void xTreeView_AfterExpand(object sender, TreeViewEventArgs e)
        {
            ExpandedItems.Add(e.Node.Name);
        }

        private void vouwAllesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selnode = xTreeView.SelectedNode;
            if (selnode == null)
                xTreeView.CollapseAll();
            else
            {
                if (selnode.Nodes.Count > 0)
                    selnode.Collapse(false);
                else selnode.Parent?.Collapse(false);
            }
        }

        private void ontvouwAllesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selnode = xTreeView.SelectedNode;
            if (selnode == null)
                xTreeView.ExpandAll();
            else
            {
                if (selnode.Nodes.Count > 0)
                    selnode.Expand();
                else selnode.Parent?.ExpandAll();
            }
        }

        private void herstellenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(xTreeView.SelectedNode != null)
            {
                if(xTreeView.SelectedNode.Tag is string filepath)
                {
                    RestoreBackup(filepath);
                }
                else if(xTreeView.SelectedNode.Tag is ZipEntry entry)
                {
                    var root = GetRootNode(xTreeView.SelectedNode);
                    if (root.Tag is string file)
                    {
                        var items = GetAllNodes(xTreeView.SelectedNode).Select(x=> (x.Tag as ZipEntry)?.Name).ToList();
                        RestoreBackup(file,items);
                    }
                }
            }
        }

        private TreeNode GetRootNode(TreeNode node)
        {
            if (node == null) return null;
            var xparent = node.Parent;
            while ((xparent?.Parent) != null) 
            { xparent = xparent.Parent; }
            return xparent;
        }

        private List<TreeNode> GetAllNodes(TreeNode node)
        {

            var xret = new List<TreeNode>();
            if (node == null) return xret;
            if(node.Nodes.Count > 0)
            {
                foreach(var n in node.Nodes)
                {
                    var items = GetAllNodes((TreeNode)n);
                    if (items.Count > 0)
                        xret.AddRange(items);
                    else xret.Add((TreeNode)n);
                }
            }
            else
            {
                xret.Add(node);
            }
            return xret;
        }

        private void xCancelButton_Click(object sender, EventArgs e)
        {
            _arg?.Token?.Cancel();
        }

        private void BackupRestoreForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _arg?.Token?.Cancel();
            _BackupWatcher?.Dispose();
        }

        private void xloadzip_Click(object sender, EventArgs e)
        {
            if (isrestoring) return;
            var ofd = new OpenFileDialog();
            ofd.Title = "Backup bestand(.zip) laden";
            ofd.Filter = "Zip|*.zip|Alles|*.*";
            if(ofd.ShowDialog() == DialogResult.OK)
            {
                RestoreBackup(ofd.FileName);
            }
        }

        private void xStatusResetTimer_Tick(object sender, EventArgs e)
        {
            xStatusResetTimer.Stop();
            xStatusLabel.Text = "Idle";
            xProgressBar.Value = 0;
            xStatusLabel.Invalidate();
            xProgressBar.Invalidate();
            xStatusStrip.Visible = false;
        }
    }
}
