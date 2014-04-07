using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.NetworkPaint.Models.ToolsConstructors
{
	public class PointConstructor : BaseToolConstructor
	{
		private bool _isPaint;

		public PointConstructor(SendData sendDataCallback, Graphics graphics)
			: base(sendDataCallback, graphics)
		{
			this._type = FigureTypeEnum.Point;
		}

		public override void Draw(int x, int y, int width, int height, Color color)
		{
			_graphics.DrawRectangle(new Pen(color), x, y, 5, 5);
		}


		public override void OnButtonDown(FormClickData e)
		{
			_isPaint = true;
		}

		public override void OnMouseMove(FormClickData e)
		{
			if (_isPaint)
			{
				Draw(e.X, e.Y, 5, 5, e.CurrentColor);
				Send(e.X, e.Y, 5, 5, e.CurrentColor);
			}
		}

		public override void OnButtonUp(FormClickData e)
		{
			_isPaint = false;
		}
	}
}
