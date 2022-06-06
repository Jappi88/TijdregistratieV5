using Rpm.Productie;
using Rpm.Various;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Rpm.SqlLite
{
    public interface IDbCollection<T>
    {
        public int Count();
        public T FindOne(string id, bool usesecondary);
        public List<T> FindAll(bool usesecondary);
        public List<T> FindAll(IsValidHandler validhandler, bool checksecondary);
        public List<T> FindAll(TijdEntry bereik, IsValidHandler validhandler, bool checksecondary);
        public List<string> GetAllIDs(bool checksecondary);
        public List<string> GetAllPaths(bool checksecondary);
        public List<T> FindAll(string[] ids, bool usesecondary);
        public List<T> FindAll(string criteria, bool fullmatch, bool usesecondary);
        public bool Replace(string oldid, T newitem, bool onlylocal, string change);
        public bool Delete(string id);
        public int DeleteAll();
        public int Delete(string[] ids);
        public bool Update(string id, T item, bool onlylocal, string change);
        public bool Upsert(string id, T item, bool onlylocal, string change);
        public T FromPath<T>(string filepath);
        public bool Exists(string id);

        public bool RaiseEventWhenChanged { get; set; }
        public bool RaiseEventWhenDeleted { get; set; }
        public MultipleFileDb MultiFiles { get; }

        public event FileSystemEventHandler InstanceChanged;
        public event FileSystemEventHandler InstanceDeleted;
        public event ProgressChangedHandler ProgressChanged;
        public void Close();
    }
}