using Rpm.Productie;

namespace Rpm.ViewModels
{
    public class WerkPlekModel
    {
        public WerkPlekModel(Personeel[] personen, string werkplek)
        {
        }

        public Personeel[] Personen { get; set; }
        public string WerkPlek { get; set; }
        public int AantalGemaakt { get; set; }
        public string Omschrijving { get; set; }

        private void InitFields(Personeel[] personen, string werkplek)
        {
            Personen = personen;
            WerkPlek = werkplek;
        }
    }
}