using System;
using System.Collections.Generic;

namespace Dramatiker.Library
{
	public class PulsingRegion : ILightRegion
	{
		public float Opacity { get; private set; }
		public Color Color1;
		public Color Color2;
		public float FadeIn;
		protected float _time = 0;
		public float Frequency = 1;
		protected Light _light;

		public PulsingRegion(Color color1, Color color2, float fadeIn, float frequency)
		{
			Color1 = color1;
			Color2 = color2;
			Frequency = frequency;
			FadeIn = fadeIn;
			Opacity = 0;
		}

		public void Initialize(Light light)
		{
			_light = light;
			Opacity = 0f;
			_time = 0;
		}

		public virtual Color GetColor(float delta)
		{
			_time += delta;
			if (Opacity < 1f)
			{
				Opacity += delta / FadeIn;
				if (Opacity > 1f)
					Opacity = 1f;
			}

			float val = 0.5f+MathF.Sin(MathF.PI + _time*Frequency)*.5f;

			var c = Color.Lerp(Color1, Color2, val);

			return new Color(c, (byte)(Opacity * 255));
		}

		public void ApplyLight(LightPlayer lightPlayer)
		{
			_light.AddRegion(this);
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
			throw new NotImplementedException();
		}

		public string TextFromObject()
		{
			throw new NotImplementedException();
		}
	}
}
