using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Http.Dispatcher;
using System.Web.Routing;
using Swagger.Net;
using System.Web.Http.Routing;
using System.Collections.Generic;

//[assembly: WebActivator.PreApplicationStartMethod(typeof(WebApi.App_Start.SwaggerNet), "PreStart")]
//[assembly: WebActivator.PostApplicationStartMethod(typeof(WebApi.App_Start.SwaggerNet), "PostStart")]
namespace WebApi.App_Start 
{
    public static class SwaggerNet 
    {
        public static void PreStart() 
        {
            RouteTable.Routes.MapHttpRoute(
                name: "SwaggerApi",
                routeTemplate: "api/docs/{controller}",
                defaults: new { swagger = true },
                constraints: new { action = new OptionsConstraint() }  //���·�ɵĵ��ĸ����ԣ���ֹOptions�ظ�����
            );            
        }
        
        public static void PostStart() 
        {
            var config = GlobalConfiguration.Configuration;

            config.Filters.Add(new SwaggerActionFilter());
            
            try
            {
                config.Services.Replace(typeof(IDocumentationProvider),
                    new XmlCommentDocumentationProvider(HttpContext.Current.Server.MapPath("~/bin/WebApi.XML")));
            }
            catch (FileNotFoundException)
            {
                throw new Exception("Please enable \"XML documentation file\" in project properties with default (bin\\WebApi.XML) value or edit value in App_Start\\SwaggerNet.cs");
            }
        }

        /// <summary>
        /// ��ֹ����ʱOptions�Լ�������ظ��ύ
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