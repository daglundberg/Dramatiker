namespace Dramatiker.Library.Lights;

public interface ILightEvent : IEvent
{
	float Opacity { get; }
	
	Fixture? Fixture { get; }
	
	void Reset();
	
	Color GetColor(float delta);
	
	void ApplyLight(LightPlayer lightPlayer);
}