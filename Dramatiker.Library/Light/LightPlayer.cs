using System;
using System.Collections.Generic;

namespace Dramatiker.Library
{
	public class LightPlayer : IDisposable
	{
		LightController _lightController;
		public List<Light> Lights;

		public LightPlayer()
		{
			_lightController = new LightController("COM3");
		}

		public void Dispose()
		{

		}
	}
}
