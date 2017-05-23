using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Web;
using Zoo.Database.Domain;

namespace Zoo.Database.Configuration
{
	public class SpecieConfiguration : EntityTypeConfiguration<Specie>
	{
		public SpecieConfiguration()
		{
			Property(x => x.Name).HasMaxLength(MaxLengths.Name);
		}
	}
}