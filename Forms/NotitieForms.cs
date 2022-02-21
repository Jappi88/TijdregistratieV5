using System;
using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Productie;
using Rpm.Various;

namespace Forms
{
    public partial class NotitieForms : MetroBaseForm
    {
        public NotitieForms()
        {
            InitializeComponent();
            Notitie = new NotitieEntry("", NotitieType.Algemeen);
            SetTitle();
        }

        public NotitieForms(NotitieEntry note) : this()
        {
            Notitie = note ?? new NotitieEntry("", NotitieType.Algemeen);
            xnaam.Text = Notitie.Naam;
            xnotitie.Text = Notitie.Notitie;
            SetTitle();
        }

        public NotitieForms(NotitieEntry note, ProductieFormulier prod) : this()
        {
            Notitie = note ?? new NotitieEntry("", prod);
            xnaam.Text = Notitie.Naam;
            xnotitie.Text = Notitie.Notitie;
            SetTitle();
        }

        public NotitieForms(NotitieEntry note, Bewerking bew) : this()
        {
            Notitie = note ?? new NotitieEntry("", bew);
            xnaam.Text = Notitie.Naam;
            xnotitie.Text = Notitie.Notitie;
            SetTitle();
        }

        public NotitieForms(NotitieEntry note, WerkPlek plek) : this()
        {
            Notitie = note ?? new NotitieEntry("", plek);
            xnaam.Text = Notitie.Naam;
            xnotitie.Text = Notitie.Notitie;
            SetTitle();
        }

        public NotitieEntry Notitie { get; set; }


        public string Title
        {
            get => Text;
            set => Text = value;
        }

        private void SetTitle()
        {
            if (Notitie == null)
                Text = "Nieuwe Notitie";
            else
                Text = $"{Enum.GetName(typeof(NotitieType), Notitie.Type)} Notitie voor {Notitie.Path}";
        }

        private void xokb_Click(object sender, EventArgs e)
        {
            Notitie.Naam = xnaam.Text.Trim();
            Notitie.Notitie = xnotitie.Text.Trim();
            DialogResult = DialogResult.OK;
        }

        private void xcancelb_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}