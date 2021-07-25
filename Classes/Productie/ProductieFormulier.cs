using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using LiteDB;
using Polenter.Serialization;
using ProductieManager.Classes.Productie;
using ProductieManager.Misc;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ProductieManager.Productie
{
    [Serializable]
    public class ProductieFormulier : IProductieBase
    {
        #region "Variables"

        public string Versie = "1.0";

        internal ProductieState _state;

        public override ProductieState State
        {
            get { return GetCurrentState(); }
            set { _state = value; }
        }

        [BsonId]
        public override string ProductieNr { get => base.ProductieNr; set => base.ProductieNr = value; }

        public int AantalProducties { get; set; }

        private DateTime _leverdatum;

        public override DateTime LeverDatum
        {
            get
            {
                return _leverdatum;
            }
            set
            {
                _leverdatum = value;
                if (Bewerkingen != null && Bewerkingen.Length > 0)
                    Bewerkingen[Bewerkingen.Length - 1].LeverDatum = value;
            }
        }

        private Bewerking[] _bewerkingen;

        public Bewerking[] Bewerkingen
        {
            get { return _bewerkingen; }
            set
            {
                _bewerkingen = value;
                UpdateBewerkingen();
            }
        }

        public void UpdateBewerkingen()
        {
            if (Bewerkingen != null && Bewerkingen.Length > 0)
            {
                foreach (var b in Bewerkingen)
                {
                    b.Parent = this;
                    b.WerkPlekken.ForEach(x => x.Werk = b);
                }
            }
        }

        public override string Omschrijving { get => base.Omschrijving.WrapText(150); set => base.Omschrijving = value; }

        private List<Materiaal> _materialen;
        public List<Materiaal> Materialen { get { return _materialen; } set { _materialen = value; _materialen?.ForEach(x => x.Parent = this); } }
        public int ViewImageIndex { get; set; }

        private int _aantalgemaakt;

        public virtual int AantalGemaakt
        {
            get { return Bewerkingen == null ? _aantalgemaakt : Bewerkingen.Sum(x => x.AantalGemaakt) / Bewerkingen.Length; }
            set { _aantalgemaakt = value; }
        }

        [BsonIgnore]
        public int Personen { get { return Bewerkingen == null ? 0 : Bewerkingen.Sum(x => x.GetPersoneel().Length); } }

        [BsonIgnore]
        public override bool TeLaat => Werktijd.EerstVolgendewerkdag(DateTime.Now) > LeverDatum;

        [BsonIgnore]
        public override bool IsNieuw => DateTime.Now.TimeOfDay < DatumToegevoegd.AddHours(4).TimeOfDay;

        // public override bool IsNieuw => false;

        #endregion "Variables"

        #region "Constructor"

        public ProductieFormulier()
        {
        }

        #endregion "Constructor"

        #region "Data Management"

        public static byte[] Serialize(ProductieFormulier prod)
        {
            try
            {
                byte[] xreturn = null;
                using (MemoryStream ms = new MemoryStream())
                {
                    SharpSerializer sh = new SharpSerializer(true);
                    //sh.PropertyProvider = new ProductieFormulierProvider();
                    sh.Serialize(prod, ms);
                    xreturn = ms.ToArray();
                }
                return xreturn;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static ProductieFormulier DeSerializeFromData(byte[] data, Manager manager)
        {
            try
            {
                if (data == null || data.Length == 0)
                    return null;
                ProductieFormulier xreturn = null;
                using (MemoryStream ms = new MemoryStream(data))
                {
                    //BinaryFormatter bf = new BinaryFormatter();
                    SharpSerializer sh = new SharpSerializer(true);
                    //sh.PropertyProvider = new ProductieFormulierProvider();
                    xreturn = sh.Deserialize(ms) as ProductieFormulier;
                    if (xreturn != null)
                        foreach (var x in xreturn.Bewerkingen)
                        {
                            x.OnBewerkingChanged += xreturn.BewerkingChanged;
                        }
                }
                return xreturn;
            }
            catch (Exception ex) { return null; }
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

        public static ProductieFormulier[] FromPdf(byte[] data, Manager manager)
        {
            try
            {
                List<ProductieFormulier> pfs = new List<ProductieFormulier> { };
                using (MemoryStream ms = new MemoryStream(data))
                {
                    using PdfReader reader = new PdfReader(data);
                    string txt = "";
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        ProductieFormulier pf;
                        string pdftext = PdfTextExtractor.GetTextFromPage(reader, i);
                        if (string.IsNullOrEmpty(pdftext))
                        {
                            var pg = reader.GetPageN(i);
                            var res = PdfReader.GetPdfObject(pg.Get(PdfName.RESOURCES)) as PdfDictionary;
                            var xobj = PdfReader.GetPdfObject(res.Get(PdfName.XOBJECT)) as PdfDictionary;
                            if (xobj == null) continue;

                            var keys = xobj.Keys;
                            if (keys.Count == 0) continue;

                            var obj = xobj.Get(keys.ElementAt(0));
                            if (!obj.IsIndirect()) continue;

                            var tg = PdfReader.GetPdfObject(obj) as PdfDictionary;
                            var type = PdfReader.GetPdfObject(tg.Get(PdfName.SUBTYPE)) as PdfName;
                            if (!PdfName.IMAGE.Equals(type)) continue;

                            int XrefIndex = (obj as PRIndirectReference).Number;
                            var pdfStream = reader.GetPdfObject(XrefIndex) as PRStream;
                            var xdata = PdfReader.GetStreamBytesRaw(pdfStream);
                            pf = ProductieFormulier.FromImage(xdata, manager);
                            if (pf != null && pfs.Where(t => t.ProductieNr.ToLower() == pf.ProductieNr.ToLower()).FirstOrDefault() == null)
                                pfs.Add(pf);
                        }
                        else
                            txt += pdftext;
                    }
                    if (!string.IsNullOrEmpty(txt) && txt.Length > 200)
                    {
                        var pf = ProductieFormulier.FromText(txt, manager);
                        if (pf != null && pfs.Where(t => t.ProductieNr.ToLower() == pf.ProductieNr.ToLower()).FirstOrDefault() == null)
                            pfs.Add(pf);
                    }
                    reader.Close();
                }
                return pfs.ToArray();
            }
            catch
            {
                return null;
            }
        }

        public static ProductieFormulier FromImage(byte[] data, Manager manager)
        {
            try
            {
                ProductieFormulier pr = null;
                using (MemoryStream ms = new MemoryStream(data))
                {
                    Bitmap productie = (Bitmap)Image.FromStream(ms);
                    if (productie != null)
                    {
                        if (productie.Width > productie.Height)
                            productie.RotateFlip(RotateFlipType.Rotate270FlipNone);

                        string txt = productie.GetTextFromImage();
                        pr = FromText(txt, manager);
                    }
                }
                return pr;
            }
            catch (Exception ex) { throw ex; }
        }

        public static ProductieFormulier FromText(string text, Manager manager)
        {
            try
            {
                if (text == null || text.Length <= KeyValues.Sum(t => t.Length))
                    return null;
                text = text.Replace('ì', 'i').Replace("ĳ", "ij").Replace("—", "-");
                text = RemoveDots(text);
                ProductieFormulier p = new ProductieFormulier();
                p.InitDefault();
                p.DatumToegevoegd = DateTime.Now;
                double doorlooptijd = 0;
                int start = -1;
                int end = -1;
                for (int i = 0; i < KeyValues.Length; i++)
                {
                    int index = text.ToLower().GetWordIndex(KeyValues[i], 0);
                    string value;
                    if (index > -1)
                    {
                        index += KeyValues[i].Length + 1;
                        value = text.Substring(index, GetLengthBySpace(text.Substring(index)));
                        if (!string.IsNullOrEmpty(value) && !string.IsNullOrWhiteSpace(value))
                        {
                            switch (i)
                            {
                                case 0: //productienr
                                    p.ProductieNr = value;
                                    break;

                                case 1: //leverdatum
                                    DateTime dt = new DateTime();
                                    if (DateTime.TryParse(value.Replace('—', '-'), out dt))
                                        p.LeverDatum = dt.Add(Manager.Opties.EindWerkdag);
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
                                    p.DoorloopTijd = doorlooptijd;
                                    break;

                                case 4: //artikelnr/Aantal en omschrijving
                                    start = text.ToLower().GetWordIndex("omschrijving:", 0);
                                    if (start > -1)
                                    {
                                        start += 13;
                                        end = text.ToLower().GetWordIndex("geproduceerd:", start);
                                        if (end > -1)
                                        {
                                            value = text.Substring(start, end - start).Replace("Aantal", "").Replace("Artikelnr.", "").Replace("Omschrijving", "").Replace("datum gereed", "").TrimStart(new char[] { '\n' }).Replace("\n\n", "\n").TrimEnd(new char[] { '\n' }).Trim();
                                            value = value.Replace("paraaf gereed\n", "");
                                            start = 0;
                                            end = value.Length;
                                            int aantal = 0;
                                            int length = GetLengthBySpace(value);
                                            string d = value.Substring(0, length).Replace(".", "");
                                            if (int.TryParse(d, out aantal))
                                                p.Aantal = aantal;
                                            start += (length + 1);
                                            d = value.Substring(start);
                                            length = GetLengthBySpace(d);
                                            string str = value.Substring(start, length);
                                            if (!string.IsNullOrEmpty(str))
                                            {
                                                p.ArtikelNr = str;
                                            }
                                            start += (length + 1);
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
                                            string[] values = value.Split('\n');
                                            if (values.Length > 0)
                                            {
                                                List<Materiaal> mats = new List<Materiaal> { };
                                                foreach (string mat in values)
                                                {
                                                    if (mat.Length < 10)
                                                        continue;
                                                    Materiaal xmat = new Materiaal(p);

                                                    //artikel nummer
                                                    int length = GetLengthBySpace(mat);
                                                    string xvalue = mat.Substring(0, length);
                                                    start = length;
                                                    xmat.ArtikelNr = xvalue;
                                                    length++;
                                                    string tmp = mat.Substring(length, mat.Length - length);

                                                    //aantal per stuk
                                                    length = GetLengthBySpace(tmp);
                                                    xvalue = tmp.Substring(0, length);
                                                    length++;
                                                    double matvalue;
                                                    tmp = tmp.Substring(length, tmp.Length - length);
                                                    if (double.TryParse(xvalue, out matvalue))
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
                                                    length++;
                                                    tmp = tmp.Substring(length, tmp.Length - length);
                                                    index = 0;
                                                    while (!char.IsUpper(tmp[index]))
                                                        index++;

                                                    if (index > 0)
                                                    {
                                                        length = index;
                                                        xvalue += " " + tmp.Substring(0, length).TrimEnd(' ');
                                                        tmp = tmp.Substring(length, tmp.Length - length);
                                                    }
                                                    xmat.Locatie = xvalue;

                                                    //omschrijving
                                                    xvalue = tmp.Substring(0, tmp.Length - (xmat.Eenheid.Length)).TrimEnd(new char[] { ' ' });
                                                    xmat.Omschrijving = xvalue;

                                                    mats.Add(xmat);
                                                }
                                                p.Materialen = mats;
                                            }
                                        }
                                    }
                                    catch { }
                                    break;

                                case 7: //toelichting bewerking
                                    start = index;
                                    if (start > -1)
                                    {
                                        end = text.ToLower().IndexOf("verpakkingsinstructie", start);
                                        if (end > -1)
                                        {
                                            value = text.Substring(start, end - start).TrimStart(new char[] { '\n' }).Replace("\n\n", "\n").TrimEnd(new char[] { '\n' });
                                            string[] values = value.Split('\n');
                                            List<string> bws = new List<string> { };
                                            for (int j = 0; j < values.Length; j++)
                                            {
                                                if (values[j].Contains("II") || values[j].Length < 5)
                                                    continue;
                                                string[] b = values[j].Trim().GetBewerkingen(Manager.BewerkingenLijst);
                                                if (b.Length > 0)
                                                    bws.AddRange(b);
                                            }
                                            List<Bewerking> xbws = new List<Bewerking> { };
                                            foreach (var s in bws)
                                            {
                                                int count = xbws.Sum(x => x.Naam.ToLower().Split('[')[0].Trim() == s.ToLower().Trim() ? 1 : 0);
                                                string naam = s;
                                                if (count > 0)
                                                    naam = $"{s}[{count - 1}]";
                                                Bewerking bw = new Bewerking(doorlooptijd / bws.Count);
                                                //bw.OnBewerkingChanged += BewerkingChanged;
                                                bw.LeverDatum = p.LeverDatum;
                                                bw.Parent = p;
                                                bw.Aantal = p.Aantal;
                                                bw.Naam = naam;
                                                bw.Omschrijving = p.Omschrijving;
                                                bw.ProductieNr = p.ProductieNr;
                                                bw.ArtikelNr = p.ArtikelNr;
                                                bw.State = ProductieState.Gestopt;
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
                UpdateDoorloopTijd(p);
                return p;
            }
            catch
            {
                return null;
            }
        }

        public static bool UpdateDoorloopTijd(ProductieFormulier form)
        {
            ProductieFormulier[] olds;
            bool xreturn = false;
            if (form != null)
            {
                if (form.Aantal > 0 && form.DoorloopTijd > 0)
                    form.PerUur = Math.Round(form.Aantal / form.DoorloopTijd, 0);
                form.GemiddeldPerUur = form.PerUur;
                olds = Manager.Database.GetProducties(x => x.State == ProductieState.Gereed && x.ArtikelNr.ToLower() == form.ArtikelNr.ToLower()).ToArray();

                if (olds != null && olds.Length > 0)
                {
                    form.AantalProducties = olds.Length;
                    double peruur = Math.Round(olds.Sum(x => x.PerUur) / olds.Length, 0);
                    if (peruur > 0 && form.AantalGemaakt > 0)
                    {
                        form.DoorloopTijd = form.AantalGemaakt / peruur;
                        form.GemiddeldPerUur = peruur;
                        xreturn = true;
                    }
                    if (form.Bewerkingen != null && form.Bewerkingen.Length > 0)
                    {
                        foreach (Bewerking b in form.Bewerkingen)
                        {
                            peruur = 0;
                            int count = 0;
                            foreach (var p in olds)
                            {
                                if (p.Bewerkingen != null && p.Bewerkingen.Length > 0)
                                {
                                    Bewerking[] bws = p.Bewerkingen.Where(x => x.Naam.ToLower() == b.Naam.ToLower()).ToArray();
                                    if (bws.Length > 0)
                                    {
                                        count += bws.Length;
                                        peruur += bws.Sum(x => x.PerUur);
                                    }
                                }
                            }

                            if (count > 0 && peruur > 0 && b.AantalGemaakt > 0)
                            {
                                peruur /= count;
                                b.DoorloopTijd = Math.Round(b.AantalGemaakt / peruur, 1);
                                b.PerUur = peruur;
                                xreturn = true;
                            }
                        }
                    }
                }

                form.VerwachtLeverDatum = form.VerwachtDatumGereed();
            }
            return xreturn;
        }

        public string GetNextTextBlock(string value, out string block)
        {
            int length = GetLengthBySpace(value);
            block = value.Substring(0, length);
            length++;
            return value.Substring(length, value.Length - length);
        }

        public bool InitDefault()
        {
            try
            {
                Bewerkingen = new Bewerking[] { };
                Materialen = new List<Materiaal> { };
                State = ProductieState.Gestopt;
                var types = this.GetType().GetProperties().Where(t => t.CanWrite).ToArray();
                foreach (PropertyInfo type in types)
                {
                    if (type.PropertyType.Equals(typeof(string)))
                    {
                        if (type.GetValue(this) == null)
                            type.SetValue(this, "");
                    }
                    if (type.PropertyType.Equals(typeof(DateTime)))
                    {
                        if ((DateTime)type.GetValue(this) == DateTime.MinValue)
                            type.SetValue(this, DateTime.Now);
                    }
                }
                return true;
            }
            catch { return false; }
        }

        public bool UpdateForm(bool updatebewerking, bool updategemiddeld, string change = "Update Formulier", bool save = true)
        {
            try
            {
                ProductieFormulier[] forms = null;
                if (updategemiddeld)
                {
                    forms = Manager.Database.GetProducties(x => x.ArtikelNr.ToLower() == ArtikelNr.ToLower()).ToArray();
                    GemiddeldPerUur = TotaalGemiddeldAantalPerUur(forms);
                    TotaalTijdGewerkt = TotaalGewerkteUren(forms);
                    AantalProducties = forms.Length;
                }
                if (State == ProductieState.Gestart)
                {
                    TijdGewerkt = TijdAanGewerkt();
                    ActueelPerUur = ActueelProductenPerUur();

                    if (Aantal > 0 && DoorloopTijd > 0)
                        PerUur = Math.Round(Aantal / DoorloopTijd, 0);
                    else PerUur = 0;
                }

                Gereed = GereedPercentage();
                if (State != ProductieState.Gereed && State != ProductieState.Verwijderd)
                    VerwachtLeverDatum = VerwachtDatumGereed();

                if (updatebewerking)
                    if (Bewerkingen != null && Bewerkingen.Length > 0)
                    {
                        foreach (Bewerking b in Bewerkingen)
                            b.UpdateBewerking(forms, $"[{b.Path}] Bewerking Update", false);
                    }
                if (save)
                    Manager.Database.UpSert(this, change);
                else if (State == ProductieState.Gestart)
                    Manager.FormulierChanged(this, this);
                FormulierChanged(this);
                return true;
            }
            catch { return false; }
        }

        #endregion "Data Management"

        #region "Public Methods"

        public ProductieState GetCurrentState()
        {
            if (Bewerkingen == null || Bewerkingen.Length == 0 || _state == ProductieState.Verwijderd)
                return _state;
            if (!Bewerkingen.Any(x => x.State != ProductieState.Gereed))
                return ProductieState.Gereed;
            if (Bewerkingen.Any(x => x.State == ProductieState.Gestart))
                return ProductieState.Gestart;
            return ProductieState.Gestopt;
        }

        public TimeSpan TijdNodig()
        {
            try
            {
                if (Bewerkingen == null || Bewerkingen.Length == 0)
                    return new TimeSpan();
                return TimeSpan.FromHours(Bewerkingen.Sum(x => x.TijdNodig()));
            }
            catch { return new TimeSpan(); }
        }

        public TimeSpan TijdOver()
        {
            return this.WerkTijdNodigTotLeverdatum();
        }

        public int GetViewImageIndex()
        {
            if (IsNieuw && State == ProductieState.Gestopt)
            {
                return 0;// Resources.page_document_16748.CombineImage(Resources.new_25355, 48, 48);
            }
            else if (TeLaat && State == ProductieState.Gestopt)
            {
                return 1;// Resources.page_document_16748.CombineImage(Resources.Warning_36828, 48, 48);
            }
            else
            {
                switch (State)
                {
                    case ProductieState.Gestart:
                        return 2;// Resources.page_document_16748.CombineImage(Resources.play_6048, width, height);
                    case ProductieState.Gestopt:
                        return 3;// Resources.page_document_16748;
                    case ProductieState.Verwijderd:
                        return 4;// Resources.page_document_16748.CombineImage(Resources.delete_1577, width, height);
                    case ProductieState.Gereed:
                        return 5;// Resources.page_document_16748.CombineImage(Resources.check_1582, width, height);
                }
            }
            return -1;
        }

        public bool MeldGereed(int aantal, string paraaf)
        {
            try
            {
                if (State != ProductieState.Gereed)
                {
                    if (State == ProductieState.Verwijderd)
                        throw new Exception("Productie is verwijderd en kan daarom niet gereed gemeld worden.\nVoeg de productie opnieuw toe, en probeer het opnieuw.");
                    double tijd = 0;
                    foreach (Bewerking b in Bewerkingen.Where(t => t.State != ProductieState.Verwijderd && t.State != ProductieState.Gereed))
                    {
                        b.MeldBewerkingGereed(this, paraaf, aantal, false);
                        tijd += b.DoorloopTijd;
                    }
                    if (tijd > 0)
                        DoorloopTijd = tijd;
                    if (DoorloopTijd > 0 && AantalGemaakt > 0)
                        PerUur = Math.Round(AantalGemaakt / DoorloopTijd, 1);
                    State = ProductieState.Gereed;
                }
                DatumGereed = DateTime.Now;
                AantalGemaakt = aantal;
                Paraaf = paraaf;
                //UpdateDoorloopTijd(this);
                UpdateForm(true, true, "Productie Gereed Gemeld");
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public double TijdAanGewerkt()
        {
            DateTime gestopt = State == ProductieState.Gestart ? DateTime.Now : TijdGestopt;
            double tijd = TotaalTijdGewerkt;
            if (Bewerkingen == null || Bewerkingen.Length == 0)
                tijd += Werktijd.TijdGewerkt(TijdGestart, gestopt).TotalHours;
            else
                tijd = Bewerkingen.Sum(x => x.TijdAanGewerkt());
            return Math.Round(tijd, 2);
        }

        public double TotaalGewerkteUren(ProductieFormulier[] forms)
        {
            try
            {
                double value = DoorloopTijd;
                if (forms == null)
                    return value;
                if (forms.Length > 0)
                    value = (forms.Sum(t => t.TijdGewerkt));
                else
                    value = TijdAanGewerkt();
                return Math.Round(value, 2);
            }
            catch { return 0; }
        }

        public double TotaalGemiddeldAantalPerUur(ProductieFormulier[] forms)
        {
            if (forms == null || forms.Length == 0)
                return PerUur;
            var x = forms;
            if (x == null && Bewerkingen?.Length > 0)
                return Math.Round((Bewerkingen.Sum(t => t.PerUur) / Bewerkingen.Length), 0);
            double tg = x.Sum(t => t.Bewerkingen.Sum(t => t.PerUur) / Bewerkingen.Length) / x.Length;
            if (tg > 0)
                return Math.Round(tg, 0);
            return 100;
        }

        private double ActueelProductenPerUur()
        {
            if (Bewerkingen?.Length > 0)
                return Math.Round((Bewerkingen.Sum(t => t.ProductenPerUur()) / Bewerkingen.Length), 0);
            return ProductenPerUur(AantalGemaakt, TijdAanGewerkt());
        }

        public double ProductenPerUur(int aantal, double tijd)
        {
            if (aantal == 0 || tijd == 0)
                return 0;
            return Math.Round((aantal / tijd), 1);
        }

        public double GereedPercentage()
        {
            return Bewerkingen == null || Bewerkingen.Length == 0 ? 0 : Math.Round((Bewerkingen.Sum(t => t.GereedPercentage()) / Bewerkingen.Length), 1);
        }

        public double GereedPercentage(double max, double current)
        {
            if (current > 0)
            {
                double val = Math.Round(((current / max) * 100), 1);
                if (val > 100)
                    val = 100;
                return val;
            }
            return 0;
        }

        public DateTime VerwachtDatumGereed()
        {
            int aantal = Aantal;
            if (AantalGemaakt > Aantal)
                aantal = 0;
            else aantal = Aantal - AantalGemaakt;
            if (aantal == 0)
                return Werktijd.EerstVolgendewerkdag(DateTime.Now);

            double tijd = (aantal / ActueelProductenPerUur());

            if (tijd < 0 || double.IsInfinity(tijd))
                return Werktijd.EerstVolgendewerkdag(DateTime.Now);

            if (Bewerkingen != null)
            {
                foreach (Bewerking b in Bewerkingen)
                    tijd /= b.GetPersoneel().AantalPersTijdMultiplier();
            }

            if (tijd < 0 || double.IsInfinity(tijd))
                return Werktijd.EerstVolgendewerkdag(DateTime.Now);

            return Werktijd.DatumNaTijd(DateTime.Now, TimeSpan.FromHours(tijd));
        }

        public DateTime VerwachtDatumGereed(DateTime start, double tijd, int aantal, int gedaan, Bewerking[] bewerkingen)
        {
            if (gedaan >= aantal)
                return Werktijd.EerstVolgendewerkdag(DateTime.Now);

            double peruur = aantal / tijd;

            if (double.IsInfinity(peruur) || peruur == 0)
                return Werktijd.EerstVolgendewerkdag(DateTime.Now);

            int xaantal = aantal - gedaan;

            double xtijd = xaantal / peruur;

            if (xaantal == 0)
                return Werktijd.EerstVolgendewerkdag(DateTime.Now);

            if (bewerkingen != null)
            {
                foreach (Bewerking b in bewerkingen)
                    xtijd /= b.GetPersoneel().AantalPersTijdMultiplier();
            }
            return Werktijd.DatumNaTijd(DateTime.Now, TimeSpan.FromHours(xtijd));
        }

        public Personeel[] AantalPersonenShifts()
        {
            if (Bewerkingen == null)
                return new Personeel[] { };
            List<Personeel> shifts = new List<Personeel> { };
            foreach (var bew in Bewerkingen)
                shifts.AddRange(bew.GetPersoneel().Where(x => x.Actief));
            return shifts.ToArray();
        }

        public int AantalPersonenNodig()
        {
            TimeSpan tijdover = TijdOver();
            if (tijdover.TotalHours <= 0)
                return 0;
            int aantal = Aantal - AantalGemaakt;
            return (int)Math.Ceiling(((aantal / ActueelProductenPerUur()) / tijdover.TotalHours));
        }

        #endregion "Public Methods"

        #region "Private Methods"

        private int MeestGemaaktBewerking()
        {
            if (Bewerkingen == null || Bewerkingen.Length == 0)
                return AantalGemaakt;
            int x = 0;
            foreach (Bewerking b in Bewerkingen)
            {
                if (b.AantalGemaakt > x)
                    x = b.AantalGemaakt;
            }
            return x;
        }

        private static string RemoveDots(string value)
        {
            int index = -1;
            int count = 0;
            string xreturn = value;
            while ((index = xreturn.IndexOf('.', index + 1)) > -1 && index < xreturn.Length)
            {
                for (int i = index; i < xreturn.Length; i++)
                {
                    if (xreturn[i] == '.')
                    {
                        count++;
                    }
                    else break;
                }
                if (count > 1)
                {
                    xreturn = xreturn.Remove(index, count);
                }
                count = 0;
            }
            return xreturn;
        }

        private static string[] KeyValues
        {
            get
            {
                return new string[] { "productienr.", "leverdatum", "doorlooptijd:", "benodigde uren:", "artikelnr.", "omschrijving", "benodigde materialen", "toelichting bewerking" };
            }
        }

        private static int GetLengthBySpace(string text)
        {
            if (text == null)
                return -1;
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i] == ' ' || text[i] == '\n')
                    return i;
            }
            return text.Length;
        }

        #endregion "Private Methods"

        #region "Events"

        public event FormulierChangedHandler OnFormulierChanged;

        public void FormulierChanged(object sender)
        {
            OnFormulierChanged?.Invoke(sender, this);
        }

        public event FormulierActieHandler OnFormulierActie;

        public void FormulierActie(Bewerking bew, AktieType type)
        {
            OnFormulierActie?.Invoke(this, bew, type);
        }

        public event BewerkingChangedHandler OnBewerkingChanged;

        public void BewerkingChanged(object sender, Bewerking bew)
        {
            string change = bew.LastChanged == null || bew.LastChanged.Change == null ? $"[{bew.Path}] Bewerking Gewijzigd" : bew.LastChanged.Change;
            Manager.Database.UpSert(this, change);
            OnBewerkingChanged?.Invoke(sender, bew);
            FormulierChanged(sender);
            Manager.BewerkingChanged(sender, bew);
        }

        #endregion "Events"

        public override bool Equals(object obj)
        {
            ProductieFormulier form = obj as ProductieFormulier;
            if (form == null)
                return false;
            return ProductieNr.ToLower() == form.ProductieNr.ToLower();
        }

        public override int GetHashCode()
        {
            return ProductieNr.GetHashCode();
        }
    }
}