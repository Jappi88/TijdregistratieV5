using Forms.MetroBase;
using Rpm.Productie;

namespace Forms
{
    public partial class BerekenLeverdatumForm : MetroBaseForm
    {
        public BerekenLeverdatumForm()
        {
            InitializeComponent();
        }

        public string ArtikelNr
        {
            get => berekenLeverdatumUI1.ArtikelNr;
            set => berekenLeverdatumUI1.ArtikelNr = value;
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        public void SetBewerking(Bewerking bewerking)
        {
            berekenLeverdatumUI1.SetProductie(bewerking);
        }
    }
}
