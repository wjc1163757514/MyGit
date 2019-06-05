using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebDemo.Class
{
    public static class ShareClass
    {
        //多个页面共享变量
        public static int Load = 0;
        public static string UserName="";
    }
}