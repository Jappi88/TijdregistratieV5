namespace ProductieManager.Classes.Provider
{
    public class ProductieFormulierProvider : Polenter.Serialization.Advanced.PropertyProvider
    {
        public ProductieFormulierProvider()
        {
            base.PropertiesToIgnore.Add(typeof(int), "AantalProducties");
        }
    }
}