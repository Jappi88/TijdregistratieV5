using Forms;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forms.Combineer;
using Rpm.Misc;

namespace Controls
{
    public partial class CombineerUI : UserControl
    {
        public Bewerking Productie { get; private set; }

        public CombineerUI()
        {
            InitializeComponent();
        }

        public void UpdateBewerking(Bewerking bewerking)
        {
            if (this.Disposing || this.IsDisposed || !this.Visible || bewerking == null) return;
            this.BeginInvoke(new Action(() =>
            {
                try
                {
                    Productie = bewerking;
                    var xcurrentitems = xcontainer.Controls.Count > 0
                        ? xcontainer.Controls.Cast<CombineerEntryUI>().ToList()
                        : new List<CombineerEntryUI>();
                    var xremove = xcurrentitems.Where(x => !bewerking.Combies.Any(b =>
                            string.Equals(x.Productie.ProductieNr + $"\\{x.Productie.Naam}",
                                b.ProductieNr + $"\\{b.BewerkingNaam}", StringComparison.CurrentCultureIgnoreCase)))
                        .ToList();
                    foreach (var item in xremove)
                    {
                        xcurrentitems.Remove(item);
                        xcontainer.Controls.Remove(item);
                    }

                    xaddproductie.Enabled = bewerking.Activiteit > 1;
                    foreach (var combi in bewerking.Combies)
                    {
                        var prod = Werk.FromPath(combi.Path);
                        if (prod?.Bewerking == null)
                        {
                            continue;
                        }

                        var xold = xcurrentitems.FirstOrDefault(x =>
                            string.Equals(x.Productie.ProductieNr + $"\\{x.Productie.Naam}",
                                combi.ProductieNr + $"\\{combi.BewerkingNaam}",
                                StringComparison.CurrentCultureIgnoreCase));
                        if (xold == null)
                        {
                            xold = new CombineerEntryUI();
                            xold.Tag = combi;
                            xold.Dock = DockStyle.Top;
                            xold.LoadBewerking(bewerking, prod.Bewerking);
                            xcontainer.Controls.Add(xold);
                            xold.SendToBack();
                        }
                        else
                        {
                            xold.LoadBewerking(bewerking, prod.Bewerking);
                        }
                    }

                    InitFields();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }));
        }

        private void InitFields()
        {
            var msg = "";
            if (Productie != null)
            {
                var combs = Productie.Combies.Where(x => x.IsRunning).ToList();
                string xcount = combs.Count == 0 ? "geen" : combs.Count.ToString();
                string x0 = combs.Count == 1 ? "is" : "zijn";
                string x1 = combs.Count == 1 ? "combinatie" : "combinaties";
                string x2 = combs.Count == 0
                    ? ""
                    : $"met totaal {combs.Sum(x => x.Activiteit)}% aan activiteit.";

                string title = combs.Count > 0
                    ? $"Er {x0} {xcount} actieve {x1} {x2}"
                    : $"Combineer producties met {Productie.Naam} van {Productie.Omschrijving}";

                msg = $"<span color='darkred'><b>{title}</b><br>" +
                      $"Gecombineerde producties zullen automatisch starten,stoppen en onderbroken/hervat worden.<br>" +
                      $"<b>Huidige productie draait op {Productie.Activiteit}%</b></span>";
            }

            xomschrijving.Text = msg;
        }

        private bool IsAllowedCombi(object item, string filter, bool tempfilter = false )
        {
            if (Productie == null) return false;
            if(item is Bewerking bewerking)
            {
                if (bewerking.State is ProductieState.Gereed or ProductieState.Verwijderd)
                    return false;
                var wps = Manager.BewerkingenLijst.GetWerkplekken(bewerking.Naam);
                var xwps = Manager.BewerkingenLijst.GetWerkplekken(Productie.Naam);
                if (!wps.Any(x => xwps.Any(w => string.Equals(x, w, StringComparison.CurrentCultureIgnoreCase))))
                    return false;
                if (bewerking.Combies.Any(x => string.Equals(x.Path, Productie.Path, StringComparison.CurrentCultureIgnoreCase)))
                {
                    return false;
                }
                return bewerking.IsAllowed(filter, tempfilter);
            }
            return false;
        }

        private void xaddproductie_Click(object sender, EventArgs e)
        {
            try
            {
                if (Manager.Database == null || Manager.Database.IsDisposed || Productie == null) return;
                var bws = Manager.Database.xGetAllBewerkingen(false, true, true);
                var xbwselector = new BewerkingSelectorForm(bws);
                xbwselector.IsValidHandler = IsAllowedCombi;
                if (xbwselector.ShowDialog(this) == DialogResult.OK)
                {
                    var selected = xbwselector.SelectedBewerkingen;
                    if (selected.Count == 0) return;
                    List<Bewerking> added = new List<Bewerking>();
                    foreach (var item in selected)
                    {
                        var xnew = new NewCombineerForm(Productie, item);
                        if (xnew.ShowDialog(this) == DialogResult.OK)
                        {
                            Productie.Combies.Add(xnew.SelectedEntry);
                            
                            item.Combies.RemoveAll(x => string.Equals(Path.Combine(x.ProductieNr, x.BewerkingNaam),
                                Productie.Path, StringComparison.CurrentCultureIgnoreCase));
                            item.Combies.Add(new CombineerEntry()
                            {
                                ProductieNr = Productie.ProductieNr, BewerkingNaam = Productie.Naam,
                                Activiteit = 100 - xnew.SelectedEntry.Activiteit, Periode = new TijdEntry(DateTime.Now, default)
                            });
                            var msg = $"[{item.ArtikelNr} | {item.ProductieNr}] toegevoegd als combinatie.\n" +
                                  $"Activiteit is gewijzigd!";
                            if (item.xUpdateBewerking(null, msg))
                                added.Add(item);
                            System.Threading.Thread.Sleep(500);
                            Application.DoEvents();
                        }
                    }

                    if (added.Count > 0)
                    {
                        var x1 = added.Count == 1 ? "Productie" : "Producties";
                        var left = 100 - Productie.Combies.Sum(x => x.Activiteit);
                        var msg = $"{added.Count} {x1} toegevoegd als combinatie.\n" +
                                  $"Huidige productie activiteit is gewijzigd van {Productie.Activiteit}% naar {left}%";

                        if (Productie.xUpdateBewerking(null, msg))
                        {
                            _= Productie.UpdateCombies();
                        }
                    }
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }
    }
}
