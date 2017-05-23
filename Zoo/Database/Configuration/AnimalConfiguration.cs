using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Zoo.Database.Domain;

namespace Zoo.Database.Configuration
{
	public class AnimalConfiguration : EntityTypeConfiguration<Animal>
	{
		public AnimalConfiguration()
		{
			Property(x => x.Name).HasMaxLength(MaxLengths.Name);

			HasRequired(x => x.Specie)
				.WithMany(x => x.Animals)
				.WillCascadeOnDelete(false);
		}
	}
}