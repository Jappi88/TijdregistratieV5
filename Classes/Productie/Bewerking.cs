using LiteDB;
using ProductieManager.Mailing;
using ProductieManager.Misc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ProductieManager.Productie
{
    [Serializable]
    public class Bewerking : IProductieBase
    {
        [BsonId(true)]
        public int Id { get; set; }

        public string Naam { get; set; }

        public List<WerkPlek> WerkPlekken { get; set; } = new List<WerkPlek> { };
        public int AanbevolenPersonen { get; set; }
        public override bool TeLaat => Werktijd.EerstVolgendewerkdag(DateTime.Now) > LeverDatum;
        public override bool IsNieuw => DateTime.Now.TimeOfDay < DatumToegevoegd.AddHours(4).TimeOfDay;
        public int AantalPersonen { get { return GetPersoneel().Where(x => x.Actief).Count(); } }
        public int AantalBewerkingen { get; set; }
        public int _gemaakt { get; set; }

        public int AantalGemaakt
        {
            get
            {
                return GetAantalGemaakt();
            }
            set
            {
                SetAantalGemaakt(value);
            }
        }

        public override string ProductieNr { get { return Parent == null ? base.ProductieNr : Parent.ProductieNr; } set { base.ProductieNr = value; } }
        public override string ArtikelNr { get { return Parent == null ? base.ArtikelNr : Parent.ArtikelNr; } set => base.ArtikelNr = value; }
        public override string Omschrijving { get { return (Parent == null ? base.Omschrijving : Parent.Omschrijving).WrapText(150); } set => base.Omschrijving = value; }

        public string Path { get { return ProductieNr + "\\" + Naam; } }

        [BsonRef]
        public ProductieFormulier Parent { get; set; }

        public Bewerking()
        {
            State = ProductieState.Gestopt;
        }

        public Bewerking(double doorlooptijd) : this()
        {
            DoorloopTijd = doorlooptijd;
        }

        public Personeel[] GetPersoneel()
        {
            if (WerkPlekken == null || WerkPlekken.Count == 0)
                return new Personeel[] { };
            List<Personeel> personeel = new List<Personeel> { };
            WerkPlekken.ForEach(x => personeel.AddRange(x.Personen));
            return personeel.ToArray();
        }

        public Personeel[] GetPersoneel(string werkplek)
        {
            if (WerkPlekken == null || WerkPlekken.Count == 0)
                return new Personeel[] { };
            List<Personeel> personeel = new List<Personeel> { };
            WerkPlekken.Where(x => x.Naam.ToLower() == werkplek.ToLower()).ToList().ForEach(x => personeel.AddRange(x.Personen));
            return personeel.ToArray();
        }

        public bool SetPersoneel(Personeel[] personen)
        {
            if (personen == null)
                return false;
            try
          {
                if (WerkPlekken.Count > 0)
                {
                    List<Personeel> old = new List<Personeel> { };
                    for (int i = 0; i < WerkPlekken.Count; i++)
                    {
                        if (personen.Length > 0)
                            WerkPlekken[i].Personen.RemoveAll(x => !personen.Any(t => t.Equals(x)));
                        else WerkPlekken[i].Personen.Clear();
                        if (i + 1 < WerkPlekken.Count)
                        {
                            for(int j = i + 1; j < WerkPlekken.Count; j ++)
                            {
                                if(WerkPlekken[j].Naam.ToLower() == WerkPlekken[i].Naam.ToLower())
                                {
                                    if (WerkPlekken[j].Personen.Count > 0)
                                    {
                                       WerkPlekken[i].AddPersonen(WerkPlekken[j].Personen.ToArray(), this);
                                    }
                                    WerkPlekken.RemoveAt(j--);
                                    i--;
                                }
                            }
                        }
                    }
                }

                bool xreturn = true;
                foreach (var persoon in personen)
                {
                    xreturn |= AddPersoneel(persoon, persoon.Werkplek);
                }
                WerkPlekken.ForEach(x => x.CalculatePerUur(false));
                return xreturn;
            }
            catch { return false; }
        }

        public bool AddPersoneel(Personeel persoon, string werkplek)
        {
            try
            {
                if (persoon != null && werkplek != null)
                {
                    persoon.Werkplek = werkplek;
                    WerkPlek old = WerkPlekken.FirstOrDefault(x => x.Personen.Any(x => x.PersoneelNaam.ToLower() == persoon.PersoneelNaam.ToLower()));
                    WerkPlek current = WerkPlekken.FirstOrDefault(x => x.Naam.ToLower() == werkplek.ToLower());

                    if (current == null)
                    {
                        current = new WerkPlek(new Personeel[] { persoon }, werkplek, this);
                        WerkPlekken.Add(current);
                        return true;
                    }
                    else
                    {
                        if (old != null && old.Naam.ToLower() != current.Naam.ToLower())
                            old.RemovePersoon(persoon.PersoneelNaam);
                        return current.AddPersoon(persoon, this);
                    }
                }
                return false;
            }
            catch { return false; }
        }

        public bool UpdateBewerking(ProductieFormulier[] allforms, string change, bool save)
        {
            if (allforms != null)
            {
                ProductieFormulier[] forms = allforms.Where(t => ArtikelNr.ToLower() == t.ArtikelNr.ToLower()).ToArray(); ;
                AantalBewerkingen = AlleBewerkingen(forms).Length;
                GemiddeldPerUur = GemiddeldAantalPerUur(forms);
                TotaalTijdGewerkt = TotaalGewerkteUren(forms);
            }
            if (WerkPlekken != null)
            {
                WerkPlekken.ForEach(x => x.CalculatePerUur(false));
            }
            TijdGewerkt = TijdAanGewerkt();
            ActueelPerUur = ProductenPerUur();
            PerUur = Math.Round((Aantal / DoorloopTijd), 0);
            VerwachtLeverDatum = VerwachtDatumGereed();
            Gereed = GereedPercentage();
            AanbevolenPersonen = AantalPersonenNodig();
            
            if (AantalPersonen == 0 && State == ProductieState.Gestart)
                StopProductie(true);
            LastChanged = new Classes.SqlLite.UserChange() { Change = change };

            if (Parent != null)
            {
                Aantal = Parent.Aantal;
                Omschrijving = Parent.Omschrijving;
                ArtikelNr = Parent.ArtikelNr;
                ProductieNr = Parent.ProductieNr;

                if (save)
                    Parent.BewerkingChanged(this, this);
            }

            BewerkingChanged(this, this);
            return true;
        }

        public Bewerking CreateNewInstance(ProductieFormulier parent)
        {
            Bewerking b = new Bewerking();
            b.State = State;
            b.Naam = Naam;
            b.DoorloopTijd = DoorloopTijd;
            b.TijdGewerkt = TijdGewerkt;
            b.TotaalTijdGewerkt = TotaalTijdGewerkt;
            b.TijdGestart = TijdGestart;
            b.TijdGestopt = TijdGestopt;
            b.VerwachtLeverDatum = VerwachtLeverDatum;
            b.LeverDatum = parent.LeverDatum;
            b.DatumToegevoegd = parent.DatumToegevoegd;
            b.Notitie = parent.Notitie;
            b.Omschrijving = parent.Omschrijving;
            b.Paraaf = Paraaf;
            b.Aantal = parent.Aantal;
            b.DatumVerwijderd = DatumVerwijderd;
            b.SetPersoneel(GetPersoneel().Select(t => Personeel.CreateNew(t)).ToArray());
            b.UpdateBewerking(null, $"[{Path}] Bewerkingen aangepast", false);
            return b;
        }

        public bool StartProductie(bool email)
        {
            try
            {
                if (State == ProductieState.Verwijderd)
                    throw new Exception("Productie is verwijdert en kan daarom niet gestart worden.");
                if (State == ProductieState.Gereed)
                    throw new Exception("Productie is al gereed gemeld en kan daarom niet gestart worden.");
                if (GetPersoneel().Where(x => x.Actief).Count() == 0)
                    throw new Exception("Geen personeel geselecteerd om te werken.");
                if (State != ProductieState.Gestart)
                {
                    foreach (WerkPlek plek in WerkPlekken)
                    {
                        plek.TijdGestart = DateTime.Now;
                        foreach (Personeel per in plek.Personen)
                        {
                            if (per.Actief)
                            {
                                per.WerktAan = Path;
                                per.Gestart = DateTime.Now;
                                Personeel x = Manager.Database.GetPersoneel(per.PersoneelNaam);
                                if (x != null)
                                {
                                    x.WerktAan = per.WerktAan;
                                    x.Werkplek = per.Werkplek;
                                    x.Gestart = per.Gestart;
                                    x.Gestopt = per.Gestopt;
                                    SkillTree.Update(this, x, false);
                                    Manager.Database.UpSert(x, "Productie Gestart");
                                }
                            }
                        }
                    }
                    LaatstAantalUpdate = DateTime.Now;
                    State = ProductieState.Gestart;
                    TijdGestart = DateTime.Now;
                    UpdateBewerking(null, $"[{Path}] Bewerking Gestart", true);

                    if (email)
                        RemoteProductie.RespondByEmail(this, $"Productie [{ProductieNr.ToUpper()}] {Naam} is zojuist gestart.");
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool StopProductie(bool email)
        {
            try
            {
                if (State == ProductieState.Gestopt)
                    return false;
                if (State == ProductieState.Gereed || State == ProductieState.Verwijderd)
                    return false;
                TijdGestopt = DateTime.Now;
                State = ProductieState.Gestopt;

                foreach (WerkPlek plek in WerkPlekken)
                {
                    plek.TijdGestopt = DateTime.Now;
                    plek.LastTijdGewerkt += plek.TijdAanGewerkt(false);
                    foreach (Personeel per in plek.Personen)
                    {
                        if (per.Actief)
                        {
                            per.Gestopt = DateTime.Now;
                            //per.Gestart = DateTime.Now;
                            Personeel x = Manager.Database.GetPersoneel(per.PersoneelNaam);
                            if (x != null && x.WerktAan != null && this.Equals(x.WerktAan))
                            {
                                x.Gestopt = per.Gestopt;
                                SkillTree.Update(this, x, false);
                                x.Gestart = per.Gestart;
                                x.WerktAan = null;
                                Manager.Database.UpSert(x, $"[{x.PersoneelNaam}] uit werk [{Path}] gehaald");
                            }
                        }
                    }

                }

                UpdateBewerking(null, $"[{Path}] Bewerking Gestopt", true);
                if (email)
                    RemoteProductie.RespondByEmail(this, $"Productie [{ProductieNr.ToUpper()}] {Naam} is zojuist gestopt.");
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public ProductieFormulier GetParent()
        {
            if (Parent != null)
                return Parent;
            if (ProductieNr == null)
                return null;
            var parent = Manager.Database.GetProductie(ProductieNr);
            if (parent != null && parent.Bewerkingen != null)
            {
                for (int i = 0; i < parent.Bewerkingen.Length; i++)
                    if (parent.Bewerkingen[i].Naam.ToLower() == Naam.ToLower())
                        parent.Bewerkingen[i] = this;
            }
            return parent;
        }

        public bool MeldBewerkingGereed(ProductieFormulier parent, string paraaf, int aantal, bool update)
        {
            StopProductie(false);
            AantalGemaakt = aantal;
            DatumGereed = DateTime.Now;

            if (TotaalTijdGewerkt > 0 && AantalGemaakt > 0)
                PerUur = Math.Round(AantalGemaakt / DoorloopTijd, 0);

            if (double.IsInfinity(PerUur) || double.IsNaN(PerUur))
                PerUur = 100;
            Paraaf = paraaf;
            State = ProductieState.Gereed;
            int count = 0;
            if (parent.Bewerkingen != null)
                count = parent.Bewerkingen.Where(t => t.State != ProductieState.Gereed).Count();
            RemoteProductie.RespondByEmail(this, $"Productie [{ProductieNr.ToUpper()}] {Naam} is zojuist gereed gemeld door {Paraaf}.");
            if (count == 0)
                parent.MeldGereed(aantal, paraaf);
            else
            {
                UpdateBewerking(null, $"[{Path}] Gereed Gemeld", true);
            }
            return true;
        }

        public DateTime VerwachtDatumGereed()
        {
            double tijd = TijdNodig();
            tijd /= GetPersoneel().AantalPersTijdMultiplier();

            return Werktijd.DatumNaTijd(DateTime.Now, TimeSpan.FromHours(tijd));
        }

        public Bewerking[] AlleBewerkingen(ProductieFormulier[] forms)
        {
            List<Bewerking> bws = new List<Bewerking> { };
            if (forms == null)
                return bws.ToArray();
            foreach (ProductieFormulier v in forms)
            {
                if (v.Bewerkingen != null)
                    bws.AddRange(v.Bewerkingen.Where(s => s.Naam.ToLower() == Naam.ToLower()).ToArray());
            }
            return bws.ToArray();
        }

        public double TotaalGewerkteUren(ProductieFormulier[] forms)
        {
            if (forms == null)
                return TotaalTijdGewerkt;
            var x = forms;
            if (x == null)
                return TotaalTijdGewerkt;
            double gewerkt = Math.Round((x.Sum(t => t.Bewerkingen.Sum(s => s.Naam.ToLower() == Naam.ToLower() ? s.TijdGewerkt : 0))), 2);
            if (double.IsNaN(gewerkt) || double.IsInfinity(gewerkt))
                gewerkt = 0;
            return gewerkt;
        }

        public double GemiddeldAantalPerUur(ProductieFormulier[] forms)
        {
            if (forms == null)
                return 0;
            var x = forms;
            if (x == null)
                return ProductenPerUur();
            Bewerking[][] bws = x.Select(t => t.Bewerkingen.Where(b => b.Naam.ToLower() == Naam.ToLower()).ToArray()).ToArray();
            int aantalbws = bws.Sum(t => t.Length);
            double peruur = bws.Sum(t => t.Sum(b => b.ProductenPerUur())) / aantalbws;
            return Math.Round(peruur, 0);
        }

        public double GereedPercentage()
        {
            if (Aantal > 0)
            {
                double val = Math.Round(((AantalGemaakt / (double)Aantal) * 100), 1);
                if (val > 100)
                    val = 100;
                return val;
            }
            return 0;
        }

        public double ProductenPerUur()
        {
            double tijd = TijdAanGewerkt();
            double peruur = 100;
            if (tijd > 0 && AantalGemaakt > 0)
                peruur = Math.Round((AantalGemaakt / tijd), 0);
            else if (DoorloopTijd > 0 && Aantal > 0)
                peruur = Math.Round((Aantal / DoorloopTijd), 0);
            if (peruur == 0)
                peruur = 50;
            return peruur;
        }

        public double TijdAanGewerkt()
        {
            double tijd = 0;
            if (WerkPlekken != null && WerkPlekken.Count > 0)
            {
                foreach (var wp in WerkPlekken)
                    tijd += wp.TijdAanGewerkt(true);
            }

            if (tijd < 0 || double.IsInfinity(tijd))
            { tijd = 0; }

            return Math.Round(tijd, 2);
        }

        public double TijdNodig()
        {
            try
            {
                if (AantalGemaakt >= Aantal)
                    return 0;
                double tijd = ((Aantal - AantalGemaakt) / ProductenPerUur());
                return Math.Round(tijd, 2);
            }
            catch { return 0; }
        }

        public int AantalPersonenNodig()
        {
            double nodig = TijdNodig();
            if (nodig == 0)
                return 0;
            double tijdover = Werktijd.TijdGewerkt(DateTime.Now, LeverDatum).TotalHours;
            if (tijdover == 0)
                return 0;
            if (nodig <= tijdover)
                return 1;
            int value = (int)Math.Ceiling(nodig / tijdover);
            return value;
        }

        public event BewerkingChangedHandler OnBewerkingChanged;

        public void BewerkingChanged(object sender, Bewerking bewerking)
        {
            OnBewerkingChanged?.Invoke(sender, bewerking);
        }

        public void BewerkingChanged(string change = null)
        {
            if (change == null)
                change = $"[{Path}] Bewerking Gewijzigd";
            UpdateBewerking(null, change, true);
        }

        public int GetAantalGemaakt()
        {
            int aantal = 0;
            bool done = false;
            if (WerkPlekken != null && WerkPlekken.Count > 0)
            {
                foreach (WerkPlek plek in WerkPlekken)
                    if (plek.Werk.Equals(this))
                    {
                        aantal += plek.AantalGemaakt;
                        done = true;
                    }
            }
            return done ? aantal : _gemaakt;
        }

        public void UpdateAantal()
        {
            if (WerkPlekken != null)
            {
                WerkPlek[] plekken = WerkPlekken.Where(x => x.Werk != null && x.Werk.Equals(this)).ToArray();
                if (plekken.Length > 0)
                {
                    _gemaakt = plekken.Sum(x => x.AantalGemaakt);
                }
            }
        }

        public void SetAantalGemaakt(int aantal)
        {
            _gemaakt = aantal;
            if (WerkPlekken != null)
            {
                WerkPlek[] plekken = WerkPlekken.Where(x => x.Werk.Equals(this)).ToArray();
                if (plekken.Length > 0)
                {
                    if (plekken.Sum(x => x.AantalGemaakt) != _gemaakt)
                    {
                        int split = _gemaakt / plekken.Length;
                        int rest = _gemaakt % plekken.Length;
                        foreach (WerkPlek plek in plekken)
                            plek.AantalGemaakt = split;
                        plekken[plekken.Length - 1].AantalGemaakt += rest;
                    }
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is string)
                return Path.ToLower() == (obj as string).ToLower();
            Bewerking bew = obj as Bewerking;
            if (bew == null)
                return false;

            return bew.Path.ToLower() == Path.ToLower();
        }

        public override int GetHashCode()
        {
            return Path.GetHashCode();
        }
    }
}