using System;
using System.Device.Gpio;
using Dramatiker.Library;
using System.IO;

namespace Dramatiker.Experiments
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine("Hello World!");
			using GpioController _controller = new GpioController();
			int pin = 26;
		
			_controller.OpenPin(pin, PinMode.Input);

			int count = 0;
			while (true)
			{
				count++;
				_controller.WaitForEvent(pin, PinEventTypes.Falling, new TimeSpan(24, 0, 0));
				Console.WriteLine($"Event {count}");
			}
		}
	}
}
