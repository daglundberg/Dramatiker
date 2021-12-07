using System.Collections.Generic;
using System.ComponentModel;

namespace Dramatiker.Library
{
	public class FadeInEvent : IAudioEvent, ISerial
	{
		public FadeInEvent(AudioItem itemToFade, int fadeLength)
		{
			ItemToFadeIn = itemToFade;
			FadeLength = fadeLength;
		}

		public FadeInEvent() { }

		[DisplayName("Item To Fade In")]
		public AudioItem ItemToFadeIn { get; set; }

		[DisplayName("Fade In Length (milliseconds)")]
		public int FadeLength { get; set; }

		public void ApplyAudio(AudioPlayer audioPlayer)
		{
			audioPlayer.PlayAudioItem(ItemToFadeIn, FadeLength);
		}

		public string GetDescription()
		{
			return $"{ ItemToFadeIn }\nover {decimal.Round((decimal)FadeLength / (decimal)1000, 3)} seconds";
		}

		public string GetTitle()
		{
			return "Fade In";
		}

		public override string ToString()
		{
			return $"{GetTitle()}\n{GetDescription()}";
		}

		public void LoadFromText(string[] data, List<AudioItem> audioItems)
		{
			ItemToFadeIn = audioItems.Find(x => x.FileName == data[1]);
			FadeLength = int.Parse(data[2]);
		}

		public string TextFromObject()
		{
			return $"FadeInEvent,{ItemToFadeIn.FileName},{FadeLength}";
		}
	}
}