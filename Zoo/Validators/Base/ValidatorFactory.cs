using Castle.MicroKernel;
using Castle.Windsor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zoo.Validators.Base
{
	public class ValidatorFactory : IValidatorFactory
	{
		private readonly IWindsorContainer _kernel;

		public ValidatorFactory(IWindsorContainer kernel)
		{
			_kernel = kernel;
		}

		public IValidator<T> GetValidator<T>()
		{
			return _kernel.Resolve<IValidator<T>>();
		}
	}
}