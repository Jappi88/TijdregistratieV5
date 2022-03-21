using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Rpm.Productie;
using Rpm.SqlLite;

namespace Rpm.MeldingCenter
{
    public class MeldingBeheer : IDisposable
    {
        public MultipleFileDb Database { get; private set; }

        public bool IsDisposed => _disposed;

        public MeldingBeheer(string root)
        {
            Database = new MultipleFileDb(root, true, "1.0.0.0", DbType.MeldingCenter);
            Database.FileChanged += Database_FileChanged;
        }

        private void Database_FileChanged(object sender, System.IO.FileSystemEventArgs e)
        {
            try
            {
                string name = Path.GetFileNameWithoutExtension(e.FullPath);
                var xent = Database.GetEntry<MeldingEntry>(name, false);
                if (xent != null)
                {
                    if (xent.ShouldShow() && xent.ShowMelding())
                    {
                        var xold = Database.GetEntry<MeldingEntry>(name, false);
                        if (xold != null)
                        {
                            xent.ReadUsers.AddRange(xold.Recievers.Where(x =>
                                !xent.ReadUsers.Any(r =>
                                    string.Equals(r, x, StringComparison.CurrentCultureIgnoreCase))));
                        }
                        SaveMelding(xent);
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public MeldingEntry CreateMelding(string message, string title, List<string> recievers, byte[] imagedata,
            bool save)
        {
            try
            {
                var melding = new MeldingEntry
                {
                    Body = message,
                    Subject = title,
                    Recievers = recievers,
                    ImageData = imagedata
                };
                if (save)
                    SaveMelding(melding);
                return melding;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public bool SaveMelding(MeldingEntry melding)
        {
            try
            {
                if (melding == null ) return false;
                if (Database is not { CanRead: true })
                    return false;
                return Database.Upsert(melding.ID.ToString(), melding, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public Task CheckForMeldingen(int interval)
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    if (interval > 0)
                        Thread.Sleep(interval);
                    if (Database is not {CanRead: true})
                        return;
                    var xmeldingen = Database.GetAllEntries<MeldingEntry>(false).Where(x => x.ShouldShow())
                        .ToList();
                    if (xmeldingen.Count > 0)
                        foreach (var melding in xmeldingen)
                        {
                            if (melding.ShowMelding())
                                SaveMelding(melding);
                        }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            });

        }

        public void Dispose()
        {
            // Dispose of unmanaged resources.
            Dispose(true);
            // Suppress finalization.
            GC.SuppressFinalize(this);
        }

        private bool _disposed;
        protected virtual void Dispose(bool disposing)
        {
            if (_disposed)
            {
                return;
            }

            if (disposing)
            {
                // _FileChangedNotifyTimer?.Dispose();
                Database?.Dispose();
            }
            _disposed = true;
        }
    }
}
