using System;
using Rpm.Various;

namespace Rpm.Mailing
{
    [Serializable]
    public class InkomendAdres
    {
        public InkomendAdres()
        {
            Actions = new MessageAction[] { };
            Adres = "";
        }

        public InkomendAdres(string adres, MessageAction[] acties)
        {
            Actions = acties;
            Adres = adres;
        }

        public string Adres { get; set; }

        public MessageAction[] Actions { get; set; }
    }
}