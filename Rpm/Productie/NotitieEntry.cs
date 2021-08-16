using System;
using System.Collections.Generic;
using System.Linq;
using Rpm.Misc;
using Rpm.Various;

namespace Rpm.Productie
{
    public class NotitieEntry
    {
        public IProductieBase Productie { get; set; }
        public WerkPlek  Werkplek { get; set; }
        public string Naam { get; set; }
        public string Notitie { get; set; }
        public NotitieType Type { get; set; }

        public DateTime DatumToegevoegd { get; set; }

        public int ID { get; private set; }

        public string Path => Werkplek?.Path ?? Productie?.Path;

        public NotitieEntry()
        {
            DatumToegevoegd = DateTime.Now;
            Naam = "";
            Notitie = "";
            Type = NotitieType.Algemeen;
            ID = base.GetHashCode();
        }

        public NotitieEntry(string notitie, NotitieType type) : this()
        {
            Notitie = notitie;
            Type = type;
            ID = base.GetHashCode();
        }

        public NotitieEntry(string notitie, WerkPlek plek) : this(notitie, NotitieType.Werkplek)
        {
            Werkplek = plek;
            ID = base.GetHashCode();
        }

        public NotitieEntry(string notitie, Bewerking bew) : this(notitie, NotitieType.Bewerking)
        {
            Productie = bew;
            ID = base.GetHashCode();
        }

        public NotitieEntry(string notitie, IProductieBase productie) : this()
        {
            Productie = productie;
            Notitie = notitie;
            if (productie is Bewerking)
                Type = NotitieType.Bewerking;
            else Type = NotitieType.Productie;
            ID = base.GetHashCode();
        }

        public NotitieEntry(string notitie, ProductieFormulier productie) : this(notitie, NotitieType.Productie)
        {
            Productie = productie;
            ID = base.GetHashCode();
        }

        public async void UpdateEntry(NotitieEntry entry, bool save)
        {
            try
            {
                if (entry != null)
                {
                    entry.Type = Type;
                    Naam = entry.Naam;
                    Werkplek = entry.Werkplek;
                    Productie = entry.Productie;
                    Notitie = entry.Notitie;
                    DatumToegevoegd = DateTime.Now;
                }

                switch (Type)
                {
                    case NotitieType.Algemeen:
                        if (Manager.Opties == null) return;
                        
                        Manager.Opties.Notities ??= new List<NotitieEntry>();
                        Manager.Opties.Notities.Remove(this);
                        if (entry != null)
                        {
                            Manager.Opties.Notities.Add(entry);
                        }
                        if (save)
                            await Manager.Opties.Save($"{Manager.Opties.Username} Algemene notities gewijzigd!", false,true);
                        break;
                    case NotitieType.BewerkingGereed:
                    case NotitieType.ProductieGereed:
                        if (Productie != null)
                        {
                            Productie.GereedNote = entry?.CreateCopy();
                           await  Productie.Update($"Gereed notitie aangepast voor {Path}", save);
                        }
                        break;
                    case NotitieType.DeelsGereed:
                        if (Productie is Bewerking werk)
                        {
                            var deel = werk.DeelGereedMeldingen?.FirstOrDefault(x => x.Note.Equals(this));
                            if (deel == null) return;
                            deel.Note = entry?.CreateCopy();
                            await Productie.Update($"Deels Gereed notitie aangepast voor {Path}", save);
                        }
                        break;
                    case NotitieType.Werkplek:
                        if (Werkplek != null)
                        {
                            Werkplek.Note = entry?.CreateCopy();
                            if (save && Werkplek.Werk != null)
                                await Werkplek.Werk.UpdateBewerking(null, $"Notitie aangepast voor {Path}", true);

                        }
                        break;
                    case NotitieType.Bewerking:
                    case NotitieType.Productie:
                        if (Productie != null)
                        {
                            Productie.Note = entry?.CreateCopy();
                            await Productie.Update($"Notitie aangepast voor {Path}", save);
                        }
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is NotitieEntry ent)
                return ent.ID == ID;
            return false;
        }

        public override int GetHashCode()
        {
            return ID;
        }
    }
}
