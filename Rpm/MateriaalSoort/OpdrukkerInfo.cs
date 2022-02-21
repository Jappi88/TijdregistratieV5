using System;
using System.Collections.Generic;
using System.IO;
using Rpm.Misc;
using Rpm.Productie;

namespace Rpm.MateriaalSoort
{
    public class OpdrukkerInfo
    {
        #region Ctors

        public OpdrukkerInfo()
        {
            Types = OpdrukkerTypes;
            Diameters = BuisDiameters;
            WandDiktes = BuisWandDiktes;
            LipLengtes = OpdrukkerLipLengtes;
            Aantallen = PakketAantallen;
        }

        #endregion Ctors

        #region Variables

        public Dictionary<int, string> Types { get; set; }
        public Dictionary<int, decimal> Diameters { get; set; }
        public Dictionary<int, decimal> WandDiktes { get; set; }
        public Dictionary<int, decimal> LipLengtes { get; set; }
        public Dictionary<int, int> Aantallen { get; set; }

        #endregion Variables

        #region public Members

        public bool SaveInfo()
        {
            try
            {
                var xpath = Manager.DbPath;
                var xfile = Path.Combine(xpath, "OpdrukkerInfo.rpm");
                return this.Serialize(xfile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public OpdrukkerInfo LoadInfo()
        {
            try
            {
                var xpath = Manager.DbPath;
                var xfile = Path.Combine(xpath, "OpdrukkerInfo.rpm");
                var xdata = xfile.DeSerialize<OpdrukkerInfo>();
                if (xdata != null)
                {
                    Types = xdata.Types;
                    Diameters = xdata.Diameters;
                    WandDiktes = xdata.WandDiktes;
                    LipLengtes = xdata.LipLengtes;
                }

                return this;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        #endregion public Members

        #region Static Members

        public static Dictionary<int, string> OpdrukkerTypes = new()
        {
            {0, "Enkel D=6,5 A=10 D=6,5 A=10"},
            {1, "Enkel D=8,5 A=15 D=8,5 A=15"},
            {3, "Enkel D=8,5 A=15 D=8,5 A=25"},
            {5, "Enkel D=8,5 A=25 D=8,5 A=25"},
            {8, "Enkel D=6,5 A=10 D=8,5 A=15"},
            {20, "Dubbel D=6,5 A=10 D=6,5 A=10 H=20"},
            {25, "Dubbel D=6,5 A=10 D=6,5 A=10 H=20"},
            {50, "Enkel D=6,5 A=10 D=8,5 A=25"},
            {54, "Gedraaide lip Gebogen D=6,5 A=10 D=6,5 A=15"},
            {60, "Enkel D=6,5 A=10 D=6,5 A=10"},
            {64, "Gedraaide lip N.G.B D=6,5 A=10 D=6,5 A=15"},
            {65, "Gedraaide lip N.G.B D=8,5 A=15 D=6,5 A=15"},
            {83, "Flat roof D=8,5 A=15 D=8,5 A=15"},
            {84, "East-West D=8,5 A=25 D=8,5 A=25"},
            {85, "ValkBox 1 D=6,5 A=10 D=8,5 A=15"},
            {86, "ValkBox 2 D=6,5 A=10 D=8,5 A=15"}
        };

        public static Dictionary<int, int> PakketAantallen = new()
        {
            {0, 792},
            {1, 599},
            {2, 504},
            {3, 450},
            {4, 127},
            {5, 308},
            {6, 91},
            {7, 1},
            {8, 192},
            {9, 1}
        };

        public static Dictionary<int, decimal> BuisDiameters = new()
        {
            {0, 19},
            {1, 22},
            {2, 23.5m},
            {3, 25},
            {4, 27},
            {5, 30},
            {6, 32},
            {7, 0},
            {8, 38},
            {9, 0}
        };

        public static Dictionary<int, decimal> BuisWandDiktes = new()
        {
            {0, 0},
            {1, 0.9m},
            {2, 1},
            {3, 1.2m},
            {4, 1.5m},
            {5, 1.8m},
            {6, 2.1m},
            {7, 0},
            {8, 2.5m},
            {9, 0}
        };

        public static Dictionary<int, decimal> OpdrukkerLipLengtes = new()
        {
            {0, 22},
            {1, 30},
            {2, 42},
            {3, 50},
            {4, 60},
            {5, 70},
            {6, 80},
            {7, 90},
            {8, 100},
            {9, 35}
        };

        public static OpdrukkerInfo Load()
        {
            return new OpdrukkerInfo().LoadInfo();
        }

        #endregion Static Members
    }
}