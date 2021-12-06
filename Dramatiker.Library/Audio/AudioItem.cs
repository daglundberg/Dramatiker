using System;
using System.Collections.Generic;
using System.IO;

namespace Dramatiker.Library
{
	public class LocationObject
	{
		public LocationObject(string folder)
		{
			CurrentLocation = folder;
		}

		public string CurrentLocation
		{
			get; private set;
		}
	}

	public class AudioItem : IEquatable<AudioItem>, ISerial
	{
		public AudioItem(LocationObject location, string fileName, bool isLooping, float volume = 1.0f)
		{			
			FileName = fileName;
			IsLooping = isLooping;
			Volume = volume;
			_location = location;
		}

		public AudioItem(LocationObject location)
		{
			_location = location;
		}

		private LocationObject _location;

		public string FileName {  get; set; }

		public bool IsLooping { get; set; }

		public float Volume { get; set; }

		public string FullFilePath
		{
			get
			{
				return Path.Combine(_location.CurrentLocation, FileName);
			}
		}

		public override bool Equals(object obj)
		{
			return Equals(obj as AudioItem);
		}

		public bool Equals(AudioItem other)
        {
			return other != null &&
				FileName == other.FileName;
		}

		public override int GetHashCode()
		{
			return FileName.GetHashCode();
		}

		public override string ToString()
		{
			return FileName;
		}

		public void LoadFromText(string[] data, List<AudioItem> audioItems)
		{
			FileName = data[1];
			IsLooping = bool.Parse(data[2]);
			Volume = float.Parse(data[3]);
		}

		public string TextFromObject()
		{
			return $"AudioItem,{FileName},{IsLooping},{Volume}";
		}
	}
}
