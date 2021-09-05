using System.Collections.Generic;
using System.Windows.Forms;

namespace Controls
{
    public partial class GereedMeldingenUI : UserControl
    {
        public List<string> Producties { get; } = new();

        public GereedMeldingenUI()
        {
            InitializeComponent();
        }
    }
}