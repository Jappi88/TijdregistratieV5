using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ProductieManager.Forms;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using Various;

namespace Forms
{
    public partial class WerktijdChanger : Forms.MetroBase.MetroBaseForm
    {
        private Rooster CurrentRooster { get; set; }
        private List<Rooster> SpecialeRoosters { get; set; }
        public bool SaveChanges { get; set; }
        public WerktijdChanger()
        {
            InitializeComponent();
            ((OLVColumn)xwerktijden.Columns[0]).AspectGetter = TijdGestartGet;
            ((OLVColumn)xwerktijden.Columns[1]).AspectGetter = TijdGestoptGet;
            ((OLVColumn)xwerktijden.Columns[2]).AspectGetter = TijdGewerktGet;
            ((OLVColumn)xwerktijden.Columns[3]).AspectGetter = EigenRoosterGet;
            ((OLVColumn)xwerktijden.Columns[4]).AspectGetter = IsActiefGet;
        }
        public WerktijdChanger(Klus klus):this()
        {
            
            Klusje = klus;
            CurrentRooster = klus.Tijden.WerkRooster?.CreateCopy();
            SpecialeRoosters = klus.Tijden.SpecialeRoosters?.CreateCopy();
            xwerktijden.SetObjects(klus.Tijden.Uren);
            UpdateStatus();
            UpdateUurGewerkt(null);
            UpdateFormText();
        }

        public string Title
        {
            get => this.Text;
            set
            {
                this.Text = value;
                this.Invalidate();
            }
        }

        public WerktijdChanger(WerkPlek werk):this()
        {
            Werklplek = werk;
            CurrentRooster = werk.Tijden.WerkRooster?.CreateCopy();
            SpecialeRoosters = werk.Tijden.SpecialeRoosters?.CreateCopy();
            xwerktijden.SetObjects(werk.Tijden.Uren);
            UpdateStatus();
            UpdateUurGewerkt(null);
            UpdateFormText();
        }

        public Klus Klusje { get; }
        public WerkPlek Werklplek { get; }

        private void UpdateFormText()
        {
            if (Werklplek != null)
            {
                Text = $@"Wijzig werktijden van: {Werklplek.Path}";
                
            }
            else if (Klusje != null)
            {
                Text = $@"Wijzig werktijden van '{Klusje.PersoneelNaam}' op '{Klusje.Path}'";
            }
            if (CurrentRooster != null && CurrentRooster.IsCustom())
                Text += @" [Aangepaste Rooster]";
            this.Invalidate();
        }

        public bool IsActief()
        {
            if (Klusje != null) return Klusje.Status == ProductieState.Gestart;
            if (Werklplek != null) return Werklplek.Werk.State == ProductieState.Gestart;
            return false;
        }

        private object TijdGewerktGet(object item)
        {
            if (item is TijdEntry pair)
            {
                var rooster = GetRooster(pair);
                if (pair.ExtraTijd != null)
                {
                    var ent = new TijdEntry(GetFirstGestart(), GetLastGestopt(), rooster);
                    return $"Extra {Math.Round(pair.ExtraTijd.ExtraUren(ent, rooster), 2)} uur";
                }

                return pair.TijdGewerkt(rooster, null,SpecialeRoosters) + " uur";
            }

            return 0;
        }

        private Rooster GetRooster(TijdEntry entry)
        {
            if (entry?.WerkRooster != null &&  entry.WerkRooster.IsCustom())
                return entry.WerkRooster;
            if (CurrentRooster != null && CurrentRooster.IsCustom())
                return CurrentRooster; 
            return Manager.Opties.GetWerkRooster();
        }

        private DateTime GetFirstGestart()
        {
            var items = xwerktijden.Objects.Cast<TijdEntry>().Where(x => x.ExtraTijd == null).ToArray();
            if (items.Length == 0)
                return DateTime.Now;
            var date = new DateTime();
            foreach (var item in items)
                if (item.Start < date || date.IsDefault())
                    date = item.Start;
            return date;
        }

        private DateTime GetLastGestopt()
        {
            var items = xwerktijden.Objects.Cast<TijdEntry>().Where(x => x.ExtraTijd == null).ToArray();
            if (items.Length == 0)
                return DateTime.Now;
            var date = new DateTime();
            foreach (var item in items)
            {
                if (item.InUse) return DateTime.Now;
                if (item.Stop > date)
                    date = item.Stop;
            }

            return date;
        }

        private object TijdGestartGet(object item)
        {
            if (item is TijdEntry pair) return pair.Start;

            return 0;
        }

        private object TijdGestoptGet(object item)
        {
            if (item is TijdEntry pair)
            {
                if (pair.InUse)
                    return DateTime.Now;
                return pair.Stop;
            }

            return 0;
        }

        private object IsActiefGet(object item)
        {
            if (item is TijdEntry pair) return pair.InUse ? "Ja" : "Nee";

            return "Geen Idee";
        }

        private object EigenRoosterGet(object item)
        {
            if (item is TijdEntry pair) return pair.WerkRooster!= null && pair.WerkRooster.IsCustom()? "Ja" : "Nee";

            return "Geen Idee";
        }

        private void xaddb_Click(object sender, EventArgs e)
        {
            if (AddTijd(xstartdate.Value, xstopdate.Value,null, false,true,GetRooster(null)))
            {
                UpdateUurGewerkt(null);
                UpdateStatus();
            }
        }

        private bool AddTijd(DateTime start, DateTime stop, List<TijdEntry> tijden, bool isactief, bool isnew, Rooster rooster)
        {

            var uurgewerkt = UurGewerkt(rooster);
            if (uurgewerkt <= 0 && isnew)
            {
                XMessageBox.Show(this, $"Ongeldige tijd!\n" +
                                 "Je totale uren kunnen niet '0' zijn.", "Ongeldig", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
                return false;
            }


            //if (stop > DateTime.Now || start > DateTime.Now)
            //{
            //    XMessageBox.Show(
            //        "Ben je helderziend ofzo?\n\n" +
            //        $"Je kan een start of stop tijd niet later zetten dan dat het nu is ({DateTime.Now})",
            //        "Waarschuwing",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            var wt = tijden??GetWerkTijden();
            //if (wt.Any(x => start > x.Start && stop < x.Stop))
            //{
            //    XMessageBox.Show(
            //        "Het is niet mogelijk om een tijd toe te voegen waar al reeds in wordt gewerkt!\nVul in een tijd dat voor of na een bestaande tijdframe zit.",
            //        "Reeds Gewerkt",
            //        MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            return AddWerkTijd(start, stop,wt, isactief,rooster);
        }

        private bool AddWerkTijd(DateTime start, DateTime stop,List<TijdEntry> tijden, bool isactief,Rooster rooster = null)
        {
            var wt = tijden??GetWerkTijden();
            var lijst = new UrenLijst();
            lijst.SpecialeRoosters = SpecialeRoosters;
            lijst.WerkRooster = CurrentRooster;
            lijst.SetUren(wt.ToArray(), isactief,true);
            rooster ??= GetRooster(null);
            var xent = new TijdEntry(start, stop,rooster) {InUse = isactief};
            var changed = false;
            lijst.Add(xent, ref changed);
            xwerktijden.SetObjects(lijst.Uren);
            xwerktijden.SelectedObject = xent;
            xwerktijden.SelectedItem?.EnsureVisible();
            SetEnable();
            return changed;
        }

        private bool AddWerkTijd(TijdEntry entry)
        {
            var selected = xwerktijden.SelectedObject;
            var wt = GetWerkTijden();
            wt.Add(entry);
            xwerktijden.SetObjects(wt);
            xwerktijden.SelectedObject = selected;
            xwerktijden.SelectedItem?.EnsureVisible();
            SetEnable();
            return true;
        }

        private void xdeleteb_Click(object sender, EventArgs e)
        {
            VerwijderSelected();
        }

        private void VerwijderSelected()
        {
            if (xwerktijden.SelectedObjects.Count > 0)
            {
                foreach (var x in xwerktijden.SelectedObjects.Cast<TijdEntry>())
                    if (!x.InUse)
                        xwerktijden.RemoveObject(x);
                SetEnable();
                UpdateStatus();
            }
        }

        private void xokb_Click(object sender, EventArgs e)
        {
            var rooster = CurrentRooster != null && CurrentRooster.IsValid()
                ? CurrentRooster
                : Manager.Opties?.GetWerkRooster()??Rooster.StandaartRooster();
            var tijden = GetWerkTijden().ToArray();
            var result = DialogResult.No;
            if (Klusje != null)
            {
                Klusje.Tijden.WerkRooster = rooster;
                Klusje.Tijden.SpecialeRoosters = SpecialeRoosters;
                Klusje.Tijden.SetUren(tijden, Klusje.Status == ProductieState.Gestart,true);
            }
            else if(Werklplek != null)
            {
                bool isgestart = false;
                if (Werklplek.Personen is {Count: > 0})
                {
                    var xpers = Werklplek.Personen.Where(x => x.IngezetAanKlus(Werklplek.Path,true)).ToList();
                    if (xpers.Count > 0)
                    {
                        var x0 = xpers.Count == 1 ? xpers[0].PersoneelNaam : $"Er zijn {xpers.Count}";
                        var x1 = xpers.Count == 1 ? "is" : "medewerkers";
                        var x2 = xpers.Count == 1 ? xpers[0].PersoneelNaam : "ze";
                        var x3 = xpers.Count == 1 ? "deze werktijden" : "allemaal deze werktijden";
                        var x4 = xpers.Count == 1 ? "krijgt" : "hebben";
                        result = XMessageBox.Show(this, $"{x0} {x1} actief op {Werklplek.Naam}.\n\n" +
                                                      $"Wil je {x2} {x3} geven?",
                            "Medewerkers Werktijden",
                            MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Cancel) return;
                    }
                }
                Werklplek.Tijden.WerkRooster = rooster;
                Werklplek.Tijden.SpecialeRoosters = SpecialeRoosters;
                Werklplek.Tijden.SetUren(tijden, Werklplek.Werk.State == ProductieState.Gestart, true);
                //Werklplek.Tijden.UpdateUrenRooster(true,false);
                Werklplek.UpdateWerkRooster(rooster,false, false,result == DialogResult.Yes, SaveChanges,false, true, false);
            }

            DialogResult = DialogResult.OK;
        }

        private void xcancelb_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xstartdate_ValueChanged(object sender, EventArgs e)
        {
            UpdateUurGewerkt(GetRooster(xwerktijden.SelectedObject as TijdEntry));
        }

        private void xstopdate_ValueChanged(object sender, EventArgs e)
        {
            UpdateUurGewerkt(GetRooster(xwerktijden.SelectedObject as TijdEntry));
        }

        private void UpdateUurGewerkt(Rooster rooster)
        {
            xuurgewerkt.Text = UurGewerkt(rooster) + " uur";
            xwerktijden.RefreshObjects(xwerktijden.Objects.Cast<TijdEntry>().ToList());
        }

        private void UpdateStatus()
        {
            //update status totaal uur vrij
            var rooster = GetRooster(null);
            var tijden = xwerktijden.Objects.Cast<TijdEntry>().ToArray();
            var lijst = new UrenLijst(tijden)
            {
                SpecialeRoosters = SpecialeRoosters,
                WerkRooster = rooster
            };
            xstatuslabel.Text = $@"Totaal Gewerkt: {Math.Round(lijst.TijdGewerkt(rooster, null), 2)} uur";
        }

        public double UurGewerkt(DateTime start, DateTime stop, Rooster rooster)
        {
            rooster ??= GetRooster(null);
            return Math.Round(
                Werktijd.TijdGewerkt(new TijdEntry(start, stop, rooster), rooster,SpecialeRoosters).TotalHours, 2);
        }

        public double UurGewerkt(Rooster rooster)
        {
            return UurGewerkt(xstartdate.Value, xstopdate.Value,rooster);
        }

        public List<TijdEntry> GetWerkTijden()
        {
            return xwerktijden.Objects?.Cast<TijdEntry>().ToList() ?? new List<TijdEntry>();
        }

        private void xwerktijden_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEnable();
        }

        private void SetEnable()
        {
            xdeleteb.Enabled = xwerktijden.SelectedObjects.Count > 0;
            xpasaan.Enabled = xwerktijden.SelectedObjects.Count == 1;
            xentryrooster.Enabled = xwerktijden.SelectedObjects.Count == 1;
            if (xwerktijden.SelectedObject is TijdEntry entry)
            {
                xstartdate.Value = entry.Start;

                if (entry.InUse)
                {
                    xdeleteb.Enabled = false;
                    xgestoptlabel.Text = @"Momenteel Actief!";
                    xstopdate.Value = DateTime.Now;
                    xstopdate.Enabled = false;
                    return;
                }

                xstopdate.Value = entry.ExtraTijd != null
                    ? entry.Start.AddHours(entry.ExtraTijd.Tijd.TotalHours)
                    : entry.Stop;
            }

            xgestoptlabel.Text = @"Gestopt Op";
            xstopdate.Enabled = true;
        }

        private void xpasaan_Click(object sender, EventArgs e)
        {
            if (xwerktijden.SelectedObject is TijdEntry remove)
            {

                if (remove.ExtraTijd != null)
                {
                    //remove.ExtraTijd.Start = xstartdate.Value;
                    //remove.ExtraTijd.Stop = xstopdate.Value;
                    var exform = new AddExtraTijdForm(remove);
                    if (exform.ShowDialog() == DialogResult.OK)
                    {
                        xwerktijden.RemoveObject(remove);
                        xwerktijden.AddObject(exform.ExtraTijd);
                        xwerktijden.SelectedObject = exform.ExtraTijd;
                        xwerktijden.SelectedItem?.EnsureVisible();
                    }
                }
                else
                {
                    var tijden = GetWerkTijden();
                    tijden.Remove(remove);
                    var start = xstartdate.Value;
                    var stop = remove.InUse ? remove.Stop : xstopdate.Value;
                    AddTijd(start, stop, tijden, remove.InUse, false,GetRooster(remove));
                    //if (AddTijd(start, stop ,tijden, remove.InUse,false))
                    //    xwerktijden.RemoveObject(remove);
                }

                UpdateUurGewerkt(GetRooster(xwerktijden.SelectedObject as TijdEntry));
                UpdateStatus();
            }
        }

        private void xaddextratime_Click(object sender, EventArgs e)
        {
            var xt = new AddExtraTijdForm();
            if (xt.ShowDialog() == DialogResult.OK)
            {
                var ts = xt.ExtraTijd;
                AddWerkTijd(ts);
                UpdateUurGewerkt(GetRooster(xwerktijden.SelectedObject as TijdEntry));
                UpdateStatus();
            }
        }

        private void EditEntryRooster()
        {
            if (xwerktijden.SelectedObject is TijdEntry entry)
            {
                var path = Klusje?.Path ?? Werklplek?.Path;
                if (path == null) return;
                var currentrooster = entry.WerkRooster;
                var rs = new RoosterForm(currentrooster ?? Manager.Opties.GetWerkRooster(),
                    $"Rooster voor {path}");
                rs.ViewPeriode = false;
                if (rs.ShowDialog() != DialogResult.OK) return;

                entry.WerkRooster = rs.WerkRooster;

                xwerktijden.RefreshObject(entry);
                UpdateUurGewerkt(GetRooster(entry));
                UpdateStatus();
                UpdateFormText();
            }
        }

        private void EditRooster()
        {
            var path = Klusje?.Path ?? Werklplek?.Path;
            if (path == null) return;
            var rooster = GetRooster(null);
            var rs = new RoosterForm(rooster, $"Rooster voor {path}");
            rs.ViewPeriode = false;
            if (rs.ShowDialog() != DialogResult.OK) return;
            rooster = rs.WerkRooster;
            CurrentRooster = rooster;
            var ents = GetWerkTijden();
            var ent = ents.FirstOrDefault(x => x.InUse);
            if (ent != null)
            {
                ent.WerkRooster = rooster;
                var selected = xwerktijden.SelectedObject;
                xwerktijden.SetObjects(ents);
                xwerktijden.SelectedObject = selected;
                xwerktijden.SelectedItem?.EnsureVisible();
                UpdateUurGewerkt(GetRooster(xwerktijden.SelectedObject as TijdEntry));
                UpdateStatus();
                UpdateFormText();
            }
        }

        private void xroosterb_Click(object sender, EventArgs e)
        {
            EditRooster();
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            var valid = false;
            if (xwerktijden.SelectedObject is TijdEntry ent)
            {
                roosterToolStripMenuItem.Enabled = ent.ExtraTijd == null;
                valid = roosterToolStripMenuItem.Enabled;
            }
            else
            {
                roosterToolStripMenuItem.Enabled = false;
            }

            verwijderToolStripMenuItem.Enabled = xwerktijden.SelectedObjects?.Count > 0 &&
                                                 xwerktijden.SelectedObjects.Cast<TijdEntry>().Any(x => !x.InUse);
            valid |= verwijderToolStripMenuItem.Enabled;
            e.Cancel = !valid;
        }

        private void roosterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditEntryRooster();
        }

        private void verwijderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VerwijderSelected();
        }

        private void xwerktijden_ButtonClick(object sender, CellClickEventArgs e)
        {
            EditEntryRooster();
        }

        private void xspeciaalroosterb_Click(object sender, EventArgs e)
        {
            var sproosters = new SpeciaalWerkRoostersForm(SpecialeRoosters);
            if (sproosters.ShowDialog() == DialogResult.OK)
            {
                SpecialeRoosters = sproosters.Roosters;
                UpdateUurGewerkt(GetRooster(null));
                UpdateStatus();
            }
        }

        private void xentryrooster_Click(object sender, EventArgs e)
        {
            EditEntryRooster();
        }
    }
}