using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;

namespace Rpm.NativeMethods
{
    public static class NativeFileInfo
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SHFILEINFO
        {
            public IntPtr hIcon;
            public int iIcon;
            public uint dwAttributes;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;
        };

        public static class FILE_ATTRIBUTE
        {
            public const uint FILE_ATTRIBUTE_READONLY = 0x00000001;
            public const uint FILE_ATTRIBUTE_HIDDEN = 0x00000002;
            public const uint FILE_ATTRIBUTE_SYSTEM = 0x00000004;
            public const uint FILE_ATTRIBUTE_DIRECTORY = 0x00000010;
            public const uint FILE_ATTRIBUTE_ARCHIVE = 0x00000020;
            public const uint FILE_ATTRIBUTE_DEVICE = 0x00000040;
            public const uint FILE_ATTRIBUTE_NORMAL = 0x00000080;
            public const uint FILE_ATTRIBUTE_TEMPORARY = 0x00000100;
            public const uint FILE_ATTRIBUTE_SPARSE_FILE = 0x00000200;
            public const uint FILE_ATTRIBUTE_REPARSE_POINT = 0x00000400;
            public const uint FILE_ATTRIBUTE_COMPRESSED = 0x00000800;
            public const uint FILE_ATTRIBUTE_OFFLINE = 0x00001000;
            public const uint FILE_ATTRIBUTE_NOT_CONTENT_INDEXED = 0x00002000;
            public const uint FILE_ATTRIBUTE_ENCRYPTED = 0x00004000;
            public const uint FILE_ATTRIBUTE_VIRTUAL = 0x00010000;
        }

        public enum SHGFI
        {
            SHGFI_ICON = 0x000000100,
            SHGFI_DISPLAYNAME = 0x000000200,
            SHGFI_TYPENAME = 0x000000400,
            SHGFI_ATTRIBUTES = 0x000000800,
            SHGFI_ICONLOCATION = 0x000001000,
            SHGFI_EXETYPE = 0x000002000,
            SHGFI_SYSICONINDEX = 0x000004000,
            SHGFI_LINKOVERLAY = 0x000008000,
            SHGFI_SELECTED = 0x000010000,
            SHGFI_ATTR_SPECIFIED = 0x000020000,
            SHGFI_LARGEICON = 0x000000000,
            SHGFI_SMALLICON = 0x000000001,
            SHGFI_OPENICON = 0x000000002,
            SHGFI_SHELLICONSIZE = 0x000000004,
            SHGFI_PIDL = 0x000000008,
            SHGFI_USEFILEATTRIBUTES = 0x000000010,
            SHGFI_ADDOVERLAYS = 0x000000020,
            SHGFI_OVERLAYINDEX = 0x000000040
        }

        [DllImport("shell32.dll")]
        public static extern IntPtr SHGetFileInfo(string pszPath, uint dwFileAttributes, ref SHFILEINFO psfi, uint cbSizeFileInfo, uint uFlags);
        public static SHFILEINFO GetFileInfo(string filename, FileAttributes attributes)
        {
            try
            {
                NativeFileInfo.SHFILEINFO info = new NativeFileInfo.SHFILEINFO();
                uint dwFileAttributes = NativeFileInfo.FILE_ATTRIBUTE.FILE_ATTRIBUTE_NORMAL;
                dwFileAttributes |= (uint)attributes;
                uint uFlags = (uint)(NativeFileInfo.SHGFI.SHGFI_TYPENAME |SHGFI.SHGFI_USEFILEATTRIBUTES | SHGFI.SHGFI_DISPLAYNAME);

                NativeFileInfo.SHGetFileInfo(filename, dwFileAttributes, ref info, (uint)Marshal.SizeOf(info), uFlags);
                info.dwAttributes = (uint)attributes;
                return info;
            }
            catch
            {
                return default;
            }
        }

        [DllImport(@"urlmon.dll", CharSet = CharSet.Auto)]
        private extern static System.UInt32 FindMimeFromData(
    System.UInt32 pBC,
    [MarshalAs(UnmanagedType.LPStr)] System.String pwzUrl,
    [MarshalAs(UnmanagedType.LPArray)] byte[] pBuffer,
    System.UInt32 cbSize,
    [MarshalAs(UnmanagedType.LPStr)] System.String pwzMimeProposed,
    System.UInt32 dwMimeFlags,
    out System.UInt32 ppwzMimeOut,
    System.UInt32 dwReserverd
);

        public static string GetMimeFromFile(this string filename)
        {
            if (!File.Exists(filename))
                throw new FileNotFoundException(filename + " not found");

            byte[] buffer = new byte[256];
            using (FileStream fs = new FileStream(filename, FileMode.Open))
            {
                if (fs.Length >= 256)
                    fs.Read(buffer, 0, 256);
                else
                    fs.Read(buffer, 0, (int)fs.Length);
            }
            try
            {
                System.UInt32 mimetype;
                FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
                System.IntPtr mimeTypePtr = new IntPtr(mimetype);
                string mime = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);
                return mime;
            }
            catch (Exception e)
            {
                return "unknown/unknown";
            }
        }

        public static string GetMimeFromData(this byte[] data)
        {
            if (data == null || data.Length == 0) return "unknown/unknown";
            byte[] buffer = new byte[256];
            if (data.Length >= 256)
                Array.Copy(data, buffer, 256);
            else
                Array.Copy(data, buffer, (int)data.Length);
            try
            {
                System.UInt32 mimetype;
                FindMimeFromData(0, null, buffer, 256, null, 0, out mimetype, 0);
                System.IntPtr mimeTypePtr = new IntPtr(mimetype);
                string mime = Marshal.PtrToStringUni(mimeTypePtr);
                Marshal.FreeCoTaskMem(mimeTypePtr);
                return mime;
            }
            catch (Exception e)
            {
                return "unknown/unknown";
            }
        }


    }
}
