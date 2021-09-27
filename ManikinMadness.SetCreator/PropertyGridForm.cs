using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ManikinMadness.Library;

namespace ManikinMadness.SetCreator
{
	public partial class PropertyGridForm : Form
	{
		private PropertyGridForm()
		{
			InitializeComponent();
		}

		public PropertyGridForm(IEvent eventObject)
		{
			InitializeComponent();
			propertyGrid1.SelectedObject = eventObject;
		}
	}
}
