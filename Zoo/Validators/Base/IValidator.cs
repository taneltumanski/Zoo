using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zoo.Validators.Base
{
	public interface IValidator<T>
	{
		ValidationResult Validate(T model);
	}
}