using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Rpm.Productie;

namespace Forms
{
    public partial class VrijeTijdForm : Forms.MetroBase.MetroBaseForm
    {
        public VrijeTijdForm()
        {
            InitializeComponent();
            VrijeTijd = new UrenLijst();
            ((OLVColumn) xvrijetijdlist.Columns[0]).ImageGetter = sender => 0;
            ((OLVColumn) xvrijetijdlist.Columns[2]).AspectGetter = VrijTijdGetter;
        }

        public VrijeTijdForm(Personeel pers) : this()
        {
            Persoon = pers;
        }

        public UrenLijst VrijeTijd { get; }
        public Personeel Persoon { get; set; }

        private object VrijTijdGetter(object item)
        {
            if (item is TijdEntry entry)
            {
                var ex = VrijeDagen(entry);
                return Math.Round(
                    Werktijd.TijdGewerkt(entry, Persoon.WerkRooster ?? Manager.Opties.GetWerkRooster(),null, ex).TotalHours, 2);
            }

            return 0;
        }

        public new DialogResult ShowDialog(IWin32Window owner)
        {
            Init();
            return base.ShowDialog(owner);
        }

        private void Init()
        {
            var rooster = Persoon?.WerkRooster ?? Manager.Opties.GetWerkRooster();
            xstartvrij.Value = DateTime.Now.Add(rooster.StartWerkdag);
            xeindvrij.Value = DateTime.Now.Add(rooster.EindWerkdag);
            if (Persoon != null)
            {
                Text = $"Beheer {Persoon.PersoneelNaam} Vrije Uren";
                var entries = new List<TijdEntry>();
                if (Persoon.VrijeDagen != null)
                    entries = Persoon.VrijeDagen.Uren;
                xvrijetijdlist.SetObjects(entries);
            }

            UpdateStatus();
        }

        private void SetEnable()
        {
            xverwijdervrij.Enabled = xvrijetijdlist.SelectedObjects.Count > 0;
        }

        private void UpdateStatus()
        {
            //update status totaal uur vrij
            double vrij = 0;
            foreach (var model in xvrijetijdlist.Objects.Cast<TijdEntry>()) vrij += model.TotaalTijd;
            xstatus.Text = $"Totaal vrij: {Math.Round(vrij, 2)} uur";
        }

        private void xstartvrij_ValueChanged(object sender, EventArgs e)
        {
            UpdateUurVrij();
        }

        public double UurVrij()
        {
            var rooster = Persoon.WerkRooster ?? Manager.Opties.GetWerkRooster();
            return Math.Round(
                Werktijd.TijdGewerkt(new TijdEntry(xstartvrij.Value, xeindvrij.Value, rooster),rooster
                    ,null, VrijeDagen()).TotalHours, 2);
        }

        public List<TijdEntry> GetVrijeDagen()
        {
            return GetVrijeDagen(null);
        }

        public Dictionary<DateTime, DateTime> VrijeDagen()
        {
            return VrijeDagen(null);
        }

        public Dictionary<DateTime, DateTime> VrijeDagen(TijdEntry skip)
        {
            var xreturn = new Dictionary<DateTime, DateTime>();
            if (xvrijetijdlist.Items.Count > 0)
                foreach (var model in xvrijetijdlist.Objects.Cast<TijdEntry>())
                    if (skip == null || skip.Start != model.Start && skip.Stop != model.Stop)
                        xreturn[model.Start] = model.Stop;
            return xreturn;
        }

        public List<TijdEntry> GetVrijeDagen(TijdEntry skip)
        {
            var xreturn = new List<TijdEntry>();
            if (xvrijetijdlist.Items.Count > 0)
                foreach (var model in xvrijetijdlist.Objects.Cast<TijdEntry>())
                    if (skip == null || skip.Start != model.Start && skip.Stop != model.Stop)
                        xreturn.Add(new TijdEntry(model.Start, model.Stop, model.WerkRooster));
            return xreturn;
        }

        private void UpdateUurVrij()
        {
            xuurvrij.Text = UurVrij() + " uur";
        }

        private void xeindvrij_ValueChanged(object sender, EventArgs e)
        {
            UpdateUurVrij();
        }

        private void xvoegvrijtoe_Click(object sender, EventArgs e)
        {
            var x = UurVrij();
            if (x <= 0)
            {
                XMessageBox.Show(this, $"Ongeldige aanvraag!\n" +
                                 "Je totale uren kunnen niet '0' zijn.", "Ongeldig", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
            else
            {
                var start = xstartvrij.Value;
                var stop = xeindvrij.Value;
                var changed = false;
                var rooster = Persoon?.WerkRooster;
                var xent = new TijdEntry(start, stop, rooster);
                foreach (var item in xvrijetijdlist.Objects.Cast<TijdEntry>())
                    if (item.Equals(xent))
                    {
                        item.Start = start;
                        item.Stop = stop;
                        changed = true;
                        xvrijetijdlist.RefreshObject(item);
                        break;
                    }

                if (!changed)
                    xvrijetijdlist.AddObject(xent);
                UpdateUurVrij();
                UpdateStatus();
            }
        }

        private void xverwijdervrij_Click(object sender, EventArgs e)
        {
            if (xvrijetijdlist.SelectedObjects.Count > 0)
                if (XMessageBox.Show(this, $"Weetje zeker dat je alle geselecteerde vrije uren wilt verwijderen?"
                    , "Ongeldig", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    xvrijetijdlist.RemoveObjects(xvrijetijdlist.SelectedObjects);
                    //var vrij = new Dictionary<DateTime, DateTime>();
                    //var models = new List<TijdEntry>();
                    //foreach (var model in xvrijetijdlist.Objects.Cast<TijdEntry>())
                    //{
                    //    model.VrijeTijd = new Dictionary<DateTime, DateTime>();
                    //    foreach (var x in vrij)
                    //        model.VrijeTijd.Add(x.Key, x.Value);
                    //    xvrijetijdlist.RefreshObject(model);
                    //    vrij.Add(model.Start, model.Stop);
                    //}

                    UpdateUurVrij();
                    UpdateStatus();
                }
        }

        private void xok_Click(object sender, EventArgs e)
        {
            VrijeTijd.SetUren(GetVrijeDagen().ToArray(), false,true);
            DialogResult = DialogResult.OK;
        }

        private void xannuleer_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xvrijetijdlist_SelectedIndexChanged(object sender, EventArgs e)
        {
            SetEnable();
        }

        private void VrijeTijdForm_Shown(object sender, EventArgs e)
        {
            Manager.OnPersoneelDeleted += Manager_OnPersoneelDeleted;
            Manager.OnPersoneelChanged += Manager_OnPersoneelChanged;
        }

        private void Manager_OnPersoneelChanged(object sender, Personeel user)
        {
            if (Persoon == null || this.IsDisposed) return;
            if (!string.Equals(Persoon.PersoneelNaam, user.PersoneelNaam, StringComparison.CurrentCultureIgnoreCase)) return;
            Persoon = user;
        }

        private void Manager_OnPersoneelDeleted(object sender, string id)
        {
            if (Persoon == null || this.IsDisposed) return;
            if (!string.Equals(Persoon.PersoneelNaam, id, StringComparison.CurrentCultureIgnoreCase)) return;
            this.BeginInvoke(new MethodInvoker(this.Close));
        }

        private void VrijeTijdForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnPersoneelDeleted -= Manager_OnPersoneelDeleted;
            Manager.OnPersoneelChanged -= Manager_OnPersoneelChanged;
        }
    }
}