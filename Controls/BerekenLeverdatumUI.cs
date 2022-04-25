using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Forms;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Productie.ArtikelRecords;
using Various;

namespace Controls
{
    public partial class BerekenLeverdatumUI : UserControl
    {
        public string ArtikelNr
        {
            get => xartikelnrTextbox.Text.Trim();
            set => xartikelnrTextbox.Text = value?.Trim();
        }
        public BerekenLeverdatumUI()
        {
            InitializeComponent();
            InitAutoFill();
        }

        private void InitAutoFill()
        {
            try
            {
                xartikelnrTextbox.AutoCompleteCustomSource = new AutoCompleteStringCollection();
                if (Manager.ArtikelRecords is { Disposed: false })
                    xartikelnrTextbox.AutoCompleteCustomSource.AddRange(Manager.ArtikelRecords.GetAllRecords()
                        .Where(x => !x.IsWerkplek).Select(x => x.ArtikelNr).ToArray());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private ArtikelRecord _Artikelrecord;
        private void SetArtikelNr(string artnr)
        {
            try
            {
                _Artikelrecord = Manager.ArtikelRecords?.GetRecord(artnr?.Trim());
                xbeginop.SetValue(DateTime.Now);
                if (_Artikelrecord == null)
                    xaantal.SetValue(0);
                else
                    xaantal.SetValue((int)(_Artikelrecord.AantalGemaakt / _Artikelrecord.UpdatedProducties.Count));
                xgeproduceerd.SetValue(0);
                xperuur.SetValue(_Artikelrecord?.PerUur ?? 0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void SetProductie(Bewerking bew)
        {
            if (bew == null) return;
            ArtikelNr = bew.ArtikelNr;
            if (_Artikelrecord == null)
            {
                _Artikelrecord = new ArtikelRecord
                {
                    ArtikelNr = bew.ArtikelNr,
                    Omschrijving = bew.Omschrijving,
                    AantalGemaakt = bew.Aantal,
                    TijdGewerkt = bew.DoorloopTijd
                };
                _Artikelrecord.UpdatedProducties.Add(bew.ProductieNr);
            }
            
            xbeginop.SetValue(bew.LeverDatum);
            xgewenstleverdatumradio.Checked = true;
            xaantal.SetValue(bew.Aantal);
            xgeproduceerd.SetValue(bew.TotaalGemaakt);
            if (bew.ActueelPerUur <= 0)
                xperuur.SetValue((decimal)bew.PerUur);
            else
                xperuur.SetValue((decimal)bew.ActueelPerUur);
        }

        private void xartikelnrTextbox_TextChanged(object sender, System.EventArgs e)
        {
            xtekeningbutton.Visible = xartikelnrTextbox.Text.Trim().Length > 4;
            SetArtikelNr(xartikelnrTextbox.Text);
        }

        private void xroosterbutton_Click(object sender, EventArgs e)
        {
            if (Manager.Opties == null) return;
            var rooster = Manager.Opties.TijdelijkeRooster;
            var rsform = new RoosterForm(rooster, "Beheer Werk Rooster");
            rsform.ViewPeriode = false;
            rsform.SetRooster(rooster,Manager.Opties?.NationaleFeestdagen,Manager.Opties?.SpecialeRoosters);
            if (rsform.ShowDialog() == DialogResult.OK)
            {
                if (Manager.Opties == null) return;
                Manager.Opties.NationaleFeestdagen = rsform.RoosterUI.NationaleFeestdagen().ToArray();
                Manager.Opties.SpecialeRoosters = rsform.RoosterUI.SpecialeRoosters;
                Manager.Opties.TijdelijkeRooster = rsform.WerkRooster;
                Manager.Opties.Save("Werk Rooster aangepast");
                UpdateHtml();
            }
        }

        private void xtekeningbutton_Click(object sender, EventArgs e)
        {
            ShowWerkTekening();
        }

        private void ShowWerkTekening()
        {
            if (string.IsNullOrEmpty(xartikelnrTextbox.Text.Trim())) return;
            Tools.ShowSelectedTekening(xartikelnrTextbox.Text.Trim(), TekeningClosed);
        }

        private void TekeningClosed(object sender, EventArgs e)
        {
            this.Parent?.Select();
            this.Parent?.BringToFront();
            this.Parent?.Focus();
        }

        private void UpdateHtml()
        {
            try
            {
                var sb = new StringBuilder();

                sb.AppendLine("<ul>");
                var xpers = xaantalpersonen.Value;
                var pu = xperuur.Value;
                var xprod = xaantal.Value - xgeproduceerd.Value;
                var uur = ((xprod / pu) / xpers);
                DateTime gereedop;
                DateTime startop;
                if (xgewenstleverdatumradio.Checked)
                {
                    gereedop = xbeginop.Value;
                    startop = Werktijd.DatumVoorTijd(xbeginop.Value,
                        TimeSpan.FromHours((double)uur), Manager.Opties.GetWerkRooster(),
                        Manager.Opties.SpecialeRoosters);
                }
                else
                {
                    gereedop = Werktijd.DatumNaTijd(xbeginop.Value,
                        TimeSpan.FromHours((double)uur), Manager.Opties.GetWerkRooster(),
                        Manager.Opties.SpecialeRoosters);
                    startop = xbeginop.Value;
                }
              
                sb.AppendLine($"<li>Aantal Personen: <b>{xpers}</b></li>");
                
                sb.AppendLine($"<li>Aantal P/u: <b>{pu} p/u</b></li>");
                sb.AppendLine($"<li>Aantal Produceren: <b>{(int)xaantal.Value} st.</b></li>");
                sb.AppendLine($"<li>Aantal Gemaakt: <b>{(int)xgeproduceerd.Value} st.</b></li>");
                
                sb.AppendLine($"<li>Aantal Resterend: <b>{(int)xprod} st.</b></li>");
                if (xprod > 0 && pu > 0)
                {
                    uur = Math.Round(xprod / pu, 2);
                    sb.AppendLine($"<li>Doorlooptijd (1p): <b>{uur} uur</b></li>");
                    if (xpers > 1)
                    {
                        sb.AppendLine(
                            $"<li>Doorlooptijd ({xpers}p): <b>{Math.Round(((xprod / pu) / xpers), 2)} uur</b></li>");
                    }
                }
                sb.AppendLine($"<li>Beginnen Op: <span color='purple'><u><b>{startop.ToString("f").FirstCharToUpper()} uur</b></u></span></li>");
                sb.AppendLine(
                    $"<li>Gereed Op: <span color='purple'><u><b>{gereedop.ToString("f").FirstCharToUpper()} uur</b></u></span></li>");
                sb.AppendLine("</ul>");
                xinfo.Text = GetHtml(sb.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private string GetHtml(string body)
        {
            string ximage = $"<td width = '32' style = 'padding: 5px 5px 0 0' >\r\n" +
                            $"<img width='{96}' height='{96}'  src = 'BerekenIcon' />\r\n" +
                            $"</td>";
            var xreturn = $"<html>\r\n" +
                          $"<head>\r\n" +
                          $"<style>{IProductieBase.GetStylesheet("StyleSheet")}</style>\r\n" +
                          $"<Title>{xartikelnrTextbox.Text.Trim()}</Title>\r\n" +
                          $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                          $"</head>\r\n" +
                          $"<body style='background - color: {Color.AliceBlue.Name}; background-gradient: {Color.White.Name}; background-gradient-angle: 180; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
                          $"<h1 align='center' style='color: {Color.Navy.Name}'>\r\n" +
                          $"       {_Artikelrecord?.Omschrijving??"Bereken Leverdatum"}\r\n" +
                          $"        <br/>\r\n" +
                          $"        <span style=\'font-size: x-small;\'>ArtikelNr: {ArtikelNr}</span>\r\n " +
                          $"</h1>\r\n" +
                          $"<blockquote class='whitehole'>\r\n" +
                          $"       <p style = 'margin-top: 0px' >\r\n" +
                          $"<table border = '0' width = '100%' >\r\n" +
                          $"<tr style = 'vertical-align: top;' >\r\n" +
                          ximage+
                          $"<td>" +
                          "<div>" + 
                          $"{body}" + 
                          $"<hr />" +
                          "</div>" + 
                          $"</td>" +
                          $"</tr>\r\n" +
                          $"</table >\r\n" +
                          $"</p>\r\n" +
                          $"</blockquote>\r\n" +
                          $"</body>\r\n" +
                          $"</html>";
            return xreturn;
        }

        private void ValueChanged(object sender, EventArgs e)
        {
            UpdateHtml();
        }

        private void xinfo_ImageLoad(object sender, TheArtOfDev.HtmlRenderer.Core.Entities.HtmlImageLoadEventArgs e)
        {
            if (e.Src == "BerekenIcon")
            {
                e.Callback(Resources.calculate_office_icon_256x256);
            }
        }
    }
}
