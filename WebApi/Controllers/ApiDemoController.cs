using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data;
using Newtonsoft.Json;


namespace WebApi.Controllers
{
    public class ApiDemoController : ApiController
    {
        // GET: api/ApiDemo
        [HttpGet]
        [HttpPost]
        public DataTable GetList(string Name)
        {
            string str = "SELECT * FROM DB_TEST.DBO.STUDENT";
            if (Name != null)
            {
                if (Name.Trim() != "")
                {
                    str = "SELECT * FROM DB_TEST.DBO.STUDENT WHERE STUDENTNAME='" + Name + "'";
                }
            }
            DataTable dt = DBHelper.GetDataTableBySql(str);
            return dt;
        }

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
        /// <param name="User"></param>
        /// <returns></returns>
        [HttpGet]
        [HttpPost]
        public string ApiTests([FromBody]object User)
        {
            ApiTest test = JsonConvert.DeserializeObject<ApiTest>(User.ToString());
            string str = "";
            if (test.UserName==null||test.PassWord==null)
            {
                str = "传参格式不正确！";
            }
            else if (test.UserName.Trim() == "" || test.PassWord.Trim() == "")
            {
                str = "参数值为空！";
            }
            else 
            {
                str = "姓名是" + test.UserName + " 密码为:" + test.PassWord;
            }
            return str;
        }
    }

    public class ApiTest {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}
