using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WebDemo.Class;

namespace WebDemo
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        protected async void Btn_Click(object sender, EventArgs e)
        {
            string userName = this.Txt_User.Text;
            string passWord = this.Txt_Pwd.Text;
            UserRequest userRequest = new UserRequest() { PassWord = passWord, UserName = userName };
            TokenResult tokenResult=await UserServer.UserLoginToken(userRequest);
            //判断是否登录请求Token成功
            if (tokenResult.Status == 200)
            {
                Session["UserSession"] =new UserSession() 
                {
                    IsLogin=true,
                    UserName=userName,
                    Token=tokenResult.Token
                };
                Session.Timeout = 60;
                Response.Redirect("Index.aspx",false);
            }
            else
            {
                Response.Write("<script>alert('登录失败');</script>");
            }
        }
    }
}