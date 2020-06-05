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
using System.Text.RegularExpressions;

namespace WebDemo
{
    public partial class Index : System.Web.UI.Page
    {
        protected async void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Session["UserSession"] ==null)
                {
                    Response.Redirect("Login.aspx",false);
                }
                else
                {
                    //绑定数据源DataTable
                    UserSession userSession = (UserSession)Session["UserSession"];
                    this.Repeater1.DataSource = new List<UserResult>() { await UserServer.UserGetUserMsg(userSession.UserName, userSession.Token) };
                    this.Repeater1.DataBind();
                }
            }
        }

        public void Get() {

        }

        //退出登录
        protected void Btn_Close_Click(object sender, EventArgs e)
        {
            Session.Remove("UserSession");
            Response.Redirect("Login.aspx",false);
        }

        //加载所有数据
        protected async void Btn_Load_Click(object sender, EventArgs e)
        {
            //绑定数据源DataTable
            List<UserResult> list;
            UserSession userSession = (UserSession)Session["UserSession"];
            if (Cache["UserList"] == null)
            {
                list =await UserServer.UserGetUserList(userSession.Token);
                Cache.Insert("UserList", list, null, DateTime.Now.AddSeconds(60), TimeSpan.Zero);
            }
            else
            {
                list = (List<UserResult>)Cache["UserList"];
            }
            this.Repeater1.DataSource = list;
            this.Repeater1.DataBind();

        }

        //清除Cache缓存
        protected void Btn_ClearCache_Click(object sender, EventArgs e)
        {
            Cache.Remove("UserList");
        }

        //发送邮件按钮
        protected async void Btn_SendEmail_Click(object sender, EventArgs e)
        {
            //分割收件人
            string[] listTargetEmail = Regex.Split(Txt_TargetEmail.Text, ",", RegexOptions.IgnoreCase);
            ApiEmailReuqest apiEmailReuqest = new ApiEmailReuqest()
            {
                UserEmail = "1163757514@qq.com",
                UserEmailPassWord = "mncervgdnpcsjcgg",
                ToEmailAddress = listTargetEmail.ToList<string>(),
                CCEmailAddress = listTargetEmail.ToList<string>(),
                EmailBody = Txt_EmailBody.Text
            };

            string sendEmailMsg =await UserServer.UserSendEmail(apiEmailReuqest);
            Response.Write("<script>alert('"+ sendEmailMsg + "');</script>");
        }

        protected void Repeater1_ItemCreated(object sender, RepeaterItemEventArgs e)
        {

        }

        protected void Repeater2_ItemCreated(object sender, RepeaterItemEventArgs e)
        {

        }

        //进入操作页面
        protected void Btn_File_Click(object sender, EventArgs e)
        {
            Response.Redirect("Test.aspx",false);
        }
    }
}