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
		public CustomPropertyGridForm(IEvent eventObject, IEnumerable<AudioItem> audioItems)
		{
			Event = eventObject;
			InitializeComponent();

			PropertyInfo[] props = Event.GetType().GetProperties();
			foreach (PropertyInfo prop in props)
			{
				if (prop.PropertyType == typeof(AudioItem))
				{					
					AudioItemSelector itemSelector = new AudioItemSelector(GetAttributeDisplayName(prop), audioItems);
					itemSelector.SelectedItemChanged += ItemSelector_SelectedItemChanged;
					flowLayoutPanel1. Controls.Add(itemSelector);
				}
				else if (prop.PropertyType == typeof(int))
				{
					Label label = new Label();
					label.Text = prop.Name;
					flowLayoutPanel1.Controls.Add(label);
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

		}
	}
}
