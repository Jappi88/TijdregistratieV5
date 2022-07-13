using MetroFramework;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Controls;
using Rpm.Misc;
using Rpm.SqlLite;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rpm.Productie.ArtikelRecords
{
    public class ArtikelRecords : IDisposable
    {
        public string Version = "1.0.0.0";
        public  MultipleFileDb Database { get; private set; }
        public string DbPath { get; private set; }
        public bool Disposed => _disposed;

        public ArtikelRecords(string database)
        {
            DbPath = Path.Combine(database, "ArtikelRecords");
            if (!Directory.Exists(DbPath))
                Directory.CreateDirectory(DbPath);
            Database = new MultipleFileDb(DbPath, true, Version, DbType.ArtikelRecords)
            {
                MonitorCorrupted = false
            };
            Database.FileChanged += Database_FileChanged;
            Database.FileDeleted += Database_FileDeleted;
        }

        public List<ArtikelOpmerking> GetAllAlgemeenRecordsOpmerkingen()
        {
            try
            {
                var xop = new List<ArtikelOpmerking>();
                string xfile = Path.Combine(DbPath, "Algemeen.rpm");
                if (File.Exists(xfile))
                    xop = xfile.DeSerialize<List<ArtikelOpmerking>>();
                return xop;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return new List<ArtikelOpmerking>();
        }

        public bool SaveAlgemeenOpmerkingen(List<ArtikelOpmerking> opmerkingen)
        {
            try
            {
                string xfile = Path.Combine(DbPath, "Algemeen.rpm");
                return opmerkingen.Serialize(xfile);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public ArtikelRecord GetRecord(string artnr)
        {
            try
            {
                return Database?.GetEntry<ArtikelRecord>(artnr, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        public List<ArtikelRecord> GetAllRecords()
        {
            try
            {
                return Database?.GetAllEntries<ArtikelRecord>(true)??new List<ArtikelRecord>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return new List<ArtikelRecord>();
            }
        }

        public bool SaveRecord(ArtikelRecord record)
        {
            try
            {
                if (string.IsNullOrEmpty(record?.ArtikelNr))
                    return false;
                return Database?.Upsert(record.ArtikelNr, record, false) ?? false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public Task<int> SaveRecords(List<ArtikelRecord> records, ProgressArg changed = null)
        {
            return Task.Factory.StartNew(() =>
            {
                var xret = 0;
                try
                {
                    if (records == null || records.Count == 0)
                        return 0;
                    if (changed != null)
                    {
                        changed.Message = $"Updating ArtikelRecords...";
                        changed.Current = 0;
                        changed.Max = records.Count;
                        changed.OnChanged(this);
                    }

                    for (var i = 0; i < records.Count; i++)
                    {
                        var xrec = records[i];
                        if (changed != null)
                        {
                            changed.Value = xrec;
                            changed.Current = i;
                            changed.Max = records.Count;
                            changed.OnChanged(this);
                        }
                        if (SaveRecord(xrec))
                            xret++;
                        //if (!xrec.IsWerkplek)
                        //    BijlageBeheer.UpdateBijlage(xrec.ArtikelNr, Encoding.UTF8.GetBytes(GetRecordInfo(xrec)),
                        //        $"{xrec.ArtikelNr}_ProductieInfo.txt");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return xret;
            });
        }

        private void Database_FileDeleted(object sender, FileSystemEventArgs e)
        {
            OnArtikelDeleted(e);
        }

        public bool UpdateWaardes(ProductieFormulier form, ArtikelRecord record = null, bool save = true)
        {
            try
            {
                if (string.IsNullOrEmpty(form?.ArtikelNr))
                    return false;
                if (form.State != ProductieState.Gereed) return false;
                var file = record??Database?.GetEntry<ArtikelRecord>(form.ArtikelNr, false);
                if (file != null)
                {
                    if (file.UpdatedProducties.Exists(x =>
                            string.Equals(form.ProductieNr, x, StringComparison.CurrentCultureIgnoreCase)))
                        return false;
                    file.ArtikelNr = form.ArtikelNr;
                    file.Omschrijving = form.Omschrijving;
                    file.VorigeAantalGemaakt = file.AantalGemaakt;
                    file.AantalGemaakt += form.TotaalGemaakt;
                    file.VorigeTijdGewerkt = form.TijdGewerkt;
                    file.TijdGewerkt += form.TijdAanGewerkt(false);
                    file.IsWerkplek = false;
                    var sts = form.GetAlleStoringen(false);
                    sts.ForEach(x =>
                    {
                        if (!file.Onderbrekeningen.Contains(x))
                            file.Onderbrekeningen.Add(x);
                    });
                    file.UpdateStoringen();
                    if (file.LaatstGeupdate.IsDefault() || form.DatumGereed > file.LaatstGeupdate)
                        file.LaatstGeupdate = form.DatumGereed;
                    if (file.Vanaf.IsDefault() || form.TijdGestart < file.Vanaf)
                        file.Vanaf = form.TijdGestart;

                }
                else
                {
                    file = new ArtikelRecord()
                    {
                        AantalGemaakt = form.TotaalGemaakt,
                        ArtikelNr = form.ArtikelNr,
                        Omschrijving = form.Omschrijving,
                        TijdGewerkt = form.TijdAanGewerkt(false),
                        LaatstGeupdate = form.DatumGereed,
                        Vanaf = form.TijdGestart,
                        IsWerkplek = false
                    };
                }
                file.UpdatedProducties.Add(form.ProductieNr);
                if (save)
                {
                    //if (!file.IsWerkplek)
                    //    BijlageBeheer.UpdateBijlage(form.ArtikelNr, Encoding.UTF8.GetBytes(GetRecordInfo(file)),
                    //        $"{form.ArtikelNr}_ProductieInfo.txt");
                    return Database?.Upsert(form.ArtikelNr, file, false)??false;
                }

                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool UpdateWaardes(WerkPlek plek, ArtikelRecord record = null, bool save = true)
        {
            try
            {
                if (string.IsNullOrEmpty(plek?.Naam))
                    return false;
                if (plek.Werk is not { State: ProductieState.Gereed }) return false;
                var file = record ?? Database.GetEntry<ArtikelRecord>(plek.Naam, false);
                if (file != null)
                {
                    if (file.UpdatedProducties.Exists(x =>
                            string.Equals(plek.ProductieNr, x, StringComparison.CurrentCultureIgnoreCase)))
                        return false;
                    file.ArtikelNr = plek.Naam;
                    file.Omschrijving = $"{plek.Naam} Werkplek";
                    file.VorigeAantalGemaakt = file.AantalGemaakt;
                    file.AantalGemaakt += plek.TotaalGemaakt;
                    file.VorigeTijdGewerkt = plek.TijdGewerkt;
                    file.TijdGewerkt += plek.TijdAanGewerkt();
                    file.IsWerkplek = true;
                    var sts = plek.Storingen.Where(x => !string.IsNullOrEmpty(x.StoringType) && x.TotaalTijd > 0).ToList();
                    sts.ForEach(x =>
                    {
                        if (!file.Onderbrekeningen.Contains(x))
                            file.Onderbrekeningen.Add(x);
                    });
                    file.UpdateStoringen();
                    var xstop = plek.GestoptOp();
                    var xstart = plek.GestartOp();
                    if (xstop > file.LaatstGeupdate)
                        file.LaatstGeupdate = xstop;
                    if (file.Vanaf.IsDefault() || xstart < file.Vanaf)
                        file.Vanaf = xstart;
                }
                else
                {
                    file = new ArtikelRecord()
                    {
                        AantalGemaakt = plek.TotaalGemaakt,
                        ArtikelNr = plek.Naam,
                        Omschrijving = $"{plek.Naam} Werkplek",
                        TijdGewerkt = plek.TijdAanGewerkt(),
                        IsWerkplek = true,
                        Vanaf = plek.GestartOp(),
                        LaatstGeupdate = plek.GestoptOp()
                    };
                }
                file.UpdatedProducties.Add(plek.ProductieNr);
                if (save)
                    Database.Upsert(plek.Naam, file, false);
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool RemoveWaardes(WerkPlek plek, ArtikelRecord record = null, bool save = true)
        {
            try
            {
                if (string.IsNullOrEmpty(plek?.Naam))
                    return false;
                var file = record ?? Database.GetEntry<ArtikelRecord>(plek.Naam, false);
                if (file != null)
                {
                    if (!file.UpdatedProducties.Exists(x =>
                            string.Equals(plek.ProductieNr, x, StringComparison.CurrentCultureIgnoreCase)))
                        return false;
                    file.ArtikelNr = plek.Naam;
                    file.Omschrijving = $"{plek.Naam} Werkplek";
                    file.VorigeAantalGemaakt = file.AantalGemaakt;
                    file.AantalGemaakt -= plek.TotaalGemaakt;
                    file.VorigeTijdGewerkt = plek.TijdGewerkt;
                    file.TijdGewerkt -= plek.TijdAanGewerkt();
                    file.IsWerkplek = true;
                    var sts = plek.Storingen.Where(x => !string.IsNullOrEmpty(x.StoringType) && x.TotaalTijd > 0).ToList();
                    sts.ForEach(x =>
                    {
                        if (file.Onderbrekeningen.Contains(x))
                            file.Onderbrekeningen.Remove(x);
                    });
                    file.UpdateStoringen();
                    var xstop = plek.GestoptOp();
                    var xstart = plek.GestartOp();
                    if (xstop > file.LaatstGeupdate)
                        file.LaatstGeupdate = xstop;
                    if (file.Vanaf.IsDefault() || xstart < file.Vanaf)
                        file.Vanaf = xstart;
                    file.UpdatedProducties.Remove(plek.ProductieNr);
                    if (file.Opmerkingen != null && file.Opmerkingen.Count > 0)
                    {
                        foreach (var op in file.Opmerkingen)
                        {
                            op.AantalGetoond = (int)(file.AantalGemaakt / op.FilterWaarde);
                        }
                    }
                    if (save)
                        Database.Upsert(plek.Naam, file, false);
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public bool RemoveWaardes(ProductieFormulier form, ArtikelRecord record = null, bool save = true)
        {
            try
            {
                if (string.IsNullOrEmpty(form?.ArtikelNr))
                    return false;
                var file = record ?? Database?.GetEntry<ArtikelRecord>(form.ArtikelNr, false);
                if (file != null)
                {
                    if (!file.UpdatedProducties.Exists(x =>
                            string.Equals(form.ProductieNr, x, StringComparison.CurrentCultureIgnoreCase)))
                        return false;
                    file.ArtikelNr = form.ArtikelNr;
                    file.Omschrijving = form.Omschrijving;
                    file.VorigeAantalGemaakt = file.AantalGemaakt;
                    file.AantalGemaakt -= form.TotaalGemaakt;
                    file.VorigeTijdGewerkt = form.TijdGewerkt;
                    file.TijdGewerkt -= form.TijdAanGewerkt(false);
                    file.IsWerkplek = false;
                    var sts = form.GetAlleStoringen(false);
                    sts.ForEach(x =>
                    {
                        if (file.Onderbrekeningen.Contains(x))
                            file.Onderbrekeningen.Remove(x);
                    });
                    file.UpdateStoringen();
                    if (file.LaatstGeupdate.IsDefault() || form.DatumGereed > file.LaatstGeupdate)
                        file.LaatstGeupdate = form.DatumGereed;
                    if (file.Vanaf.IsDefault() || form.TijdGestart < file.Vanaf)
                        file.Vanaf = form.TijdGestart;
                    file.UpdatedProducties.Remove(form.ProductieNr);
                    if(file.Opmerkingen != null && file.Opmerkingen.Count > 0)
                    {
                        foreach(var op in file.Opmerkingen)
                        {
                            op.AantalGetoond = (int)(file.AantalGemaakt / op.FilterWaarde);
                        }
                    }
                    if (save)
                    {
                        //if (!file.IsWerkplek)
                        //    BijlageBeheer.UpdateBijlage(form.ArtikelNr, Encoding.UTF8.GetBytes(GetRecordInfo(file)),
                        //        $"{form.ArtikelNr}_ProductieInfo.txt");
                        return Database?.Upsert(form.ArtikelNr, file, false) ?? false;
                    }
                }
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        public static string GetRecordInfo(ArtikelRecord record)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                if (record == null)
                    throw new Exception("ArtikelRecord kan niet null zijn!");
                var pu = 0;
                if (record.AantalGemaakt > 0 && record.TijdGewerkt > 0)
                    pu = (int)((double)record.AantalGemaakt / record.TijdGewerkt);
                sb.AppendLine($"Product Informatie Voor {record.ArtikelNr}");
                sb.AppendLine($"{record.Omschrijving}\n");
                sb.AppendLine($"Aantal Geproduceerd: {record.UpdatedProducties.Count}");
                sb.AppendLine($"Totaal Gemaakt: {record.AantalGemaakt}");
                sb.AppendLine($"Totaal Tijd Gewerkt: {record.TijdGewerkt}");
                sb.AppendLine($"Gemiddeld Per Uur: {pu} P/u");
                sb.AppendLine($"Gemeten Vanaf: {record.Vanaf:D}");
                sb.AppendLine($"Geüpdate Op: {record.LaatstGeupdate:D}");
                sb.AppendLine($"Producties: {string.Join(", ", record.UpdatedProducties)}");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return sb.ToString();
        }

        private bool _checking;

        public async void CheckForOpmerkingen(bool includealgemeen)
        {
            if (_checking) return;
            _checking = true;
            //List<ArtikelRecord> records = new List<ArtikelRecord>();
            var xreturn = new Dictionary<ArtikelRecord, List<ArtikelOpmerking>>();
            await Task.Factory.StartNew(() =>
            {
                _checking = true;
                try
                {
                    var records = Database.GetAllEntries<ArtikelRecord>(false, new List<string>() {"algemeen"});
                    foreach (var file in records)
                    {
                        var rs = CheckForRecordOpmerkingen(file, false);
                        if (rs.Count > 0)
                            foreach (var r in rs)
                                xreturn.Add(r.Key, r.Value);
                    }

                    if (includealgemeen)
                    {
                        var rs = CheckForAlgemeenOpmerkingen(records);
                        if (rs.Count > 0)
                            foreach (var r in rs)
                                xreturn.Add(r.Key, r.Value);
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                _checking = false;


            });
            if (xreturn.Count > 0)
            {
                if (Manager.Opties is not { ToonArtikelRecordMeldingen: true }) return;
                var xopen = Application.OpenForms.OfType<Form_Alert>().ToList();
                for (int i = 0; i < xopen.Count; i++)
                {
                    var x = xopen[i];
                    try
                    {
                        x.Invoke(new MethodInvoker(() =>
                        {
                            try
                            {
                                x.Close();
                                x.Dispose();
                            }
                            catch { }
                        }));
                    }
                    catch { }
                }
                DoOpmerkingenRecords(xreturn);
            }
        }

        public Dictionary<ArtikelRecord, List<ArtikelOpmerking>> CheckForAlgemeenOpmerkingen(List<ArtikelRecord> records)
        {
            var xreturn = new Dictionary<ArtikelRecord, List<ArtikelOpmerking>>();
            try
            {
                var opmerkingen = GetAllAlgemeenRecordsOpmerkingen();
                if (opmerkingen.Count > 0)
                {
                    records ??= Database.GetAllEntries<ArtikelRecord>(false,new List<string>() {"algemeen"});
                    foreach (var file in records)
                    {
                        var rs = CheckForOpmerkingen(file, opmerkingen,true);
                        if(rs.Count > 0)
                            foreach (var r in rs)
                                xreturn.Add(r.Key, r.Value);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xreturn;
        }

        public void DoOpmerkingenRecords(Dictionary<ArtikelRecord, List<ArtikelOpmerking>> values)
        {
            if (Manager.Opties is not { ToonArtikelRecordMeldingen: true }) return;
            foreach (var value in values)
            {
                var record = value.Key;
                foreach (var op in value.Value)
                {
                    var bttns = new Dictionary<string, DialogResult>();
                    bttns.Add("Sluiten", DialogResult.No);
                    bttns.Add("Begrepen", DialogResult.Yes);
                    //Opmerking Tonen
                    if (Manager.Opties != null && Manager.Opties.SpeelMeldingToonAf)
                    {
                        Task.Factory.StartNew(() =>
                        {
                            using var soundPlayer = new SoundPlayer(Resources.mixkit_happy_bells_notification_937);
                            soundPlayer.Play();
                        });
                    }
                    if (Manager.OnRequestRespondDialog(record.GetOpmerking(op), record.GetTitle(op),
                            MessageBoxButtons.OK, MessageBoxIcon.Information, null, bttns,
                            op.ImageData?.ImageFromBytes() ?? Resources.default_opmerking_16757_256x256,
                            MetroColorStyle.Purple) == DialogResult.Yes)
                    {
                        if (Manager.Opties?.Username == null)
                            return;
                        if (op.GelezenDoor.ContainsKey(Manager.Opties.Username))
                        {
                            op.GelezenDoor[Manager.Opties.Username] = DateTime.Now;
                        }
                        else
                            op.GelezenDoor.Add(Manager.Opties.Username, DateTime.Now);
                        op.AantalGetoond = (int)(record.AantalGemaakt / op.FilterWaarde);
                        if (op.IsAlgemeen)
                        {
                            var alg = GetAllAlgemeenRecordsOpmerkingen();
                            var xindex = alg.IndexOf(op);
                            if (xindex > -1)
                            {
                                alg[xindex] = op;
                                SaveAlgemeenOpmerkingen(alg);
                            }
                        }
                        else
                        {
                            var alg = record.Opmerkingen;
                            var xindex = alg.IndexOf(op);
                            if (xindex > -1)
                            {
                                alg[xindex] = op;
                                Database?.Upsert(record.ArtikelNr, record, false,false);
                            }

                        }
                    }
                }
            }
        }

        public Dictionary<ArtikelRecord, List<ArtikelOpmerking>> CheckForOpmerkingen(ArtikelRecord record, List<ArtikelOpmerking> opmerkingen, bool isalgemeen)
        {
            var xreturn = new Dictionary<ArtikelRecord, List<ArtikelOpmerking>>();
            try
            {
                if (record == null) return xreturn;
                foreach (var op in opmerkingen)
                {
                    switch (op.FilterOp)
                    {
                        case FilterOp.Artikelen:
                            if (record.IsWerkplek) continue;
                            break;
                        case FilterOp.Werkplaatsen:
                            if (!record.IsWerkplek) continue;
                            break;
                    }
                    switch (op.Filter)
                    {
                        case ArtikelFilter.GelijkAan:
                            switch (op.FilterSoort)
                            {
                                case ArtikelFilterSoort.AantalGemaakt:
                                    if (record.AantalGemaakt != op.FilterWaarde)
                                        continue;
                                    break;
                                case ArtikelFilterSoort.TijdGewerkt:
                                    if ((decimal)record.TijdGewerkt != op.FilterWaarde)
                                        continue;
                                    break;
                            }

                            break;
                        case ArtikelFilter.GelijkAanOfHoger:
                            switch (op.FilterSoort)
                            {
                                case ArtikelFilterSoort.AantalGemaakt:
                                    if (record.AantalGemaakt >= op.FilterWaarde)
                                        break;
                                    continue;
                                case ArtikelFilterSoort.TijdGewerkt:
                                    if ((decimal) record.TijdGewerkt >= op.FilterWaarde)
                                        break;
                                    continue;
                            }

                            break;
                        case ArtikelFilter.Hoger:
                            switch (op.FilterSoort)
                            {
                                case ArtikelFilterSoort.AantalGemaakt:
                                    if (record.AantalGemaakt > op.FilterWaarde)
                                        break;
                                    continue;
                                case ArtikelFilterSoort.TijdGewerkt:
                                    if ((decimal)record.TijdGewerkt > op.FilterWaarde)
                                     break;
                                    continue;
                            }

                            break;
                        case ArtikelFilter.HerhaalElke:
                            switch (op.FilterSoort)
                            {
                                case ArtikelFilterSoort.AantalGemaakt:
                                    var xnewaantal = (int)(record.AantalGemaakt / op.FilterWaarde);
                                    if (xnewaantal > op.AantalGetoond)
                                    {
                                        op.GelezenDoor.Clear();
                                        break;
                                    }
                                    continue;
                                case ArtikelFilterSoort.TijdGewerkt:
                                    //var xoldtijd = (int)(record.VorigeTijdGewerkt / (double)op.FilterWaarde);
                                    var xnewtijd = (int)(record.TijdGewerkt / (double)op.FilterWaarde);
                                    if (xnewtijd > op.AantalGetoond)
                                    {
                                        op.AantalGetoond = xnewtijd;
                                        op.GelezenDoor.Clear();
                                        break;
                                    }
                                    continue;
                            }

                            break;
                    }

                    if (op.GelezenDoor.ContainsKey(Manager.Opties.Username))
                    {
                        if (op.GelezenDoor[Manager.Opties.Username] >= record.LaatstGeupdate)
                            continue;
                    }
                    if (op.OpmerkingVoor.Any(x =>
                            string.Equals("iedereen", x, StringComparison.CurrentCultureIgnoreCase) ||
                            string.Equals(Manager.Opties.Username, x, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        if (xreturn.ContainsKey(record))
                            xreturn[record].Add(op);
                        else xreturn.Add(record, new List<ArtikelOpmerking>() {op});
                        
                    }
                }

                
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return xreturn;
        }

        public Dictionary<ArtikelRecord, List<ArtikelOpmerking>> CheckForRecordOpmerkingen(ArtikelRecord record, bool isalgemeen)
        {
            var xreturn = new Dictionary<ArtikelRecord, List<ArtikelOpmerking>>();
            if (Manager.Opties == null) return xreturn;
            if (record?.Opmerkingen != null && record.Opmerkingen.Count > 0)
            {
                var rs = CheckForOpmerkingen(record, record.Opmerkingen,isalgemeen);
                if (rs.Count > 0)
                    foreach (var r in rs)
                        xreturn.Add(r.Key, r.Value);
            }

            return xreturn;
        }

        private void Database_FileChanged(object sender, FileSystemEventArgs e)
        {
            CheckForOpmerkingen(true);
            OnArtikelChanged(e);
        }

        public event FileSystemEventHandler ArtikelChanged;
        public event FileSystemEventHandler ArtikelDeleted;

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            Database?.Close();
            Database = null;
            _disposed = true;
        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        protected virtual void OnArtikelChanged(FileSystemEventArgs f)
        {
            ArtikelChanged?.Invoke(this, f);
        }

        protected virtual void OnArtikelDeleted(FileSystemEventArgs f)
        {
            ArtikelDeleted?.Invoke(this, f);
        }
    }
}
