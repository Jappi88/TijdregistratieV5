using System;
using System.Linq;
using Rpm.Productie;

namespace Rpm.Productie
{
    public class Werk
    {
        public bool IsValid => Formulier != null;
        public ProductieFormulier Formulier { get; set; }
        public Bewerking Bewerking { get; set; }
        public WerkPlek Plek { get; set; }

        public static Werk FromPath(string path, ProductieFormulier parent)
        {
            var werk = new Werk();
            try
            {
                if (string.IsNullOrEmpty(path)) throw new Exception("Werk locatie kan niet leeg zijn!");
                string[] xpaths = path.Split('\\');
                if (xpaths.Length == 0) throw new Exception("Werk locatie kan niet leeg zijn!");
                var prod = parent ?? Manager.Database.GetProductie(xpaths[0]).Result;
                if (prod == null)
                    return null;
                werk.Formulier = prod;
                if (xpaths.Length > 1)
                    werk.Bewerking = prod.Bewerkingen?.FirstOrDefault(x =>
                        string.Equals(x.Naam, xpaths[1], StringComparison.CurrentCultureIgnoreCase));
                if (xpaths.Length > 2 && werk.Bewerking != null)
                    werk.Plek = werk.Bewerking.WerkPlekken?.FirstOrDefault(x =>
                        string.Equals(x.Naam, xpaths[2], StringComparison.CurrentCultureIgnoreCase));
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return werk;
        }

        public static Werk FromPath(string path)
        {
            return FromPath(path, null);
        }
    }
}
