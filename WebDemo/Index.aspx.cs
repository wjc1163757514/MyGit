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
                    //拼接接口URL
                    string str = ShareClass.ApiUrl+"GetList?Name=" + ShareClass.UserName;
                    //调用接口，将返回值反序列化为DataTable对象
                    DataTable dt = JsonConvert.DeserializeObject<DataTable>(ShareClass.HttpPost(str));
                    //绑定数据源
                    this.Repeater1.DataSource = dt;
                    this.Repeater1.DataBind();
                }
            }
                
        }

        //退出登录
        protected void Btn_Close_Click(object sender, EventArgs e)
        {
            ShareClass.Load = 0;
            Response.Redirect("Login.aspx");
        }

        //加载所有数据
        protected void Btn_Load_Click(object sender, EventArgs e)
        {
            string str = string.Format(ShareClass.ApiUrl+"GetList");
            this.Repeater1.DataSource = JsonConvert.DeserializeObject<DataTable>(ShareClass.HttpPost(str));
            this.Repeater1.DataBind();
        }
    }
}