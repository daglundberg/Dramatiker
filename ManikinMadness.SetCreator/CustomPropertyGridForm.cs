using ManikinMadness.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManikinMadness.SetCreator
{
	public partial class CustomPropertyGridForm : Form
	{
		private CustomPropertyGridForm()
		{
			InitializeComponent();
		}

		public IEvent Event { get; private set; }
		PropertyInfo[] properties;
		public CustomPropertyGridForm(IEvent eventObject, List<AudioItem> audioItems)
		{
			Event = eventObject;
			InitializeComponent();

			properties = Event.GetType().GetProperties();
			foreach (PropertyInfo prop in properties.Reverse())
			{
				if (prop.PropertyType == typeof(AudioItem))
				{					
					AudioItemSelector itemSelector = new AudioItemSelector(GetAttributeDisplayName(prop), audioItems, (AudioItem)prop.GetValue(Event));
					itemSelector.SelectedItemChanged += (object sender, EventArgs e) =>
					{
						prop.SetValue(Event, sender);
					};
					flowLayoutPanel1. Controls.Add(itemSelector);
				}
				else if (prop.PropertyType == typeof(int))
				{
					Panel panel = new Panel();
					panel.Height = 30;
					panel.Width = Width-5;
					Label label = new Label();
					label.AutoSize = true;
					label.Text = GetAttributeDisplayName(prop);
					panel.Controls.Add(label);
					label.Location = new Point(4, 4);

					NumericUpDown upDown = new NumericUpDown();
					upDown.Maximum = 5*60*1000;
					upDown.Minimum = 0;
					upDown.Increment = 1000;

					upDown.Value = (int)prop.GetValue(Event);
					upDown.ValueChanged += (object sender, EventArgs e) =>
					{
						prop.SetValue(Event, (int)upDown.Value);
					};

					panel.Controls.Add(upDown);
					upDown.Location = new Point(200, 2);
					

					flowLayoutPanel1.Controls.Add(panel);
				}
				else
				{
					Label label = new Label();
					label.Text = prop.Name;
					flowLayoutPanel1.Controls.Add(label);
				}
/*				switch (prop.PropertyType.)
				{
					case AudioItem:
						//
						break;
				}*/
			}
		}

		private void ItemSelector_SelectedItemChanged(object sender, EventArgs e)
		{
			
		}

		private string GetAttributeDisplayName(PropertyInfo property)
		{
			var atts = property.GetCustomAttributes(
				typeof(DisplayNameAttribute), true);
			if (atts.Length == 0)
				return null;
			return (atts[0] as DisplayNameAttribute).DisplayName;
		}

		private void button1_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}
