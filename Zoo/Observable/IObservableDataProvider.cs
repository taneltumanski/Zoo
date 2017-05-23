using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Zoo.Dtos;
using Zoo.Observable.Dto;

namespace Zoo.Observable
{
	public interface IObservableDataProvider : IObservable<PushDataDto>
	{
		void OnUpdate<T>(T data, string dataKey);
		void OnDelete<T>(T data, string dataKey);
		void OnDelete(int id, string dataKey);
	}
}