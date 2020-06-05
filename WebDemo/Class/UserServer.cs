using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Web;
using Newtonsoft.Json.Linq;
using System.Reflection;

namespace WebDemo.Class
{
    /// <summary>
    /// 用户相关操作类
    /// </summary>
    public class UserServer
    {
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="request">用户实体类</param>
        /// <returns>请求是否成功</returns>
        public async static Task<bool> UserLogin(UserRequest request) 
        {
            string requestUrl =await GetRequestUrl("ApiDemo/UserLogin", null);

            string result =await HttpContext.HttpPostAsync(requestUrl,request);

            JObject js = JsonConvert.DeserializeObject<JObject>(result);

            return (bool)js["Status"];
        }

        /// <summary>
        /// 登录 获取Token
        /// </summary>
        /// <param name="request">用户实体对象</param>
        /// <returns>返回Token对象</returns>
        public async static Task<TokenResult> UserLoginToken(UserRequest request)
        {
            string requestUrl = await GetRequestUrl("ApiDemo/UserLoginToken", null);

            string result = await HttpContext.HttpPostAsync(requestUrl, request);

            TokenResult tokenResult = JsonConvert.DeserializeObject<TokenResult>(result);

            return tokenResult;
        }

        /// <summary>
        /// 获取单个用户信息
        /// </summary>
        /// <param name="UserName">用户名称</param>
        /// <param name="Token">用户Token</param>
        /// <returns>单个用户信息对象</returns>
        public async static Task<UserResult> UserGetUserMsg(string userName,string token)
        {
            string requestUrl = await GetRequestUrl("ApiDemo/GetList", new {UserName= userName, Token= token });

            string result = await HttpContext.HttpGetAsync(requestUrl);

            ApiResult apiResult = JsonConvert.DeserializeObject<ApiResult>(result);

            UserResult userResult = JsonConvert.DeserializeObject<UserResult>(apiResult.Result.ToString());

            return userResult;
        }

        /// <summary>
        /// 获取所有用户信息列表
        /// </summary>
        /// <param name="Token">用户Token</param>
        /// <returns>用户信息列表List</returns>
        public async static Task<List<UserResult>> UserGetUserList(string token)
        {
            string requestUrl = await GetRequestUrl("ApiDemo/GetAllList", new {Token = token });

            string result = await HttpContext.HttpGetAsync(requestUrl);

            ApiResult apiResult = JsonConvert.DeserializeObject<ApiResult>(result);

            List<UserResult> userResultList = JsonConvert.DeserializeObject<List<UserResult>>(apiResult.Result.ToString());

            return userResultList;
        }

        /// <summary>
        /// 用户上传文件
        /// </summary>
        /// <param name="request">文件实体对象</param>
        /// <param name="Token">用户Token</param>
        /// <returns>请求是否成功</returns>
        public async static Task<FileResult> UserPostFile(FileRequest request,string token)
        {
            string requestUrl = await GetRequestUrl("ApiDemo/PostFile", new { Token = token });

            string result = await HttpContext.HttpPostAsync(requestUrl, request);

            ApiResult apiResult = JsonConvert.DeserializeObject<ApiResult>(result);

            FileResult fileResult = JsonConvert.DeserializeObject<FileResult>(apiResult.Result.ToString());

            return fileResult;
        }

        /// <summary>
        /// 用户加载文件列表
        /// </summary>
        /// <param name="UserName">用户名</param>
        /// <param name="Token">用户Token</param>
        /// <returns>文件列表List</returns>
        public async static Task<List<FileListResult>> UserGetFileList(string userName, string token)
        {
            string requestUrl = await GetRequestUrl("ApiDemo/GetFileList", new {UserName=userName, Token = token });

            string result = await HttpContext.HttpGetAsync(requestUrl);

            ApiResult apiResult = JsonConvert.DeserializeObject<ApiResult>(result);


            List<FileListResult> fileListResult = JsonConvert.DeserializeObject<List<FileListResult>>(apiResult.Result.ToString());

            return fileListResult;
        }

        /// <summary>
        /// 用户发送邮件 暂时不验证token
        /// </summary>
        /// <param name="request">邮件实体对象</param>
        /// <param name="Token">用户Token</param>
        /// <returns>请求返回消息内容</returns>
        public async static Task<string> UserSendEmail(ApiEmailReuqest request)
        {
            string requestUrl = await GetRequestUrl("ApiDemo/SendEmail", null);

            string result = await HttpContext.HttpPostAsync(requestUrl, request);

            ApiResult apiResult = JsonConvert.DeserializeObject<ApiResult>(result);

            return apiResult.Message;
        }
        
        /// <summary>
        /// 拼接请求的Url
        /// </summary>
        /// <param name="ActioinUrl">Action地址</param>
        /// <param name="token">用户Token 会拼接到Url之后</param>
        /// <returns>返回拼接好的Url</returns>
        private async static Task<string> GetRequestUrl(string actioinUrl,object request) {
            StringBuilder result = new StringBuilder();
            //BaseUrl
            result.Append(ShareClass.AutoApiBaseUrl);
            //Action
            result.Append(actioinUrl);
            //request参数拼接URL
            if (request != null)
            {
                PropertyInfo[] propertis = request.GetType().GetProperties();
                result.Append("?");
                foreach (var p in propertis)
                {
                    var v = p.GetValue(request, null);
                    if (v == null)
                        continue;

                    result.Append(p.Name);
                    result.Append("=");
                    result.Append(HttpUtility.UrlEncode(v.ToString()));
                    result.Append("&");
                }
                result.Remove(result.Length - 1, 1);
            }

            return await Task.Run(() => result.ToString());
        }


    }
}