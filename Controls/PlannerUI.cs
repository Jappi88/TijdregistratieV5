using System;
using System.Windows.Forms;
using Calendar;

namespace Controls
{
    public partial class PlannerUI : UserControl
    {
        public PlannerUI()
        {
            InitializeComponent();
            xsheduler.Renderer = new Office12Renderer();
        }
    }
}
