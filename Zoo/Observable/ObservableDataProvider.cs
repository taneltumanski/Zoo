using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Disposables;
using System.Web;
using Zoo.Dtos;
using Zoo.Observable.Dto;
using System.Collections.Concurrent;
using log4net;

namespace Zoo.Observable
{
	public class ObservableDataProvider : ObservableBase<PushDataDto>, IObservableDataProvider
	{
		private readonly ConcurrentDictionary<IObserver<PushDataDto>, bool> _subscribers = new ConcurrentDictionary<IObserver<PushDataDto>, bool>();
		private readonly ILog _log = LogManager.GetLogger(typeof(ObservableDataProvider));

		public void OnUpdate<T>(T data, string dataKey)
		{
			var pushData = new PushDataDto() {
				Data = data,
				Name = dataKey
			};

			PushUpdate(pushData);
		}

		public void OnDelete<T>(T data, string dataKey)
		{
			var pushData = new PushDataDto() {
				Data = data,
				IsDeleted = true,
				Name = dataKey
			};

			PushUpdate(pushData);
		}

		public void OnDelete(int id, string dataKey)
		{
			var pushData = new PushDataDto() {
				Id = id,
				Data = new { Id = id },
				IsDeleted = true,
				Name = dataKey
			};

			PushUpdate(pushData);
		}

		private void PushUpdate(PushDataDto data)
		{
			foreach (var item in _subscribers) {
				try {
					item.Key.OnNext(data);
				} catch (Exception e) {
					_log.Error(e);
				}
			}
		}

		protected override IDisposable SubscribeCore(IObserver<PushDataDto> observer)
		{
			bool val;
			_subscribers.TryAdd(observer, true);
			return Disposable.Create(() => _subscribers.TryRemove(observer, out val));
		}
	}
}