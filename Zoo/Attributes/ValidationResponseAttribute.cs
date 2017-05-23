using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Zoo.Validators.Base;

namespace Zoo.Attributes
{
	public class ValidationResponseAttribute : ActionFilterAttribute
	{
		public override Task OnActionExecutedAsync(HttpActionExecutedContext actionExecutedContext, CancellationToken cancellationToken)
		{
			if (actionExecutedContext.Response.IsSuccessStatusCode) {
				var returnType = actionExecutedContext.ActionContext.ActionDescriptor.ReturnType;

				if (returnType == typeof(ValidationResult)) {
					ValidationResult result;

					if (actionExecutedContext.Response.TryGetContentValue(out result)) {
						if (!result.IsSuccess) {
							actionExecutedContext.Response = actionExecutedContext.Request.CreateResponse(System.Net.HttpStatusCode.BadRequest, result);
						}
					}
				}
			}

			return base.OnActionExecutedAsync(actionExecutedContext, cancellationToken);
		}
	}
}