﻿using Forms;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Forms.Combineer;

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
                      $"Alle gecombineerde producties zullen automatisch starten,stoppen en worden gereed gemeld.<br>" +
                      $"<b>Huidige productie draait op {Productie.Activiteit}%</b></span>";
            }

            xomschrijving.Text = msg;
        }

        private void xaddproductie_Click(object sender, EventArgs e)
        {
            try
            {
                if (Manager.Database == null || Manager.Database.IsDisposed || Productie == null) return;
                var prods = Manager.Database.GetAllBewerkingen(false, true, true).Result;
                prods.Remove(Productie);
                prods.RemoveAll(x =>
                    x.Combies.Any(c =>
                        string.Equals(c.Path, Productie.Path, StringComparison.CurrentCultureIgnoreCase)));
                if (prods.Count == 0)
                {
                    XMessageBox.Show(this, $"Geen producties om te combineren");
                    return;
                }
                var xbwselector = new BewerkingSelectorForm(prods, false, false);
                if (xbwselector.ShowDialog() == DialogResult.OK)
                {
                    var selected = xbwselector.SelectedBewerkingen;
                    if (selected.Count == 0) return;
                    List<Bewerking> added = new List<Bewerking>();
                    foreach (var item in selected)
                    {
                        var xnew = new NewCombineerForm(Productie, item);
                        if (xnew.ShowDialog() == DialogResult.OK)
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
                            if (item.UpdateBewerking(null, msg).Result)
                                added.Add(item);
                            Task.Delay(500).Wait();
                            Application.DoEvents();
                        }
                    }

                    if (added.Count > 0)
                    {
                        var x1 = added.Count == 1 ? "Productie" : "Producties";
                        var left = 100 - Productie.Combies.Sum(x => x.Activiteit);
                        var msg = $"{added.Count} {x1} toegevoegd als combinatie.\n" +
                                  $"Huidige productie activiteit is gewijzigd van {Productie.Activiteit}% naar {left}%";

                        if (Productie.UpdateBewerking(null, msg).Result)
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
