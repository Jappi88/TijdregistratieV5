using Controls;
using Forms.MetroBase;
using ProductieManager.Properties;
using Rpm.Productie;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Forms;
using ProductieManager.Rpm.Misc;
using Rpm.Settings;

namespace ProductieManager.Forms
{
    public partial class DbSelectorForm : MetroBaseForm
    {
        public DbSelectorForm()
        {
            InitializeComponent();
            LoadDbs();
        }

        private void LoadDbs()
        {
            try
            {
                SetTile("Basis Database");
                SetTile("Open Database");
                var xroot = Path.GetDirectoryName(Manager.AppRootPath);
                if (Directory.Exists(xroot))
                {
                    var dirs = Directory.GetDirectories(xroot);
                    foreach (var dir in dirs)
                    {
                        if (Directory.Exists(Path.Combine(dir, "RPM_Data")))
                        {
                            SetTile(dir);
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void SetTile(string path)
        {
            try
            {
                var xtile = new Tile();
                xtile.Text = path;
                xtile.TileCountFont = new Font(new FontFamily("Segoe UI"), 18, FontStyle.Bold);
                xtile.Size = new Size(256, 96);
                xtile.Font = new Font(new FontFamily("Segoe UI"), 14, FontStyle.Regular);
                xtile.BackColor = IProductieBase.GetProductSoortColor("horti");
                xtile.ForeColor = Color.AliceBlue;
                var xname = Path.GetFileNameWithoutExtension(path);
                if (int.TryParse(xname, out var xvalue))
                {
                    xtile.TileCount = xvalue;
                }
                xtile.TileImage = GetImage(path);
                xtile.AllowTileEdit = false;
                xtile.AllowSelection = false;
                xtile.Click += Xtile_Click;
                tileViewer1.Controls.Add(xtile);
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void UpdateTiles()
        {
            var xtiles = tileViewer1.Controls.Cast<Tile>().ToList();
            xtiles.ForEach(x =>
            {

                x.TileImage = GetImage(x.Text);
                x.Invalidate();
            });
        }

        private Image GetImage(string path)
        {
            var xselected = string.Equals(path, Manager.AppRootPath, StringComparison.CurrentCultureIgnoreCase);
            var ximg = xselected
                ? Resources.businessapplication_database_Accept
                : Resources.database_load_96x96;
            if (path.ToLower() == "basis database")
            {
                ximg = Resources.database_21835_96x96;
            }
            else if (path.ToLower() == "open database")
            {
                ximg = Resources.choose_database_96x96;
            }
            return ximg.ResizeImage(68,68);
        }

        private async void LoadDatabase(string path)
        {
            try
            {
               await ProductieView._manager.Load(path, true, true, true);
               UpdateTiles();
            }
            catch (Exception e)
            {
                XMessageBox.Show(this, e.Message, "Fout", MessageBoxIcon.Error);
            }
        }

        private void Xtile_Click(object sender, EventArgs e)
        {
            try
            {
                string path = (sender as Tile)?.Text;
                if (path == null) return;
                if (path.ToLower() == "basis database")
                {
                    var stng = Manager.DefaultSettings ?? UserSettings.GetDefaultSettings();
                    if (Directory.Exists(stng.MainDB.RootPath))
                        path = stng.MainDB.RootPath;
                    else path = Path.Combine(Application.StartupPath,"ProductieManager");
                    var xyearpath = Path.Combine(path, DateTime.Now.Year.ToString());
                    if (Directory.Exists(xyearpath))
                    {
                        path = xyearpath;
                    }
                }
                else if(path.ToLower() == "open database")
                {
                    var fb = new FolderBrowserDialog();
                    fb.Description = "Kies een locatie waar de map RPM_Data zich bevind";
                    if (fb.ShowDialog() == DialogResult.OK)
                    {
                        if (Directory.Exists(Path.Combine(fb.SelectedPath, "RPM_Data")))
                        {
                            path = fb.SelectedPath;
                        }
                        else
                        {
                            throw new Exception($"Locatie '{fb.SelectedPath}' bevat geen 'RPM_Data' folder!");
                        }
                    }
                    else return;
                }

                if (!Directory.Exists(path))
                {
                    throw new Exception($"Locatie '{path}' bestaat niet!");
                }
                if (string.Equals(path.TrimEnd('\\'), Manager.AppRootPath,
                        StringComparison.CurrentCultureIgnoreCase))
                    return;
                LoadDatabase(path);
            }
            catch (Exception ex)
            {
                XMessageBox.Show(this, ex.Message, "Fout", MessageBoxIcon.Error);
            }
        }
    }
}
