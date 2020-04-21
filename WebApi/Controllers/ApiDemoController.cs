using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Newtonsoft.Json;
using WebApi.Models;
using System.Web;
using System.IO;

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
        public IHttpActionResult GetList(string Name)
        {
            //获取数据
            string str = "SELECT * FROM DB_TEST.DBO.STUDENT WHERE STUDENTNAME='" + Name + "'";
            DataTable dt = DBHelper.GetDataTableBySql(str);
            //实例化接口返回对象
            ApiResult Result = new ApiResult()
            {
                ResultNo = 1,
                Message = "调用OK",
                Elapsed = 10,
                Timestamp = DateTime.Now,
                Total = dt.Rows.Count,
                Result = dt
            };
            if (Name == "" || Name == null)
            {
                Result.Message = "未传参";
                Result.ResultNo = 0;
            }
            //返回序列化的字符串
            return Json(Result);
        }

        /// <summary>
        /// 获取所有用户信息
        /// </summary>
        /// <returns></returns>
        [HttpGet]
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
                Timestamp = DateTime.Now,
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
        [HttpPost]
        public bool UserLogin([FromBody]dynamic body)
        {
            if (body == null || body.ToString() == "System.Object")
            {
                return false;
            }
            ApiTest apiTest = JsonConvert.DeserializeObject<ApiTest>(body.ToString());
            string Str = string.Format("SELECT * FROM DB_TEST.DBO.STUDENT WHERE StudentName='{0}' AND Pwd='{1}'", apiTest.UserName, apiTest.PassWord);
            return DBHelper.GetDataTableBySql(Str).Rows.Count > 0 ? true : false;
        }

        /// <summary>
        /// Body传参+JSON反序列化试一手
        /// </summary>
        /// <param name="body">body参数名</param>
        /// <returns>返回反序列化后的字符串</returns>
        [HttpPost]
        public string ApiTest([FromBody]dynamic body)
        {
            if (body == null || body.ToString() == "System.Object")
            {
                return "未传参";
            }
            ApiTest test = JsonConvert.DeserializeObject<ApiTest>(body.ToString());
            string str;
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
        /// 两种方式传参共用试一手
        /// </summary>
        /// <param name="Tokey">url参数</param>
        /// <param name="body">body参数</param>
        /// <returns>Json格式的对象</returns>
        [HttpGet]
        [HttpPost]
        public IHttpActionResult ApiTest(string Tokey, [FromBody]dynamic body)
        {
            ApiTest test = new ApiTest() { UserName = "默认", PassWord = "123" };
            if (body != null)
            {
                test = JsonConvert.DeserializeObject<ApiTest>(body.ToString());
            }
            test.PassWord +=Tokey;
            return Json(test);
        }

        /// <summary>
        /// 文件上传接口
        /// </summary>
        /// <param name="File"></param>
        /// <returns></returns>
        [HttpPost]
        public IHttpActionResult PostFile([FromBody]dynamic body)
        {
            FileRequest res = JsonConvert.DeserializeObject<FileRequest>(body);
            string fullpath = "";
            string message;
            if (res.File == null)
            {
                message = "未选择文件";
            }
            else
            {
                fullpath = ApiShareClass.SaveFile(res);
                message = "上传成功！";
            }

            FileResult result = new FileResult()
            {
                ContentLength = 0,
                ContentType = "txt",
                FileName = res.FileName,
                FilePath = fullpath,
                message = message
            };
            return Json(result);
        }

        /// <summary>
        /// 获取文件列表
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpGet]
        public IHttpActionResult GetFileList(String UserName)
        {
            //获取数据
            string str = "SELECT * FROM [DB_Test].[dbo].[FileInfo] WHERE StudentID=" +
                "(SELECT StudentID FROM [DB_Test].[dbo].[Student] WHERE StudentName='" + UserName + "')";
            DataTable dt = DBHelper.GetDataTableBySql(str);
            //实例化接口返回对象
            ApiResult Result = new ApiResult()
            {
                ResultNo = 1,
                Message = "调用OK",
                Elapsed = 10,
                Timestamp = DateTime.Now,
                Total = dt.Rows.Count,
                Result = dt
            };
            if (UserName == "" || UserName == null)
            {
                Result.Message = "未传参";
                Result.ResultNo = 0;
            }
            //返回序列化的字符串
            return Json(Result);
        }

        /// <summary>
        /// 可解决Ajax发起的默认Options预请求方法，和路由配合
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public string Options() => "200"; // HTTP 200 response with empty body

        [HttpGet]
        [HttpPost]
        public string GetString(string UserName) => "调用接口OK" + UserName;

        /// <summary>
        /// 测试用
        /// </summary>
        /// <returns>返回用DataTable</returns>
        [HttpGet]
        public DataTable GetListByDataTable()
        {
            //获取数据
            string str = "SELECT * FROM DB_TEST.DBO.STUDENT";
            DataTable dt = DBHelper.GetDataTableBySql(str);
            return dt;
        }

        [HttpPost]
        [HttpGet]
        public IHttpActionResult GetListByJson(int Count)
        {
            //获取数据
            string str = "SELECT TOP "+ Count + " [StudentID],[StudentName],[SEX] ,[CreateTime],[UpdateTime]FROM[DB_Tests].[dbo].[StudentS]";
            DataTable dt = DBHelper.GetDataTableBySql(str);
            return Json(dt);
        }
    }
}
