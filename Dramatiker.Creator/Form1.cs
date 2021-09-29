using System;
using System.IO;
using System.Windows.Forms;
using Dramatiker.Library;

namespace Dramatiker.SetCreator
{
	public partial class Form1 : Form
	{
		//private Project _currentProject;
		private ProjectControl _projectControl;

		public Form1()
		{
			InitializeComponent();
			CloseProject();
		}

		private void Form1_Load(object sender, EventArgs e)
		{

		}

		private void newProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			//close current project first! 
			CloseProject();

			//determine project folder
			FolderBrowserDialog folderBrowserDialog = new();
			if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{

				var newProj = new Project(folderBrowserDialog.SelectedPath);
				var projectControl = new ProjectControl(newProj);

				_projectControl = projectControl;
				this.Controls.Add(_projectControl);
				menuStrip1.Items.Add(_projectControl.ToolStripItem);

				_projectControl.Dock = DockStyle.Fill;
				Controls.Remove(menuStrip1);
				Controls.Add(menuStrip1);
				menuStrip1.Dock = DockStyle.Top;

			}
			else
				return;

			closeProjectToolStripMenuItem.Enabled = true;
			saveToolStripMenuItem.Enabled = true;
			exportSetToolStripMenuItem.Enabled = true;
			
		}

		private void CloseProject()
		{
			if (_projectControl!=null)
				if (_projectControl.ToolStripItem != null)
					menuStrip1.Items.Remove(_projectControl.ToolStripItem);

			this.Controls.Remove(_projectControl);

			closeProjectToolStripMenuItem.Enabled = false;
			saveToolStripMenuItem.Enabled = false;
			exportSetToolStripMenuItem.Enabled = false;
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Application.Exit();			
		}

		private void closeProjectToolStripMenuItem_Click(object sender, EventArgs e)
		{
			CloseProject();
		}

		private void addAudioFileToolStripMenuItem_Click(object sender, EventArgs e)
		{			

		}
	}
}
