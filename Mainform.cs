using AutoUpdaterDotNET;
using Forms;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.SqlLite;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;
using Various;
using Timer = System.Windows.Forms.Timer;

namespace ProductieManager
{
    /// <summary>
    /// De hoofd scherm van de productiemanager
    /// </summary>
    [Serializable]
    public partial class Mainform : Form
    {
        private string _bootDir;
        private PathWatcher _dbWatcher;
        /// <summary>
        /// De titel van de hoofdscherm
        /// </summary>
        public string MainAppTitle
        {
            get => this.Text;
            set => this.Text = value;
        }

        /// <summary>
        /// Update de titel en status text
        /// </summary>
        public void UpdateTitle()
        {
            string xuser = Manager.LogedInGebruiker == null ? "Niet Ingelogd" : $"Ingelogd als: {Manager.LogedInGebruiker.Username}";
            MainAppTitle = $"Productie Manager Versie {ProductVersion} {xuser}";
            xstatuslabel.Text = $@"Database: {Manager.AppRootPath}";
        }
        /// <summary>
        /// Of de hoofdscherm bezig is met laden
        /// </summary>
        public static bool IsLoading { get; private set; }
        private SplashScreen _splash;
        /// <summary>
        /// De timer die ingesteld staat om te kijken voor nieuwe updates
        /// </summary>
        public Timer Updatechecker;

        /// <summary>
        /// Maak een nieuwe hoofdscherm aan
        /// </summary>
        public Mainform()
        {
            //InitBootDir(bootdir);
            InitializeComponent();
            IsLoading = true;
            SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer,
                true);
            //StickyWindow.RegisterExternalReferenceForm(this);
            AutoUpdater.AppCastURL = "https://www.dropbox.com/s/3bnp0so9o1mj8at/UpdateInfo.xml?dl=1";
            AutoUpdater.AppTitle = "Productie Manager";
            AutoUpdater.ShowSkipButton = false;
            AutoUpdater.ApplicationExitEvent += AutoUpdater_ApplicationExitEvent;
            Hide();
            Manager.OnSettingsChanged += ProductieView1_OnSettingsChanged;
            Manager.OnManagerLoaded += _manager_OnManagerLoaded;
            Manager.OnRemoteMessage += _manager_OnRemoteMessage;
            
            //Shown += Mainform_Shown;
            //Task.Run(new Action(productieView1.LoadForm));
            _splash = new SplashScreen(3000) {WindowState = FormWindowState.Normal};
            Shown += Mainform_Shown;
            _splash.Shown += _splash_Shown;
            _splash.Closed += _splash_Closed;
            _splash.Show();
            _dbWatcher = new PathWatcher();
            _dbWatcher.PathLocationFound += _DbWatcher_PathLocationFound;
            _dbWatcher.PathLocationLost += _DbWatcher_PathLocationLost;
        }

        private void _splash_Closed(object sender, EventArgs e)
        {
            IsLoading = false;
            _splash?.Dispose();
            _splash = null;
            StartPosition = FormStartPosition.CenterParent;
            Show();
            this.InitLastInfo();
            Select();
            BringToFront();
            UpdateTitle();
            productieView1.ShowStartupForms();
            AutoUpdater.Start();
        }

        private void AutoUpdater_ApplicationExitEvent()
        {
            Exit(false);
        }

        /// <summary>
        /// Sluit de ProductieManager 
        /// </summary>
        /// <param name="restart">Of je de productiemanager wilt restarten</param>
        public static async void Exit(bool restart)
        {
            var proc = Process.GetCurrentProcess();
            if (proc.MainModule != null && restart)
                Process.Start(proc.MainModule.FileName);
            Manager.DefaultSettings?.SaveAsDefault();
            if (Manager.Opties != null)
            {
                bool cancel = false;
                var opties = Manager.Opties;
                Manager.SettingsChanging(null, ref opties, ref cancel);
                if (cancel) return;
                Manager.Opties = opties;
                await Manager.Opties.Save();
            }
            proc.Kill();
        }
        /// <summary>
        /// Een event die aangeeft of de hoofd database niet meer bestaat
        /// </summary>
        /// <param name="sender">De gene die dat heeft geconstateerd</param>
        /// <param name="e">Event informatie</param>
        private void _DbWatcher_PathLocationLost(object sender, EventArgs e)
        {
            this.Invoke(new Action(DoDbLocationLost));
        }

        private async void DoDbLocationLost()
        {
            try
            {
                _dbWatcher?.Stop();
                bool found = false;
                for (int i = 0; i < 5; i++)
                {
                    if (Directory.Exists(Manager.DefaultSettings.MainDB.UpdatePath))
                    {
                        found = true;
                        break;
                    }

                    await Task.Delay(400);
                }

                if (found) return;
                Dictionary<string, DialogResult> xbtns = new Dictionary<string, DialogResult>
                {
                    {"Herstart", DialogResult.OK},
                    {"Ga Offline", DialogResult.Cancel},
                    {"Afsluiten", DialogResult.No},
                    {"Kies DB", DialogResult.Yes}
                };
                var xrslt = XMessageBox.Show("Oorspronkelijke database kan niet geladen worden!\n\n" + " * Kies 'Herstart' als je de ProductieManager opnieuw wilt opstarten.\n" + " * Kies 'Offline' als je gewoon offline wilt werken.\n" + " * Kies anders voor een andere database of sluit de ProductieManager af.", "Database niet gevonden!", MessageBoxIcon.Warning, null, xbtns);
                if (xrslt == DialogResult.OK)
                {
                    Application.Restart();
                    return;
                }

                if (xrslt == DialogResult.No)
                {
                    this.Close();
                    return;
                }

                if (xrslt == DialogResult.Yes)
                {
                    DbPathChooser ps = new DbPathChooser();
                    if (ps.ShowDialog() == DialogResult.OK)
                    {
                        Manager.DefaultSettings.MainDB.RootPath = ps.SelectedPath;
                        Exit(true);
                        return;
                    }
                }

                productieView1.LoadManager(Manager.DefaultSettings.TempMainDB.RootPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            _dbWatcher?.Start();
        }

        /// <summary>
        /// Of de hoofd database weer is gevonden
        /// </summary>
        /// <param name="sender">De gene die dat heeft geconstateerd</param>
        /// <param name="e">De event informatoie</param>
        private void _DbWatcher_PathLocationFound(object sender, EventArgs e)
        {
            try
            {
                Exit(true);
            }
            catch (Exception ex)
            {
                _dbWatcher.PathLost = true;
                Console.WriteLine(ex);
            }
        }

        private void InitBootDir(string path = null)
        {

            _bootDir = Application.StartupPath;
            Manager.AppRootPath = path ?? _bootDir;

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
                        // ignored
                    }

                    Application.DoEvents();
                }

                if (rootpath != null)
                {
                    _bootDir = rootpath;
                }
                else
                {
                    tempdbent.RootPath = _bootDir;
                    if (tempdbent.LastUpdated < dbent.LastUpdated || tempdbent.LastUpdated.IsDefault())
                    {
                        tempdbent.LastUpdated = DateTime.Now;
                    }

                }

                Manager.DefaultSettings.MainDB = dbent;
                Manager.DefaultSettings.TempMainDB = tempdbent;
                Manager.DefaultSettings.SaveAsDefault();
            }
        }

        private void _manager_OnRemoteMessage(RemoteMessage message, Manager instance)
        {
           ShowMessagePopup(message);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="msg"></param>
        public void ShowMessagePopup(RemoteMessage msg)
        {
            if (msg == null) return;
            if (Manager.Opties is {ToonLogNotificatie: false}) return;
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
            this.BeginInvoke(new Action(() =>
            {
                if (_splash != null)
                {
                    if (_splash.CanClose)
                        _splash.Close();
                    else _splash.CanClose = true;

                }
                _dbWatcher.WatchPath(Manager.DefaultSettings.MainDB.UpdatePath, false, true);
               
            }));

        }

        private void _splash_Shown(object sender, EventArgs e)
        {
            IsLoading = true;
            Task.Factory.StartNew(new Action(Action));
            //BeginInvoke(new Action(Action));
        }

        private void Action()
        {
            this.BeginInvoke(new Action(() =>
            {
                InitBootDir();
                xversie.Text = $@"Versie {ProductVersion}";
                productieView1.LoadManager(_bootDir);
                productieView1.ShowUnreadMessage = true;
                productieView1.UpdateUnreadMessages(null);
                productieView1.UpdateUnreadOpmerkingen();
            }));

        }

        private void Mainform_Shown(object sender, EventArgs e)
        {
            FormShown();
            Invalidate();
            Updatechecker = new Timer { Interval = 60000 };
            Updatechecker.Tick += _updatechecker_Tick;
            Updatechecker.Start();
        }

        private void _updatechecker_Tick(object sender, EventArgs e)
        {
            AutoUpdater.Start();
        }

        /// <summary>
        /// Een event voor als de hoofdscherm zichtbaar is
        /// </summary>
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


        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.valksystemen.nl/");
        }

        private void mynotifyicon_Click()
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

        private void Cnn_InfoMessage(SqlInfoMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
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
               
                Manager.ProductieProvider?.StopSync();
                Manager.ProductieProvider?.DisableOfflineDb();
                if (Manager.LogedInGebruiker != null)
                    Manager.LogOut(this,false);
                else Manager.SaveSettings(Manager.Opties, false, false, true);
                productieView1.DetachEvents();
                //  _updatechecker?.Stop();
                // _updatechecker = null;
                //Manager.Database?.Dispose();
                //ProductieView._manager?.Dispose();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private void Mainform_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized && Manager.Opties is {MinimizeToTray: true})
            {
                this.Hide();
                notifyIcon1.Visible = true;
                notifyIcon1.Text = $@"ProductieManager Versie {Application.ProductVersion}";
                notifyIcon1.ShowBalloonTip(5000);
            }
        }

        #endregion "Mainform"

        #region IMain Interface
        /// <summary>
        /// Toon de hoofdscherm
        /// </summary>
        public void ShowForm()
        {
            Show();
        }

        /// <summary>
        /// Geef aan dat de hoofdscherm zichtbaar is
        /// </summary>
        protected void FormShown()
        {
            OnFormShown?.Invoke(this, EventArgs.Empty);
        }

        #endregion IMain Interface

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Show();
            this.WindowState = FormWindowState.Normal;
            notifyIcon1.Visible = false;
        }

    }
}