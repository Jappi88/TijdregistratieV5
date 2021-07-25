using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;
using Rpm.Various;

namespace Rpm.SqlLite
{
    public class SqlDatabase
    {
        public static object Locker = new();

        private readonly BackgroundWorker _serverwatcher;

        public SqlDatabase() : this("RPM")
        {
        }

        public SqlDatabase(string dbname)
        {
            DbName = dbname;
            _serverwatcher = new BackgroundWorker();
            _serverwatcher.WorkerReportsProgress = true;
            _serverwatcher.WorkerSupportsCancellation = true;
            _serverwatcher.DoWork += _serverwatcher_DoWork;
            _serverwatcher.ProgressChanged += _serverwatcher_ProgressChanged;
        }

        public Dictionary<DbType, DateTime> LastUpdated { get; } = new();

        public string DbName { get; }

        //public SqlConnection Connection { get; private set; }
        public int SyncInterval { get; set; } = 1000;
        public bool IsSyncing { get; private set; }

        private void _serverwatcher_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if (e.UserState != null && e.UserState is List<SqlDataEntry> entries) OnEntryChanged(entries);
        }

        private async void _serverwatcher_DoWork(object sender, DoWorkEventArgs e)
        {
            while (true)
            {
                if (_serverwatcher.CancellationPending) break;
                IsSyncing = true;
                await Task.WhenAll(
                    UpdateEntries(DbType.Producties),
                    UpdateEntries(DbType.Medewerkers),
                    UpdateEntries(DbType.Opties),
                    UpdateEntries(DbType.Accounts));
                Thread.Sleep(SyncInterval);
            }

            IsSyncing = false;
        }

        public void StartSync()
        {
            if (IsSyncing && _serverwatcher.IsBusy)
                return;
            _serverwatcher.RunWorkerAsync();
        }

        public void StopSync()
        {
            if (!_serverwatcher.IsBusy) return;
            _serverwatcher.CancelAsync();
        }

        private async Task<int> UpdateEntries(DbType type)
        {
            var dt = new DateTime(1800, 1, 1);
            if (LastUpdated.ContainsKey(type)) dt = LastUpdated[type];
            var entries = await GetLatestEntries(type, dt, DateTime.MaxValue);
            if (entries.Count > 0)
            {
                OnEntryChanged(entries);
                var done = await UpdateLocal(entries);
            }

            LastUpdated[type] = DateTime.Now;
            return 0;
        }

        private Task<int> UpdateLocal(List<SqlDataEntry> entries)
        {
            return Task.Run(async () =>
            {
                var count = 0;
                try
                {
                    foreach (var entry in entries)
                    {
                        if (entry.DataObject?.Length == 0) continue;
                        switch (entry.Type)
                        {
                            case DbType.GereedProducties:
                            case DbType.Producties:
                                var prod = entry.DataObject.DeCompress().DeSerialize<ProductieFormulier>();
                                if (prod == null) continue;
                                //var old = Manager.Database.GetProductie(prod.ProductieNr);
                                //if (old != null && old.LastChanged != null && prod.LastChanged != null && prod.LastChanged.ServerUpdated <= old.LastChanged.ServerUpdated)
                                //    continue;
                                //if (prod.LastChanged == null && old.LastChanged != null) continue;
                                prod.LastChanged = prod.LastChanged.UpdateChange("Updated from server",
                                    prod.State == ProductieState.Gereed ? DbType.GereedProducties : DbType.Producties);
                                prod.LastChanged.ServerUpdated = DateTime.Now;
                                await Manager.Database.UpSert(prod, false);
                                count++;
                                break;
                            case DbType.Changes:
                                break;
                            case DbType.Medewerkers:
                                var pers = entry.DataObject.DeCompress().DeSerialize<Personeel>();
                                if (pers == null) continue;
                                //var oldpers = Manager.Database.GetPersoneel(pers.PersoneelNaam);
                                //if (oldpers != null && oldpers.LastChanged != null && pers.LastChanged != null && pers.LastChanged.ServerUpdated <= oldpers.LastChanged.ServerUpdated)
                                //    continue;
                                //if (pers.LastChanged == null && oldpers.LastChanged != null) continue;
                                pers.LastChanged =
                                    pers.LastChanged.UpdateChange("Updated from server", DbType.Medewerkers);
                                pers.LastChanged.ServerUpdated = DateTime.Now;
                                await Manager.Database.UpSert(pers,
                                    $"{pers.PersoneelNaam} is geupdated vanuit de server.", false);
                                count++;
                                break;
                            case DbType.Opties:
                                var settings = entry.DataObject.DeCompress().DeSerialize<UserSettings>();
                                if (settings == null) continue;
                                //var oldset = Manager.Database.GetSetting(settings.Username);
                                //if (oldset != null && oldset.LastChanged != null && settings.LastChanged != null && settings.LastChanged.ServerUpdated <= oldset.LastChanged.ServerUpdated)
                                //    continue;
                                //if (settings.LastChanged == null && oldset.LastChanged != null) continue;
                                settings.LastChanged =
                                    settings.LastChanged.UpdateChange("Updated from server", DbType.Opties);
                                settings.LastChanged.ServerUpdated = DateTime.Now;
                                await Manager.Database.UpSert(settings, false);
                                count++;
                                break;
                            case DbType.Accounts:
                                var acc = entry.DataObject.DeCompress().DeSerialize<UserAccount>();
                                if (acc == null) continue;
                                //var oldcc = Manager.Database.GetSetting(acc.Username);
                                //if (oldcc != null && oldcc.LastChanged != null && acc.LastChanged != null && acc.LastChanged.ServerUpdated <= oldcc.LastChanged.ServerUpdated)
                                //    continue;
                                //if (acc.LastChanged == null && oldcc.LastChanged != null) continue;
                                acc.LastChanged = acc.LastChanged.UpdateChange("Updated from server", DbType.Accounts);
                                acc.LastChanged.ServerUpdated = DateTime.Now;
                                await Manager.Database.UpSert(acc, false);
                                count++;
                                break;
                            case DbType.Messages:
                                break;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

                return count;
            });
        }

        public SqlConnection Connection()
        {
            var sConnB = new SqlConnectionStringBuilder
            {
                DataSource = "mssql-24126-0.cloudclusters.net,24126",
                InitialCatalog = "RPM",
                UserID = "ProductieManager",
                Password = "Valkrpm01",
                AsynchronousProcessing = true,
                MaxPoolSize = 999
            };
            return new SqlConnection(sConnB.ConnectionString);
        }

        public Task<SqlDataEntry[]> GetAllEntries(DbType type)
        {
            return Task.Run(() =>
            {
                lock (Locker)
                {
                    var entries = new List<SqlDataEntry>();
                    using (var connection = Connection())
                    {
                        var dbname = Enum.GetName(typeof(DbType), type);
                        using (var command = new SqlCommand($"SELECT * FROM [dbo].{dbname}", connection))
                        {
                            try
                            {
                                connection.Open();
                                using (var Locationreader = command.ExecuteReader())
                                {
                                    while (Locationreader.Read())
                                    {
                                        var entry = ReadDataEntry(Locationreader);
                                        if (entry != null)
                                        {
                                            entry.Type = type;
                                            entries.Add(entry);
                                        }
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine(ex.Message);
                            }
                            finally
                            {
                                connection.Close();
                            }
                        }
                    }

                    return entries.ToArray();
                }
            });
        }

        public Task<List<SqlDataEntry>> GetLatestEntries(DbType type, DateTime from, DateTime tot)
        {
            return Task.Run(() =>
            {
                var entries = new List<SqlDataEntry>();
                if (from.Year < 1800)
                    from = new DateTime(1800, 1, 1);
                using (var connection = Connection())
                {
                    var dbname = Enum.GetName(typeof(DbType), type);
                    using (var command =
                        new SqlCommand(
                            $"SELECT * FROM [dbo].{dbname} WHERE LastChanged >= @LastChanged AND LastChanged <= @Tot AND ChangedBy != @ChangedBy",
                            connection))
                    {
                        try
                        {
                            command.Parameters.Add(@"LastChanged", SqlDbType.DateTime).Value = from;
                            command.Parameters.Add(@"Tot", SqlDbType.DateTime).Value = tot;
                            command.Parameters.Add(@"ChangedBy", SqlDbType.VarChar, 50).Value = Manager.SystemID;
                            connection.Open();
                            using (var Locationreader = command.ExecuteReader())
                            {
                                while (Locationreader.Read())
                                {
                                    var entry = ReadDataEntry(Locationreader);
                                    if (entry != null)
                                    {
                                        entry.Type = type;
                                        entries.Add(entry);
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }

                return entries;
            });
        }

        public Task<List<T>> GetLatestEntries<T>(DbType type, DateTime from, DateTime tot)
        {
            return Task.Run(async () =>
            {
                var entries = new List<T>();
                var lastentries = await GetLatestEntries(type, from, tot);
                if (lastentries?.Count > 0)
                    foreach (var xlast in lastentries)
                        try
                        {
                            var inst = xlast.DataObject.DeCompress().DeSerialize<T>();
                            if (inst != null)
                                entries.Add(inst);
                        }
                        catch
                        {
                        }

                return entries;
            });
        }

        public Task<bool> Exists(string name, DbType type)
        {
            return Task.Run(() =>
            {
                using (var connection = Connection())
                {
                    try
                    {
                        var dbname = Enum.GetName(typeof(DbType), type);
                        var count = 0;
                        using (var command = new SqlCommand($"SELECT COUNT(*) FROM [dbo].{dbname} WHERE Name='{name}'",
                            connection))
                        {
                            connection.Open();
                            count = (int) command.ExecuteScalar();
                        }

                        return count > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                return false;
            });
        }

        public Task<T> GetInstance<T>(string name)
        {
            return Task.Run(() =>
            {
                var xreturn = default(T);
                using (var connection = Connection())
                {
                    try
                    {
                        var type = Functions.GetInstanceType<T>();
                        var dbname = Enum.GetName(typeof(DbType), type);

                        using (var command = new SqlCommand($"SELECT * FROM [dbo].{dbname} WHERE Name='{name}'",
                            connection))
                        {
                            connection.Open();
                            using (var Locationreader = command.ExecuteReader())
                            {
                                while (Locationreader.Read())
                                {
                                    var entry = ReadDataEntry(Locationreader);
                                    if (entry?.DataObject != null)
                                    {
                                        xreturn = entry.DataObject.DeCompress().DeSerialize<T>();
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                return xreturn;
            });
        }

        public Task<List<T>> GetInstances<T>(string[] names)
        {
            return Task.Run(() =>
            {
                var xreturn = new List<T>();
                using (var connection = Connection())
                {
                    try
                    {
                        var type = Functions.GetInstanceType<T>();
                        var dbname = Enum.GetName(typeof(DbType), type);

                        using (var command =
                            new SqlCommand(
                                $"SELECT * FROM [dbo].{dbname} WHERE {string.Join(" OR ", names.Select(x => $"Name='{x}'"))}",
                                connection))
                        {
                            connection.Open();
                            using (var Locationreader = command.ExecuteReader())
                            {
                                while (Locationreader.Read())
                                {
                                    var entry = ReadDataEntry(Locationreader);
                                    if (entry?.DataObject != null)
                                    {
                                        var instance = entry.DataObject.DeCompress().DeSerialize<T>();
                                        if (instance != null)
                                            xreturn.Add(instance);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                return xreturn;
            });
        }

        public Task<List<T>> GetAllInstances<T>()
        {
            return Task.Run(() =>
            {
                var xreturn = new List<T>();
                using (var connection = Connection())
                {
                    try
                    {
                        var type = Functions.GetInstanceType<T>();
                        var dbname = Enum.GetName(typeof(DbType), type);

                        using (var command = new SqlCommand($"SELECT * FROM [dbo].{dbname}", connection))
                        {
                            connection.Open();
                            using (var Locationreader = command.ExecuteReader())
                            {
                                while (Locationreader.Read())
                                {
                                    var entry = ReadDataEntry(Locationreader);
                                    if (entry?.DataObject != null)
                                    {
                                        var instance = entry.DataObject.DeCompress().DeSerialize<T>();
                                        if (instance != null)
                                            xreturn.Add(instance);
                                    }
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }

                return xreturn;
            });
        }

        public Task<bool> Upsert<T>(T instance, string name, DbType type)
        {
            return Task.Run(() =>
            {
                lock (Locker)
                {
                    using var connection = Connection();
                    try
                    {
                        //if (type == DbType.GereedProducties)
                        //    type = DbType.Producties;
                        var dbname = Enum.GetName(typeof(DbType), type);

                        var query = $"IF EXISTS(SELECT * FROM [dbo].{dbname} WHERE Name=@Name)" +
                                    $" UPDATE [dbo].{dbname} SET Name=@Name, LastChanged=@LastChanged, ChangedBy=@ChangedBy, DataObject=@DataObject WHERE Name=@Name" +
                                    " ELSE" +
                                    $" INSERT INTO [dbo].{dbname}(Name, LastChanged, ChangedBy, DataObject) VALUES(@Name, @LastChanged, @ChangedBy, @DataObject)";

                        var rowsAffected = 0;
                        using var cmd = new SqlCommand(query, connection);
                        //cmd.Parameters.Add("@Id", SqlDbType.Int).Value = 0;
                        cmd.Parameters.Add("@Name", SqlDbType.VarChar, 50).Value = name;
                        cmd.Parameters.Add("@LastChanged", SqlDbType.DateTime).Value = DateTime.Now;
                        cmd.Parameters.Add("@ChangedBy", SqlDbType.VarChar, 16).Value = Manager.SystemID;
                        cmd.Parameters.Add("@DataObject", SqlDbType.Binary).Value =
                            instance.Serialize().Compress();
                        connection.Open();
                        rowsAffected = cmd.ExecuteNonQuery();

                        return rowsAffected > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                    finally
                    {
                        connection.Close();
                    }

                    return false;
                }
            });
        }

        public Task<bool> Delete(string name, DbType type)
        {
            return Task.Run(() =>
            {
                lock (Locker)
                {
                    using (var connection = Connection())
                    {
                        try
                        {
                            //if (type == DbType.GereedProducties)
                            //    type = DbType.Producties;
                            var dbname = Enum.GetName(typeof(DbType), type);
                            var query = $"DELETE * FROM [dbo].{dbname} WHERE Name='{name}'";
                            var rowsAffected = 0;
                            using (var cmd = new SqlCommand(query, connection))
                            {
                                connection.Open();
                                rowsAffected = cmd.ExecuteNonQuery();
                            }

                            return rowsAffected > 0;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return false;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            });
        }

        public Task<int> DeleteAll(DbType type)
        {
            return Task.Run(() =>
            {
                lock (Locker)
                {
                    using (var connection = Connection())
                    {
                        try
                        {
                            //if (type == DbType.GereedProducties)
                            //    type = DbType.Producties;
                            var dbname = Enum.GetName(typeof(DbType), type);
                            var query = $"DELETE * FROM [dbo].{dbname}";
                            var rowsAffected = 0;
                            using (var cmd = new SqlCommand(query, connection))
                            {
                                connection.Open();
                                rowsAffected = cmd.ExecuteNonQuery();
                            }

                            return rowsAffected;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return -1;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            });
        }

        public Task<int> Delete(string[] names, DbType type)
        {
            var rowsAffected = 0;
            return Task.Run(() =>
            {
                lock (Locker)
                {
                    using (var connection = Connection())
                    {
                        try
                        {
                            //if (type == DbType.GereedProducties)
                            //    type = DbType.Producties;
                            var dbname = Enum.GetName(typeof(DbType), type);
                            var query =
                                $"DELETE * FROM [dbo].{dbname} WHERE {string.Join(", ", names.Select(x => $"Name='{x}'"))}";

                            using (var cmd = new SqlCommand(query, connection))
                            {
                                connection.Open();
                                rowsAffected = cmd.ExecuteNonQuery();
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }

                    return rowsAffected;
                }
            });
        }

        private SqlDataEntry ReadDataEntry(SqlDataReader reader)
        {
            SqlDataEntry entry = null;
            try
            {
                entry = new SqlDataEntry
                {
                    Id = (int) reader["Id"],
                    Name = (string) reader["Name"],
                    LastChanged = (DateTime) reader["LastChanged"],
                    ChangedBy = (string) reader["ChangedBy"],
                    DataObject = (byte[]) reader["DataObject"]
                };
            }
            catch
            {
            }

            return entry;
        }

        public Task<int> Count(DbType type)
        {
            return Task.Run(() =>
            {
                lock (Locker)
                {
                    using (var connection = Connection())
                    {
                        try
                        {
                            //if (type == DbType.GereedProducties)
                            //    type = DbType.Producties;
                            var dbname = Enum.GetName(typeof(DbType), type);
                            var query = $"SELECT COUNT(*) FROM [dbo].{dbname}";
                            var rowsAffected = 0;
                            using (var cmd = new SqlCommand(query, connection))
                            {
                                connection.Open();
                                rowsAffected = (int) cmd.ExecuteScalar();
                            }

                            return rowsAffected;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.Message);
                            return 0;
                        }
                        finally
                        {
                            connection.Close();
                        }
                    }
                }
            });
        }

        public event ConnectionHandler ConnectionChanged;

        public void OnConnectionChanged(ConnectionState state)
        {
            ConnectionChanged?.Invoke(this, state);
        }

        public event EntryChangedHandler EntryChanged;

        public void OnEntryChanged(List<SqlDataEntry> entries)
        {
            EntryChanged?.Invoke(this, entries);
        }
    }

    public delegate void ConnectionHandler(SqlDatabase database, ConnectionState state);

    public delegate void EntryChangedHandler(SqlDatabase database, List<SqlDataEntry> entries);
}