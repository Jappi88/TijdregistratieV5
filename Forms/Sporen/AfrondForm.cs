using Forms.MetroBase;

namespace Forms.Sporen
{
    public partial class AfrondForm : MetroBaseForm
    {
        public AfrondForm()
        {
            InitializeComponent();
        }

        public bool IsLager => xlagerRadio.Checked;

        public decimal Factor => xfactor.Value;
    }
}
