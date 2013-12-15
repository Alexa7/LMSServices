using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace LMSServices
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Routes.MapHttpRoute(
                name: "CalculateNumOfDaysRoute",
                routeTemplate: "api/CalculateNumOfDays/fromDate/{fromDate}/toDate/{toDate}",
                defaults: new { fromDate = DateTime.Now, toDate = DateTime.Now }
            );

            config.Routes.MapHttpRoute(
                name: "GetDescriptionRoute",
                routeTemplate: "api/GetDescription/type/{type}/value/{value}",
                defaults: null,
                constraints: new { type = @"^[a-z]+$", value = @"^[a-z]+$" }
            );

            config.Formatters.XmlFormatter.UseXmlSerializer = true;
        }
    }
}
