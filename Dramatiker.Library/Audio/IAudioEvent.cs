namespace Dramatiker.Library.Audio;

public interface IAudioEvent : IEvent
{
	AudioItem? AudioItem { get; }
	void ApplyAudio(AudioPlayer audioPlayer);
}