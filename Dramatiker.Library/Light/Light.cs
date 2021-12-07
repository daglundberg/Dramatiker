using System.Collections.Generic;

namespace Dramatiker.Library
{
	public class Light
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
		}

		public Color GetColor(float delta)
		{
			Color output = default;
			foreach (var region in _lightRegions)
			{
				var col = region.GetColor(delta);
				output = Color.Lerp(output, col, (float)col.A / 255f);
			}
			return output;
		}

	}
}
