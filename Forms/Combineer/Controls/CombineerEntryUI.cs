using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Forms;
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
                if (ParentProductie == null || Productie == null)
                {
                    xgroup.Text = "";
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
                    if (combi != null && (decimal) combi.Activiteit != (decimal)(xactiviteit.Tag??0m))
                    {
                        xactiviteit.SetValue((decimal) combi.Activiteit);
                        xactiviteit.Tag = xactiviteit.Value;
                    }
                    xupdate.Enabled = true;
                    xOnkoppel.Enabled = true;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xupdate_Click(object sender, EventArgs e)
        {
            if (ParentProductie == null || Productie == null) return;
            var total = (decimal) ParentProductie.Combies
                .Where(x => !string.Equals(x.Path, Productie.Path, StringComparison.CurrentCultureIgnoreCase))
                .Sum(x => x.Activiteit);
            var cur =  xactiviteit.Value;
            if ((total + cur) >= 100)
            {
                XMessageBox.Show(
                    "Het is niet mogelijk om de gecombineerde producties meer actief te laten zijn dan de oorspronkelijke productie...\n\n" +
                    $"Het totale activiteit van de gecombineerde producties zijn {(total + cur)}%", "Te Hoog",
                    MessageBoxIcon.Exclamation);
                xactiviteit.SetValue((decimal)xactiviteit.Tag);
                return;
            }
            cur = 100 - cur;
            total = (decimal) Productie.Combies
                .Where(x => !string.Equals(x.Path, ParentProductie.Path, StringComparison.CurrentCultureIgnoreCase))
                .Sum(x => x.Activiteit);
            if ((total + cur) >= 100)
            {
                var extra = (total + cur + 1) - 100;
                XMessageBox.Show($"De gekozen combi heeft al een combinaties van {total}%!\n" +
                                 $"Om deze productie te kunnen combineren, dien je de resterende activiteit van { (100 - cur)}% op te kunnen vangen.\n\n" +
                                 $"Je komt {extra}% te kort om te kunnen combineren.", "Geen Ruimte", MessageBoxIcon.Exclamation);
                xactiviteit.SetValue((decimal)xactiviteit.Tag);
                return;
            }

            
            var parentcombi = ParentProductie.Combies.FirstOrDefault(x =>
                string.Equals(Path.Combine(x.ProductieNr, x.BewerkingNaam), Productie.Path,
                    StringComparison.CurrentCultureIgnoreCase));
            var combi = Productie.Combies.FirstOrDefault(x =>
                string.Equals(Path.Combine(x.ProductieNr, x.BewerkingNaam), ParentProductie.Path,
                    StringComparison.CurrentCultureIgnoreCase));
            if (combi != null && cur != (decimal) combi.Activiteit)
            {

                string msg = $"[GECOMBINEERD][{Productie.ProductieNr}|{Productie.ArtikelNr}]\n" +
                             $"Activiteit aangepast van {combi.Activiteit}% naar {cur}%";
                combi.Activiteit = (double) cur;
                // ParentProductie.Activiteit = (double)xparentactiviteit;
                _ = Productie.UpdateBewerking(null, msg);
            }
            var xparentactiviteit = xactiviteit.Value;
            if (parentcombi != null && xparentactiviteit != (decimal)parentcombi.Activiteit)
            {
                

                string msg = $"[{ParentProductie.ProductieNr}|{ParentProductie.ArtikelNr}]\n" +
                             $"Activiteit aangepast van {parentcombi.Activiteit}% naar {xparentactiviteit}%";
                parentcombi.Activiteit = (double)xparentactiviteit;
                //Productie.Activiteit = (double)cur;
                _ = ParentProductie.UpdateBewerking(null, msg);
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
    }
}
