using System;

namespace ProductieManager.Productie
{
    [Serializable]
    public class IProductieBase
    {
        public Classes.SqlLite.UserChange LastChanged { get; set; }
        public virtual ProductieState State { get; set; }
        public int Aantal { get; set; }
        public virtual string ArtikelNr { get; set; }
        public virtual string ProductieNr { get; set; }

        public double DoorloopTijd { get; set; }
        public DateTime DatumToegevoegd { get; set; }//  datum van het toevoegen van de productie formulier
        public DateTime DatumVerwijderd { get; set; }
        public DateTime DatumGereed { get; set; }
        public DateTime VerwachtLeverDatum { get; set; }
        public DateTime TijdGestart { get; set; }
        public DateTime LaatstAantalUpdate { get; set; }
        public DateTime TijdGestopt { get; set; }
        public double TotaalTijdGewerkt { get; set; }
        public double TijdGewerkt { get; set; }
        public virtual DateTime LeverDatum { get; set; }// de datum waarop de productie klaar moet zijn
        public virtual string Omschrijving { get; set; }//productie omschrijving
        public string Notitie { get; set; }//eventuele extra notitie(waar je op moet letten bijv)
        public string Paraaf { get; set; }//De persoon die aftekent
        public string GereedNotitie { get; set; }
        public virtual bool TeLaat { get; }
        public virtual bool IsNieuw { get; }
        public double Gereed { get; set; }

        public double GemiddeldPerUur { get; set; }
        public virtual double ActueelPerUur { get; set; }
        public double PerUur { get; set; }
    }
}