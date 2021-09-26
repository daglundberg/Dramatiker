using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ManagedBass;

namespace ManikinMadness.Library
{
	public class AudioPlayer : IDisposable
	{
		Dictionary<AudioItem, int> _playingItems = new();

		public AudioPlayer()
		{
			Bass.Init();
		}

		public void PlayAudioItem(AudioItem item, int fadeInLength = 0)
		{
			int handle = Bass.CreateStream(item.FileName, 0, 0, item.IsLooping? BassFlags.Loop: BassFlags.Default);

			if (fadeInLength <= 0)
				Bass.ChannelSetAttribute(handle, ChannelAttribute.Volume, item.Volume);
			else
			{
				Bass.ChannelSetAttribute(handle, ChannelAttribute.Volume, 0);
				Bass.ChannelSlideAttribute(handle, ChannelAttribute.Volume, item.Volume, fadeInLength, true);
			}

			Bass.ChannelPlay(handle); // Begin Playback.
			
			_playingItems.Add(item, handle);
		}

		public void StopAudioItem(AudioItem item, int fadeOutLength = 0)
		{
			if (_playingItems.ContainsKey(item) == false)
				throw new Exception("That item is not here...");

			_playingItems.TryGetValue(item, out var handle);

			Task.Run(() =>
			{
				if (fadeOutLength > 0)
				{
					Bass.ChannelSlideAttribute(handle, ChannelAttribute.Volume, 0.0f, fadeOutLength, true);
					Thread.Sleep(fadeOutLength + 50);

					while (Bass.ChannelIsSliding(handle, ChannelAttribute.Volume) == true)
					{
						Thread.Sleep(100);
					}
				}
			})
			.ContinueWith(t =>
			{					
				// Fully stop Playback.
				Bass.ChannelStop(handle);
				Bass.StreamFree(handle);
				_playingItems.Remove(item);
			});
		}

		public void Dispose()
		{
			foreach (int handle in _playingItems.Values)
			{
				Bass.StreamFree(handle);
			}

			Bass.Free();
		}
	}
}
