using Polenter.Serialization;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.SqlLite;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using ProductieManager.Rpm.SqlLite;
using Rpm.Misc;

namespace Rpm.Productie
{
    public class IProductieBase : IDbLogging
    {
        [Display(Name = "Root", Description = "BASIS formulier waarvan de informatie wordt gehaald")]
        public virtual ProductieFormulier Root { get; }

        [Display(Name = "LastChanged", Description = "Informatie van de laatste wijzigingen")]
        public virtual UserChange LastChanged { get; set; }

        [Display(Name = "State", Description = "Huidige productie status")]
        public virtual ProductieState State { get; set; }

        [Display(Name = "Aantal", Description = "Totaal aantal dat geproduceerd moet worden")]
        public virtual int Aantal { get; set; }

        [Display(Name = "Eenheid", Description = "Productie eenheid")]
        public virtual string Eenheid { get; set; }

        [Display(Name = "GemiddeldAantalGemaakt",
            Description = "Gemiddelde Aantal gemaakt van alle bewerkingen van dit ArtikelNr")]
        public virtual int GemiddeldAantalGemaakt { get; set; }

        [Display(Name = "AantalGemaakt", Description = "Aantal gemaakt waarvan de deels gereedmeldingen verekend zijn")]
        public virtual int AantalGemaakt { get; set; }

        [Display(Name = "AantalTeMaken",
            Description = "Aantal om te produceren waarvan de deels gereedmeldingen verekend zijn")]
        public virtual int AantalTeMaken { get; }

        [Display(Name = "TotaalGemaakt", Description = "Aantal gemaakt inclusief alle deels gereedmeldingen")]
        public virtual int TotaalGemaakt { get; }

        [Display(Name = "AantalNogTeMaken", Description = "Aantal om nog te produceren")]
        public virtual int AantalNogTeMaken { get; }

        [Display(Name = "AanbevolenPersonen", Description = "Aantal aanbevolen mensen om in te zetten")]
        public virtual int AanbevolenPersonen { get; set; }

        [Display(Name = "ArtikelNr", Description = "ArtikelNr, een nummer waarvan een materiaal wordt aangeduid")]
        public virtual string ArtikelNr { get; set; }

        [Display(Name = "ProductieNr", Description = "Een unieke productie nummer")]
        public virtual string ProductieNr { get; set; }

        [Display(Name = "Naam",
            Description =
                "Een naam van de huidige productie. Een Formulier zal een ProductieNr weergeven, een bewerking de BewerkingNaam")]
        public virtual string Naam { get; set; }

        [Display(Name = "DoorloopTijd",
            Description = "Tijd wat op de Productieformulier staat dat nodig is voor de productie")]
        public virtual double DoorloopTijd { get; set; }

        [Display(Name = "GemiddeldDoorlooptijd",
            Description = "DoorloopTijd berekent op basis van de Aantal om te maken en de Gemiddelde Aantal Per Uur")]
        public virtual double GemiddeldDoorlooptijd { get; set; }

        [Display(Name = "Activiteit",
            Description =
                "Het aantal procent van hoe actief de productie is. hoe lager de percentage, hoe minder tijd gewerkt")]
        public virtual double Activiteit { get; } = 100;
        //[Display(Name = "VerpakkingsInstructies", Description = "VerpakkingsInformatie over hoe de geproduceerde product verpakt moet worden")]
        //[ExcludeFromSerialization]
        //public virtual VerpakkingInstructie VerpakkingInstries
        //{
        //    get => _verpakking;
        //    set => _verpakking = value;
        //}

        [Display(Name = "VerpakkingsInstructies",
            Description = "VerpakkingsInformatie over hoe de geproduceerde product verpakt moet worden")]
        public virtual VerpakkingInstructie VerpakkingsInstructies { get; set; }

        [Display(Name = "VerpakkingsType", Description = "Verpakkingtype van het geproduceerde product")]
        public virtual string VerpakkingsType => VerpakkingsInstructies?.VerpakkingType ?? "n.v.t.";

        [Display(Name = "PalletSoort", Description = "Palletsoort dat gebruikt dient te worden")]
        public virtual string PalletSoort => VerpakkingsInstructies?.PalletSoort ?? "n.v.t.";

        [Display(Name = "BulkLocatie", Description = "Bulk Locatie voor het geproduceerde product")]
        public virtual string BulkLocatie => VerpakkingsInstructies?.BulkLocatie ?? "n.v.t";

        [Display(Name = "StandaardLocatie", Description = "Standaard Locatie voor het geproduceerde product")]
        public virtual string StandaardLocatie => VerpakkingsInstructies?.StandaardLocatie ?? "n.v.t.";

        [Display(Name = "WerkplekkenName", Description = "Namen van de gebruikte werkplaatsen")]
        public virtual string WerkplekkenName { get; }

        [Display(Name = "PersoneelNamen", Description = "Namen van de personen die aan deze productie hebben gewerkt")]
        public virtual string PersoneelNamen { get; }

        [Display(Name = "DatumToegevoegd", Description = "Datum waarvan de productie is toegevoegd/aangemaakt")]
        public virtual DateTime DatumToegevoegd { get; set; } = DateTime.Now;

        //  datum van het toevoegen van de productie formulier
        [Display(Name = "DatumVerwijderd", Description = "Datum waarvan de productie eventueel verwijderd is")]
        public virtual DateTime DatumVerwijderd { get; set; }

        [Display(Name = "DatumGereed", Description = "Datum waarvan de productie eventueel gereed is")]
        public virtual DateTime DatumGereed { get; set; }

        [Display(Name = "VerwachtLeverDatum", Description = "Datum waarvan wordt verwacht dat de productie klaar is")]
        public virtual DateTime VerwachtLeverDatum { get; set; }

        [Display(Name = "TijdGestart", Description = "Datum en tijd waarvan de productie eventueel is gestart")]
        public virtual DateTime TijdGestart { get; set; }

        [Display(Name = "StartOp", Description = "Datum en tijd waarop wordt aangeraden om de productie te starten")]
        public virtual DateTime StartOp { get; set; }

        [Display(Name = "LaatstAantalUpdate", Description = "Datum en tijd waarop de aantal laatst gewijzigd is")]
        public virtual DateTime LaatstAantalUpdate { get; set; }

        [Display(Name = "TijdGestopt", Description = "Datum en tijd waarop is gestopt")]
        public virtual DateTime TijdGestopt { get; set; }

        [Display(Name = "TotaalTijdGewerkt", Description = "Totale tijd gewerkt van alle producties met dit ArtikelNr")]
        public virtual double TotaalTijdGewerkt { get; set; }

        [Display(Name = "Personen", Description = "Personen ingezet die werken aan deze productie")]
        [ExcludeFromSerialization]
        public virtual Personeel[] Personen { get; set; }

        [Display(Name = "TijdGewerkt", Description = "Tijd gewerkt aan deze productie")]
        public virtual double TijdGewerkt { get; set; }

        [Display(Name = "TijdNodig",
            Description = "TijdNodig is berekent op basis van de aantal nog te maken en de actuele aantal per uur")]

        public virtual double TijdNodig
        {
            get
            {
                if (this is Bewerking bew)
                    return bew.GetTijdNodig();
                if (this is ProductieFormulier form)
                    return Math.Round(form.GetTijdNodig().TotalHours, 2);
                return DoorloopTijd;
            }
        }

        [Display(Name = "TijdOver",
            Description =
                "Tijd dat nog over is op basis van de aantal nog te maken en de actuele aantal per uur, gedeeld door de aantal actieve medewerkers.")]
        public virtual double TijdOver
        {
            get
            {
                if (this is Bewerking bew)
                    return bew.GetTijdOver();
                if (this is ProductieFormulier form)
                    return Math.Round(form.GetTijdOver().TotalHours, 2);
                return DoorloopTijd;
            }
        }

        [Display(Name = "LeverDatum", Description = "Datum waarvan de productie gereed moet zijn")]
        public virtual DateTime LeverDatum { get; set; } // de datum waarop de productie klaar moet zijn

        [Display(Name = "Omschrijving", Description = "Product omschrijving")]
        public virtual string Omschrijving { get; set; } //productie omschrijving

        [Display(Name = "Opmerking", Description = "Productie opmerking")]
        public virtual string Opmerking { get; set; }

        //[ExcludeFromSerialization]
        [Display(Name = "Note", Description = "Notitie gegevens")]
        public virtual NotitieEntry Note { get; set; }


        //[ExcludeFromSerialization]
        [Display(Name = "GereedNote", Description = "Gereed notitie")]
        public virtual NotitieEntry GereedNote { get; set; }


        [Display(Name = "Path", Description = "Aanduidig waar de productie zich begeeft in de formulier")]
        [ExcludeFromSerialization]
        public virtual string Path { get; set; }

        [Display(Name = "Paraaf", Description = "Persoon die heeft gereed gemeld")]
        public virtual string Paraaf { get; set; } //De persoon die aftekent

        [Display(Name = "TeLaat", Description = "True als de productie te laat is, anders FALSE")]
        public virtual bool TeLaat => DateTime.Now > LeverDatum;

        [Display(Name = "IsNieuw", Description = "True voor als de productie nieuw is")]
        public virtual bool IsNieuw => DatumToegevoegd.AddHours((Manager.Opties?.NieuwTijd ?? 4)) > DateTime.Now;

        [Display(Name = "Gereed", Description = "Aantal percentage gereed")]
        public virtual double Gereed { get; set; }

        [Display(Name = "TijdGewerktPercentage", Description = "Procent TijdGewerkt t.o.v. de Doorlooptijd")]
        public virtual double TijdGewerktPercentage { get; set; }

        [Display(Name = "DeelsGereed", Description = "Aantal deels gereed gemeld")]
        public virtual int DeelsGereed { get; }

        private double _GemiddeldPerUur { get; set; }
        [Display(Name = "GemiddeldPerUur",
            Description = "Gemiddelde van de basis p/u volgens alle producties met dit ArtikelNr")]
        [ExcludeFromSerialization]
        public virtual double GemiddeldPerUur
        {
            get => _GemiddeldPerUur <= 0 ? PerUur : _GemiddeldPerUur;
            set => _GemiddeldPerUur = value;
        }

       
        private double _GemiddeldActueelPerUur { get; set; }
        [Display(Name = "GemiddeldActueelPerUur",
            Description = "Gemiddelde GEMETEN aantal per uur volgens alle producties met dit ArtikelNr")]
        [ExcludeFromSerialization]
        public virtual double GemiddeldActueelPerUur
        {
            get => _GemiddeldActueelPerUur <= 0 ? ActueelPerUur : _GemiddeldActueelPerUur;
            set => _GemiddeldActueelPerUur = value;
        }
        [Display(Name = "ActueelPerUur", Description = "Gemeten aantal per uur voor deze productie")]
        public virtual double ActueelPerUur { get; set; }

        [Display(Name = "PerUur", Description = "Aantal per uur volgens de ProductieFormulier")]
        public virtual double PerUur => Aantal > 0 && DoorloopTijd > 0 ? (int) (Aantal / DoorloopTijd) : 0;

        [Display(Name = "ProcentAfwijkingPerUur",
            Description = "Aantal procent afwijking tussen de gemeten en de basis aantal per uur")]
        public virtual decimal ProcentAfwijkingPerUur => GetAfwijking();

        [Display(Name = "GemiddeldProcentAfwijkingPerUur",
            Description = "Aantal procent afwijking tussen de gemiddelde gemeten en de basis aantal per uur")]
        public virtual decimal GemiddeldProcentAfwijkingPerUur => GetGemiddeldAfwijking();

        [Display(Name = "GestartDoor", Description = "Naam/Afdeling die de productie heeft gestart")]
        public virtual string GestartDoor { get; set; }

        [Display(Name = "Geproduceerd", Description = "Aantal keer waarvan deze productie totaal is geproduceert")]
        public virtual int Geproduceerd { get; set; }

        [Display(Name = "ProductSoort", Description = "Soort product zoals: 'Solar','Horti' of 'Techno'")]
        public virtual string ProductSoort { get; set; }

        [Display(Name = "ControlePunten", Description = "Punten waar je vooral op moet controleren")]
        public virtual string ControlePunten { get; set; }

        [Display(Name = "ControleRatio", Description = "Controle Ratio, Percentage van hoevaak de aantallen worden door gegeven op basis van 1.5 uur")]
        public virtual double ControleRatio
        {
            get
            {
                if (this is Bewerking bew) return bew.GetControleRatio();
                if (this is ProductieFormulier prod) return prod.GetControleRatio();
                return 0;
            }
        }

        [Display(Name = "ActueelAantalGemaakt",
            Description =
                "Het aantal dat gemaakt zou moeten zijn berekent op basis van de door gegeven aantallen.")]
        public virtual int ActueelAantalGemaakt => GetActueelAantalGemaakt();

        public virtual int GetActueelAantalGemaakt(ref double tijd)
        {
            tijd += TijdGewerkt;
            return AantalGemaakt;
        }

        public int GetActueelAantalGemaakt()
        {
            double xtijd = 0;
            return GetActueelAantalGemaakt(ref xtijd);
        }

        public bool HeeftGewerkt(TijdEntry bereik)
        {
            try
            {
                if (this is Bewerking bew)
                {
                    return bew.WerkPlekken?.Any(x => x.HeeftGewerkt(bereik))??false;
                }

                if (this is ProductieFormulier prod)
                {
                    return prod.Bewerkingen?.Any(x => x.HeeftGewerkt(bereik)) ?? false;
                }

                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public decimal GetAfwijking()
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

        public decimal GetGemiddeldAfwijking()
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

        public virtual Bewerking[] GetBewerkingen()
        {
            return Root?.Bewerkingen ?? new Bewerking[] { };
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
            if (this is Bewerking bew)
                return bew.GetAlleStoringen(onlyactive);
            if (this is ProductieFormulier prod)
                return prod.GetAlleStoringen(onlyactive).ToArray();
            return new Storing[] { };
        }

        public virtual Task<bool> Update(string change, bool save, bool raiseevent)
        {
            try
            {
                if (this is Bewerking bew)
                    return bew.UpdateBewerking(null, change, save);
                if (this is ProductieFormulier form)
                    return form.UpdateForm(true, false, null, change, save,save,raiseevent);
                return Task.FromResult<bool>(false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return Task.FromResult<bool>(false);
            }
        }
      
        public static Color GetNegativeColorByPercentage(decimal percentage)
        {
            if (percentage <= -25)
                return Color.Red;
            if (percentage <= -15)
                return Color.DarkOrange;
            if (percentage <= -5)
                return Color.Orange;
            if (percentage <= 5)
                return Color.Green;
            if (percentage <= 15)
                return Color.DarkGreen;
            if (percentage <= 50)
                return Color.HotPink;
            if (percentage > 50)
                return Color.RoyalBlue;
            return Color.Black;
        }

        public static Color GetPositiveColorByPercentage(decimal percentage)
        {
            if (percentage <= 25)
                return Color.Red;
            if (percentage <= 50)
                return Color.DarkOrange;
            if (percentage <= 75)
                return Color.Orange;
            if (percentage <= 100)
                return Color.Green;
            if (percentage <= 105)
                return Color.DarkGreen;
            if (percentage <= 125)
                return Color.HotPink;
            if (percentage > 125)
                return Color.RoyalBlue;
            return Color.Black;
        }

        public static Color GetValidColor(bool valid)
        {
            if (valid)
                return Color.DarkGreen;
            return Color.Red;
        }

        public static Color GetProductSoortColor(string soort)
        {
            if(string.IsNullOrEmpty(soort))return Color.Black;
            switch (soort.ToLower())
            {
                case "horti":
                    return Color.FromArgb(9, 122, 192);
                case "solar":
                    return Color.FromArgb(251, 186, 40);
                case "techno":
                    return Color.FromArgb(54, 162, 63);
                case "red":
                    return Color.FromArgb(199, 17, 39);
            }
            return Color.Black;
        }

        public static short GetProductieStateExcelColorIndex(ProductieState state)
        {
            switch (state)
            {
                case ProductieState.Gestopt:
                    return IndexedColors.LightYellow.Index;
                case ProductieState.Gestart:
                    return IndexedColors.LightCornflowerBlue.Index;
                case ProductieState.Gereed:
                    return IndexedColors.LightGreen.Index;
                case ProductieState.Verwijderd:
                    return IndexedColors.LightOrange.Index;
            }

            return IndexedColors.White.Index;
        }

        public static Color GetProductieStateColor(ProductieState state)
        {
            switch (state)
            {
                case ProductieState.Gestopt:
                    return Color.LightYellow;
                case ProductieState.Gestart:
                    return Color.FromArgb(255, 235, 255);
                case ProductieState.Gereed:
                    return Color.FromArgb(200, 255, 200);
                case ProductieState.Verwijderd:
                    return Color.Goldenrod;
            }

            return Color.White;
        }

        public static string GetStylesheet(string src)
        {
            if (src == "StyleSheet")
            {
                return @"h1, h2, h3 { color: navy}
                    h1 { margin-bottom: .3em; font:12pt Tahoma}
                    h2 { margin-bottom: .3em; font:10pt Tahoma}
                    h3 { margin-bottom: .4em; font:10pt Tahoma }
                    ul { margin-top: .5em }
                    ul li {margin: .5em}
                    body { font:10pt Tahoma}
		            pre  { border:solid 1px gray; background-color:#eee; padding:1em }
                    a:link { text-decoration: none; }
                    a:hover { text-decoration: underline; }
                    .gray    { color:gray; }
                    .example { background-color:#efefef; corner-radius:5px; padding:0.5em; }
                    .whitehole { background-color:white; corner-radius:10px; padding:0px; }
                    .caption { font-size: 1.1em }
                    .comment { color: green; margin-bottom: 5px; margin-left: 3px; }
                    .comment2 { color: green; }";
            }
            if (src == "StyleSheet1")
            {
                return @"h1, h2, h3 { color: navy}
                    h1 { margin-bottom: .3em; font:18pt Tahoma}
                    h2 { margin-bottom: .3em; font:14pt Tahoma}
                    h3 { margin-bottom: .4em; font:10pt Tahoma }
                    ul { margin-top: .5em }
                    ul li {margin: .5em}
                    body { font:10pt Tahoma}
		            pre  { border:solid 1px gray; background-color:#eee; padding:1em }
                    a:link { text-decoration: none; }
                    a:hover { text-decoration: underline; }
                    .gray    { color:gray; }
                    .example { background-color:#efefef; corner-radius:5px; padding:0.5em; }
                    .whitehole { background-color:white; corner-radius:10px; padding:0px; }
                    .caption { font-size: 1.1em }
                    .comment { color: green; margin-bottom: 5px; margin-left: 3px; }
                    .comment2 { color: green; }";
            }

            return null;
        }

        public string GetVerpakkingHtmlText(VerpakkingInstructie verpakking, string title, Color backcolor, Color backgroundgradient,
            Color textcolor, bool useimage)
        {
            var x = verpakking ?? VerpakkingsInstructies?? new VerpakkingInstructie();
            return x.CreateHtmlText(title, backcolor, backgroundgradient, textcolor, useimage);
        }

        public string GetProductieInfoHtml(string title, Color backcolor, Color backgroundgradient,
            Color textcolor, bool useimage)
        {
            string ximage = $"<td width = '32' style = 'padding: 5px 5px 0 0' >\r\n" +
                            $"<img width='{64}' height='{64}'  src = 'ProductieInfoicon' />\r\n" +
                            $"</td>";
            if (!useimage) ximage = "";
            var xreturn = $"<html>\r\n" +
              $"<head>\r\n" +
              $"<style>{GetStylesheet("StyleSheet")}</style>\r\n" +
              $"<Title>{title}</Title>\r\n" +
              $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
              $"</head>\r\n" +
              $"<body style='background - color: {backcolor.Name}; background-gradient: {backgroundgradient.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
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
              $"<div>\r\n" +
              $"ArtikelNr: <b>{ArtikelNr}</b>, ProductieNr: <b>{ProductieNr}</b><br>" +
              $"Omschrijving: <b>{Omschrijving?.Replace("\n", " <br> ")}</b><br><br>" +
              $"Status: <b>{Enum.GetName(typeof(ProductieState), State)}</b><br>" +
              $"{(State != ProductieState.Gestart ? "Laatst " : "")}Gestart Door: <b>{(string.IsNullOrEmpty(GestartDoor) ? "Niemand" : GestartDoor)}</b><br>" +
              $"Aantal Gemaakt: <b>{TotaalGemaakt} / {Aantal} ({Gereed}%)</b><br>" +
              $"Per Uur: <b>{ActueelPerUur} i.p.v. {PerUur} P/u <span style = 'color: {GetNegativeColorByPercentage(ProcentAfwijkingPerUur).Name}'>({ProcentAfwijkingPerUur}%)</span></b><br>" +
              $"Gemiddeld Per Uur: <b>{GemiddeldActueelPerUur} i.p.v. {GemiddeldPerUur} <span style = 'color: {GetNegativeColorByPercentage(GemiddeldProcentAfwijkingPerUur).Name}'>({GemiddeldProcentAfwijkingPerUur}%)</span></b><br>" +
              $"Tijd Gewerkt: <b>{TijdGewerkt} uur</b><br>" +
              $"{CombiesHtml()}" +
              $"Aantal Aanbevolen Personen: <b>{AanbevolenPersonen}</b><br>" +
              $"Opmerking: <b>{Opmerking?.Replace("\n", "<br>") ?? "Geen Opmerking."}</b><br><br>" +
              $"</div>\r\n" +
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

        public static string CreateHtmlMessage(string message, string title, Color txtcolor, Color backcolor,
            Color backgradient, string imgname, Size imgsize)
        {
            string ximage = $"<td width = '{imgsize.Width}' style = 'padding: 5px 5px 0 0' >\r\n" +
                           $"<img width='{imgsize.Width}' height='{imgsize.Height}'  src = '{imgname}' />\r\n" +
                           $"</td>";
            if (string.IsNullOrEmpty(imgname)) ximage = "";
            var xreturn = $"<html>\r\n" +
              $"<head>\r\n" +
              $"<style>{GetStylesheet("StyleSheet1")}</style>\r\n" +
              $"<Title>{title}</Title>\r\n" +
              $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
              $"</head>\r\n" +
              $"<body style='background - color: {backcolor.Name}; background-gradient: {backgradient.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
              $"<h1 align='center' style='color: {txtcolor.Name}'>\r\n" +
              $"       {title}\r\n" +
              $"</h1>\r\n" +
              $"<blockquote class='whitehole'>\r\n" +
              $"       <p style = 'margin-top: 0px' >\r\n" +
              $"<table border = '0' width = '100%' >\r\n" +
              $"<tr style = 'vertical-align: top;' >\r\n" +
              ximage +
              $"<td>" +
              $"<div>\r\n" +
              $"{message}"+
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

        public string CombiesHtml()
        {
            try
            {
                if (this is Bewerking {Combies: {Count: > 0}} bew)
                {
                    var xvalue = $"<ul>" +
                                 $"{string.Join("\n", bew.Combies.Select(x => $"<li>Combinatie van {x.Activiteit}% tussen '{x.Periode.Start:M-d-yy HH:mm}' en '{(x.IsRunning ? DateTime.Now : x.Periode.Stop):M-d-yy HH:mm}'</li>"))}";

                    xvalue += $"<li>Huidige Activiteit: <u>{bew.Activiteit}%</u></li>";
                    
                    xvalue += "</ul>";
                    return xvalue;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return $"";
        }


        public string GetMaterialenHtml(string title, Color backcolor, Color backgroundgradient,
    Color textcolor, bool useimage)
        {
            string ximage = $"<td width = '32' style = 'padding: 5px 5px 0 0' >\r\n" +
                            $"<img width='{64}' height='{64}'  src = 'Materialenicon' />\r\n" +
                            $"</td>";
            if (!useimage) ximage = "";
            var xreturn = $"<html>\r\n" +
              $"<head>\r\n" +
              $"<style>{GetStylesheet("StyleSheet")}</style>\r\n" +
              $"<Title>{title}</Title>\r\n" +
              $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
              $"</head>\r\n" +
              $"<body style='background - color: {backcolor.Name}; background-gradient: {backgroundgradient.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
              $"<h1 align='center' style='color: {textcolor.Name}'>\r\n" +
              $"       {title}\r\n" +
              $"        <br/>\r\n" +
              $"        <span style='font-size: x-small;'>ArtikelNr: {ArtikelNr}, ProductieNr: {ProductieNr}</span>\r\n " +
              $"</h1>\r\n" +
              $"<blockquote class='whitehole'>\r\n" +
              $"       <p style = 'margin-top: 0px' >\r\n" +
              $"<table border = '0' width = '100%' >\r\n" +
              $"<tr style = 'vertical-align: top;' >\r\n" +
              ximage + 
              $"<td>" +
              $"<ul>\r\n" +
              string.Join("<br>", GetMaterialen().Select(x =>
                  ($"<li>" +
                   $"<div Color=RoyalBlue>[{x.ArtikelNr}] {x.Omschrijving}</div>" +
                   $"<div>Locatie: <b>{x.Locatie}</b></div>" +
                   $"<div>Verbuik Per Eenheid: <b>{Math.Round(x.AantalPerStuk, 4)} {(x.Eenheid.ToLower() == "m" ? "meter" : x.Eenheid)}</b></div>" +
                   $"<div>Verbuikt: <b>{Math.Round(TotaalGemaakt * x.AantalPerStuk, 4)} {(x.Eenheid.ToLower() == "m" ? "meter" : x.Eenheid)}</b></div>" +
                   $"<div>Totaal Nodig: <b>{Math.Round(Aantal * x.AantalPerStuk, 4)} {(x.Eenheid.ToLower() == "m" ? "meter" : x.Eenheid)}</b></div>" +
                   $"<div>Aantal Afkeur: <b>{Math.Round(x.AantalAfkeur, 4)} {(x.Eenheid.ToLower() == "m" ? "meter" : x.Eenheid)} ({x.AfKeurProcent()})</b></div>" +
                   $"</li>"))) +
              $"</ul>\r\n" +
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

        public string GetNotitiesHtml(string title, Color backcolor, Color backgroundgradient,
            Color textcolor, bool useimage)
        {
            string ximage = $"<td width = '32' style = 'padding: 5px 5px 0 0' >\r\n" +
                            $"<img width='{64}' height='{64}'  src = 'NotitiesIcon' />\r\n" +
                            $"</td>";
            if (!useimage) ximage = "";
            var xreturn = $"<html>\r\n" +
                          $"<head>\r\n" +
                          $"<style>{GetStylesheet("StyleSheet")}</style>\r\n" +
                          $"<Title>{title}</Title>\r\n" +
                          $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                          $"</head>\r\n" +
                          $"<body style='background - color: {backcolor.Name}; background-gradient: {backgroundgradient.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
                          $"<h1 align='center' style='color: {textcolor.Name}'>\r\n" +
                          $"       {title}\r\n" +
                          $"        <br/>\r\n" +
                          $"        <span style=\'font-size: x-small;\'>ArtikelNr: {ArtikelNr}, ProductieNr: {ProductieNr}</span>\r\n " +
                          $"</h1>\r\n" +
                          $"<blockquote class='whitehole'>\r\n" +
                          $"       <p style = 'margin-top: 0px' >\r\n" +
                          $"<table border = '0' width = '100%' >\r\n" +
                          $"<tr style = 'vertical-align: top;' >\r\n" +
                          ximage+
                          $"<td>" +
                          $"<div>\r\n" +
                          $"Notitie: <b>{Note?.Notitie?.Replace("\n", "<br>") ?? "Geen Notitie."}</b><br>" +
                          $"Gereed Notitie: <b>{GereedNote?.Notitie?.Replace("\n", "<br>") ?? "Geen Gereed Notitie."}</b><br>" +
                          $"</div>\r\n" +
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

        public string GetDatumsHtml(string title, Color backcolor, Color backgroundgradient,
    Color textcolor, bool useimage)
        {
            string ximage = $"<td width = '32' style = 'padding: 5px 5px 0 0' >\r\n" +
                            $"<img width='{64}' height='{64}'  src = 'Datumsicon' />\r\n" +
                            $"</td>";
            if (!useimage) ximage = "";
            var xreturn = $"<html>\r\n" +
                          $"<head>\r\n" +
                          $"<style>{GetStylesheet("StyleSheet")}</style>\r\n" +
                          $"<Title>{title}</Title>\r\n" +
                          $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                          $"</head>\r\n" +
                          $"<body style='background - color: {backcolor.Name}; background-gradient: {backgroundgradient.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
                          $"<h1 align='center' style='color: {textcolor.Name}'>\r\n" +
                          $"       {title}\r\n" +
                          $"        <br/>\r\n" +
                          $"        <span style='font-size: x-small;'>ArtikelNr: {ArtikelNr}, ProductieNr: {ProductieNr}</span>\r\n " +
                          $"</h1>\r\n" +
                          $"<blockquote class='whitehole'>\r\n" +
                          $"       <p style = 'margin-top: 0px' >\r\n" +
                          $"<table border = '0' width = '100%' >\r\n" +
                          $"<tr style = 'vertical-align: top;' >\r\n" +
                          ximage +
                          $"<td>" +
                          $"<ul>\r\n" +
                          $"<li>Leverdatum is Op: <b>{LeverDatum:f}</b></li>" +
                          $"<li>Uiterlijk starten Op: <b>{StartOp:f}</b></li>" +
                          $"<li>Aantal Gewijzigd Op: <b>{LaatstAantalUpdate:f}</b></li>" +
                          $"<li>Toegevoegd Op: <b>{DatumToegevoegd:f}</b></li>" +
                          $"<li>Gestart Op: <b>{TijdGestart:f}</b></li>" +
                          $"<li>Gestopt Op: <b>{(State == ProductieState.Gestart ? "Is nog bezig." : TijdGestopt.ToString("f"))}</b></li>" +
                          $"<li>Gereed Gemeld op: <b>{(State != ProductieState.Gereed ? "Nog niet gereed gemeld." : (DatumGereed.ToString("f")))}</b></li>" +
                          $"</ul>\r\n" +
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

        public string GetWerkplekkenHtml(string title, Color backcolor, Color backgroundgradient,
Color textcolor, bool useimage)
        {
            string ximage = $"<td width = '32' style = 'padding: 5px 5px 0 0' >\r\n" +
                            $"<img width='{64}' height='{64}'  src = 'WerkplaatsenIcon' />\r\n" +
                            $"</td>";
            if (!useimage) ximage = "";
            var xreturn = $"<html>\r\n" +
                          $"<head>\r\n" +
                          $"<style>{GetStylesheet("StyleSheet")}</style>\r\n" +
                          $"<Title>{ArtikelNr}</Title>\r\n" +
                          $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                          $"</head>\r\n" +
                          $"<body style='background - color: {backcolor.Name}; background-gradient: {backgroundgradient.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
                          $"<h1 align='center' style='color: {textcolor.Name}'>\r\n" +
                          $"      {title}\r\n" +
                          $"        <br/>\r\n" +
                          $"       {(this is Bewerking ? Naam + "<br>" : "")}\r\n" +
                          $"        <span style=\'font-size: x-small;\'>ArtikelNr: {ArtikelNr}, ProductieNr: {ProductieNr}</span>\r\n " +
                          $"</h1>\r\n" +
                          $"<blockquote class='whitehole'>\r\n" +
                          $"       <p style = 'margin-top: 0px' 'margin-bottom: 0px'>\r\n" +
                          $"<table border = '0' width = '100%' >\r\n" +
                          $"<tr style = 'vertical-align: top;' >\r\n" +
                          ximage +
                          $"<td>" +
                          $"<div>\r\n" +
                          string.Join("<br>", GetWerkPlekken().Select(x =>
                              ($"<h3>{x.Naam}</h3>" +
                               $"<div>" +
                               $"Medewerkers: <div><b>{string.Join("<br>", x.Personen.Select(p => p.PersoneelNaam + $"({p.TotaalTijdGewerkt} Uur gewerkt)"))}</b><br></div>" +
                               $"Aantal Gemaakt: <b>{x.AantalGemaakt} / {Aantal}</b><br>" +
                               $"Actueel Per Uur: <b>{x.PerUur} i.p.v. {x.PerUurBase}</b><br>" +
                               $"Notitie: <b>{x.Note?.Notitie?.Replace("\n", "<br>") ?? "Geen Notitie."}</b><br>" +
                               $"</div>"))) +
                          
                          "</div>" +
                          $"<hr />" +
                          "</td>" +
                          $"</tr>\r\n" +
                          $"</table >\r\n" +
                          $"</p>\r\n" +
                          $"</blockquote>\r\n" +
                          $"</body>\r\n" +
                          $"</html>";
            return xreturn;
        }

        private string GetBewerkingenGemaaktHtml()
        {
            var bws = GetBewerkingen().Where(x => !x.Equals(this)).ToArray();
            if (bws.Length == 0) return string.Empty;
            return $"" + 
                string.Join("\n", bws.Select(x => $"<li><span style = 'color:purple'>{x.Naam}=> <b>{x.TotaalGemaakt}/{x.Aantal}</b>" +
                $" <span style = 'color: {GetPositiveColorByPercentage((decimal)x.Gereed).Name}'>({x.Gereed}%)</span>" +
                $"</span></li>"))
                + "<br>";
        }

        public string GetHeaderHtmlBody(string title, Bitmap image, Size imagesize, Color backcolor,
            Color backgroundgradient,
            Color textcolor, bool useimage)
        {
            string ximage = image == null || !useimage
                ? string.Empty
                : $"<td width = '32' style = 'padding: 5px 5px 0 0' >\r\n" +
                  $"<img width='{imagesize.Width}' height='{imagesize.Height}'  src = 'data:image/png;base64,{image.Base64Encoded()}' />\r\n" +
                  $"</td>";
            var xcolor = GetProductSoortColor(ProductSoort);
            var xrgb = $"rgb({xcolor.R}, {xcolor.G}, {xcolor.B})'";
            string prodsoort = string.IsNullOrEmpty(ProductSoort)
                ? ""
                : $"<span color='{xrgb}'><b>[{ProductSoort}]</b></span>";
            var ratio = Math.Round(ControleRatio,2);
            if (!useimage) ximage = "";
            var xreturn = $"<html>\r\n" +
                          $"<head>\r\n" +
                          $"<style>{GetStylesheet("StyleSheet")}</style>\r\n" +
                          $"<Title>{ArtikelNr}</Title>\r\n" +
                          $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                          $"</head>\r\n" +
                          $"<body style='background - color: {backcolor.Name}; background-gradient: {backgroundgradient.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 0px 0px'>\r\n" +
                          $"<h1 align='center' style='color: {textcolor.Name}'>\r\n" +
                          $"      {prodsoort} {title}\r\n" +
                          $"        <br/>\r\n" +
                          $"       {(this is Bewerking ? Naam + "<br>" : "")}\r\n" +
                          $"        <span style=\'font-size: x-small;\'>ArtikelNr: {ArtikelNr}, ProductieNr: {ProductieNr}</span>\r\n " +
                          $"</h1>\r\n" +
                          $"<blockquote class='whitehole'>\r\n" +
                          $"       <p style = 'margin-top: 0px' 'margin-bottom: 0px'>\r\n" +
                          $"<table border = '0' width = '100%' >\r\n" +
                          $"<tr style = 'vertical-align: top;' >\r\n" +
                          ximage +
                          $"<td><div>" +
                          $"<h2>Leverdatum: <span style = 'color: {GetValidColor(LeverDatum > DateTime.Now).Name}>{LeverDatum:f}</span>.</h2>" +
                          $"<h2>" +
                          $"{(State != ProductieState.Gereed ? $"Verwachte Leverdatum: <span style = 'color:{GetValidColor(VerwachtLeverDatum < LeverDatum).Name}> {VerwachtLeverDatum:f}</span>." : $"Gereed Gemeld Op: <span style = 'color:{GetValidColor(true).Name}>{DatumGereed:f}</span>.")}\r\n" +
                          $"</h2><hr />\r\n<h2>" +
                          $"Aantal Gemaakt: <u>{TotaalGemaakt}</u> / {Aantal} <span style = 'color: {GetPositiveColorByPercentage((decimal) Gereed).Name}'>({Gereed}%)</span><br>" +
                          $"<ul><li><u>Actueel</u> Aantal Gemaakt: <b><u>{ActueelAantalGemaakt}</u></b> / {Aantal}</li>" +
                           GetBewerkingenGemaaktHtml() +
                          $"</ul>" +
                          $"Tijd Gewerkt: <u>{TijdGewerkt}</u> / {Math.Round(DoorloopTijd, 2)} uur <span style = 'color:{GetPositiveColorByPercentage((decimal) TijdGewerktPercentage).Name}'>({TijdGewerktPercentage}%)</span><br>" +
                          $"{CombiesHtml()}" +
                          $"Per Uur: <u>{ActueelPerUur}</u> i.p.v. {PerUur} P/u <span style = 'color: {GetNegativeColorByPercentage(ProcentAfwijkingPerUur).Name}'>({ProcentAfwijkingPerUur}%)</span><br>" +
                          $"Controle Ratio: <span style = 'color: {GetNegativeColorByPercentage((decimal)ratio).Name}'>{(ratio > 0 ? $"+{ratio}" : ratio)}%</span><br><br>" +
                          $"<span style = 'color: {Color.DarkRed.Name}'><u>{Opmerking}</u></span><br>" +
                          $"<span style = 'color: {Color.DarkRed.Name}'><u>{ControlePunten}</u></span><br>" +
                          $"</h2><hr />\r\n" +
                          "</div>" +
                          "</td>" +
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
            string xname = "";
            return GetImageFromResources(ref xname);
        }

        public Bitmap GetImageFromResources(ref string name)
        {
            var img = this is ProductieFormulier ? Resources.page_document_16748_128_128 : Resources.operation;
            bool onderbroken = this.GetStoringen(true).Length > 0;
            if (this is Bewerking bew)
            {
                name += "bew";
                if (bew.Combies.Count > 0)
                {
                    name += "_combi";
                    img = img.CombineImage(Resources.UnMerge_arrows_32x32, ContentAlignment.BottomLeft, 2);
                }
                if (!string.IsNullOrEmpty(bew.Note?.Notitie))
                {
                    name += "_note";
                    img = img.CombineImage(Resources.Note_msgIcon_32x32, ContentAlignment.TopLeft, 2);
                }
            }
            else if (this is ProductieFormulier prod)
            {
                name += "prod";
                if (prod.Bewerkingen?.Any(x => x.Combies.Count > 0) ?? false)
                {
                    name += "_combi";
                    img = img.CombineImage(Resources.UnMerge_arrows_32x32, ContentAlignment.BottomLeft, 2);
                }

                if (!string.IsNullOrEmpty(prod.Note?.Notitie))
                {
                    name += "_note";
                    img = img.CombineImage(Resources.Note_msgIcon_32x32, ContentAlignment.TopLeft, 2);
                }
            }

            if (onderbroken)
            {
                name += "_onderbroken";
                img = img.CombineImage(Resources.Stop_Hand__32x32, ContentAlignment.BottomRight, 2);
            }
            else
            {
                name += "_" + Enum.GetName(typeof(ProductieState), State)?.ToLower();
                switch (State)
                {
                    case ProductieState.Gestopt:
                        if (IsNieuw)
                        {
                            name += "_nieuw";
                            img = img.CombineImage(Resources.new_25355, 2);
                        }
                        else if (TeLaat)
                        {
                            name += "_telaat";
                            img = img.CombineImage(Resources.Warning_36828, 2);
                        }

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
            }
            return img;
        }

        public int GetImageIndexFromList(ImageList list)
        {
            if (list == null) return -1;
            string name = "";
            var img = this.GetImageFromResources(ref name);
            if (!string.IsNullOrEmpty(name) && img != null)
            {
                if (!list.Images.ContainsKey(name))
                    list.Images.Add(name, img);
                return list.Images.IndexOfKey(name);
            }

            return -1;
        }
    }
}