using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Zoo.Database.Domain;
using Zoo.Dtos;
using Zoo.Validators.Base;

namespace Zoo.Services.Interfaces
{
	public interface IAnimalService : IBaseService
	{
		AnimalDto Get(int id);
		IEnumerable<AnimalDto> Get();

		Task<ValidationResult> AddOrUpdate(AnimalDto animal);
		Task<ValidationResult> Delete(AnimalDto Animal);
		Task<ValidationResult> Delete(int id);
	}
}