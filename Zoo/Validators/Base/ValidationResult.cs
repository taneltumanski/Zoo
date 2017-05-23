using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections.Immutable;

namespace Zoo.Validators.Base
{
	public class ValidationResult
	{
		public static ValidationResult Empty => new ValidationResult();

		private Dictionary<string, ICollection<string>> _errors { get; } = new Dictionary<string, ICollection<string>>();

		public ImmutableDictionary<string, ImmutableList<string>> Errors => _errors.ToImmutableDictionary(x => x.Key, x => x.Value.ToImmutableList());
		public bool IsSuccess => !_errors.Any();

		private readonly object _lock = new object();

		public void AddError(string key, string value)
		{
			lock (_lock) {
				if (!_errors.ContainsKey(key)) {
					_errors[key] = new List<string>();
				}

				_errors[key].Add(value);
			}
		}
	}
}