using System;
using System.IO;

namespace Dramatiker.Library;

public class AudioItem : IEquatable<AudioItem>, ISerializable
{
	private readonly LocationObject _location;

	public AudioItem(LocationObject location, string fileName, bool isLooping, float volume = 1.0f)
	{
		FileName = fileName;
		IsLooping = isLooping;
		Volume = volume;
		_location = location;

		if (File.Exists(FullFilePath) == false)
			Console.WriteLine($"Could not find {FullFilePath}");
	}

	public AudioItem(LocationObject location)
	{
		_location = location;
	}

	public string FileName { get; set; }

	public bool IsLooping { get; set; }

	public float Volume { get; set; }

	public string FullFilePath => Path.Combine(_location.CurrentLocation, FileName);

	public bool Equals(AudioItem other)
	{
		return other != null &&
		       FileName == other.FileName;
	}

	public void Deserialize(string[] data, Set set)
	{
		FileName = data[1];
		IsLooping = bool.Parse(data[2]);
		Volume = float.Parse(data[3]);
	}

	public string Serialize()
	{
		return Serializer.Serialize(this,
			FileName,
			IsLooping,
			Volume);
	}

	public override bool Equals(object obj)
	{
		return Equals(obj as AudioItem);
	}

	public override int GetHashCode()
	{
		return FileName.GetHashCode();
	}

	public override string ToString()
	{
		return FileName;
	}
}