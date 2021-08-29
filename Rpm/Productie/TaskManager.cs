using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Rpm.Mailing;
using Rpm.Misc;
using Rpm.Settings;
using Rpm.Various;

namespace Rpm.Productie
{
    public class TaskManager : IQueue
    {
        public TaskManager(Manager parent, object data)
        {
            Parent = parent;
            ProductieData = data;
        }

        public Manager Parent { get; set; }
        public object ProductieData { get; set; }
        public bool CleanUp { get; set; }

        public bool IsPrioritize { get; set; }

        public bool ReQueue { get; set; }

        public int Id => GetHashCode();

        public RemoteMessage[] Results { get; set; }

        public void Cancel()
        {
            throw new NotImplementedException();
        }

        public bool CheckEquals(IQueue queue)
        {
            return queue.Id == Id;
        }

        public Task DoWork()
        {
            return Task.Factory.StartNew(Process);
        }

        private static Task<List<RemoteMessage>> ProcessFile(byte[] data)
        {
            return Task.Run(async () =>
            {
                if (data != null && data.Length > 0)
                {
                    var pfs = new List<RemoteMessage>();
                    if (data.IsImage())
                    {
                        var pf = await ProductieFormulier.FromImage(data);
                        if (pf != null)
                            pfs.Add(new RemoteMessage($"{pf.ProductieNr} toegevoegd!", MessageAction.NieweProductie,
                                MsgType.Success, data, pf, pf.ProductieNr));
                        else
                            pfs.Add(new RemoteMessage("Ongeldige productie formulier!", MessageAction.AlgemeneMelding,
                                MsgType.Fout));
                    }
                    else
                    {
                        //pdf?
                        var pfss = await ProductieFormulier.FromPdf(data);
                        if (pfss != null && pfss.Count > 0)
                            pfs.AddRange(pfss
                                .Where(x => pfs.All(t => t.ProductieNummer.ToLower() != x.ProductieNr.ToLower()))
                                .Select(x => new RemoteMessage($"{x.ProductieNr} toegevoegd!",
                                    MessageAction.NieweProductie,
                                    MsgType.Success, data, x, x.ProductieNr)));
                    }

                    var naam = pfs.Count == 1
                        ? $"productiebon [{pfs[0].ProductieNummer}]"
                        : $"{pfs.Count} productiebonnen ";
                    return pfs;
                }

                return null;
            });
        }

        private async void Process()
        {
            try
            {
                var rms = new List<RemoteMessage>();
                if (ProductieData is byte[] data)
                {
                    var msgs = await ProcessFile(data);
                    if (msgs != null && msgs.Count > 0)
                        rms.AddRange(msgs.Where(x =>
                            rms.All(t => !string.Equals(t.ProductieNummer, x.ProductieNummer, StringComparison.CurrentCultureIgnoreCase))));
                }
                else if (ProductieData is string fp1)
                {
                    if (File.Exists(fp1))
                    {
                        var msgs = await ProcessFile(File.ReadAllBytes(fp1));
                        if (msgs != null && msgs.Count > 0)
                            rms.AddRange(msgs.Where(x =>
                                rms.All(t => !string.Equals(t.ProductieNummer, x.ProductieNummer, StringComparison.CurrentCultureIgnoreCase))));
                    }
                }
                else if (ProductieData is string[] fps)
                {
                    foreach (var fp in fps)
                        if (File.Exists(fp))
                        {
                            var msgs = await ProcessFile(File.ReadAllBytes(fp));
                            if (msgs != null && msgs.Count > 0)
                                rms.AddRange(msgs.Where(x =>
                                    rms.All(t => !string.Equals(t.ProductieNummer, x.ProductieNummer, StringComparison.CurrentCultureIgnoreCase))));
                        }
                }
                else if (ProductieData is RemoteMessage msg)
                {
                    if (msg.Value is byte[] bytes)
                    {
                        var msgs = await ProcessFile(bytes);
                        if (msgs != null && msgs.Count > 0)
                            rms.AddRange(msgs.Where(x =>
                                rms.All(t => !string.Equals(t.ProductieNummer, x.ProductieNummer, StringComparison.CurrentCultureIgnoreCase))));
                    }
                    else
                    {
                        rms.Add(msg);
                    }
                }

                Results = await UpdateProductieFromMessage(rms.ToArray());
            }
            catch
            {
            }
        }

        private async Task<RemoteMessage[]> UpdateProductieFromMessage(RemoteMessage[] messages)
        {
            var xreturn = messages.ToList();
            foreach (var message in messages)
            {
                switch (message.Action)
                {
                    case MessageAction.ProductieNotitie:
                        if (!string.IsNullOrEmpty(message.ProductieNummer))
                        {
                            var xpr = await Manager.Database.GetProductie(message.ProductieNummer);
                            xpr.Note ??= new NotitieEntry(message.Message, xpr);
                            xpr.Note.Notitie = message.Message;
                            if (!await Manager.Database.UpSert(xpr))
                                xreturn.Remove(message);
                        }

                        break;

                    case MessageAction.ProductieWijziging:
                        var form = await Manager.Database.GetProductie(message.ProductieNummer);
                        if (form != null)
                        {
                            var properties = typeof(ProductieFormulier).GetProperties();
                            var values = message.Message.Trim().Split('\n');
                            var changed = false;
                            foreach (var v in values)
                                if (v.Length > 4 && v.Contains("="))
                                {
                                    var xs = v.Split('=');
                                    foreach (var p in properties)
                                        if (p.CanWrite && string.Equals(p.Name, xs[0].Trim(), StringComparison.CurrentCultureIgnoreCase))
                                        {
                                            var xset = xs[1].Trim().ToObjectValue(p.PropertyType);
                                            if (xset != null)
                                            {
                                                p.SetValue(form, xset);
                                                var eenheid = "";
                                                if (p.PropertyType == typeof(double))
                                                    eenheid = "uur";
                                                if (xs[0].ToLower().Contains("aantal"))
                                                    eenheid = "stuk(s)";
                                                var r = new RemoteMessage(
                                                    $"{xs[0]} is gewijzigd naar {xs[1]} {eenheid}",
                                                    MessageAction.ProductieWijziging, MsgType.Info, null, form,
                                                    form.ProductieNr);
                                                xreturn.Add(r);
                                                changed = true;
                                            }

                                            break;
                                        }
                                }

                            if (changed)
                                await form.UpdateForm(true, false, null,
                                    $"[{form.ProductieNr}] Productie gewijzigd vanuit email");
                            xreturn.Remove(message);
                        }

                        break;

                    case MessageAction.ProductieVerwijderen:
                        if (!await Manager.RemoveProductie(message.ProductieNummer, true))
                            xreturn.Remove(message);
                        break;

                    case MessageAction.NieweProductie:
                        if (message.Value is ProductieFormulier pr) await Manager.AddProductie(pr);
                        break;
                    case MessageAction.AlgemeneMelding:
                        break;

                    case MessageAction.GebruikerUpdate:
                        var lines = message.Message.Trim().Split('\n');
                        var nameline = lines.FirstOrDefault(x => x.ToLower().StartsWith("naam="));
                        if (nameline != null)
                        {
                            var name = nameline.Split('=')[1];
                            var acc = await Manager.Database.GetAccount(name);
                            var isnew = false;
                            if (acc == null)
                            {
                                acc = new UserAccount {Username = name};
                                isnew = true;
                            }

                            var isvalid = true;
                            foreach (var line in lines)
                            {
                                if (line.ToLower().StartsWith("naam="))
                                    continue;
                                string[] values;
                                if (line.ToLower().StartsWith("pass="))
                                {
                                    values = line.Split('=');
                                    if (values.Length > 1)
                                    {
                                        var pass = values[1].Trim();
                                        if (pass.Length >= 6)
                                            acc.Password = pass;
                                        else isvalid = false;
                                    }
                                }
                                else if (line.ToLower().StartsWith("level="))
                                {
                                    values = line.Split('=');
                                    if (values.Length > 1)
                                    {
                                        var level = values[1].Trim();
                                        if (int.TryParse(level, out var xlevel))
                                        {
                                            if (xlevel > 3)
                                                xlevel = 3;
                                            acc.AccesLevel = (AccesType) xlevel;
                                        }
                                        else
                                        {
                                            isvalid = false;
                                        }
                                    }
                                }
                            }

                            if (isvalid && await Manager.Database.UpSert(acc, "Gewijziged vanuit email."))
                            {
                                string xmessage = null;
                                xmessage = isnew ? $"Gebruiker: '{acc.Username}' is zojuist toegevoegd!" : $"Gebruiker: '{acc.Username}' is zojuist aangepast!";
                                xreturn.Remove(message);
                                xreturn.Add(new RemoteMessage(xmessage, MessageAction.GebruikerUpdate, MsgType.Success,
                                    null, acc));
                            }
                        }

                        break;
                }
            }

            return xreturn.ToArray();
        }
    }
}