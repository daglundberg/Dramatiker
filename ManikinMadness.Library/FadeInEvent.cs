using System.ComponentModel;

namespace ManikinMadness.Library
{
	public class FadeInEvent : IEvent
	{
		public FadeInEvent(AudioItem itemToFade, int fadeLength)
		{
			ItemToFadeIn = itemToFade;
			FadeLength = fadeLength;
		}

		[DisplayName("Item To Fade In")]
		public AudioItem ItemToFadeIn { get; set; }

		[DisplayName("Fade In Length (seconds)")]
		public int FadeLength { get; set; }

		public void ApplyEvent(AudioPlayer audioPlayer)
		{
			audioPlayer.PlayAudioItem(ItemToFadeIn, FadeLength);
		}
		public string GetDescription()
		{
			return $"{ ItemToFadeIn }\nover {decimal.Round((decimal)FadeLength / (decimal)1000, 3)} seconds";
		}

		public string GetTitle()
		{
			return "Fade In";
		}

		public override string ToString()
		{
			return $"{GetTitle()}\n{GetDescription()}";
		}
	}
}