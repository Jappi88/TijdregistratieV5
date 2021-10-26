using Rpm.Misc;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Rpm.Productie
{
    public static class TaakBeheer
    {

        public static Taak[] GetProductieTaken(this ProductieFormulier form)
        {
            try
            {
                var xreturn = new List<Taak>();
                if (form?.Bewerkingen == null || form.Bewerkingen.Length == 0)
                    return xreturn.ToArray();
                if (Manager.Opties == null || !Manager.Opties.GebruikTaken)
                    return xreturn.ToArray();
                var bws = form.Bewerkingen.Where(x =>
                        x.State != ProductieState.Verwijderd && x.State != ProductieState.Gereed && x.IsAllowed(null))
                    .ToArray();
                if (bws.Length == 0) return xreturn.ToArray();
                foreach(var bw in bws)
                {
                    var taken = GetBewerkingTaken(bw);
                    if (taken.Count > 0)
                        xreturn.AddRange(taken);
                }
                return xreturn.ToArray();
            }
            catch
            {
                return new Taak[] { };
            }
        }

        public static List<Taak> GetBewerkingTaken(Bewerking bew)
        {
            List<Taak> xreturn = new List<Taak>();
            try
            {
                if (bew == null) return xreturn;
                //Taak maken voor alle producties (behalve de verwijderde) waar een onderbreking nog voor open staat.
                if (bew.State != ProductieState.Verwijderd)
                {
                    if (bew.WerkPlekken != null)
                        foreach (var wp in bew.WerkPlekken)
                        {
                            var count = 0;
                            if (wp.Storingen != null)
                                count = wp.Storingen.Count(x => !x.IsVerholpen);
                            if (count > 0)
                            {
                                //maak taak aan.
                                var t = new TakenLijst(wp).Onderbreking(TaakUrgentie.ZSM);
                                if (t != null)
                                    xreturn.Add(t);
                            }
                        }
                }

                //Taken maken voor zowel een gestarte als een gestopte bewerking.
                if (bew.State == ProductieState.Gestart || bew.State == ProductieState.Gestopt)
                {
                    //Taak maken voor als de bewerking te laat is.
                    if (Manager.Opties.TaakAlsTelaat && bew.TeLaat)
                    {
                        var tijd = Werktijd.TijdGewerkt(bew.LeverDatum, DateTime.Now, null, null);
                        if (tijd.TotalSeconds > 0)
                        {
                            var t = new TakenLijst(bew).Telaat(TaakUrgentie.ZSM);
                            if (t != null)
                                xreturn.Add(t);
                        }
                    }

                    //Taak maken voor als de bewerking al gereed is.
                    if (Manager.Opties.TaakAlsGereed && bew.TotaalGemaakt >= bew.Aantal)
                    {
                        xreturn.Add(new TakenLijst(bew).BewerkingGereedMelden(TaakUrgentie.ZSM));
                    }
                }

                switch (bew.State)
                {
                    case ProductieState.Gestopt:

                        //taak maken als er een productie gestart moet worden.
                        if (Manager.Opties.TaakVoorStart && bew.TotaalGemaakt < bew.Aantal)
                        {
                            //ff kijken of de taak wel gestart moet worden, en of het tijd is om de taak wel te geven

                            var tijdvoorstart = TimeSpan.FromMinutes(Manager.Opties.MinVoorStart);
                            var realrooster = Manager.Opties.GetWerkRooster();
                            var rooster = realrooster;
                            var uiterlijk = bew.StartOp.Subtract(tijdvoorstart);
                            var urgentie = Taak.GetUrgentie(uiterlijk.AddHours(1));
                            if (Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster, realrooster, null) >= uiterlijk)
                            {
                                var t = new TakenLijst(bew).Starten(urgentie);
                                if (t != null)
                                    xreturn.Add(t);
                            }
                        }

                        //taak geven als de productie klaar gezet moet worden
                        if (Manager.Opties.TaakVoorKlaarZet &&
                            bew.GetMaterialen().Any(x => !x.IsKlaarGezet) && bew.TotaalGemaakt < bew.Aantal)
                        {
                            //ff kijken of de taak wel gestart moet worden, en of het tijd is om de taak wel te geven
                            var tijdvoorstart = TimeSpan.FromMinutes(Manager.Opties.MinVoorStart);
                            var tijdvoorklaarzet = TimeSpan.FromMinutes(Manager.Opties.MinVoorKlaarZet);
                            var realrooster = Manager.Opties.GetWerkRooster();
                            var rooster = realrooster;
                            var uiterlijk = bew.StartOp.Subtract(tijdvoorstart).Subtract(tijdvoorklaarzet);
                            var urgentie = Taak.GetUrgentie(uiterlijk.AddHours(1));
                            if (Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster, realrooster, null) >= uiterlijk)
                            {
                                var t = new TakenLijst(bew).KlaarZetten(urgentie);
                                if (t != null)
                                    xreturn.Add(t);
                            }
                        }
                        break;
                    case ProductieState.Gestart:
                        //Taak aanmaken voor een controle
                        if (Manager.Opties.TaakVoorControle)
                        {
                            if (bew.WerkPlekken is {Count: > 0})
                            {
                                foreach (var wp in bew.WerkPlekken)
                                {
                                    if (!wp.IsActief()) continue;
                                    if (wp.LaatstAantalUpdate < DateTime.Now.Subtract(TimeSpan.FromMinutes(Manager.Opties.MinVoorControle)))
                                    {
                                        xreturn.Add(new TakenLijst(wp).Controleren(TaakUrgentie.ZSM, wp));
                                    }
                                }
                            }
                        }

                        //Taak aanmaken voor het wisselen van personeel.
                        if (Manager.Opties.TaakVoorPersoneel && bew.IsBemand)
                        {
                            var dt = DateTime.Now;
                            var pers = bew.AantalPersonenNodig(ref dt, false);
                            if (bew.AantalActievePersonen != pers && pers > 0)
                            {
                                xreturn.Add(new TakenLijst(bew).PersoneelChange(TaakUrgentie.ZSM));
                            }
                        }

                        //Taak aanmaken voor personeel die vrij is tijdens een actieve bewerking
                        if (Manager.Opties.TaakPersoneelVrij)
                        {
                            foreach (var pers in bew.GetPersoneel())
                            {
                                if (!pers.IsBezig)
                                    continue;
                                //tijd dat we kijken of iemand vrij is (8uur)
                                var tijd = pers.IsVrijOver();
                                if (tijd is > -1 and <= 8)
                                {
                                    var t = new TakenLijst(pers, bew).PersoneelVrij(TaakUrgentie.ZodraMogelijk);
                                    if (t != null)
                                        xreturn.Add(t);
                                }
                            }
                        }
                        break;
                    case ProductieState.Gereed:
                        break;
                    case ProductieState.Verwijderd:
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return xreturn;
        }


    }
}