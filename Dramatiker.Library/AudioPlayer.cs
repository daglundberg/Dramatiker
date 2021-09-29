using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using Dramatiker.Library.Properties;
using ManagedBass;

namespace Dramatiker.Library
{
	public class AudioPlayer : IDisposable
	{
		Dictionary<AudioItem, int> _playingItems = new();
		List<int> _openHandles = new();

		public AudioPlayer()
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
				Bass.Init();
			else
				Bass.Init(2);
		}

		public void PlayAudioItem(AudioItem item, int fadeInLength = 0)
		{
			int handle = 0;
			if (_playingItems.ContainsKey(item))
			{
				_playingItems.TryGetValue(item, out handle);
				Bass.ChannelStop(handle);
			}
			else
			{
				handle = Bass.CreateStream(item.FileName, 0, 0, item.IsLooping? BassFlags.Loop: BassFlags.Default);
				_playingItems.Add(item, handle);
			}

			if (fadeInLength <= 0)
				Bass.ChannelSetAttribute(handle, ChannelAttribute.Volume, item.Volume);
			else
			{
				Bass.ChannelSetAttribute(handle, ChannelAttribute.Volume, 0);
				Bass.ChannelSlideAttribute(handle, ChannelAttribute.Volume, item.Volume, fadeInLength, true);
			}

			Bass.ChannelPlay(handle); // Begin Playback.
		}

		public void PlayAudioFile(byte[] file)
		{
			int handle = Bass.CreateStream(file, 1, file.Length-5, BassFlags.Default);
			_openHandles.Add(handle);
			Bass.ChannelSetAttribute(handle, ChannelAttribute.Volume, 1);

			Bass.ChannelPlay(handle);
		}

		public void PlayStartUpSound()
		{
			PlayAudioFile(Resources.dramatiker);
		}

		public void StopAudioItem(AudioItem item, int fadeOutLength = 0)
		{
			if (_playingItems.ContainsKey(item) == false)
				return;

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
				Bass.ChannelStop(handle);
				Bass.StreamFree(handle);
			}

			foreach (int handle in _openHandles)
			{
				Bass.ChannelStop(handle);
				Bass.StreamFree(handle);
			}

			Bass.Free();
		}
	}
}
