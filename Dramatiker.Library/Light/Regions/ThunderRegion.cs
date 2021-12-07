﻿using System;
using System.Collections.Generic;

namespace Dramatiker.Library
{
	public class ThunderRegion : PulsingRegion
	{
		private float flashPos = 0;
		private Random rand;

		public ThunderRegion(float fadeIn) : base(new Color(0,0,110), new Color(0,40,20), fadeIn, 1.4f)
		{
			rand = new Random();
		}

		public new void Initialize(Light light)
		{
			base.Initialize(light);
		}

		public override Color GetColor(float delta)
		{
			var c = base.GetColor(delta);
			if (Opacity == 1)
			{
				if (rand.Next(0, 100) > 97)
					flashPos = 1;

				if (flashPos > 0)
				{
					flashPos -= 0.2f;

					c = Color.Lerp(c, Color.White, flashPos);
				}
			}

			return c;
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