using System;
using System.Collections.Generic;
using System.ComponentModel;
using ManikinMadness.Library;

namespace ManikinMadness.SetCreator
{
	public class Project
	{
		public Project(string folder)
		{
			Folder = folder;
			AudioItems = new BindingList<AudioItem>();
		}

		public string Folder { get; private set; }
		List<IEvent> Events { get; set; }
		public BindingList<AudioItem> AudioItems { get; set; }

		public void Save()
		{

		}

		public Set CreateSet()
		{
			throw new NotImplementedException();
			//return null;
		}
	}
}
