using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Rpm.Misc;
using Rpm.NativeMethods;
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
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(id) || data == null || data.Length == 0) return false;
                if (!data.IsImage()) return false;
                var xpath = Path.Combine(Manager.DbPath, "Bijlages", id);
                if (!Directory.Exists(xpath))
                    Directory.CreateDirectory(xpath);
                var xfile = Path.Combine(xpath, name);
                using var fs = new FileStream(xfile, FileMode.Create, FileAccess.ReadWrite, FileShare.ReadWrite);
                fs.Write(data, 0, data.Length);
                fs.Flush();
                fs.Close();
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
    }
}
