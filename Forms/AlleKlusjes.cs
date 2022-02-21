using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Forms.MetroBase;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;

namespace Forms
{
    public partial class AlleKlusjes : MetroBaseForm
    {
        public readonly List<StartProductie> _formuis = new();

        public AlleKlusjes(Personeel persoon)
        {
            InitializeComponent();
            Persoon = persoon;
            InitImageList();
            ((OLVColumn) xklusjes.Columns[0]).ImageGetter = TaskImageGet;
            ((OLVColumn) xklusjes.Columns[5]).AspectGetter = TaskGewerktTijdGet;
        }

        public Personeel Persoon { get; private set; }

        private void InitImageList()
        {
            //Klus afbeeldingen
            xklusimages.Images.Add(
                Resources.taskboardlinear_106203.CombineImage(Resources.new_25355, 2.25)); //new document 0
            xklusimages.Images.Add(
                Resources.taskboardlinear_106203.CombineImage(Resources.Warning_36828, 2.25)); //warning document 1
            xklusimages.Images.Add(
                Resources.taskboardlinear_106203.CombineImage(Resources.play_button_icon_icons_com_60615,
                    2.25)); //play document 2
            xklusimages.Images.Add(Resources.taskboardlinear_106203); // regular document 3
            xklusimages.Images.Add(
                Resources.taskboardlinear_106203.CombineImage(Resources.delete_1577, 2.25)); //deleted document 4
            xklusimages.Images.Add(
                Resources.taskboardlinear_106203.CombineImage(Resources.check_1582, 2.25)); // checked document 5
        }

        private object TaskImageGet(object item)
        {
            if (item is Klus klus)
                switch (klus.Status)
                {
                    case ProductieState.Gestopt:
                        return klus.IsNieuw ? 0 : 3;
                    case ProductieState.Gestart:
                        return 2;
                    case ProductieState.Gereed:
                        return 5;
                    case ProductieState.Verwijderd:
                        return 4;
                    default:
                        return 3;
                }

            return 3;
        }

        private object TaskGewerktTijdGet(object item)
        {
            if (item is Klus klus) return GetKlusTijdGewerkt(klus) + " uur";

            return 0 + " uur";
        }

        private void AlleKlusjes_Load(object sender, EventArgs e)
        {
            InitPersoon();
        }

        private void InitPersoon()
        {
            var selected = xklusjes.SelectedObject;
            var filter = xsearchbox.Text.Trim().ToLower() != "zoeken..." ? xsearchbox.Text.Trim().ToLower() : null;
            if (filter == null)
                xklusjes.SetObjects(Persoon.Klusjes);
            else
                xklusjes.SetObjects(Persoon.Klusjes.Where(x =>
                    x.ArtikelNr != null && x.ArtikelNr.ToLower().Contains(filter) ||
                    x.Omschrijving != null && x.Omschrijving.ToLower().Contains(filter) ||
                    x.Path.ToLower().Contains(filter)));
            if (selected != null)
                xklusjes.SelectedObject = selected;
            xklusjes.SelectedItem?.EnsureVisible();
            UpdateHeader();
            UpdateStatus();
        }

        private void xsearch_Enter(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb is {Text: "Zoeken..."}) tb.Text = "";
        }

        private void xsearch_Leave(object sender, EventArgs e)
        {
            var tb = sender as TextBox;
            if (tb != null)
                if (string.IsNullOrWhiteSpace(tb.Text))
                    tb.Text = "Zoeken...";
        }

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            if (xsearchbox.Text.Trim().ToLower() != "zoeken...")
                InitPersoon();
        }

        private void UpdateHeader()
        {
            if (Persoon.Klusjes.Count > 0)
            {
                var xvalue = Persoon.Klusjes.Count == 1 ? "klus" : "klusjes";
                Text =
                    $"{Persoon.PersoneelNaam} heeft {Persoon.Klusjes.Count} {xvalue} van {Persoon.TotaalTijdGewerkt} uur.";
            }
            else
            {
                Text = $"{Persoon.PersoneelNaam} heeft nog geen klusjes.";
            }
        }

        private void UpdateStatus()
        {
            if (xklusjes.SelectedObjects.Count > 0)
            {
                var selected = xklusjes.SelectedObjects.Cast<Klus>().ToArray();
                if (selected.Length == 1)
                    xstatuslabel.Text =
                        $"{selected[0].Naam} geselecteerd met {GetKlusTijdGewerkt(selected[0])} uur gewerkt op {selected[0].WerkPlek}.";
                else
                    xstatuslabel.Text =
                        $"{selected.Length} klusjes geselecteerd van totaal {GetKlusjesTijdGewerkt(selected)} uur.";
            }
            else
            {
                if (xklusjes.Objects != null)
                {
                    var items = xklusjes.Objects.Cast<Klus>().ToArray();
                    if (items.Length == 1)
                        xstatuslabel.Text =
                            $"{items[0].Naam} met {GetKlusTijdGewerkt(items[0])} uur gewerkt op {items[0].WerkPlek}.";
                    else
                        xstatuslabel.Text =
                            $"{items.Length} klusjes van totaal {GetKlusjesTijdGewerkt(items)} uur.";
                }
                else
                {
                    xstatuslabel.Text = "Geen Klusjes.";
                }
            }
        }


        private double GetKlusTijdGewerkt(Klus klus)
        {
            if (Persoon == null || klus == null || !string.Equals(klus.PersoneelNaam, Persoon.PersoneelNaam,
                    StringComparison.OrdinalIgnoreCase))
                return 0;
            return Math.Round(klus.TijdGewerkt(Persoon.VrijeDagen.ToDictionary(), klus.Tijden?.WerkRooster).TotalHours,
                2);
        }

        private double GetKlusjesTijdGewerkt(Klus[] klusjes)
        {
            if (Persoon == null || klusjes == null)
                return 0;
            var xklusjes = klusjes.Where(x =>
                string.Equals(x.PersoneelNaam, Persoon.PersoneelNaam, StringComparison.OrdinalIgnoreCase)).ToArray();
            if (xklusjes.Length == 0)
                return 0;
            return Math.Round(
                xklusjes.Sum(x => x.TijdGewerkt(Persoon.VrijeDagen.ToDictionary(), x.Tijden?.WerkRooster).TotalHours),
                2);
        }

        private bool EnableKlusButtons()
        {
            xwijzigklus.Visible = xklusjes.SelectedObjects.Count == 1;
            xverwijderklusjes.Visible = xklusjes.SelectedObjects.Count > 0;
            xopenproductie.Visible = xklusjes.SelectedObjects.Count > 0;
            return xklusjes.SelectedObjects.Count > 0;
        }

        private Klus GetCurrentklus()
        {
            if (xklusjes.SelectedObjects.Count == 0)
                return null;
            return xklusjes.SelectedObjects.Cast<Klus>().FirstOrDefault();
        }

        private void xklusjes_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateStatus();
            EnableKlusButtons();
        }

        private void AlleKlusjes_Shown(object sender, EventArgs e)
        {
            //Manager.OnLoginChanged += Manager_OnLoginChanged;
            Manager.OnPersoneelChanged += Manager_OnPersoneelChanged;
            Manager.OnBewerkingChanged += Manager_OnBewerkingChanged;
        }

        private void AlleKlusjes_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Manager.OnLoginChanged -= Manager_OnLoginChanged;
            Manager.OnPersoneelChanged -= Manager_OnPersoneelChanged;
            Manager.OnBewerkingChanged -= Manager_OnBewerkingChanged;
        }

        private void Manager_OnBewerkingChanged(object sender, Bewerking bewerking, string change,
            bool shownotification)
        {
            if (IsDisposed) return;
            var pers = bewerking?.GetPersoneel()
                .Where(x => string.Equals(x.PersoneelNaam, Persoon.PersoneelNaam,
                    StringComparison.CurrentCultureIgnoreCase)).ToArray();
            if (pers?.Length > 0)
            {
                var update = 0;
                foreach (var per in pers) update += Persoon.UpdateFrom(per, false);

                if (update > 0)
                    if (InvokeRequired)
                        Invoke(new Action(InitPersoon));
                    else
                        InitPersoon();
            }
        }

        private void Manager_OnPersoneelChanged(object sender, Personeel user)
        {
            if (IsDisposed) return;
            if (InvokeRequired)
            {
                Invoke(new Action(() =>
                {
                    if (user?.PersoneelNaam.ToLower() == Persoon.PersoneelNaam.ToLower())
                    {
                        Persoon = user;
                        InitPersoon();
                    }
                }));
            }
            else
            {
                if (user?.PersoneelNaam.ToLower() == Persoon.PersoneelNaam.ToLower())
                {
                    Persoon = user;
                    InitPersoon();
                }
            }
        }

        private void Manager_OnLoginChanged(UserAccount user, object instance)
        {
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void xwijzigklus_Click(object sender, EventArgs e)
        {
            var klus = GetCurrentklus();
            if (klus != null)
                try
                {
                    var xedit = new NieuwKlusForm(Persoon, klus, true);
                    if (xedit.ShowDialog() == DialogResult.OK)
                        //xedit.Formulier?.UpdateForm(true, false, $"{klus.Naam} aangepast", true);
                        xklusjes.RefreshObject(klus);
                }
                catch (Exception ex)
                {
                    XMessageBox.Show(this, ex.Message, "Fout", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
        }

        private void xnewklus_Click(object sender, EventArgs e)
        {
        }

        private void xverwijderklusjes_Click(object sender, EventArgs e)
        {
            if (xklusjes.SelectedObjects.Count > 0)
                if (XMessageBox.Show(this, "Weetje zeker dat je alle geselecteerde klusjes wilt verwijderen?\n\n" +
                                           "LET OP:\n" +
                                           "Klusjes verwijderen zal ook je gewerkte tijden weghalen en de producties beinvloeden!\n" +
                                           "Wil je toch doorgaan?", "Verwijderen", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    var count = 0;
                    foreach (var klus in xklusjes.SelectedObjects.Cast<Klus>())
                    {
                        klus.RemoveWerk();
                        Persoon.Klusjes.Remove(klus);
                        xklusjes.RemoveObject(klus);
                        count++;
                    }

                    var xvalue = count == 1 ? "klus" : "klusjes";
                    Manager.Database.UpSert(Persoon, $"[{Persoon.PersoneelNaam}]{count} {xvalue} verwijderd");
                }
        }

        private void xopenproductie_Click(object sender, EventArgs e)
        {
            if (xklusjes.SelectedObjects.Count > 0)
            {
                var invalid = "";
                foreach (var k in xklusjes.SelectedObjects.Cast<Klus>())
                {
                    var werk = k.GetWerk();
                    if (!werk.IsValid)
                    {
                        invalid += $"Bestaat niet: {k.Path}\n";
                        continue;
                    }

                    if (werk.Formulier.State == ProductieState.Verwijderd)
                    {
                        invalid += $"Verwijderd: {k.Path}\n";
                        continue;
                    }

                    Manager.FormulierActie(new object[] {werk.Formulier, werk.Bewerking}, MainAktie.OpenProductie);
                    //ShowProductieForm(werk.Formulier, werk.Bewerking);
                }

                if (!string.IsNullOrEmpty(invalid))
                    XMessageBox.Show(this, "De volgende klus(jes) zijn niet meer geldig!\n" +
                                           $"{invalid.TrimEnd(',', ' ')}", "Ongeldig", MessageBoxIcon.Warning);
            }
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {
            openProductieToolStripMenuItem.Enabled = xklusjes.SelectedObjects.Count > 0;
            wijzigToolStripMenuItem.Enabled = xklusjes.SelectedObjects.Count == 1;
            verwijderToolStripMenuItem.Enabled = xklusjes.SelectedObjects.Count > 0;
            e.Cancel = xklusjes.SelectedObjects.Count == 0;
        }

        private void UpdateFormulier(ProductieFormulier form)
        {
            if (IsDisposed) return;
            if (form != null)
                try
                {
                    var prodform = _formuis?.FirstOrDefault(x => !x.IsDisposed && x.Formulier.Equals(form));
                    prodform?.UpdateFields(form, null);
                }
                catch (ObjectDisposedException)
                {
                    Console.WriteLine(@"Disposed!");
                }
        }
    }
}