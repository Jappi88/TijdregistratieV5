using ProductieManager.Rpm.Misc;
using Rpm.SqlLite;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.ViewModels;

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
            Database = new MultipleFileDb(DbPath, true,Version, DbType.ArtikelRecords);
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
                var file = Database.GetEntry<ArtikelRecord>(form.ArtikelNr);
                if (file != null)
                {
                    if (file.UpdatedProducties.Exists(x =>
                            string.Equals(form.ProductieNr, x, StringComparison.CurrentCultureIgnoreCase)))
                        return;
                    file.Omschrijving = form.Omschrijving;
                    file.VorigeAantalGemaakt = form.AantalGemaakt;
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
                Database.Upsert(form.ArtikelNr, file, false);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private bool _checking = false;
        public void CheckForOpmerkingen(bool includealgemeen)
        {
            if (_checking) return;
            _checking = true;
            //List<ArtikelRecord> records = new List<ArtikelRecord>();
            Task.Factory.StartNew(() =>
            {
                _checking = true;
                try
                {
                    var records = Database.GetAllEntries<ArtikelRecord>();
                    foreach (var file in records)
                    {
                        CheckForRecordOpmerkingen(file, false);
                    }

                    if (includealgemeen)
                        CheckForAlgemeenOpmerkingen(records);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                _checking = false;
            });

        }

        public void xCheckForAlgemeenOpmerkingen(List<ArtikelRecord> records)
        {
            if (_checking) return;
            _checking = true;
            //List<ArtikelRecord> records = new List<ArtikelRecord>();
            Task.Factory.StartNew(() =>
            {
                _checking = true;
                try
                {
                    records ??= Database.GetAllEntries<ArtikelRecord>();
                    CheckForAlgemeenOpmerkingen(records);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                _checking = false;
            });

        }

        public void CheckForAlgemeenOpmerkingen(List<ArtikelRecord> records)
        {

            try
            {
                var opmerkingen = LoadOpmerkingen();
                if (opmerkingen.Count > 0)
                {
                    records ??= Database.GetAllEntries<ArtikelRecord>();
                    foreach (var file in records)
                    {
                        CheckForOpmerkingen(file, opmerkingen,true);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

        }

        public void CheckForOpmerkingen(ArtikelRecord record, List<ArtikelOpmerking> opmerkingen, bool isalgemeen)
        {
            try
            {
                foreach (var op in opmerkingen)
                {
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
                                    if (((record.AantalGemaakt - record.VorigeAantalGemaakt) / op.FilterWaarde) < 1)
                                        continue;
                                    break;
                                case ArtikelFilterSoort.TijdGewerkt:
                                    if (((decimal)(record.TijdGewerkt - record.VorigeTijdGewerkt) / op.FilterWaarde) < 1)
                                        continue;
                                    break;
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
                        //Opmerking Tonen
                        if (Manager.OnRequestRespondDialog(record.GetOpmerking(op), $"Opmerking Van {op.GeplaatstDoor}",
                                MessageBoxButtons.OK, MessageBoxIcon.Information, null, null,
                                op.ImageData?.ImageFromBytes()??Resources.default_opmerking_16757_256x256, MetroColorStyle.Purple) == DialogResult.OK)
                        {
                            if (Manager.Opties?.Username == null)
                                return;
                            if (op.GelezenDoor.ContainsKey(Manager.Opties.Username))
                            {
                                op.GelezenDoor[Manager.Opties.Username] = DateTime.Now;
                            }
                            else
                                op.GelezenDoor.Add(Manager.Opties.Username, DateTime.Now);
                            if (isalgemeen)
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
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public void CheckForRecordOpmerkingen(ArtikelRecord record, bool isalgemeen)
        {
            if (Manager.Opties == null) return;
            if (record?.Opmerkingen != null && record.Opmerkingen.Count > 0)
            {
                CheckForOpmerkingen(record, record.Opmerkingen,isalgemeen);
            }
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
