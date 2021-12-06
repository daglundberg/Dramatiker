using System;
using System.Collections.Generic;
using System.IO;

namespace Dramatiker.Library
{
	public class Set
	{
		public List<AudioItem> AudioItems { get; set; }
		public List<IEvent> Events { get; set; }
		public List<Cue> Cues { get; set; }
		public int CurrentIndex { get; private set; }

		public Set()
		{
			CurrentIndex = 0;
			Events = new List<IEvent>();
			AudioItems = new List<AudioItem>();
			Cues = new List<Cue>();
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
					case "Cue":
						var cue = new Cue();
						cue.LoadFromText(line, AudioItems);
						Cues.Add(cue);
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

			foreach (Cue cue in Cues)
			{
				streamWriter.WriteLine(cue.TextFromObject());
			}

			streamWriter.Flush();

		}

		public void TriggerNext(AudioPlayer audioPlayer)
		{
			Console.WriteLine($"{CurrentIndex + 1} / {Cues.Count}: {Cues[CurrentIndex]}");

			foreach (IEvent e in Cues[CurrentIndex].Events)
			{
				e.ApplyEvent(audioPlayer);
			}

			CurrentIndex++;
		}

		public Cue GetNextCue()
        {
			if (CurrentIndex < Cues.Count)
				return Cues[CurrentIndex];
			else
				return null;
        }

		public Cue GetPreviousCue()
        {
			if (CurrentIndex > 0)
				return Cues[CurrentIndex - 1];
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
				return CurrentIndex >= Cues.Count;
			}
		}
	}
}
