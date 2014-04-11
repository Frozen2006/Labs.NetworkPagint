using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.NetworkPaint.Models.ToolsConstructors
{
	public class BaseToolConstructor
	{
		public delegate void SendData(TransferPackage package);

		protected SendData _sendDataCallback;

		protected Graphics _graphics;

		protected FigureTypeEnum _type = FigureTypeEnum.Base;

		public BaseToolConstructor(SendData sendDataCallback, Graphics graphics)
		{
			_sendDataCallback = sendDataCallback;
			_graphics = graphics;
		}

		public virtual void Draw(int x, int y, int width, int height, Color color)
		{
		}

		public void Send(int x, int y, int width, int height, Color color)
		{
            
			var data = new TransferPackage()
			{
				Figure = _type,
				Height = height,
				Width = width,
				X = x,
				Y = y,
				ColorR = color.R,
                ColorG = color.G,
                ColorB = color.B
			};

			if (_sendDataCallback != null)
			{
				_sendDataCallback(data);
			}
			
		}

		public virtual void OnButtonDown(FormClickData e)
		{
		}

		public virtual void OnMouseMove(FormClickData e)
		{
		}

		public virtual void OnButtonUp(FormClickData e)
		{
		}
	}
}
