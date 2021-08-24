using System;
using System.IO;
using Rpm.Productie;

namespace FolderSync
{
    public enum FolderSynchronizationItemFileOption
    {
        SourceCreate,SourceDelete, SourceOverwrite, DestinationCreate, DestinationDelete, DestinationOverwrite, NoOperation
    }

    public class FolderSynchronizationItemFile : FileOperation
    {
        protected string _SourceFileName;
        protected string _DestinationFileName;
        protected FolderSynchronizationItemFileOption _Option;

        #region Properties

        public string SourceFileName
        {
            get => _SourceFileName;
            set => _SourceFileName = value;
        }

        public string DestinationFileName
        {
            get => _DestinationFileName;
            set => _DestinationFileName = value;
        }

        public FolderSynchronizationItemFileOption Option
        {
            get => _Option;
            set => _Option = value;
        }

        #endregion

        public FolderSynchronizationItemFile(string source, string destination,
            FolderSynchronizationItemFileOption option)
        {
            _SourceFileName = source;
            _DestinationFileName = destination;
            _Option = option;
        }



        public override void DoOperation()
        {
            try
            {
                switch (_Option)
                {
                    case FolderSynchronizationItemFileOption.SourceCreate:
                        if (File.Exists(_SourceFileName) == false)
                        {
                            File.Copy(_DestinationFileName, _SourceFileName);
                            //var sourcefi = new FileInfo(_SourceFileName);
                            //var destfi = new FileInfo(_DestinationFileName);
                            //sourcefi.LastWriteTime = destfi.LastWriteTime;
                        }
                        break;
                    case FolderSynchronizationItemFileOption.SourceOverwrite:
                        if (File.Exists(_SourceFileName))
                        {
                            File.Copy(_DestinationFileName, _SourceFileName, true);
                            //var sourcefi = new FileInfo(_SourceFileName);
                            //var destfi = new FileInfo(_DestinationFileName);
                            //sourcefi.LastWriteTime = destfi.LastWriteTime;
                        }
                        break;
                    case FolderSynchronizationItemFileOption.DestinationCreate:
                        if (File.Exists(_DestinationFileName) == false)
                        {

                            File.Copy(_SourceFileName, _DestinationFileName);
                            //var sourcefi = new FileInfo(_SourceFileName);
                            //var destfi = new FileInfo(_DestinationFileName);
                            //destfi.LastWriteTime = sourcefi.LastWriteTime;
                        }
                        break;
                    case FolderSynchronizationItemFileOption.DestinationDelete:
                    case FolderSynchronizationItemFileOption.SourceDelete:
                        try
                        {
                            if (File.Exists(_DestinationFileName))
                                File.Delete(_DestinationFileName);
                            else if (File.Exists(_SourceFileName))
                                File.Delete(_SourceFileName);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                        }
                        break;
                    case FolderSynchronizationItemFileOption.DestinationOverwrite:
                        if (File.Exists(_DestinationFileName))
                        {
                            //var sourcefi = new FileInfo(_SourceFileName);
                            File.Copy(_SourceFileName, _DestinationFileName, true);
                            
                            //var destfi = new FileInfo(_DestinationFileName);
                            //destfi.LastWriteTime = sourcefi.LastWriteTime;
                        }
                        break;
                    case FolderSynchronizationItemFileOption.NoOperation:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            catch (Exception e)
            {
            }

        }

        public override bool Equals(object obj)
        {
            return (obj is FolderSynchronizationItemFile item &&
                    string.Equals(item.SourceFileName, SourceFileName, StringComparison.CurrentCultureIgnoreCase) &&
                    string.Equals(item.DestinationFileName, DestinationFileName,
                        StringComparison.CurrentCultureIgnoreCase));
        }

        public override int GetHashCode()
        {
            int id1 = string.IsNullOrEmpty(SourceFileName) ? 0 : SourceFileName.ToLower().GetHashCode();
            int id2 = string.IsNullOrEmpty(DestinationFileName) ? 0 : DestinationFileName.ToLower().GetHashCode();
            return id1 | id2;
        }
    }
}
