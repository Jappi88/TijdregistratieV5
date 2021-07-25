using ProductieManager.Misc;
using ProductieManager.Properties;
using ProductieManager.Settings;
using System.Drawing;

namespace ProductieManager.ViewModels
{
    public class SettingModel
    {
        public string Name { get { return Settings == null ? "N/A" : Settings.Username; } }

        public string GroupName
        {
            get
            {
                if (Settings == null)
                    return "Onbekend";
                if (Settings.Username.ToLower().Contains("default"))
                    return "Standaart Instellingen";
                else return "Gebruikers";
            }
        }

        public Image ItemImage
        {
            get
            {
                return IsLoaded ? Resources.industry_setting_114090.CombineImage(Resources.check_1582, 32, 32) : Resources.industry_setting_114090.ResizeImage(32, 32);
            }
        }

        public UserSettings Settings { get; set; }

        public string LoadSettings { get { return "Laad Opties"; } }

        public string ReplaceSettings { get { return "Overschrijf Opties"; } }
        public bool IsLoaded { get; set; }

        public SettingModel(UserSettings setting)
        {
            Settings = setting;
        }
    }
}