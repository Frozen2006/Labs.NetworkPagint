using BSUIR.NetworkPaint.Models;
using BSUIR.NetworkPaint.Models.ToolsConstructors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BSUIR.NetworkPaint.AppLogic
{
	public class SceneManager
	{
		private ServerConnection _connection = new ServerConnection();
		private BaseToolConstructor _currentTool;
		private Graphics _graphics;
		private Color _currentColor = Color.Black;
		private Timer _timer;

		private const int UpdatePeriod = 10;

		public string ServerName { get; set; }

		public SceneManager(Graphics graphics)
		{
			_graphics = graphics;
			_currentTool = new PointConstructor(_connection.SendPackage, _graphics);
			_timer = new Timer(TimerProc, null, 200, UpdatePeriod);
		}

		private void TimerProc(object obj)
		{
			TransferPackage[] data = new TransferPackage[0];

			try
			{
				data = _connection.GetRecivedData();
			}
			catch (Exception)
			{
				_timer.Dispose();
			}

			foreach (var tool in data)
			{
				BaseToolConstructor currentTool = new PointConstructor(null, _graphics);
				if (tool.Figure == FigureTypeEnum.Point)
				{
					currentTool = new PointConstructor(null, _graphics);
				}
				if (tool.Figure == FigureTypeEnum.Rectangle)
				{
					currentTool = new RectangleConstructor(null, _graphics);
				}
				currentTool.Draw(tool.X, tool.Y, tool.Width, tool.Height, tool.Color);
			}
		}

		public void Start()
		{
			_connection.Connect();
			ServerName = _connection.GetServerName();
		}

		public void End()
		{
			_connection.Disconnect();
		}

		public void OnButtonDown(int x, int y)
		{
			var data = new FormClickData()
			{
				CurrentColor = _currentColor,
				X = x,
				Y = y
			};

			_currentTool.OnButtonDown(data);
		}

		public void OnMouseMove(int x, int y)
		{
			var data = new FormClickData()
			{
				CurrentColor = _currentColor,
				X = x,
				Y = y
			};

			_currentTool.OnMouseMove(data);
		}

		public void OnButtonUp(int x, int y)
		{
			var data = new FormClickData()
			{
				CurrentColor = _currentColor,
				X = x,
				Y = y
			};

			_currentTool.OnButtonUp(data);
		}

		public void ChangeCurrentColor(Color color)
		{
			_currentColor = color;
		}

		public void ChangeTool(FigureTypeEnum tool)
		{
			if (tool == FigureTypeEnum.Point)
			{
				_currentTool = new PointConstructor(_connection.SendPackage, _graphics);
			}
			if (tool == FigureTypeEnum.Rectangle)
			{
				_currentTool = new RectangleConstructor(_connection.SendPackage, _graphics);
			}
		}
	}
}
