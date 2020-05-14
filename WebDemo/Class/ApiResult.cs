using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace WebDemo.Class
{
    public class ApiResult
    {
        /// <summary>
        /// 返回值状态
        /// </summary>
        public int ResultNo { get; set; }
        /// <summary>
        /// 详情信息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 整体总数
        /// </summary>
        public int Total { get; set; }
        /// <summary>
        /// 所用时间
        /// </summary>
        public int Elapsed { get; set; }
        /// <summary>
        /// 调用时间
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// 返回的数据表
        /// </summary>
        public object Result { get; set; }
    }
}