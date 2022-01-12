using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;

namespace Rpm.DailyUpdate
{
    public class Daily
    {
        public string CreatedDaily { get; private set; }

        public Task CreateDaily()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    return;
                    if (Manager.LogedInGebruiker == null || Manager.Database?.ProductieFormulieren == null)
                    {
                        lock (CreatedDaily)
                        {
                            CreatedDaily = string.Empty;
                        }

                        return;
                    }

                    var prods = Manager.Database.GetBewerkingen(ViewState.Gestopt, false, null, null).Result;
                    if (prods is {Count: > 0})
                        prods = prods.Where(x => x.IsAllowed()).ToList();
                    var telaat = prods.Where(x => x.TeLaat).ToList();
                    var telangzaam = prods
                        .Where(x => x.State == ProductieState.Gestart && x.ProcentAfwijkingPerUur < -10).ToList();
                    var weinigcontrol = prods.Where(x =>
                        x.State == ProductieState.Gestart && x.WerkPlekken.Any(w => w.ControleRatio() < -15)).ToList();
                    var komentelaat = prods.Where(x => x.VerwachtLeverDatum > x.LeverDatum).ToList();
                    string username = Manager.LogedInGebruiker.Username.FirstCharToUpper();
                    string xgroet = GetDailyGroet();

                    string xret = $"<h2>{xgroet} {username}!</h2>" +
                                  $"<h4>De ProductieManager heeft de volgende mededeling voor u:</h4>";
                    if (telaat.Count == 0 && telangzaam.Count == 0 && weinigcontrol.Count == 0 &&
                        komentelaat.Count == 0)
                    {
                        xret += $"<h3>U doet het uistekend!!<br>" +
                                $"Er zijn geen bijzonderheden, omdat u alles goed bijhoudt.<br>" +
                                $"Ga zo door!</h3>";
                    }

                    lock (CreatedDaily)
                    {
                        CreatedDaily = xret;
                    }

                    RaiseEvent();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        public static string GetDailyGroet()
        {
            var xnow = DateTime.Now.TimeOfDay;
            string xreturn = "Goeiedag";
            if (xnow > TimeSpan.FromHours(0) && xnow < TimeSpan.FromHours(12))
                xreturn = "Goedemorgen";
            if (xnow >= TimeSpan.FromHours(12) && xnow < TimeSpan.FromHours(18))
                xreturn = "Goedemiddag";
            if (xnow >= TimeSpan.FromHours(18) && xnow <= TimeSpan.FromHours(24))
                xreturn = "Goedenavond";
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
