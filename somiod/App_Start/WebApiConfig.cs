using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace somiod
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/somiod/",
                defaults: new { controller = "somiod" }
            );

            /*
            //Applications routes
            config.Routes.MapHttpRoute(
                name: "Application",
                routeTemplate: "api/somiod/",
                defaults: new { controller = "Applications" }
            );

            //Modules routes
            config.Routes.MapHttpRoute(
                name: "Module",
                routeTemplate: "api/somiod/{application}/",
                defaults: new { controller = "Modules" }
            );

            //Subscriptions routes
            config.Routes.MapHttpRoute(
                name: "Subscription",
                routeTemplate: "api/somiod/{application}/{module}",
                defaults: new { controller = "Subscriptions" }
            );
            */

            //TODO: Delete this after confirming its not needed
            /*
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                //routeTemplate: "api/{controller}/{id}",
                routeTemplate: "api/{controller}/",
                defaults: new { id = RouteParameter.Optional }
            );
            */
        }
    }
}
