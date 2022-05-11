using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Dramatiker.Library.Lights;

namespace Dramatiker.Library;

public class Set
{
	private List<Cue> _cues;

	public Set()
	{
		Events = new Register<IEvent>();
		AudioItems = new Register<AudioItem>();
		Cues = new Register<Cue>();
		Fixtures = new Register<Fixture>();

		Events.ItemAdded += OnItemAdded;
		AudioItems.ItemAdded += OnItemAdded;
		Cues.ItemAdded += OnItemAdded;
	}

	public Register<AudioItem> AudioItems { get; }
	public Register<IEvent> Events { get; }
	public Register<Cue> Cues { get; }
	public Register<Fixture> Fixtures { get; }
	public int CurrentIndex { get; private set; } = 0;

	public bool IsCompleted => CurrentIndex >= Cues.Count;

	private void OnItemAdded(object? sender, ItemAddedEventArgs e)
	{
		switch (e.Item)
		{
			case Cue cue:
				foreach (var ievent in cue.Events)
					Events.Add(ievent);
				break;

			case IAudioEvent fie:
				AudioItems.Add(fie.AudioItem);
				break;

			case ILightEvent sr:
				Fixtures.Add(sr.Fixture);
				break;
		}
	}


	public void TriggerNext(AudioPlayer audioPlayer, LightPlayer lightPlayer)
	{
		Console.WriteLine($@"{CurrentIndex + 1}: {Cues[CurrentIndex]} ({CurrentIndex + 1}/{Cues.Count})");

		foreach (var e in Cues[CurrentIndex].Events)
		{
			if (e is IAudioEvent)
				((IAudioEvent) e).ApplyAudio(audioPlayer);

			if (e is ILightEvent)
				((ILightEvent) e).ApplyLight(lightPlayer);
		}

		lightPlayer.Flush();

		CurrentIndex++;
	}

	public Cue GetNextCue()
	{
		if (CurrentIndex < Cues.Count)
			return Cues[CurrentIndex];
		return null;
	}

	public Cue GetPreviousCue()
	{
		if (CurrentIndex > 0)
			return Cues[CurrentIndex - 1];
		return null;
	}

	public void GoBack()
	{
		CurrentIndex--;
	}

	public void Restart()
	{
		CurrentIndex = 0;
	}

	public void GoForward()
	{
		CurrentIndex++;
	}
}