using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dramatiker.Library
{
	public class Cue : ISerial
	{
		public string Name { get; set; }
		private List<IEvent> _events;

		public Cue(string name = "Nameless Cue")
		{
			_events = new();
			Name = name;
		}

		public IEnumerable<IEvent> Events
		{
			get
			{
				return _events;
			}
		}

		public void AddEvent(IEvent e)
		{
			_events.Add(e);
		}

		public void LoadFromText(string[] data, List<AudioItem> audioItems)
		{
			throw new NotImplementedException();
		}

		public string TextFromObject()
		{
			throw new NotImplementedException();
		}

		public string GetDescription()
		{
			var stringBuilder = new StringBuilder();

			foreach (var e in Events)
			{
				stringBuilder.Append(e.GetDescription());
			}
			return stringBuilder.ToString();
		}

		public string GetTitle()
		{
			return Name;
		}

		public override string ToString()
		{
			return $"{GetTitle()}\n{GetDescription()}";
		}
	}
}
