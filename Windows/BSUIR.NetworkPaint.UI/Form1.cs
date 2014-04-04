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
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
		}

		ServerConnection _connection = new ServerConnection();

		private void Form1_Load(object sender, EventArgs e)
		{
			_connection.FindAServer();
		}
	}
}
