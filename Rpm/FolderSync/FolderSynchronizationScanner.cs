using FolderSync.MultiKey;
using Rpm.Misc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace FolderSync
{
    /// <summary>
    /// Synchronisatie opties
    /// </summary>
    public enum FolderSynchorizationOption
    {
        /// <summary>
        /// Synchroniseer aan beide zeides
        /// </summary>
        Both,
        /// <summary>
        /// Synchroniseer alleen naar een bestemming
        /// </summary>
        Destination,
        /// <summary>
        /// Synchroniseer naar een bron, en maak ze aan als ze niet bestaan
        /// </summary>
        SourceCreate
    }

    /// <summary>
    /// Een scanner dat zoekt naar gewijzigde bestanden
    /// </summary>
    public class FolderSynchronizationScanner
    {
        /// <summary>
        /// De  folder van de bron
        /// </summary>
        protected string Source = string.Empty;
        /// <summary>
        /// De foilder van de bestemming
        /// </summary>
        protected string Destination = string.Empty;
        /// <summary>
        /// Opties voor de synchronisatie
        /// </summary>
        protected FolderSynchorizationOption Options = FolderSynchorizationOption.Both;
        /// <summary>
        /// Alle wijzigingen die nog uitgevoerd moeten worden
        /// </summary>
        public MultiKeyCollection<FileOperation> SyncCollection;        
        /// <summary>
        /// Een vergelijker voor de bestanden
        /// </summary>
        protected MultiFileComparer Comparer;

        /// <summary>
        /// Maak een nieuwe Folder scanner aan
        /// </summary>
        /// <param name="source">De bron van de scanner</param>
        /// <param name="destination">Bestemming van de scanner</param>
        /// <param name="option">Synchronisatie opties</param>
        public FolderSynchronizationScanner(string source, string destination, FolderSynchorizationOption option)
        {
            if (Directory.Exists(source) == false || Directory.Exists(destination) == false) return;

            SyncCollection = new MultiKeyCollection<FileOperation>(new[] { "SourceFileName", "DestinationFileName" });
            Comparer = new MultiFileComparer();

            Source = source;
            Destination = destination;
            Options = option;
        }

        /// <summary>
        /// Start het scannen van wijzigingen
        /// </summary>
        /// <returns>Geeft een taak terug waar je op kan wachten, of op de achtergron wilt laten draaien</returns>
        public Task Sync()
        {
            return Task.Factory.StartNew(() => StartFolder("", -1));
        }

        /// <summary>
        /// Start het scannen van een folder
        /// </summary>
        /// <param name="folder">De folder die gescannen moet worden</param>
        /// <param name="level">De diepte van de folder die gescanned moet worden</param>
        public void StartFolder(string folder, int level)
        {
            string sourcePath = Path.Combine(Source, folder);
            string destinationPath = Path.Combine(Destination, folder);

            if (Directory.Exists(sourcePath) == false) AddCreateFolderTask(sourcePath);
            if (Directory.Exists(destinationPath) == false) AddCreateFolderTask(destinationPath);

            if (Directory.Exists(sourcePath)) LoadFilesInFirstPath(sourcePath, destinationPath, false);
            if (Options == FolderSynchorizationOption.Both && Directory.Exists(destinationPath)) 
                LoadFilesInFirstPath(destinationPath, sourcePath, true);

            if (Directory.Exists(sourcePath))
            {
                foreach (string subfolder in Directory.GetDirectories(sourcePath))
                {
                    string shortfoldername = subfolder.GetLastPathName(level + 1);
                    StartFolder(shortfoldername, level + 1);
                }
            }

            if (Options == FolderSynchorizationOption.Both && Directory.Exists(destinationPath))
            {
                foreach (string subfolder in Directory.GetDirectories(destinationPath))
                {
                    string shortfoldername = subfolder.GetLastPathName(level + 1);
                    StartFolder(shortfoldername, level + 1);
                }
            }
        }

        /// <summary>
        /// Laad de bestanden die in de eerste folder is
        /// </summary>
        /// <param name="sourcePath">De bron van de folder</param>
        /// <param name="destinationPath">Folder bestemming</param>
        /// <param name="flip">Draai de scan de andere kant op</param>
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
                        int result = Comparer.Compare(sourceInfo, destinationInfo);

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

                        if (Options == FolderSynchorizationOption.SourceCreate)
                        {
                            string xpath = Path.Combine(sourcePath, Path.GetFileName(delfile));
                            AddFileTask(new(xpath, delfile, FolderSynchronizationItemFileOption.SourceCreate));
                        }
                        else
                            AddFileTask(new(delfile, delfile, FolderSynchronizationItemFileOption.DestinationDelete));
                    }
                }
            }
        }

        /// <summary>
        /// Voeg toe om maak een nieuwe taak aan voor een folder
        /// </summary>
        /// <param name="folder">De folder waarvan een taak gemaakt moet worden</param>
        public void AddCreateFolderTask(string folder)
        {
            FolderSynchronizationItemFolder item = new FolderSynchronizationItemFolder(folder, FolderSynchronizationItemFolderOption.CreateFolder);
            FolderSynchronizationItemFolder existingitem = (FolderSynchronizationItemFolder) SyncCollection.GetAddFolderSyncEntry(item);
            if (existingitem.Option != item.Option)
            {
                existingitem.Option = item.Option;
            }
        }

        /// <summary>
        /// Voet toe of maak aan een nieuwe taak voor het wijzigen van een bestand
        /// </summary>
        /// <param name="item"></param>
        public void AddFileTask(FolderSynchronizationItemFile item)
        {
            FolderSynchronizationItemFile existingitem = (FolderSynchronizationItemFile) SyncCollection.GetAddFileSyncEntry(item);
            if (existingitem.Option != item.Option)
            {
                existingitem.Option = item.Option;
            }
        }
    }
}
