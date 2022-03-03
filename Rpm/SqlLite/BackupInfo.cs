using ICSharpCode.SharpZipLib.Checksum;
using ICSharpCode.SharpZipLib.Zip;
using Polenter.Serialization;
using Rpm.Misc;
using Rpm.Productie;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Timer = System.Timers.Timer;

namespace Rpm.SqlLite
{
    public class BackupInfo
    {
        public string DbVersion { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedFileName { get; set; }
        public DateTime Created { get; set; }
        public bool IsCreating { get; set; }
        public DateTime Creating { get; set; }
        [ExcludeFromSerialization]
        public bool IsSyncing { get; private set; }
        public CancellationTokenSource CancellationToken;

        internal Timer _BackupSyncTimer = new Timer();
        private double _interval;

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

        public void StartBackupSyncer(double interval)
        {

            if (IsSyncing)
            {
                BackupInterval = interval;
                return;
            }
            IsSyncing = true;
            _BackupSyncTimer?.Stop();
            _BackupSyncTimer?.Dispose();
            _BackupSyncTimer = new Timer(interval);
            _BackupSyncTimer.Elapsed += _BackupSyncTimer_Elapsed;
            _BackupSyncTimer.Start();
        }

        private async void _BackupSyncTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                IsSyncing = true;
                _BackupSyncTimer?.Stop();
                var bki = BackupInfo.Load();
                if (!bki.IsCreating || (bki.IsCreating && (DateTime.Now - bki.Creating).TotalHours > 2))
                {
                    if (Manager.Opties is {CreateBackup: true} && (DateTime.Now - bki.Created).TotalHours >
                        TimeSpan.FromMilliseconds(Manager.Opties.BackupInterval).TotalHours)
                    {
                        IsCreating = false;
                        await CreateBackup();
                    }
                }

                if (Manager.Opties is {CreateBackup: true})
                    _BackupSyncTimer?.Start();
                else IsSyncing = false;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        public Task CreateBackup()
        {

            if (CancellationToken == null)
                CancellationToken = new CancellationTokenSource();
            return Task.Run(async () =>
            {
                if (IsCreating) return;
                try
                {
                    Creating = DateTime.Now;
                    IsCreating = true;
                    this.Save();


                    string path = Manager.BackupPath + $"\\RPM_Backup_{DateTime.Now: ddMMHHmmss}.zip";
                    bool valid = await ZipFileDirectory(Manager.DbPath, path, CancellationToken);
                    //zip.Close();
                    IsCreating = false;
                    if (valid && !CancellationToken.IsCancellationRequested)
                    {
                        CreatedBy = Manager.Opties?.Username;
                        Created = DateTime.Now;
                        CreatedFileName = path;

                        if (Manager.Opties != null)
                        {
                            var backups = Directory.GetFiles(Manager.BackupPath, "*.zip", SearchOption.TopDirectoryOnly)
                                .OrderBy(x => new FileInfo(x).CreationTime).ToList();
                            if (backups.Count > Manager.Opties.MaxBackupCount)
                            {
                                try
                                {
                                    int xc = backups.Count - Manager.Opties.MaxBackupCount;
                                    for (int i = 0; i < xc; i++)
                                        File.Delete(backups[i]);
                                }
                                catch (Exception e)
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
                        catch (Exception e)
                        {
                        }
                    }
                    this.Save();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
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
        public static Task<bool> ZipFileDirectory(string strDirectory, string zipedFile,
            CancellationTokenSource cancellation = null)
        {
            return Task.Run(async () =>
            {
                using FileStream ZipFile = File.Create(zipedFile);
                using ZipOutputStream s = new ZipOutputStream(ZipFile);
                return await ZipSetp(strDirectory, s, "", cancellation);
            });

        }

        /// <summary>
        /// Recursive traversal directory
        /// </summary>
        /// <param name="strDirectory">The directory.</param>
        /// <param name="s">The ZipOutputStream Object.</param>
        /// <param name="parentPath">The parent path.</param>
        /// <param name="cancellation">A token for possible cancellation</param>
        private static Task<bool> ZipSetp(string strDirectory, ZipOutputStream s, string parentPath,
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

                    foreach (string file in filenames) // traverse all files and directories
                    {
                        cancellation?.Token.ThrowIfCancellationRequested();
                        if (Directory.Exists(file)
                        ) // is treated as a directory first. If this directory exists, recursively copy the files below the directory.
                        {
                            string pPath = parentPath;
                            pPath += file.Substring(file.LastIndexOf("\\", StringComparison.Ordinal) + 1);
                            pPath += "\\";
                            if (!await ZipSetp(file, s, pPath, cancellation)) return false;
                        }
                        else // Otherwise compress the file directly
                        {
                            try
                            {


                                // Open the compressed file
                                using FileStream fs = File.OpenRead(file);
                                byte[] buffer = new byte[fs.Length];
                                fs.Read(buffer, 0, buffer.Length);

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
