using AdminApplication.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdminApplication.Controller
{
    public class Client
    {
        private static string url = "https://localhost:8080/api";
        private static string userToken;


       



        public static List<RegistryTableItem> GetRegistryItems(string registryName)
        {
            
            
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url+ "/registry/" +registryName);
            
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + userToken);


            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {

                List<RegistryTableItem> result = JsonConvert.DeserializeObject<List<RegistryTableItem>>(streamReader.ReadToEnd()) as List<RegistryTableItem>;
                return result;
            }
        }

        public static void Login(string text, string password)
        {
            ServicePointManager.ServerCertificateValidationCallback =
                 delegate (object s, X509Certificate certificate,
                X509Chain chain, SslPolicyErrors sslPolicyErrors)
                 { return true; };
            //var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/admin/login");
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://localhost:8080/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {


                string json = "{\"email\" : \"" + text + "\",\"password\" : \"" + password + "\" }";
                Console.WriteLine(json);

                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            string token;
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                token = streamReader.ReadToEnd();
            }



            httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/admin/login");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + token);
            httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {

                string result =  streamReader.ReadToEnd();
                if(result.Equals("ok"))
                {
                    userToken = token;
                    return;
                }else
                {
                    throw new Exception("derp");
                }
                
            }

            




         
        }


        public static void  UpdateRegistry(String registryName, RegistryTableItem registryTableItem)
        {
            
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/registry/" + registryName + "Registry");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.UseDefaultCredentials = true;
            httpWebRequest.Headers.Add("Authorization", "Bearer " + userToken);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {



                streamWriter.Write(registryTableItem.ToJSON());
                streamWriter.Flush();
                streamWriter.Close();
            }
            using (HttpWebResponse response = (HttpWebResponse) httpWebRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                Console.WriteLine(  reader.ReadToEndAsync());
            }
           
            
        }
        public static void AddToRegistry(String registryName, RegistryTableItem registryTableItem)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/registry/" + registryName + "Registry");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + userToken);

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = registryTableItem.ToJSON();


                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                Console.WriteLine(reader.ReadToEndAsync());
            }
           
        }
        public static List<Account> GetAccounts()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/admin/users");

            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + userToken);


            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {

                List<Account> result = JsonConvert.DeserializeObject<List<Account>>(streamReader.ReadToEnd()) as List<Account>;
                return result;
            }
        }
        public static void UpdateAccount(Account account)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/admin/users/" + account.Id);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + userToken);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = account.ToJSONUpdate();


                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                Console.WriteLine(reader.ReadToEndAsync());
            }
        }
        public static void AddAccount(NewAccountDTO newAccountDTO)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/admin/users");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + userToken);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = newAccountDTO.ToJSON();


                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                Console.WriteLine(reader.ReadToEndAsync());
            }
        }
        public static List<Comment> GetComments()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url+"/admin/comments");

            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + userToken);


            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {

                List<Comment> result = JsonConvert.DeserializeObject<List<Comment>>(streamReader.ReadToEnd()) as List<Comment>;
                return result;
            }
        }
        public static void ApproveComment(String approval, Comment currentComment)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/admin/comments/" + currentComment.Id);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + userToken);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                


                streamWriter.Write(approval);
                streamWriter.Flush();
                streamWriter.Close();
            }
            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                Console.WriteLine(reader.ReadToEndAsync());
            }
        }
        public static List<String> GetProfanities()
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url+"/admin/profanity");

            httpWebRequest.Method = "GET";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + userToken);


            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {

                List<String> result = JsonConvert.DeserializeObject<List<String>>(streamReader.ReadToEnd()) as List<String>;
                return result;
            }

        }

        public static void AddProfanity(String profanity)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/admin/profanity");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + userToken);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {



                streamWriter.Write(profanity);
                streamWriter.Flush();
                streamWriter.Close();
            }
            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                Console.WriteLine(reader.ReadToEndAsync());
            }
        }
        public static void RemoveProfanity(String profanity)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url + "/admin/profanity");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";
            httpWebRequest.Headers.Add("Authorization", "Bearer " + userToken);
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {



                streamWriter.Write(profanity);
                streamWriter.Flush();
                streamWriter.Close();
            }
            using (HttpWebResponse response = (HttpWebResponse)httpWebRequest.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                Console.WriteLine(reader.ReadToEndAsync());
            }
        }
    }
}
