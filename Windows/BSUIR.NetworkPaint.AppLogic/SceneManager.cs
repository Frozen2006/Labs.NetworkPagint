﻿using BSUIR.NetworkPaint.Models;
using BSUIR.NetworkPaint.Models.ToolsConstructors;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR.NetworkPaint.AppLogic
{
	public class SceneManager
	{
		private ServerConnection _connection = new ServerConnection();
		private BaseToolConstructor _currentTool;
		private Graphics _graphics;
		private Color _currentColor = Color.Black;

		public SceneManager(Graphics graphics)
		{
			_graphics = graphics;
			_currentTool = new PointConstructor(_connection.SendPackage, _graphics);
		}

		public void Start()
		{
			_connection.Connect();
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
	}
}
