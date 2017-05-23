using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using System.Web;
using Zoo.Database;
using Zoo.Database.Domain;
using Zoo.Dtos;
using Zoo.Observable;
using Zoo.Services.Interfaces;
using Zoo.Validators.Base;

namespace Zoo.Services
{
	public class AnimalService : IAnimalService
	{
		public ZooContext Context { get; set; }

		public IObservableDataProvider ObservableProvider { get; set; }
		public IValidatorFactory ValidatorFactory { get; set; }

		public AnimalDto Get(int id)
		{
			return Get().FirstOrDefault(x => x.Id == id);
		}

		public IEnumerable<AnimalDto> Get()
		{
			return Context
				.Animals
				.Select(GetExpression());
		}

		public async Task<ValidationResult> AddOrUpdate(AnimalDto animal)
		{
			var validator = ValidatorFactory.GetValidator<AnimalDto>();
			var validationResult = validator.Validate(animal);

			if (validationResult.IsSuccess) {
				// TODO avoid race conditions with transactions
				var existingEntity = await Context.Animals.FindAsync(animal.Id);

				if (existingEntity == null) {
					existingEntity = new Animal();

					Context.Animals.Add(existingEntity);
				} else {
					existingEntity.ModifiedAt = DateTimeOffset.UtcNow;
				}

				existingEntity.Name = animal.Name;
				existingEntity.BirthDate = animal.BirthDate;
				existingEntity.Specie = Context.Species.Find(animal.Specie.Id);

				await Context.SaveChangesAsync();

				var updatedDto = GetExpression().Compile()(existingEntity);

				ObservableProvider.OnUpdate(updatedDto, "Animal");
			}

			return validationResult;
		}

		public async Task<ValidationResult> Delete(int id)
		{
			var result = ValidationResult.Empty;

			var entity = await Context.Animals.FindAsync(id);

			if (entity != null) {
				Context.Animals.Remove(entity);

				await Context.SaveChangesAsync();

				ObservableProvider.OnDelete(id, "Animal");
			} else {
				result.AddError("{{animal.delete.missing}}", "Animal does not exist");
			}

			return result;
		}

		public Task<ValidationResult> Delete(AnimalDto Animal)
		{
			return Delete(Animal.Id);
		}

		private Expression<Func<Animal, AnimalDto>> GetExpression()
		{
			return x => new AnimalDto() {
				Id = x.Id,
				Name = x.Name,
				CreatedAt = x.CreatedAt,
				ModifiedAt = x.ModifiedAt,
				BirthDate = x.BirthDate,
				Specie = new SpecieDto() {
					Id = x.Specie.Id,
					Name = x.Specie.Name,
					CreatedAt = x.Specie.CreatedAt,
					ModifiedAt = x.Specie.ModifiedAt
				}
			};
		}
	}
}