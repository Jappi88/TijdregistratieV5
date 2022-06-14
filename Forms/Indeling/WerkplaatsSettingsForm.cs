using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;

namespace Forms
{
    public partial class WerkplaatsSettingsForm : MetroBase.MetroBaseForm
    {
        public WerkplaatsSettings Settings = new WerkplaatsSettings();
        public WerkplaatsSettingsForm()
        {
            InitializeComponent();
        }

        public WerkplaatsSettingsForm(WerkplaatsSettings settings):this()
        {
            if(settings != null)
                Settings = settings.CreateCopy();
            this.Title = $"{settings.Name} Instellingen";
        }

        private void LoadSettings()
        {
            Settings ??= new WerkplaatsSettings();
            roosterUI1.SetRooster(Settings.WerkRooster, Manager.Opties?.NationaleFeestdagen, Settings.SpecialeRoosters);
            filterEntryEditorUI1.LoadFilterEntries(typeof(IProductieBase), Settings.Voorwaardes, false);
        }

        private void WerkplaatsSettingsForm_Shown(object sender, System.EventArgs e)
        {
            LoadSettings();
        }

        private void xopslaan_Click(object sender, System.EventArgs e)
        {
            Settings ??= new WerkplaatsSettings();
            Settings.Voorwaardes ??= new Filter() {Name = $"{Settings.Name} Voorwaardes" };
            Settings.Voorwaardes.Filters = filterEntryEditorUI1.Criterias;
            Settings.WerkRooster = roosterUI1.WerkRooster;
            Settings.SpecialeRoosters = roosterUI1.SpecialeRoosters;
        }
    }
}
