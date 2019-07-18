using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization.Json;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebDemo.Class
{
    public static class ShareClass
    {
        //多个页面共享变量
        public static int Load = 0;
        public static string UserName = "";
        public static string ApiUrl = "http://www.wangjc.top:886/api/ApiDemo/";
        public static string ApiTest = "http://localhost:50668/api/ApiDemo/";

        //HTTP-Post方法
        public static string HttpPost(string url)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            request.ContentLength = 0;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        //HTTP-Post方法，Body传参实现登录   并不能body传json参数，玩不来
        public static string HttpPost(string Url, object ticket)
        {
            DataContractJsonSerializer serializer = new DataContractJsonSerializer(ticket.GetType());
            MemoryStream stream = new MemoryStream();
            serializer.WriteObject(stream, ticket);
            byte[] dataBytes = new byte[stream.Length];
            stream.Position = 0;
            stream.Read(dataBytes, 0, (int)stream.Length);
            string param = Encoding.UTF8.GetString(dataBytes);
            byte[] bs = Encoding.ASCII.GetBytes(param);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(Url);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = bs.Length;
            using (Stream reqStream = req.GetRequestStream())
            {
                reqStream.Write(bs, 0, bs.Length);
            }
            HttpWebResponse hwr = req.GetResponse() as HttpWebResponse;
            System.IO.StreamReader myreader = new System.IO.StreamReader(hwr.GetResponseStream(), Encoding.UTF8);
            string responseText = myreader.ReadToEnd();
            return responseText;
        }

        //直接用HttpClientBody传json参数
        public static string HttpContentPost(string Url, ApiClass body)
        {
            //body传参
            var data = JsonConvert.SerializeObject(body);
            HttpContent httpContent = new StringContent(data);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            using (HttpClient httpClient = new HttpClient())
            {
                string responseJson = httpClient.PostAsync(Url, httpContent)
                   .Result.Content.ReadAsStringAsync().Result;
                return responseJson;
            }
        }

    }
}