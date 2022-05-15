using System.Device.Gpio;
using Dramatiker.Library.Properties;

namespace Dramatiker.Library;

public class GpioWaiter : IWaiter
{
	private readonly GpioController _controller;
	private readonly int _inputPin;

	public GpioWaiter(int inputPin = 26)
	{
		_inputPin = inputPin;
		_controller = new GpioController();
		_controller.OpenPin(_inputPin, PinMode.Input);
	}

	public void Wait()
	{
		Thread.Sleep(1000);
		Console.WriteLine(Resources.PressThePedal);
		//_controller.WaitForEvent(pin, PinEventTypes.Falling, new TimeSpan(24,0,0));

		var count = 0;

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