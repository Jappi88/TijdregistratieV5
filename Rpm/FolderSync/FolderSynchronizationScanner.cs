using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FolderSync.MultiKey;
using Rpm.Misc;
using Rpm.Productie;

namespace FolderSync
{
    public enum FolderSynchorizationOption
    {
        Both, Destination
    }

    public class FolderSynchronizationScanner
    {
        protected string _Source = string.Empty;
        protected string _Destination = string.Empty;
        protected FolderSynchorizationOption _Options = FolderSynchorizationOption.Both;
        protected MultiKeyCollection<FileOperation> _SyncCollection = null;        
        protected MultiFileComparer _Comparer = null;

        public MultiKeyCollection<FileOperation> SyncCollection => _SyncCollection;

        public FolderSynchronizationScanner(string source, string destination, FolderSynchorizationOption option)
        {
            if (Directory.Exists(source) == false || Directory.Exists(destination) == false) return;

            _SyncCollection = new MultiKeyCollection<FileOperation>(new string[] { "SourceFileName", "DestinationFileName" });
            _Comparer = new MultiFileComparer();

            _Source = source;
            _Destination = destination;
            _Options = option;
        }

        public Task Sync()
        {
            return Task.Factory.StartNew(() => StartFolder("", -1));
        }

        protected void StartFolder(string folder, int level)
        {
            string sourcePath = Path.Combine(_Source, folder);
            string destinationPath = Path.Combine(_Destination, folder);

            if (Directory.Exists(sourcePath) == false) AddCreateFolderTask(sourcePath);
            if (Directory.Exists(destinationPath) == false) AddCreateFolderTask(destinationPath);

            if (Directory.Exists(sourcePath) == true) LoadFilesInFirstPath(sourcePath, destinationPath, false);
            if (_Options == FolderSynchorizationOption.Both && Directory.Exists(destinationPath)) 
                LoadFilesInFirstPath(destinationPath, sourcePath, true);

            if (Directory.Exists(sourcePath) == true)
            {
                foreach (string subfolder in Directory.GetDirectories(sourcePath))
                {
                    string shortfoldername = subfolder.GetLastPathName(level + 1);
                    StartFolder(shortfoldername, level + 1);
                }
            }

            if (_Options == FolderSynchorizationOption.Both && Directory.Exists(destinationPath) == true)
            {
                foreach (string subfolder in Directory.GetDirectories(destinationPath))
                {
                    string shortfoldername = subfolder.GetLastPathName(level + 1);
                    StartFolder(shortfoldername, level + 1);
                }
            }
        }

        protected void LoadFilesInFirstPath(string sourcePath, string destinationPath, bool flip)
        {
            string[] files = Directory.GetFiles(sourcePath);

            foreach (string file in files)
            {
                string shortfilename = Path.GetFileName(file);
                string sourcefilename = Path.Combine(sourcePath, shortfilename);
                string destinationfilename = Path.Combine(destinationPath, shortfilename);
                FolderSynchronizationItemFile item = null;
                var sourceExist = File.Exists(sourcefilename); // Source must be exist
                if (!sourceExist) continue;
                {
                    var destinationExist = File.Exists(destinationfilename);
                    if (destinationExist == false)
                    {
                        if (flip == false)
                        {
                            item = new FolderSynchronizationItemFile(sourcefilename, destinationfilename,
                                FolderSynchronizationItemFileOption.DestinationCreate);

                        }
                        else
                        {
                            item = new FolderSynchronizationItemFile(destinationfilename, sourcefilename,
                                FolderSynchronizationItemFileOption.SourceCreate);
                        }

                    }
                    else
                    {
                        FileInfo sourceInfo = new FileInfo(sourcefilename);
                        FileInfo destinationInfo = new FileInfo(destinationfilename);
                        int result = _Comparer.Compare(sourceInfo, destinationInfo);

                        FolderSynchronizationItemFileOption option = FolderSynchronizationItemFileOption.NoOperation;
                        if (result < 0)
                        {
                            if (flip == false) option = FolderSynchronizationItemFileOption.SourceOverwrite;
                            else option = FolderSynchronizationItemFileOption.DestinationOverwrite;
                        }
                        else if (result > 0)
                        {
                            if (flip == false) option = FolderSynchronizationItemFileOption.DestinationOverwrite;
                            else option = FolderSynchronizationItemFileOption.SourceOverwrite;
                        }

                        if (option != FolderSynchronizationItemFileOption.NoOperation)
                        {
                            if (flip == false)
                                item = new FolderSynchronizationItemFile(sourceInfo.FullName, destinationInfo.FullName,
                                    option);
                            else
                                item = new FolderSynchronizationItemFile(destinationInfo.FullName, sourceInfo.FullName,
                                    option);
                        }
                    }
                }

                if (item != null)
                {
                    AddFileTask(item);
                }
            }

            if (Directory.Exists(destinationPath))
            {
                string[] destFiles = Directory.GetFiles(destinationPath);
                string[] delFiles = destFiles.Where(x => !files.Any(f =>
                        string.Equals(f.GetLastOfPathName(1), x.GetLastOfPathName(1),
                            StringComparison.CurrentCultureIgnoreCase)))
                    .ToArray();
                if (delFiles.Length > 0)
                {
                    foreach (var delfile in delFiles)
                    {
                        AddFileTask(new(delfile, delfile, FolderSynchronizationItemFileOption.DestinationDelete));
                    }
                }
            }
        }

        public void AddCreateFolderTask(string folder)
        {
            FolderSynchronizationItemFolder item = new FolderSynchronizationItemFolder(folder, FolderSynchronizationItemFolderOption.CreateFolder);
            FolderSynchronizationItemFolder existingitem = (FolderSynchronizationItemFolder) _SyncCollection.GetAddObject((FileOperation) item);
            if (existingitem.Option != item.Option)
            {
                existingitem.Option = item.Option;
            }
        }

        public void AddFileTask(FolderSynchronizationItemFile item)
        {
            FolderSynchronizationItemFile existingitem = (FolderSynchronizationItemFile) _SyncCollection.GetAddObject((FileOperation) item);
            if (existingitem.Option != item.Option)
            {
                existingitem.Option = item.Option;
            }
        }
    }
}
