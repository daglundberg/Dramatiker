using System;
using Dramatiker.Library;

namespace Dramatiker.Client
{
	partial class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Welcome to Dramatiker! Written by Dag Lundberg (c) 2021");

			AudioPlayer audioPlayer = new AudioPlayer();

			Set set = new Set();
			Console.WriteLine("Enter path to set.json file:");
			set.LoadFromFile(Console.ReadLine());

			Console.WriteLine($"Press enter to move forward in the set...");

			while (set.IsCompleted == false)
			{
				Console.ReadKey();
				set.TriggerNext(audioPlayer);
			}

			Console.WriteLine($"Finished set.\nPress enter to exit program.");
			Console.ReadKey();

			audioPlayer.Dispose();
		}
	}
}
