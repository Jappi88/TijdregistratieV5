using ProductieManager.Productie;
using System;
using System.Globalization;

namespace ProductieManager.Classes.ViewModels
{
    public class PersoneelModel
    {
        public Personeel PersoneelLid { get; set; }
        public int ImageIndex { get; set; }
        public string Naam { get => PersoneelLid == null ? "N.V.T" : PersoneelLid.PersoneelNaam; }
        public string WerkTijd { get => PersoneelLid == null ? "N.V.T" : $"Werkt van {PersoneelLid.BeginDag.ToString(@"hh\:mm", new CultureInfo("fr-FR"))} tot {PersoneelLid.EindDag.ToString(@"hh\:mm", new CultureInfo("fr-FR"))} uur."; }
        public string IsVrij { get => PersoneelLid == null ? "N.V.T" : PersoneelLid.IsVrij(DateTime.Now) ? "Is Vrij" : "Is Aanwezig"; }
        public string WerktAan { get => PersoneelLid == null || PersoneelLid.WerktAan == null ? "N.V.T" : $"{PersoneelLid.WerktAan}"; }
        public string Gestart { get => PersoneelLid == null || PersoneelLid.WerktAan == null ? "N.V.T" : $"{PersoneelLid.Gestart.ToString("dd-MM-yy HH:mm")} uur"; }
        public string TijdGewerkt { get => PersoneelLid == null || PersoneelLid.WerktAan == null ? "N.V.T" : $"{PersoneelLid.TijdGewerkt} uur"; }
        public string WerkPlek { get => PersoneelLid == null || PersoneelLid.WerktAan == null ? "N.V.T" : $"{PersoneelLid.Werkplek} [{PersoneelLid.PerUur} p/u]"; }
        public string Kracht { get => PersoneelLid == null ? "N.V.T" : PersoneelLid.IsUitzendKracht ? "Externe Kracht" : "Interne Kracht"; }
        public string ProductieNr { get => PersoneelLid == null ? "N.V.T" : PersoneelLid.WerktAan == null ? "N.V.T" : PersoneelLid.WerktAan; }

        public PersoneelModel(Personeel pers)
        {
            PersoneelLid = pers;
        }

        public override bool Equals(object obj)
        {
            if (obj is string)
                return Naam.ToLower() == (obj as string).ToLower();
            else if (obj is Personeel)
                return Naam.ToLower() == (obj as Personeel).PersoneelNaam.ToLower();
            else if (obj is PersoneelModel)
                return Naam.ToLower() == (obj as PersoneelModel).Naam.ToLower();
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return Naam.GetHashCode();
        }
    }
}