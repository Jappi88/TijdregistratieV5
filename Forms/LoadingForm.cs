using Rpm.Various;
using System;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class LoadingForm : MetroBase.MetroBaseForm
    {
        public ProgressArg Arg { get; set; } = new ProgressArg();
        public bool CloseIfFinished { get; set; }
        public bool ShowCloseButton { get => xsluiten.Visible; set=> xsluiten.Visible = value; }
        public LoadingForm()
        {
            InitializeComponent();
            Arg.Changed += Arg_Changed;
        }

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
                if (this.IsDisposed || this.Disposing) return;
                if (InvokeRequired)
                    this.Invoke(new MethodInvoker(() => UpdateArg(arg)));
                else
                {
                    if (CloseIfFinished && (arg.IsCanceled || arg.Type == ProgressType.ReadCompleet || arg.Type == ProgressType.WriteCompleet))
                    {
                        if (arg.IsCanceled)
                            DialogResult = DialogResult.Cancel;
                        else
                            DialogResult = DialogResult.OK;
                        this.Close();
                        return;
                    }
                    if (arg.IsCanceled)
                    {
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
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void xsluiten_Click(object sender, EventArgs e)
        {
            Arg.IsCanceled = true;
            this.Close();
        }

        public Task ShowDialogAsync(IWin32Window owner)
        {
            var task = Task.Run(() =>
            {
                if (this.IsDisposed) return;
                this.ShowDialog();
            });
            Application.DoEvents();
            return task;
        }

        private void LoadingForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Arg != null && !Arg.IsCanceled)
            {
                Arg.IsCanceled = true;
                Arg.OnChanged(this);
            }
        }
    }
}
