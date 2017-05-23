using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Zoo.Database;
using Zoo.Database.Domain;
using Zoo.Dtos;
using Zoo.Services.Interfaces;

namespace Zoo.Services
{
	public class SpeciesService : ISpeciesService
	{
		public ZooContext Context { get; set; }

		public SpecieDto Get(int id)
		{
			return Get().FirstOrDefault(x => x.Id == id);
		}

		public IEnumerable<SpecieDto> Get()
		{
			return Context
				.Species
				.Select(GetExpression());
		}

		private Expression<Func<Specie, SpecieDto>> GetExpression()
		{
			return x => new SpecieDto() {
				Id = x.Id,
				Name = x.Name,
				CreatedAt = x.CreatedAt,
				ModifiedAt = x.ModifiedAt
			};
		}
	}
}