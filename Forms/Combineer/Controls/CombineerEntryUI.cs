using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Forms;
using Forms.Combineer;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Controls
{
    public partial class CombineerEntryUI : UserControl
    {
        public Bewerking ParentProductie { get; private set; }
        public Bewerking Productie { get; private set; }
        public CombineerEntryUI()
        {
            InitializeComponent();
        }

        public void LoadBewerking(Bewerking parent, Bewerking bewerking)
        {
            try
            {
                ParentProductie = parent;
                Productie = bewerking;
                UpdateFields();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateFields()
        {
            if (ParentProductie == null || Productie == null)
            {
                xgroup.Text = "";
                xCombiPeriode.Enabled = false;
                xOnkoppel.Enabled = false;
                xupdate.Enabled = false;
            }
            else
            {
                xgroup.Text =
                    $"Productie '{ParentProductie.ProductieNr} {ParentProductie.Naam}' is gekoppeld aan '{Productie.ProductieNr} {Productie.Naam}'";
                var combi = ParentProductie.Combies.FirstOrDefault(x =>
                    string.Equals(x.Path, Productie.Path,
                        StringComparison.CurrentCultureIgnoreCase));
                if (combi != null && (decimal)combi.Activiteit != (decimal)(xactiviteit.Tag ?? 0m))
                {
                    xactiviteit.SetValue((decimal)combi.Activiteit);
                    xactiviteit.Tag = xactiviteit.Value;
                }

                var ximg = Resources.systemtime_778_32_32;
                if (combi != null)
                {
                    if (combi.IsRunning)
                        ximg = ximg.CombineImage(Resources.play_button_icon_icons_com_60615, ContentAlignment.BottomLeft, 1.5);
                    else ximg = ximg.CombineImage(Resources.check_1582, ContentAlignment.BottomLeft, 1.5);
                }

                xCombiPeriode.Image = ximg;
                xupdate.Enabled = true;
                xOnkoppel.Enabled = true;
            }
        }


        private bool DoCheck(ref decimal current)
        {
            if (ParentProductie == null || Productie == null) return false;
            var total = (decimal)ParentProductie.Combies
                .Where(x => !string.Equals(x.Path, Productie.Path, StringComparison.CurrentCultureIgnoreCase) && x.IsRunning)
                .Sum(x => x.Activiteit);
            var cur = xactiviteit.Value;
            if ((total + cur) >= 100)
            {
                XMessageBox.Show(
                    "Het is niet mogelijk om de gecombineerde producties meer actief te laten zijn dan de oorspronkelijke productie...\n\n" +
                    $"Het totale activiteit van de gecombineerde producties zijn {(total + cur)}%", "Te Hoog",
                    MessageBoxIcon.Exclamation);
                xactiviteit.SetValue((decimal)xactiviteit.Tag);
                return false;
            }
            cur = 100 - cur;
            total = (decimal)Productie.Combies
                .Where(x => !string.Equals(x.Path, ParentProductie.Path, StringComparison.CurrentCultureIgnoreCase) && x.IsRunning)
                .Sum(x => x.Activiteit);
            if ((total + cur) >= 100)
            {
                var extra = (total + cur + 1) - 100;
                XMessageBox.Show($"De gekozen combi heeft al een combinaties van {total}%!\n" +
                                 $"Om deze productie te kunnen combineren, dien je de resterende activiteit van { (100 - cur)}% op te kunnen vangen.\n\n" +
                                 $"Je komt {extra}% te kort om te kunnen combineren.", "Geen Ruimte", MessageBoxIcon.Exclamation);
                xactiviteit.SetValue((decimal)xactiviteit.Tag);
                return false;
            }

            return true;
        }

        private void xupdate_Click(object sender, EventArgs e)
        {

            var cur = xactiviteit.Value;
            if (!DoCheck(ref cur)) return;
            
            var parentcombi = ParentProductie.Combies.FirstOrDefault(x =>
                string.Equals(Path.Combine(x.ProductieNr, x.BewerkingNaam), Productie.Path,
                    StringComparison.CurrentCultureIgnoreCase));
            var combi = Productie.Combies.FirstOrDefault(x =>
                string.Equals(Path.Combine(x.ProductieNr, x.BewerkingNaam), ParentProductie.Path,
                    StringComparison.CurrentCultureIgnoreCase));
            if (parentcombi != null && cur != (decimal)parentcombi.Activiteit)
            {

                string msg = $"[{ParentProductie.ProductieNr}|{ParentProductie.ArtikelNr}]\n" +
                             $"Activiteit aangepast van {parentcombi.Activiteit}% naar {cur}%";
                parentcombi.Activiteit = (double) cur;
                // ParentProductie.Activiteit = (double)xparentactiviteit;
                _ = ParentProductie.UpdateBewerking(null, msg);
            }
            var xparentactiviteit = 100 - xactiviteit.Value;
            if (combi != null && xparentactiviteit != (decimal)combi.Activiteit)
            {
                

                string msg = $"[GECOMBINEERD][{Productie.ProductieNr}|{Productie.ArtikelNr}]\n" +
                             $"Activiteit aangepast van {combi.Activiteit}% naar {xparentactiviteit}%";
                combi.Activiteit = (double)xparentactiviteit;
                //Productie.Activiteit = (double)cur;
                _ = Productie.UpdateBewerking(null, msg);
            }
        }

        private void xOpenProductie_Click(object sender, EventArgs e)
        {
            if (Productie == null) return;
            Manager.FormulierActie(new object[] {Productie.Parent, Productie}, MainAktie.OpenProductie);
        }

        private void xOnkoppel_Click(object sender, EventArgs e)
        {
            if (ParentProductie == null || Productie == null) return;
            if (XMessageBox.Show(
                    $"Weetje zeker dat je de combinatie van '{Productie.Omschrijving}'({Productie.ProductieNr}) wilt ontkoppelen?",
                    "Combinatie Ontkoppelen", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) ==
                DialogResult.No) return;
            if (ParentProductie.Combies.RemoveAll(x =>
                    string.Equals(x.ProductieNr, Productie.ProductieNr, StringComparison.CurrentCultureIgnoreCase)) > 0)
            {
                string msg = $"[{Productie.ProductieNr}|{Productie.ArtikelNr}] {Productie.Naam}\n" +
                             $"Gecombineerde Productie is niet meer gekoppeld!";
                _ = ParentProductie.UpdateBewerking(null, msg);
            }

            if (Productie.Combies.RemoveAll(x =>
                    string.Equals(x.ProductieNr, ParentProductie.ProductieNr, StringComparison.CurrentCultureIgnoreCase)) > 0)
            {
                string msg = $"[{ParentProductie.ProductieNr}|{ParentProductie.ArtikelNr}] {ParentProductie.Omschrijving}\n" +
                             $"Productie is onkoppeld!";
                _ = Productie.UpdateBewerking(null, msg);
            }
        }

        private void xCombiPeriode_Click(object sender, EventArgs e)
        {
            var parentcombi = ParentProductie?.Combies.FirstOrDefault(x =>
                string.Equals(Path.Combine(x.ProductieNr, x.BewerkingNaam), Productie.Path,
                    StringComparison.CurrentCultureIgnoreCase));
            if (parentcombi != null)
            {
                var period = parentcombi.Periode.CreateCopy();
                var xdt = new CombineerPeriodeForm(period.CreateCopy());
                if (xdt.ShowDialog() == DialogResult.OK)
                {
                    var xindex = ParentProductie.Combies.IndexOf(parentcombi);
                    if (xindex > -1)
                    {
                        ParentProductie.Combies[xindex].Periode = xdt.SelectedPeriode;
                        var cur = xactiviteit.Value;
                        if (xdt.SelectedPeriode.Stop.IsDefault() && !DoCheck(ref cur))
                        {
                            ParentProductie.Combies[xindex].Periode = period;
                            return;
                        }

                        UpdateFields();
                        ParentProductie.UpdateBewerking(null,
                            $"[{parentcombi.ProductieNr}] {parentcombi.BewerkingNaam}\n" +
                            $"Periode Aangepast!");
                    }
                }
            }
        }
    }
}
