﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class InkomendMailInfoForm : MetroBase.MetroBaseForm
    {
        public InkomendMailInfoForm()
        {
            InitializeComponent();
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
