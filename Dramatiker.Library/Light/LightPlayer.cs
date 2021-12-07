using System;
using System.Collections.Generic;
using System.Threading;

namespace Dramatiker.Library
{
	public class LightPlayer : IDisposable
	{
		LightController _lightController;
		public Light[] Lights;
		Thread _thread;
		bool CONTINUE = true;

		public LightPlayer(Light[] lights)
		{
			Lights = lights;

			_lightController = new LightController("COM3", (lights.Length*4 > 24) ? lights.Length * 4 : 24); ;
			_thread = new Thread(() => RunLights(Lights, _lightController, ref CONTINUE));
			_thread.Start();
		}

		public void PlayLight(Light light, Color color)
		{
			_lightController.SetColor(light.FirstChannel, color);
		}

		public void Flush()
		{
			_lightController.Flush();
		}

		public static void RunLights(IEnumerable<Light> lights, LightController lightController, ref bool shouldContinue)
		{
			while (shouldContinue)
			{
				Thread.Sleep(50);
				foreach (Light light in lights)
				{
					lightController.SetColor(light.FirstChannel, light.GetColor(0.1f));
				}
				lightController.Flush();
			}
		}

		public void Dispose()
		{
			CONTINUE = false;
			_thread.Join();		
			_lightController.ClearChannels();
			_lightController.Flush();
			_lightController.Close();
		}
	}
}
