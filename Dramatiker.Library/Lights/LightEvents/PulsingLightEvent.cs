namespace Dramatiker.Library.Lights;

public class PulsingLightEvent : ILightEvent
{
	protected float _time;
	public Color Color1;
	public Color Color2;
	public float FadeIn;
	public float Frequency = 1;

	public PulsingLightEvent(Fixture fixture, Color color1, Color color2, float fadeIn, float frequency)
	{
		Color1 = color1;
		Color2 = color2;
		Frequency = frequency;
		FadeIn = fadeIn;

		Fixture = fixture;
		_time = 0;
	}

	public PulsingLightEvent(string[] data, Set set)
	{
		Fixture = set.Fixtures.Find(x => x.Name == data[1]);
		FadeIn = float.Parse(data[2]);
		Color1 = new Color(data[3]);
		Color2 = new Color(data[4]);
		Frequency = float.Parse(data[5]);
	}

	public float Opacity { get; private set; }
	public Fixture? Fixture { get; protected set; }

	public virtual Color GetColor(float delta)
	{
		_time += delta;
		if (Opacity < 1f)
		{
			Opacity += delta / FadeIn;
			if (Opacity > 1f)
				Opacity = 1f;
		}

		var val = 0.5f + MathF.Sin(MathF.PI + _time * Frequency) * .5f;

		var c = Color.Lerp(Color1, Color2, val);

		return new Color(c, (byte) (Opacity * 255));
	}

	public virtual void ApplyLight(LightPlayer lightPlayer)
	{
		Fixture?.AddRegion(this);
	}

	public void Reset()
	{
		Opacity = 0;
	}

	public void Deserialize(string[] data, Set set)
	{
		Fixture = set.Fixtures.Find(x => x.Name == data[1]);
		FadeIn = float.Parse(data[2]);
		Color1 = new Color(data[3]);
		Color2 = new Color(data[4]);
		Frequency = float.Parse(data[5]);
	}

	public string Serialize()
	{
		return Serializer.Serialize(this,
			Fixture != null ? Fixture.Name : "NULL",
			FadeIn,
			Color1.ToHex(),
			Color2.ToHex(),
			Frequency);
	}
}