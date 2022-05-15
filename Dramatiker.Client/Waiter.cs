using System;
using System.Device.Gpio;
using System.Threading;

namespace Dramatiker.Client;

public interface IWaiter : IDisposable
{
	void Wait();
}

public class ConsoleWaiter : IWaiter
{
	private readonly ConsoleKey _key;
	public ConsoleWaiter(ConsoleKey key = ConsoleKey.Enter)
	{
		_key = key;
	}
	public void Dispose()
	{
	}

	public void Wait()
	{
		Thread.Sleep(50);
		Console.WriteLine($"Press {_key} to continue...");
		while (Console.ReadKey().Key != _key){};
	}
}

public class GpioWaiter : IWaiter
{
	readonly GpioController _controller;
	readonly int _inputPin;

	public GpioWaiter(int inputPin = 26)
	{
		_inputPin = inputPin;
		_controller = new GpioController();
		_controller.OpenPin(_inputPin, PinMode.Input);
	}

	public void Wait()
	{
		Thread.Sleep(1000);
		Console.WriteLine($"Press pedal to continue...");
		//_controller.WaitForEvent(pin, PinEventTypes.Falling, new TimeSpan(24,0,0));

		int count = 0;

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
	}
	
	public void Dispose()
	{
		_controller.ClosePin(_inputPin);
		_controller.Dispose();
	}
}