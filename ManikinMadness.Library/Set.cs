using System;
using System.Collections.Generic;

namespace ManikinMadness.Library
{
	public class Set
	{
		public string Name { get; set; }
		public List<IEvent> Events { get; set; }
		public int CurrentIndex { get; private set; }

		public Set()
		{
			CurrentIndex = 0;
			Events = new List<IEvent>();
		}

		public void LoadFromFile(string pathToSetFile)
		{
			CurrentIndex = 0;

			Events = new List<IEvent>();

			List<AudioItem> audioItems = new List<AudioItem>();
			audioItems.Add(new AudioItem(1, @"C:\Users\Dag\Desktop\vanilla.mp3", true));
			audioItems.Add(new AudioItem(2, @"C:\Users\Dag\Desktop\hmm.mp3", true));
			audioItems.Add(new AudioItem(3, @"C:\Users\Dag\Desktop\Havet 3.mp3", true));
			audioItems.Add(new AudioItem(4, @"C:\Users\Dag\Desktop\Havet 4.mp3", true));
			audioItems.Add(new AudioItem(5, @"C:\Users\Dag\Desktop\Havet 5.mp3", true));
			audioItems.Add(new AudioItem(6, @"C:\Users\Dag\Desktop\Havet 6.mp3", true));

			foreach (AudioItem item in audioItems)
			{
				Events.Add(new FadeInEvent(item, 4500));
				Events.Add(new FadeOutEvent(item, 25000));
			}
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
