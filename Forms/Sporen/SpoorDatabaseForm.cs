using System.Collections.Generic;
using BrightIdeasSoftware;
using Forms.MetroBase;
using Rpm.MateriaalSoort;

namespace Forms.Sporen
{
    public partial class SpoorDatabaseForm : MetroBaseForm
    {
        public SpoorDatabaseForm(OpdrukkerInfo info)
        {
            InitializeComponent();
            Info = info;
            xdiameters.SetObjects(info.Diameters);
        }

        public OpdrukkerInfo Info { get; set; }

        private void xdiameters_CellEditFinished(object sender, CellEditEventArgs e)
        {
            if (Info == null) return;
            if (e.RowObject is KeyValuePair<int, decimal> item)
                if (decimal.TryParse(e.NewValue.ToString(), out var value))
                {
                    Info.Diameters[item.Key] = value;
                    // var xdic = new Dictionary<int, decimal>();
                    var pair = new KeyValuePair<int, decimal>(item.Key, value);
//xdic.Add(pair);
                    xdiameters.AddObject(pair);
                    xdiameters.RemoveObject(item);
                }
        }
    }
}