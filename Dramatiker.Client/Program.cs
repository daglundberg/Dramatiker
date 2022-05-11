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
		static int Main(string[] args)
		{
			Console.WriteLine("Welcome to Dramatiker! Written by Dag Lundberg (c) 2021");

			string[] paths = 
			{
				"/media/DRAMATIKER/",
				"/media/dramatiker/",
				Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Dramatiker", "Set"),
				Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Dramatiker"),
				Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
				Directory.GetCurrentDirectory()
			};
			
			Set set = null;
			foreach (var path in paths)
				if (Directory.Exists(path))
				{
					string[] files = Directory.GetFiles(path, "*.drama");
					if (files.Length > 0)
					{
						set = Serializer.LoadFromFile(files[0]);
						Console.WriteLine($"Loaded: {files[0]}");
						break;
					}
				}

			if (set == null)
			{
				Console.WriteLine("No set.drama file found. Exiting...");
				return 1;
			}

			using var waiter = new Waiter(Waiter.InputType.Keyboard);

			int config = -1;
			if (args != null)
				if (args.Length > 0)
					config = int.Parse(args[0]);
			
			using var audioPlayer = new AudioPlayer(config);
			audioPlayer.PlayStartUpSound();

			string portName;
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				portName = "COM3";
			else
				portName = "/dev/ttyUSB0";

			using var lightPlayer = new LightPlayer(set.Fixtures, new EntecUsbPro(portName, 512));

			while (set.IsCompleted == false)
			{
				waiter.Wait();
				set.TriggerNext(audioPlayer, lightPlayer);
				lightPlayer.SHOULDCHECK = true;
			}

			Console.WriteLine($"Finished set.\n Trigger to exit program.");
			waiter.Wait();
			Console.WriteLine("Exiting...");

			return 0;
		}
	}
}
