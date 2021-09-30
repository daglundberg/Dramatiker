using System;
using System.Collections.Generic;
using System.IO;

namespace Dramatiker.Library
{
	public class Set
	{
		public List<AudioItem> AudioItems { get; set; }
		public List<IEvent> Events { get; set; }
		public int CurrentIndex { get; private set; }

		public Set()
		{
			CurrentIndex = 0;
			Events = new List<IEvent>();
			AudioItems = new List<AudioItem>();
		}

		public void LoadFromFile(string pathToSetFile)
		{
			var locationObject = new LocationObject(Path.GetDirectoryName(pathToSetFile));

			using StreamReader streamReader = new StreamReader(pathToSetFile);
			while (streamReader.EndOfStream == false)
			{
				var line = streamReader.ReadLine().Split(',');
				switch (line[0])
				{
					case "AudioItem":
						var ai = new AudioItem(locationObject);
						ai.LoadFromText(line, AudioItems);
						AudioItems.Add(ai);
						break;
					case "FadeInEvent":
						var fie = new FadeInEvent();
						fie.LoadFromText(line, AudioItems);
						Events.Add(fie);
						break;
					case "FadeOutEvent":
						var foe = new FadeOutEvent();
						foe.LoadFromText(line, AudioItems);
						Events.Add(foe);
						break;
					case "CrossFadeEvent":
						var cfe = new CrossFadeEvent();
						cfe.LoadFromText(line, AudioItems);
						Events.Add(cfe);
						break;
				}
			}

			CurrentIndex = 0;
		}

		public void SaveToFile(string pathToSetFile)
        {
			using StreamWriter streamWriter = new StreamWriter(pathToSetFile);
			foreach (AudioItem audioItem in AudioItems)
			{
				streamWriter.WriteLine(audioItem.TextFromObject());
			}

			foreach (IEvent e in Events)
			{
				streamWriter.WriteLine(e.TextFromObject());
			}
			streamWriter.Flush();

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
