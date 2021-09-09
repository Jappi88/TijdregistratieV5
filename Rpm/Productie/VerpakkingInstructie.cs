using System;

namespace Rpm.Productie
{
    public class VerpakkingInstructie
    {
        public string VerpakkingType { get; set; }
        public string PalletSoort { get; set; }
        public int VerpakkenPer { get; set; }
        public int LagenOpColli { get; set; }
        public int DozenOpColli { get; set; }
        public int PerLaagOpColli { get; set; }
        public int ProductenPerColli { get; set; }
        public string StandaardLocatie { get; set; }
        public string BulkLocatie { get; set; }
        public bool IsLijdend { get; set; }
        public DateTime LastChanged { get; set; }

        public VerpakkingInstructie()
        {
            LastChanged = DateTime.Now;
        }
    }
}
