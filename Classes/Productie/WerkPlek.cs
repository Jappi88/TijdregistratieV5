using LiteDB;
using ProductieManager.Classes.Productie;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductieManager.Productie
{
    public class WerkPlek
    {
        [BsonId(true)]
        public int Id { get; set; }

        [BsonRef]
        public Bewerking Werk { get; set; }

        public List<Personeel> Personen { get; set; } = new List<Personeel> { };
        public List<Storing> Storingen { get; set; } = new List<Storing> { };

        public string Naam { get; set; }

        public DateTime LaatstAantalUpdate { get; set; }

        public string Path { get { return Werk == null || Werk.Path == null ? Naam : Werk.Path + "\\" + Naam; } }
        public string FriendlyVerwachtGereed { get { return Werk == null ? "N.V.T" : Werk.VerwachtDatumGereed().ToString("dddd dd MMMM yyyy HH:mm uur"); } }

        public int PerUur { get; set; }

        public double LastTijdGewerkt { get; set; }

        public double TijdGewerkt { get => TijdAanGewerkt(true); }

        private int _aantalgemaakt;

        public virtual int AantalGemaakt
        {
            get { return _aantalgemaakt; }
            set
            {
                if (value != _aantalgemaakt)
                {
                    _aantalgemaakt = value;
                    CalculatePerUur(true);
                    if (Werk != null)
                        Werk.UpdateAantal();
                }
            }
        }

        public string PersonenLijst
        {
            get
            {
                string value = "Geen Personeel";
                if (Personen != null && Personen.Count > 0)
                {
                    value = "";
                    for (int i = 0; i < Personen.Count; i++)
                    {
                        if (i > 0)
                            value += " ";
                        value += Personen[i].PersoneelNaam;
                        if (i < (Personen.Count - 1))
                            value += ",";
                    }
                }
                return value;
            }
        }

        public DateTime TijdGestart { get; set; }

        public DateTime TijdGestopt { get; set; }

        public WerkPlek(Personeel[] personen, string werkplek, Bewerking werk)
        {
            Personen = new List<Personeel>() { };
            if (personen != null && personen.Length > 0)
                Personen.AddRange(personen);
            if (werkplek == null)
                Naam = "N.V.T";
            else
                Naam = werkplek;

            Werk = werk;
        }

        public WerkPlek(string naam, Bewerking bew) : this(null, naam, bew)
        {
        }

        public WerkPlek() : this(null, null, null)
        {
        }

        public string GetGestartTijd()
        {
            if (Personen == null || Personen.Count == 0)
                return "N/A";
            DateTime dt = DateTime.MaxValue;
            foreach (Personeel per in Personen)
            {
                if (per.Gestart < dt)
                    dt = per.Gestart;
            }
            return dt.ToString("dd-MM-yyyy HH:mm");
        }

        public bool AddPersoon(Personeel persoon, Bewerking werk)
        {
            if (Personen == null || werk == null)
                return false;
            Werk = werk;
            var xpersoon = Personen.Remove(persoon);
            persoon.Werkplek = Naam;
            Personen.Add(persoon);
            return true;
        }

        public int AddPersonen(Personeel[] personen, Bewerking werk)
        {
            if (Personen == null || werk == null || personen == null) 
                return 0;
            int xreturn = 0;
            foreach (var persoon in personen)
            {
                if (AddPersoon(persoon, werk))
                    xreturn++;
            }
            return xreturn;
        }

        public void CalculatePerUur(bool updatedb)
        {
            if (Personen == null || Werk == null || Personen.Count == 0 || AantalGemaakt == 0 || (Werk != null && Werk.State != ProductieState.Gestart))
                PerUur = 0;
            else
            {
                double tijd = TijdAanGewerkt(true);
                foreach (Personeel per in Personen)
                {
                    double tg = per.TijdAanGewerkt(GetStoringen()).TotalHours;
                    if (per.WerktAan != null && Werk != null && Werk.Path.ToLower() == per.WerktAan.ToLower())
                    {
                        //pak het percentage van hoveel tijd gewerkt is.
                        double percentage = ((tg / tijd) * 100);
                        int eachp = percentage > 0 ? (int)((AantalGemaakt / 100) * percentage) : 0;
                        int peruur = tg == 0 ? eachp : (int)(eachp / tg);
                        bool same = peruur == per.PerUur;
                        per.PerUur = peruur;
                        if (updatedb)
                        {
                            var xdbpers = Manager.Database.GetPersoneel(per.PersoneelNaam);
                            if (xdbpers != null)
                            {
                                xdbpers.PerUur = per.PerUur;
                                Manager.Database.UpSert(xdbpers);
                            }
                        }
                        else if (!same)
                        {
                            Manager.PersoneelChanged(this, per);
                        }
                    }
                }

                if (tijd == 0)
                    PerUur = 0;
                else
                    PerUur = (int)(AantalGemaakt / tijd);
            }
        }

        public double TijdAanGewerkt(bool inclast)
        {
            double tijd = 0;
            if (inclast)
                tijd += LastTijdGewerkt;
            if (Personen == null || Personen.Count == 0)
                return tijd;
            tijd += Math.Round(Personen.Sum(x => x.TijdAanGewerkt(GetStoringen()).TotalHours), 2);

            return tijd;
        }

        public Dictionary<DateTime, DateTime> GetStoringen()
        {
            Dictionary<DateTime, DateTime> storingen = new Dictionary<DateTime, DateTime> { };
            Storingen.ForEach(x => storingen.Add(x.Gestart, x.IsVerholpen ? x.Gestopt : DateTime.Now));
            return storingen;
        }

        public bool RemovePersoon(string naam)
        {
            if (Personen == null)
                return false;
            Personeel per = Personen.FirstOrDefault(x => x.PersoneelNaam.ToLower() == naam.ToLower());
            if (per != null)
                return Personen.Remove(per);
            return false;
        }

        public override bool Equals(object obj)
        {
            if (obj is WerkPlek)
                return (obj as WerkPlek).Path.ToLower() == Path.ToLower();
            if(obj is string)
                return (obj as string).ToLower() == Path.ToLower();
            return false;
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }
    }
}