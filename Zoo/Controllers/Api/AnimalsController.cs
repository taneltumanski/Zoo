using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Zoo.Dtos;
using Zoo.Services;
using Zoo.Services.Interfaces;
using Zoo.Validators;
using Zoo.Validators.Base;

namespace Zoo.Controllers.Api
{
	public class AnimalsController : ApiBaseController
	{
		public IAnimalService AnimalService { get; set; }

		public IEnumerable<AnimalDto> Get()
		{
			return AnimalService.Get();
		}

		public AnimalDto Get(int id)
		{
			return AnimalService.Get(id);
		}

		public Task<ValidationResult> Post([FromBody]AnimalDto value)
		{
			return AnimalService.AddOrUpdate(value);
		}

		public Task<ValidationResult> Delete(int id)
		{
			return AnimalService.Delete(id);
		}
	}
}
