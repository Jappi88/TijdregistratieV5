using Forms;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Forms.Sporen;
using Rpm.MateriaalSoort;
using Various;

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

        public decimal RestStuk { get; set; } = 20;
        public decimal MaxUitgangsLengte { get; set; } = 7500;

        public bool ShowOpslaan
        {
            get => xopslaan.Visible;
            set => xopslaan.Visible = value;
        }

        public bool ShowSluiten
        {
            get => xsluiten.Visible;
            set
            {
                xsluiten.Visible = value;
                xbuttonseperator.Visible = value;
            }
        }

        public bool ShowPerUur { get; set; }

        public bool ShowOpdrukkerArtikelNr
        {
            get => xopdrukkerpanel.Visible;
            set => xopdrukkerpanel.Visible = value;
        }

        public Opdrukker OpdrukkerArtikel { get; set; }

        public ProductieVerbruikUI()
        {
            InitializeComponent();
            xmachine.SelectedIndex = 0;
            xmachine.SelectedIndexChanged += Xmachine_SelectedIndexChanged;
            if (Manager.SporenBeheer != null)
            {
                xopdrukkerartikelnr.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                xopdrukkerartikelnr.AutoCompleteCustomSource.AddRange(Manager.SporenBeheer.GetAlleIDs().ToArray());
            }
        }

        private void Xmachine_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateOpdrukkerInfo();
        }

        public string Title
        {
            get => groupBox1.Text;
            set=> groupBox1.Text = value;
        }

        private int GetAantalSporen()
        {
            if ((_spoor == null || xaantalsporen.Value ==1) && !string.IsNullOrEmpty(_form?.Opmerking))
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
                var artikel = _form?.ArtikelNr ?? xopdrukkerartikelnr.Text;
                if (artikel.ToLower().StartsWith("vul in een artikelnr"))
                    artikel = _spoor?.ArtikelNr;
                if (!string.IsNullOrEmpty(artikel))
                {
                    if (Manager.SporenBeheer != null)
                        spoor = Manager.SporenBeheer.GetSpoor(artikel.Trim());
                }
                if (spoor == null)
                {
                    spoor = new SpoorEntry
                    {
                        ArtikelNr = artikel,
                        ProductLengte = GetSelectedMateriaalLengte(),
                        AantalSporen = GetAantalSporen(),
                        PakketAantal = (int)xperPak.Value,
                        Aantal = (int) xproduceren.Value,
                        UitgangsLengte = xuitganglengte.Value,
                        ProductOmschrijving = _form?.Omschrijving??OpdrukkerArtikel?.GetHtmlString()
                    };
                    if (OpdrukkerArtikel != null)
                        spoor.PerUur = OpdrukkerArtikel.PerUur();
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
            var aantal = _form?.Aantal ?? _spoor?.Aantal ?? 1;
            if (_form is {State: ProductieState.Gereed})
            {
                aantal = _form.TotaalGemaakt;
            }

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
                    _spoor = GetSpoorInfo();
                if (_spoor != null)
                {
                    xaantalsporen.SetValue(_spoor.AantalSporen);
                    xuitganglengte.SetValue(_spoor.UitgangsLengte);
                    xprodlengte.SetValue(_spoor.ProductLengte);
                    xperPak.SetValue(_spoor.PakketAantal);
                }
            }
            _spoor ??= GetSpoorInfo()??new SpoorEntry();
            aantal = (int) xproduceren.Value;

            var xvalue = Math.Round(xprodlengte.Value / 1000, 4);
            var xtotal = Math.Round(xuitganglengte.Value / 1000, 4);

            xprodlabel.Text = $"Productlengte({xvalue}m)";
            xlengtelabel.Text = $"Uitgangslengte({xtotal}m)";
            xinfo.Text = _spoor.CreateHtmlText(aantal, ShowPerUur);

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
                    xprodlengte.SetValue(_spoor.ProductLengte);//mm
                    xperPak.SetValue(_spoor.PakketAantal);
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
                var lengte = (decimal) GetSelectedMateriaalArtikelNr().AantalPerStuk;
                if (!xmat.Eenheid.ToLower().Contains("stuk"))
                    lengte *= 1000;
                return lengte;
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
                if (string.IsNullOrEmpty(xart) && !string.IsNullOrEmpty(_form?.ArtikelNr))
                {
                    xart = _form.ArtikelNr;
                    xoms = _form.Omschrijving;
                }
                else
                {
                    if (!xopdrukkerartikelnr.Text.Trim().ToLower().StartsWith("vul in een artikelnr") &&
                        xopdrukkerartikelnr.Text.Trim().Length > 4)
                        xart = xopdrukkerartikelnr.Text.Trim();
                    if (IsOpdrukkerArtikelNr() && OpdrukkerArtikel != null)
                        xoms = OpdrukkerArtikel.GetTitle();
                }

                if (Manager.SporenBeheer != null && !string.IsNullOrEmpty(xart))
                {
                    var xmat = GetSelectedMateriaalArtikelNr();
                    _spoor ??= GetSpoorInfo()??new SpoorEntry();
                    _spoor.ArtikelNr = xart;
                    _spoor.MateriaalArtikelNr = xmat?.ArtikelNr;
                    _spoor.MateriaalOmschrijving = xmat?.Omschrijving;
                    _spoor.ProductOmschrijving = xoms;
                    _spoor.ProductHtmlOmschrijving = OpdrukkerArtikel?.GetHtmlString();
                    var xtxtform = new TextFieldEditor();
                    xtxtform.EnableSecondaryField = false;
                    xtxtform.FieldImage = Resources.user_64_64;
                    xtxtform.MultiLine = false;
                    xtxtform.UseSecondary = false;
                    xtxtform.Title = "Vul in je Naam";
                    if (xtxtform.ShowDialog() == DialogResult.Cancel) return;
                    _spoor.AangepastDoor = xtxtform.SelectedText.Trim().FirstCharToUpper();
                    _spoor.AangepastOp = DateTime.Now;
                    Manager.SporenBeheer.SaveSpoor(_spoor,
                        $"[SPOOR][{_spoor.ArtikelNr}] {_spoor.ProductOmschrijving} succesvol opgeslagen!");
                    UpdateFields(false);
                }
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
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

        private void xperPak_ValueChanged(object sender, EventArgs e)
        {
            if (_spoor != null)
            {
                _spoor.PakketAantal = (int)xperPak.Value;
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


        public decimal OptimalUitgangsLengte(decimal prodlengte, decimal rest, decimal maxlengte)
        {
            var xcurlengte = maxlengte;
            if (prodlengte == 0) return maxlengte;
            var xrest = (xcurlengte % prodlengte);
            if (xrest >= rest)
            {
                var dif = xrest - rest;
                xcurlengte -= dif;
            }
            else
            {
                var xaantallen = xcurlengte - rest;
                xrest = (xaantallen % prodlengte);
                if (xrest == 0)
                    xcurlengte = xaantallen;
                else
                    xcurlengte -= xrest; // - xrest;
            }

            return xcurlengte;
        }

        private readonly char[] _opdrukkertypes = new char[] {'a', 'g', 's'};

        private void xoptimalemaat_Click(object sender, EventArgs e)
        {
            try
            {
                var xfirst = xopdrukkerartikelnr.Text.ToLower().FirstOrDefault();
                bool isopdrukker = _opdrukkertypes.Any(x => x == xfirst);
                var reststuk =  isopdrukker? RestStuk : 20;
                var maxlengte = isopdrukker? MaxUitgangsLengte : 7500;
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

                var xcurlengte = OptimalUitgangsLengte(xprodlengte.Value, reststuk, maxlengte);
                xuitganglengte.SetValue((int)xcurlengte);
            }
            catch (Exception exception)
            {
                XMessageBox.Show(this, exception.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            if (Parent is Form form)
                form.Close();
        }

        private bool IsOpdrukkerArtikelNr()
        {
            var txt = xopdrukkerartikelnr.Text.Trim().ToLower();
            var xfirst = txt.FirstOrDefault();
            bool isopdrukker = _opdrukkertypes.Any(x => x == xfirst);
            return isopdrukker;
        }

        private void xopdrukkerartikelnr_TextChanged(object sender, EventArgs e)
        {
            if (xopdrukkerartikelnr.Text.Length >= 4)
            {
                var isopdrukker = IsOpdrukkerArtikelNr();
                if (isopdrukker)
                {
                    UpdateOpdrukkerInfo();
                    UpdateFields(true, null);
                }
                else
                {
                    xmachine.Visible = false;
                    OpdrukkerArtikel = null;
                    UpdateFields(true, null);
                }
            }
            else
            {
                xmachine.Visible = false;
                OpdrukkerArtikel = null;
            }

        }

        private void UpdateOpdrukkerInfo()
        {
            if (xmachine.SelectedIndex is < 0 or > 1)
                return;
            if (!IsOpdrukkerArtikelNr()) return;
            OpdrukkerArtikel ??= new Opdrukker();
            OpdrukkerArtikel.Machine = (OpdrukkerMachine) (xmachine.SelectedIndex);
            OpdrukkerArtikel.UpdateOpdrukkerArtikelNr(xopdrukkerartikelnr.Text);
            xmachine.Visible = true;
            _spoor = GetSpoorInfo();
            _spoor.ProductOmschrijving = OpdrukkerArtikel.GetTitle();
            _spoor.ProductHtmlOmschrijving = OpdrukkerArtikel.GetHtmlString();
            xprodlengte.SetValue(OpdrukkerArtikel.KnipMaat);
            xperPak.SetValue(OpdrukkerArtikel.PakketAantal);
            xaantalsporen.Value = xmachine.SelectedIndex == 0 ? 1 : 2;
            var xcurlengte = OptimalUitgangsLengte(OpdrukkerArtikel.KnipMaat, 50, 12450);
            xcurlengte = ((int)Math.Round(xcurlengte / 50.0m)) * 50;
            xuitganglengte.SetValue((int)xcurlengte);
        }

        private void xopdrukkerartikelnr_Enter(object sender, EventArgs e)
        {
            if (xopdrukkerartikelnr.Text.ToLower().StartsWith("vul in een artikelnr"))
            {
                xopdrukkerartikelnr.CharacterCasing = CharacterCasing.Upper;
                xopdrukkerartikelnr.Text = "";
            }
        }

        private void xopdrukkerartikelnr_Leave(object sender, EventArgs e)
        {
            if (xopdrukkerartikelnr.Text.Trim().Length < 1)
            {
                xopdrukkerartikelnr.CharacterCasing = CharacterCasing.Normal;
                xopdrukkerartikelnr.Text = "Vul in een ArtikelNr..";
            }
        }

        private void xaantalafronden_Click(object sender, EventArgs e)
        {
            var xp = xprodlengte.Value == 0? 0 : ((int)(xuitganglengte.Value / xprodlengte.Value));
            var restsporen = 0;
            var restpakket = xperPak.Value > 0 ? xproduceren.Value % (xperPak.Value * xp) : 0;
            var aantal = xproduceren.Value;

            var xnodig = xp > 0 ? aantal < xp ? 1 : Math.Ceiling(aantal / xp) : 0;

            if (xnodig > 0)
            {
                if (xnodig * xp < aantal)
                    xnodig++;
                var xsporen = xaantalsporen.Value;
                restsporen = (int) (xnodig % xsporen);
            }

            var xselect = new List<string>();
            if (restsporen > 0)
            {
                xselect.Add("Rond af naar Boven voor een volle Lading");
                xselect.Add("Rond af naar Beneden voor een volle Lading");
            }
            if (restpakket > 0)
            {
                xselect.Add("Rond af naar Boven voor een volle Pakket");
                xselect.Add("Rond af naar Beneden voor een volle Pakket");
            }
            if (xselect.Count == 0)
            {
                var x1 = xproduceren.Value == 1 ? "product" : "producten";
                XMessageBox.Show(
                    this, $"Met {xproduceren.Value} {x1} eindig je al op een volle pakket en een volle lading.");
                return;
            }

            var xform = new XMessageBox();
            var dlg = xform.ShowDialog(this, $"Waar wil je de aantal van {xproduceren.Value} producten op afronden?",
                "Aantal Afronden", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, xselect.ToArray());
            if (dlg == DialogResult.Cancel) return;
            var xs = xform.SelectedValue;
            if (xs.EndsWith("Lading"))
            {
                var boven = xs.Contains("Boven");
                var xpakktets = boven ? Math.Ceiling(xproduceren.Value / (xaantalsporen.Value * xp)) : Math.Floor(xproduceren.Value / (xaantalsporen.Value * xp));
                var xp1 = (int)((xpakktets * xaantalsporen.Value) * xp);
                xproduceren.SetValue(xp1);

            }
            else if (xs.EndsWith("Pakket"))
            {
                var boven = xs.Contains("Boven");
                var xpakktets = boven ? Math.Ceiling(xproduceren.Value / (xperPak.Value * xp)) : Math.Floor(xproduceren.Value / (xperPak.Value * xp));
                var xp1 = (int)((xpakktets * xperPak.Value) * xp);
                xproduceren.SetValue(xp1);
            }
        }

        private void xdatabase_Click(object sender, EventArgs e)
        {
            var info = OpdrukkerInfo.Load();
            var xform = new SpoorDatabaseForm(info);
            if (xform.ShowDialog() == DialogResult.OK)
            {
                info.SaveInfo();
            }
        }

        private void xtoonwerktekening_Click(object sender, EventArgs e)
        {
            ShowWerkTekening();
        }

        private void ShowWerkTekening()
        {
            if (string.IsNullOrEmpty(_spoor?.ArtikelNr??_form?.ArtikelNr)) return;
            Tools.ShowSelectedTekening(_spoor?.ArtikelNr??_form.ArtikelNr, TekeningClosed);
        }

        private void TekeningClosed(object sender, EventArgs e)
        {
            var form = Application.OpenForms["MainForm"];
            form?.BringToFront();
            form?.Focus();
            this.Parent?.Select();
            this.Parent?.BringToFront();
            this.Parent?.Focus();
        }
    }
}
