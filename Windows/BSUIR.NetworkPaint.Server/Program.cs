using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.NetworkPaint.Server
{
	class Program
	{
		private static FindingResponcer responcer = new FindingResponcer(2431, "Test server");
		private static ServerDataResender _dataResender = new ServerDataResender(2435);

		static void Main(string[] args)
		{
			Console.ReadKey();
		}
	}
}
