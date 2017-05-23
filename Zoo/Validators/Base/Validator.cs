using FluentValidation;
using System;
using Zoo.Database;

namespace Zoo.Validators.Base
{
	public abstract class Validator<T> : AbstractValidator<T>, IValidator<T>
	{
		public ZooContext Context { get; set; }

		public new ValidationResult Validate(T model)
		{
			var result = base.Validate(model);
			var returnResult = ValidationResult.Empty;

			foreach (var item in result.Errors) {
				returnResult.AddError(item.PropertyName, item.ErrorMessage);
			}

			return returnResult;
		}
	}
}