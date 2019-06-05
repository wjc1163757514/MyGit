using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDemo.API;
using Newtonsoft.Json;
using System.Data;
using WebDemo.Class;
using System.IO;
using System.Text;
using System.Net;

namespace WebDemo
{
    public partial class Index : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (ShareClass.Load == 0)
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    string str = "http://47.107.47.39:886/api/ApiDemo/GetList?Name="+ ShareClass.UserName;
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(HttpPost(str));
                    this.Repeater1.DataSource = dt;
                    this.Repeater1.DataBind();
                    //ApiTest api = new ApiTest();
                    //var json=api.GetString("admin");
                    //DataTable dt = JsonConvert.DeserializeObject<DataTable>(json);
                    //this.Label1.Text = dt.Rows[0]["msg"].ToString();
                }
            }
                
        }

        //退出登录
        protected void Btn_Close_Click(object sender, EventArgs e)
        {
            ShareClass.Load = 0;
            Response.Redirect("Login.aspx");
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

            //加载所有数据
            protected void Btn_Load_Click(object sender, EventArgs e)
        {
            string str = string.Format("http://47.107.47.39:886/api/ApiDemo/GetList?Name=");
            this.Repeater1.DataSource = JsonConvert.DeserializeObject<DataTable>(HttpPost(str));
            this.Repeater1.DataBind();
        }
    }
}