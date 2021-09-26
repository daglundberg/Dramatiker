using System;
using System.Collections.Generic;

namespace ManikinMadness
{
	partial class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Manikin Madness");

			AudioPlayer audioPlayer = new AudioPlayer();

			List<AudioItem> audioItems = new List<AudioItem>();
			audioItems.Add(new AudioItem(1, @"C:\Users\Dag\Desktop\Havet 1.mp3", true));
			audioItems.Add(new AudioItem(2, @"C:\Users\Dag\Desktop\Havet 2.mp3", true));
			audioItems.Add(new AudioItem(3, @"C:\Users\Dag\Desktop\Havet 3.mp3", true));
			audioItems.Add(new AudioItem(4, @"C:\Users\Dag\Desktop\Havet 4.mp3", true));
			audioItems.Add(new AudioItem(5, @"C:\Users\Dag\Desktop\Havet 5.mp3", true));
			audioItems.Add(new AudioItem(6, @"C:\Users\Dag\Desktop\Havet 6.mp3", true));

			List<IEvent> events = new();

			foreach (AudioItem item in audioItems)
			{
				events.Add(new FadeInEvent(item, 5000));
				events.Add(new FadeOutEvent(item, 5000));
			}
			events.AddRange(new IEvent[]
			{
/*				new FadeInEvent(audioItems[0], 5000),
				new CrossFadeEvent(audioItems[0], audioItems[1], 10000),
				new FadeOutEvent(audioItems[1], 5000),*/

/*				new FadeInEvent(audioItems[2], 5000),
				new FadeOutEvent(audioItems[2], 10000)*/
			}); ;

			int currentEvent = 0;
			Console.WriteLine($"{currentEvent} / {events.Count}");

			while (currentEvent < events.Count)
			{
				Console.ReadKey();
				events[currentEvent].ApplyEvent(audioPlayer);
				currentEvent++;
				Console.WriteLine($"{currentEvent} / {events.Count}");
			}
			Console.WriteLine($"Finished set.");
			Console.ReadKey();

			audioPlayer.Dispose();
		}
	}
}
