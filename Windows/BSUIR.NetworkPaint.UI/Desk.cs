using BSUIR.NetworkPaint.AppLogic;
using BSUIR.NetworkPaint.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSUIR.NetworkPaint.UI
{
	public partial class Desk : Form
	{
		private SceneManager _manager;
		private LoadingWindow _loadingWindow;

		public Desk()
		{
			this.Hide();
			InitializeComponent();

			_loadingWindow = new LoadingWindow();
			ThreadPool.QueueUserWorkItem(RunNewForm);

			_manager = new SceneManager(paintSurface.CreateGraphics());
		}


		private void RunNewForm(object info)
		{
			_loadingWindow.Show();
			Application.Run();
		}


		private void Form1_Load(object sender, EventArgs e)
		{
			_manager.Start();

			this.Invoke((MethodInvoker)delegate()
			{
				_loadingWindow.Hide();
				_loadingWindow.Dispose();
			});

			
			this.Show();

			this.Text = "Interactivity desk: " + _manager.ServerName;
		}

		private void Desk_FormClosing(object sender, FormClosingEventArgs e)
		{
			_manager.End();
		}

		private void paintSurface_MouseUp(object sender, MouseEventArgs e)
		{
			_manager.OnButtonUp(e.X, e.Y);
		}

		private void paintSurface_MouseDown(object sender, MouseEventArgs e)
		{
			_manager.OnButtonDown(e.X, e.Y);
		}

		private void paintSurface_MouseLeave(object sender, EventArgs e)
		{
			_manager.OnButtonUp(0, 0);
		}

		private void paintSurface_MouseMove(object sender, MouseEventArgs e)
		{
			_manager.OnMouseMove(e.X, e.Y);
		}

		private void colorDisplay_Click(object sender, EventArgs e)
		{
			if (colorDialog1.ShowDialog() == DialogResult.OK)
			{
				colorDisplay.BackColor = colorDialog1.Color;
				_manager.ChangeCurrentColor(colorDialog1.Color);
			}
		}

		private void pointRadio_CheckedChanged(object sender, EventArgs e)
		{
			_manager.ChangeTool(FigureTypeEnum.Point);
		}

		private void rectabgleRadio_CheckedChanged(object sender, EventArgs e)
		{
			_manager.ChangeTool(FigureTypeEnum.Rectangle);
		}


	}
}
