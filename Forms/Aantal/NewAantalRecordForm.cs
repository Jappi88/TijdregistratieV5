using Rpm.Misc;
using Rpm.Productie.AantalHistory;
using System.Windows.Forms;

namespace Forms.Aantal
{
    public partial class NewAantalRecordForm : Forms.MetroBase.MetroBaseForm
    {
        public NewAantalRecordForm()
        {
            InitializeComponent();
        }

        public NewAantalRecordForm(AantalRecord record):this()
        {
            if (record != null)
            {
                xfirstvalue.SetValue(record.Aantal);
                xsecondvalue.SetValue(record.LastAantal);
                xstartdatum.SetValue(record.DateChanged);
                xstopdatum.SetValue(record.GetGestopt());
            }

            SelectedRecord = record;
            xok.Text = "Wijzigen";
        }

        public AantalRecord SelectedRecord { get; private set; }

        private void button1_Click(object sender, System.EventArgs e)
        {
            SelectedRecord ??= new AantalRecord();
            SelectedRecord._endDate = xstopdatum.Value;
            SelectedRecord.Aantal = (int) xfirstvalue.Value;
            SelectedRecord.DateChanged = xstartdatum.Value;
            SelectedRecord.LastAantal = (int) xsecondvalue.Value;
            DialogResult = DialogResult.OK;
        }

        private void xfirstvalue_ValueChanged(object sender, System.EventArgs e)
        {
            xgemaaktlabel.Text = (xsecondvalue.Value - xfirstvalue.Value).ToString("##.###");
        }
    }
}
