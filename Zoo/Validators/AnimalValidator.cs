using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zoo.Database;
using Zoo.Dtos;
using Zoo.Validators.Base;

namespace Zoo.Validators
{
	public class AnimalValidator : Validator<AnimalDto>
	{
		public AnimalValidator()
		{
			RuleFor(x => x.Name)
				.NotEmpty();

			RuleFor(x => x.Name)
				.Must((parent, name) => !Context.Animals.Any(x => x.Id != parent.Id && x.Name == name && x.SpecieId == parent.Specie.Id))
				.When(x => x.Specie != null)
				.WithMessage($"Animal with the same name and species already exists");

			RuleFor(x => x.Name)
				.MaximumLength(MaxLengths.Name);

			RuleFor(x => x.BirthDate)
				.NotEmpty()
				.WithMessage("Birth date cannot be empty");

			RuleFor(x => x.Specie)
				.NotNull()
				.WithMessage("Specie cannot be empty");

			RuleFor(x => x.Specie.Id)
				.GreaterThan(0)
				.Must(x => Context.Species.Any(y => y.Id == x))
				.When(x => x.Specie != null)
				.WithMessage("Invalid species");
		}
	}
}