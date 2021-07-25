using ProductieManager.Mailing;
using ProductieManager.Misc;
using ProductieManager.Productie;
using System;

namespace ProductieManager.Settings
{
    [Serializable]
    public class UserSettings
    {
        public Classes.SqlLite.UserChange LastChanged { get; set; }
        public string Username { get; set; }

        public string OSID { get; set; }

        public UserSettings()
        {
            Initdefault();
        }

        #region "Methods"

        public void Initdefault()
        {
            OSID = CpuID.ProcessorId();
            Username = $"Default[{OSID}]";
            LastChanged = new Classes.SqlLite.UserChange() { User = Username, Change = $"Optie [{Username}] Aangemaakt" };
            //werktijden
            StartWerkdag = new TimeSpan(07, 30, 00);
            EindWerkdag = new TimeSpan(16, 30, 00);
            StartPauze1 = new TimeSpan(09, 45, 00);
            DuurPauze1 = new TimeSpan(00, 15, 00);
            StartPauze2 = new TimeSpan(12, 00, 00);
            DuurPauze2 = new TimeSpan(00, 30, 00);
            StartPauze3 = new TimeSpan(14, 45, 00);
            DuurPauze3 = new TimeSpan(00, 15, 00);
            NationaleFeestdagen = new DateTime[] { };

            //Taken
            TaakVoorStart = true;
            MinVoorStart = 60;
            TaakVoorKlaarZet = true;
            MinVoorKlaarZet = 180;
            TaakVoorControle = true;
            MinVoorControle = 60;
            TaakVoorPersoneel = true;
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
            Bewerkingen = new string[] { };
            DeelInPerAfdeling = false;
            DeelInPerBewerking = false;
            DeelAllesIn = true;
            AantalPerPagina = 10;

            //admin
            PasTelaatDatumAan = false;
            VerzendAdres = new UitgaandAdres[] { };
            OntvangAdres = new InkomendAdres[] { };
            SyncInterval = 10000;
            SyncLocaties = new string[] { };
            VerwijderVerwerkteBestanden = false;
            StartNaOpstart = true;
            SluitPcAf = true;
            AfsluitTijd = new TimeSpan(16, 45, 0);
            MinimizeToTray = false;
            ToonAlleGestartProducties = false;
            //Gebruiker Info
            //Weergave
            WeergaveFilters = new ViewState[] { };
            ViewDataBewerkingen = new byte[] { };
            ViewDataProductie = new byte[] { };
        }

        public bool Save(string change = null)
        {
            if (change == null)
                change = "Saved";
            if (ViewDataBewerkingen != null && ViewDataBewerkingen.Length > 0)
            {
                ViewDataBewerkingen = ViewDataBewerkingen.Compress();
            }
            if (ViewDataProductie != null && ViewDataProductie.Length > 0)
            {
                ViewDataProductie = ViewDataProductie.Compress();
            }
            return Manager.Database.UpSert(this);
        }

        #endregion "Methods"

        #region "WerkTijden"

        public TimeSpan StartWerkdag { get; set; }
        public TimeSpan EindWerkdag { get; set; }
        public TimeSpan StartPauze1 { get; set; }
        public TimeSpan DuurPauze1 { get; set; }
        public TimeSpan StartPauze2 { get; set; }
        public TimeSpan DuurPauze2 { get; set; }
        public TimeSpan StartPauze3 { get; set; }
        public TimeSpan DuurPauze3 { get; set; }

        public DateTime[] NationaleFeestdagen { get; set; }

        //public string _StartWerkdag{get;set;}
        //public string _EindWerkdag { get; set; }
        //public string _StartPauze1 { get; set; }
        //public string _DuurPauze1 { get; set; }
        //public string _StartPauze2 { get; set; }
        //public string _DuurPauze2 { get; set; }
        //public string _StartPauze3 { get; set; }
        //public string _DuurPauze3 { get; set; }
        //public string[] _NationaleFeestdagen { get; set; }

        #endregion "WerkTijden"

        #region "Taken"

        public bool TaakVoorStart { get; set; }
        public int MinVoorStart { get; set; }
        public bool TaakVoorKlaarZet { get; set; }
        public int MinVoorKlaarZet { get; set; }
        public bool TaakVoorControle { get; set; }
        public int MinVoorControle { get; set; }
        public bool TaakAlsTelaat { get; set; }
        public bool TaakAlsGereed { get; set; }
        public bool TaakVoorPersoneel { get; set; }
        public bool GebruikTaken { get; set; }
        public bool TaakPersoneelVrij { get; set; }
        public int TaakSyncInterval { get; set; }

        #endregion "Taken"

        #region "Weergave Producties"

        public bool ToonVolgensAfdelingen { get; set; }
        public bool ToonVolgensBewerkingen { get; set; }
        public bool ToonAllesVanBeide { get; set; }
        public bool ToonAlles { get; set; }
        public string[] Afdelingen { get; set; }
        public string[] Bewerkingen { get; set; }
        public bool DeelInPerAfdeling { get; set; }
        public bool DeelInPerBewerking { get; set; }
        public bool DeelAllesIn { get; set; }
        public int AantalPerPagina { get; set; }

        #endregion "Weergave Producties"

        #region "Gebruiker"

        //weergave
        public int FilterBewerkingIndex { get; set; }

        public byte[] ViewDataProductie { get; set; }
        public byte[] ViewDataBewerkingen { get; set; }
        public ViewState[] WeergaveFilters { get; set; }

        #endregion "Gebruiker"

        #region "Admin Opties"

        public bool PasTelaatDatumAan { get; set; }
        public UitgaandAdres[] VerzendAdres { get; set; }
        public InkomendAdres[] OntvangAdres { get; set; }
        public int SyncInterval { get; set; }
        public string[] SyncLocaties { get; set; }
        public bool VerwijderVerwerkteBestanden { get; set; }
        public bool AutoLogin { get; set; }
        public bool StartNaOpstart { get; set; }
        public bool SluitPcAf { get; set; }
        public TimeSpan AfsluitTijd { get; set; }
        public bool MinimizeToTray { get; set; }
        public bool ToonAlleGestartProducties { get; set; }

        #endregion "Admin Opties"
    }
}