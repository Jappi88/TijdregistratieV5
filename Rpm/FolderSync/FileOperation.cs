using FolderSync.Reflection;
using System.Threading.Tasks;

namespace FolderSync
{
    public class FileOperation : ReflectionObject, IFileOperation
    {
        public virtual Task DoOperation()
        {
            return Task.CompletedTask;
        }
    }
}
