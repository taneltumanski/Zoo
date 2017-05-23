using Castle.MicroKernel;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;

namespace Zoo.IoC.Windsor
{
	public class WindsorDependencyResolver : System.Web.Http.Dependencies.IDependencyResolver
	{
        private readonly IWindsorContainer _container;

        public WindsorDependencyResolver(IWindsorContainer container)
        {
            if (container == null)
            {
                throw new ArgumentNullException(nameof(container));
            }

            _container = container;
        }

        public void Dispose()
        {
        }

        public object GetService(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? _container.Resolve(serviceType) : null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.Kernel.HasComponent(serviceType) ? _container.ResolveAll(serviceType).Cast<object>() : new object[] { };
        }

        public IDependencyScope BeginScope()
        {
            return new WindsorDependencyScope(_container);
        }
    }
}