using System;
using System.IO;

namespace Dramatiker.Library
{
	public class AudioItem : IEquatable<AudioItem>
	{
		public AudioItem(string fileName, bool isLooping, float volume = 1.0f)
		{			
			FileName = fileName;
			IsLooping = isLooping;
			Volume = volume;
		}

		public AudioItem() { }

		public string FileName {  get; set; }

		public bool IsLooping { get; set; }

		public float Volume { get; set; }
		public string FriendlyName
		{
			get
            {
				return Path.GetFileName(FileName);
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
			return FriendlyName;
		}
	}
}
