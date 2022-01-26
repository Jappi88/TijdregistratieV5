using System;
using System.Drawing;
using Polenter.Serialization;

namespace Rpm.Productie
{
    public class SpoorEntry
    {
        public int ID { get; private set; }
        public string ArtikelNr { get; set; }
        public string MateriaalArtikelNr { get; set; }
        public string MateriaalOmschrijving { get; set; }
        public string ProductOmschrijving { get; set; }
        public string ProductHtmlOmschrijving { get; set; }
        public string AangepastDoor { get; set; }
        public DateTime AangepastOp { get; set; }
        public int AantalSporen { get; set; } = 1;
        public int PakketAantal { get; set; }
        public int Aantal { get; set; }
        public decimal ProductLengte { get; set; }
        public decimal UitgangsLengte { get; set; } = 7000;
        [ExcludeFromSerialization]
        public int PerUur { get; set; }

        public SpoorEntry()
        {
            AangepastOp = DateTime.Now;
            ID = AangepastOp.GetHashCode();
        }

        public bool ContainsFilter(string criteria)
        {
            if (string.IsNullOrEmpty(criteria)) return true;
            if (!string.IsNullOrEmpty(ArtikelNr) && ArtikelNr.ToLower().Contains(criteria.ToLower()))
                return true;
            if (!string.IsNullOrEmpty(ProductOmschrijving) &&
                ProductOmschrijving.ToLower().Contains(criteria.ToLower()))
                return true;
            if (!string.IsNullOrEmpty(AangepastDoor) && AangepastDoor.ToLower().Contains(criteria.ToLower()))
                return true;
            return false;
        }

        public string CreateHtmlText(int aantal, bool checkperuur)
        {
            var xvalue = Math.Round(ProductLengte, 4);
            var xtotal = Math.Round(UitgangsLengte, 4);
            var xstuks = ProductLengte > 0 && UitgangsLengte > 0 ? Math.Round(ProductLengte / UitgangsLengte, 4) : 0;
            string xret;

            var xprodsperlengte = xvalue > 0 ? (int) (xtotal / xvalue) : 0;
            var xrest = xvalue > 0 ? Math.Round((xtotal % xvalue), 2) : 0;
            var xnodig = xprodsperlengte > 0 ? aantal < xprodsperlengte ? 1 : (aantal / xprodsperlengte) : 0;

            if (xnodig == 0)
            {
                if (ProductLengte == 0)
                    xret = $"<span color='{Color.Navy.Name}'>" +
                           $"Er is geen materiaal nodig voor een productlengte van <b>{ProductLengte}mm</b>." +
                           $"</span>";
                else
                    xret = $"<span color='{Color.Navy.Name}'>" +
                           $"Er kunnen geen producten van <b>{Math.Round(ProductLengte, 2)}mm</b> gehaald worden uit <b>{UitgangsLengte}mm</b>!" +
                           $"</span>";
            }
            else
            {
                if (xnodig * xprodsperlengte < aantal)
                    xnodig++;
                var xsporen = AantalSporen;
                var xrestsporen = (int) (xnodig % xsporen);
                if (xnodig < xsporen)
                {
                    xsporen = xnodig;
                    xrestsporen = 0;
                }

                var xaantalladen = xnodig > 0 ? xnodig < xsporen ? 1 : xnodig / xsporen : 0;
                var x1 = xsporen == 1 ? "spoor" : "sporen";
                var x2 = xrestsporen == 1 ? "spoor" : "sporen";
                var l1 = xnodig == 1 ? "lengte" : "lengtes";
                var xmaken = (xnodig * (int) xprodsperlengte);
                var overschot = xmaken > aantal ? (xmaken - aantal) : 0;
                var x3 = overschot == 1 ? "product" : "producten";
                var xpakket = "";
                var peruur = "";
                if (PakketAantal > 0)
                {
                    var xaantalpakketten = xnodig > 0 ? xnodig < PakketAantal ? 1 : xnodig / PakketAantal : 0;
                    var xrestpakket = xnodig > 0 ? (int) (xnodig % PakketAantal) : 0;
                    if (xaantalpakketten > 0)
                    {
                        var a1 = xaantalpakketten == 1 ? "pakket" : "pakketten";
                        var a2 = xrestpakket == 1 ? "lengte" : "lengtes";
                        var a0 = xaantalpakketten == 1 ? "is" : "zijn";
                        var a4 = PakketAantal == 1 ? "lengte" : "lengtes";
                        if (xnodig < PakketAantal)
                        {
                            xpakket = $"Dat is een restpakket van <b>{xrestpakket}</b> {a2}";
                        }
                        else
                        {
                            xpakket = $"Dat {a0} <b>{xaantalpakketten}</b> {a1} van <b>{PakketAantal}</b> {a4}";
                            if (xrestpakket > 0)
                                xpakket += $" en een restpakket van <b>{xrestpakket}</b> {a2}";
                        }

                        if (xpakket.Length > 1)
                            xpakket += ".<br>";
                    }
                }

                if (checkperuur && !string.IsNullOrEmpty(ArtikelNr) && ArtikelNr.Length > 4)
                {
                    var ent = Manager.ArtikelRecords?.GetRecord(ArtikelNr);
                    if (ent != null)
                        PerUur = ent.PerUur;
                    if (PerUur > 0 && aantal > 0)
                    {
                        double tijd = Math.Round(((double) aantal / PerUur), 2);
                        peruur =
                            $"<h5>" +
                            $"Gemiddeld produceer je <b>{PerUur}p/u</b>, dat is <b>{tijd} uur</b> om <b>{aantal}</b> te produceren.<br>" +
                            $"Als je nu begint ben je {Werktijd.DatumNaTijd(DateTime.Now, TimeSpan.FromHours(tijd), Manager.Opties?.GetWerkRooster(), Manager.Opties?.SpecialeRoosters):f} klaar." +
                            $"</h5>";
                    }
                }

                var xoverschotmaat = overschot > 0
                    ? $"Dat is een overshot van <b>{overschot}</b> {x3}<b>({Math.Round((overschot * ProductLengte) / 1000, 4)} meter)</b>.<br>"
                    : "";
                var xoms = string.IsNullOrEmpty(ProductHtmlOmschrijving) ? string.IsNullOrEmpty(ProductOmschrijving) ? "" : $"<h4>{ProductOmschrijving}</h4>" : ProductHtmlOmschrijving;
                xret = $"<p color='{Color.Navy.Name}'>" +
                      //$"<ul>" +
                       xoms +
                       peruur +
                       $"Een productLengte van <b>{Math.Round(ProductLengte, 3)}mm</b> is <b>{xstuks}(stuk)</b> van <b>{UitgangsLengte}mm</b>.<br><br>" +
                       $"Met een productlengte van <b>{xvalue / 1000}m</b> heb je <b>{xnodig}</b> {l1} nodig.<br>" +
                       $"{xpakket}" +
                       $"Met <b>{xnodig}</b> lengtes kan je <b>{xmaken}/ {aantal}</b> producten maken.<br>" +
                       $"{xoverschotmaat}" +
                       $"Met <b>{xsporen}</b> {x1} is dat <b>{xaantalladen}</b> keer laden{(xrestsporen > 0 ? $" en een restlading van <b>{xrestsporen}</b> {x2}" : "")}.<br><br>" +
                       $"Je haalt <b>{(int) xprodsperlengte}</b> producten uit <b>{xtotal / 1000} meter</b> met een reststuk van <b>{xrest}mm({Math.Round((xrest / xtotal) * 100,4)}%)</b>.<br>" +
                      // $"</ul>" +
                       $"</p>";
            }

            return xret;
        }

    public bool CompareTo(SpoorEntry verpakking)
        {
            var verp = verpakking;
            if (verp == null) return false;
            if (!string.Equals(ArtikelNr, verp.ArtikelNr, StringComparison.CurrentCultureIgnoreCase))
                return false;
            if (!string.Equals(ProductOmschrijving, verp.ProductOmschrijving, StringComparison.CurrentCultureIgnoreCase))
                return false;
            if (!string.Equals(AangepastDoor, verp.AangepastDoor, StringComparison.CurrentCultureIgnoreCase))
                return false;

            return true;
        }

        public override bool Equals(object obj)
        {
            if (obj is SpoorEntry verp)
            {
                if (!string.IsNullOrEmpty(ArtikelNr) && string.Equals(ArtikelNr, verp.ArtikelNr,
                        StringComparison.CurrentCultureIgnoreCase))
                    return true;
                return CompareTo(verp);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return ArtikelNr?.GetHashCode()??0;
        }
    }
}
