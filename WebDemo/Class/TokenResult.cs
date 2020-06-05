using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Class
{
    public class TokenResult
    {
        /// <summary>
        /// token值
        /// </summary>
        public string Token { get; set; }
        /// <summary>
        /// 有效时间
        /// </summary>
        public double Validitytime { get; set; }
        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }
    }
}