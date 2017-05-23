using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Zoo.Helpers
{
	public static class DateTimeExt
	{
		public static int GetAge(this DateTimeOffset dateTime)
		{
			return GetAge(dateTime, DateTimeOffset.UtcNow);
		}

		// http://stackoverflow.com/a/1404
		public static int GetAge(this DateTimeOffset birthdate, DateTimeOffset now)
		{
			// Calculate the age.
			var age = now.Year - birthdate.Year;
			// Go back to the year the person was born in case of a leap year
			if (birthdate > now.AddYears(-age)) {
				age--;
			}

			return age;
		}
	}
}