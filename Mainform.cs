using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoUpdaterDotNET;
using Controls;
using Forms;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.SqlLite;
using Various;
using Timer = System.Windows.Forms.Timer;

namespace ProductieManager
{
    [Serializable]
    public partial class Mainform : Form
    {
        private string _BootDir;
        private PathWatcher _DbWatcher;
        public string MainAppTitle
        {
            get => this.Text;
            set => this.Text = value;
        }


        public void UpdateTitle()
        {
            string xuser = Manager.LogedInGebruiker == null ? "Niet Ingelogd" : $"Ingelogd als: {Manager.LogedInGebruiker.Username}";
            MainAppTitle = $"Productie Manager Versie {ProductVersion} {xuser}";
            xstatuslabel.Text = $"Database: {Manager.AppRootPath}";
        }

        public static bool IsLoading { get; private set; }
        private SplashScreen _splash;

        public Timer _updatechecker;

        public Mainform() : this(null)
        {
        }

        public Mainform(string bootdir = null)
        {
            //InitBootDir(bootdir);
            InitializeComponent();
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            //StickyWindow.RegisterExternalReferenceForm(this);
            AutoUpdater.AppCastURL = "https://www.dropbox.com/s/3bnp0so9o1mj8at/UpdateInfo.xml?dl=1";
            AutoUpdater.AppTitle = "Productie Manager";
            AutoUpdater.ShowSkipButton = false;
            _updatechecker = new Timer {Interval = 60000};
            _updatechecker.Tick += _updatechecker_Tick;
            _updatechecker.Start();
            Hide();
            Manager.OnSettingsChanged += ProductieView1_OnSettingsChanged;
            Manager.OnManagerLoaded += _manager_OnManagerLoaded;
            Manager.OnRemoteMessage += _manager_OnRemoteMessage;
            
            //Shown += Mainform_Shown;
            //Task.Run(new Action(productieView1.LoadForm));
            _splash = new SplashScreen(1500) {WindowState = FormWindowState.Normal};
            _splash.FinishedLoading += _splash_FinishedLoading;
            _splash.FormClosed += Screen_FormClosed;
            Shown += Mainform_Shown;
            _splash.Shown += _splash_Shown;
            _splash.Show();
            _DbWatcher = new PathWatcher();
            _DbWatcher.PathLocationFound += _DbWatcher_PathLocationFound;
            _DbWatcher.PathLocationLost += _DbWatcher_PathLocationLost;
        }

        private void _DbWatcher_PathLocationLost(object sender, EventArgs e)
        {
            _DbWatcher?.Stop();
            Dictionary<string, DialogResult> xbtns = new Dictionary<string, DialogResult>();
            xbtns.Add("Blijf Offline", DialogResult.Cancel);
            xbtns.Add("Herstart", DialogResult.OK);
            xbtns.Add("Kies DB", DialogResult.Yes);
            this.Invoke(new MethodInvoker(() =>
            {
                try
                {
                    var xrslt = XMessageBox.Show("Oorspronkelijke database kan niet geladen worden!\n\n" +
                        "Kies 'Offline' als je gewoon op de standaard database wilt werken.\n" +
                        "Kies 'Herstart' als je de ProductieManager opnieuw wilt opstarten.\n" +
                        "Kies anders voor een andere database.", "Database niet gevonden!",
                        MessageBoxIcon.Exclamation, null, xbtns);
                    if (xrslt == DialogResult.OK) { Application.Restart(); return; }
                    if (xrslt == DialogResult.Yes)
                    {
                        DbPathChooser ps = new DbPathChooser();
                        if (ps.ShowDialog() == DialogResult.OK)
                        {
                            Manager.DefaultSettings.MainDB.RootPath = ps.SelectedPath;
                            Application.Restart();
                            return;
                        }
                    }
                    productieView1.LoadManager(Manager.DefaultSettings.TempMainDB.RootPath, true);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }));
            _DbWatcher?.Start();
        }

        private void _DbWatcher_PathLocationFound(object sender, EventArgs e)
        {
            try
            {
                Application.Restart();
            }
            catch (Exception ex) { };
        }

        private Task InitBootDir(string path = null)
        {
            return Task.Run(() =>
            {
                _BootDir = Application.StartupPath;
                Manager.AppRootPath = path ?? _BootDir;
               
                if (Manager.DefaultSettings != null)
                {
                    var dbent = Manager.DefaultSettings.MainDB;
                    var tempdbent = Manager.DefaultSettings.TempMainDB;
                    dbent ??= new DatabaseUpdateEntry()
                    {
                        Naam = "Main Boot Dir",
                        RootPath = Manager.DefaultSettings.MainDbPath ?? Application.StartupPath,
                        UpdateDatabases =
                            new List<DbType>()
                            {
                                DbType.Accounts,
                                DbType.Producties,
                                DbType.GereedProducties,
                                DbType.Medewerkers,
                                DbType.Opties
                            }
                    };
                    tempdbent ??= new DatabaseUpdateEntry()
                    {
                        Naam = "Temp Boot Dir",
                        RootPath = Application.StartupPath,
                        UpdateDatabases =
                            new List<DbType>()
                            {
                                DbType.Accounts,
                                DbType.Producties,
                                DbType.GereedProducties,
                                DbType.Medewerkers,
                                DbType.Opties
                            }
                    };
                    string rootpath = null;
                    for (int i = 0; i < 5; i++)
                    {
                        try
                        {
                            if (Directory.Exists(dbent.UpdatePath))
                            {
                                rootpath = dbent.RootPath;
                                break;
                            }
                        }
                        catch (Exception e)
                        {
                        }

                        Application.DoEvents();
                    }

                    if (rootpath != null)
                    {
                        _BootDir = rootpath;
                    }
                    else
                    {
                        tempdbent.RootPath = _BootDir;
                        if (tempdbent.LastUpdated < dbent.LastUpdated || tempdbent.LastUpdated.IsDefault())
                        {
                            tempdbent.LastUpdated = DateTime.Now;
                        }

                    }

                    Manager.DefaultSettings.MainDB = dbent;
                    Manager.DefaultSettings.TempMainDB = tempdbent;
                    Manager.DefaultSettings.SaveAsDefault();
                }
            });
        }

        private void _splash_FinishedLoading(object sender, EventArgs e)
        {
            IsLoading = false;
        }

        private void _manager_OnRemoteMessage(RemoteMessage message, Manager instance)
        {
           ShowMessagePopup(message);
        }

        public void ShowMessagePopup(RemoteMessage msg)
        {
            if (msg == null) return;
            try
            {
                //if (InvokeRequired)
                //    BeginInvoke(new Action(msg.Notify));
                //else
                    msg.Notify(this);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void _manager_OnManagerLoaded()
        {
            if (_splash != null)
                if (_splash.CanClose)
                    _splash.Close();
                else _splash.CanClose = true;
            _DbWatcher.WatchPath(Manager.DefaultSettings.MainDB.UpdatePath, false, true);
        }

        private void _splash_Shown(object sender, EventArgs e)
        {
            IsLoading = true;
            BeginInvoke(new Action(async () =>
            {
                await InitBootDir();
                UpdateTitle();
                xversie.Text = $"Versie {ProductVersion}";
                productieView1.LoadManager(_BootDir);
            }));
        }

        private void Mainform_Shown(object sender, EventArgs e)
        {
            FormShown();
            Invalidate();
        }

        private void _updatechecker_Tick(object sender, EventArgs e)
        {
            AutoUpdater.Start();
        }

        public event EventHandler OnFormShown;

        //private void InitBootDir(string bootdir)
        //{
        //    if (bootdir != null)
        //    {
        //        BootDir = bootdir;
        //    }
        //    else
        //    {
        //        var path = Manager.AppRootPath;
        //        var choosenew = false;
        //        if (string.IsNullOrEmpty(path) || !Directory.Exists(path))
        //            choosenew = true;
        //        else if (!string.IsNullOrEmpty(path) && Directory.Exists(path + "\\RPM_Data"))
        //            choosenew = Directory.GetFiles(path + "\\RPM_Data", "*.db", SearchOption.TopDirectoryOnly).Length ==
        //                        0;
        //        else choosenew = true;
        //        if (choosenew)
        //        {
        //            var db = new DbPathChooser();
        //            db.SelectedPath = path;
        //            if (db.ShowDialog() == DialogResult.OK) BootDir = db.SelectedPath;
        //        }
        //        else
        //        {
        //            BootDir = path;
        //        }
        //    }
        //}

        private void CleanupUnusedDlls()
        {
            try
            {
                var asms = AppDomain.CurrentDomain.GetAssemblies();
                var names = new List<AssemblyName>();
                foreach (var asm in asms)
                {
                    var asmlibs = asm.GetReferencedAssemblies().Where(x =>
                        !x.FullName.ToLower().StartsWith("system") &&
                        !x.FullName.ToLower().StartsWith("mscorlib")).ToArray();
                    foreach(var asmlib in asmlibs)
                        if(!names.Any(x=> x.Equals(asmlib)))
                            names.Add(asmlib);
                }

                var dlls = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll",
                    SearchOption.TopDirectoryOnly);
                foreach (var dll in dlls)
                {
                    var dllname = Path.GetFileNameWithoutExtension(dll);
                    var dllasm = names.FirstOrDefault(x =>
                        string.Equals(dllname, x.Name, StringComparison.CurrentCultureIgnoreCase));
                    
                    if (dllasm == null)
                        File.Delete(dll);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ProductieView1_OnSettingsChanged(object instance,UserSettings user, bool reinit)
        {
            BeginInvoke(new Action(UpdateTitle));
        }

        private void Screen_FormClosed(object sender, FormClosedEventArgs e)
        {
            StartPosition = FormStartPosition.CenterParent;
            Show();
            this.InitLastInfo();
            Select();
            BringToFront();
            AutoUpdater.Start();
            productieView1.ShowUnreadMessage = true;
            productieView1.UpdateUnreadMessages(null);
            _splash.Dispose();
            _splash = null;
            IsLoading = false;
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.valksystemen.nl/");
        }

        private void mynotifyicon_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
        }

        private void openenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                Show();
                WindowState = FormWindowState.Normal;
            }
        }

        private void sluitenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Cnn_InfoMessage(object sender, SqlInfoMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        private static List<string> getAllHostNames()
        {
            var hostNames = new List<string>();
            var ipaddress = Dns.GetHostAddresses(Dns.GetHostName());
            string hname;

            foreach (var ip in ipaddress)
                //if ipv4
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    hname = Dns.GetHostEntry(ip).HostName.ToLower();
                    if (!hostNames.Contains(hname)) hostNames.Add(hname);
                }

            return hostNames;
        }

        #region "Mainform"

        private void Mainform_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                try
                {
                    var forms = Application.OpenForms;
                    for(int i = 0; i < forms.Count; i++)
                    {
                        var open = forms[i];
                        if (string.IsNullOrEmpty(open.Name) || open.Name == "Mainform") continue;
                        open.Close();
                    }
                }
                catch (Exception exception)
                {
                    Console.WriteLine(exception);
                }


                this.SetLastInfo();
                Manager.DefaultSettings?.SaveAsDefault();
                Manager.SaveSettings(Manager.Opties, false, false, true);

                productieView1.DetachEvents();
                //  _updatechecker?.Stop();
                // _updatechecker = null;
                Manager.Database?.Dispose();
                ProductieView._manager?.Dispose();
            }
            catch (Exception)
            {
            }
        }

        #endregion "Mainform"

        #region IMain Interface

        public void ShowForm()
        {
            Show();
        }

        protected void FormShown()
        {
            OnFormShown?.Invoke(this, EventArgs.Empty);
        }

        #endregion IMain Interface

        private void Mainform_Resize(object sender, EventArgs e)
        {
            if(this.WindowState == FormWindowState.Minimized && Manager.Opties != null && Manager.Opties.MinimizeToTray)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.Text = $"ProductieManager Versie {Application.ProductVersion}";
                notifyIcon1.ShowBalloonTip(5000);
            }
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }
    }
}