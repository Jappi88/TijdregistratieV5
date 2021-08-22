using FolderSync.Reflection;

namespace FolderSync
{
    public class FileOperation : ReflectionObject, IFileOperation
    {
        public virtual void DoOperation()
        {
        }
    }
}
