using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductieManager.Rpm.Misc
{

    public static partial class NumberExtensions

    {

        #region Vaste teksten

        private const string Nul = "nul";

        private const string Honderd = "honderd";

        private const string Duizend = "duizend";

        private const string Miljoen = "miljoen";

        private const string Miljard = "miljard";


        private static readonly string[] EenhedenTot20 = new string[]

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

            "negentien",

        };



        private static readonly string[] EenhedenVanaf20 = new string[]

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

            "negenen",

        };



        private static readonly string[] TientallenVanaf20 = new string[]

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

        private static int Eenheden(this int getal) => getal % 10;

        private static int Tientallen(this int getal) => (getal / 10) % 10;

        private static int Honderdtallen(this int getal) => (getal / 100) % 10;

        private static int Duizendtallen(this int getal) => (getal / 1000) % 1000;

        private static int Miljoentallen(this int getal) => (getal / 1000000) % 1000;

        private static int Miljardtallen(this int getal) => (getal / 1000000000) % 1000;

        private static int Euro(this decimal bedrag) => (int) bedrag;

        private static int Eurocent(this decimal bedrag) => (int) (Math.Round(bedrag % 1, 2) * 100);

        #endregion



        #region Afgeleide eigenschappen

        private static bool HeeftEenhedenTot20(this int getal) => getal > 0 && getal.Tientallen() < 2;

        private static bool HeeftTientallenVanaf20(this int getal) => getal.Tientallen() >= 2;

        private static bool HeeftHonderdtal(this int getal) => getal.Honderdtallen() == 1;

        private static bool HeeftHonderdtallen(this int getal) => getal.Honderdtallen() > 1;

        private static bool HeeftDuizendtal(this int getal) => getal.Duizendtallen() == 1;

        private static bool HeeftDuizendtallen(this int getal) => getal.Duizendtallen() > 1;

        private static bool HeeftMiljoentallen(this int getal) => getal.Miljoentallen() > 0;
        private static bool HeeftMiljardtallen(this int getal) => getal.Miljardtallen() > 0;

        private static bool HeeftCenten(this decimal bedrag) => bedrag.Eurocent() > 0;

        #endregion



        #region Samengestelde teksten

        private static string BedragTekst(this decimal bedrag) =>

            bedrag.HeeftCenten()
                ? $"{bedrag.Euro().Tekst()} euro en {bedrag.Eurocent().Tekst()} cent"
                : $"{bedrag.Euro().Tekst()} euro";



        private static string Tekst(this int getal) =>

            getal.HeeftMiljardtallen()
                ? $"{getal.Miljardtallen().TekstTotDuizend()} {Miljard} {getal.TekstTotMiljard()}".TrimEnd()
                : getal.TekstTotMiljard();



        private static string TekstTotMiljard(this int getal) =>

            getal.HeeftMiljoentallen()
                ? $"{getal.Miljoentallen().TekstTotDuizend()} {Miljoen} {getal.TekstTotMiljoen()}".TrimEnd()
                : getal.TekstTotMiljoen();



        private static string TekstTotMiljoen(this int getal) =>

            getal.HeeftDuizendtallen()
                ? $"{getal.Duizendtallen().TekstTotDuizend()}{Duizend} {getal.TekstTotDuizend()}".TrimEnd()
                : getal.TekstTotTweeDuizend();



        private static string TekstTotTweeDuizend(this int getal) =>

            getal.HeeftDuizendtal() ? $"{Duizend} {getal.TekstTotDuizend()}".TrimEnd() : getal.TekstTotDuizend();



        private static string TekstTotDuizend(this int getal) =>

            getal.HeeftHonderdtallen()
                ? $"{EenhedenTot20[getal.Honderdtallen()]}{Honderd}{getal.TekstTotHonderd()}"
                : getal.TekstTotTweeHonderd();



        private static string TekstTotTweeHonderd(this int getal) =>

            getal.HeeftHonderdtal() ? $"{Honderd}{getal.TekstTotHonderd()}" : getal.TekstTotHonderd();



        private static string TekstTotHonderd(this int getal) =>

            getal.HeeftTientallenVanaf20()
                ? $"{EenhedenVanaf20[getal.Eenheden()]}{TientallenVanaf20[getal.Tientallen() - 2]}"
                : getal.TekstTotTwintig();



        private static string TekstTotTwintig(this int getal) =>

            getal.HeeftEenhedenTot20() ? EenhedenTot20[getal % 20] : Nul;

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
