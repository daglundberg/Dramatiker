namespace ManikinMadness
{
	partial class Program
	{
		class FadeInEvent : IEvent
		{
			public FadeInEvent(AudioItem itemToFade, int fadeLength)
			{
				_item = itemToFade;
				_fadeLength = fadeLength;
			}

			AudioItem _item;
			int _fadeLength;

			public void ApplyEvent(AudioPlayer audioPlayer)
			{
				audioPlayer.PlayAudioItem(_item, _fadeLength);
			}
		}

		class CrossFadeEvent : IEvent
		{
			public CrossFadeEvent(AudioItem itemToFadeOut, AudioItem itemToFadeIn, int fadeLength)
			{
				_fadeIn = itemToFadeIn;
				_fadeOut = itemToFadeOut;
				_fadeLength = fadeLength;
			}

			AudioItem _fadeIn, _fadeOut;
			int _fadeLength;

			public void ApplyEvent(AudioPlayer audioPlayer)
			{
				var ine = new FadeInEvent(_fadeIn, _fadeLength);
				var oute = new FadeOutEvent(_fadeOut, _fadeLength);

				ine.ApplyEvent(audioPlayer);
				oute.ApplyEvent(audioPlayer);
			}
		}
	}
}
