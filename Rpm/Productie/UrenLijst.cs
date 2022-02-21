﻿using System;
using System.Collections.Generic;
using System.Linq;
using Rpm.Misc;

namespace Rpm.Productie
{
    public class UrenLijst
    {
        internal Rooster _rooster;

        public UrenLijst()
        {
        }

        public UrenLijst(TijdEntry[] uren)
        {
            Uren = uren.ToList();
        }

        public UrenLijst(List<TijdEntry> uren)
        {
            Uren = uren;
        }

        public List<TijdEntry> Uren { get; private set; } = new();

        public Rooster WerkRooster
        {
            get => _rooster;
            set => _rooster = value;
        }

        public List<Rooster> SpecialeRoosters { get; set; }

        public int Count => Uren?.Count ?? 0;

        public bool IsActief => Uren != null && Uren.Any(x => x.InUse);

        public TijdEntry GetTijd(DateTime start, DateTime stop)
        {
            return Uren.FirstOrDefault(x => x.Equals(new TijdEntry(start, stop, WerkRooster)));
        }

        public void SetUren(TijdEntry[] uren, bool isactief, bool isnew)
        {
            if (Uren == null || isnew) Uren = new List<TijdEntry>();
            if (uren is {Length: > 0})
            {
                var isbussy = false;
                foreach (var tijd in uren)
                {
                    isbussy |= tijd.InUse;
                    Add(tijd.CreateCopy());
                }

                //UpdateUrenRooster(false,null);
                if (isactief && !isbussy)
                    UpdateTijdGewerkt(DateTime.Now, DateTime.Now, true);
            }
        }

        public void UpdateUrenRooster(bool dospecialrooster, Rooster rooster)
        {
            lock (Uren)
            {
                if (Uren is {Count: > 0})
                {
                    SpecialeRoosters ??= new List<Rooster>();
                    rooster ??= WerkRooster;
                    var currooster = rooster == null || !rooster.IsValid()
                        ? Manager.Opties?.GetWerkRooster() ?? Rooster.StandaartRooster()
                        : rooster;
                    var xflag = WerkRooster != null && !WerkRooster.SameTijden(currooster);
                    WerkRooster = currooster;
                    if (dospecialrooster && Manager.Opties != null)
                    {
                        var removed = SpecialeRoosters.RemoveAll(x =>
                            Manager.Opties.SpecialeRoosters.All(p => x.Vanaf.Date != p.Vanaf.Date));
                        for (var i = 0; i < Uren.Count; i++)
                        {
                            var xent = Uren[i];
                            if (xent.ExtraTijd != null || !xent.InUse) continue;
                            var xspc = Manager.Opties.SpecialeRoosters
                                ?.Where(x =>
                                    new TijdEntry(x.Vanaf.Date, x.Vanaf.Date.Add(x.EindWerkdag)).ContainsBereik(xent))
                                .ToList();
                            //var xspc = Manager.Opties?.SpecialeRoosters
                            //   ?.Where(x => (x.Vanaf.Date >= xent.Start.Date && x.Vanaf.Date <= xent.Stop.Date) && (
                            //  xent.Stop.TimeOfDay >= x.StartWerkdag && ((x.Vanaf.Date != xent.Start.Date || (xent.Start.TimeOfDay <= x.EindWerkdag))))).ToList();
                            xspc = xspc?.Where(x => SpecialeRoosters.All(s => s.Vanaf.Date != x.Vanaf.Date)).ToList();

                            if (xspc is {Count: > 0})
                                SpecialeRoosters.AddRange(xspc);
                        }
                    }

                    //if(xflag && Uren.Any(x=> x.InUse))
                    //{
                    //    SetStop();
                    //    SetStart();
                    //}
                    for (var i = 0; i < Uren.Count; i++)
                    {
                        var xent = Uren[i];
                        if (xent.ExtraTijd != null) continue;
                        if (xent.InUse)
                        {
                            xent.WerkRooster = currooster;
                            break;
                        }
                    }
                }
            }
        }

        public Rooster GetTodayRooster()
        {
            var dt = DateTime.Now;
            return SpecialeRoosters?.FirstOrDefault(x => x.Vanaf.Date == dt.Date) ?? WerkRooster;
        }

        public bool Remove(TijdEntry entry)
        {
            lock (Uren)
            {
                return Uren.Remove(entry);
            }
        }

        public int RemoveAllEmpty()
        {
            lock (Uren)
            {
                return Uren.RemoveAll(x =>
                    x.TijdGewerkt(x.WerkRooster ?? WerkRooster, null, SpecialeRoosters) == 0 && !x.InUse);
            }
        }

        public bool ContainsBereik(TijdEntry bereik)
        {
            try
            {
                if (Count == 0) return false;
                var xstart = bereik.Start;
                var xstop = bereik.Stop;
                if (bereik.Start.TimeOfDay == new TimeSpan())
                {
                    var xstartr = SpecialeRoosters.FirstOrDefault(x => x.Vanaf.Date == bereik.Start.Date) ??
                                  WerkRooster;
                    if (xstartr != null)
                        xstart = bereik.Start.Add(xstartr.StartWerkdag);
                }

                if (bereik.Stop.TimeOfDay == new TimeSpan())
                {
                    var xstartr = SpecialeRoosters.FirstOrDefault(x => x.Vanaf.Date == bereik.Stop.Date) ??
                                  WerkRooster;
                    if (xstartr != null)
                        xstop = bereik.Stop.Add(xstartr.EindWerkdag);
                }

                foreach (var uur in Uren)
                {
                    if (uur.Start >= xstart && uur.Start < xstop)
                        return true;
                    if (uur.Start <= xstart && uur.Stop > xstop)
                        return true;
                }

                return Uren.Any(x =>
                    x.Start >= xstart && x.Start < xstop ||
                    x.Stop > xstart && x.Stop <= xstop);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public TijdEntry Add(TijdEntry entry)
        {
            var x = false;
            return Add(entry, ref x);
        }

        public TijdEntry Add(TijdEntry entry, ref bool isnew, bool reorder = true)
        {
            var changed = false;
            var rooster = WerkRooster == null || !WerkRooster.IsCustom()
                ? Manager.Opties?.GetWerkRooster()
                : WerkRooster;
            if (entry.WerkRooster == null || !entry.WerkRooster.IsCustom())
                entry.WerkRooster = rooster;
            lock (Uren)
            {
                if (Uren.Count == 0)
                {
                    Uren.Add(entry.CreateCopy());
                    isnew = true;
                    return entry;
                }
            }

            //controlleer eerst of de entry extra tijd is, en of we die kunnen toevoegen.
            if (entry.ExtraTijd != null)
            {
                lock (Uren)
                {
                    if (!Uren.Any(x => x.ExtraTijd != null && x.ExtraTijd.Equals(entry.ExtraTijd)))
                    {
                        Uren.Add(entry.CreateCopy());
                        return entry;
                    }
                }

                return entry;
            }

            //Het snelste is om eerst te kijken of dit al een actieve tijd lijn is, en of de nieuwe tijd wel meer dan 0 is.
            if (entry.TijdGewerkt(entry.WerkRooster ?? rooster, null, SpecialeRoosters) == 0 && entry.InUse &&
                Uren.Any(x => x.InUse))
                return entry;

            var xent = Uren.FirstOrDefault(x => x.ID != 0 && entry.ID != 0 && entry.ID == x.ID);
            if (xent != null)
            {
                xent.Start = entry.Start;
                xent.Stop = entry.Stop;
                if (xent.WerkRooster == null || !xent.WerkRooster.IsCustom())
                    if (entry.WerkRooster != null && entry.WerkRooster.IsCustom())
                        xent.WerkRooster = entry.WerkRooster;
                    else xent.WerkRooster = rooster;
                if (!xent.InUse && entry.InUse)
                {
                    lock (Uren)
                    {
                        Uren.ForEach(x => x.InUse = false);
                    }

                    xent.InUse = true;
                }

                changed = true;
            }

            if (!changed)
            {
                xent = Uren.FirstOrDefault(x =>
                    entry.Start >= x.Start && entry.Start < x._gestopt);
                if (xent != null)
                    //we hebben een entry gevonden waar de start tijd tussen zit.
                    //we willen alleen de stop veranderen en eventueel de rooster
                    if (entry.Stop > xent.Stop)
                    {
                        xent.Stop = entry.Stop;
                        if (xent.WerkRooster == null || !xent.WerkRooster.IsCustom())
                            if (entry.WerkRooster != null && entry.WerkRooster.IsCustom())
                                xent.WerkRooster = entry.WerkRooster;
                            else xent.WerkRooster = rooster;
                        if (!xent.InUse && entry.InUse)
                        {
                            lock (Uren)
                            {
                                Uren.ForEach(x => x.InUse = false);
                            }

                            xent.InUse = true;
                        }

                        changed = true;
                    }
            }

            if (!changed)
            {
                //we gaan zoeken naar een tijd waarvan de niewe tijd stop tussen valt, en alleen de start van aangepast hoeft te worden.
                xent = Uren.FirstOrDefault(x =>
                    entry._gestopt > x.Start && entry._gestopt <= x._gestopt ||
                    entry._gestopt == x._gestopt);
                if (xent != null)
                    //we hebben een entry gevonden waar de stop tijd tussen zit.
                    //we willen alleen de start veranderen en eventueel de rooster
                    if (entry.Start < xent.Start)
                    {
                        xent.Start = entry.Start;
                        if (xent.WerkRooster == null || !xent.WerkRooster.IsCustom())
                            if (entry.WerkRooster != null && entry.WerkRooster.IsCustom())
                                xent.WerkRooster = entry.WerkRooster;
                            else xent.WerkRooster = rooster;
                        if (!xent.InUse && entry.InUse)
                        {
                            lock (Uren)
                            {
                                Uren.ForEach(x => x.InUse = false);
                            }

                            xent.InUse = true;
                        }

                        changed = true;
                    }
            }

            if (!changed)
            {
                xent = Uren.LastOrDefault(x => x.ContainsBereik(entry));
                if (xent != null)
                {
                    xent.InUse = entry.InUse;
                    xent.Stop = entry.Stop;
                    xent.WerkRooster = entry.WerkRooster;
                }
                else
                {
                    lock (Uren)
                    {
                        if (entry.InUse)
                            Uren.ForEach(x => x.InUse = false);

                        Uren.Add(entry.CreateCopy());
                    }
                }
            }

            //we gaan alle tijden pakken die tussen andere tijden zitten.

            if (Uren.Count > 1)
            {
                var toremove = Uren.Where(x => x.ExtraTijd == null && Uren.Any(s =>
                            s.ExtraTijd == null && x.Start >= s.Start && x._gestopt < s._gestopt ||
                            x.Start > s.Start && x._gestopt <= s._gestopt) || !x.InUse &&
                        x.TijdGewerkt(x.WerkRooster ?? rooster, null, SpecialeRoosters) <= 0)
                    .ToArray();
                lock (Uren)
                {
                    foreach (var old in toremove)
                    {
                        Uren.Remove(old);
                        //tijd die ingebruik is maar wel verwijderd word moeten we natuurlijk in andere in plaats geven.
                        if (old.InUse)
                            Uren.Add(new TijdEntry(DateTime.Now, DateTime.Now, WerkRooster)
                                {WerkRooster = old.WerkRooster, InUse = true});
                    }
                }

                lock (Uren)
                {
                    if (Uren.Count > 1)
                        for (var i = 0; i < Uren.Count; i++)
                        {
                            var te = Uren[i];
                            for (var j = 0; j < Uren.Count; j++)
                            {
                                if (j == i) continue;
                                var xte = Uren[j];
                                if (xte.Start < te.Stop && xte.Stop > te.Start)
                                {
                                    if (xte.Stop > te.Stop)
                                        te.Stop = xte.Stop;
                                    if (xte.InUse)
                                        te.InUse = true;
                                    Uren.Remove(xte);
                                    j--;
                                }
                            }
                        }
                }
            }

            lock (Uren)
            {
                if (reorder && Uren.Count > 1)
                    Uren = Uren.OrderBy(x => x.Start).ToList();
            }

            isnew = !changed;
            return entry;
        }

        public void AddRange(TijdEntry[] tijden)
        {
            if (tijden?.Length > 0)
            {
                foreach (var t in tijden)
                {
                    var x = false;
                    Add(t, ref x, false);
                }

                lock (Uren)
                {
                    Uren = Uren.OrderBy(x => x.Start).ToList();
                }
            }
        }

        public TijdEntry Add(DateTime start, DateTime stop, string omschrijving = "")
        {
            var entry = new TijdEntry(omschrijving, start, stop, WerkRooster);
            Add(entry);
            return entry;
        }

        public TijdEntry Add(DateTime start, DateTime stop, bool isactief, string omschrijving = "")
        {
            var entry = new TijdEntry(omschrijving, start, stop, WerkRooster) {InUse = isactief};
            Add(entry);
            return entry;
        }


        public TijdEntry UpdateTijdGewerkt(DateTime start, DateTime stop, bool isactief)
        {
            return Add(start, stop, isactief);
        }


        public TijdEntry UpdateTijdGewerkt(TijdEntry entry)
        {
            return Add(entry);
        }

        public double TijdGewerkt(Rooster rooster, Dictionary<DateTime, DateTime> exclude)
        {
            if (Manager.Opties == null || Uren == null || Uren.Count == 0)
                return 0;
            rooster ??= WerkRooster;
            if (rooster == null || !rooster.IsValid())
                rooster = Manager.Opties?.GetWerkRooster() ?? Rooster.StandaartRooster();
            var extra = Uren.Where(x => x.ExtraTijd is {Tijd: {TotalHours: > 0}}).Select(x => x.ExtraTijd)
                .ToList();
            var tijden = Uren.Where(x => x.ExtraTijd == null).ToArray();
            if (tijden.Length == 0)
                return extra.Sum(x => x.ExtraUren(null, rooster));
            double tijd = 0;
            double extratijd = 0;
            var start = GetFirstStart();
            var stop = GetLastStop();

            foreach (var xt in extra)
            {
                if (xt.Tijd.TotalHours <= 0) continue;
                extratijd += xt.ExtraUren(new TijdEntry(start, stop, rooster), rooster);
            }

            foreach (var t in tijden) tijd += t.TijdGewerkt(rooster, exclude, SpecialeRoosters);

            return tijd + extratijd;
        }

        public double TijdGewerkt(Rooster rooster, DateTime Vanaf, DateTime tot, Dictionary<DateTime, DateTime> exclude)
        {
            if (Uren == null || Uren.Count == 0)
                return 0;
            rooster ??= WerkRooster;
            if (rooster == null || !rooster.IsValid())
                rooster = Manager.Opties?.GetWerkRooster() ?? Rooster.StandaartRooster();
            var xextra = Uren.Where(x => x.ExtraTijd is {Tijd: {TotalHours: > 0}}).Select(x => x.ExtraTijd)
                .ToList();
            var extra = new List<ExtraTijd>();
            xextra.ForEach(x =>
            {
                var xtmp = x.CreateRange(Vanaf, tot);
                if (xtmp != null) extra.Add(xtmp);
            });

            var xtijden = Uren.Where(x => x.ExtraTijd == null).ToList();

            var tijden = new List<TijdEntry>();
            double tijd = 0;
            xtijden.ForEach(x =>
            {
                var xtmp = x.CreateRange(Vanaf, tot, x.WerkRooster ?? WerkRooster, SpecialeRoosters);
                if (xtmp != null)
                {
                    tijden.Add(xtmp);
                    tijd += xtmp.TijdGewerkt(rooster, exclude, SpecialeRoosters);
                }
            });
            if (tijden.Count == 0)
                return extra.Sum(x => x.ExtraUren(null, rooster));

            tijden.ForEach(x =>
            {
                if (x.Start < Vanaf)
                    x.Start = Vanaf;
                if (x.Stop > tot)
                {
                    x.InUse = false;
                    x.Stop = tot;
                }
            });
            var start = GetFirstStart(tijden);
            var stop = GetLastStop(tijden);
            foreach (var xt in extra)
            {
                if (xt.Tijd.TotalHours <= 0) continue;
                tijd += xt.ExtraUren(new TijdEntry(start, stop, rooster), rooster);
            }

            return tijd;
        }

        public int UpdateLijst(UrenLijst from, bool overwriterooster)
        {
            if (from?.Uren == null)
                return -1;
            Uren ??= new List<TijdEntry>();
            var done = 0;
            SpecialeRoosters ??= new List<Rooster>();
            lock (SpecialeRoosters)
            {
                var xadd = from.SpecialeRoosters?.Where(x => SpecialeRoosters.All(s => s.Vanaf.Date != x.Vanaf.Date))
                    .ToList();
                if (xadd is {Count: > 0}) SpecialeRoosters.AddRange(xadd);
            }

            foreach (var tijd in from.Uren)
            {
                var changed = false;
                lock (Uren)
                {
                    foreach (var t in Uren)
                    {
                        if (t.WerkRooster == null || !t.WerkRooster.IsCustom())
                            t.WerkRooster = WerkRooster;
                        if (t.Equals(tijd))
                        {
                            if (t.WerkRooster == null || !t.WerkRooster.IsCustom())
                                t.WerkRooster = tijd.WerkRooster != null && tijd.WerkRooster.IsCustom()
                                    ? tijd.WerkRooster
                                    : WerkRooster;
                            t.Omschrijving = tijd.Omschrijving;
                            t.Start = tijd.Start;
                            t.Stop = tijd.Stop;
                            changed = true;
                            done++;
                            break;
                        }

                        if (t.ContainsBereik(tijd))
                        {
                            if (t.WerkRooster == null || !t.WerkRooster.IsCustom())
                                t.WerkRooster = tijd.WerkRooster != null && tijd.WerkRooster.IsCustom()
                                    ? tijd.WerkRooster
                                    : WerkRooster;
                            t.Omschrijving = tijd.Omschrijving;
                            if (t.Start > tijd.Start)
                                t.Start = tijd.Start;
                            if (t.Stop < tijd.Stop)
                                t.Stop = tijd.Stop;
                            changed = true;
                            done++;
                            break;
                        }
                    }
                }

                if (!changed)
                {
                    var x = false;
                    Add(tijd, ref x);
                    if (x)
                        done++;
                }
            }

            UpdateUrenRooster(false, overwriterooster ? from.WerkRooster : null);
            return done;
        }

        public DateTime GetFirstStart(List<TijdEntry> uren)
        {
            if (Uren?.Count == 0)
                return DateTime.MinValue;
            var dt = new DateTime();
            foreach (var tijd in uren)
            {
                if (tijd.ExtraTijd != null) continue;
                if (tijd.InUse) return tijd.Start;
                if (tijd.Start < dt || dt.IsDefault())
                    dt = tijd.Start;
            }

            return dt;
        }

        public TijdEntry GetAvailibleEntry()
        {
            if (Uren == null || Uren.Count == 0)
                return UpdateTijdGewerkt(DateTime.Now, DateTime.Now, false);
            TijdEntry xent = null;
            for (var i = 0; i < Uren.Count; i++)
            {
                var ent = Uren[i];
                if (ent.InUse) return ent;
                if (xent == null || ent.Stop >= xent.Stop)
                    xent = ent;
            }

            if (xent == null)
                return UpdateTijdGewerkt(DateTime.Now, DateTime.Now, false);
            return xent;
        }

        public DateTime GetFirstStart()
        {
            return GetFirstStart(Uren);
        }

        public void SetStop()
        {
            SetStop(DateTime.Now);
        }

        public void SetStop(DateTime stop)
        {
            var lastkey = GetInUseEntry(true);

            lastkey.Stop = stop;
            lastkey.InUse = false;
            RemoveAllEmpty();
        }

        public void SetStart()
        {
            UpdateTijdGewerkt(DateTime.Now, DateTime.Now, true);
        }

        public void SetStart(DateTime start, DateTime stop)
        {
            UpdateTijdGewerkt(start, stop, true);
        }


        public DateTime GetLastStop(List<TijdEntry> uren)
        {
            if (Uren?.Count == 0)
                return DateTime.MinValue;
            var dt = new DateTime();
            foreach (var tijd in uren)
            {
                if (tijd.ExtraTijd != null) continue;
                if (tijd.InUse) return DateTime.Now;
                if (tijd.Stop > dt)
                    dt = tijd.Stop;
            }

            return dt;
        }

        public DateTime GetLastStop()
        {
            return GetLastStop(Uren);
        }

        public TijdEntry GetInUseEntry(bool createnew)
        {
            if (Uren == null)
                Uren = new List<TijdEntry>();
            var ent = Uren.FirstOrDefault(x => x.InUse);
            //if (ent == null)
            //    ent = Uren.FirstOrDefault(x => x.TijdGewerkt(x.WerkRooster ?? WerkRooster, null, SpecialeRoosters) == 0);
            if (ent == null && createnew)
            {
                ent = new TijdEntry(DateTime.Now, DateTime.Now, WerkRooster) {InUse = true};
                Uren.Add(ent);
            }

            return ent;
        }

        public TijdEntry GetLastStopEntry(bool allocate)
        {
            if (Uren == null || Uren?.Count == 0)
            {
                Uren = new List<TijdEntry>();
                var ent = new TijdEntry(DateTime.Now, DateTime.Now, WerkRooster);
                Uren.Add(ent);
                return ent;
            }

            TijdEntry dt = null;
            if (allocate)
            {
                dt = Uren.FirstOrDefault(x => x.TijdGewerkt(x.WerkRooster ?? WerkRooster, null, SpecialeRoosters) == 0);
                if (dt != null)
                    return dt;
            }

            foreach (var tijd in Uren)
                if (dt == null || tijd.Stop > dt.Stop)
                    dt = tijd;
            if (dt == null)
            {
                dt = new TijdEntry(DateTime.Now, DateTime.Now, WerkRooster);
                Uren.Add(dt);
            }

            return dt;
        }

        public Dictionary<DateTime, DateTime> ToDictionary()
        {
            var dc = new Dictionary<DateTime, DateTime>();
            if (Uren != null)
                foreach (var uur in Uren)
                    dc[uur.Start] = uur.Stop;
            return dc;
        }
    }
}