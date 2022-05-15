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
	public Fixture[] Lights;

	public LightPlayer(IEnumerable<Fixture> lights, IDmxBackend dmxBackend)
	{
		Lights = lights.ToArray();

		_dmxBackend = dmxBackend;
	//if (_dmxBackend != null)
		_thread = new Thread(() => RunLights(Lights, _dmxBackend, ref CONTINUE));
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

	public static void RunLights(Fixture[] fixtures, IDmxBackend lightController, ref bool shouldContinue)
	{
		const int fps = 40;
		const int timeStepMs = 1000 / fps;
		const float timeStepS = timeStepMs / 1000f;
		
		while (shouldContinue)
		{
			Thread.Sleep(timeStepMs);

			for (int i = 0; i < fixtures.Length; i++)
			{
				lock (fixtures[i])
				{
					lightController.SetColor(fixtures[i], fixtures[i].GetColor(timeStepS));
				}
			}

			lightController.Flush();
		}
	}
}