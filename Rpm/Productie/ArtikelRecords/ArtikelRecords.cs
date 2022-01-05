using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using System.Windows.Forms;
using Org.BouncyCastle.Asn1.Cmp;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.SqlLite;
using Rpm.Various;

namespace Rpm.Productie.ArtikelRecords
{
    public class ArtikelRecords : IDisposable
    {
        public string Version = "1.0.0.0";
        public  MultipleFileDb Database { get; private set; }
        
        public bool Disposed => _disposed;

        public ArtikelRecords(string database)
        {
            var path = Path.Combine(database, "ArtikelRecords");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            Database = new MultipleFileDb(path, true,Version, DbType.ArtikelRecords);
            Database.FileChanged += Database_FileChanged;
            Database.FileDeleted += Database_FileDeleted;
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
                    file.AantalGemaakt += form.TotaalGemaakt;
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
        public void CheckForOpmerkingen()
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
                        CheckForUpmerkingen(file);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                _checking = false;
            });

        }

        public void CheckForUpmerkingen(ArtikelRecord record)
        {
            if (Manager.Opties == null) return;
            if (record?.Opmerkingen != null && record.Opmerkingen.Count > 0)
            {
                foreach (var op in record.Opmerkingen)
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
                                    if ((decimal) record.TijdGewerkt != op.FilterWaarde)
                                        continue;
                                    break;
                            }

                            break;
                        case ArtikelFilter.GelijkAanOfHoger:
                            switch (op.FilterSoort)
                            {
                                case ArtikelFilterSoort.AantalGemaakt:
                                    if (record.AantalGemaakt >= op.FilterWaarde)
                                        continue;
                                    break;
                                case ArtikelFilterSoort.TijdGewerkt:
                                    if ((decimal) record.TijdGewerkt >= op.FilterWaarde)
                                        continue;
                                    break;
                            }

                            break;
                        case ArtikelFilter.Hoger:
                            switch (op.FilterSoort)
                            {
                                case ArtikelFilterSoort.AantalGemaakt:
                                    if (record.AantalGemaakt > op.FilterWaarde)
                                        continue;
                                    break;
                                case ArtikelFilterSoort.TijdGewerkt:
                                    if ((decimal) record.TijdGewerkt > op.FilterWaarde)
                                        continue;
                                    break;
                            }

                            break;
                        case ArtikelFilter.HerhaalElke:
                            if (op.GelezenDoor.ContainsKey(Manager.Opties.Username))
                                op.GelezenDoor.Remove(Manager.Opties.Username);
                            switch (op.FilterSoort)
                            {
                                case ArtikelFilterSoort.AantalGemaakt:
                                    if ((record.AantalGemaakt % op.FilterWaarde) != 0)
                                        continue;
                                    break;
                                case ArtikelFilterSoort.TijdGewerkt:
                                    if (((decimal) record.TijdGewerkt % op.FilterWaarde) != 0)
                                        continue;
                                    break;
                            }

                            break;
                    }

                    if (op.GelezenDoor.ContainsKey(Manager.Opties.Username))
                        continue;
                    if (op.OpmerkingVoor.Any(x =>
                            string.Equals("iedereen", x, StringComparison.CurrentCultureIgnoreCase) ||
                            string.Equals(Manager.Opties.Username, x, StringComparison.CurrentCultureIgnoreCase)))
                    {
                        //Opmerking Tonen
                        if (Manager.OnRequestRespondDialog(op.Opmerking, $"Opmerking Van {op.GelezenDoor}",
                                MessageBoxButtons.OK, MessageBoxIcon.None, null, null,
                                op.ImageData?.ImageFromBytes()) == DialogResult.OK)
                        {
                            if (Manager.Opties?.Username == null)
                                return;
                            op.GelezenDoor.Add(Manager.Opties.Username, DateTime.Now);
                        }
                    }
                }
            }
        }

        private void Database_FileChanged(object sender, FileSystemEventArgs e)
        {
            CheckForOpmerkingen();
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
