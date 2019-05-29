using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace WebDemo
{
    public static class DBHelper
    {
        //偷偷用一个防止强行打开页面的家伙
        public static int Load = 0;
        public static string UserName="";
        //字符串连接对象
        private static string sqlstr= ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        //数据库连接对象
        private static SqlConnection connection = new SqlConnection(sqlstr);

        //打开链接
        static void OpenConn() {
            if (connection.State == ConnectionState.Closed)
            {
                connection.Open();
            }
            if (connection.State == ConnectionState.Broken)
            {
                connection.Close();
                connection.Open();
            }
        }

        //关闭链接
        static void CloseConn()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        //查询，返回DataTable
        public static DataTable GetDataTableBySql(string str) {
            OpenConn();
            DataTable dt = new DataTable();
            SqlDataAdapter dap = new SqlDataAdapter(str, connection);
            dap.Fill(dt);
            CloseConn();
            return dt;
        }

        //执行增删改
        public static bool ExecuteNoQuery(string str) {
            OpenConn();
            SqlCommand cmd = new SqlCommand(str,connection);
            bool bit = cmd.ExecuteNonQuery() > 0 ? true : false;
            CloseConn();
            return bit;
        }
    }
}