using System;
using System.Device.Gpio;
using System.Runtime.InteropServices;

namespace Dramatiker.Client
{
	partial class Program
	{
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
					_controller.OpenPin(pin, PinMode.Input);
				}
			}

			public Waiter(InputType inputType)
			{
				_type = inputType;

				if (_type == InputType.GPIO)
				{
					_controller = new GpioController();
					_controller.OpenPin(pin, PinMode.Input);
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
