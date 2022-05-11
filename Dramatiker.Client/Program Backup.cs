/*
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
		static void Main(string[] args)
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
						Console.WriteLine($"Loaded: {path}");#1#

			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Dramatiker", "Set");
			path = Directory.GetCurrentDirectory();
			LocationObject location = new LocationObject(path);
			Set set = Serializer.LoadFromFile("mysetfile.drama");

			/*var havet1 = new AudioItem(location, "Havet 1.mp3", false);
			var havet2 = new AudioItem(location, "Havet 2.mp3", false);
			var havet3 = new AudioItem(location, "Havet 3.mp3", false);
			var havet4 = new AudioItem(location, "Havet 4.mp3", false);
			var havet5 = new AudioItem(location, "Havet 5.mp3", false);
			var havet6 = new AudioItem(location, "Havet 6.mp3", false);
			Fixture light1 = new Fixture() { FirstChannel = 0, Name = "Hav 1" };
			Fixture light5 = new Fixture() { FirstChannel = 16, Name = "Hav 2" };

			Fixture light2 = new Fixture() { FirstChannel = 4, Name = "Light 2" };
			Fixture middle = new Fixture() { FirstChannel = 8, Name = "Light 3" };
			Fixture light4 = new Fixture() { FirstChannel = 12, Name = "Light 4" };
			Fixture light6 = new Fixture() { FirstChannel = 20, Name = "Light 6" };

			set.Cues.AddRange(new Cue[]
			{
				new Cue("Pre show", new IEvent[]
				{
					new StaticLightEvent(light1, Color.Blue, 5),
					new StaticLightEvent(light2, Color.Blue, 5),
					new StaticLightEvent(middle, Color.Blue, 5),
					new StaticLightEvent(light4, Color.Blue, 5),
					new StaticLightEvent(light5, Color.Blue, 5),
					new StaticLightEvent(light6, Color.Blue, 5),
				}),

				new Cue("Uwe", new IEvent[]
				{
					new StaticLightEvent(light1, Color.WarmWhite, 5),
					new StaticLightEvent(light2, Color.WarmWhite, 5),
					new StaticLightEvent(middle, Color.Yellow, 5),
					new StaticLightEvent(light4, Color.WarmWhite, 5),
					new StaticLightEvent(light5, Color.WarmWhite, 5),
					new StaticLightEvent(light6, Color.Yellow, 5),

				}),

				new Cue("Fiska", new IEvent[]
				{
					new StaticLightEvent(light1, Color.Black, 5),
					new StaticLightEvent(light2, Color.PinkWhite, 5),
					new StaticLightEvent(middle, Color.Black, 5),
					new StaticLightEvent(light4, Color.Yellow, 5),
					new StaticLightEvent(light5, Color.Black, 5),
					new StaticLightEvent(light6, Color.Black, 5),

				}),

				new Cue("Hav", new IEvent[]
				{
					new PlayAudioEvent(havet1, 500),

					new StaticLightEvent(light1, Color.Blue, 5),
					new StaticLightEvent(light2, Color.Pink, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new StaticLightEvent(light4, Color.Pink, 5),
					new StaticLightEvent(light5, Color.Blue, 5),
				}),

				new Cue("Fiska 2", new IEvent[]
				{
					new StopAudioEvent(havet1, 10000),

					new StaticLightEvent(light1, Color.Black, 5),
					new StaticLightEvent(light2, Color.PinkWhite, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new StaticLightEvent(light4, Color.Yellow, 5),
					new StaticLightEvent(light5, Color.Black, 5),
				}),

				new Cue("Hav (Tvattmaskin)", new IEvent[]
				{
					new PlayAudioEvent(havet2, 500),

					new StaticLightEvent(light1, Color.Blue, 5),
					new StaticLightEvent(light2, Color.Pink, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new StaticLightEvent(light4, Color.Pink, 5),
					new StaticLightEvent(light5, Color.Blue, 5),
				}),

				new Cue("Tvättmaskin", new IEvent[]
				{
					new StopAudioEvent(havet2, 10000),

					new StaticLightEvent(light1, Color.Black, 5),
					new StaticLightEvent(light2, Color.PinkWhite, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new StaticLightEvent(light4, Color.Yellow, 5),
					//new SolidRegion(light5, Color.Black, 5),
				}),

				new Cue("Hav (Huset)", new IEvent[]
				{
					new PlayAudioEvent(havet3, 5000),

					new StaticLightEvent(light1, Color.Blue, 5),
					new StaticLightEvent(light2, Color.Pink, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new StaticLightEvent(light4, Color.Orange, 5),
					new StaticLightEvent(light5, Color.Blue, 5),
				}),

				new Cue("Gräsklipp", new IEvent[]
				{
					new StopAudioEvent(havet3, 10000),

					new StaticLightEvent(light1, Color.Black, 5),
					new StaticLightEvent(light2, Color.PinkWhite, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new StaticLightEvent(light4, Color.Yellow, 5),
					new StaticLightEvent(light5, Color.Black, 5),
				}),

				new Cue("Hav (Lilla Gumman)", new IEvent[]
				{
					new PlayAudioEvent(havet4, 5000),

					new StaticLightEvent(light1, Color.BlueWhite, 5),
					new StaticLightEvent(light2, Color.WarmWhite, 5),
					//new SolidRegion(middle, Color.Black, 5),
					new PulsingLightEvent(light4, Color.Red, Color.Turquise, 5, 1.0f),
					new PulsingLightEvent(light5, Color.Blue, Color.Green, 5, 2.2f),
				}),

				new Cue("Grevinnan", new IEvent[]
				{
					new StopAudioEvent(havet4, 10000),

					//new SolidRegion(light1, Color.Black, 5),
					new StaticLightEvent(light2, Color.WarmWhite, 5),
					new StaticLightEvent(middle, Color.Yellow, 5),
					new StaticLightEvent(light4, Color.PinkWhite, 5),
					new StaticLightEvent(light5, Color.Black, 5),
				}),

				new Cue("Hav (Drottning)", new IEvent[]
				{
					new PlayAudioEvent(havet5, 5000),

					new PulsingLightEvent(light1, Color.Blue, Color.Green, 5, 1.5f),
					new StaticLightEvent(light2, Color.Black, 5),
					new StaticLightEvent(middle, Color.Black, 5),
					new StaticLightEvent(light4, Color.Black, 5),
					new PulsingLightEvent(light5, Color.Blue, Color.Green, 5, 2.2f),
				}),

				new Cue("Drottning", new IEvent[]
				{
					new StopAudioEvent(havet5, 10000),

					new StaticLightEvent(light1, Color.Black, 5),
					//new SolidRegion(light2, Color.Black, 5),
					new StaticLightEvent(middle, Color.White, 5),
					new StaticLightEvent(light4, Color.Yellow, 5),
					new StaticLightEvent(light5, Color.Black, 5),
					new StaticLightEvent(light6, Color.Yellow, 5),
				}),

				new Cue("Storm!", new IEvent[]
				{
					new PlayAudioEvent(havet6, 5000),

					new ThunderLightEvent(light1, 8),
					new PulsingLightEvent(light2, Color.Red, Color.Black, 5, .15f),
					new PulsingLightEvent(middle, new Color(0,0,110), new Color(0,40,20), 5, 2.2f),
					new PulsingLightEvent(light4, Color.Black, Color.Blue, 5, .08f),
					new ThunderLightEvent(light5, 8),
					new ThunderLightEvent(light6, 8),
				}),

				new Cue("Cue 15", new IEvent[]
				{
					new StopAudioEvent(havet6, 10000),

					new StaticLightEvent(light1, Color.Black, 5),
					new StaticLightEvent(light2, Color.PinkWhite, 5),
					new StaticLightEvent(middle, Color.Black, 5),
					new StaticLightEvent(light4, Color.Yellow, 5),
					new StaticLightEvent(light5, Color.Black, 5),
					new StaticLightEvent(light6, Color.Black, 5),
				}),

				new Cue("Cue 16", new IEvent[]
				{
					new StaticLightEvent(light1, Color.WarmWhite, 5),
					new StaticLightEvent(light2, Color.WarmWhite, 5),
					new StaticLightEvent(middle, Color.Yellow, 5),
					new StaticLightEvent(light4, Color.WarmWhite, 5),
					new StaticLightEvent(light5, Color.WarmWhite, 5),
				}),

				new Cue("Kärlek", new IEvent[]
				{
					new StaticLightEvent(light1, Color.Black, 5),
					new StaticLightEvent(light2, Color.Yellow, 5),
					new StaticLightEvent(middle, Color.Black, 5),
					new StaticLightEvent(light4, Color.Red, 5),
					new StaticLightEvent(light5, Color.Black, 5),
				}),

				new Cue("Applåder", new IEvent[]
				{
					new StaticLightEvent(light1, Color.WarmWhite, 5),
					new StaticLightEvent(light2, Color.WarmWhite, 5),
					new StaticLightEvent(middle, Color.WarmWhite, 5),
					new StaticLightEvent(light4, Color.WarmWhite, 5),
					new StaticLightEvent(light5, Color.WarmWhite, 5),
					new StaticLightEvent(light6, Color.WarmWhite, 5),
				}),

				new Cue("Black out", new IEvent[]
				{
					new StaticLightEvent(light1, Color.Black, 5),
					new StaticLightEvent(light2, Color.Black, 5),
					new StaticLightEvent(middle, Color.Black, 5),
					new StaticLightEvent(light4, Color.Black, 5),
					new StaticLightEvent(light5, Color.Black, 5),
					new StaticLightEvent(light6, Color.Black, 5),
				}),
			}) ;

			var z = set.Serialize();
			Console.Write(z.ToString());#1#
			//set.SaveToFile("mysetfile.drama");

			//return;
			
			using var waiter = new Waiter(Waiter.InputType.Keyboard);

			int config = -1;
			if (args != null)
				if (args.Length > 0)
					config = int.Parse(args[0]);
			
			using var audioPlayer = new AudioPlayer(config);
			audioPlayer.PlayStartUpSound();
			//var lights = new Fixture[] { light1, light2, middle, light4, light5, light6 };

			string portName;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				portName = "COM3";
			else
				portName = "/dev/ttyUSB0";

		//	portName = "/dev/tty.usbserial-AB0PEUM4";

			using var lightPlayer = new LightPlayer(set.Fixtures, new EntecUsbPro(portName, 512));

			while (set.IsCompleted == false)
			{
				waiter.Wait();
				set.TriggerNext(audioPlayer, lightPlayer);
				Thread.Sleep(1000);
			}

			Console.WriteLine($"Finished set.\n Trigger to exit program.");
			waiter.Wait();
			Console.WriteLine("Exiting...");
		}
	}
}
*/
