using BrightIdeasSoftware;
using Forms;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Forms
{
    public partial class DeelsGereedMeldingenForm : Forms.MetroBase.MetroBaseForm
    {
        public Bewerking Bewerking { get; private set; }
        public List<DeelsGereedMelding> DeelMeldingen { get; private set; }
        public DeelsGereedMeldingenForm(Bewerking bew)
        {
            InitializeComponent();
            Bewerking = bew.CreateCopy();
            this.Text = $"Deels gereedmeldingen voor {bew.Path} {bew.Omschrijving}";
            DeelMeldingen = Bewerking.DeelGereedMeldingen.CreateCopy();
            ((OLVColumn) xgereedlijst.Columns[0]).ImageGetter = (item) => 0;
            InitFields();
        }

        private void InitFields()
        {
            xgereedlijst.SetObjects(DeelMeldingen);
            UpdateStatus();
        }

        private void UpdateStatus()
        {
            var xitems = xgereedlijst.Objects.Cast<DeelsGereedMelding>().ToList();
            int gemaakt = xitems.Sum(x => x.Aantal);
            string xvar = gemaakt == 1 ? "stuk" : "stuks";
            xstatuslabel.Text = $"Totaal {gemaakt} {xvar} gereed gemeld.";
        }

        private void xokb_Click(object sender, EventArgs e)
        {
            UpdateBewerking();
            DialogResult = DialogResult.OK;
        }

        private void xcancelb_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            var prodnr = Bewerking?.ProductieNr;
            if (changedform == null || !string.Equals(changedform.ProductieNr, prodnr)) return;
            if (Bewerking != null)
            {
                var xbew = changedform.Bewerkingen.FirstOrDefault(x => x.Equals(Bewerking));
                if (xbew != null)
                    Bewerking = xbew;
            }
        }

        private void DeelsGereedMeldingenForm_Shown(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
        }

        private void DeelsGereedMeldingenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
        }

        private async void xadd_Click(object sender, EventArgs e)
        {
            if (Bewerking == null) return;
            var wps = Bewerking.WerkPlekken.Select(x=> x.Naam).ToList();
            string wp = null;
            if (wps.Count > 1)
            {
                wps.Add("Alle Werkplekken");
                WerkPlekChooser wpChooser = new WerkPlekChooser(wps.ToArray(),null);
                if (wpChooser.ShowDialog(this) == DialogResult.Cancel) return;
                wp = wpChooser.SelectedName?.ToLower() == "alle werkplekken" ? null : wpChooser.SelectedName;
            }
            else wp = wps.FirstOrDefault();

            DeelsGereedForm gereedform = new DeelsGereedForm(Bewerking);
            gereedform.Text = $"Nieuwe deels gereedmelding voor {Bewerking.Path}";
            if (wp != null)
                gereedform.Text += $"\\{wp}";
            if (gereedform.ShowDialog(this) == DialogResult.OK)
            {
                var xgereed = gereedform.GereedMelding;
                xgereed.WerkPlek = wp;
                UpdateBewerking();
                await Bewerking.MeldDeelsGereed(xgereed, false);
                DeelMeldingen = Bewerking.DeelGereedMeldingen.CreateCopy();
                InitFields();
            }
        }

        private void UpdateBewerking()
        {
            try
            {
                var newitems = xgereedlijst.Objects.Cast<DeelsGereedMelding>().ToList();
                var olditems = DeelMeldingen.Where(x => !newitems.Any(t => t.Equals(x))).ToList();
                var wps = Bewerking.WerkPlekken;
                foreach (var old in olditems)
                {
                    var wp = wps?.FirstOrDefault(x =>
                        string.Equals(x.Naam, old.WerkPlek, StringComparison.CurrentCultureIgnoreCase));
                    if (wp != null) wp.AantalGemaakt += old.Aantal;
                }

                foreach (var newitem in newitems)
                {
                    var xitem = Bewerking.DeelGereedMeldingen.FirstOrDefault(x => x.Equals(newitem));
                    if (xitem == null) continue;
                    if (xitem.Aantal != newitem.Aantal)
                    {
                        var wp = wps?.FirstOrDefault(x =>
                            string.Equals(x.Naam, newitem.WerkPlek, StringComparison.CurrentCultureIgnoreCase));
                        if (wp == null) continue;
                        if (newitem.Aantal > xitem.Aantal)
                        {
                            var xdif = newitem.Aantal - xitem.Aantal;
                            wp.AantalGemaakt -= xdif;
                        }
                        else
                        {
                            var xdif = xitem.Aantal - newitem.Aantal;
                            wp.AantalGemaakt += xdif;
                        }
                    }
                }
                Bewerking.DeelGereedMeldingen = newitems.CreateCopy();
            }
            catch (Exception e)
            {
            }
        }

        private void xedit_Click(object sender, EventArgs e)
        {
            if (xgereedlijst.SelectedObject is DeelsGereedMelding melding)
            {
                DeelsGereedForm gereedform = new DeelsGereedForm(melding);
                gereedform.Text = $"Wijzig deels gereedmelding voor {Bewerking.Path}";
                if (melding.WerkPlek != null)
                    gereedform.Text += $"\\{melding.WerkPlek}";
                if (gereedform.ShowDialog(this) == DialogResult.OK)
                {
                    xgereedlijst.RefreshObject(gereedform.GereedMelding);
                    UpdateStatus();
                }
            }
        }

        private void xdelete_Click(object sender, EventArgs e)
        {
            if (xgereedlijst.SelectedObjects.Count > 0)
            {
                if (XMessageBox.Show(this, $"Weetje zeker dat je alle geselecteerde gereed meldingen wilt verwijderen?\n\n" +
                                     "Er bestaat de risico dat je de tell kwijt raakt!", "Waarschuwing",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    xgereedlijst.RemoveObjects(xgereedlijst.SelectedObjects);
                }
            }
        }

        private void xgereedlijst_SelectedIndexChanged(object sender, EventArgs e)
        {
            xdelete.Enabled = xgereedlijst.SelectedObjects.Count > 0;
            xedit.Enabled = xgereedlijst.SelectedObjects.Count == 1;
        }
    }
}
