using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace WebAppIMaster.App_Start
{
    public class WebApiConfig
    {
        public static void Register( HttpConfiguration config )
        {
            // Маршруты Web API
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );


            // отключаем возможность вывода данных в формате xml
            config.Formatters.Remove(config.Formatters.XmlFormatter);
        }
    }
}