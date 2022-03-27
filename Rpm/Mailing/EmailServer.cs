using Rpm.Mailing;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Rpm.Misc;
using Rpm.Productie;

namespace ProductieManager.Rpm.Mailing
{
    public class EmailServer
    {
        public bool IsRunning { get; private set; }

        public int Interval { get; set; } = 30000;
        public DateTime LastUpdated { get; private set; }

        public void Start()
        {
            Run();
        }

        public void Stop()
        {
            IsRunning = false;
        }

        private void UpdateServer()
        {
            try
            {
                var file = Path.Combine(Manager.DbPath, "OServer.rpm");
                this.Serialize(file);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private bool CanCheck()
        {
            try
            {
                var file = Path.Combine(Manager.DbPath, "OServer.rpm").DeSerialize<EmailServer>();
                if (file == null) return true;
                if(file.LastUpdated > DateTime.Now.Subtract(TimeSpan.FromMilliseconds(Interval))) return false;
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        private void Run()
        {
            if (IsRunning) return;
            IsRunning = true;
            Task.Factory.StartNew(() =>
            {
                try
                {
                    while (IsRunning)
                    {
                        //if (CanCheck())
                        //{
                        //LastUpdated = DateTime.Now;
                        //UpdateServer();
                        if (Manager.Opties?.InkomendMail != null && Manager.Opties.InkomendMail.AllowedActions.Count > 0)
                            RemoteProductie.ControlleerOpMessages();
                        //}

                        Thread.Sleep(Interval);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                IsRunning = false;
            });
            
        }
    }
}
