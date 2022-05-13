using System;
using System.Collections.Generic;

namespace Dramatiker.Library.Lights;

public class Fixture : ISerializable
{
	public string Name { get; set; }

	public int FirstChannel { get; set; }

	public Color Color => default;

	private List<ILightEvent> _lightRegions { get; } = new();

	public void Deserialize(string[] data, Set set)
	{
		Name = data[1];
		FirstChannel = int.Parse(data[2]);
	}

	public string Serialize()
	{
		return Serializer.Serialize(this,
			Name,
			FirstChannel);
	}

	public void AddRegion(ILightEvent lightEvent)
	{
		_lightRegions.Add(lightEvent);
		//TODO: Remove regions that are not having an effect on the output anymore.
		
		if (_lightRegions.Count > 2)
		{
			//_lightRegions.RemoveAt(0);
			_lightRegions[0].FlaggedForRemoval = true;
		}
		//TODO: I don't think the above is thread safe.
	}

	public void CleanFlaggedRegions()
	{
		// for (int i = 0; i < _lightRegions.Count; i++)
		// {
		// 	if (_lightRegions[i].FlaggedForRemoval)
		// 	{
		// 		_lightRegions.RemoveAt(i);
		// 		i--;
		// 		Console.WriteLine("Removed item");
		// 	}
		// }
	}

	public void Reset()
	{
		_lightRegions.Clear();
	}

	public Color GetColor(float delta)
	{
		Color output = default;
		
		//TODO: Collection changed exeption was thrown here!!!
		for (int i = 0; i < _lightRegions.Count; i++) 
		{ 
			var region = _lightRegions[i];
			if (region != null)
			{
				var col = region.GetColor(delta);
				output = Color.Lerp(output, col, col.A / 255f);
			}
			else
			{
				Console.WriteLine("NULL!!!");
			}
		}
			
		return output;
	}
}