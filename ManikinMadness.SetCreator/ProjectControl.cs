using ManikinMadness.Library;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ManikinMadness.SetCreator
{
	public partial class ProjectControl : UserControl
	{
		public Project Project { get; private set; }
		public ToolStripItem ToolStripItem;
		public ProjectControl()
		{
			InitializeComponent();
		}

		public ProjectControl(Project project)
		{
			Project = project;
			InitializeComponent();
			ToolStripItem = contextMenuStrip1.Items[0];
			listBox1.Items.Clear();
			listBox1.DataSource = Project.AudioItems;
		}

		private void addAudioFileToolStripMenuItem_Click(object sender, EventArgs e)
		{
			OpenFileDialog openFileDialog = new();
			openFileDialog.Multiselect = true;
			openFileDialog.Filter = "Audio files |*.mp3;*.wav;*.aiff;*.ogg;";
			if (openFileDialog.ShowDialog() == DialogResult.OK)
			{
				foreach (string path in openFileDialog.FileNames)
				{
					string fileName;
					if (Path.GetDirectoryName(path) == Project.Folder)
					{
						//File is already in project folder
						MessageBox.Show($"{path} is already in the project folder.", "Already included!");
						fileName = path;
					}
					else
					{
						MessageBox.Show($"{path} is not already in the project folder. It will get copied now.", "Not included!");
						//Copy the file to project folder
						string newFileName = Path.Combine(Project.Folder, Path.GetFileName(path));

						if (File.Exists(newFileName))
						{
							if (MessageBox.Show("There is already a file in the project folder with the same name. Do you want to overwrite it?", "Overwrite?", MessageBoxButtons.YesNoCancel) != DialogResult.Yes)
							{
								fileName = null;
								return;
							}
						}
						fileName = newFileName;

						File.Copy(path, newFileName, true);

					}
					if (fileName != null)
						Project.AudioItems.Add(new AudioItem(0, fileName, true, 1));
				}
			}
		}

		private void createFadeInEventToolStripMenuItem_Click(object sender, EventArgs e)
		{
			FadeInEvent fadeInEvent = new FadeInEvent(null, 5000);
			Project.Set.Events.Add(fadeInEvent);
			EventControl eventControl = new EventControl(fadeInEvent, Project);
			flowLayoutPanel1.Controls.Add(eventControl);
			eventControl.Width = flowLayoutPanel1.Width - 30;
		}

		private void ProjectControl_Load(object sender, EventArgs e)
		{

		}
	}
}
