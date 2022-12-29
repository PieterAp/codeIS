using somiod.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Services.Description;

namespace somiod
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services
            config.Formatters.XmlFormatter.UseXmlSerializer = true;

            //config.Formatters.Clear();
            //config.Formatters.Add(new CustomNamespaceXmlFormatter { UseXmlSerializer = true });


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/somiod/",
                defaults: new { controller = "somiod" }
            );
        }
    }
}
