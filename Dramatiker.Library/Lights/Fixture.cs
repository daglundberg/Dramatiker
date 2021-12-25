﻿using System.Collections.Generic;

namespace Dramatiker.Library.Lights
{
	public class Fixture
	{
		public string Name { get; set; }

		public int FirstChannel { get; set; }

		public Color Color
		{
			get
			{
				return default;
			}
		}

		private List<ILightRegion> _lightRegions { get; set; } = new List<ILightRegion>();

		public void AddRegion(ILightRegion lightRegion)
		{
			_lightRegions.Add(lightRegion);
			//TODO: Remove regions that are not having an effect on the output anymore.
			if (_lightRegions.Count > 2)
				_lightRegions.RemoveAt(0);
			//TODO: I don't think the above is thread safe.
		}

		public Color GetColor(float delta)
		{
			Color output = default;
			try
			{

				foreach (var region in _lightRegions)
				{
					var col = region.GetColor(delta);//TODO: Collection changed exeption was thrown here!!!
					output = Color.Lerp(output, col, (float)col.A / 255f);
				}
			}
			catch
			{
				System.Console.WriteLine("COLLECTION CHANGED!");
			}
			return output;
		}

	}
}