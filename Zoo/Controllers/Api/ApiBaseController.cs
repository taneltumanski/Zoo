using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using Zoo.Attributes;
using Zoo.Validators.Base;

namespace Zoo.Controllers.Api
{
	[ValidationResponse]
	public abstract class ApiBaseController : ApiController
	{
	}
}