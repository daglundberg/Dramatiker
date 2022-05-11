using System.IO;
using System.Text;
using Dramatiker.Library.Lights;

namespace Dramatiker.Library;

public static class Serializer
{
	public const string Separator = "|";

	public static string Serialize(object type, params object[] list)
	{
		var stringBuilder = new StringBuilder();
		stringBuilder.Append(type.GetType().Name);

		for (var i = 1; i < list.Length; i++)
		{
			stringBuilder.Append(Separator);
			stringBuilder.Append(list[i]);
		}

		return stringBuilder.ToString();
	}
	
	public static Set LoadFromFile(string pathToSetFile)
	{
		var locationObject = new LocationObject(Path.GetDirectoryName(pathToSetFile));

		using var streamReader = new StreamReader(pathToSetFile);
		var builder = new StringBuilder();
		while (streamReader.EndOfStream == false) builder.AppendLine(streamReader.ReadLine());

		return DeserializeSet(builder, locationObject.CurrentLocation);
	}

	public static void SaveToFile(Set set, string pathToSetFile)
	{
		using var streamWriter = new StreamWriter(pathToSetFile);
		streamWriter.Write(SerializeSet(set));
		streamWriter.Flush();
	}

	public static StringBuilder SerializeSet(Set set)
	{
		var stringBuilder = new StringBuilder();

		foreach (var audioItem in set.AudioItems)
			stringBuilder.AppendLine(audioItem.Serialize());

		foreach (var fixture in set.Fixtures)
			stringBuilder.AppendLine(fixture.Serialize());

		foreach (var cue in set.Cues)
		{
			stringBuilder.AppendLine();
			stringBuilder.AppendLine(cue.Serialize());
			foreach (var ievent in cue.Events)
			{
				stringBuilder.Append("   ");
				stringBuilder.AppendLine(ievent is ThunderLightEvent thunderEvent
					? thunderEvent.Serialize()
					: ievent.Serialize());
			}
		}

		return stringBuilder;
	}

	public static Set DeserializeSet(StringBuilder data, string pathToSetFile)
	{
		var set = new Set();
		var locationObject = new LocationObject(Path.GetDirectoryName(pathToSetFile));

		using var stringReader = new StringReader(data.ToString());

		Cue lastCue = null;
		while (true)
		{
			var l = stringReader.ReadLine();
			if (l == null) break;
			if (string.IsNullOrWhiteSpace(l)) continue;

			var line = l.Split(Separator);
			switch (line[0].Trim())
			{
				case "AudioItem":
					var ai = new AudioItem(locationObject);
					ai.Deserialize(line, set);
					set.AudioItems.Add(ai);
					break;

				case "Fixture":
					var fi = new Fixture();
					fi.Deserialize(line, set);
					set.Fixtures.Add(fi);
					break;

				case "PlayAudioEvent":
					var pae = new PlayAudioEvent();
					pae.Deserialize(line, set);
					set.Events.Add(pae);
					lastCue?.AddEvent(pae);
					break;

				case "StopAudioEvent":
					var sae = new StopAudioEvent();
					sae.Deserialize(line, set);
					set.Events.Add(sae);
					lastCue?.AddEvent(sae);
					break;

				case "StaticLightEvent":
					var sle = new StaticLightEvent();
					sle.Deserialize(line, set);
					set.Events.Add(sle);
					lastCue?.AddEvent(sle);
					break;

				case "ThunderLightEvent":
					var tle = new ThunderLightEvent();
					tle.Deserialize(line, set);
					set.Events.Add(tle);
					lastCue?.AddEvent(tle);
					break;

				case "PulsingLightEvent":
					var ple = new StaticLightEvent();
					ple.Deserialize(line, set);
					set.Events.Add(ple);
					lastCue?.AddEvent(ple);
					break;

				case "Cue":
					var cue = new Cue();
					cue.Deserialize(line, set);
					set.Cues.Add(cue);
					lastCue = cue;
					break;
			}
		}

		return set;
	}
}