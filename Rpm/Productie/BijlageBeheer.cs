using System;
using System.Collections.Generic;
using System.Drawing;
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

        public static bool HasValidExtension(string name)
        {
            if (string.IsNullOrEmpty(name)) return false;
            var ext = Path.GetExtension(name);
            if (string.IsNullOrEmpty(ext)) return false;
            switch (ext.ToLower())
            {
                case ".txt":
                case ".docx" :
                case ".xlsx" :
                case ".pdf" :
                    return true;
            }

            return false;
        }

        public static byte[] Resize2Maxbytes(byte[] byteImageIn, long maxsize)
        {
            byte[] currentByteImageArray = byteImageIn;
            double scale = 1f;
            using MemoryStream inputMemoryStream = new MemoryStream(byteImageIn);
            Image fullsizeImage = Image.FromStream(inputMemoryStream);
            while (currentByteImageArray.Length > maxsize)
            {
                Bitmap fullSizeBitmap = new Bitmap(fullsizeImage, new Size((int)(fullsizeImage.Width * scale), (int)(fullsizeImage.Height * scale)));
                using MemoryStream resultStream = new MemoryStream();
                fullSizeBitmap.Save(resultStream, fullsizeImage.RawFormat);

                currentByteImageArray = resultStream.ToArray();
                resultStream.Dispose();
                resultStream.Close();

                scale -= 0.01f;
            }

            return currentByteImageArray;
        }

        public static bool UpdateBijlage(string id, byte[] data, string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(id) || data == null || data.Length == 0) return false;
                if (!HasValidExtension(name))
                {
                    if (!data.IsImage()) return false;
                    if (!Functions.IsRecognisedImageFile(name)) return false;
                }
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
