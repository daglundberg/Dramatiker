namespace Dramatiker.Library;

public interface ISerializable
{
	void Deserialize(string[] data, Set set);

	string Serialize();
}