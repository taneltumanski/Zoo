using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zoo.Database.Domain
{
	public class Animal : Entity
	{
		public string Name { get; set; }
		public DateTimeOffset BirthDate { get; set; }

		public int SpecieId { get; set; }
		public virtual Specie Specie { get; set; }
	}
}