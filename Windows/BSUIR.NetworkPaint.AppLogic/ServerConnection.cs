﻿using BSUIR.NetworkPaint.NetworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.NetworkPaint.AppLogic
{
	public class ServerConnection
	{
		public void FindAServer()
		{
			ServerFinder finder = new ServerFinder(2431);

			var a = finder.Find();

			ClientConnection connection = new ClientConnection(a.Addresses[0], 2435);

			connection.SendPackage(new Models.TransferPackage());

			var data = connection.GetRecivedData();
			data = connection.GetRecivedData();
			data = connection.GetRecivedData();
			data = connection.GetRecivedData();
		}
	}
}
