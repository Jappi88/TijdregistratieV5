using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Rpm.Productie;
using Rpm.Various;

namespace Rpm.ViewModels
{
    public class PersoneelModel
    {
        public PersoneelModel(Personeel pers)
        {
            PersoneelLid = pers;
        }

        public Personeel PersoneelLid { get; set; }
        public string Naam => PersoneelLid == null ? "N.V.T" : PersoneelLid.PersoneelNaam;

        public string WerkTijd => PersoneelLid == null
            ? "N.V.T"
            : PersoneelLid.WerkRooster?.WerkRoosterInfo() ?? Manager.Opties?.WerkRooster?.WerkRoosterInfo();

        public string IsVrij => PersoneelLid == null ? "N.V.T" :
            PersoneelLid.IsVrij(DateTime.Now) ? "Is Vrij" : "Is Aanwezig";

        public string WerktAan => GetWerk();

        public string Gestart => !PersoneelLid.IsBezig
            ? "N.V.T"
            : $"{PersoneelLid.GestartOp():dd-MM-yy HH:mm} uur";

        public string TijdGewerkt => !PersoneelLid.IsBezig
            ? "N.V.T"
            : $"{PersoneelLid.TijdGewerkt} uur";

        public string WerkPlek => GetWerkPlekken();

        public string Afdeling => PersoneelLid?.Afdeling;

        public string Kracht => PersoneelLid.IsUitzendKracht ? "Externe Kracht" : "Interne Kracht";

        public string ProductieNr => GetProducties();

        public string GetWerk()
        {
            if (PersoneelLid == null)
                return "N.V.T";
            var xreturn = "";
            var xklusjes = PersoneelLid.Klusjes.Where(x => x.Status == ProductieState.Gestart).ToList();
            if (xklusjes.Count == 1)
                xreturn = $"[{xklusjes[0].Naam}({xklusjes[0].ProductieNr})]";
            else if (xklusjes.Count > 1)
                xreturn = $"{xklusjes.Count} klusjes";
            else xreturn = "N.V.T";
            return xreturn;
        }

        public string GetProducties()
        {
            if (PersoneelLid == null)
                return "N.V.T";
            var xreturn = "";
            foreach (var klus in PersoneelLid.Klusjes)
            {
                if (klus.Status != ProductieState.Gestart)
                    continue;
                xreturn += $"{klus.ProductieNr}, ";
            }

            return xreturn.Length > 0 ? xreturn.TrimEnd(',', ' ') : "N.V.T";
        }

        public string GetWerkPlekken()
        {
            if (PersoneelLid == null)
                return "N.V.T";
            var xreturn = "";
            var wps = new List<string>();
            foreach (var klus in PersoneelLid.Klusjes)
            {
                if (klus.Status != ProductieState.Gestart || wps.Any(x =>
                    string.Equals(x, klus.WerkPlek, StringComparison.CurrentCultureIgnoreCase)))
                    continue;
                wps.Add(klus.WerkPlek);
                xreturn += $"{klus.WerkPlek}, ";
            }

            return wps.Count > 0 ? xreturn.TrimEnd(',', ' ') : "N.V.T";
        }

        public override bool Equals(object obj)
        {
            if (obj is string o)
                return string.Equals(Naam, o, StringComparison.CurrentCultureIgnoreCase);
            if (obj is Personeel personeel)
                return string.Equals(Naam, personeel.PersoneelNaam, StringComparison.CurrentCultureIgnoreCase);
            if (obj is PersoneelModel model)
                return string.Equals(Naam, model.Naam, StringComparison.CurrentCultureIgnoreCase);
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Naam.GetHashCode();
        }
    }
}