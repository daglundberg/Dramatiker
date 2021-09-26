using System;
using System.Collections.Generic;
using ManikinMadness.Library;

namespace ManikinMadness
{
	partial class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to Manikin Madness! Written by Dag Lundberg (c) 2021");

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
				events.Add(new FadeInEvent(item, 5500));
				events.Add(new FadeOutEvent(item, 5500));
			}

			int currentEvent = 0;
			Console.WriteLine($"{currentEvent} / {events.Count}");

			while (currentEvent < events.Count)
			{
				Console.ReadKey();
				events[currentEvent].ApplyEvent(audioPlayer);
				Console.WriteLine($"{currentEvent+1} / {events.Count}: {events[currentEvent]}");

				currentEvent++;
			}
			Console.WriteLine($"Finished set.");
			Console.ReadKey();

			audioPlayer.Dispose();
		}
	}
}
