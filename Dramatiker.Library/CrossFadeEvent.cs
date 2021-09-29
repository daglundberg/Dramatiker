using System.ComponentModel;

namespace Dramatiker.Library
{
	public class CrossFadeEvent : IEvent
	{
		public CrossFadeEvent(AudioItem itemToFadeOut, AudioItem itemToFadeIn, int fadeLength)
		{
			ItemToFadeIn = itemToFadeIn;
			ItemToFadeOut = itemToFadeOut;
			FadeLength = fadeLength;
		}

        public CrossFadeEvent()
        {

        }

		[DisplayName("Item To Fade In")]
		public AudioItem ItemToFadeIn { get; set; }

		[DisplayName("Item To Fade Out")]
		public AudioItem ItemToFadeOut { get; set; }

		[DisplayName("Cross-Fade Length (milliseconds)")]
		public int FadeLength { get; set; }

		public void ApplyEvent(AudioPlayer audioPlayer)
		{
			var ine = new FadeInEvent(ItemToFadeIn, FadeLength);
			var oute = new FadeOutEvent(ItemToFadeOut, FadeLength);

			ine.ApplyEvent(audioPlayer);
			oute.ApplyEvent(audioPlayer);
		}

        public string GetDescription()
        {
			return $"From \"{ ItemToFadeOut }\"\nto \"{ ItemToFadeIn }\"\nover {decimal.Round((decimal)FadeLength / (decimal)1000, 3)} seconds";
        }

        public string GetTitle()
        {
			return "Cross-Fade";
        }

        public override string ToString()
		{
			return $"{GetTitle()}\n{GetDescription()}";
		}
	}
}
