using BSUIR.NetworkPaint.AppLogic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSUIR.NetworkPaint.UI
{
	public partial class Desk : Form
	{
		private SceneManager _manager;

		public Desk()
		{
			InitializeComponent();

			_manager = new SceneManager(paintSurface.CreateGraphics());
		}

		

		private void Form1_Load(object sender, EventArgs e)
		{
			_manager.Start();
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
	}
}
