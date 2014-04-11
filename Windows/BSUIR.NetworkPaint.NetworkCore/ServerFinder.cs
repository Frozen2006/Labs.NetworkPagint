using BSUIR.NetworkPaint.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BSUIR.NetworkPaint.NetworkCore
{
	public class ServerFinder
	{
		private const int MaxWaitTime = 60;
		private UdpClient _client;
		private IPEndPoint _endpoint;
		private IPEndPoint _sendEndpoint;
		private volatile bool _isDataAccepted;
		private volatile ServerData _data;

		public ServerFinder(int port)
		{
			_endpoint = new IPEndPoint(IPAddress.Any, port);
			_sendEndpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), port+1);
			_client = new UdpClient(_endpoint);
		}

		public ServerData Find()
		{
			_client.BeginReceive(new AsyncCallback(ReciveCallback), _client);

			var startTime = DateTime.Now;

			while( (!_isDataAccepted) && ( (DateTime.Now - startTime).Seconds < MaxWaitTime ) )
			{
				_client.Send(new byte[0], 0, _sendEndpoint);
				Thread.Sleep(500);
			}

			if (_isDataAccepted)
			{
				return _data;
			} else 
			{
				return null;
			}
		}

		public void ReciveCallback(IAsyncResult result)
		{

			var listner = (UdpClient)result.AsyncState;

			if (listner == null)
				return;

			EndPoint ep = (EndPoint)_endpoint;

			byte[] data = new byte[8192];

			data = listner.Receive(ref _endpoint);

			if (data.Length > 0)
			{
				MemoryStream stream = new MemoryStream(data);
                XmlSerializer formatter = new XmlSerializer(typeof(ServerData));
				stream.Seek(0, SeekOrigin.Begin);

				_data = (ServerData)formatter.Deserialize(stream);

				_client.EndReceive(result, ref _endpoint);

				_isDataAccepted = true;
			}
			else
			{
				_client.EndReceive(result, ref _endpoint);
				_client.BeginReceive(new AsyncCallback(ReciveCallback), _client);
			}

			
		}
	}
}
