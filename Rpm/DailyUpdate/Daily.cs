using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Rpm.DailyUpdate
{
    public class Daily
    {
        public List<string> CreatedDailies { get; private set; } = new List<string>();
        public ImageList ImageList = new ImageList() { ColorDepth = ColorDepth.Depth32Bit};
        public Task CreateDaily(bool forceshow = false)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    lock (ImageList)
                    {
                        ImageList.Images.Clear();
                    }
                    lock (CreatedDailies)
                    {
                        CreatedDailies.Clear();
                    }
                    if (Manager.Opties == null || Manager.LogedInGebruiker == null ||
                        Manager.Database?.ProductieFormulieren == null)
                    {
                        return;
                    }

                    if (!forceshow && !Manager.Opties.ShowDaylyMessage)
                    {
                        return;
                    }
                    var prods = Manager.Database.GetBewerkingen(ViewState.Gestopt, false, null, null).Result;
                    if (prods is {Count: > 0})
                        prods = prods.Where(x => x.IsAllowed() && x.State != ProductieState.Gereed && x.State != ProductieState.Verwijderd).ToList();
                    var telaat = prods.Where(x => x.TeLaat).ToList();
                    var telangzaam = prods
                        .Where(x => x.State == ProductieState.Gestart && x.StartedByMe() && x.ProcentAfwijkingPerUur < -10).ToList();
                    var weinigcontrol = prods.Where(x =>
                        x.State == ProductieState.Gestart && x.StartedByMe() && x.WerkPlekken.Any(w => w.ControleRatio() < -15)).ToList();
                    var komentelaat = prods.Where(x => x.VerwachtLeverDatum > x.LeverDatum).ToList();

                    string xret;
                    if (telaat.Count == 0 && telangzaam.Count == 0 && weinigcontrol.Count == 0 &&
                        komentelaat.Count == 0)
                    {
                        xret = $"<h2 color='DarkGreen'>Er zijn geen bijzonderheden, omdat u alles goed bijhoudt.<br>" +
                                $"Ga zo door!</h2>";
                        lock (ImageList)
                        {
                            ImageList.Images.Add("uitstekend", GetImageFromGrade(0));
                        }
                        lock (CreatedDailies)
                        {
                            CreatedDailies.Add(IProductieBase.CreateHtmlMessage(xret, "U doet het uistekend!!", Color.DarkGreen, Color.White, Color.White, "uitstekend", new Size(84, 84)));
                        }
                    }
                    else
                    {
                        if (telaat.Count > 0)
                        {
                            var x1 = telaat.Count == 1 ? "productie" : "producties";
                            var x2 = telaat.Count == 1 ? "is" : "zijn";
                            var xtitle = $"Er {x2} {telaat.Count} {x1} te laat, kan je erna kijken?";
                            xret = $"{GetTelaatProductiesRefsString(telaat, GetColorFromGrade(telaat.Count))}";
                            lock (ImageList)
                            {
                                ImageList.Images.Add("telaat", GetImageFromGrade(telaat.Count));
                            }
                            lock (CreatedDailies)
                            {
                                CreatedDailies.Add(IProductieBase.CreateHtmlMessage(xret, xtitle, Color.DarkGreen, Color.White, Color.White, "telaat", new Size(84,84)));
                            }

                        }

                        if (komentelaat.Count > 0)
                        {
                            var x1 = komentelaat.Count == 1 ? "productie" : "producties";
                            var x2 = komentelaat.Count == 1 ? "is" : "zijn";
                            var x3 = komentelaat.Count == 1 ? "zal" : "zullen";
                            var xtitle = $"Er {x2} {komentelaat.Count} {x1} die telaat {x3} zijn, mischien tijd om te schakelen?";
                            xret = $"{GetKomenTelaatProductiesRefsString(komentelaat, GetColorFromGrade(komentelaat.Count))}";
                            lock (ImageList)
                            {
                                ImageList.Images.Add("komentelaat", GetImageFromGrade(komentelaat.Count));
                            }
                            lock (CreatedDailies)
                            {
                                CreatedDailies.Add(IProductieBase.CreateHtmlMessage(xret, xtitle, Color.DarkGreen, Color.White, Color.White, "komentelaat", new Size(84, 84)));
                            }
                        }

                        if (telangzaam.Count > 0)
                        {
                            var x1 = telangzaam.Count == 1 ? "productie" : "producties";
                            var x2 = telangzaam.Count == 1 ? "is" : "zijn";
                            var x3 = telangzaam.Count == 1 ? "presteert" : "presteren";
                            var xtitle = $"Er {x2} {telangzaam.Count} {x1} die lager dan -10% {x3}, ruimte voor verbetering?";
                            xret = $"{GetVerbeteringProductiesRefsString(telangzaam, Color.Black)}";
                            lock (ImageList)
                            {
                                ImageList.Images.Add("telangzaam", GetImageFromGrade(telangzaam.Count));
                            }
                            lock (CreatedDailies)
                            {
                                CreatedDailies.Add(IProductieBase.CreateHtmlMessage(xret, xtitle, Color.DarkGreen, Color.White, Color.White, "telangzaam", new Size(84, 84)));
                            }
                        }

                        if (weinigcontrol.Count > 0)
                        {
                            var x1 = weinigcontrol.Count == 1 ? "productie" : "producties";
                            var x2 = weinigcontrol.Count == 1 ? "is" : "zijn";
                            var x3 = weinigcontrol.Count == 1 ? "heeft" : "hebben";
                            var xtitle = $"Er {x2} {weinigcontrol.Count} {x1} die iets meer controle nodig {x3}...";
                            xret = $"{GetControleProductiesRefsString(weinigcontrol, Color.Black)}";
                            lock (ImageList)
                            {
                                ImageList.Images.Add("weinigcontrol", GetImageFromGrade(weinigcontrol.Count));
                            }
                            lock (CreatedDailies)
                            {
                                CreatedDailies.Add(IProductieBase.CreateHtmlMessage(xret, xtitle, Color.DarkGreen, Color.White, Color.White, "weinigcontrol", new Size(84, 84)));
                            }
                        }
                    }
                    RaiseEvent();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        private Image GetImageFromGrade(int grade)
        {
            Bitmap cards = Resources.Emotes_Grades;
            int index = 4;
            if (grade < 2)
                index = 4;
            else if (grade < 4)
                index = 3;
            else if (grade < 6)
                index = 2;
            else if (grade < 8)
                index = 1;
            else index = 0;
            var xpos = (4 + (index * 130) + (10 * index));
            Rectangle srcRect = new Rectangle(xpos, 2, 130, 130);
            Bitmap card = (Bitmap)cards.Clone(srcRect, cards.PixelFormat);
            return card;
        }

        private Color GetColorFromGrade(int grade)
        {
            Color xret;
            if (grade < 2)
                xret = Color.DarkGreen;
            else if (grade < 4)
                xret = Color.Green;
            else if (grade < 6)
                xret = Color.Orange;
            else if (grade < 8)
                xret = Color.Red;
            else xret = Color.Maroon;
            return xret;
        }

        private string GetTelaatProductiesRefsString(List<Bewerking> prods, Color color)
        {
            return
                $"<ul>" +
                $"{string.Join("\r\n", prods.Select(x => $"<li><a color= '{color.Name}' href='{x.Path}'><b><u>{x.Path} ({x.LeverDatum:g})</u></b></a></li>"))}" +
                $"</ul>";

        }

        private string GetKomenTelaatProductiesRefsString(List<Bewerking> prods, Color color)
        {
            return
                $"<ul>" +
                $"{string.Join("\r\n", prods.Select(x => $"<li><a color='{color.Name}' href='{x.Path}'><b><u>{x.Path} zal klaar zijn op '{x.VerwachtLeverDatum:g}' i.p.v '{x.LeverDatum:g}'</u></b></a></li>"))}" +
                $"</ul>";

        }

        private string GetVerbeteringProductiesRefsString(List<Bewerking> prods, Color color)
        {
            return
                $"<ul>" +
                $"{string.Join("\r\n", prods.Select(x => GetProductieVerbeteringHtml(x,color)))}" +
                "</ul>";

        }

        private string GetControleProductiesRefsString(List<Bewerking> prods, Color color)
        {
            return
                $"<ul>" +
                $"{string.Join("\r\n", prods.Select(x => GetProductieControleHtml(x, color)))}" +
                "</ul>";

        }

        private string GetProductieVerbeteringHtml(Bewerking productie, Color txtcolor)
        {
            var x = productie;
            return $"<li><a color='{txtcolor.Name}' href='{x.Path}'>" +
                   $"<b><u>{x.Path} Draait op {x.ActueelPerUur} i.p.v. {x.PerUur} P/u <span style = 'color: {IProductieBase.GetNegativeColorByPercentage(x.ProcentAfwijkingPerUur).Name}'>({x.ProcentAfwijkingPerUur}%)</span></u></b>" +
                   $"</a></li>";
        }

        private string GetProductieControleHtml(Bewerking productie, Color txtcolor)
        {
            var x = productie;
            var x1 = x.WerkPlekken.Count == 1 ? "werkplek" : "werkplaatsen";
            var x2 = x.WerkPlekken.Count == 1 ? "heeft" : "hebben";
            return $"<li><a color='{txtcolor.Name}' href='{x.Path}'>" +
                   $"<b><u>{x.Path} Heeft {x.WerkPlekken.Count} {x1} die iets meer controle nodig {x2}:" +
                  // $"<ul>" +
                   $"{string.Join("\r\n", x.WerkPlekken.Where(w=> w.ControleRatio() < -15).Select(w=> $"<li>{w.Naam} is maar {w.AantalHistory.Aantallen.Count} keer gecontrolleerd in {w.TijdGewerkt} uur tijd<span color='{IProductieBase.GetNegativeColorByPercentage((decimal)w.ControleRatio()).Name}'>({x.GetControleRatio()}%)</span></li>"))}" +
                  // $"</ul>" +
                   $"</u></b>" +
                   $"</a></li>";
        }

        public static string GetDailyGroet()
        {
            var xnow = DateTime.Now.TimeOfDay;
            string xreturn = $"Goeiedag {Manager.LogedInGebruiker?.Username}";
            if (xnow > TimeSpan.FromHours(0) && xnow < TimeSpan.FromHours(12))
                xreturn = $"Goedemorgen {Manager.LogedInGebruiker?.Username}";
            if (xnow >= TimeSpan.FromHours(12) && xnow < TimeSpan.FromHours(18))
                xreturn = $"Goedemiddag {Manager.LogedInGebruiker?.Username}";
            if (xnow >= TimeSpan.FromHours(18) && xnow <= TimeSpan.FromHours(24))
                xreturn = $"Goedenavond {Manager.LogedInGebruiker?.Username}";
            return xreturn;
        }

        /// <summary>
        /// Lock for SomeEvent delegate access.
        /// </summary>
        private readonly object dailyCreatedLock = new object();

        /// <summary>
        /// Delegate variable backing the SomeEvent event.
        /// </summary>
        private EventHandler<EventArgs> dailyCreated;

        /// <summary>
        /// Description for the event.
        /// </summary>
        public event EventHandler<EventArgs> DailyCreated
        {
            add
            {
                lock (this.dailyCreatedLock)
                {
                    this.dailyCreated += value;
                }
            }

            remove
            {
                lock (this.dailyCreatedLock)
                {
                    this.dailyCreated -= value;
                }
            }
        }

        public void RaiseEvent()
        {
            this.OnDailyCreated(EventArgs.Empty);
        }

        /// <summary>
        /// Raises the SomeEvent event.
        /// </summary>
        /// <param name="e">The event arguments.</param>
        protected virtual void OnDailyCreated(EventArgs e)
        {
            EventHandler<EventArgs> handler;

            lock (this.dailyCreatedLock)
            {
                handler = this.dailyCreated;
            }

            handler?.Invoke(this, e);
        }
    }
}
