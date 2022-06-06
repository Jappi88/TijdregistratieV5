using ProductieManager.Rpm.SqlLite;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

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

        public DatabaseInstance(DbInstanceType instanceType,DbType type, string dbrootpath,string dbname, bool watchdatabase)
        {
            DbName = dbname;
            DbRootPath = dbrootpath;
            //CollectionName = collectionname;
            InstanceType = instanceType;
            InitInstance(watchdatabase,type);
        }

        public string DbName { get; }
        public string DbRootPath { get; }
        //public string CollectionName { get; private set; }
        public string TypeName { get; private set; }
        public DbType Type { get; private set; }
        public DbInstanceType InstanceType { get; }
        public SqlDatabase ServerDb { get; private set; }
        //public LiteDatabase LocalDb { get; private set; }
        //public ILiteCollection<T> LocalDbCollection { get; private set; }
        public MultipleFileDb MultiFiles { get; private set; }

        private void InitInstance(bool watchdatabase, DbType dbtype)
        {
            var type = typeof(T);
            TypeName = type.Name;
            Type = dbtype;
            var rootpath = $"{DbRootPath}\\{DbName}";
            switch (InstanceType)
            {
                case DbInstanceType.LiteDb:
                    //LocalDb = new LiteDatabase(new ConnectionString(rootpath + ".db")
                    //    {Connection = ConnectionType.Shared});
                    //LocalDbCollection = LocalDb.GetCollection<T>(CollectionName);
                    break;
                case DbInstanceType.MultipleFiles:
                    MultiFiles = new MultipleFileDb(rootpath, watchdatabase, Assembly.GetExecutingAssembly().GetName().Version.ToString(), Type);
                    if (watchdatabase)
                    {
                        MultiFiles.FileChanged += MultiFiles_FileChanged;
                        MultiFiles.FileDeleted += MultiFiles_FileDeleted;
                    }

                    break;
                case DbInstanceType.Server:
                    ServerDb = new SqlDatabase(TypeName);
                    break;
            }
        }

        #region Multi Events
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
                case "dbversion":
                    return DbType.Versions;
            }

            return DbType.Geen;
        }

        #region Implemented



        public T FindOne(string id, bool usesecondary)
        {
                try
                {
                    switch (InstanceType)
                    {
                        case DbInstanceType.LiteDb:
                            //if (LocalDbCollection == null) throw new NullReferenceException();
                            //return LocalDbCollection.FindById(id);
                        case DbInstanceType.MultipleFiles:
                            if (MultiFiles == null) throw new NullReferenceException();
                            return MultiFiles.GetEntry<T>(id, usesecondary);
                        case DbInstanceType.Server:
                            if (ServerDb == null) throw new NullReferenceException();
                        return default;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }

                return default;
        }

        public List<T> FindAll(bool usesecondary)
        {
            var xreturn = new List<T>();
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        //if (LocalDbCollection == null) throw new NullReferenceException();
                        //xreturn = GetQueryItems(LocalDbCollection.Query());
                        break;
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        xreturn = MultiFiles.GetAllEntries<T>(usesecondary);
                        break;
                    case DbInstanceType.Server:
                        break;
                }
            }
            catch (Exception)
            {
            }

            return xreturn;
        }

        public List<T> FindAll(IsValidHandler validhandler, bool checksecondary)
        {
            var xreturn = new List<T>();
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        //if (LocalDbCollection == null) throw new NullReferenceException();
                        //xreturn = GetQueryItems(LocalDbCollection.Query());
                        break;
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        xreturn = MultiFiles.GetEntries<T>(validhandler, checksecondary);
                        break;
                    case DbInstanceType.Server:
                        break;
                }
            }
            catch (Exception)
            {
            }

            return xreturn;
        }

        public List<T> FindAll(TijdEntry bereik, IsValidHandler validhandler, bool checksecondary)
        {
            var xreturn = new List<T>();
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        //if (LocalDbCollection == null) throw new NullReferenceException();
                        //xreturn = GetQueryItems(LocalDbCollection.Query());
                        break;
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        xreturn = MultiFiles.GetEntries<T>(validhandler, checksecondary);
                        break;
                    case DbInstanceType.Server:
                      
                        break;
                }
            }
            catch (Exception)
            {
            }

            return xreturn;
        }

        public List<string> GetAllIDs(bool checksecondary)
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
        }

        public List<string> GetAllPaths(bool checksecondary)
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
        }

        public List<T> FindAll(string[] ids, bool usesecondary)
        {
            var xreturn = new List<T>();
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        //if (LocalDbCollection == null) throw new NullReferenceException();
                        //foreach (var id in ids)
                        //{
                        //    var xent = LocalDbCollection.FindOne(id);
                        //    if (xent != null)
                        //        xreturn.Add(xent);
                        //}

                        break;
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        xreturn = MultiFiles.GetEntries<T>(ids, usesecondary);
                        break;
                    case DbInstanceType.Server:
                        break;
                }
            }
            catch
            {
            }

            return xreturn;
        }

        public List<T> FindAll(string criteria, bool fullmatch, bool usesecondary)
        {
            var xreturn = new List<T>();
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        //if (LocalDbCollection == null) throw new NullReferenceException();
                        break;
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        xreturn = MultiFiles.FindEntries<T>(criteria, fullmatch, usesecondary);
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
        }

        public bool Replace(string oldid, T newitem, bool onlylocal, string change)
        {
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        //if (LocalDbCollection == null) throw new NullReferenceException();
                        //LocalDbCollection.Delete(oldid);
                        //LocalDbCollection.Upsert(oldid.TrimEnd(' '), newitem);
                        return true;
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        if (!string.IsNullOrEmpty(change) && newitem is IDbLogging ent)
                        {
                            ent.Logs ??= new List<LogEntry>();
                            ent.Logs.Add(new LogEntry(change, MsgType.Info));
                        }
                        return MultiFiles.Replace<T>(oldid, newitem, onlylocal);
                    case DbInstanceType.Server:
                        //if (ServerDb == null) throw new NullReferenceException();
                        //_ = ServerDb.Delete(oldid, Type).Result;
                        //return ServerDb.Upsert(newitem, oldid.TrimEnd(' '), Type).Result;
                        break;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(string id)
        {
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        //if (LocalDbCollection == null) throw new NullReferenceException();
                        //LocalDbCollection.Delete(id);
                        return true;
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        return MultiFiles.Delete(id);
                    case DbInstanceType.Server:
                        //if (ServerDb == null) throw new NullReferenceException();
                        //return ServerDb.Delete(id, Type).Result;
                        break;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public int DeleteAll()
        {
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                    //if (LocalDbCollection == null) throw new NullReferenceException();
                    //return LocalDbCollection.DeleteAll();
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        return MultiFiles.DeleteAll();
                    case DbInstanceType.Server:
                        //if (ServerDb == null) throw new NullReferenceException();
                        //return ServerDb.DeleteAll(Type).Result;
                        break;
                }

                return 0;
            }
            catch
            {
                return -1;
            }
        }
        public int Delete(string[] ids)
        {
            var done = 0;
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        //if (LocalDbCollection == null) throw new NullReferenceException();
                        //foreach (var id in ids)
                        //    if (LocalDbCollection.Delete(id))
                        //        done++;
                        break;
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        done = MultiFiles.Delete(ids);
                        break;
                    case DbInstanceType.Server:
                        //if (ServerDb == null) throw new NullReferenceException();
                        //done = ServerDb.Delete(ids, Type).Result;
                        break;
                }
            }
            catch
            {
            }

            return done;
        }

        public bool Update(string id, T item, bool onlylocal, string change)
        {
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        //if (LocalDbCollection == null) throw new NullReferenceException();
                        //return LocalDbCollection.Upsert(id, item);
                        return false;
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        if (!string.IsNullOrEmpty(change) && item is IDbLogging ent)
                        {
                            ent.Logs ??= new List<LogEntry>();
                            ent.Logs.Add(new LogEntry(change, MsgType.Info));
                        }

                        return MultiFiles.Upsert(id, item, onlylocal);
                    case DbInstanceType.Server:
                        //if (ServerDb == null) throw new NullReferenceException();
                        //return ServerDb.Upsert(item, id, Type).Result;
                        break;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public T FromPath<T>(string filepath)
        {
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        //if (LocalDbCollection == null) throw new NullReferenceException();
                        break;
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        return MultipleFileDb.xFromPath<T>(filepath, true);
                    case DbInstanceType.Server:
                        if (ServerDb == null) throw new NullReferenceException();
                        break;
                }


            }
            catch
            {
            }
            return default;
        }

        public bool Exists(string id)
        {
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        //if (LocalDbCollection == null) throw new NullReferenceException();
                        //return LocalDbCollection.Exists(id);
                        return false;
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        return MultiFiles.Exists(id);
                    case DbInstanceType.Server:
                        //if (ServerDb == null) throw new NullReferenceException();
                        //return ServerDb.Exists(id, Type).Result;
                        break;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool Upsert(string id, T item, bool onlylocal, string change)
        {
            return Update(id, item,onlylocal,change);
        }


        public void Close()
        {
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        //LocalDb?.Dispose();
                        //LocalDbCollection = null;
                        break;
                    case DbInstanceType.MultipleFiles:
                        MultiFiles?.Close();
                        MultiFiles?.DisposeSecondayPath();
                        MultiFiles = null;
                        break;
                    case DbInstanceType.Server:
                        ServerDb = null;
                        break;
                }
            }
            catch
            {
            }
        }

        public int Count()
        {
            try
            {
                switch (InstanceType)
                {
                    case DbInstanceType.LiteDb:
                        //if (LocalDbCollection == null) throw new NullReferenceException();
                        //return LocalDbCollection.Count();
                        return -1;
                    case DbInstanceType.MultipleFiles:
                        if (MultiFiles == null) throw new NullReferenceException();
                        return MultiFiles.Count();
                    case DbInstanceType.Server:
                        //if (ServerDb == null) throw new NullReferenceException();
                        //return ServerDb.Count(Type).Result;
                        break;
                }

                return 0;
            }
            catch
            {
                return 0;
            }
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