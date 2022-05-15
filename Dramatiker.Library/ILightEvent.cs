using Dramatiker.Library.Lights;

namespace Dramatiker.Library;

public interface ILightEvent : IEvent
{
	void Reset();
	float Opacity { get; }

	bool FlaggedForRemoval { get; set; }

	Fixture? Fixture { get; }
	Color GetColor(float delta);

	void ApplyLight(LightPlayer lightPlayer);
}