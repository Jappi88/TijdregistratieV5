﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;

namespace Rpm.Opmerking
{
    public class Opmerkingen
    {
        public List<OpmerkingEntry> OpmerkingenLijst { get; set; }

        public string FilePath { get; private set; }

        public event EventHandler OnOpmerkingenChanged;

        private CustomFileWatcher _watcher;

        public Opmerkingen()
        {
            OpmerkingenLijst = new List<OpmerkingEntry>();
            SetFilePath();
            _watcher = new CustomFileWatcher(Manager.DbPath + $"\\Opmerkingen", "*.rpm");
            _watcher.FileChanged += _watcher_Changed;
            _watcher.FileDeleted += _watcher_Deleted;
        }

        private void _watcher_Deleted(object sender, FileSystemEventArgs e)
        {
            lock (OpmerkingenLijst)
            {
                OpmerkingenLijst = new List<OpmerkingEntry>();
            }

            Save();
            OpmerkingenChanged();
        }

        private void _watcher_Changed(object sender, FileSystemEventArgs e)
        {
            OpmerkingenChanged();
        }

        private void _changedtimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            OpmerkingenChanged();
        }

        private void SetFilePath()
        {
            var dir = Manager.DbPath + $"\\Opmerkingen";
            if (!Directory.Exists(dir))
                Directory.CreateDirectory(dir);
            FilePath = dir + "\\Opmerkingen.rpm";
        }

        public int Remove(OpmerkingEntry entry)
        {
            if (entry == null) return -1;
            try
            {
                if (OpmerkingenLijst == null) return -1;
                return OpmerkingenLijst.RemoveAll(x =>
                    string.Equals(entry.Title, x.Title, StringComparison.CurrentCultureIgnoreCase) &&
                    string.Equals(entry.Afzender, x.Afzender, StringComparison.CurrentCultureIgnoreCase));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return -1;
            }
        }

        public Task<bool> Load()
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (!File.Exists(FilePath) && !await Save())
                        return false;
                    lock (OpmerkingenLijst)
                    {
                        OpmerkingenLijst = FilePath.DeSerialize<List<OpmerkingEntry>>();

                    }

                    if (OpmerkingenLijst == null)
                    {
                        OpmerkingenLijst = new List<OpmerkingEntry>();
                        await Save();
                    }

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            });
        }

        public Task<bool> Save()
        {
            return Task.Factory.StartNew(() =>
            {
                try
                {
                    SetFilePath();
                    lock (OpmerkingenLijst)
                    {
                        OpmerkingenLijst ??= new List<OpmerkingEntry>();
                        OpmerkingenLijst.Serialize(FilePath);
                    }

                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            });
        }

        public List<OpmerkingEntry> GetAvailibleNotes()
        {
            return OpmerkingenLijst.Where(x => x.CanRead()).ToList();
        }

        public int SetNotes(List<OpmerkingEntry> notes)
        {
            int xreturn = 0;
            lock (OpmerkingenLijst)
            {
                foreach (var note in notes)
                {
                    OpmerkingenLijst.RemoveAll(x =>
                        string.Equals(note.Title, x.Title, StringComparison.CurrentCultureIgnoreCase) && 
                        string.Equals(note.Afzender, x.Afzender, StringComparison.CurrentCultureIgnoreCase));
                    OpmerkingenLijst.Add(note);
                    xreturn++;
                }
            }

            return xreturn;
        }

        public List<OpmerkingEntry> GetUnreadNotes()
        {
            return OpmerkingenLijst.Where(x => x.CanRead() && !x.IsGelezen).ToList();
        }

        protected virtual void OpmerkingenChanged()
        {
            Load().Wait();
            OnOpmerkingenChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
