using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiveCharts;
using LiveCharts.Defaults;
using NPOI.HSSF.EventUserModel;
using Rpm.Productie;

namespace ProductieManager.Forms.Charting
{
    internal class ChartAantalSerie
    {
        public WerkPlek Plek { get; private set; }
        public ChartValues<ObservableValue> Values { get; set; } = new ChartValues<ObservableValue>();

        public bool UpdateGemaakt(WerkPlek plek, ref List<string> label)
        {
            try
            {
                
                if (plek?.Werk == null) return false;
                Plek = plek;
                var aantallen = plek.AantalHistory.Aantallen.ToArray();
                if (aantallen.Length < Values.Count)
                    Values.RemoveAt(aantallen.Length);
                for (int i = 0; i < aantallen.Length; i++)
                {
                    var aantal = 0;
                    var record = aantallen[i];
                    double tijd = 0;
                    if (!record.IsActive)
                    {
                        aantal = record.GetGemaakt();
                    }
                    else
                        aantal = plek.GetActueelAantalGemaakt(ref tijd);

                    if (Values.Count <= i)
                        Values.Add(new ObservableValue(aantal));
                    else
                        Values[i].Value = aantal;
                    label.Add(record.GetGestopt().ToString(""));
                }

                //int index = 0;

                //while (start < stop)
                //{
                //    double tijd = 0;
                //    var aantal = plek.GetAantalGemaakt(start, start.Add(TimeSpan.FromHours(1)), ref tijd, true);
                //    start = start.Add(TimeSpan.FromHours(1));
                //    if (Values.Count <= index)
                //        Values.Add(new ObservableValue(aantal));
                //    else 
                //        Values[index].Value = aantal;
                //    index++;
                //}

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return false;
        }
    }
}
