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
			Location = new LocationObject(folder);
			Set = new Set();			
		}

		public LocationObject Location { get; private set; }
		public Set Set { get; set; }
	}

}
