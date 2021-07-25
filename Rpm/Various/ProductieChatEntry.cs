using System;
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
            string.IsNullOrEmpty(Ontvanger) ? new string[0] : Ontvanger.Split(';').Where(x=> !string.IsNullOrEmpty(x)).Select(x => x.Trim()).ToArray();
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
            try
            {
                bool updated = false;
                foreach (var ontv in Ontvangers)
                {
                    try
                    {
                        string path = ProductieChat.ChatPath + $"\\{ontv}\\Berichten\\{ID}.rpm";
                        updated |= this.Serialize(path);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                return updated;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

        }
    }
}
