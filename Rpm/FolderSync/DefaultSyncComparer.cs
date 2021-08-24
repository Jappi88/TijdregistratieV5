using System;
using System.IO;

namespace FolderSync
{ 
    public class DefaultSyncComparer : IFileComparer
    {
        /// <summary>
        /// Compare two fils infos. If the first file has last write date lesser than second file, it will return negative value
        /// If first file has last write date bigger than second file, it will return positive value
        /// If both files last write date is the same, it will return 0
        /// </summary>
        /// <param name="sourcefile">First file info</param>
        /// <param name="destinationfile">Second file info</param>
        /// <returns></returns>
        public virtual int Compare(FileInfo sourcefile, FileInfo destinationfile)
        {
            //TimeSpan ts = sourcefile.CreationTime - destinationfile.CreationTime;
            //if (ts.TotalMilliseconds < -2000)
            //    return -1;
            //if (ts.TotalMilliseconds > 2000)
            //    return 1;
            var ts = sourcefile.LastWriteTime - destinationfile.LastWriteTime;
            if (ts.TotalMilliseconds < 0)
                return -1;
            if (ts.TotalMilliseconds > 0)
                return 1;
            return 0;
        }
    }
}
