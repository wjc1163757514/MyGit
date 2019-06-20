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
               // ShareClass.Load = 0;
            }
        }

        protected void Btn_Click(object sender, EventArgs e)
        {
            string User = this.Txt_User.Text;
            string Pwd = this.Txt_Pwd.Text;
            string parameter = string.Format(ShareClass.ApiUrl+"UserLogin?UserName={0}&PassWord={1}", User, Pwd);
            string str = ShareClass.HttpPost(parameter);
            if (str == "true")
            {
                ShareClass.Load = 1;
                ShareClass.UserName = User;
                Response.Redirect("Index.aspx");
            }
            else
            {
                ShareClass.Load = 0;
                Response.Write("<script>alert('登录失败');</script>");
            }
        }
    }
}