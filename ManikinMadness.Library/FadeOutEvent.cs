﻿using System.ComponentModel;

namespace ManikinMadness.Library
{
	public class FadeOutEvent : IEvent
	{
		public FadeOutEvent(AudioItem itemToFade, int fadeLength)
		{
			ItemToFadeOut = itemToFade;
			FadeLength = fadeLength;
		}

		[DisplayName("Item To Fade Out")]
		public AudioItem ItemToFadeOut { get; set; }

		[DisplayName("Fade Out Length (seconds)")]
		public int FadeLength { get; set; }

		public void ApplyEvent(AudioPlayer audioPlayer)
		{
			audioPlayer.StopAudioItem(ItemToFadeOut, FadeLength);
		}
		public string GetDescription()
		{
			return $"{ ItemToFadeOut }\nover {decimal.Round((decimal)FadeLength / (decimal)1000, 3)} seconds";
		}

		public string GetTitle()
		{
			return "Fade Out";
		}

		public override string ToString()
		{
			return $"{GetTitle()}\n{GetDescription()}";
		}
	}
}
