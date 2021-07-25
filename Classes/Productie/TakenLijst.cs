using ProductieManager.Productie;

namespace ProductieManager.Classes.Productie
{
    public class TakenLijst
    {
        public TaakBeheer TaakManager { get; private set; }
        public ProductieFormulier Formulier { get; private set; }
        public Bewerking Bewerking { get; private set; }
        public Personeel Persoon { get; private set; }

        public TakenLijst(TaakBeheer manager, ProductieFormulier form)
        {
            TaakManager = manager;
            Formulier = form;
        }

        public TakenLijst(TaakBeheer manager, Bewerking bewerking)
        {
            TaakManager = manager;
            Bewerking = bewerking;
            Formulier = bewerking.Parent;
        }

        public TakenLijst(TaakBeheer manager, Personeel persoon, Bewerking bewerking)
        {
            Bewerking = bewerking;
            TaakManager = manager;
            Persoon = persoon;
        }

        public static Taak GetUpdatedTaak(Taak taak)
        {
            if (taak == null)
                return null;
            TakenLijst lijst;
            if (taak.Persoon != null)
                lijst = new TakenLijst(taak.Beheer, taak.Persoon, taak.Bewerking);
            else if (taak.Bewerking != null)
                lijst = new TakenLijst(taak.Beheer, taak.Bewerking);
            else lijst = new TakenLijst(taak.Beheer, taak.Formulier);
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

                case AktieType.None:
                    break;
            }
            xtaak.HashCode = taak.HashCode;
            return xtaak;
        }

        public Taak Telaat(TaakUrgentie urgentie)
        {
            Taak t;
            if (Bewerking != null)
                t = new Taak(TaakManager, Bewerking, AktieType.Telaat, urgentie);
            else t = new Taak(TaakManager, Formulier, AktieType.Telaat, urgentie);
            t.Beschrijving = WrapText($"[{t.ProductieNr}] '{t.Omschrijving}' is te laat!");
            return t;
        }

        public Taak Starten(TaakUrgentie urgentie)
        {
            Taak t;
            int persnodig = 0;
            if (Bewerking != null)
            {
                persnodig = Bewerking.AantalPersonenNodig();
                t = new Taak(TaakManager, Bewerking, AktieType.Beginnen, urgentie);
            }
            else t = new Taak(TaakManager, Formulier, AktieType.Beginnen, urgentie);

            string xn = persnodig == 1 ? "persoon" : "personen";
            if (persnodig > 0)
                t.Beschrijving = $"[{t.ProductieNr}] '{t.Omschrijving}' leverdatum nadert! en moet daarom gestart worden.\n Het is aanbevolen om met {persnodig} {xn} te werken.";
            else
                t.Beschrijving = $"[{t.ProductieNr}] '{t.Omschrijving}' leverdatum nadert! en moet z.s.m gestart worden.";
            t.Beschrijving = WrapText(t.Beschrijving);
            return t;
        }

        public Taak KlaarZetten(TaakUrgentie urgentie)
        {
            Taak t;
            if (Bewerking != null)
                t = new Taak(TaakManager, Bewerking, AktieType.KlaarZetten, urgentie);
            else t = new Taak(TaakManager, Formulier, AktieType.KlaarZetten, urgentie);
            t.Beschrijving = $"[{t.ProductieNr}] '{t.Omschrijving}'  kan alvast klaar gezet worden";
            t.Beschrijving = WrapText(t.Beschrijving);
            return t;
        }

        public Taak BewerkingGereedMelden(TaakUrgentie urgentie)
        {
            Taak t;
            if (Bewerking != null)
                t = new Taak(TaakManager, Bewerking, AktieType.BewerkingGereed, urgentie);
            else t = new Taak(TaakManager, Formulier, AktieType.BewerkingGereed, urgentie);
            t.Beschrijving = $"[{t.ProductieNr}] {t.Omschrijving} Bewerking is klaar en kan gereed gemeld worden!";
            t.Beschrijving = WrapText(t.Beschrijving);
            return t;
        }

        public Taak GereedMelden(TaakUrgentie urgentie)
        {
            Taak t;
            if (Bewerking != null)
                t = new Taak(TaakManager, Bewerking, AktieType.GereedMelden, urgentie);
            else t = new Taak(TaakManager, Formulier, AktieType.GereedMelden, urgentie);
            t.Beschrijving = $"[{t.ProductieNr}] Productie is klaar en kan gereed gemeld worden!";
            t.Beschrijving = WrapText(t.Beschrijving);
            return t;
        }

        public Taak Controleren(TaakUrgentie urgentie, string werkplek)
        {
            Taak t;
            if (Bewerking != null)
                t = new Taak(TaakManager, Bewerking, AktieType.ControleCheck, urgentie);
            else t = new Taak(TaakManager, Formulier, AktieType.ControleCheck, urgentie);
            t.Plek = werkplek;
            t.Beschrijving = $"[{t.ProductieNr}][{werkplek}] Het is tijd om '{t.Omschrijving}' te controleren!";
            t.Beschrijving = WrapText(t.Beschrijving);
            return t;
        }

        public Taak PersoneelVrij(TaakUrgentie urgentie)
        {
            Taak t;
            if (Persoon != null)
                t = new Taak(TaakManager, Bewerking, Persoon, AktieType.PersoneelVrij, urgentie);
            else t = new Taak(TaakManager, Formulier, AktieType.PersoneelVrij, urgentie);
            if (Persoon != null)
            {
                double tijd = Persoon.IsVrijOver();
                if (tijd > 0)
                    t.Beschrijving = $"[{t.ProductieNr}] Let OP!! {Persoon.PersoneelNaam} is over {tijd} uur vrij.";
                else t.Beschrijving = $"[{t.ProductieNr}] Let OP!! {Persoon.PersoneelNaam} is vrij!";
            }
            t.Beschrijving = WrapText(t.Beschrijving);
            return t;
        }

        public Taak PersoneelChange(TaakUrgentie urgentie)
        {
            Taak t;
            if (Bewerking != null)
                t = new Taak(TaakManager, Bewerking, AktieType.PersoneelChange, urgentie);
            else t = new Taak(TaakManager, Formulier, AktieType.PersoneelChange, urgentie);
            int pers = Bewerking == null ? (Formulier == null ? 0 : Formulier.AantalPersonenShifts().Length) : Bewerking.AantalPersonen;
            int persnodig = Bewerking == null ? (Formulier == null ? 0 : Formulier.AantalPersonenNodig()) : Bewerking.AantalPersonenNodig();
            string xn = persnodig == 1 ? "persoon" : "personen";
            string xn1 = pers == 1 ? "persoon" : "personen";
            if (persnodig == 0 && (Bewerking == null ? Formulier == null ? false : Formulier.TeLaat : Bewerking.TeLaat))
                t.Beschrijving = $"[{t.ProductieNr}] Leverdatum is te laat.Verzet de leverdatum of stop de productie en zet de {pers} {xn1} ergens anders in.";
            else if (persnodig == 0)
                t.Beschrijving = $"[{t.ProductieNr}] Het is niet meer nodig om mensen op dit klus te zetten. Je kan nu {pers} {xn1} ergens anders inzetten.";
            else if (pers > persnodig)
            {
                pers = pers - persnodig;
                xn = pers == 1 ? "kan" : "kunnen";
                xn1 = pers == 1 ? "persoon" : "personen";
                string xn2 = pers == 1 ? "is" : "zijn";
                t.Beschrijving = $"[{t.ProductieNr}] '{t.Omschrijving}' loopt goed! {pers} {xn1} {xn2} overbodig, en {xn} eventueel ergers anders voor worden ingezet.";
            }
            else
                t.Beschrijving = $"[{t.ProductieNr}] Aantal personeel bij '{t.Omschrijving}' moet aangepast worden om de datum te halen van {pers} naar {persnodig} {xn}";
            t.Beschrijving = WrapText(t.Beschrijving);
            return t;
        }

        public string WrapText(string text)
        {
            if (text == null)
                return "";
            int width = 60;
            string[] spl = text.Replace("\n", " ").Split(' ');
            string xreturn = "";
            int current = 0;
            foreach (string s in spl)
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