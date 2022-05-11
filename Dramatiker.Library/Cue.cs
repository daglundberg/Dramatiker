using System;
using System.Collections.Generic;

namespace Dramatiker.Library;

public class Cue : ISerializable
{
	private readonly List<IEvent> _events;

	public Cue(string name = "Nameless Cue")
	{
		_events = new List<IEvent>();
		Name = name;
	}

	public Cue(string name, IEvent[] events)
	{
		_events = new List<IEvent>(events);
		Name = name;
	}

	public Cue(IEvent[] events)
	{
		_events = new List<IEvent>(events);
		Name = "Nameless Cue";
	}

	public string Name { get; set; }

	public IEnumerable<IEvent> Events => _events;

	public void Deserialize(string[] data, Set set)
	{
		Name = data[1];
	}

	public string Serialize()
	{
		return Serializer.Serialize(this,
			Name);
	}

	public void AddEvent(IEvent e)
	{
		_events.Add(e);
	}

	public override string ToString()
	{
		return Name;
	}
}