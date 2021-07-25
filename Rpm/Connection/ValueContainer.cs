using System.Collections.Generic;
using System.Runtime.Serialization;
using Rpm.Productie;
using Rpm.Settings;

namespace Rpm.Connection
{
    [DataContract]
    public class ValueContainer
    {
        public ValueContainer()
        {
        }

        public ValueContainer(object value)
        {
            AddValue(value);
        }

        [DataMember] public List<Personeel> Personen { get; set; } = new();
        [DataMember] public List<ProductieFormulier> Producties { get; set; } = new();
        [DataMember] public List<UserSettings> Settings { get; set; } = new();
        [DataMember] public List<UserAccount> Accounts { get; set; } = new();

        public void AddValue(object value)
        {
            if (value != null)
                switch (value)
                {
                    case UserAccount[] accounts:
                        Accounts.AddRange(accounts);
                        break;
                    case Personeel[] personen:
                        Personen.AddRange(personen);
                        break;
                    case UserSettings[] settings:
                        Settings.AddRange(settings);
                        break;
                    case ProductieFormulier[] prods:
                        Producties.AddRange(prods);
                        break;
                    case UserAccount account:
                        Accounts.Add(account);
                        break;
                    case Personeel persoon:
                        Personen.Add(persoon);
                        break;
                    case UserSettings setting:
                        Settings.Add(setting);
                        break;
                    case ProductieFormulier prod:
                        Producties.Add(prod);
                        break;
                }
        }
    }
}