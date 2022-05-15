namespace Dramatiker.Library.Lights;

public class StaticLightEvent : ILightEvent
{
	public Color Color;
	public float FadeIn;

	public StaticLightEvent(Fixture fixture, Color color, float fadeIn)
	{
		Fixture = fixture;
		Color = color;
		FadeIn = fadeIn;
	}

	public StaticLightEvent(string[] data, Set set)
	{
		Fixture = set.Fixtures.Find(x => x.Name == data[1]);
		FadeIn = float.Parse(data[2]);
		Color = new Color(data[3]);
	}
	

	public void Reset()
	{
		Opacity = 0;
	}

	public float Opacity { get; private set; }
	public Fixture? Fixture { get; private set; }

	public Color GetColor(float delta)
	{
		if (Opacity < 1f)
		{
			Opacity += delta / FadeIn;
			if (Opacity > 1f)
				Opacity = 1f;
		}

		return new Color(Color, (byte) (Opacity * 255));
	}

	public void ApplyLight(LightPlayer lightPlayer)
	{
		Fixture?.AddRegion(this);
	}

	public void Deserialize(string[] data, Set set)
	{
		Fixture = set.Fixtures.Find(x => x.Name == data[1]);
		FadeIn = float.Parse(data[2]);
		Color = new Color(data[3]);
	}

	public string Serialize()
	{
		return Serializer.Serialize(this,
			Fixture != null ? Fixture.Name : "NULL",
			FadeIn,
			Color.ToHex());
	}
}