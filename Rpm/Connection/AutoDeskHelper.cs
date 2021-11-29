using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ProductieManager.Rpm.Connection
{
     public class AutoDeskHelper
     {
         public static readonly string DeskUrl = "http://valkvault01/AutodeskTC/valkvault01/Valk#/Entity/Entities?folder=root&start=0&query={0}&searchSubFolder=true&searchLatestVersion=true";

         public static async Task<string> GetTekeningPdfAsync(string artnr)
         {
             try
             {
                 var url = string.Format(DeskUrl, artnr);
                 using var client = new HttpClient();
                 client.BaseAddress = new Uri(url);
                 client.DefaultRequestHeaders.Accept.Clear();
                 client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                 //setup login data
                 var username = "Viewer";
                 var password = "Katsoja73!";
                 var formContent = new FormUrlEncodedContent(new[]
                 {
                     new KeyValuePair<string, string>("grant_type", "password"),
                     new KeyValuePair<string, string>("username", username),
                     new KeyValuePair<string, string>("password", password),
                 });
                 //send request
                 HttpResponseMessage responseMessage = await client.PostAsync("/Token", formContent);
                 //get access token from response body
                 var responseJson = await responseMessage.Content.ReadAsStringAsync();
                 var jObject = JObject.Parse(responseJson);
                 var token = jObject.GetValue("access_token")?.ToString();
                 return token;
             }
             catch (Exception e)
             {
                 Console.WriteLine(e);
                 return null;
             }
         }
     }
}
