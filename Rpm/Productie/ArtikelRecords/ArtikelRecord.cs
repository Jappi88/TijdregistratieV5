﻿using System;
using System.Collections.Generic;

namespace Rpm.Productie.ArtikelRecords
{
    public class ArtikelRecord
    {
        public string ArtikelNr { get; set; }
        public string Omschrijving { get; set; }
        public DateTime Vanaf { get; set; }
        public DateTime LaatstGeupdate { get; set; }
        public decimal VorigeAantalGemaakt { get; set; }
        public decimal AantalGemaakt { get; set; }
        public double VorigeTijdGewerkt { get; set; }
        public double TijdGewerkt { get; set; }
        public List<string> UpdatedProducties { get; set; } = new List<string>();
        public List<ArtikelOpmerking> Opmerkingen { get; set; } = new List<ArtikelOpmerking>();
        public bool IsWerkplek { get; set; }
        public int PerUur => GetPerUur();

       public int GetPerUur()
       {
           if (AantalGemaakt == 0) return 0;
           if (TijdGewerkt == 0) return (int)AantalGemaakt;
           int peruur = (int)(AantalGemaakt / (decimal)TijdGewerkt);
           //if (UpdatedProducties.Count > 0)
           //    peruur /= UpdatedProducties.Count;
           return peruur;
       }

       public void ResetValues(bool opmerkingen)
       {
           UpdatedProducties?.Clear();
           Vanaf = new DateTime();
           VorigeAantalGemaakt = 0;
           AantalGemaakt = 0;
           VorigeTijdGewerkt = 0;
           TijdGewerkt = 0;
           if (opmerkingen)
               Opmerkingen?.Clear();
       }

       public string GetOpmerking(ArtikelOpmerking opmerking)
        {
            if (string.IsNullOrEmpty(opmerking?.Opmerking)) return string.Empty;
            return string.Format(opmerking.Opmerking, ArtikelNr, Omschrijving, opmerking.FilterWaarde, AantalGemaakt, TijdGewerkt, PerUur,
                UpdatedProducties.Count);
        }

        public string GetTitle(ArtikelOpmerking opmerking)
        {
            if (string.IsNullOrEmpty(opmerking?.Opmerking)) return string.Empty;
            return string.Format(opmerking.Title, ArtikelNr, Omschrijving,opmerking.FilterWaarde, AantalGemaakt, TijdGewerkt, PerUur,
                UpdatedProducties.Count);
        }

        public override bool Equals(object obj)
        {
            if (obj is ArtikelRecord record)
                return string.Equals(ArtikelNr, record.ArtikelNr, StringComparison.CurrentCultureIgnoreCase);
            if (obj is string value)
                return string.Equals(ArtikelNr, value, StringComparison.CurrentCultureIgnoreCase);
            return false;
        }

        public override int GetHashCode()
        {
            return ArtikelNr?.GetHashCode() ?? 0;
        }
    }
}
