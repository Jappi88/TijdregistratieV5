using System;
using System.Collections;
using System.Data;
using System.Management;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

namespace Rpm.Misc
{
    public static class SharedFolders
    {
        public static DataTable GetSharedFolderAccessRule()
        {
            var DT = new DataTable();

            try
            {
                DT.Columns.Add("ShareName");
                DT.Columns.Add("Caption");
                DT.Columns.Add("Path");
                DT.Columns.Add("Domain");
                DT.Columns.Add("User");
                DT.Columns.Add("AccessMask");
                DT.Columns.Add("AceType");

                var Scope = new ManagementScope(@"\\.\root\cimv2");
                Scope.Connect();
                var Query = new ObjectQuery("SELECT * FROM Win32_LogicalShareSecuritySetting");
                var Searcher = new ManagementObjectSearcher(Scope, Query);
                var QueryCollection = Searcher.Get();

                foreach (ManagementObject SharedFolder in QueryCollection)
                {
                    var ShareName = (string) SharedFolder["Name"];
                    var Caption = (string) SharedFolder["Caption"];
                    var LocalPath = string.Empty;
                    var Win32Share =
                        new ManagementObjectSearcher("SELECT Path FROM Win32_share WHERE Name = '" + ShareName + "'");
                    foreach (ManagementObject ShareData in Win32Share.Get()) LocalPath = (string) ShareData["Path"];

                    var Method = SharedFolder.InvokeMethod("GetSecurityDescriptor", null, new InvokeMethodOptions());
                    var Descriptor = (ManagementBaseObject) Method["Descriptor"];
                    var DACL = (ManagementBaseObject[]) Descriptor["DACL"];
                    foreach (var ACE in DACL)
                    {
                        var Trustee = (ManagementBaseObject) ACE["Trustee"];

                        // Full Access = 2032127, Modify = 1245631, Read Write = 118009, Read Only = 1179817
                        var Row = DT.NewRow();
                        Row["ShareName"] = ShareName;
                        Row["Caption"] = Caption;
                        Row["Path"] = LocalPath;
                        Row["Domain"] = (string) Trustee["Domain"];
                        Row["User"] = (string) Trustee["Name"];
                        Row["AccessMask"] = (uint) ACE["AccessMask"];
                        Row["AceType"] = (uint) ACE["AceType"];
                        DT.Rows.Add(Row);
                        DT.AcceptChanges();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.StackTrace, ex.Message);
            }

            return DT;
        }
    }

    public sealed class NetworkBrowser
    {
        #region Public Constructor

        #endregion

        #region Public Methods

        /// <summary>
        ///     Uses the DllImport : NetServerEnum
        ///     with all its required parameters
        ///     (see http://msdn.microsoft.com/library/default.asp?
        ///     url=/library/en-us/netmgmt/netmgmt/netserverenum.asp
        ///     for full details or method signature) to
        ///     retrieve a list of domain SV_TYPE_WORKSTATION
        ///     and SV_TYPE_SERVER PC's
        /// </summary>
        /// <returns>
        ///     Arraylist that represents
        ///     all the SV_TYPE_WORKSTATION and SV_TYPE_SERVER
        ///     PC's in the Domain
        /// </returns>
        public ArrayList getNetworkComputers()
        {
            //local fields
            var networkComputers = new ArrayList();
            const int MAX_PREFERRED_LENGTH = -1;
            var SV_TYPE_WORKSTATION = 1;
            var SV_TYPE_SERVER = 2;
            var buffer = IntPtr.Zero;
            var tmpBuffer = IntPtr.Zero;
            var entriesRead = 0;
            var totalEntries = 0;
            var resHandle = 0;
            var sizeofINFO = Marshal.SizeOf(typeof(_SERVER_INFO_100));


            try
            {
                //call the DllImport : NetServerEnum 
                //with all its required parameters
                //see http://msdn.microsoft.com/library/
                //default.asp?url=/library/en-us/netmgmt/netmgmt/netserverenum.asp
                //for full details of method signature
                var ret = NetServerEnum(null, 100, ref buffer,
                    MAX_PREFERRED_LENGTH,
                    out entriesRead,
                    out totalEntries, SV_TYPE_WORKSTATION |
                                      SV_TYPE_SERVER, null, out
                    resHandle);
                //if the returned with a NERR_Success 
                //(C++ term), =0 for C#
                if (ret == 0)
                    //loop through all SV_TYPE_WORKSTATION 
                    //and SV_TYPE_SERVER PC's
                    for (var i = 0; i < totalEntries; i++)
                    {
                        //get pointer to, Pointer to the 
                        //buffer that received the data from
                        //the call to NetServerEnum. 
                        //Must ensure to use correct size of 
                        //STRUCTURE to ensure correct 
                        //location in memory is pointed to
                        tmpBuffer = new IntPtr((int) buffer +
                                               i * sizeofINFO);
                        //Have now got a pointer to the list 
                        //of SV_TYPE_WORKSTATION and 
                        //SV_TYPE_SERVER PC's, which is unmanaged memory
                        //Needs to Marshal data from an 
                        //unmanaged block of memory to a 
                        //managed object, again using 
                        //STRUCTURE to ensure the correct data
                        //is marshalled 
                        var svrInfo = (_SERVER_INFO_100)
                            Marshal.PtrToStructure(tmpBuffer,
                                typeof(_SERVER_INFO_100));

                        //add the PC names to the ArrayList
                        networkComputers.Add(svrInfo.sv100_name);
                    }
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                //The NetApiBufferFree function frees 
                //the memory that the 
                //NetApiBufferAllocate function allocates
                NetApiBufferFree(buffer);
            }

            //return entries found
            return networkComputers;
        }

        #endregion

        #region Dll Imports

        //declare the Netapi32 : NetServerEnum method import
        [DllImport("Netapi32", CharSet = CharSet.Auto,
            SetLastError = true)]
        [SuppressUnmanagedCodeSecurity]
        /// <summary>
        /// Netapi32.dll : The NetServerEnum function lists all servers
        /// of the specified type that are
        /// visible in a domain. For example, an 
        /// application can call NetServerEnum
        /// to list all domain controllers only
        /// or all SQL servers only.
        /// You can combine bit masks to list
        /// several types. For example, a value 
        /// of 0x00000003  combines the bit
        /// masks for SV_TYPE_WORKSTATION 
        /// (0x00000001) and SV_TYPE_SERVER (0x00000002)
        /// </summary>
        public static extern int NetServerEnum(
            string ServerNane, // must be null
            int dwLevel,
            ref IntPtr pBuf,
            int dwPrefMaxLen,
            out int dwEntriesRead,
            out int dwTotalEntries,
            int dwServerType,
            string domain, // null for login domain
            out int dwResumeHandle
        );

        //declare the Netapi32 : NetApiBufferFree method import
        [DllImport("Netapi32", SetLastError = true)]
        [SuppressUnmanagedCodeSecurity]
        /// <summary>
        /// Netapi32.dll : The NetApiBufferFree function frees 
        /// the memory that the NetApiBufferAllocate function allocates. 
        /// Call NetApiBufferFree to free
        /// the memory that other network 
        /// management functions return.
        /// </summary>
        public static extern int NetApiBufferFree(
            IntPtr pBuf);

        //create a _SERVER_INFO_100 STRUCTURE
        [StructLayout(LayoutKind.Sequential)]
        public struct _SERVER_INFO_100
        {
            internal int sv100_platform_id;
            [MarshalAs(UnmanagedType.LPWStr)] internal string sv100_name;
        }

        #endregion
    }
}