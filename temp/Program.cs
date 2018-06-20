using AdminApplication.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace temp
{
    class Program
    {
        //private static string password;

        static void Main(string[] args)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                 delegate (object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
            { return true; };
            RegistryTableItem registryTableItem = new RegistryTableItem();
            registryTableItem.Id = 11;
            registryTableItem.Active = "false";

            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:8080/api/registry/lodgingRegistry");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.UseDefaultCredentials = true;
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"id\" : \"11\",\"active\" : \"true\"}";
                string json2 = registryTableItem.ToJSON();
                streamWriter.Write(json2);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                Console.WriteLine(result);
                Console.ReadLine();
            }


           
        }
    }
}
