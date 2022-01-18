using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using Forms;
using NPOI.XSSF.UserModel.Charts;
using Org.BouncyCastle.Asn1.Mozilla;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;

namespace Controls
{
    public partial class ProductieVerbruikUI : UserControl
    {
        private ProductieFormulier _form;

        public bool ShowMateriaalSelector
        {
            get => xmaterialen.Visible;
            set => xmaterialen.Visible = value;
        }

        public bool ShowOpslaan
        {
            get => xopslaan.Visible;
            set => xopslaan.Visible = value;
        }
        public ProductieVerbruikUI()
        {
            InitializeComponent();
        }

        public string Title
        {
            get => groupBox1.Text;
            set=> groupBox1.Text = value;
        }

        private int GetAantalSporen()
        {
            if (_spoor == null || xaantalsporen.Value ==1 && !string.IsNullOrEmpty(_form?.Opmerking))
            {
                var xindex = _form.Opmerking.IndexOf("max banen:", StringComparison.CurrentCultureIgnoreCase);
                if (xindex > -1)
                {
                    string xval = "";
                    xindex += 10;
                    var xtmp = _form.Opmerking.Substring(xindex, _form.Opmerking.Length - xindex);
                    bool begin = false;
                    foreach (var xchar in xtmp)
                    {
                        if (char.IsDigit(xchar))
                        {
                            begin = true;
                            xval += xchar;
                        }
                        else if (begin)
                            break;
                    }

                    if (!int.TryParse(xval, out var sporen))
                        sporen = (int) xaantalsporen.Value;
                    return sporen;
                }
            }

            return (int)xaantalsporen.Value;
        }



        public SpoorEntry GetSpoorInfo()
        {
            try
            {
                SpoorEntry spoor = _spoor;
                if (!string.IsNullOrEmpty(_form.ArtikelNr))
                {
                    if (Manager.SporenBeheer != null)
                        spoor = Manager.SporenBeheer.GetSpoor(_form.ArtikelNr.Trim());
                }
                if (spoor == null)
                {
                    spoor = new SpoorEntry
                    {
                        ProductLengte = GetSelectedMateriaalLengte(),
                        AantalSporen = GetAantalSporen(),
                        Aantal = (int) xproduceren.Value,
                        UitgangsLengte = xuitganglengte.Value
                    };
                }

                return spoor;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
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
                var xselected = xmaterialen.SelectedIndex;
                xmaterialen.Items.Clear();
                if (form?.Materialen == null) return;
                xprods.AddRange(form.Materialen.Select(x => (decimal) x.AantalPerStuk * ((int)(x.Eenheid.ToLower().StartsWith("m") ? 1000 : 1))));
               
                
                xmaterialen.Items.AddRange(form.Materialen.Select(x => (object) $"[{x.ArtikelNr}]{x.Omschrijving} ({x.AantalPerStuk * ((int)(x.Eenheid.ToLower().StartsWith("m") ? 1000 : 1))} mm)")
                    .ToArray());
                if (xselected > -1 && xmaterialen.Items.Count > xselected)
                    xmaterialen.SelectedIndex = xselected;
                else if (xmaterialen.Items.Count > 0)
                    xmaterialen.SelectedIndex = 0;
                UpdateFields(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateFields(bool initvalues, SpoorEntry entry = null)
        {
            var aantal = 0;
            aantal = _form?.Aantal ?? _spoor?.Aantal ?? 1;
            if (xproduceren.Tag is int val)
            {
                if (val != aantal)
                {
                    xproduceren.SetValue(aantal);
                    xproduceren.Tag = aantal;
                }
            }
            else
            {
                xproduceren.SetValue(aantal);
                xproduceren.Tag = aantal;
            }
            if (initvalues)
            {
                if (entry != null)
                    _spoor = entry;
                else
                    _spoor ??= GetSpoorInfo();
                if (_spoor != null)
                {
                    xaantalsporen.SetValue(_spoor.AantalSporen);
                    xuitganglengte.SetValue(_spoor.UitgangsLengte);
                    xprodlengte.SetValue(_spoor.ProductLengte);
                }
            }
            _spoor ??= GetSpoorInfo()??new SpoorEntry();
            aantal = (int) xproduceren.Value;

            var xvalue = Math.Round(xprodlengte.Value / 1000, 4);
            var xtotal = Math.Round(xuitganglengte.Value / 1000, 4);
            //var xstuks = xprodlengte.Value > 0 && xuitganglengte.Value > 0
            //    ? Math.Round(xprodlengte.Value / xuitganglengte.Value, 4)
            //    : 0;
            xprodlabel.Text = $"Productlengte({xvalue}m)";
            xlengtelabel.Text = $"Uitgangslengte({xtotal}m)";
            xinfo.Text = _spoor.CreateHtmlText(aantal);
            //var xprodsperlengte = xvalue > 0 ? (int) (xtotal / xvalue) : 0;
            //var xrest = xvalue > 0 ? Math.Round((xtotal % xvalue) * 1000, 2) : 0;
            //var xnodig = xprodsperlengte > 0 ? aantal < xprodsperlengte ? 1 : (aantal / xprodsperlengte) : 0;

            //if (xnodig == 0)
            //{
            //    xinfo.Text = $"<span color='{Color.Navy.Name}'>" +
            //                 $"Er kunnen geen producten van <b>{Math.Round(xprodlengte.Value, 2)}mm</b> gehaald worden uit <b>{xuitganglengte.Value}mm</b>!" +
            //                 $"</span>";
            //}
            //else
            //{
            //    if (xnodig * xprodsperlengte < aantal)
            //        xnodig++;
            //    var xsporen = (int) xaantalsporen.Value;
            //    var xrestsporen = (int) (xnodig % xsporen);
            //    if (xnodig < xsporen)
            //    {
            //        xsporen = xnodig;
            //        xrestsporen = 0;
            //    }
            //    var xaantalladen = xnodig > 0 ? xnodig < xsporen ? 1 : xnodig / xsporen : 0;
            //    var x1 = xsporen == 1 ? "spoor" : "sporen";
            //    var x2 = xrestsporen == 1 ? "spoor" : "sporen";
            //    xinfo.Text = $"<span color='{Color.Navy.Name}'>" +
            //                 $"Een productLengte van <b>{Math.Round(xprodlengte.Value, 2)}mm</b> is <b>{xstuks}(stuk)</b> van <b>{xuitganglengte.Value}mm</b><br><br>" +
            //                 $"Met een productlengte van <b>{xvalue}m</b> heb je <b>{xnodig}</b> lengtes nodig.<br>" +
            //                 $"Dat is <b>{xaantalladen}</b> keer laden met <b>{xsporen}</b> {x1}{(xrestsporen > 0 ? $" en een restlading van <b>{xrestsporen}</b> {x2}" : "")}.<br>" +
            //                 $"Met <b>{xnodig}</b> lengtes kan je <b>{(xnodig * (int) xprodsperlengte)}/ {aantal}</b> producten maken.<br>" +
            //                 $"Je haalt <b>{(int) xprodsperlengte}</b> producten uit <b>{xtotal} meter</b> met een reststuk van <b>{xrest}mm</b></span>";
            //}


            if (!string.IsNullOrEmpty(_spoor?.AangepastDoor))
            {
                xaangepastdoor.Text =
                    $"<span style=\'font-size: x-small; color: {Color.Navy.Name};\'>Aangepast door <b>{_spoor.AangepastDoor}</b> op: <br>{_spoor.AangepastOp:f}</span>";

            }
            else xaangepastdoor.Text = "";
        }

        private int _index = -1;
        private SpoorEntry _spoor;
        private void xmaterialen_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (xmaterialen.SelectedIndex > -1 && xprods.Count > xmaterialen.SelectedIndex && _index != xmaterialen.SelectedIndex)
            {
               
                _index = xmaterialen.SelectedIndex;
                _spoor = GetSpoorInfo();
                if (_spoor != null)
                {
                    xaantalsporen.SetValue(_spoor.AantalSporen);
                    xuitganglengte.SetValue(_spoor.UitgangsLengte);
                    xprodlengte.SetValue(_spoor.ProductLengte);
                }
                if (xprodlengte.Value == 0)
                    xprodlengte.SetValue(xprods[xmaterialen.SelectedIndex]);
            }
        }

        private void xprodlengte_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar.Equals('.') || e.KeyChar.Equals(','))
            {
                e.KeyChar = ((CultureInfo)CultureInfo.CurrentCulture).NumberFormat.NumberDecimalSeparator.ToCharArray()[0];
            }
        }

        public Materiaal GetSelectedMateriaalArtikelNr()
        {
            try
            {
                if (_form?.Materialen == null)
                    return null;
                var index = xmaterialen.SelectedIndex;
                if (index > -1 && _form.Materialen.Count > index)
                {
                    var xmat = _form.Materialen[index];
                    return xmat;
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public decimal GetSelectedMateriaalLengte()
        {
            try
            {
                var xmat = GetSelectedMateriaalArtikelNr();
                if (xmat == null)
                    return xprodlengte.Value;
                return (decimal) GetSelectedMateriaalArtikelNr().AantalPerStuk;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return 1;
            }
        }

        private void xopslaan_Click(object sender, EventArgs e)
        {
            try
            {
                if (Manager.SporenBeheer == null || Manager.SporenBeheer.Disposed)
                    throw new Exception("Sporen database is niet beschikbaar!");
                var xart = _spoor?.ArtikelNr;
                var xoms = _spoor?.ProductOmschrijving;
                if (string.IsNullOrEmpty(xart) && !string.IsNullOrEmpty(_form.ArtikelNr))
                {
                    xart = _form.ArtikelNr;
                    xoms = _form.Omschrijving;
                }
                if (Manager.SporenBeheer != null && !string.IsNullOrEmpty(xart))
                {
                    var xmat = GetSelectedMateriaalArtikelNr();
                    _spoor ??= GetSpoorInfo()??new SpoorEntry();
                    _spoor.ArtikelNr = xart;
                    _spoor.MateriaalArtikelNr = xmat?.ArtikelNr;
                    _spoor.MateriaalOmschrijving = xmat?.Omschrijving;
                    _spoor.ProductOmschrijving = xoms;
                    var xtxtform = new TextFieldEditor();
                    xtxtform.EnableSecondaryField = false;
                    xtxtform.FieldImage = Resources.user_64_64;
                    xtxtform.MultiLine = false;
                    xtxtform.UseSecondary = false;
                    xtxtform.Title = "Vul in je Naam";
                    if (xtxtform.ShowDialog() == DialogResult.Cancel) return;
                    _spoor.AangepastDoor = xtxtform.SelectedText.Trim().FirstCharToUpper();
                    Manager.SporenBeheer.SaveSpoor(_spoor,
                        $"[SPOOR][{_spoor.ArtikelNr}] {_spoor.ProductOmschrijving} succesvol opgeslagen!");
                    UpdateFields(false);
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xprodlengte_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Enter)
            {
                e.Handled = e.SuppressKeyPress = true;
            }
        }

        private void xprodlengte_ValueChanged(object sender, EventArgs e)
        {
            if (_spoor != null)
            {
                _spoor.ProductLengte = xprodlengte.Value;

                UpdateFields(false);
            }
        }

        private void xuitganglengte_ValueChanged(object sender, EventArgs e)
        {
            if (_spoor != null)
            {
                _spoor.UitgangsLengte = xuitganglengte.Value;
                UpdateFields(false);
            }
        }

        private void xaantalsporen_ValueChanged(object sender, EventArgs e)
        {
            if (_spoor != null)
            {
                _spoor.AantalSporen = (int)xaantalsporen.Value;
                UpdateFields(false);
            }
        }

        private void xproduceren_ValueChanged(object sender, EventArgs e)
        {
            if (_spoor != null)
            {
                _spoor.Aantal = (int)xproduceren.Value;
                UpdateFields(false);
            }
        }

        private void xoptimalemaat_Click(object sender, EventArgs e)
        {
            try
            {
                var reststuk = 20m;
                var maxlengte = 9000m;
                var xprod = this.xprodlengte.Value;
                if (xprod <= 0)
                    throw new Exception("Productlengte kan niet '0' zijn!");
                var xtxtform = new TextFieldEditor();
                xtxtform.EnableSecondaryField = false;
                xtxtform.FieldImage = Resources.geometry_measure_96x96;
                xtxtform.MultiLine = false;
                xtxtform.MinimalTextLength = 1;
                xtxtform.SelectedText = reststuk.ToString(CultureInfo.InvariantCulture);
                xtxtform.UseSecondary = false;
                xtxtform.Title = "Wat is de minimale reststuk?(mm)";
                if (xtxtform.ShowDialog() == DialogResult.Cancel) return;
                decimal.TryParse(xtxtform.SelectedText.Trim(), out reststuk);
                xtxtform.Title = "Wat is de maximale UitgangsLengte?(mm)";
                xtxtform.SelectedText = maxlengte.ToString(CultureInfo.InvariantCulture);
                if (xtxtform.ShowDialog() == DialogResult.Cancel) return;
                decimal.TryParse(xtxtform.SelectedText.Trim(), out maxlengte);
                xtxtform.Dispose();
                var xcurlengte = maxlengte;
                var xrest = (xcurlengte % xprod);
                if (xrest >= reststuk)
                {
                    var dif = xrest - reststuk;
                    xcurlengte -= dif;
                }
                else
                {
                    var xaantallen = xcurlengte - reststuk;
                    xrest = (xaantallen % xprod);
                    if (xrest == 0)
                        xcurlengte = xaantallen;
                    else
                        xcurlengte -= xrest; // - xrest;
                }

                xuitganglengte.SetValue((int)xcurlengte);
            }
            catch (Exception exception)
            {
                XMessageBox.Show(exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }
    }
}
