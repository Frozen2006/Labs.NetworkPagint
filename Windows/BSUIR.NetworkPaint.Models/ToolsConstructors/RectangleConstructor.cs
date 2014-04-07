using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.NetworkPaint.Models.ToolsConstructors
{
	public class RectangleConstructor : BaseToolConstructor
	{
		private bool _isPaint;
		private int _startX;
		private int _startY;
		private Graphics _originalBmp;

		public RectangleConstructor(SendData sendDataCallback, Graphics graphics)
			: base(sendDataCallback, graphics)
		{
			this._type = FigureTypeEnum.Rectangle;
		}

		public override void Draw(int x, int y, int width, int height, Color color)
		{
			_graphics.DrawRectangle(new Pen(color), x, y, width, height);
		}


		public override void OnButtonDown(FormClickData e)
		{
			_isPaint = true;
			_startX = e.X;
			_startY = e.Y;
			//GraphicsBitmapConverter.GraphicsToBitmap(g, Rectangle.Truncate(g.VisibleClipBounds));
		}

		public override void OnMouseMove(FormClickData e)
		{
			/*if (_isPaint)
			{
				Draw(e.X, e.Y, 5, 5, e.CurrentColor);
				Send(e.X, e.Y, 5, 5, e.CurrentColor);
			}*/
		}

		public override void OnButtonUp(FormClickData e)
		{
			if (e.X == 0 && e.Y == 0)
			{
				_isPaint = false;
				return;
			}
			Draw(_startX, _startY, e.X - _startX, e.Y - _startY, e.CurrentColor);
			Send(_startX, _startY, e.X - _startX, e.Y - _startY, e.CurrentColor);
			_isPaint = false;
		}
	}
}
