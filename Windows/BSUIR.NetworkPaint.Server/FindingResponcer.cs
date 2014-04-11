using BSUIR.NetworkPaint.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BSUIR.NetworkPaint.Server
{
	public class FindingResponcer
	{
		private ServerData _data;
		private int _port;
		private UdpClient _client;
		private IPEndPoint _listenEndpoint;
		private IPEndPoint _sendEndpoint;

		public FindingResponcer(int port, string serverName)
		{
			_port = port;

			_data = new ServerData()
			{
				ServerName = serverName
			};

			_listenEndpoint = new IPEndPoint(IPAddress.Any, port+1);
			_sendEndpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), port);


			_client = new UdpClient(_listenEndpoint);

			List<IPAddress> addresses = new List<IPAddress>();

			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (IPAddress ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
					addresses.Add(ip);
				}
			}

            _data.Addresses = addresses.Select(m => m.ToString()).ToArray();

			StartListen();
		}


		public void StartListen()
		{
			_client.BeginReceive(new AsyncCallback(ReciveCallback), _client);
		}

		public void ReciveCallback(IAsyncResult result)
		{
			MemoryStream stream = new MemoryStream();

            BinaryFormatter formatter = new BinaryFormatter();

            var dataToSend = CustomXmlSerializer.Serialize<ServerData>(_data);

            formatter.Serialize(stream, dataToSend);

			var bytes = stream.GetBuffer();

			_client.Send(bytes, bytes.Length, _sendEndpoint);

			_client.EndReceive(result, ref _listenEndpoint);
			StartListen();
		}
	}
}
