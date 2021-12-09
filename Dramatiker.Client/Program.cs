using System;
using System.Threading;
using Dramatiker.Library;
using System.IO;
using Dramatiker.Library.Lights;
using Dramatiker.Library.Lights.Backends;
using System.Runtime.InteropServices;

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
			var havet3 = new AudioItem(location, "Havet 3.mp3", false);
			var havet4 = new AudioItem(location, "Havet 4.mp3", false);
			var havet5 = new AudioItem(location, "Havet 5.mp3", false);
			var havet6 = new AudioItem(location, "Havet 6.mp3", false);
			Fixture light1 = new Fixture() { FirstChannel = 0, Name = "Light 1" };
			Fixture light2 = new Fixture() { FirstChannel = 4, Name = "Light 2" };
			Fixture middle = new Fixture() { FirstChannel = 8, Name = "Light 3" };
			Fixture light4 = new Fixture() { FirstChannel = 12, Name = "Light 4" };
			Fixture light5 = new Fixture() { FirstChannel = 16, Name = "Light 5" };

			set.Cues.AddRange(new Cue[]
			{
				new Cue("Cue 0 - Pre show", new IEvent[]
				{
					new SolidRegion(light1, Color.Blue, 5),
					new SolidRegion(light2, Color.Black, 5),
					new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Black, 5),
					new SolidRegion(light5, Color.Blue, 5),
				}),

				new Cue("Cue 1 - Uwe", new IEvent[]
				{
					new SolidRegion(light1, Color.WarmWhite, 5),
					new SolidRegion(light2, Color.WarmWhite, 5),
					new SolidRegion(middle, Color.Yellow, 5),
					new SolidRegion(light4, Color.WarmWhite, 5),
					new SolidRegion(light5, Color.WarmWhite, 5),
				}),

				new Cue("Cue 2 - Fiska", new IEvent[]
				{
					new SolidRegion(light1, Color.Black, 5),
					new SolidRegion(light2, Color.PinkWhite, 5),
					new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Yellow, 5),
					new SolidRegion(light5, Color.Black, 5),
				}),

				new Cue("Cue 3 - Hav", new IEvent[]
				{
					new FadeInEvent(havet1, 500),

					new SolidRegion(light1, Color.Blue, 5),
					new SolidRegion(light2, Color.Pink, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Black, 5),
					//new SolidRegion(light5, Color.Black, 5),
				}),

				new Cue("Cue 4 - Fiska 2", new IEvent[]
				{
					new FadeOutEvent(havet1, 10000),

					new SolidRegion(light1, Color.Black, 5),
					new SolidRegion(light2, Color.PinkWhite, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Yellow, 5),
					//new SolidRegion(light5, Color.Black, 5),
				}),

				new Cue("Cue 3B - Hav", new IEvent[]
				{
					new FadeInEvent(havet2, 500),

					new SolidRegion(light1, Color.Blue, 5),
					new SolidRegion(light2, Color.Pink, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Black, 5),
					//new SolidRegion(light5, Color.Black, 5),
				}),

				new Cue("Cue 4B", new IEvent[]
				{
					new FadeOutEvent(havet2, 10000),

					new SolidRegion(light1, Color.Black, 5),
					new SolidRegion(light2, Color.PinkWhite, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Yellow, 5),
					//new SolidRegion(light5, Color.Black, 5),
				}),

				new Cue("Cue 5 - Hav 2", new IEvent[]
				{
					new FadeInEvent(havet3, 5000),

					new SolidRegion(light1, Color.Blue, 5),
					new SolidRegion(light2, Color.Pink, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Orange, 5),
					new SolidRegion(light5, Color.Blue, 5),
				}),

				new Cue("Cue 6 - Grasklipp", new IEvent[]
				{
					new FadeOutEvent(havet3, 10000),

					new SolidRegion(light1, Color.Black, 5),
					new SolidRegion(light2, Color.PinkWhite, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Yellow, 5),
					new SolidRegion(light5, Color.Black, 5),
				}),

				new Cue("Cue 7 - Lilla Gumman", new IEvent[]
				{
					new FadeInEvent(havet4, 5000),

					new SolidRegion(light1, Color.BlueWhite, 5),
					new SolidRegion(light2, Color.WarmWhite, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Red, 5),
					//new SolidRegion(light5, Color.Black, 5),
				}),

				new Cue("Cue 8 - Hav/Herrgard", new IEvent[]
				{
					new SolidRegion(light1, Color.Black, 5),
					new PulsingRegion(light2, Color.Blue, Color.Green, 5, 1.5f),
					new SolidRegion(middle, Color.Turquise, 5),
					new SolidRegion(light4, Color.Pink, 5),
					//new SolidRegion(light5, Color.Black, 5),
				}),

				new Cue("Cue 9 - Grevinnan", new IEvent[]
				{
					new FadeOutEvent(havet4, 10000),

					//new SolidRegion(light1, Color.Black, 5),
					new SolidRegion(light2, Color.WarmWhite, 5),
					new SolidRegion(middle, Color.Yellow, 5),
					new SolidRegion(light4, Color.PinkWhite, 5),
					//new SolidRegion(light5, Color.Black, 5),
				}),

				new Cue("Cue 10 - Drottn. Hav", new IEvent[]
				{
					new FadeInEvent(havet5, 5000),

					new PulsingRegion(light1, Color.Blue, Color.Green, 5, 1.5f),
					new SolidRegion(light2, Color.Black, 5),
					new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Black, 5),
					new PulsingRegion(light5, Color.Blue, Color.Green, 5, 2.2f),
				}),

				new Cue("Cue 11 - Drottn. Hav", new IEvent[]
				{
					new FadeOutEvent(havet5, 10000),

					new SolidRegion(light1, Color.Black, 5),
					//new SolidRegion(light2, Color.Black, 5),
					new SolidRegion(middle, Color.OrangeWhite, 5),
					new SolidRegion(light4, Color.Yellow, 5),
					new SolidRegion(light5, Color.Black, 5),
				}),

				new Cue("Cue 12 - Storm!", new IEvent[]
				{
					new FadeInEvent(havet6, 5000),

					new ThunderRegion(light1, 8),
					//new SolidRegion(light2, Color.Black, 5),
					new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Black, 5),
					new PulsingRegion(light5, new Color(0,0,110), new Color(0,40,20), 5, 2.2f),
				}),

				new Cue("Cue 13", new IEvent[]
				{
					new FadeOutEvent(havet6, 10000),

					new SolidRegion(light1, Color.Black, 5),
					new SolidRegion(light2, Color.PinkWhite, 5),
					new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Yellow, 5),
					new SolidRegion(light5, Color.Black, 5),
				}),

				new Cue("Cue 14", new IEvent[]
				{
					new SolidRegion(light1, Color.WarmWhite, 5),
					new SolidRegion(light2, Color.WarmWhite, 5),
					new SolidRegion(middle, Color.Yellow, 5),
					new SolidRegion(light4, Color.WarmWhite, 5),
					new SolidRegion(light5, Color.WarmWhite, 5),
				}),

				new Cue("Cue 15 - Karlek", new IEvent[]
				{
					new SolidRegion(light1, Color.Black, 5),
					new SolidRegion(light2, Color.Yellow, 5),
					new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Red, 5),
					new SolidRegion(light5, Color.Black, 5),
				}),

				new Cue("Cue 16 - Black", new IEvent[]
				{
					new SolidRegion(light1, Color.Black, 5),
					new SolidRegion(light2, Color.Black, 5),
					new SolidRegion(middle, Color.Black, 5),
					new SolidRegion(light4, Color.Black, 5),
					new SolidRegion(light5, Color.Black, 5),
				}),
			}) ;



			using var waiter = new Waiter();

			using var audioPlayer = new AudioPlayer();
			audioPlayer.PlayStartUpSound();
			var lights = new Fixture[] { light1, light2 };

			string portName;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				portName = "COM3";
			else
				portName = "/dev/ttyUSB0";

			using var lightPlayer = new LightPlayer(lights, new EntecUsbPro(portName, (lights.Length * 4 > 24) ? lights.Length * 4 : 24));

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
