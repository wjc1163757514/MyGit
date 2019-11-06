using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebDemo.Class
{
    public class ApiEmailReuqest
    {
        public string UserEmail { get; set; }   //发件人

        public string UserEmailPassWord { get; set; }  //授权码

        public List<string> ToEmailAddress { get; set; }  //收件人

        public List<string> CCEmailAddress { get; set; }  //抄送人

        public string EmailBody { get; set; }  //内容
    }
}