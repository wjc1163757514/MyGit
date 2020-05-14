using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Class
{
    public class UserResult
    {
        public UserResult()
        {
        }
        /// <summary>
        /// 编号
        /// </summary>
        public int StudentID { get; set; }
        /// <summary>
        /// 姓名
        /// </summary>
        public string StudentName { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreateTime { get; set; }
        /// <summary>
        /// 性别
        /// </summary>
        public string Sex { get; set; }
        /// <summary>
        /// 密码
        /// </summary>
        public string Pwd { get; set; }
    }
}