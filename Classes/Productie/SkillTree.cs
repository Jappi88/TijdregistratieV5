using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductieManager.Productie
{
    public class SkillTree
    {
        public List<SkillEntry> SkillEntries { get; set; }
        public DateTime Vanaf { get; set; }

        public SkillTree()
        {
            SkillEntries = new List<SkillEntry> { };
            Vanaf = DateTime.Now;
        }

        public static bool Update(Bewerking werk, Personeel pers, bool save)
        {
            try
            {
                if (werk == null || pers == null || pers.WerktAan == null || !werk.Equals(pers.WerktAan) || pers.Werkplek == null)
                    return false;
                if (pers.Skills == null)
                    pers.Skills = new SkillTree();
                SkillEntry entry = pers.Skills.SkillEntries.FirstOrDefault(x => x.Path.ToLower() == werk.Path.ToLower() && x.WerkPlek.ToLower() == pers.Werkplek.ToLower());
                WerkPlek wp = werk.WerkPlekken.FirstOrDefault(x => x.Naam.ToLower() == pers.Werkplek.ToLower());
                
                if (entry == null)
                    pers.Skills.SkillEntries.Add(new SkillEntry()
                    { ArtikelNr = werk.ArtikelNr, Gestart = pers.Gestart, Gestopt = pers.Gestopt, Path = werk.Path, Omschrijving = werk.Omschrijving, Tijdgewerkt = wp == null?pers.TijdGewerkt : pers.TijdAanGewerkt(wp.GetStoringen()).TotalHours, WerkPlek = pers.Werkplek });
                else
                {

                    // DateTime gestart = pers.Gestart > entry.Gestopt || pers.Gestart < entry.Gestart ? pers.Gestart : entry.Gestopt;
                    //DateTime gestopt = pers.Gestopt < entry.Gestart || pers.Gestopt > entry.Gestopt
                    Dictionary<DateTime, DateTime> exclude = new Dictionary<DateTime, DateTime> { };
                    if (wp != null)
                        exclude = wp.GetStoringen();
                    exclude.Add(entry.Gestart, entry.Gestopt);
                    if (pers.VrijeDagen != null && pers.VrijeDagen.Count > 0)
                    {
                        foreach (var v in pers.VrijeDagen)
                            exclude.Add(v.Key, v.Value);
                    }
                    double tijd = Werktijd.TijdGewerkt(pers.Gestart, pers.Gestopt, pers.BeginDag, pers.EindDag, exclude).TotalHours;
                    if (pers.Gestart < entry.Gestart)
                        entry.Gestart = pers.Gestart;
                    if (pers.Gestopt > entry.Gestopt)
                        entry.Gestopt = pers.Gestopt;

                    entry.Tijdgewerkt += tijd;
                    entry.Omschrijving = werk.Omschrijving;
                    entry.ArtikelNr = werk.ArtikelNr;
                    entry.Path = werk.Path;
                    entry.WerkPlek = pers.Werkplek;
                }
                if (pers.Gestart < pers.Skills.Vanaf)
                    pers.Skills.Vanaf = pers.Gestart;
                return save ? Manager.Database.UpSert(pers) : true;
            }
            catch { return false; }
        }
    }
}