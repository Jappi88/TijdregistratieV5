using ProductieManager.Properties;
using Rpm.Productie;
using System.Drawing;
using System.Windows.Forms;

namespace Controls
{
    public partial class WerkPlekInfoUI : UserControl
    {
        public string Werkplek { get; private set; }
        public Bewerking Huidig { get;private set;  }
        public Bewerking Volgende { get; private set; } 
        public WerkPlekInfoUI()
        {
            InitializeComponent();
        }

        private string GetWerkplekOmschrijving(WerkPlek plek)
        {
            return $"[{plek.ProductieNr}|{plek.ArtikelNr}] {plek.WerkNaam} van {plek.Omschrijving}.";
        }

        private string GetWerkplekOmschrijving(string plek, Bewerking werk)
        {
            return $"[{werk.ProductieNr}|{werk.ArtikelNr}] {werk.Naam} van {werk.Omschrijving}.";
        }

        public void Init(WerkPlek bezig, Bewerking volgende)
        {
            groupBox1.Text = bezig.Naam;
            Werkplek = bezig.Naam;
            Huidig = bezig.Werk;
            Volgende = volgende;
            xhuidigpic.Image = bezig.Werk.State == Rpm.Various.ProductieState.Gestart ? 
                Resources.play_button_icon_icons_com_60615 : Resources.stop_red256_24890;
            if(bezig != null)
            {
                xmeebezig.Text = $"<b>NU OP {bezig.Naam}</b>: {GetWerkplekOmschrijving(bezig)}";
            }
            if(volgende != null)
            {
                xvolgende.Text = $"<b>VOLGENDE OP {bezig.Naam}</b>: {GetWerkplekOmschrijving(bezig.Naam,volgende)}";
            }
        }

        public void Init(string werkplek,Bewerking huidig, Bewerking volgende)
        {
            groupBox1.Text = werkplek;
            Werkplek = werkplek;
            Huidig = huidig;
            Volgende = volgende;
            xhuidigpic.Image = huidig != null ?
               Resources.play_button_icon_icons_com_60615 : Resources.stop_red256_24890;
            if (huidig == null)
            {
                xmeebezig.Text = $"<b>{werkplek} is niet bezig!</b>";
            }
            else xmeebezig.Text = $"<b>NU OP {werkplek}</b>: {GetWerkplekOmschrijving(werkplek,huidig)}";
            if (volgende != null)
                xvolgende.Text = $"<b>VOLGENDE OP {werkplek}</b>: {GetWerkplekOmschrijving(werkplek, volgende)}";
            else xvolgende.Text = $"<b>Er is geen productie beschikbaar voor {werkplek}.</b>";
        }

        private void xmeebezig_MouseEnter(object sender, System.EventArgs e)
        {
            if (sender is Control c)
                c.BackColor = Color.AliceBlue;
        }

        private void xmeebezig_MouseLeave(object sender, System.EventArgs e)
        {
            if (sender is Control c)
                c.BackColor = Color.White;
        }

        private void xmeebezig_DoubleClick(object sender, System.EventArgs e)
        {
            if (Huidig != null)
                Manager.FormulierActie(new object[] { Huidig.Parent, Huidig }, Rpm.Various.MainAktie.OpenProductie);
        }

        private void xvolgende_DoubleClick(object sender, System.EventArgs e)
        {
            if (Volgende != null)
                Manager.FormulierActie(new object[] { Volgende.Parent, Volgende }, Rpm.Various.MainAktie.OpenProductie);
        }
    }
}
