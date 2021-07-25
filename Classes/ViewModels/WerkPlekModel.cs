using ProductieManager.Productie;

namespace ProductieManager.Classes.ViewModels
{
    public class WerkPlekModel
    {
        public Personeel[] Personen { get; set; }
        public string WerkPlek { get; set; }
        public int AantalGemaakt { get; set; }
        public string Omschrijving { get; set; }

        public WerkPlekModel(Personeel[] personen, string werkplek)
        {
        }

        private void InitFields(Personeel[] personen, string werkplek)
        {
            Personen = personen;
            WerkPlek = werkplek;
        }
    }
}