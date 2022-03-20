using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Org.BouncyCastle.Math.EC.Rfc7748;
using Rpm.Productie;
using Rpm.Various;

namespace ProductieManager.Rpm.Productie
{
    public static class BijlageBeheer
    {
        public static List<string> GetBijlages(string id)
        {
            try
            {
                var xpath = Path.Combine(Manager.DbPath, "Bijlages", id);
                if (Directory.Exists(xpath))
                    return Directory.GetFiles(xpath).ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return new List<string>();
        }

        public static bool UpdateBijlage(string id, byte[] data, string name)
        {
            try
            {
                var xpath = Path.Combine(Manager.DbPath, "Bijlages", id);
                if (!Directory.Exists(xpath))
                    Directory.CreateDirectory(xpath);
                var xfile = Path.Combine(xpath, name);
                using var fs = new FileStream(xfile, FileMode.Create, FileAccess.ReadWrite, FileShare.None);
                fs.Write(data, 0, data.Length);
                fs.Flush();
                fs.Close();
                if (Manager.Database != null)
                {
                    var xbase = id;
                    if (id.Contains("\\") || id.Contains("/"))
                    {
                        var xindex = id.IndexOf("\\", StringComparison.CurrentCultureIgnoreCase);
                        if(xindex == -1)
                            xindex = id.IndexOf("/", StringComparison.CurrentCultureIgnoreCase);
                        if (xindex > -1)
                        {
                            xbase = id.Substring(0, xindex);
                        }
                    }
                    var bw = Manager.Database.GetBewerkingen(ViewState.Gestart, true, null, null).Result
                        .FirstOrDefault(x => string.Equals(x.ArtikelNr, xbase, StringComparison.CurrentCultureIgnoreCase));
                    if (bw != null)
                    {
                        Manager.FormulierActie(new object[] {bw.Parent, bw,true, 11}, MainAktie.OpenProductie);
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
