using System;

namespace ProductieManager.Rpm.Connection
{
    public class AutoDeskHelper
    {
        public static readonly string DeskUrl =
            "http://valkvault01/AutodeskTC/valkvault01/Valk#/Entity/Entities?folder=root&start=0&query={0}&searchSubFolder=true&searchLatestVersion=true";

        public static readonly string DownloadUrl =
            "http://valkvault01/AutodeskTC/valkvault01/Valk#/Entity/Details?id={0}&amp;itemtype=File";

        public static string GetTekeningPdfLink(string artnr)
        {
            try
            {
                var url = string.Format(DeskUrl, artnr);
                return url;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }
    }
}