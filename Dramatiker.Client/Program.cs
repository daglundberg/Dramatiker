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

			Set set = new Set();

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
			Console.WriteLine($"Loaded: {path}");
			using var waiter = new Waiter();

			using var audioPlayer = new AudioPlayer();
			audioPlayer.PlayStartUpSound();


			while (set.IsCompleted == false)
			{
				waiter.Wait();
				Console.WriteLine($"=======EVENT {set.CurrentIndex}========");
				set.TriggerNext(audioPlayer);
				Thread.Sleep(1000);				
			}

			Console.WriteLine($"Finished set.\n Trigger to exit program.");
			waiter.Wait();

			Console.WriteLine("Exiting");
			audioPlayer.Dispose();
		}
	}
}
