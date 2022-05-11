using System;
using Dramatiker.Library.Lights;

namespace Dramatiker.Library;

public class StaticLightEvent : ILightEvent
{
	public Color Color;
	public float FadeIn;
	public bool FlaggedForRemoval { get; set; } = false;

	public StaticLightEvent(Fixture fixture, Color color, float fadeIn)
	{
		Fixture = fixture;
		Color = color;
		FadeIn = fadeIn;
	}
	
	public StaticLightEvent()
	{

	}

	public float Opacity { get; private set; } = 0;
	public Fixture Fixture { get; private set; }

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
		Fixture.AddRegion(this);
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
			Fixture.Name,
			FadeIn,
			Color.ToHex());
	}
}