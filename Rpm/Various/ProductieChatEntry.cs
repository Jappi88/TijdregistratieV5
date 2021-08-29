using System;
using System.IO;
using System.Linq;
using Rpm.Misc;

namespace ProductieManager.Rpm.Various
{
    public class ProductieChatEntry
    {
        public string ID { get; set; }
        public UserChat Afzender { get; set; }
        public string Ontvanger { get; set; }

        public string[] Ontvangers =>
            string.IsNullOrEmpty(Ontvanger)
                ? new string[]{}
                : Ontvanger.Split(';').Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim()).ToArray();

        public string Bericht { get; set; }
        public DateTime Tijd { get; set; }
        public bool IsGelezen { get; set; }
        public bool IsPrivate => Ontvangers.Length <= 1;

        public ProductieChatEntry()
        {
            ID = DateTime.Now.Ticks.ToString();
            Tijd = DateTime.Now;
        }

        public bool UpdateMessage()
        {
            var paths = ProductieChat.GetWritePaths(false);
            if (paths.Length == 0) return false;
            foreach (var ontv in Ontvangers)
            {
                try
                {
                    if (string.IsNullOrEmpty(ontv)) continue;

                    var xfirst = paths[0];
                    var xfile = Path.Combine(xfirst, "Chat", ontv, "Berichten", $"{ID}.rpm");
                    if (this.Serialize(xfile))
                    {
                        for (int i = 1; i < paths.Length; i++)
                        {
                            var path2 = Path.Combine(paths[i], "Chat", ontv, "Berichten", $"{ID}.rpm");
                            for (int j = 0; j < 5; j++)
                            {
                                try
                                {
                                    File.Copy(xfile, path2, true);
                                    break;
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                            }
                        }

                        return true;
                    }

                    return false;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            }

            return false;
        }


    }
}
