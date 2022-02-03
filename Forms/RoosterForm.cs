﻿using System;
using System.Drawing;
using System.Windows.Forms;
using Rpm.Misc;
using Rpm.Productie;

namespace Forms
{
    public partial class RoosterForm : Forms.MetroBase.MetroBaseForm
    {
        public RoosterForm()
        {
            InitializeComponent();
            ViewPeriode = false;
        }

        public RoosterForm(Rooster rooster, string title = null) : this()
        {
            WerkRooster = rooster?.CreateCopy()??Manager.Opties.GetWerkRooster();
            roosterUI1.WerkRooster = WerkRooster;
            if (WerkRooster != null)
            {
                if (WerkRooster.GebruiktVanaf)
                {
                    xvanafdate.Checked = true;
                    xvanafdate.SetValue(WerkRooster.Vanaf);
                }

                if (WerkRooster.GebruiktTot)
                {
                    xtotdate.Checked = true;
                    xtotdate.SetValue(WerkRooster.Tot);
                }
            }

            if (title != null)
            {
                this.Text = title;
                this.Invalidate();
            }
        }

        public Rooster WerkRooster { get; set; }

        public bool ViewPeriode
        {
            get => xperiodegroup.Visible;
            set
            {
                xperiodegroup.Visible = value;
                if (value)
                {
                    this.MinimumSize = new Size(550, 600);
                    
                }
                else
                {
                    this.MinimumSize = new Size(550, 500);
                }

                this.Size = this.MinimumSize;
            }
        }

        private void xcancelb_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void xokb_Click(object sender, EventArgs e)
        {
            WerkRooster = roosterUI1.WerkRooster;
            WerkRooster.GebruiktVanaf = xvanafdate.Checked;
            WerkRooster.GebruiktTot = xtotdate.Checked;
            if (xvanafdate.Checked)
                WerkRooster.Vanaf = xvanafdate.Value;
            if (xtotdate.Checked)
                WerkRooster.Tot = xtotdate.Value;
            DialogResult = DialogResult.OK;
        }

        private void xgebruikperiode_CheckedChanged(object sender, EventArgs e)
        {
            ViewPeriode = xgebruikperiode.Checked;
        }
    }
}