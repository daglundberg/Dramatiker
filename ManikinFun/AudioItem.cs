namespace ManikinMadness
{
	partial class Program
	{
		public class AudioItem
		{
			public AudioItem(int id, string fileName, bool isLooping, float volume = 1.0f)
			{
				ID = id;
				FileName = fileName;
				IsLooping = isLooping;
				Volume = volume;
			}

			public int ID { get; private set; }
			public string FileName;
			public bool IsLooping;
			public float Volume;
		}

	}
}
