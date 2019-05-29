using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebDemo
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DBHelper.Load = 0;
            }
        }

        protected void Btn_Click(object sender, EventArgs e)
        {
            string User = this.Txt_User.Text;
            string Pwd = this.Txt_Pwd.Text;
            string parameter = string.Format("http://47.107.47.39:886/api/ApiDemo/UserLogin?UserName={0}&PassWord={1}", User, Pwd);
            string str = HttpPost(parameter);
            if (str == "true")
            {
                DBHelper.Load = 1;
                DBHelper.UserName = User;
                Response.Redirect("Index.aspx");
            }
            else
            {
                DBHelper.Load = 0;
                Response.Write("<script>alert('登录失败');</script>");
            }
        }

        //HTTP-Post方法
        public static string HttpPost(string url)
        {
            //ServicePointManager.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(CheckValidationResult);
            Encoding encoding = Encoding.UTF8;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.Accept = "text/html, application/xhtml+xml, */*";
            request.ContentType = "application/json";
            request.ContentLength = 0;

            //byte[] buffer = encoding.GetBytes(body);
            //request.ContentLength = buffer.Length;
            //request.GetRequestStream().Write(buffer, 0, buffer.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }
    }
}