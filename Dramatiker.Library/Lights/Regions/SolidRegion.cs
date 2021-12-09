using Dramatiker.Library.Lights;
using System.Collections.Generic;

namespace Dramatiker.Library
{
	public class SolidRegion : ILightRegion
	{
		public float Opacity { get; private set; }
		public Color Color;
		public float FadeIn;
		private Fixture _fixture;

		public SolidRegion(Fixture fixture, Color color, float fadeIn)
		{
			_fixture = fixture;
			Color = color;
			FadeIn = fadeIn;
			Opacity = 0;
		}

		public Color GetColor(float delta)
		{
			if (Opacity < 1f)
			{
				Opacity += delta / FadeIn;
				if (Opacity > 1f)
					Opacity = 1f;
			}

			return new Color(Color, (byte)(Opacity * 255));
		}

		public void ApplyLight(LightPlayer lightPlayer)
		{
			_fixture.AddRegion(this);
		}

		public string GetTitle()
		{
			return ToString();
		}

		public string GetDescription()
		{
			return ToString();
		}

		public void LoadFromText(string[] data, List<AudioItem> audioItems)
		{
			throw new System.NotImplementedException();
		}

		public string TextFromObject()
		{
			throw new System.NotImplementedException();
		}
	}
}
