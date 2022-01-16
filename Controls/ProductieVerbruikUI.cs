using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.Mozilla;
using Rpm.Misc;
using Rpm.Productie;

namespace Controls
{
    public partial class ProductieVerbruikUI : UserControl
    {
        private ProductieFormulier _form;
        public ProductieVerbruikUI()
        {
            InitializeComponent();
        }

        private void xrekenmachine_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("calc.exe");
        }

        private readonly List<decimal> xprods = new List<decimal>();

        public void InitFields(ProductieFormulier form)
        {
            try
            {
                _form = form;
                xprods.Clear();
                xmaterialen.Items.Clear();
                if (form?.Materialen == null) return;
                xprods.AddRange(form.Materialen.Select(x => (decimal) x.AantalPerStuk * ((int)(x.Eenheid.ToLower().StartsWith("m") ? 1000 : 1))));
                var xselected = xmaterialen.SelectedIndex;
                
                xmaterialen.Items.AddRange(form.Materialen.Select(x => (object) $"[{x.ArtikelNr}]{x.Omschrijving} ({x.AantalPerStuk * ((int)(x.Eenheid.ToLower().StartsWith("m") ? 1000 : 1))})")
                    .ToArray());
                if (xselected > -1 && xmaterialen.Items.Count > xselected)
                    xmaterialen.SelectedIndex = xselected;
                else if (xmaterialen.Items.Count > 0)
                    xmaterialen.SelectedIndex = 0;
                UpdateFields();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateFields()
        {
            var xvalue = Math.Round(xprodlengte.Value / 1000, 4);
            var xtotal = Math.Round(xuitganglengte.Value / 1000, 4);
            xprodlabel.Text = $"Product Lengte({xvalue}m)";
            xlengtelabel.Text = $"Uitgang Lengte({xtotal}m)";
            if (_form != null)
            {
                var xprodsperlengte = xvalue > 0 ? (xtotal / xvalue) : 0;
                var xrest = xvalue > 0 ? Math.Round((xtotal % xvalue) * 1000,2) : 0;
                var xnodig = xprodsperlengte > 0 ? (_form.Aantal / (int)xprodsperlengte) : 0;
                if (xnodig * (int) xprodsperlengte < _form.Aantal)
                    xnodig++;
                xinfo.Text = $"<span color='{Color.Navy.Name}'>Met een productlengte van <b>{xvalue}m</b> heb je <b>{xnodig}</b> lengtes nodig.<br>" +
                             $"Met <b>{xnodig}</b> lengtes kan je <b>{(xnodig * (int)xprodsperlengte)}/ {_form.Aantal}</b> producten maken.<br>" +
                             $"Je haalt <b>{(int)xprodsperlengte}</b> producten uit <b>{xtotal} meter</b> met een reststuk van <b>{xrest}mm</b></span>";
            }
        }

        private int _index = -1;
        private void xmaterialen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xmaterialen.SelectedIndex > -1 && xprods.Count > xmaterialen.SelectedIndex && _index != xmaterialen.SelectedIndex)
            {
                xprodlengte.SetValue(xprods[xmaterialen.SelectedIndex]);
                _index = xmaterialen.SelectedIndex;
            }
        }

        private void xprodlengte_ValueChanged_1(object sender, EventArgs e)
        {
            UpdateFields();
        }
    }
}
