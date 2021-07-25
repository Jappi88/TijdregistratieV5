using ProductieManager.Productie;
using System;

namespace ProductieManager.Classes.Productie
{
    public class Taak
    {
        public TaakBeheer Beheer { get; set; }

        private ProductieFormulier _form;

        public ProductieFormulier Formulier
        {
            get { return _form; }
            set { _form = value; }
        }

        public Bewerking Bewerking { get; set; }
        public Personeel Persoon { get; set; }
        public string Beschrijving { get; set; }
        public string Plek { get; set; }

        public int HashCode { get; set; }

        private TaakUrgentie _urgentie;

        public TaakUrgentie Urgentie
        {
            get
            {
                return Uitgevoerd ? TaakUrgentie.Geen_Prioriteit : _urgentie == TaakUrgentie.None ? GetUrgentie(DatumToegevoegd) : _urgentie;
            }
            set { _urgentie = value; }
        }

        public string ProductieNr { get { return Bewerking == null ? (Formulier == null ? "N/A" : Formulier.ProductieNr) : Bewerking.ProductieNr; } }
        public string ArtikelNr { get { return Bewerking == null ? (Formulier == null ? "N/A" : Formulier.ArtikelNr) : Bewerking.ArtikelNr; } }
        public string Omschrijving { get { return Bewerking == null ? (Formulier == null ? "N/A" : Formulier.Omschrijving) : Bewerking.Naam; } }
        public string ProductieInfo { get { return $"ProdNr: {ProductieNr} | ArtNr: {ArtikelNr}"; } }
        public AktieType Type { get; set; }
        public bool Uitgevoerd { get; set; }
        public bool Gelezen { get; set; }
        public DateTime DatumToegevoegd { get; set; }
        public DateTime DatumUitgevoerd { get; set; }

        public Taak()
        {
            Beschrijving = "N/A";
            Urgentie = TaakUrgentie.None;
            DatumToegevoegd = DateTime.Now;
            Type = AktieType.None;
            HashCode = GetHashCode();
        }

        public Taak(TaakBeheer beheer, ProductieFormulier formulier, string omschrijving, AktieType type, TaakUrgentie urgentie)
        {
            Beheer = beheer;
            Urgentie = urgentie;
            Formulier = formulier;
            Beschrijving = omschrijving;
            DatumToegevoegd = DateTime.Now;
            Type = type;
            HashCode = GetHashCode();
        }

        public Taak(TaakBeheer beheer, ProductieFormulier formulier, AktieType type, TaakUrgentie urgentie)
        {
            Beheer = beheer;
            Urgentie = urgentie;
            Formulier = formulier;
            DatumToegevoegd = DateTime.Now;
            Type = type;
            HashCode = GetHashCode();
        }

        public Taak(TaakBeheer beheer, ProductieFormulier formulier, Bewerking bewerking, AktieType type, TaakUrgentie urgentie) : this(beheer, formulier, type, urgentie)
        {
            Bewerking = bewerking;
        }

        public Taak(TaakBeheer beheer, Bewerking bewerking, string omschrijving, AktieType type, TaakUrgentie urgentie) : this(beheer, bewerking, type, urgentie)
        {
            Beschrijving = omschrijving;
        }

        public Taak(TaakBeheer beheer, Bewerking bewerking, AktieType type, TaakUrgentie urgentie)
        {
            Beheer = beheer;
            Urgentie = urgentie;
            Bewerking = bewerking;
            Formulier = bewerking.Parent;
            DatumToegevoegd = DateTime.Now;
            Type = type;
            HashCode = GetHashCode();
        }

        public Taak(TaakBeheer beheer, Bewerking bew, Personeel persoon, string omschrijving, AktieType type, TaakUrgentie urgentie)
        {
            Beheer = beheer;
            Bewerking = bew;
            Formulier = bew.Parent;
            Urgentie = urgentie;
            Persoon = persoon;
            Beschrijving = omschrijving;
            DatumToegevoegd = DateTime.Now;
            Type = type;
            HashCode = GetHashCode();
        }

        public Taak(TaakBeheer beheer, Bewerking bew, Personeel persoon, AktieType type, TaakUrgentie urgentie)
        {
            Beheer = beheer;
            Bewerking = bew;
            Formulier = bew.Parent;
            Urgentie = urgentie;
            Persoon = persoon;
            DatumToegevoegd = DateTime.Now;
            Type = type;
            HashCode = GetHashCode();
        }

        public event TaakHandler OnVereistAandacht;

        public void VereistAandacht()
        {
            OnVereistAandacht?.Invoke(Beheer, this);
        }

        public event TaakHandler OnUpdate;

        public void Update()
        {
            Taak xtaak = TakenLijst.GetUpdatedTaak(this);
            xtaak.Formulier = Formulier;
            bool update = Urgentie != xtaak.Urgentie || Beschrijving != xtaak.Beschrijving;
            Urgentie = xtaak.Urgentie;
            Beschrijving = xtaak.Beschrijving;
            if (update)
                OnUpdate?.Invoke(Beheer, this);
        }

        public static TaakUrgentie GetUrgentie(DateTime time)
        {
            DateTime xtime = Werktijd.EerstVolgendewerkdag(DateTime.Now);
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

        public bool Equals(Taak taak)
        {
            if (taak == null)
                return false;
            if (taak.Formulier != null && Formulier != null)
                if (Formulier.ProductieNr.ToLower() != taak.Formulier.ProductieNr.ToLower())
                    return false;
            if (Type != taak.Type)
                return false;
            return true;
        }
    }
}