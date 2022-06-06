using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using Polenter.Serialization;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Timer = System.Timers.Timer;

namespace Rpm.SqlLite
{
    public class BackupInfo
    {
        public string DbVersion { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedFileName { get; set; }
        public int MaxBackupCount { get; set; }
        public DateTime Created { get; set; }
        public bool IsCreating { get; set; }
        public DateTime Creating { get; set; }
        public List<string> ExcludeNames { get; set; } = new List<string>();
        [ExcludeFromSerialization]
        public bool IsSyncing { get; private set; }
        public CancellationTokenSource CancellationToken;

        internal Timer _BackupSyncTimer;
        public double _interval { get; set; } = TimeSpan.FromHours(1).TotalMilliseconds;

        [ExcludeFromSerialization]
        public double BackupInterval
        {
            get => _interval;
            set
            {
                _interval = value;
                if (_BackupSyncTimer != null)
                {
                    try
                    {
                        _BackupSyncTimer.Interval = value;
                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }

        public BackupInfo()
        {
            DbVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        public void StartBackupSyncer()
        {

            if (IsSyncing)
            {
                return;
            }
            IsSyncing = true;
            _BackupSyncTimer?.Stop();
            _BackupSyncTimer?.Dispose();
            _BackupSyncTimer = new Timer(BackupInterval);
            _BackupSyncTimer.Elapsed += _BackupSyncTimer_Elapsed;
            _BackupSyncTimer.Start();
        }

        private async void _BackupSyncTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                IsSyncing = true;
                _BackupSyncTimer?.Stop();
                if (Manager.Opties is { CreateBackup: true })
                {
                    var bki = BackupInfo.Load();
                    if (Manager.BackupInfo != null)
                    {
                        lock (Manager.BackupInfo)
                        {
                            Manager.BackupInfo.ExcludeNames = bki.ExcludeNames;
                            Manager.BackupInfo.BackupInterval = bki.BackupInterval;
                            Manager.BackupInfo.MaxBackupCount = bki.MaxBackupCount;
                        }
                    }

                    if (!bki.IsCreating || (bki.IsCreating && (DateTime.Now - bki.Creating).TotalHours > 1))
                    {
                        if ((DateTime.Now - bki.Created).TotalHours >
                            TimeSpan.FromMilliseconds(bki.BackupInterval).TotalHours)
                        {
                            bki.IsCreating = false;
                            await CreateBackup(bki);
                        }
                    }

                    _BackupSyncTimer?.Start();
                }

            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

            IsSyncing = false;
        }

        public List<string> GetExcludeNames()
        {
            var xret = new List<string>();
            try
            {
                if (ExcludeNames == null) return xret;
                for (int i = 0; i < ExcludeNames.Count; i++)
                {
                    var xname = ExcludeNames[i];
                    if (Enum.TryParse(xname, true, out DbType type))
                    {
                        switch (type)
                        {
                            case DbType.Producties:
                                xret.Add("SqlDatabase");
                                break;
                            case DbType.Opmerkingen:
                                xret.Add("Opmerkingen");
                                break;
                            case DbType.Medewerkers:
                                xret.Add("PersoneelDb");
                                break;
                            case DbType.GereedProducties:
                                xret.Add("GereedDb");
                                break;
                            case DbType.Opties:
                                xret.Add("SettingDb");
                                break;
                            case DbType.Accounts:
                                xret.Add("AccountsDb");
                                break;
                            case DbType.Logs:
                                xret.Add("LogDb");
                                break;
                            case DbType.Versions:
                                xret.Add("VersionDb");
                                break;
                            case DbType.Messages:
                                xret.Add("Chat");
                                break;
                            case DbType.Klachten:
                                xret.Add("Klachten");
                                break;
                            case DbType.Verpakkingen:
                                xret.Add("Verpakking");
                                break;
                            case DbType.ArtikelRecords:
                                xret.Add("ArtikelRecords");
                                break;
                            case DbType.SpoorOverzicht:
                                xret.Add("Sporen");
                                break;
                            case DbType.LijstLayouts:
                                xret.Add("LijstLayouts");
                                break;
                            case DbType.MeldingCenter:
                                xret.Add("Meldingen");
                                break;
                            case DbType.Bijlages:
                                xret.Add("Bijlages");
                                break;
                            case DbType.ProductieFormulieren:
                                xret.Add("Productie Formulieren");
                                break;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xret;
        }

        public static Task CreateBackup(BackupInfo info)
        {
          
            return Task.Run(async () =>
            {
                if(info == null) return;
                info.CancellationToken ??= new CancellationTokenSource();
                if (info.IsCreating) return;
                try
                {
                    info.Creating = DateTime.Now;
                    info.IsCreating = true;
                    info.Save();
                    string path = Manager.BackupPath + $"\\RPM_Backup_{DateTime.Now: ddMMHHmmss}.zip";
                    bool valid = await ZipFileDirectory(Manager.DbPath, path, info.GetExcludeNames(), info.CancellationToken);
                    //zip.Close();
                    info.IsCreating = false;
                    if (valid && !info.CancellationToken.IsCancellationRequested)
                    {
                        info.CreatedBy = Manager.Opties?.Username;
                        info.Created = DateTime.Now;
                        info.CreatedFileName = path;

                        if (Manager.Opties != null)
                        {
                            var backups = Directory.GetFiles(Manager.BackupPath, "*.zip", SearchOption.TopDirectoryOnly)
                                .OrderBy(x => new FileInfo(x).CreationTime).ToList();
                            if (backups.Count > info.MaxBackupCount)
                            {
                                try
                                {
                                    int xc = backups.Count - info.MaxBackupCount;
                                    for (int i = 0; i < xc; i++)
                                        File.Delete(backups[i]);
                                }
                                catch
                                {
                                }
                            }
                        }
                    }
                    else
                    {
                        try
                        {
                            File.Delete(path);
                        }
                        catch
                        {
                        }
                    }
                    info.Save();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }

        public static Task<bool> CreateBackup(BackupInfo info, string filename)
        {

            return Task.Run(() =>
            {
                if (info == null) return false;
                info.CancellationToken ??= new CancellationTokenSource();
                if (info.IsCreating) return false;
                try
                {
                    info.IsCreating = true;
                    string path = filename;
                    bool valid = xZipFileDirectory(Manager.DbPath, path, info.GetExcludeNames(), info.CancellationToken);
                    //zip.Close();
                    info.IsCreating = false;
                    return valid;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }
            });
        }

        public bool Save()
        {
            return this.Serialize(Manager.BackupPath + "\\BackupInfo.rpm");
        }

        public static BackupInfo Load()
        {
            return (Manager.BackupPath + "\\BackupInfo.rpm").DeSerialize<BackupInfo>() ?? new BackupInfo();
        }

        /// <summary>
        /// Compress multi-level directories
        /// </summary>
        /// <param name="strDirectory">The directory.</param>
        /// <param name="zipedFile">The ziped file.</param>
        /// <param name="cancellation">A token for possible cancellation</param>
        public static Task<bool> ZipFileDirectory(string strDirectory, string zipedFile,List<string> exclude,
            CancellationTokenSource cancellation = null)
        {
            return Task.Run(async () =>
            {
                using FileStream ZipFile = File.Create(zipedFile);
                using ZipOutputStream s = new ZipOutputStream(ZipFile);
                return await ZipSetp(strDirectory, s, "", exclude, cancellation);
            });

        }

        public static bool xZipFileDirectory(string strDirectory, string zipedFile, List<string> exclude,
    CancellationTokenSource cancellation = null)
        {
            using FileStream ZipFile = File.Create(zipedFile);
            using ZipOutputStream s = new ZipOutputStream(ZipFile);
            return xZipSetp(strDirectory, s, "", exclude, cancellation);

        }

        /// <summary>
        /// Recursive traversal directory
        /// </summary>
        /// <param name="strDirectory">The directory.</param>
        /// <param name="s">The ZipOutputStream Object.</param>
        /// <param name="parentPath">The parent path.</param>
        /// <param name="cancellation">A token for possible cancellation</param>
        private static Task<bool> ZipSetp(string strDirectory, ZipOutputStream s, string parentPath,List<string> exclude,
            CancellationTokenSource cancellation = null)
        {
            return Task.Run(async () =>
            {
                try
                {
                    cancellation?.Token.ThrowIfCancellationRequested();
                    if (strDirectory[strDirectory.Length - 1] != Path.DirectorySeparatorChar)
                    {
                        strDirectory += Path.DirectorySeparatorChar;
                    }

                    Crc32 crc = new Crc32();

                    string[] filenames = Directory.GetFileSystemEntries(strDirectory);
                    if (exclude != null && exclude.Any())
                    {
                        filenames = filenames.Where(x => !exclude.Any(f =>
                                string.Equals(Path.GetFileName(x), f, StringComparison.CurrentCultureIgnoreCase)))
                            .ToArray();
                    }
                    foreach (string file in filenames) // traverse all files and directories
                    {
                        cancellation?.Token.ThrowIfCancellationRequested();
                        if (Directory.Exists(file)
                        ) // is treated as a directory first. If this directory exists, recursively copy the files below the directory.
                        {
                            string pPath = parentPath;
                            pPath += file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                            pPath += "\\";
                            if (!await ZipSetp(file, s, pPath,exclude, cancellation)) return false;
                        }
                        else // Otherwise compress the file directly
                        {
                            try
                            {


                                // Open the compressed file
                                using FileStream fs = File.OpenRead(file);
                                byte[] buffer = new byte[fs.Length];
                                var read = fs.Read(buffer, 0, buffer.Length);

                                string fileName = parentPath +
                                                  file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                                ZipEntry entry = new ZipEntry(fileName) {DateTime = DateTime.Now, Size = fs.Length};


                                fs.Close();

                                crc.Reset();
                                crc.Update(buffer);

                                entry.Crc = crc.Value;
                                s.PutNextEntry(entry);

                                s.Write(buffer, 0, buffer.Length);
                            }
                            catch (Exception e)
                            {
                            }
                        }
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

        private static bool xZipSetp(string strDirectory, ZipOutputStream s, string parentPath, List<string> exclude,
    CancellationTokenSource cancellation = null)
        {
            try
            {
                cancellation?.Token.ThrowIfCancellationRequested();
                if (strDirectory[strDirectory.Length - 1] != Path.DirectorySeparatorChar)
                {
                    strDirectory += Path.DirectorySeparatorChar;
                }

                Crc32 crc = new Crc32();

                string[] filenames = Directory.GetFileSystemEntries(strDirectory);
                if (exclude != null && exclude.Any())
                {
                    filenames = filenames.Where(x => !exclude.Any(f =>
                            string.Equals(Path.GetFileName(x), f, StringComparison.CurrentCultureIgnoreCase)))
                        .ToArray();
                }
                foreach (string file in filenames) // traverse all files and directories
                {
                    cancellation?.Token.ThrowIfCancellationRequested();
                    if (Directory.Exists(file)
                    ) // is treated as a directory first. If this directory exists, recursively copy the files below the directory.
                    {
                        string pPath = parentPath;
                        pPath += file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                        pPath += "\\";
                        if (!xZipSetp(file, s, pPath, exclude, cancellation)) return false;
                    }
                    else // Otherwise compress the file directly
                    {
                        try
                        {


                            // Open the compressed file
                            using FileStream fs = File.OpenRead(file);
                            byte[] buffer = new byte[fs.Length];
                            var read = fs.Read(buffer, 0, buffer.Length);

                            string fileName = parentPath +
                                              file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                            ZipEntry entry = new ZipEntry(fileName) { DateTime = DateTime.Now, Size = fs.Length };


                            fs.Close();

                            crc.Reset();
                            crc.Update(buffer);

                            entry.Crc = crc.Value;
                            s.PutNextEntry(entry);

                            s.Write(buffer, 0, buffer.Length);
                        }
                        catch (Exception e)
                        {
                        }
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

        public static bool IsValidBackup(string zipedFile, string password)
        {
            try
            {
                using ZipInputStream s = new ZipInputStream(File.OpenRead(zipedFile)) { Password = password };
                var xret = false;
                try
                {
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        var names = theEntry.Name?.Split('/');
                        if(names == null || names.Length == 0 || names.Length > 1)
                        {
                            continue;
                        }
                        var name = names.First()?.Trim();
                        var ext = Path.GetExtension(name).ToLower();
                        if (string.IsNullOrEmpty(name) || (theEntry.IsFile && (ext != ".rpm" && ext != ".db")))
                        {
                            xret = false;
                            break;
                        }
                        else if (theEntry.IsDirectory)
                        {
                            var type = LocalDatabase.GetDbType(name);
                            if (type == DbType.Geen)
                            {
                                xret = false; break;
                            }
                        }
                        xret = true;
                    }
                }
                catch (Exception e)
                {

                }
                finally
                {
                    s.Close();
                }
                return xret;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Extract a zip file.
        /// </summary>
        /// <param name="zipedFile">The ziped file.</param>
        /// <param name="strDirectory">The STR directory.</param>
        /// <param name="password">The password for the zip file. </param>
        /// <param name="overWrite">Overwrite existing files. </param>
        /// <param name="cancellation">A token for possible cancellation</param>
        public Task UnZip(string zipedFile, string strDirectory, string password, bool overWrite, CancellationTokenSource cancellation = null)
        {
            return Task.Run(() =>
            {
                try
                {
                    if (strDirectory == "")
                        strDirectory = Directory.GetCurrentDirectory();
                    if (!strDirectory.EndsWith("\\"))
                        strDirectory = strDirectory + "\\";

                    using ZipInputStream s = new ZipInputStream(File.OpenRead(zipedFile)) {Password = password};

                    try
                    {
                        ZipEntry theEntry;
                        while ((theEntry = s.GetNextEntry()) != null)
                        {
                            cancellation?.Token.ThrowIfCancellationRequested();
                            string directoryName = "";
                            string pathToZip = "";
                            pathToZip = theEntry.Name;

                            if (pathToZip != "")
                                directoryName = Path.GetDirectoryName(pathToZip) + "\\";

                            string fileName = Path.GetFileName(pathToZip);

                            Directory.CreateDirectory(strDirectory + directoryName);

                            if (fileName != "")
                            {
                                if ((File.Exists(strDirectory + directoryName + fileName) && overWrite) ||
                                    (!File.Exists(strDirectory + directoryName + fileName)))
                                {
                                    using FileStream streamWriter =
                                        File.Create(strDirectory + directoryName + fileName);
                                    bool valid = false;
                                    try
                                    {
                                        byte[] data = new byte[2048];
                                        while (true)
                                        {
                                            var size = s.Read(data, 0, data.Length);

                                            if (size > 0)
                                                streamWriter.Write(data, 0, size);
                                            else
                                                break;
                                            cancellation?.Token.ThrowIfCancellationRequested();
                                        }

                                        valid = true;
                                    }
                                    catch (Exception e)
                                    {
                                    }
                                    finally
                                    {
                                        streamWriter.Close();
                                    }

                                    if (!valid)
                                        try
                                        {
                                            File.Delete(strDirectory + directoryName + fileName);
                                        }
                                        catch (Exception e)
                                        {
                                        }

                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {

                    }
                    finally
                    {
                        s.Close();
                    }
                }
                catch (Exception e)
                {
                }
            });

        }

    }
}
