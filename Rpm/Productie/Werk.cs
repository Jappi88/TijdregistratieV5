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
        public string Path { get; set; }

        public static Werk FromPath(string path, ProductieFormulier parent)
        {
            var werk = new Werk();
            try
            {
                if (string.IsNullOrEmpty(path)) return werk;
                werk.Path = path.Replace("/", "\\").Trim();
                string[] xpaths = path.Split('\\');
                if (xpaths.Length == 0) return werk;
                var prod = parent ?? Manager.Database.GetProductie(xpaths[0].Trim(), false);
                if (prod == null)
                    return werk;
                werk.Formulier = prod;
                if (xpaths.Length > 1)
                {
                    var secvalue = xpaths[1].Trim();
                    if (int.TryParse(secvalue, out var bewindex))
                    {
                        if (prod.Bewerkingen != null && bewindex > 0)
                        {
                            if (bewindex <= prod.Bewerkingen.Length)
                            {
                                werk.Bewerking = prod.Bewerkingen[bewindex - 1];
                            }
                            else werk.Bewerking = prod.Bewerkingen[prod.Bewerkingen.Length - 1];
                        }
                    }
                    else
                        werk.Bewerking = prod.Bewerkingen?.FirstOrDefault(x =>
                            string.Equals(x.Naam, secvalue, StringComparison.CurrentCultureIgnoreCase));
                    if (werk.Bewerking != null)
                        werk.Path = werk.Bewerking.Path;

                }

                if (xpaths.Length > 2 && werk.Bewerking != null)
                {
                    var thirdvalue = xpaths[2].Trim();
                    if (int.TryParse(thirdvalue, out var wpindex))
                    {
                        if (werk.Bewerking?.WerkPlekken != null && wpindex > 0)
                        {
                            if (wpindex <= werk.Bewerking.WerkPlekken.Count)
                            {
                                werk.Plek = werk.Bewerking.WerkPlekken[wpindex - 1];
                            }
                            else werk.Plek = werk.Bewerking.WerkPlekken[werk.Bewerking.WerkPlekken.Count - 1];
                        }
                    }
                    else
                    {
                        werk.Plek = werk.Bewerking.WerkPlekken?.FirstOrDefault(x =>
                            string.Equals(x.Naam, thirdvalue, StringComparison.CurrentCultureIgnoreCase));
                    }

                    if (werk.Plek != null)
                        werk.Path = werk.Plek.Path;
                }
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
