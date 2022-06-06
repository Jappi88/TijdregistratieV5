using Forms;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Various;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Controls
{
    public partial class RoosterUI : UserControl
    {
        private Rooster _rooster;

        public RoosterUI()
        {
            InitializeComponent();
        }

        public Rooster WerkRooster
        {
            get => _rooster;
            set => SetRooster(value);
        }

        public bool ShowSpecialeRoosterButton
        {
            get => xspeciaalroosterb.Visible;
            set => xspeciaalroosterb.Visible = value;
        }
        public bool ShowNationaleFeestDagen
        {
            get => xnationaleFeestdageGroup.Visible;
            set => xnationaleFeestdageGroup.Visible = value;
        }

        public bool AutoUpdateBewerkingen { get; set; }

        public List<Rooster> SpecialeRoosters { get; set; }

        public void SetRooster(Rooster rooster)
        {
            _rooster = rooster ?? Manager.Opties?.WerkRooster?.CreateCopy();
            if (_rooster != null)
            {
                xstartwerkdag.Value = Convert.ToDateTime(_rooster.StartWerkdag.ToString());
                xeindwerkdag.Value = Convert.ToDateTime(_rooster.EindWerkdag.ToString());

                xstartpauze1.Value = Convert.ToDateTime(_rooster.StartPauze1.ToString());
                xstartpauze2.Value = Convert.ToDateTime(_rooster.StartPauze2.ToString());
                xstartpauze3.Value = Convert.ToDateTime(_rooster.StartPauze3.ToString());

                xduurpauze1.Value = Convert.ToDateTime(_rooster.DuurPauze1.ToString());
                xduurpauze2.Value = Convert.ToDateTime(_rooster.DuurPauze2.ToString());
                xduurpauze3.Value = Convert.ToDateTime(_rooster.DuurPauze3.ToString());

                xgebruiktpauze.Checked = _rooster.GebruiktPauze;
            }
        }

        public void SetRooster(Rooster rooster, DateTime[] nationalefeestdagen, List<Rooster> specialeroosters)
        {
            SetRooster(rooster);
            nationalefeestdagen ??= Manager.Opties?.NationaleFeestdagen;
            SpecialeRoosters = specialeroosters?? Manager.Opties?.SpecialeRoosters??new List<Rooster>();
            ListFeestdagen(nationalefeestdagen);
        }

        private Rooster GetRooster()
        {
            return new Rooster
            {
                StartWerkdag = xstartwerkdag.Value.TimeOfDay,
                EindWerkdag = xeindwerkdag.Value.TimeOfDay,
                StartPauze1 = xstartpauze1.Value.TimeOfDay,
                StartPauze2 = xstartpauze2.Value.TimeOfDay,
                StartPauze3 = xstartpauze3.Value.TimeOfDay,
                DuurPauze1 = xduurpauze1.Value.TimeOfDay,
                DuurPauze2 = xduurpauze2.Value.TimeOfDay,
                DuurPauze3 = xduurpauze3.Value.TimeOfDay,
                GebruiktPauze = xgebruiktpauze.Checked
            };
        }

        private void xgebruiktpauze_CheckedChanged(object sender, EventArgs e)
        {
            xpauzetijdengroup.Enabled = xgebruiktpauze.Checked;
            _rooster.GebruiktPauze = xgebruiktpauze.Checked;
            OnValueChanged(sender);
        }

        private void xstartwerkdag_ValueChanged(object sender, EventArgs e)
        {
            if (sender is DateTimePicker nrc)
            {
                var max = new TimeSpan(1, 0, 0);
                switch (nrc.Name)
                {
                    case "xstartwerkdag":
                        _rooster.StartWerkdag = xstartwerkdag.Value.TimeOfDay;
                        break;
                    case "xeindwerkdag":
                        _rooster.EindWerkdag = xeindwerkdag.Value.TimeOfDay;
                        break;
                    case "xstartpauze1":
                        _rooster.StartPauze1 = xstartpauze1.Value.TimeOfDay;
                        break;
                    case "xstartpauze2":
                        _rooster.StartPauze2 = xstartpauze2.Value.TimeOfDay;
                        break;
                    case "xstartpauze3":
                        _rooster.StartPauze3 = xstartpauze3.Value.TimeOfDay;
                        break;
                    case "xduurpauze1":
                        if (nrc.Value.TimeOfDay >= max)
                            nrc.Value = nrc.Value.Date + max;
                        _rooster.DuurPauze1 = xduurpauze1.Value.TimeOfDay;
                        break;
                    case "xduurpauze2":
                        if (nrc.Value.TimeOfDay >= max)
                            nrc.Value = nrc.Value.Date + max;
                        _rooster.DuurPauze2 = xduurpauze2.Value.TimeOfDay;
                        break;
                    case "xduurpauze3":
                        if (nrc.Value.TimeOfDay >= max)
                            nrc.Value = nrc.Value.Date + max;
                        _rooster.DuurPauze3 = xduurpauze3.Value.TimeOfDay;
                        break;
                }

                OnValueChanged(sender);
            }
        }

        public List<DateTime> NationaleFeestdagen()
        {
            var xlist = new List<DateTime>();
            try
            {
              
                foreach (ListViewItem lv in xfeestdagen.Items)
                {
                    var dt = (DateTime) lv.Tag;
                    if (!xlist.Contains(dt))
                        xlist.Add(dt);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return xlist;
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            xremovefeestdag.Visible = xfeestdagen.SelectedItems.Count > 0;
        }

        private void xaddfesstdag_Click(object sender, EventArgs e)
        {
            var date = xfeestdagdate.Value.Date.ToString("dddd dd MMMM yyyy");
            var lv = new ListViewItem(date);
            lv.Tag = xfeestdagdate.Value.Date;
            var exist = false;
            foreach (ListViewItem x in xfeestdagen.Items)
                if (string.Equals(x.Text, date, StringComparison.CurrentCultureIgnoreCase))
                {
                    exist = true;
                    break;
                }

            if (exist)
                XMessageBox.Show(this, $"Datum is al toegevoegd", "Bestaat Al", MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            else
            {
                xfeestdagen.Items.Add(lv);
                OnValueChanged(sender);
            }
        }

        private void xremovefeestdag_Click(object sender, EventArgs e)
        {
            if (XMessageBox.Show(this, $"Weetje zeker dat je alle geselecteerde datums wilt verwijderen?", "Verwijderen",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                foreach (ListViewItem lv in xfeestdagen.SelectedItems)
                    lv.Remove();
        }

        private void ListFeestdagen(DateTime[] nationalefeestdagen)
        {
            xfeestdagen.BeginUpdate();
            xfeestdagen.Items.Clear();
            nationalefeestdagen ??= new DateTime[] { };
            foreach (var x in nationalefeestdagen)
                xfeestdagen.Items.Add(new ListViewItem(x.ToString("dddd dd MMMM yyyy")) {Tag = x.Date});
            xfeestdagen.EndUpdate();
            xremovefeestdag.Visible = xfeestdagen.SelectedItems.Count > 0;
        }

        private void xspeciaalroosterb_Click(object sender, EventArgs e)
        {
            var sproosters = new SpeciaalWerkRoostersForm(SpecialeRoosters);
            if (sproosters.ShowDialog() == DialogResult.OK)
            {
                SpecialeRoosters = sproosters.Roosters;
                if (AutoUpdateBewerkingen)
                {
                    var acces1 = Manager.LogedInGebruiker is { AccesLevel: >= AccesType.ProductieBasis };
                    if (acces1 && sproosters.Roosters.Count > 0)
                    {
                        var bws = Manager.xGetBewerkingen(new ViewState[] { ViewState.Gestart }, true, false);
                        bws = bws.Where(x => string.Equals(Manager.Opties.Username, x.GestartDoor,
                            StringComparison.CurrentCultureIgnoreCase)).ToList();
                        if (bws.Count > 0)
                        {
                            var bwselector = new WerkplekSelectorForm(bws, true);
                            bwselector.Title = "Selecteer Werkplaatsen waarvan de rooster aangepast moet worden";
                            if (bwselector.ShowDialog() == DialogResult.OK)
                                _ = Manager.UpdateGestarteProductieRoosters(bwselector.SelectedWerkplekken, null);
                        }
                    }
                }
            }

        }

        public event EventHandler ValueChanged;

        protected virtual void OnValueChanged(object sender)
        {
            ValueChanged?.Invoke(sender, EventArgs.Empty);
        }

        private void xstandaard_Click(object sender, EventArgs e)
        {
            if (XMessageBox.Show(this, $"Weetje zeker dat je een standaard rooster wilt?", "Standaard Rooster",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                SetRooster(Rooster.StandaartRooster());
        }
    }
}