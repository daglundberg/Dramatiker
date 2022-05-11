namespace Dramatiker.Library;

public class LocationObject
{
	public LocationObject(string folder)
	{
		CurrentLocation = folder;
	}

	public string CurrentLocation { get; }
}