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
        private Socket _socket;
		private IPEndPoint _endpoint;
		private IPEndPoint _sendEndpoint;
		private volatile bool _isDataAccepted;
		private volatile ServerData _data;

		public ServerFinder(int port)
		{
			_endpoint = new IPEndPoint(IPAddress.Any, port);
			_sendEndpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), port+1);
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);

            _socket.SetSocketOption(SocketOptionLevel.Udp, SocketOptionName.NoDelay, 1);
     _socket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1); 
            _socket.Bind(_endpoint);

            //_socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.AddMembership, new MulticastOption(IPAddress.Parse("255.255.255.255"), IPAddress.Any));
    // May want to set this:
    _socket.SetSocketOption(SocketOptionLevel.IP, SocketOptionName.MulticastTimeToLive, 0); // only LAN

		}

		public ServerData Find()
		{
			var startTime = DateTime.Now;

            Thread q = new Thread(() => ReciveData());
            q.Start();
           // ReciveData();

			while( (!_isDataAccepted) && ( (DateTime.Now - startTime).Seconds < MaxWaitTime ) )
			{
                SocketAsyncEventArgs e = new SocketAsyncEventArgs();
                e.SetBuffer(new byte[0], 0, 0);

                _socket.Connect(_sendEndpoint);
                _socket.Send(new byte[0]);
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

        public void ReciveData()
        {
            byte[] buffer = new byte[8192];

            EndPoint converted = _endpoint;

            _socket.ReceiveFrom(buffer, ref converted);
       
        }


		public void ReciveCallback(object sender, SocketAsyncEventArgs e)
		{
            
			byte[] data = new byte[8192];

            data = e.Buffer;

			if (data.Length > 0)
			{
				MemoryStream stream = new MemoryStream(data);
                //XmlSerializer formatter = new XmlSerializer(typeof(ServerData));

                BinaryFormatter formatter = new BinaryFormatter();

				stream.Seek(0, SeekOrigin.Begin);

                var recivedData = (string)formatter.Deserialize(stream);

                _data = CustomXmlSerializer.Deserialize<ServerData>(recivedData);

				_isDataAccepted = true;
			}

			
		}
	}
}
