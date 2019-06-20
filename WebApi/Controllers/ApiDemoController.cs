using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Controllers
{
    public class ApiDemoController : ApiController
    {
        /// <summary>
        /// 获取单个用户信息
        /// </summary>
        /// <param name="Name">用户名，没有则获取所有</param>
        /// <returns>返回用户Table</returns>
        [HttpGet]
        [HttpPost]
        public DataTable GetList(string Name)
        {
            string str = "SELECT * FROM DB_TEST.DBO.STUDENT WHERE STUDENTNAME='" + Name + "'";
            DataTable dt = DBHelper.GetDataTableBySql(str);
            return dt;
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public DataTable GetList()
        {
            DataTable dt = DBHelper.GetDataTableBySql("SELECT * FROM DB_TEST.DBO.STUDENT");
            return dt;
        }

        /// <summary>
        /// 登录接口
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="PassWord">密码</param>
        /// <returns>bool值表示登录是否成功</returns>
        [HttpGet]
        [HttpPost]
        public bool UserLogin(string UserName, string PassWord)
        {
            string Str = string.Format("SELECT * FROM DB_TEST.DBO.STUDENT WHERE StudentName='{0}' AND Pwd='{1}'", UserName, PassWord);
            return DBHelper.GetDataTableBySql(Str).Rows.Count > 0 ? true : false;
        }

        /// <summary>
        /// Body传参+JSON反序列化试一手
        /// </summary>
        /// <param name="User">body参数名</param>
        /// <returns>返回反序列化后的字符串</returns>
        [HttpGet]
        [HttpPost]
        public string ApiTests([FromBody]object User)
        {
            if (User==null)
            {
                return "未传参";
            }
            ApiTest test = JsonConvert.DeserializeObject<ApiTest>(User.ToString());
            string str = "";
            if (test.UserName == null || test.PassWord == null)
            {
                str = "传参格式不正确！";
            }
            else if (test.UserName.Trim() == "" || test.PassWord.Trim() == "")
            {
                str = "参数值为空！";
            }
            else
            {
                str = "body传参调用接口ok,姓名是" + test.UserName + " 密码为:" + test.PassWord;
            }
            return str;
        }

        /// <summary>
        /// Body传参+JSON反序列化试一手
        /// </summary>
        /// <param name="User">body参数名</param>
        /// <returns>返回反序列化后的字符串</returns>
        [HttpGet]
        [HttpPost]
        public string ApiTests(string Pwd)
        {
            return "params传参数调用接口OK,参数：" + Pwd;
        }
    }
}
