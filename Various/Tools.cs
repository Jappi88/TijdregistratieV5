using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ProductieManager;
using ProductieManager.Forms;
using Rpm.Misc;
using Rpm.Productie;
using Rpm.Settings;

namespace Various
{
    public static class Tools
    {
        public static OLVColumn CreateColumn(this string name, Font font)
        {
            var clm = new OLVColumn();
            clm.AspectName = name;
            clm.IsEditable = false;
            clm.IsTileViewColumn = true;
            clm.Text = name;
            clm.ToolTipText = name;
            clm.Width = name.MeasureString(font).Width * 2;
            clm.WordWrap = true;
            return clm;
        }

        public static OLVColumn[] CreateColumns(this Type instance, Font font, string[] exclude = null)
        {
            var xreturn = new List<OLVColumn>();
            if (instance == null)
                return xreturn.ToArray();
            var prs = instance.GetProperties();
            foreach (var pr in prs)
            {
                if (pr.Name.ToLower() == "id" ||
                    exclude != null && exclude.Any(x => pr.Name.ToLower() == x.ToLower()))
                    continue;
                xreturn.Add(CreateColumn(pr.Name, font));
            }

            return xreturn.ToArray();
        }

        public static bool HasUpdate()
        {
            //updatepath is the location where I upload updated exe 
            var UpdatePath = AppDomain.CurrentDomain.BaseDirectory + "Update\\" + AppDomain.CurrentDomain.FriendlyName;
            //applocation is the location from where this console app runs.It will also be the location where the new file will be saved 
            var AppLocation = AppDomain.CurrentDomain.BaseDirectory + AppDomain.CurrentDomain.FriendlyName;


            try
            {
                FileVersionInfo info1 = null;
                FileVersionInfo info2 = null;
                if (File.Exists(UpdatePath))
                    //If there is a file in the update location info1 will get the fileinfo of that file 
                    info1 = FileVersionInfo.GetVersionInfo(UpdatePath);

                if (File.Exists(AppLocation))
                    //if the exe is already present in the aplocation get its information also
                    info2 = FileVersionInfo.GetVersionInfo(AppLocation);

                return info1 != null && info2 != null &&
                       string.Compare(info1.ProductVersion, info2.ProductVersion, StringComparison.Ordinal) > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void InitLastInfo(this Form form, bool location)
        {
            try
            {
                if (Manager.DefaultSettings == null || form == null) return;
                if (form.WindowState == FormWindowState.Minimized)
                    form.WindowState = FormWindowState.Normal;
                
                else form.StartPosition = FormStartPosition.CenterScreen;
                if (Manager.DefaultSettings.LastFormInfo.ContainsKey(form.Name))
                {
                    var xvalue = Manager.DefaultSettings.LastFormInfo[form.Name];
                    if (xvalue == null) return;
                    if (xvalue.WindowState == FormWindowState.Minimized)
                        xvalue.WindowState = FormWindowState.Normal;
                    if (xvalue.WindowState == FormWindowState.Normal)
                    {
                        //form.Location = xvalue.Location;
                        if (xvalue.Size.Width > 50 && xvalue.Size.Height > 50)
                            form.Size = xvalue.Size;                      
                    }
                    if (location)
                    {
                        
                        var y = (xvalue.Location.Y);
                        var x = (xvalue.Location.X);
                        var loc = new Rectangle();
                        form.Invoke(new MethodInvoker(() => loc = Screen.GetBounds(new Point(x, y))));
                        if (!loc.Contains(new Point(x, y + form.Height)))
                        {
                            y = (loc.Y);
                            x = (loc.X);
                        }
                        form.Location = new Point(x, y);
                    }
                    form.WindowState = xvalue.WindowState;
                    form.Invalidate();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void SetLastInfo(this Form form)
        {
            try
            {
                if (Manager.DefaultSettings == null || form == null) return;
                if (Manager.DefaultSettings.LastFormInfo.ContainsKey(form.Name))
                {
                    var xvalue = Manager.DefaultSettings.LastFormInfo[form.Name];
                    if (xvalue == null) return;
                    xvalue.Location = form.Location;
                    xvalue.Size = form.Size;
                    xvalue.WindowState = form.WindowState;
                }
                else
                    Manager.DefaultSettings.LastFormInfo.Add(form.Name,
                        new LastFormScreenInfo()
                        {
                            Location = form.Location,
                            Name = form.Name,
                            Size = form.Size,
                            WindowState = form.WindowState
                        });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public static void ShowSelectedTekening(IWin32Window owner, string artnr, EventHandler browserclosed)
        {
            if(!string.IsNullOrEmpty(artnr))
            {
                var xart = artnr;
                //Process.Start(xtek); 
                var wb = new WebBrowserForm();
                wb.FilesFormatToOpen = new string[] { "{0}_fbr.pdf" };
                wb.FilesToOpen.Add(xart);
                wb.CloseIfNotFound = true;
                wb.OpenIfFound = true;
                wb.Navigate();
                // wb.Navigate("C:\\Users\\Gebruiker\\Dropbox\\ProductieManager\\Autodesk Vault.html");
                wb.ShowDialog(owner);
                browserclosed?.Invoke(wb, EventArgs.Empty);
            }
        }
    }
}