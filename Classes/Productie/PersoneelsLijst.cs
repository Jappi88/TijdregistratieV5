using Polenter.Serialization;
using System;
using System.IO;
using System.Linq;

namespace ProductieManager.Productie
{
    [Serializable]
    public class PersoneelsLijst
    {
        [ExcludeFromSerialization]
        public Manager PManager { get; set; }

        private FileSystemWatcher _dbwatcher;
        public string InstanceID { get; set; }
        public Personeel[] PersoneelLeden { get; set; }

        public PersoneelsLijst()
        {
            PersoneelLeden = new Personeel[] { };
            InstanceID = Misc.CpuID.ProcessorId();
        }

        private void _dbwatcher_Changed(object sender, FileSystemEventArgs e)
        {
            if (e.Name.ToLower() == "personeelleden.db")
            {
                PersoneelsLijst lijst = new PersoneelsLijst(PManager).LoadPersoneelsLijst(PManager, false);
                if (lijst.InstanceID != InstanceID)
                {
                    PersoneelLeden = lijst.PersoneelLeden;
                    SavePersoneelsLijst(this);
                    PersoneelChanged();
                }
            }
        }

        private void InitWatcher()
        {
            string path = Manager.DbPath;
            if (Directory.Exists(path))
                Directory.CreateDirectory(path);
            _dbwatcher = new FileSystemWatcher(path, "*.db");
            _dbwatcher.EnableRaisingEvents = true;
            _dbwatcher.NotifyFilter = NotifyFilters.CreationTime | NotifyFilters.LastWrite;
            _dbwatcher.Changed += _dbwatcher_Changed;
        }

        public PersoneelsLijst(Manager manager) : this()
        {
            PManager = manager;
        }

        public void PManager_OnBewerkingChanged(Bewerking bewerking, bool updateform, bool updatedb, bool triggerdb)
        {
            if (PersoneelLeden != null && bewerking != null)
            {
                foreach (Personeel pers in bewerking.GetPersoneel())
                {
                    Personeel ex = PersoneelLeden.FirstOrDefault(x => x.PersoneelNaam.ToLower() == pers.PersoneelNaam.ToLower());

                    if (ex != null)
                    {
                        ex.Actief = true;
                        if (ex.WerktAan != null && bewerking.Equals(ex.WerktAan))
                        {
                            if (pers.Actief)
                            {
                                ex.Werkplek = pers.Werkplek;
                                ex.Gestart = pers.Gestart;
                                ex.Gestopt = pers.Gestopt;
                            }
                            else ex.WerktAan = null;
                        }
                        //else if (ex.WerktAan == null && pers.Actief)
                        // ex.WerktAan = bewerking;
                        else if (!pers.Actief && ex.WerktAan != null && bewerking.Equals(ex.WerktAan))
                            ex.WerktAan = null;
                    }
                }
            }
        }

        public PersoneelsLijst LoadPersoneelsLijst(Manager manager, bool withwatcher)
        {
            PersoneelsLijst pers = null;
            this.PManager = manager;
            string persfile = Manager.DbPath + "\\PersoneelLeden.db";
            try
            {
                if (File.Exists(persfile))
                {
                    using (FileStream fs = new FileStream(persfile, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        SharpSerializer sh = new SharpSerializer(true);
                        pers = sh.Deserialize(fs) as PersoneelsLijst;
                        PersoneelLeden = pers.PersoneelLeden;
                        fs.Close();
                        if (withwatcher)
                        {
                            InitWatcher();
                        }
                    }
                }
            }
            catch (Exception ex) { PersoneelLeden = new Personeel[] { }; }
            return this;
        }

        public static bool SavePersoneelsLijst(PersoneelsLijst lijst)
        {
            string persfile = Manager.DbPath + "\\PersoneelLeden.db";
            string tmp = Manager.TempPath + "\\" + Path.GetRandomFileName();
            if (!Directory.Exists(Manager.TempPath))
                Directory.CreateDirectory(Manager.TempPath);
            try
            {
                if (lijst == null)
                    throw new Exception();
                using (FileStream fs = new FileStream(tmp, FileMode.Create))
                {
                    SharpSerializer sh = new SharpSerializer(true);
                    sh.Serialize(lijst, fs);
                }
                if (File.Exists(persfile))
                    File.Delete(persfile);
                File.Move(tmp, persfile);
                return true;
            }
            catch { return false; }
        }

        public event PersoneelLijstHandler OnPersoneelChanged;

        public void PersoneelChanged()
        {
            OnPersoneelChanged?.Invoke(this);
        }
    }
}