using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zoo.Database.Domain
{
	public class Specie : Entity
	{
		public string Name { get; set; }

		public virtual ICollection<Animal> Animals { get; set; }
	}
}