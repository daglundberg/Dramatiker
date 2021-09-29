using System;
using System.Threading;
using System.Device.Gpio;
using Dramatiker.Library;

namespace Dramatiker.Client
{
	partial class Program
	{
		static void Main()
		{
			Console.WriteLine("Welcome to Dramatiker! Written by Dag Lundberg (c) 2021");

			using var audioPlayer = new AudioPlayer();
			audioPlayer.PlayStartUpSound();

			Console.ReadLine();
			Set set = new Set();
			set.LoadFromFile("/home/pi/Downloads/set.xml");

			int pin = 26;
			using var controller = new GpioController();
			controller.OpenPin(pin, PinMode.InputPullUp);

			Console.WriteLine($"Press pedal to move forward in the set...");

			while (set.IsCompleted == false)
			{
				var x = controller.WaitForEvent(pin, PinEventTypes.Falling, new TimeSpan(24, 0, 0));
				if (x.EventTypes == PinEventTypes.Falling)
				{
					Console.WriteLine($"=======EVENT {set.CurrentIndex}========");
					set.TriggerNext(audioPlayer);
					Thread.Sleep(1000);
				}
			}

			Console.WriteLine($"Finished set.\nPress pedal to exit program.");

			controller.WaitForEvent(pin, PinEventTypes.Falling, TimeSpan.MaxValue);
			Console.WriteLine("Exiting");
			audioPlayer.Dispose();
		}
	}
}
