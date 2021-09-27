using Polenter.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Xml.Serialization;

namespace ManikinMadness.Library
{
	public class Set
	{
		public string Name { get; set; }

		[DataMember]
		public List<IEvent> Events { get; set; }
		public int CurrentIndex { get; private set; }

		public Set()
		{
			CurrentIndex = 0;
			Events = new List<IEvent>();
		}

		public void LoadFromFile(string pathToSetFile)
		{
			var serializer = new SharpSerializer();

			Events = (List<IEvent>)serializer.Deserialize(pathToSetFile);

			/*			StreamReader streamReader= new StreamReader(pathToSetFile);
						string data = streamReader.ReadToEnd();

						XmlSerializer xmlSerializer = new XmlSerializer(typeof(object), new Type[] { typeof(FadeInEvent), typeof(FadeOutEvent), typeof(CrossFadeEvent) });

						var s = xmlSerializer.Deserialize(streamReader);

						var result = JsonSerializer.Deserialize<List<object>>(data);

						foreach (object item in result)
						{
							if (item.GetType() == typeof(FadeInEvent))
							{
								Events.Add((FadeInEvent)item);
							}
							else if (item.GetType() == typeof(FadeOutEvent))
							{
								Events.Add((FadeOutEvent)item);
							}
						}*/

			//Events = JsonSerializer.Deserialize<List<object>>(data);

			CurrentIndex = 0;
		}

		public void SaveToFile(string pathToSetFile)
        {
			//using StreamWriter streamWriter = new StreamWriter(pathToSetFile);

			var serializer = new SharpSerializer();
			serializer.Serialize(Events, pathToSetFile);

/*			XmlSerializer xmlSerializer = new XmlSerializer(typeof(object), new Type[] { typeof(FadeInEvent), typeof(FadeOutEvent), typeof(CrossFadeEvent) });
			//xmlSerializer.Serialize(Events, typeof(List<IEvent>));

			foreach (IEvent e in Events)
            {
				if (e.GetType() == typeof(FadeInEvent))
                {
					xmlSerializer.Serialize(streamWriter, (FadeInEvent)e);
				}
				else if (e.GetType() == typeof(FadeOutEvent))
                {
					xmlSerializer.Serialize(streamWriter, (FadeOutEvent)e);
				}
				else if (e.GetType() == typeof(CrossFadeEvent))
                {
					xmlSerializer.Serialize(streamWriter, (CrossFadeEvent)e);
				}
			}*/

			//streamWriter.Flush();
		}

		public void TriggerNext(AudioPlayer audioPlayer)
		{
			Console.WriteLine($"{CurrentIndex + 1} / {Events.Count}: {Events[CurrentIndex]}");

			Events[CurrentIndex].ApplyEvent(audioPlayer);
			CurrentIndex++;
		}

		public IEvent GetNextEvent()
        {
			if (CurrentIndex < Events.Count)
				return Events[CurrentIndex];
			else
				return null;
        }

		public IEvent GetPreviousEvent()
        {
			if (CurrentIndex > 0)
				return Events[CurrentIndex - 1];
			else
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

		public bool IsCompleted
		{
			get
			{
				return CurrentIndex >= Events.Count;
			}
		}
	}
}
