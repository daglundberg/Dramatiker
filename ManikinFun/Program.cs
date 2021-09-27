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

			Set set = new Set();
			set.LoadFromFile("");

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
