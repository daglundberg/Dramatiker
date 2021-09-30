namespace Dramatiker.Library
{
	public interface IEvent : ISerial
	{
		void ApplyEvent(AudioPlayer audioPlayer);

		string GetTitle();
		string GetDescription();
	}
}
