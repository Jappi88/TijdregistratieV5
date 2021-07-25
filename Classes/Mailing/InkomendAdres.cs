using System;

namespace ProductieManager.Mailing
{
    [Serializable]
    public class InkomendAdres
    {
        public string Adres { get; set; }

        public MessageAction[] Actions { get; set; }

        public InkomendAdres()
        {
            Actions = new MessageAction[] { };
        }

        public InkomendAdres(string adres, MessageAction[] acties)
        {
            Actions = acties;
            Adres = adres;
        }
    }
}