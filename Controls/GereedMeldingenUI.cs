using System.Collections.Generic;
using System.Windows.Forms;

namespace Controls
{
    public partial class GereedMeldingenUI : UserControl
    {
        public List<string> Producties { get; private set; } = new List<string>();

        public GereedMeldingenUI()
        {
            InitializeComponent();
        }
    }
}
