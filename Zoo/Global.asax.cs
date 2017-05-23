using Castle.Windsor;
using Castle.Windsor.Installer;
using Castle.Windsor.Mvc;
using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;
using SignalR.Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Zoo.Database;
using Zoo.Database.Migrations;

[assembly: OwinStartup(typeof(Zoo.Startup))]

namespace Zoo
{
	public class WebApiApplication : HttpApplication
	{
		public static IWindsorContainer Container { get; private set; }

		protected void Application_Start()
		{
			Cfg.AppSettings.Init<IConfig>();

			System.Data.Entity.Database.SetInitializer(new CustomDatabaseInitializer());

			Container = new WindsorContainer().Install(FromAssembly.This());

			// custom controllerfactory
			ControllerBuilder.Current.SetControllerFactory(new WindsorControllerFactory(Container.Kernel));

			var controllerActivator = new WindsorControllerActivator(Container.Kernel);

			// Replace default API controller activator with custom one
			GlobalConfiguration.Configuration.Services.Replace(typeof(IHttpControllerActivator), controllerActivator);

			// SignalR dependancy injection
			GlobalHost.DependencyResolver = new WindsorDependencyResolver(Container);

			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(x => WebApiConfig.Register(x, Container));
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);
		}
	}

	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			app.MapSignalR(new HubConfiguration() {
				Resolver = new WindsorDependencyResolver(WebApiApplication.Container) 
			});
		}
	}
}
