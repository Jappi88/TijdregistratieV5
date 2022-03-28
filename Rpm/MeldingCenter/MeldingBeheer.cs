using Rpm.SqlLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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
                    if (xent.ShouldShow())
                    {
                        xent.UpdateRead(true);
                        SaveMelding(xent);
                        xent.ShowMelding();
                    }
                }
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public MeldingEntry CreateMelding(string message, string title,string messageid,string messagetype, List<string> recievers, byte[] imagedata,
            bool save,bool overwrite, string action = null,string actionid = null, int actionviewindex = 0)
        {
            try
            {
                if(recievers is { Count: > 0 })
                {
                    recievers.RemoveAll(string.IsNullOrEmpty);
                }
                var melding = new MeldingEntry
                {
                    Body = message,
                    Subject = title,
                    Recievers = recievers,
                    ImageData = imagedata,
                    Action = action,
                    ActionID = actionid,
                    ActionViewIndex = actionviewindex,
                    MessageID = messageid,
                    MessageType = messagetype
                };
                if (save)
                {
                    if (!overwrite && Database.Exists(melding.ID.ToString()))
                        return melding;
                    SaveMelding(melding);
                }
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
                            melding.UpdateRead(true);
                            SaveMelding(melding);
                            melding.ShowMelding();
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
