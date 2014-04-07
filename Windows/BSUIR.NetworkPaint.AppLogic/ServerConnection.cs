using BSUIR.NetworkPaint.Models;
using BSUIR.NetworkPaint.NetworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.NetworkPaint.AppLogic
{
	public class ServerConnection:IDisposable
	{
		private readonly int _finderPort = 2431;
		private readonly int _exchangePort = 2435;
		private readonly ServerFinder _finder;
		private ClientConnection _connection;
		private IPAddress _serverAddress;

		public ServerConnection()
		{
			_finder = new ServerFinder(_finderPort);
		}

		public ServerConnection(int finderPort, int exchangePort)
		{
			_finderPort = finderPort;
			_exchangePort = exchangePort;
			_finder = new ServerFinder(_finderPort);
		}

		public void Connect()
		{
			var recivedData = _finder.Find();
			if (recivedData == null)
			{
				throw new TimeoutException("Server wasn't finded in requested time");
			}
			_serverAddress = recivedData.Addresses.First();

			_connection = new ClientConnection(_serverAddress, _exchangePort);
		}

		public void SendPackage(TransferPackage package)
		{
			_connection.SendPackage(package);
		}

		public TransferPackage[] GetRecivedData()
		{
			return _connection.GetRecivedData();
		}

		public void Disconnect()
		{
			if (_connection != null)
			{
				_connection.Disconnect();
			}
			else
			{
				throw new Exception("Not connected yet");
			}
		}

		private bool _isDisposed;

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		public void Dispose(bool forceDispose)
		{
			if (!_isDisposed)
			{
				if (forceDispose)
				{
					Disconnect();
				}
				_isDisposed = true;
			}
		}


		~ServerConnection()
		{
			Dispose(false);
		}




		/*public void FindAServer()
		{
			ServerFinder finder = new ServerFinder(2431);

			var a = finder.Find();

			ClientConnection connection = new ClientConnection(a.Addresses[0], 2435);

			connection.SendPackage(new Models.TransferPackage());

			var data = connection.GetRecivedData();
			data = connection.GetRecivedData();
			data = connection.GetRecivedData();
			data = connection.GetRecivedData();
		}*/
	}
}
