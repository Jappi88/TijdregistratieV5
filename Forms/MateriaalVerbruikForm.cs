﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using Forms.MetroBase;
using ProductieManager.Rpm.Productie;
using Rpm.Productie;
using Rpm.Various;

namespace Forms
{
    public partial class MateriaalVerbruikForm : MetroBaseForm
    {
        private bool _isbussy;
        public List<MateriaalEntryInfo> Materialen = new();

        public MateriaalVerbruikForm()
        {
            InitializeComponent();
            xstart.Value = DateTime.Now.Subtract(TimeSpan.FromDays(365));
            xsearchbox.ShowClearButton = true;
            //((OLVColumn) xmateriaalList.Columns[2]).AspectGetter = VerbruikAspect;
            //((OLVColumn)xmateriaalList.Columns[3]).AspectGetter = AfkeurAspect;
        }

        private object VerbruikAspect(object item)
        {
            if (item == null) return 0;
            if (item is MateriaalEntryInfo entry) return entry.Verbruik + $" {entry.Eenheid}";

            return 0;
        }

        private object AfkeurAspect(object item)
        {
            if (item == null) return 0;
            if (item is MateriaalEntryInfo entry) return entry.Afkeur + $" {entry.Eenheid}";

            return 0;
        }

        public async void LoadMaterialen()
        {
            if (_isbussy) return;
            try
            {
                _isbussy = true;
                xloadinglabel.Visible = true;
                var mats = await MateriaalBeheer.GetMateriaalVerbruik(new TijdEntry(xstart.Value, xstop.Value, null));
                Materialen = mats.Select(x => x.Value).ToList();
                xmateriaalList.SetObjects(Materialen);
                xloadinglabel.Visible = false;
                Text = $"Materiaal verbruik vanaf {xstart.Value:D} t/m {xstop.Value:D}";
                UpdateStatus();
                Invalidate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _isbussy = false;
        }

        private void UpdateStatus()
        {
            if (xmateriaalList.Items.Count > 0)
            {
                var items = xmateriaalList.Objects.Cast<MateriaalEntryInfo>().ToList();
                var x1 = items.Count == 1 ? "materiaal" : "materialen";
                xstatuslabel.Text =
                    $"<div Color= RoyalBlue><b>{(items.Count == 0 ? "Geen" : items.Count)} {x1} geladen.</b></div>";
                if (items.Count > 0)
                {
                    var xverbruik = new Dictionary<string, double>();
                    var xafkeur = new Dictionary<string, double>();
                    foreach (var item in items)
                    {
                        var verbruik = item.Verbruik;
                        if (xverbruik.ContainsKey(item.Eenheid))
                            xverbruik[item.Eenheid] += verbruik;
                        else if (verbruik > 0) xverbruik.Add(item.Eenheid, verbruik);

                        if (xafkeur.ContainsKey(item.Eenheid))
                            xafkeur[item.Eenheid] += item.Afkeur;
                        else if (item.Afkeur > 0) xafkeur.Add(item.Eenheid, item.Afkeur);
                    }

                    if (xverbruik.Count > 0)
                        xstatuslabel.Text +=
                            $"<div Color= DarkGreen><b>Totaal {string.Join(", ", xverbruik.Select(x => $"{x.Value} {x.Key}"))} verbruikt.</b></div>";

                    if (xafkeur.Count > 0)
                        xstatuslabel.Text +=
                            $"<div Color= Red><b>Totaal {string.Join(", ", xafkeur.Select(x => $"{x.Value} {x.Key}"))} afkeur.</b></div>";
                }
            }
            else
            {
                xstatuslabel.Text = "<b>Geen materialen geladen.</b>";
            }
        }

        private void DoProgress(object sender, ProgressArg arg)
        {
            Invoke(new MethodInvoker(() =>
            {
                xloadinglabel.Text = arg.Message + $"{arg.Pogress}%";
                Invalidate();
            }));
            Application.DoEvents();
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void xloadmaterialen_Click(object sender, EventArgs e)
        {
            LoadMaterialen();
        }

        private void xsearchbox_Enter(object sender, EventArgs e)
        {
            if (xsearchbox.Text.ToLower() == "zoeken...")
                xsearchbox.Text = "";
        }

        private void xsearchbox_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(xsearchbox.Text.Trim()))
                xsearchbox.Text = "Zoeken...";
        }

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            if (xsearchbox.Text.ToLower() == "zoeken..." || _isbussy) return;
            var xcrit = xsearchbox.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(xcrit))
                xmateriaalList.SetObjects(Materialen);
            else
                xmateriaalList.SetObjects(Materialen.Where(x => !string.IsNullOrEmpty(x.ArtikelNr) &&
                    x.ArtikelNr.ToLower().Contains(xcrit) || !string.IsNullOrEmpty(x.Omschrijving) &&
                    x.Omschrijving.ToLower().Contains(xcrit)));
            UpdateStatus();
        }

        private void xmateriaalList_FormatCell(object sender, FormatCellEventArgs e)
        {
            switch (e.Column.Text.ToLower())
            {
                case "artikelnr":
                    e.SubItem.BackColor = Color.LightCyan;
                    break;
                case "omschrijving":
                    e.SubItem.BackColor = Color.LightSkyBlue;
                    break;
                case "verbruik":
                    e.SubItem.BackColor = Color.PaleGreen;
                    break;
                case "afkeur":
                    e.SubItem.BackColor = Color.PaleVioletRed;
                    break;
                case "gemiddeld p/eenheid":
                    e.SubItem.BackColor = Color.LightSteelBlue;
                    break;
                case "aantal producties":
                    e.SubItem.BackColor = Color.AliceBlue;
                    break;
                case "eenheid":
                    e.SubItem.BackColor = Color.LightGoldenrodYellow;
                    break;
            }
        }

        private void MateriaalVerbruikForm_Shown(object sender, EventArgs e)
        {
            LoadMaterialen();
        }
    }
}