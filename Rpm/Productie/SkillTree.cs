using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace Rpm.Productie
{
    [DataContract]
    public class SkillTree
    {
        public SkillTree()
        {
            SkillEntries = new List<SkillEntry>();
            Vanaf = DateTime.Now;
        }

        public List<SkillEntry> SkillEntries { get; set; }
        public DateTime Vanaf { get; set; }

        public bool IsNewerThen(SkillTree tree)
        {
            if (tree == null || tree.SkillEntries.Count < SkillEntries.Count)
                return false;
            if (SkillEntries == null || SkillEntries.Count < tree.SkillEntries.Count)
                return true;
            var xreturn = SkillEntries.Any(x => tree.SkillEntries.Any(s => x.BijGewerkt > s.BijGewerkt));
            return xreturn;
        }

        public bool Update(Klus klus, Dictionary<DateTime, DateTime> exclude)
        {
            if (klus == null)
                return false;
            var entry = new SkillEntry
            {
                ArtikelNr = klus.ArtikelNr,
                BijGewerkt = DateTime.Now,
                Gestart = klus.GestartOp(),
                Gestopt = klus.GestoptOp(),
                Omschrijving = klus.Omschrijving,
                Path = klus.Path,
                TijdGewerkt = klus.TijdGewerkt(exclude, klus.Tijden?.WerkRooster).TotalHours,
                WerkPlek = klus.WerkPlek
            };
            if (entry.Gestart < Vanaf)
                Vanaf = entry.Gestart;

            var xent = SkillEntries.FirstOrDefault(x => x.Equals(entry));
            if (xent != null)
            {
                if (entry.Gestart < xent.Gestart)
                    xent.Gestart = entry.Gestart;
                if (entry.Gestopt > xent.Gestopt)
                    xent.Gestopt = entry.Gestopt;
                xent.TijdGewerkt += entry.TijdGewerkt;
                xent.BijGewerkt = DateTime.Now;
                if (xent.Gestart < Vanaf)
                    Vanaf = entry.Gestart;
            }
            else
            {
                SkillEntries.Add(entry);
            }

            return true;
        }


        //public static bool Update(Bewerking werk, Personeel pers, bool save)
        //{
        //    try
        //    {
        //        if (werk == null || pers == null || pers.WerktAan == null || !werk.Equals(pers.WerktAan) ||
        //            pers.Werkplek == null)
        //            return false;
        //        if (pers.Skills == null)
        //            pers.Skills = new SkillTree();
        //        var changed = false;
        //        var gestopt = werk.State == ProductieState.Gestart ? DateTime.Now : pers.Gestopt;
        //        var entry = pers.Skills.SkillEntries.FirstOrDefault(x =>
        //            x.ArtikelNr.ToLower() == werk.ArtikelNr.ToLower() &&
        //            x.WerkPlek.ToLower() == pers.Werkplek.ToLower());
        //        var wp = werk.WerkPlekken.FirstOrDefault(x => x.Naam.ToLower() == pers.Werkplek.ToLower());
        //        if (wp == null)
        //            return false;
        //        var exclude = wp.GetStoringen();

        //        if (entry == null)
        //        {
        //            pers.Skills.SkillEntries.Add(new SkillEntry
        //            {
        //                ArtikelNr = werk.ArtikelNr, Gestart = pers.Gestart, Gestopt = gestopt, Path = werk.Path,
        //                Omschrijving = werk.Omschrijving, TijdGewerkt = pers.TijdAanGewerkt(exclude,wp.Path).TotalHours,
        //                WerkPlek = pers.Werkplek
        //            });
        //            changed = true;
        //        }
        //        else
        //        {
        //            // DateTime gestart = pers.Gestart > entry.Gestopt || pers.Gestart < entry.Gestart ? pers.Gestart : entry.Gestopt;
        //            //DateTime gestopt = pers.Gestopt < entry.Gestart || pers.Gestopt > entry.Gestopt
        //            exclude.Add(entry.Gestart, entry.Gestopt);
        //            if (pers.VrijeDagen != null && pers.VrijeDagen.Count > 0)
        //                foreach (var v in pers.VrijeDagen)
        //                    exclude.Add(v.Key, v.Value);
        //            var tijd = Werktijd.TijdGewerkt(pers.Gestart, gestopt, pers.BeginDag, pers.EindDag, exclude)
        //                .TotalHours;
        //            if (pers.Gestart < entry.Gestart)
        //                entry.Gestart = pers.Gestart;
        //            if (gestopt > entry.Gestopt)
        //                entry.Gestopt = gestopt;

        //            entry.TijdGewerkt += tijd;

        //            if (tijd > 0)
        //            {
        //                changed = entry.BijGewerkt.AddMinutes(5) < DateTime.Now;
        //                entry.BijGewerkt = DateTime.Now;
        //            }

        //            entry.Omschrijving = werk.Omschrijving;
        //            entry.ArtikelNr = werk.ArtikelNr;
        //            entry.Path = werk.Path;
        //            entry.WerkPlek = pers.Werkplek;
        //        }

        //        if (pers.Gestart < pers.Skills.Vanaf)
        //            pers.Skills.Vanaf = pers.Gestart;
        //        if (save)
        //            changed |= Manager.Database.UpSert(pers);
        //        return changed;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
    }
}