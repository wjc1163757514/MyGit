using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Newtonsoft.Json;
using WebApi.Models;
using Newtonsoft.Json.Converters;
using System.Web.Http.Controllers;

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
        public IHttpActionResult GetList(string Name)
        {
            //获取数据
            string str = "SELECT * FROM DB_TEST.DBO.STUDENT WHERE STUDENTNAME='" + Name + "'";
            DataTable dt = DBHelper.GetDataTableBySql(str);
            //实例化接口返回对象
            ApiResult Result = new ApiResult() {
                ResultNo = 0,
                Message = "调用OK",
                Elapsed = 10,
                Timestamp = DateTime.Now.Date,
                Total=dt.Rows.Count,
                Result=dt
            };
            //返回序列化的字符串
            return Json(Result);
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public IHttpActionResult GetList()
        {
            //获取数据
            DataTable dt = DBHelper.GetDataTableBySql("SELECT * FROM DB_TEST.DBO.STUDENT");
            //实例化接口返回对象
            ApiResult Result = new ApiResult()
            {
                ResultNo = 0,
                Message = "调用OK",
                Elapsed = 10,
                Timestamp = DateTime.Now.Date,
                Total = dt.Rows.Count,
                Result = dt
            };
            //返回序列化的字符串
            return Json(Result);
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
        public string ApiTest([FromBody]dynamic User)
        {
            if (User==null||User.ToString()== "System.Object")
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
                str = JsonConvert.SerializeObject(test);
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
        public string ApiTest(string UserName)
        {

            ApiTest test = new ApiTest() {UserName=UserName,PassWord="123456" };
            string str = JsonConvert.SerializeObject(test);
            return str;
        }

        /// <summary>
        /// 可解决Ajax发起的默认Options方法，和路由配合
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public string Options()
        {
            return "200"; // HTTP 200 response with empty body
        }

        [HttpGet]
        [HttpPost]
        public string GetString(string UserName) {
            return "调用接口OK" + UserName;
        }
    }
}
