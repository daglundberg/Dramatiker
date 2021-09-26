namespace ManikinMadness.Library
{
	public class FadeInEvent : IEvent
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

		public override string ToString()
		{
			return $"Fade in \"{ _item }\" over {decimal.Round((decimal)_fadeLength/(decimal)1000,3)} seconds";
		}
	}
}