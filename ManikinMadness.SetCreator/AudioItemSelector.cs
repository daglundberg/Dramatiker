using ManikinMadness.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManikinMadness.SetCreator
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
		public AudioItemSelector(string label, IEnumerable<AudioItem> audioItems)
		{
			items = audioItems;
			InitializeComponent();
			label1.Text = label;
			foreach (AudioItem item in audioItems)
			{
				comboBox1.Items.Add(item.FriendlyName);
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{
			SelectedItem = items.ToArray()[comboBox1.SelectedIndex];
			SelectedItemChanged?.Invoke(SelectedItem, e);
		}
	}
}
