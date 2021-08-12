using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using LiteDB;
using Polenter.Serialization;
using Rpm.Mailing;
using Rpm.SqlLite;
using Rpm.Misc;
using Rpm.Various;

namespace Rpm.Productie
{
    [DataContract]
    public class ProductieFormulier : IProductieBase
    {
        #region "Constructor"

        #endregion "Constructor"

        public override bool Equals(object obj)
        {
            if (!(obj is ProductieFormulier form))
                return false;
            return string.Equals(ProductieNr, form.ProductieNr, StringComparison.CurrentCultureIgnoreCase);
        }

        public override int GetHashCode()
        {
            return ProductieNr.GetHashCode();
        }

        #region "Variables"

        public const double CurrentVersie = 2.1;

        internal ProductieState _state;

        public override ProductieState State
        {
            get => GetCurrentState();
            set => _state = value;
        }

        [BsonId]
        public override string ProductieNr
        {
            get => base.ProductieNr;
            set => base.ProductieNr = value;
        }

        public override string Path => ProductieNr;

        public override string Naam
        {
            get => ProductieNr;
            set => base.Naam = value;
        }

        internal double _versie;

        public double Versie
        {
            get => _versie <= 0 ? CurrentVersie : _versie;
            private set => _versie = value;
        } //= _Versie;

        [ExcludeFromSerialization]
        public int AantalProducties { get; set; }

        public override ProductieFormulier Root => this;
        public override string WerkplekkenName => string.Join(", ", GetAlleWerkplekken().Select(x => x.Naam));
        public override string PersoneelNamen => string.Join(", ", Personen.Select(x => x.PersoneelNaam));

        private Bewerking[] _bewerkingen;

        public Bewerking[] Bewerkingen
        {
            get { return _bewerkingen ?? new Bewerking[] { }; }
            set => _bewerkingen = value;
            //UpdateBewerkingen();
        }

        //public override string Omschrijving
        //{
        //    get => base.Omschrijving;//.WrapText(150);
        //    set => base.Omschrijving = value;
        //}

        public List<Materiaal> Materialen { get; set; } = new List<Materiaal>();

        [ExcludeFromSerialization]
        public int ViewImageIndex { get; set; }

        private int _aantal;
        public override int Aantal
        {
            get => _aantal;
            set
            {
                if (Materialen != null && _aantal != value)
                {
                    for (int i = 0; i < Materialen.Count; i++)
                    {
                        var mat = Materialen[i];
                        var ps = mat.AantalPerStuk;
                        if (ps == 0 && mat._aantal > 0)
                        {
                            ps = mat._aantal / value;
                            mat.AantalPerStuk = ps;
                        }
                        mat._aantal = value * ps;
                    }
                }

                _aantal = value;
            }
        }
        private int _aantalgemaakt;

        public override int AantalGemaakt
        {
            get
            {
                return Bewerkingen == null || Bewerkingen.Length == 0
                    ? _aantalgemaakt
                    : Bewerkingen.Sum(x => x.AantalGemaakt) / Bewerkingen.Length;
            }
            set
            {
                _aantalgemaakt = value;
                //if (Bewerkingen != null)
                //{
                //    foreach (var bew in Bewerkingen)
                //        bew.AantalGemaakt = value;
                //}
            }
        }

        public override int TotaalGemaakt
        {
            get
            {
                return Bewerkingen == null || Bewerkingen.Length == 0
                    ? _aantalgemaakt
                    : Bewerkingen.Sum(x => x.TotaalGemaakt) / Bewerkingen.Length;
            }
        }

        public override int AantalTeMaken
        {
            get
            {
                return Bewerkingen == null || Bewerkingen.Length == 0
                    ? Aantal
                    : Bewerkingen.Sum(x => x.AantalTeMaken) / Bewerkingen.Length;
            }

        }

        public override int AantalNogTeMaken
        {
            get
            {
                return Bewerkingen == null || Bewerkingen.Length == 0
                    ? Aantal
                    : Bewerkingen.Sum(x => x.AantalNogTeMaken) / Bewerkingen.Length;
            }

        }

        public override int DeelsGereed => Bewerkingen == null || Bewerkingen.Length == 0? 0 : Bewerkingen.Sum(x=> x.DeelsGereed) / Bewerkingen.Length;

        public int AantalPersonen
        {
            get { return Bewerkingen?.Sum(x => x.GetPersoneel().Length) ?? 0; }
        }
        
        public override Personeel[] Personen => Bewerkingen?.Length == 0
            ? new Personeel[0]
            : Bewerkingen?.SelectMany(x => x.Personen).ToArray();

        private string _note;
        private string _gereednote;
        [ExcludeFromSerialization]
        public string Notitie
        {
            get => _note;
            set
            {
                _note = value;
                if (Note == null && !string.IsNullOrEmpty(value))
                    Note = new NotitieEntry(value, this);
            }
        }
        [ExcludeFromSerialization]
        public string GereedNotitie
        {
            get => _gereednote;
            set
            {
                _gereednote = value;
                if (GereedNote == null && !string.IsNullOrEmpty(value))
                    GereedNote = new NotitieEntry(value, this) { Type = NotitieType.ProductieGereed, Naam = Paraaf };
            }
        }

        //public NotitieEntry GereedNote
        //{
        //    get => _gereednote ??= !string.IsNullOrEmpty(GereedNotitie)
        //        ? new NotitieEntry(GereedNotitie, NotitieType.ProductieGereed)
        //        : null;
        //    set
        //    {
        //        _gereednote = value;
        //        if (_gereednote != null)
        //        {
        //            // _gereednote = value.CreateCopy();
        //            _gereednote.Type = NotitieType.ProductieGereed;
        //            // _gereednote.Path = Path;
        //        }
        //    }
        //}

        //public NotitieEntry Note
        //{
        //    get => _note ??= !string.IsNullOrEmpty(Notitie)
        //        ? new NotitieEntry(Notitie, NotitieType.Productie)
        //        : null;
        //    set
        //    {
        //        _note = value;
        //        if (_note != null)
        //        {
        //            //_note = value.CreateCopy();
        //            _note.Type = NotitieType.Productie;
        //            //_note.Path = Path;
        //        }
        //    }
        //}

        // public override bool IsNieuw => false;

        #endregion "Variables"

        #region "Data Management"

        //public static byte[] Serialize(ProductieFormulier prod)
        //{
        //    try
        //    {
        //        byte[] xreturn = null;
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            SharpSerializer sh = new SharpSerializer(true);
        //            //sh.PropertyProvider = new ProductieFormulierProvider();
        //            sh.Serialize(prod, ms);
        //            xreturn = ms.ToArray();
        //        }
        //        return xreturn;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public static ProductieFormulier DeSerializeFromData(byte[] data, Manager manager)
        //{
        //    try
        //    {
        //        if (data == null || data.Length == 0)
        //            return null;
        //        ProductieFormulier xreturn = null;
        //        using (MemoryStream ms = new MemoryStream(data))
        //        {
        //            //BinaryFormatter bf = new BinaryFormatter();
        //            SharpSerializer sh = new SharpSerializer(true);
        //            //sh.PropertyProvider = new ProductieFormulierProvider();
        //            xreturn = sh.Deserialize(ms) as ProductieFormulier;
        //            if (xreturn != null)
        //                foreach (var x in xreturn.Bewerkingen)
        //                {
        //                    x.OnBewerkingChanged += xreturn.BewerkingChanged;
        //                }
        //        }
        //        return xreturn;
        //    }
        //    catch (Exception ex) { return null; }
        //}

        public Task<bool> UpdateFrom(ProductieFormulier form, string change = null)
        {
            return Task.Run(async () =>
            {
                if (form == null ||
                    !string.Equals(form.ProductieNr, ProductieNr, StringComparison.CurrentCultureIgnoreCase))
                    return false;

                try
                {
                    var xreturn = false;
                    if (form.Bewerkingen != null && form.Bewerkingen.Length > 0)
                    {
                        var bws = Bewerkingen.ToList();
                        foreach (var bew in form.Bewerkingen)
                        {
                            var mybew = Bewerkingen.FirstOrDefault(x => x.Equals(bew));
                            if (bew.LastChanged != null &&
                                (mybew == null ||
                                 bew.LastChanged != null && mybew.LastChanged == null ||
                                 mybew.LastChanged.TimeChanged < bew.LastChanged.TimeChanged))
                            {
                                if (mybew != null)
                                    bws.Remove(mybew);
                                // bew.IsBemand = mybew.GetBemand();
                                bws.Add(bew);
                                xreturn = true;
                            }
                        }

                        if (xreturn)
                        {
                            Bewerkingen = bws.ToArray();
                            await UpdateForm(true, false, null, change ?? $"[{ProductieNr}] Updated.");
                        }
                    }

                    return xreturn;
                }
                catch (Exception)
                {
                    return false;
                }
            });
        }

        public async void UpdateBemand(bool save)
        {
            if (Bewerkingen == null) return;
            foreach (var bew in Bewerkingen) bew.IsBemand = bew.GetBemand();
            if (save)
                await UpdateForm(true, true);
        }

        public async Task<bool> UpdateVersion()
        {
            try
            {
                if (_versie < 2.0)
                {
                    if (Bewerkingen != null && Bewerkingen.Length > 0)
                        foreach (var bew in Bewerkingen)
                        {
                            //update productie 'isbemand' variable
                            var ent = Manager.BewerkingenLijst.GetEntry(bew.Naam.Split('[')[0]);
                            if (ent != null)
                                bew.IsBemand = ent.IsBemand;

                            //update alle personeel klusjes.
                            if (bew.WerkPlekken.Count > 0 && bew.State == ProductieState.Gestart)
                                foreach (var wp in bew.WerkPlekken)
                                {
                                    var pers = wp.Personen;
                                    foreach (var per in pers)
                                    {
                                        var klus = new Klus(per, wp);
                                        klus.UpdateTijdGewerkt(wp.TijdGestart, wp.TijdGestopt, false);
                                        klus.Start();
                                        per.ReplaceKlus(klus);
                                        var xper = await Manager.Database.GetPersoneel(per.PersoneelNaam);
                                        if (xper != null)
                                        {
                                            xper.ReplaceKlus(klus);
                                            per.VrijeDagen = xper.VrijeDagen;
                                            await Manager.Database.UpSert(xper, $"{xper.PersoneelNaam} klus update");
                                        }
                                    }
                                }
                        }

                    _versie = CurrentVersie;
                    await UpdateForm(true, false, null, $"[{ProductieNr}]Productie geupdate naar versie {_versie}");
                    return true;
                }

                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool CopyFromInstance(ProductieFormulier form)
        {
            if (form == null)
                return false;
            var types = form.GetType().GetProperties().Where(t => t.CanWrite).ToArray();
            foreach (var type in types)
                type.SetValue(this, type.GetValue(form));
            return true;
        }

        public static Task<List<ProductieFormulier>> FromPdf(byte[] data)
        {
            return Task.Run(async () =>
            {
                try
                {
                    var pfs = new List<ProductieFormulier>();
                    using var ms = new MemoryStream(data);
                    using var reader = new PdfReader(data);
                    var txt = "";
                    for (var i = 1; i <= reader.NumberOfPages; i++)
                    {
                        ProductieFormulier pf = null;
                        var pdftext = PdfTextExtractor.GetTextFromPage(reader, i);
                        if (string.IsNullOrEmpty(pdftext))
                        {
                            var pg = reader.GetPageN(i);
                            var res = PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES)) as PdfDictionary;
                            if (res != null)
                            {
                                var xobj = PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT)) as PdfDictionary;
                                if (xobj == null) continue;

                                var keys = xobj.Keys;
                                if (keys.Count == 0) continue;

                                var obj = xobj.Get(keys.ElementAt(0));
                                if (!obj.IsIndirect()) continue;

                                var tg = PdfReader.GetPdfObject(obj) as PdfDictionary;
                                var type = PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE)) as PdfName;
                                if (!PdfName.IMAGE.Equals(type)) continue;

                                var XrefIndex = ((PRIndirectReference) obj).Number;
                                var pdfStream = reader.GetPdfObject(XrefIndex) as PRStream;
                                var xdata = PdfReader.GetStreamBytesRaw(pdfStream);
                                pf = await FromImage(xdata);
                            }

                            if (pf != null &&
                                !pfs.Any(t => string.Equals(t.ProductieNr.ToLower(), pf.ProductieNr.ToLower(),
                                    StringComparison.CurrentCultureIgnoreCase)))
                                pfs.Add(pf);
                        }
                        else
                        {
                            if (txt?.Length > 0)
                            {
                                var xend = txt.LastIndexOf("Verpakkingsinstructie", StringComparison.Ordinal);
                                txt = (xend > 0 ? txt.Substring(0, xend) : txt).TrimEnd('\n') + "\n";

                                var xstart = pdftext.LastIndexOf("...................", StringComparison.Ordinal) + 19;
                                var piece = pdftext.Substring(xstart, pdftext.Length - xstart).TrimStart('\n');
                                txt += piece;
                            }
                            else
                            {
                                txt += pdftext;
                            }
                        }
                    }

                    if (!string.IsNullOrEmpty(txt) && txt.Length > 200)
                    {
                        var pf = await FromText(txt);
                        if (pf != null && pfs.FirstOrDefault(t =>
                                string.Equals(t.ProductieNr, pf.ProductieNr,
                                    StringComparison.CurrentCultureIgnoreCase)) ==
                            null)
                            pfs.Add(pf);
                    }

                    reader.Close();
                    return pfs;
                }
                catch (Exception)
                {
                    return null;
                }
            });
        }

        public static Task<ProductieFormulier> FromImage(byte[] data)
        {
            return Task.Run(async () =>
            {
                try
                {
                    ProductieFormulier pr = null;
                    using (var ms = new MemoryStream(data))
                    {
                        var productie = (Bitmap) Image.FromStream(ms);
                        {
                            if (productie.Width > productie.Height)
                                productie.RotateFlip(RotateFlipType.Rotate270FlipNone);

                            var txt = productie.GetTextFromImage();
                            pr = await FromText(txt);
                        }
                    }

                    return pr;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            });
        }

        public static Task<ProductieFormulier> FromText(string text)
        {
            return Task.Run(async () =>
            {
                try
                {
                    if (text == null || text.Length <= KeyValues.Sum(t => t.Length))
                        return null;
                    text = text.Replace('ì', 'i').Replace("ĳ", "ij").Replace("—", "-");
                    text = RemoveDots(text);
                    var p = new ProductieFormulier();
                    p.InitDefault();
                    p.DatumToegevoegd = DateTime.Now;
                    double doorlooptijd = 0;
                    for (var i = 0; i < KeyValues.Length; i++)
                    {
                        var index = text.ToLower().GetWordIndex(KeyValues[i], 0);
                        if (index > -1)
                        {
                            index += KeyValues[i].Length + 1;
                            var value = text.Substring(index, GetLengthBySpace(text.Substring(index)));
                            if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                            {
                                var start = -1;
                                var end = -1;
                                switch (i)
                                {
                                    case 0: //productienr
                                        p.ProductieNr = value;
                                        break;

                                    case 1: //leverdatum
                                        var dt = new DateTime();
                                        if (DateTime.TryParse(value.Replace('—', '-'), out dt))
                                            p.LeverDatum = dt.Add(Manager.Opties.GetWerkRooster().EindWerkdag);
                                        break;

                                    case 2: //doorlooptijd
                                        double t = 1;
                                        if (double.TryParse(value, out t))
                                            doorlooptijd = t * 7.5;
                                        else
                                            doorlooptijd = 7.5;
                                        p.DoorloopTijd = doorlooptijd;
                                        break;

                                    case 3: //benodigde uren
                                        double xt = 1;
                                        if (double.TryParse(value, out xt))
                                            doorlooptijd = xt;
                                        else
                                            doorlooptijd = 7.5;
                                        p.DoorloopTijd = Math.Round(doorlooptijd, 2);
                                        break;

                                    case 4: //artikelnr/Aantal en omschrijving
                                        start = text.ToLower().GetWordIndex("omschrijving:", 0);
                                        if (start > -1)
                                        {
                                            start += 13;
                                            end = text.ToLower().GetWordIndex("geproduceerd:", start);
                                            if (end > -1)
                                            {
                                                value = text.Substring(start, end - start).Replace("Aantal", "")
                                                    .Replace("Artikelnr.", "").Replace("Omschrijving", "")
                                                    .Replace("datum gereed", "").TrimStart('\n').Replace("\n\n", "\n")
                                                    .TrimEnd('\n').Trim();
                                                value = value.Replace("paraaf gereed\n", "");
                                                start = 0;
                                                end = value.Length;
                                                var length = GetLengthBySpace(value);
                                                var d = value.Substring(0, length).Replace(".", "");
                                                if (int.TryParse(d, out var aantal))
                                                    p.Aantal = aantal;
                                                start += length + 1;
                                                d = value.Substring(start);
                                                length = GetLengthBySpace(d);
                                                var str = value.Substring(start, length);
                                                if (!string.IsNullOrEmpty(str)) p.ArtikelNr = str;
                                                start += length + 1;
                                                if (start < end)
                                                {
                                                    str = value.Substring(start, end - start).Trim();
                                                    p.Omschrijving = str;
                                                }
                                            }
                                        }

                                        break;

                                    case 6: //benodigde materialen
                                        try
                                        {
                                            start = text.ToLower().GetWordIndex("afkeur:", index);
                                            if (start > -1)
                                            {
                                                start += 7;
                                                end = text.ToLower().GetWordIndex("toelichting bewerking:", start);
                                                value = text.Substring(start, end - start);
                                                var values = value.Split('\n');
                                                if (values.Length > 0)
                                                {
                                                    var mats = new List<Materiaal>();
                                                    foreach (var mat in values)
                                                    {
                                                        if (mat.Length < 10)
                                                            continue;
                                                        try
                                                        {
                                                            var xmat = new Materiaal(p);

                                                            //artikel nummer
                                                            var length = GetLengthBySpace(mat);
                                                            var xvalue = mat.Substring(0, length);
                                                            start = length;
                                                            xmat.ArtikelNr = xvalue;
                                                            length++;
                                                            var tmp = mat.Substring(length, mat.Length - length);

                                                            //aantal per stuk
                                                            length = GetLengthBySpace(tmp);
                                                            xvalue = tmp.Substring(0, length);
                                                            length++;
                                                            tmp = tmp.Substring(length, tmp.Length - length);
                                                            if (double.TryParse(xvalue, out var matvalue))
                                                                xmat.AantalPerStuk = matvalue;

                                                            //aantal
                                                            length = GetLengthBySpace(tmp);
                                                            xvalue = tmp.Substring(0, length).Replace(".", "");
                                                            length++;
                                                            tmp = tmp.Substring(length, tmp.Length - length);
                                                            if (double.TryParse(xvalue, out matvalue))
                                                                xmat.Aantal = matvalue;

                                                            //eenheid
                                                            length = GetLengthBySpace(tmp);
                                                            xvalue = tmp.Substring(0, length);
                                                            length++;
                                                            tmp = tmp.Substring(length, tmp.Length - length);
                                                            xmat.Eenheid = xvalue;

                                                            //locatie
                                                            length = GetLengthBySpace(tmp);
                                                            xvalue = tmp.Substring(0, length);
                                                            //if (xvalue == xvalue.ToUpper())
                                                            //{
                                                            //    xmat.Locatie = xvalue;
                                                            //}
                                                            bool valid = false;
                                                            if (length < 9)
                                                            {
                                                                valid = true;
                                                                foreach (var xchar in xvalue)
                                                                    if (!char.IsUpper(xchar) && !char.IsDigit(xchar) && xchar != '-' && xchar != '.')
                                                                    {
                                                                        valid = false;
                                                                        break;
                                                                    }
                                                            }

                                                            if (valid)
                                                            {
                                                                xmat.Locatie = xvalue;
                                                                length++;
                                                            }
                                                            else
                                                            {
                                                                xmat.Locatie = "N.V.T";
                                                                length = 0;
                                                            }

                                                            if (length > 0)
                                                            {
                                                                var leftover = tmp.Length - length;
                                                                if (leftover > 0)
                                                                    tmp = tmp.Substring(length, leftover);
                                                                //index = 0;
                                                                //while (index < tmp.Length && !char.IsUpper(tmp[index]))
                                                                //    index++;

                                                                //if (index > 0 && index <= xvalue.Length)
                                                                //{
                                                                //    length = index;
                                                                //    xvalue += " " + tmp.Substring(0, length)
                                                                //        .TrimEnd(' ');
                                                                //    tmp = tmp.Substring(length, tmp.Length - length);
                                                                //    xmat.Locatie = xvalue;
                                                                //}
                                                                //else tmp = xvalue + " " + tmp;
                                                            }


                                                            //omschrijving
                                                            xvalue = tmp.Substring(0, tmp.Length - xmat.Eenheid.Length)
                                                                .TrimEnd(' ');
                                                            xmat.Omschrijving = xvalue;

                                                            mats.Add(xmat);
                                                        }
                                                        catch(Exception ex)
                                                        {
                                                            Console.WriteLine(ex);
                                                        }
                                                    }

                                                    p.Materialen = mats;
                                                }
                                            }
                                        }
                                        catch
                                        {
                                        }

                                        break;

                                    case 7: //toelichting bewerking
                                        start = index;
                                        if (start > -1)
                                        {
                                            end = text.ToLower().IndexOf("verpakkingsinstructie", start,
                                                StringComparison.Ordinal);
                                            if (end > -1)
                                            {
                                                value = text.Substring(start, end - start).TrimStart('\n')
                                                    .Replace("\n\n", "\n").TrimEnd('\n');
                                                var values = value.Split('\n');
                                                var bws = new List<BewerkingEntry>();
                                                foreach (var t1 in values)
                                                {
                                                    if (t1.Replace(" ","").All(x=> x == 'I'))
                                                        break;
                                                    var splitbws = t1.Split(' ');
                                                    var xcrit = "";
                                                    foreach (var split in splitbws)
                                                    {
                                                        xcrit += split;
                                                        var xb = Manager.BewerkingenLijst.GetEntry(xcrit.Trim());
                                                        if (xb != null)
                                                        {
                                                            xcrit = "";
                                                            bws.Add(xb);
                                                            continue;
                                                        }

                                                        xcrit += " ";
                                                    }
                                                }

                                                var xbws = new List<Bewerking>();
                                                foreach (var s in bws)
                                                {
                                                    var count = xbws.Sum(x =>
                                                        x.Naam.ToLower().Split('[')[0].Trim() == s.Naam.ToLower().Trim()
                                                            ? 1
                                                            : 0);
                                                    var naam = s.Naam;
                                                    if (count > 0)
                                                        naam = $"{s.Naam}[{count - 1}]";
                                                    var bw = new Bewerking(Math.Round(doorlooptijd / bws.Count, 2))
                                                    {
                                                        IsBemand = s.IsBemand,
                                                        LeverDatum = p.LeverDatum,
                                                        DatumToegevoegd = DateTime.Now,
                                                        Parent = p,
                                                        Aantal = p.Aantal,
                                                        Naam = naam,
                                                        Omschrijving = p.Omschrijving,
                                                        ProductieNr = p.ProductieNr,
                                                        ArtikelNr = p.ArtikelNr,
                                                        State = ProductieState.Gestopt
                                                    };
                                                    //bw.OnBewerkingChanged += BewerkingChanged;
                                                    await bw.UpdateBewerking(null, $"[{bw.Path}] Nieuw Aangemaakt",
                                                        false);
                                                    xbws.Add(bw);
                                                }

                                                bws.Clear();
                                                bws = null;
                                                p.Bewerkingen = xbws.ToArray();
                                            }
                                        }

                                        break;
                                }
                            }
                        }
                    }

                    if (string.IsNullOrEmpty(p.ProductieNr))
                        return null;
                    return p;
                }
                catch
                {
                    return null;
                }
            });
        }

        public static Task<ProductieFormulier> UpdateDoorloopTijd(List<ProductieFormulier> forms, ProductieFormulier form,string change, bool showmessage, bool raiseevent)
        {
            return Task.Run(async () =>
            {
                if (form != null)
                {
                    try
                    {
                        form.GemiddeldPerUur = form.ActueelPerUur;
                        if (form.ActueelPerUur > 0 && form.Aantal > 0)
                            form.GemiddeldDoorlooptijd = Math.Round(form.Aantal / form.ActueelPerUur, 2);
                        if (forms != null)
                            forms = form.ArtikelNr != null ? forms.Where(x => x.ArtikelNr != null &&
                            string.Equals(x.ArtikelNr, form.ArtikelNr, StringComparison.CurrentCultureIgnoreCase)).ToList() : new List<ProductieFormulier>();
                        else forms = await Manager.Database.GetProducties(form.ArtikelNr, true, ProductieState.Gereed, true);

                        form.Geproduceerd = forms.Count;
                        var peruur = forms.Sum(x => x.PerUur) / forms.Count;
                        var gemiddeldperuur = forms.Sum(x => x.ActueelPerUur) / forms.Count;
                        if (peruur > 0) form.GemiddeldPerUur = (int)peruur;
                        if (gemiddeldperuur > 0) form.GemiddeldActueelPerUur = (int)gemiddeldperuur;
                        if (gemiddeldperuur > 0 && form.Aantal > 0)
                            form.GemiddeldDoorlooptijd = Math.Round((form.TotaalGemaakt > form.Aantal?form.TotaalGemaakt : form.Aantal) / gemiddeldperuur, 2);
                        if (form.Bewerkingen != null && form.Bewerkingen.Length > 0)
                            foreach (var b in form.Bewerkingen)
                                await b.UpdateBewerking(forms, null, false);
                        await form.UpdateForm(false, false, null, 
                            $"[{form.ProductieNr}|{form.ArtikelNr}]{change}\nDoorlooptijd opnieuw berekend",true, showmessage, raiseevent);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }

                return form;
            });
        }

        public string GetNextTextBlock(string value, out string block)
        {
            var length = GetLengthBySpace(value);
            block = value.Substring(0, length);
            length++;
            return value.Substring(length, value.Length - length);
        }

        private bool InitDefault()
        {
            try
            {
                Bewerkingen = new Bewerking[] { };
                Materialen = new List<Materiaal>();
                State = ProductieState.Gestopt;
                var types = GetType().GetProperties().Where(t => t.CanWrite).ToArray();
                foreach (var type in types)
                {
                    if (type.PropertyType == typeof(string))
                        if (type.GetValue(this) == null)
                            type.SetValue(this, "");
                    if (type.PropertyType == typeof(DateTime))
                        if ((DateTime) type.GetValue(this) == DateTime.MinValue)
                            type.SetValue(this, DateTime.Now);
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateForm(bool updatebewerking, bool updategemiddeld,
            List<ProductieFormulier> formulieren = null, string change = "Update Formulier",
            bool save = true, bool showmessage = true, bool raiseevent = true)
        {

            try
            {
                var forms = formulieren;
                double peruur = 0;
                if (updategemiddeld)
                {
                    await UpdateDoorloopTijd(formulieren, this, change, false, raiseevent);
                    return true;
                }
                else
                {
                    if (State != ProductieState.Verwijderd)
                        VerwachtLeverDatum = VerwachtDatumGereed();
                    else if (State == ProductieState.Gereed)
                        VerwachtLeverDatum = DatumGereed;

                    if (updatebewerking)
                        if (Bewerkingen != null && Bewerkingen.Length > 0)
                            foreach (var b in Bewerkingen)
                            {
                                if (!b.IsAllowed(null)) continue;
                                await b.UpdateBewerking(forms, $"[{b.Path}] Bewerking Update", false);
                            }

                    peruur = ActueelProductenPerUur();
                    if (peruur > 0)
                        ActueelPerUur = (int)peruur;
                    GemiddeldDoorlooptijd = GemiddeldDoorlooptijd > 0 ? GemiddeldDoorlooptijd : DoorloopTijd;
                    var st = DateTime.Now;
                    AanbevolenPersonen = AantalPersonenNodig(ref st);
                    StartOp = st;
                    TijdGewerkt = TijdAanGewerkt();
                    Gereed = GereedPercentage();

                    if (GemiddeldPerUur <= 0)
                        GemiddeldPerUur = ActueelPerUur;
                    if (TotaalTijdGewerkt <= 0)
                        TotaalTijdGewerkt = TijdGewerkt;

                    if (save)
                        await Manager.Database.UpSert(this, change, showmessage);
                    else if (raiseevent)
                        Manager.FormulierChanged(this, this);
                    if (raiseevent)
                        FormulierChanged(this);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        #endregion "Data Management"

        #region "Public Methods"

        public bool ContainsProductiePdf()
        {
            return File.Exists($"{Manager.ProductieFormPath}\\{ProductieNr}.pdf");
        }

        public void OpenProductiePdf()
        {
            if (ContainsProductiePdf()) Process.Start($"{Manager.ProductieFormPath}\\{ProductieNr}.pdf");
        }

        public ProductieState GetCurrentState()
        {
            if (Bewerkingen == null)
                return _state;
            var bws = Bewerkingen.Where(x => x.IsAllowed(null)).ToArray();
            if (bws.Length == 0)
                return _state;
            if (bws.All(x => x.State == ProductieState.Gereed))
                return ProductieState.Gereed;
            if (bws.Any(x => x.State == ProductieState.Gestart))
                return ProductieState.Gestart;
            if (bws.All(x => x.State == ProductieState.Verwijderd))
                return ProductieState.Verwijderd;
            return ProductieState.Gestopt;
        }

        public List<NotitieEntry> GetNotities()
        {
            var xreturn = new List<NotitieEntry>();

            if (Note != null && !string.IsNullOrEmpty(Note.Notitie))
            {
                xreturn.Add(Note);
            }

            if (GereedNote != null && !string.IsNullOrEmpty(GereedNote.Notitie))
            {
                xreturn.Add(GereedNote);
            }

            if (Bewerkingen != null)
            {
                foreach (var bew in Bewerkingen)
                {
                    if (bew.Note != null && !string.IsNullOrEmpty(bew.Note.Notitie))
                    {
                        xreturn.Add(bew.Note);
                    }

                    if (bew.GereedNote != null && !string.IsNullOrEmpty(bew.GereedNote.Notitie))
                    {
                        xreturn.Add(bew.GereedNote);
                    }

                    if (bew.DeelGereedMeldingen != null)
                    {
                        foreach(var dg in bew.DeelGereedMeldingen)
                            if(dg.Note != null && !string.IsNullOrEmpty(dg.Note.Notitie))
                                xreturn.Add(dg.Note);
                    }

                    if (bew.WerkPlekken.Count == 0) continue;
                    foreach (var wp in bew.WerkPlekken)
                    {
                        if (wp.Note != null && !string.IsNullOrEmpty(wp.Note.Notitie))
                        {
                            xreturn.Add(wp.Note);
                        }
                    }
                }
            }

            return xreturn;
        }

        public static async Task<ProductieFormulier> CreateNewProductie(string artikelnr, string omschrijving, int aantal,
            DateTime leverdatum, int peruur,BewerkingEntry bewerking, string productienr = null)
        {
            if (string.IsNullOrEmpty(artikelnr) || bewerking == null || string.IsNullOrEmpty(bewerking.Naam))
                return null;
            if (peruur <= 0)
                peruur = 100;
            var prodnr = string.IsNullOrEmpty(productienr)
                ? $"PM0{DateTime.Now.Ticks.ToString().Substring(5, 7)}"
                : productienr;
            if (await Manager.Database.ProductieExist(prodnr))
                throw new Exception($"Productie '{prodnr}' bestaat al en kan niet nogmaals worden toegevoegd!");
            ProductieFormulier prod = new ProductieFormulier
            {
                ProductieNr = prodnr,
                ArtikelNr = artikelnr,
                DatumToegevoegd = DateTime.Now,
                Aantal = aantal,
                LeverDatum = leverdatum,
                Omschrijving = omschrijving,
                DoorloopTijd = peruur >= aantal? 1 : aantal / peruur
            };
            Bewerking bew = new Bewerking
            {
                Parent = prod, Naam = bewerking.Naam, DoorloopTijd = prod.DoorloopTijd, LeverDatum = prod.LeverDatum,
                DatumToegevoegd = DateTime.Now,
                IsBemand = bewerking.IsBemand
            };
            prod.Bewerkingen = new Bewerking[] {bew};
            return prod;
        }

        public TimeSpan TijdNodig()
        {
            try
            {
                if (Bewerkingen == null || Bewerkingen.Length == 0)
                    return new TimeSpan();
                return TimeSpan.FromHours(Bewerkingen.Sum(x => x.TijdNodig()));
            }
            catch
            {
                return new TimeSpan();
            }
        }

        public TimeSpan TijdOverTotLeverdatum()
        {
            return this.WerkTijdNodigTotLeverdatum();
        }

        public Bewerking GetFirstAvailibleBewerking()
        {
            if (Bewerkingen == null || Bewerkingen.Length == 0) return null;
            for(int i = 0; i < Bewerkingen.Length; i++)
            {
                var bw = Bewerkingen[i];
                if (!bw.IsAllowed()) continue;
                if ((bw.State == ProductieState.Gereed || bw.State == ProductieState.Verwijderd) &&
                    (i + 1) >= Bewerkingen.Length)
                    return bw;
                if (bw.State == ProductieState.Gestart) return bw;
            }

            return Bewerkingen.FirstOrDefault(x => x.IsAllowed());
        }
        public async Task<bool> MeldGereed(int aantal, string paraaf, string notitie, bool sendmail)
        {
            try
            {
                //if (State == ProductieState.Gereed) return true;
                if (State == ProductieState.Verwijderd)
                    throw new Exception(
                        "Productie is verwijderd en kan daarom niet gereed gemeld worden.\nVoeg de productie opnieuw toe, en probeer het opnieuw.");

                DatumGereed = DateTime.Now;
                AantalGemaakt = aantal;
                LaatstAantalUpdate = DateTime.Now;
                Paraaf = paraaf;
                GereedNote = new NotitieEntry(notitie,this) {Naam = paraaf, Type = NotitieType.ProductieGereed};

                double tijd = 0;
                foreach (var b in Bewerkingen)
                {
                    if (!b.IsAllowed(null)) continue;
                    if (b.State != ProductieState.Verwijderd && b.State != ProductieState.Gereed)
                        await b.MeldBewerkingGereed(paraaf, aantal, notitie, false,false);
                    tijd += b.TijdGewerkt;
                }

                if (tijd > 0 && TotaalGemaakt > 0)
                    ActueelPerUur = Math.Round(TotaalGemaakt / tijd);
                State = ProductieState.Gereed;
                TijdGewerkt = tijd;
                var xa = TotaalGemaakt == 1 ? "stuk" : "stuks";
                var change =
                    $"[{ProductieNr.ToUpper()}|{ArtikelNr}] {paraaf} heeft is zojuist {TotaalGemaakt} {xa} gereed gemeld in {TijdGewerkt} uur({ActueelPerUur} P/u).";
               
                await UpdateForm(false, false, null, change);
                _ = UpdateDoorloopTijd(null, this, null, false, true);
                if (sendmail)
                    RemoteProductie.RespondByEmail(this, change);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double TijdAanGewerkt()
        {
            var gestopt = State == ProductieState.Gestart ? DateTime.Now : TijdGestopt;
            var tijd = TotaalTijdGewerkt;
            if (Bewerkingen == null || Bewerkingen.Length == 0)
                tijd += Werktijd.TijdGewerkt(TijdGestart, gestopt, null,null).TotalHours;
            else
                tijd = Bewerkingen.Sum(x => x.TijdAanGewerkt());
            return Math.Round(tijd, 2);
        }

        public double TotaalGewerkteUren(List<ProductieFormulier> forms)
        {
            try
            {
                double value = 0;
                if (forms == null || forms.Count == 0)
                    value = TijdAanGewerkt();
                else
                    value = forms.Sum(t => t.TijdGewerkt);

                return Math.Round(value, 2);
            }
            catch
            {
                return 0;
            }
        }

        public double TotaalGemiddeldAantalPerUur(List<ProductieFormulier> forms)
        {
            if (forms == null || forms.Count == 0)
                return PerUur;
            forms = forms.Where(x => x.ActueelProductenPerUur() > 0).ToList();
            var tg = forms.Count == 0 ? 0 : forms.Sum(t => t.ActueelProductenPerUur()) / forms.Count;
            if (tg > 0)
                return Math.Round(tg, 0);
            return ActueelProductenPerUur();
        }

        private double ActueelProductenPerUur()
        {
            if (Bewerkingen?.Length > 0)
            {
                var bws = Bewerkingen.Where(x => x.TijdAanGewerkt() > 0).ToArray();
                var actueel = bws.Sum(t => t.TijdAanGewerkt());
                if (actueel > 0)
                {
                    var xaantal = AantalGemaakt > 0 ? AantalGemaakt : Aantal;

                    actueel /= bws.Length;
                    actueel = xaantal / actueel;
                }

                return Math.Round(actueel, 0);
            }

            return ProductenPerUur(AantalGemaakt, TijdAanGewerkt());
        }

        public double ProductenPerUur(int aantal, double tijd)
        {
            if (aantal == 0 || tijd == 0)
                return 0;
            return Math.Round(aantal / tijd, 0);
        }

        public double GereedPercentage()
        {
            return Bewerkingen == null || Bewerkingen.Length == 0
                ? 0
                : Math.Round(Bewerkingen.Sum(t => t.GereedPercentage()) / Bewerkingen.Length, 1);
        }

        public double GereedPercentage(double max, double current)
        {
            if (current > 0)
            {
                var val = Math.Round(current / max * 100, 1);
                if (val > 100)
                    val = 100;
                return val;
            }

            return 0;
        }

        public DateTime VerwachtDatumGereed()
        {
            if (State == ProductieState.Gereed) return DatumGereed;
            int aantal;
            if (TotaalGemaakt > Aantal)
                aantal = 0;
            else aantal = Aantal - TotaalGemaakt;
            var rooster = Manager.Opties.GetWerkRooster();
            if (aantal <= 0)
                return Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster,rooster,null);
            var peruur = (int) ActueelProductenPerUur();
            if (peruur == 0)
                peruur = PerUur;
            var tijd = peruur > 0 ? aantal / peruur : DoorloopTijd;

            if (tijd < 0 || double.IsInfinity(tijd))
                return Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster,rooster, null);

            if (Bewerkingen != null)
                foreach (var b in Bewerkingen)
                    tijd /= b.GetPersoneel().AantalPersTijdMultiplier();

            if (tijd < 0 || double.IsInfinity(tijd))
                return Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster,rooster, null);

            return Werktijd.DatumNaTijd(DateTime.Now, TimeSpan.FromHours(tijd), null, null);
        }

        public DateTime VerwachtDatumGereed(DateTime start, double tijd, int aantal, int gedaan,
            Bewerking[] bewerkingen)
        {
            var rooster = Manager.Opties.GetWerkRooster();
            if (gedaan >= aantal)
                return Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster,rooster, null);

            var peruur = aantal / tijd;

            if (double.IsInfinity(peruur) || peruur == 0)
                return Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster, rooster, null);

            var xaantal = aantal - gedaan;

            var xtijd = xaantal / peruur;

            if (xaantal == 0)
                return Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster, rooster, null);

            if (bewerkingen != null)
                foreach (var b in bewerkingen)
                    xtijd /= b.GetPersoneel().AantalPersTijdMultiplier();
            return Werktijd.DatumNaTijd(DateTime.Now, TimeSpan.FromHours(xtijd), null, null);
        }

        public Personeel[] AantalPersonenShifts()
        {
            if (Bewerkingen == null)
                return new Personeel[] { };
            var shifts = new List<Personeel>();
            foreach (var bew in Bewerkingen)
                shifts.AddRange(bew.GetPersoneel().Where(x => x.Actief));
            return shifts.ToArray();
        }

        public int AantalPersonenNodig(ref DateTime startop)
        {
            if (Bewerkingen == null || Bewerkingen.Length == 0) return 0;
            startop = DateTime.Now;
            int pers = 0;
            for (int i = 0; i < Bewerkingen.Length; i++)
            {
                
                pers += Bewerkingen[i].AantalPersonenNodig(ref startop, i > 0);
            }
            //var tijdover = TijdOverTotLeverdatum();
            //if (tijdover.TotalHours <= 0)
            //    return 0;
            //var aantal = Aantal - AantalGemaakt;
            //int pers = (int) Math.Ceiling(aantal / ActueelProductenPerUur() / tijdover.TotalHours);
            //if (pers < 1) pers = 1;
            //startop = DateTime.Now;
            //var pp = tijdover.TotalHours / pers;
            //startop = startop.Subtract(TimeSpan.FromHours(pp));
            return pers;
        }

        public List<WerkPlek> GetAlleWerkplekken()
        {
            var plekken = new List<WerkPlek>();
            if (Bewerkingen != null && Bewerkingen.Length > 0)
                foreach (var bew in Bewerkingen)
                    if (bew.WerkPlekken != null && bew.WerkPlekken.Count > 0)
                        plekken.AddRange(bew.WerkPlekken);
            return plekken;
        }

        public List<Storing> GetAlleStoringen(bool alleenactief)
        {
            var xreturn = new List<Storing>();
            try
            {
                var plekken = GetAlleWerkplekken();
                foreach (var plek in plekken)
                {
                    var storingen = plek.Storingen;
                    if (alleenactief)
                        storingen = storingen.Where(x => !x.IsVerholpen).ToList();
                    if (storingen.Count > 0)
                        xreturn.AddRange(storingen);
                }
            }
            catch
            {
            }

            return xreturn;
        }

        public Task<KeyValuePair<string,int>> GetAanbevolenPersoneelHtml()
        {
            return Task.Run(async() =>
            {
                string xret = string.Empty;
                int xpers = 0;
                try
                {
                    if (Bewerkingen == null || Bewerkingen.Length == 0) return new KeyValuePair<string, int>();
                    xret = $"<html>\r\n" +
                           $"<head>\r\n" +
                           $"<style>{GetStylesheet("StyleSheet")}</style>\r\n" +
                           $"<Title>{ArtikelNr}</Title>\r\n" +
                           $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                           $"</head>\r\n" +
                           $"<body style='background - color: {Color.DarkGreen.Name}; background-gradient: {Color.DarkGreen.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 50px 0px'>\r\n" +
                           $"<h1 align='center' style='color: {Color.White.Name}'>\r\n" +
                           $"       Aanbevolen personen voor {Omschrijving}\r\n" +
                           $"        <br/>\r\n" +
                           $"        <span style=\'font-size: x-small;\'>ArtikelNr: {ArtikelNr}, ProductieNr: {ProductieNr}</span>\r\n " +
                           $"</h1>\r\n" +
                           $"<blockquote class='whitehole'>\r\n" +
                           $"       <p style = 'margin-top: 0px' >\r\n" +
                           $"<table border = '0' width = '100%' >\r\n" +
                           $"<tr style = 'vertical-align: top;' >\r\n" +
                           $"<td>\r\n";
                    foreach (var bw in Bewerkingen)
                    {
                        if (!bw.IsAllowed()) continue;
                        var x = await bw.GetAanbevolenPersoneelHtml(false, xpers);
                        xpers = x.Value;
                        xret += x.Key;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return new KeyValuePair<string, int>(xret, xpers);
            });
        }

        public Task<KeyValuePair<string, int>> GetAanbevolenWerkplekkenHtml()
        {
            return Task.Run(async() =>
            {
                string xret = string.Empty;
                int xpers = 0;
                try
                {
                    if (Bewerkingen == null || Bewerkingen.Length == 0) return new KeyValuePair<string, int>();
                    xret = $"<html>\r\n" +
                           $"<head>\r\n" +
                           $"<style>{GetStylesheet("StyleSheet")}</style>\r\n" +
                           $"<Title>{ArtikelNr}</Title>\r\n" +
                           $"<link rel = 'Stylesheet' href = 'StyleSheet' />\r\n" +
                           $"</head>\r\n" +
                           $"<body style='background - color: {Color.DarkGreen.Name}; background-gradient: {Color.DarkGreen.Name}; background-gradient-angle: 250; margin: 0px 0px; padding: 0px 0px 50px 0px'>\r\n" +
                           $"<h1 align='center' style='color: {Color.White.Name}'>\r\n" +
                           $"       Aanbevolen werkplekken voor {Omschrijving}\r\n" +
                           $"        <br/>\r\n" +
                           $"        <span style=\'font-size: x-small;\'>ArtikelNr: {ArtikelNr}, ProductieNr: {ProductieNr}</span>\r\n " +
                           $"</h1>\r\n" +
                           $"<blockquote class='whitehole'>\r\n" +
                           $"       <p style = 'margin-top: 0px' >\r\n" +
                           $"<table border = '0' width = '100%' >\r\n" +
                           $"<tr style = 'vertical-align: top;' >\r\n" +
                           $"<td>\r\n";
                    foreach (var bw in Bewerkingen)
                    {
                        if (!bw.IsAllowed()) continue;
                        var x = await bw.GetAanbevolenWerkplekHtml(false, xpers);
                        xpers = x.Value;
                        xret += x.Key;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }

                return new KeyValuePair<string, int>(xret, xpers);
            });
        }

        public bool IsOnderbroken()
        {
            try
            {
                return GetAlleStoringen(true).Count > 0;
            }
            catch
            {
                return false;
            }
        }

        #endregion "Public Methods"

        #region "Private Methods"

        private int MeestGemaaktBewerking()
        {
            if (Bewerkingen == null || Bewerkingen.Length == 0)
                return AantalGemaakt;
            var x = 0;
            foreach (var b in Bewerkingen)
                if (b.AantalGemaakt > x)
                    x = b.AantalGemaakt;
            return x;
        }

        private static string RemoveDots(string value)
        {
            var index = -1;
            var count = 0;
            var xreturn = value;
            while ((index = xreturn.IndexOf('.', index + 1)) > -1 && index < xreturn.Length)
            {
                for (var i = index; i < xreturn.Length; i++)
                    if (xreturn[i] == '.')
                        count++;
                    else break;
                if (count > 1) xreturn = xreturn.Remove(index, count);
                count = 0;
            }

            return xreturn;
        }

        private static string[] KeyValues
        {
            get
            {
                return new[]
                {
                    "productienr.", "leverdatum", "doorlooptijd:", "benodigde uren:", "artikelnr.", "omschrijving",
                    "benodigde materialen", "toelichting bewerking"
                };
            }
        }

        private static int GetLengthBySpace(string text)
        {
            if (text == null)
                return -1;
            for (var i = 0; i < text.Length; i++)
                if (text[i] == ' ' || text[i] == '\n')
                    return i;
            return text.Length;
        }

        #endregion "Private Methods"

        #region "Events"

        public event FormulierChangedHandler OnFormulierChanged;

        public void FormulierChanged(object sender)
        {
            OnFormulierChanged?.Invoke(sender, this);
        }

        public event BewerkingChangedHandler OnBewerkingChanged;

        public async void BewerkingChanged(object sender, Bewerking bew, string change)
        {
            var xchange = change ?? (bew.LastChanged?.Change ?? $"[{bew.Path}] Bewerking Gewijzigd");
            await UpdateForm(false, false, null, xchange);
            //Manager.Database.UpSert(this, xchange);
            OnBewerkingChanged?.Invoke(sender, bew, xchange);
            FormulierChanged(sender);
            Manager.BewerkingChanged(sender, bew, xchange);
        }

        #endregion "Events"
    }
}