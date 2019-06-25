using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Routing;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 配置和服务

            // Web API 路由
            config.MapHttpAttributeRoutes();

            //config.Routes.MapHttpRoute(
            //    name: "DefaultApi",
            //    routeTemplate: "api/{controller}/{id}",
            //    defaults: new { id = RouteParameter.Optional }
            //);

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional },
                constraints: new { action = new OptionsConstraint()}  //添加路由的第四个属性，防止Options重复调用
                );

        }

        /// <summary>
        /// 防止跨域时Options自检引起的重复提交重复提交
        /// </summary>
        public class OptionsConstraint : IHttpRouteConstraint
        {
            public bool Match(System.Net.Http.HttpRequestMessage request, IHttpRoute route, string parameterName,
                IDictionary<string, object> values, HttpRouteDirection routeDirection)
            {
                if (request.Method.ToString().ToLower() == "options")
                {
                    if (parameterName != null)
                    {
                        values[parameterName] = "Options";
                    }
                }
                return true;
            }
        }

    }
}
