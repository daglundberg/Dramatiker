using System;
using System.Collections.Generic;

namespace Dramatiker.Library
{
	public class LightItem : IEvent, ISerial
	{
		public void ApplyEvent(AudioPlayer audioPlayer)
		{
			throw new NotImplementedException();
		}

		public string GetDescription()
		{
			throw new NotImplementedException();
		}

		public string GetTitle()
		{
			throw new NotImplementedException();
		}

		public void LoadFromText(string[] data, List<AudioItem> audioItems)
		{
			throw new NotImplementedException();
		}

		public string TextFromObject()
		{
			throw new NotImplementedException();
		}
	}
}
