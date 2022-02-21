using System.IO;

namespace FolderSync
{
    public interface IFileComparer
    {
        int Compare(FileInfo info1, FileInfo info2);
    }
}