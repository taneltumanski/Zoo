using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zoo.Observable.Dto
{
	public class PushDataDto
	{
		public object Data { get; set; }
		public string Name { get; set; }
		public int Id { get; set; }
		public bool IsDeleted { get; set; }
	}
}