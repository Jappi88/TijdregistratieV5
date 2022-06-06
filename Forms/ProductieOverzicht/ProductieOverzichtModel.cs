using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Forms
{
    public class ProductieOverzichtModel
    {
        public ProductieOverzichtModel()
        {

        }

        public ProductieOverzichtModel(IProductieBase productie)
        {
            Model = productie;
        }

        public IProductieBase Model { get; set; }
        public string ArtikelNr { get => Model?.ArtikelNr; }
        public string ProductieNr { get => Model?.ProductieNr; }
        public DateTime LeverDatum { get => Model?.LeverDatum??default; }
        private string _omschrijving;
        public string Omschrijving { get => _omschrijving ?? Model?.Omschrijving; set => _omschrijving = value; }
        public DateTime StartOp { get; set; }
        public DateTime GereedOp { get; set; }
        public int AantalPersonen { get; set; }
        private int _peruur = -1;
        public int PerUur { get => (int)GetPerUur(); set => _peruur = value; }
        public double InstelTijd { get; set; }
        private int _aantal = 0;
        private int _gemaakt = -1;
        public int Aantal { get => _aantal > 0 ? _aantal : Model?.Aantal ?? 0; set => _aantal = value; }
        public int AantalGemaakt { get => _gemaakt > -1 ? _gemaakt : Model?.TotaalGemaakt ?? 0; set => _gemaakt = value; }
        public bool IsBewerking { get => Model is Bewerking; }
        public bool ContainsBewerkingen { get => ChildrenModels?.Any(x => x.IsBewerking) ?? false; }
        public List<ProductieOverzichtModel> ChildrenModels { get; set; } = new List<ProductieOverzichtModel>();
        private double GetPerUur()
        {
            if (_peruur > 0) return _peruur;
            if (Model == null) return 0;
            if (Model.ActueelPerUur > 0) return Model.ActueelPerUur;
            return Model.PerUur;
        }
    }
}
