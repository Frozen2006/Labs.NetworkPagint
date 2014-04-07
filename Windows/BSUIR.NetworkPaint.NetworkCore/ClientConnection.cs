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

namespace BSUIR.NetworkPaint.NetworkCore
{
	public class ClientConnection
	{
		private TcpClient _client;
		private Thread _acceptThread;
		private List<TransferPackage> _reciveBuffer = new List<TransferPackage>();
		private bool _isConnectionClosed;

		public ClientConnection(IPAddress address, int port)
		{
			_client = new TcpClient(address.ToString(), port);

			_acceptThread = new Thread(Listen);
			_acceptThread.Start();
		}

		private void Listen()
		{
			NetworkStream stream = _client.GetStream();
			while (_client.Connected)
			{
				BinaryFormatter formatter = new BinaryFormatter();

				TransferPackage recivedData = new TransferPackage();
				try
				{
					recivedData = (TransferPackage)formatter.Deserialize(stream);
				}
				catch (SerializationException e)
				{
					_isConnectionClosed = true;
				}

				lock (_reciveBuffer)
				{
					_reciveBuffer.Add(recivedData);
				}
			}
		}

		public bool IsConnected()
		{
			return _client.Connected;
		}

		public void Disconnect()
		{
			_client.Close();
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
			var stream = _client.GetStream();

			BinaryFormatter formatter = new BinaryFormatter();

			formatter.Serialize(stream, package);
		}
	}
}
