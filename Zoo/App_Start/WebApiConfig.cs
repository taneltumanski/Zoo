using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Zoo.IoC.Windsor;

namespace Zoo
{
	public static class WebApiConfig
	{
		public static void Register(HttpConfiguration config, IWindsorContainer container)
		{
			// Web API configuration and services
			config.DependencyResolver = new WindsorDependencyResolver(container);

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);

			// Make JSON default response
			var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
			config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);

#if DEBUG
			config.Formatters.JsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;
#endif
		}
	}
}
