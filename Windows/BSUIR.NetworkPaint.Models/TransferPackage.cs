using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.NetworkPaint.Models
{
	[Serializable]
	public class TransferPackage
	{
		public Guid ClientId { get; set; }
		public FigureTypeEnum Figure { get; set; }
		public int X { get; set; }
		public int Y { get; set; }
		public int Width { get; set; }
		public int Height { get; set; }
	}
}
