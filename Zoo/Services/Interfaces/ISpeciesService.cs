using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zoo.Dtos;

namespace Zoo.Services.Interfaces
{
	public interface ISpeciesService : IBaseService
	{
		SpecieDto Get(int id);
		IEnumerable<SpecieDto> Get();
	}
}