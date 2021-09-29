using System;
using System.Collections.Generic;
using System.ComponentModel;
using Dramatiker.Library;

namespace Dramatiker.SetCreator
{
	public class Project
	{
		public Project(string folder)
		{
			Folder = folder;
			AudioItems = new BindingList<AudioItem>();
			Set = new Set();			
		}

		public string Folder { get; private set; }
		List<IEvent> Events { get; set; }
		public BindingList<AudioItem> AudioItems { get; set; }
		public Set Set { get; set; }

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
