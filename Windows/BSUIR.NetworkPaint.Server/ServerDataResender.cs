using BSUIR.NetworkPaint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BSUIR.NetworkPaint.Server
{
	public class ServerDataResender
	{
		private TcpListener _listner;
		private List<TcpClient> _clients = new List<TcpClient>();

		public ServerDataResender(int port)
		{
			_listner = new TcpListener(IPAddress.Any, port);
            _listner.Start();
			StartListen();
		}

		private void StartListen()
		{
			_listner.BeginAcceptTcpClient(new AsyncCallback(ResendData), _listner);
		}

		private void ResendData(IAsyncResult result)
		{
            Console.WriteLine("Client connected!");

            var listner = (TcpListener)result.AsyncState;

            var client = _listner.EndAcceptTcpClient(result);
            StartListen();
            
			_clients.Add(client);

            XmlSerializer serializer = new XmlSerializer(typeof(TransferPackage));
			var stream = client.GetStream();

            TransferPackage data = null;
            int brokedPackages = 0;

			while (client.Connected)
			{
                try
                {
                    data = (TransferPackage)serializer.Deserialize(stream);
                }
                catch(SerializationException)
                {
                    if (client.Connected)
                    {
                        Console.WriteLine("Recived broked data");
                        brokedPackages++;

                        if (brokedPackages > 10)
                        {
                            Console.WriteLine("Client disconnected");
                            _clients.Remove(client);
                            return;
                        }
                        continue;
                    } 
                    else
                    {
                        Console.WriteLine("Client disconnected");
                        _clients.Remove(client);
                        return;
                    }
                }
				
				foreach (var currentClient in _clients)
				{
					serializer.Serialize(currentClient.GetStream(), data);
				}
			}
		}
	}
}
