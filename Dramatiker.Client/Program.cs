using System;
using System.Threading;
using Dramatiker.Library;
using System.IO;

namespace Dramatiker.Client
{
	partial class Program
	{
		static void Main()
		{
			Console.WriteLine("Welcome to Dramatiker! Written by Dag Lundberg (c) 2021");

			/*			Set set = new Set();

						string path = "";

						if (File.Exists("/media/DRAMATIKER/set.drama"))
						{
							path = "/media/DRAMATIKER/set.drama";
						}
						else
						{
							path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Dramatiker", "Set", "set.drama");
						}

						set.LoadFromFile(path);
						Console.WriteLine($"Loaded: {path}");*/

			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Dramatiker", "Set");
			LocationObject location = new LocationObject(path);
			Set set = new Set();

			var havet1 = new AudioItem(location, "Havet 1.mp3", false);
			var havet2 = new AudioItem(location, "Havet 2.mp3", false);
			var havet6 = new AudioItem(location, "Havet 6.mp3", false);
			Light light1 = new Light() { FirstChannel = 0, Name = "Light 1" };
			Light light2 = new Light() { FirstChannel = 4, Name = "Light 2" };


			// ==== Cue 1 ====
			Cue cue1 = new Cue("Cue1");
			{
/*				FadeInEvent fadeInEvent = new FadeInEvent(havet1, 1000);
				cue1.AddEvent(fadeInEvent);*/

				SolidRegion solidRegion = new SolidRegion(Color.Red, 5);
				solidRegion.Initialize(light1);
				cue1.AddEvent(solidRegion);
			}

			// ==== Cue 2 ====
			Cue cue2 = new Cue("Cue2");
			{
				FadeInEvent fadeOutEvent = new FadeInEvent(havet6, 1000);
				cue2.AddEvent(fadeOutEvent);

				ThunderRegion solidRegion = new ThunderRegion(5);
				solidRegion.Initialize(light1);
				cue2.AddEvent(solidRegion);

				PulsingRegion solidRegion2 = new PulsingRegion(new Color(0, 0, 120), new Color(0, 100, 80), 5, 1);
				solidRegion2.Initialize(light2);
				cue2.AddEvent(solidRegion2);
			}

			// ==== Cue 3 ====
			Cue cue3 = new Cue("Cue3");
			{
				FadeOutEvent fadeOutEvent = new FadeOutEvent(havet6, 10000);
				cue3.AddEvent(fadeOutEvent);

				SolidRegion solidRegion = new SolidRegion(Color.Purple, 20);
				solidRegion.Initialize(light1);
				cue3.AddEvent(solidRegion);

				SolidRegion solidRegion2 = new SolidRegion(Color.Black, 20);
				solidRegion2.Initialize(light2);
				cue3.AddEvent(solidRegion2);
			}

			set.Cues.AddRange(new Cue[]{ cue1, cue2, cue3 });

			using var waiter = new Waiter();

			using var audioPlayer = new AudioPlayer();
			audioPlayer.PlayStartUpSound();

			using var lightPlayer = new LightPlayer(new Light[] { light1, light2 });


			while (set.IsCompleted == false)
			{
				waiter.Wait();
				Console.WriteLine($"=======CUE {set.CurrentIndex}========");
				set.TriggerNext(audioPlayer, lightPlayer);
				Thread.Sleep(1000);				
			}

			Console.WriteLine($"Finished set.\n Trigger to exit program.");
			waiter.Wait();

			Console.WriteLine("Exiting");
		}
	}
}
