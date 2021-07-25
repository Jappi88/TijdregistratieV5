using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace ProductieManager.Classes.Misc
{
    public static class BootLoader
    {
        public static bool SetupBoot(string bootpath, string currentapppath, bool runapp)
        {
            try
            {
                string BootDir = bootpath + "\\ProductieManager";
                currentapppath = currentapppath.TrimEnd('\\');
                if (BootDir.ToLower() == currentapppath.ToLower())
                    return false;
                if (!Directory.Exists(BootDir))
                    Directory.CreateDirectory(BootDir);
                if (!Directory.Exists(currentapppath))
                    return false;

                string[] allfiles = GetAllBootFiles(currentapppath);
                string exe = allfiles.FirstOrDefault(x => Path.GetFileNameWithoutExtension(x).ToLower() == "productiemanager");

                if (exe == null)
                    return false;

                string exefilename = Path.GetFileName(exe);
                string bootexe = BootDir + "\\" + exefilename;

                //check eerst als er een boot bestand staat en of het nodig is op te updaten.
                if (!IsSameApp(bootexe, exe))
                {
                    string update = currentapppath + "\\Update";
                    string updatefile = $"{update}\\{exefilename}";
                    if (Directory.Exists(update) && File.Exists(updatefile))
                    {
                        bool isnew = CheckforUpdate(currentapppath);
                        if (isnew)
                            exe = updatefile;
                    }

                    allfiles = allfiles.Where(x => !x.EndsWith(".exe")).ToArray();

                    foreach (string file in allfiles)
                    {
                        File.Copy(file, BootDir + "\\" + Path.GetFileName(file), true);
                    }
                    File.Copy(exe, bootexe, true);
                }

                if (runapp)
                    Process.Start(bootexe, currentapppath);
                return true;
            }
            catch (Exception ex) { return false; }
        }

        public static string[] GetAllBootFiles(string path)
        {
            return Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly)
            .Where(file => new string[] { ".dll", ".exe" }
            .Contains(Path.GetExtension(file)))
            .ToArray();
        }

        public static bool CheckforUpdate(string apppath)
        {
            string assemblyname = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name + ".exe";
            string updatepath = apppath + "\\Update";
            string version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
            try
            {
                if (!Directory.Exists(updatepath))
                    Directory.CreateDirectory(updatepath);

                string filepath = $"{updatepath}\\{assemblyname}";
                if (File.Exists(filepath))
                {
                    FileVersionInfo info = FileVersionInfo.GetVersionInfo(filepath);
                    return info.ProductVersion.CompareTo(version) > 0;
                }
            }
            catch (Exception)
            {
                return true;
            }
            return false;
        }

        public static bool IsSameApp(string firstapp, string secondapp)
        {
            try
            {
                if (File.Exists(firstapp) && File.Exists(secondapp))
                {
                    FileVersionInfo info1 = FileVersionInfo.GetVersionInfo(firstapp);
                    FileVersionInfo info2 = FileVersionInfo.GetVersionInfo(secondapp);
                    return info1.ProductVersion == info2.ProductVersion;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return false;
        }

        public static bool ReplaceExeFile(string sourcefile, string destinationfile, bool startapp)
        {
            try
            {
                if (!File.Exists(sourcefile + "_old"))
                    File.Move(sourcefile, sourcefile + "_old");
                File.Copy(destinationfile, sourcefile, true);
                if (startapp)
                    Process.Start(sourcefile, "0");
                return true;
            }
            catch { return false; }
        }
    }
}