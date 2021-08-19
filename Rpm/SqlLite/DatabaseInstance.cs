using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using LiteDB;
using Rpm.Various;

namespace Rpm.SqlLite
{
    public enum DbInstanceType
    {
        LiteDb,
        MultipleFiles,
        Server
    }

    public class DatabaseInstance<T> : IDbCollection<T>
    {
        public bool RaiseEventWhenChanged { get; set; } = true;
        public bool RaiseEventWhenDeleted { get; set; } = true;
        public event FileSystemEventHandler InstanceChanged;
        public event FileSystemEventHandler InstanceDeleted;
        public event ProgressChangedHandler ProgressChanged;

        public DatabaseInstance(DbInstanceType instanceType, string dbrootpath, string dbname, string collectionname)
        {
            DbName = dbname;
            DbRootPath = dbrootpath;
            CollectionName = collectionname;
            InstanceType = instanceType;
            InitInstance();
        }

        public string DbName { get; }
        public string DbRootPath { get; }
        public string CollectionName { get; private set; }
        public string TypeName { get; private set; }
        public DbType Type { get; private set; }
        public DbInstanceType InstanceType { get; }
        public SqlDatabase ServerDb { get; private set; }
        public LiteDatabase LocalDb { get; private set; }
        public ILiteCollection<T> LocalDbCollection { get; private set; }
        public MultipleFileDb MultiFiles { get; private set; }

        private void InitInstance()
        {
            var type = typeof(T);
            TypeName = type.Name;
            Type = GetType(TypeName);
            var rootpath = $"{DbRootPath}\\{DbName}";
            switch (InstanceType)
            {
                case DbInstanceType.LiteDb:
                    LocalDb = new LiteDatabase(new ConnectionString(rootpath + ".db")
                        {Connection = ConnectionType.Shared});
                    LocalDbCollection = LocalDb.GetCollection<T>(CollectionName);
                    break;
                case DbInstanceType.MultipleFiles:
                    MultiFiles = new MultipleFileDb(rootpath);
                    CollectionName = DbName;
                    MultiFiles.FileChanged += MultiFiles_FileChanged;
                    MultiFiles.FileDeleted += MultiFiles_FileDeleted;
                    MultiFiles.SecondaryFileChanged += MultiFiles_SecondaryFileChanged;
                    MultiFiles.SecondaryFileDeleted += MultiFiles_SecondaryFileDeleted;
                    break;
                case DbInstanceType.Server:
                    ServerDb = new SqlDatabase(TypeName);
                    CollectionName = TypeName;
                    break;
            }
        }

        #region Multi Events
        private void MultiFiles_SecondaryFileDeleted(object sender, System.IO.FileSystemEventArgs e)
        {
          OnInstanceDeleted(e);
        }

        private void MultiFiles_SecondaryFileChanged(object sender, System.IO.FileSystemEventArgs e)
        {
           OnInstanceChanged(e);
        }

        private void MultiFiles_FileDeleted(object sender, System.IO.FileSystemEventArgs e)
        {
            OnInstanceDeleted(e);
        }

        private void MultiFiles_FileChanged(object sender, System.IO.FileSystemEventArgs e)
        {
            OnInstanceChanged(e);
        }
        #endregion Multi Events

        private DbType GetType(string name)
        {
            switch (name.ToLower())
            {
                case "productieformulier":
                    return DbType.Producties;
                case "personeel":
                    return DbType.Medewerkers;
                case "useraccount":
                    return DbType.Accounts;
                case "usersettings":
                    return DbType.Opties;
                case "logentry":
                    return DbType.Logs;
                case "userchange":
                    return DbType.Changes;
                case "dbversion":
                    return DbType.Versions;
            }

            return DbType.None;
        }

        #region Implemented



        public Task<T> FindOne(string id)
        {
            return Task.Run(async () =>
            {
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            if (LocalDbCollection == null) throw new NullReferenceException();
                            return LocalDbCollection.FindById(id);
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            return MultiFiles.GetEntry<T>(id);
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                            return await ServerDb.GetInstance<T>(id);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                return default;
            });
        }

        private List<T> GetQueryItems(ILiteQueryable<T> query)
        {
            var xreturn = new List<T>();
            try
            {
                var count = query.Count();
                for (var i = 0; i < count; i++)
                    try
                    {
                        var xvalue = query.Skip(i).First();
                        if (xvalue != null)
                            xreturn.Add(xvalue);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
            }
            catch
            {
            }

            return xreturn;
        }

        public Task<List<T>> FindAll()
        {
            return Task.Run(async () =>
            {
                var xreturn = new List<T>();
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            if (LocalDbCollection == null) throw new NullReferenceException();
                            xreturn = GetQueryItems(LocalDbCollection.Query());
                            break;
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            xreturn = MultiFiles.GetAllEntries<T>();
                            break;
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                            xreturn = await ServerDb.GetAllInstances<T>();
                            break;
                    }
                }
                catch (Exception)
                {
                }

                return xreturn;
            });
        }

        public Task<List<T>> FindAll(IsValidHandler validhandler)
        {
            return Task.Run(async () =>
            {
                var xreturn = new List<T>();
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            if (LocalDbCollection == null) throw new NullReferenceException();
                            xreturn = GetQueryItems(LocalDbCollection.Query());
                            break;
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            xreturn = MultiFiles.GetEntries<T>(validhandler);
                            break;
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                            xreturn = await ServerDb.GetAllInstances<T>();
                            break;
                    }
                }
                catch (Exception)
                {
                }

                return xreturn;
            });
        }

        public Task<List<string>> GetAllIDs(bool checksecondary)
        {
            return Task.Run(() =>
            {
                var xreturn = new List<string>();
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            //if (LocalDbCollection == null) throw new NullReferenceException();
                            // xreturn = LocalDbCollection.FindAll().ToList();
                            break;
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            xreturn = MultiFiles.GetAllIDs(checksecondary);
                            break;
                        case DbInstanceType.Server:
                            //if (ServerDb == null) throw new NullReferenceException();
                            // xreturn = await ServerDb.GetAllInstances<T>();
                            break;
                    }
                }
                catch (Exception)
                {
                }

                return xreturn;
            });
        }

        public Task<List<string>> GetAllPaths(bool checksecondary)
        {
            return Task.Run(() =>
            {
                var xreturn = new List<string>();
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            //if (LocalDbCollection == null) throw new NullReferenceException();
                            // xreturn = LocalDbCollection.FindAll().ToList();
                            break;
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            xreturn = MultiFiles.GetAllPaths(checksecondary);
                            break;
                        case DbInstanceType.Server:
                            //if (ServerDb == null) throw new NullReferenceException();
                            // xreturn = await ServerDb.GetAllInstances<T>();
                            break;
                    }
                }
                catch (Exception)
                {
                }

                return xreturn;
            });
        }

        public Task<List<T>> FindAll(string[] ids)
        {
            return Task.Run(async () =>
            {
                var xreturn = new List<T>();
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            if (LocalDbCollection == null) throw new NullReferenceException();
                            foreach (var id in ids)
                            {
                                var xent = LocalDbCollection.FindOne(id);
                                if (xent != null)
                                    xreturn.Add(xent);
                            }

                            break;
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            xreturn = MultiFiles.GetEntries<T>(ids);
                            break;
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                            xreturn = await ServerDb.GetInstances<T>(ids);
                            break;
                    }
                }
                catch
                {
                }

                return xreturn;
            });
        }

        public Task<List<T>> FindAll(string criteria, bool fullmatch)
        {
            return Task.Run(() =>
            {
                var xreturn = new List<T>();
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            if (LocalDbCollection == null) throw new NullReferenceException();
                            break;
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            xreturn = MultiFiles.FindEntries<T>(criteria,fullmatch);
                            break;
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                            break;
                    }
                }
                catch
                {
                }

                return xreturn;
            });
        }

        public Task<List<T>> FindAll(DateTime vanaf, DateTime tot, IsValidHandler validhandler)
        {
            return Task.Run(async () =>
            {
                var xreturn = new List<T>();
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            if (LocalDbCollection == null) throw new NullReferenceException();
                            xreturn = LocalDbCollection.FindAll().ToList();
                            break;
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            xreturn = MultiFiles.GetEntries<T>(vanaf, tot,validhandler);
                            break;
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                            xreturn = await ServerDb.GetLatestEntries<T>(Type, vanaf, tot);
                            break;
                    }
                }
                catch
                {
                }

                return xreturn;
            });
        }

        public Task<bool> Replace(string oldid, T newitem)
        {
            return Task.Run(async () =>
            {
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            if (LocalDbCollection == null) throw new NullReferenceException();
                            LocalDbCollection.Delete(oldid);
                            LocalDbCollection.Upsert(oldid.TrimEnd(' '), newitem);
                            return true;
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            return MultiFiles.Replace(oldid, newitem);
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                            await ServerDb.Delete(oldid, Type);
                            return await ServerDb.Upsert(newitem, oldid.TrimEnd(' '), Type);
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> Delete(string id)
        {
            return Task.Run(async () =>
            {
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            if (LocalDbCollection == null) throw new NullReferenceException();
                            LocalDbCollection.Delete(id);
                            return true;
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            return MultiFiles.Delete(id);
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                            return await ServerDb.Delete(id, Type);
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<int> DeleteAll()
        {
            return Task.Run(async () =>
            {
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            if (LocalDbCollection == null) throw new NullReferenceException();
                            return LocalDbCollection.DeleteAll();
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            return MultiFiles.DeleteAll();
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                            return await ServerDb.DeleteAll(Type);
                    }

                    return 0;
                }
                catch
                {
                    return -1;
                }
            });
        }

        public Task<int> Delete(string[] ids)
        {
            return Task.Run(async () =>
            {
                var done = 0;
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            if (LocalDbCollection == null) throw new NullReferenceException();
                            foreach (var id in ids)
                                if (LocalDbCollection.Delete(id))
                                    done++;
                            break;
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            done = MultiFiles.Delete(ids);
                            break;
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                            done = await ServerDb.Delete(ids, Type);
                            break;
                    }
                }
                catch
                {
                }

                return done;
            });
        }

        public Task<bool> Update(string id, T item)
        {
            return Task.Run(async () =>
            {
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            if (LocalDbCollection == null) throw new NullReferenceException();
                            return LocalDbCollection.Upsert(id, item);
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            return MultiFiles.Upsert(id, item);
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                            return await ServerDb.Upsert(item, id, Type);
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> Exists(string id)
        {
            return Task.Run(async () =>
            {
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            if (LocalDbCollection == null) throw new NullReferenceException();
                            return LocalDbCollection.Exists(id);
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            return MultiFiles.Exists(id);
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                            return await ServerDb.Exists(id, Type);
                    }

                    return false;
                }
                catch
                {
                    return false;
                }
            });
        }

        public Task<bool> Upsert(string id, T item)
        {
            return Update(id, item);
        }


        public void Close()
        {
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        if (LocalDbCollection == null) throw new NullReferenceException();
                        LocalDb?.Dispose();
                        LocalDbCollection = null;
                        break;
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        MultiFiles = null;
                        break;
                    case DbInstanceType.Server:
                        if (ServerDb == null) throw new NullReferenceException();
                        ServerDb = null;
                        break;
                }
            }
            catch
            {
            }
        }

        public Task<int> Count()
        {
            return Task.Run(async () =>
            {
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            if (LocalDbCollection == null) throw new NullReferenceException();
                            return LocalDbCollection.Count();
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            return MultiFiles.Count();
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                            return await ServerDb.Count(Type);
                    }

                    return 0;
                }
                catch
                {
                    return 0;
                }
            });
        }

        #endregion Implemented

        protected virtual void OnInstanceChanged(FileSystemEventArgs value)
        {
            if (RaiseEventWhenChanged)
                InstanceChanged?.Invoke(this, value);
        }

        protected virtual void OnInstanceDeleted(FileSystemEventArgs value)
        {
            if (RaiseEventWhenDeleted)
                InstanceDeleted?.Invoke(this, value);
        }

        protected virtual void OnProgressChanged(ProgressArg arg)
        {
            ProgressChanged?.Invoke(this, arg);
        }
    }
}