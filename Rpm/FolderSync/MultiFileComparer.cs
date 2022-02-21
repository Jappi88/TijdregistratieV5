using System.IO;

namespace FolderSync
{
    public class MultiFileComparer
    {
        public int Compare(FileInfo file1, FileInfo file2)
        {
            return new DefaultSyncComparer().Compare(file1, file2);
        }
    }
}