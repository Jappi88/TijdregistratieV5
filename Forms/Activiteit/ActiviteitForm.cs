using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ProductieManager.Properties;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.SqlLite;
using Rpm.Various;

namespace Forms.Activiteit
{
    public partial class ActiviteitForm : MetroBase.MetroBaseForm
    {
        private readonly List<UserChange> _changes;
        public ActiviteitForm()
        {
            InitializeComponent();
            imageList1.Images.Add(Resources.infolog);
            ((OLVColumn)xActiviteitList.Columns[0]).ImageGetter = (x)=> 0;
            _changes = GetLogs();
            UpdateList();
            xActiviteitList.Sort(0);
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            try
            {
                if (changedform is { Bewerkingen: { Length: > 0 } })
                {
                    foreach (var bw in changedform.Bewerkingen)
                    {
                        if(bw.IsAllowed() && bw.LastChanged != null)
                            UpdateLogs(bw.LastChanged, bw);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void UpdateLogs(UserChange change, Bewerking bew)
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(() =>UpdateLogs(change, bew)));
                else
                {
                    if (change != null)
                    {
                        change = change.CreateCopy();
                        var index = _changes.IndexOf(change);
                        change.Reference = bew.Path;
                        if (index == -1)
                        {
                            _changes.Add(change);
                            if (!IsAllowed(change)) return;
                            xActiviteitList.AddObject(change);
                            xActiviteitList.SelectedObject =change;
                            xActiviteitList.SelectedItem?.EnsureVisible();
                        }
                        else
                        {
                            xActiviteitList.RefreshObject(change);
                            _changes[index] = change;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private List<UserChange> GetLogs()
        {
            var changes = new List<UserChange>();
            try
            {
                var prods = Manager.Database.GetAllBewerkingen(false, true, true).Result;
               foreach (var bewerking in prods)
               {

                   var change = bewerking.LastChanged?.CreateCopy();
                   if (change == null) continue;
                   change.Reference = bewerking.Path;
                   changes.Add(change);
               }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return changes.OrderBy(x=> x.TimeChanged).ToList();
        }

        private bool IsAllowed(UserChange change)
        {
            if (change == null) return false;
            if (xvandaag.Checked && change.TimeChanged.Date != DateTime.Now.Date)
                return false;
            var xcrit = xsearch.Text.Trim().ToLower();
            if (string.IsNullOrEmpty(xcrit)) return true;
            return change.User != null && change.User.ToLower().Contains(xcrit) ||
                   change.Change != null && change.Change.ToLower().Contains(xcrit);
        }

        private void UpdateList()
        {
            try
            {


                var items = _changes.Where(IsAllowed).ToList();
                xActiviteitList.BeginUpdate();
                xActiviteitList.SetObjects(items);
                xActiviteitList.EndUpdate();
                if (items.Count > 0)
                {
                    xActiviteitList.SelectedObject = items[items.Count - 1];
                    xActiviteitList.SelectedItem?.EnsureVisible();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void ActiviteitForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            _changes.Clear();
        }

        private void xsearch_TextChanged(object sender, EventArgs e)
        {
            UpdateList();
        }

        private void xActiviteitList_DoubleClick(object sender, EventArgs e)
        {
            if (xActiviteitList.SelectedObject is UserChange change)
            {
                var werk = Werk.FromPath(change.Reference);
                if (werk?.Bewerking != null)
                {
                    Manager.FormulierActie(new object[] { werk.Formulier, werk.Bewerking }, MainAktie.OpenProductie);
                }
            }
        }

        private void xvandaag_CheckedChanged(object sender, EventArgs e)
        {
            UpdateList();
        }
    }
}
