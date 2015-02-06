using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Nickel.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Nickel
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API 구성 및 서비스

            var formatters = GlobalConfiguration.Configuration.Formatters;
            var jsonFormatter = formatters.JsonFormatter;
            var settings = jsonFormatter.SerializerSettings;
            settings.Formatting = Formatting.Indented;
            settings.ContractResolver = new CamelCasePropertyNamesContractResolver();

            var container = new UnityContainer();
            container.RegisterType<IRecordFilter, DefaultRecordFilter>();
            container.RegisterType<ISitesProvider, DefaultSitesProvider>(new ContainerControlledLifetimeManager());
            config.DependencyResolver = new UnityResolver(container);

            // Web API 경로
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
