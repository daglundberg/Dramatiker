namespace Dramatiker.Library.Lights.Backends;

/// <summary>
///     Displays
/// </summary>
public class DummyBackend : IDmxBackend
{
	/// <summary>Instantiate Controller.</summary>
	/// <param name="port">COM port to use for communication.</param>
	/// <param name="dmxSize">Number of channels from 24 to 512.</param>
	/// <param name="baudrate">Baudrate for serial connection.</param>
	/// <param name="timeout">Serial connection timeout.</param>
	public DummyBackend(int dmxSize)
	{
		DmxSize = dmxSize;
		if (DmxSize > 512 || DmxSize < 24)
		{
			Console.WriteLine(@"Size of DMX channel frame must be between 24 and 512! Defaulting to 512.");
			DmxSize = 512;
		}

		IsConnected = true;

		Message = CreateMessage();
	}

	public int DmxSize { get; }
	public byte[] Message { get; }

	private Span<byte> Channels => new(Message, 5, DmxSize);

	public void SetChannel(int channel, byte value)
	{
		Channels[channel] = value;
	}

	public void SetColor(Fixture light, Color color)
	{
		var span = Channels.Slice(light.FirstChannel, 4);

		span[0] = color.R;
		span[1] = color.G;
		span[2] = color.B;
		//span[3] = color.A;
	}

	public void ClearChannels()
	{
		Channels.Clear();
	}

	public void AllChannelsOn()
	{
		Channels.Fill(255);
	}

	public void SetAllChannels(byte value)
	{
		Channels.Fill(value);
	}

	public void Close()
	{
		//_serialPort.Close();
	}

	public void Flush()
	{
		LightUpdated?.Invoke(this, EventArgs.Empty);
	}

	public bool IsConnected { get; }

	public event EventHandler? LightUpdated;

	private byte[] CreateMessage()
	{
		var message = new byte[DmxSize + 6];
		message[0] = 0;
		message[1] = 6;
		message[2] = (byte) ((DmxSize + 1) & 0xFF);
		message[3] = (byte) (((DmxSize + 1) >> 8) & 0xFF);
		message[4] = 0;
		message[Message.Length - 1] = 0;
		return message;
	}

	public System.Drawing.Color GetColor(int i)
	{
		var span = Channels.Slice(i, 3);
		return System.Drawing.Color.FromArgb(255, span[0], span[1], span[2]);
	}
}