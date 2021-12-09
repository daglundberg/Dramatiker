using Dramatiker.Library.Lights.Backends;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dramatiker.Creator
{
	public partial class DummyBackendForm : Form
	{
		public DummyBackendForm()
		{
			InitializeComponent();
		}
		private DummyBackend _dummy;
		//private List<PictureBox> pictureBoxes = new List<PictureBox>();
		public DummyBackendForm(DummyBackend backend)
		{
			InitializeComponent();
			_dummy = backend;

			backend.LightUpdated += Backend_LightUpdated;

			for (int i = 0; i < 3; i ++)
			{
				flowLayoutPanel1.Controls.Add(new PictureBox() { Width = 100, Height = 100 });
			}
		
		}

		private delegate void EventArgsDelegate(object sender, EventArgs ea);

		private void Backend_LightUpdated(object sender, EventArgs e)
		{
			if (InvokeRequired)
			{
				Invoke(new EventArgsDelegate(Backend_LightUpdated), new object[] { sender, e });
				return;
			}

			for (int i = 0; i < 3; i++)
			{
				var picture = flowLayoutPanel1.Controls[i];
				picture.BackColor = _dummy.GetColor(i * 4);
				picture.Refresh();

			}
			
		}
	}
}
