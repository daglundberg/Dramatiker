namespace Dramatiker.Library
{
	public interface IEvent : ISerial
	{
		string GetTitle();
		string GetDescription();
	}

	public interface IAudioEvent : IEvent
	{
		void ApplyAudio(AudioPlayer audioPlayer);
	}

	public interface ILightRegion : IEvent
	{
		void Initialize(Light light);

		Color GetColor(float delta);

		float Opacity { get; }

		void ApplyLight(LightPlayer lightPlayer);
	}
}
