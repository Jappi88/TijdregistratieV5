using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ProductieManager.Properties;
using ProductieManager.Rpm.Misc;
using Rpm.Misc;
using Rpm.NativeMethods;

namespace Forms.FileBrowser
{
    public class BrowseEntry
    {
        public string RootPath { get; private set; }
        public string Path { get; private set; }
        public string Name { get; private set; }
        public string Type { get; private set; }
        public long Size { get; private set; }
        public bool IsDirectory { get; private set; }
        public string FriendlySize { get; private set; }
        public DateTime LastChanged { get;private set; }

        public BrowseEntry(string path)
        {
            SetPath(path);
        }

        public void SetPath(string path)
        {
            try
            {
                Path = path;
                RootPath = string.IsNullOrEmpty(Path) ? string.Empty : Directory.GetParent(Path).FullName;
                var info = GetFileInfo();
                Name = System.IO.Path.GetFileNameWithoutExtension(Path);
                IsDirectory = Directory.Exists(path);

                Type = IsDirectory? "Bestandsmap" : info.szTypeName;
                Size = GetSize();
                FriendlySize = GetFriendlySize();
                LastChanged = GetDateTimeChanged();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public Image GetIcon(Size size)
        {
            try
            {
                if (!Exists()) return null;
               
                var key = GetImageKey();
                if (string.IsNullOrEmpty(key)) return null;
                Bitmap image = null;
                if(IsDirectory)
                {
                    if (key.Contains("content"))
                        image = Resources.folder_with_contents_96x96;
                    else image = Resources.folder_open_Empty_96x96;
                    if (size != image.Size)
                        image = image.ResizeImage(size);
                }
                else
                {
                    image = WindowsThumbnailProvider.GetThumbnail(Path,size.Width, size.Height, ThumbnailOptions.None);
                }

                if (image == null) return null;
                image.Tag = key;
                return image;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public string GetImageKey()
        {
            if (IsDirectory)
            {
                if (HasContent())
                    return "directory_content";
                return "directory_empty";
            }
            if (Path.IsImageFile())
                return System.IO.Path.GetFileName(Path);
            var ext = System.IO.Path.GetExtension(Path);
            if (string.IsNullOrEmpty(ext))
                ext = "file";
            return ext;
        }

        public bool HasContent()
        {
            if (!IsDirectory) return false;
            if (!Directory.Exists(Path)) return false;
            return Directory.EnumerateFileSystemEntries(Path).Any();
        }

        public void Rename(string newname)
        {
            if (string.IsNullOrEmpty(Path)) return;
            var xxt = IsDirectory? "" : System.IO.Path.GetExtension(Path);
            if (!IsDirectory && System.IO.Path.HasExtension(newname))
            {
                xxt = System.IO.Path.GetExtension(newname);
                newname += xxt;
            }
            else
                newname += xxt;

            string oldname = System.IO.Path.GetFileName(Path);
            string newpath = System.IO.Path.Combine(RootPath, newname);
            bool flag = string.Equals(oldname, newname, StringComparison.CurrentCultureIgnoreCase) &&
                        !string.Equals(newname, oldname);
            if (IsDirectory)
            {
                if (flag)
                {
                    Directory.Move(Path, Path + "_tmp");
                    Directory.Move(Path + "_tmp", newpath);
                }
                else
                    Microsoft.VisualBasic.FileIO.FileSystem.RenameDirectory(Path, newname);
            }
            else
            {
                if (flag)
                {
                    File.Move(Path, Path + "_tmp");
                    File.Move(Path + "_tmp", newpath);
                }
                else
                    Microsoft.VisualBasic.FileIO.FileSystem.RenameFile(Path, newname);
            }
            SetPath(newpath);
        }

        public void MoveTo(string path)
        {
            try
            {
                if (IsDirectory)
                    Microsoft.VisualBasic.FileIO.FileSystem.MoveDirectory(Path, path, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                else
                    Microsoft.VisualBasic.FileIO.FileSystem.MoveFile(Path, path, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                SetPath(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            if (!Directory.Exists(targetPath))
                Directory.CreateDirectory(targetPath);

            // Copy each file into the new directory.
            foreach (string fi in Directory.GetFiles(sourcePath))
            {
                File.Copy(fi,System.IO.Path.Combine(targetPath, System.IO.Path.GetFileName(fi)), true);
            }

            // Copy each subdirectory using recursion.
            foreach (var diSourceSubDir in Directory.GetDirectories(sourcePath))
            {
                DirectoryInfo nextTargetSubDir =
                    new DirectoryInfo(targetPath).CreateSubdirectory(System.IO.Path.GetFileName(diSourceSubDir));
                CopyFilesRecursively(diSourceSubDir, nextTargetSubDir.FullName);
            }
        }

        public void CopyTo(string path, bool vbstyle)
        {
            try
            {
                if (vbstyle)
                {
                    if (IsDirectory)
                        Microsoft.VisualBasic.FileIO.FileSystem.CopyDirectory(Path, path,
                            Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
                            Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                    else
                        Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(Path, path,
                            Microsoft.VisualBasic.FileIO.UIOption.AllDialogs,
                            Microsoft.VisualBasic.FileIO.UICancelOption.ThrowException);
                }
                else
                {
                    if (IsDirectory)
                        CopyFilesRecursively(Path, path);
                    else File.Copy(Path, path,true);
                }

                SetPath(path);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public void Delete()
        {
            if (IsDirectory)
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteDirectory(Path, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
            else
                Microsoft.VisualBasic.FileIO.FileSystem.DeleteFile(Path, Microsoft.VisualBasic.FileIO.UIOption.AllDialogs, Microsoft.VisualBasic.FileIO.RecycleOption.SendToRecycleBin);
        }

        public FileAttributes GetAttributes()
        {
            try
            {
                if(!Exists())
                    return FileAttributes.Normal;
                if (string.IsNullOrEmpty(Path))
                    return FileAttributes.Normal;
                return  File.GetAttributes(Path);
            }
            catch
            {
                return FileAttributes.Normal;
            }
        }

        public NativeFileInfo.SHFILEINFO GetFileInfo()
        {
            try
            {
                return NativeFileInfo.GetFileInfo(Path, GetAttributes());
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        public long GetSize()
        {
            if (IsDirectory) return 0;
            if (string.IsNullOrEmpty(Path)) return 0;
            if(!File.Exists(Path)) return 0;
            return new FileInfo(Path).Length;
        }

        public string GetFriendlySize()
        {
            try
            {
                if (IsDirectory) return string.Empty;
                long size = GetSize();
                return GetFriendlySize(size);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        public static string GetFriendlySize(long size)
        {
            try
            {
                string[] suf = { "B", "KB", "MB", "GB", "TB", "PB", "EB" }; //Longs run out around EB
                if (size == 0)
                    return "0 " + suf[0];
                long bytes = Math.Abs(size);
                int place = Convert.ToInt32(Math.Floor(Math.Log(bytes, 1024)));
                double num = Math.Round(bytes / Math.Pow(1024, place), 1);
                return (Math.Sign(size) * num).ToString() + " " + suf[place];
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return string.Empty;
            }
        }

        public DateTime GetDateTimeChanged()
        {
            try
            {
                if (string.IsNullOrEmpty(Path)) return default;
                if (IsDirectory)
                {
                    if (Directory.Exists(Path))
                        return Directory.GetLastWriteTime(Path);
                }
                if(File.Exists(Path))
                {
                    return File.GetLastWriteTime(Path);
                }
                return default;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return default;
            }
        }

        public override bool Equals(object obj)
        {
            if (obj is BrowseEntry ent)
                return string.Equals(ent.Path, Path, StringComparison.CurrentCultureIgnoreCase);
            if(obj is string xval)
                return string.Equals(xval, Path, StringComparison.CurrentCultureIgnoreCase);
             return false;
        }

        public bool Exists()
        {
            if (string.IsNullOrEmpty(Path)) return false;
            if(IsDirectory)
                return Directory.Exists(Path);
            return File.Exists(Path);
        }

        public override int GetHashCode()
        {
            return Path?.GetHashCode()??0;
        }
    }
}
