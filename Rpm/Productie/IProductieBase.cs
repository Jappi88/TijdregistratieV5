using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Polenter.Serialization;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.SqlLite;
using Rpm.Various;

namespace Rpm.Productie
{
    [DataContract]
    public class IProductieBase
    {
        public IProductieBase()
        {
        }

        public virtual UserChange LastChanged { get; set; }
        public virtual ProductieState State { get; set; }
        public virtual int Aantal { get; set; }
        public virtual int AantalGemaakt { get; set; }
        public virtual int AantalTeMaken { get; }
        public virtual int TotaalGemaakt { get; }
        public virtual int AantalNogTeMaken { get; }
        public virtual int AanbevolenPersonen { get; set; }
        public virtual string ArtikelNr { get; set; }
        public virtual string ProductieNr { get; set; }
        public virtual string Naam { get; set; }
        public virtual double DoorloopTijd { get; set; }
        public virtual double GemiddeldDoorlooptijd { get; set; }
        public virtual ProductieFormulier Root { get; }
        public virtual string WerkplekkenName { get; }
        public virtual string PersoneelNamen { get; }
        public virtual DateTime DatumToegevoegd { get; set; } //  datum van het toevoegen van de productie formulier
        public virtual DateTime DatumVerwijderd { get; set; }
        public virtual DateTime DatumGereed { get; set; }
        public virtual DateTime VerwachtLeverDatum { get; set; }
        public virtual DateTime TijdGestart { get; set; }
        public virtual DateTime StartOp { get; set; }
        public virtual DateTime LaatstAantalUpdate { get; set; }
        public virtual DateTime TijdGestopt { get; set; }
        public virtual double TotaalTijdGewerkt { get; set; }
        [ExcludeFromSerialization] public virtual Personeel[] Personen { get; set; }
        public double TijdGewerkt { get; set; }
        public virtual DateTime LeverDatum { get; set; } // de datum waarop de productie klaar moet zijn

        public virtual string Omschrijving { get; set; } //productie omschrijving

        //[ExcludeFromSerialization]
        public virtual NotitieEntry Note { get; set; }

        //[ExcludeFromSerialization]
        public virtual NotitieEntry GereedNote { get; set; }

        [ExcludeFromSerialization] public virtual string Path { get; set; }
        public virtual string Paraaf { get; set; } //De persoon die aftekent
        public virtual bool TeLaat => DateTime.Now > LeverDatum;
        public virtual bool IsNieuw => DatumToegevoegd.AddHours(4) > DateTime.Now;
        public virtual double Gereed { get; set; }
        public virtual int DeelsGereed { get; }
        public virtual double GemiddeldPerUur { get; set; }
        public virtual double GemiddeldActueelPerUur { get; set; }
        public virtual double ActueelPerUur { get; set; }
        public virtual int PerUur => Aantal > 0 && DoorloopTijd > 0 ? (int) (Aantal / DoorloopTijd) : 0;
        public virtual decimal ProcentAfwijkingPerUur => GetAfwijking();
        public virtual decimal GemiddeldProcentAfwijkingPerUur => GetGemiddeldAfwijking();
        public virtual string GestartDoor { get; set; }
        public virtual int Geproduceerd { get; set; }

        private decimal GetAfwijking()
        {
            try
            {
                return (ActueelPerUur - PerUur) == 0 ? 0 : Math.Round((decimal)(((ActueelPerUur - PerUur) / (PerUur == 0? ActueelPerUur :  PerUur)) * 100), 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        private decimal GetGemiddeldAfwijking()
        {
            try
            {
                return (GemiddeldActueelPerUur - GemiddeldPerUur) == 0 ? 0 : Math.Round((decimal)(((GemiddeldActueelPerUur - GemiddeldPerUur) / (GemiddeldPerUur == 0 ? GemiddeldActueelPerUur : GemiddeldPerUur)) * 100), 2);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return 0;
            }
        }

        public virtual List<WerkPlek> GetWerkPlekken()
        {
            return Root?.GetAlleWerkplekken() ?? new List<WerkPlek>();
        }

        public virtual List<Materiaal> GetMaterialen()
        {
            return Root?.Materialen?.Where(x=> x != null).ToList() ?? new List<Materiaal>();
        }

        public virtual Personeel[] GetPersonen(bool onlyactive)
        {
            return Personen.Where(x => x.IngezetAanKlus(Path, onlyactive, out _)).ToArray();
        }

        public virtual Storing[] GetStoringen(bool onlyactive)
        {
            return Root?.GetAlleStoringen(onlyactive).ToArray();
        }

        public virtual async Task<bool> Update(string change, bool save)
        {
            try
            {
                if (this is Bewerking bew)
                    return await bew.UpdateBewerking(null, change, save);
                if (this is ProductieFormulier form)
                    return await form.UpdateForm(true, false, null, change, save);
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }
      
        private Color GetColorByPercentage(decimal percentage)
        {
            if (percentage <= -50)
                return Color.Maroon;
            if (percentage <= -25)
                return Color.Red;
            if (percentage <= -15)
                return Color.DarkOrange;
            if (percentage <= -5)
                return Color.Orange;
            if (percentage <= 5)
                return Color.Green;
            if (percentage >= 5)
                return Color.DarkGreen;
            if (percentage >= 25)
                return Color.RoyalBlue;
            if (percentage >= 50)
                return Color.Purple;
            return Color.Black;
        }

        public static string GetStylesheet(string src)
        {
            if (src == "StyleSheet")
            {
                return @"h1, h2, h3 { color: navy; font:12pt Tahoma; }
                    h1 { margin-bottom: .35em }
                    h2 { margin-bottom: .3em }
                    h3 { margin-bottom: .4em }
                    ul { margin-top: .5em }
                    ul li {margin: .25em}
                    body { font:10pt Tahoma}
		            pre  { border:solid 1px gray; background-color:#eee; padding:1em }
                    a:link { text-decoration: none; }
                    a:hover { text-decoration: underline; }
                    .gray    { color:gray; }
                    .example { background-color:#efefef; corner-radius:5px; padding:0.5em; }
                    .whitehole { background-color:white; corner-radius:10px; padding:5px; }
                    .caption { font-size: 1.1em }
                    .comment { color: green; margin-bottom: 5px; margin-left: 3px; }
                    .comment2 { color: green; }";
            }

            return null;
        }

        public string GetHtmlBody(string title, Bitmap image, Size imagesize, Color backcolor, Color backgroundgradient,
            Color textcolor)
        {
            string ximage = image == null? string.Empty : $"<td width = '32' style = 'padding: 5px 5px 0 0' >\r\n" +
                            $"<img width='{imagesize.Width}' height='{imagesize.Height}'  src = 'data:image/png;base64,{image.Base64Encoded()}' />\r\n" +
                            $"</td>";

            var xreturn = $"<html>\r\n" +
                          $"<head>\r\n" +
                          $"<style>{GetStylesheet("StyleSheet")}</style>\r\n" +
                          $"<Title>{ArtikelNr}</Title>\r\n" +
                          $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                          $"</head>\r\n" +
                          $"<body style='background - color: {backcolor.Name}; background-gradient: {backgroundgradient.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 50px 0px'>\r\n" +
                          $"<h1 align='center' style='color: {textcolor.Name}'>\r\n" +
                          $"       {title}\r\n" +
                          $"        <br/>\r\n" +
                          $"        <span style=\'font-size: x-small;\'>ArtikelNr: {ArtikelNr}, ProductieNr: {ProductieNr}</span>\r\n " +
                          $"</h1>\r\n" +
                          $"<blockquote class='whitehole'>\r\n" +
                          $"       <p style = 'margin-top: 0px' >\r\n" +
                          $"<table border = '0' width = '100%' >\r\n" +
                          $"<tr style = 'vertical-align: top;' >\r\n" +
                          ximage +
                          $"<td>" +
                          $"<h3>\r\n" +
                          $"<h3>{(State != ProductieState.Gereed ? $"Verwachte Leverdatum: <b>{VerwachtLeverDatum:f}</b>" : $"Gereed Gemeld Op: <b>{DatumGereed:f}</b>")}</h3>" +
                          $"Productie Info\r\n" +
                          $"</h3 >\r\n" +
                          $"<div>\r\n" +
                          $"Omschrijving: <b>{Omschrijving}</b><br>" +
                          $"ArtikelNr: <b>{ArtikelNr}</b><br>" +
                          $"ProductieNr: <b>{ProductieNr}</b><br>" +
                          $"Status: <b>{Enum.GetName(typeof(ProductieState), State)}</b><br>" +
                          $"{(State != ProductieState.Gestart? "Laatst ": "")}Gestart Door: <b>{(string.IsNullOrEmpty(GestartDoor)?"Niemand":GestartDoor)}</b><br>" +
                          $"Aantal Gemaakt: <b>{TotaalGemaakt} / {Aantal}</b><br>" +
                          $"Tijd Gewerkt: <b>{TijdGewerkt} uur</b><br>" +
                          $"Aantal Aanbevolen Personen: <b>{AanbevolenPersonen}</b><br>" +
                          $"Per Uur: <b>{ActueelPerUur} i.p.v. {PerUur} <span style = 'color: {GetColorByPercentage(ProcentAfwijkingPerUur).Name}'>({ProcentAfwijkingPerUur}%)</span></b><br>" +
                          $"Gemiddeld Per Uur: <b>{GemiddeldActueelPerUur} i.p.v. {GemiddeldPerUur} <span style = 'color: {GetColorByPercentage(GemiddeldProcentAfwijkingPerUur).Name}'>({GemiddeldProcentAfwijkingPerUur}%)</span></b><br>" +
                          $"Notitie: <b>{Note?.Notitie ?? "Geen notitie."}</b><br>" +
                          $"Gereed Notitie: <b>{GereedNote?.Notitie ?? "Geen notitie."}</b><br>" +
                          $"</div>\r\n" +
                          $"<hr />" +
                          $"<h3>\r\n" +
                          $"Productie Datums\r\n" +
                          $"</h3 >\r\n" +
                          $"<div>\r\n" +
                          $"Leverdatum is Op: <b>{LeverDatum:f}</b><br>" +
                          $"Uiterlijk starten Op: <b>{StartOp:f}</b><br>" +
                          $"Aantal Gewijzigd Op: <b>{LaatstAantalUpdate:f}</b><br>" +
                          $"Toegevoegd Op: <b>{DatumToegevoegd:f}</b><br>" +
                          $"Gestart Op: <b>{TijdGestart:f}</b><br>" +
                          $"Gestopt Op: <b>{(State == ProductieState.Gestart ? "Is nog bezig." : TijdGestopt.ToString("f"))}</b><br>" +
                          $"Gereed Gemeld op: <b>{(State != ProductieState.Gereed ? "Nog niet gereed gemeld." : (DatumGereed.ToString("f")))}</b><br>" +
                          $"<hr />" +
                          string.Join("<br>", GetWerkPlekken().Select(x =>
                              ($"<h3>{x.Naam}</h3>" +
                               $"<div>" +
                               $"Medewerkers: <div><b>{string.Join("<br>", x.Personen.Select(p => p.PersoneelNaam + $"({p.TotaalTijdGewerkt} Uur gewerkt)"))}</b><br></div>" +
                               $"Aantal Gemaakt: <b>{x.AantalGemaakt} / {Aantal}</b><br>" +
                               $"Actueel Per Uur: <b>{x.PerUur} i.p.v. {x.PerUurBase}</b><br>" +
                               $"Notitie: <b>{x.Note?.Notitie ?? "Geen notitie."}</b><br>" +
                               $"</div>"))) +
                          $"</div>\r\n" +
                          $"<hr />" +
                          $"<h3>Materialen Verbruik</h3>" +
                          string.Join("<br>", GetMaterialen().Select(x =>
                              ($"<div Color=RoyalBlue>[{x.ArtikelNr}] {x.Omschrijving}</div>" +
                               $"<div>Locatie: <b>{x.Locatie}</b></div>" +
                               $"<div>Verbuik Per Eenheid: <b>{x.AantalPerStuk} {(x.Eenheid.ToLower() == "m" ? "meter" : x.Eenheid)}</b></div>" +
                               $"<div>Verbuik: <b>{TotaalGemaakt * x.AantalPerStuk} {(x.Eenheid.ToLower() == "m" ? "meter" : x.Eenheid)}</b></div>" +
                               $"<div>Aantal Afkeur: <b>{x.AantalAfkeur} {(x.Eenheid.ToLower() == "m" ? "meter" : x.Eenheid)} ({x.AfKeurProcent()})</b></div>"))) +
                          $"<hr />" +
                          $"</td>" +
                          $"</tr>\r\n" +
                          $"</table >\r\n" +
                          $"</p>\r\n" +
                          $"</blockquote>\r\n" +
                          $"</body>\r\n" +
                          $"</html>";
            return xreturn;
        }

        public Bitmap GetImageFromResources()
        {
            Bitmap img = this is ProductieFormulier ? Resources.page_document_16748_128_128 : Resources.operation;
            switch (State)
            {
                case ProductieState.Gestopt:
                    if (IsNieuw)
                        img = img.CombineImage(Resources.new_25355, 2);
                    else if (TeLaat)
                        img = img.CombineImage(Resources.Warning_36828, 2);
                    break;
                case ProductieState.Gestart:
                    img = img.CombineImage(Resources.play_button_icon_icons_com_60615, 2);
                    break;
                case ProductieState.Gereed:
                    img = img.CombineImage(Resources.check_1582, 2);
                    break;
                case ProductieState.Verwijderd:
                    img = img.CombineImage(Resources.delete_1577, 2);
                    break;
            }

            return img;
        }
    }
}