namespace Dramatiker.Library;

public class LocationObject
{
	public LocationObject(string? folder)
	{
		CurrentLocation = folder ?? throw new NullReferenceException();
	}

	public string CurrentLocation { get; }
}