using System;
using System.Device.Gpio;
using System.Runtime.InteropServices;
using System.Threading;

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
			int _inputPin = 26;
			int _ledPin = 13;

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
					_controller.OpenPin(_inputPin, PinMode.Input);
					_controller.OpenPin(_ledPin, PinMode.Output);
				}
			}

			public Waiter(InputType inputType)
			{
				_type = inputType;

				if (_type == InputType.GPIO)
				{
					_controller = new GpioController();
					_controller.OpenPin(_inputPin, PinMode.Input);
				}
			}

			public void Wait()
			{
				if (_type == InputType.GPIO)
				{
					Console.WriteLine($"Press pedal to move forward in the set...");
					//_controller.WaitForEvent(pin, PinEventTypes.Falling, new TimeSpan(24,0,0));
					
					int count = 0;
					_controller.Write(_ledPin, PinValue.Low);

					while (count < 10)
					{
						var val = _controller.Read(_inputPin);

						if (val == PinValue.High)
						{
							count = 0;
						}
						else if (val == PinValue.Low)
						{
							count++;
							Thread.Sleep(30);
						}
					}
					_controller.Write(_ledPin, PinValue.High);
					
				}
				else if (_type == InputType.Keyboard)
				{
					Console.WriteLine($"Press any key to move forward in the set...");
					Console.ReadKey();
				}
			}

			public void Dispose()
			{
				_controller?.ClosePin(_inputPin);
				_controller?.ClosePin(_ledPin);
				_controller?.Dispose();
			}
		}
	}
}
