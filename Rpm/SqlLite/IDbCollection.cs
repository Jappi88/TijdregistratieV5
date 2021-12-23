using Rpm.Productie;
using Rpm.Various;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Rpm.SqlLite
{
    public interface IDbCollection<T>
    {
        public Task<int> Count();
        public Task<T> FindOne(string id);
        public Task<List<T>> FindAll();
        public Task<List<T>> FindAll(IsValidHandler validhandler);
        public Task<List<T>> FindAll(TijdEntry bereik, IsValidHandler validhandler);
        public Task<List<string>> GetAllIDs(bool checksecondary);
        public Task<List<string>> GetAllPaths(bool checksecondary);
        public Task<List<T>> FindAll(string[] ids);
        public Task<List<T>> FindAll(string criteria, bool fullmatch);
        public Task<bool> Replace(string oldid, T newitem, bool onlylocal);
        public Task<bool> Delete(string id);
        public Task<int> DeleteAll();
        public Task<int> Delete(string[] ids);
        public Task<bool> Update(string id, T item, bool onlylocal);
        public Task<bool> Upsert(string id, T item, bool onlylocal);
        public Task<T> FromPath<T>(string filepath);
        public Task<bool> Exists(string id);

        public bool RaiseEventWhenChanged { get; set; }
        public bool RaiseEventWhenDeleted { get; set; }
        public MultipleFileDb MultiFiles { get; }

        public event FileSystemEventHandler InstanceChanged;
        public event FileSystemEventHandler InstanceDeleted;
        public event ProgressChangedHandler ProgressChanged;
        public void Close();
    }
}