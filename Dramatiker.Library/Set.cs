using Polenter.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Xml.Serialization;

namespace Dramatiker.Library
{
	public class Set
	{
		public string Name { get; set; }

		[DataMember]
		public List<IEvent> Events { get; set; }
		public int CurrentIndex { get; private set; }

		public Set()
		{
			CurrentIndex = 0;
			Events = new List<IEvent>();
		}

		public void LoadFromFile(string pathToSetFile)
		{
			var serializer = new SharpSerializer();
			Events = (List<IEvent>)serializer.Deserialize(pathToSetFile);

			CurrentIndex = 0;
		}

		public void SaveToFile(string pathToSetFile)
        {
			var serializer = new SharpSerializer();
			serializer.Serialize(Events, pathToSetFile);
		}

		public void TriggerNext(AudioPlayer audioPlayer)
		{
			Console.WriteLine($"{CurrentIndex + 1} / {Events.Count}: {Events[CurrentIndex]}");

			Events[CurrentIndex].ApplyEvent(audioPlayer);
			CurrentIndex++;
		}

		public IEvent GetNextEvent()
        {
			if (CurrentIndex < Events.Count)
				return Events[CurrentIndex];
			else
				return null;
        }

		public IEvent GetPreviousEvent()
        {
			if (CurrentIndex > 0)
				return Events[CurrentIndex - 1];
			else
				return null;
        }

		public void GoBack()
		{
			CurrentIndex--;
		}

		public void Restart()
        {
			CurrentIndex = 0;
        }

		public void GoForward()
		{
			CurrentIndex++;
		}

		public bool IsCompleted
		{
			get
			{
				return CurrentIndex >= Events.Count;
			}
		}
	}
}
