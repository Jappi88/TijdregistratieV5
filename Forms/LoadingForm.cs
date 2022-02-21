using System;
using System.Windows.Forms;
using Forms.MetroBase;
using Rpm.Various;

namespace Forms
{
    public partial class LoadingForm : MetroBaseForm
    {
        public LoadingForm()
        {
            InitializeComponent();
            Arg.Changed += Arg_Changed;
        }

        public ProgressArg Arg { get; set; } = new();

        private void Arg_Changed(object sender, ProgressArg arg)
        {
            if (InvokeRequired)
                Invoke(new Action(() => UpdateArg(arg)));
            else UpdateArg(arg);
        }

        private void UpdateArg(ProgressArg arg)
        {
            try
            {
                if (IsDisposed || Disposing) return;
                if (arg.IsCanceled)
                {
                    Dispose();
                    return;
                }

                xstatustext.Text = arg.Message;
                xstatustext.Invalidate();
                xsluiten.Text = arg.Type == ProgressType.ReadCompleet ? "Sluiten" : "Annuleren";
                if (arg.Current > 0)
                {
                    xprogressbar.Style = ProgressBarStyle.Blocks;
                    xprogressbar.Maximum = arg.Max;
                    xprogressbar.Value = arg.Current > arg.Max ? arg.Max : arg.Current;
                    xprogressbar.Text = $"{arg.Current} / {arg.Max}";
                }
                else
                {
                    xprogressbar.Text = "";
                    xprogressbar.Maximum = 100;
                    xprogressbar.Value = 75;
                    xprogressbar.Style = ProgressBarStyle.Marquee;
                }

                xprogressbar.Invalidate();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void LoadingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Arg != null)
            {
                Arg.IsCanceled = true;
                Arg.OnChanged(this);
            }
        }
    }
}