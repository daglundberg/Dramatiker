namespace ManikinMadness.Library
{
	public interface IEvent
	{
		void ApplyEvent(AudioPlayer audioPlayer);

		string GetTitle();
		string GetDescription();
	}
}
