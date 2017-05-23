using System;
using System.Collections.Concurrent;

namespace Zoo.Hubs
{
	public class ZooHubConnectionManager
	{
		public static readonly ZooHubConnectionManager Instance = new ZooHubConnectionManager();

		public ConcurrentDictionary<string, IDisposable> ConnectedUsers = new ConcurrentDictionary<string, IDisposable>();

		private ZooHubConnectionManager()
		{

		}
	}
}