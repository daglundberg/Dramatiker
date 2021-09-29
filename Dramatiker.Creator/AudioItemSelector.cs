using Dramatiker.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dramatiker.SetCreator
{
	public partial class AudioItemSelector : UserControl
	{
		private AudioItemSelector()
		{
			InitializeComponent();
		}

		public event EventHandler SelectedItemChanged;
		public AudioItem SelectedItem { get; private set; }

		IEnumerable<AudioItem> items;
		public AudioItemSelector(string label, List<AudioItem> audioItems, AudioItem currentItem)
		{

			InitializeComponent();

			SelectedItem = currentItem;
			items = audioItems;

			label1.Text = label;
			foreach (AudioItem item in audioItems)
			{
				comboBox1.Items.Add(item.FriendlyName);
			}

			if (currentItem != null)
				comboBox1.Text = currentItem.FriendlyName;

			comboBox1.Text = "Null";

			//comboBox1.Text = "yeah";
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectedItem = items.ToArray()[comboBox1.SelectedIndex];
			SelectedItemChanged?.Invoke(SelectedItem, e);
		}
	}
}
