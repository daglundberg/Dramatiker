namespace ManikinMadness
{
	partial class Program
	{
		class FadeOutEvent : IEvent
		{
			public FadeOutEvent(AudioItem itemToFade, int fadeLength)
			{
				_item = itemToFade;
				_fadeLength = fadeLength;
			}

			AudioItem _item;
			int _fadeLength;

			public void ApplyEvent(AudioPlayer audioPlayer)
			{
				audioPlayer.StopAudioItem(_item, _fadeLength);
			}
		}

	}
}
