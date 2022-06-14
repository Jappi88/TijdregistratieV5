using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Forms
{
    public partial class ProductieDatumOvericht : MetroBase.MetroBaseForm
    {
        public class BewerkingComparer : IEqualityComparer<Bewerking>
        {
            public bool Equals(Bewerking x, Bewerking y)
            {
                return string.Equals(x.ProductieNr, y.ProductieNr, StringComparison.CurrentCultureIgnoreCase);
            }

            public int GetHashCode(Bewerking obj)
            {
                return obj.ProductieNr?.GetHashCode()??0;
            }
        }

        private List<Bewerking> Models = new List<Bewerking>();

        public ProductieDatumOvericht(List<Bewerking> bws)
        {
            InitializeComponent();
            treeView1.NodeMouseClick += (sender, args) =>
            {
                if (args.Node != null)
                    args.Node.SelectedImageIndex = args.Node.ImageIndex;
                treeView1.SelectedNode = args.Node;
            };
            imageList1.Images.Add(Resources.ic_done_128_28244);//selected
            imageList1.Images.Add(Resources.operation_32x32.CombineImage(Resources.play_button_icon_icons_com_60615, 1.75));//gestarte bewerkingen
            imageList1.Images.Add(Resources.operation_32x32.CombineImage(Resources.check_1582, 1.75));//gereed bewerkingen
            imageList1.Images.Add(Resources.operation_32x32);//bewerkingen
            imageList1.Images.Add(Resources.infolog);//fieldInfo
            imageList1.Images.Add(Resources.documents_32x32);//List
            imageList1.Images.Add(Resources.page_document_16748.CombineImage(Resources.play_button_icon_icons_com_60615, 1.75));//gestarte producties
            imageList1.Images.Add(Resources.page_document_16748.CombineImage(Resources.check_1582, 1.75));//gereed producties
            imageList1.Images.Add(Resources.page_document_16748);//producties
            Models = bws; 
            LoadModels();
            xsearchbox.ShowClearButton = true;
        }



        private void LoadModels()
        {
            try
            {
                var filter = xsearchbox.Text.Trim();
                var models = GetOverzichtModels(Models).ToList();
                var tns = ModelsToTreeNode(models,filter);
                treeView1.Nodes.Clear();
                treeView1.Nodes.AddRange(tns.ToArray());
            }
            catch { }
        }

        private ProductieOverzichtModel GetProductieModel(Bewerking b, string lastartnr, ref DateTime gereeddate, ref DateTime startdate)
        {
           var model = new ProductieOverzichtModel(b);
            model.Omschrijving = b.Naam;
            model.StartOp = startdate;
            bool ombouw = !string.Equals(lastartnr, b.ArtikelNr, StringComparison.CurrentCultureIgnoreCase);
            switch (b.State)
            {
                case ProductieState.Gestart:
                    model.StartOp = b.GestartOp();
                    model.GereedOp = b.VerwachtDatumGereed();
                    model.AantalPersonen = b.AantalActievePersonen;
                    model.InstelTijd = b.InstelTijd;
                    if (b.TotaalGemaakt < b.Aantal)
                    {
                        startdate = model.GereedOp;
                        gereeddate = model.GereedOp;
                    }
                    break;
                case ProductieState.Gestopt:
                    model.StartOp = b.GetStartOp(startdate, out var pers);
                    model.GereedOp = b.VerwachtDatumGereed(startdate, pers, ombouw);
                    model.AantalPersonen = pers;
                    model.InstelTijd = b.InstelTijd;
                    if (b.TotaalGemaakt < b.Aantal)
                    {
                        startdate = model.GereedOp;
                        gereeddate = model.GereedOp;
                    }
                    break;
                case ProductieState.Gereed:
                    model.StartOp = b.GestartOp();
                    model.GereedOp = b.DatumGereed;
                    model.AantalPersonen = b.AantalActievePersonen;
                    model.InstelTijd = b.InstelTijd;
                    break;
            }
            return model;
        }

        private ProductieOverzichtModel GetProductieModel(ProductieFormulier p, string lastartnr, ref DateTime date, ref DateTime startdate)
        {
            if (p.Bewerkingen == null || p.Bewerkingen.Length == 0)
                return null;
            var model = new ProductieOverzichtModel(p);
            if (p.TotaalGemaakt < p.Aantal)
            {
                model.StartOp = startdate;
                model.GereedOp = date;
            }
            else
            {
                var rooster = Manager.Opties?.GetWerkRooster() ?? Rooster.StandaartRooster();
                model.StartOp = Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster, rooster, Manager.Opties?.SpecialeRoosters);
                model.GereedOp = model.StartOp;
            }
            switch (p.State)
            {
                case ProductieState.Gestart:
                case ProductieState.Gereed:
                    model.StartOp = p.GestartOp();
                    break;
            }
            for (int j = 0; j < p.Bewerkingen.Length; j++)
            {
                var b = p.Bewerkingen[j];
                var bmodel = GetProductieModel(b, lastartnr, ref date, ref startdate);
                if (bmodel != null)
                {
                    model.ChildrenModels.Add(bmodel);
                    if (bmodel.GereedOp > model.GereedOp)
                        model.GereedOp = bmodel.GereedOp;
                    if (date > startdate)
                        startdate = date;
                }
            }
            if (p.State == ProductieState.Gereed)
                model.GereedOp = p.DatumGereed;
            if(model.ChildrenModels.Count > 0)
            {
                var aantal = model.ChildrenModels.Sum(x => x.Aantal);
                if(aantal > 0)
                {
                    model.Aantal = aantal;
                    model.AantalGemaakt = model.ChildrenModels.Sum(x => x.AantalGemaakt);
                }
            }
            return model;
        }

        private List<TreeNode> ModelsToTreeNode(List<ProductieOverzichtModel>  models, string filter)
        {
            var xret = new List<TreeNode>();
            for(int i = 0; i < models.Count; i++)
            {
                var model = models[i];
                var tn = ModelToTreeNode(model,filter);
                if(tn != null)
                {
                    xret.Add(tn);
                }

            }
            return xret;
        }

        private TreeNode ModelToTreeNode(ProductieOverzichtModel model, string filter)
        {
            if (model == null) return null;
            var hdr = model.Omschrijving;
            if(!model.IsBewerking && !string.IsNullOrEmpty(model.ArtikelNr) && !string.IsNullOrEmpty(model.ProductieNr))
            {
                hdr = $"[{model.ArtikelNr}, {model.ProductieNr}]" + hdr;
            }
            var tn = new TreeNode(hdr);
            tn.Tag = model;
            var xbase = model.Model is ProductieFormulier ? 5 : 0;
            tn.ImageIndex = 3 + xbase;
            if (model.Model != null)
            {
                if (!model.Model.ContainsFilter(filter)) return null;
                switch (model.Model.State)
                {
                    case ProductieState.Gestart:
                        tn.ImageIndex = 1 + xbase;
                        break;
                    case ProductieState.Gereed:
                        tn.ImageIndex = 2 + xbase;
                        break;
                }
            }
            else tn.ImageIndex = 5;
            if (!model.StartOp.IsDefault())
                tn.Nodes.Add(new TreeNode("Starten Op: " + model.StartOp.ToString("f"),4,0));
            if (!model.GereedOp.IsDefault())
                tn.Nodes.Add(new TreeNode("Gereed Op: " + model.GereedOp.ToString("f"), 4, 0));
            if (!model.LeverDatum.IsDefault())
                tn.Nodes.Add(new TreeNode("LeverDatum: " + model.LeverDatum.ToString("f"), 4, 0));
            if (model.PerUur > 0)
                tn.Nodes.Add(new TreeNode($"Aantal PerUur: {model.PerUur} p/u",4,0));
            if(model.Aantal > 0)
                tn.Nodes.Add(new TreeNode($"Aantal Gemaakt: {model.AantalGemaakt} / {model.Aantal}", 4, 0));
            if (model.InstelTijd > 0)
                tn.Nodes.Add(new TreeNode($"InstelTijd: {model.InstelTijd} uur", 4, 0));

            if(model.ChildrenModels.Count > 0)
            {
                var x1 = model.ChildrenModels.Count == 1 ? "productie" : "producties";
                if(model.ContainsBewerkingen)
                    x1 = model.ChildrenModels.Count == 1 ? "bewerking" : "bewerkingen";
                var nodes = ModelsToTreeNode(model.ChildrenModels, filter);
                if (nodes.Count > 0)
                {
                    var xtn = new TreeNode($"Bevat {nodes.Count} {x1}", 5, 0);
                    xtn.Nodes.AddRange(nodes.ToArray());
                    tn.Nodes.Add(xtn);
                    if (model.Model == null)
                        tn.Text = tn.Text + $" ({nodes.Count})";
                }
                else return null;
            }
            return tn;
        }

        private List<ProductieOverzichtModel> GetOverzichtModels(List<Bewerking> bws)
        {
            var models = new List<ProductieOverzichtModel>();
            try
            {
                var rooster = Manager.Opties?.GetWerkRooster() ?? Rooster.StandaartRooster();
                var curdate = Werktijd.EerstVolgendeWerkdag(DateTime.Now, ref rooster, rooster, Manager.Opties?.SpecialeRoosters);
                var startdate = new DateTime();
                var prods = bws.Distinct(new BewerkingComparer()).Select(x => x.Parent);
                var gestart = prods.Where(x => x.State is ProductieState.Gestart).OrderBy(x=> x.GestartOp()).ToList();
                var gestopt = prods.Where(x => x.State is ProductieState.Gestopt).OrderBy(x=> x.LeverDatum).ThenBy(x=> x.WerkplekkenName).ThenBy(x=> x.ArtikelNr).ToList();
                var gereed = prods.Where(x => x.State is ProductieState.Gereed).OrderBy(x=> x.DatumGereed).ToList();
               
                if (gestart.Count > 0)
                {
                    var model = GetOverzichtModel(gestart, ref startdate, ref curdate, "Actieve");
                    if(model != null)
                        models.Add(model);
                }

                if (gestopt.Count > 0)
                {
                    startdate = curdate;
                    var model = GetOverzichtModel(gestopt, ref startdate, ref curdate, "Inactieve");
                    if (model != null)
                        models.Add(model);
                }

                if (gereed.Count > 0)
                {
                    startdate = DateTime.MaxValue;
                    curdate = DateTime.Now;
                    var model = GetOverzichtModel(gereed, ref startdate, ref curdate, "Gereed");
                    if (model != null)
                        models.Add(model);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return models;
        }

        private ProductieOverzichtModel GetOverzichtModel(List<ProductieFormulier> prods, ref DateTime startdate, ref DateTime gereeddate, string modeltype)
        {
            try
            {
                var model = new ProductieOverzichtModel();
                model.StartOp = startdate;
                for (int i = 0; i < prods.Count; i++)
                {
                    var p = prods[i];
                    var last = i > 0 ? prods[i - 1].ArtikelNr : null;
                    var info = GetProductieModel(p, last, ref gereeddate, ref startdate);
                    if (info != null)
                    {
                        model.ChildrenModels.Add(info);
                        if(info.StartOp < model.StartOp || model.StartOp.IsDefault())
                            model.StartOp = info.StartOp;
                    }
                }
                if (model.ChildrenModels.Count > 0)
                {
                    model.ChildrenModels = model.ChildrenModels.OrderBy(x => x.StartOp).ToList();
                    var x1 = model.ChildrenModels.Count == 1 ? "productie" : "producties";
                    model.Omschrijving = $"{modeltype} {x1}";
                    model.GereedOp = gereeddate;
                    model.PerUur = model.ChildrenModels.Sum(x => x.PerUur) / model.ChildrenModels.Count;
                    var aantal = model.ChildrenModels.Sum(x => x.Aantal);
                    if (aantal > 0)
                    {
                        model.Aantal = aantal;
                        model.AantalGemaakt = model.ChildrenModels.Sum(x => x.AantalGemaakt);
                    }
                    if (model.StartOp.IsDefault())
                        model.StartOp = startdate;
                    return model;
                }
                return null;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        private void xsearchbox_TextChanged(object sender, EventArgs e)
        {
            searchtimer.Stop();
            xsearchbox.ShowClearButton = true;
            xsearchbox.Invalidate();
            searchtimer.Start();
        }

        private void searchtimer_Tick(object sender, EventArgs e)
        {
            searchtimer.Stop();
            LoadModels();
        }

        private void vouwAllesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selnode = treeView1.SelectedNode;
            if (selnode == null)
                treeView1.CollapseAll();
            else
            {
                if (selnode.Nodes.Count > 0)
                    selnode.Collapse(false);
                else selnode.Parent?.Collapse(false);
            }
        }

        private void ontvouwAllesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var selnode = treeView1.SelectedNode;
            if (selnode == null)
                treeView1.ExpandAll();
            else
            {
                if (selnode.Nodes.Count > 0)
                    selnode.Expand();
                else selnode.Parent?.ExpandAll();
            }
        }
    }
}
