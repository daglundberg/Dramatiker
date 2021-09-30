using System.Collections.Generic;

namespace Dramatiker.Library
{
	public interface ISerial
	{
		void LoadFromText(string[] data, List<AudioItem> audioItems);

		string TextFromObject();
	}
}
