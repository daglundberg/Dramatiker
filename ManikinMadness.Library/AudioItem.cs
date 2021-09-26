using System.IO;

namespace ManikinMadness.Library
{
	public class AudioItem
	{
		public AudioItem(int id, string fileName, bool isLooping, float volume = 1.0f)
		{			
			ID = id;
			FileName = fileName;
			IsLooping = isLooping;
			Volume = volume;
			FriendlyName = Path.GetFileName(FileName);
		}

		public int ID { get; private set; }
		public string FileName;
		public bool IsLooping;
		public float Volume;
		public string FriendlyName { get; private set; }

		public override string ToString()
		{
			return FriendlyName;
		}
	}


}
