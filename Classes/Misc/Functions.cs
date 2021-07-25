using ProductieManager.Classes.SqlLite;
using ProductieManager.Controls;
using ProductieManager.Mailing;
using ProductieManager.Productie;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Management;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace ProductieManager.Misc
{
    public static class Functions
    {

        public static bool IsAllowed(this ProductieFormulier form)
        {
            if (form == null)
                return false;
            if (Manager.Opties == null || Manager.Opties.ToonAlles)
                return true;
            if (Manager.Opties.ToonVolgensBewerkingen && (Manager.Opties.Bewerkingen == null || Manager.Opties.Bewerkingen.Length == 0))
                return false;
            if (Manager.Opties.ToonVolgensAfdelingen && (Manager.Opties.Afdelingen == null || Manager.Opties.Afdelingen.Length == 0))
                return false;
            if (Manager.Opties.ToonAllesVanBeide && (Manager.Opties.Afdelingen == null || Manager.Opties.Afdelingen.Length == 0) &&
                                                    (Manager.Opties.Bewerkingen == null || Manager.Opties.Bewerkingen.Length == 0))
                return false;

            if (Manager.Opties.ToonAllesVanBeide || Manager.Opties.ToonVolgensBewerkingen && Manager.Opties.Bewerkingen != null && Manager.Opties.Bewerkingen.Length > 0)
            {
                if (form.Bewerkingen != null && form.Bewerkingen.Length > 0)
                {
                    return Manager.Opties.Bewerkingen.Any(x => form.Bewerkingen.Any(s => s.Naam.ToLower().Split('[')[0] == x.ToLower()));
                }
                return false;
            }

            if (Manager.Opties.ToonAllesVanBeide || Manager.Opties.ToonVolgensAfdelingen && Manager.Opties.Afdelingen != null && Manager.Opties.Afdelingen.Length > 0)
            {
                if (form.Bewerkingen != null && form.Bewerkingen.Length > 0)
                {
                    int count = 0;
                    foreach (Bewerking bew in form.Bewerkingen)
                    {
                        var xs = Manager.BewerkingenLijst.FirstOrDefault(t => t.Value.Contains(bew.Naam.Split('[')[0])).Key;
                        if (xs != null && xs.Length > 0)
                        {
                            if (Manager.Opties.Afdelingen.Any(x => xs.Any(s => s.ToLower() == x.ToLower())))
                                count++;
                        }
                    }
                    return count > 0;
                }
                return false;
            }
            return true;
        }

        public static bool IsAllowed(this Bewerking bew)
        {
            if (bew == null)
                return false;
            if (Manager.Opties == null || Manager.Opties.ToonAlles)
                return true;

            if (Manager.Opties.ToonVolgensBewerkingen && (Manager.Opties.Bewerkingen == null || Manager.Opties.Bewerkingen.Length == 0))
                return false;
            if (Manager.Opties.ToonVolgensAfdelingen && (Manager.Opties.Afdelingen == null || Manager.Opties.Afdelingen.Length == 0))
                return false;
            if (Manager.Opties.ToonAllesVanBeide && (Manager.Opties.Afdelingen == null || Manager.Opties.Afdelingen.Length == 0) &&
                                                    (Manager.Opties.Bewerkingen == null || Manager.Opties.Bewerkingen.Length == 0))
                return false;

            if (Manager.Opties.ToonAllesVanBeide || Manager.Opties.ToonVolgensBewerkingen && Manager.Opties.Bewerkingen != null && Manager.Opties.Bewerkingen.Length > 0)
            {
                return Manager.Opties.Bewerkingen.Any(x => bew.Naam.ToLower().Split('[')[0] == x.ToLower());
            }

            if (Manager.Opties.ToonAllesVanBeide || Manager.Opties.ToonVolgensAfdelingen && Manager.Opties.Afdelingen != null && Manager.Opties.Afdelingen.Length > 0)
            {
                var xs = Manager.BewerkingenLijst.FirstOrDefault(t => t.Value.Contains(bew.Naam.Split('[')[0])).Key;
                if (xs != null && xs.Length > 0)
                    return Manager.Opties.Afdelingen.Any(x => xs.Any(s => s.ToLower() == x.ToLower()));
            }
            return true;
        }

        public static bool IsValidState(this ProductieFormulier form, ViewState vstate)
        {
            if (form == null)
                return false;
            switch (vstate)
            {
                case ViewState.Gestopt:
                    return form.State == ProductieState.Gestopt;

                case ViewState.Gestart:
                    return form.State == ProductieState.Gestart;

                case ViewState.Gereed:
                    return form.State == ProductieState.Gereed;

                case ViewState.Verwijderd:
                    return form.State == ProductieState.Verwijderd;

                case ViewState.Telaat:
                    return form.TeLaat && (form.State == ProductieState.Gestopt || form.State == ProductieState.Gestart);

                case ViewState.Nieuw:
                    return form.IsNieuw;

                case ViewState.Alles:
                    return true;

                case ViewState.None:
                    return false;
            }
            return true;
        }

        public static bool IsValidState(this Bewerking bew, ViewState vstate)
        {
            if (bew == null)
                return false;
            switch (vstate)
            {
                case ViewState.Gestopt:
                    return bew.State == ProductieState.Gestopt;

                case ViewState.Gestart:
                    return bew.State == ProductieState.Gestart;

                case ViewState.Gereed:
                    return bew.State == ProductieState.Gereed;

                case ViewState.Verwijderd:
                    return bew.State == ProductieState.Verwijderd;

                case ViewState.Telaat:
                    return bew.TeLaat && (bew.State == ProductieState.Gestopt || bew.State == ProductieState.Gestart);

                case ViewState.Nieuw:
                    return bew.IsNieuw;

                case ViewState.Alles:
                    return true;

                case ViewState.None:
                    return false;
            }
            return true;
        }

        public static void CheckUserAvailibility(this Personeel[] personen, Bewerking bew, Personeel[] dbpersonen)
        {
            try
            {
                if (personen != null && personen.Length > 0)
                {
                    foreach (Personeel per in personen)
                    {
                        Personeel xpers = dbpersonen.FirstOrDefault(x=> x.PersoneelNaam.ToLower() == per.PersoneelNaam.ToLower());
                        if (per.Actief && xpers != null && xpers.WerktAan != null && bew != null && !bew.Equals(xpers.WerktAan))
                            throw new Exception($"{xpers.PersoneelNaam} werkt al aan {xpers.WerktAan}!");
                        else if (xpers != null)
                        {
                            if (per.Actief)
                            {
                                WerkPlek plek = bew.WerkPlekken.FirstOrDefault(x => x.Personen.Any(t => t.PersoneelNaam.ToLower() == per.PersoneelNaam.ToLower()));
                                if(plek != null)
                                {
                                    double tijd = plek.TijdAanGewerkt(true);
                                    double tg = per.TijdAanGewerkt(plek.GetStoringen()).TotalHours;
                                    int percentage = (int)((tg / tijd) * 100);
                                    int eachp = percentage > 0 ? (int)((plek.AantalGemaakt / 100) * percentage) : 0;
                                    per.PerUur = tg == 0 ? eachp : (int)(eachp / tg);
                                }
                                xpers.PerUur = per.PerUur;
                                xpers.Gestart = per.Gestart;
                                xpers.Gestopt = per.Gestopt;
                                if (bew.State == ProductieState.Gestart && per.Actief)
                                {
                                    xpers.Werkplek = per.Werkplek;
                                    xpers.WerktAan = bew.Path;
                                }
                            }
                            else
                                xpers.WerktAan = null;
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }

        public static bool CheckUserAvailibility(this Personeel[] personen, Bewerking bew)
        {
            try
            {
                if (personen != null && personen.Length > 0 && bew != null)
                {
                    foreach (Personeel per in personen.Where(x => x.Actief))
                    {
                        Personeel xpers = Manager.Database.GetPersoneel(per.PersoneelNaam);
                        if (xpers != null && xpers.WerktAan != null && !bew.Equals(xpers.WerktAan))
                            throw new Exception($"{per.PersoneelNaam} werkt al aan {per.WerktAan}!");
                    }
                    return true;
                }
                return false;
            }
            catch (Exception ex) { throw ex; }
        }

        public static bool ZetOpInactief(this Personeel personeel)
        {
            try
            {
                if (personeel == null || personeel.WerktAan == null)
                    return false;
                Personeel xdb = Manager.Database.GetPersoneel(personeel.PersoneelNaam);
                if (xdb != null && xdb.WerktAan != null)
                {
                    Bewerking bew = null;
                    ProductieFormulier prod = xdb.GetWerk(out bew);
                    if (bew != null)
                    {
                        WerkPlek[] wps = bew.WerkPlekken.Where(x => x.Personen.Any(t => t.PersoneelNaam.ToLower() == personeel.PersoneelNaam.ToLower())).ToArray();
                        bool saved = false;
                        foreach (var wp in wps)
                        {
                            Personeel xpers = wp.Personen.FirstOrDefault(x => x.PersoneelNaam.ToLower() == personeel.PersoneelNaam.ToLower());
                            if (xpers != null && xpers.Actief)
                            {
                                double tg = xpers.TijdAanGewerkt(wp.GetStoringen()).TotalHours;
                                wp.LastTijdGewerkt += tg;
                                xpers.Gestopt = DateTime.Now;
                                xpers.WerktAan = null;
                                xpers.Actief = false;
                                saved = true;
                            }
                        }
                        if (saved)
                            bew.UpdateBewerking(null, $"{personeel.PersoneelNaam} op inactief gezet.", true);

                    }
                    xdb.Gestopt = DateTime.Now;
                    SkillTree.Update(bew, xdb, false);
                    xdb.WerktAan = null;
                    Manager.Database.UpSert(xdb, $"{personeel.PersoneelNaam} op inactief gezet.");
                    return true;
                }
                return false;
            }
            catch { return false; }
        }

        public static string Sha1(this string input)
        {
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(input));
                var sb = new StringBuilder(hash.Length * 2);

                foreach (byte b in hash)
                {
                    // can be "x2" if you want lowercase
                    sb.Append(b.ToString("X2"));
                }

                return sb.ToString();
            }
        }

        public static byte[] DeCompress(this byte[] data)
        {
            byte[] xreturn = null;
            try
            {
                using (var compressed = new MemoryStream(data))
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        using (var zip = new BufferedStream(new System.IO.Compression.GZipStream(compressed, System.IO.Compression.CompressionMode.Decompress)))
                        {
                            zip.CopyTo(ms);
                        }
                        xreturn = ms.ToArray();
                    }
                }
            }
            catch (Exception ex) { }
            return xreturn;
        }

        public static byte[] Compress(this byte[] data)
        {
            byte[] xreturn = null;
            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    using (System.IO.Compression.GZipStream zip = new System.IO.Compression.GZipStream(ms, System.IO.Compression.CompressionLevel.Fastest))
                    {
                        zip.Write(data, 0, data.Length);
                    }
                    xreturn = ms.ToArray();
                }
            }
            catch { }
            return xreturn;
        }

        public static bool IsValidState(this ProductieFormulier form, ViewState[] vstate)
        {
            if (form == null)
                return false;
            foreach (ViewState state in vstate)
            {
                if (form.IsValidState(state))
                    return true;
            }
            return false;
        }

        public static byte[] StringToByteArray(string entry)
        {
            return Encoding.UTF8.GetBytes(entry);
        }

        public static string ByteArrayToString(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        public static byte[] HexStringToByteArray(string hex)
        {
            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        public static int ReadInt32(this Stream input)
        {
            byte[] buffer = new byte[4];
            input.Read(buffer, 0, 4);
            return BitConverter.ToInt32(buffer, 0);
        }

        public static void WriteInt32(this Stream input, int value)
        {
            byte[] buffer = BitConverter.GetBytes(value);
            input.Write(buffer, 0, buffer.Length);
        }

        public static ProductieFormulier ReadProductie(this Stream input, Manager parent)
        {
            int length = input.ReadInt32();
            byte[] data = new byte[length];
            input.Read(data, 0, length);
            return ProductieFormulier.DeSerializeFromData(data, parent);
        }

        public static int GetWordIndex(this string blok, string value, int startindex)
        {
            if (string.IsNullOrEmpty(blok) || string.IsNullOrEmpty(value))
                return -1;

            int index = blok.IndexOf(value[0], startindex);
            int cur = 0;
            while (index > -1 && index < (blok.Length - value.Length))
            {
                cur++;
                string x = blok.Substring(index, value.Length);
                if (x.Match(value, value.Length - 2))
                    return index;
                index = blok.IndexOf(value[0], index + 1);
            }
            return -1;
        }

        public static bool Match(this string a, string b, int matchcount)
        {
            int count = 0;
            int cur = 0;
            for (int i = 0; i < a.Length; i++)
            {
                if (i > b.Length)
                    break;
                if (a[i] == b[cur])
                { count++; cur++; }
            }
            if (count >= matchcount)
                return true;
            return false;
        }

        public static int CountSameNodes(this TreeView tv, string name, bool subnodes)
        {
            if (tv == null || name == null) return 0;
            int count = 0;
            foreach (TreeNode x in tv.Nodes)
            {
                if (x.Text.ToLower() == name.ToLower())
                    count++;
                if (subnodes)
                    count += CountSameNodes(x, name, subnodes);
            }
            return count;
        }

        public static int CountSameNodes(this TreeNode node, string name, bool subnodes)
        {
            if (node == null) return 0;
            int count = 0;
            foreach (TreeNode x in node.Nodes)
            {
                if (x.Text.ToLower() == name.ToLower())
                    count++;
                if (subnodes)
                    count += CountSameNodes(x, name, subnodes);
            }
            return count;
        }

        public static bool ContainsNode(this TreeView tv, string name, bool subnodes)
        {
            if (tv == null || name == null) return false;
            foreach (TreeNode x in tv.Nodes)
            {
                if (x.Text.ToLower() == name.ToLower())
                    return true;
                if (subnodes && ContainsNode(x, name, subnodes))
                    return true;
            }
            return false;
        }

        public static bool ContainsNode(this TreeNode node, string name, bool subnodes)
        {
            if (node == null) return false;
            foreach (TreeNode x in node.Nodes)
            {
                if (x.Text.ToLower() == name.ToLower())
                    return true;
                if (subnodes && x.Nodes.Count > 0)
                    return ContainsNode(x, name, subnodes);
            }
            return false;
        }

        public static string GetTextFromImage(this Bitmap image)
        {
            string xreturn = "";
            //using (TesseractEngine x = new TesseractEngine(Mainform.BootDir, "nld", EngineMode.TesseractOnly))
            //{
            //    using var page = x.Process(image, PageSegMode.SingleColumn);
            //    xreturn = page.GetText().Trim();
            //}
            return xreturn;
        }

        public static Bitmap ResizeImage(this Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using var wrapMode = new ImageAttributes();
                wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
            }

            return destImage;
        }

        public static Image CombineImage(this Image a, Image b, double scale)
        {
            return a.CombineImage(b, a.Width, a.Height, ContentAlignment.BottomRight, scale);
        }

        public static Image CombineImage(this Image a, Image b, ContentAlignment contentAlignment, double scale)
        {
            return a.CombineImage(b, a.Width, a.Height, contentAlignment, scale);
        }

        public static Image CombineImage(this Image a, Image b, int width, int height, double scale)
        {
            return a.CombineImage(b, width, height, ContentAlignment.BottomRight, scale);
        }

        public static Image CombineImage(this Image a, Image b, int width, int height)
        {
            return a.CombineImage(b, width, height, ContentAlignment.BottomRight, 1.5);
        }

        public static Image CombineImage(this Image a, Image b, int width, int height, ContentAlignment alignment, double scale)
        {
            Image xreturn = a.ResizeImage(width, height);
            b = b.ResizeImage((int)(width / scale), (int)(height / scale));
            try
            {
                Size align = new Size(width - b.Width, height - b.Height);
                int cwidth = align.Width;
                int cheight = align.Height;
                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                        align = new Size(0, 0);
                        break;

                    case ContentAlignment.TopCenter:
                        align = new Size(cwidth / 2, 0);
                        break;

                    case ContentAlignment.TopRight:
                        align = new Size(cwidth, 0);
                        break;

                    case ContentAlignment.MiddleLeft:
                        align = new Size(0, cheight / 2);
                        break;

                    case ContentAlignment.MiddleCenter:
                        align = new Size(cwidth / 2, cheight / 2);
                        break;

                    case ContentAlignment.MiddleRight:
                        align = new Size(cwidth, cheight / 2);
                        break;

                    case ContentAlignment.BottomLeft:
                        align = new Size(0, cheight);
                        break;

                    case ContentAlignment.BottomCenter:
                        align = new Size(cwidth / 2, cheight);
                        break;

                    case ContentAlignment.BottomRight:
                        align = new Size(cwidth, cheight);
                        break;
                }
                using (Graphics g = System.Drawing.Graphics.FromImage(xreturn))
                {
                    g.Clear(Color.Transparent);
                    g.DrawImage(a,
                      new Rectangle(0, 0, width, height));

                    g.DrawImage(b,
                     new Rectangle(align.Width, align.Height, b.Width, b.Height));
                }
                return xreturn;
            }
            catch (Exception)
            {
                if (xreturn != null)
                    xreturn.Dispose();

                return null;
            }
        }

        public static void AppendText(this RichTextBox box, string text, Color color, Font font = null)
        {
            try
            {
                box.SelectionStart = box.TextLength;
                box.SelectionLength = 0;

                box.SelectionColor = color;
                if (font != null)
                    box.SelectionFont = font;
                box.AppendText(text);
                box.SelectionColor = box.ForeColor;
            }
            catch { }
        }

        public static string WrapText(this string text, int width)
        {
            string x = text;
            if (text != null && text.Length > width)
            {
                int count = 0;
                string value = text.Replace("\n", " ");
                foreach (char v in value)
                {
                    if (count >= width && v == ' ')
                    {
                        x += "\n";
                        count = 0;
                    }
                    else
                    {
                        x += v;
                        count++;
                    }
                }
            }
            return x;
        }

        public static bool IsImage(this byte[] data)
        {
            List<string> jpg = new List<string> { "FF", "D8" };
            List<string> bmp = new List<string> { "42", "4D" };
            //List<string> gif = new List<string> { "47", "49", "46" };
            // List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
            List<List<string>> imgTypes = new List<List<string>> { jpg, bmp };

            List<string> bytesIterated = new List<string>();

            for (int i = 0; i < 8; i++)
            {
                string bit = data[i].ToString("X2");
                bytesIterated.Add(bit);

                bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                if (isImage)
                    return true;
            }

            return false;
        }

        public static string[] GetBewerkingen(this string value, Dictionary<string[], string[]> bewerkingen)
        {
            if (bewerkingen == null || value == null)
                return new string[] { };
            string[] values = value.Split(' ');
            List<string> xreturn = new List<string>();
            string bew = "";
            foreach (string x in values)
            {
                bew += x;
                var xbew = bewerkingen.Where(t => t.Value.Where(s => s.ToLower() == bew.ToLower()).FirstOrDefault() != null).FirstOrDefault();
                if (xbew.Value != null && xbew.Key != null)
                {
                    xreturn.Add(bew);
                    bew = "";
                }
                else
                    bew += " ";
            }
            return xreturn.ToArray();
        }

        public static string[] BewerkingenByKey(this Dictionary<string[], string[]> bewerkingen, string keyvalue)
        {
            List<string> xreturn = new List<string> { };
            foreach (var b in bewerkingen)
            {
                var result = b.Key.Where(t => t.ToLower() == keyvalue.ToLower()).FirstOrDefault();
                if (result != null)
                {
                    xreturn.AddRange(b.Value);
                }
            }
            return xreturn.ToArray();
        }

        public static string[] KeyvaluesByBewerking(this Dictionary<string[], string[]> bewerkingen, string bewerking)
        {
            List<string> xreturn = new List<string> { };
            foreach (var b in bewerkingen)
            {
                var result = b.Value.Where(t => t.ToLower() == bewerking.ToLower()).FirstOrDefault();
                if (result != null)
                {
                    xreturn.AddRange(b.Key);
                }
            }
            return xreturn.ToArray();
        }

        public enum DateTimeConvertType
        {
            FullDate,
            OnlyDate,
            OnlyTime
        }

        private static Regex CreateValidEmailRegex()
        {
            string validEmailPattern = @"^(?!\.)(""([^""\r\\]|\\[""\r\\])*""|"
                + @"([-a-z0-9!#$%&'*+/=?^_`{|}~]|(?<!\.)\.)*)(?<!\.)"
                + @"@[a-z0-9][\w\.-]*[a-z0-9]\.[a-z][a-z\.]*[a-z]$";

            return new Regex(validEmailPattern, RegexOptions.IgnoreCase);
        }

        public static bool EmailIsValid(this string emailAddress)
        {
            if (emailAddress == null)
                return false;
            Regex ValidEmailRegex = CreateValidEmailRegex();
            return ValidEmailRegex.IsMatch(emailAddress);
        }

        public static string DateToString(this DateTime date, DateTimeConvertType type)
        {
            switch (type)
            {
                case DateTimeConvertType.FullDate:
                    return date.ToString("dddd d MMMM yyyy HH:mm");

                case DateTimeConvertType.OnlyDate:
                    return date.ToString("dddd d MMMM yyyy");

                case DateTimeConvertType.OnlyTime:
                    return date.TimeOfDay.ToString("HH:mm");
            }
            return date.ToString();
        }

        public static DateTime ToDateTime(this string value)
        {
            DateTime date = new DateTime();
            DateTime.TryParse(value, out date);
            return date;
        }

        public static TimeSpan ToTime(this string value)
        {
            DateTime date = new DateTime();
            DateTime.TryParse(value, out date);
            return date.TimeOfDay;
        }

        public static object ToObjectValue(this string value, Type type)
        {
            object xreturn = null;
            if (typeof(int) == type)
            {
                int val;
                if (int.TryParse(value, out val))
                    xreturn = val;
            }
            else if (typeof(string) == type)
            {
                xreturn = value;
            }
            else if (typeof(double) == type)
            {
                double val;
                if (double.TryParse(value, out val))
                    xreturn = val;
            }
            else if (typeof(DateTime) == type)
            {
                DateTime val;
                if (DateTime.TryParse(value, out val))
                    xreturn = val;
            }
            else if (typeof(bool) == type)
            {
                bool val;
                if (bool.TryParse(value, out val))
                    xreturn = val;
            }
            return xreturn;
        }

        public static T ConvertObject<T>(object input)
        {
            return (T)Convert.ChangeType(input, typeof(T));
        }

        public static void Alert(this string msg, MsgType type)
        {
            Form_Alert frm = new Form_Alert();
            frm.showAlert(msg, type);
        }

        public static string ReadResource(string name)
        {
            // Determine path
            try
            {
                string xreturn = null;
                var assembly = Assembly.GetExecutingAssembly();
                string resourcePath = name;
                // Format: "{Namespace}.{Folder}.{filename}.{Extension}"

                resourcePath = assembly.GetManifestResourceNames()
                    .Single(str => str.ToLower().EndsWith(name.ToLower()));

                using (Stream stream = assembly.GetManifestResourceStream(resourcePath))
                using (StreamReader reader = new StreamReader(stream))
                {
                    xreturn = reader.ReadToEnd();
                }
                return xreturn;
            }
            catch { return null; }
        }

        public static Dictionary<string[], string[]> LoadBewerkingLijst(string txt)
        {
            Dictionary<string[], string[]> xreturn = new Dictionary<string[], string[]> { };
            try
            {
                if (!File.Exists(txt))
                {
                    string value = ReadResource(txt);
                    if (value != null)
                        File.WriteAllText(txt, value);
                    else return xreturn;
                }
                using StreamReader sr = new StreamReader(txt);
                {
                    bool isread = false;
                    string line = "";
                    while (sr.Peek() > -1)
                    {
                        line = isread ? line : sr.ReadLine();
                        isread = false;
                        if (line.Length < 4)
                            continue;
                        if (line.StartsWith("[") && line.EndsWith("]"))
                        {
                            //de afdelingen waar de bewerkingen worden uitgevoerd.
                            string[] keys = line.Trim(new char[] { '[', ']' }).Split(',');
                            List<string> values = new List<string> { };
                            while (sr.Peek() > -1)
                            {
                                line = sr.ReadLine();
                                if (line.Length < 4)
                                    continue;
                                isread = true;
                                if (line.StartsWith("[") && line.EndsWith("]"))
                                    break;
                                values.Add(line.Trim());
                            }
                            xreturn.Add(keys, values.ToArray());
                        }
                    }
                }
            }
            catch { }
            return xreturn;
        }

        public static bool PublicInstancePropertiesEqual<T>(this T self, T to, params string[] ignore) where T : class
        {
            if (self == null && to == null)
                return true;
            if (self == null || to == null)
                return false;
            var prop1 = self.GetType().GetProperties().Where(t => (t.PropertyType != typeof(byte[]))).ToArray();
            var prop2 = to.GetType().GetProperties().Where(t => (t.PropertyType != typeof(byte[]))).ToArray();
            if (prop1.Length != prop2.Length)
                return false;
            foreach (var prop in prop1)
            {
                if (prop2.Where(t => (t.Name == prop.Name && t.GetValue(self).CompareObjectTo(prop.GetValue(to)))).FirstOrDefault() == null)
                    return false;
            }
            return true;
        }

        public static bool CompareObjectTo(this object a, object b)
        {
            if (a == null && b == null)
                return true;
            if (a == null || b == null || a.GetType() != b.GetType())
                return false;
            if (a is byte[] || b is byte[])
                return true;
            if (a is DateTime[])
            {
                DateTime[] xa = a as DateTime[];
                DateTime[] xb = b as DateTime[];
                if (xa.Length != xb.Length)
                    return false;
                for (int i = 0; i < xa.Length; i++)
                {
                    if (xa[i] != xb[i])
                        return false;
                }
                return true;
            }
            else if (a is UitgaandAdres[])
            {
                UitgaandAdres[] a1 = a as UitgaandAdres[];
                UitgaandAdres[] a2 = b as UitgaandAdres[];
                if (a1.Length != a2.Length)
                    return false;
                for (int i = 0; i < a1.Length; i++)
                {
                    if (a1[i].Adres.ToLower() != a2[i].Adres.ToLower())
                        return false;

                    if (a1[i].States != null && a2[i].States == null)
                        return false;
                    if (a2[i].States != null && a1[i].States == null)
                        return false;
                    if (a1[i].States.Length != a2[i].States.Length)
                        return false;
                    for (int j = 0; j < a1[i].States.Length; j++)
                        if (!a2[i].States.Any(x => x == a1[i].States[j]))
                            return false;
                }
                return true;
            }
            else if (a is InkomendAdres[])
            {
                InkomendAdres[] a1 = a as InkomendAdres[];
                InkomendAdres[] a2 = b as InkomendAdres[];
                if (a1.Length != a2.Length)
                    return false;
                for (int i = 0; i < a1.Length; i++)
                {
                    if (a1[i].Adres.ToLower() != a2[i].Adres.ToLower())
                        return false;

                    if (a1[i].Actions != null && a2[i].Actions == null)
                        return false;
                    if (a2[i].Actions != null && a1[i].Actions == null)
                        return false;
                    if (a1[i].Actions.Length != a2[i].Actions.Length)
                        return false;
                    for (int j = 0; j < a1[i].Actions.Length; j++)
                        if (!a2[i].Actions.Any(x => x == a1[i].Actions[j]))
                            return false;
                }
                return true;
            }
            else if (a is ViewState[])
            {
                ViewState[] a1 = a as ViewState[];
                ViewState[] a2 = b as ViewState[];
                if (a1.Length != a2.Length)
                    return false;
                for (int i = 0; i < a1.Length; i++)
                {
                    if (a1[i] != a2[i])
                        return false;
                }
                return true;
            }
            else if (a is string[])
            {
                string[] xa = a as string[];
                string[] xb = b as string[];
                if (xa.Length != xb.Length)
                    return false;
                for (int i = 0; i < xa.Length; i++)
                {
                    if (xa[i].ToLower() != xb[i].ToLower())
                        return false;
                }
                return true;
            }
            else if (a is UserChange)
                return true;
            else
            {
                bool xs = a.Equals(b);
                if (!xs)
                    Console.WriteLine("");
                return xs;
            }
        }

        public static bool IsSimpleType(
           this Type type)
        {
            return
               type.IsValueType ||
               type.IsPrimitive ||
               new[]
               {
               typeof(String),
               typeof(String[]),
               typeof(Decimal),
               typeof(DateTime),
               typeof(DateTimeOffset),
               typeof(TimeSpan),
               typeof(Guid)
               }.Contains(type) ||
               (Convert.GetTypeCode(type) != TypeCode.Object);
        }

        public static Type GetUnderlyingType(this MemberInfo member)
        {
            switch (member.MemberType)
            {
                case MemberTypes.Event:
                    return ((EventInfo)member).EventHandlerType;

                case MemberTypes.Field:
                    return ((FieldInfo)member).FieldType;

                case MemberTypes.Method:
                    return ((MethodInfo)member).ReturnType;

                case MemberTypes.Property:
                    return ((PropertyInfo)member).PropertyType;

                default:
                    throw new ArgumentException
                    (
                       "Input MemberInfo must be if type EventInfo, FieldInfo, MethodInfo, or PropertyInfo"
                    );
            }
        }

        public static void Shutdown()
        {
            ManagementBaseObject mboShutdown = null;
            ManagementClass mcWin32 = new ManagementClass("Win32_OperatingSystem");
            mcWin32.Get();

            // You can't shutdown without security privileges
            mcWin32.Scope.Options.EnablePrivileges = true;
            ManagementBaseObject mboShutdownParams =
                     mcWin32.GetMethodParameters("Win32Shutdown");

            // Flag 1 means we want to shut down the system. Use "2" to reboot.
            mboShutdownParams["Flags"] = "1";
            mboShutdownParams["Reserved"] = "0";
            foreach (ManagementObject manObj in mcWin32.GetInstances())
            {
                mboShutdown = manObj.InvokeMethod("Win32Shutdown",
                                               mboShutdownParams, null);
            }
        }

        public static void SetAutoStartOnBoot(bool autostart)
        {
            string path = Assembly.GetEntryAssembly().Location; // for getting the location of exe file ( it can change when you change the location of exe)
            string fileName = Assembly.GetExecutingAssembly().GetName().Name; // for getting the name of exe file( it can change when you change the name of exe)
            StartExeWhenPcStartup(fileName, path, autostart); // start the exe autometically when computer is stared.
        }

        public static void StartExeWhenPcStartup(string filename, string filepath, bool startup)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (startup) key.SetValue(filename, filepath);
            else key.DeleteValue(filename, false);
        }
    }
}