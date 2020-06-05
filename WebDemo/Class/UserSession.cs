using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Class
{
    /// <summary>
    /// 用于保存用户Session
    /// </summary>
    public class UserSession
    {
        /// <summary>
        /// 是否登录
        /// </summary>
        public bool IsLogin { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }
}