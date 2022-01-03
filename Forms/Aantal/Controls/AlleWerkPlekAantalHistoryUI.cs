using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework.Controls;
using Rpm.Productie;

namespace Forms.Aantal.Controls
{
    public partial class AlleWerkPlekAantalHistoryUI : UserControl
    {
        public Bewerking Productie { get; private set; }
        public AlleWerkPlekAantalHistoryUI()
        {
            InitializeComponent();
        }

        public void UpdateBewerking(Bewerking bew)
        {
            try
            {
                if (this.InvokeRequired)
                    this.Invoke(new MethodInvoker(() => xUpdateBewerking(bew)));
                else xUpdateBewerking(bew);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xUpdateBewerking(Bewerking bew)
        {
            try
            {
                Productie = bew;
                if (bew?.WerkPlekken == null)
                {
                    xWerkplekTabs.TabPages.Clear();
                    return;
                }

                var tabs = xWerkplekTabs.TabPages.Count > 0
                    ? xWerkplekTabs.TabPages.Cast<MetroTabPage>().ToList()
                    : new List<MetroTabPage>();
                var remove = tabs.Where(x => !bew.WerkPlekken.Any(b => b.Equals(x.Tag)));
                xWerkplekTabs.SuspendLayout();
                foreach (var tab in remove)
                {
                    xWerkplekTabs.TabPages.Remove(tab);
                    tabs.Remove(tab);
                }

                var selected = xWerkplekTabs.SelectedIndex;
                foreach (var plek in bew.WerkPlekken)
                {
                    var xold = tabs.FirstOrDefault(x => plek.Equals(x.Tag));
                    if (xold != null)
                    {
                        if (xold.Controls.Count > 0)
                        {
                            foreach(var c in xold.Controls)
                                if (c is AantalGemaaktHistoryUI history)
                                    history.UpdateList(plek);
                        }
                    }
                    else
                    {
                        xold = new MetroTabPage();
                        xold.Text = plek.Naam;
                        xold.Tag = plek;
                        var xhistory = new AantalGemaaktHistoryUI();
                        xhistory.Dock = DockStyle.Fill;
                        xhistory.UpdateList(plek);
                        xold.Controls.Add(xhistory);
                        xWerkplekTabs.TabPages.Add(xold);
                        xWerkplekTabs.Invalidate();
                    }
                }

                if (xWerkplekTabs.TabPages.Count > 0)
                    xWerkplekTabs.SelectedIndex =
                        selected < xWerkplekTabs.TabPages.Count && selected > -1 ? selected : 0;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                xWerkplekTabs.ResumeLayout(true);
            }
        }
    }
}
