using System;
using System.Drawing;
using System.Globalization;
using System.Linq;

namespace Rpm.MateriaalSoort
{
    public enum OpdrukkerMachine
    {
        Opdrukker1,
        Opdrukker2
    }

    public class Opdrukker
    {
        public int PakketAantal { get; set; }
        public OpdrukkerInfo Info { get; private set; }
        public string ArtikelNr { get; set; }
        public string BuisSoort { get; set; }
        public int OpdrukkerType { get; set; }
        public int BuisDiameter { get; set; }
        public int BuisWandDikte { get; set; }
        public decimal Hoh { get; set; }
        public decimal KnipMaat { get; set; }
        public decimal HoekRaamkant { get; set; }
        public decimal HoekKlemKlant { get; set; }
        public int LengteRaamKant { get; set; }
        public int LengteKlemKant { get; set; }
        public OpdrukkerMachine Machine { get; set; } = OpdrukkerMachine.Opdrukker1;

        public bool IsDubbelgat()
        {
            return OpdrukkerType is >= 20 and <= 25;
        }

        public bool IsGedraaideLip()
        {
            return (OpdrukkerType is >= 64 and <= 65 or 54);
        }

        public bool IsGebogen()
        {
            return (OpdrukkerType is 54 || HoekKlemKlant > HoekRaamkant);
        }

        public string GetTitle()
        {
            var xsoort = "Alu.";
            if (!string.IsNullOrEmpty(BuisSoort))
            {
                switch (BuisSoort.ToLower())
                {
                    case "a":
                        xsoort = "Alu.";
                        break;
                    case "g":
                        xsoort = "Geanod.";
                        break;
                    case "s":
                        xsoort = "Stl.";
                        break;
                }
            }
            string xmat = "xx";
            if (Info != null)
            {
                if (Info.Diameters.ContainsKey(BuisDiameter))
                    xmat = ((int) Info.Diameters[BuisDiameter]).ToString();
                if (Info.WandDiktes.ContainsKey(BuisWandDikte))
                    xmat += "x" + Math.Round(Info.WandDiktes[BuisWandDikte], 1).ToString(CultureInfo.InvariantCulture)
                        .Replace(".", ",");
            }

            return $"{xsoort} Valk-Plus opdrukker {xmat} / hoh {(int) Hoh} mm";
        }

        public string GetHtmlString()
        {
            var xsoort = "Alu.";
            if (!string.IsNullOrEmpty(BuisSoort))
            {
                switch (BuisSoort.ToLower())
                {
                    case "a":
                        xsoort = "Alu.";
                        break;
                    case "g":
                        xsoort = "Geanod.";
                        break;
                    case "s":
                        xsoort = "Stl.";
                        break;
                }
            }

            string xmat = "xx";
            var zijdeA = "";
            var zijdeB = "";
            var lipA = "0";
            var lipB = "0";
            string xsoortlip = "";
            bool isdubbelgat = IsDubbelgat();
            bool isgedraaid = IsGedraaideLip();
            if (Info != null)
            {
                if (Info.Diameters.ContainsKey(BuisDiameter))
                    xmat = ((int) Info.Diameters[BuisDiameter]).ToString();
                if (Info.WandDiktes.ContainsKey(BuisWandDikte))
                    xmat += "x"  + Math.Round(Info.WandDiktes[BuisWandDikte], 1).ToString(CultureInfo.InvariantCulture).Replace(".",",");
                if (Info.Types.ContainsKey(OpdrukkerType))
                {
                    var xtype = Info.Types[OpdrukkerType];
                    var xvalues = xtype.Split(' ');
                    var xfirstafstand = xvalues.FirstOrDefault(x => x.ToLower().StartsWith("a="));
                    var xfirstgat = xvalues.FirstOrDefault(x => x.ToLower().StartsWith("d="));
                    var xlastafstand = xvalues.LastOrDefault(x => x.ToLower().StartsWith("a="));
                    var xlastgat = xvalues.LastOrDefault(x => x.ToLower().StartsWith("d="));

                    zijdeA = $"(Ø{xfirstgat?.Split('=').Last()} / {xfirstafstand?.ToLower()} mm)";
                    if (isdubbelgat)
                        zijdeB = $"(2xØ{xlastgat?.Split('=').Last()} / {xlastafstand?.ToLower()} mm) dgp";
                    else
                        zijdeB = $"(Ø{xlastgat?.Split('=').Last()} / {xlastafstand?.ToLower()} mm)";

                    if (isgedraaid)
                    {
                        int xend = xtype.ToLower().IndexOf("d=", StringComparison.CurrentCultureIgnoreCase);
                        if (xend > 0)
                        {
                            xsoortlip = xtype.Substring(0, xend).Trim();
                        }
                    }
                }

                if (Info.LipLengtes.ContainsKey(LengteRaamKant))
                    lipA = ((int)Info.LipLengtes[LengteRaamKant]).ToString();
                if (Info.LipLengtes.ContainsKey(LengteKlemKant))
                    lipB = ((int)Info.LipLengtes[LengteKlemKant]).ToString();
            }
            
            decimal hoekbuiging = HoekKlemKlant - HoekRaamkant;

            var xret = $"<h4 color='{Color.Navy.Name}'>" +
                       $"{xsoort} Valk-Plus opdrukker {xmat} / hoh {(int) Hoh} mm<br>" +
                       $"* zijde A - {lipA} mm lip, {(int) HoekRaamkant}° {zijdeA}<br>" +
                       $"* zijde B - {lipB} mm lip, {(int) HoekKlemKlant}° {zijdeB}<br>";
            if (hoekbuiging > 0)
                xret += $"Buiging {hoekbuiging}° tov recht, gat Ø6,5 aan zijde B van bocht";
            if (isgedraaid)
                xret += $"{(hoekbuiging > 0? "<br>" : "")}{xsoortlip}";
            xret += "</h4>";
           return xret;
        }

        public int PerUur()
        {
            var xbase = Machine == OpdrukkerMachine.Opdrukker1 ? 980 : 1400;
            decimal subtrek = 0;
            if (IsGedraaideLip())
                xbase = 500;
            if(xbase > 500)
            {
                var xextra = Hoh - 2000;
                if (xextra >= 0)
                    subtrek = ((xextra / 250) * 15);
            }

            if (HoekKlemKlant > 0)
            {
                var xm1 = ((HoekKlemKlant / 75) * 100);
                var xm2 = ((HoekKlemKlant / 75) * 100);
                subtrek += (xm1 + xm2);
            }

            xbase -= (int)subtrek;
            return xbase;
        }

        public decimal GetExtraMaat()
        {
            var xret = 0m;

            decimal xmarge = 8;
            decimal maxhoek = 125;
            var percentraam = ((HoekRaamkant / maxhoek) * 100);
            var percentklem = ((HoekKlemKlant / maxhoek) * 100);
            xret += ((xmarge / 100) * percentraam);
            xret += ((xmarge / 100) * percentklem);

            if (Info != null)
            {
                if (Info.Types.ContainsKey(OpdrukkerType))
                {
                    var xvalues = Info.Types[OpdrukkerType].Split(' ');
                    foreach (var xval in xvalues)
                    {
                        if (!xval.Contains("=")) continue;
                        if (xval.ToLower().Contains("a") || xval.ToLower().Contains("h"))
                        {
                            var xsplit = xval.Split('=');
                            if (xsplit.Length > 1 && decimal.TryParse(xsplit[1], out var xmaat))
                                xret += xmaat;
                        }
                    }
                }
            }

            if (Machine == OpdrukkerMachine.Opdrukker1)
                xret += 40;
            else xret += 20;
            return xret;
        }

        public void UpdateOpdrukkerArtikelNr(string artikelnr)
        {
            try
            {
                if (string.IsNullOrEmpty(artikelnr)) return;
                var text = artikelnr.Trim();
                if (text.Length < 4) return;
                // var value = text.Substring(text.Length - 8, 8);
                var buissoort = text.Length >= 15 ? text.Substring(0, 1) : "A";
                var diameter = text.Length >= 14 ? text.Substring(text.Length - 14, 1) : "0";
                var wanddikte = text.Length >= 13 ? text.Substring(text.Length - 13, 1) : "0";
                var buistype = text.Length >= 12 ? text.Substring(text.Length - 12, 2) : "20";
                var raamliplengte = text.Length >= 10 ? text.Substring(text.Length - 10, 1) : "0";
                var klemliplengte = text.Length >= 9 ? text.Substring(text.Length - 9, 1) : "0";
                int index = text.Length is >= 4 and <= 8 ? 0 : text.Length - 8;
                var hohtxt = (text.Length - index) >= 4 && index > -1? text.Substring(index, 4) : "0";
                var raamkant = text.Length >= 6 ? text.Substring(text.Length - 4, 2) : "0";
                var klemkant = text.Length >= 8 ? text.Substring(text.Length - 2, 2) : "0";

                BuisSoort = buissoort;
                Info ??= OpdrukkerInfo.Load();
                if (int.TryParse(diameter, out var xdiameter))
                {
                    BuisDiameter = xdiameter;
                    if (Info != null)
                        PakketAantal = Info.Aantallen[BuisDiameter];
                }

                if (int.TryParse(wanddikte, out var xwanddikte))
                {
                    BuisWandDikte = xwanddikte;
                }

                if (int.TryParse(buistype, out var xbuistype))
                {
                    OpdrukkerType = xbuistype;
                }

                if (int.TryParse(raamliplengte, out var xraamliplengte))
                {
                    LengteRaamKant = xraamliplengte;
                }

                if (int.TryParse(klemliplengte, out var xklemliplengte))
                {
                    LengteKlemKant = xklemliplengte;
                }

                if (decimal.TryParse(hohtxt, out var hoh))
                {
                    Hoh = hoh;
                }

                if (decimal.TryParse(raamkant, out var raam))
                {
                    HoekRaamkant = raam;
                }

                if (decimal.TryParse(klemkant, out var klem))
                {
                    HoekKlemKlant = klem;
                }
                Info = OpdrukkerInfo.Load()??new OpdrukkerInfo();
                KnipMaat = (int)Math.Ceiling(hoh + GetExtraMaat());
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
