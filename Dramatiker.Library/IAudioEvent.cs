namespace Dramatiker.Library;

public interface IAudioEvent : IEvent
{
	AudioItem? AudioItem { get; }
	void ApplyAudio(AudioPlayer audioPlayer);
}