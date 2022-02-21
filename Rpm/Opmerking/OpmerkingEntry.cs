using System;
using System.Collections.Generic;
using System.Linq;
using Rpm.Productie;

namespace Rpm.Opmerking
{
    public class OpmerkingEntry
    {
        public OpmerkingEntry()
        {
            Title = "";
            Opmerking = "";
            Reacties = new List<ReactieEntry>();
            Afzender = Manager.Opties?.Username ?? "Onbekend";
            Ontvangers = new List<string>();
            GeplaatstOp = DateTime.Now;
        }

        public string Afzender { get; set; }
        public List<string> Ontvangers { get; set; }
        public List<ReactieEntry> Reacties { get; set; }
        public string Title { get; set; }
        public string Opmerking { get; set; }
        public DateTime GeplaatstOp { get; set; }
        public Dictionary<string, byte[]> Bijlages { get; set; } = new();
        public List<string> Producties { get; set; }

        public bool IsGelezen =>
            string.Equals(Afzender, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase) ||
            Reacties is {Count: > 0} && Reacties.All(x => x.IsGelezen);

        public void SetOpmerking(string title, string opmerking, List<string> ontvangers)
        {
            Title = title;
            Ontvangers = ontvangers;
            Ontvangers ??= new List<string>();
            Opmerking = opmerking;
            GeplaatstOp = DateTime.Now;
        }

        public void SetReactie(string reactie)
        {
            Reacties ??= new List<ReactieEntry>();
            var rea = Reacties.FirstOrDefault(x =>
                string.Equals(x.ReactieVan, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));
            if (rea == null)
            {
                var xrea = new ReactieEntry();
                xrea.SetReactie(reactie);
                Reacties.Add(xrea);
            }
            else
            {
                rea.SetReactie(reactie);
            }
        }

        public ReactieEntry GetReactie()
        {
            return Reacties?.FirstOrDefault(x =>
                string.Equals(x.ReactieVan, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));
        }

        public void SetIsGelezen(bool gelezen)
        {
            Reacties ??= new List<ReactieEntry>();
            //var reas = Reacties.Where(x => !x.GelezenDoor.Any(s=> 
            //    string.Equals(s, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase)));
            var xrea = Reacties.FirstOrDefault(x =>
                string.Equals(Manager.Opties?.Username, x.ReactieVan, StringComparison.CurrentCultureIgnoreCase));
            if (!gelezen && xrea != null)
                Reacties.Remove(xrea);
            else if (xrea == null && gelezen)
                Reacties.Add(new ReactieEntry());
            else if (gelezen)
                xrea.SetGelezen();
            foreach (var rea in Reacties)
            {
                rea.GelezenDoor.RemoveAll(x =>
                    string.Equals(x, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));
                if (gelezen)
                    rea.GelezenDoor.Add(Manager.Opties?.Username);
            }
            //if (rea == null)
            //{
            //    var xrea = new ReactieEntry();
            //    xrea.SetGelezen();
            //    Reacties.Add(xrea);
            //}
            //else rea.SetGelezen();
        }

        public bool CanRead()
        {
            return string.Equals(Afzender, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase) ||
                   Ontvangers != null && Ontvangers.Any(x =>
                       string.Equals(x, "iedereen", StringComparison.CurrentCultureIgnoreCase) ||
                       string.Equals(x, Manager.Opties?.Username, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}