﻿using System;
using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Productie;

namespace Forms
{
    public partial class MateriaalForm : MetroBaseForm
    {
        public MateriaalForm()
        {
            InitializeComponent();
        }

        public ProductieFormulier Formulier { get; set; }

        public DialogResult ShowDialog(ProductieFormulier form)
        {
            if (form != null)
            {
                Formulier = form;
                Text = $"Materialen Voor: [{form.ProductieNr}]|[{form.ArtikelNr}] {form.Omschrijving}";
                materiaalUI1.InitMaterialen(form);
                return base.ShowDialog();
            }

            return DialogResult.Cancel;
        }

        private void xok_Click(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged -= Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted -= Manager_OnFormulierDeleted;

            if (materiaalUI1.SaveMaterials())
                DialogResult = DialogResult.OK;
            else
                DialogResult = DialogResult.Cancel;
        }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void MateriaalForm_Shown(object sender, EventArgs e)
        {
            Manager.OnFormulierChanged += Manager_OnFormulierChanged;
            Manager.OnFormulierDeleted += Manager_OnFormulierDeleted;
        }

        private void Manager_OnFormulierDeleted(object sender, string id)
        {
            var prodnr = Formulier?.ProductieNr;
            if (IsDisposed || Formulier == null || id == null || !string.Equals(id, prodnr)) return;
            BeginInvoke(new MethodInvoker(Close));
        }

        private void Manager_OnFormulierChanged(object sender, ProductieFormulier changedform)
        {
            var prodnr = Formulier?.ProductieNr;
            if (changedform == null || !string.Equals(changedform.ProductieNr, prodnr)) return;
            Formulier = changedform;
            materiaalUI1.Formulier = Formulier;
        }
    }
}