using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Caching;
using System.Web.Security;
using Org.BouncyCastle.Crypto;
using Polenter.Serialization.Core;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.SqlLite;

namespace ProductieManager.Rpm.SqlLite
{
    public class FileDeletionManager
    {
        public FileInfo RemoteFileInfo { get; private set; }
        public FileInfo LocalFileInfo { get; private set; }
        //public List<FileDeletionEntry> RemoteDeletedFiles { get; private set; }
        //public List<FileDeletionEntry> LocalDeletedFiles { get; private set; }
        private FileSystemWatcher _localWatcher;
        private FileSystemWatcher _remoteWatcher;

        public FileDeletionManager()
        {
           
        }

        private bool isloading = false;
        public async void Load()
        {
            if (isloading) return;
            isloading = true;
           
            var remotefiles = await GetRemoteDeletedFileEntries();
            var localfiles = await GetLocalDeletedFileEntries();
            //_remoteWatcher?.Dispose();
            //_localWatcher?.Dispose();
            bool changed = false;
            if (remotefiles != null && localfiles != null)
            {
                var xadd = localfiles.Where(x =>
                    x.DeletedOn > RemoteFileInfo.LastWriteTime ||
                    !remotefiles.Any(s => s.IsDeleted(x.Path))).ToList();
                changed = xadd.Count > 0;
                foreach (var a in xadd)
                {
                    remotefiles.RemoveAll(x => x.IsDeleted(a.Path));
                    remotefiles.Add(a);
                }

                xadd = remotefiles.Where(x =>
                    x.DeletedOn > LocalFileInfo.LastWriteTime ||
                    !localfiles.Any(s => s.IsDeleted(x.Path))).ToList();
                changed |= xadd.Count > 0;
                foreach (var a in xadd)
                {
                    localfiles.RemoveAll(x => x.IsDeleted(a.Path));
                    localfiles.Add(a);
                }
            }
            else
            {
                if (localfiles == null && remotefiles != null)
                {
                    changed = true;
                    localfiles = remotefiles;
                }
                else if (localfiles != null)
                {
                    changed = true;
                    remotefiles = localfiles;
                }
            }

            //lock (this)
            //{
            //    RemoteDeletedFiles = remotefiles;
            //    LocalDeletedFiles = localfiles;
            //}

            if (changed)
                //await Save();
            //if (remotefiles != null)
            //{
            //    _remoteWatcher = new FileSystemWatcher(Manager.DefaultSettings.MainDB.UpdatePath);
            //    _remoteWatcher.Filter = "*FileBin.rpm";
            //    _remoteWatcher.EnableRaisingEvents = true;
            //    _remoteWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            //    _remoteWatcher.Changed += _remoteWatcher_Changed;
            //}

            //if (localfiles != null)
            //{

            //    _localWatcher = new FileSystemWatcher(Manager.DefaultSettings.TempMainDB.UpdatePath);
            //    _localWatcher.Filter = "*FileBin.rpm";
            //    _localWatcher.EnableRaisingEvents = true;
            //    _localWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName;
            //    _localWatcher.Changed += _localWatcher_Changed;
            //}

            isloading = false;
        }

        private void _localWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Load();
        }

        private void _remoteWatcher_Changed(object sender, FileSystemEventArgs e)
        {
            Load();
        }

        public Task<List<FileDeletionEntry>> GetLocalDeletedFileEntries()
        {
            return Task.Factory.StartNew(() =>
            {
                var xreturn = new List<FileDeletionEntry>();
                try
                {
                    if (Manager.DefaultSettings?.TempMainDB == null || !Directory.Exists(Manager.DefaultSettings.TempMainDB.UpdatePath))
                        return null;
                    string fpath = Manager.DefaultSettings.TempMainDB.UpdatePath + "\\FileBin.rpm";
                    if (!File.Exists(fpath)) return xreturn;
                    LocalFileInfo = new(fpath);
                    xreturn = fpath.DeSerialize<List<FileDeletionEntry>>();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return xreturn;
            });
        }

        //public Task<bool> Save(List<FileDeletionEntry> items)
        //{
        //    return Task.Run(() =>
        //    {
        //        try
        //        {
        //            bool xreturn = false;
        //            if (Manager.DefaultSettings?.TempMainDB != null &&
        //                Directory.Exists(Manager.DefaultSettings.TempMainDB.UpdatePath))
        //            {
        //                string fpath = Manager.DefaultSettings.TempMainDB.UpdatePath + "\\FileBin.rpm";
        //                    xreturn = items.Serialize(fpath);
        //            }
        //            if (Manager.DefaultSettings?.MainDB != null &&
        //                Directory.Exists(Manager.DefaultSettings.MainDB.UpdatePath))
        //            {
        //                string fpath = Manager.DefaultSettings.MainDB.UpdatePath + "\\FileBin.rpm";
        //                    xreturn |= (RemoteDeletedFiles = (RemoteDeletedFiles ?? new List<FileDeletionEntry>()))
        //                        .Serialize(fpath);
        //            }
        //            return xreturn;
        //        }
        //        catch (Exception e)
        //        {
        //            Console.WriteLine(e);
        //            return false;
        //        }
        //    });
        //}

        public Task<bool> UpdateLocalFileDeletionEntry(FileDeletionEntry fileentry)
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (Manager.DefaultSettings?.TempMainDB == null ||
                        !Directory.Exists(Manager.DefaultSettings?.TempMainDB.UpdatePath))
                        return false;
                    bool xreturn = false;
                    var local = await GetLocalDeletedFileEntries();
                    if (local == null)
                        local = new List<FileDeletionEntry>();
                    else
                        local.RemoveAll(x => x.IsDeleted(fileentry.Path));
                    local.Add(fileentry);
                    string fpath = Manager.DefaultSettings.TempMainDB.UpdatePath + "\\FileBin.rpm";

                    xreturn = local.Serialize(fpath);
                    return xreturn;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            });
        }

        public async void UpdateDeletePath(string path)
        {
            if (IsLocalPath(path))
                await UpdateLocalFileDeletionEntry(new(path));
            else
                await UpdateRemoteFileDeletionEntry(new(path));
        }

        public Task<bool> UpdateRemoteFileDeletionEntry(FileDeletionEntry fileentry)
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (Manager.DefaultSettings?.MainDB == null ||
                        !Directory.Exists(Manager.DefaultSettings?.MainDB.UpdatePath))
                        return false;
                    bool xreturn = false;
                    var xremote = await GetRemoteDeletedFileEntries();
                    if (xremote == null)
                        xremote = new List<FileDeletionEntry>();
                    else
                        xremote.RemoveAll(x => x.IsDeleted(fileentry.Path));
                    xremote.Add(fileentry);
                    string fpath = Manager.DefaultSettings.MainDB.UpdatePath + "\\FileBin.rpm";

                    xreturn = xremote.Serialize(fpath);

                    return xreturn;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            });
        }

        public async Task<bool> IsDeleted(string path)
        {
            try
            {
                var xlocalval = (await GetLocalDeletedFileEntries())?.FirstOrDefault(x => x.IsDeleted(path));
                bool islocal = IsLocalPath(path);
                if (xlocalval != null && islocal) return true;
                var xremotexval = (await GetRemoteDeletedFileEntries())?.FirstOrDefault(x => x.IsDeleted(path));
                if (xlocalval != null && xremotexval != null)
                    return true;
                if (xremotexval != null && !islocal)
                    return true;
                return false;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public async Task<bool> UndoDeletion(string path)
        {
            try
            {
                bool xreturn = false;
                int xremoved = 0;
                var local = await GetLocalDeletedFileEntries();
                if (local != null)
                {
                    xremoved = local?.RemoveAll(x => x.IsDeleted(path)) ?? 0;

                    if (xremoved > 0)
                    {
                        if (Manager.DefaultSettings?.TempMainDB != null &&
                            Directory.Exists(Manager.DefaultSettings.TempMainDB.UpdatePath))
                        {

                            string fpath = Manager.DefaultSettings.TempMainDB.UpdatePath + "\\FileBin.rpm";
                            xreturn = local.Serialize(fpath);
                        }
                    }
                }

                var xremote = await GetRemoteDeletedFileEntries();
                if (xremote != null)
                {

                    xremoved = xremote?.RemoveAll(x => x.IsDeleted(path)) ?? 0;

                    if (xremoved > 0)
                    {
                        if (Manager.DefaultSettings?.MainDB != null &&
                            Directory.Exists(Manager.DefaultSettings.MainDB.UpdatePath))
                        {

                            string fpath = Manager.DefaultSettings.MainDB.UpdatePath + "\\FileBin.rpm";
                            xreturn |= xremote.Serialize(fpath);
                        }
                    }
                }



                return xreturn;

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
        }

        private bool IsLocalPath(string path)
        {
            var xlocalpath = Manager.DefaultSettings?.TempMainDB?.UpdatePath?.ToLower();
            if (xlocalpath == null) return false;
            return path.ToLower().StartsWith(xlocalpath);
        }

        public  Task<List<FileDeletionEntry>> GetRemoteDeletedFileEntries()
        {
            return Task.Factory.StartNew(() =>
            {
                var xreturn = new List<FileDeletionEntry>();
                try
                {
                    if (Manager.DefaultSettings?.MainDB == null || !Directory.Exists(Manager.DefaultSettings.MainDB.UpdatePath))
                        return xreturn;
                    string fpath = Manager.DefaultSettings.MainDB.UpdatePath + "\\FileBin.rpm";
                    if (!File.Exists(fpath)) return xreturn;
                    RemoteFileInfo = new FileInfo(fpath);
                    xreturn = fpath.DeSerialize<List<FileDeletionEntry>>();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return xreturn;
            });
        }

    }
}
