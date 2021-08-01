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
                //een taak geven als personeel vrij is/word
                if (Manager.Opties.TaakPersoneelVrij)
                    foreach (var b in bws)
                        if (b.State == ProductieState.Gestart)
                            foreach (var pers in b.GetPersoneel())
                            {
                                if (!pers.IsBezig)
                                    continue;
                                //tijd dat we kijken of iemand vrij is (8uur)
                                var tijd = pers.IsVrijOver();
                                if (tijd > -1 && tijd <= 8)
                                {
                                    var t = new TakenLijst(pers, b).PersoneelVrij(TaakUrgentie.ZodraMogelijk);
                                    if (t != null)
                                        xreturn.Add(t);
                                }
                            }

                //Taak aanmaken als een productie te laat is.
                if (Manager.Opties.TaakAlsTelaat && form.TeLaat && form.State != ProductieState.Gereed &&
                    form.State != ProductieState.Verwijderd)
                {
                    var t = new TakenLijst(form).Telaat(TaakUrgentie.ZSM);
                    if (t != null)
                        xreturn.Add(t);
                }

                //taak maken als er een productie gestart moet worden.
                if (Manager.Opties.TaakVoorStart && !form.TeLaat)
                {
                    //ff kijken of de taak wel gestart moet worden, en of het tijd is om de taak wel te geven
                    var tijdvoorstart = TimeSpan.FromMinutes(Manager.Opties.MinVoorStart);
                    //var tijdnodig = form.TijdNodig();
                    //tijdvoorstart = Werktijd.AantalWerkdagen(tijdvoorstart.Add(tijdnodig), null);

                    foreach (var b in bws)
                        if (b.State != ProductieState.Gestart && b.TotaalGemaakt < b.Aantal)
                        {
                            var realrooster = Manager.Opties.GetWerkRooster();
                            var rooster = realrooster;
                            var uiterlijk = b.StartOp.Subtract(tijdvoorstart);
                            var urgentie = Taak.GetUrgentie(uiterlijk.AddHours(1));
                            if (Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster, realrooster,null) >= uiterlijk)
                            {
                                var t = new TakenLijst(b).Starten(urgentie);
                                if (t != null)
                                    xreturn.Add(t);
                            }
                        }
                }

                //taak geven als de productie klaar gezet moet worden
                if (Manager.Opties.TaakVoorKlaarZet && !form.TeLaat && form.State == ProductieState.Gestopt &&
                    form.Materialen.Any(x => !x.IsKlaarGezet) && form.TotaalGemaakt < form.Aantal)
                {
                    //ff kijken of de taak wel gestart moet worden, en of het tijd is om de taak wel te geven
                    var tijdvoorstart = TimeSpan.FromMinutes(Manager.Opties.MinVoorStart);
                    var tijdvoorklaarzet = TimeSpan.FromMinutes(Manager.Opties.MinVoorKlaarZet);
                    var realrooster = Manager.Opties.GetWerkRooster();
                    var rooster = realrooster;
                    var uiterlijk = form.StartOp.Subtract(tijdvoorstart).Subtract(tijdvoorklaarzet);
                    var urgentie = Taak.GetUrgentie(uiterlijk.AddHours(1));
                    if (Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster, realrooster, null) >= uiterlijk)
                    {
                        var t = new TakenLijst(form).KlaarZetten(urgentie);
                        if (t != null)
                            xreturn.Add(t);
                    }
                }

                //taak geven als de productie gereed gemeld moet worden
                if (Manager.Opties.TaakAlsGereed &&
                    form.State != ProductieState.Verwijderd && form.State != ProductieState.Gereed)
                {
                    xreturn.AddRange(from b in bws
                        where b.State != ProductieState.Verwijderd && b.State != ProductieState.Gereed &&
                              b.AantalGemaakt >= b.Aantal
                        select new TakenLijst(b).BewerkingGereedMelden(TaakUrgentie.ZSM)
                        into t
                        where t != null
                        select t);
                }

                //Taak geven voor als het aantal personeel aangepast moet worden per bewerking.
                if (Manager.Opties.TaakVoorPersoneel)
                    if (form.Bewerkingen != null)
                    {
                        var dt = DateTime.Now;
                        xreturn.AddRange(from b in bws
                            where b.IsBemand
                            let pers = b.AantalPersonenNodig(ref dt,true)
                            where b.AantalActievePersonen != pers && pers > 0 && b.State == ProductieState.Gestart
                            select new TakenLijst(b).PersoneelChange(TaakUrgentie.ZSM)
                            into t
                            where t != null
                            select t);

                    }

                //Taak geven voor als er een onderbreking open staat
                if (Manager.Opties.TaakVoorOnderbreking)
                    if (form.Bewerkingen != null)
                        foreach (var b in bws)
                            if (b.WerkPlekken != null)
                                foreach (var wp in b.WerkPlekken)
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

                //Taak geven als de productie gecontroleerd moet worden
                if (Manager.Opties.TaakVoorControle)
                    xreturn.AddRange(from b in bws
                        where b.WerkPlekken != null && b.WerkPlekken.Count > 0
                        from plek in b.WerkPlekken
                        where plek.LaatstAantalUpdate <
                              DateTime.Now.Subtract(TimeSpan.FromMinutes(Manager.Opties.MinVoorControle)) &&
                              b.State == ProductieState.Gestart && plek.IsActief()
                        select new TakenLijst(plek).Controleren(TaakUrgentie.ZSM, plek)
                        into t
                        where t != null
                        select t);

                //else if (b.LaatstAantalUpdate < DateTime.Now.Subtract(TimeSpan.FromMinutes(Manager.Opties.MinVoorControle)) && b.State == ProductieState.Gestart)
                //{
                //    Taak t = new TakenLijst(this, b).Controleren(TaakUrgentie.ZSM, n);
                //    t.OnUpdate += TaakUpdated;
                //    t.OnVereistAandacht += TaakVereistAandacht;
                //    xreturn.Add(t);
                //}

                return xreturn.ToArray();
            }
            catch
            {
                return new Taak[] { };
            }
        }
    }
}