using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Zoo.Observable;
using System.Collections.Concurrent;

namespace Zoo.Hubs
{
	public class ZooHub : Hub
	{
		public IObservableDataProvider ObservableDataProvider { get; set; }

		private readonly ZooHubConnectionManager ConnectionManager = ZooHubConnectionManager.Instance;

		public ZooHub()
		{
			ObservableDataProvider = WebApiApplication.Container.Resolve<IObservableDataProvider>();
		}

		public override Task OnConnected()
		{
			var connectionId = this.Context.ConnectionId;
			var observable = ObservableDataProvider.Subscribe(x => this.Clients.Client(connectionId).Message(x));
			ConnectionManager.ConnectedUsers[Context.ConnectionId] = observable;

			return base.OnConnected();
		}

		public override Task OnDisconnected(bool stopCalled)
		{
			IDisposable disposable;

			if (ConnectionManager.ConnectedUsers.TryRemove(this.Context.ConnectionId, out disposable)) {
				disposable.Dispose();
			}

			return base.OnDisconnected(stopCalled);
		}
	}
}