using System;

namespace ProductieManager.Rpm.Misc
{
    public static class NumberExtensions

    {
        #region Vaste teksten

        private const string Nul = "nul";

        private const string Honderd = "honderd";

        private const string Duizend = "duizend";

        private const string Miljoen = "miljoen";

        private const string Miljard = "miljard";


        private static readonly string[] EenhedenTot20 =
        {
            "",

            "een",

            "twee",

            "drie",

            "vier",

            "vijf",

            "zes",

            "zeven",

            "acht",

            "negen",

            "tien",

            "elf",

            "twaalf",

            "dertien",

            "veertien",

            "vijftien",

            "zestien",

            "zeventien",

            "achtien",

            "negentien"
        };


        private static readonly string[] EenhedenVanaf20 =
        {
            "",

            "eenen",

            "tweeën",

            "drieën",

            "vieren",

            "vijfen",

            "zesen",

            "zevenen",

            "achten",

            "negenen"
        };


        private static readonly string[] TientallenVanaf20 =
        {
            "twintig",

            "dertig",

            "veertig",

            "vijftig",

            "zestig",

            "zeventig",

            "tachtig",

            "negentig"
        };

        #endregion


        #region Berekeningen

        private static int Eenheden(this int getal)
        {
            return getal % 10;
        }

        private static int Tientallen(this int getal)
        {
            return getal / 10 % 10;
        }

        private static int Honderdtallen(this int getal)
        {
            return getal / 100 % 10;
        }

        private static int Duizendtallen(this int getal)
        {
            return getal / 1000 % 1000;
        }

        private static int Miljoentallen(this int getal)
        {
            return getal / 1000000 % 1000;
        }

        private static int Miljardtallen(this int getal)
        {
            return getal / 1000000000 % 1000;
        }

        private static int Euro(this decimal bedrag)
        {
            return (int) bedrag;
        }

        private static int Eurocent(this decimal bedrag)
        {
            return (int) (Math.Round(bedrag % 1, 2) * 100);
        }

        #endregion


        #region Afgeleide eigenschappen

        private static bool HeeftEenhedenTot20(this int getal)
        {
            return getal > 0 && getal.Tientallen() < 2;
        }

        private static bool HeeftTientallenVanaf20(this int getal)
        {
            return getal.Tientallen() >= 2;
        }

        private static bool HeeftHonderdtal(this int getal)
        {
            return getal.Honderdtallen() == 1;
        }

        private static bool HeeftHonderdtallen(this int getal)
        {
            return getal.Honderdtallen() > 1;
        }

        private static bool HeeftDuizendtal(this int getal)
        {
            return getal.Duizendtallen() == 1;
        }

        private static bool HeeftDuizendtallen(this int getal)
        {
            return getal.Duizendtallen() > 1;
        }

        private static bool HeeftMiljoentallen(this int getal)
        {
            return getal.Miljoentallen() > 0;
        }

        private static bool HeeftMiljardtallen(this int getal)
        {
            return getal.Miljardtallen() > 0;
        }

        private static bool HeeftCenten(this decimal bedrag)
        {
            return bedrag.Eurocent() > 0;
        }

        #endregion


        #region Samengestelde teksten

        private static string BedragTekst(this decimal bedrag)
        {
            return bedrag.HeeftCenten()
                ? $"{bedrag.Euro().Tekst()} euro en {bedrag.Eurocent().Tekst()} cent"
                : $"{bedrag.Euro().Tekst()} euro";
        }


        private static string Tekst(this int getal)
        {
            return getal.HeeftMiljardtallen()
                ? $"{getal.Miljardtallen().TekstTotDuizend()} {Miljard} {getal.TekstTotMiljard()}".TrimEnd()
                : getal.TekstTotMiljard();
        }


        private static string TekstTotMiljard(this int getal)
        {
            return getal.HeeftMiljoentallen()
                ? $"{getal.Miljoentallen().TekstTotDuizend()} {Miljoen} {getal.TekstTotMiljoen()}".TrimEnd()
                : getal.TekstTotMiljoen();
        }


        private static string TekstTotMiljoen(this int getal)
        {
            return getal.HeeftDuizendtallen()
                ? $"{getal.Duizendtallen().TekstTotDuizend()}{Duizend} {getal.TekstTotDuizend()}".TrimEnd()
                : getal.TekstTotTweeDuizend();
        }


        private static string TekstTotTweeDuizend(this int getal)
        {
            return getal.HeeftDuizendtal() ? $"{Duizend} {getal.TekstTotDuizend()}".TrimEnd() : getal.TekstTotDuizend();
        }


        private static string TekstTotDuizend(this int getal)
        {
            return getal.HeeftHonderdtallen()
                ? $"{EenhedenTot20[getal.Honderdtallen()]}{Honderd}{getal.TekstTotHonderd()}"
                : getal.TekstTotTweeHonderd();
        }


        private static string TekstTotTweeHonderd(this int getal)
        {
            return getal.HeeftHonderdtal() ? $"{Honderd}{getal.TekstTotHonderd()}" : getal.TekstTotHonderd();
        }


        private static string TekstTotHonderd(this int getal)
        {
            return getal.HeeftTientallenVanaf20()
                ? $"{EenhedenVanaf20[getal.Eenheden()]}{TientallenVanaf20[getal.Tientallen() - 2]}"
                : getal.TekstTotTwintig();
        }


        private static string TekstTotTwintig(this int getal)
        {
            return getal.HeeftEenhedenTot20() ? EenhedenTot20[getal % 20] : Nul;
        }

        #endregion


        #region Public API

        public static string ToUitgeschrevenBedrag(this decimal bedrag)

        {
            if (bedrag < 0)

                throw new ArgumentOutOfRangeException($"Ongeldige invoer: {bedrag}, bedrag moet groter zijn dan 0");

            return bedrag.BedragTekst();
        }


        public static string ToUitgeschrevenTekst(this int getal)

        {
            if (getal < 0)

                throw new ArgumentOutOfRangeException($"Ongeldige invoer: {getal}, getal moet groter zijn dan 0");


            return getal.Tekst();
        }

        #endregion
    }
}