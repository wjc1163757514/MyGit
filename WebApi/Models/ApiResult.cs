using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WebApi.Models
{
    public class ApiResult
    {
        public int ResultNo { get; set; }        //返回值状态
        public string Message { get; set; }      //详情信息
        public int Total { get; set; }           //整体总数
        public int Elapsed { get; set; }         //所用时间
        public DateTime Timestamp { get; set; }  //调用时间
        //public string Machine { get; set; }    //不知道是啥
        public DataTable Result { get; set; }    //返回的数据表

    }
}