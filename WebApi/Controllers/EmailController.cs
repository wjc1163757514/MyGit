using System;
using System.Net;
using WebApi.Models;
using System.Web.Http;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Configuration;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace WebApi.Controllers
{
    public class EmailController : ApiController
    {
        private static readonly string SmtpHost = ConfigurationManager.AppSettings["SmtpHost"].ToString();
        private static readonly string SmtpPort = ConfigurationManager.AppSettings["SmtpPort"].ToString();
        

        [HttpPost]
        [HttpGet]
        public string SendEmailTest(String Test)
        {
            try
            {
                string host = "smtp.qq.com";//设置邮件的服务器smtp.qq.com

                //初始化SMTP类
                SmtpClient smtp = new SmtpClient(host)
                {
                    EnableSsl = true, //开启安全连接。
                    Credentials = new NetworkCredential("1163757514@qq.com", "uoteepfhtjepjecd"), //创建用户凭证
                    DeliveryMethod = SmtpDeliveryMethod.Network, //使用网络传送
                    Port = 587, //端口设置
                };

                //创建邮件
                MailMessage message = new MailMessage("1163757514@qq.com", "1163757514@qq.com",
                "邮件Demo测试，勿回，可删", "1163757514@qq.com");

                //初始化收件人和抄送人
                    message.To.Add(Test);
                    message.CC.Add("1163757514@qq.com");
                
                smtp.Send(message); //发送邮件

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw ex;
            }
            return "发送成功";
        }
        [HttpPost]
        public string SendEmail([FromBody]dynamic body)
        {
            if (body == null || body.ToString() == "System.Object")
            {
                return "参数不正确";
            }
            try
            {
                ApiEmailRequest EmailReuqest = JsonConvert.DeserializeObject<ApiEmailRequest>(body.ToString());
                
                string host ="smtp."+ ApiShareClass.AfterByIndex(EmailReuqest.UserEmail,"@"); //设置邮件的服务器

                //初始化SMTP类
                SmtpClient smtp = new SmtpClient(host)
                {
                    EnableSsl = true, //开启安全连接。
                    Credentials = new NetworkCredential(EmailReuqest.UserEmail, EmailReuqest.UserEmailPassWord), //创建用户凭证
                    DeliveryMethod = SmtpDeliveryMethod.Network, //使用网络传送
                    Port = int.Parse(SmtpPort)  //端口设置，很关键 亲测阿里服务器25和465都用不了
                };

                //创建邮件
                MailMessage message = new MailMessage(EmailReuqest.UserEmail, EmailReuqest.ToEmailAddress[0].ToString(),
                "邮件Demo测试，勿回，可删", EmailReuqest.EmailBody);

                //初始化收件人和抄送人
                foreach (string item in EmailReuqest.ToEmailAddress)
                {
                    if (item!= EmailReuqest.ToEmailAddress[0].ToString())
                    {
                        message.To.Add(item);
                    }
                }
                foreach (string item in EmailReuqest.CCEmailAddress)
                {
                    message.CC.Add(item);
                }

                //附件路径
                //string fileAddress = @"C:\WebApiFile\替换专用.txt";
                //string MIME = MimeMapping.GetMimeMapping(fileAddress);//文件的MediaType MIME 
                //message.AlternateViews.Add(new AlternateView(fileAddress, MIME)); //发送附加内容（附加的内容为文件中的内容）

                //发送附加件
                //message.Attachments.Add(new Attachment(fileAddress, MIME));

                //解决验证过程，远程证书无效
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (Object obj, X509Certificate certificate, X509Chain chain, SslPolicyErrors errors) { return true; };

                smtp.Send(message); //发送邮件

            }
            catch (Exception ex)
            {
                return ex.Message;
                throw ex;
            }
            return "发送成功";
        }

        /// <summary>
        /// 可解决Ajax发起的默认Options预请求方法，和路由配合
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public string Options() => "200"; // HTTP 200 response with empty body

    }
}