using ProductieManager.Properties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Forms
{
    public partial class MethodsForm : Forms.MetroBase.MetroBaseForm
    {
        //public List<Task<bool>> TasksList { get; private set; }
        public MethodsForm(Dictionary<string, Task<bool>> tasks)
        {
            InitializeComponent();
            //TasksList = new List<Task<bool>>();
            LoadTasks(tasks);
        }

        public void LoadTasks(Dictionary<string, Task<bool>> tasks)
        {
            if (tasks.Count == 0) return;

            listView1.Items.Clear();
            foreach (var task in tasks)
            {
                var xdisc = task.Key;
                var lv = new ListViewItem(xdisc)
                {
                    ImageIndex = 0,
                    Tag = task.Value
                };
                listView1.Items.Add(lv);
                //lock (TasksList)
                //{
                //    TasksList.Add(task.Value);
                //}
            }
        }

        private bool _IsRunning;
        private bool _Cancel;

        private async void RunTasks()
        {
            if (_IsRunning) return;
            _IsRunning = true;
            _Cancel = false;
            try
            {
                for (int i = 0; i < listView1.Items.Count; i++)
                {
                    if (_Cancel) break;
                    var item = listView1.Items[i];

                    if (item.Tag is Task<bool> task)
                    {
                        item.ImageIndex = 1;
                        listView1.Invalidate();
                        item.EnsureVisible();
                        task.Start();
                        _ = await task;
                        listView1.Items.RemoveAt(i--);
                        listView1.Invalidate();
                    }
                    Application.DoEvents();
                    this.Select();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _IsRunning = false;
            if (listView1.Items.Count == 0)
                this.Close();
        }

        private void xannuleren_Click(object sender, EventArgs e)
        {
            if (CloseTasks())
                this.Close();
        }

        private bool CloseTasks()
        {
            int count = listView1.Items.Count;

            if (count > 0)
            {
                string x1 = count == 1 ? "actie" : "acties";
                if (XMessageBox.Show(
                    this,$"Er staan nog {count} {x1} open!\nDeze scherm sluiten zal alle acties annuleren...\n\n" +
                    $"Weetje zeker dat je alsnog wilt sluiten?", "Alles Annuleren?", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning) == DialogResult.No)
                {
                    return false;
                }
            }

            _Cancel = true;
            listView1.Items.Clear();
            return true;
        }

        private void xpausestart_Click(object sender, EventArgs e)
        {
            if (!_IsRunning)
            {
                xpausestart.Enabled = true;
                xpausestart.Image = Resources.PauseHot_26935;
                xpausestart.Text = @"Pauzeer Acties";
                RunTasks();
            }
            else
            {
                _Cancel = true;
                xpausestart.Enabled = false;
                while (true)
                {
                    Application.DoEvents();
                    if (!listView1.Items.Cast<ListViewItem>().Select(x => x.Tag as Task<bool>)
                        .Any(x => x is {Status: TaskStatus.Running}))
                        break;
                }

                _IsRunning = false;
                xpausestart.Enabled = true;
                xpausestart.Image = Resources.play_button_icon_icons_com_60615;
                xpausestart.Text = @"Hervat Acties";
                
            }
            xpausestart.Invalidate();
        }

        private void MethodsForm_Shown(object sender, EventArgs e)
        {
            RunTasks();
        }

        private void MethodsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!CloseTasks())
                e.Cancel = true;
        }
    }
}
