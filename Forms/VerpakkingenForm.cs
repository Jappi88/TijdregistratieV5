﻿using BrightIdeasSoftware;
using ProductieManager.Properties;
using Rpm.Productie;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Forms
{
    public partial class VerpakkingenForm : Forms.MetroBase.MetroBaseForm
    {
        private List<VerpakkingInstructie> Verpakkingen { get; set; } = new List<VerpakkingInstructie>();
        public VerpakkingenForm()
        {
            InitializeComponent();
            imageList1.Images.Add(Resources.package_box_10801);
            ((OLVColumn) xVerpakkingen.Columns[0]).ImageGetter = (x) => 0;
        }

        private void InitList(bool reload)
        {
            try
            {
                var xselected = xVerpakkingen.SelectedObject;
                xVerpakkingen.BeginUpdate();
                if (reload)
                {
                    Verpakkingen?.Clear();
                    if (Manager.Verpakkingen is {Disposed: false})
                    {
                        Verpakkingen = Manager.Verpakkingen.GetAlleVerpakkingen();
                    }
                }

                string xfilter = xsearch.Text.Trim();
                var xitems = Verpakkingen;
                if (xfilter.Length > 0)
                    xitems = Verpakkingen.Where(x => x.ContainsFilter(xfilter)).ToList();
                xVerpakkingen.SetObjects(xitems);
                xVerpakkingen.SelectedObject = xselected;
                if (xVerpakkingen.SelectedObject == null)
                {
                    xVerpakkingen.SelectedIndex = 0;
                }

                xVerpakkingen.SelectedItem?.EnsureVisible();
                UpdateFields();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            xVerpakkingen.EndUpdate();
        }

        private void metroTextBox1_TextChanged(object sender, System.EventArgs e)
        {
            InitList(false);
        }

        private void VerpakkingenForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Manager.VerpakkingChanged -= Manager_VerpakkingChanged;
            Manager.VerpakkingDeleted -= Manager_VerpakkingDeleted;
        }

        private void Manager_VerpakkingDeleted(object sender, System.EventArgs e)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    if (sender is string value)
                    {
                        var xremoved = Verpakkingen.RemoveAll(x =>
                            string.Equals(x.ArtikelNr, value, StringComparison.CurrentCultureIgnoreCase));
                        if (xremoved > 0)
                        {
                            InitList(false);
                        }
                    }

                }));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void Manager_VerpakkingChanged(object sender, System.EventArgs e)
        {
            try
            {
                this.BeginInvoke(new MethodInvoker(() =>
                {
                    if (sender is VerpakkingInstructie verpakking)
                    {
                        var xold = Verpakkingen.FirstOrDefault(x =>
                            string.Equals(x.ArtikelNr, verpakking.ArtikelNr, StringComparison.CurrentCultureIgnoreCase));
                        if (xold != null)
                        {
                            var xindex = Verpakkingen.IndexOf(xold);
                            if (xindex != -1)
                            {
                                Verpakkingen[xindex] = verpakking;
                            }
                        }
                        else
                        {
                            Verpakkingen.Add(verpakking);
                            xold = verpakking;
                        }

                        UpdateVerpakking(verpakking);
                    }

                }));
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }

        }

        private void UpdateVerpakking(VerpakkingInstructie verpakking)
        {
            try
            {
                var xitems = xVerpakkingen.Objects.Cast<VerpakkingInstructie>().ToList();
                var xold = Verpakkingen.FirstOrDefault(x =>
                    string.Equals(x.ArtikelNr, verpakking.ArtikelNr, StringComparison.CurrentCultureIgnoreCase));
                bool valid = verpakking != null && verpakking.ContainsFilter(xsearch.Text.Trim());

                if (xold == null && valid)
                {
                    xVerpakkingen.AddObject(verpakking);
                    xVerpakkingen.SelectedObject = verpakking;
                    xVerpakkingen.SelectedItem?.EnsureVisible();
                }
                else if (xold != null)
                {
                    if (valid)
                    {
                        xVerpakkingen.RefreshObject(verpakking);
                    }
                    else xVerpakkingen.RemoveObject(xold);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void VerpakkingenForm_Shown(object sender, System.EventArgs e)
        {
            Manager.VerpakkingChanged += Manager_VerpakkingChanged;
            Manager.VerpakkingDeleted += Manager_VerpakkingDeleted;
            InitList(true);
        }

        private void xVerpakkingen_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateFields();
        }

        private void UpdateFields()
        {
            try
            {
                xdelete.Enabled = xVerpakkingen.SelectedObjects.Count > 0;
                verwijderenToolStripMenuItem.Enabled = xdelete.Enabled;
                if (xVerpakkingen.SelectedObject is VerpakkingInstructie xverp)
                {
                    verpakkingInstructieUI1.AllowEditMode = true;
                    verpakkingInstructieUI1.InitFields(xverp, verpakkingInstructieUI1.IsEditmode, $"Aangepaste VerpakkingsInstructie voor '{xverp.ArtikelNr}'", Color.SaddleBrown,
                        Color.White);
                }
                else
                {
                    verpakkingInstructieUI1.Clear();
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void xdelete_Click(object sender, EventArgs e)
        {
            if (xVerpakkingen.SelectedObjects.Count > 0)
            {
                if (XMessageBox.Show(this, $"Weetje zekere dat je alle geselecteerde verpakkingen wilt verwijderen?",
                        "Verwijderen", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                {
                    var xitems = xVerpakkingen.SelectedObjects.Cast<VerpakkingInstructie>();
                    foreach (var item in xitems)
                    {
                        if (Manager.Verpakkingen is {Disposed: false})
                        {
                            Manager.Verpakkingen.RemoveVerpakking(item);
                        }
                    }
                    UpdateFields();
                }
            }
        }
    }
}
