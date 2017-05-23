using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using Zoo.Database;
using Zoo.Observable;
using Zoo.Services.Interfaces;
using Zoo.Validators.Base;

namespace Zoo.IoC.Windsor
{
	public class Installers : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
			container
				.Register(
					// MVC controllers
					Classes.FromThisAssembly()
						.BasedOn<IController>()
						.If(t => t.Name.EndsWith("Controller"))
						.LifestyleTransient(),

					// WebAPI controllers
					Classes.FromThisAssembly()
						.BasedOn<IHttpController>()
						.If(t => t.Name.EndsWith("Controller"))
						.LifestyleTransient()
				)
				.Register(
					// Services
					Classes.FromThisAssembly()
						.BasedOn<IBaseService>()
						.WithService
						.FromInterface()
						.LifestylePerWebRequest()
				)
				.Register(
					// Configuration
					Component.For<IConfig>()
						.Instance(Cfg.AppSettings.Get<IConfig>())
				)
				.Register(
					// Db context
					Component.For<ZooContext>()
						.LifestylePerWebRequest()
				)
				.Register(
					// Observable provider
					Component.For<IObservableDataProvider>()
						.ImplementedBy<ObservableDataProvider>()
						.LifestyleSingleton()
				)
				.Register(
					// Validators
					Classes.FromThisAssembly()
						.BasedOn(typeof(IValidator<>))
						.WithService.FromInterface()
						.LifestylePerWebRequest(),

					// Validator factory
					Component.For<IValidatorFactory>()
						.UsingFactoryMethod((kernel, context) => new ValidatorFactory(container))
						.LifestylePerWebRequest()
				);
        }
    }
}