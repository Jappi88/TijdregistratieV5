using BrightIdeasSoftware;
using System;
using System.Collections.Generic;
using System.Windows.Documents;
using System.Windows.Forms;

namespace Forms.Sporen
{
    public class OptimaleVerbruikEntry
    {
        [OLVColumn(Width = 150, TextAlign = HorizontalAlignment.Left, ImageAspectName = "ImageIndex")]
        public string ArtikelNr { get; set; }

        [OLVColumn(Width = 150, TextAlign = HorizontalAlignment.Left)]
        public string Omschrijving { get; set; }

        [OLVColumn("Huidige Uitgangslengte", Width = 160, TextAlign = HorizontalAlignment.Left,
            AspectToStringFormat = "{0} mm")]
        public decimal Uitgangslengte { get; set; }
        [OLVColumn(Width = 150, TextAlign = HorizontalAlignment.Left,AspectToStringFormat = "{0} mm")]
        public decimal Productlengte { get; set; }
        [OLVColumn("Gewenste Reststuk", Width = 120, TextAlign = HorizontalAlignment.Left,AspectToStringFormat = "{0} mm", IsEditable = false)]
        public decimal RestStuk { get; set; }

        [OLVColumn("Huidige Reststuk", Width = 120, TextAlign = HorizontalAlignment.Left,
            AspectToStringFormat = "{0} mm", IsEditable = false)]
        public decimal HuidigeReststuk =>
            (decimal)Math.Round(
                (Uitgangslengte > Productlengte && Productlengte > 0 ? (Uitgangslengte % Productlengte) : 0), 2);

        [OLVColumn("Aantal Stuks", Width = 150, TextAlign = HorizontalAlignment.Left, IsEditable = false)]
        public int AantalStuks => (Uitgangslengte > Productlengte && Productlengte > 0 ? (int)(Uitgangslengte / Productlengte) : 0);
        [OLVColumn("#Geproduceerd", Width = 120, TextAlign = HorizontalAlignment.Left, IsEditable = false)]
        public int Geproduceerd { get; set; } = 1;

        public Dictionary<string, object> Changes { get; set; } = new Dictionary<string, object>();


        [OLVColumn(Width = 120, TextAlign = HorizontalAlignment.Left,AspectToStringFormat = "{0} mm")]
        public decimal Voorkeur1 { get; set; }
        [OLVColumn( Width = 120, TextAlign = HorizontalAlignment.Left,
            AspectToStringFormat = "{0} mm", IsEditable = false)]
        public decimal Reststuk1 =>
            (decimal)Math.Round(
                (Voorkeur1 > Productlengte && Productlengte > 0 ? (Voorkeur1 % Productlengte) : 0), 2);

        [OLVColumn("Aantal Stuks1", Width = 150, TextAlign = HorizontalAlignment.Left, IsEditable = false)]
        public int Voorkeur1Stuks => (Voorkeur1 > Productlengte && Productlengte > 0 ? (int)(Voorkeur1 / Productlengte) : 0);



        [OLVColumn(Width = 120, TextAlign = HorizontalAlignment.Left,AspectToStringFormat = "{0} mm")]
        public decimal Voorkeur2 { get; set; }
        [OLVColumn( Width = 120, TextAlign = HorizontalAlignment.Left,
            AspectToStringFormat = "{0} mm", IsEditable = false)]
        public decimal Reststuk2 =>
            (decimal)Math.Round(
                (Voorkeur2 > Productlengte && Productlengte > 0 ? (Voorkeur2 % Productlengte) : 0), 2);

        [OLVColumn("Aantal Stuks2", Width = 150, TextAlign = HorizontalAlignment.Left, IsEditable = false)]
        public int Voorkeur2Stuks =>
            (Voorkeur2 > Productlengte && Productlengte > 0 ? (int)(Voorkeur2 / Productlengte) : 0);




        [OLVColumn(Width = 120, TextAlign = HorizontalAlignment.Left,AspectToStringFormat = "{0} mm")]
        public decimal Voorkeur3 { get; set; }
        [OLVColumn( Width = 120, TextAlign = HorizontalAlignment.Left,
            AspectToStringFormat = "{0} mm", IsEditable = false)]
        public decimal Reststuk3 =>
            (decimal)Math.Round(
                (Voorkeur3 > Productlengte && Productlengte > 0 ? (Voorkeur3 % Productlengte) : 0), 2);
        [OLVColumn("Aantal Stuks3", Width = 150, TextAlign = HorizontalAlignment.Left, IsEditable = false)]
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
