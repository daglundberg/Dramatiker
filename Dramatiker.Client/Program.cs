using System;
using System.Threading;
using System.Device.Gpio;
using Dramatiker.Library;
using System.IO;
using System.Runtime.InteropServices;

namespace Dramatiker.Client
{
	partial class Program
	{
		static void Main()
		{
			Console.WriteLine("Welcome to Dramatiker! Written by Dag Lundberg (c) 2021");

			Set set = new Set();
			var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Dramatiker", "Set", "set.drama");
			set.LoadFromFile(path);

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

		public class Waiter : IDisposable
		{
			public enum InputType
			{
				GPIO,
				Keyboard,
			}

			GpioController _controller;
			int pin = 26;

			private InputType _type;

			public Waiter()
			{
				if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				{
					_type = InputType.Keyboard;
				}
				else
				{
					_type = InputType.GPIO;
					_controller = new GpioController();
					_controller.OpenPin(pin, PinMode.InputPullUp);
				}
			}

			public void Wait()
			{
				if (_type == InputType.GPIO)
				{
					Console.WriteLine($"Press pedal to move forward in the set...");
					_controller.WaitForEvent(pin, PinEventTypes.Falling, new TimeSpan(24,0,0));
				}
				else if (_type == InputType.Keyboard)
				{
					Console.WriteLine($"Press any key to move forward in the set...");
					Console.ReadKey();
				}
			}

			public void Dispose()
			{
				_controller?.ClosePin(pin);
				_controller?.Dispose();
			}
		}
	}
}
