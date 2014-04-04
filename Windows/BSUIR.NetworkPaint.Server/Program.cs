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
		static void Main(string[] args)
		{
			Console.ReadKey();
		}
	}
}
