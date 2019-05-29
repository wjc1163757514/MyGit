using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using Newtonsoft.Json;
using System.Data;
using WebDemo.Class;

namespace WebDemo.API
{
    /// <summary>
    /// ApiTest 的摘要说明
    /// </summary>
    [WebService(Namespace = "WebDemo.API")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
     [System.Web.Script.Services.ScriptService]
    public  class ApiTest : System.Web.Services.WebService
    {

        [WebMethod]
        public  string GetString(string str)
        {
            ApiClass apiClass = new ApiClass() {Id=1,Msg="先给个默认值" };

            if (str=="admin")
            {
                apiClass.Msg = "调用接口OK";
            }
            else
            {
                apiClass.Msg = "参数都TM搞错了";
            }
            return JsonConvert.SerializeObject(apiClass);
        }
    }
}
