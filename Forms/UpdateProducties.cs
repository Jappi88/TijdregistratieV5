using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forms.MetroBase;
using ProductieManager.Properties;
using Rpm.Productie;
using Rpm.Productie.ArtikelRecords;
using Rpm.Settings;
using Rpm.SqlLite;
using Rpm.Various;

namespace Forms
{
    public partial class UpdateProducties : MetroBaseForm
    {
        public CancellationTokenSource Cancelation;

        public UpdateProducties(DatabaseUpdateEntry updateentry = null)
        {
            InitializeComponent();
            UpdateEntry = updateentry;
            UpdateMethod = UpdateForms;
            Cancelation = new CancellationTokenSource();
        }

        public Func<Task> UpdateMethod { get; set; }
        public DatabaseUpdateEntry UpdateEntry { get; }
        public bool IsBussy { get; private set; }

        public bool ShowStop { get; set; } = true;

        public bool StartWhenShown { get; set; }

        public bool CloseWhenFinished { get; set; }
        public bool IsFinished { get; private set; }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void xstartb_Click(object sender, EventArgs e)
        {
            Start();
        }

        private void Start()
        {
            if (IsBussy)
            {
                Stop();
            }
            else
            {
                if (Manager.Database == null || Manager.Database.IsDisposed)
                {
                    XMessageBox.Show(
                        this,
                        "Database is niet beschikbaar!\nZorg ervoor dat je een geldige database hebt, en restart het programma.",
                        "Ongeldige Database", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    //var count = await Manager.Database.Count(DbType.GereedProducties);
                    //count += await Manager.Database.Count(DbType.Producties);
                    //if (count > 0)
                    //{
                    Cancelation = new CancellationTokenSource();
                    StartMethod(UpdateMethod);

                    //}
                    //else if (!CloseWhenFinished)
                    //    XMessageBox.Show(this, $"Er is zijn geen producties beschikbaar", "Productie Updates");
                
                }
            }
        }

        public Task StartMethod(Func<Task> Update)
        {
            return Update?.Invoke();
        }

        private void Stop()
        {
            if (!IsBussy)
                return;
            IsBussy = false;
            Cancelation.Cancel();
        }

        private Task UpdateForms()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    if (IsDisposed) return;
                    OnStarted();
                    var count = 0;
                    var oldlog = Manager.Database.LoggerEnabled;
                    var oldnot = Manager.Database.NotificationEnabled;
                    Manager.Database.LoggerEnabled = false;
                    Manager.Database.NotificationEnabled = false;

                    if (UpdateEntry != null)
                    {
                        xUpdateFromDatabaseEntry(UpdateEntry);
                    }
                    else
                    {
                        Manager.Database.RaiseEventWhenChanged = false;
                        Manager.Database.RaiseEventWhenDeleted = false;
                        Cancelation?.Token.ThrowIfCancellationRequested();
                        DoProgress("Updating Medewerkers...", count, 100);
                        xUpdateUserActivity(false);
                        if (!IsBussy || IsDisposed || Disposing)
                            return;
                        Cancelation?.Token.ThrowIfCancellationRequested();
                        DoProgress("Producties laden...", count, 100);
                        var files = Manager.xGetAllProductiePaths(true, true);
                        Cancelation?.Token.ThrowIfCancellationRequested();
                        var forms = Manager.Database.xGetAllProducties(true, false,null, true);
                        Cancelation?.Token.ThrowIfCancellationRequested();
                        var artikels = new List<ArtikelRecord>();
                        foreach (var file in files)
                        {
                            if (!IsBussy || IsDisposed || Disposing)
                                break;
                            var form = MultipleFileDb.xFromPath<ProductieFormulier>(file, true);
                            Cancelation?.Token.ThrowIfCancellationRequested();
                            if (form == null) continue;
                            form.xUpdateForm(true, true, forms, "", true, false, false, true);
                            Cancelation?.Token.ThrowIfCancellationRequested();
                            if (form.State == ProductieState.Gereed)
                            {
                                var xartikel = artikels.FirstOrDefault(x => string.Equals(form.ArtikelNr, x.ArtikelNr,
                                    StringComparison.CurrentCultureIgnoreCase));
                                if (xartikel == null)
                                {
                                    xartikel = Manager.ArtikelRecords?.GetRecord(form.ArtikelNr ) ??
                                               new ArtikelRecord();
                                    xartikel.ResetValues(false);
                                    if (Manager.ArtikelRecords?.UpdateWaardes(form, xartikel, false) ?? false)
                                        artikels.Add(xartikel);
                                }
                                else
                                {
                                    Manager.ArtikelRecords?.UpdateWaardes(form, xartikel, false);
                                }
                                Cancelation?.Token.ThrowIfCancellationRequested();
                                foreach (var bw in form.Bewerkingen)
                                    foreach (var plek in bw.WerkPlekken)
                                    {
                                        Cancelation?.Token.ThrowIfCancellationRequested();
                                        xartikel = artikels.FirstOrDefault(x => string.Equals(plek.Naam, x.ArtikelNr,
                                            StringComparison.CurrentCultureIgnoreCase));
                                        if (xartikel == null)
                                        {
                                            xartikel = Manager.ArtikelRecords?.GetRecord(plek.Naam) ??
                                                       new ArtikelRecord();
                                            xartikel.ResetValues(false);
                                            if (Manager.ArtikelRecords?.UpdateWaardes(plek, xartikel, false) ?? false)
                                                artikels.Add(xartikel);
                                        }
                                        else
                                        {
                                            Manager.ArtikelRecords?.UpdateWaardes(plek, xartikel, false);
                                        }
                                    }
                            }

                            count++;
                            DoProgress($"Updating productie '{form.ProductieNr}'", count, files.Count);
                        }
                        Cancelation?.Token.ThrowIfCancellationRequested();
                        if (Manager.ArtikelRecords != null)
                        {
                            var xprogarg = new ProgressArg();
                            xprogarg.Changed += Xprogarg_Changed;
                            Manager.ArtikelRecords.SaveRecords(artikels, xprogarg).Wait();
                        }

                        if (IsBussy)
                        {
                            var opties = Manager.DefaultSettings ?? UserSettings.GetDefaultSettings();
                            IsFinished = true;
                            opties.UpdateDatabaseVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
                            opties.SaveAsDefault();
                            var x1 = count == 1 ? "update" : "updates";
                            DoProgress($"{count} {x1} Uitgevoerd!", 100);
                        }
                    }

                    Manager.Database.RaiseEventWhenChanged = true;
                    Manager.Database.RaiseEventWhenDeleted = true;
                    Manager.Database.LoggerEnabled = oldlog;
                    Manager.Database.NotificationEnabled = oldnot;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
                OnFinished();
            });
        }

        private void Xprogarg_Changed(object sender, ProgressArg arg)
        {
            DoProgress($"{arg.Message}", arg.Current, arg.Max);
        }

        public Task UpdateUserActivity(bool gestart)
        {
            return Task.Run(() =>
            {
                xUpdateUserActivity(gestart);
  
            });
        }

        public void xUpdateUserActivity(bool gestart)
        {
            try
            {
                var users = Manager.Database.xGetAllPersoneel();
                var done = 0;
                for (var i = 0; i < users.Count; i++)
                {
                    if (!IsBussy || IsDisposed || Disposing)
                        break;
                    Cancelation?.Token.ThrowIfCancellationRequested();
                    var user = users[i];
                    DoProgress($"Updating {user.PersoneelNaam}...", i, users.Count);
                    var userchanged = false;
                    if (user.PersoneelNaam.EndsWith(" "))
                    {
                        user.PersoneelNaam = user.PersoneelNaam.Trim();
                        userchanged = true;
                    }

                    var toremove = new List<Klus>();

                    var klusjes = gestart
                        ? user.Klusjes.Where(x => x.Status == ProductieState.Gestart).ToList()
                        : user.Klusjes
                            .ToList(); //.Where(x=> x.IsActief).ToList(); //.Where(x => x.Status == ProductieState.Gestart).ToArray();

                    if (klusjes.Count > 0)
                        foreach (var klus in klusjes)
                        {
                            Cancelation?.Token.ThrowIfCancellationRequested();
                            var pair = klus.GetWerk();
                            var prod = pair?.Formulier;
                            var bew = pair?.Bewerking;
                            if (prod == null || bew == null)
                            {
                                user.Klusjes.Remove(klus);
                                userchanged = true;
                                continue;
                            }

                            var saved = false;
                            var plek =
                                bew.WerkPlekken.FirstOrDefault(x =>
                                    string.Equals(x.Naam, klus.WerkPlek,
                                        StringComparison.CurrentCultureIgnoreCase));
                            if (plek == null)
                            {
                                toremove.Add(klus);
                                continue;
                            }
                            Cancelation?.Token.ThrowIfCancellationRequested();
                            var xolduser = plek.Personen.FirstOrDefault(x =>
                                string.Equals(x.PersoneelNaam, user.PersoneelNaam,
                                    StringComparison.CurrentCultureIgnoreCase));
                            var msg = "";
                            if (bew.State != klus.Status)
                            {
                                switch (bew.State)
                                {
                                    case ProductieState.Gestopt:
                                        klus.Stop();
                                        saved = true;
                                        break;
                                    case ProductieState.Gestart:
                                        if (klus.IsActief)
                                        {
                                            klus.Start();
                                            saved = true;
                                        }

                                        break;
                                    case ProductieState.Gereed:
                                        klus.MeldGereed();
                                        saved = true;
                                        break;
                                    case ProductieState.Verwijderd:
                                        klus.Stop();
                                        klus.Status = ProductieState.Verwijderd;
                                        saved = true;
                                        break;
                                }

                                msg =
                                    $"{klus.Path} van {user.PersoneelNaam}  is verandert naar {Enum.GetName(typeof(ProductieState), klus.Status)}.";
                            }
                            Cancelation?.Token.ThrowIfCancellationRequested();
                            if (klus.Tijden.IsActief && klus.Status != ProductieState.Gestart)
                            {
                                klus.Stop();
                                saved = true;
                            }

                            if (saved)
                            {
                                userchanged = true;
                                if (xolduser != null && xolduser.ReplaceKlus(klus))
                                    bew.UpdateBewerking(null, $"{klus.Naam} klus geupdate");
                            }
                        }
                    Cancelation?.Token.ThrowIfCancellationRequested();
                    if (userchanged || toremove.Count > 0)
                    {
                        foreach (var k in toremove)
                            user.Klusjes.Remove(k);
                        Manager.Database.xUpSert(user.PersoneelNaam, user, $"{user.PersoneelNaam} update");
                    }

                    done++;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Task<int> UpdateFromDatabaseEntry(DatabaseUpdateEntry entry)
        {
            return Task.Run(() =>
            {
                var count = 0;
                try
                {
                    count = xUpdateFromDatabaseEntry(entry);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return count;
            });
        }

        public int xUpdateFromDatabaseEntry(DatabaseUpdateEntry entry)
        {
            var count = 0;
            try
            {
                count = Manager.Database.xUpdateDbFromDb(entry, Cancelation, ProgressChanged);
                DoProgress("Update Geslaagd!", 100);
                IsFinished = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return count;
        }

        public void ProgressChanged(object sender, ProgressArg arg)
        {
            arg.IsCanceled = !IsBussy;
            DoProgress(arg.Message, arg.Pogress);
        }

        private void OnFinished()
        {
            if (IsDisposed) return;
            if (InvokeRequired)
            {
                if (CloseWhenFinished)
                {
                    Invoke(new Action(() => DialogResult = DialogResult.OK));
                    //this.Invoke(new Action(this.Close));
                }
                else
                {
                    xstartb.Invoke(new Action(() => xstartb.Image = Resources.play_button_icon_icons_com_60615));
                    xstartb.Invoke(new Action(xstartb.Invalidate));
                    circularProgressBar1.Invoke(new Action(() => circularProgressBar1.Value = 65));
                    circularProgressBar1.Invoke(new Action(() => circularProgressBar1.SubscriptText = ""));
                    circularProgressBar1.Invoke(new Action(circularProgressBar1.Invalidate));
                }
            }
            else
            {
                if (CloseWhenFinished)
                {
                    DialogResult = DialogResult.OK;
                    //this.Close();
                }
                else
                {
                    xstartb.Image = Resources.play_button_icon_icons_com_60615;
                    xstartb.Invalidate();
                    circularProgressBar1.Value = 68;
                    circularProgressBar1.SubscriptText = "";
                    circularProgressBar1.Invalidate();
                }
            }

            IsBussy = false;
        }

        private void OnStarted()
        {
            if (InvokeRequired)
            {
                xstartb.Invoke(new Action(() => xstartb.Image = Resources.stop_red256_24890));
                xstartb.Invoke(new Action(xstartb.Invalidate));
            }
            else
            {
                xstartb.Image = Resources.stop_red256_24890;
                xstartb.Invalidate();
            }

            IsBussy = true;
        }

        private void DoProgress(string message, double min, double max)
        {
            var percentage = max > 0 && min > 0 ? (int)(min / max * 100) : 0;
            DoProgress(message, percentage);
        }

        private void DoProgress(string message, int percentage)
        {
            if (InvokeRequired)
            {
                circularProgressBar1.Invoke(
                    new Action(() => circularProgressBar1.Text = $"{message}"));
                circularProgressBar1.Invoke(new Action(() => circularProgressBar1.Value = percentage));
                circularProgressBar1.Invoke(new Action(() => circularProgressBar1.SubscriptText = percentage + "%"));
                circularProgressBar1.Invoke(new Action(circularProgressBar1.Invalidate));
            }
            else
            {
                circularProgressBar1.Text = $"{message}";
                circularProgressBar1.Value = percentage;
                circularProgressBar1.SubscriptText = percentage + "%";
                circularProgressBar1.Invalidate();
            }
        }

        private void UpdateProducties_FormClosing(object sender, FormClosingEventArgs e)
        {
            Stop();
        }

        private void UpdateProducties_Shown(object sender, EventArgs e)
        {
            xstartb.Visible = ShowStop;
            //var openform = Application.OpenForms["SplashScreen"];
            //openform?.Close();
            if (StartWhenShown)
                Start();
        }
    }
}