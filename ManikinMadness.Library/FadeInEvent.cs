using System.Collections.Generic;
using System.ComponentModel;

namespace ManikinMadness.Library
{
	public class FadeInEvent : IEvent
	{
		public FadeInEvent(AudioItem itemToFade, int fadeLength)
		{
			ItemToFadeIn = itemToFade;
			FadeLength = fadeLength;
		}

		[DisplayName("Item To Fade In")]
		public AudioItem ItemToFadeIn { get; set; }
		public int FadeLength { get; set; }
		public Foo Foo { get; set; }

		public void ApplyEvent(AudioPlayer audioPlayer)
		{
			audioPlayer.PlayAudioItem(ItemToFadeIn, FadeLength);
		}

		public override string ToString()
		{
			return $"Fade in \"{ ItemToFadeIn }\" {decimal.Round((decimal)FadeLength/(decimal)1000,3)} seconds";
		}
	}


	public class ListStringConverter : StringConverter
	{
		public static List<object> Objects = new List<object>();

		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		public override StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			return new StandardValuesCollection(Objects);
		}
	}

	public class Foo
	{
		public Foo()
		{
			ListStringConverter.Objects = new List<object>() { new Bar("Bar0"), new Bar("Bar1"), new Bar("Bar2") };
		}

		[DisplayName(nameof(SelectedBar)),
		 Browsable(true),
		 TypeConverter(typeof(ListStringConverter))]
		public Bar SelectedBar { get; set; } = null;
	}

	public class Bar
	{
		public string Name;

		public Bar(string name) { Name = name; }

		public override string ToString()
		{
			return Name;
		}
	}
}