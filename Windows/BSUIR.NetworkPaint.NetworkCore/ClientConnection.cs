using BSUIR.NetworkPaint.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BSUIR.NetworkPaint.NetworkCore
{
	public class ClientConnection
	{
		private Thread _acceptThread;
		private List<TransferPackage> _reciveBuffer = new List<TransferPackage>();
		private bool _isConnectionClosed;
        private Socket _socket;
        private IPEndPoint _endpoint;
        private NetworkStream _stream;

		public ClientConnection(IPAddress address, int port)
		{
            _endpoint = new IPEndPoint(address, port);

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Connect(_endpoint);

            _stream = new NetworkStream(_socket);

			_acceptThread = new Thread(Listen);
			_acceptThread.Start();
		}

		private void Listen()
		{
            NetworkStream stream = _stream;
			while (_socket.Connected)
			{
                //XmlSerializer serializer = new XmlSerializer(typeof(TransferPackage));
                BinaryFormatter formatter = new BinaryFormatter();


				TransferPackage recivedData = new TransferPackage();
				try
				{
                    var incomingData = (string)formatter.Deserialize(stream);
                    recivedData = CustomXmlSerializer.Deserialize<TransferPackage>(incomingData);
				}
				catch (SerializationException e)
				{
					_isConnectionClosed = true;
				}
                catch(DecoderFallbackException e)
                {

                }

				lock (_reciveBuffer)
				{
					_reciveBuffer.Add(recivedData);
				}
			}
		}

		public bool IsConnected()
		{
			return _socket.Connected;
		}

		public void Disconnect()
		{
            _socket.Disconnect(true);
			_socket.Close();
		}

		public TransferPackage[] GetRecivedData()
		{
			if (_isConnectionClosed)
			{
				throw new Exception("Connection closed");
			}

			TransferPackage[] data;
			lock (_reciveBuffer)
			{
				data = _reciveBuffer.ToArray();
				_reciveBuffer.Clear();
			}
			return data;
		}

		public void SendPackage(TransferPackage package)
		{
            var stream = _stream;

            BinaryFormatter formatter = new BinaryFormatter();

            var data = CustomXmlSerializer.Serialize<TransferPackage>(package);

			formatter.Serialize(stream, data);
		}
	}
}
