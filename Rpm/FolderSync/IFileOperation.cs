using System.Threading.Tasks;

namespace FolderSync
{
    public interface IFileOperation
    {
        Task DoOperation();
    }
}
