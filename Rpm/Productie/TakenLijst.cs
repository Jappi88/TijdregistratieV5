using System;
using System.Linq;
using Rpm.Various;

namespace Rpm.Productie
{
    public class TakenLijst
    {
        public TakenLijst()
        {
        }

        public TakenLijst(ProductieFormulier form)
        {
            Formulier = form;
        }

        public TakenLijst(Bewerking bewerking)
        {
            Bewerking = bewerking;
            Formulier = bewerking?.Parent;
        }

        public TakenLijst(Personeel persoon, Bewerking bewerking)
        {
            Bewerking = bewerking;
            Formulier = bewerking?.Parent;
            Persoon = persoon;
        }

        public TakenLijst(ProductieFormulier form, Personeel persoon, Bewerking bewerking,
            WerkPlek plek)
        {
            Bewerking = bewerking;
            Formulier = form ?? bewerking?.Parent;
            Plek = plek;
            Persoon = persoon;
        }

        public TakenLijst(WerkPlek plek)
        {
            Plek = plek;
            Bewerking = plek.Werk;
            Formulier = plek?.Werk?.Parent;
        }

        //public TaakBeheer TaakManager { get; }
        public ProductieFormulier Formulier { get; }
        public Bewerking Bewerking { get; }
        public Personeel Persoon { get; }
        public WerkPlek Plek { get; }

        public static Taak GetUpdatedTaak(Taak taak)
        {
            if (taak == null)
                return null;
            var lijst = new TakenLijst(taak.Formulier, taak.Persoon, taak.Bewerking, taak.Plek);
            Taak xtaak = null;
            switch (taak.Type)
            {
                case AktieType.ControleCheck:
                    xtaak = lijst.Controleren(taak.Urgentie, taak.Plek);
                    break;

                case AktieType.Beginnen:
                    xtaak = lijst.Starten(taak.Urgentie);
                    break;

                case AktieType.GereedMelden:
                    xtaak = lijst.GereedMelden(taak.Urgentie);
                    break;

                case AktieType.BewerkingGereed:
                    xtaak = lijst.BewerkingGereedMelden(taak.Urgentie);
                    break;

                case AktieType.KlaarZetten:
                    xtaak = lijst.KlaarZetten(taak.Urgentie);
                    break;

                case AktieType.Stoppen:
                    break;

                case AktieType.PersoneelChange:
                    xtaak = lijst.PersoneelChange(taak.Urgentie);
                    break;

                case AktieType.PersoneelVrij:
                    xtaak = lijst.PersoneelVrij(taak.Urgentie);
                    break;

                case AktieType.Telaat:
                    xtaak = lijst.Telaat(taak.Urgentie);
                    break;
                case AktieType.Onderbreking:
                    xtaak = lijst.Onderbreking(taak.Urgentie);
                    break;
                case AktieType.None:
                    break;
            }

            if (xtaak != null)
                xtaak.HashCode = taak.HashCode;
            return xtaak;
        }

        public Taak Telaat(TaakUrgentie urgentie)
        {
            Taak t;
            if (Bewerking != null)
                t = new Taak(Bewerking, AktieType.Telaat, urgentie);
            else t = new Taak(Formulier, AktieType.Telaat, urgentie);
            var leverdatum = Bewerking?.LeverDatum ?? Formulier.LeverDatum;
            var tijd = Werktijd.TijdGewerkt(leverdatum, DateTime.Now, null, null);
            t.Beschrijving = $"Productie is {Math.Round(tijd.TotalHours, 2)} uur te laat!";
            return t;
        }

        public Taak Starten(TaakUrgentie urgentie)
        {
            Taak t;
            var persnodig = 0;
            var tstarten = DateTime.Now;
            if (Bewerking != null)
            {
                persnodig = Bewerking.AantalPersonenNodig(ref tstarten, false);
                t = new Taak(Bewerking, AktieType.Beginnen, urgentie);
            }
            else
            {
                persnodig = Formulier.AantalPersonenNodig(ref tstarten);
                t = new Taak(Formulier, AktieType.Beginnen, urgentie);
            }

            var tijdnodig = Bewerking?.WerkTijdNodigTotLeverdatum() ??
                            Formulier?.WerkTijdNodigTotLeverdatum() ?? new TimeSpan();
            var naam = Bewerking?.Naam ?? Formulier.ProductieNr;
            var tijd = Math.Round(tijdnodig.TotalHours, 2);
            var xn = persnodig == 1 ? "persoon" : "personen";
            if (persnodig > 0)
                t.Beschrijving =
                    $"Over {tijd} uur moet {naam} klaar zijn.\r\n " +
                    $"Het is aanbevolen om uiterlijk op {tstarten:dddd dd MMMM HH:mm} uur te starten met {persnodig} {xn}.";
            else
                t.Beschrijving = $"Over {tijd} uur moet {naam} klaar zijn.\r\n " +
                                 $"Het is aanbevolen om uiterlijk op {tstarten:dddd dd MMMM HH:mm} uur te starten!";
            return t;
        }

        public Taak KlaarZetten(TaakUrgentie urgentie)
        {
            Taak t;
            if (Bewerking != null)
                t = new Taak(Bewerking, AktieType.KlaarZetten, urgentie);
            else t = new Taak(Formulier, AktieType.KlaarZetten, urgentie);
            var xmats = Bewerking?.GetMaterialen() ?? Formulier.Materialen;
            if (xmats.Count > 0)
            {
                var xn = xmats.Count == 1 ? "materiaal" : "materialen";
                var xn1 = xmats.Count == 1 ? $"{xmats[0].ArtikelNr} | {xmats[0].Omschrijving}" : $"{xmats.Count} {xn}";
                t.Beschrijving = $"Je kan nu {xn1} klaarzetten.";
            }
            else
            {
                t.Beschrijving = "Je kan nu de materialen klaarzetten.";
            }

            return t;
        }

        public Taak BewerkingGereedMelden(TaakUrgentie urgentie)
        {
            Taak t;
            if (Bewerking != null)
                t = new Taak(Bewerking, AktieType.BewerkingGereed, urgentie);
            else t = new Taak(Formulier, AktieType.BewerkingGereed, urgentie);
            t.Beschrijving = "Bewerking is klaar en kan gereed gemeld worden!";
            return t;
        }

        public Taak GereedMelden(TaakUrgentie urgentie)
        {
            Taak t;
            if (Bewerking != null)
                t = new Taak(Bewerking, AktieType.GereedMelden, urgentie);
            else t = new Taak(Formulier, AktieType.GereedMelden, urgentie);
            t.Beschrijving = "Productie is klaar en kan gereed gemeld worden!";
            return t;
        }

        public Taak Controleren(TaakUrgentie urgentie, WerkPlek werkplek)
        {
            var t = new Taak(werkplek, AktieType.ControleCheck, urgentie);
            var xtijd = werkplek.LaatsAantalUpdateMinutes();
            var xvalue = xtijd == 0
                ? "Net gecontroleerd"
                : Math.Round(TimeSpan.FromMinutes(xtijd).TotalHours, 2) + " uur voor het laatst";
            t.Beschrijving = $"Je hebt {xvalue} gecontroleerd.\r\n " +
                             "Het is nu weer tijd om te controleren.";
            return t;
        }

        public Taak PersoneelVrij(TaakUrgentie urgentie)
        {
            Taak t;
            if (Persoon != null)
                t = new Taak(Bewerking, Persoon, AktieType.PersoneelVrij, urgentie);
            else t = new Taak(Formulier, AktieType.PersoneelVrij, urgentie);
            if (Persoon != null)
            {
                var tijd = Persoon.IsVrijOver();
                if (tijd > 0)
                    t.Beschrijving = $"Let OP!! {Persoon.PersoneelNaam} is over {tijd} uur vrij.";
                else t.Beschrijving = $"Let OP!! {Persoon.PersoneelNaam} is vrij!";
            }

            return t;
        }

        public Taak Onderbreking(TaakUrgentie urgentie)
        {
            var t = new Taak();
            t.Beschrijving = "Geen actieve onderbrekeningen";
            if (Plek != null)
            {
                t = new Taak(Plek, AktieType.Onderbreking, urgentie);
                var count = Plek.Storingen?.Where(x => !x.IsVerholpen).Count() ?? 0;
                if (count > 0)
                {
                    var value = count == 1 ? "onderbreking" : "onderbrekeningen";
                    var value2 = count == 1 ? "staat" : "staan";
                    t.Beschrijving = $"Let Op!! Er {value2} {count} actieve {value} open!";
                }
            }

            return t;
        }

        public Taak PersoneelChange(TaakUrgentie urgentie)
        {
            Taak t;
            if (Bewerking != null)
                t = new Taak(Bewerking, AktieType.PersoneelChange, urgentie);
            else t = new Taak(Formulier, AktieType.PersoneelChange, urgentie);
            var pers = Bewerking?.AantalActievePersonen ?? (Formulier?.AantalPersonenShifts().Length ?? 0);
            var starten = DateTime.Now;
            var leverdatum = Bewerking?.LeverDatum ?? Formulier?.LeverDatum ?? DateTime.Now;
            var persnodig = Bewerking?.AantalPersonenNodig(ref starten, false) ??
                            (Formulier?.AantalPersonenNodig(ref starten) ?? 0);
            var xn = persnodig == 1 ? "persoon" : "personen";
            var xn1 = pers == 1 ? "persoon" : "personen";
            if (persnodig == 0 && (Bewerking?.TeLaat ?? (Formulier?.TeLaat ?? false)))
            {
                t.Beschrijving =
                    "Leverdatum is te laat!.\r\n " +
                    $"Verzet de leverdatum of stop de productie en zet de {pers} {xn1} ergens anders in.";
            }
            else if (persnodig == 0)
            {
                t.Beschrijving =
                    "Het is niet meer nodig om mensen op dit klus te zetten.\r\n " +
                    $"Je kan nu {pers} {xn1} ergens anders inzetten.";
            }
            else if (pers > persnodig)
            {
                if (starten < DateTime.Now) return null;
                pers = pers - persnodig;
                xn = pers == 1 ? "kan" : "kunnen";
                xn1 = pers == 1 ? "persoon" : "personen";
                var xn2 = pers == 1 ? "is" : "zijn";
                t.Beschrijving = "Productie loopt goed!\r\n " +
                                 $"{pers} {xn1} {xn2} overbodig, en {xn} eventueel ergers anders voor worden ingezet.";
            }
            else
            {
                t.Beschrijving =
                    $"Aantal personeel moet aangepast worden om de datum te halen van {pers} naar {persnodig} {xn}.";
            }

            return t;
        }

        public string WrapText(string text)
        {
            if (text == null)
                return "";
            var width = 60;
            var spl = text.Replace("\n", " ").Split(' ');
            var xreturn = "";
            var current = 0;
            foreach (var s in spl)
            {
                xreturn += s;
                if (current >= width)
                {
                    current = 0;
                    xreturn += " \n";
                }
                else
                {
                    current += s.Length;
                    xreturn += " ";
                }
            }

            return xreturn;
        }
    }
}