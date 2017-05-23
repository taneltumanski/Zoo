using System;

namespace Zoo.Dtos
{
	public class SpecieDto
	{
		public int Id { get; set; }
		public string Name { get; set; }
		
		public DateTimeOffset CreatedAt { get; set; }
		public DateTimeOffset? ModifiedAt { get; set; }
	}
}