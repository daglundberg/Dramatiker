using Dramatiker.Library;
using Dramatiker.Library.Lights;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Dramatiker.SetCreator
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

			foreach (IAudioEvent e in Project.Set.Events)
			{
				EventControl eventControl = new EventControl(e, Project);
				flowLayoutPanel1.Controls.Add(eventControl);
				eventControl.Width = flowLayoutPanel1.Width - 30;
			}
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
					if (Path.GetDirectoryName(path) == Project.Location.CurrentLocation)
					{
						//File is already in project folder
						//MessageBox.Show($"{path} is already in the project folder.", "Already included!");
						fileName = path;
					}
					else
					{
						MessageBox.Show($"{path} is not already in the project folder. It will get copied now.", "Not included!");
						//Copy the file to project folder
						string newFileName = Path.Combine(Project.Location.CurrentLocation, Path.GetFileName(path));

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
						Project.Set.AudioItems.Add(new AudioItem(Project.Location, Path.GetFileName(fileName), true, 1));
				}
			}
		}

		private void createFadeInEventToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Project.Set.AudioItems.Count > 0)
			{
				var fadeInEvent = new FadeInEvent(Project.Set.AudioItems[0], 15000);

				CustomPropertyGridForm customPropertyGridForm = new CustomPropertyGridForm(fadeInEvent, Project.Set.AudioItems.ToList());
				if (customPropertyGridForm.ShowDialog() == DialogResult.OK)
				{
					Project.Set.Events.Add(fadeInEvent);
					EventControl eventControl = new EventControl(fadeInEvent, Project);
					flowLayoutPanel1.Controls.Add(eventControl);
					eventControl.Width = flowLayoutPanel1.Width - 30;
				}
			}
			else
			{
				MessageBox.Show("Can't create a Fade In Event without at least one audio item in the pool.");
			}
		}

		private void ProjectControl_Load(object sender, EventArgs e)
		{

		}

		private void createFadeOutEventToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Project.Set.AudioItems.Count > 0)
			{
				var fadeOutEvent = new FadeOutEvent(Project.Set.AudioItems[0], 30000);

				CustomPropertyGridForm customPropertyGridForm = new CustomPropertyGridForm(fadeOutEvent, Project.Set.AudioItems.ToList());
				if (customPropertyGridForm.ShowDialog() == DialogResult.OK)
				{
					Project.Set.Events.Add(fadeOutEvent);
					EventControl eventControl = new EventControl(fadeOutEvent, Project);
					flowLayoutPanel1.Controls.Add(eventControl);
					eventControl.Width = flowLayoutPanel1.Width - 30;
				}
			}
			else
			{
				MessageBox.Show("Can't create a Fade Out Event without at least one audio item in the pool.");
			}
		}

        private void playbackToolStripMenuItem_Click(object sender, EventArgs e)
        {
			Project.Set.Restart();
			PlaybackForm playbackForm = new PlaybackForm(Project.Set);
			playbackForm.Show();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
			string path = Path.Combine(Project.Location.CurrentLocation, "set.drama");
			Project.Set.SaveToFile(path);
        }
    }
}
