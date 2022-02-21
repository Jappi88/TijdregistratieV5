using System;
using System.Runtime.Serialization;
using Rpm.Various;

namespace Rpm.Productie
{
    [DataContract]
    public class Taak
    {
        private TaakUrgentie _urgentie;

        public Taak()
        {
            Beschrijving = "N/A";
            Urgentie = TaakUrgentie.None;
            DatumToegevoegd = DateTime.Now;
            Type = AktieType.None;
            HashCode = base.GetHashCode();
        }

        public Taak(ProductieFormulier formulier, string omschrijving, AktieType type,
            TaakUrgentie urgentie)
        {
            //Beheer = beheer;
            Urgentie = urgentie;
            Formulier = formulier;
            Title = $"[{formulier.ProductieNr}-{formulier.ArtikelNr}] {formulier.Omschrijving}";
            Beschrijving = omschrijving;
            DatumToegevoegd = DateTime.Now;
            Type = type;
            HashCode = base.GetHashCode();
        }

        public Taak(ProductieFormulier formulier, AktieType type, TaakUrgentie urgentie)
        {
            //Beheer = beheer;
            Urgentie = urgentie;
            Formulier = formulier;
            Title = $"[{formulier.ProductieNr}-{formulier.ArtikelNr}] {formulier.Omschrijving}";
            DatumToegevoegd = DateTime.Now;
            Type = type;
            HashCode = base.GetHashCode();
        }

        public Taak(ProductieFormulier formulier, Bewerking bewerking, AktieType type,
            TaakUrgentie urgentie) : this(formulier, type, urgentie)
        {
            Bewerking = bewerking;
        }

        public Taak(Bewerking bewerking, string omschrijving, AktieType type, TaakUrgentie urgentie)
            : this(bewerking, type, urgentie)
        {
            Beschrijving = omschrijving;
        }

        public Taak(Bewerking bewerking, AktieType type, TaakUrgentie urgentie)
        {
            //Beheer = beheer;
            Urgentie = urgentie;
            Bewerking = bewerking;
            Title = $"[{bewerking.ProductieNr}-{bewerking.ArtikelNr} {bewerking.Naam}] {bewerking.Omschrijving}";
            Formulier = bewerking.Parent;
            Beschrijving = bewerking.Naam;
            DatumToegevoegd = DateTime.Now;
            Type = type;
            HashCode = base.GetHashCode();
        }

        public Taak(WerkPlek plek, AktieType type, TaakUrgentie urgentie)
        {
            Urgentie = urgentie;
            Plek = plek;
            if (plek != null)
                Title = $"[{plek.Werk?.ProductieNr}-{plek.Werk?.ArtikelNr} {plek.Naam}] {plek.Omschrijving}";
            else Title = "N.V.T.";
            Bewerking = plek?.Werk;
            Beschrijving = plek?.Naam;
            Formulier = plek?.Werk?.Parent;
            DatumToegevoegd = DateTime.Now;
            Type = type;
            HashCode = base.GetHashCode();
        }

        public Taak(Bewerking bew, Personeel persoon, string omschrijving, AktieType type,
            TaakUrgentie urgentie)
        {
            Bewerking = bew;
            Formulier = bew.Parent;
            Title = $"[{bew.ProductieNr}-{bew.ArtikelNr} {bew.Naam}] {bew.Omschrijving}";
            Urgentie = urgentie;
            Persoon = persoon;
            Beschrijving = omschrijving;
            DatumToegevoegd = DateTime.Now;
            Type = type;
            HashCode = base.GetHashCode();
        }

        public Taak(Bewerking bew, Personeel persoon, AktieType type, TaakUrgentie urgentie)
        {
            Bewerking = bew;
            Formulier = bew.Parent;
            Title = $"[{bew.ProductieNr}-{bew.ArtikelNr} {bew.Naam}] {bew.Omschrijving}";
            Beschrijving = bew.Naam;
            Urgentie = urgentie;
            Persoon = persoon;
            DatumToegevoegd = DateTime.Now;
            Type = type;
            HashCode = base.GetHashCode();
        }

        public ProductieFormulier Formulier { get; set; }
        public Bewerking Bewerking { get; set; }
        public Personeel Persoon { get; set; }
        public WerkPlek Plek { get; set; }

        public string Title { get; }

        public string Beschrijving { get; set; }
        // public string Plek { get; set; }

        public int HashCode { get; set; }

        public TaakUrgentie Urgentie
        {
            get => Uitgevoerd ? TaakUrgentie.Geen_Prioriteit :
                _urgentie == TaakUrgentie.None ? GetUrgentie(DatumToegevoegd) : _urgentie;
            set => _urgentie = value;
        }

        public string ProductieNr =>
            Bewerking == null ? Formulier == null ? "N/A" : Formulier.ProductieNr : Bewerking.ProductieNr;

        public string ArtikelNr =>
            Bewerking == null ? Formulier == null ? "N/A" : Formulier.ArtikelNr : Bewerking.ArtikelNr;

        public string Omschrijving =>
            Bewerking == null ? Formulier == null ? "N/A" : Formulier.Omschrijving : Bewerking.Naam;

        public string ProductieInfo => $"ProdNr: {ProductieNr} | ArtNr: {ArtikelNr}";
        public AktieType Type { get; set; }
        public bool Uitgevoerd { get; set; }
        public bool Gelezen { get; set; }
        public DateTime DatumToegevoegd { get; set; }
        public DateTime DatumUitgevoerd { get; set; }

        //public event TaakHandler OnUpdate;

        //public bool Update(ProductieFormulier form)
        //{
        //    try
        //    {
        //        Formulier = form ?? Formulier;
        //        if (Formulier != null)
        //        {
        //            if (Bewerking != null)
        //                Bewerking = Formulier.Bewerkingen.FirstOrDefault(x => x.Equals(Bewerking)) ?? Bewerking;
        //            if (Plek != null) Plek = Formulier.GetAlleWerkplekken().FirstOrDefault(x => x.Equals(Plek)) ?? Plek;
        //        }

        //        var xtaak = TakenLijst.GetUpdatedTaak(this);
        //        var update = Urgentie != xtaak.Urgentie || Beschrijving != xtaak.Beschrijving;
        //        return update;
        //    }
        //    catch
        //    {
        //        return false;
        //    }

        //    //Urgentie = xtaak.Urgentie;
        //    //Beschrijving = xtaak.Beschrijving;
        //    //if (update)
        //    //    OnUpdate?.Invoke(Beheer, this);
        //}

        public static TaakUrgentie GetUrgentie(DateTime time)
        {
            var rooster = Manager.Opties?.GetWerkRooster();
            var xtime = Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster, rooster, null);
            if (time <= xtime.AddMinutes(30))
                return TaakUrgentie.PerDirect;
            if (time <= xtime.AddMinutes(60))
                return TaakUrgentie.ZSM;
            if (time <= xtime.AddMinutes(90))
                return TaakUrgentie.ZodraMogelijk;
            if (time <= xtime.AddMinutes(240))
                return TaakUrgentie.Geen_Prioriteit;
            return TaakUrgentie.None;
        }

        public override bool Equals(object item)
        {
            if (item is Taak taak)
            {
                if (taak.Type != Type)
                    return false;
                var xreturn = true;
                if (taak.Formulier != null && Formulier != null)
                    if (!string.Equals(Formulier.ProductieNr, taak.Formulier.ProductieNr,
                            StringComparison.CurrentCultureIgnoreCase))
                        xreturn = false;
                if (taak.Plek != null && Plek != null)
                    xreturn &= taak.Plek.Equals(Plek);
                if (taak.Bewerking != null && Bewerking != null)
                    xreturn &= taak.Bewerking.Equals(Bewerking);
                if (taak.Persoon != null && Persoon != null)
                    xreturn &= taak.Persoon.Equals(Persoon);
                return xreturn;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode;
        }

        public string GetPath()
        {
            if (Plek != null)
                return Plek.Path;
            if (Bewerking != null)
                return Bewerking.Path;
            if (Formulier != null)
                return Formulier.ProductieNr;
            if (Persoon != null) return Persoon.PersoneelNaam;
            return Enum.GetName(typeof(AktieType), Type);
        }

        //protected virtual void Update(TaakBeheer manager, Taak taak)
        //{
        //    OnUpdate?.Invoke(manager, taak);
        //}
    }
}