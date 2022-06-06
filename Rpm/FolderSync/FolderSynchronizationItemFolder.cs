using System.IO;
using System.Threading.Tasks;

namespace FolderSync
{
    public enum FolderSynchronizationItemFolderOption
    {
        CreateFolder, NoOperation
    }

    public class FolderSynchronizationItemFolder : FileOperation
    {
        protected string _SourceFileName = string.Empty;
        protected string _DestinationFileName = string.Empty;
        protected FolderSynchronizationItemFolderOption _Option = FolderSynchronizationItemFolderOption.NoOperation;

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

        public FolderSynchronizationItemFolderOption Option
        {
            get => _Option;
            set => _Option = value;
        }
        #endregion

        public FolderSynchronizationItemFolder(string foldername, FolderSynchronizationItemFolderOption option)
        {
            _SourceFileName = foldername;
            _Option = option;
        }

        public override Task DoOperation()
        {
            return Task.Factory.StartNew(() =>
            {
                if (_Option == FolderSynchronizationItemFolderOption.CreateFolder)
                {
                    if (Directory.Exists(_SourceFileName) == false)
                    { Directory.CreateDirectory(_SourceFileName); }
                }
            });
        }
    }
}
