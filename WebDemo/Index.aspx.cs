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
                    string url = ShareClass.ApiUrl+"GetList?Name=" + ShareClass.UserName;
                    //绑定数据源DataTable
                    this.Repeater1.DataSource = GetResult(url);
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
            //拼接接口URL
            string url = string.Format(ShareClass.ApiUrl+"GetList");
            //绑定数据源DataTable
            this.Repeater1.DataSource = GetResult(url);
            this.Repeater1.DataBind();
        }

        private DataTable GetResult(string url) {
            //调用接口，返回值序列化为ApiResult对象
            ApiResult result = JsonConvert.DeserializeObject<ApiResult>(ShareClass.HttpPost(url));
            return result.Result;
        }
    }
}