using BSUIR.NetworkPaint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.NetworkPaint.Server
{
	public class ServerDataResender
	{
		private TcpListener _listner;
		private List<TcpClient> _clients = new List<TcpClient>();

		public ServerDataResender(int port)
		{
			_listner = new TcpListener(IPAddress.Any, port);
			StartListen();
		}

		private async void StartListen()
		{
			var client = await _listner.AcceptTcpClientAsync();
			_clients.Add(client);
			ResendData(client);
		}

		private async void ResendData(TcpClient client)
		{
			BinaryFormatter formatter = new BinaryFormatter();
			var stream = client.GetStream();

			while (client.Connected)
			{
				var data = (TransferPackage)formatter.Deserialize(stream);
				foreach (var currentClient in _clients)
				{
					formatter.Serialize(currentClient.GetStream(), data);
				}
			}
		}
	}
}
