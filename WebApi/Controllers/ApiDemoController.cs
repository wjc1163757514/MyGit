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

        [HttpGet]
        [HttpPost]
        public string ApiTests()
        {
            return "测试ok";
        }
    }
}
