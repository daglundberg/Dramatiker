using Dramatiker.Library.Properties;

namespace Dramatiker.Library;

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
		Console.WriteLine(Resources.PressKeyToContinue, _key);
		while (Console.ReadKey().Key != _key)
		{
		}

		;
	}
}