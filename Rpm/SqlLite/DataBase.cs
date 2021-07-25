using Polenter.Serialization;
using ProductieManager.Classes.Misc;
using ProductieManager.Classes.SqlLite;
using ProductieManager.Productie;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductieManager.Classes.Productie
{
    public class DataBase
    {
        public UserChange LastReadChange { get; set; }
        public Manager PManager { get; private set; }
        public double Version => 1.0;
        public string ID { get; set; }
        public DateTime LastChanged { get; set; }
        public int TableCount { get; private set; }
        public List<V1Table> Tables { get; set; }

        BackgroundWorker _worker;

        public DataBase(Manager manager)
        {
            PManager = manager;
        }

        #region ReadDatabase
        public void ReadDatabaseAsync()
        {
            if (_worker != null && _worker.IsBusy)
                return;
            _worker = new BackgroundWorker();
            _worker.DoWork += _worker_ReadDatabase;
            _worker.ProgressChanged += _worker_ReadDatabaseProgressChanged;
            _worker.RunWorkerCompleted += _worker_ReadDatabaseCompleted;
            _worker.RunWorkerAsync();
        }

        private void _worker_ReadDatabaseProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            ProgressChanged(e.UserState as ProgressArg);
        }

        private void _worker_ReadDatabaseCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ProgressArg arg = new ProgressArg();
            arg.Message = $"Loading Complete!";
            arg.Type = ProgressType.ReadCompleet;
            arg.Value = Tables;
            ProgressCompleted(arg);
        }

        private void _worker_ReadDatabase(object sender, DoWorkEventArgs e)
        {
            try
            {
                using (FileStream fs = GetDbStream())
                {
                    using (BinaryReader br = new BinaryReader(fs))
                    {
                        ProgressArg arg = new ProgressArg();
                        arg.Type = ProgressType.ReadBussy;

                        double version = br.ReadDouble();
                        ID = br.ReadString();
                        LastChanged = DateTime.FromBinary(br.ReadInt64());
                        TableCount = br.ReadInt32();
                        if (version == Version)
                        {
                            List<V1Table> tables = new List<V1Table>();
                            if (Tables != null && Tables.Count > 0)
                                Tables.Clear();
                            for (int i = 0; i < TableCount; i++)
                            {
                                arg.Pogress = ((i / TableCount) * 100);
                                if (arg.Pogress > 100)
                                    arg.Pogress = 100;
                                arg.Message = $"Loading Entries...[{i}/{TableCount}]";
                                _worker.ReportProgress(arg.Pogress, arg);
                                tables.Add(V1Table.Read(this, fs, fs.Position, true));
                            }
                            Tables = tables;
                        }
                    }
                }
            }
            catch (Exception ex) { throw ex; }
        }
        #endregion

        #region WriteDatabase

        public V1Table[] BuildDataBase(ProductieFormulier[] forms)
        {
            //Todo: Build new database from scratch.
            try
            {
                List<V1Table> tables = new List<V1Table> { };
                if (forms == null)
                    throw new Exception("Productieformulieren kunnen niet 'null' zijn.");
                foreach(ProductieFormulier form in forms)
                {
                    V1Table table = CreateTable(form);
                    if (table != null)
                        tables.Add(table);
                }

                if(RefreshTableOffsets(tables.ToArray()))
                {
                    //Todo: write tables + data
                    foreach (V1Table table in tables)
                    {
                        
                    }
                }
                return null;
            }
            catch (Exception ex) { throw ex; }
        }

        public V1Table CreateTable(ProductieFormulier form)
        {
            try
            {
                V1Table table = new V1Table(this);

                byte[] buffer = ProductieFormulier.Serialize(form);
                table.Entry.ArtikelNr = form.ArtikelNr;
                table.Entry.ProductieNr = form.ProductieNr;
                bool flag = table.Entry.Length != buffer.Length;
                table.Entry.Length = buffer.Length;
                table.Entry.LastChanged = DateTime.Now;
                table.Entry.ProductieHash = Crc32Algorithm.Compute(buffer, 0, buffer.Length);

                byte[] entry = table.EntryToByteArray();
                table.TableLength = entry.Length;
                return table;
            }
            catch { return null; }
        }

        #endregion

        #region Productie Management

        public ProductieFormulier GetProductie(string productienr)
        {
            try
            {
                if (Tables == null || Tables.Count == 0)
                    return null;
                // V1Table table = Tables.FirstOrDefault(x => x.Entry != null && x.Entry.ProductieNr.ToLower() == productienr.ToLower());
                V1Table table = null;
                int index = Tables.IndexOf(new V1Table(this) { Entry = new V1TableEntry() { ProductieNr = productienr } });
                if (index > -1)
                    table = Tables[index];
                if (table != null)
                {
                    ProductieFormulier form = null;
                    using (FileStream fs = GetDbStream())
                    {
                        form = V1Productie.Read(PManager, fs, table.Entry.Offset, table.Entry.Length, true).Productie;
                    }
                    return form;
                }
                return null;
            }
            catch { return null; }
        }

        public ProductieFormulier[] GetProducties(ProductieState state)
        {
            try
            {
                if (Tables == null || Tables.Count == 0)
                    throw new Exception();
                V1Table[] tables = Tables.Where(x => x.Entry != null && x.Entry.State == state).ToArray();
                if (tables.Length > 0)
                {
                    return GetProducties(tables);
                }
                throw new Exception();
            }
            catch { return new ProductieFormulier[] { }; }
        }

        public ProductieFormulier[] GetProducties(string crit)
        {
            try
            {
                if (Tables == null || Tables.Count == 0)
                    throw new Exception();
                V1Table[] tables = Tables.Where(x => x.Entry != null && ((x.Entry.ArtikelNr != null && x.Entry.ArtikelNr.ToLower() == crit.ToLower()) ||
                                                                         (x.Entry.ProductieNr != null && x.Entry.ProductieNr.ToLower() == crit.ToLower()))).ToArray();
                if (tables.Length > 0)
                {
                    return GetProducties(tables);
                }
                throw new Exception();
            }
            catch { return new ProductieFormulier[] { }; }
        }

        public ProductieFormulier[] GetProducties(V1Table[] tables)
        {
            try
            {
                if (tables == null || tables.Length == 0)
                    throw new Exception();
                List<ProductieFormulier> forms = new List<ProductieFormulier> { };
                using (FileStream fs = GetDbStream())
                {
                    foreach (V1Table table in tables)
                    {
                        ProductieFormulier form = V1Productie.Read(PManager, fs, table.Entry.Offset, table.Entry.Length, true).Productie;
                        if (form != null)
                            forms.Add(form);
                    }
                }
                return forms.ToArray();
            }
            catch { return new ProductieFormulier[] { }; }
        }
        #endregion

        #region DatabaseManagement
        public bool RefreshTableOffsets(V1Table[] tables)
        {
            //Todo
            //Fix all table offsets
            if(tables != null && tables.Length > 0)
            {
                long offset = 28; // start offset after header
                offset += tables.Sum(x => x.TableLength);
                for(int i = 0; i < Tables.Count; i++)
                {
                    Tables[i].Entry.Offset = offset;
                    offset += Tables[i].Entry.Length;
                }
                return true;
            }
            return false;
        }

        public V1Table GetTable(ProductieFormulier prod)
        {
            if (Tables == null || Tables.Count == 0)
                return null;
            try
            {
                int index = Tables.IndexOf(new V1Table(this) { Entry = new V1TableEntry() { ProductieNr = prod.ProductieNr } });
                if (index > -1)
                    return Tables[index];
                return null;
            }
            catch { return null; }
        }
        #endregion

        public static FileStream GetDbStream()
        {
            if (!File.Exists(Manager.DbPath))
                throw new Exception("Opgegeven bestand bestaat niet!");
            return new FileStream(Manager.DbPath, FileMode.Open, FileAccess.Read, FileShare.Read);
        }


        public event ProgressChangedHandler OnProgressChanged;
        public event ProgressChangedHandler OnProgressCompleted;

        public void ProgressChanged(ProgressArg arg)
        {
            OnProgressChanged?.Invoke(this, arg);
        }

        public void ProgressCompleted(ProgressArg arg)
        {
            OnProgressChanged?.Invoke(this, arg);
        }

        public event FormulierChangedHandler OnFormulierChanged;

        public void FormulierChanged(ProductieFormulier formulier)
        {
            OnFormulierChanged?.Invoke(formulier, false, false);
        }

    }

    public class V1Table
    {
        public DataBase Parent { get; set; }
        public int TableLength { get; set; }
        public V1TableEntry Entry { get; set; }

        public V1Table(DataBase parent)
        {
            Parent = parent;
            Entry = new V1TableEntry();
        }


        public static V1Table Read(DataBase parent, Stream input, long offset, bool advancelength)
        {
            try
            {
                long last = input.Position;
                input.Position = offset;
                V1Table pr = new V1Table(parent);
                pr.Parent = parent;
                BinaryReader br = new BinaryReader(input);
                pr.TableLength = br.ReadInt32();
                byte[] data = br.ReadBytes(pr.TableLength);
                if (!advancelength)
                    input.Position = last;
                SharpSerializer sh = new SharpSerializer(true);
                using (MemoryStream ms = new MemoryStream(data))
                {
                    pr.Entry = sh.Deserialize(ms) as V1TableEntry;
                }
                data = null;
                return pr;
            }
            catch (Exception ex) { throw ex; }
        }

        public byte[] ToByteArray()
        {
            byte[] xreturn = null;
            using (MemoryStream ms = new MemoryStream())
            {
                byte[] entrydata = EntryToByteArray();
                if(entrydata != null)
                {
                    using (BinaryWriter bw = new BinaryWriter(ms))
                    {
                        bw.Write(entrydata.Length);
                        bw.Write(entrydata);
                    }
                }
                xreturn = ms.ToArray();
            }
            return xreturn;
        }

        public byte[] EntryToByteArray()
        {
            try
            {
                byte[] xreturn = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    SharpSerializer sh = new SharpSerializer(true);
                    sh.Serialize(Entry, ms);
                    xreturn = ms.ToArray();
                }
                return xreturn;

            }
            catch { return null; }
        }

        public override bool Equals(object obj)
        {
            if (obj is V1Table)
                return (Entry != null && (Entry.ProductieNr.ToLower() == obj.ToString().ToLower()));
            else if (obj is string)
                return (Entry != null && (Entry.ProductieNr.ToLower() == obj.ToString().ToLower() || Entry.ArtikelNr.ToLower() == obj.ToString().ToLower()));
            else if (obj is int)
                return (Entry != null && Entry.ProductieHash == (int)obj);
            else return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public class V1TableEntry
    {
        public uint ProductieHash { get; set; }
        public ProductieState State { get; set; }
        public string ProductieNr { get; set; }
        public string ArtikelNr { get; set; }
        public DateTime LastChanged { get; set; }
        public long Offset { get; set; }
        public int Length { get; set; }


    }

    public class V1Productie
    {
        public ProductieFormulier Productie { get; set; }

        public static V1Productie Read(Manager manager, Stream input, long offset, int length, bool advancelength)
        {
            try
            {
                V1Productie pr = new V1Productie();
                long last = input.Position;
                input.Position = offset;
                byte[] xdata = new byte[length];
                input.Read(xdata, 0, length);
                if (!advancelength)
                    input.Position = last;
                pr.Productie = ProductieFormulier.DeSerializeFromData(xdata, manager);
                return pr;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
