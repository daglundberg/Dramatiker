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

	public static void RunLights(Fixture[] fixtures, IDmxBackend lightController, ref bool shouldContinue, ref bool shouldCheck)
	{
		const int fps = 40;
		const int timeStepMs = 1000 / fps;
		const float timeStepS = timeStepMs / 1000f;
		
		while (shouldContinue)
		{
			Thread.Sleep(timeStepMs);

			if (shouldCheck)
			{
				Console.WriteLine("Checking for removes");
			
				foreach (var fixture in fixtures)
					fixture.CleanFlaggedRegions();
				shouldCheck = false;
			}

			for (int i = 0; i < fixtures.Length; i++)
			{
				lightController.SetColor(fixtures[i], fixtures[i].GetColor(timeStepS));
			}

			lightController.Flush();
		}
	}
}