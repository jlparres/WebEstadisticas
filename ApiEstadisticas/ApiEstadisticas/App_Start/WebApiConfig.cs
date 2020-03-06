using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ApiEstadisticas
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Configuración y servicios de API web

            // Rutas de API web
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            string webEstadisticas = ConfigurationManager.AppSettings["WebEstadisticas"].ToString();

            var cors = new EnableCorsAttribute(webEstadisticas, "*", "*");
            config.EnableCors(cors);
        }
    }
}
