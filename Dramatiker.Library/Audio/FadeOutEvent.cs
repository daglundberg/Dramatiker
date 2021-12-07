using System.Collections.Generic;
using System.ComponentModel;

namespace Dramatiker.Library
{
	public class FadeOutEvent : IAudioEvent
	{
		public FadeOutEvent(AudioItem itemToFade, int fadeLength)
		{
			ItemToFadeOut = itemToFade;
			FadeLength = fadeLength;
		}

        public FadeOutEvent()
        {

        }

		[DisplayName("Item To Fade Out")]
		public AudioItem ItemToFadeOut { get; set; }

		[DisplayName("Fade Out Length (milliseconds)")]
		public int FadeLength { get; set; }

		public void ApplyAudio(AudioPlayer audioPlayer)
		{
			audioPlayer.StopAudioItem(ItemToFadeOut, FadeLength);
		}
		public string GetDescription()
		{
			return $"{ ItemToFadeOut }\nover {decimal.Round((decimal)FadeLength / (decimal)1000, 3)} seconds";
		}

		public string GetTitle()
		{
			return "Fade Out";
		}

		public override string ToString()
		{
			return $"{GetTitle()}\n{GetDescription()}";
		}

		public void LoadFromText(string[] data, List<AudioItem> audioItems)
		{
			ItemToFadeOut = audioItems.Find(x => x.FileName == data[1]);
			FadeLength = int.Parse(data[2]);
		}

		public string TextFromObject()
		{
			return $"FadeOutEvent,{ItemToFadeOut.FileName},{FadeLength}";
		}
	}
}
