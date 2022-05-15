using System;
using Dramatiker.Library.Lights;

namespace Dramatiker.Library;

public class ThunderLightEvent : PulsingLightEvent
{
	private float flashPos;
	private readonly Random rand = new Random();

	public ThunderLightEvent(Fixture fixture, float fadeIn) : base(fixture, new Color(0, 0, 110), new Color(0, 40, 20),
		fadeIn, 1.4f)
	{

	}

	public ThunderLightEvent(string[] data, Set set) : base(data, set)
	{
		// Fixture = set.Fixtures.Find(x => x.Name == data[1]);
		// FadeIn = float.Parse(data[2]);
		// Color1 = new Color(data[3]);
		// Color2 = new Color(data[4]);
		// Frequency = float.Parse(data[5]);
	}

	public override Color GetColor(float delta)
	{
		var c = base.GetColor(delta);
		if (Opacity >= 1)
		{
			if (rand.Next(0, 100) > 89)
				flashPos = 1;

			if (flashPos > 0)
			{
				flashPos -= 0.2f;

				c = Color.Lerp(c, Color.White, flashPos);
			}
		}

		return c;
	}

	public override void ApplyLight(LightPlayer lightPlayer)
	{
		Fixture?.AddRegion(this);
	}

	public new void Deserialize(string[] data, Set set)
	{
		Fixture = set.Fixtures.Find(x => x.Name == data[1]);
		FadeIn = float.Parse(data[2]);
		Color1 = new Color(data[3]);
		Color2 = new Color(data[4]);
		Frequency = float.Parse(data[5]);
	}

	public new string Serialize()
	{
		return Serializer.Serialize(this,
			Fixture != null? Fixture.Name : "NULL",
			FadeIn,
			Color1.ToHex(),
			Color2.ToHex(),
			Frequency);
	}
}