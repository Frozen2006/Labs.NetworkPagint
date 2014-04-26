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

namespace BSUIR.NetworkPaint.Server
{
	public class FindingResponcer
	{
		private ServerData _data;
		private int _port;
		private UdpClient _client;
		private IPEndPoint _listenEndpoint;
		private IPEndPoint _sendEndpoint;
        private Socket _socket;
        private bool _isActive = true;
        Thread _listenThread;

		public FindingResponcer(int port, string serverName)
		{
			_port = port;

			_data = new ServerData()
			{
				ServerName = serverName
			};

			_listenEndpoint = new IPEndPoint(IPAddress.Any, port+1);
			_sendEndpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), port);


			//_client = new UdpClient(_listenEndpoint);

			List<IPAddress> addresses = new List<IPAddress>();

			var host = Dns.GetHostEntry(Dns.GetHostName());
			foreach (IPAddress ip in host.AddressList)
			{
				if (ip.AddressFamily == AddressFamily.InterNetwork)
				{
                    if (ip.Address == IPAddress.Parse("192.168.173.1").Address)
                    {
                        continue;
                    }
					addresses.Add(ip);
				}
			}

            _data.Addresses = addresses.Select(m => m.ToString()).ToArray();

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.EnableBroadcast = true;
            _socket.Bind(_listenEndpoint);

			StartListen();
		}


		public void StartListen()
		{
            _listenThread = new Thread(() => ListenCycle());
            _listenThread.Start();
		}

        public void ListenCycle()
        {
            while (_isActive)
            {
                byte[] buffer = new byte[8192];
                _socket.Receive(buffer); //this is a blicking operation. It will be ended only when request was recived

                ReciveCallback(_socket);
                //Thread sendThread = new Thread(() => ReciveCallback(_socket)); //start new thread to resend server data
                //sendThread.Start();
            }
        }

		private void ReciveCallback(Socket socket)
		{
            Console.WriteLine("Tacked UDP package");
			MemoryStream stream = new MemoryStream();

            BinaryFormatter formatter = new BinaryFormatter();

            var dataToSend = CustomXmlSerializer.Serialize<ServerData>(_data);

            formatter.Serialize(stream, dataToSend);

			var bytes = stream.GetBuffer();

            socket.SendTo(bytes, _sendEndpoint);
		}
	}
}
