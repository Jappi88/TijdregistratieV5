using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Forms.Sporen
{
    public class OptimaleVerbruikEntry
    {
        [OLVColumn(Width = 150, TextAlign = HorizontalAlignment.Left, ImageAspectName = "ImageIndex", DisplayIndex = 0)]
        public string ArtikelNr { get; set; }

        [OLVColumn(Width = 150, TextAlign = HorizontalAlignment.Left, DisplayIndex = 1)]
        public string Omschrijving { get; set; }

        [OLVColumn("Huidige Uitgangslengte", Width = 160, TextAlign = HorizontalAlignment.Left,
            AspectToStringFormat = "{0} mm", DisplayIndex = 2)]
        public decimal Uitgangslengte { get; set; }
        [OLVColumn(Width = 150, TextAlign = HorizontalAlignment.Left,AspectToStringFormat = "{0} mm", DisplayIndex = 3)]
        public decimal Productlengte { get; set; }
        [OLVColumn("Gewenste Reststuk", Width = 120, TextAlign = HorizontalAlignment.Left,AspectToStringFormat = "{0} mm", IsEditable = false, DisplayIndex = 4)]
        public decimal RestStuk { get; set; }

        [OLVColumn("Huidige Reststuk", Width = 120, TextAlign = HorizontalAlignment.Left,
            AspectToStringFormat = "{0} mm", IsEditable = false, DisplayIndex = 5)]
        public decimal HuidigeReststuk =>
            (decimal)Math.Round(
                (Uitgangslengte > Productlengte && Productlengte > 0 ? (Uitgangslengte % Productlengte) : 0), 2);

        [OLVColumn("Aantal Stuks", Width = 150, TextAlign = HorizontalAlignment.Left, IsEditable = false, DisplayIndex = 6)]
        public int AantalStuks => (Uitgangslengte > Productlengte && Productlengte > 0 ? (int)(Uitgangslengte / Productlengte) : 0);
        [OLVColumn("#Geproduceerd", Width = 120, TextAlign = HorizontalAlignment.Left, IsEditable = false, DisplayIndex = 7)]
        public int Geproduceerd { get; set; } = 1;

        public Dictionary<string, object> Changes { get; set; } = new Dictionary<string, object>();


        [OLVColumn(Width = 120, TextAlign = HorizontalAlignment.Left,AspectToStringFormat = "{0} mm", DisplayIndex = 8)]
        public decimal Voorkeur1 { get; set; }
        [OLVColumn( Width = 120, TextAlign = HorizontalAlignment.Left,
            AspectToStringFormat = "{0} mm", IsEditable = false, DisplayIndex = 9)]
        public decimal Reststuk1 =>
            (decimal)Math.Round(
                (Voorkeur1 > Productlengte && Productlengte > 0 ? (Voorkeur1 % Productlengte) : 0), 2);

        [OLVColumn("Aantal Stuks1", Width = 150, TextAlign = HorizontalAlignment.Left, IsEditable = false, DisplayIndex = 10)]
        public int Voorkeur1Stuks => (Voorkeur1 > Productlengte && Productlengte > 0 ? (int)(Voorkeur1 / Productlengte) : 0);



        [OLVColumn(Width = 120, TextAlign = HorizontalAlignment.Left,AspectToStringFormat = "{0} mm", DisplayIndex = 11)]
        public decimal Voorkeur2 { get; set; }
        [OLVColumn( Width = 120, TextAlign = HorizontalAlignment.Left,
            AspectToStringFormat = "{0} mm", IsEditable = false, DisplayIndex = 12)]
        public decimal Reststuk2 =>
            (decimal)Math.Round(
                (Voorkeur2 > Productlengte && Productlengte > 0 ? (Voorkeur2 % Productlengte) : 0), 2);

        [OLVColumn("Aantal Stuks2", Width = 150, TextAlign = HorizontalAlignment.Left, IsEditable = false, DisplayIndex = 13)]
        public int Voorkeur2Stuks =>
            (Voorkeur2 > Productlengte && Productlengte > 0 ? (int)(Voorkeur2 / Productlengte) : 0);




        [OLVColumn(Width = 120, TextAlign = HorizontalAlignment.Left,AspectToStringFormat = "{0} mm", DisplayIndex = 14)]
        public decimal Voorkeur3 { get; set; }
        [OLVColumn( Width = 120, TextAlign = HorizontalAlignment.Left,
            AspectToStringFormat = "{0} mm", IsEditable = false, DisplayIndex = 15)]
        public decimal Reststuk3 =>
            (decimal)Math.Round(
                (Voorkeur3 > Productlengte && Productlengte > 0 ? (Voorkeur3 % Productlengte) : 0), 2);
        [OLVColumn("Aantal Stuks3", Width = 150, TextAlign = HorizontalAlignment.Left, IsEditable = false, DisplayIndex = 16)]
        public int Voorkeur3Stuks => (Voorkeur3 > Productlengte && Productlengte > 0 ? (int)(Voorkeur3 / Productlengte) : 0);


        public int ImageIndex { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is OptimaleVerbruikEntry entry)
            {
                return string.Equals(entry.ArtikelNr, ArtikelNr, StringComparison.CurrentCultureIgnoreCase) && entry.Productlengte == Productlengte;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return ((ArtikelNr?.GetHashCode() ?? 0) ^ Productlengte.GetHashCode());
        }
    }
}
