using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Rpm.Misc
{
    public static class BootLoader
    {
        public static bool SetupBoot(string bootpath, string currentapppath, bool runapp)
        {
            try
            {
                var BootDir = bootpath + "\\ProductieManager";
                currentapppath = currentapppath.TrimEnd('\\');
                if (BootDir.ToLower() == currentapppath.ToLower())
                    return false;
                if (!Directory.Exists(BootDir))
                    Directory.CreateDirectory(BootDir);
                if (!Directory.Exists(currentapppath))
                    return false;

                var allfiles = GetAllBootFiles(currentapppath);
                var exe = allfiles.FirstOrDefault(x =>
                    Path.GetFileNameWithoutExtension(x).ToLower() == "productiemanager");

                if (exe == null)
                    return false;

                var exefilename = Path.GetFileName(exe);
                var bootexe = BootDir + "\\" + exefilename;

                //check eerst als er een boot bestand staat en of het nodig is op te updaten.
                if (!IsSameApp(bootexe, exe))
                {
                    var update = currentapppath + "\\Update";
                    var updatefile = $"{update}\\{exefilename}";
                    if (Directory.Exists(update) && File.Exists(updatefile))
                    {
                        var isnew = CheckforUpdate(currentapppath);
                        if (isnew)
                            exe = updatefile;
                    }

                    allfiles = allfiles.Where(x => !x.EndsWith(".exe")).ToArray();

                    foreach (var file in allfiles) File.Copy(file, BootDir + "\\" + Path.GetFileName(file), true);
                    File.Copy(exe, bootexe, true);
                }

                if (runapp)
                    Process.Start(bootexe, currentapppath);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static string[] GetAllBootFiles(string path)
        {
            return Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly)
                .Where(file => new[] {".dll", ".exe"}
                    .Contains(Path.GetExtension(file)))
                .ToArray();
        }

        public static bool CheckforUpdate(string apppath)
        {
            var assemblyname = Assembly.GetExecutingAssembly().GetName().Name + ".exe";
            var updatepath = apppath + "\\Update";
            var version = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            try
            {
                if (!Directory.Exists(updatepath))
                    Directory.CreateDirectory(updatepath);

                var filepath = $"{updatepath}\\{assemblyname}";
                if (File.Exists(filepath))
                {
                    var info = FileVersionInfo.GetVersionInfo(filepath);
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
                    var info1 = FileVersionInfo.GetVersionInfo(firstapp);
                    var info2 = FileVersionInfo.GetVersionInfo(secondapp);
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
            catch
            {
                return false;
            }
        }
    }
}