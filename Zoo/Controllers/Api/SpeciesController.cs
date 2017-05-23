using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Zoo.Dtos;
using Zoo.Services.Interfaces;

namespace Zoo.Controllers.Api
{
	public class SpeciesController : ApiBaseController
	{
		public ISpeciesService SpeciesService { get; set; }

		public IEnumerable<SpecieDto> Get()
		{
			return SpeciesService.Get();
		}

		public SpecieDto Get(int id)
		{
			return SpeciesService.Get(id);
		}
	}
}
