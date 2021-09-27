using ManikinMadness.Library;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace ManikinMadness.SetCreator
{
	public partial class EventControl : UserControl
	{
		public IEvent Event { get; private set; }
		private Project _projectReference;
		private EventControl()
		{
			InitializeComponent();
		}

		public EventControl(IEvent eventObject, Project project)
		{
			Event = eventObject;
			_projectReference = project;
			InitializeComponent();

			UpdateLabels();
		}

		private void EventControl_Click(object sender, EventArgs e)
		{
			CustomPropertyGridForm gridForm = new CustomPropertyGridForm(Event, _projectReference.AudioItems.ToList());
			if (gridForm.ShowDialog() == DialogResult.OK)
			{

			}
			UpdateLabels();
		}

		private void UpdateLabels()
        {
			lblDescription.Text = Event.GetDescription();

			lblTitle.Text = Event.GetTitle();
		}

		private void EventControl_Load(object sender, EventArgs e)
		{

		}
	}
}
