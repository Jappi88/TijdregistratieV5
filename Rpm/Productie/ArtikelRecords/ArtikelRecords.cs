using MetroFramework;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.SqlLite;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        public List<ArtikelOpmerking> LoadOpmerkingen()
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

        public bool SaveOpmerkingen(List<ArtikelOpmerking> opmerkingen)
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
                return Database?.GetEntry<ArtikelRecord>(artnr);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        private void Database_FileDeleted(object sender, FileSystemEventArgs e)
        {
            OnArtikelDeleted(e);
        }

        public void UpdateWaardes(ProductieFormulier form)
        {
            try
            {
                if (string.IsNullOrEmpty(form?.ArtikelNr))
                    return;
                if (form.State != ProductieState.Gereed) return;
                var file = Database?.GetEntry<ArtikelRecord>(form.ArtikelNr);
                if (file != null)
                {
                    if (file.UpdatedProducties.Exists(x =>
                            string.Equals(form.ProductieNr, x, StringComparison.CurrentCultureIgnoreCase)))
                        return;
                    file.Omschrijving = form.Omschrijving;
                    file.VorigeAantalGemaakt = file.AantalGemaakt;
                    file.AantalGemaakt += form.TotaalGemaakt;
                    file.VorigeTijdGewerkt = form.TijdGewerkt;
                    file.TijdGewerkt += form.TijdAanGewerkt();
                    file.LaatstGeupdate = DateTime.Now;
                    
                }
                else
                {
                    file = new ArtikelRecord()
                    {
                        AantalGemaakt = form.TotaalGemaakt,
                        ArtikelNr = form.ArtikelNr,
                        Omschrijving = form.Omschrijving,
                        TijdGewerkt = form.TijdAanGewerkt()
                    };
                }
                file.UpdatedProducties.Add(form.ProductieNr);
                Database?.Upsert(form.ArtikelNr, file, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void UpdateWaardes(WerkPlek plek)
        {
            try
            {
                if (string.IsNullOrEmpty(plek?.Naam))
                    return;
                if (plek.Werk is not {State: ProductieState.Gereed}) return;
                var file = Database.GetEntry<ArtikelRecord>(plek.Naam);
                if (file != null)
                {
                    if (file.UpdatedProducties.Exists(x =>
                            string.Equals(plek.ProductieNr, x, StringComparison.CurrentCultureIgnoreCase)))
                        return;
                    file.Omschrijving = $"{plek.Naam} Werkplek";
                    file.VorigeAantalGemaakt = file.AantalGemaakt;
                    file.AantalGemaakt += plek.TotaalGemaakt;
                    file.VorigeTijdGewerkt = plek.TijdGewerkt;
                    file.TijdGewerkt += plek.TijdAanGewerkt();
                    file.LaatstGeupdate = DateTime.Now;
                    file.IsWerkplek = true;
                }
                else
                {
                    file = new ArtikelRecord()
                    {
                        AantalGemaakt = plek.TotaalGemaakt,
                        ArtikelNr = plek.Naam,
                        Omschrijving = $"{plek.Naam} Werkplek",
                        TijdGewerkt = plek.TijdAanGewerkt(),
                        IsWerkplek = true
                    };
                }
                file.UpdatedProducties.Add(plek.ProductieNr);
                Database.Upsert(plek.Naam, file, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private bool _checking = false;

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
                    var records = Database.GetAllEntries<ArtikelRecord>(new List<string>() {"algemeen"});
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
                    // Task.Delay(7000).Wait();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                _checking = false;


            });
            if (xreturn.Count > 0)
            {
                var xopen = Application.OpenForms.Cast<Form>().Where(x =>
                    string.Equals(x.Text, "form_alert", StringComparison.CurrentCultureIgnoreCase)).ToList();
                xopen.ForEach(x =>
                {
                    x.Invoke(new MethodInvoker(() =>
                    {
                        x.Close();
                        x.Dispose();
                    }));
                });
                DoOpmerkingenRecords(xreturn);
            }
        }

        public Dictionary<ArtikelRecord, List<ArtikelOpmerking>> CheckForAlgemeenOpmerkingen(List<ArtikelRecord> records)
        {
            var xreturn = new Dictionary<ArtikelRecord, List<ArtikelOpmerking>>();
            try
            {
                var opmerkingen = LoadOpmerkingen();
                if (opmerkingen.Count > 0)
                {
                    records ??= Database.GetAllEntries<ArtikelRecord>(new List<string>() {"algemeen"});
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
            foreach (var value in values)
            {
                var record = value.Key;
                foreach (var op in value.Value)
                {
                    var bttns = new Dictionary<string, DialogResult>();
                    bttns.Add("Sluiten", DialogResult.No);
                    bttns.Add("Begrepen", DialogResult.Yes);
                    //Opmerking Tonen
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

                        if (op.IsAlgemeen)
                        {
                            var alg = LoadOpmerkingen();
                            var xindex = alg.IndexOf(op);
                            if (xindex > -1)
                            {
                                alg[xindex] = op;
                                SaveOpmerkingen(alg);
                            }
                        }
                        else
                        {
                            var alg = record.Opmerkingen;
                            var xindex = alg.IndexOf(op);
                            if (xindex > -1)
                            {
                                alg[xindex] = op;
                                Database?.Upsert(record.ArtikelNr, record, false);
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
                                    var xoldaantal = (int) (record.VorigeAantalGemaakt / op.FilterWaarde);
                                    var xnewaantal = (int)(record.AantalGemaakt / op.FilterWaarde);
                                    if (xnewaantal > xoldaantal)
                                    {
                                        //op.GelezenDoor.Remove(Manager.Opties.Username);
                                        break;
                                    }
                                    continue;
                                case ArtikelFilterSoort.TijdGewerkt:
                                    var xoldtijd = (int)(record.VorigeTijdGewerkt / (double)op.FilterWaarde);
                                    var xnewtijd = (int)(record.TijdGewerkt / (double)op.FilterWaarde);
                                    if (xnewtijd > xoldtijd)
                                    {
                                        //op.GelezenDoor.Remove(Manager.Opties.Username);
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
