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
                if (Session["Login"]==null || Session["Login"].ToString() != "True")
                {
                    Response.Redirect("Login.aspx");
                }
                else
                {
                    //拼接接口URL

                    string url = ShareClass.ApiUrl + "GetList?Name=" + ShareClass.UserName;
                    //绑定数据源DataTable
                    this.Repeater1.DataSource = ShareClass.GetDataTableByUrl(url);
                    this.Repeater1.DataBind();
                }
            }
        }

        public void get() {

        }

        //退出登录
        protected void Btn_Close_Click(object sender, EventArgs e)
        {
            Session.Remove("Login");
            Response.Redirect("Login.aspx");
        }

        //加载所有数据
        protected void Btn_Load_Click(object sender, EventArgs e)
        {
            //拼接接口URL
            string url = string.Format(ShareClass.ApiUrl + "GetList");
            //绑定数据源DataTable
            DataTable dt;
            if (Cache["DataTable"] == null)
            {
                dt = ShareClass.GetDataTableByUrl(url);
                Cache.Insert("DataTable", dt, null, DateTime.Now.AddSeconds(60), TimeSpan.Zero);
            }
            else
            {
                dt = (DataTable)Cache["DataTable"];
            }
            this.Repeater1.DataSource = dt;
            this.Repeater1.DataBind();

        }

        protected void Btn_ClearCache_Click(object sender, EventArgs e)
        {
            //清除Cache缓存
            Cache.Remove("DataTable");
        }

        //发送邮件按钮
        protected void Btn_SendEmail_Click(object sender, EventArgs e)
        {
           Response.Write("<script>alert('"+ ShareClass.SendEmail(Txt_TargetEmail.Text, Txt_EmailBody.Text) + "');</script>");
        }

        protected void Repeater1_ItemCreated(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void Repeater2_ItemCreated(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void Btn_File_Click(object sender, EventArgs e)
        {
            Response.Redirect("Test.aspx");
        }
    }
}