using BSUIR.NetworkPaint.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BSUIR.NetworkPaint.Server
{
	public class ServerDataResender
	{
		private List<Socket> _clients = new List<Socket>();
        private Socket _socket;
        private EndPoint endPoint;


		public ServerDataResender(int port)
		{
			/*_listner = new TcpListener(IPAddress.Any, port);
                        _listner.Start();*/
            endPoint = new IPEndPoint(IPAddress.Any, port);

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(endPoint);

			StartListen();
		}

		private async void StartListen()
		{
            _socket.Listen(Int32.MaxValue);
            while(true)
            {
                var client = _socket.Accept();
                Thread thread = new Thread(() => ResendData(client));
                thread.Start();
            }
		}

		private void ResendData(Socket client)
		{
            Console.WriteLine("Client connected!");

			_clients.Add(client);

            BinaryFormatter formatter = new BinaryFormatter();

            var stream = new NetworkStream(client);

            
            TransferPackage data = null;
            int brokedPackages = 0;

			while (client.Connected)
			{
                try
                {
                    var incomingData = (string)formatter.Deserialize(stream);
                    data = CustomXmlSerializer.Deserialize<TransferPackage>(incomingData);
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
                catch(IOException)
                {
                    Console.WriteLine("Network problem. Client was disconnected without 'disconnect()' call");
                    _clients.Remove(client);
                    return;
                }
                
				
				foreach (var currentClient in _clients)
				{
                    var outData = CustomXmlSerializer.Serialize<TransferPackage>(data);
                    formatter.Serialize(new NetworkStream(currentClient), outData);
				}
			}
		}
	}
}
