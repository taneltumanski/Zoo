using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zoo.Helpers;

namespace Zoo.Dtos
{
	public class AnimalDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public DateTimeOffset BirthDate { get; set; }
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset? ModifiedAt { get; set; }

		public SpecieDto Specie { get; set; }

		public int Age => BirthDate.GetAge();
	}
}