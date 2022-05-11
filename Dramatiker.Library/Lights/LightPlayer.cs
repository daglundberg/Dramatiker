using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Dramatiker.Library.Lights.Backends;

namespace Dramatiker.Library.Lights;

public class LightPlayer : IDisposable
{
	private readonly IDmxBackend _dmxBackend;
	private readonly Thread _thread;
	private bool CONTINUE = true;
	public bool SHOULDCHECK = false;
	public Fixture[] Lights;

	public LightPlayer(IEnumerable<Fixture> lights, IDmxBackend dmxBackend)
	{
		Lights = lights.ToArray();

		_dmxBackend = dmxBackend;

		_thread = new Thread(() => RunLights(Lights, _dmxBackend, ref CONTINUE, ref SHOULDCHECK));
		_thread.Start();
	}

	public void Dispose()
	{
		CONTINUE = false;
		_thread.Join();
		_dmxBackend.ClearChannels();
		_dmxBackend.Flush();
		_dmxBackend.Close();
	}

	public void Flush()
	{
		_dmxBackend.Flush();
	}

	public static void RunLights(IEnumerable<Fixture> lights, IDmxBackend lightController, ref bool shouldContinue, ref bool shouldCheck)
	{
		while (shouldContinue)
		{
			Thread.Sleep(25);

			if (shouldCheck)
			{
				Console.WriteLine("Checking for removes");

				foreach (var light in lights)
					light.CleanFlaggedRegions();
				shouldCheck = false;
			}
			
			foreach (var light in lights)
				lightController.SetColor(light, light.GetColor(0.1f));
			lightController.Flush();
		}
	}
}