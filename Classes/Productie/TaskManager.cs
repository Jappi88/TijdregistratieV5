using ProductieManager.Mailing;
using ProductieManager.Misc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace ProductieManager.Productie
{
    public class TaskManager : IQueue
    {
        public Manager Parent { get; set; }
        public object ProductieData { get; set; }
        public bool CleanUp { get; set; }

        public TaskManager(Manager parent, object data)
        {
            Parent = parent;
            ProductieData = data;
        }

        public bool IsPrioritize { get; set; }

        public bool ReQueue { get; set; }

        public int ID { get { return this.GetHashCode(); } }

        public RemoteMessage[] Results { get; set; }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public bool CheckEquals(IQueue queue)
        {
            return queue.ID == this.ID;
        }

        public Task DoWork()
        {
            return Task.Factory.StartNew(Process);
        }

        private RemoteMessage[] ProcessFile(byte[] data)
        {
            if (data != null && data.Length > 0)
            {
                List<RemoteMessage> pfs = new List<RemoteMessage> { };
                if (data.IsImage())
                {
                    ProductieFormulier pf = ProductieFormulier.FromImage(data, Parent);
                    if (pf != null)
                    {
                        pfs.Add(new RemoteMessage($"{pf.ProductieNr} toegevoegd!", MessageAction.NieweProductie, MsgType.Success, data, pf, pf.ProductieNr));
                    }
                }
                else
                {
                    //pdf?
                    var pfss = ProductieFormulier.FromPdf(data, Parent);
                    if (pfss != null && pfss.Length > 0)
                    {
                        pfs.AddRange(pfss.Select(x => new RemoteMessage($"{x.ProductieNr} toegevoegd!", MessageAction.NieweProductie, MsgType.Success, data, x, x.ProductieNr)));
                    }
                }
                string naam = pfs.Count == 1 ? $"productiebon [{pfs[0].ProductieNummer}]" : $"{pfs.Count} productiebonnen ";
                return pfs.ToArray();
            }
            return null;
        }

        private void Process()
        {
            try
            {
                List<RemoteMessage> rms = new List<RemoteMessage> { };
                if (ProductieData is byte[])
                {
                    RemoteMessage[] msgs = ProcessFile(ProductieData as byte[]);
                    if (msgs != null && msgs.Length > 0)
                        rms.AddRange(msgs);
                }
                else if (ProductieData is string)
                {
                    string fp = ProductieData as string;
                    if (File.Exists(fp))
                    {
                        RemoteMessage[] msgs = ProcessFile(File.ReadAllBytes(fp));
                        if (msgs != null && msgs.Length > 0)
                            rms.AddRange(msgs);
                    }
                }
                else if (ProductieData is string[])
                {
                    string[] fps = ProductieData as string[];
                    foreach (string fp in fps)
                        if (File.Exists(fp))
                        {
                            RemoteMessage[] msgs = ProcessFile(File.ReadAllBytes(fp));
                            if (msgs != null && msgs.Length > 0)
                                rms.AddRange(msgs);
                        }
                }
                else if (ProductieData is RemoteMessage)
                {
                    RemoteMessage msg = ProductieData as RemoteMessage;
                    if (msg.Value is byte[])
                    {
                        RemoteMessage[] msgs = ProcessFile((byte[])msg.Value);
                        if (msgs != null && msgs.Length > 0)
                            rms.AddRange(msgs);
                    }
                }

                Results = AddProductieFromMessage(rms.ToArray());
                if (Results.Length > 0 && CleanUp)
                {
                    if (ProductieData is byte[] || ProductieData is RemoteMessage)
                        ProductieData = null;
                    else if (ProductieData is string)
                    {
                        string filepath = ProductieData as string;
                        string newdir = "\\Verwerkte Productieformulieren";
                        string fp = Path.GetDirectoryName(filepath) + newdir;
                        if (!Directory.Exists(fp))
                            Directory.CreateDirectory(fp);
                        string filename = Path.GetFileName(filepath);
                        string newfilename = $"{fp}\\{filename}_old";
                        int count = Directory.GetFiles(fp).Where(x => Path.GetFileName(x).ToLower() == newfilename.Split('[')[0].ToLower()).ToArray().Length;
                        if (count > 0)
                            newfilename = newfilename + $"[{count - 1}]";
                        if (!File.Exists(newfilename))
                            File.Move($"{fp}\\{filename}", newfilename);
                    }
                    else if (ProductieData is string[])
                    {
                        string[] files = ProductieData as string[];
                        string newdir = "\\Verwerkte Productieformulieren";
                        foreach (string file in files)
                        {
                            string fp = Path.GetDirectoryName(file) + newdir;
                            if (!Directory.Exists(fp))
                                Directory.CreateDirectory(fp);

                            string ext = Path.GetExtension(file);
                            string filename = Path.GetFileName(file).Replace(ext, "");

                            string newfilename = $"{fp}\\{filename}_old";
                            int count = Directory.GetFiles(fp).Where(x => (Path.GetFileName(x).Replace(ext, "").Split('[')[0] + ext).ToLower() == (($"{ filename}_old").Split('[')[0] + ext).ToLower()).ToArray().Length;
                            if (count > 0)
                                newfilename = newfilename + $"[{count}]";
                            newfilename += ext;
                            if (!File.Exists(newfilename))
                                File.Move(file, newfilename);
                        }
                    }
                }
            }
            catch { }
        }

        private RemoteMessage[] AddProductieFromMessage(RemoteMessage[] messages)
        {
            List<RemoteMessage> xreturn = messages.ToList();
            foreach (RemoteMessage message in messages)
            {
                ProductieFormulier form;

                switch (message.Action)
                {
                    case MessageAction.ProductieNotitie:
                        if (!string.IsNullOrEmpty(message.ProductieNummer))
                        {
                            var pr = Manager.Database.GetProductie(message.ProductieNummer);
                            pr.Notitie = message.Message;
                            if (!Manager.Database.UpSert(pr))
                                xreturn.Remove(message);
                        }
                        break;

                    case MessageAction.ProductieWijziging:
                        form = Manager.Database.GetProductie(message.ProductieNummer);
                        if (form != null)
                        {
                            PropertyInfo[] properties = typeof(ProductieFormulier).GetProperties();
                            string[] values = message.Message.Trim().Split('\n');
                            bool changed = false;
                            foreach (var v in values)
                            {
                                if (v.Length > 4 && v.Contains("="))
                                {
                                    string[] xs = v.Split('=');
                                    foreach (PropertyInfo p in properties)
                                    {
                                        if (p.CanWrite && p.Name.ToLower() == xs[0].Trim().ToLower())
                                        {
                                            object xset = xs[1].Trim().ToObjectValue(p.PropertyType);
                                            if (xset != null)
                                            {
                                                p.SetValue(form, xset);
                                                string eenheid = "";
                                                if (p.PropertyType.Equals(typeof(double)))
                                                    eenheid = "uur";
                                                if (xs[0].ToLower().Contains("aantal"))
                                                    eenheid = "stuk(s)";
                                                var r = new RemoteMessage($"{xs[0]} is gewijzigd naar {xs[1]} {eenheid}", MessageAction.ProductieWijziging, MsgType.Info, null, form, form.ProductieNr);
                                                xreturn.Add(r);
                                                changed = true;
                                            }
                                            break;
                                        }
                                    }
                                }
                            }
                            if (changed)
                                form.UpdateForm(true, false, "Productie Gewijzigd", true);
                            xreturn.Remove(message);
                        }
                        break;

                    case MessageAction.ProductieVerwijderen:
                        if (!Parent.RemoveProductie(message.ProductieNummer, true))
                            xreturn.Remove(message);
                        break;

                    case MessageAction.NieweProductie:
                        if (message.Value is ProductieFormulier)
                        {
                            ProductieFormulier pr = message.Value as ProductieFormulier;
                            if (pr != null && Manager.Database.Exist(message.Value as ProductieFormulier))
                            {
                                xreturn.Remove(message);
                                xreturn.Add(new RemoteMessage($"Productie [{pr.ProductieNr}] bestaat al, en kan niet nogmaals worden toegevoegd!", MessageAction.None, MsgType.Warning));
                            }
                        }
                        break;

                    case MessageAction.AlgemeneMelding:
                        break;

                    case MessageAction.GebruikerUpdate:
                        string[] lines = message.Message.Trim().Split('\n');
                        string nameline = lines.FirstOrDefault(x => x.ToLower().StartsWith("naam="));
                        if (nameline != null)
                        {
                            string name = nameline.Split('=')[1];
                            Settings.UserAccount acc = Manager.Database.GetAccount(name);
                            bool isnew = false;
                            if (acc == null)
                            {
                                acc = new Settings.UserAccount();
                                acc.Username = name;
                                isnew = true;
                            }
                            bool isvalid = true;
                            foreach (string line in lines)
                            {
                                if (line.ToLower().StartsWith("naam="))
                                    continue;
                                string[] values;
                                if (line.ToLower().StartsWith("pass="))
                                {
                                    values = line.Split('=');
                                    if (values.Length > 1)
                                    {
                                        string pass = values[1].Trim();
                                        if (pass.Length >= 6)
                                        {
                                            acc.Password = pass;
                                            isvalid &= true;
                                        }
                                        else isvalid = false;
                                    }
                                }
                                else if (line.ToLower().StartsWith("level="))
                                {
                                    values = line.Split('=');
                                    if (values.Length > 1)
                                    {
                                        string level = values[1].Trim();
                                        int xlevel = -1;
                                        if (int.TryParse(level, out xlevel))
                                        {
                                            if (xlevel > 3)
                                                xlevel = 3;
                                            acc.AccesLevel = (AccesType)xlevel;
                                            isvalid &= true;
                                        }
                                        else isvalid = false;
                                    }
                                }
                            }

                            if (isvalid && Manager.Database.UpSert(acc, "Gewijziged vanuit email."))
                            {
                                string xmessage = null;
                                if (isnew)
                                    xmessage = $"Gebruiker: '{acc.Username}' is zojuist toegevoegd!";
                                else xmessage = $"Gebruiker: '{acc.Username}' is zojuist aangepast!";
                                xreturn.Remove(message);
                                xreturn.Add(new RemoteMessage(xmessage, MessageAction.GebruikerUpdate, MsgType.Success, null, acc));
                            }
                        }
                        break;
                }
            }
            return xreturn.ToArray();
        }
    }
}