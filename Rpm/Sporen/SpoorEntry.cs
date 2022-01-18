using System;
using System.Drawing;

namespace Rpm.Productie
{
    public class SpoorEntry
    {
        public int ID { get; private set; }
        public string ArtikelNr { get; set; }
        public string MateriaalArtikelNr { get; set; }
        public string MateriaalOmschrijving { get; set; }
        public string ProductOmschrijving { get; set; }
        public string AangepastDoor { get; set; }
        public DateTime AangepastOp { get; set; }
        public int AantalSporen { get; set; } = 1;
        public int Aantal { get; set; }
        public decimal ProductLengte { get; set; }
        public decimal UitgangsLengte { get; set; } = 7000;

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

        public string CreateHtmlText(int aantal)
        {
            var xvalue = Math.Round(ProductLengte / 1000, 4);
            var xtotal = Math.Round(UitgangsLengte / 1000, 4);
            var xstuks = ProductLengte > 0 && UitgangsLengte > 0 ? Math.Round(ProductLengte / UitgangsLengte, 4) : 0;
            var xret = "Geen verbruik";

            var xprodsperlengte = xvalue > 0 ? (int) (xtotal / xvalue) : 0;
            var xrest = xvalue > 0 ? Math.Round((xtotal % xvalue) * 1000, 2) : 0;
            var xnodig = xprodsperlengte > 0 ? aantal < xprodsperlengte ? 1 : (aantal / xprodsperlengte) : 0;

            if (xnodig == 0)
            {
                xret = $"<span color='{Color.Navy.Name}'>" +
                             $"Er kunnen geen producten van <b>{Math.Round(ProductLengte, 2)}mm</b> gehaald worden uit <b>{UitgangsLengte}mm</b>!" +
                             $"</span>";
            }
            else
            {
                if (xnodig * xprodsperlengte < aantal)
                    xnodig++;
                var xsporen = AantalSporen;
                var xrestsporen = (int)(xnodig % xsporen);
                if (xnodig < xsporen)
                {
                    xsporen = xnodig;
                    xrestsporen = 0;
                }
                var xaantalladen = xnodig > 0 ? xnodig < xsporen ? 1 : xnodig / xsporen : 0;
                var x1 = xsporen == 1 ? "spoor" : "sporen";
                var x2 = xrestsporen == 1 ? "spoor" : "sporen";
                var xmaken = (xnodig * (int) xprodsperlengte);
                var overschot = xmaken > aantal ? (xmaken - aantal) : 0;
                var x3 = overschot == 1 ? "product" : "producten";
                var xoverschotmaat =overschot > 0? $"<span>Dat is een overshot van <b>{overschot}</b> {x3}<b>({Math.Round((overschot * ProductLengte) / 1000, 4)} meter).</b></span>" : "";
                xret = $"<span color='{Color.Navy.Name}'>" +
                             $"Een productLengte van <b>{Math.Round(ProductLengte, 2)}mm</b> is <b>{xstuks}(stuk)</b> van <b>{UitgangsLengte}mm</b><br><br>" +
                             $"Met een productlengte van <b>{xvalue}m</b> heb je <b>{xnodig}</b> lengtes nodig.<br>" +
                             $"Dat is <b>{xaantalladen}</b> keer laden met <b>{xsporen}</b> {x1}{(xrestsporen > 0 ? $" en een restlading van <b>{xrestsporen}</b> {x2}" : "")}.<br>" +
                             $"Met <b>{xnodig}</b> lengtes kan je <b>{xmaken}/ {aantal}</b> producten maken.<br>" +
                             $"{xoverschotmaat}<br>" +
                             $"Je haalt <b>{(int)xprodsperlengte}</b> producten uit <b>{xtotal} meter</b> met een reststuk van <b>{xrest}mm</b></span>";
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
