namespace Dramatiker.Library.Lights;

public class Fixture : ISerializable
{
	private readonly List<ILightEvent> _lightRegions = new();

	public Fixture(string name)
	{
		Name = name;
	}

	public Fixture(string[] data, Set set)
	{
		Name = data[1];
		FirstChannel = int.Parse(data[2]);
	}

	public string Name { get; private set; }

	public int FirstChannel { get; private set; }

	public Color Color => default;

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
		lock (this)
		{
			_lightRegions.Add(lightEvent);

			//TODO: This removes regions that might still have an effect on the output.
			if (_lightRegions.Count > 2)
				_lightRegions.RemoveAt(0);
		}
	}

	public void Reset()
	{
		_lightRegions.Clear();
	}

	public Color GetColor(float delta)
	{
		Color output = default;

		for (var i = _lightRegions.Count - 1; i > 0; i--)
		{
			var region = _lightRegions[i];

			var col = region.GetColor(delta);
			output = Color.Lerp(output, col, col.A / 255f);

			if (output.A == 255)
				return output;
		}

		return output;
	}
}