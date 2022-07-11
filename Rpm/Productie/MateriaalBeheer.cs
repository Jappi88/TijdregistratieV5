using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductieManager.Rpm.Productie
{
    public static class MateriaalBeheer
    {

        public static Task<Dictionary<string, MateriaalEntryInfo>> GetMateriaalVerbruik(TijdEntry bereik, ProgressChangedHandler progressChanged = null)
        {
            return Task.Run(() =>
            {
               
                var xreturn = new Dictionary<string, MateriaalEntryInfo> ();
                if (Manager.Database?.ProductieFormulieren == null) return xreturn;
                try
                {
                    progressChanged?.Invoke(null,
                        new ProgressArg()
                        {
                            Message = "Producties laden...", Progress = 0, Type = ProgressType.ReadBussy, Value = xreturn
                        });
                    var prods = Manager.Database.xGetAllProducties(true, true, bereik,null,true);
                    for (int i = 0; i < prods.Count; i++)
                    {
                        int percent = ((i / prods.Count) * 100);
                        progressChanged?.Invoke(null,
                            new ProgressArg()
                            {
                                Message = "Materialen laden...",
                                Progress = percent,
                                Type = ProgressType.ReadBussy,
                                Value = xreturn
                            });
                        var prod = prods[i];
                        if (prod.Materialen == null || prod.Materialen.Count == 0) continue;
                        for (int j = 0; j < prod.Materialen.Count; j++)
                        {
                            var mat = prod.Materialen[j];
                            if (mat == null || string.IsNullOrEmpty(mat.ArtikelNr)) continue;
                            MateriaalEntryInfo xmatinfo = null;
                            if (xreturn.ContainsKey(mat.ArtikelNr))
                                xmatinfo = xreturn[mat.ArtikelNr];
                            else
                            {
                                xmatinfo = new MateriaalEntryInfo
                                {
                                    ID = xreturn.Count
                                };
                                xreturn.Add(mat.ArtikelNr, xmatinfo);
                            }
                            xmatinfo.ArtikelNr = mat.ArtikelNr;
                            mat.ID = xmatinfo.ID;
                            if (!string.IsNullOrEmpty(mat.Omschrijving))
                                xmatinfo.Omschrijving = mat.Omschrijving;
                            if (!string.IsNullOrEmpty(mat.Eenheid))
                                xmatinfo.Eenheid = mat.Eenheid.ToLower() == "m" ? "Meter" : mat.Eenheid;
                            xmatinfo.Materialen.Add(mat);
                        }
                    }
                    progressChanged?.Invoke(null,
                        new ProgressArg()
                        {
                            Message = "Materialen succesvol geladen!",
                            Progress = 100,
                            Type = ProgressType.ReadCompleet,
                            Value = xreturn
                        });

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return xreturn;
            });
        }

    }
}
