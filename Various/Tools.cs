﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using BrightIdeasSoftware;
using ProductieManager;
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

        public static void InitLastInfo(this Form form)
        {
            try
            {
                if (Manager.DefaultSettings == null || form == null) return;
                if (Manager.DefaultSettings.LastFormInfo.ContainsKey(form.Name))
                {
                    var xvalue = Manager.DefaultSettings.LastFormInfo[form.Name];
                    if (xvalue == null) return;
                  
                    if (xvalue.WindowState == FormWindowState.Normal)
                    {
                        form.Location = xvalue.Location;
                        form.Size = xvalue.Size;
                    }
                    if (xvalue.WindowState == FormWindowState.Minimized)
                        xvalue.WindowState = FormWindowState.Normal;
                    form.WindowState = xvalue.WindowState;
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
    }
}