using Rpm.Settings;

namespace Rpm.ViewModels
{
    public class SettingModel
    {
        public SettingModel(UserSettings setting)
        {
            Settings = setting;
        }

        public string Name => Settings == null ? "N/A" : Settings.Username;

        public string GroupName
        {
            get
            {
                if (Settings == null)
                    return "Onbekend";
                if (Settings.Username.ToLower().Contains("default"))
                    return "Standaart Instellingen";
                return "Gebruikers";
            }
        }

        public UserSettings Settings { get; set; }

        public string LoadSettings => "Laad Opties";

        public string ReplaceSettings => "Overschrijf";
        public bool IsLoaded { get; set; }
    }
}