using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Configuration;

namespace WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var allowedOrigin = ConfigurationManager.AppSettings["AllowedOrigin"] ?? "http://localhost:4200";
            var cors = new EnableCorsAttribute(allowedOrigin, "Authorization,Content-Type", "GET,POST,PUT,DELETE,OPTIONS");
            config.EnableCors(cors);

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
