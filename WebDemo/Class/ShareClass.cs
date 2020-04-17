using System;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace WebDemo.Class
{
    public static class ShareClass
    {
        //多个页面共享变量
        public static int Load = 0;
        public static string UserName = "";
        public static string ApiUrl = "http://www.wangjc.top:886/api/ApiDemo/";
        public static string ApiTestUrl = "http://localhost:50668/api/ApiDemo/";
        public static string AutoApiUrl = ApiUrl;

        //HTTP-Get方法
        public static string HttpGet(string url)
        {
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            request.ContentLength = 0;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        //HttpWebRequest方法，Body传参
        public static string HttpPost(string url, object postData)
        {

            var data = JsonConvert.SerializeObject(postData);
            byte[] bs = Encoding.UTF8.GetBytes(data);

            HttpWebRequest result = (HttpWebRequest)WebRequest.Create(url);
            result.ContentType = "application/json";
            result.ContentLength = bs.Length;
            result.Method = "POST";
                                  
            using (Stream reqStream = result.GetRequestStream())
            {
                reqStream.ReadTimeout = 10000;
                reqStream.WriteTimeout= 10000;
                reqStream.Write(bs, 0, bs.Length);
            }
            using (WebResponse wr = result.GetResponse())
            {
                string reader = new StreamReader(wr.GetResponseStream(),
                    Encoding.UTF8).ReadToEnd();
                return reader;
            }
        }
        
        //直接用HttpClientBody传json参数
        public static string HttpContentPost(string Url, object body)
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

        //发送邮件功能
        public static String SendEmail(String TargetEmail, String body)
        {
            //分割收件人
            string[] ListTargetEmail = Regex.Split(TargetEmail, ",", RegexOptions.IgnoreCase);
            ApiEmailReuqest ApiParameter = new ApiEmailReuqest()
            {
                UserEmail = "1163757514@qq.com",
                UserEmailPassWord = "mncervgdnpcsjcgg",
                ToEmailAddress = ListTargetEmail.ToList<string>(),
                CCEmailAddress = ListTargetEmail.ToList<string>(),
                EmailBody = body
            };
            return ShareClass.HttpContentPost("http://www.wangjc.top:886/api/Email/SendEmail", ApiParameter);
        }

        //附件试一试
        public static String SendEmailFile(ApiEmailReuqest EmailReuqest)
        {
            try
            {
                string host = "smtp.qq.com";//设置邮件的服务器smtp.qq.com

                //初始化SMTP类
                SmtpClient smtp = new SmtpClient(host)
                {
                    EnableSsl = true, //开启安全连接。
                    Credentials = new NetworkCredential(EmailReuqest.UserEmail, EmailReuqest.UserEmailPassWord), //创建用户凭证
                    DeliveryMethod = SmtpDeliveryMethod.Network, //使用网络传送
                    Port = 587  //端口设置，很关键 亲测阿里服务器25和465都用不了
                };

                //创建邮件
                MailMessage message = new MailMessage(EmailReuqest.UserEmail, EmailReuqest.ToEmailAddress[0].ToString(),
                "邮件Demo测试，勿回，可删", EmailReuqest.EmailBody);
                //发件人昵称
                message.From = new MailAddress("1163757514@qq.com", "House730日运营报表专用邮箱");


                //初始化收件人和抄送人
                foreach (string item in EmailReuqest.ToEmailAddress)
                {
                    if (item != EmailReuqest.ToEmailAddress[0].ToString())
                    {
                        message.To.Add(item);
                    }
                }
                foreach (string item in EmailReuqest.CCEmailAddress)
                {
                    message.CC.Add(item);
                }

                //附件路径
                string fileAddress = @"C:\Users\wbwangjc\Desktop\730日报表数据\730日报表数据20190912.xlsx";
                string MIME = MimeMapping.GetMimeMapping(fileAddress);//文件的MediaType MIME 
                message.AlternateViews.Add(new AlternateView(fileAddress, MIME)); //发送附加内容（附加的内容为文件中的内容）

                //发送附加件
                message.Attachments.Add(new Attachment(fileAddress, MIME));
                smtp.Send(message); //发送邮件

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw ex;
            }
            return "发送成功";
        }

        //DataTable转Excel
        public static string dataTableToCsv(DataTable table, string file)

        {
            try
            {
            string title = "";

            FileStream fs = new FileStream(file, FileMode.OpenOrCreate);

            //FileStream fs1 = File.Open(file, FileMode.Open, FileAccess.Read);

            StreamWriter sw = new StreamWriter(new BufferedStream(fs), System.Text.Encoding.Default);

            for (int i = 0; i < table.Columns.Count; i++)

            {

                title += table.Columns[i].ColumnName + "\t"; //栏位：自动跳到下一单元格

            }

            title = title.Substring(0, title.Length - 1) + "\n";

            sw.Write(title);

            foreach (DataRow row in table.Rows)

            {

                string line = "";

                for (int i = 0; i < table.Columns.Count; i++)

                {

                    line += row[i].ToString().Trim() + "\t"; //内容：自动跳到下一单元格

                }

                line = line.Substring(0, line.Length - 1) + "\n";

                sw.Write(line);

            }

            sw.Close();

            fs.Close();

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return "生成成功";

        }

        //上传文件功能
        public static string UploadingFile(FileRequest request) {
            String body = JsonConvert.SerializeObject(request);
            return ShareClass.HttpPost(AutoApiUrl+ "PostFile", body);
        }

        //Http-Get通用，返回DataTable
        public static DataTable GetDataTableByUrl(string url) {
            //调用接口，返回值序列化为ApiResult对象
            ApiResult result = JsonConvert.DeserializeObject<ApiResult>(ShareClass.HttpGet(url));
            return result.Result;
        }
    }
}