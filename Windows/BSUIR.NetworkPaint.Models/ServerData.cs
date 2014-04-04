using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.NetworkPaint.Models
{
	[Serializable]
	public class ServerData
	{
		public string ServerName { get; set; }
		public IPAddress[] Addresses { get; set; }
	}
}
