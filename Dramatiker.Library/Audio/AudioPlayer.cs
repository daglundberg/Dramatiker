using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Dramatiker.Library.Properties;
using ManagedBass;

namespace Dramatiker.Library;

public class AudioPlayer : IDisposable
{
	private readonly List<int> _openHandles = new();
	private readonly Dictionary<AudioItem, int> _playingItems = new();

	public AudioPlayer(int config = -1)
	{
		Console.WriteLine($"Setting up audio with config {config}");

		Bass.Init(config);
	}

	public void Dispose()
	{
		foreach (var handle in _playingItems.Values)
		{
			Bass.ChannelStop(handle);
			Bass.StreamFree(handle);
		}

		foreach (var handle in _openHandles)
		{
			Bass.ChannelStop(handle);
			Bass.StreamFree(handle);
		}

		Bass.Free();
	}

	public void PlayAudioItem(AudioItem item, int fadeInLength = 0)
	{
		var handle = 0;
		if (_playingItems.ContainsKey(item))
		{
			_playingItems.TryGetValue(item, out handle);
			Bass.ChannelStop(handle);
		}
		else
		{
			handle = Bass.CreateStream(item.FileName, 0, 0, item.IsLooping ? BassFlags.Loop : BassFlags.Default);
			_playingItems.Add(item, handle);
		}

		if (fadeInLength <= 0)
		{
			Bass.ChannelSetAttribute(handle, ChannelAttribute.Volume, item.Volume);
		}
		else
		{
			Bass.ChannelSetAttribute(handle, ChannelAttribute.Volume, 0);
			Bass.ChannelSlideAttribute(handle, ChannelAttribute.Volume, item.Volume, fadeInLength, true);
		}

		Bass.ChannelPlay(handle); // Begin Playback.
	}

	public void PlayAudioFile(byte[] file)
	{
		var handle = Bass.CreateStream(file, 1, file.Length - 5, BassFlags.Default);
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

					while (Bass.ChannelIsSliding(handle, ChannelAttribute.Volume)) Thread.Sleep(100);
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
}