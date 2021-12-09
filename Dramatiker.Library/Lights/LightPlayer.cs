using Dramatiker.Library.Lights.Backends;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace Dramatiker.Library.Lights
{
	public class LightPlayer : IDisposable
	{
		IDmxBackend _dmxBackend;
		public Fixture[] Lights;
		Thread _thread;
		bool CONTINUE = true;

		public LightPlayer(Fixture[] lights, IDmxBackend dmxBackend)
		{
			Lights = lights;

			_dmxBackend = dmxBackend;

			_thread = new Thread(() => RunLights(Lights, _dmxBackend, ref CONTINUE));
			_thread.Start();
		}

		public void Flush()
		{
			_dmxBackend.Flush();
		}

		public static void RunLights(IEnumerable<Fixture> lights, IDmxBackend lightController, ref bool shouldContinue)
		{
			while (shouldContinue)
			{
				Thread.Sleep(50);
				foreach (Fixture light in lights)
				{
					lightController.SetColor(light, light.GetColor(0.1f));
				}
				lightController.Flush();
			}
		}

		public void Dispose()
		{
			CONTINUE = false;
			_thread.Join();		
			_dmxBackend.ClearChannels();
			_dmxBackend.Flush();
			_dmxBackend.Close();
		}
	}
}
