namespace Zoo.Database.Migrations
{
	using Domain;
	using MoreLinq;
	using System;
	using System.Data.Entity;
	using System.Data.Entity.Migrations;
	using System.Linq;

	public sealed class Configuration : DbMigrationsConfiguration<ZooContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Database\Migrations";
        }

        protected override void Seed(ZooContext context)
        {
			var dbSpecies = context.Species.Select(x => x.Name).ToHashSet();
			var dbAnimals = context.Animals.Select(x => new { x.Name, SpecieName = x.Specie.Name }).ToHashSet();

			var species = new[] {
				new Specie() { Name = "Turtle" },
				new Specie() { Name = "Snake" },
				new Specie() { Name = "Bear" },
				new Specie() { Name = "Dragon" }
			};

			var animals = new[] {
				new Animal() {
					Name = "Joonas",
					BirthDate = new DateTimeOffset(2001, 2, 15, 0, 0, 0, TimeSpan.Zero),
					Specie = species[2]
				},
				new Animal() {
					Name = "Theodor",
					BirthDate = new DateTimeOffset(1879, 6, 1, 0, 0, 0, TimeSpan.Zero),
					Specie = species[0]
				},
				new Animal() {
					Name = "Drake",
					BirthDate = new DateTimeOffset(2010, 12, 19, 0, 0, 0, TimeSpan.Zero),
					Specie = species[3]
				}
			};

			var addSpecies = species
				.Where(x => !dbSpecies.Contains(x.Name))
				.ToArray();

			var addAnimals = animals
				.Where(x => !dbAnimals.Contains(new { x.Name, SpecieName = x.Specie.Name }))
				.ToArray();

			context.Species.AddOrUpdate(
				x => x.Name,
				addSpecies
			);

			context.Animals.AddOrUpdate(
				x => x.Name,
				addAnimals
			);
        }
    }
}
