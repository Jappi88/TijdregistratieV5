using ProductieManager.Productie;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;

namespace ProductieManager.Classes.Productie
{
    public class TaakBeheer
    {
        private BackgroundWorker _Autobeheer = new BackgroundWorker();

        public bool IsRunning { get; set; }

        private Manager PManager { get; set; }
        public List<Taak> Taken { get; set; }
        public DateTime InitTime { get; set; }

        public TaakBeheer(Manager manager)
        {
            PManager = manager;
            Taken = new List<Taak> { };
            InitTime = DateTime.Now;
            _Autobeheer.DoWork += _Autobeheer_DoWork;
            _Autobeheer.WorkerReportsProgress = true;
            _Autobeheer.ProgressChanged += _Autobeheer_ProgressChanged;
        }

        private void _Autobeheer_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null)
            {
                if (Taken != null && Taken.Count > 0)
                    Taken.Clear();
                Taken = e.UserState as List<Taak>;
                if (Taken == null)
                    Taken = new List<Taak> { };
                TakenUpdated(Taken.ToArray());
            }
        }

        private void _Autobeheer_DoWork(object sender, DoWorkEventArgs e)
        {
            while (IsRunning && Manager.Opties != null && Manager.Opties.GebruikTaken)
            {
                var taken = UpdateTaken();
                if (taken != null)
                    _Autobeheer.ReportProgress(0, taken);
                Thread.Sleep(Manager.Opties.TaakSyncInterval);
            }
            StopBeheer();
        }

        public List<Taak> UpdateTaken()
        {
            return xUpdateTaken();
            // inv.Invoke();
        }

        private List<Taak> xUpdateTaken()
        {
            try
            {
                if (Manager.Opties == null || !Manager.Opties.GebruikTaken)
                {
                    StopBeheer();
                }
                else
                {
                    List<Taak> xtaken = new List<Taak> { };
                    bool xresult = false;

                    xresult = false;
                    List<Taak> taken = new List<Taak> { };
                    foreach (ProductieFormulier form in Manager.GetProducties(new ViewState[] { ViewState.Gestart, ViewState.Gestopt }, true))
                    {
                        form.UpdateForm(true, false, "", false);
                        if (Manager.LogedInGebruiker != null)
                        {
                            Taak[] formtaken = GetProductieTaken(form);
                            Taak[] old = Taken.Where(t => t.Formulier != null && t.Formulier.Equals(form)).ToArray();
                            if (formtaken.Length != old.Length)
                                xresult = true;

                            if (formtaken.Length > 0)
                            {
                                if (!xresult)
                                {
                                    foreach (Taak t in formtaken)
                                    {
                                        t.Update();
                                        xresult |= !old.Any(x => x.Type == t.Type);
                                    }
                                }
                                taken.AddRange(formtaken);
                            }
                        }
                    }
                    xresult |= Taken.Count != taken.Count;
                    if (xresult)
                    {
                        return taken;
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public void UpdateProductieTaken(ProductieFormulier form)
        {
            if (form != null)
            {
                Taak[] formtaken = GetProductieTaken(form);
                Taak[] old = Taken.Where(t => t.Formulier != null && t.Formulier.Equals(form)).ToArray();
                bool xresult = false;
                if (formtaken.Length != old.Length)
                    xresult = true;
                else if (formtaken.Length > 0)
                {
                    foreach (Taak t in formtaken)
                    {
                        xresult |= !old.Any(x => x.Type == t.Type);
                    }
                }
                if (xresult)
                {
                    if (old.Length > 0)
                        foreach (Taak t in old)
                            Taken.Remove(t);

                    Taken.AddRange(formtaken);
                    TakenUpdated(Taken.ToArray());
                }
            }
        }

        public void StartBeheer()
        {
            if (!IsRunning && _Autobeheer != null && !_Autobeheer.IsBusy && Manager.Opties != null && Manager.Opties.GebruikTaken)
            {
                IsRunning = true;
                _Autobeheer.RunWorkerAsync();
            }
            if (Manager.Opties == null || !Manager.Opties.GebruikTaken)
                StopBeheer();
        }

        public void StopBeheer()
        {
            IsRunning = false;
            if (Taken != null && Taken.Count > 0)
            {
                Taken.Clear();
                TakenUpdated(Taken.ToArray());
            }
        }

        public Taak[] GetProductieTaken(ProductieFormulier form)
        {
            try
            {
                List<Taak> xreturn = new List<Taak> { };
                if (form == null)
                    return xreturn.ToArray();
                if (Manager.Opties == null || !Manager.Opties.GebruikTaken)
                    return xreturn.ToArray();

                //een taak geven als personeel vrij is/word
                if (Manager.Opties.TaakPersoneelVrij && form.Bewerkingen != null)
                {
                    foreach (Bewerking b in form.Bewerkingen)
                    {
                        if (b.State == ProductieState.Gestart)
                        {
                            foreach (Personeel pers in b.GetPersoneel())
                            {
                                if (pers.WerktAan == null || !pers.Actief)
                                    continue;
                                //tijd dat we kijken of iemand vrij is (8uur)
                                double tijd = pers.IsVrijOver();
                                if (tijd > -1 && tijd <= 8)
                                {
                                    Taak t = new TakenLijst(this, pers, b).PersoneelVrij(TaakUrgentie.ZodraMogelijk);
                                    if (t != null)
                                    {
                                        t.OnUpdate += TaakUpdated;
                                        t.OnVereistAandacht += TaakVereistAandacht;
                                        xreturn.Add(t);
                                    }
                                }
                            }
                        }
                    }
                }

                //Taak aanmaken als een productie te laat is.
                if (Manager.Opties.TaakAlsTelaat && form.TeLaat && (form.State != ProductieState.Gereed && form.State != ProductieState.Verwijderd))
                {
                    Taak t = new TakenLijst(this, form).Telaat(TaakUrgentie.ZSM);
                    t.OnUpdate += TaakUpdated;
                    t.OnVereistAandacht += TaakVereistAandacht;
                    xreturn.Add(t);
                }

                //taak maken als er een productie gestart moet worden.
                if (Manager.Opties.TaakVoorStart && !form.TeLaat)
                {
                    //ff kijken of de taak wel gestart moet worden, en of het tijd is om de taak wel te geven
                    TimeSpan tijdvoorstart = TimeSpan.FromMinutes(Manager.Opties.MinVoorStart);
                    TimeSpan tijdnodig = form.TijdNodig();
                    tijdvoorstart = Werktijd.AantalWerkdagen(tijdvoorstart.Add(tijdnodig));

                    foreach (Bewerking b in form.Bewerkingen)
                    {
                        if (b.State != ProductieState.Gestart)
                        {
                            DateTime uiterlijk = Werktijd.EerstVolgendewerkdag(b.LeverDatum.Subtract(tijdvoorstart));
                            TaakUrgentie urgentie = Taak.GetUrgentie(uiterlijk.AddHours(1));
                            if (Werktijd.EerstVolgendewerkdag(DateTime.Now) >= uiterlijk)
                            {
                                Taak t = new TakenLijst(this, b).Starten(urgentie);
                                t.OnUpdate += TaakUpdated;
                                t.OnVereistAandacht += TaakVereistAandacht;
                                xreturn.Add(t);
                            }
                        }
                    }
                }

                //taak geven als de productie klaar gezet moet worden
                if (Manager.Opties.TaakVoorKlaarZet && !form.TeLaat && form.State == ProductieState.Gestopt && form.Materialen.Any(x => !x.IsKlaarGezet))
                {
                    //ff kijken of de taak wel gestart moet worden, en of het tijd is om de taak wel te geven
                    TimeSpan tijdvoorstart = TimeSpan.FromMinutes(Manager.Opties.MinVoorStart);
                    TimeSpan tijdvoorklaarzet = TimeSpan.FromMinutes(Manager.Opties.MinVoorKlaarZet);
                    TimeSpan tijdnodig = form.TijdNodig();
                    tijdvoorstart = Werktijd.AantalWerkdagen(tijdnodig.Add(tijdvoorstart).Add(tijdvoorklaarzet));
                    DateTime uiterlijk = Werktijd.EerstVolgendewerkdag(form.LeverDatum.Subtract(tijdvoorstart));
                    if (Werktijd.EerstVolgendewerkdag(DateTime.Now) >= uiterlijk)
                    {
                        TaakUrgentie urgentie = Taak.GetUrgentie(uiterlijk.AddHours(1));

                        Taak t = new TakenLijst(this, form).KlaarZetten(urgentie);
                        t.OnUpdate += TaakUpdated;
                        t.OnVereistAandacht += TaakVereistAandacht;
                        xreturn.Add(t);
                    }
                }

                //taak geven als de productie gereed gemeld moet worden
                if (Manager.Opties.TaakAlsGereed && form.Bewerkingen != null && form.State != ProductieState.Verwijderd && form.State != ProductieState.Gereed)
                {
                    if (!form.Bewerkingen.Any(x => x.State != ProductieState.Gereed))
                    {
                        Taak t = new TakenLijst(this, form).GereedMelden(TaakUrgentie.ZSM);
                        t.OnUpdate += TaakUpdated;
                        t.OnVereistAandacht += TaakVereistAandacht;
                        xreturn.Add(t);
                    }
                    else
                    {
                        foreach (Bewerking b in form.Bewerkingen)
                        {
                            if (b.State != ProductieState.Gereed && b.AantalGemaakt >= b.Aantal)
                            {
                                Taak t = new TakenLijst(this, b).BewerkingGereedMelden(TaakUrgentie.ZSM);
                                t.OnUpdate += TaakUpdated;
                                t.OnVereistAandacht += TaakVereistAandacht;
                                xreturn.Add(t);
                            }
                        }
                    }
                }

                //Taak geven voor als het aantal personeel aangepast moet worden per bewerking.
                if (Manager.Opties.TaakVoorPersoneel)
                {
                    foreach (Bewerking b in form.Bewerkingen)
                    {
                        int pers = b.AantalPersonenNodig();
                        if (b.AantalPersonen != pers && pers > 0 && b.State == ProductieState.Gestart)
                        {
                            Taak t = new TakenLijst(this, b).PersoneelChange(TaakUrgentie.ZSM);
                            t.OnUpdate += TaakUpdated;
                            t.OnVereistAandacht += TaakVereistAandacht;
                            xreturn.Add(t);
                        }
                    }
                }

                //Taak geven als de productie gecontroleerd moet worden
                if (Manager.Opties.TaakVoorControle)
                {
                    foreach (Bewerking b in form.Bewerkingen)
                    {
                        if (b.WerkPlekken != null && b.WerkPlekken.Count > 0)
                        {
                            foreach (WerkPlek plek in b.WerkPlekken)
                            {
                                if (plek.LaatstAantalUpdate < DateTime.Now.Subtract(TimeSpan.FromMinutes(Manager.Opties.MinVoorControle)) && b.State == ProductieState.Gestart)
                                {
                                    Taak t = new TakenLijst(this, b).Controleren(TaakUrgentie.ZSM, plek.Naam);
                                    t.OnUpdate += TaakUpdated;
                                    t.OnVereistAandacht += TaakVereistAandacht;
                                    xreturn.Add(t);
                                }
                            }
                        }
                        else if (b.LaatstAantalUpdate < DateTime.Now.Subtract(TimeSpan.FromMinutes(Manager.Opties.MinVoorControle)) && b.State == ProductieState.Gestart)
                        {
                            Taak t = new TakenLijst(this, b).Controleren(TaakUrgentie.ZSM, "Bewerking");
                            t.OnUpdate += TaakUpdated;
                            t.OnVereistAandacht += TaakVereistAandacht;
                            xreturn.Add(t);
                        }
                    }
                }
                return xreturn.ToArray();
            }
            catch { return new Taak[] { }; }
        }

        public event TakenHandler OnTakenUpdated;

        public event TaakHandler OnTaakUpdated;

        public event TaakHandler OnTaakVereistAandacht;

        public void TakenUpdated(Taak[] taken)
        {
            OnTakenUpdated?.Invoke(this, taken);
        }

        public void TakenUpdated()
        {
            OnTakenUpdated?.Invoke(this, Taken.ToArray());
        }

        public void TaakUpdated(TaakBeheer beheer, Taak taak)
        {
            OnTaakUpdated?.Invoke(beheer, taak);
        }

        public void TaakUpdated(Taak taak)
        {
            OnTaakUpdated?.Invoke(this, taak);
        }

        public void TaakVereistAandacht(TaakBeheer manager, Taak taak)
        {
            OnTaakVereistAandacht?.Invoke(manager, taak);
        }

        public void TaakVereistAandacht(Taak taak)
        {
            OnTaakVereistAandacht?.Invoke(this, taak);
        }
    }
}