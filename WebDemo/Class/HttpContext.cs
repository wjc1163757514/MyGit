using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebDemo.Class
{
    /// <summary>
    /// Http相关操作请求类
    /// </summary>
    public class HttpContext
    {
        /// <summary>
        /// HTTP-Get方法
        /// </summary>
        /// <param name="url">Get请求,传入Url即可</param>
        /// <returns></returns>
        public async static Task<string> HttpGetAsync(string url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            request.ContentLength = 0;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return await Task.Run(()=> reader.ReadToEnd()); 
            }
        }

        /// <summary>
        /// 直接用HttpClientBody传json参数
        /// </summary>
        /// <param name="Url">请求Url</param>
        /// <param name="body">body参数</param>
        /// <returns>json</returns>
        public async static Task<string> HttpPostAsync(string Url, object body)
        {
            //body传参
            var data = JsonConvert.SerializeObject(body);
            HttpContent httpContent = new StringContent(data);
            httpContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            using (HttpClient httpClient = new HttpClient())
            {
                return await Task.Run(() => httpClient.PostAsync(Url, httpContent)
                   .Result.Content.ReadAsStringAsync().Result);
            }
        }
    }
}