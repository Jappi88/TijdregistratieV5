using LiteDB;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductieManager.Productie
{
    [Serializable]
    public class Personeel
    {
        public Classes.SqlLite.UserChange LastChanged { get; set; }
        public string WerktAan { get; set; }
        public SkillTree Skills { get; set; }
        public string Path { get { return ((WerktAan != null ? WerktAan + "\\Personeel\\" : "") + PersoneelNaam); } }
        public int ImageIndex { get; set; }

        [BsonId]
        public string PersoneelNaam { get; set; }

        public string Werkplek { get; set; }
        public double Efficientie { get; set; }
        public int PerUur { get; set; }
        public DateTime TijdIngezet { get; set; }
        public DateTime Gestart { get; set; }
        public DateTime Gestopt { get; set; }
        public TimeSpan BeginDag { get; set; }
        public TimeSpan EindDag { get; set; }
        public Dictionary<DateTime, DateTime> VrijeDagen { get; set; }
        public bool IsBezig { get { return WerktAan != null; } }
        public bool IsUitzendKracht { get; set; }
        public bool Actief { get; set; }
        public double TijdGewerkt { get { return Math.Round(TijdAanGewerkt(null).TotalHours, 2); } }

        public Personeel()
        {
            Efficientie = 100;
            PersoneelNaam = "N.V.T";
        }

        public Personeel(string naam, string werkplek) : this(naam)
        {
            Werkplek = werkplek;
        }

        public Personeel(string naam) : this()
        {
            PersoneelNaam = naam;
        }

        public static Personeel CreateNew(Personeel persoon)
        {
            return new Personeel()
            {
                Actief = persoon.Actief,
                BeginDag = persoon.BeginDag,
                Efficientie = persoon.Efficientie,
                EindDag = persoon.EindDag,
                Gestart = persoon.Gestart,
                Gestopt = persoon.Gestopt,
                ImageIndex = persoon.ImageIndex,
                IsUitzendKracht = persoon.IsUitzendKracht,
                LastChanged = persoon.LastChanged,
                PersoneelNaam = persoon.PersoneelNaam,
                TijdIngezet = persoon.TijdIngezet,
                VrijeDagen = persoon.VrijeDagen,
                Werkplek = persoon.Werkplek,
                WerktAan = persoon.WerktAan
            };
        }

        //de tijd dat die persoon heeft gewerkt
        public TimeSpan TijdAanGewerkt(Dictionary<DateTime, DateTime> exclude)
        {
            DateTime xgestopt = WerktAan == null || !Actief ? Gestopt : DateTime.Now;
            if (Gestopt > xgestopt)
                xgestopt = Gestopt;
            if (Gestart == null || Gestopt == null || Gestart >= xgestopt)
                return new TimeSpan();

            Dictionary<DateTime, DateTime> storingen = new Dictionary<DateTime, DateTime> { };
            if (exclude != null && exclude.Count > 0)
                storingen = exclude;

            if (VrijeDagen != null && VrijeDagen.Count > 0)
                foreach (var v in VrijeDagen)
                    storingen.Add(v.Key, v.Value);


            return Werktijd.TijdGewerkt(Gestart, xgestopt, BeginDag, EindDag, storingen);
        }

        public double TijdExtraNodig(double uren)
        {
            return (uren * (Efficientie / 100));
        }

        public bool IsVrij(DateTime time)
        {
            if (VrijeDagen == null || VrijeDagen.Count == 0)
                return false;
            foreach (var v in VrijeDagen)
            {
                if (time >= v.Key && time <= v.Value)
                    return true;
            }
            return false;
        }

        public double IsVrijOver()
        {
            DateTime firststart = new DateTime(1, 1, 1);
            if (VrijeDagen != null && VrijeDagen.Count > 0)
            {
                foreach (var v in VrijeDagen)
                    if (firststart < v.Key)
                        firststart = v.Key;
            }
            if (firststart.Year == 1 || firststart < DateTime.Now)
                return -1;
            return Math.Round(Werktijd.TijdGewerkt(DateTime.Now, firststart).TotalHours, 2);
        }

        public bool IsVrijOver(TimeSpan time)
        {
            return IsVrij(DateTime.Now.Add(time));
        }

        public TimeSpan TijdVrij()
        {
            TimeSpan time = new TimeSpan();
            if (VrijeDagen != null && VrijeDagen.Count > 0)
            {
                foreach (var v in VrijeDagen)
                    time = time.Add(Werktijd.TijdGewerkt(v.Key, v.Value, BeginDag, EindDag));
            }
            return time;
        }

        public ProductieFormulier GetWerk(out Bewerking werk)
        {
            werk = null;
            if (WerktAan != null)
            {
                string[] values = WerktAan.Split('\\');
                if (values.Length > 1)
                {
                    ProductieFormulier form = Manager.Database.GetProductie(values[0]);
                    if (form != null)
                    {
                        if (form.Bewerkingen != null && form.Bewerkingen.Length > 0)
                        {
                            werk = form.Bewerkingen.FirstOrDefault(t => t.Naam.ToLower() == values[1].ToLower().ToLower());
                        }
                    }
                    return form;
                }
            }
            return null;
        }

        public override bool Equals(object obj)
        {
            if (obj is string)
                return PersoneelNaam.ToLower() == (obj as string).ToLower();
            else if (obj is Personeel)
                return PersoneelNaam.ToLower() == (obj as Personeel).PersoneelNaam.ToLower();
            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            if (PersoneelNaam != null)
                return PersoneelNaam.GetHashCode();
            return base.GetHashCode();
        }
    }
}