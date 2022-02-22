using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using Controls;
using Polenter.Serialization;
using ProductieManager.Properties;
using ProductieManager.Rpm.Mailing;
using ProductieManager.Rpm.Misc;
using ProductieManager.Rpm.Settings;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.SqlLite;
using Rpm.Various;

namespace Rpm.Settings
{
    [Serializable]
    public class UserSettings
    {
        public UserSettings()
        {
            Initdefault();
        }

        public string BackgroundImagePath { get; set; }
        public string LastPreviewVersion { get; set; } = "1.0.0.0";
        public string AutoLoginUsername { get; set; }
        public UserChange LastChanged { get; set; }
        public string Username { get; set; }
        public string BoundUsername { get; set; }

        [ExcludeFromSerialization] public string OSID { get; set; }

        public string SystemID { get; set; }

        //[ExcludeFromSerialization]
        public string MainDbPath { get; set; }

        public DatabaseUpdateEntry MainDB { get; set; }
        public DatabaseUpdateEntry TempMainDB { get; set; }
        public List<string> PersoneelIndeling { get; set; }
        public List<string> WerkplaatsIndeling { get; set; }
        public int TileCountRefreshRate { get; set; } = 30000;
        public int TileViewBackgroundColorRGB { get; set; } = Color.White.ToArgb();
        public List<TileInfoEntry> TileLayout { get; set; }
        public FlowDirection TileFlowDirection { get; set; } = FlowDirection.TopDown;
        public bool PreviewShown { get; set; }

        #region "Methods"

        public List<TileInfoEntry> GetAllDefaultEntries(bool incextra)
        {
            var xtiles = new List<TileInfoEntry>();
            try
            {
                xtiles.Add(new TileInfoEntry
                {
                    Text = "Producties",
                    ForeColor = Color.Purple,
                    GroupName = "Producties",
                    Name = "Producties",
                    Size = new Size(256, 96),
                    TileColor = Color.FromArgb(215, 174, 255),
                    TileImage = Resources.operation,
                    TileIndex = 0,
                    IsDefault = true
                });
                xtiles.Add(new TileInfoEntry
                {
                    Text = "Actieve Werkplaatsen",
                    ForeColor = Color.Navy,
                    GroupName = "Werkplaatsen",
                    Name = "Werkplaatsen",
                    Size = new Size(256, 96),
                    TileColor = Color.FromArgb(164, 209, 255),
                    TileImage = Resources.iconfinder_technology,
                    TileIndex = 1,
                    IsDefault = true
                });
                xtiles.Add(new TileInfoEntry
                {
                    Text = "Recente Gereedmeldingen",
                    ForeColor = Color.White,
                    GroupName = "Gereedmeldingen",
                    Name = "Gereedmeldingen",
                    Size = new Size(256, 96),
                    TileColor = Color.Green,
                    TileImage = new Bitmap(Resources.operation.CombineImage(Resources.check_1582, 1.75)),
                    TileIndex = 2,
                    IsDefault = true
                });
                xtiles.Add(new TileInfoEntry
                {
                    Text = "Alle Onderbrekeningen",
                    ForeColor = Color.White,
                    GroupName = "Onderbrekingen",
                    Name = "Onderbrekingen",
                    Size = new Size(256, 96),
                    TileColor = Color.SteelBlue,
                    TileImage = Resources.onderhoud128_128,
                    TileIndex = 3,
                    IsDefault = true
                });
                xtiles.Add(new TileInfoEntry
                {
                    Text = "Bereken Verbruik",
                    ForeColor = Color.White,
                    GroupName = "Verbruik",
                    Name = "xverbruik",
                    Size = new Size(256, 96),
                    TileColor = Color.RosyBrown,
                    TileImage = Resources.geometry_measure_96x96,
                    TileIndex = 4,
                    IsDefault = true
                });

                xtiles.Add(new TileInfoEntry
                {
                    Text = "Artikelen Verbruik",
                    ForeColor = Color.Brown,
                    GroupName = "Verbruik",
                    Name = "xverbruikbeheren",
                    Size = new Size(256, 96),
                    TileColor = Color.FromArgb(223, 191, 191),
                    TileImage = Resources.geometry_measure_96x96,
                    TileIndex = 5,
                    IsDefault = true
                });
                xtiles.Add(new TileInfoEntry
                {
                    Text = "Wijzig AantalGemaakt",
                    ForeColor = Color.White,
                    GroupName = "AantalGemaakt",
                    Name = "xchangeaantal",
                    Size = new Size(256, 96),
                    TileColor = Color.FromArgb(245, 122, 135),
                    TileImage = Resources.Count_tool_34564__1_,
                    TileIndex = 6,
                    IsDefault = true,
                    AccesLevel = AccesType.ProductieBasis
                });
                xtiles.Add(new TileInfoEntry
                {
                    Text = "Zoek WerkTekening",
                    ForeColor = Color.White,
                    GroupName = "Werktekening",
                    Name = "xsearchtekening",
                    Size = new Size(256, 96),
                    TileColor = Color.DarkGoldenrod,
                    TileImage = new Bitmap(
                        Resources.libreoffice_draw_icon_128x128.CombineImage(Resources.search_locate_find_6278, 1.75)),
                    TileIndex = 7,
                    IsDefault = true
                });

                xtiles.Add(new TileInfoEntry
                {
                    Text = "Personeel Indeling",
                    ForeColor = Color.White,
                    GroupName = "Indeling",
                    Name = "xpersoneelindeling",
                    Size = new Size(256, 96),
                    TileColor = Color.MediumSlateBlue,
                    TileImage = Resources.user_rotation_96x96,
                    TileIndex = 8,
                    IsDefault = true,
                    AccesLevel = AccesType.ProductieBasis
                });

                xtiles.Add(new TileInfoEntry
                {
                    Text = "Werkplaats Indeling",
                    ForeColor = IProductieBase.GetProductSoortColor("horti"),
                    GroupName = "Indeling",
                    Name = "xwerkplaatsindeling",
                    Size = new Size(256, 96),
                    TileColor = Color.AliceBlue,
                    TileImage = Resources.werkplaatsindeling_96x96,
                    TileIndex = 9,
                    IsDefault = true,
                    AccesLevel = AccesType.ProductieBasis
                });

                xtiles.Add(new TileInfoEntry
                {
                    Text = "Exporteer Excel",
                    ForeColor = Color.DarkGreen,
                    GroupName = "Excel",
                    Name = "xcreateexcel",
                    Size = new Size(256, 96),
                    TileColor = Color.FromArgb(215, 255, 215),
                    TileImage = Resources.microsoft_excel_22733_128x128,
                    TileIndex = 10,
                    IsDefault = true
                });

                xtiles.Add(new TileInfoEntry
                {
                    Text = "Toon Statistieken",
                    ForeColor = Color.SteelBlue,
                    GroupName = "Statistieken",
                    Name = "xstats",
                    Size = new Size(256, 96),
                    TileColor = Color.Pink,
                    TextFontStyle = FontStyle.Bold,
                    TileImage = Resources.Charts_96x96,
                    TileIndex = 11,
                    IsDefault = true
                });

                xtiles.Add(new TileInfoEntry
                {
                    Text = "Zoek Producties",
                    ForeColor = Color.White,
                    GroupName = "Zoeken",
                    Name = "xzoekproducties",
                    Size = new Size(256, 96),
                    TileColor = Color.SlateBlue,
                    TileImage = Resources.FocusEye_img_128_128,
                    TileIndex = 12,
                    IsDefault = true
                });

                xtiles.Add(new TileInfoEntry
                {
                    Text = "Personeel",
                    ForeColor = Color.Navy,
                    GroupName = "Personeel",
                    Name = "xpersoneel",
                    Size = new Size(256, 96),
                    TileColor = Color.FromArgb(125, 175, 255),
                    TileImage = Resources.users_clients_group_16774,
                    TileIndex = 13,
                    IsDefault = true
                });

                xtiles.Add(new TileInfoEntry
                {
                    Text = "Alle Artikelen",
                    ForeColor = Color.White,
                    GroupName = "Artikelen",
                    Name = "xalleartikelen",
                    Size = new Size(256, 96),
                    TileColor = Color.FromArgb(95, 95, 95),
                    TileImage = Resources.product_document_file_96x96,
                    TileIndex = 14,
                    IsDefault = true
                });

                xtiles.Add(new TileInfoEntry
                {
                    Text = "Artikel/Werkplaats Records",
                    ForeColor = Color.FromArgb(253, 185, 253),
                    GroupName = "Artikelen",
                    Name = "xartikelrecords",
                    Size = new Size(256, 96),
                    TileColor = Color.FromArgb(152, 5, 152),
                    TileImage = Resources.time_management_tasks_96x96,
                    TileIndex = 15,
                    IsDefault = true
                });

                xtiles.Add(new TileInfoEntry
                {
                    Text = "Productie Volgorde",
                    ForeColor = Color.White,
                    GroupName = "Producties",
                    Name = "xproductievolgorde",
                    Size = new Size(256, 96),
                    TileColor = Color.Navy,
                    TileImage = Resources.taskboardflat_106022,
                    TileIndex = 16,
                    IsDefault = true
                });

                xtiles.Add(new TileInfoEntry
                {
                    Text = "Klachten",
                    ForeColor = Color.White,
                    GroupName = "Klachten",
                    Name = "xklachten",
                    Size = new Size(256, 96),
                    TileColor = IProductieBase.GetProductSoortColor("red"),
                    TextFontStyle = FontStyle.Bold,
                    TileImage = Resources.Leave_80_icon_icons_com_57305_128x128,
                    TileIndex = 17,
                    IsDefault = true
                });

                xtiles.Add(new TileInfoEntry
                {
                    Text = "Excel WeekOverzicht",
                    ForeColor = Color.White,
                    GroupName = "Excel",
                    Name = "xweekoverzicht",
                    Size = new Size(256, 96),
                    TileColor = IProductieBase.GetProductSoortColor("Horti"),
                    TileImage = Resources.microsoft_excel_22733_128x128,
                    TileIndex = 18,
                    IsDefault = true
                });
                xtiles.Add(new TileInfoEntry
                {
                    Text = "Alle Notities",
                    ForeColor = Color.White,
                    GroupName = "Noties",
                    Name = "xallenotities",
                    Size = new Size(256, 96),
                    TileColor = Color.DarkRed,
                    TileImage = Resources.memo_pad_notes_reminder_task_icon_128x128,
                    TileIndex = 19,
                    IsDefault = true
                });
                xtiles.Add(new TileInfoEntry
                {
                    Text = "Beheer Filters",
                    ForeColor = Color.White,
                    GroupName = "Beheer",
                    Name = "xbeheerfilters",
                    Size = new Size(256, 96),
                    TileColor = Color.LightSkyBlue,
                    TileImage = Resources.filter_96x96,
                    TileIndex = 19,
                    IsDefault = true
                });
                xtiles.Add(new TileInfoEntry
                {
                    Text = "Productie Chat",
                    ForeColor = Color.Navy,
                    GroupName = "Beheer",
                    Name = "xchat",
                    Size = new Size(256, 96),
                    TileColor = Color.LightSteelBlue,
                    TileImage = Resources.chat_26_icon_96x96,
                    TileIndex = 19,
                    IsDefault = true
                });

                if (incextra)
                    if (Filters.Count > 0)
                        foreach (var f in Filters)
                        {
                            if (f.IsTempFilter)
                                continue;
                            xtiles.Add(new TileInfoEntry
                            {
                                Text = f.Name,
                                ForeColor = Color.White,
                                TileColor = Color.CadetBlue,
                                GroupName = "Filter",
                                Name = f.Name,
                                LinkID = f.ID,
                                Size = new Size(256, 96),
                                TileImage = new Bitmap(Resources.operation.CombineImage(Resources.filter_32x32, 2.5)),
                                TileIndex = xtiles.Count,
                                IsDefault = true
                            });
                        }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xtiles;
        }

        public void Initdefault()
        {
            SystemID = Manager.SystemId;
            NieuwTijd = 4.0;
            Username = "Default";
            BoundUsername = "ihab";
            LastChanged = new UserChange {User = Username, Change = $"Optie [{Username}] Aangemaakt"};
            //werktijden
            SetWerkRooster(Rooster.StandaartRooster());
            NationaleFeestdagen = new DateTime[] { };
            SpecialeRoosters = new List<Rooster>();
            PersoneelIndeling = new List<string>();
            WerkplaatsIndeling = new List<string>();
            TileLayout = GetAllDefaultEntries(false);
            //create default Tiles
            //voor de producties

            //Taken
            ToonLijstNaNieuweTaak = true;
            TaakVoorStart = true;
            MinVoorStart = 60;
            TaakVoorKlaarZet = true;
            MinVoorKlaarZet = 180;
            TaakVoorControle = true;
            MinVoorControle = 60;
            TaakVoorPersoneel = true;
            TaakVoorOnderbreking = true;
            TaakAlsGereed = true;
            TaakAlsTelaat = true;
            TaakPersoneelVrij = true;
            GebruikTaken = true;
            TaakSyncInterval = 10000;
            //weergave producties
            ToonVolgensAfdelingen = false;
            ToonVolgensBewerkingen = false;
            ToonAlles = true;
            Afdelingen = new string[] { };
            Filters = new List<Filter>();
            Bewerkingen = new string[] { };
            DeelInPerAfdeling = false;
            DeelInPerBewerking = false;
            DeelAllesIn = true;
            AantalPerPagina = 10;
            ToegelatenProductieCrit = new List<string>();

            //database
            UpdateDatabaseVersion = "1.0.0.0";
            CreateWeekOverzichten = false;
            DoCurrentWeekOverzicht = false;
            var xset = ExcelSettings.CreateSettings("ExcelColumns", true);
            WeekOverzichtPath = Manager.WeekOverzichtPath;
            WeekOverzichtUpdateInterval = 5 * 60000; //5min
            VanafWeek = 6;
            VanafJaar = 2021;
            DbUpdateEntries = new List<DatabaseUpdateEntry>();
            DbUpdateInterval = 1; //minuten
            Notities = new List<NotitieEntry>();
            CreateBackup = true;
            BackupInterval = TimeSpan.FromHours(1).TotalMilliseconds;
            MaxBackupCount = 100;
            OfflineDabaseTypes = new List<DbType>();
            OfflineDbSyncInterval = 1000;
            GebruikOfflineMetSync = true;
            OfflineDabaseTypes.Add(DbType.Producties);
            OfflineDabaseTypes.Add(DbType.GereedProducties);
            OfflineDabaseTypes.Add(DbType.Medewerkers);
            OfflineDabaseTypes.Add(DbType.Opmerkingen);
            OfflineDabaseTypes.Add(DbType.Opties);
            OfflineDabaseTypes.Add(DbType.Messages);
            OfflineDabaseTypes.Add(DbType.Accounts);
            OfflineDabaseTypes.Add(DbType.ArtikelRecords);
            OfflineDabaseTypes.Add(DbType.Klachten);
            OfflineDabaseTypes.Add(DbType.SpoorOverzicht);
            //admin
            EmailClients = new List<EmailClient>();
            VerzendAdres = new List<UitgaandAdres>();
            OntvangAdres = new List<InkomendAdres>();
            SyncInterval = 10000;
            GereedSyncInterval = 300000;
            ProductieLijstSyncInterval = 300000; //5 min
            AutoProductieLijstSync = true;
            AutoGereedSync = true;
            MailSyncInterval = 60000;
            SyncLocaties = new[] {Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)};
            VerwijderVerwerkteBestanden = false;
            StartNaOpstart = false;
            SluitPcAf = false;
            AfsluitTijd = new TimeSpan(16, 45, 0);
            MinimizeToTray = false;
            ToonAlleGestartProducties = false;
            ToonProductieLogs = false;
            GebruikLocalSync = true;
            LicenseGelezen = false;
            ToonLogNotificatie = true;
            ToonProductieNaToevoegen = true;
            //Gebruiker Info
            //Weergave
            PersoneelAfdelingFilter = "";
            ProductieWeergaveFilters = new ViewState[] { };
            _viewwerkplekdata = new byte[] { };
            _viewvaarddata = new byte[] { };
            _viewstoringdata = new byte[] { };
            _viewpersoneeldata = new byte[] { };
            _viewallenotitiesdata = new byte[] { };
            LastFormInfo = new Dictionary<string, LastFormScreenInfo>();
        }

        public Task<bool> Save(string change = null, bool init = false, bool triggersaved = false,
            bool showmessage = true)
        {
            return Task.Run(async () =>
            {
                if (Manager.Database?.AllSettings == null) return false;
                change ??= $"[{Username}] Instellingen Opgeslagen";
                if (await Manager.Database.UpSert(this, change, showmessage))
                {
                    if (triggersaved)
                        Manager.SettingsChanged(this, init);
                    return true;
                }

                return false;
            });
        }

        public static UserSettings GetDefaultSettings()
        {
            UserSettings xreturn = null;
            var defaultpath = Application.StartupPath + "\\DefaultSettings.db";
            if (File.Exists(defaultpath))
                for (var i = 0; i < 10; i++)
                {
                    try
                    {
                        using var fs = new FileStream(defaultpath, FileMode.Open, FileAccess.ReadWrite,
                            FileShare.Read);

                        var str = new SharpSerializer();
                        xreturn = str.Deserialize(fs) as UserSettings;
                        fs.Close();
                        break;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }

                    Application.DoEvents();
                }

            xreturn ??= Manager.Opties?.CreateCopy();
            if (xreturn != null)
                xreturn.Username = "Default Settings";
            return xreturn ?? new UserSettings();
        }

        public bool SaveAsDefault()
        {
            var username = Username;
            var xreturn = false;
            try
            {
                Username = "Default Settings";
                var defaultpath = AppDomain.CurrentDomain.BaseDirectory + "\\DefaultSettings.db";
                using var fs = new FileStream(defaultpath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read);
                var str = new SharpSerializer();
                str.Serialize(this, fs);
                fs.Close();
                xreturn = true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                xreturn = false;
            }

            Username = username;
            return xreturn;
        }

        #endregion "Methods"

        #region "WerkTijden"

        private Rooster _huidigerooster;

        public Rooster WerkRooster
        {
            get => _huidigerooster;
            set => _huidigerooster = value;
        }

        public Rooster GetWerkRooster()
        {
            if (TijdelijkeRooster != null && TijdelijkeRooster.IsValid())
                return TijdelijkeRooster.CreateCopy();
            return _huidigerooster.CreateCopy();
        }

        public void SetWerkRooster(Rooster rooster)
        {
            _huidigerooster = rooster;
        }

        public Rooster TijdelijkeRooster { get; set; }
        public DateTime[] NationaleFeestdagen { get; set; }
        public List<Rooster> SpecialeRoosters { get; set; }

        #endregion "WerkTijden"

        #region "Taken"

        public bool ToonLijstNaNieuweTaak { get; set; }
        public bool TaakVoorStart { get; set; }
        public int MinVoorStart { get; set; }
        public bool TaakVoorKlaarZet { get; set; }
        public int MinVoorKlaarZet { get; set; }
        public bool TaakVoorControle { get; set; }
        public int MinVoorControle { get; set; }
        public bool TaakAlsTelaat { get; set; }
        public bool TaakAlsGereed { get; set; }
        public bool TaakVoorPersoneel { get; set; }
        public bool TaakVoorOnderbreking { get; set; }
        public bool GebruikTaken { get; set; }
        public bool TaakPersoneelVrij { get; set; }
        public int TaakSyncInterval { get; set; }

        #endregion "Taken"

        #region "Weergave Producties"

        public double NieuwTijd { get; set; }
        public bool ToonVolgensAfdelingen { get; set; }
        public bool ToonVolgensBewerkingen { get; set; }
        public bool ToonAllesVanBeide { get; set; }
        public bool ToonAlles { get; set; }
        public string[] Afdelingen { get; set; }
        private string[] _bewerkingen;

        public string[] Bewerkingen
        {
            get => _bewerkingen;
            set => _bewerkingen = value;
        }

        public List<Filter> Filters { get; set; }
        public bool DeelInPerAfdeling { get; set; }
        public bool DeelInPerBewerking { get; set; }
        public bool DeelAllesIn { get; set; }
        public int AantalPerPagina { get; set; }
        public List<string> ToegelatenProductieCrit { get; set; }
        public bool ToonPersoneelIndelingNaOpstart { get; set; }
        public bool ToonWerkplaatsIndelingNaOpstart { get; set; }

        #endregion "Weergave Producties"

        #region "Gebruiker"

        public bool ShowDaylyMessage { get; set; } = true;

        //weergave
        public string PersoneelAfdelingFilter { get; set; }
        public byte[] _viewwerkplekdata { get; set; }
        public byte[] _viewvaarddata { get; set; }
        public byte[] _viewstoringdata { get; set; }
        public byte[] _viewpersoneeldata { get; set; }
        public byte[] _viewallenotitiesdata { get; set; }
        public bool UseLastGereedStart { get; set; }
        public DateTime LastGereedStart { get; set; }
        public bool UseLastGereedStop { get; set; }
        public DateTime LastGereedStop { get; set; }
        public Dictionary<string, LastFormScreenInfo> LastFormInfo { get; set; }

        [ExcludeFromSerialization]
        public byte[] ViewDataWerkplekState
        {
            get => _viewwerkplekdata?.DeCompress();
            set => _viewwerkplekdata = value?.Compress();
        }

        [ExcludeFromSerialization]
        public byte[] ViewDataVaardighedenState
        {
            get => _viewvaarddata?.DeCompress();
            set => _viewvaarddata = value?.Compress();
        }

        [ExcludeFromSerialization]
        public byte[] ViewDataStoringenState
        {
            get => _viewstoringdata?.DeCompress();
            set => _viewstoringdata = value?.Compress();
        }

        [ExcludeFromSerialization]
        public byte[] ViewDataPersoneelState
        {
            get => _viewpersoneeldata?.DeCompress();
            set => _viewpersoneeldata = value?.Compress();
        }

        [ExcludeFromSerialization]
        public byte[] AlleNotitiesState
        {
            get => _viewallenotitiesdata?.DeCompress();
            set => _viewallenotitiesdata = value?.Compress();
        }

        public ViewState[] ProductieWeergaveFilters { get; set; }
        public ViewState[] BewerkingWeergaveFilters { get; set; }

        #endregion "Gebruiker"

        #region Database

        public string UpdateDatabaseVersion { get; set; }
        public string WeekOverzichtPath { get; set; }
        public bool CreateWeekOverzichten { get; set; }
        public bool DoCurrentWeekOverzicht { get; set; }
        public int WeekOverzichtUpdateInterval { get; set; }

        [ExcludeFromSerialization] public List<ExcelSettings> ExcelColumns { get; set; }

        public int VanafWeek { get; set; }
        public int VanafJaar { get; set; }
        public bool GebruikOfflineMetSync { get; set; }
        public int OfflineDbSyncInterval { get; set; }
        public List<DbType> OfflineDabaseTypes { get; set; }
        public List<DatabaseUpdateEntry> DbUpdateEntries { get; set; }
        public int DbUpdateInterval { get; set; }
        public double BackupInterval { get; set; }
        public bool CreateBackup { get; set; }
        public int MaxBackupCount { get; set; }
        public List<NotitieEntry> Notities { get; set; }

        #endregion Database

        #region "Admin Opties"

        public List<EmailClient> EmailClients { get; set; }
        public List<UitgaandAdres> VerzendAdres { get; set; }
        public List<InkomendAdres> OntvangAdres { get; set; }
        public int SyncInterval { get; set; }
        public int GereedSyncInterval { get; set; }
        public bool AutoGereedSync { get; set; }
        public bool AutoProductieLijstSync { get; set; }
        public int ProductieLijstSyncInterval { get; set; }
        public int MailSyncInterval { get; set; }
        public string[] SyncLocaties { get; set; }
        public bool VerwijderVerwerkteBestanden { get; set; }
        public bool StartNaOpstart { get; set; }
        public bool SluitPcAf { get; set; }
        public TimeSpan AfsluitTijd { get; set; }
        public bool MinimizeToTray { get; set; }
        public bool ToonAlleGestartProducties { get; set; }
        public bool ToonProductieNaToevoegen { get; set; }
        public bool ToonProductieLogs { get; set; }
        public bool GebruikLocalSync { get; set; }
        public bool LicenseGelezen { get; set; }
        public bool ToonLogNotificatie { get; set; }

        #endregion "Admin Opties"
    }
}